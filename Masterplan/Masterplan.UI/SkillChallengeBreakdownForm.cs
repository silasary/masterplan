using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class SkillChallengeBreakdownForm : Form
	{
		private IContainer components;

		private KeyAbilitiesPanel AbilitiesPanel;

		public SkillChallengeBreakdownForm(SkillChallenge sc)
		{
			this.InitializeComponent();
			this.AbilitiesPanel.Analyse(sc);
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
			this.AbilitiesPanel = new KeyAbilitiesPanel();
			base.SuspendLayout();
			this.AbilitiesPanel.Dock = DockStyle.Fill;
			this.AbilitiesPanel.Location = new Point(0, 0);
			this.AbilitiesPanel.Name = "AbilitiesPanel";
			this.AbilitiesPanel.Size = new Size(752, 290);
			this.AbilitiesPanel.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(752, 290);
			base.Controls.Add(this.AbilitiesPanel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SkillChallengeBreakdownForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Skill Challenge Breakdown";
			base.ResumeLayout(false);
		}
	}
}
