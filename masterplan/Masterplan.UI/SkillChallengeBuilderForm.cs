using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;
using Utils.Controls;

namespace Masterplan.UI
{
	internal class SkillChallengeBuilderForm : Form
	{
		private SkillChallenge fChallenge;

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private TabControl Pages;

		private TabPage SkillsPage;

		private ListView SkillList;

		private ColumnHeader SkillHdr;

		private ColumnHeader DCHdr;

		private ToolStrip SkillsToolbar;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private SplitContainer SkillSplitter;

		private ListView SkillSourceList;

		private ColumnHeader SkillSourceHdr;

		private TabPage InfoPage;

		private SplitContainer InfoSplitter;

		private DefaultTextBox VictoryBox;

		private ToolStrip VictoryToolbar;

		private ToolStripLabel toolStripLabel1;

		private DefaultTextBox DefeatBox;

		private ToolStrip DefeatButton;

		private ToolStripLabel toolStripLabel2;

		private SplitContainer OverviewSplitter;

		private TabPage OverviewPage;

		private Label LevelLbl;

		private NumericUpDown LevelBox;

		private TextBox NameBox;

		private Label NameLbl;

		private Label CompLbl;

		private NumericUpDown CompBox;

		private ListView InfoList;

		private ColumnHeader InfoHdr;

		private ColumnHeader StdDCHdr;

		private GroupBox LevelGroup;

		private GroupBox CompGroup;

		private Label XPLbl;

		private Label XPInfoLbl;

		private Label LengthLbl;

		private Label LengthInfoLbl;

		private ColumnHeader AbilityHdr;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton BreakdownBtn;

		private TabPage NotesPage;

		private DefaultTextBox NotesBox;

		private ToolStrip Toolbar;

		private ToolStripDropDownButton FileMenu;

		private ToolStripMenuItem FileExport;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripLabel SuccessCountLbl;

		private ToolStripLabel FailureCountLbl;

		private ToolStripButton ResetProgressBtn;

		public SkillChallenge SkillChallenge
		{
			get
			{
				return this.fChallenge;
			}
		}

		public SkillChallengeData SelectedSkill
		{
			get
			{
				if (this.SkillList.SelectedItems.Count != 0)
				{
					return this.SkillList.SelectedItems[0].Tag as SkillChallengeData;
				}
				return null;
			}
		}

		public string SelectedSourceSkill
		{
			get
			{
				if (this.SkillSourceList.SelectedItems.Count != 0)
				{
					return this.SkillSourceList.SelectedItems[0].Text;
				}
				return "";
			}
		}

		public SkillChallengeBuilderForm(SkillChallenge sc)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fChallenge = (sc.Copy() as SkillChallenge);
			this.update_all();
			List<string> skillNames = Skills.GetSkillNames();
			foreach (string current in skillNames)
			{
				string text = Skills.GetKeyAbility(current).Substring(0, 3);
				string text2 = text.Substring(0, 3);
				ListViewItem listViewItem = this.SkillSourceList.Items.Add(current);
				ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems.Add(text2);
				listViewItem.UseItemStyleForSubItems = false;
				listViewSubItem.ForeColor = SystemColors.GrayText;
				listViewItem.Group = this.SkillSourceList.Groups[0];
			}
			List<string> abilityNames = Skills.GetAbilityNames();
			foreach (string current2 in abilityNames)
			{
				string text3 = current2.Substring(0, 3);
				ListViewItem listViewItem2 = this.SkillSourceList.Items.Add(current2);
				ListViewItem.ListViewSubItem listViewSubItem2 = listViewItem2.SubItems.Add(text3);
				listViewItem2.UseItemStyleForSubItems = false;
				listViewSubItem2.ForeColor = SystemColors.GrayText;
				listViewItem2.Group = this.SkillSourceList.Groups[1];
			}
			ListViewItem listViewItem3 = this.SkillSourceList.Items.Add("(custom skill)");
			ListViewItem.ListViewSubItem listViewSubItem3 = listViewItem3.SubItems.Add("");
			listViewItem3.UseItemStyleForSubItems = false;
			listViewSubItem3.ForeColor = SystemColors.GrayText;
			listViewItem3.Group = this.SkillSourceList.Groups[2];
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = (this.SelectedSkill != null);
			this.EditBtn.Enabled = (this.SelectedSkill != null);
			this.BreakdownBtn.Enabled = (this.fChallenge.Skills.Count != 0);
			SkillChallengeResult results = this.fChallenge.Results;
			this.ResetProgressBtn.Enabled = (results.Successes + results.Fails != 0);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fChallenge.Name = this.NameBox.Text;
			this.fChallenge.Complexity = (int)this.CompBox.Value;
			if (this.LevelBox.Enabled)
			{
				this.fChallenge.Level = (int)this.LevelBox.Value;
			}
			this.fChallenge.Success = ((this.VictoryBox.Text != this.VictoryBox.DefaultText) ? this.VictoryBox.Text : "");
			this.fChallenge.Failure = ((this.DefeatBox.Text != this.DefeatBox.DefaultText) ? this.DefeatBox.Text : "");
			this.fChallenge.Notes = ((this.NotesBox.Text != this.NotesBox.DefaultText) ? this.NotesBox.Text : "");
		}

