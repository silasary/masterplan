using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.Controls
{
    partial class WelcomePanel
    {

        private IContainer components;

        private TitlePanel TitlePanel;

        private WebBrowser MenuBrowser;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.MenuBrowser = new WebBrowser();
            this.TitlePanel = new TitlePanel();
            base.SuspendLayout();
            this.MenuBrowser.Dock = DockStyle.Right;
            this.MenuBrowser.IsWebBrowserContextMenuEnabled = false;
            this.MenuBrowser.Location = new Point(364, 0);
            this.MenuBrowser.MinimumSize = new Size(20, 20);
            this.MenuBrowser.Name = "MenuBrowser";
            this.MenuBrowser.ScriptErrorsSuppressed = true;
            this.MenuBrowser.Size = new Size(345, 429);
            this.MenuBrowser.TabIndex = 5;
            this.MenuBrowser.WebBrowserShortcutsEnabled = false;
            this.MenuBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.MenuBrowser_Navigating);
            this.TitlePanel.Dock = DockStyle.Fill;
            this.TitlePanel.Font = new Font("Calibri", 11f);
            this.TitlePanel.ForeColor = Color.MidnightBlue;
            this.TitlePanel.Location = new Point(0, 0);
            this.TitlePanel.Margin = new Padding(3, 4, 3, 4);
            this.TitlePanel.Mode = TitlePanel.TitlePanelMode.WelcomeScreen;
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new Size(364, 429);
            this.TitlePanel.TabIndex = 4;
            this.TitlePanel.Title = "Masterplan";
            this.TitlePanel.Zooming = false;
            this.TitlePanel.FadeFinished += new EventHandler(this.TitlePanel_FadeFinished);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            base.Controls.Add(this.TitlePanel);
            base.Controls.Add(this.MenuBrowser);
            base.Name = "WelcomePanel";
            base.Size = new Size(709, 429);
            base.ResumeLayout(false);
        }

    }
}
