using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Masterplan.UI
{
    partial class ArtifactBuilderForm
    {
        private IContainer components;

        private Panel BtnPnl;

        private Button CancelBtn;

        private Button OKBtn;

        private WebBrowser StatBlockBrowser;

        private ToolStrip Toolbar;

        private ToolStripDropDownButton FileMenu;

        private ToolStripMenuItem FileImport;

        private ToolStripMenuItem FileExport;


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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ArtifactBuilderForm));
            this.BtnPnl = new Panel();
            this.CancelBtn = new Button();
            this.OKBtn = new Button();
            this.StatBlockBrowser = new WebBrowser();
            this.Toolbar = new ToolStrip();
            this.FileMenu = new ToolStripDropDownButton();
            this.FileImport = new ToolStripMenuItem();
            this.FileExport = new ToolStripMenuItem();
            this.BtnPnl.SuspendLayout();
            this.Toolbar.SuspendLayout();
            base.SuspendLayout();
            this.BtnPnl.Controls.Add(this.CancelBtn);
            this.BtnPnl.Controls.Add(this.OKBtn);
            this.BtnPnl.Dock = DockStyle.Bottom;
            this.BtnPnl.Location = new Point(0, 443);
            this.BtnPnl.Name = "BtnPnl";
            this.BtnPnl.Size = new Size(384, 35);
            this.BtnPnl.TabIndex = 2;
            this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.CancelBtn.DialogResult = DialogResult.Cancel;
            this.CancelBtn.Location = new Point(297, 6);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new Size(75, 23);
            this.CancelBtn.TabIndex = 1;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.OKBtn.DialogResult = DialogResult.OK;
            this.OKBtn.Location = new Point(216, 6);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new Size(75, 23);
            this.OKBtn.TabIndex = 0;
            this.OKBtn.Text = "OK";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.StatBlockBrowser.AllowWebBrowserDrop = false;
            this.StatBlockBrowser.Dock = DockStyle.Fill;
            this.StatBlockBrowser.IsWebBrowserContextMenuEnabled = false;
            this.StatBlockBrowser.Location = new Point(0, 25);
            this.StatBlockBrowser.MinimumSize = new Size(20, 20);
            this.StatBlockBrowser.Name = "StatBlockBrowser";
            this.StatBlockBrowser.ScriptErrorsSuppressed = true;
            this.StatBlockBrowser.Size = new Size(384, 418);
            this.StatBlockBrowser.TabIndex = 2;
            this.StatBlockBrowser.WebBrowserShortcutsEnabled = false;
            this.StatBlockBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.Browser_Navigating);
            this.Toolbar.Items.AddRange(new ToolStripItem[]
            {
                this.FileMenu
            });
            this.Toolbar.Location = new Point(0, 0);
            this.Toolbar.Name = "Toolbar";
            this.Toolbar.Size = new Size(384, 25);
            this.Toolbar.TabIndex = 0;
            this.Toolbar.Text = "toolStrip1";
            this.FileMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.FileMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.FileImport,
                this.FileExport
            });
            this.FileMenu.Image = (Image)resources.GetObject("FileMenu.Image");
            this.FileMenu.ImageTransparentColor = Color.Magenta;
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new Size(38, 22);
            this.FileMenu.Text = "File";
            this.FileImport.Name = "FileImport";
            this.FileImport.Size = new Size(119, 22);
            this.FileImport.Text = "Import...";
            this.FileImport.Click += new EventHandler(this.FileImport_Click);
            this.FileExport.Name = "FileExport";
            this.FileExport.Size = new Size(119, 22);
            this.FileExport.Text = "Export...";
            this.FileExport.Click += new EventHandler(this.FileExport_Click);
            base.AcceptButton = this.OKBtn;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.CancelBtn;
            base.ClientSize = new Size(384, 478);
            base.Controls.Add(this.StatBlockBrowser);
            base.Controls.Add(this.BtnPnl);
            base.Controls.Add(this.Toolbar);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ArtifactBuilderForm";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Artifact Builder";
            this.BtnPnl.ResumeLayout(false);
            this.Toolbar.ResumeLayout(false);
            this.Toolbar.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

    }
}
