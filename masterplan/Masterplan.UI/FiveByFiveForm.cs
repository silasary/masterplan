using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class FiveByFiveForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private FiveByFivePanel FiveByFivePanel;

		private FiveByFiveData f5x5;

		private bool fCreatePlot;

		public FiveByFiveData FiveByFive
		{
			get
			{
				return this.f5x5;
			}
		}

		public bool CreatePlot
		{
			get
			{
				return this.fCreatePlot;
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
			this.FiveByFivePanel = new FiveByFivePanel();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(401, 306);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(482, 306);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 1;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.FiveByFivePanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.FiveByFivePanel.BorderStyle = BorderStyle.FixedSingle;
			this.FiveByFivePanel.Location = new Point(12, 12);
			this.FiveByFivePanel.Name = "FiveByFivePanel";
			this.FiveByFivePanel.Size = new Size(545, 288);
			this.FiveByFivePanel.TabIndex = 2;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(569, 341);
			base.Controls.Add(this.FiveByFivePanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FiveByFiveForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Five by Five";
			base.FormClosing += new FormClosingEventHandler(this.FiveByFiveForm_FormClosing);
			base.ResumeLayout(false);
		}

		public FiveByFiveForm(FiveByFiveData five_by_five)
		{
			this.InitializeComponent();
			this.f5x5 = five_by_five.Copy();
			this.FiveByFivePanel.Data = this.f5x5;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}

		private void FiveByFiveForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == DialogResult.OK)
			{
				string text = "Do you want to build a plotline from these items?";
				DialogResult dialogResult = MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				DialogResult dialogResult2 = dialogResult;
				if (dialogResult2 != DialogResult.Cancel)
				{
					switch (dialogResult2)
					{
					case DialogResult.Yes:
						this.fCreatePlot = true;
						return;
					case DialogResult.No:
						this.fCreatePlot = false;
						return;
					default:
						return;
					}
				}
				else
				{
					e.Cancel = true;
				}
			}
		}
	}
}
