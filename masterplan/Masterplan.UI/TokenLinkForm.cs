using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TokenLinkForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label NameLbl;

		private ComboBox LinkTextBox;

		private TokenLink fLink;

		public TokenLink Link
		{
			get
			{
				return this.fLink;
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
			this.LinkTextBox = new ComboBox();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(110, 50);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 2;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(191, 50);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 3;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(31, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Text:";
			this.LinkTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LinkTextBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.LinkTextBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.LinkTextBox.FormattingEnabled = true;
			this.LinkTextBox.Location = new Point(49, 12);
			this.LinkTextBox.Name = "LinkTextBox";
			this.LinkTextBox.Size = new Size(217, 21);
			this.LinkTextBox.Sorted = true;
			this.LinkTextBox.TabIndex = 1;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(278, 85);
			base.Controls.Add(this.LinkTextBox);
			base.Controls.Add(this.NameLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TokenLinkForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Link";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public TokenLinkForm(TokenLink link)
		{
			this.InitializeComponent();
			this.LinkTextBox.Items.Add("Marked");
			this.LinkTextBox.Items.Add("Oath");
			this.LinkTextBox.Items.Add("Quarry");
			this.LinkTextBox.Items.Add("Curse");
			this.LinkTextBox.Items.Add("Shroud");
			this.LinkTextBox.Items.Add("Dominated");
			this.LinkTextBox.Items.Add("Sanctioned");
			this.fLink = link.Copy();
			this.LinkTextBox.Text = this.fLink.Text;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fLink.Text = this.LinkTextBox.Text;
		}
	}
}
