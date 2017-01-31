using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class OptionClassForm : Form
	{
		private Class fClass;

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label NameLbl;

		private TextBox NameBox;

		private Label RoleLbl;

		private TextBox RoleBox;

		private Label PowerSourceLbl;

		private TextBox PowerSourceBox;

		private TabControl Pages;

		private TabPage GeneralPage;

		private TabPage ProficiencyPage;

		private TextBox KeyAbilityBox;

		private Label KeyAbilityLbl;

		private TextBox ImplementBox;

		private Label ImplementLbl;

		private TextBox WeaponBox;

		private Label WeaponLbl;

		private Label ArmourLbl;

		private TextBox ArmourBox;

		private NumericUpDown SurgeBox;

		private Label SurgeLbl;

		private NumericUpDown HPSubsequentBox;

		private Label HPSubsequentLbl;

		private NumericUpDown HPFirstBox;

		private Label HPFirstLbl;

		private TextBox SkillBox;

		private Label SkillLbl;

		private TabPage DescriptionPage;

		private TabPage OverviewPage;

		private TabPage LevelPage;

		private TextBox RacesBox;

		private Label RacesLbl;

		private TextBox ReligionBox;

		private Label ReligionLbl;

		private Label CharacteristicsLbl;

		private TextBox CharacteristicsBox;

		private ListView LevelList;

		private ColumnHeader LevelHdr;

		private ToolStrip LevelToolbar;

		private ToolStripButton EditBtn;

		private TextBox DefencesBox;

		private Label DefencesLbl;

		private TextBox QuoteBox;

		private Label QuoteLbl;

		private TextBox DescBox;

		public Class Class
		{
			get
			{
				return this.fClass;
			}
		}

		public LevelData SelectedLevel
		{
			get
			{
				if (this.LevelList.SelectedItems.Count != 0)
				{
					return this.LevelList.SelectedItems[0].Tag as LevelData;
				}
				return null;
			}
		}

		public OptionClassForm(Class c)
		{
			this.InitializeComponent();
			this.fClass = c.Copy();
			this.NameBox.Text = this.fClass.Name;
			this.RoleBox.Text = this.fClass.Role;
			this.PowerSourceBox.Text = this.fClass.PowerSource;
			this.KeyAbilityBox.Text = this.fClass.KeyAbilities;
			this.HPFirstBox.Value = this.fClass.HPFirst;
			this.HPSubsequentBox.Value = this.fClass.HPSubsequent;
			this.SurgeBox.Value = this.fClass.HealingSurges;
			this.ArmourBox.Text = this.fClass.ArmourProficiencies;
			this.WeaponBox.Text = this.fClass.WeaponProficiencies;
			this.ImplementBox.Text = this.fClass.Implements;
			this.DefencesBox.Text = this.fClass.DefenceBonuses;
			this.SkillBox.Text = this.fClass.TrainedSkills;
			this.DescBox.Text = this.fClass.Description;
			this.QuoteBox.Text = this.fClass.Quote;
			this.CharacteristicsBox.Text = this.fClass.OverviewCharacteristics;
			this.ReligionBox.Text = this.fClass.OverviewReligion;
			this.RacesBox.Text = this.fClass.OverviewRaces;
			this.update_levels();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fClass.Name = this.NameBox.Text;
			this.fClass.Role = this.RoleBox.Text;
			this.fClass.PowerSource = this.PowerSourceBox.Text;
			this.fClass.KeyAbilities = this.KeyAbilityBox.Text;
			this.fClass.HPFirst = (int)this.HPFirstBox.Value;
			this.fClass.HPSubsequent = (int)this.HPSubsequentBox.Value;
			this.fClass.HealingSurges = (int)this.SurgeBox.Value;
			this.fClass.ArmourProficiencies = this.ArmourBox.Text;
			this.fClass.WeaponProficiencies = this.WeaponBox.Text;
			this.fClass.Implements = this.ImplementBox.Text;
			this.fClass.DefenceBonuses = this.DefencesBox.Text;
			this.fClass.TrainedSkills = this.SkillBox.Text;
			this.fClass.Description = this.DescBox.Text;
			this.fClass.Quote = this.QuoteBox.Text;
			this.fClass.OverviewCharacteristics = this.CharacteristicsBox.Text;
			this.fClass.OverviewReligion = this.ReligionBox.Text;
			this.fClass.OverviewRaces = this.RacesBox.Text;
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLevel != null)
			{
				int num = this.fClass.Levels.IndexOf(this.SelectedLevel);
				bool flag = num == -1;
				OptionLevelForm optionLevelForm = new OptionLevelForm(this.SelectedLevel, flag);
				if (optionLevelForm.ShowDialog() == DialogResult.OK)
				{
					if (flag)
					{
						this.fClass.FeatureData = optionLevelForm.Level;
					}
					else
					{
						this.fClass.Levels[num] = optionLevelForm.Level;
					}
					this.update_levels();
				}
			}
		}

		private void update_levels()
		{
			this.LevelList.Items.Clear();
			this.add_level(this.fClass.FeatureData);
			foreach (LevelData current in this.fClass.Levels)
			{
				this.add_level(current);
			}
		}

		private void add_level(LevelData ld)
		{
			ListViewItem listViewItem = this.LevelList.Items.Add(ld.ToString());
			listViewItem.Tag = ld;
			if (ld.Count == 0)
			{
				listViewItem.ForeColor = SystemColors.GrayText;
			}
			if (ld == this.fClass.FeatureData)
			{
				listViewItem.Group = this.LevelList.Groups[0];
				return;
			}
			int num = (ld.Level - 1) / 10;
			listViewItem.Group = this.LevelList.Groups[num + 1];
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
			ListViewGroup listViewGroup = new ListViewGroup("Class Features", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Heroic Tier", HorizontalAlignment.Left);
			ListViewGroup listViewGroup3 = new ListViewGroup("Paragon Tier", HorizontalAlignment.Left);
			ListViewGroup listViewGroup4 = new ListViewGroup("Epic Tier", HorizontalAlignment.Left);
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OptionClassForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.RoleLbl = new Label();
			this.RoleBox = new TextBox();
			this.PowerSourceLbl = new Label();
			this.PowerSourceBox = new TextBox();
			this.Pages = new TabControl();
			this.GeneralPage = new TabPage();
			this.SurgeBox = new NumericUpDown();
			this.SurgeLbl = new Label();
			this.HPSubsequentBox = new NumericUpDown();
			this.HPSubsequentLbl = new Label();
			this.HPFirstBox = new NumericUpDown();
			this.HPFirstLbl = new Label();
			this.KeyAbilityBox = new TextBox();
			this.KeyAbilityLbl = new Label();
			this.ProficiencyPage = new TabPage();
			this.SkillBox = new TextBox();
			this.SkillLbl = new Label();
			this.DefencesBox = new TextBox();
			this.DefencesLbl = new Label();
			this.ImplementBox = new TextBox();
			this.ImplementLbl = new Label();
			this.WeaponBox = new TextBox();
			this.WeaponLbl = new Label();
			this.ArmourBox = new TextBox();
			this.ArmourLbl = new Label();
			this.DescriptionPage = new TabPage();
			this.QuoteBox = new TextBox();
			this.QuoteLbl = new Label();
			this.DescBox = new TextBox();
			this.OverviewPage = new TabPage();
			this.RacesBox = new TextBox();
			this.RacesLbl = new Label();
			this.ReligionBox = new TextBox();
			this.ReligionLbl = new Label();
			this.CharacteristicsBox = new TextBox();
			this.CharacteristicsLbl = new Label();
			this.LevelPage = new TabPage();
			this.LevelList = new ListView();
			this.LevelHdr = new ColumnHeader();
			this.LevelToolbar = new ToolStrip();
			this.EditBtn = new ToolStripButton();
			this.Pages.SuspendLayout();
			this.GeneralPage.SuspendLayout();
			((ISupportInitialize)this.SurgeBox).BeginInit();
			((ISupportInitialize)this.HPSubsequentBox).BeginInit();
			((ISupportInitialize)this.HPFirstBox).BeginInit();
			this.ProficiencyPage.SuspendLayout();
			this.DescriptionPage.SuspendLayout();
			this.OverviewPage.SuspendLayout();
			this.LevelPage.SuspendLayout();
			this.LevelToolbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(205, 300);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(286, 300);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(6, 9);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(94, 6);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(224, 20);
			this.NameBox.TabIndex = 1;
			this.RoleLbl.AutoSize = true;
			this.RoleLbl.Location = new Point(6, 35);
			this.RoleLbl.Name = "RoleLbl";
			this.RoleLbl.Size = new Size(32, 13);
			this.RoleLbl.TabIndex = 2;
			this.RoleLbl.Text = "Role:";
			this.RoleBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RoleBox.Location = new Point(94, 32);
			this.RoleBox.Multiline = true;
			this.RoleBox.Name = "RoleBox";
			this.RoleBox.ScrollBars = ScrollBars.Vertical;
			this.RoleBox.Size = new Size(224, 51);
			this.RoleBox.TabIndex = 3;
			this.PowerSourceLbl.AutoSize = true;
			this.PowerSourceLbl.Location = new Point(6, 92);
			this.PowerSourceLbl.Name = "PowerSourceLbl";
			this.PowerSourceLbl.Size = new Size(77, 13);
			this.PowerSourceLbl.TabIndex = 4;
			this.PowerSourceLbl.Text = "Power Source:";
			this.PowerSourceBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.PowerSourceBox.Location = new Point(94, 89);
			this.PowerSourceBox.Multiline = true;
			this.PowerSourceBox.Name = "PowerSourceBox";
			this.PowerSourceBox.ScrollBars = ScrollBars.Vertical;
			this.PowerSourceBox.Size = new Size(224, 51);
			this.PowerSourceBox.TabIndex = 5;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.GeneralPage);
			this.Pages.Controls.Add(this.ProficiencyPage);
			this.Pages.Controls.Add(this.DescriptionPage);
			this.Pages.Controls.Add(this.OverviewPage);
			this.Pages.Controls.Add(this.LevelPage);
			this.Pages.Location = new Point(12, 12);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(349, 282);
			this.Pages.TabIndex = 0;
			this.GeneralPage.Controls.Add(this.SurgeBox);
			this.GeneralPage.Controls.Add(this.SurgeLbl);
			this.GeneralPage.Controls.Add(this.HPSubsequentBox);
			this.GeneralPage.Controls.Add(this.HPSubsequentLbl);
			this.GeneralPage.Controls.Add(this.HPFirstBox);
			this.GeneralPage.Controls.Add(this.HPFirstLbl);
			this.GeneralPage.Controls.Add(this.KeyAbilityBox);
			this.GeneralPage.Controls.Add(this.KeyAbilityLbl);
			this.GeneralPage.Controls.Add(this.PowerSourceBox);
			this.GeneralPage.Controls.Add(this.PowerSourceLbl);
			this.GeneralPage.Controls.Add(this.RoleBox);
			this.GeneralPage.Controls.Add(this.RoleLbl);
			this.GeneralPage.Controls.Add(this.NameBox);
			this.GeneralPage.Controls.Add(this.NameLbl);
			this.GeneralPage.Location = new Point(4, 22);
			this.GeneralPage.Name = "GeneralPage";
			this.GeneralPage.Padding = new Padding(3);
			this.GeneralPage.Size = new Size(341, 256);
			this.GeneralPage.TabIndex = 0;
			this.GeneralPage.Text = "General";
			this.GeneralPage.UseVisualStyleBackColor = true;
			this.SurgeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SurgeBox.Location = new Point(94, 224);
			this.SurgeBox.Name = "SurgeBox";
			this.SurgeBox.Size = new Size(224, 20);
			this.SurgeBox.TabIndex = 13;
			this.SurgeLbl.AutoSize = true;
			this.SurgeLbl.Location = new Point(6, 226);
			this.SurgeLbl.Name = "SurgeLbl";
			this.SurgeLbl.Size = new Size(82, 13);
			this.SurgeLbl.TabIndex = 12;
			this.SurgeLbl.Text = "Healing Surges:";
			this.HPSubsequentBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.HPSubsequentBox.Location = new Point(94, 198);
			this.HPSubsequentBox.Name = "HPSubsequentBox";
			this.HPSubsequentBox.Size = new Size(224, 20);
			this.HPSubsequentBox.TabIndex = 11;
			this.HPSubsequentLbl.AutoSize = true;
			this.HPSubsequentLbl.Location = new Point(6, 200);
			this.HPSubsequentLbl.Name = "HPSubsequentLbl";
			this.HPSubsequentLbl.Size = new Size(74, 13);
			this.HPSubsequentLbl.TabIndex = 10;
			this.HPSubsequentLbl.Text = "HP (per level):";
			this.HPFirstBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.HPFirstBox.Location = new Point(94, 172);
			this.HPFirstBox.Name = "HPFirstBox";
			this.HPFirstBox.Size = new Size(224, 20);
			this.HPFirstBox.TabIndex = 9;
			this.HPFirstLbl.AutoSize = true;
			this.HPFirstLbl.Location = new Point(6, 174);
			this.HPFirstLbl.Name = "HPFirstLbl";
			this.HPFirstLbl.Size = new Size(75, 13);
			this.HPFirstLbl.TabIndex = 8;
			this.HPFirstLbl.Text = "HP (first level):";
			this.KeyAbilityBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.KeyAbilityBox.Location = new Point(94, 146);
			this.KeyAbilityBox.Name = "KeyAbilityBox";
			this.KeyAbilityBox.Size = new Size(224, 20);
			this.KeyAbilityBox.TabIndex = 7;
			this.KeyAbilityLbl.AutoSize = true;
			this.KeyAbilityLbl.Location = new Point(6, 149);
			this.KeyAbilityLbl.Name = "KeyAbilityLbl";
			this.KeyAbilityLbl.Size = new Size(66, 13);
			this.KeyAbilityLbl.TabIndex = 6;
			this.KeyAbilityLbl.Text = "Key Abilities:";
			this.ProficiencyPage.Controls.Add(this.SkillBox);
			this.ProficiencyPage.Controls.Add(this.SkillLbl);
			this.ProficiencyPage.Controls.Add(this.DefencesBox);
			this.ProficiencyPage.Controls.Add(this.DefencesLbl);
			this.ProficiencyPage.Controls.Add(this.ImplementBox);
			this.ProficiencyPage.Controls.Add(this.ImplementLbl);
			this.ProficiencyPage.Controls.Add(this.WeaponBox);
			this.ProficiencyPage.Controls.Add(this.WeaponLbl);
			this.ProficiencyPage.Controls.Add(this.ArmourBox);
			this.ProficiencyPage.Controls.Add(this.ArmourLbl);
			this.ProficiencyPage.Location = new Point(4, 22);
			this.ProficiencyPage.Name = "ProficiencyPage";
			this.ProficiencyPage.Padding = new Padding(3);
			this.ProficiencyPage.Size = new Size(341, 256);
			this.ProficiencyPage.TabIndex = 1;
			this.ProficiencyPage.Text = "Proficiencies";
			this.ProficiencyPage.UseVisualStyleBackColor = true;
			this.SkillBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.SkillBox.Location = new Point(91, 185);
			this.SkillBox.Multiline = true;
			this.SkillBox.Name = "SkillBox";
			this.SkillBox.ScrollBars = ScrollBars.Vertical;
			this.SkillBox.Size = new Size(237, 65);
			this.SkillBox.TabIndex = 9;
			this.SkillLbl.Location = new Point(6, 188);
			this.SkillLbl.Name = "SkillLbl";
			this.SkillLbl.Size = new Size(79, 62);
			this.SkillLbl.TabIndex = 8;
			this.SkillLbl.Text = "Trained Skills:";
			this.DefencesBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DefencesBox.Location = new Point(91, 159);
			this.DefencesBox.Name = "DefencesBox";
			this.DefencesBox.Size = new Size(237, 20);
			this.DefencesBox.TabIndex = 7;
			this.DefencesLbl.AutoSize = true;
			this.DefencesLbl.Location = new Point(6, 162);
			this.DefencesLbl.Name = "DefencesLbl";
			this.DefencesLbl.Size = new Size(56, 13);
			this.DefencesLbl.TabIndex = 6;
			this.DefencesLbl.Text = "Defences:";
			this.ImplementBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ImplementBox.Location = new Point(91, 108);
			this.ImplementBox.Multiline = true;
			this.ImplementBox.Name = "ImplementBox";
			this.ImplementBox.ScrollBars = ScrollBars.Vertical;
			this.ImplementBox.Size = new Size(237, 45);
			this.ImplementBox.TabIndex = 5;
			this.ImplementLbl.Location = new Point(6, 111);
			this.ImplementLbl.Name = "ImplementLbl";
			this.ImplementLbl.Size = new Size(79, 42);
			this.ImplementLbl.TabIndex = 4;
			this.ImplementLbl.Text = "Implements:";
			this.WeaponBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.WeaponBox.Location = new Point(91, 57);
			this.WeaponBox.Multiline = true;
			this.WeaponBox.Name = "WeaponBox";
			this.WeaponBox.ScrollBars = ScrollBars.Vertical;
			this.WeaponBox.Size = new Size(237, 45);
			this.WeaponBox.TabIndex = 3;
			this.WeaponLbl.Location = new Point(6, 60);
			this.WeaponLbl.Name = "WeaponLbl";
			this.WeaponLbl.Size = new Size(79, 42);
			this.WeaponLbl.TabIndex = 2;
			this.WeaponLbl.Text = "Weapon Proficiencies:";
			this.ArmourBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ArmourBox.Location = new Point(91, 6);
			this.ArmourBox.Multiline = true;
			this.ArmourBox.Name = "ArmourBox";
			this.ArmourBox.ScrollBars = ScrollBars.Vertical;
			this.ArmourBox.Size = new Size(237, 45);
			this.ArmourBox.TabIndex = 1;
			this.ArmourLbl.Location = new Point(6, 9);
			this.ArmourLbl.Name = "ArmourLbl";
			this.ArmourLbl.Size = new Size(79, 42);
			this.ArmourLbl.TabIndex = 0;
			this.ArmourLbl.Text = "Armour Proficiencies:";
			this.DescriptionPage.Controls.Add(this.QuoteBox);
			this.DescriptionPage.Controls.Add(this.QuoteLbl);
			this.DescriptionPage.Controls.Add(this.DescBox);
			this.DescriptionPage.Location = new Point(4, 22);
			this.DescriptionPage.Name = "DescriptionPage";
			this.DescriptionPage.Padding = new Padding(3);
			this.DescriptionPage.Size = new Size(341, 256);
			this.DescriptionPage.TabIndex = 2;
			this.DescriptionPage.Text = "Description";
			this.DescriptionPage.UseVisualStyleBackColor = true;
			this.QuoteBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.QuoteBox.Location = new Point(51, 230);
			this.QuoteBox.Name = "QuoteBox";
			this.QuoteBox.Size = new Size(284, 20);
			this.QuoteBox.TabIndex = 5;
			this.QuoteLbl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.QuoteLbl.AutoSize = true;
			this.QuoteLbl.Location = new Point(6, 233);
			this.QuoteLbl.Name = "QuoteLbl";
			this.QuoteLbl.Size = new Size(39, 13);
			this.QuoteLbl.TabIndex = 4;
			this.QuoteLbl.Text = "Quote:";
			this.DescBox.AcceptsReturn = true;
			this.DescBox.AcceptsTab = true;
			this.DescBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.DescBox.Location = new Point(6, 6);
			this.DescBox.Multiline = true;
			this.DescBox.Name = "DescBox";
			this.DescBox.ScrollBars = ScrollBars.Vertical;
			this.DescBox.Size = new Size(329, 218);
			this.DescBox.TabIndex = 3;
			this.OverviewPage.Controls.Add(this.RacesBox);
			this.OverviewPage.Controls.Add(this.RacesLbl);
			this.OverviewPage.Controls.Add(this.ReligionBox);
			this.OverviewPage.Controls.Add(this.ReligionLbl);
			this.OverviewPage.Controls.Add(this.CharacteristicsBox);
			this.OverviewPage.Controls.Add(this.CharacteristicsLbl);
			this.OverviewPage.Location = new Point(4, 22);
			this.OverviewPage.Name = "OverviewPage";
			this.OverviewPage.Padding = new Padding(3);
			this.OverviewPage.Size = new Size(341, 256);
			this.OverviewPage.TabIndex = 3;
			this.OverviewPage.Text = "Overview";
			this.OverviewPage.UseVisualStyleBackColor = true;
			this.RacesBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.RacesBox.Location = new Point(91, 199);
			this.RacesBox.Multiline = true;
			this.RacesBox.Name = "RacesBox";
			this.RacesBox.ScrollBars = ScrollBars.Vertical;
			this.RacesBox.Size = new Size(237, 51);
			this.RacesBox.TabIndex = 5;
			this.RacesLbl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.RacesLbl.AutoSize = true;
			this.RacesLbl.Location = new Point(6, 202);
			this.RacesLbl.Name = "RacesLbl";
			this.RacesLbl.Size = new Size(41, 13);
			this.RacesLbl.TabIndex = 4;
			this.RacesLbl.Text = "Races:";
			this.ReligionBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.ReligionBox.Location = new Point(91, 142);
			this.ReligionBox.Multiline = true;
			this.ReligionBox.Name = "ReligionBox";
			this.ReligionBox.ScrollBars = ScrollBars.Vertical;
			this.ReligionBox.Size = new Size(237, 51);
			this.ReligionBox.TabIndex = 3;
			this.ReligionLbl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.ReligionLbl.AutoSize = true;
			this.ReligionLbl.Location = new Point(6, 145);
			this.ReligionLbl.Name = "ReligionLbl";
			this.ReligionLbl.Size = new Size(48, 13);
			this.ReligionLbl.TabIndex = 2;
			this.ReligionLbl.Text = "Religion:";
			this.CharacteristicsBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.CharacteristicsBox.Location = new Point(91, 6);
			this.CharacteristicsBox.Multiline = true;
			this.CharacteristicsBox.Name = "CharacteristicsBox";
			this.CharacteristicsBox.ScrollBars = ScrollBars.Vertical;
			this.CharacteristicsBox.Size = new Size(237, 130);
			this.CharacteristicsBox.TabIndex = 1;
			this.CharacteristicsLbl.AutoSize = true;
			this.CharacteristicsLbl.Location = new Point(6, 9);
			this.CharacteristicsLbl.Name = "CharacteristicsLbl";
			this.CharacteristicsLbl.Size = new Size(79, 13);
			this.CharacteristicsLbl.TabIndex = 0;
			this.CharacteristicsLbl.Text = "Characteristics:";
			this.LevelPage.Controls.Add(this.LevelList);
			this.LevelPage.Controls.Add(this.LevelToolbar);
			this.LevelPage.Location = new Point(4, 22);
			this.LevelPage.Name = "LevelPage";
			this.LevelPage.Padding = new Padding(3);
			this.LevelPage.Size = new Size(341, 256);
			this.LevelPage.TabIndex = 4;
			this.LevelPage.Text = "Levels";
			this.LevelPage.UseVisualStyleBackColor = true;
			this.LevelList.Columns.AddRange(new ColumnHeader[]
			{
				this.LevelHdr
			});
			this.LevelList.Dock = DockStyle.Fill;
			this.LevelList.FullRowSelect = true;
			listViewGroup.Header = "Class Features";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup2.Header = "Heroic Tier";
			listViewGroup2.Name = "listViewGroup2";
			listViewGroup3.Header = "Paragon Tier";
			listViewGroup3.Name = "listViewGroup3";
			listViewGroup4.Header = "Epic Tier";
			listViewGroup4.Name = "listViewGroup4";
			this.LevelList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2,
				listViewGroup3,
				listViewGroup4
			});
			this.LevelList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.LevelList.HideSelection = false;
			this.LevelList.Location = new Point(3, 28);
			this.LevelList.MultiSelect = false;
			this.LevelList.Name = "LevelList";
			this.LevelList.Size = new Size(335, 225);
			this.LevelList.TabIndex = 1;
			this.LevelList.UseCompatibleStateImageBehavior = false;
			this.LevelList.View = View.Details;
			this.LevelList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.LevelHdr.Text = "Level";
			this.LevelHdr.Width = 300;
			this.LevelToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.EditBtn
			});
			this.LevelToolbar.Location = new Point(3, 3);
			this.LevelToolbar.Name = "LevelToolbar";
			this.LevelToolbar.Size = new Size(335, 25);
			this.LevelToolbar.TabIndex = 0;
			this.LevelToolbar.Text = "toolStrip1";
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)componentResourceManager.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new Size(31, 22);
			this.EditBtn.Text = "Edit";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(373, 335);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OptionClassForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Class";
			this.Pages.ResumeLayout(false);
			this.GeneralPage.ResumeLayout(false);
			this.GeneralPage.PerformLayout();
			((ISupportInitialize)this.SurgeBox).EndInit();
			((ISupportInitialize)this.HPSubsequentBox).EndInit();
			((ISupportInitialize)this.HPFirstBox).EndInit();
			this.ProficiencyPage.ResumeLayout(false);
			this.ProficiencyPage.PerformLayout();
			this.DescriptionPage.ResumeLayout(false);
			this.DescriptionPage.PerformLayout();
			this.OverviewPage.ResumeLayout(false);
			this.OverviewPage.PerformLayout();
			this.LevelPage.ResumeLayout(false);
			this.LevelPage.PerformLayout();
			this.LevelToolbar.ResumeLayout(false);
			this.LevelToolbar.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
