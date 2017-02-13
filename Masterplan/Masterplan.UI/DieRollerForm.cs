using Masterplan.Controls;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class DieRollerForm : Form
	{
		private IContainer components;

		private DicePanel DicePanel;

		public DiceExpression Expression
		{
			get
			{
				return this.DicePanel.Expression;
			}
			set
			{
				this.DicePanel.Expression = value;
			}
		}

		public DieRollerForm()
		{
			this.InitializeComponent();
			this.DicePanel.UpdateView();
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
			this.DicePanel = new DicePanel();
			base.SuspendLayout();
			this.DicePanel.Dock = DockStyle.Fill;
			this.DicePanel.Expression = null;
			this.DicePanel.Location = new Point(0, 0);
			this.DicePanel.Name = "DicePanel";
			this.DicePanel.Size = new Size(247, 372);
			this.DicePanel.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(247, 372);
			base.Controls.Add(this.DicePanel);
			base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DieRollerForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Die Roller";
			base.ResumeLayout(false);
		}
	}
}
