﻿namespace Masterplan.UI
{
	partial class PowerBuilderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PowerBuilderForm));
            this.Toolbar = new System.Windows.Forms.ToolStrip();
            this.PowerBrowserBtn = new System.Windows.Forms.ToolStripButton();
            this.BtnPnl = new System.Windows.Forms.Panel();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.OKBtn = new System.Windows.Forms.Button();
            this.StatBlockBrowser = new System.Windows.Forms.WebBrowser();
            this.Toolbar.SuspendLayout();
            this.BtnPnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // Toolbar
            // 
            this.Toolbar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PowerBrowserBtn});
            this.Toolbar.Location = new System.Drawing.Point(0, 0);
            this.Toolbar.Name = "Toolbar";
            this.Toolbar.Size = new System.Drawing.Size(885, 27);
            this.Toolbar.TabIndex = 0;
            this.Toolbar.Text = "toolStrip1";
            // 
            // PowerBrowserBtn
            // 
            this.PowerBrowserBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PowerBrowserBtn.Image = ((System.Drawing.Image)(resources.GetObject("PowerBrowserBtn.Image")));
            this.PowerBrowserBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PowerBrowserBtn.Name = "PowerBrowserBtn";
            this.PowerBrowserBtn.Size = new System.Drawing.Size(110, 24);
            this.PowerBrowserBtn.Text = "Power Browser";
            this.PowerBrowserBtn.Click += new System.EventHandler(this.PowerBrowserBtn_Click);
            // 
            // BtnPnl
            // 
            this.BtnPnl.Controls.Add(this.CancelBtn);
            this.BtnPnl.Controls.Add(this.OKBtn);
            this.BtnPnl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnPnl.Location = new System.Drawing.Point(0, 410);
            this.BtnPnl.Margin = new System.Windows.Forms.Padding(4);
            this.BtnPnl.Name = "BtnPnl";
            this.BtnPnl.Size = new System.Drawing.Size(885, 43);
            this.BtnPnl.TabIndex = 2;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(769, 7);
            this.CancelBtn.Margin = new System.Windows.Forms.Padding(4);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(100, 28);
            this.CancelBtn.TabIndex = 1;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // OKBtn
            // 
            this.OKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBtn.Location = new System.Drawing.Point(661, 7);
            this.OKBtn.Margin = new System.Windows.Forms.Padding(4);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(100, 28);
            this.OKBtn.TabIndex = 0;
            this.OKBtn.Text = "OK";
            this.OKBtn.UseVisualStyleBackColor = true;
            // 
            // StatBlockBrowser
            // 
            this.StatBlockBrowser.AllowWebBrowserDrop = false;
            this.StatBlockBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatBlockBrowser.IsWebBrowserContextMenuEnabled = false;
            this.StatBlockBrowser.Location = new System.Drawing.Point(0, 27);
            this.StatBlockBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.StatBlockBrowser.MinimumSize = new System.Drawing.Size(27, 25);
            this.StatBlockBrowser.Name = "StatBlockBrowser";
            this.StatBlockBrowser.ScriptErrorsSuppressed = true;
            this.StatBlockBrowser.Size = new System.Drawing.Size(885, 383);
            this.StatBlockBrowser.TabIndex = 2;
            this.StatBlockBrowser.WebBrowserShortcutsEnabled = false;
            this.StatBlockBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.Browser_Navigating);
            // 
            // PowerBuilderForm
            // 
            this.AcceptButton = this.OKBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(885, 453);
            this.Controls.Add(this.StatBlockBrowser);
            this.Controls.Add(this.BtnPnl);
            this.Controls.Add(this.Toolbar);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "PowerBuilderForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Power Builder";
            this.Toolbar.ResumeLayout(false);
            this.Toolbar.PerformLayout();
            this.BtnPnl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip Toolbar;
		private System.Windows.Forms.Panel BtnPnl;
		private System.Windows.Forms.Button CancelBtn;
		private System.Windows.Forms.Button OKBtn;
		private System.Windows.Forms.WebBrowser StatBlockBrowser;
		private System.Windows.Forms.ToolStripButton PowerBrowserBtn;
	}
}