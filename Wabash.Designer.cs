namespace ArkaneSystems.Wabash
{
    partial class Wabash
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripSeparator mnsSeparator2;
            System.Windows.Forms.ToolStripSeparator mnsSeparator;
            System.Windows.Forms.ToolStripSeparator mnsSeparator3;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wabash));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.iconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniShell = new System.Windows.Forms.ToolStripMenuItem();
            this.mniOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mniPing = new System.Windows.Forms.ToolStripMenuItem();
            this.mniStartup = new System.Windows.Forms.ToolStripMenuItem();
            this.mniExit = new System.Windows.Forms.ToolStripMenuItem();
            this.logBox = new System.Windows.Forms.ListBox();
            mnsSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            mnsSeparator = new System.Windows.Forms.ToolStripSeparator();
            mnsSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.iconMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnsSeparator2
            // 
            mnsSeparator2.Name = "mnsSeparator2";
            mnsSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // mnsSeparator
            // 
            mnsSeparator.Name = "mnsSeparator";
            mnsSeparator.Size = new System.Drawing.Size(145, 6);
            // 
            // mnsSeparator3
            // 
            mnsSeparator3.Name = "mnsSeparator3";
            mnsSeparator3.Size = new System.Drawing.Size(145, 6);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.iconMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Wabash: starting...";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // iconMenu
            // 
            this.iconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniShell,
            mnsSeparator2,
            this.mniOpen,
            this.mniPing,
            mnsSeparator,
            this.mniStartup,
            mnsSeparator3,
            this.mniExit});
            this.iconMenu.Name = "iconMenu";
            this.iconMenu.Size = new System.Drawing.Size(149, 132);
            // 
            // mniShell
            // 
            this.mniShell.Enabled = false;
            this.mniShell.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.mniShell.Name = "mniShell";
            this.mniShell.Size = new System.Drawing.Size(148, 22);
            this.mniShell.Text = "Open &shell";
            this.mniShell.Click += new System.EventHandler(this.mniShell_Click);
            // 
            // mniOpen
            // 
            this.mniOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mniOpen.Name = "mniOpen";
            this.mniOpen.Size = new System.Drawing.Size(148, 22);
            this.mniOpen.Text = "Open &log";
            this.mniOpen.Click += new System.EventHandler(this.mniOpen_Click);
            // 
            // mniPing
            // 
            this.mniPing.Name = "mniPing";
            this.mniPing.Size = new System.Drawing.Size(148, 22);
            this.mniPing.Text = "&Ping daemon";
            this.mniPing.Click += new System.EventHandler(this.mniPing_Click);
            // 
            // mniStartup
            // 
            this.mniStartup.CheckOnClick = true;
            this.mniStartup.Name = "mniStartup";
            this.mniStartup.Size = new System.Drawing.Size(148, 22);
            this.mniStartup.Text = "Run at startup";
            this.mniStartup.CheckedChanged += new System.EventHandler(this.mniStartup_CheckedChanged);
            // 
            // mniExit
            // 
            this.mniExit.Name = "mniExit";
            this.mniExit.Size = new System.Drawing.Size(148, 22);
            this.mniExit.Text = "E&xit";
            this.mniExit.Click += new System.EventHandler(this.mniExit_Click);
            // 
            // logBox
            // 
            this.logBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logBox.Location = new System.Drawing.Point(12, 12);
            this.logBox.Name = "logBox";
            this.logBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.logBox.Size = new System.Drawing.Size(440, 173);
            this.logBox.TabIndex = 0;
            // 
            // Wabash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 201);
            this.Controls.Add(this.logBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(480, 240);
            this.MinimizeBox = false;
            this.Name = "Wabash";
            this.ShowInTaskbar = false;
            this.Text = "Wabash";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Wabash_FormClosing);
            this.Load += new System.EventHandler(this.Wabash_Load);
            this.Shown += new System.EventHandler(this.Wabash_Shown);
            this.VisibleChanged += new System.EventHandler(this.Wabash_VisibleChanged);
            this.iconMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ListBox logBox;
        private System.Windows.Forms.ContextMenuStrip iconMenu;
        private System.Windows.Forms.ToolStripMenuItem mniOpen;
        private System.Windows.Forms.ToolStripMenuItem mniExit;
        private System.Windows.Forms.ToolStripMenuItem mniPing;
        private System.Windows.Forms.ToolStripMenuItem mniShell;
        private System.Windows.Forms.ToolStripMenuItem mniStartup;
    }
}

