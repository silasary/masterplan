using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class LevelRangePanel : UserControl
	{
		private IContainer components;

		private Label MinLbl;

		private NumericUpDown MinBox;

		private Label MaxLbl;

		private NumericUpDown MaxBox;

		private Label NameLbl;

		private TextBox NameBox;

		private bool fInitialising;

        public event EventHandler RangeChanged;

		public int MinimumLevel
		{
			get
			{
				return (int)this.MinBox.Value;
			}
		}

		public int MaximumLevel
		{
			get
			{
				return (int)this.MaxBox.Value;
			}
		}

		public string NameQuery
		{
			get
			{
				return this.NameBox.Text;
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
			this.MinLbl = new Label();
			this.MinBox = new NumericUpDown();
			this.MaxLbl = new Label();
			this.MaxBox = new NumericUpDown();
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			((ISupportInitialize)this.MinBox).BeginInit();
			((ISupportInitialize)this.MaxBox).BeginInit();
			base.SuspendLayout();
			this.MinLbl.AutoSize = true;
			this.MinLbl.Location = new Point(3, 31);
			this.MinLbl.Name = "MinLbl";
			this.MinLbl.Size = new Size(76, 13);
			this.MinLbl.TabIndex = 2;
			this.MinLbl.Text = "Minimum level:";
			this.MinBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.MinBox.Location = new Point(88, 29);
			NumericUpDown arg_F6_0 = this.MinBox;
			int[] array = new int[4];
			array[0] = 40;
			arg_F6_0.Maximum = new decimal(array);
			NumericUpDown arg_112_0 = this.MinBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_112_0.Minimum = new decimal(array2);
			this.MinBox.Name = "MinBox";
			this.MinBox.Size = new Size(273, 20);
			this.MinBox.TabIndex = 3;
			NumericUpDown arg_161_0 = this.MinBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_161_0.Value = new decimal(array3);
			this.MinBox.ValueChanged += new EventHandler(this.MinBox_ValueChanged);
			this.MaxLbl.AutoSize = true;
			this.MaxLbl.Location = new Point(3, 57);
			this.MaxLbl.Name = "MaxLbl";
			this.MaxLbl.Size = new Size(79, 13);
			this.MaxLbl.TabIndex = 4;
			this.MaxLbl.Text = "Maximum level:";
			this.MaxBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.MaxBox.Location = new Point(88, 55);
			NumericUpDown arg_215_0 = this.MaxBox;
			int[] array4 = new int[4];
			array4[0] = 40;
			arg_215_0.Maximum = new decimal(array4);
			NumericUpDown arg_234_0 = this.MaxBox;
			int[] array5 = new int[4];
			array5[0] = 1;
			arg_234_0.Minimum = new decimal(array5);
			this.MaxBox.Name = "MaxBox";
			this.MaxBox.Size = new Size(273, 20);
			this.MaxBox.TabIndex = 5;
			NumericUpDown arg_286_0 = this.MaxBox;
			int[] array6 = new int[4];
			array6[0] = 1;
			arg_286_0.Value = new decimal(array6);
			this.MaxBox.ValueChanged += new EventHandler(this.MaxBox_ValueChanged);
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(3, 6);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(88, 3);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(273, 20);
			this.NameBox.TabIndex = 1;
			this.NameBox.TextChanged += new EventHandler(this.NameBox_TextChanged);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.Controls.Add(this.MaxBox);
			base.Controls.Add(this.MaxLbl);
			base.Controls.Add(this.MinBox);
			base.Controls.Add(this.MinLbl);
			base.Name = "LevelRangePanel";
			base.Size = new Size(364, 80);
			((ISupportInitialize)this.MinBox).EndInit();
			((ISupportInitialize)this.MaxBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public LevelRangePanel()
		{
			this.InitializeComponent();
		}

		public void SetLevelRange(int minlevel, int maxlevel)
		{
			this.fInitialising = true;
			this.MinBox.Value = Math.Max(this.MinBox.Minimum, minlevel);
			this.MaxBox.Value = Math.Min(this.MaxBox.Maximum, maxlevel);
			this.fInitialising = false;
		}

		private void MinBox_ValueChanged(object sender, EventArgs e)
		{
			if (this.fInitialising)
			{
				return;
			}
			this.fInitialising = true;
			this.MaxBox.Value = Math.Max(this.MaxBox.Value, this.MinBox.Value);
			this.fInitialising = false;
			if (this.RangeChanged != null)
			{
				this.RangeChanged(this, new EventArgs());
			}
		}

		private void MaxBox_ValueChanged(object sender, EventArgs e)
		{
			if (this.fInitialising)
			{
				return;
			}
			this.fInitialising = true;
			this.MinBox.Value = Math.Min(this.MaxBox.Value, this.MinBox.Value);
			this.fInitialising = false;
			if (this.RangeChanged != null)
			{
				this.RangeChanged(this, new EventArgs());
			}
		}

		private void NameBox_TextChanged(object sender, EventArgs e)
		{
			if (this.RangeChanged != null)
			{
				this.RangeChanged(this, new EventArgs());
			}
		}
	}
}
