using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class HealForm : Form
	{
		private List<Pair<CombatData, EncounterCard>> fTokens;

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label HPLbl;

		private NumericUpDown HPBox;

		private CheckBox TempHPBox;

		private Label SurgeLbl;

		private NumericUpDown SurgeBox;

		public HealForm(List<Pair<CombatData, EncounterCard>> tokens)
		{
			this.InitializeComponent();
			this.fTokens = tokens;
		}

		private void DamageForm_Shown(object sender, EventArgs e)
		{
			this.SurgeBox.Select(0, 1);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			int num = (int)this.SurgeBox.Value;
			int num2 = (int)this.HPBox.Value;
			foreach (Pair<CombatData, EncounterCard> current in this.fTokens)
			{
				int num3 = 0;
				if (current.Second != null)
				{
					num3 = current.Second.HP;
				}
				else
				{
					Hero hero = Session.Project.FindHero(current.First.ID);
					if (hero != null)
					{
						num3 = hero.HP;
					}
				}
				int num4 = num3 / 4;
				int num5 = num4 * num + num2;
				if (this.TempHPBox.Checked)
				{
					current.First.TempHP = Math.Max(num5, current.First.TempHP);
				}
				else
				{
					if (current.First.Damage > num3)
					{
						current.First.Damage = num3;
					}
					current.First.Damage -= num5;
					if (current.First.Damage < 0)
					{
						current.First.Damage = 0;
					}
				}
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
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.HPLbl = new Label();
			this.HPBox = new NumericUpDown();
			this.TempHPBox = new CheckBox();
			this.SurgeLbl = new Label();
			this.SurgeBox = new NumericUpDown();
			((ISupportInitialize)this.HPBox).BeginInit();
			((ISupportInitialize)this.SurgeBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(116, 110);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 5;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(197, 110);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 6;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.HPLbl.AutoSize = true;
			this.HPLbl.Location = new Point(12, 40);
			this.HPLbl.Name = "HPLbl";
			this.HPLbl.Size = new Size(25, 13);
			this.HPLbl.TabIndex = 2;
			this.HPLbl.Text = "HP:";
			this.HPBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.HPBox.Location = new Point(68, 38);
			this.HPBox.Maximum = 1000;
			this.HPBox.Name = "HPBox";
			this.HPBox.Size = new Size(204, 20);
			this.HPBox.TabIndex = 3;
			this.TempHPBox.AutoSize = true;
			this.TempHPBox.Location = new Point(68, 75);
			this.TempHPBox.Name = "TempHPBox";
			this.TempHPBox.Size = new Size(153, 17);
			this.TempHPBox.TabIndex = 4;
			this.TempHPBox.Text = "Add as temporary hit points";
			this.TempHPBox.UseVisualStyleBackColor = true;
			this.SurgeLbl.AutoSize = true;
			this.SurgeLbl.Location = new Point(12, 14);
			this.SurgeLbl.Name = "SurgeLbl";
			this.SurgeLbl.Size = new Size(43, 13);
			this.SurgeLbl.TabIndex = 0;
			this.SurgeLbl.Text = "Surges:";
			this.SurgeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SurgeBox.Location = new Point(68, 12);
			this.SurgeBox.Maximum = 10;
			this.SurgeBox.Name = "SurgeBox";
			this.SurgeBox.Size = new Size(204, 20);
			this.SurgeBox.TabIndex = 1;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(284, 145);
			base.Controls.Add(this.SurgeBox);
			base.Controls.Add(this.SurgeLbl);
			base.Controls.Add(this.TempHPBox);
			base.Controls.Add(this.HPBox);
			base.Controls.Add(this.HPLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "HealForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Heal";
			base.Shown += new EventHandler(this.DamageForm_Shown);
			((ISupportInitialize)this.HPBox).EndInit();
			((ISupportInitialize)this.SurgeBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
