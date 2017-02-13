using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Utils.Forms
{
	public class ProgressScreen : Form
	{
		private int fActions;

		private IContainer components;

		private Panel SplashPanel;

		private Label ActionLbl;

		private ProgressBar Gauge;

		private Label SubActionLbl;

		public int Actions
		{
			get
			{
				return this.fActions;
			}
			set
			{
				this.fActions = value;
				if (this.fActions == 0)
				{
					this.Gauge.Maximum = 1;
					this.Progress = 1;
					return;
				}
				this.Gauge.Maximum = this.fActions;
			}
		}

		public string CurrentAction
		{
			get
			{
				return this.ActionLbl.Text;
			}
			set
			{
				this.ActionLbl.Text = value;
				this.SubActionLbl.Text = "";
				this.Refresh();
			}
		}

		public string CurrentSubAction
		{
			get
			{
				return this.SubActionLbl.Text;
			}
			set
			{
				this.SubActionLbl.Text = value;
				this.Refresh();
			}
		}

		public int Progress
		{
			get
			{
				return this.Gauge.Value;
			}
			set
			{
				this.Gauge.Value = Math.Min(value, this.Gauge.Maximum);
				this.Refresh();
			}
		}

		public ProgressScreen(string title, int actions)
		{
			this.InitializeComponent();
			this.Text = title;
			this.Actions = actions;
			this.ActionLbl.Text = "Loading...";
			this.SubActionLbl.Text = "";
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
			this.SplashPanel = new Panel();
			this.SubActionLbl = new Label();
			this.ActionLbl = new Label();
			this.Gauge = new ProgressBar();
			this.SplashPanel.SuspendLayout();
			base.SuspendLayout();
			this.SplashPanel.BackColor = SystemColors.Window;
			this.SplashPanel.BorderStyle = BorderStyle.FixedSingle;
			this.SplashPanel.Controls.Add(this.SubActionLbl);
			this.SplashPanel.Controls.Add(this.ActionLbl);
			this.SplashPanel.Controls.Add(this.Gauge);
			this.SplashPanel.Dock = DockStyle.Fill;
			this.SplashPanel.Location = new Point(0, 0);
			this.SplashPanel.Name = "SplashPanel";
			this.SplashPanel.Size = new Size(388, 74);
			this.SplashPanel.TabIndex = 0;
			this.SubActionLbl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SubActionLbl.ForeColor = SystemColors.GrayText;
			this.SubActionLbl.Location = new Point(11, 25);
			this.SubActionLbl.Name = "SubActionLbl";
			this.SubActionLbl.Size = new Size(364, 13);
			this.SubActionLbl.TabIndex = 4;
			this.SubActionLbl.Text = "[sub action]";
			this.ActionLbl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ActionLbl.Location = new Point(11, 8);
			this.ActionLbl.Name = "ActionLbl";
			this.ActionLbl.Size = new Size(364, 13);
			this.ActionLbl.TabIndex = 3;
			this.ActionLbl.Text = "[action]";
			this.Gauge.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Gauge.Location = new Point(10, 48);
			this.Gauge.Maximum = 1;
			this.Gauge.Name = "Gauge";
			this.Gauge.Size = new Size(366, 16);
			this.Gauge.Step = 1;
			this.Gauge.Style = ProgressBarStyle.Continuous;
			this.Gauge.TabIndex = 2;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(388, 74);
			base.ControlBox = false;
			base.Controls.Add(this.SplashPanel);
			base.FormBorderStyle = FormBorderStyle.None;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ProgressScreen";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Progress";
			//base.TopMost = true;
			this.SplashPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
