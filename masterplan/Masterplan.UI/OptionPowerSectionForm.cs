using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class OptionPowerSectionForm : Form
	{
		private PlayerPowerSection fSection;

		private IContainer components;

		private Label HeaderLbl;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TextBox DetailsBox;

		private Button OKBtn;

		private Button CancelBtn;

		private ComboBox HeaderBox;

		public PlayerPowerSection Section
		{
			get
			{
				return this.fSection;
			}
		}

		public OptionPowerSectionForm(PlayerPowerSection section)
		{
			this.InitializeComponent();
			this.HeaderBox.Items.Add("Attack");
			this.HeaderBox.Items.Add("Trigger");
			this.HeaderBox.Items.Add("Effect");
			this.HeaderBox.Items.Add("Aftereffect");
			this.HeaderBox.Items.Add("Hit");
			this.HeaderBox.Items.Add("Miss");
			this.HeaderBox.Items.Add("Target");
			this.HeaderBox.Items.Add("Prerequisite");
			this.HeaderBox.Items.Add("Requirement");
			this.HeaderBox.Items.Add("Sustain");
			this.HeaderBox.Items.Add("Special");
			this.fSection = section.Copy();
			this.HeaderBox.Text = this.fSection.Header;
			this.DetailsBox.Text = this.fSection.Details;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fSection.Header = this.HeaderBox.Text;
			this.fSection.Details = this.DetailsBox.Text;
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
			this.HeaderLbl = new Label();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.DetailsBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.HeaderBox = new ComboBox();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			base.SuspendLayout();
			this.HeaderLbl.AutoSize = true;
			this.HeaderLbl.Location = new Point(12, 15);
			this.HeaderLbl.Name = "HeaderLbl";
			this.HeaderLbl.Size = new Size(45, 13);
			this.HeaderLbl.TabIndex = 0;
			this.HeaderLbl.Text = "Header:";
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Location = new Point(12, 39);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(307, 146);
			this.Pages.TabIndex = 2;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new Padding(3);
			this.DetailsPage.Size = new Size(299, 120);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.Dock = DockStyle.Fill;
			this.DetailsBox.Location = new Point(3, 3);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new Size(293, 114);
			this.DetailsBox.TabIndex = 0;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(163, 191);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(244, 191);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.HeaderBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.HeaderBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.HeaderBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.HeaderBox.FormattingEnabled = true;
			this.HeaderBox.Location = new Point(63, 12);
			this.HeaderBox.Name = "HeaderBox";
			this.HeaderBox.Size = new Size(256, 21);
			this.HeaderBox.Sorted = true;
			this.HeaderBox.TabIndex = 1;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(331, 226);
			base.Controls.Add(this.HeaderBox);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.HeaderLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OptionPowerSectionForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Section";
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
