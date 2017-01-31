using Masterplan.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class InfoForm : Form
	{
		private IContainer components;

		private InfoPanel InfoPanel;

		public int Level
		{
			get
			{
				return this.InfoPanel.Level;
			}
			set
			{
				this.InfoPanel.Level = value;
			}
		}

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
			this.InfoPanel = new InfoPanel();
			base.SuspendLayout();
			this.InfoPanel.Dock = DockStyle.Fill;
			this.InfoPanel.Level = 1;
			this.InfoPanel.Location = new Point(0, 0);
			this.InfoPanel.Name = "InfoPanel";
			this.InfoPanel.Size = new Size(246, 433);
			this.InfoPanel.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(246, 433);
			base.Controls.Add(this.InfoPanel);
			base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "InfoForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Information";
			base.ResumeLayout(false);
		}

		public InfoForm()
		{
			this.InitializeComponent();
			this.InfoPanel.Level = ((Session.Project != null) ? Session.Project.Party.Level : 1);
		}
	}
}
