using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CreatureStatsForm : Form
	{
		private ICreature fCreature;

		private int fHP;

		private int fInit;

		private int fAC;

		private int fNAD;

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label InitLbl;

		private Label FortLbl;

		private NumericUpDown FortBox;

		private NumericUpDown InitBox;

		private NumericUpDown WillBox;

		private Label HPLbl;

		private NumericUpDown ACBox;

		private Label WillLbl;

		private Label RefLbl;

		private NumericUpDown HPBox;

		private Label ACLbl;

		private NumericUpDown RefBox;

		private GroupBox InitGroup;

		private GroupBox HPGroup;

		private GroupBox DefGroup;

		private Button DefaultBtn;

		private Button InitRecBtn;

		private Button HPRecBtn;

		private Button WillRecBtn;

		private Button RefRecBtn;

		private Button FortRecBtn;

		private Button ACRecBtn;

		public ICreature Creature
		{
			get
			{
				return this.fCreature;
			}
		}

		public CreatureStatsForm(ICreature c)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fCreature = c;
			if (this.fCreature.Role != null && this.fCreature.Role is Minion)
			{
				this.HPBox.Value = 1m;
				this.HPGroup.Enabled = false;
			}
			else
			{
				this.HPBox.Value = this.fCreature.HP;
			}
			this.InitBox.Value = this.fCreature.Initiative;
			this.ACBox.Value = this.fCreature.AC;
			this.FortBox.Value = this.fCreature.Fortitude;
			this.RefBox.Value = this.fCreature.Reflex;
			this.WillBox.Value = this.fCreature.Will;
			this.update_recommendations();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.HPRecBtn.Enabled = (this.HPBox.Value != this.fHP);
			this.InitRecBtn.Enabled = (this.InitBox.Value != this.fInit);
			this.ACRecBtn.Enabled = (this.ACBox.Value != this.fAC);
			this.FortRecBtn.Enabled = (this.FortBox.Value != this.fNAD);
			this.RefRecBtn.Enabled = (this.RefBox.Value != this.fNAD);
			this.WillRecBtn.Enabled = (this.WillBox.Value != this.fNAD);
			this.DefaultBtn.Enabled = (this.HPRecBtn.Enabled || this.InitRecBtn.Enabled || this.ACRecBtn.Enabled || this.FortRecBtn.Enabled || this.RefRecBtn.Enabled || this.WillRecBtn.Enabled);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			if (this.fCreature.Role is ComplexRole)
			{
				this.fCreature.HP = (int)this.HPBox.Value;
			}
			this.fCreature.Initiative = (int)this.InitBox.Value;
			this.fCreature.AC = (int)this.ACBox.Value;
			this.fCreature.Fortitude = (int)this.FortBox.Value;
			this.fCreature.Reflex = (int)this.RefBox.Value;
			this.fCreature.Will = (int)this.WillBox.Value;
		}

		private void update_recommendations()
		{
			bool flag = this.fCreature.Role != null && this.fCreature.Role is Minion;
			this.fHP = (flag ? 1 : Statistics.HP(this.fCreature.Level, this.fCreature.Role as ComplexRole, this.fCreature.Constitution.Score));
			this.fInit = Statistics.Initiative(this.fCreature.Level, this.fCreature.Role);
			this.fAC = Statistics.AC(this.fCreature.Level, this.fCreature.Role);
			this.fNAD = Statistics.NAD(this.fCreature.Level, this.fCreature.Role);
			this.HPRecBtn.Text = (flag ? "-" : ("Recommended: " + this.fHP));
			this.InitRecBtn.Text = "Recommended: " + this.fInit;
			this.ACRecBtn.Text = "Recommended: " + this.fAC;
			this.FortRecBtn.Text = "Recommended: " + this.fNAD;
			this.RefRecBtn.Text = "Recommended: " + this.fNAD;
			this.WillRecBtn.Text = "Recommended: " + this.fNAD;
		}

		private void DefaultBtn_Click(object sender, EventArgs e)
		{
			this.HPBox.Value = this.fHP;
			this.InitBox.Value = this.fInit;
			this.ACBox.Value = this.fAC;
			this.FortBox.Value = this.fNAD;
			this.RefBox.Value = this.fNAD;
			this.WillBox.Value = this.fNAD;
		}

		private void HPRecBtn_Click(object sender, EventArgs e)
		{
			this.HPBox.Value = this.fHP;
		}

		private void InitRecBtn_Click(object sender, EventArgs e)
		{
			this.InitBox.Value = this.fInit;
		}

		private void ACRecBtn_Click(object sender, EventArgs e)
		{
			this.ACBox.Value = this.fAC;
		}

		private void FortRecBtn_Click(object sender, EventArgs e)
		{
			this.FortBox.Value = this.fNAD;
		}

		private void RefRecBtn_Click(object sender, EventArgs e)
		{
			this.RefBox.Value = this.fNAD;
		}

		private void WillRecBtn_Click(object sender, EventArgs e)
		{
			this.WillBox.Value = this.fNAD;
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
			this.InitLbl = new Label();
			this.FortLbl = new Label();
			this.FortBox = new NumericUpDown();
			this.InitBox = new NumericUpDown();
			this.WillBox = new NumericUpDown();
			this.HPLbl = new Label();
			this.ACBox = new NumericUpDown();
			this.WillLbl = new Label();
			this.RefLbl = new Label();
			this.HPBox = new NumericUpDown();
			this.ACLbl = new Label();
			this.RefBox = new NumericUpDown();
			this.InitGroup = new GroupBox();
			this.InitRecBtn = new Button();
			this.HPGroup = new GroupBox();
			this.HPRecBtn = new Button();
			this.DefGroup = new GroupBox();
			this.WillRecBtn = new Button();
			this.RefRecBtn = new Button();
			this.FortRecBtn = new Button();
			this.ACRecBtn = new Button();
			this.DefaultBtn = new Button();
			((ISupportInitialize)this.FortBox).BeginInit();
			((ISupportInitialize)this.InitBox).BeginInit();
			((ISupportInitialize)this.WillBox).BeginInit();
			((ISupportInitialize)this.ACBox).BeginInit();
			((ISupportInitialize)this.HPBox).BeginInit();
			((ISupportInitialize)this.RefBox).BeginInit();
			this.InitGroup.SuspendLayout();
			this.HPGroup.SuspendLayout();
			this.DefGroup.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(193, 261);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(274, 261);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.InitLbl.AutoSize = true;
			this.InitLbl.Location = new Point(6, 21);
			this.InitLbl.Name = "InitLbl";
			this.InitLbl.Size = new Size(49, 13);
			this.InitLbl.TabIndex = 0;
			this.InitLbl.Text = "Initiative:";
			this.FortLbl.AutoSize = true;
			this.FortLbl.Location = new Point(6, 48);
			this.FortLbl.Name = "FortLbl";
			this.FortLbl.Size = new Size(51, 13);
			this.FortLbl.TabIndex = 3;
			this.FortLbl.Text = "Fortitude:";
			this.FortBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.FortBox.Location = new Point(67, 45);
			NumericUpDown arg_380_0 = this.FortBox;
			int[] array = new int[4];
			array[0] = 1000;
			arg_380_0.Maximum = new decimal(array);
			this.FortBox.Name = "FortBox";
			this.FortBox.Size = new Size(141, 20);
			this.FortBox.TabIndex = 4;
			this.InitBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.InitBox.Location = new Point(67, 19);
			NumericUpDown arg_3F4_0 = this.InitBox;
			int[] array2 = new int[4];
			array2[0] = 1000;
			arg_3F4_0.Maximum = new decimal(array2);
			this.InitBox.Minimum = new decimal(new int[]
			{
				1000,
				0,
				0,
				int.MinValue
			});
			this.InitBox.Name = "InitBox";
			this.InitBox.Size = new Size(141, 20);
			this.InitBox.TabIndex = 1;
			this.WillBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.WillBox.Location = new Point(67, 97);
			NumericUpDown arg_490_0 = this.WillBox;
			int[] array3 = new int[4];
			array3[0] = 1000;
			arg_490_0.Maximum = new decimal(array3);
			this.WillBox.Name = "WillBox";
			this.WillBox.Size = new Size(141, 20);
			this.WillBox.TabIndex = 10;
			this.HPLbl.AutoSize = true;
			this.HPLbl.Location = new Point(6, 21);
			this.HPLbl.Name = "HPLbl";
			this.HPLbl.Size = new Size(25, 13);
			this.HPLbl.TabIndex = 0;
			this.HPLbl.Text = "HP:";
			this.ACBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ACBox.Location = new Point(67, 19);
			NumericUpDown arg_567_0 = this.ACBox;
			int[] array4 = new int[4];
			array4[0] = 1000;
			arg_567_0.Maximum = new decimal(array4);
			this.ACBox.Name = "ACBox";
			this.ACBox.Size = new Size(141, 20);
			this.ACBox.TabIndex = 1;
			this.WillLbl.AutoSize = true;
			this.WillLbl.Location = new Point(6, 99);
			this.WillLbl.Name = "WillLbl";
			this.WillLbl.Size = new Size(27, 13);
			this.WillLbl.TabIndex = 9;
			this.WillLbl.Text = "Will:";
			this.RefLbl.AutoSize = true;
			this.RefLbl.Location = new Point(6, 74);
			this.RefLbl.Name = "RefLbl";
			this.RefLbl.Size = new Size(40, 13);
			this.RefLbl.TabIndex = 6;
			this.RefLbl.Text = "Reflex:";
			this.HPBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.HPBox.Location = new Point(67, 19);
			NumericUpDown arg_69D_0 = this.HPBox;
			int[] array5 = new int[4];
			array5[0] = 5000;
			arg_69D_0.Maximum = new decimal(array5);
			this.HPBox.Name = "HPBox";
			this.HPBox.Size = new Size(141, 20);
			this.HPBox.TabIndex = 1;
			this.ACLbl.AutoSize = true;
			this.ACLbl.Location = new Point(7, 22);
			this.ACLbl.Name = "ACLbl";
			this.ACLbl.Size = new Size(24, 13);
			this.ACLbl.TabIndex = 0;
			this.ACLbl.Text = "AC:";
			this.RefBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RefBox.Location = new Point(67, 71);
			NumericUpDown arg_773_0 = this.RefBox;
			int[] array6 = new int[4];
			array6[0] = 1000;
			arg_773_0.Maximum = new decimal(array6);
			this.RefBox.Name = "RefBox";
			this.RefBox.Size = new Size(141, 20);
			this.RefBox.TabIndex = 7;
			this.InitGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.InitGroup.Controls.Add(this.InitRecBtn);
			this.InitGroup.Controls.Add(this.InitBox);
			this.InitGroup.Controls.Add(this.InitLbl);
			this.InitGroup.Location = new Point(12, 68);
			this.InitGroup.Name = "InitGroup";
			this.InitGroup.Size = new Size(337, 50);
			this.InitGroup.TabIndex = 1;
			this.InitGroup.TabStop = false;
			this.InitGroup.Text = "Initiative";
			this.InitRecBtn.Location = new Point(214, 16);
			this.InitRecBtn.Name = "InitRecBtn";
			this.InitRecBtn.Size = new Size(117, 23);
			this.InitRecBtn.TabIndex = 2;
			this.InitRecBtn.Text = "(init)";
			this.InitRecBtn.UseVisualStyleBackColor = true;
			this.InitRecBtn.Click += new EventHandler(this.InitRecBtn_Click);
			this.HPGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.HPGroup.Controls.Add(this.HPRecBtn);
			this.HPGroup.Controls.Add(this.HPBox);
			this.HPGroup.Controls.Add(this.HPLbl);
			this.HPGroup.Location = new Point(12, 12);
			this.HPGroup.Name = "HPGroup";
			this.HPGroup.Size = new Size(337, 50);
			this.HPGroup.TabIndex = 0;
			this.HPGroup.TabStop = false;
			this.HPGroup.Text = "Hit Points";
			this.HPRecBtn.Location = new Point(214, 16);
			this.HPRecBtn.Name = "HPRecBtn";
			this.HPRecBtn.Size = new Size(117, 23);
			this.HPRecBtn.TabIndex = 2;
			this.HPRecBtn.Text = "(hp)";
			this.HPRecBtn.UseVisualStyleBackColor = true;
			this.HPRecBtn.Click += new EventHandler(this.HPRecBtn_Click);
			this.DefGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.DefGroup.Controls.Add(this.WillRecBtn);
			this.DefGroup.Controls.Add(this.RefRecBtn);
			this.DefGroup.Controls.Add(this.FortRecBtn);
			this.DefGroup.Controls.Add(this.ACRecBtn);
			this.DefGroup.Controls.Add(this.ACBox);
			this.DefGroup.Controls.Add(this.RefLbl);
			this.DefGroup.Controls.Add(this.RefBox);
			this.DefGroup.Controls.Add(this.WillLbl);
			this.DefGroup.Controls.Add(this.WillBox);
			this.DefGroup.Controls.Add(this.ACLbl);
			this.DefGroup.Controls.Add(this.FortBox);
			this.DefGroup.Controls.Add(this.FortLbl);
			this.DefGroup.Location = new Point(12, 124);
			this.DefGroup.Name = "DefGroup";
			this.DefGroup.Size = new Size(337, 131);
			this.DefGroup.TabIndex = 2;
			this.DefGroup.TabStop = false;
			this.DefGroup.Text = "Defences";
			this.WillRecBtn.Location = new Point(214, 94);
			this.WillRecBtn.Name = "WillRecBtn";
			this.WillRecBtn.Size = new Size(117, 23);
			this.WillRecBtn.TabIndex = 11;
			this.WillRecBtn.Text = "(will)";
			this.WillRecBtn.UseVisualStyleBackColor = true;
			this.WillRecBtn.Click += new EventHandler(this.WillRecBtn_Click);
			this.RefRecBtn.Location = new Point(214, 69);
			this.RefRecBtn.Name = "RefRecBtn";
			this.RefRecBtn.Size = new Size(117, 23);
			this.RefRecBtn.TabIndex = 8;
			this.RefRecBtn.Text = "(ref)";
			this.RefRecBtn.UseVisualStyleBackColor = true;
			this.RefRecBtn.Click += new EventHandler(this.RefRecBtn_Click);
			this.FortRecBtn.Location = new Point(214, 42);
			this.FortRecBtn.Name = "FortRecBtn";
			this.FortRecBtn.Size = new Size(117, 23);
			this.FortRecBtn.TabIndex = 5;
			this.FortRecBtn.Text = "(fort)";
			this.FortRecBtn.UseVisualStyleBackColor = true;
			this.FortRecBtn.Click += new EventHandler(this.FortRecBtn_Click);
			this.ACRecBtn.Location = new Point(214, 16);
			this.ACRecBtn.Name = "ACRecBtn";
			this.ACRecBtn.Size = new Size(117, 23);
			this.ACRecBtn.TabIndex = 2;
			this.ACRecBtn.Text = "(ac)";
			this.ACRecBtn.UseVisualStyleBackColor = true;
			this.ACRecBtn.Click += new EventHandler(this.ACRecBtn_Click);
			this.DefaultBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.DefaultBtn.Location = new Point(12, 261);
			this.DefaultBtn.Name = "DefaultBtn";
			this.DefaultBtn.Size = new Size(142, 23);
			this.DefaultBtn.TabIndex = 5;
			this.DefaultBtn.Text = "Set to Recommendations";
			this.DefaultBtn.UseVisualStyleBackColor = true;
			this.DefaultBtn.Click += new EventHandler(this.DefaultBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(361, 296);
			base.Controls.Add(this.DefaultBtn);
			base.Controls.Add(this.DefGroup);
			base.Controls.Add(this.HPGroup);
			base.Controls.Add(this.InitGroup);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CreatureStatsForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Combat Statistics";
			((ISupportInitialize)this.FortBox).EndInit();
			((ISupportInitialize)this.InitBox).EndInit();
			((ISupportInitialize)this.WillBox).EndInit();
			((ISupportInitialize)this.ACBox).EndInit();
			((ISupportInitialize)this.HPBox).EndInit();
			((ISupportInitialize)this.RefBox).EndInit();
			this.InitGroup.ResumeLayout(false);
			this.InitGroup.PerformLayout();
			this.HPGroup.ResumeLayout(false);
			this.HPGroup.PerformLayout();
			this.DefGroup.ResumeLayout(false);
			this.DefGroup.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
