using Masterplan.Tools.Generators;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils.Wizards;

namespace Masterplan.Wizards
{
	internal class MapAreasPage : UserControl, IWizardPage
	{
		private IContainer components;

		private Label MaxInfoLbl;

		private Label MaxAreasLbl;

		private NumericUpDown MaxAreasBox;

		private NumericUpDown MinAreasBox;

		private Label MinAreasLbl;

		private Label MinInfoLbl;

		private MapBuilderData fData;

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
			this.MaxInfoLbl = new Label();
			this.MaxAreasLbl = new Label();
			this.MaxAreasBox = new NumericUpDown();
			this.MinAreasBox = new NumericUpDown();
			this.MinAreasLbl = new Label();
			this.MinInfoLbl = new Label();
			((ISupportInitialize)this.MaxAreasBox).BeginInit();
			((ISupportInitialize)this.MinAreasBox).BeginInit();
			base.SuspendLayout();
			this.MaxInfoLbl.Dock = DockStyle.Top;
			this.MaxInfoLbl.Location = new Point(0, 0);
			this.MaxInfoLbl.Name = "MaxInfoLbl";
			this.MaxInfoLbl.Size = new Size(307, 40);
			this.MaxInfoLbl.TabIndex = 0;
			this.MaxInfoLbl.Text = "How many areas do you want in your map? The map builder will try to generate a map with this number of areas.";
			this.MaxAreasLbl.AutoSize = true;
			this.MaxAreasLbl.Location = new Point(3, 42);
			this.MaxAreasLbl.Name = "MaxAreasLbl";
			this.MaxAreasLbl.Size = new Size(46, 13);
			this.MaxAreasLbl.TabIndex = 1;
			this.MaxAreasLbl.Text = "At Most:";
			this.MaxAreasBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.MaxAreasBox.Location = new Point(58, 40);
			NumericUpDown arg_156_0 = this.MaxAreasBox;
			int[] array = new int[4];
			array[0] = 1;
			arg_156_0.Minimum = new decimal(array);
			this.MaxAreasBox.Name = "MaxAreasBox";
			this.MaxAreasBox.Size = new Size(246, 20);
			this.MaxAreasBox.TabIndex = 2;
			NumericUpDown arg_1A5_0 = this.MaxAreasBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_1A5_0.Value = new decimal(array2);
			this.MaxAreasBox.ValueChanged += new EventHandler(this.MaxAreasBox_ValueChanged);
			this.MinAreasBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.MinAreasBox.Location = new Point(58, 113);
			this.MinAreasBox.Name = "MinAreasBox";
			this.MinAreasBox.Size = new Size(246, 20);
			this.MinAreasBox.TabIndex = 5;
			NumericUpDown arg_22C_0 = this.MinAreasBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_22C_0.Value = new decimal(array3);
			this.MinAreasLbl.AutoSize = true;
			this.MinAreasLbl.Location = new Point(3, 115);
			this.MinAreasLbl.Name = "MinAreasLbl";
			this.MinAreasLbl.Size = new Size(49, 13);
			this.MinAreasLbl.TabIndex = 4;
			this.MinAreasLbl.Text = "At Least:";
			this.MinInfoLbl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.MinInfoLbl.Location = new Point(0, 84);
			this.MinInfoLbl.Name = "MinInfoLbl";
			this.MinInfoLbl.Size = new Size(307, 26);
			this.MinInfoLbl.TabIndex = 3;
			this.MinInfoLbl.Text = "The map will be rebuilt if it has fewer than this number of areas:";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.MinInfoLbl);
			base.Controls.Add(this.MinAreasBox);
			base.Controls.Add(this.MinAreasLbl);
			base.Controls.Add(this.MaxAreasBox);
			base.Controls.Add(this.MaxAreasLbl);
			base.Controls.Add(this.MaxInfoLbl);
			base.Name = "MapAreasPage";
			base.Size = new Size(307, 151);
			((ISupportInitialize)this.MaxAreasBox).EndInit();
			((ISupportInitialize)this.MinAreasBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public MapAreasPage()
		{
			this.InitializeComponent();
		}

		public void OnShown(object data)
		{
			if (this.fData == null)
			{
				this.fData = (data as MapBuilderData);
				this.MaxAreasBox.Value = this.fData.MaxAreaCount;
				this.MinAreasBox.Value = this.fData.MinAreaCount;
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
			this.fData.MaxAreaCount = (int)this.MaxAreasBox.Value;
			this.fData.MinAreaCount = (int)this.MinAreasBox.Value;
			return true;
		}

		private void MaxAreasBox_ValueChanged(object sender, EventArgs e)
		{
			this.MinAreasBox.Maximum = this.MaxAreasBox.Value;
		}
	}
}
