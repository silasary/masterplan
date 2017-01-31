using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class OptionCreatureLoreForm : Form
	{
		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private TabControl Pages;

		private Button OKBtn;

		private Button CancelBtn;

		private TabPage InfoPage;

		private ListView InfoList;

		private ColumnHeader InfoHdr;

		private ToolStrip LevelToolbar;

		private ToolStripButton EditBtn;

		private Label SkillLbl;

		private ToolStripButton AddBtn;

		private ToolStripButton RemoveBtn;

		private ComboBox SkillBox;

		private CreatureLore fCreatureLore;

		public CreatureLore CreatureLore
		{
			get
			{
				return this.fCreatureLore;
			}
		}

		public Pair<int, string> SelectedInformation
		{
			get
			{
				if (this.InfoList.SelectedItems.Count != 0)
				{
					return this.InfoList.SelectedItems[0].Tag as Pair<int, string>;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OptionCreatureLoreForm));
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.Pages = new TabControl();
			this.InfoPage = new TabPage();
			this.InfoList = new ListView();
			this.InfoHdr = new ColumnHeader();
			this.LevelToolbar = new ToolStrip();
			this.AddBtn = new ToolStripButton();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.SkillLbl = new Label();
			this.SkillBox = new ComboBox();
			this.Pages.SuspendLayout();
			this.InfoPage.SuspendLayout();
			this.LevelToolbar.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(81, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Creature Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(99, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(230, 20);
			this.NameBox.TabIndex = 1;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.InfoPage);
			this.Pages.Location = new Point(12, 65);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(317, 203);
			this.Pages.TabIndex = 4;
			this.InfoPage.Controls.Add(this.InfoList);
			this.InfoPage.Controls.Add(this.LevelToolbar);
			this.InfoPage.Location = new Point(4, 22);
			this.InfoPage.Name = "InfoPage";
			this.InfoPage.Padding = new Padding(3);
			this.InfoPage.Size = new Size(309, 177);
			this.InfoPage.TabIndex = 2;
			this.InfoPage.Text = "Information";
			this.InfoPage.UseVisualStyleBackColor = true;
			this.InfoList.Columns.AddRange(new ColumnHeader[]
			{
				this.InfoHdr
			});
			this.InfoList.Dock = DockStyle.Fill;
			this.InfoList.FullRowSelect = true;
			this.InfoList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.InfoList.HideSelection = false;
			this.InfoList.Location = new Point(3, 28);
			this.InfoList.MultiSelect = false;
			this.InfoList.Name = "InfoList";
			this.InfoList.Size = new Size(303, 146);
			this.InfoList.TabIndex = 1;
			this.InfoList.UseCompatibleStateImageBehavior = false;
			this.InfoList.View = View.Details;
			this.InfoList.DoubleClick += new EventHandler(this.FeatureEditBtn_Click);
			this.InfoHdr.Text = "Information";
			this.InfoHdr.Width = 273;
			this.LevelToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.AddBtn,
				this.RemoveBtn,
				this.EditBtn
			});
			this.LevelToolbar.Location = new Point(3, 3);
			this.LevelToolbar.Name = "LevelToolbar";
			this.LevelToolbar.Size = new Size(303, 25);
			this.LevelToolbar.TabIndex = 0;
			this.LevelToolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddBtn.Image = (Image)componentResourceManager.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new Size(33, 22);
			this.AddBtn.Text = "Add";
			this.AddBtn.Click += new EventHandler(this.AddBtn_Click);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)componentResourceManager.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)componentResourceManager.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new Size(31, 22);
			this.EditBtn.Text = "Edit";
			this.EditBtn.Click += new EventHandler(this.FeatureEditBtn_Click);
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(173, 274);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 5;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(254, 274);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 6;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.SkillLbl.AutoSize = true;
			this.SkillLbl.Location = new Point(12, 41);
			this.SkillLbl.Name = "SkillLbl";
			this.SkillLbl.Size = new Size(60, 13);
			this.SkillLbl.TabIndex = 2;
			this.SkillLbl.Text = "Skill Name:";
			this.SkillBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SkillBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.SkillBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.SkillBox.FormattingEnabled = true;
			this.SkillBox.Location = new Point(99, 38);
			this.SkillBox.Name = "SkillBox";
			this.SkillBox.Size = new Size(230, 21);
			this.SkillBox.TabIndex = 3;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(341, 309);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.SkillBox);
			base.Controls.Add(this.SkillLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OptionCreatureLoreForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Creature Lore";
			this.Pages.ResumeLayout(false);
			this.InfoPage.ResumeLayout(false);
			this.InfoPage.PerformLayout();
			this.LevelToolbar.ResumeLayout(false);
			this.LevelToolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public OptionCreatureLoreForm(CreatureLore cl)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.SkillBox.Items.Add("Arcana");
			this.SkillBox.Items.Add("Dungeoneering");
			this.SkillBox.Items.Add("History");
			this.SkillBox.Items.Add("Nature");
			this.SkillBox.Items.Add("Religion");
			this.fCreatureLore = cl.Copy();
			this.NameBox.Text = this.fCreatureLore.Name;
			this.SkillBox.Text = this.fCreatureLore.SkillName;
			this.update_information();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = (this.SelectedInformation != null);
			this.EditBtn.Enabled = (this.SelectedInformation != null);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fCreatureLore.Name = this.NameBox.Text;
			this.fCreatureLore.SkillName = this.SkillBox.Text;
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			OptionInformationForm optionInformationForm = new OptionInformationForm(new Pair<int, string>
			{
				First = 10,
				Second = ""
			});
			if (optionInformationForm.ShowDialog() == DialogResult.OK)
			{
				this.fCreatureLore.Information.Add(optionInformationForm.Information);
				this.update_information();
			}
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedInformation != null)
			{
				this.fCreatureLore.Information.Remove(this.SelectedInformation);
				this.update_information();
			}
		}

		private void FeatureEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedInformation != null)
			{
				int index = this.fCreatureLore.Information.IndexOf(this.SelectedInformation);
				OptionInformationForm optionInformationForm = new OptionInformationForm(this.SelectedInformation);
				if (optionInformationForm.ShowDialog() == DialogResult.OK)
				{
					this.fCreatureLore.Information[index] = optionInformationForm.Information;
					this.update_information();
				}
			}
		}

		private void update_information()
		{
			this.fCreatureLore.Information.Sort();
			this.InfoList.Items.Clear();
			foreach (Pair<int, string> current in this.fCreatureLore.Information)
			{
				string text = string.Concat(new object[]
				{
					"DC ",
					current.First,
					": ",
					current.Second
				});
				ListViewItem listViewItem = this.InfoList.Items.Add(text);
				listViewItem.Tag = current;
			}
		}
	}
}
