using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CreatureSkillForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private CheckBox TrainedBox;

		private Label AbilityNameLbl;

		private Label AbilityBonusLbl;

		private Label LevelLbl;

		private Label LevelBonusLbl;

		private Label MiscLbl;

		private NumericUpDown MiscBox;

		private Label TotalLbl;

		private Label TotalBonusLbl;

		private Label TrainingBonusLbl;

		private int fAbility;

		private int fLevel;

		public bool Trained
		{
			get
			{
				return this.TrainedBox.Checked;
			}
		}

		public int Misc
		{
			get
			{
				return (int)this.MiscBox.Value;
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
			this.TrainedBox = new CheckBox();
			this.AbilityNameLbl = new Label();
			this.AbilityBonusLbl = new Label();
			this.LevelLbl = new Label();
			this.LevelBonusLbl = new Label();
			this.MiscLbl = new Label();
			this.MiscBox = new NumericUpDown();
			this.TotalLbl = new Label();
			this.TotalBonusLbl = new Label();
			this.TrainingBonusLbl = new Label();
			((ISupportInitialize)this.MiscBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(31, 138);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 9;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(112, 138);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 10;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.TrainedBox.AutoSize = true;
			this.TrainedBox.Location = new Point(12, 51);
			this.TrainedBox.Name = "TrainedBox";
			this.TrainedBox.Size = new Size(62, 17);
			this.TrainedBox.TabIndex = 4;
			this.TrainedBox.Text = "Trained";
			this.TrainedBox.UseVisualStyleBackColor = true;
			this.TrainedBox.CheckedChanged += new EventHandler(this.TrainedBox_CheckedChanged);
			this.AbilityNameLbl.AutoSize = true;
			this.AbilityNameLbl.Location = new Point(12, 9);
			this.AbilityNameLbl.Name = "AbilityNameLbl";
			this.AbilityNameLbl.Size = new Size(39, 13);
			this.AbilityNameLbl.TabIndex = 0;
			this.AbilityNameLbl.Text = "[ability]";
			this.AbilityBonusLbl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.AbilityBonusLbl.Location = new Point(113, 9);
			this.AbilityBonusLbl.Name = "AbilityBonusLbl";
			this.AbilityBonusLbl.Size = new Size(74, 13);
			this.AbilityBonusLbl.TabIndex = 1;
			this.AbilityBonusLbl.Text = "[ability bonus]";
			this.AbilityBonusLbl.TextAlign = ContentAlignment.TopRight;
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(12, 30);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(54, 13);
			this.LevelLbl.TabIndex = 2;
			this.LevelLbl.Text = "Half level:";
			this.LevelBonusLbl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LevelBonusLbl.Location = new Point(113, 30);
			this.LevelBonusLbl.Name = "LevelBonusLbl";
			this.LevelBonusLbl.Size = new Size(74, 13);
			this.LevelBonusLbl.TabIndex = 3;
			this.LevelBonusLbl.Text = "[level bonus]";
			this.LevelBonusLbl.TextAlign = ContentAlignment.TopRight;
			this.MiscLbl.AutoSize = true;
			this.MiscLbl.Location = new Point(12, 75);
			this.MiscLbl.Name = "MiscLbl";
			this.MiscLbl.Size = new Size(64, 13);
			this.MiscLbl.TabIndex = 5;
			this.MiscLbl.Text = "Misc bonus:";
			this.MiscBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.MiscBox.Location = new Point(116, 73);
			this.MiscBox.Minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				int.MinValue
			});
			this.MiscBox.Name = "MiscBox";
			this.MiscBox.Size = new Size(71, 20);
			this.MiscBox.TabIndex = 6;
			this.MiscBox.TextAlign = HorizontalAlignment.Right;
			this.MiscBox.ValueChanged += new EventHandler(this.MiscBox_ValueChanged);
			this.TotalLbl.AutoSize = true;
			this.TotalLbl.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.TotalLbl.Location = new Point(12, 100);
			this.TotalLbl.Name = "TotalLbl";
			this.TotalLbl.Size = new Size(40, 13);
			this.TotalLbl.TabIndex = 7;
			this.TotalLbl.Text = "Total:";
			this.TotalBonusLbl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TotalBonusLbl.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.TotalBonusLbl.Location = new Point(113, 100);
			this.TotalBonusLbl.Name = "TotalBonusLbl";
			this.TotalBonusLbl.Size = new Size(74, 13);
			this.TotalBonusLbl.TabIndex = 8;
			this.TotalBonusLbl.Text = "[total]";
			this.TotalBonusLbl.TextAlign = ContentAlignment.TopRight;
			this.TrainingBonusLbl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TrainingBonusLbl.Location = new Point(113, 51);
			this.TrainingBonusLbl.Name = "TrainingBonusLbl";
			this.TrainingBonusLbl.Size = new Size(74, 13);
			this.TrainingBonusLbl.TabIndex = 11;
			this.TrainingBonusLbl.Text = "[train bonus]";
			this.TrainingBonusLbl.TextAlign = ContentAlignment.TopRight;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(199, 173);
			base.Controls.Add(this.TrainingBonusLbl);
			base.Controls.Add(this.TotalBonusLbl);
			base.Controls.Add(this.TotalLbl);
			base.Controls.Add(this.MiscBox);
			base.Controls.Add(this.MiscLbl);
			base.Controls.Add(this.LevelBonusLbl);
			base.Controls.Add(this.LevelLbl);
			base.Controls.Add(this.AbilityBonusLbl);
			base.Controls.Add(this.AbilityNameLbl);
			base.Controls.Add(this.TrainedBox);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CreatureSkillForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Creature Skill";
			((ISupportInitialize)this.MiscBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public CreatureSkillForm(string skill_name, string ability_name, int ability_bonus, int level_bonus, bool trained, int misc_bonus)
		{
			this.InitializeComponent();
			this.fAbility = ability_bonus;
			this.fLevel = level_bonus;
			this.Text = skill_name;
			this.AbilityNameLbl.Text = ability_name + " bonus:";
			this.AbilityBonusLbl.Text = this.fAbility.ToString();
			this.LevelBonusLbl.Text = this.fLevel.ToString();
			this.TrainedBox.Checked = trained;
			this.MiscBox.Value = misc_bonus;
			this.update_total();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}

		private void update_total()
		{
			int num = this.TrainedBox.Checked ? 5 : 0;
			int num2 = this.fAbility + this.fLevel + num + this.Misc;
			this.TrainingBonusLbl.Text = (this.TrainedBox.Checked ? "5" : "");
			this.TotalBonusLbl.Text = num2.ToString();
		}

		private void TrainedBox_CheckedChanged(object sender, EventArgs e)
		{
			this.update_total();
		}

		private void MiscBox_ValueChanged(object sender, EventArgs e)
		{
			this.update_total();
		}
	}
}
