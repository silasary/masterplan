using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class OptionThemeForm : Form
	{
		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private TabControl Pages;

		private TabPage DetailsPage;

		private Button OKBtn;

		private Button CancelBtn;

		private TabPage LevelPage;

		private ListView LevelList;

		private ColumnHeader FeatureHdr;

		private ToolStrip LevelToolbar;

		private ToolStripButton LevelEditBtn;

		private TextBox PrereqBox;

		private Label PrereqLbl;

		private TextBox QuoteBox;

		private Label QuoteLbl;

		private TextBox DetailsBox;

		private Label SourceLbl;

		private Label RoleLbl;

		private Label PowerLbl;

		private Button PowerBtn;

		private ComboBox RoleBox;

		private ComboBox SourceBox;

		private Theme fTheme;

		public Theme Theme
		{
			get
			{
				return this.fTheme;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(OptionThemeForm));
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.QuoteBox = new TextBox();
			this.QuoteLbl = new Label();
			this.DetailsBox = new TextBox();
			this.LevelPage = new TabPage();
			this.LevelList = new ListView();
			this.FeatureHdr = new ColumnHeader();
			this.LevelToolbar = new ToolStrip();
			this.LevelEditBtn = new ToolStripButton();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.PrereqBox = new TextBox();
			this.PrereqLbl = new Label();
			this.SourceLbl = new Label();
			this.RoleLbl = new Label();
			this.PowerLbl = new Label();
			this.PowerBtn = new Button();
			this.RoleBox = new ComboBox();
			this.SourceBox = new ComboBox();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.LevelPage.SuspendLayout();
			this.LevelToolbar.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(104, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(257, 20);
			this.NameBox.TabIndex = 1;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Controls.Add(this.LevelPage);
			this.Pages.Location = new Point(12, 147);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(349, 217);
			this.Pages.TabIndex = 10;
			this.DetailsPage.Controls.Add(this.QuoteBox);
			this.DetailsPage.Controls.Add(this.QuoteLbl);
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new Padding(3);
			this.DetailsPage.Size = new Size(341, 191);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.QuoteBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.QuoteBox.Location = new Point(51, 165);
			this.QuoteBox.Name = "QuoteBox";
			this.QuoteBox.Size = new Size(284, 20);
			this.QuoteBox.TabIndex = 2;
			this.QuoteLbl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.QuoteLbl.AutoSize = true;
			this.QuoteLbl.Location = new Point(6, 168);
			this.QuoteLbl.Name = "QuoteLbl";
			this.QuoteLbl.Size = new Size(39, 13);
			this.QuoteLbl.TabIndex = 1;
			this.QuoteLbl.Text = "Quote:";
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.DetailsBox.Location = new Point(6, 6);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new Size(329, 153);
			this.DetailsBox.TabIndex = 0;
			this.LevelPage.Controls.Add(this.LevelList);
			this.LevelPage.Controls.Add(this.LevelToolbar);
			this.LevelPage.Location = new Point(4, 22);
			this.LevelPage.Name = "LevelPage";
			this.LevelPage.Padding = new Padding(3);
			this.LevelPage.Size = new Size(341, 193);
			this.LevelPage.TabIndex = 2;
			this.LevelPage.Text = "Levels";
			this.LevelPage.UseVisualStyleBackColor = true;
			this.LevelList.Columns.AddRange(new ColumnHeader[]
			{
				this.FeatureHdr
			});
			this.LevelList.Dock = DockStyle.Fill;
			this.LevelList.FullRowSelect = true;
			this.LevelList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.LevelList.HideSelection = false;
			this.LevelList.Location = new Point(3, 28);
			this.LevelList.MultiSelect = false;
			this.LevelList.Name = "LevelList";
			this.LevelList.Size = new Size(335, 162);
			this.LevelList.TabIndex = 1;
			this.LevelList.UseCompatibleStateImageBehavior = false;
			this.LevelList.View = View.Details;
			this.LevelList.DoubleClick += new EventHandler(this.FeatureEditBtn_Click);
			this.FeatureHdr.Text = "Feature";
			this.FeatureHdr.Width = 300;
			this.LevelToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.LevelEditBtn
			});
			this.LevelToolbar.Location = new Point(3, 3);
			this.LevelToolbar.Name = "LevelToolbar";
			this.LevelToolbar.Size = new Size(335, 25);
			this.LevelToolbar.TabIndex = 0;
			this.LevelToolbar.Text = "toolStrip1";
			this.LevelEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LevelEditBtn.Image = (Image)resources.GetObject("LevelEditBtn.Image");
			this.LevelEditBtn.ImageTransparentColor = Color.Magenta;
			this.LevelEditBtn.Name = "LevelEditBtn";
			this.LevelEditBtn.Size = new Size(31, 22);
			this.LevelEditBtn.Text = "Edit";
			this.LevelEditBtn.Click += new EventHandler(this.FeatureEditBtn_Click);
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(205, 370);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 11;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(286, 370);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 12;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.PrereqBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.PrereqBox.Location = new Point(104, 38);
			this.PrereqBox.Name = "PrereqBox";
			this.PrereqBox.Size = new Size(257, 20);
			this.PrereqBox.TabIndex = 3;
			this.PrereqLbl.AutoSize = true;
			this.PrereqLbl.Location = new Point(12, 41);
			this.PrereqLbl.Name = "PrereqLbl";
			this.PrereqLbl.Size = new Size(70, 13);
			this.PrereqLbl.TabIndex = 2;
			this.PrereqLbl.Text = "Prerequisites:";
			this.SourceLbl.AutoSize = true;
			this.SourceLbl.Location = new Point(12, 94);
			this.SourceLbl.Name = "SourceLbl";
			this.SourceLbl.Size = new Size(77, 13);
			this.SourceLbl.TabIndex = 6;
			this.SourceLbl.Text = "Power Source:";
			this.RoleLbl.AutoSize = true;
			this.RoleLbl.Location = new Point(12, 67);
			this.RoleLbl.Name = "RoleLbl";
			this.RoleLbl.Size = new Size(86, 13);
			this.RoleLbl.TabIndex = 4;
			this.RoleLbl.Text = "Secondary Role:";
			this.PowerLbl.AutoSize = true;
			this.PowerLbl.Location = new Point(12, 123);
			this.PowerLbl.Name = "PowerLbl";
			this.PowerLbl.Size = new Size(81, 13);
			this.PowerLbl.TabIndex = 8;
			this.PowerLbl.Text = "Granted Power:";
			this.PowerBtn.Location = new Point(104, 118);
			this.PowerBtn.Name = "PowerBtn";
			this.PowerBtn.Size = new Size(257, 23);
			this.PowerBtn.TabIndex = 9;
			this.PowerBtn.Text = "Edit";
			this.PowerBtn.UseVisualStyleBackColor = true;
			this.PowerBtn.Click += new EventHandler(this.PowerBtn_Click);
			this.RoleBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.RoleBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.RoleBox.FormattingEnabled = true;
			this.RoleBox.Location = new Point(104, 64);
			this.RoleBox.Name = "RoleBox";
			this.RoleBox.Size = new Size(257, 21);
			this.RoleBox.TabIndex = 5;
			this.SourceBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.SourceBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.SourceBox.FormattingEnabled = true;
			this.SourceBox.Location = new Point(104, 91);
			this.SourceBox.Name = "SourceBox";
			this.SourceBox.Size = new Size(257, 21);
			this.SourceBox.TabIndex = 7;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(373, 405);
			base.Controls.Add(this.SourceBox);
			base.Controls.Add(this.RoleBox);
			base.Controls.Add(this.PowerBtn);
			base.Controls.Add(this.PowerLbl);
			base.Controls.Add(this.SourceLbl);
			base.Controls.Add(this.RoleLbl);
			base.Controls.Add(this.PrereqBox);
			base.Controls.Add(this.PrereqLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OptionThemeForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Theme";
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.LevelPage.ResumeLayout(false);
			this.LevelPage.PerformLayout();
			this.LevelToolbar.ResumeLayout(false);
			this.LevelToolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public OptionThemeForm(Theme theme)
		{
			this.InitializeComponent();
			this.RoleBox.Items.Add("Controller");
			this.RoleBox.Items.Add("Defender");
			this.RoleBox.Items.Add("Leader");
			this.RoleBox.Items.Add("Striker");
			this.SourceBox.Items.Add("Martial");
			this.SourceBox.Items.Add("Arcane");
			this.SourceBox.Items.Add("Divine");
			this.SourceBox.Items.Add("Primal");
			this.SourceBox.Items.Add("Psionic");
			this.SourceBox.Items.Add("Shadow");
			this.SourceBox.Items.Add("Elemental");
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fTheme = theme.Copy();
			this.NameBox.Text = this.fTheme.Name;
			this.PrereqBox.Text = this.fTheme.Prerequisites;
			this.RoleBox.Text = this.fTheme.SecondaryRole;
			this.SourceBox.Text = this.fTheme.PowerSource;
			this.DetailsBox.Text = this.fTheme.Details;
			this.QuoteBox.Text = this.fTheme.Quote;
			this.update_levels();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.LevelEditBtn.Enabled = (this.SelectedLevel != null);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fTheme.Name = this.NameBox.Text;
			this.fTheme.Prerequisites = this.PrereqBox.Text;
			this.fTheme.Details = this.DetailsBox.Text;
			this.fTheme.Quote = this.QuoteBox.Text;
		}

		private void FeatureEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLevel != null)
			{
				int index = this.fTheme.Levels.IndexOf(this.SelectedLevel);
				OptionLevelForm optionLevelForm = new OptionLevelForm(this.SelectedLevel, true);
				if (optionLevelForm.ShowDialog() == DialogResult.OK)
				{
					this.fTheme.Levels[index] = optionLevelForm.Level;
					this.update_levels();
				}
			}
		}

		private void update_levels()
		{
			this.LevelList.Items.Clear();
			foreach (LevelData current in this.fTheme.Levels)
			{
				ListViewItem listViewItem = this.LevelList.Items.Add(current.ToString());
				listViewItem.Tag = current;
				if (current.Count == 0)
				{
					listViewItem.ForeColor = SystemColors.GrayText;
				}
			}
		}

		private void PowerBtn_Click(object sender, EventArgs e)
		{
			OptionPowerForm optionPowerForm = new OptionPowerForm(this.fTheme.GrantedPower);
			if (optionPowerForm.ShowDialog() == DialogResult.OK)
			{
				this.fTheme.GrantedPower = optionPowerForm.Power;
			}
		}
	}
}
