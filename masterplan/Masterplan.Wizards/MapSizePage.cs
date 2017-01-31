using Masterplan.Tools.Generators;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils.Wizards;

namespace Masterplan.Wizards
{
	internal class MapSizePage : UserControl, IWizardPage
	{
		private MapBuilderData fData;

		private IContainer components;

		private Label InfoLbl;

		private NumericUpDown HeightBox;

		private Label HeightLbl;

		private NumericUpDown WidthBox;

		private Label WidthLbl;

		public bool AllowNext
		{
			get
			{
				return false;
			}
		}

		public bool AllowBack
		{
			get
			{
				return true;
			}
		}

		public bool AllowFinish
		{
			get
			{
				return true;
			}
		}

		public MapSizePage()
		{
			this.InitializeComponent();
		}

		public void OnShown(object data)
		{
			if (this.fData == null)
			{
				this.fData = (data as MapBuilderData);
				this.WidthBox.Value = this.fData.Width;
				this.HeightBox.Value = this.fData.Height;
			}
		}

		public bool OnBack()
		{
			return true;
		}

		public bool OnNext()
		{
			return true;
		}

		public bool OnFinish()
		{
			this.fData.Width = (int)this.WidthBox.Value;
			this.fData.Height = (int)this.HeightBox.Value;
			return true;
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
			this.InfoLbl = new Label();
			this.HeightBox = new NumericUpDown();
			this.HeightLbl = new Label();
			this.WidthBox = new NumericUpDown();
			this.WidthLbl = new Label();
			((ISupportInitialize)this.HeightBox).BeginInit();
			((ISupportInitialize)this.WidthBox).BeginInit();
			base.SuspendLayout();
			this.InfoLbl.Dock = DockStyle.Top;
			this.InfoLbl.Location = new Point(0, 0);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(307, 40);
			this.InfoLbl.TabIndex = 0;
			this.InfoLbl.Text = "What size of map would you like to build?";
			this.HeightBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.HeightBox.Location = new Point(58, 66);
			NumericUpDown arg_F0_0 = this.HeightBox;
			int[] array = new int[4];
			array[0] = 200;
			arg_F0_0.Maximum = new decimal(array);
			NumericUpDown arg_10C_0 = this.HeightBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_10C_0.Minimum = new decimal(array2);
			this.HeightBox.Name = "HeightBox";
			this.HeightBox.Size = new Size(246, 20);
			this.HeightBox.TabIndex = 10;
			NumericUpDown arg_15C_0 = this.HeightBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_15C_0.Value = new decimal(array3);
			this.HeightLbl.AutoSize = true;
			this.HeightLbl.Location = new Point(3, 68);
			this.HeightLbl.Name = "HeightLbl";
			this.HeightLbl.Size = new Size(41, 13);
			this.HeightLbl.TabIndex = 9;
			this.HeightLbl.Text = "Height:";
			this.WidthBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.WidthBox.Location = new Point(58, 40);
			NumericUpDown arg_1FD_0 = this.WidthBox;
			int[] array4 = new int[4];
			array4[0] = 200;
			arg_1FD_0.Maximum = new decimal(array4);
			NumericUpDown arg_21C_0 = this.WidthBox;
			int[] array5 = new int[4];
			array5[0] = 1;
			arg_21C_0.Minimum = new decimal(array5);
			this.WidthBox.Name = "WidthBox";
			this.WidthBox.Size = new Size(246, 20);
			this.WidthBox.TabIndex = 7;
			NumericUpDown arg_26E_0 = this.WidthBox;
			int[] array6 = new int[4];
			array6[0] = 1;
			arg_26E_0.Value = new decimal(array6);
			this.WidthLbl.AutoSize = true;
			this.WidthLbl.Location = new Point(3, 42);
			this.WidthLbl.Name = "WidthLbl";
			this.WidthLbl.Size = new Size(38, 13);
			this.WidthLbl.TabIndex = 6;
			this.WidthLbl.Text = "Width:";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.HeightBox);
			base.Controls.Add(this.HeightLbl);
			base.Controls.Add(this.WidthBox);
			base.Controls.Add(this.WidthLbl);
			base.Controls.Add(this.InfoLbl);
			base.Name = "MapSizePage";
			base.Size = new Size(307, 114);
			((ISupportInitialize)this.HeightBox).EndInit();
			((ISupportInitialize)this.WidthBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
