#region header

// Wabash - Program.cs
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
using System.Windows.Forms ;

#endregion

namespace ArkaneSystems.Wabash
{
    static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main ()
        {
            Application.EnableVisualStyles () ;
            Application.SetCompatibleTextRenderingDefault (false) ;
            Application.Run (new Wabash ()) ;
        }
    }
}
