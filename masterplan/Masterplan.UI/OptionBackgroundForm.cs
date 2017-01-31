using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class OptionBackgroundForm : Form
	{
		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private Label FeatLbl;

		private TextBox FeatBox;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TextBox DetailsBox;

		private Button OKBtn;

		private Button CancelBtn;

		private TextBox SkillBox;

		private Label SkillLbl;

		private PlayerBackground fBackground;

		public PlayerBackground Background
		{
			get
			{
				return this.fBackground;
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
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.FeatLbl = new Label();
			this.FeatBox = new TextBox();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.DetailsBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.SkillBox = new TextBox();
			this.SkillLbl = new Label();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(129, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(306, 20);
			this.NameBox.TabIndex = 1;
			this.FeatLbl.AutoSize = true;
			this.FeatLbl.Location = new Point(12, 67);
			this.FeatLbl.Name = "FeatLbl";
			this.FeatLbl.Size = new Size(111, 13);
			this.FeatLbl.TabIndex = 4;
			this.FeatLbl.Text = "Recommended Feats:";
			this.FeatBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.FeatBox.Location = new Point(129, 64);
			this.FeatBox.Name = "FeatBox";
			this.FeatBox.Size = new Size(306, 20);
			this.FeatBox.TabIndex = 5;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Location = new Point(12, 90);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(423, 155);
			this.Pages.TabIndex = 6;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new Padding(3);
			this.DetailsPage.Size = new Size(415, 129);
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
			this.DetailsBox.Size = new Size(409, 123);
			this.DetailsBox.TabIndex = 0;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(279, 251);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 7;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(360, 251);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 8;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.SkillBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SkillBox.Location = new Point(129, 38);
			this.SkillBox.Name = "SkillBox";
			this.SkillBox.Size = new Size(306, 20);
			this.SkillBox.TabIndex = 3;
			this.SkillLbl.AutoSize = true;
			this.SkillLbl.Location = new Point(12, 41);
			this.SkillLbl.Name = "SkillLbl";
			this.SkillLbl.Size = new Size(89, 13);
			this.SkillLbl.TabIndex = 2;
			this.SkillLbl.Text = "Associated Skills:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(447, 286);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.FeatBox);
			base.Controls.Add(this.FeatLbl);
			base.Controls.Add(this.SkillBox);
			base.Controls.Add(this.SkillLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OptionBackgroundForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Background";
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public OptionBackgroundForm(PlayerBackground bg)
		{
			this.InitializeComponent();
			this.fBackground = bg.Copy();
			this.NameBox.Text = this.fBackground.Name;
			this.SkillBox.Text = this.fBackground.AssociatedSkills;
			this.FeatBox.Text = this.fBackground.RecommendedFeats;
			this.DetailsBox.Text = this.fBackground.Details;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fBackground.Name = this.NameBox.Text;
			this.fBackground.AssociatedSkills = this.SkillBox.Text;
			this.fBackground.RecommendedFeats = this.FeatBox.Text;
			this.fBackground.Details = this.DetailsBox.Text;
		}
	}
}
