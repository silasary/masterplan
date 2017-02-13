using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class PowerInfoForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label NameLbl;

		private TextBox NameBox;

		private Label KeywordLbl;

		private TextBox KeywordBox;

		public string PowerName
		{
			get
			{
				return this.NameBox.Text;
			}
		}

		public string PowerKeywords
		{
			get
			{
				return this.KeywordBox.Text;
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
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.KeywordLbl = new Label();
			this.KeywordBox = new TextBox();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(192, 73);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 15;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(273, 73);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 16;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(94, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(254, 20);
			this.NameBox.TabIndex = 1;
			this.KeywordLbl.AutoSize = true;
			this.KeywordLbl.Location = new Point(12, 41);
			this.KeywordLbl.Name = "KeywordLbl";
			this.KeywordLbl.Size = new Size(56, 13);
			this.KeywordLbl.TabIndex = 2;
			this.KeywordLbl.Text = "Keywords:";
			this.KeywordBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.KeywordBox.Location = new Point(94, 38);
			this.KeywordBox.Name = "KeywordBox";
			this.KeywordBox.Size = new Size(254, 20);
			this.KeywordBox.TabIndex = 3;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(360, 108);
			base.Controls.Add(this.KeywordBox);
			base.Controls.Add(this.KeywordLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PowerInfoForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Power Info";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public PowerInfoForm(CreaturePower power)
		{
			this.InitializeComponent();
			this.NameBox.Text = power.Name;
			this.KeywordBox.Text = power.Keywords;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}
	}
}
