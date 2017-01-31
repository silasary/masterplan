using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CreatureTemplateStatsForm : Form
	{
		private CreatureTemplate fTemplate;

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private NumericUpDown WillBox;

		private Label WillLbl;

		private NumericUpDown RefBox;

		private Label RefLbl;

		private NumericUpDown FortBox;

		private Label FortLbl;

		private NumericUpDown ACBox;

		private Label ACLbl;

		private NumericUpDown HPBox;

		private Label HPLbl;

		private NumericUpDown InitBox;

		private Label InitLbl;

		public CreatureTemplate Template
		{
			get
			{
				return this.fTemplate;
			}
		}

		public CreatureTemplateStatsForm(CreatureTemplate t)
		{
			this.InitializeComponent();
			this.fTemplate = t.Copy();
			this.HPBox.Value = this.fTemplate.HP;
			this.InitBox.Value = this.fTemplate.Initiative;
			this.ACBox.Value = this.fTemplate.AC;
			this.FortBox.Value = this.fTemplate.Fortitude;
			this.RefBox.Value = this.fTemplate.Reflex;
			this.WillBox.Value = this.fTemplate.Will;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fTemplate.HP = (int)this.HPBox.Value;
			this.fTemplate.Initiative = (int)this.InitBox.Value;
			this.fTemplate.AC = (int)this.ACBox.Value;
			this.fTemplate.Fortitude = (int)this.FortBox.Value;
			this.fTemplate.Reflex = (int)this.RefBox.Value;
			this.fTemplate.Will = (int)this.WillBox.Value;
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
			this.WillBox = new NumericUpDown();
			this.WillLbl = new Label();
			this.RefBox = new NumericUpDown();
			this.RefLbl = new Label();
			this.FortBox = new NumericUpDown();
			this.FortLbl = new Label();
			this.ACBox = new NumericUpDown();
			this.ACLbl = new Label();
			this.HPBox = new NumericUpDown();
			this.HPLbl = new Label();
			this.InitBox = new NumericUpDown();
			this.InitLbl = new Label();
			((ISupportInitialize)this.WillBox).BeginInit();
			((ISupportInitialize)this.RefBox).BeginInit();
			((ISupportInitialize)this.FortBox).BeginInit();
			((ISupportInitialize)this.ACBox).BeginInit();
			((ISupportInitialize)this.HPBox).BeginInit();
			((ISupportInitialize)this.InitBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(84, 193);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 14;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(165, 193);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 15;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.WillBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.WillBox.Location = new Point(76, 156);
			this.WillBox.Minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				-2147483648
			});
			this.WillBox.Name = "WillBox";
			this.WillBox.Size = new Size(164, 20);
			this.WillBox.TabIndex = 11;
			this.WillLbl.AutoSize = true;
			this.WillLbl.Location = new Point(12, 158);
			this.WillLbl.Name = "WillLbl";
			this.WillLbl.Size = new Size(27, 13);
			this.WillLbl.TabIndex = 10;
			this.WillLbl.Text = "Will:";
			this.RefBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RefBox.Location = new Point(76, 130);
			this.RefBox.Minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				-2147483648
			});
			this.RefBox.Name = "RefBox";
			this.RefBox.Size = new Size(164, 20);
			this.RefBox.TabIndex = 9;
			this.RefLbl.AutoSize = true;
			this.RefLbl.Location = new Point(12, 132);
			this.RefLbl.Name = "RefLbl";
			this.RefLbl.Size = new Size(37, 13);
			this.RefLbl.TabIndex = 8;
			this.RefLbl.Text = "Reflex";
			this.FortBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.FortBox.Location = new Point(76, 104);
			this.FortBox.Minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				-2147483648
			});
			this.FortBox.Name = "FortBox";
			this.FortBox.Size = new Size(164, 20);
			this.FortBox.TabIndex = 7;
			this.FortLbl.AutoSize = true;
			this.FortLbl.Location = new Point(12, 106);
			this.FortLbl.Name = "FortLbl";
			this.FortLbl.Size = new Size(51, 13);
			this.FortLbl.TabIndex = 6;
			this.FortLbl.Text = "Fortitude:";
			this.ACBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ACBox.Location = new Point(76, 78);
			this.ACBox.Minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				-2147483648
			});
			this.ACBox.Name = "ACBox";
			this.ACBox.Size = new Size(164, 20);
			this.ACBox.TabIndex = 5;
			this.ACLbl.AutoSize = true;
			this.ACLbl.Location = new Point(12, 80);
			this.ACLbl.Name = "ACLbl";
			this.ACLbl.Size = new Size(24, 13);
			this.ACLbl.TabIndex = 4;
			this.ACLbl.Text = "AC:";
			this.HPBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.HPBox.Location = new Point(76, 12);
			this.HPBox.Minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				-2147483648
			});
			this.HPBox.Name = "HPBox";
			this.HPBox.Size = new Size(164, 20);
			this.HPBox.TabIndex = 1;
			this.HPLbl.AutoSize = true;
			this.HPLbl.Location = new Point(12, 14);
			this.HPLbl.Name = "HPLbl";
			this.HPLbl.Size = new Size(58, 13);
			this.HPLbl.TabIndex = 0;
			this.HPLbl.Text = "HP / level:";
			this.InitBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.InitBox.Location = new Point(76, 38);
			this.InitBox.Minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				-2147483648
			});
			this.InitBox.Name = "InitBox";
			this.InitBox.Size = new Size(164, 20);
			this.InitBox.TabIndex = 3;
			this.InitLbl.AutoSize = true;
			this.InitLbl.Location = new Point(12, 40);
			this.InitLbl.Name = "InitLbl";
			this.InitLbl.Size = new Size(49, 13);
			this.InitLbl.TabIndex = 2;
			this.InitLbl.Text = "Initiative:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(252, 228);
			base.Controls.Add(this.HPBox);
			base.Controls.Add(this.InitLbl);
			base.Controls.Add(this.InitBox);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.WillLbl);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.ACBox);
			base.Controls.Add(this.HPLbl);
			base.Controls.Add(this.RefLbl);
			base.Controls.Add(this.RefBox);
			base.Controls.Add(this.FortBox);
			base.Controls.Add(this.ACLbl);
			base.Controls.Add(this.FortLbl);
			base.Controls.Add(this.WillBox);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CreatureTemplateStatsForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Combat Stats";
			((ISupportInitialize)this.WillBox).EndInit();
			((ISupportInitialize)this.RefBox).EndInit();
			((ISupportInitialize)this.FortBox).EndInit();
			((ISupportInitialize)this.ACBox).EndInit();
			((ISupportInitialize)this.HPBox).EndInit();
			((ISupportInitialize)this.InitBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
