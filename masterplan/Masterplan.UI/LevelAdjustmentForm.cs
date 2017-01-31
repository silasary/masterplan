using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class LevelAdjustmentForm : Form
	{
		private const string LEVEL_UP = "More difficult";

		private const string LEVEL_DOWN = "Less difficult";

		private IContainer components;

		private Label LevelLbl;

		private Button OKBtn;

		private Button CancelBtn;

		private NumericUpDown LevelBox;

		private Label InfoLbl;

		private ComboBox DirectionBox;

		private Label DirLbl;

		public int LevelAdjustment
		{
			get
			{
				int num = (int)this.LevelBox.Value;
				if (this.DirectionBox.SelectedItem.ToString() == "Less difficult")
				{
					num *= -1;
				}
				return num;
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
			this.LevelLbl = new Label();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.LevelBox = new NumericUpDown();
			this.InfoLbl = new Label();
			this.DirectionBox = new ComboBox();
			this.DirLbl = new Label();
			((ISupportInitialize)this.LevelBox).BeginInit();
			base.SuspendLayout();
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(13, 46);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(41, 13);
			this.LevelLbl.TabIndex = 1;
			this.LevelLbl.Text = "Levels:";
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(73, 109);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 5;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(154, 109);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 6;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.LevelBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LevelBox.Location = new Point(71, 44);
			NumericUpDown arg_203_0 = this.LevelBox;
			int[] array = new int[4];
			array[0] = 30;
			arg_203_0.Maximum = new decimal(array);
			NumericUpDown arg_21F_0 = this.LevelBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_21F_0.Minimum = new decimal(array2);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new Size(158, 20);
			this.LevelBox.TabIndex = 2;
			NumericUpDown arg_26E_0 = this.LevelBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_26E_0.Value = new decimal(array3);
			this.InfoLbl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.InfoLbl.Location = new Point(12, 9);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(217, 32);
			this.InfoLbl.TabIndex = 0;
			this.InfoLbl.Text = "How many levels up or down do you want to adjust this plot?";
			this.DirectionBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DirectionBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.DirectionBox.FormattingEnabled = true;
			this.DirectionBox.Location = new Point(71, 70);
			this.DirectionBox.Name = "DirectionBox";
			this.DirectionBox.Size = new Size(158, 21);
			this.DirectionBox.TabIndex = 4;
			this.DirLbl.AutoSize = true;
			this.DirLbl.Location = new Point(13, 73);
			this.DirLbl.Name = "DirLbl";
			this.DirLbl.Size = new Size(52, 13);
			this.DirLbl.TabIndex = 3;
			this.DirLbl.Text = "Direction:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(241, 144);
			base.Controls.Add(this.DirLbl);
			base.Controls.Add(this.DirectionBox);
			base.Controls.Add(this.InfoLbl);
			base.Controls.Add(this.LevelBox);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.LevelLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "LevelAdjustmentForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Level Adjustment";
			((ISupportInitialize)this.LevelBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public LevelAdjustmentForm()
		{
			this.InitializeComponent();
			this.DirectionBox.Items.Add("More difficult");
			this.DirectionBox.Items.Add("Less difficult");
			this.DirectionBox.SelectedIndex = 0;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}
	}
}
