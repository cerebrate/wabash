#region header

// Wabash - DaemonManager.cs
// 
// Alistair J. R. Young
// Arkane Systems
// 
// Copyright Arkane Systems 2012-2016.  All rights reserved.
// 
// Created: 2016-10-16 1:13 AM

#endregion

#region using

using System ;
using System.Diagnostics ;
using System.IO ;
using System.Threading ;
using System.Threading.Tasks ;

using ArkaneSystems.Wabash.Properties ;

using PostSharp.Patterns.Model ;
using PostSharp.Patterns.Threading ;

#endregion

#region header

// Wabash - DaemonManager.cs
// 
// Alistair J. R. Young
// Arkane Systems
// 
// Copyright Arkane Systems 2012-2016.  All rights reserved.
// 
// Created: 2016-10-16 1:13 AM

#endregion

namespace ArkaneSystems.Wabash
{
    [Synchronized]
    public sealed class DaemonManager
    {
        private const string WslProcess = @"bash.exe" ;
        private const string WslArguments = @"-c /usr/bin/start-wabashd" ;

        private const int CompatibleDaemonVersion = 4 ;

        [Reference]
        private readonly Wabash owner ;

        [Reference]
        private StreamWriter channel ;

        [Reference]
        private Process wabashd ;

        public DaemonManager (Wabash owner) { this.owner = owner ; }

        // Start the daemon.
        [Reentrant]
        public async Task Start ()
        {
            // Start the WSL process.
            this.wabashd = Process.Start (new ProcessStartInfo
                                          {
                                              FileName =
                                                  Path.Combine (
                                                                Environment.GetFolderPath (
                                                                                           Environment.SpecialFolder
                                                                                                      .System),
                                                                DaemonManager.WslProcess),
                                              Arguments = DaemonManager.WslArguments,
                                              UseShellExecute = false,
                                              CreateNoWindow = true,
                                              RedirectStandardInput = true,
                                              RedirectStandardOutput = true
                                          }) ;

            Thread.Sleep (500) ;

            if (this.wabashd.HasExited)
            {
                this.owner.Die (
                                "Could not start wabashd. Is it already running? If not, is an old lock file (/tmp/wabashd.exe.lock) present?") ;
                return ;
            }

            // Start the reader loop listener.
            this.InputReader (this.wabashd.StandardOutput) ;

            // Set the channel.
            this.channel = this.wabashd.StandardInput ;

            // Fire up the server-starter loop.
            this.StartServices () ;
        }

        [Background]
        private void StartServices ()
        {
            foreach (string service in Settings.Default.Services)
            {
                this.channel.WriteLine ($"strt {service}") ;
                Thread.Sleep(250);
            }
        }

        // Stop the daemon.
        [Reentrant]
        public async Task Stop () => this.channel.WriteLine ("stop") ;

        // Ping the daemon.
        [Reentrant]
        public async Task Ping () => this.channel.WriteLine ("ping") ;

        // Daemon input reader loop function.
        [Background]
        [ExplicitlySynchronized]
        private void InputReader (StreamReader output)
        {
            while (true)
            {
                string recv = output.ReadLine ().Trim () ;

                // Place the string in the log window
                this.owner.WriteLogString (recv) ;

                // Check length
                if (recv.Length < 4)
                    continue ;

                // Action the message
                string action = recv.Substring (0, 4) ;
                string[] split = recv.Split (' ') ;

                switch (action)
                {
                    case "vers":
                        // version message
                        int version = int.Parse (split[1]) ;

                        if (version != DaemonManager.CompatibleDaemonVersion)
                        {
                            this.owner.Die ("Incompatible daemon version detected.") ;
                        }

                        break ;

                    case "sess":
                        // status message
                        int sessions = int.Parse (split[1]) ;
                        int daemons = int.Parse (split[3]) ;

                        this.owner.UpdateCounts (sessions, daemons) ;

                        break ;

                    case "stop":
                        // termination message
                        this.owner.Exit () ;

                        // needed to prevent exception when thread terminates.
                        Thread.CurrentThread.Suspend () ;
                        continue ;

                    case "pong":
                        // ping response
                        this.owner.Message ("Successfully pinged daemon.") ;

                        break ;

                    case "svup":
                        // service start response
                        this.owner.Message (recv.Remove (0, 5)) ;
                        break ;

                    case "shel":
                        // Shell identification
                        this.owner.Shell = split[1] ;
                        break ;

                    default:
                        // error, unknown, or garbled message
                        try
                        {
                            this.wabashd.Kill () ;
                        }
                        catch
                        {
                            // ignored; if we can't kill it, or its already dead, we can't do anything.
                        }

                        this.owner.Die ($"Error, unknown message, or garbled channel: {recv}") ;

                        // needed to prevent exception when thread terminates.
                        Thread.CurrentThread.Suspend () ;
                        continue ;
                }
            }

            // If we fall through the while, we exit function and quit the thread.
        }
    }
}
