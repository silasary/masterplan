using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CreatureAbilityForm : Form
	{
		private ICreature fCreature;

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private TextBox StrModBox;

		private NumericUpDown StrBox;

		private TextBox ChaModBox;

		private NumericUpDown ChaBox;

		private Label StrLbl;

		private Label ChaLbl;

		private TextBox WisModBox;

		private NumericUpDown WisBox;

		private Label WisLbl;

		private TextBox IntModBox;

		private NumericUpDown IntBox;

		private Label ConLbl;

		private Label IntLbl;

		private TextBox DexModBox;

		private NumericUpDown DexBox;

		private NumericUpDown ConBox;

		private Label DexLbl;

		private TextBox ConModBox;

		public ICreature Creature
		{
			get
			{
				return this.fCreature;
			}
		}

		public CreatureAbilityForm(ICreature c)
		{
			this.InitializeComponent();
			this.fCreature = c;
			this.StrBox.Value = this.fCreature.Strength.Score;
			this.ConBox.Value = this.fCreature.Constitution.Score;
			this.DexBox.Value = this.fCreature.Dexterity.Score;
			this.IntBox.Value = this.fCreature.Intelligence.Score;
			this.WisBox.Value = this.fCreature.Wisdom.Score;
			this.ChaBox.Value = this.fCreature.Charisma.Score;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fCreature.Strength.Score = (int)this.StrBox.Value;
			this.fCreature.Constitution.Score = (int)this.ConBox.Value;
			this.fCreature.Dexterity.Score = (int)this.DexBox.Value;
			this.fCreature.Intelligence.Score = (int)this.IntBox.Value;
			this.fCreature.Wisdom.Score = (int)this.WisBox.Value;
			this.fCreature.Charisma.Score = (int)this.ChaBox.Value;
		}

		private void StrBox_ValueChanged(object sender, EventArgs e)
		{
			this.update_mods();
		}

		private void update_mods()
		{
			int arg_0D_0 = this.fCreature.Level / 2;
			this.StrModBox.Text = this.get_text((int)this.StrBox.Value);
			this.ConModBox.Text = this.get_text((int)this.ConBox.Value);
			this.DexModBox.Text = this.get_text((int)this.DexBox.Value);
			this.IntModBox.Text = this.get_text((int)this.IntBox.Value);
			this.WisModBox.Text = this.get_text((int)this.WisBox.Value);
			this.ChaModBox.Text = this.get_text((int)this.ChaBox.Value);
		}

		private string get_text(int score)
		{
			int modifier = Ability.GetModifier(score);
			int num = modifier + this.fCreature.Level / 2;
			string text = modifier.ToString();
			if (modifier >= 0)
			{
				text = "+" + text;
			}
			string text2 = num.ToString();
			if (num >= 0)
			{
				text2 = "+" + text2;
			}
			return text + " / " + text2;
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
			this.StrModBox = new TextBox();
			this.StrBox = new NumericUpDown();
			this.ChaModBox = new TextBox();
			this.ChaBox = new NumericUpDown();
			this.StrLbl = new Label();
			this.ChaLbl = new Label();
			this.WisModBox = new TextBox();
			this.WisBox = new NumericUpDown();
			this.WisLbl = new Label();
			this.IntModBox = new TextBox();
			this.IntBox = new NumericUpDown();
			this.ConLbl = new Label();
			this.IntLbl = new Label();
			this.DexModBox = new TextBox();
			this.DexBox = new NumericUpDown();
			this.ConBox = new NumericUpDown();
			this.DexLbl = new Label();
			this.ConModBox = new TextBox();
			((ISupportInitialize)this.StrBox).BeginInit();
			((ISupportInitialize)this.ChaBox).BeginInit();
			((ISupportInitialize)this.WisBox).BeginInit();
			((ISupportInitialize)this.IntBox).BeginInit();
			((ISupportInitialize)this.DexBox).BeginInit();
			((ISupportInitialize)this.ConBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(149, 181);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 18;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(230, 181);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 19;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.StrModBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.StrModBox.Location = new Point(209, 12);
			this.StrModBox.Name = "StrModBox";
			this.StrModBox.ReadOnly = true;
			this.StrModBox.Size = new Size(96, 20);
			this.StrModBox.TabIndex = 2;
			this.StrModBox.TabStop = false;
			this.StrModBox.Text = "[str]";
			this.StrModBox.TextAlign = HorizontalAlignment.Center;
			this.StrBox.Location = new Point(83, 12);
			this.StrBox.Name = "StrBox";
			this.StrBox.Size = new Size(120, 20);
			this.StrBox.TabIndex = 1;
			this.StrBox.ValueChanged += new EventHandler(this.StrBox_ValueChanged);
			this.ChaModBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ChaModBox.Location = new Point(209, 142);
			this.ChaModBox.Name = "ChaModBox";
			this.ChaModBox.ReadOnly = true;
			this.ChaModBox.Size = new Size(96, 20);
			this.ChaModBox.TabIndex = 17;
			this.ChaModBox.TabStop = false;
			this.ChaModBox.Text = "[cha]";
			this.ChaModBox.TextAlign = HorizontalAlignment.Center;
			this.ChaBox.Location = new Point(83, 142);
			this.ChaBox.Name = "ChaBox";
			this.ChaBox.Size = new Size(120, 20);
			this.ChaBox.TabIndex = 16;
			this.ChaBox.ValueChanged += new EventHandler(this.StrBox_ValueChanged);
			this.StrLbl.AutoSize = true;
			this.StrLbl.Location = new Point(12, 15);
			this.StrLbl.Name = "StrLbl";
			this.StrLbl.Size = new Size(50, 13);
			this.StrLbl.TabIndex = 0;
			this.StrLbl.Text = "Strength:";
			this.ChaLbl.AutoSize = true;
			this.ChaLbl.Location = new Point(12, 145);
			this.ChaLbl.Name = "ChaLbl";
			this.ChaLbl.Size = new Size(53, 13);
			this.ChaLbl.TabIndex = 15;
			this.ChaLbl.Text = "Charisma:";
			this.WisModBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.WisModBox.Location = new Point(209, 116);
			this.WisModBox.Name = "WisModBox";
			this.WisModBox.ReadOnly = true;
			this.WisModBox.Size = new Size(96, 20);
			this.WisModBox.TabIndex = 14;
			this.WisModBox.TabStop = false;
			this.WisModBox.Text = "[wis]";
			this.WisModBox.TextAlign = HorizontalAlignment.Center;
			this.WisBox.Location = new Point(83, 116);
			this.WisBox.Name = "WisBox";
			this.WisBox.Size = new Size(120, 20);
			this.WisBox.TabIndex = 13;
			this.WisBox.ValueChanged += new EventHandler(this.StrBox_ValueChanged);
			this.WisLbl.AutoSize = true;
			this.WisLbl.Location = new Point(12, 119);
			this.WisLbl.Name = "WisLbl";
			this.WisLbl.Size = new Size(48, 13);
			this.WisLbl.TabIndex = 12;
			this.WisLbl.Text = "Wisdom:";
			this.IntModBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.IntModBox.Location = new Point(209, 90);
			this.IntModBox.Name = "IntModBox";
			this.IntModBox.ReadOnly = true;
			this.IntModBox.Size = new Size(96, 20);
			this.IntModBox.TabIndex = 11;
			this.IntModBox.TabStop = false;
			this.IntModBox.Text = "[int]";
			this.IntModBox.TextAlign = HorizontalAlignment.Center;
			this.IntBox.Location = new Point(83, 90);
			this.IntBox.Name = "IntBox";
			this.IntBox.Size = new Size(120, 20);
			this.IntBox.TabIndex = 10;
			this.IntBox.ValueChanged += new EventHandler(this.StrBox_ValueChanged);
			this.ConLbl.AutoSize = true;
			this.ConLbl.Location = new Point(12, 41);
			this.ConLbl.Name = "ConLbl";
			this.ConLbl.Size = new Size(65, 13);
			this.ConLbl.TabIndex = 3;
			this.ConLbl.Text = "Constitution:";
			this.IntLbl.AutoSize = true;
			this.IntLbl.Location = new Point(12, 93);
			this.IntLbl.Name = "IntLbl";
			this.IntLbl.Size = new Size(64, 13);
			this.IntLbl.TabIndex = 9;
			this.IntLbl.Text = "Intelligence:";
			this.DexModBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DexModBox.Location = new Point(209, 64);
			this.DexModBox.Name = "DexModBox";
			this.DexModBox.ReadOnly = true;
			this.DexModBox.Size = new Size(96, 20);
			this.DexModBox.TabIndex = 8;
			this.DexModBox.TabStop = false;
			this.DexModBox.Text = "[dex]";
			this.DexModBox.TextAlign = HorizontalAlignment.Center;
			this.DexBox.Location = new Point(83, 64);
			this.DexBox.Name = "DexBox";
			this.DexBox.Size = new Size(120, 20);
			this.DexBox.TabIndex = 7;
			this.DexBox.ValueChanged += new EventHandler(this.StrBox_ValueChanged);
			this.ConBox.Location = new Point(83, 38);
			this.ConBox.Name = "ConBox";
			this.ConBox.Size = new Size(120, 20);
			this.ConBox.TabIndex = 4;
			this.ConBox.ValueChanged += new EventHandler(this.StrBox_ValueChanged);
			this.DexLbl.AutoSize = true;
			this.DexLbl.Location = new Point(12, 67);
			this.DexLbl.Name = "DexLbl";
			this.DexLbl.Size = new Size(51, 13);
			this.DexLbl.TabIndex = 6;
			this.DexLbl.Text = "Dexterity:";
			this.ConModBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ConModBox.Location = new Point(209, 38);
			this.ConModBox.Name = "ConModBox";
			this.ConModBox.ReadOnly = true;
			this.ConModBox.Size = new Size(96, 20);
			this.ConModBox.TabIndex = 5;
			this.ConModBox.TabStop = false;
			this.ConModBox.Text = "[con]";
			this.ConModBox.TextAlign = HorizontalAlignment.Center;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(317, 216);
			base.Controls.Add(this.StrModBox);
			base.Controls.Add(this.StrBox);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.ChaModBox);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.ChaBox);
			base.Controls.Add(this.StrLbl);
			base.Controls.Add(this.ConModBox);
			base.Controls.Add(this.ChaLbl);
			base.Controls.Add(this.DexLbl);
			base.Controls.Add(this.WisModBox);
			base.Controls.Add(this.ConBox);
			base.Controls.Add(this.WisBox);
			base.Controls.Add(this.DexBox);
			base.Controls.Add(this.WisLbl);
			base.Controls.Add(this.DexModBox);
			base.Controls.Add(this.IntModBox);
			base.Controls.Add(this.IntLbl);
			base.Controls.Add(this.IntBox);
			base.Controls.Add(this.ConLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CreatureAbilityForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Ability Scores";
			((ISupportInitialize)this.StrBox).EndInit();
			((ISupportInitialize)this.ChaBox).EndInit();
			((ISupportInitialize)this.WisBox).EndInit();
			((ISupportInitialize)this.IntBox).EndInit();
			((ISupportInitialize)this.DexBox).EndInit();
			((ISupportInitialize)this.ConBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
