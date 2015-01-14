namespace Masterplan.UI
{
	partial class EncounterReportListForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EncounterReportListForm));
			this.Splitter = new System.Windows.Forms.SplitContainer();
			this.EncounterList = new System.Windows.Forms.ListView();
			this.EncounterHdr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Toolbar = new System.Windows.Forms.ToolStrip();
			this.ReportBtn = new System.Windows.Forms.ToolStripButton();
			this.RemoveBtn = new System.Windows.Forms.ToolStripButton();
			this.CloseBtn = new System.Windows.Forms.Button();
			this.ReportBrowser = new System.Windows.Forms.WebBrowser();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.Toolbar.SuspendLayout();
			this.SuspendLayout();
			// 
			// Splitter
			// 
			this.Splitter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.Splitter.Location = new System.Drawing.Point(12, 12);
			this.Splitter.Name = "Splitter";
			// 
			// Splitter.Panel1
			// 
			this.Splitter.Panel1.Controls.Add(this.EncounterList);
			this.Splitter.Panel1.Controls.Add(this.Toolbar);
			// 
			// Splitter.Panel2
			// 
			this.Splitter.Panel2.Controls.Add(this.ReportBrowser);
			this.Splitter.Size = new System.Drawing.Size(696, 328);
			this.Splitter.SplitterDistance = 293;
			this.Splitter.TabIndex = 0;
			// 
			// EncounterList
			// 
			this.EncounterList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.EncounterHdr});
			this.EncounterList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.EncounterList.FullRowSelect = true;
			this.EncounterList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.EncounterList.HideSelection = false;
			this.EncounterList.Location = new System.Drawing.Point(0, 25);
			this.EncounterList.MultiSelect = false;
			this.EncounterList.Name = "EncounterList";
			this.EncounterList.Size = new System.Drawing.Size(293, 303);
			this.EncounterList.TabIndex = 1;
			this.EncounterList.UseCompatibleStateImageBehavior = false;
			this.EncounterList.View = System.Windows.Forms.View.Details;
			this.EncounterList.SelectedIndexChanged += new System.EventHandler(this.EncounterList_SelectedIndexChanged);
			this.EncounterList.DoubleClick += new System.EventHandler(this.RunBtn_Click);
			// 
			// EncounterHdr
			// 
			this.EncounterHdr.Text = "Encounters";
			this.EncounterHdr.Width = 248;
			// 
			// Toolbar
			// 
			this.Toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReportBtn,
            this.RemoveBtn});
			this.Toolbar.Location = new System.Drawing.Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(293, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			// 
			// ReportBtn
			// 
			this.ReportBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.ReportBtn.Image = ((System.Drawing.Image)(resources.GetObject("ReportBtn.Image")));
			this.ReportBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ReportBtn.Name = "ReportBtn";
			this.ReportBtn.Size = new System.Drawing.Size(78, 22);
			this.ReportBtn.Text = "Open Report";
			this.ReportBtn.Click += new System.EventHandler(this.RunBtn_Click);
			// 
			// RemoveBtn
			// 
			this.RemoveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = ((System.Drawing.Image)(resources.GetObject("RemoveBtn.Image")));
			this.RemoveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new System.EventHandler(this.RemoveBtn_Click);
			// 
			// CloseBtn
			// 
			this.CloseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CloseBtn.Location = new System.Drawing.Point(633, 346);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new System.Drawing.Size(75, 23);
			this.CloseBtn.TabIndex = 1;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			// 
			// ReportBrowser
			// 
			this.ReportBrowser.AllowWebBrowserDrop = false;
			this.ReportBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ReportBrowser.IsWebBrowserContextMenuEnabled = false;
			this.ReportBrowser.Location = new System.Drawing.Point(0, 0);
			this.ReportBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.ReportBrowser.Name = "ReportBrowser";
			this.ReportBrowser.ScriptErrorsSuppressed = true;
			this.ReportBrowser.Size = new System.Drawing.Size(399, 328);
			this.ReportBrowser.TabIndex = 0;
			this.ReportBrowser.WebBrowserShortcutsEnabled = false;
			// 
			// EncounterReportListForm
			// 
			this.AcceptButton = this.CloseBtn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(720, 381);
			this.Controls.Add(this.CloseBtn);
			this.Controls.Add(this.Splitter);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EncounterReportListForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Encounter Reports";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel1.PerformLayout();
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer Splitter;
		private System.Windows.Forms.ListView EncounterList;
		private System.Windows.Forms.ColumnHeader EncounterHdr;
		private System.Windows.Forms.ToolStrip Toolbar;
		private System.Windows.Forms.ToolStripButton ReportBtn;
		private System.Windows.Forms.ToolStripButton RemoveBtn;
		private System.Windows.Forms.Button CloseBtn;
		private System.Windows.Forms.WebBrowser ReportBrowser;
	}
}