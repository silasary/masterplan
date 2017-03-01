using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.Tools.Import;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class HeroForm : Form
	{
		private Hero fHero;

		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private Label PlayerLbl;

		private Button OKBtn;

		private Button CancelBtn;

		private Label SizeLbl;

		private ComboBox SizeBox;

		private TextBox PlayerBox;

		private Label RaceLbl;

		private TextBox RaceBox;

		private Label ClassLbl;

		private TextBox ClassBox;

		private Label SourceLbl;

		private Label RoleLbl;

		private ComboBox RoleBox;

		private ComboBox SourceBox;

		private TextBox PPBox;

		private Label PPLbl;

		private TextBox EDBox;

		private Label EDLbl;

		private GroupBox ClassGroup;

		private GroupBox RaceGroup;

		private GroupBox NameGroup;

		private TabControl Pages;

		private TabPage GeneralPage;

		private TabPage AdvancedPage;

		private Panel PortraitPanel;

		private PictureBox PortraitBox;

		private ToolStrip PortraitToolbar;

		private ToolStripButton PortraitBrowse;

		private ToolStripButton PortraitClear;

		private GroupBox LanguageGroup;

		private TextBox LanguageBox;

		private GroupBox PortraitGroup;

		private TabPage StatsPage;

		private GroupBox SkillsGroup;

		private NumericUpDown InsightBox;

		private NumericUpDown PerceptionBox;

		private Label InsightLbl;

		private Label PerceptionLbl;

		private GroupBox DefencesGroup;

		private NumericUpDown RefBox;

		private NumericUpDown WillBox;

		private Label RefLbl;

		private Label WillLbl;

		private NumericUpDown ACBox;

		private NumericUpDown FortBox;

		private Label ACLbl;

		private Label FortLbl;

		private GroupBox HPGroup;

		private NumericUpDown HPBox;

		private Label HPLbl;

		private GroupBox InitGroup;

		private NumericUpDown InitBox;

		private Label InitLbl;

		private NumericUpDown LevelBox;

		private Label LevelLbl;

		private TabPage EffectsPage;

		private ListView EffectList;

		private ToolStrip EffectToolbar;

		private ToolStripButton EffectRemoveBtn;

		private ToolStripButton EffectEditBtn;

		private ColumnHeader EffectHdr;

		private Label EffectLbl;

		private ToolStripButton PortraitPasteBtn;

		private Button iPlay4eBtn;

		private ToolStripDropDownButton EffectAddBtn;

		private ToolStripMenuItem effectToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem EffectAddToken;

		private ToolStripMenuItem EffectAddOverlay;

		public Hero Hero
		{
			get
			{
				return this.fHero;
			}
		}

		public OngoingCondition SelectedEffect
		{
			get
			{
				if (this.EffectList.SelectedItems.Count != 0)
				{
					return this.EffectList.SelectedItems[0].Tag as OngoingCondition;
				}
				return null;
			}
		}

		public CustomToken SelectedToken
		{
			get
			{
				if (this.EffectList.SelectedItems.Count != 0)
				{
					return this.EffectList.SelectedItems[0].Tag as CustomToken;
				}
				return null;
			}
		}

		public HeroForm(Hero h)
		{
			this.InitializeComponent();
			foreach (CreatureSize creatureSize in Enum.GetValues(typeof(CreatureSize)))
			{
				this.SizeBox.Items.Add(creatureSize);
			}
			foreach (HeroRoleType heroRoleType in Enum.GetValues(typeof(HeroRoleType)))
			{
				this.RoleBox.Items.Add(heroRoleType);
			}
			this.SourceBox.Items.Add("Arcane");
			this.SourceBox.Items.Add("Divine");
			this.SourceBox.Items.Add("Elemental");
			this.SourceBox.Items.Add("Martial");
			this.SourceBox.Items.Add("Primal");
			this.SourceBox.Items.Add("Psionic");
			this.SourceBox.Items.Add("Shadow");
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.fHero = h.Copy();
			this.iPlay4eBtn.Visible = (this.fHero.Key != "");
			this.update_hero();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.PortraitPasteBtn.Enabled = Clipboard.ContainsImage();
			this.PortraitClear.Enabled = (this.fHero.Portrait != null);
			this.EffectRemoveBtn.Enabled = (this.SelectedEffect != null || this.SelectedToken != null);
			this.EffectEditBtn.Enabled = (this.SelectedEffect != null || this.SelectedToken != null);
		}

		private void HeroForm_Shown(object sender, EventArgs e)
		{
			this.NameBox.Focus();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fHero.Name = this.NameBox.Text;
			this.fHero.Player = this.PlayerBox.Text;
			this.fHero.Level = (int)this.LevelBox.Value;
			this.fHero.Race = this.RaceBox.Text;
			this.fHero.Size = (CreatureSize)this.SizeBox.SelectedItem;
			this.fHero.Class = this.ClassBox.Text;
			this.fHero.ParagonPath = this.PPBox.Text;
			this.fHero.EpicDestiny = this.EDBox.Text;
			this.fHero.PowerSource = this.SourceBox.Text;
			this.fHero.Role = (HeroRoleType)this.RoleBox.SelectedItem;
			this.fHero.Languages = this.LanguageBox.Text;
			this.fHero.HP = (int)this.HPBox.Value;
			this.fHero.AC = (int)this.ACBox.Value;
			this.fHero.Fortitude = (int)this.FortBox.Value;
			this.fHero.Reflex = (int)this.RefBox.Value;
			this.fHero.Will = (int)this.WillBox.Value;
			this.fHero.InitBonus = (int)this.InitBox.Value;
			this.fHero.PassiveInsight = (int)this.InsightBox.Value;
			this.fHero.PassivePerception = (int)this.PerceptionBox.Value;
		}

		private void PortraitBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = Program.ImageFilter;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.fHero.Portrait = Image.FromFile(openFileDialog.FileName);
				Program.SetResolution(this.fHero.Portrait);
				this.image_changed();
			}
		}

		private void PortraitPaste_Click(object sender, EventArgs e)
		{
			if (Clipboard.ContainsImage())
			{
				this.fHero.Portrait = Clipboard.GetImage();
				Program.SetResolution(this.fHero.Portrait);
				this.image_changed();
			}
		}

		private void PortraitClear_Click(object sender, EventArgs e)
		{
			this.fHero.Portrait = null;
			this.image_changed();
		}

		private void EffectAddBtn_Click(object sender, EventArgs e)
		{
			OngoingCondition condition = new OngoingCondition();
			EffectForm effectForm = new EffectForm(condition, this.fHero);
			if (effectForm.ShowDialog() == DialogResult.OK)
			{
				this.fHero.Effects.Add(effectForm.Effect);
				this.update_effects();
			}
		}

		private void EffectAddToken_Click(object sender, EventArgs e)
		{
			CustomTokenForm customTokenForm = new CustomTokenForm(new CustomToken
			{
				Name = "New Token",
				Type = CustomTokenType.Token
			});
			if (customTokenForm.ShowDialog() == DialogResult.OK)
			{
				this.fHero.Tokens.Add(customTokenForm.Token);
				this.update_effects();
			}
		}

		private void EffectAddOverlay_Click(object sender, EventArgs e)
		{
			CustomOverlayForm customOverlayForm = new CustomOverlayForm(new CustomToken
			{
				Name = "New Overlay",
				Type = CustomTokenType.Overlay
			});
			if (customOverlayForm.ShowDialog() == DialogResult.OK)
			{
				this.fHero.Tokens.Add(customOverlayForm.Token);
				this.update_effects();
			}
		}

		private void EffectRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedEffect != null)
			{
				this.fHero.Effects.Remove(this.SelectedEffect);
				this.update_effects();
			}
			if (this.SelectedToken != null)
			{
				this.fHero.Tokens.Remove(this.SelectedToken);
				this.update_effects();
			}
		}

		private void EffectEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedEffect != null)
			{
				int index = this.fHero.Effects.IndexOf(this.SelectedEffect);
				EffectForm effectForm = new EffectForm(this.SelectedEffect, this.fHero);
				if (effectForm.ShowDialog() == DialogResult.OK)
				{
					this.fHero.Effects[index] = effectForm.Effect;
					this.update_effects();
				}
			}
			if (this.SelectedToken != null)
			{
				int index2 = this.fHero.Tokens.IndexOf(this.SelectedToken);
				switch (this.SelectedToken.Type)
				{
				case CustomTokenType.Token:
				{
					CustomTokenForm customTokenForm = new CustomTokenForm(this.SelectedToken);
					if (customTokenForm.ShowDialog() == DialogResult.OK)
					{
						this.fHero.Tokens[index2] = customTokenForm.Token;
						this.update_effects();
						return;
					}
					break;
				}
				case CustomTokenType.Overlay:
				{
					CustomOverlayForm customOverlayForm = new CustomOverlayForm(this.SelectedToken);
					if (customOverlayForm.ShowDialog() == DialogResult.OK)
					{
						this.fHero.Tokens[index2] = customOverlayForm.Token;
						this.update_effects();
					}
					break;
				}
				default:
					return;
				}
			}
		}

		private void image_changed()
		{
			this.PortraitBox.Image = this.fHero.Portrait;
		}

		private void update_hero()
		{
			this.NameBox.Text = this.fHero.Name;
			this.PlayerBox.Text = this.fHero.Player;
			this.LevelBox.Value = this.fHero.Level;
			this.RaceBox.Text = this.fHero.Race;
			this.SizeBox.SelectedItem = this.fHero.Size;
			this.ClassBox.Text = this.fHero.Class;
			this.PPBox.Text = this.fHero.ParagonPath;
			this.EDBox.Text = this.fHero.EpicDestiny;
			this.SourceBox.Text = this.fHero.PowerSource;
			this.RoleBox.SelectedItem = this.fHero.Role;
			this.LanguageBox.Text = this.fHero.Languages;
			if (this.fHero.Portrait != null)
			{
				this.PortraitBox.Image = this.fHero.Portrait;
			}
			this.HPBox.Value = this.fHero.HP;
			this.ACBox.Value = this.fHero.AC;
			this.FortBox.Value = this.fHero.Fortitude;
			this.RefBox.Value = this.fHero.Reflex;
			this.WillBox.Value = this.fHero.Will;
			this.InitBox.Value = this.fHero.InitBonus;
			this.InsightBox.Value = this.fHero.PassiveInsight;
			this.PerceptionBox.Value = this.fHero.PassivePerception;
			this.update_effects();
		}

		private void update_effects()
		{
			this.EffectList.Items.Clear();
			foreach (OngoingCondition current in this.fHero.Effects)
			{
				ListViewItem listViewItem = this.EffectList.Items.Add(current.ToString(null, false));
				listViewItem.Tag = current;
				listViewItem.Group = this.EffectList.Groups[0];
			}
			foreach (CustomToken current2 in this.fHero.Tokens)
			{
				ListViewItem listViewItem2 = this.EffectList.Items.Add(current2.Name);
				listViewItem2.Tag = current2;
				switch (current2.Type)
				{
				case CustomTokenType.Token:
					listViewItem2.Group = this.EffectList.Groups[1];
					break;
				case CustomTokenType.Overlay:
					listViewItem2.Group = this.EffectList.Groups[2];
					break;
				}
			}
		}

		private void iPlay4eBtn_Click(object sender, EventArgs e)
		{
			HeroIPlay4eForm heroIPlay4eForm = new HeroIPlay4eForm(this.fHero.Key, true);
			if (heroIPlay4eForm.ShowDialog() == DialogResult.OK)
			{
				Hero hero = new Hero();
				hero.Key = this.fHero.Key;
                if (AppImport.ImportExternalHero(hero))
				{
					hero.ID = this.fHero.ID;
					hero.Effects.AddRange(this.fHero.Effects);
					this.fHero = hero;
					this.update_hero();
					return;
				}
				MessageBox.Show("The External character could not be downloaded.", "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(HeroForm));
			ListViewGroup listViewGroup = new ListViewGroup("Effects", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Map Tokens", HorizontalAlignment.Left);
			ListViewGroup listViewGroup3 = new ListViewGroup("Map Overlays", HorizontalAlignment.Left);
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.PlayerLbl = new Label();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.SizeLbl = new Label();
			this.SizeBox = new ComboBox();
			this.PlayerBox = new TextBox();
			this.RaceLbl = new Label();
			this.RaceBox = new TextBox();
			this.ClassLbl = new Label();
			this.ClassBox = new TextBox();
			this.SourceLbl = new Label();
			this.RoleLbl = new Label();
			this.RoleBox = new ComboBox();
			this.SourceBox = new ComboBox();
			this.PPBox = new TextBox();
			this.PPLbl = new Label();
			this.EDBox = new TextBox();
			this.EDLbl = new Label();
			this.ClassGroup = new GroupBox();
			this.RaceGroup = new GroupBox();
			this.NameGroup = new GroupBox();
			this.LevelBox = new NumericUpDown();
			this.LevelLbl = new Label();
			this.Pages = new TabControl();
			this.GeneralPage = new TabPage();
			this.AdvancedPage = new TabPage();
			this.LanguageGroup = new GroupBox();
			this.LanguageBox = new TextBox();
			this.PortraitGroup = new GroupBox();
			this.PortraitPanel = new Panel();
			this.PortraitBox = new PictureBox();
			this.PortraitToolbar = new ToolStrip();
			this.PortraitBrowse = new ToolStripButton();
			this.PortraitPasteBtn = new ToolStripButton();
			this.PortraitClear = new ToolStripButton();
			this.StatsPage = new TabPage();
			this.InitGroup = new GroupBox();
			this.InitBox = new NumericUpDown();
			this.InitLbl = new Label();
			this.HPGroup = new GroupBox();
			this.HPBox = new NumericUpDown();
			this.HPLbl = new Label();
			this.SkillsGroup = new GroupBox();
			this.InsightBox = new NumericUpDown();
			this.PerceptionBox = new NumericUpDown();
			this.InsightLbl = new Label();
			this.PerceptionLbl = new Label();
			this.DefencesGroup = new GroupBox();
			this.RefBox = new NumericUpDown();
			this.WillBox = new NumericUpDown();
			this.RefLbl = new Label();
			this.WillLbl = new Label();
			this.ACBox = new NumericUpDown();
			this.FortBox = new NumericUpDown();
			this.ACLbl = new Label();
			this.FortLbl = new Label();
			this.EffectsPage = new TabPage();
			this.EffectList = new ListView();
			this.EffectHdr = new ColumnHeader();
			this.EffectToolbar = new ToolStrip();
			this.EffectAddBtn = new ToolStripDropDownButton();
			this.effectToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.EffectAddToken = new ToolStripMenuItem();
			this.EffectAddOverlay = new ToolStripMenuItem();
			this.EffectRemoveBtn = new ToolStripButton();
			this.EffectEditBtn = new ToolStripButton();
			this.EffectLbl = new Label();
			this.iPlay4eBtn = new Button();
			this.ClassGroup.SuspendLayout();
			this.RaceGroup.SuspendLayout();
			this.NameGroup.SuspendLayout();
			((ISupportInitialize)this.LevelBox).BeginInit();
			this.Pages.SuspendLayout();
			this.GeneralPage.SuspendLayout();
			this.AdvancedPage.SuspendLayout();
			this.LanguageGroup.SuspendLayout();
			this.PortraitGroup.SuspendLayout();
			this.PortraitPanel.SuspendLayout();
			((ISupportInitialize)this.PortraitBox).BeginInit();
			this.PortraitToolbar.SuspendLayout();
			this.StatsPage.SuspendLayout();
			this.InitGroup.SuspendLayout();
			((ISupportInitialize)this.InitBox).BeginInit();
			this.HPGroup.SuspendLayout();
			((ISupportInitialize)this.HPBox).BeginInit();
			this.SkillsGroup.SuspendLayout();
			((ISupportInitialize)this.InsightBox).BeginInit();
			((ISupportInitialize)this.PerceptionBox).BeginInit();
			this.DefencesGroup.SuspendLayout();
			((ISupportInitialize)this.RefBox).BeginInit();
			((ISupportInitialize)this.WillBox).BeginInit();
			((ISupportInitialize)this.ACBox).BeginInit();
			((ISupportInitialize)this.FortBox).BeginInit();
			this.EffectsPage.SuspendLayout();
			this.EffectToolbar.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(6, 22);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(89, 19);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(180, 20);
			this.NameBox.TabIndex = 1;
			this.PlayerLbl.AutoSize = true;
			this.PlayerLbl.Location = new Point(6, 48);
			this.PlayerLbl.Name = "PlayerLbl";
			this.PlayerLbl.Size = new Size(39, 13);
			this.PlayerLbl.TabIndex = 2;
			this.PlayerLbl.Text = "Player:";
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(151, 412);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(232, 412);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.SizeLbl.AutoSize = true;
			this.SizeLbl.Location = new Point(6, 48);
			this.SizeLbl.Name = "SizeLbl";
			this.SizeLbl.Size = new Size(30, 13);
			this.SizeLbl.TabIndex = 2;
			this.SizeLbl.Text = "Size:";
			this.SizeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SizeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.SizeBox.Location = new Point(89, 45);
			this.SizeBox.Name = "SizeBox";
			this.SizeBox.Size = new Size(180, 21);
			this.SizeBox.TabIndex = 3;
			this.PlayerBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.PlayerBox.Location = new Point(89, 45);
			this.PlayerBox.Name = "PlayerBox";
			this.PlayerBox.Size = new Size(180, 20);
			this.PlayerBox.TabIndex = 3;
			this.RaceLbl.AutoSize = true;
			this.RaceLbl.Location = new Point(6, 22);
			this.RaceLbl.Name = "RaceLbl";
			this.RaceLbl.Size = new Size(36, 13);
			this.RaceLbl.TabIndex = 0;
			this.RaceLbl.Text = "Race:";
			this.RaceBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RaceBox.Location = new Point(89, 19);
			this.RaceBox.Name = "RaceBox";
			this.RaceBox.Size = new Size(180, 20);
			this.RaceBox.TabIndex = 1;
			this.ClassLbl.AutoSize = true;
			this.ClassLbl.Location = new Point(6, 22);
			this.ClassLbl.Name = "ClassLbl";
			this.ClassLbl.Size = new Size(35, 13);
			this.ClassLbl.TabIndex = 0;
			this.ClassLbl.Text = "Class:";
			this.ClassBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ClassBox.Location = new Point(89, 19);
			this.ClassBox.Name = "ClassBox";
			this.ClassBox.Size = new Size(180, 20);
			this.ClassBox.TabIndex = 1;
			this.SourceLbl.AutoSize = true;
			this.SourceLbl.Location = new Point(6, 48);
			this.SourceLbl.Name = "SourceLbl";
			this.SourceLbl.Size = new Size(77, 13);
			this.SourceLbl.TabIndex = 2;
			this.SourceLbl.Text = "Power Source:";
			this.RoleLbl.AutoSize = true;
			this.RoleLbl.Location = new Point(6, 75);
			this.RoleLbl.Name = "RoleLbl";
			this.RoleLbl.Size = new Size(60, 13);
			this.RoleLbl.TabIndex = 4;
			this.RoleLbl.Text = "Class Role:";
			this.RoleBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RoleBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.RoleBox.FormattingEnabled = true;
			this.RoleBox.Location = new Point(89, 72);
			this.RoleBox.Name = "RoleBox";
			this.RoleBox.Size = new Size(180, 21);
			this.RoleBox.TabIndex = 5;
			this.SourceBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SourceBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.SourceBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.SourceBox.FormattingEnabled = true;
			this.SourceBox.Location = new Point(89, 45);
			this.SourceBox.Name = "SourceBox";
			this.SourceBox.Size = new Size(180, 21);
			this.SourceBox.TabIndex = 3;
			this.PPBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.PPBox.Location = new Point(89, 99);
			this.PPBox.Name = "PPBox";
			this.PPBox.Size = new Size(180, 20);
			this.PPBox.TabIndex = 7;
			this.PPLbl.AutoSize = true;
			this.PPLbl.Location = new Point(6, 102);
			this.PPLbl.Name = "PPLbl";
			this.PPLbl.Size = new Size(75, 13);
			this.PPLbl.TabIndex = 6;
			this.PPLbl.Text = "Paragon Path:";
			this.EDBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.EDBox.Location = new Point(89, 125);
			this.EDBox.Name = "EDBox";
			this.EDBox.Size = new Size(180, 20);
			this.EDBox.TabIndex = 9;
			this.EDLbl.AutoSize = true;
			this.EDLbl.Location = new Point(6, 128);
			this.EDLbl.Name = "EDLbl";
			this.EDLbl.Size = new Size(69, 13);
			this.EDLbl.TabIndex = 8;
			this.EDLbl.Text = "Epic Destiny:";
			this.ClassGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ClassGroup.Controls.Add(this.ClassBox);
			this.ClassGroup.Controls.Add(this.RoleBox);
			this.ClassGroup.Controls.Add(this.EDLbl);
			this.ClassGroup.Controls.Add(this.EDBox);
			this.ClassGroup.Controls.Add(this.PPLbl);
			this.ClassGroup.Controls.Add(this.SourceBox);
			this.ClassGroup.Controls.Add(this.RoleLbl);
			this.ClassGroup.Controls.Add(this.PPBox);
			this.ClassGroup.Controls.Add(this.SourceLbl);
			this.ClassGroup.Controls.Add(this.ClassLbl);
			this.ClassGroup.Location = new Point(6, 201);
			this.ClassGroup.Name = "ClassGroup";
			this.ClassGroup.Size = new Size(275, 157);
			this.ClassGroup.TabIndex = 2;
			this.ClassGroup.TabStop = false;
			this.ClassGroup.Text = "Class";
			this.RaceGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RaceGroup.Controls.Add(this.RaceBox);
			this.RaceGroup.Controls.Add(this.SizeBox);
			this.RaceGroup.Controls.Add(this.RaceLbl);
			this.RaceGroup.Controls.Add(this.SizeLbl);
			this.RaceGroup.Location = new Point(6, 117);
			this.RaceGroup.Name = "RaceGroup";
			this.RaceGroup.Size = new Size(275, 78);
			this.RaceGroup.TabIndex = 1;
			this.RaceGroup.TabStop = false;
			this.RaceGroup.Text = "Race";
			this.NameGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameGroup.Controls.Add(this.LevelBox);
			this.NameGroup.Controls.Add(this.LevelLbl);
			this.NameGroup.Controls.Add(this.NameBox);
			this.NameGroup.Controls.Add(this.PlayerBox);
			this.NameGroup.Controls.Add(this.NameLbl);
			this.NameGroup.Controls.Add(this.PlayerLbl);
			this.NameGroup.Location = new Point(6, 6);
			this.NameGroup.Name = "NameGroup";
			this.NameGroup.Size = new Size(275, 105);
			this.NameGroup.TabIndex = 0;
			this.NameGroup.TabStop = false;
			this.NameGroup.Text = "Overview";
			this.LevelBox.Location = new Point(89, 71);
			NumericUpDown arg_F59_0 = this.LevelBox;
			int[] array = new int[4];
			array[0] = 30;
			arg_F59_0.Maximum = new decimal(array);
			NumericUpDown arg_F78_0 = this.LevelBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_F78_0.Minimum = new decimal(array2);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new Size(180, 20);
			this.LevelBox.TabIndex = 5;
			NumericUpDown arg_FCA_0 = this.LevelBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_FCA_0.Value = new decimal(array3);
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(6, 73);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(36, 13);
			this.LevelLbl.TabIndex = 4;
			this.LevelLbl.Text = "Level:";
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.GeneralPage);
			this.Pages.Controls.Add(this.AdvancedPage);
			this.Pages.Controls.Add(this.StatsPage);
			this.Pages.Controls.Add(this.EffectsPage);
			this.Pages.Location = new Point(12, 12);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(295, 394);
			this.Pages.TabIndex = 0;
			this.GeneralPage.Controls.Add(this.NameGroup);
			this.GeneralPage.Controls.Add(this.ClassGroup);
			this.GeneralPage.Controls.Add(this.RaceGroup);
			this.GeneralPage.Location = new Point(4, 22);
			this.GeneralPage.Name = "GeneralPage";
			this.GeneralPage.Padding = new Padding(3);
			this.GeneralPage.Size = new Size(287, 368);
			this.GeneralPage.TabIndex = 0;
			this.GeneralPage.Text = "General";
			this.GeneralPage.UseVisualStyleBackColor = true;
			this.AdvancedPage.Controls.Add(this.LanguageGroup);
			this.AdvancedPage.Controls.Add(this.PortraitGroup);
			this.AdvancedPage.Location = new Point(4, 22);
			this.AdvancedPage.Name = "AdvancedPage";
			this.AdvancedPage.Padding = new Padding(3);
			this.AdvancedPage.Size = new Size(287, 368);
			this.AdvancedPage.TabIndex = 1;
			this.AdvancedPage.Text = "Advanced";
			this.AdvancedPage.UseVisualStyleBackColor = true;
			this.LanguageGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LanguageGroup.Controls.Add(this.LanguageBox);
			this.LanguageGroup.Location = new Point(6, 6);
			this.LanguageGroup.Name = "LanguageGroup";
			this.LanguageGroup.Size = new Size(275, 58);
			this.LanguageGroup.TabIndex = 1;
			this.LanguageGroup.TabStop = false;
			this.LanguageGroup.Text = "Languages";
			this.LanguageBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.LanguageBox.Location = new Point(6, 19);
			this.LanguageBox.Multiline = true;
			this.LanguageBox.Name = "LanguageBox";
			this.LanguageBox.ScrollBars = ScrollBars.Vertical;
			this.LanguageBox.Size = new Size(263, 33);
			this.LanguageBox.TabIndex = 0;
			this.PortraitGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.PortraitGroup.Controls.Add(this.PortraitPanel);
			this.PortraitGroup.Location = new Point(6, 70);
			this.PortraitGroup.Name = "PortraitGroup";
			this.PortraitGroup.Size = new Size(275, 292);
			this.PortraitGroup.TabIndex = 2;
			this.PortraitGroup.TabStop = false;
			this.PortraitGroup.Text = "Portrait";
			this.PortraitPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.PortraitPanel.Controls.Add(this.PortraitBox);
			this.PortraitPanel.Controls.Add(this.PortraitToolbar);
			this.PortraitPanel.Location = new Point(6, 19);
			this.PortraitPanel.Name = "PortraitPanel";
			this.PortraitPanel.Size = new Size(263, 267);
			this.PortraitPanel.TabIndex = 25;
			this.PortraitBox.Dock = DockStyle.Fill;
			this.PortraitBox.Location = new Point(0, 25);
			this.PortraitBox.Name = "PortraitBox";
			this.PortraitBox.Size = new Size(263, 242);
			this.PortraitBox.SizeMode = PictureBoxSizeMode.Zoom;
			this.PortraitBox.TabIndex = 1;
			this.PortraitBox.TabStop = false;
			this.PortraitToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.PortraitBrowse,
				this.PortraitPasteBtn,
				this.PortraitClear
			});
			this.PortraitToolbar.Location = new Point(0, 0);
			this.PortraitToolbar.Name = "PortraitToolbar";
			this.PortraitToolbar.Size = new Size(263, 25);
			this.PortraitToolbar.TabIndex = 0;
			this.PortraitToolbar.Text = "toolStrip1";
			this.PortraitBrowse.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PortraitBrowse.Image = (Image)resources.GetObject("PortraitBrowse.Image");
			this.PortraitBrowse.ImageTransparentColor = Color.Magenta;
			this.PortraitBrowse.Name = "PortraitBrowse";
			this.PortraitBrowse.Size = new Size(49, 22);
			this.PortraitBrowse.Text = "Browse";
			this.PortraitBrowse.Click += new EventHandler(this.PortraitBrowse_Click);
			this.PortraitPasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PortraitPasteBtn.Image = (Image)resources.GetObject("PortraitPasteBtn.Image");
			this.PortraitPasteBtn.ImageTransparentColor = Color.Magenta;
			this.PortraitPasteBtn.Name = "PortraitPasteBtn";
			this.PortraitPasteBtn.Size = new Size(39, 22);
			this.PortraitPasteBtn.Text = "Paste";
			this.PortraitPasteBtn.Click += new EventHandler(this.PortraitPaste_Click);
			this.PortraitClear.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PortraitClear.Image = (Image)resources.GetObject("PortraitClear.Image");
			this.PortraitClear.ImageTransparentColor = Color.Magenta;
			this.PortraitClear.Name = "PortraitClear";
			this.PortraitClear.Size = new Size(38, 22);
			this.PortraitClear.Text = "Clear";
			this.PortraitClear.Click += new EventHandler(this.PortraitClear_Click);
			this.StatsPage.Controls.Add(this.InitGroup);
			this.StatsPage.Controls.Add(this.HPGroup);
			this.StatsPage.Controls.Add(this.SkillsGroup);
			this.StatsPage.Controls.Add(this.DefencesGroup);
			this.StatsPage.Location = new Point(4, 22);
			this.StatsPage.Name = "StatsPage";
			this.StatsPage.Padding = new Padding(3);
			this.StatsPage.Size = new Size(287, 368);
			this.StatsPage.TabIndex = 2;
			this.StatsPage.Text = "Statistics";
			this.StatsPage.UseVisualStyleBackColor = true;
			this.InitGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.InitGroup.Controls.Add(this.InitBox);
			this.InitGroup.Controls.Add(this.InitLbl);
			this.InitGroup.Location = new Point(6, 202);
			this.InitGroup.Name = "InitGroup";
			this.InitGroup.Size = new Size(275, 53);
			this.InitGroup.TabIndex = 2;
			this.InitGroup.TabStop = false;
			this.InitGroup.Text = "Initiative Bonus";
			this.InitBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.InitBox.Location = new Point(89, 19);
			this.InitBox.Minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				int.MinValue
			});
			this.InitBox.Name = "InitBox";
			this.InitBox.Size = new Size(180, 20);
			this.InitBox.TabIndex = 1;
			this.InitLbl.AutoSize = true;
			this.InitLbl.Location = new Point(6, 21);
			this.InitLbl.Name = "InitLbl";
			this.InitLbl.Size = new Size(49, 13);
			this.InitLbl.TabIndex = 0;
			this.InitLbl.Text = "Initiative:";
			this.HPGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.HPGroup.Controls.Add(this.HPBox);
			this.HPGroup.Controls.Add(this.HPLbl);
			this.HPGroup.Location = new Point(6, 6);
			this.HPGroup.Name = "HPGroup";
			this.HPGroup.Size = new Size(275, 53);
			this.HPGroup.TabIndex = 0;
			this.HPGroup.TabStop = false;
			this.HPGroup.Text = "Hit Points";
			this.HPBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.HPBox.Location = new Point(89, 19);
			NumericUpDown arg_19DE_0 = this.HPBox;
			int[] array4 = new int[4];
			array4[0] = 500;
			arg_19DE_0.Maximum = new decimal(array4);
			this.HPBox.Name = "HPBox";
			this.HPBox.Size = new Size(180, 20);
			this.HPBox.TabIndex = 1;
			this.HPLbl.AutoSize = true;
			this.HPLbl.Location = new Point(6, 21);
			this.HPLbl.Name = "HPLbl";
			this.HPLbl.Size = new Size(25, 13);
			this.HPLbl.TabIndex = 0;
			this.HPLbl.Text = "HP:";
			this.SkillsGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SkillsGroup.Controls.Add(this.InsightBox);
			this.SkillsGroup.Controls.Add(this.PerceptionBox);
			this.SkillsGroup.Controls.Add(this.InsightLbl);
			this.SkillsGroup.Controls.Add(this.PerceptionLbl);
			this.SkillsGroup.Location = new Point(6, 261);
			this.SkillsGroup.Name = "SkillsGroup";
			this.SkillsGroup.Size = new Size(275, 79);
			this.SkillsGroup.TabIndex = 3;
			this.SkillsGroup.TabStop = false;
			this.SkillsGroup.Text = "Passive Skills";
			this.InsightBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.InsightBox.Location = new Point(89, 19);
			this.InsightBox.Name = "InsightBox";
			this.InsightBox.Size = new Size(180, 20);
			this.InsightBox.TabIndex = 1;
			this.PerceptionBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.PerceptionBox.Location = new Point(89, 45);
			this.PerceptionBox.Name = "PerceptionBox";
			this.PerceptionBox.Size = new Size(180, 20);
			this.PerceptionBox.TabIndex = 3;
			this.InsightLbl.AutoSize = true;
			this.InsightLbl.Location = new Point(6, 21);
			this.InsightLbl.Name = "InsightLbl";
			this.InsightLbl.Size = new Size(41, 13);
			this.InsightLbl.TabIndex = 0;
			this.InsightLbl.Text = "Insight:";
			this.PerceptionLbl.AutoSize = true;
			this.PerceptionLbl.Location = new Point(6, 47);
			this.PerceptionLbl.Name = "PerceptionLbl";
			this.PerceptionLbl.Size = new Size(58, 13);
			this.PerceptionLbl.TabIndex = 2;
			this.PerceptionLbl.Text = "Perception";
			this.DefencesGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DefencesGroup.Controls.Add(this.RefBox);
			this.DefencesGroup.Controls.Add(this.WillBox);
			this.DefencesGroup.Controls.Add(this.RefLbl);
			this.DefencesGroup.Controls.Add(this.WillLbl);
			this.DefencesGroup.Controls.Add(this.ACBox);
			this.DefencesGroup.Controls.Add(this.FortBox);
			this.DefencesGroup.Controls.Add(this.ACLbl);
			this.DefencesGroup.Controls.Add(this.FortLbl);
			this.DefencesGroup.Location = new Point(6, 65);
			this.DefencesGroup.Name = "DefencesGroup";
			this.DefencesGroup.Size = new Size(275, 131);
			this.DefencesGroup.TabIndex = 1;
			this.DefencesGroup.TabStop = false;
			this.DefencesGroup.Text = "Defences";
			this.RefBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RefBox.Location = new Point(89, 71);
			this.RefBox.Name = "RefBox";
			this.RefBox.Size = new Size(180, 20);
			this.RefBox.TabIndex = 5;
			this.WillBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.WillBox.Location = new Point(89, 97);
			this.WillBox.Name = "WillBox";
			this.WillBox.Size = new Size(180, 20);
			this.WillBox.TabIndex = 7;
			this.RefLbl.AutoSize = true;
			this.RefLbl.Location = new Point(6, 73);
			this.RefLbl.Name = "RefLbl";
			this.RefLbl.Size = new Size(40, 13);
			this.RefLbl.TabIndex = 4;
			this.RefLbl.Text = "Reflex:";
			this.WillLbl.AutoSize = true;
			this.WillLbl.Location = new Point(6, 99);
			this.WillLbl.Name = "WillLbl";
			this.WillLbl.Size = new Size(27, 13);
			this.WillLbl.TabIndex = 6;
			this.WillLbl.Text = "Will:";
			this.ACBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ACBox.Location = new Point(89, 19);
			this.ACBox.Name = "ACBox";
			this.ACBox.Size = new Size(180, 20);
			this.ACBox.TabIndex = 1;
			this.FortBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.FortBox.Location = new Point(89, 45);
			this.FortBox.Name = "FortBox";
			this.FortBox.Size = new Size(180, 20);
			this.FortBox.TabIndex = 3;
			this.ACLbl.AutoSize = true;
			this.ACLbl.Location = new Point(6, 21);
			this.ACLbl.Name = "ACLbl";
			this.ACLbl.Size = new Size(24, 13);
			this.ACLbl.TabIndex = 0;
			this.ACLbl.Text = "AC:";
			this.FortLbl.AutoSize = true;
			this.FortLbl.Location = new Point(6, 47);
			this.FortLbl.Name = "FortLbl";
			this.FortLbl.Size = new Size(51, 13);
			this.FortLbl.TabIndex = 2;
			this.FortLbl.Text = "Fortitude:";
			this.EffectsPage.Controls.Add(this.EffectList);
			this.EffectsPage.Controls.Add(this.EffectToolbar);
			this.EffectsPage.Controls.Add(this.EffectLbl);
			this.EffectsPage.Location = new Point(4, 22);
			this.EffectsPage.Name = "EffectsPage";
			this.EffectsPage.Padding = new Padding(3);
			this.EffectsPage.Size = new Size(287, 368);
			this.EffectsPage.TabIndex = 3;
			this.EffectsPage.Text = "Combat";
			this.EffectsPage.UseVisualStyleBackColor = true;
			this.EffectList.Columns.AddRange(new ColumnHeader[]
			{
				this.EffectHdr
			});
			this.EffectList.Dock = DockStyle.Fill;
			this.EffectList.FullRowSelect = true;
			listViewGroup.Header = "Effects";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup2.Header = "Map Tokens";
			listViewGroup2.Name = "listViewGroup2";
			listViewGroup3.Header = "Map Overlays";
			listViewGroup3.Name = "listViewGroup3";
			this.EffectList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2,
				listViewGroup3
			});
			this.EffectList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.EffectList.HideSelection = false;
			this.EffectList.Location = new Point(3, 28);
			this.EffectList.MultiSelect = false;
			this.EffectList.Name = "EffectList";
			this.EffectList.Size = new Size(281, 266);
			this.EffectList.TabIndex = 1;
			this.EffectList.UseCompatibleStateImageBehavior = false;
			this.EffectList.View = View.Details;
			this.EffectList.DoubleClick += new EventHandler(this.EffectEditBtn_Click);
			this.EffectHdr.Text = "Effect / Token / Overlay";
			this.EffectHdr.Width = 239;
			this.EffectToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.EffectAddBtn,
				this.EffectRemoveBtn,
				this.EffectEditBtn
			});
			this.EffectToolbar.Location = new Point(3, 3);
			this.EffectToolbar.Name = "EffectToolbar";
			this.EffectToolbar.Size = new Size(281, 25);
			this.EffectToolbar.TabIndex = 0;
			this.EffectToolbar.Text = "toolStrip1";
			this.EffectAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EffectAddBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.effectToolStripMenuItem,
				this.toolStripSeparator1,
				this.EffectAddToken,
				this.EffectAddOverlay
			});
			this.EffectAddBtn.Image = (Image)resources.GetObject("EffectAddBtn.Image");
			this.EffectAddBtn.ImageTransparentColor = Color.Magenta;
			this.EffectAddBtn.Name = "EffectAddBtn";
			this.EffectAddBtn.Size = new Size(42, 22);
			this.EffectAddBtn.Text = "Add";
			this.effectToolStripMenuItem.Name = "effectToolStripMenuItem";
			this.effectToolStripMenuItem.Size = new Size(152, 22);
			this.effectToolStripMenuItem.Text = "Effect";
			this.effectToolStripMenuItem.Click += new EventHandler(this.EffectAddBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(149, 6);
			this.EffectAddToken.Name = "EffectAddToken";
			this.EffectAddToken.Size = new Size(152, 22);
			this.EffectAddToken.Text = "Map Token";
			this.EffectAddToken.Click += new EventHandler(this.EffectAddToken_Click);
			this.EffectAddOverlay.Name = "EffectAddOverlay";
			this.EffectAddOverlay.Size = new Size(152, 22);
			this.EffectAddOverlay.Text = "Map Overlay";
			this.EffectAddOverlay.Click += new EventHandler(this.EffectAddOverlay_Click);
			this.EffectRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EffectRemoveBtn.Image = (Image)resources.GetObject("EffectRemoveBtn.Image");
			this.EffectRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.EffectRemoveBtn.Name = "EffectRemoveBtn";
			this.EffectRemoveBtn.Size = new Size(54, 22);
			this.EffectRemoveBtn.Text = "Remove";
			this.EffectRemoveBtn.Click += new EventHandler(this.EffectRemoveBtn_Click);
			this.EffectEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EffectEditBtn.Image = (Image)resources.GetObject("EffectEditBtn.Image");
			this.EffectEditBtn.ImageTransparentColor = Color.Magenta;
			this.EffectEditBtn.Name = "EffectEditBtn";
			this.EffectEditBtn.Size = new Size(31, 22);
			this.EffectEditBtn.Text = "Edit";
			this.EffectEditBtn.Click += new EventHandler(this.EffectEditBtn_Click);
			this.EffectLbl.Dock = DockStyle.Bottom;
			this.EffectLbl.Location = new Point(3, 294);
			this.EffectLbl.Name = "EffectLbl";
			this.EffectLbl.Size = new Size(281, 71);
			this.EffectLbl.TabIndex = 2;
			this.EffectLbl.Text = resources.GetString("EffectLbl.Text");
			this.iPlay4eBtn.Location = new Point(12, 412);
			this.iPlay4eBtn.Name = "iPlay4eBtn";
			this.iPlay4eBtn.Size = new Size(75, 23);
			this.iPlay4eBtn.TabIndex = 3;
			this.iPlay4eBtn.Text = "iPlay4E";
			this.iPlay4eBtn.UseVisualStyleBackColor = true;
			this.iPlay4eBtn.Click += new EventHandler(this.iPlay4eBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(319, 447);
			base.Controls.Add(this.iPlay4eBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "HeroForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Player Character";
			base.Shown += new EventHandler(this.HeroForm_Shown);
			this.ClassGroup.ResumeLayout(false);
			this.ClassGroup.PerformLayout();
			this.RaceGroup.ResumeLayout(false);
			this.RaceGroup.PerformLayout();
			this.NameGroup.ResumeLayout(false);
			this.NameGroup.PerformLayout();
			((ISupportInitialize)this.LevelBox).EndInit();
			this.Pages.ResumeLayout(false);
			this.GeneralPage.ResumeLayout(false);
			this.AdvancedPage.ResumeLayout(false);
			this.LanguageGroup.ResumeLayout(false);
			this.LanguageGroup.PerformLayout();
			this.PortraitGroup.ResumeLayout(false);
			this.PortraitPanel.ResumeLayout(false);
			this.PortraitPanel.PerformLayout();
			((ISupportInitialize)this.PortraitBox).EndInit();
			this.PortraitToolbar.ResumeLayout(false);
			this.PortraitToolbar.PerformLayout();
			this.StatsPage.ResumeLayout(false);
			this.InitGroup.ResumeLayout(false);
			this.InitGroup.PerformLayout();
			((ISupportInitialize)this.InitBox).EndInit();
			this.HPGroup.ResumeLayout(false);
			this.HPGroup.PerformLayout();
			((ISupportInitialize)this.HPBox).EndInit();
			this.SkillsGroup.ResumeLayout(false);
			this.SkillsGroup.PerformLayout();
			((ISupportInitialize)this.InsightBox).EndInit();
			((ISupportInitialize)this.PerceptionBox).EndInit();
			this.DefencesGroup.ResumeLayout(false);
			this.DefencesGroup.PerformLayout();
			((ISupportInitialize)this.RefBox).EndInit();
			((ISupportInitialize)this.WillBox).EndInit();
			((ISupportInitialize)this.ACBox).EndInit();
			((ISupportInitialize)this.FortBox).EndInit();
			this.EffectsPage.ResumeLayout(false);
			this.EffectsPage.PerformLayout();
			this.EffectToolbar.ResumeLayout(false);
			this.EffectToolbar.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
