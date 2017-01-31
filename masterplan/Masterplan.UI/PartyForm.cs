using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class PartyForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label SizeLbl;

		private NumericUpDown SizeBox;

		private Label LevelLbl;

		private NumericUpDown LevelBox;

		private Party fParty;

		public Party Party
		{
			get
			{
				return this.fParty;
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
			this.SizeLbl = new Label();
			this.SizeBox = new NumericUpDown();
			this.LevelLbl = new Label();
			this.LevelBox = new NumericUpDown();
			((ISupportInitialize)this.SizeBox).BeginInit();
			((ISupportInitialize)this.LevelBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(108, 75);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 4;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(189, 75);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 5;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.SizeLbl.AutoSize = true;
			this.SizeLbl.Location = new Point(12, 14);
			this.SizeLbl.Name = "SizeLbl";
			this.SizeLbl.Size = new Size(57, 13);
			this.SizeLbl.TabIndex = 0;
			this.SizeLbl.Text = "Party Size:";
			this.SizeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SizeBox.Location = new Point(81, 12);
			NumericUpDown arg_203_0 = this.SizeBox;
			int[] array = new int[4];
			array[0] = 20;
			arg_203_0.Maximum = new decimal(array);
			NumericUpDown arg_21F_0 = this.SizeBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_21F_0.Minimum = new decimal(array2);
			this.SizeBox.Name = "SizeBox";
			this.SizeBox.Size = new Size(183, 20);
			this.SizeBox.TabIndex = 1;
			NumericUpDown arg_26E_0 = this.SizeBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_26E_0.Value = new decimal(array3);
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(12, 40);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(63, 13);
			this.LevelLbl.TabIndex = 2;
			this.LevelLbl.Text = "Party Level:";
			this.LevelBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LevelBox.Location = new Point(81, 38);
			NumericUpDown arg_30C_0 = this.LevelBox;
			int[] array4 = new int[4];
			array4[0] = 30;
			arg_30C_0.Maximum = new decimal(array4);
			NumericUpDown arg_32B_0 = this.LevelBox;
			int[] array5 = new int[4];
			array5[0] = 1;
			arg_32B_0.Minimum = new decimal(array5);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new Size(183, 20);
			this.LevelBox.TabIndex = 3;
			NumericUpDown arg_37D_0 = this.LevelBox;
			int[] array6 = new int[4];
			array6[0] = 1;
			arg_37D_0.Value = new decimal(array6);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(276, 110);
			base.Controls.Add(this.LevelBox);
			base.Controls.Add(this.LevelLbl);
			base.Controls.Add(this.SizeBox);
			base.Controls.Add(this.SizeLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PartyForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Party";
			((ISupportInitialize)this.SizeBox).EndInit();
			((ISupportInitialize)this.LevelBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public PartyForm(Party p)
		{
			this.InitializeComponent();
			this.fParty = p;
			this.SizeBox.Value = this.fParty.Size;
			this.LevelBox.Value = this.fParty.Level;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fParty.Size = (int)this.SizeBox.Value;
			this.fParty.Level = (int)this.LevelBox.Value;
		}
	}
}
