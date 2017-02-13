using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class DemographicsForm : Form
	{
		private IContainer components;

		private DemographicsPanel BreakdownPanel;

		private ToolStrip Toolbar;

		private ToolStripButton LevelBtn;

		private ToolStripButton RoleBtn;

		private ToolStripButton StatusBtn;

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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(DemographicsForm));
			this.Toolbar = new ToolStrip();
			this.LevelBtn = new ToolStripButton();
			this.RoleBtn = new ToolStripButton();
			this.BreakdownPanel = new DemographicsPanel();
			this.StatusBtn = new ToolStripButton();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.LevelBtn,
				this.RoleBtn,
				this.StatusBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(752, 25);
			this.Toolbar.TabIndex = 1;
			this.Toolbar.Text = "toolStrip1";
			this.LevelBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LevelBtn.Image = (Image)resources.GetObject("LevelBtn.Image");
			this.LevelBtn.ImageTransparentColor = Color.Magenta;
			this.LevelBtn.Name = "LevelBtn";
			this.LevelBtn.Size = new Size(54, 22);
			this.LevelBtn.Text = "By Level";
			this.LevelBtn.Click += new EventHandler(this.LevelBtn_Click);
			this.RoleBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RoleBtn.Image = (Image)resources.GetObject("RoleBtn.Image");
			this.RoleBtn.ImageTransparentColor = Color.Magenta;
			this.RoleBtn.Name = "RoleBtn";
			this.RoleBtn.Size = new Size(50, 22);
			this.RoleBtn.Text = "By Role";
			this.RoleBtn.Click += new EventHandler(this.RoleBtn_Click);
			this.BreakdownPanel.Dock = DockStyle.Fill;
			this.BreakdownPanel.Library = null;
			this.BreakdownPanel.Location = new Point(0, 25);
			this.BreakdownPanel.Mode = DemographicsMode.Level;
			this.BreakdownPanel.Name = "BreakdownPanel";
			this.BreakdownPanel.Size = new Size(752, 265);
			this.BreakdownPanel.TabIndex = 0;
			this.StatusBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.StatusBtn.Image = (Image)resources.GetObject("StatusBtn.Image");
			this.StatusBtn.ImageTransparentColor = Color.Magenta;
			this.StatusBtn.Name = "StatusBtn";
			this.StatusBtn.Size = new Size(59, 22);
			this.StatusBtn.Text = "By Status";
			this.StatusBtn.Click += new EventHandler(this.StatusBtn_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(752, 290);
			base.Controls.Add(this.BreakdownPanel);
			base.Controls.Add(this.Toolbar);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "LibraryBreakdownForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Library Breakdown";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public DemographicsForm(Library library, DemographicsSource source)
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.BreakdownPanel.Library = library;
			this.BreakdownPanel.Source = source;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RoleBtn.Enabled = (this.BreakdownPanel.Source != DemographicsSource.MagicItems);
			this.StatusBtn.Enabled = (this.BreakdownPanel.Source != DemographicsSource.MagicItems);
			this.LevelBtn.Checked = (this.BreakdownPanel.Mode == DemographicsMode.Level);
			this.RoleBtn.Checked = (this.BreakdownPanel.Mode == DemographicsMode.Role);
			this.StatusBtn.Checked = (this.BreakdownPanel.Mode == DemographicsMode.Status);
		}

		private void LevelBtn_Click(object sender, EventArgs e)
		{
			this.BreakdownPanel.Mode = DemographicsMode.Level;
		}

		private void RoleBtn_Click(object sender, EventArgs e)
		{
			this.BreakdownPanel.Mode = DemographicsMode.Role;
		}

		private void StatusBtn_Click(object sender, EventArgs e)
		{
			this.BreakdownPanel.Mode = DemographicsMode.Status;
		}
	}
}
