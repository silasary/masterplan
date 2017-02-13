using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TileSizeForm : Form
	{
		private IContainer components;

		private Label WidthLbl;

		private NumericUpDown WidthBox;

		private Label HeightLbl;

		private NumericUpDown HeightBox;

		private Button OKBtn;

		private Button CancelBtn;

		private List<Tile> fTiles;

		private Size fSize = new Size(2, 2);

		public Size TileSize
		{
			get
			{
				return this.fSize;
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
			this.WidthLbl = new Label();
			this.WidthBox = new NumericUpDown();
			this.HeightLbl = new Label();
			this.HeightBox = new NumericUpDown();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			((ISupportInitialize)this.WidthBox).BeginInit();
			((ISupportInitialize)this.HeightBox).BeginInit();
			base.SuspendLayout();
			this.WidthLbl.AutoSize = true;
			this.WidthLbl.Location = new Point(12, 14);
			this.WidthLbl.Name = "WidthLbl";
			this.WidthLbl.Size = new Size(58, 13);
			this.WidthLbl.TabIndex = 0;
			this.WidthLbl.Text = "Tile Width:";
			this.WidthBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.WidthBox.Location = new Point(79, 12);
			NumericUpDown arg_F6_0 = this.WidthBox;
			int[] array = new int[4];
			array[0] = 1;
			arg_F6_0.Minimum = new decimal(array);
			this.WidthBox.Name = "WidthBox";
			this.WidthBox.Size = new Size(175, 20);
			this.WidthBox.TabIndex = 1;
			NumericUpDown arg_145_0 = this.WidthBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_145_0.Value = new decimal(array2);
			this.HeightLbl.AutoSize = true;
			this.HeightLbl.Location = new Point(12, 40);
			this.HeightLbl.Name = "HeightLbl";
			this.HeightLbl.Size = new Size(61, 13);
			this.HeightLbl.TabIndex = 2;
			this.HeightLbl.Text = "Tile Height:";
			this.HeightBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.HeightBox.Location = new Point(79, 38);
			NumericUpDown arg_1E2_0 = this.HeightBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_1E2_0.Minimum = new decimal(array3);
			this.HeightBox.Name = "HeightBox";
			this.HeightBox.Size = new Size(175, 20);
			this.HeightBox.TabIndex = 3;
			NumericUpDown arg_231_0 = this.HeightBox;
			int[] array4 = new int[4];
			array4[0] = 1;
			arg_231_0.Value = new decimal(array4);
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(98, 77);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 4;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(179, 77);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 5;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(266, 112);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.HeightBox);
			base.Controls.Add(this.HeightLbl);
			base.Controls.Add(this.WidthBox);
			base.Controls.Add(this.WidthLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TileSizeForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Tile Size";
			((ISupportInitialize)this.WidthBox).EndInit();
			((ISupportInitialize)this.HeightBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public TileSizeForm(List<Tile> tiles)
		{
			this.InitializeComponent();
			this.fTiles = tiles;
			int num = 0;
			int num2 = 0;
			foreach (Tile current in this.fTiles)
			{
				num += current.Size.Width;
				num2 += current.Size.Height;
			}
			num /= this.fTiles.Count;
			num2 /= this.fTiles.Count;
			this.WidthBox.Value = num;
			this.HeightBox.Value = num2;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			int width = (int)this.WidthBox.Value;
			int height = (int)this.HeightBox.Value;
			this.fSize = new Size(width, height);
		}
	}
}
