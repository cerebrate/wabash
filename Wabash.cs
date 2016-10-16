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
using System.Windows.Forms ;

#endregion

namespace Wabash
{
    public partial class Wabash : Form
    {
        private bool allowClosing ;

        public Wabash () { this.InitializeComponent () ; }

        private void Wabash_FormClosing (object sender, FormClosingEventArgs e)
        {
            if (!this.allowClosing)
            {
                // Minimize (to tray) instead of closing form.
                e.Cancel = true ;
                this.Hide () ;
            }
        }

        private void notifyIcon_MouseDoubleClick (object sender, MouseEventArgs e) => this.Show () ;

        private void mniOpen_Click (object sender, EventArgs e) => this.Show () ;

        private void mniExit_Click (object sender, EventArgs e)
        {
            this.allowClosing = true ;
            this.Close () ;
        }

        private void Wabash_VisibleChanged(object sender, EventArgs e) => this.mniOpen.Enabled = !this.Visible ;
    }
}
