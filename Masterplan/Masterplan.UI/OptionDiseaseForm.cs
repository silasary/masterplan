using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class OptionDiseaseForm : Form
	{
		private Disease fDisease;

		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TextBox DetailsBox;

		private Button OKBtn;

		private Button CancelBtn;

		private TextBox LevelBox;

		private Label LevelLbl;

		private TabPage LevelsPage;

		private TextBox MaintainBox;

		private Label MaintainLbl;

		private TextBox ImproveBox;

		private Label ImproveLbl;

		private ListView LevelList;

		private ColumnHeader LevelHdr;

		private ToolStrip Toolbar;

		private ToolStripButton AddBtn;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton UpBtn;

		private ToolStripButton DownBtn;

		public Disease Disease
		{
			get
			{
				return this.fDisease;
			}
		}

		public string SelectedLevel
		{
			get
			{
				if (this.LevelList.SelectedItems.Count != 0)
				{
					return this.LevelList.SelectedItems[0].Tag as string;
				}
				return null;
			}
		}

		public OptionDiseaseForm(Disease disease)
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.fDisease = disease.Copy();
			this.NameBox.Text = this.fDisease.Name;
			this.LevelBox.Text = this.fDisease.Level;
			this.ImproveBox.Text = this.fDisease.ImproveDC;
			this.MaintainBox.Text = this.fDisease.MaintainDC;
			this.DetailsBox.Text = this.fDisease.Details;
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = (this.SelectedLevel != null);
			this.EditBtn.Enabled = (this.SelectedLevel != null);
			this.UpBtn.Enabled = (this.SelectedLevel != null && this.fDisease.Levels[0] != this.SelectedLevel);
			this.DownBtn.Enabled = (this.SelectedLevel != null && this.fDisease.Levels[this.fDisease.Levels.Count - 1] != this.SelectedLevel);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fDisease.Name = this.NameBox.Text;
			this.fDisease.Level = this.LevelBox.Text;
			this.fDisease.ImproveDC = this.ImproveBox.Text;
			this.fDisease.MaintainDC = this.MaintainBox.Text;
			this.fDisease.Details = this.DetailsBox.Text;
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			string level = "New Disease Level";
			OptionDiseaseLevelForm optionDiseaseLevelForm = new OptionDiseaseLevelForm(level);
			if (optionDiseaseLevelForm.ShowDialog() == DialogResult.OK)
			{
				this.fDisease.Levels.Add(optionDiseaseLevelForm.DiseaseLevel);
				this.update_list();
			}
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLevel != null)
			{
				this.fDisease.Levels.Remove(this.SelectedLevel);
				this.update_list();
			}
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLevel != null)
			{
				int index = this.fDisease.Levels.IndexOf(this.SelectedLevel);
				OptionDiseaseLevelForm optionDiseaseLevelForm = new OptionDiseaseLevelForm(this.SelectedLevel);
				if (optionDiseaseLevelForm.ShowDialog() == DialogResult.OK)
				{
					this.fDisease.Levels[index] = optionDiseaseLevelForm.DiseaseLevel;
					this.update_list();
				}
			}
		}

		private void UpBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLevel != null)
			{
				int num = this.fDisease.Levels.IndexOf(this.SelectedLevel);
				string value = this.fDisease.Levels[num - 1];
				this.fDisease.Levels[num - 1] = this.SelectedLevel;
				this.fDisease.Levels[num] = value;
				this.update_list();
			}
		}

		private void DownBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLevel != null)
			{
				int num = this.fDisease.Levels.IndexOf(this.SelectedLevel);
				string value = this.fDisease.Levels[num + 1];
				this.fDisease.Levels[num + 1] = this.SelectedLevel;
				this.fDisease.Levels[num] = value;
				this.update_list();
			}
		}

		private void update_list()
		{
			this.LevelList.Items.Clear();
			this.LevelList.Items.Add("The target is cured.");
			foreach (string current in this.fDisease.Levels)
			{
				string text = current;
				if (this.fDisease.Levels.Count > 1)
				{
					int num = this.fDisease.Levels.IndexOf(current);
					if (num == 0)
					{
						text = "Initial state: " + text;
					}
					if (num == this.fDisease.Levels.Count - 1)
					{
						text = "Final state: " + text;
					}
				}
				ListViewItem listViewItem = this.LevelList.Items.Add(text);
				listViewItem.Tag = current;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(OptionDiseaseForm));
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.DetailsBox = new TextBox();
			this.LevelsPage = new TabPage();
			this.LevelList = new ListView();
			this.LevelHdr = new ColumnHeader();
			this.Toolbar = new ToolStrip();
			this.AddBtn = new ToolStripButton();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.UpBtn = new ToolStripButton();
			this.DownBtn = new ToolStripButton();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.LevelBox = new TextBox();
			this.LevelLbl = new Label();
			this.MaintainBox = new TextBox();
			this.MaintainLbl = new Label();
			this.ImproveBox = new TextBox();
			this.ImproveLbl = new Label();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.LevelsPage.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(86, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(272, 20);
			this.NameBox.TabIndex = 1;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Controls.Add(this.LevelsPage);
			this.Pages.Location = new Point(12, 116);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(346, 239);
			this.Pages.TabIndex = 10;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new Padding(3);
			this.DetailsPage.Size = new Size(338, 213);
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
			this.DetailsBox.Size = new Size(332, 207);
			this.DetailsBox.TabIndex = 0;
			this.LevelsPage.Controls.Add(this.LevelList);
			this.LevelsPage.Controls.Add(this.Toolbar);
			this.LevelsPage.Location = new Point(4, 22);
			this.LevelsPage.Name = "LevelsPage";
			this.LevelsPage.Padding = new Padding(3);
			this.LevelsPage.Size = new Size(338, 165);
			this.LevelsPage.TabIndex = 1;
			this.LevelsPage.Text = "Disease Levels";
			this.LevelsPage.UseVisualStyleBackColor = true;
			this.LevelList.Columns.AddRange(new ColumnHeader[]
			{
				this.LevelHdr
			});
			this.LevelList.Dock = DockStyle.Fill;
			this.LevelList.FullRowSelect = true;
			this.LevelList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.LevelList.HideSelection = false;
			this.LevelList.Location = new Point(3, 28);
			this.LevelList.MultiSelect = false;
			this.LevelList.Name = "LevelList";
			this.LevelList.Size = new Size(332, 134);
			this.LevelList.TabIndex = 1;
			this.LevelList.UseCompatibleStateImageBehavior = false;
			this.LevelList.View = View.Details;
			this.LevelList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.LevelHdr.Text = "Disease Level";
			this.LevelHdr.Width = 272;
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.AddBtn,
				this.RemoveBtn,
				this.EditBtn,
				this.toolStripSeparator1,
				this.UpBtn,
				this.DownBtn
			});
			this.Toolbar.Location = new Point(3, 3);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(332, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddBtn.Image = (Image)resources.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new Size(33, 22);
			this.AddBtn.Text = "Add";
			this.AddBtn.Click += new EventHandler(this.AddBtn_Click);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)resources.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)resources.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new Size(31, 22);
			this.EditBtn.Text = "Edit";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.UpBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.UpBtn.Image = (Image)resources.GetObject("UpBtn.Image");
			this.UpBtn.ImageTransparentColor = Color.Magenta;
			this.UpBtn.Name = "UpBtn";
			this.UpBtn.Size = new Size(59, 22);
			this.UpBtn.Text = "Move Up";
			this.UpBtn.Click += new EventHandler(this.UpBtn_Click);
			this.DownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DownBtn.Image = (Image)resources.GetObject("DownBtn.Image");
			this.DownBtn.ImageTransparentColor = Color.Magenta;
			this.DownBtn.Name = "DownBtn";
			this.DownBtn.Size = new Size(75, 22);
			this.DownBtn.Text = "Move Down";
			this.DownBtn.Click += new EventHandler(this.DownBtn_Click);
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(202, 361);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 11;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(283, 361);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 12;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.LevelBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LevelBox.Location = new Point(86, 38);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new Size(272, 20);
			this.LevelBox.TabIndex = 3;
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(12, 41);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(36, 13);
			this.LevelLbl.TabIndex = 2;
			this.LevelLbl.Text = "Level:";
			this.MaintainBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.MaintainBox.Location = new Point(86, 90);
			this.MaintainBox.Name = "MaintainBox";
			this.MaintainBox.Size = new Size(272, 20);
			this.MaintainBox.TabIndex = 9;
			this.MaintainLbl.AutoSize = true;
			this.MaintainLbl.Location = new Point(12, 93);
			this.MaintainLbl.Name = "MaintainLbl";
			this.MaintainLbl.Size = new Size(68, 13);
			this.MaintainLbl.TabIndex = 8;
			this.MaintainLbl.Text = "Maintain DC:";
			this.ImproveBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ImproveBox.Location = new Point(86, 64);
			this.ImproveBox.Name = "ImproveBox";
			this.ImproveBox.Size = new Size(272, 20);
			this.ImproveBox.TabIndex = 7;
			this.ImproveLbl.AutoSize = true;
			this.ImproveLbl.Location = new Point(12, 67);
			this.ImproveLbl.Name = "ImproveLbl";
			this.ImproveLbl.Size = new Size(66, 13);
			this.ImproveLbl.TabIndex = 6;
			this.ImproveLbl.Text = "Improve DC:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(370, 396);
			base.Controls.Add(this.MaintainBox);
			base.Controls.Add(this.MaintainLbl);
			base.Controls.Add(this.ImproveBox);
			base.Controls.Add(this.ImproveLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.LevelBox);
			base.Controls.Add(this.LevelLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OptionDiseaseForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Disease";
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.LevelsPage.ResumeLayout(false);
			this.LevelsPage.PerformLayout();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