		public void update_all()
		{
			this.NameBox.Text = this.fChallenge.Name;
			this.CompBox.Value = this.fChallenge.Complexity;
			this.VictoryBox.Text = this.fChallenge.Success;
			this.DefeatBox.Text = this.fChallenge.Failure;
			this.NotesBox.Text = this.fChallenge.Notes;
			if (this.fChallenge.Level != -1)
			{
				this.LevelBox.Value = this.fChallenge.Level;
			}
			else
			{
				this.LevelBox.Enabled = false;
			}
			this.update_view();
			this.update_skills();
		}

		private void LevelBox_ValueChanged(object sender, EventArgs e)
		{
			this.update_view();
		}

		private void CompBox_ValueChanged(object sender, EventArgs e)
		{
			this.update_view();
		}

		private void update_view()
		{
			int level = (int)this.LevelBox.Value;
			int complexity = (int)this.CompBox.Value;
			this.LengthLbl.Text = SkillChallenge.GetSuccesses(complexity) + " successes before 3 failures";
			this.InfoList.Items.Clear();
			if (this.fChallenge.Level != -1)
			{
				ListViewItem listViewItem = this.InfoList.Items.Add("Easy");
				listViewItem.SubItems.Add("DC " + AI.GetSkillDC(Difficulty.Easy, level));
				listViewItem.Group = this.InfoList.Groups[0];
				ListViewItem listViewItem2 = this.InfoList.Items.Add("Moderate");
				listViewItem2.SubItems.Add("DC " + AI.GetSkillDC(Difficulty.Moderate, level));
				listViewItem2.Group = this.InfoList.Groups[0];
				ListViewItem listViewItem3 = this.InfoList.Items.Add("Hard");
				listViewItem3.SubItems.Add("DC " + AI.GetSkillDC(Difficulty.Hard, level));
				listViewItem3.Group = this.InfoList.Groups[0];
				this.XPLbl.Text = SkillChallenge.GetXP(level, complexity) + " XP";
			}
			else
			{
				ListViewItem listViewItem4 = this.InfoList.Items.Add("DCs");
				ListViewItem.ListViewSubItem listViewSubItem = listViewItem4.SubItems.Add("(varies by level)");
				listViewItem4.UseItemStyleForSubItems = false;
				listViewSubItem.ForeColor = SystemColors.GrayText;
				listViewItem4.Group = this.InfoList.Groups[0];
				this.XPLbl.Text = "(XP varies by level)";
			}
			SkillChallengeResult results = this.fChallenge.Results;
			this.SuccessCountLbl.Text = "Successes: " + results.Successes;
			this.FailureCountLbl.Text = "Failures: " + results.Fails;
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSkill != null)
			{
				this.fChallenge.Skills.Remove(this.SelectedSkill);
				this.update_view();
				this.update_skills();
			}
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSkill != null)
			{
				int index = this.fChallenge.Skills.IndexOf(this.SelectedSkill);
				SkillChallengeSkillForm skillChallengeSkillForm = new SkillChallengeSkillForm(this.SelectedSkill);
				if (skillChallengeSkillForm.ShowDialog() == DialogResult.OK)
				{
					this.fChallenge.Skills[index] = skillChallengeSkillForm.SkillData;
					this.fChallenge.Skills.Sort();
					this.update_view();
					this.update_skills();
				}
			}
		}

		private void update_skills()
		{
			this.SkillList.Items.Clear();
			foreach (SkillChallengeData current in this.fChallenge.Skills)
			{
				string text = current.Difficulty + " DCs";
				if (current.DCModifier != 0)
				{
					if (current.DCModifier > 0)
					{
						text = text + " +" + current.DCModifier;
					}
					else
					{
						text = text + " " + current.DCModifier;
					}
				}
				ListViewItem listViewItem = this.SkillList.Items.Add(current.SkillName);
				listViewItem.SubItems.Add(text);
				listViewItem.Tag = current;
				switch (current.Type)
				{
				case SkillType.Primary:
					listViewItem.Group = this.SkillList.Groups[0];
					break;
				case SkillType.Secondary:
					listViewItem.Group = this.SkillList.Groups[1];
					break;
				case SkillType.AutoFail:
					listViewItem.Group = this.SkillList.Groups[2];
					break;
				}
				if (current.Details == "" && current.Success == "" && current.Failure == "")
				{
					listViewItem.ForeColor = SystemColors.GrayText;
				}
				if (current.Difficulty == Difficulty.Trivial || current.Difficulty == Difficulty.Extreme)
				{
					listViewItem.UseItemStyleForSubItems = false;
					listViewItem.SubItems[1].ForeColor = Color.Red;
				}
			}
			if (this.SkillList.Groups[0].Items.Count == 0)
			{
				ListViewItem listViewItem2 = this.SkillList.Items.Add("(none)");
				listViewItem2.Group = this.SkillList.Groups[0];
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
			if (this.SkillList.Groups[1].Items.Count == 0)
			{
				ListViewItem listViewItem3 = this.SkillList.Items.Add("(none)");
				listViewItem3.Group = this.SkillList.Groups[1];
				listViewItem3.ForeColor = SystemColors.GrayText;
			}
		}

		private void SkillSourceList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			ListViewItem listViewItem = e.Item as ListViewItem;
			if (listViewItem != null)
			{
				string text = listViewItem.Text;
				DragDropEffects dragDropEffects = base.DoDragDrop(text, DragDropEffects.Copy);
				if (dragDropEffects == DragDropEffects.Copy)
				{
					this.add_skill(text);
				}
			}
		}

		private void SkillList_DragOver(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.None;
			string text = e.Data.GetData(typeof(string)) as string;
			if (text != null && text != "")
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void SkillSourceList_DoubleClick(object sender, EventArgs e)
		{
			string selectedSourceSkill = this.SelectedSourceSkill;
			if (selectedSourceSkill != "")
			{
				this.add_skill(selectedSourceSkill);
			}
		}

		private void add_skill(string skill_name)
		{
			SkillChallengeSkillForm skillChallengeSkillForm = new SkillChallengeSkillForm(new SkillChallengeData
			{
				SkillName = skill_name
			});
			if (skillChallengeSkillForm.ShowDialog() == DialogResult.OK)
			{
				this.fChallenge.Skills.Add(skillChallengeSkillForm.SkillData);
				this.fChallenge.Skills.Sort();
				this.update_view();
				this.update_skills();
			}
		}

		private void BreakdownBtn_Click(object sender, EventArgs e)
		{
			SkillChallengeBreakdownForm skillChallengeBreakdownForm = new SkillChallengeBreakdownForm(this.fChallenge);
			skillChallengeBreakdownForm.ShowDialog();
		}

		private void ResetProgressBtn_Click(object sender, EventArgs e)
		{
			foreach (SkillChallengeData current in this.fChallenge.Skills)
			{
				current.Results.Successes = 0;
				current.Results.Fails = 0;
			}
			this.update_view();
		}

		private void FileExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Title = "Export Skill Challenge";
			saveFileDialog.FileName = this.fChallenge.Name;
			saveFileDialog.Filter = Program.SkillChallengeFilter;
			if (saveFileDialog.ShowDialog() == DialogResult.OK && !Serialisation<SkillChallenge>.Save(saveFileDialog.FileName, this.fChallenge, SerialisationMode.Binary))
			{
				string text = "The skill challenge could not be exported.";
				MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
			ListViewGroup listViewGroup = new ListViewGroup("Standard Skill DCs", HorizontalAlignment.Left);
			ComponentResourceManager resources = new ComponentResourceManager(typeof(SkillChallengeBuilderForm));
			ListViewGroup listViewGroup2 = new ListViewGroup("Primary Skills", HorizontalAlignment.Left);
			ListViewGroup listViewGroup3 = new ListViewGroup("Secondary Skills", HorizontalAlignment.Left);
			ListViewGroup listViewGroup4 = new ListViewGroup("Automatic Failure", HorizontalAlignment.Left);
			ListViewGroup listViewGroup5 = new ListViewGroup("Skills", HorizontalAlignment.Left);
			ListViewGroup listViewGroup6 = new ListViewGroup("Abilities", HorizontalAlignment.Left);
			ListViewGroup listViewGroup7 = new ListViewGroup("Custom", HorizontalAlignment.Left);
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.Pages = new TabControl();
			this.OverviewPage = new TabPage();
			this.OverviewSplitter = new SplitContainer();
			this.LevelGroup = new GroupBox();
			this.XPLbl = new Label();
			this.XPInfoLbl = new Label();
			this.LevelBox = new NumericUpDown();
			this.LevelLbl = new Label();
			this.CompGroup = new GroupBox();
			this.LengthLbl = new Label();
			this.LengthInfoLbl = new Label();
			this.CompBox = new NumericUpDown();
			this.CompLbl = new Label();
			this.InfoList = new ListView();
			this.InfoHdr = new ColumnHeader();
			this.StdDCHdr = new ColumnHeader();
			this.Toolbar = new ToolStrip();
			this.FileMenu = new ToolStripDropDownButton();
			this.FileExport = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.SuccessCountLbl = new ToolStripLabel();
			this.FailureCountLbl = new ToolStripLabel();
			this.ResetProgressBtn = new ToolStripButton();
			this.SkillsPage = new TabPage();
			this.SkillSplitter = new SplitContainer();
			this.SkillList = new ListView();
			this.SkillHdr = new ColumnHeader();
			this.DCHdr = new ColumnHeader();
			this.SkillSourceList = new ListView();
			this.SkillSourceHdr = new ColumnHeader();
			this.AbilityHdr = new ColumnHeader();
			this.SkillsToolbar = new ToolStrip();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.BreakdownBtn = new ToolStripButton();
			this.InfoPage = new TabPage();
			this.InfoSplitter = new SplitContainer();
			this.VictoryBox = new DefaultTextBox();
			this.VictoryToolbar = new ToolStrip();
			this.toolStripLabel1 = new ToolStripLabel();
			this.DefeatBox = new DefaultTextBox();
			this.DefeatButton = new ToolStrip();
			this.toolStripLabel2 = new ToolStripLabel();
			this.NotesPage = new TabPage();
			this.NotesBox = new DefaultTextBox();
			this.NameBox = new TextBox();
			this.NameLbl = new Label();
			this.Pages.SuspendLayout();
			this.OverviewPage.SuspendLayout();
			this.OverviewSplitter.Panel1.SuspendLayout();
			this.OverviewSplitter.Panel2.SuspendLayout();
			this.OverviewSplitter.SuspendLayout();
			this.LevelGroup.SuspendLayout();
			((ISupportInitialize)this.LevelBox).BeginInit();
			this.CompGroup.SuspendLayout();
			((ISupportInitialize)this.CompBox).BeginInit();
			this.Toolbar.SuspendLayout();
			this.SkillsPage.SuspendLayout();
			this.SkillSplitter.Panel1.SuspendLayout();
			this.SkillSplitter.Panel2.SuspendLayout();
			this.SkillSplitter.SuspendLayout();
			this.SkillsToolbar.SuspendLayout();
			this.InfoPage.SuspendLayout();
			this.InfoSplitter.Panel1.SuspendLayout();
			this.InfoSplitter.Panel2.SuspendLayout();
			this.InfoSplitter.SuspendLayout();
			this.VictoryToolbar.SuspendLayout();
			this.DefeatButton.SuspendLayout();
			this.NotesPage.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(352, 359);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(433, 359);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.OverviewPage);
			this.Pages.Controls.Add(this.SkillsPage);
			this.Pages.Controls.Add(this.InfoPage);
			this.Pages.Controls.Add(this.NotesPage);
			this.Pages.Location = new Point(12, 38);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(496, 315);
			this.Pages.TabIndex = 2;
			this.OverviewPage.Controls.Add(this.OverviewSplitter);
			this.OverviewPage.Controls.Add(this.Toolbar);
			this.OverviewPage.Location = new Point(4, 22);
			this.OverviewPage.Name = "OverviewPage";
			this.OverviewPage.Padding = new Padding(3);
			this.OverviewPage.Size = new Size(488, 289);
			this.OverviewPage.TabIndex = 5;
			this.OverviewPage.Text = "Overview";
			this.OverviewPage.UseVisualStyleBackColor = true;
			this.OverviewSplitter.Dock = DockStyle.Fill;
			this.OverviewSplitter.Location = new Point(3, 28);
			this.OverviewSplitter.Name = "OverviewSplitter";
			this.OverviewSplitter.Panel1.Controls.Add(this.LevelGroup);
			this.OverviewSplitter.Panel1.Controls.Add(this.CompGroup);
			this.OverviewSplitter.Panel2.Controls.Add(this.InfoList);
			this.OverviewSplitter.Size = new Size(482, 258);
			this.OverviewSplitter.SplitterDistance = 237;
			this.OverviewSplitter.TabIndex = 0;
			this.LevelGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LevelGroup.Controls.Add(this.XPLbl);
			this.LevelGroup.Controls.Add(this.XPInfoLbl);
			this.LevelGroup.Controls.Add(this.LevelBox);
			this.LevelGroup.Controls.Add(this.LevelLbl);
			this.LevelGroup.Location = new Point(4, 87);
			this.LevelGroup.Name = "LevelGroup";
			this.LevelGroup.Size = new Size(230, 78);
			this.LevelGroup.TabIndex = 10;
			this.LevelGroup.TabStop = false;
			this.LevelGroup.Text = "Level";
			this.XPLbl.AutoSize = true;
			this.XPLbl.Location = new Point(69, 48);
			this.XPLbl.Name = "XPLbl";
			this.XPLbl.Size = new Size(24, 13);
			this.XPLbl.TabIndex = 10;
			this.XPLbl.Text = "[xp]";
			this.XPInfoLbl.AutoSize = true;
			this.XPInfoLbl.Location = new Point(6, 48);
			this.XPInfoLbl.Name = "XPInfoLbl";
			this.XPInfoLbl.Size = new Size(24, 13);
			this.XPInfoLbl.TabIndex = 9;
			this.XPInfoLbl.Text = "XP:";
			this.LevelBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LevelBox.Location = new Point(72, 19);
			NumericUpDown arg_891_0 = this.LevelBox;
			int[] array = new int[4];
			array[0] = 30;
			arg_891_0.Maximum = new decimal(array);
			NumericUpDown arg_8B0_0 = this.LevelBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_8B0_0.Minimum = new decimal(array2);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new Size(152, 20);
			this.LevelBox.TabIndex = 8;
			NumericUpDown arg_902_0 = this.LevelBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_902_0.Value = new decimal(array3);
			this.LevelBox.ValueChanged += new EventHandler(this.LevelBox_ValueChanged);
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(6, 21);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(36, 13);
			this.LevelLbl.TabIndex = 7;
			this.LevelLbl.Text = "Level:";
			this.CompGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.CompGroup.Controls.Add(this.LengthLbl);
			this.CompGroup.Controls.Add(this.LengthInfoLbl);
			this.CompGroup.Controls.Add(this.CompBox);
			this.CompGroup.Controls.Add(this.CompLbl);
			this.CompGroup.Location = new Point(3, 3);
			this.CompGroup.Name = "CompGroup";
			this.CompGroup.Size = new Size(231, 78);
			this.CompGroup.TabIndex = 9;
			this.CompGroup.TabStop = false;
			this.CompGroup.Text = "Complexity / Length";
			this.LengthLbl.AutoSize = true;
			this.LengthLbl.Location = new Point(69, 49);
			this.LengthLbl.Name = "LengthLbl";
			this.LengthLbl.Size = new Size(42, 13);
			this.LengthLbl.TabIndex = 5;
			this.LengthLbl.Text = "[length]";
			this.LengthInfoLbl.AutoSize = true;
			this.LengthInfoLbl.Location = new Point(6, 49);
			this.LengthInfoLbl.Name = "LengthInfoLbl";
			this.LengthInfoLbl.Size = new Size(43, 13);
			this.LengthInfoLbl.TabIndex = 4;
			this.LengthInfoLbl.Text = "Length:";
			this.CompBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.CompBox.Location = new Point(72, 19);
			NumericUpDown arg_B3F_0 = this.CompBox;
			int[] array4 = new int[4];
			array4[0] = 20;
			arg_B3F_0.Maximum = new decimal(array4);
			NumericUpDown arg_B5E_0 = this.CompBox;
			int[] array5 = new int[4];
			array5[0] = 1;
			arg_B5E_0.Minimum = new decimal(array5);
			this.CompBox.Name = "CompBox";
			this.CompBox.Size = new Size(153, 20);
			this.CompBox.TabIndex = 3;
			NumericUpDown arg_BB0_0 = this.CompBox;
			int[] array6 = new int[4];
			array6[0] = 1;
			arg_BB0_0.Value = new decimal(array6);
			this.CompBox.ValueChanged += new EventHandler(this.CompBox_ValueChanged);
			this.CompLbl.AutoSize = true;
			this.CompLbl.Location = new Point(6, 21);
			this.CompLbl.Name = "CompLbl";
			this.CompLbl.Size = new Size(60, 13);
			this.CompLbl.TabIndex = 2;
			this.CompLbl.Text = "Complexity:";
			this.InfoList.Columns.AddRange(new ColumnHeader[]
			{
				this.InfoHdr,
				this.StdDCHdr
			});
			this.InfoList.Dock = DockStyle.Fill;
			this.InfoList.Enabled = false;
			this.InfoList.FullRowSelect = true;
			listViewGroup.Header = "Standard Skill DCs";
			listViewGroup.Name = "listViewGroup1";
			this.InfoList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup
			});
			this.InfoList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.InfoList.HideSelection = false;
			this.InfoList.Location = new Point(0, 0);
			this.InfoList.MultiSelect = false;
			this.InfoList.Name = "InfoList";
			this.InfoList.Size = new Size(241, 258);
			this.InfoList.TabIndex = 0;
			this.InfoList.UseCompatibleStateImageBehavior = false;
			this.InfoList.View = View.Details;
			this.InfoHdr.Text = "Difficulty";
			this.InfoHdr.Width = 139;
			this.StdDCHdr.Text = "DC";
			this.StdDCHdr.TextAlign = HorizontalAlignment.Right;
			this.StdDCHdr.Width = 67;
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.FileMenu,
				this.toolStripSeparator2,
				this.SuccessCountLbl,
				this.FailureCountLbl,
				this.ResetProgressBtn
			});
			this.Toolbar.Location = new Point(3, 3);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(482, 25);
			this.Toolbar.TabIndex = 1;
			this.Toolbar.Text = "toolStrip1";
			this.FileMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FileMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.FileExport
			});
			this.FileMenu.Image = (Image)resources.GetObject("FileMenu.Image");
			this.FileMenu.ImageTransparentColor = Color.Magenta;
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new Size(38, 22);
			this.FileMenu.Text = "File";
			this.FileExport.Name = "FileExport";
			this.FileExport.Size = new Size(152, 22);
			this.FileExport.Text = "Export...";
			this.FileExport.Click += new EventHandler(this.FileExport_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 25);
			this.SuccessCountLbl.Name = "SuccessCountLbl";
			this.SuccessCountLbl.Size = new Size(66, 22);
			this.SuccessCountLbl.Text = "[successes]";
			this.FailureCountLbl.Name = "FailureCountLbl";
			this.FailureCountLbl.Size = new Size(53, 22);
			this.FailureCountLbl.Text = "[failures]";
			this.ResetProgressBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ResetProgressBtn.Image = (Image)resources.GetObject("ResetProgressBtn.Image");
			this.ResetProgressBtn.ImageTransparentColor = Color.Magenta;
			this.ResetProgressBtn.Name = "ResetProgressBtn";
			this.ResetProgressBtn.Size = new Size(39, 22);
			this.ResetProgressBtn.Text = "Reset";
			this.ResetProgressBtn.Click += new EventHandler(this.ResetProgressBtn_Click);
			this.SkillsPage.Controls.Add(this.SkillSplitter);
			this.SkillsPage.Controls.Add(this.SkillsToolbar);
			this.SkillsPage.Location = new Point(4, 22);
			this.SkillsPage.Name = "SkillsPage";
			this.SkillsPage.Padding = new Padding(3);
			this.SkillsPage.Size = new Size(488, 289);
			this.SkillsPage.TabIndex = 3;
			this.SkillsPage.Text = "Skills";
			this.SkillsPage.UseVisualStyleBackColor = true;
			this.SkillSplitter.Dock = DockStyle.Fill;
			this.SkillSplitter.Location = new Point(3, 28);
			this.SkillSplitter.Name = "SkillSplitter";
			this.SkillSplitter.Panel1.Controls.Add(this.SkillList);
			this.SkillSplitter.Panel2.Controls.Add(this.SkillSourceList);
			this.SkillSplitter.Size = new Size(482, 258);
			this.SkillSplitter.SplitterDistance = 283;
			this.SkillSplitter.TabIndex = 2;
			this.SkillList.AllowDrop = true;
			this.SkillList.Columns.AddRange(new ColumnHeader[]
			{
				this.SkillHdr,
				this.DCHdr
			});
			this.SkillList.Dock = DockStyle.Fill;
			this.SkillList.FullRowSelect = true;
			listViewGroup2.Header = "Primary Skills";
			listViewGroup2.Name = "listViewGroup1";
			listViewGroup3.Header = "Secondary Skills";
			listViewGroup3.Name = "listViewGroup2";
			listViewGroup4.Header = "Automatic Failure";
			listViewGroup4.Name = "listViewGroup3";
			this.SkillList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup2,
				listViewGroup3,
				listViewGroup4
			});
			this.SkillList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SkillList.HideSelection = false;
			this.SkillList.Location = new Point(0, 0);
			this.SkillList.MultiSelect = false;
			this.SkillList.Name = "SkillList";
			this.SkillList.Size = new Size(283, 258);
			this.SkillList.TabIndex = 1;
			this.SkillList.UseCompatibleStateImageBehavior = false;
			this.SkillList.View = View.Details;
			this.SkillList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.SkillList.DragOver += new DragEventHandler(this.SkillList_DragOver);
			this.SkillHdr.Text = "Skill";
			this.SkillHdr.Width = 135;
			this.DCHdr.Text = "DC Level";
			this.DCHdr.Width = 103;
			this.SkillSourceList.Columns.AddRange(new ColumnHeader[]
			{
				this.SkillSourceHdr,
				this.AbilityHdr
			});
			this.SkillSourceList.Dock = DockStyle.Fill;
			this.SkillSourceList.FullRowSelect = true;
			listViewGroup5.Header = "Skills";
			listViewGroup5.Name = "listViewGroup1";
			listViewGroup6.Header = "Abilities";
			listViewGroup6.Name = "listViewGroup2";
			listViewGroup7.Header = "Custom";
			listViewGroup7.Name = "listViewGroup3";
			this.SkillSourceList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup5,
				listViewGroup6,
				listViewGroup7
			});
			this.SkillSourceList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SkillSourceList.HideSelection = false;
			this.SkillSourceList.Location = new Point(0, 0);
			this.SkillSourceList.MultiSelect = false;
			this.SkillSourceList.Name = "SkillSourceList";
			this.SkillSourceList.Size = new Size(195, 258);
			this.SkillSourceList.TabIndex = 0;
			this.SkillSourceList.UseCompatibleStateImageBehavior = false;
			this.SkillSourceList.View = View.Details;
			this.SkillSourceList.DoubleClick += new EventHandler(this.SkillSourceList_DoubleClick);
			this.SkillSourceList.ItemDrag += new ItemDragEventHandler(this.SkillSourceList_ItemDrag);
			this.SkillSourceHdr.Text = "Skills";
			this.SkillSourceHdr.Width = 112;
			this.AbilityHdr.Text = "Ability";
			this.AbilityHdr.Width = 49;
			this.SkillsToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.RemoveBtn,
				this.EditBtn,
				this.toolStripSeparator1,
				this.BreakdownBtn
			});
			this.SkillsToolbar.Location = new Point(3, 3);
			this.SkillsToolbar.Name = "SkillsToolbar";
			this.SkillsToolbar.Size = new Size(482, 25);
			this.SkillsToolbar.TabIndex = 0;
			this.SkillsToolbar.Text = "toolStrip1";
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
			this.BreakdownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.BreakdownBtn.Image = (Image)resources.GetObject("BreakdownBtn.Image");
			this.BreakdownBtn.ImageTransparentColor = Color.Magenta;
			this.BreakdownBtn.Name = "BreakdownBtn";
			this.BreakdownBtn.Size = new Size(107, 22);
			this.BreakdownBtn.Text = "Ability Breakdown";
			this.BreakdownBtn.Click += new EventHandler(this.BreakdownBtn_Click);
			this.InfoPage.Controls.Add(this.InfoSplitter);
			this.InfoPage.Location = new Point(4, 22);
			this.InfoPage.Name = "InfoPage";
			this.InfoPage.Padding = new Padding(3);
			this.InfoPage.Size = new Size(488, 289);
			this.InfoPage.TabIndex = 4;
			this.InfoPage.Text = "Victory / Defeat Details";
			this.InfoPage.UseVisualStyleBackColor = true;
			this.InfoSplitter.Dock = DockStyle.Fill;
			this.InfoSplitter.Location = new Point(3, 3);
			this.InfoSplitter.Name = "InfoSplitter";
			this.InfoSplitter.Panel1.Controls.Add(this.VictoryBox);
			this.InfoSplitter.Panel1.Controls.Add(this.VictoryToolbar);
			this.InfoSplitter.Panel2.Controls.Add(this.DefeatBox);
			this.InfoSplitter.Panel2.Controls.Add(this.DefeatButton);
			this.InfoSplitter.Size = new Size(482, 283);
			this.InfoSplitter.SplitterDistance = 237;
			this.InfoSplitter.TabIndex = 0;
			this.VictoryBox.AcceptsReturn = true;
			this.VictoryBox.AcceptsTab = true;
			this.VictoryBox.DefaultText = "(enter victory information here)";
			this.VictoryBox.Dock = DockStyle.Fill;
			this.VictoryBox.Location = new Point(0, 25);
			this.VictoryBox.Multiline = true;
			this.VictoryBox.Name = "VictoryBox";
			this.VictoryBox.ScrollBars = ScrollBars.Vertical;
			this.VictoryBox.Size = new Size(237, 258);
			this.VictoryBox.TabIndex = 1;
			this.VictoryBox.Text = "(enter victory information here)";
			this.VictoryToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel1
			});
			this.VictoryToolbar.Location = new Point(0, 0);
			this.VictoryToolbar.Name = "VictoryToolbar";
			this.VictoryToolbar.Size = new Size(237, 25);
			this.VictoryToolbar.TabIndex = 0;
			this.VictoryToolbar.Text = "toolStrip1";
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new Size(47, 22);
			this.toolStripLabel1.Text = "Victory:";
			this.DefeatBox.AcceptsReturn = true;
			this.DefeatBox.AcceptsTab = true;
			this.DefeatBox.DefaultText = "(enter defeat information here)";
			this.DefeatBox.Dock = DockStyle.Fill;
			this.DefeatBox.Location = new Point(0, 25);
			this.DefeatBox.Multiline = true;
			this.DefeatBox.Name = "DefeatBox";
			this.DefeatBox.ScrollBars = ScrollBars.Vertical;
			this.DefeatBox.Size = new Size(241, 258);
			this.DefeatBox.TabIndex = 2;
			this.DefeatBox.Text = "(enter defeat information here)";
			this.DefeatButton.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel2
			});
			this.DefeatButton.Location = new Point(0, 0);
			this.DefeatButton.Name = "DefeatButton";
			this.DefeatButton.Size = new Size(241, 25);
			this.DefeatButton.TabIndex = 0;
			this.DefeatButton.Text = "toolStrip2";
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new Size(44, 22);
			this.toolStripLabel2.Text = "Defeat:";
			this.NotesPage.Controls.Add(this.NotesBox);
			this.NotesPage.Location = new Point(4, 22);
			this.NotesPage.Name = "NotesPage";
			this.NotesPage.Padding = new Padding(3);
			this.NotesPage.Size = new Size(488, 289);
			this.NotesPage.TabIndex = 6;
			this.NotesPage.Text = "Notes";
			this.NotesPage.UseVisualStyleBackColor = true;
			this.NotesBox.AcceptsReturn = true;
			this.NotesBox.AcceptsTab = true;
			this.NotesBox.DefaultText = "(enter details here)";
			this.NotesBox.Dock = DockStyle.Fill;
			this.NotesBox.Location = new Point(3, 3);
			this.NotesBox.Multiline = true;
			this.NotesBox.Name = "NotesBox";
			this.NotesBox.ScrollBars = ScrollBars.Vertical;
			this.NotesBox.Size = new Size(482, 283);
			this.NotesBox.TabIndex = 3;
			this.NotesBox.Text = "(enter details here)";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(56, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(452, 20);
			this.NameBox.TabIndex = 5;
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 4;
			this.NameLbl.Text = "Name:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(520, 394);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SkillChallengeBuilderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Skill Challenge Builder";
			this.Pages.ResumeLayout(false);
			this.OverviewPage.ResumeLayout(false);
			this.OverviewPage.PerformLayout();
			this.OverviewSplitter.Panel1.ResumeLayout(false);
			this.OverviewSplitter.Panel2.ResumeLayout(false);
			this.OverviewSplitter.ResumeLayout(false);
			this.LevelGroup.ResumeLayout(false);
			this.LevelGroup.PerformLayout();
			((ISupportInitialize)this.LevelBox).EndInit();
			this.CompGroup.ResumeLayout(false);
			this.CompGroup.PerformLayout();
			((ISupportInitialize)this.CompBox).EndInit();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.SkillsPage.ResumeLayout(false);
			this.SkillsPage.PerformLayout();
			this.SkillSplitter.Panel1.ResumeLayout(false);
			this.SkillSplitter.Panel2.ResumeLayout(false);
			this.SkillSplitter.ResumeLayout(false);
			this.SkillsToolbar.ResumeLayout(false);
			this.SkillsToolbar.PerformLayout();
			this.InfoPage.ResumeLayout(false);
			this.InfoSplitter.Panel1.ResumeLayout(false);
			this.InfoSplitter.Panel1.PerformLayout();
			this.InfoSplitter.Panel2.ResumeLayout(false);
			this.InfoSplitter.Panel2.PerformLayout();
			this.InfoSplitter.ResumeLayout(false);
			this.VictoryToolbar.ResumeLayout(false);
			this.VictoryToolbar.PerformLayout();
			this.DefeatButton.ResumeLayout(false);
			this.DefeatButton.PerformLayout();
			this.NotesPage.ResumeLayout(false);
			this.NotesPage.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
