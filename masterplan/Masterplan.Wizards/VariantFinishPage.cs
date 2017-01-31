using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils.Wizards;

namespace Masterplan.Wizards
{
	internal class VariantFinishPage : UserControl, IWizardPage
	{
		private IContainer components;

		private Label InfoLbl;

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
			this.InfoLbl = new Label();
			base.SuspendLayout();
			this.InfoLbl.Dock = DockStyle.Top;
			this.InfoLbl.Location = new Point(0, 0);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(260, 40);
			this.InfoLbl.TabIndex = 0;
			this.InfoLbl.Text = "Press Finish to edit this creature, or Back to change your selections.";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.InfoLbl);
			base.Name = "VariantFinishPage";
			base.Size = new Size(260, 150);
			base.ResumeLayout(false);
		}

		public VariantFinishPage()
		{
			this.InitializeComponent();
		}

		public void OnShown(object data)
		{
		}

		public bool OnBack()
		{
			return true;
		}

		public bool OnNext()
		{
			return false;
		}

		public bool OnFinish()
		{
			return true;
		}
	}
}
