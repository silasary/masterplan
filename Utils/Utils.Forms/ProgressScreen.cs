using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Utils.Forms
{
    /// <summary>
    /// Form used to display progress of an action.
    /// </summary>
	public partial class ProgressScreen : Form
	{
		private int fActions;

        ///<summary>
        ///Gets or sets the number of actions required for completion.
        ///</summary>

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

        ///<summary>
        ///Gets or sets the text for the current action.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the text for the current action.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the current progress.
        ///</summary>
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

        ///<summary>
        ///Constructor.
        ///</summary>
        ///<param name="title">The title of the screen (not shown).</param>
        ///<param name="actions">The number of actions required for 100% completion.</param>
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
