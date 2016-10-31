#region header

// Wabash - Wabash.cs
// 
// Alistair J. R. Young
// Arkane Systems
// 
// Copyright Arkane Systems 2012-2016.  All rights reserved.
// 
// Created: 2016-10-15 8:45 PM

#endregion

#region using

using System ;
using System.Diagnostics ;
using System.IO ;
using System.Windows.Forms ;

using PostSharp.Patterns.Threading ;

#endregion

namespace ArkaneSystems.Wabash
{
    public sealed partial class Wabash : Form
    {
        private const string WslProcess = @"bash.exe";

        private bool allowClosing ;
        private readonly DaemonManager daemon ;
        private string shell = null ;
        private object shellLock = new object () ;

        public string Shell
        {
            get
            {
                lock (this.shellLock)
                {
                    return this.shell ;
                }
            }
            set
            {
                lock (this.shellLock)
                {
                    this.shell = value ;
                    this.mniShell.Enabled = true ;
                }
            }
        }

        public Wabash ()
        {
            this.InitializeComponent () ;
            this.daemon = new DaemonManager (this) ;
        }

        private void Wabash_FormClosing (object sender, FormClosingEventArgs e)
        {
            if (!this.allowClosing)
            {
                // Minimize (to tray) instead of closing form.
                e.Cancel = true ;
                this.Hide () ;
            }
        }

        private void notifyIcon_MouseDoubleClick (object sender, MouseEventArgs e) => this.OpenShell () ;

        private void mniShell_Click(object sender, EventArgs e) => this.OpenShell() ;

        private void mniOpen_Click (object sender, EventArgs e) => this.Show () ;

        private void mniExit_Click (object sender, EventArgs e) => this.daemon.Stop () ;

        private void Wabash_VisibleChanged (object sender, EventArgs e) => this.mniOpen.Enabled = !this.Visible ;

        private void Wabash_Load (object sender, EventArgs e) => this.daemon.Start () ;

        private void mniPing_Click (object sender, EventArgs e) => this.daemon.Ping () ;

        [Dispatched (true)]
        public void WriteLogString (string text)
        {
            // Trim list box if necessary.
            if (this.logBox.Items.Count == 1000)
                this.logBox.Items.RemoveAt (999) ;

            // Timestamp the message and add it.
            this.logBox.Items.Insert (0, $"{DateTime.Now:T}: {text}") ;
        }

        [Dispatched (true)]
        public void Message (string message)
            => this.notifyIcon.ShowBalloonTip (1000, "Wabash", message, ToolTipIcon.Info) ;

        [Dispatched (true)]
        public void Die (string error)
        {
            string message = $@"{error}

Terminating. wabashd may need to be terminated separately; if so, use:

kill -TERM <pid>" ;

            MessageBox.Show (this, message, "Wabash", MessageBoxButtons.OK, MessageBoxIcon.Error) ;

            this.allowClosing = true ;
            this.Close () ;
        }

        [Dispatched (true)]
        public void UpdateCounts (int sessions, int daemons)
        {
            this.notifyIcon.Text = $@"Wabash: {sessions} sessions / {daemons} daemons" ;
        }

        [Dispatched (true)]
        public void Exit ()
        {
            this.allowClosing = true ;
            this.Close () ;
        }

        private void Wabash_Shown (object sender, EventArgs e) => this.Hide () ;

        [Dispatched]
        private void OpenShell ()
        {
            if (this.Shell == null)
                return ;

            // Start the shell process.
            Process.Start(new ProcessStartInfo
            {
                FileName =
                                                  Path.Combine(
                                                                Environment.GetFolderPath(
                                                                                           Environment.SpecialFolder
                                                                                                      .System),
                                                                Wabash.WslProcess),
                Arguments = $"~ -c \"{this.Shell} -l\"",
                UseShellExecute = true
            });
        }
    }
}
