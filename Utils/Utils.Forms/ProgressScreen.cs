using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Utils.Forms
{
	public partial class ProgressScreen : Form
	{
		private int fActions;

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

	}
}
