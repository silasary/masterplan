using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class InfoPanel : UserControl
	{
		private IContainer components;

		private NumericUpDown LevelBox;

		private Label LevelLbl;

		private ListView SkillList;

		private ColumnHeader DiffHdr;

		private ColumnHeader DCHdr;

		public int Level
		{
			get
			{
				return (int)this.LevelBox.Value;
			}
			set
			{
				this.LevelBox.Value = value;
			}
		}

		public DiceExpression SelectedDamageExpression
		{
			get
			{
				if (this.SkillList.SelectedItems.Count != 0)
				{
					return this.SkillList.SelectedItems[0].Tag as DiceExpression;
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
			ListViewGroup listViewGroup = new ListViewGroup("Skill DCs", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Aid Another", HorizontalAlignment.Left);
			ListViewGroup listViewGroup3 = new ListViewGroup("Damage Expressions", HorizontalAlignment.Left);
			ListViewGroup listViewGroup4 = new ListViewGroup("Monster Knowledge", HorizontalAlignment.Left);
			this.LevelBox = new NumericUpDown();
			this.LevelLbl = new Label();
			this.SkillList = new ListView();
			this.DiffHdr = new ColumnHeader();
			this.DCHdr = new ColumnHeader();
			((ISupportInitialize)this.LevelBox).BeginInit();
			base.SuspendLayout();
			this.LevelBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LevelBox.Location = new Point(45, 3);
			NumericUpDown arg_B3_0 = this.LevelBox;
			int[] array = new int[4];
			array[0] = 30;
			arg_B3_0.Maximum = new decimal(array);
			NumericUpDown arg_D2_0 = this.LevelBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_D2_0.Minimum = new decimal(array2);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new Size(214, 20);
			this.LevelBox.TabIndex = 10;
			NumericUpDown arg_125_0 = this.LevelBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_125_0.Value = new decimal(array3);
			this.LevelBox.ValueChanged += new EventHandler(this.LevelBox_ValueChanged);
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(3, 5);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(36, 13);
			this.LevelLbl.TabIndex = 9;
			this.LevelLbl.Text = "Level:";
			this.SkillList.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.SkillList.Columns.AddRange(new ColumnHeader[]
			{
				this.DiffHdr,
				this.DCHdr
			});
			this.SkillList.FullRowSelect = true;
			listViewGroup.Header = "Skill DCs";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup2.Header = "Aid Another";
			listViewGroup2.Name = "listViewGroup2";
			listViewGroup3.Header = "Damage Expressions";
			listViewGroup3.Name = "listViewGroup3";
			listViewGroup4.Header = "Monster Knowledge";
			listViewGroup4.Name = "listViewGroup4";
			this.SkillList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2,
				listViewGroup3,
				listViewGroup4
			});
			this.SkillList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SkillList.HideSelection = false;
			this.SkillList.Location = new Point(3, 29);
			this.SkillList.MultiSelect = false;
			this.SkillList.Name = "SkillList";
			this.SkillList.Size = new Size(256, 252);
			this.SkillList.TabIndex = 0;
			this.SkillList.UseCompatibleStateImageBehavior = false;
			this.SkillList.View = View.Details;
			this.SkillList.DoubleClick += new EventHandler(this.DamageList_DoubleClick);
			this.DiffHdr.Text = "Information";
			this.DiffHdr.Width = 135;
			this.DCHdr.Text = "Value";
			this.DCHdr.TextAlign = HorizontalAlignment.Right;
			this.DCHdr.Width = 94;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.SkillList);
			base.Controls.Add(this.LevelBox);
			base.Controls.Add(this.LevelLbl);
			base.Name = "InfoPanel";
			base.Size = new Size(262, 284);
			((ISupportInitialize)this.LevelBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public InfoPanel()
		{
			this.InitializeComponent();
			this.update_list();
		}

		private void LevelBox_ValueChanged(object sender, EventArgs e)
		{
			this.update_list();
		}

		private void update_list()
		{
			int num = (int)this.LevelBox.Value;
			int num2 = 10 + num / 2;
			string text = Statistics.NormalDamage(num);
			string text2 = Statistics.MultipleDamage(num);
			string text3 = Statistics.MinionDamage(num).ToString();
			this.SkillList.BeginUpdate();
			this.SkillList.Items.Clear();
			ListViewItem listViewItem = this.SkillList.Items.Add("Easy");
			listViewItem.SubItems.Add("DC " + AI.GetSkillDC(Difficulty.Easy, num));
			listViewItem.Group = this.SkillList.Groups[0];
			ListViewItem listViewItem2 = this.SkillList.Items.Add("Moderate");
			listViewItem2.SubItems.Add("DC " + AI.GetSkillDC(Difficulty.Moderate, num));
			listViewItem2.Group = this.SkillList.Groups[0];
			ListViewItem listViewItem3 = this.SkillList.Items.Add("Hard");
			listViewItem3.SubItems.Add("DC " + AI.GetSkillDC(Difficulty.Hard, num));
			listViewItem3.Group = this.SkillList.Groups[0];
			ListViewItem listViewItem4 = this.SkillList.Items.Add("Aid Another");
			listViewItem4.SubItems.Add("DC " + num2);
			listViewItem4.Group = this.SkillList.Groups[1];
			ListViewItem listViewItem5 = this.SkillList.Items.Add("Against a single target");
			listViewItem5.SubItems.Add(text);
			listViewItem5.Tag = DiceExpression.Parse(text);
			listViewItem5.Group = this.SkillList.Groups[2];
			ListViewItem listViewItem6 = this.SkillList.Items.Add("Against multiple targets");
			listViewItem6.SubItems.Add(text2);
			listViewItem6.Tag = DiceExpression.Parse(text2);
			listViewItem6.Group = this.SkillList.Groups[2];
			ListViewItem listViewItem7 = this.SkillList.Items.Add("From a minion");
			listViewItem7.SubItems.Add(text3);
			listViewItem7.Tag = DiceExpression.Parse(text3);
			listViewItem7.Group = this.SkillList.Groups[2];
			ListViewItem listViewItem8 = this.SkillList.Items.Add("Aberrant");
			listViewItem8.SubItems.Add("Dungeoneering");
			listViewItem8.Group = this.SkillList.Groups[3];
			ListViewItem listViewItem9 = this.SkillList.Items.Add("Elemental");
			listViewItem9.SubItems.Add("Arcana");
			listViewItem9.Group = this.SkillList.Groups[3];
			ListViewItem listViewItem10 = this.SkillList.Items.Add("Fey");
			listViewItem10.SubItems.Add("Arcana");
			listViewItem10.Group = this.SkillList.Groups[3];
			ListViewItem listViewItem11 = this.SkillList.Items.Add("Immortal");
			listViewItem11.SubItems.Add("Religion");
			listViewItem11.Group = this.SkillList.Groups[3];
			ListViewItem listViewItem12 = this.SkillList.Items.Add("Natural");
			listViewItem12.SubItems.Add("Nature");
			listViewItem12.Group = this.SkillList.Groups[3];
			ListViewItem listViewItem13 = this.SkillList.Items.Add("Shadow");
			listViewItem13.SubItems.Add("Arcana");
			listViewItem13.Group = this.SkillList.Groups[3];
			ListViewItem listViewItem14 = this.SkillList.Items.Add("Undead keyword");
			listViewItem14.SubItems.Add("Religion");
			listViewItem14.Group = this.SkillList.Groups[3];
			this.SkillList.EndUpdate();
		}

		private void DamageList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedDamageExpression != null)
			{
				new DieRollerForm
				{
					Expression = this.SelectedDamageExpression
				}.ShowDialog();
			}
		}
	}
}
