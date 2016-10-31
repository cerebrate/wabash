#region header

// Wabash - SingleInstance.cs
// 
// Alistair J. R. Young
// Arkane Systems
// 
// Copyright Arkane Systems 2012-2016.  All rights reserved.
// 
// Created: 2016-10-31 8:44 AM

#endregion

#region using

using System ;
using System.Reflection ;
using System.Threading ;
using System.Windows.Forms ;

#endregion

namespace ArkaneSystems.Wabash
{
    public sealed class SingleInstance : IDisposable

    {
        private bool disposed ;
        private Mutex instanceMutex ;
        private readonly string instanceOf ;


        /// <summary>
        ///     Construct a new single-process-instance lock around the main body of an application.
        /// </summary>
        /// <remarks>
        ///     Uses the name of the entrypoint to construct a unique mutex name. You ARE using unique namespaces,
        ///     aren't you?
        /// </remarks>
        /// <exception
        ///     cref="InvalidOperationException">
        ///     Could not find valid entrypoint.
        /// </exception>
        /// <exception
        ///     cref="AbandonedMutexException">
        ///     The wait completed because a thread exited without releasing a mutex.
        ///     This exception is not thrown on Windows 98 or Windows Millennium Edition.
        /// </exception>
        /// <exception
        ///     cref="Exception">
        ///     A delegate callback throws an exception.
        /// </exception>
        public SingleInstance ()
        {
            // Use the entry point as a unique instancing mutex name.
            try
            {
                MethodInfo entryPoint = Assembly.GetEntryAssembly ().EntryPoint ;
                this.instanceOf = $"{entryPoint.DeclaringType?.FullName}.{entryPoint.Name}" ;
            }
            catch (NullReferenceException ex)
            {
                throw new InvalidOperationException ("Couldn't find valid entrypoint.", ex) ;
            }

            // Create the single-instance mutex.
            this.instanceMutex = new Mutex (false, $"single instance: {this.instanceOf}") ;

            // Wait a few seconds if contented, in case another instance of the program is still in the process
            // of shutting down.

            if (!this.instanceMutex.WaitOne (new TimeSpan (0, 0, 3), false))
            {
                // Warn the user.
                MessageBox.Show ("An instance of Wabash is already running.",
                                 "Wabash",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error) ;

                // And exit the app.
                Environment.Exit (128) ;
            }
        }

        #region IDisposable Members

        /// <summary>
        ///     Clean up after SingleInstance; release the mutex.
        /// </summary>
        public void Dispose ()
        {
            if (this.disposed)
                return ;

            this.instanceMutex.Dispose () ;
            this.instanceMutex = null ;
            this.disposed = true ;
        }

        #endregion

        /// <summary>
        ///     Clean up after SingleInstance; release the mutex.
        /// </summary>
        ~SingleInstance () { this.instanceMutex?.Dispose () ; }
    }
}
