using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class OptionFeatForm : Form
	{
		private Feat fFeat;

		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private Label PrereqLbl;

		private TextBox PrereqBox;

		private TabControl Pages;

		private TabPage BenefitPage;

		private TextBox BenefitBox;

		private Button OKBtn;

		private Button CancelBtn;

		private Label TierLbl;

		private ComboBox TierBox;

		public Feat Feat
		{
			get
			{
				return this.fFeat;
			}
		}

		public OptionFeatForm(Feat feat)
		{
			this.InitializeComponent();
			Array values = Enum.GetValues(typeof(Tier));
			foreach (Tier tier in values)
			{
				this.TierBox.Items.Add(tier);
			}
			this.fFeat = feat.Copy();
			this.NameBox.Text = this.fFeat.Name;
			this.PrereqBox.Text = this.fFeat.Prerequisites;
			this.TierBox.SelectedItem = this.fFeat.Tier;
			this.BenefitBox.Text = this.fFeat.Benefits;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fFeat.Name = this.NameBox.Text;
			this.fFeat.Prerequisites = this.PrereqBox.Text;
			this.fFeat.Tier = (Tier)this.TierBox.SelectedItem;
			this.fFeat.Benefits = this.BenefitBox.Text;
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
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.PrereqLbl = new Label();
			this.PrereqBox = new TextBox();
			this.Pages = new TabControl();
			this.BenefitPage = new TabPage();
			this.BenefitBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.TierLbl = new Label();
			this.TierBox = new ComboBox();
			this.Pages.SuspendLayout();
			this.BenefitPage.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(88, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(283, 20);
			this.NameBox.TabIndex = 1;
			this.PrereqLbl.AutoSize = true;
			this.PrereqLbl.Location = new Point(12, 68);
			this.PrereqLbl.Name = "PrereqLbl";
			this.PrereqLbl.Size = new Size(70, 13);
			this.PrereqLbl.TabIndex = 4;
			this.PrereqLbl.Text = "Prerequisites:";
			this.PrereqBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.PrereqBox.Location = new Point(88, 65);
			this.PrereqBox.Name = "PrereqBox";
			this.PrereqBox.Size = new Size(283, 20);
			this.PrereqBox.TabIndex = 5;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.BenefitPage);
			this.Pages.Location = new Point(12, 91);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(359, 138);
			this.Pages.TabIndex = 6;
			this.BenefitPage.Controls.Add(this.BenefitBox);
			this.BenefitPage.Location = new Point(4, 22);
			this.BenefitPage.Name = "BenefitPage";
			this.BenefitPage.Padding = new Padding(3);
			this.BenefitPage.Size = new Size(351, 112);
			this.BenefitPage.TabIndex = 0;
			this.BenefitPage.Text = "Benefit";
			this.BenefitPage.UseVisualStyleBackColor = true;
			this.BenefitBox.AcceptsReturn = true;
			this.BenefitBox.AcceptsTab = true;
			this.BenefitBox.Dock = DockStyle.Fill;
			this.BenefitBox.Location = new Point(3, 3);
			this.BenefitBox.Multiline = true;
			this.BenefitBox.Name = "BenefitBox";
			this.BenefitBox.ScrollBars = ScrollBars.Vertical;
			this.BenefitBox.Size = new Size(345, 106);
			this.BenefitBox.TabIndex = 0;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(215, 235);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 7;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(296, 235);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 8;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.TierLbl.AutoSize = true;
			this.TierLbl.Location = new Point(12, 41);
			this.TierLbl.Name = "TierLbl";
			this.TierLbl.Size = new Size(28, 13);
			this.TierLbl.TabIndex = 2;
			this.TierLbl.Text = "Tier:";
			this.TierBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.TierBox.FormattingEnabled = true;
			this.TierBox.Location = new Point(88, 38);
			this.TierBox.Name = "TierBox";
			this.TierBox.Size = new Size(283, 21);
			this.TierBox.TabIndex = 3;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(383, 270);
			base.Controls.Add(this.TierBox);
			base.Controls.Add(this.TierLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.PrereqBox);
			base.Controls.Add(this.PrereqLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OptionFeatForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Feat";
			this.Pages.ResumeLayout(false);
			this.BenefitPage.ResumeLayout(false);
			this.BenefitPage.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
