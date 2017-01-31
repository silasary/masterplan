using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class SkillChallengeSkillForm : Form
	{
		private const string PRIMARY = "This is a primary skill for this challenge";

		private const string SECONDARY = "This is a secondary skill for this challenge";

		private const string AUTOFAIL = "This skill incurs an automatic failure";

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label SkillLblLbl;

		private Label DiffLbl;

		private TextBox DetailsBox;

		private ComboBox SkillBox;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TabPage SuccessPage;

		private TextBox SuccessBox;

		private TabPage FailurePage;

		private TextBox FailureBox;

		private Label TypeLbl;

		private ComboBox TypeBox;

		private ComboBox DiffBox;

		private Label ModLbl;

		private NumericUpDown ModBox;

		private TabPage ProgressPage;

		private NumericUpDown FailureCountBox;

		private NumericUpDown SuccessCountBox;

		private Label FailureLbl;

		private Label SuccessLbl;

		private SkillChallengeData fSkillData;

		public SkillChallengeData SkillData
		{
			get
			{
				return this.fSkillData;
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
			this.SkillLblLbl = new Label();
			this.DiffLbl = new Label();
			this.DetailsBox = new TextBox();
			this.SkillBox = new ComboBox();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.SuccessPage = new TabPage();
			this.SuccessBox = new TextBox();
			this.FailurePage = new TabPage();
			this.FailureBox = new TextBox();
			this.TypeLbl = new Label();
			this.TypeBox = new ComboBox();
			this.DiffBox = new ComboBox();
			this.ModLbl = new Label();
			this.ModBox = new NumericUpDown();
			this.ProgressPage = new TabPage();
			this.SuccessLbl = new Label();
			this.FailureLbl = new Label();
			this.SuccessCountBox = new NumericUpDown();
			this.FailureCountBox = new NumericUpDown();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.SuccessPage.SuspendLayout();
			this.FailurePage.SuspendLayout();
			((ISupportInitialize)this.ModBox).BeginInit();
			this.ProgressPage.SuspendLayout();
			((ISupportInitialize)this.SuccessCountBox).BeginInit();
			((ISupportInitialize)this.FailureCountBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(146, 317);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 9;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(227, 317);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 10;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.SkillLblLbl.AutoSize = true;
			this.SkillLblLbl.Location = new Point(12, 15);
			this.SkillLblLbl.Name = "SkillLblLbl";
			this.SkillLblLbl.Size = new Size(29, 13);
			this.SkillLblLbl.TabIndex = 0;
			this.SkillLblLbl.Text = "Skill:";
			this.DiffLbl.AutoSize = true;
			this.DiffLbl.Location = new Point(12, 83);
			this.DiffLbl.Name = "DiffLbl";
			this.DiffLbl.Size = new Size(50, 13);
			this.DiffLbl.TabIndex = 4;
			this.DiffLbl.Text = "Difficulty:";
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.Dock = DockStyle.Fill;
			this.DetailsBox.Location = new Point(3, 3);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new Size(276, 131);
			this.DetailsBox.TabIndex = 0;
			this.SkillBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SkillBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.SkillBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.SkillBox.FormattingEnabled = true;
			this.SkillBox.Location = new Point(82, 12);
			this.SkillBox.Name = "SkillBox";
			this.SkillBox.Size = new Size(220, 21);
			this.SkillBox.TabIndex = 1;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Controls.Add(this.SuccessPage);
			this.Pages.Controls.Add(this.FailurePage);
			this.Pages.Controls.Add(this.ProgressPage);
			this.Pages.Location = new Point(12, 148);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(290, 163);
			this.Pages.TabIndex = 8;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new Padding(3);
			this.DetailsPage.Size = new Size(282, 137);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.SuccessPage.Controls.Add(this.SuccessBox);
			this.SuccessPage.Location = new Point(4, 22);
			this.SuccessPage.Name = "SuccessPage";
			this.SuccessPage.Padding = new Padding(3);
			this.SuccessPage.Size = new Size(282, 137);
			this.SuccessPage.TabIndex = 1;
			this.SuccessPage.Text = "Success";
			this.SuccessPage.UseVisualStyleBackColor = true;
			this.SuccessBox.AcceptsReturn = true;
			this.SuccessBox.AcceptsTab = true;
			this.SuccessBox.Dock = DockStyle.Fill;
			this.SuccessBox.Location = new Point(3, 3);
			this.SuccessBox.Multiline = true;
			this.SuccessBox.Name = "SuccessBox";
			this.SuccessBox.ScrollBars = ScrollBars.Vertical;
			this.SuccessBox.Size = new Size(276, 131);
			this.SuccessBox.TabIndex = 1;
			this.FailurePage.Controls.Add(this.FailureBox);
			this.FailurePage.Location = new Point(4, 22);
			this.FailurePage.Name = "FailurePage";
			this.FailurePage.Padding = new Padding(3);
			this.FailurePage.Size = new Size(282, 137);
			this.FailurePage.TabIndex = 2;
			this.FailurePage.Text = "Failure";
			this.FailurePage.UseVisualStyleBackColor = true;
			this.FailureBox.AcceptsReturn = true;
			this.FailureBox.AcceptsTab = true;
			this.FailureBox.Dock = DockStyle.Fill;
			this.FailureBox.Location = new Point(3, 3);
			this.FailureBox.Multiline = true;
			this.FailureBox.Name = "FailureBox";
			this.FailureBox.ScrollBars = ScrollBars.Vertical;
			this.FailureBox.Size = new Size(276, 131);
			this.FailureBox.TabIndex = 1;
			this.TypeLbl.AutoSize = true;
			this.TypeLbl.Location = new Point(12, 42);
			this.TypeLbl.Name = "TypeLbl";
			this.TypeLbl.Size = new Size(34, 13);
			this.TypeLbl.TabIndex = 2;
			this.TypeLbl.Text = "Type:";
			this.TypeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.TypeBox.FormattingEnabled = true;
			this.TypeBox.Location = new Point(82, 39);
			this.TypeBox.Name = "TypeBox";
			this.TypeBox.Size = new Size(220, 21);
			this.TypeBox.TabIndex = 3;
			this.TypeBox.SelectedIndexChanged += new EventHandler(this.TypeBox_SelectedIndexChanged);
			this.DiffBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DiffBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.DiffBox.FormattingEnabled = true;
			this.DiffBox.Location = new Point(82, 81);
			this.DiffBox.Name = "DiffBox";
			this.DiffBox.Size = new Size(220, 21);
			this.DiffBox.TabIndex = 5;
			this.ModLbl.AutoSize = true;
			this.ModLbl.Location = new Point(12, 110);
			this.ModLbl.Name = "ModLbl";
			this.ModLbl.Size = new Size(64, 13);
			this.ModLbl.TabIndex = 6;
			this.ModLbl.Text = "DC modifier:";
			this.ModBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ModBox.Location = new Point(82, 108);
			this.ModBox.Minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				int.MinValue
			});
			this.ModBox.Name = "ModBox";
			this.ModBox.Size = new Size(220, 20);
			this.ModBox.TabIndex = 7;
			this.ProgressPage.Controls.Add(this.FailureCountBox);
			this.ProgressPage.Controls.Add(this.SuccessCountBox);
			this.ProgressPage.Controls.Add(this.FailureLbl);
			this.ProgressPage.Controls.Add(this.SuccessLbl);
			this.ProgressPage.Location = new Point(4, 22);
			this.ProgressPage.Name = "ProgressPage";
			this.ProgressPage.Padding = new Padding(3);
			this.ProgressPage.Size = new Size(282, 137);
			this.ProgressPage.TabIndex = 3;
			this.ProgressPage.Text = "Progress";
			this.ProgressPage.UseVisualStyleBackColor = true;
			this.SuccessLbl.AutoSize = true;
			this.SuccessLbl.Location = new Point(6, 21);
			this.SuccessLbl.Name = "SuccessLbl";
			this.SuccessLbl.Size = new Size(62, 13);
			this.SuccessLbl.TabIndex = 0;
			this.SuccessLbl.Text = "Successes:";
			this.FailureLbl.AutoSize = true;
			this.FailureLbl.Location = new Point(6, 55);
			this.FailureLbl.Name = "FailureLbl";
			this.FailureLbl.Size = new Size(46, 13);
			this.FailureLbl.TabIndex = 2;
			this.FailureLbl.Text = "Failures:";
			this.SuccessCountBox.Location = new Point(74, 19);
			this.SuccessCountBox.Name = "SuccessCountBox";
			this.SuccessCountBox.Size = new Size(202, 20);
			this.SuccessCountBox.TabIndex = 1;
			this.FailureCountBox.Location = new Point(74, 53);
			this.FailureCountBox.Name = "FailureCountBox";
			this.FailureCountBox.Size = new Size(202, 20);
			this.FailureCountBox.TabIndex = 3;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(314, 352);
			base.Controls.Add(this.ModBox);
			base.Controls.Add(this.ModLbl);
			base.Controls.Add(this.DiffBox);
			base.Controls.Add(this.TypeBox);
			base.Controls.Add(this.TypeLbl);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.SkillBox);
			base.Controls.Add(this.DiffLbl);
			base.Controls.Add(this.SkillLblLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MinimizeBox = false;
			base.Name = "SkillChallengeSkillForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Skill";
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.SuccessPage.ResumeLayout(false);
			this.SuccessPage.PerformLayout();
			this.FailurePage.ResumeLayout(false);
			this.FailurePage.PerformLayout();
			((ISupportInitialize)this.ModBox).EndInit();
			this.ProgressPage.ResumeLayout(false);
			this.ProgressPage.PerformLayout();
			((ISupportInitialize)this.SuccessCountBox).EndInit();
			((ISupportInitialize)this.FailureCountBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public SkillChallengeSkillForm(SkillChallengeData scd)
		{
			this.InitializeComponent();
			List<string> skillNames = Skills.GetSkillNames();
			foreach (string current in skillNames)
			{
				this.SkillBox.Items.Add(current);
			}
			this.DiffBox.Items.Add(Difficulty.Easy);
			this.DiffBox.Items.Add(Difficulty.Moderate);
			this.DiffBox.Items.Add(Difficulty.Hard);
			this.TypeBox.Items.Add("This is a primary skill for this challenge");
			this.TypeBox.Items.Add("This is a secondary skill for this challenge");
			this.TypeBox.Items.Add("This skill incurs an automatic failure");
			this.fSkillData = scd.Copy();
			this.SkillBox.Text = this.fSkillData.SkillName;
			switch (this.fSkillData.Type)
			{
			case SkillType.Primary:
				this.TypeBox.SelectedIndex = 0;
				break;
			case SkillType.Secondary:
				this.TypeBox.SelectedIndex = 1;
				break;
			case SkillType.AutoFail:
				this.TypeBox.SelectedIndex = 2;
				break;
			}
			this.DiffBox.SelectedItem = this.fSkillData.Difficulty;
			this.ModBox.Value = this.fSkillData.DCModifier;
			this.DetailsBox.Text = this.fSkillData.Details;
			this.SuccessBox.Text = this.fSkillData.Success;
			this.FailureBox.Text = this.fSkillData.Failure;
			this.SuccessCountBox.Value = this.fSkillData.Results.Successes;
			this.FailureCountBox.Value = this.fSkillData.Results.Fails;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fSkillData.SkillName = this.SkillBox.Text;
			switch (this.TypeBox.SelectedIndex)
			{
			case 0:
				this.fSkillData.Type = SkillType.Primary;
				break;
			case 1:
				this.fSkillData.Type = SkillType.Secondary;
				break;
			case 2:
				this.fSkillData.Type = SkillType.AutoFail;
				break;
			}
			this.fSkillData.Difficulty = (Difficulty)this.DiffBox.SelectedItem;
			this.fSkillData.DCModifier = (int)this.ModBox.Value;
			this.fSkillData.Details = this.DetailsBox.Text;
			this.fSkillData.Success = this.SuccessBox.Text;
			this.fSkillData.Failure = this.FailureBox.Text;
			this.fSkillData.Results.Successes = (int)this.SuccessCountBox.Value;
			this.fSkillData.Results.Fails = (int)this.FailureCountBox.Value;
		}

		private void TypeBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool flag = this.TypeBox.Text == "This skill incurs an automatic failure";
			this.DiffLbl.Enabled = !flag;
			this.DiffBox.Enabled = !flag;
			this.ModLbl.Enabled = !flag;
			this.ModBox.Enabled = !flag;
			if (flag)
			{
				this.Pages.TabPages.Remove(this.SuccessPage);
				this.Pages.TabPages.Remove(this.FailurePage);
				return;
			}
			if (!this.Pages.TabPages.Contains(this.SuccessPage))
			{
				this.Pages.TabPages.Add(this.SuccessPage);
			}
			if (!this.Pages.TabPages.Contains(this.FailurePage))
			{
				this.Pages.TabPages.Add(this.FailurePage);
			}
		}
	}
}
