using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class OptionRaceForm : Form
	{
		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TextBox DetailsBox;

		private Button OKBtn;

		private Button CancelBtn;

		private TabPage TraitsPage;

		private TabPage FeaturesPage;

		private TabPage PowersPage;

		private ListView FeatureList;

		private ColumnHeader FeatureHdr;

		private ToolStrip FeatureToolbar;

		private ToolStripButton FeatureAddBtn;

		private ToolStripButton FeatureRemoveBtn;

		private ToolStripButton FeatureEditBtn;

		private ListView PowerList;

		private ColumnHeader PowerHdr;

		private ToolStrip PowerToolbar;

		private ToolStripButton PowerAddBtn;

		private ToolStripButton PowerRemoveBtn;

		private ToolStripButton PowerEditBtn;

		private Label AbilityScoreLbl;

		private Label WeightLbl;

		private Label HeightLbl;

		private Label SpeedLbl;

		private Label SizeLbl;

		private TextBox SkillBonusBox;

		private TextBox LanguageBox;

		private TextBox VisionBox;

		private TextBox SpeedBox;

		private ComboBox SizeBox;

		private TextBox AbilityScoreBox;

		private TextBox WeightBox;

		private TextBox HeightBox;

		private Label SkillBonusLbl;

		private Label LanguageLbl;

		private Label VisionLbl;

		private TextBox QuoteBox;

		private Label QuoteLbl;

		private Race fRace;

		public Race Race
		{
			get
			{
				return this.fRace;
			}
		}

		public Feature SelectedFeature
		{
			get
			{
				if (this.FeatureList.SelectedItems.Count != 0)
				{
					return this.FeatureList.SelectedItems[0].Tag as Feature;
				}
				return null;
			}
		}

		public PlayerPower SelectedPower
		{
			get
			{
				if (this.PowerList.SelectedItems.Count != 0)
				{
					return this.PowerList.SelectedItems[0].Tag as PlayerPower;
				}
				return null;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(OptionRaceForm));
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.Pages = new TabControl();
			this.TraitsPage = new TabPage();
			this.SkillBonusBox = new TextBox();
			this.LanguageBox = new TextBox();
			this.VisionBox = new TextBox();
			this.SpeedBox = new TextBox();
			this.SizeBox = new ComboBox();
			this.AbilityScoreBox = new TextBox();
			this.WeightBox = new TextBox();
			this.HeightBox = new TextBox();
			this.SkillBonusLbl = new Label();
			this.LanguageLbl = new Label();
			this.VisionLbl = new Label();
			this.SpeedLbl = new Label();
			this.SizeLbl = new Label();
			this.AbilityScoreLbl = new Label();
			this.WeightLbl = new Label();
			this.HeightLbl = new Label();
			this.DetailsPage = new TabPage();
			this.DetailsBox = new TextBox();
			this.FeaturesPage = new TabPage();
			this.FeatureList = new ListView();
			this.FeatureHdr = new ColumnHeader();
			this.FeatureToolbar = new ToolStrip();
			this.FeatureAddBtn = new ToolStripButton();
			this.FeatureRemoveBtn = new ToolStripButton();
			this.FeatureEditBtn = new ToolStripButton();
			this.PowersPage = new TabPage();
			this.PowerList = new ListView();
			this.PowerHdr = new ColumnHeader();
			this.PowerToolbar = new ToolStrip();
			this.PowerAddBtn = new ToolStripButton();
			this.PowerRemoveBtn = new ToolStripButton();
			this.PowerEditBtn = new ToolStripButton();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.QuoteLbl = new Label();
			this.QuoteBox = new TextBox();
			this.Pages.SuspendLayout();
			this.TraitsPage.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.FeaturesPage.SuspendLayout();
			this.FeatureToolbar.SuspendLayout();
			this.PowersPage.SuspendLayout();
			this.PowerToolbar.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(56, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(305, 20);
			this.NameBox.TabIndex = 1;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.TraitsPage);
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Controls.Add(this.FeaturesPage);
			this.Pages.Controls.Add(this.PowersPage);
			this.Pages.Location = new Point(12, 38);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(349, 248);
			this.Pages.TabIndex = 2;
			this.TraitsPage.Controls.Add(this.SkillBonusBox);
			this.TraitsPage.Controls.Add(this.LanguageBox);
			this.TraitsPage.Controls.Add(this.VisionBox);
			this.TraitsPage.Controls.Add(this.SpeedBox);
			this.TraitsPage.Controls.Add(this.SizeBox);
			this.TraitsPage.Controls.Add(this.AbilityScoreBox);
			this.TraitsPage.Controls.Add(this.WeightBox);
			this.TraitsPage.Controls.Add(this.HeightBox);
			this.TraitsPage.Controls.Add(this.SkillBonusLbl);
			this.TraitsPage.Controls.Add(this.LanguageLbl);
			this.TraitsPage.Controls.Add(this.VisionLbl);
			this.TraitsPage.Controls.Add(this.SpeedLbl);
			this.TraitsPage.Controls.Add(this.SizeLbl);
			this.TraitsPage.Controls.Add(this.AbilityScoreLbl);
			this.TraitsPage.Controls.Add(this.WeightLbl);
			this.TraitsPage.Controls.Add(this.HeightLbl);
			this.TraitsPage.Location = new Point(4, 22);
			this.TraitsPage.Name = "TraitsPage";
			this.TraitsPage.Padding = new Padding(3);
			this.TraitsPage.Size = new Size(341, 222);
			this.TraitsPage.TabIndex = 1;
			this.TraitsPage.Text = "Racial Traits";
			this.TraitsPage.UseVisualStyleBackColor = true;
			this.SkillBonusBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SkillBonusBox.Location = new Point(99, 189);
			this.SkillBonusBox.Name = "SkillBonusBox";
			this.SkillBonusBox.Size = new Size(236, 20);
			this.SkillBonusBox.TabIndex = 15;
			this.LanguageBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LanguageBox.Location = new Point(99, 163);
			this.LanguageBox.Name = "LanguageBox";
			this.LanguageBox.Size = new Size(236, 20);
			this.LanguageBox.TabIndex = 13;
			this.VisionBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.VisionBox.Location = new Point(99, 137);
			this.VisionBox.Name = "VisionBox";
			this.VisionBox.Size = new Size(236, 20);
			this.VisionBox.TabIndex = 11;
			this.SpeedBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SpeedBox.Location = new Point(99, 111);
			this.SpeedBox.Name = "SpeedBox";
			this.SpeedBox.Size = new Size(236, 20);
			this.SpeedBox.TabIndex = 9;
			this.SizeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SizeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.SizeBox.FormattingEnabled = true;
			this.SizeBox.Location = new Point(99, 84);
			this.SizeBox.Name = "SizeBox";
			this.SizeBox.Size = new Size(236, 21);
			this.SizeBox.TabIndex = 7;
			this.AbilityScoreBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.AbilityScoreBox.Location = new Point(99, 58);
			this.AbilityScoreBox.Name = "AbilityScoreBox";
			this.AbilityScoreBox.Size = new Size(236, 20);
			this.AbilityScoreBox.TabIndex = 5;
			this.WeightBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.WeightBox.Location = new Point(99, 32);
			this.WeightBox.Name = "WeightBox";
			this.WeightBox.Size = new Size(236, 20);
			this.WeightBox.TabIndex = 3;
			this.HeightBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.HeightBox.Location = new Point(99, 6);
			this.HeightBox.Name = "HeightBox";
			this.HeightBox.Size = new Size(236, 20);
			this.HeightBox.TabIndex = 1;
			this.SkillBonusLbl.AutoSize = true;
			this.SkillBonusLbl.Location = new Point(6, 192);
			this.SkillBonusLbl.Name = "SkillBonusLbl";
			this.SkillBonusLbl.Size = new Size(73, 13);
			this.SkillBonusLbl.TabIndex = 14;
			this.SkillBonusLbl.Text = "Skill Bonuses:";
			this.LanguageLbl.AutoSize = true;
			this.LanguageLbl.Location = new Point(6, 166);
			this.LanguageLbl.Name = "LanguageLbl";
			this.LanguageLbl.Size = new Size(63, 13);
			this.LanguageLbl.TabIndex = 12;
			this.LanguageLbl.Text = "Languages:";
			this.VisionLbl.AutoSize = true;
			this.VisionLbl.Location = new Point(6, 140);
			this.VisionLbl.Name = "VisionLbl";
			this.VisionLbl.Size = new Size(38, 13);
			this.VisionLbl.TabIndex = 10;
			this.VisionLbl.Text = "Vision:";
			this.SpeedLbl.AutoSize = true;
			this.SpeedLbl.Location = new Point(6, 114);
			this.SpeedLbl.Name = "SpeedLbl";
			this.SpeedLbl.Size = new Size(41, 13);
			this.SpeedLbl.TabIndex = 8;
			this.SpeedLbl.Text = "Speed:";
			this.SizeLbl.AutoSize = true;
			this.SizeLbl.Location = new Point(6, 87);
			this.SizeLbl.Name = "SizeLbl";
			this.SizeLbl.Size = new Size(30, 13);
			this.SizeLbl.TabIndex = 6;
			this.SizeLbl.Text = "Size:";
			this.AbilityScoreLbl.AutoSize = true;
			this.AbilityScoreLbl.Location = new Point(6, 61);
			this.AbilityScoreLbl.Name = "AbilityScoreLbl";
			this.AbilityScoreLbl.Size = new Size(73, 13);
			this.AbilityScoreLbl.TabIndex = 4;
			this.AbilityScoreLbl.Text = "Ability Scores:";
			this.WeightLbl.AutoSize = true;
			this.WeightLbl.Location = new Point(6, 35);
			this.WeightLbl.Name = "WeightLbl";
			this.WeightLbl.Size = new Size(87, 13);
			this.WeightLbl.TabIndex = 2;
			this.WeightLbl.Text = "Average Weight:";
			this.HeightLbl.AutoSize = true;
			this.HeightLbl.Location = new Point(6, 9);
			this.HeightLbl.Name = "HeightLbl";
			this.HeightLbl.Size = new Size(84, 13);
			this.HeightLbl.TabIndex = 0;
			this.HeightLbl.Text = "Average Height:";
			this.DetailsPage.Controls.Add(this.QuoteBox);
			this.DetailsPage.Controls.Add(this.QuoteLbl);
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new Padding(3);
			this.DetailsPage.Size = new Size(341, 222);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.DetailsBox.Location = new Point(6, 6);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new Size(329, 184);
			this.DetailsBox.TabIndex = 0;
			this.FeaturesPage.Controls.Add(this.FeatureList);
			this.FeaturesPage.Controls.Add(this.FeatureToolbar);
			this.FeaturesPage.Location = new Point(4, 22);
			this.FeaturesPage.Name = "FeaturesPage";
			this.FeaturesPage.Padding = new Padding(3);
			this.FeaturesPage.Size = new Size(341, 222);
			this.FeaturesPage.TabIndex = 2;
			this.FeaturesPage.Text = "Features";
			this.FeaturesPage.UseVisualStyleBackColor = true;
			this.FeatureList.Columns.AddRange(new ColumnHeader[]
			{
				this.FeatureHdr
			});
			this.FeatureList.Dock = DockStyle.Fill;
			this.FeatureList.FullRowSelect = true;
			this.FeatureList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.FeatureList.HideSelection = false;
			this.FeatureList.Location = new Point(3, 28);
			this.FeatureList.MultiSelect = false;
			this.FeatureList.Name = "FeatureList";
			this.FeatureList.Size = new Size(335, 191);
			this.FeatureList.TabIndex = 1;
			this.FeatureList.UseCompatibleStateImageBehavior = false;
			this.FeatureList.View = View.Details;
			this.FeatureList.DoubleClick += new EventHandler(this.FeatureEditBtn_Click);
			this.FeatureHdr.Text = "Feature";
			this.FeatureHdr.Width = 300;
			this.FeatureToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.FeatureAddBtn,
				this.FeatureRemoveBtn,
				this.FeatureEditBtn
			});
			this.FeatureToolbar.Location = new Point(3, 3);
			this.FeatureToolbar.Name = "FeatureToolbar";
			this.FeatureToolbar.Size = new Size(335, 25);
			this.FeatureToolbar.TabIndex = 0;
			this.FeatureToolbar.Text = "toolStrip1";
			this.FeatureAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FeatureAddBtn.Image = (Image)resources.GetObject("FeatureAddBtn.Image");
			this.FeatureAddBtn.ImageTransparentColor = Color.Magenta;
			this.FeatureAddBtn.Name = "FeatureAddBtn";
			this.FeatureAddBtn.Size = new Size(33, 22);
			this.FeatureAddBtn.Text = "Add";
			this.FeatureAddBtn.Click += new EventHandler(this.FeatureAddBtn_Click);
			this.FeatureRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FeatureRemoveBtn.Image = (Image)resources.GetObject("FeatureRemoveBtn.Image");
			this.FeatureRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.FeatureRemoveBtn.Name = "FeatureRemoveBtn";
			this.FeatureRemoveBtn.Size = new Size(54, 22);
			this.FeatureRemoveBtn.Text = "Remove";
			this.FeatureRemoveBtn.Click += new EventHandler(this.FeatureRemoveBtn_Click);
			this.FeatureEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FeatureEditBtn.Image = (Image)resources.GetObject("FeatureEditBtn.Image");
			this.FeatureEditBtn.ImageTransparentColor = Color.Magenta;
			this.FeatureEditBtn.Name = "FeatureEditBtn";
			this.FeatureEditBtn.Size = new Size(31, 22);
			this.FeatureEditBtn.Text = "Edit";
			this.FeatureEditBtn.Click += new EventHandler(this.FeatureEditBtn_Click);
			this.PowersPage.Controls.Add(this.PowerList);
			this.PowersPage.Controls.Add(this.PowerToolbar);
			this.PowersPage.Location = new Point(4, 22);
			this.PowersPage.Name = "PowersPage";
			this.PowersPage.Padding = new Padding(3);
			this.PowersPage.Size = new Size(341, 222);
			this.PowersPage.TabIndex = 3;
			this.PowersPage.Text = "Powers";
			this.PowersPage.UseVisualStyleBackColor = true;
			this.PowerList.Columns.AddRange(new ColumnHeader[]
			{
				this.PowerHdr
			});
			this.PowerList.Dock = DockStyle.Fill;
			this.PowerList.FullRowSelect = true;
			this.PowerList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.PowerList.HideSelection = false;
			this.PowerList.Location = new Point(3, 28);
			this.PowerList.MultiSelect = false;
			this.PowerList.Name = "PowerList";
			this.PowerList.Size = new Size(335, 191);
			this.PowerList.TabIndex = 2;
			this.PowerList.UseCompatibleStateImageBehavior = false;
			this.PowerList.View = View.Details;
			this.PowerList.DoubleClick += new EventHandler(this.PowerEditBtn_Click);
			this.PowerHdr.Text = "Feature";
			this.PowerHdr.Width = 300;
			this.PowerToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.PowerAddBtn,
				this.PowerRemoveBtn,
				this.PowerEditBtn
			});
			this.PowerToolbar.Location = new Point(3, 3);
			this.PowerToolbar.Name = "PowerToolbar";
			this.PowerToolbar.Size = new Size(335, 25);
			this.PowerToolbar.TabIndex = 1;
			this.PowerToolbar.Text = "toolStrip2";
			this.PowerAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PowerAddBtn.Image = (Image)resources.GetObject("PowerAddBtn.Image");
			this.PowerAddBtn.ImageTransparentColor = Color.Magenta;
			this.PowerAddBtn.Name = "PowerAddBtn";
			this.PowerAddBtn.Size = new Size(33, 22);
			this.PowerAddBtn.Text = "Add";
			this.PowerAddBtn.Click += new EventHandler(this.PowerAddBtn_Click);
			this.PowerRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PowerRemoveBtn.Image = (Image)resources.GetObject("PowerRemoveBtn.Image");
			this.PowerRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.PowerRemoveBtn.Name = "PowerRemoveBtn";
			this.PowerRemoveBtn.Size = new Size(54, 22);
			this.PowerRemoveBtn.Text = "Remove";
			this.PowerRemoveBtn.Click += new EventHandler(this.PowerRemoveBtn_Click);
			this.PowerEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PowerEditBtn.Image = (Image)resources.GetObject("PowerEditBtn.Image");
			this.PowerEditBtn.ImageTransparentColor = Color.Magenta;
			this.PowerEditBtn.Name = "PowerEditBtn";
			this.PowerEditBtn.Size = new Size(31, 22);
			this.PowerEditBtn.Text = "Edit";
			this.PowerEditBtn.Click += new EventHandler(this.PowerEditBtn_Click);
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(205, 292);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(286, 292);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.QuoteLbl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.QuoteLbl.AutoSize = true;
			this.QuoteLbl.Location = new Point(6, 199);
			this.QuoteLbl.Name = "QuoteLbl";
			this.QuoteLbl.Size = new Size(39, 13);
			this.QuoteLbl.TabIndex = 1;
			this.QuoteLbl.Text = "Quote:";
			this.QuoteBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.QuoteBox.Location = new Point(51, 196);
			this.QuoteBox.Name = "QuoteBox";
			this.QuoteBox.Size = new Size(284, 20);
			this.QuoteBox.TabIndex = 2;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(373, 327);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OptionRaceForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Race";
			this.Pages.ResumeLayout(false);
			this.TraitsPage.ResumeLayout(false);
			this.TraitsPage.PerformLayout();
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.FeaturesPage.ResumeLayout(false);
			this.FeaturesPage.PerformLayout();
			this.FeatureToolbar.ResumeLayout(false);
			this.FeatureToolbar.PerformLayout();
			this.PowersPage.ResumeLayout(false);
			this.PowersPage.PerformLayout();
			this.PowerToolbar.ResumeLayout(false);
			this.PowerToolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public OptionRaceForm(Race race)
		{
			this.InitializeComponent();
			Array values = Enum.GetValues(typeof(CreatureSize));
			foreach (CreatureSize creatureSize in values)
			{
				this.SizeBox.Items.Add(creatureSize);
			}
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.fRace = race.Copy();
			this.NameBox.Text = this.fRace.Name;
			this.HeightBox.Text = this.fRace.HeightRange;
			this.WeightBox.Text = this.fRace.WeightRange;
			this.AbilityScoreBox.Text = this.fRace.AbilityScores;
			this.SizeBox.SelectedItem = this.fRace.Size;
			this.SpeedBox.Text = this.fRace.Speed;
			this.VisionBox.Text = this.fRace.Vision;
			this.LanguageBox.Text = this.fRace.Languages;
			this.SkillBonusBox.Text = this.fRace.SkillBonuses;
			this.DetailsBox.Text = this.fRace.Details;
			this.QuoteBox.Text = this.fRace.Quote;
			this.update_features();
			this.update_powers();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.FeatureRemoveBtn.Enabled = (this.SelectedFeature != null);
			this.FeatureEditBtn.Enabled = (this.SelectedFeature != null);
			this.PowerRemoveBtn.Enabled = (this.SelectedPower != null);
			this.PowerEditBtn.Enabled = (this.SelectedPower != null);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fRace.Name = this.NameBox.Text;
			this.fRace.HeightRange = this.HeightBox.Text;
			this.fRace.WeightRange = this.WeightBox.Text;
			this.fRace.AbilityScores = this.AbilityScoreBox.Text;
			this.fRace.Size = (CreatureSize)this.SizeBox.SelectedItem;
			this.fRace.Speed = this.SpeedBox.Text;
			this.fRace.Vision = this.VisionBox.Text;
			this.fRace.Languages = this.LanguageBox.Text;
			this.fRace.SkillBonuses = this.SkillBonusBox.Text;
			this.fRace.Details = this.DetailsBox.Text;
			this.fRace.Quote = this.QuoteBox.Text;
		}

		private void FeatureAddBtn_Click(object sender, EventArgs e)
		{
			OptionFeatureForm optionFeatureForm = new OptionFeatureForm(new Feature
			{
				Name = "New Feature"
			});
			if (optionFeatureForm.ShowDialog() == DialogResult.OK)
			{
				this.fRace.Features.Add(optionFeatureForm.Feature);
				this.update_features();
			}
		}

		private void FeatureRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedFeature != null)
			{
				this.fRace.Features.Remove(this.SelectedFeature);
				this.update_features();
			}
		}

		private void FeatureEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedFeature != null)
			{
				int index = this.fRace.Features.IndexOf(this.SelectedFeature);
				OptionFeatureForm optionFeatureForm = new OptionFeatureForm(this.SelectedFeature);
				if (optionFeatureForm.ShowDialog() == DialogResult.OK)
				{
					this.fRace.Features[index] = optionFeatureForm.Feature;
					this.update_features();
				}
			}
		}

		private void update_features()
		{
			this.FeatureList.Items.Clear();
			foreach (Feature current in this.fRace.Features)
			{
				ListViewItem listViewItem = this.FeatureList.Items.Add(current.Name);
				listViewItem.Tag = current;
			}
			if (this.fRace.Features.Count == 0)
			{
				ListViewItem listViewItem2 = this.FeatureList.Items.Add("(none)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
		}

		private void PowerAddBtn_Click(object sender, EventArgs e)
		{
			OptionPowerForm optionPowerForm = new OptionPowerForm(new PlayerPower
			{
				Name = "New Power"
			});
			if (optionPowerForm.ShowDialog() == DialogResult.OK)
			{
				this.fRace.Powers.Add(optionPowerForm.Power);
				this.update_powers();
			}
		}

		private void PowerRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedPower != null)
			{
				this.fRace.Powers.Remove(this.SelectedPower);
				this.update_powers();
			}
		}

		private void PowerEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedPower != null)
			{
				int index = this.fRace.Powers.IndexOf(this.SelectedPower);
				OptionPowerForm optionPowerForm = new OptionPowerForm(this.SelectedPower);
				if (optionPowerForm.ShowDialog() == DialogResult.OK)
				{
					this.fRace.Powers[index] = optionPowerForm.Power;
					this.update_powers();
				}
			}
		}

		private void update_powers()
		{
			this.PowerList.Items.Clear();
			foreach (PlayerPower current in this.fRace.Powers)
			{
				ListViewItem listViewItem = this.PowerList.Items.Add(current.Name);
				listViewItem.Tag = current;
			}
			if (this.fRace.Powers.Count == 0)
			{
				ListViewItem listViewItem2 = this.PowerList.Items.Add("(none)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
		}
	}
}
