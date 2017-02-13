using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class OptionRitualForm : Form
	{
		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private Label DurationLbl;

		private TextBox DurationBox;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TextBox DetailsBox;

		private Button OKBtn;

		private Button CancelBtn;

		private TextBox TimeBox;

		private Label TimeLbl;

		private TabPage ReadAloudPage;

		private TextBox ReadAloudBox;

		private Label LevelLbl;

		private NumericUpDown LevelBox;

		private Label CatLbl;

		private ComboBox CatBox;

		private TabPage InfoPage;

		private ComboBox SkillBox;

		private Label SkillLbl;

		private TextBox MarketBox;

		private Label MarketLbl;

		private TextBox ComponentBox;

		private Label ComponentLbl;

		private Ritual fRitual;

		public Ritual Ritual
		{
			get
			{
				return this.fRitual;
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
			this.DurationLbl = new Label();
			this.DurationBox = new TextBox();
			this.Pages = new TabControl();
			this.InfoPage = new TabPage();
			this.SkillBox = new ComboBox();
			this.SkillLbl = new Label();
			this.MarketBox = new TextBox();
			this.MarketLbl = new Label();
			this.ComponentBox = new TextBox();
			this.ComponentLbl = new Label();
			this.TimeBox = new TextBox();
			this.TimeLbl = new Label();
			this.ReadAloudPage = new TabPage();
			this.ReadAloudBox = new TextBox();
			this.DetailsPage = new TabPage();
			this.DetailsBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.LevelLbl = new Label();
			this.LevelBox = new NumericUpDown();
			this.CatLbl = new Label();
			this.CatBox = new ComboBox();
			this.Pages.SuspendLayout();
			this.InfoPage.SuspendLayout();
			this.ReadAloudPage.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			((ISupportInitialize)this.LevelBox).BeginInit();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(70, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(282, 20);
			this.NameBox.TabIndex = 1;
			this.DurationLbl.AutoSize = true;
			this.DurationLbl.Location = new Point(6, 35);
			this.DurationLbl.Name = "DurationLbl";
			this.DurationLbl.Size = new Size(50, 13);
			this.DurationLbl.TabIndex = 2;
			this.DurationLbl.Text = "Duration:";
			this.DurationBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DurationBox.Location = new Point(100, 32);
			this.DurationBox.Name = "DurationBox";
			this.DurationBox.Size = new Size(226, 20);
			this.DurationBox.TabIndex = 3;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.InfoPage);
			this.Pages.Controls.Add(this.ReadAloudPage);
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Location = new Point(12, 91);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(340, 168);
			this.Pages.TabIndex = 6;
			this.InfoPage.Controls.Add(this.SkillBox);
			this.InfoPage.Controls.Add(this.SkillLbl);
			this.InfoPage.Controls.Add(this.MarketBox);
			this.InfoPage.Controls.Add(this.MarketLbl);
			this.InfoPage.Controls.Add(this.ComponentBox);
			this.InfoPage.Controls.Add(this.ComponentLbl);
			this.InfoPage.Controls.Add(this.DurationBox);
			this.InfoPage.Controls.Add(this.DurationLbl);
			this.InfoPage.Controls.Add(this.TimeBox);
			this.InfoPage.Controls.Add(this.TimeLbl);
			this.InfoPage.Location = new Point(4, 22);
			this.InfoPage.Name = "InfoPage";
			this.InfoPage.Padding = new Padding(3);
			this.InfoPage.Size = new Size(332, 142);
			this.InfoPage.TabIndex = 2;
			this.InfoPage.Text = "Information";
			this.InfoPage.UseVisualStyleBackColor = true;
			this.SkillBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SkillBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.SkillBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.SkillBox.FormattingEnabled = true;
			this.SkillBox.Location = new Point(100, 110);
			this.SkillBox.Name = "SkillBox";
			this.SkillBox.Size = new Size(226, 21);
			this.SkillBox.TabIndex = 9;
			this.SkillLbl.AutoSize = true;
			this.SkillLbl.Location = new Point(6, 113);
			this.SkillLbl.Name = "SkillLbl";
			this.SkillLbl.Size = new Size(50, 13);
			this.SkillLbl.TabIndex = 8;
			this.SkillLbl.Text = "Key Skill:";
			this.MarketBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.MarketBox.Location = new Point(100, 84);
			this.MarketBox.Name = "MarketBox";
			this.MarketBox.Size = new Size(226, 20);
			this.MarketBox.TabIndex = 7;
			this.MarketLbl.AutoSize = true;
			this.MarketLbl.Location = new Point(6, 87);
			this.MarketLbl.Name = "MarketLbl";
			this.MarketLbl.Size = new Size(70, 13);
			this.MarketLbl.TabIndex = 6;
			this.MarketLbl.Text = "Market Price:";
			this.ComponentBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ComponentBox.Location = new Point(100, 58);
			this.ComponentBox.Name = "ComponentBox";
			this.ComponentBox.Size = new Size(226, 20);
			this.ComponentBox.TabIndex = 5;
			this.ComponentLbl.AutoSize = true;
			this.ComponentLbl.Location = new Point(6, 61);
			this.ComponentLbl.Name = "ComponentLbl";
			this.ComponentLbl.Size = new Size(88, 13);
			this.ComponentLbl.TabIndex = 4;
			this.ComponentLbl.Text = "Component Cost:";
			this.TimeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TimeBox.Location = new Point(100, 6);
			this.TimeBox.Name = "TimeBox";
			this.TimeBox.Size = new Size(226, 20);
			this.TimeBox.TabIndex = 1;
			this.TimeLbl.AutoSize = true;
			this.TimeLbl.Location = new Point(6, 9);
			this.TimeLbl.Name = "TimeLbl";
			this.TimeLbl.Size = new Size(33, 13);
			this.TimeLbl.TabIndex = 0;
			this.TimeLbl.Text = "Time:";
			this.ReadAloudPage.Controls.Add(this.ReadAloudBox);
			this.ReadAloudPage.Location = new Point(4, 22);
			this.ReadAloudPage.Name = "ReadAloudPage";
			this.ReadAloudPage.Padding = new Padding(3);
			this.ReadAloudPage.Size = new Size(332, 142);
			this.ReadAloudPage.TabIndex = 1;
			this.ReadAloudPage.Text = "Read-Aloud";
			this.ReadAloudPage.UseVisualStyleBackColor = true;
			this.ReadAloudBox.AcceptsReturn = true;
			this.ReadAloudBox.AcceptsTab = true;
			this.ReadAloudBox.Dock = DockStyle.Fill;
			this.ReadAloudBox.Location = new Point(3, 3);
			this.ReadAloudBox.Multiline = true;
			this.ReadAloudBox.Name = "ReadAloudBox";
			this.ReadAloudBox.ScrollBars = ScrollBars.Vertical;
			this.ReadAloudBox.Size = new Size(326, 136);
			this.ReadAloudBox.TabIndex = 1;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new Padding(3);
			this.DetailsPage.Size = new Size(332, 142);
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
			this.DetailsBox.Size = new Size(326, 136);
			this.DetailsBox.TabIndex = 0;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(196, 265);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 7;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(277, 265);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 8;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(12, 40);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(33, 13);
			this.LevelLbl.TabIndex = 2;
			this.LevelLbl.Text = "Level";
			this.LevelBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LevelBox.Location = new Point(70, 38);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new Size(282, 20);
			this.LevelBox.TabIndex = 3;
			this.CatLbl.AutoSize = true;
			this.CatLbl.Location = new Point(12, 67);
			this.CatLbl.Name = "CatLbl";
			this.CatLbl.Size = new Size(52, 13);
			this.CatLbl.TabIndex = 4;
			this.CatLbl.Text = "Category:";
			this.CatBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.CatBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.CatBox.FormattingEnabled = true;
			this.CatBox.Location = new Point(70, 64);
			this.CatBox.Name = "CatBox";
			this.CatBox.Size = new Size(282, 21);
			this.CatBox.TabIndex = 5;
			this.CatBox.SelectedIndexChanged += new EventHandler(this.CatBox_SelectedIndexChanged);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(364, 300);
			base.Controls.Add(this.CatBox);
			base.Controls.Add(this.CatLbl);
			base.Controls.Add(this.LevelBox);
			base.Controls.Add(this.LevelLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OptionRitualForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Ritual";
			this.Pages.ResumeLayout(false);
			this.InfoPage.ResumeLayout(false);
			this.InfoPage.PerformLayout();
			this.ReadAloudPage.ResumeLayout(false);
			this.ReadAloudPage.PerformLayout();
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			((ISupportInitialize)this.LevelBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public OptionRitualForm(Ritual ritual)
		{
			this.InitializeComponent();
			Array values = Enum.GetValues(typeof(RitualCategory));
			foreach (RitualCategory ritualCategory in values)
			{
				this.CatBox.Items.Add(ritualCategory);
			}
			this.fRitual = ritual.Copy();
			this.NameBox.Text = this.fRitual.Name;
			this.LevelBox.Value = this.fRitual.Level;
			this.CatBox.SelectedItem = this.fRitual.Category;
			this.TimeBox.Text = this.fRitual.Time;
			this.DurationBox.Text = this.fRitual.Duration;
			this.ComponentBox.Text = this.fRitual.ComponentCost;
			this.MarketBox.Text = this.fRitual.MarketPrice;
			this.SkillBox.Text = this.fRitual.KeySkill;
			this.DetailsBox.Text = this.fRitual.Details;
			this.ReadAloudBox.Text = this.fRitual.ReadAloud;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fRitual.Name = this.NameBox.Text;
			this.fRitual.Level = (int)this.LevelBox.Value;
			this.fRitual.Category = (RitualCategory)this.CatBox.SelectedItem;
			this.fRitual.Time = this.TimeBox.Text;
			this.fRitual.Duration = this.DurationBox.Text;
			this.fRitual.ComponentCost = this.ComponentBox.Text;
			this.fRitual.MarketPrice = this.MarketBox.Text;
			this.fRitual.KeySkill = this.SkillBox.Text;
			this.fRitual.Details = this.DetailsBox.Text;
			this.fRitual.ReadAloud = this.ReadAloudBox.Text;
		}

		private void CatBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			RitualCategory ritualCategory = (RitualCategory)this.CatBox.SelectedItem;
			this.SkillBox.Items.Clear();
			switch (ritualCategory)
			{
			case RitualCategory.Binding:
				this.SkillBox.Items.Add("Arcana");
				this.SkillBox.Items.Add("Religion");
				return;
			case RitualCategory.Creation:
				this.SkillBox.Items.Add("Arcana");
				this.SkillBox.Items.Add("Religion");
				return;
			case RitualCategory.Deception:
				this.SkillBox.Items.Add("Arcana");
				return;
			case RitualCategory.Divination:
				this.SkillBox.Items.Add("Arcana");
				this.SkillBox.Items.Add("Nature");
				this.SkillBox.Items.Add("Religion");
				return;
			case RitualCategory.Exploration:
				this.SkillBox.Items.Add("Arcana");
				this.SkillBox.Items.Add("Nature");
				this.SkillBox.Items.Add("Religion");
				return;
			case RitualCategory.Restoration:
				this.SkillBox.Items.Add("Heal");
				return;
			case RitualCategory.Scrying:
				this.SkillBox.Items.Add("Arcana");
				return;
			case RitualCategory.Travel:
				this.SkillBox.Items.Add("Arcana");
				return;
			case RitualCategory.Warding:
				this.SkillBox.Items.Add("Arcana");
				return;
			default:
				return;
			}
		}
	}
}
