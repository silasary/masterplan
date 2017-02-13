using Masterplan.Controls;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class MapPrintingForm : Form
	{
		private MapView fMapView;

		private PrinterSettings fSettings = new PrinterSettings();

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private RadioButton OnePageBtn;

		private RadioButton PosterBtn;

		private Button PrintBtn;

		public MapPrintingForm(MapView mapview)
		{
			this.InitializeComponent();
			this.fMapView = mapview;
			this.OnePageBtn.Checked = true;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			bool @checked = this.PosterBtn.Checked;
			MapPrinting.Print(this.fMapView, @checked, this.fSettings);
		}

		private void PrintBtn_Click(object sender, EventArgs e)
		{
			PrintDialog printDialog = new PrintDialog();
			printDialog.AllowPrintToFile = false;
			printDialog.PrinterSettings = this.fSettings;
			if (printDialog.ShowDialog() == DialogResult.OK)
			{
				this.fSettings = printDialog.PrinterSettings;
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
			this.OnePageBtn = new RadioButton();
			this.PosterBtn = new RadioButton();
			this.PrintBtn = new Button();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(212, 79);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(293, 79);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OnePageBtn.AutoSize = true;
			this.OnePageBtn.Location = new Point(12, 12);
			this.OnePageBtn.Name = "OnePageBtn";
			this.OnePageBtn.Size = new Size(200, 17);
			this.OnePageBtn.TabIndex = 0;
			this.OnePageBtn.TabStop = true;
			this.OnePageBtn.Text = "Scale the map to fill one printed page";
			this.OnePageBtn.UseVisualStyleBackColor = true;
			this.PosterBtn.AutoSize = true;
			this.PosterBtn.Location = new Point(12, 35);
			this.PosterBtn.Name = "PosterBtn";
			this.PosterBtn.Size = new Size(267, 17);
			this.PosterBtn.TabIndex = 1;
			this.PosterBtn.TabStop = true;
			this.PosterBtn.Text = "Print the map at 1\" resolution (possibly many pages)";
			this.PosterBtn.UseVisualStyleBackColor = true;
			this.PrintBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.PrintBtn.Location = new Point(12, 79);
			this.PrintBtn.Name = "PrintBtn";
			this.PrintBtn.Size = new Size(123, 23);
			this.PrintBtn.TabIndex = 2;
			this.PrintBtn.Text = "Print Settings";
			this.PrintBtn.UseVisualStyleBackColor = true;
			this.PrintBtn.Click += new EventHandler(this.PrintBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(380, 114);
			base.Controls.Add(this.PrintBtn);
			base.Controls.Add(this.PosterBtn);
			base.Controls.Add(this.OnePageBtn);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MapPrintingForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Print Tactical Map";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
