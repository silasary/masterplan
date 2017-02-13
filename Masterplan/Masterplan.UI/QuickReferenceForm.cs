using Masterplan.Data;
using Masterplan.Extensibility;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class QuickReferenceForm : Form
	{
		private IContainer components;

		private Label LevelLbl;

		private NumericUpDown LevelBox;

		private GroupBox SkillGroup;

		private GroupBox DamageGroup;

		private ListView SkillList;

		private ColumnHeader DiffHdr;

		private ColumnHeader DCHdr;

		private ListView DamageList;

		private ColumnHeader TargetHdr;

		private ColumnHeader DmgHdr;

		private TabControl Pages;

		private TabPage ReferencePage;

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
			ListViewGroup listViewGroup = new ListViewGroup("Normal Damage", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Limited Damage", HorizontalAlignment.Left);
			ComponentResourceManager resources = new ComponentResourceManager(typeof(QuickReferenceForm));
			this.LevelLbl = new Label();
			this.LevelBox = new NumericUpDown();
			this.SkillGroup = new GroupBox();
			this.SkillList = new ListView();
			this.DiffHdr = new ColumnHeader();
			this.DCHdr = new ColumnHeader();
			this.DamageGroup = new GroupBox();
			this.DamageList = new ListView();
			this.TargetHdr = new ColumnHeader();
			this.DmgHdr = new ColumnHeader();
			this.Pages = new TabControl();
			this.ReferencePage = new TabPage();
			((ISupportInitialize)this.LevelBox).BeginInit();
			this.SkillGroup.SuspendLayout();
			this.DamageGroup.SuspendLayout();
			this.Pages.SuspendLayout();
			this.ReferencePage.SuspendLayout();
			base.SuspendLayout();
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(8, 8);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(36, 13);
			this.LevelLbl.TabIndex = 0;
			this.LevelLbl.Text = "Level:";
			this.LevelBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LevelBox.Location = new Point(50, 6);
			NumericUpDown arg_17F_0 = this.LevelBox;
			int[] array = new int[4];
			array[0] = 30;
			arg_17F_0.Maximum = new decimal(array);
			NumericUpDown arg_19E_0 = this.LevelBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_19E_0.Minimum = new decimal(array2);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new Size(291, 20);
			this.LevelBox.TabIndex = 1;
			NumericUpDown arg_1F0_0 = this.LevelBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_1F0_0.Value = new decimal(array3);
			this.LevelBox.ValueChanged += new EventHandler(this.LevelBox_ValueChanged);
			this.SkillGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SkillGroup.Controls.Add(this.SkillList);
			this.SkillGroup.Location = new Point(8, 32);
			this.SkillGroup.Name = "SkillGroup";
			this.SkillGroup.Size = new Size(333, 118);
			this.SkillGroup.TabIndex = 2;
			this.SkillGroup.TabStop = false;
			this.SkillGroup.Text = "Skill DCs";
			this.SkillList.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.SkillList.Columns.AddRange(new ColumnHeader[]
			{
				this.DiffHdr,
				this.DCHdr
			});
			this.SkillList.FullRowSelect = true;
			this.SkillList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SkillList.HideSelection = false;
			this.SkillList.Location = new Point(6, 19);
			this.SkillList.MultiSelect = false;
			this.SkillList.Name = "SkillList";
			this.SkillList.Size = new Size(321, 93);
			this.SkillList.TabIndex = 0;
			this.SkillList.UseCompatibleStateImageBehavior = false;
			this.SkillList.View = View.Details;
			this.DiffHdr.Text = "Difficulty";
			this.DiffHdr.Width = 200;
			this.DCHdr.Text = "DC";
			this.DCHdr.TextAlign = HorizontalAlignment.Right;
			this.DCHdr.Width = 80;
			this.DamageGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.DamageGroup.Controls.Add(this.DamageList);
			this.DamageGroup.Location = new Point(8, 156);
			this.DamageGroup.Name = "DamageGroup";
			this.DamageGroup.Size = new Size(333, 257);
			this.DamageGroup.TabIndex = 3;
			this.DamageGroup.TabStop = false;
			this.DamageGroup.Text = "Damage Expressions";
			this.DamageList.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.DamageList.Columns.AddRange(new ColumnHeader[]
			{
				this.TargetHdr,
				this.DmgHdr
			});
			this.DamageList.FullRowSelect = true;
			listViewGroup.Header = "Normal Damage";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup2.Header = "Limited Damage";
			listViewGroup2.Name = "listViewGroup2";
			this.DamageList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2
			});
			this.DamageList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.DamageList.HideSelection = false;
			this.DamageList.Location = new Point(6, 19);
			this.DamageList.MultiSelect = false;
			this.DamageList.Name = "DamageList";
			this.DamageList.Size = new Size(321, 232);
			this.DamageList.TabIndex = 1;
			this.DamageList.UseCompatibleStateImageBehavior = false;
			this.DamageList.View = View.Details;
			this.TargetHdr.Text = "Target";
			this.TargetHdr.Width = 200;
			this.DmgHdr.Text = "Damage";
			this.DmgHdr.TextAlign = HorizontalAlignment.Right;
			this.DmgHdr.Width = 80;
			this.Pages.Controls.Add(this.ReferencePage);
			this.Pages.Dock = DockStyle.Fill;
			this.Pages.Location = new Point(0, 0);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(357, 447);
			this.Pages.TabIndex = 4;
			this.ReferencePage.Controls.Add(this.LevelBox);
			this.ReferencePage.Controls.Add(this.DamageGroup);
			this.ReferencePage.Controls.Add(this.LevelLbl);
			this.ReferencePage.Controls.Add(this.SkillGroup);
			this.ReferencePage.Location = new Point(4, 22);
			this.ReferencePage.Name = "ReferencePage";
			this.ReferencePage.Padding = new Padding(3);
			this.ReferencePage.Size = new Size(349, 421);
			this.ReferencePage.TabIndex = 0;
			this.ReferencePage.Text = "Reference";
			this.ReferencePage.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(357, 447);
			base.Controls.Add(this.Pages);
			base.Icon = (Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "QuickReferenceForm";
			base.SizeGripStyle = SizeGripStyle.Hide;
			this.Text = "Quick Reference";
			base.FormClosed += new FormClosedEventHandler(this.QuickReferenceForm_FormClosed);
			((ISupportInitialize)this.LevelBox).EndInit();
			this.SkillGroup.ResumeLayout(false);
			this.DamageGroup.ResumeLayout(false);
			this.Pages.ResumeLayout(false);
			this.ReferencePage.ResumeLayout(false);
			this.ReferencePage.PerformLayout();
			base.ResumeLayout(false);
		}

		public QuickReferenceForm()
		{
			this.InitializeComponent();
			foreach (IAddIn current in Session.AddIns)
			{
				foreach (IPage current2 in current.QuickReferencePages)
				{
					TabPage tabPage = new TabPage();
					tabPage.Text = current2.Name;
					tabPage.Controls.Add(current2.Control);
					current2.Control.Dock = DockStyle.Fill;
					this.Pages.TabPages.Add(tabPage);
				}
			}
			this.UpdateView();
		}

		private void QuickReferenceForm_FormClosed(object sender, FormClosedEventArgs e)
		{
		}

		public void UpdateView()
		{
			if (Session.Project != null)
			{
				this.LevelBox.Value = Session.Project.Party.Level;
			}
			this.update_skills();
			foreach (IAddIn current in Session.AddIns)
			{
				foreach (IPage current2 in current.QuickReferencePages)
				{
					current2.UpdateView();
				}
			}
		}

		private void LevelBox_ValueChanged(object sender, EventArgs e)
		{
			this.update_skills();
		}

		private void update_skills()
		{
			int level = (int)this.LevelBox.Value;
			this.SkillList.BeginUpdate();
			this.SkillList.Items.Clear();
			ListViewItem listViewItem = this.SkillList.Items.Add("Easy");
			listViewItem.SubItems.Add(AI.GetSkillDC(Difficulty.Easy, level).ToString());
			ListViewItem listViewItem2 = this.SkillList.Items.Add("Moderate");
			listViewItem2.SubItems.Add(AI.GetSkillDC(Difficulty.Moderate, level).ToString());
			ListViewItem listViewItem3 = this.SkillList.Items.Add("Hard");
			listViewItem3.SubItems.Add(AI.GetSkillDC(Difficulty.Hard, level).ToString());
			this.SkillList.EndUpdate();
			this.DamageList.BeginUpdate();
			this.DamageList.Items.Clear();
			this.DamageList.ShowGroups = false;
			ListViewItem listViewItem4 = this.DamageList.Items.Add("Against a single target");
			listViewItem4.SubItems.Add(Statistics.NormalDamage(level));
			listViewItem4.Group = this.DamageList.Groups[0];
			ListViewItem listViewItem5 = this.DamageList.Items.Add("Against multiple targets");
			listViewItem5.SubItems.Add(Statistics.MultipleDamage(level));
			listViewItem5.Group = this.DamageList.Groups[0];
			this.DamageList.EndUpdate();
		}
	}
}
