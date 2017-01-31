using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class OptionPoisonForm : Form
	{
		private Poison fPoison;

		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TextBox DetailsBox;

		private Button OKBtn;

		private Button CancelBtn;

		private TabPage SectionPage;

		private ListView SectionList;

		private ColumnHeader SectionHdr;

		private ToolStrip SectionToolbar;

		private ToolStripButton SectionAddBtn;

		private ToolStripButton SectionRemoveBtn;

		private ToolStripButton SectionEditBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton SectionUpBtn;

		private ToolStripButton SectionDownBtn;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton SectionLeftBtn;

		private ToolStripButton SectionRightBtn;

		private Label LevelLbl;

		private NumericUpDown LevelBox;

		public Poison Poison
		{
			get
			{
				return this.fPoison;
			}
		}

		public PlayerPowerSection SelectedSection
		{
			get
			{
				if (this.SectionList.SelectedItems.Count != 0)
				{
					return this.SectionList.SelectedItems[0].Tag as PlayerPowerSection;
				}
				return null;
			}
		}

		public OptionPoisonForm(Poison poison)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fPoison = poison.Copy();
			this.NameBox.Text = this.fPoison.Name;
			this.LevelBox.Value = this.fPoison.Level;
			this.DetailsBox.Text = this.fPoison.Details;
			this.update_sections();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			int num = this.fPoison.Sections.IndexOf(this.SelectedSection);
			this.SectionRemoveBtn.Enabled = (this.SelectedSection != null);
			this.SectionEditBtn.Enabled = (this.SelectedSection != null);
			this.SectionUpBtn.Enabled = (this.SelectedSection != null && num != 0);
			this.SectionDownBtn.Enabled = (this.SelectedSection != null && num != this.fPoison.Sections.Count - 1);
			this.SectionLeftBtn.Enabled = (this.SelectedSection != null && this.SelectedSection.Indent > 0);
			this.SectionRightBtn.Enabled = false;
			if (num > 0)
			{
				PlayerPowerSection playerPowerSection = this.fPoison.Sections[num - 1];
				this.SectionRightBtn.Enabled = (this.SelectedSection != null && this.SelectedSection.Indent <= playerPowerSection.Indent);
			}
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fPoison.Name = this.NameBox.Text;
			this.fPoison.Level = (int)this.LevelBox.Value;
			this.fPoison.Details = this.DetailsBox.Text;
		}

		private void SectionAddBtn_Click(object sender, EventArgs e)
		{
			PlayerPowerSection section = new PlayerPowerSection();
			OptionPowerSectionForm optionPowerSectionForm = new OptionPowerSectionForm(section);
			if (optionPowerSectionForm.ShowDialog() == DialogResult.OK)
			{
				this.fPoison.Sections.Add(optionPowerSectionForm.Section);
				this.update_sections();
			}
		}

		private void SectionRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSection != null)
			{
				this.fPoison.Sections.Remove(this.SelectedSection);
				this.update_sections();
			}
		}

		private void SectionEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSection != null)
			{
				int index = this.fPoison.Sections.IndexOf(this.SelectedSection);
				OptionPowerSectionForm optionPowerSectionForm = new OptionPowerSectionForm(this.SelectedSection);
				if (optionPowerSectionForm.ShowDialog() == DialogResult.OK)
				{
					this.fPoison.Sections[index] = optionPowerSectionForm.Section;
					this.update_sections();
				}
			}
		}

		private void SectionUpBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSection != null)
			{
				int num = this.fPoison.Sections.IndexOf(this.SelectedSection);
				PlayerPowerSection value = this.fPoison.Sections[num - 1];
				this.fPoison.Sections[num - 1] = this.SelectedSection;
				this.fPoison.Sections[num] = value;
				this.update_sections();
				this.SectionList.SelectedIndices.Add(num - 1);
			}
		}

		private void SectionDownBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSection != null)
			{
				int num = this.fPoison.Sections.IndexOf(this.SelectedSection);
				PlayerPowerSection value = this.fPoison.Sections[num + 1];
				this.fPoison.Sections[num + 1] = this.SelectedSection;
				this.fPoison.Sections[num] = value;
				this.update_sections();
				this.SectionList.SelectedIndices.Add(num + 1);
			}
		}

		private void SectionLeftBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSection != null)
			{
				int itemIndex = this.fPoison.Sections.IndexOf(this.SelectedSection);
				this.SelectedSection.Indent--;
				this.update_sections();
				this.SectionList.SelectedIndices.Add(itemIndex);
			}
		}

		private void SectionRightBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSection != null)
			{
				int itemIndex = this.fPoison.Sections.IndexOf(this.SelectedSection);
				this.SelectedSection.Indent++;
				this.update_sections();
				this.SectionList.SelectedIndices.Add(itemIndex);
			}
		}

		private void update_sections()
		{
			this.SectionList.Items.Clear();
			foreach (PlayerPowerSection current in this.fPoison.Sections)
			{
				string text = "";
				for (int num = 0; num != current.Indent; num++)
				{
					text += "    ";
				}
				text = text + current.Header + ": " + current.Details;
				ListViewItem listViewItem = this.SectionList.Items.Add(text);
				listViewItem.Tag = current;
			}
			if (this.fPoison.Sections.Count == 0)
			{
				ListViewItem listViewItem2 = this.SectionList.Items.Add("(none)");
				listViewItem2.ForeColor = SystemColors.GrayText;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OptionPoisonForm));
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.DetailsBox = new TextBox();
			this.SectionPage = new TabPage();
			this.SectionList = new ListView();
			this.SectionHdr = new ColumnHeader();
			this.SectionToolbar = new ToolStrip();
			this.SectionAddBtn = new ToolStripButton();
			this.SectionRemoveBtn = new ToolStripButton();
			this.SectionEditBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.SectionUpBtn = new ToolStripButton();
			this.SectionDownBtn = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.SectionLeftBtn = new ToolStripButton();
			this.SectionRightBtn = new ToolStripButton();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.LevelLbl = new Label();
			this.LevelBox = new NumericUpDown();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.SectionPage.SuspendLayout();
			this.SectionToolbar.SuspendLayout();
			((ISupportInitialize)this.LevelBox).BeginInit();
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
			this.NameBox.Size = new Size(297, 20);
			this.NameBox.TabIndex = 1;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Controls.Add(this.SectionPage);
			this.Pages.Location = new Point(12, 64);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(341, 170);
			this.Pages.TabIndex = 4;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new Padding(3);
			this.DetailsPage.Size = new Size(333, 144);
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
			this.DetailsBox.Size = new Size(327, 138);
			this.DetailsBox.TabIndex = 0;
			this.SectionPage.Controls.Add(this.SectionList);
			this.SectionPage.Controls.Add(this.SectionToolbar);
			this.SectionPage.Location = new Point(4, 22);
			this.SectionPage.Name = "SectionPage";
			this.SectionPage.Padding = new Padding(3);
			this.SectionPage.Size = new Size(333, 144);
			this.SectionPage.TabIndex = 1;
			this.SectionPage.Text = "Sections";
			this.SectionPage.UseVisualStyleBackColor = true;
			this.SectionList.Columns.AddRange(new ColumnHeader[]
			{
				this.SectionHdr
			});
			this.SectionList.Dock = DockStyle.Fill;
			this.SectionList.FullRowSelect = true;
			this.SectionList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SectionList.HideSelection = false;
			this.SectionList.Location = new Point(3, 28);
			this.SectionList.MultiSelect = false;
			this.SectionList.Name = "SectionList";
			this.SectionList.Size = new Size(327, 113);
			this.SectionList.TabIndex = 1;
			this.SectionList.UseCompatibleStateImageBehavior = false;
			this.SectionList.View = View.Details;
			this.SectionHdr.Text = "Section";
			this.SectionHdr.Width = 300;
			this.SectionToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.SectionAddBtn,
				this.SectionRemoveBtn,
				this.SectionEditBtn,
				this.toolStripSeparator1,
				this.SectionUpBtn,
				this.SectionDownBtn,
				this.toolStripSeparator2,
				this.SectionLeftBtn,
				this.SectionRightBtn
			});
			this.SectionToolbar.Location = new Point(3, 3);
			this.SectionToolbar.Name = "SectionToolbar";
			this.SectionToolbar.Size = new Size(327, 25);
			this.SectionToolbar.TabIndex = 0;
			this.SectionToolbar.Text = "toolStrip1";
			this.SectionAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SectionAddBtn.Image = (Image)componentResourceManager.GetObject("SectionAddBtn.Image");
			this.SectionAddBtn.ImageTransparentColor = Color.Magenta;
			this.SectionAddBtn.Name = "SectionAddBtn";
			this.SectionAddBtn.Size = new Size(33, 22);
			this.SectionAddBtn.Text = "Add";
			this.SectionAddBtn.Click += new EventHandler(this.SectionAddBtn_Click);
			this.SectionRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SectionRemoveBtn.Image = (Image)componentResourceManager.GetObject("SectionRemoveBtn.Image");
			this.SectionRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.SectionRemoveBtn.Name = "SectionRemoveBtn";
			this.SectionRemoveBtn.Size = new Size(54, 22);
			this.SectionRemoveBtn.Text = "Remove";
			this.SectionRemoveBtn.Click += new EventHandler(this.SectionRemoveBtn_Click);
			this.SectionEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SectionEditBtn.Image = (Image)componentResourceManager.GetObject("SectionEditBtn.Image");
			this.SectionEditBtn.ImageTransparentColor = Color.Magenta;
			this.SectionEditBtn.Name = "SectionEditBtn";
			this.SectionEditBtn.Size = new Size(31, 22);
			this.SectionEditBtn.Text = "Edit";
			this.SectionEditBtn.Click += new EventHandler(this.SectionEditBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.SectionUpBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SectionUpBtn.Image = (Image)componentResourceManager.GetObject("SectionUpBtn.Image");
			this.SectionUpBtn.ImageTransparentColor = Color.Magenta;
			this.SectionUpBtn.Name = "SectionUpBtn";
			this.SectionUpBtn.Size = new Size(26, 22);
			this.SectionUpBtn.Text = "Up";
			this.SectionUpBtn.Click += new EventHandler(this.SectionUpBtn_Click);
			this.SectionDownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SectionDownBtn.Image = (Image)componentResourceManager.GetObject("SectionDownBtn.Image");
			this.SectionDownBtn.ImageTransparentColor = Color.Magenta;
			this.SectionDownBtn.Name = "SectionDownBtn";
			this.SectionDownBtn.Size = new Size(42, 22);
			this.SectionDownBtn.Text = "Down";
			this.SectionDownBtn.Click += new EventHandler(this.SectionDownBtn_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 25);
			this.SectionLeftBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SectionLeftBtn.Image = (Image)componentResourceManager.GetObject("SectionLeftBtn.Image");
			this.SectionLeftBtn.ImageTransparentColor = Color.Magenta;
			this.SectionLeftBtn.Name = "SectionLeftBtn";
			this.SectionLeftBtn.Size = new Size(31, 22);
			this.SectionLeftBtn.Text = "Left";
			this.SectionLeftBtn.Click += new EventHandler(this.SectionLeftBtn_Click);
			this.SectionRightBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SectionRightBtn.Image = (Image)componentResourceManager.GetObject("SectionRightBtn.Image");
			this.SectionRightBtn.ImageTransparentColor = Color.Magenta;
			this.SectionRightBtn.Name = "SectionRightBtn";
			this.SectionRightBtn.Size = new Size(39, 22);
			this.SectionRightBtn.Text = "Right";
			this.SectionRightBtn.Click += new EventHandler(this.SectionRightBtn_Click);
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(197, 240);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 5;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(278, 240);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 6;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(12, 40);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(36, 13);
			this.LevelLbl.TabIndex = 2;
			this.LevelLbl.Text = "Level:";
			this.LevelBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LevelBox.Location = new Point(56, 38);
			NumericUpDown arg_B50_0 = this.LevelBox;
			int[] array = new int[4];
			array[0] = 30;
			arg_B50_0.Maximum = new decimal(array);
			NumericUpDown arg_B6F_0 = this.LevelBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_B6F_0.Minimum = new decimal(array2);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new Size(297, 20);
			this.LevelBox.TabIndex = 3;
			NumericUpDown arg_BC1_0 = this.LevelBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_BC1_0.Value = new decimal(array3);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(365, 275);
			base.Controls.Add(this.LevelBox);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.LevelLbl);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.Controls.Add(this.Pages);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OptionPoisonForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Poison";
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.SectionPage.ResumeLayout(false);
			this.SectionPage.PerformLayout();
			this.SectionToolbar.ResumeLayout(false);
			this.SectionToolbar.PerformLayout();
			((ISupportInitialize)this.LevelBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
