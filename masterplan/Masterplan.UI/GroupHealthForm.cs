using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class GroupHealthForm : Form
	{
		private bool fUpdating;

		private Label fPlaceholder = new Label();

		private IContainer components;

		private Button CloseBtn;

		private ListView CombatantList;

		private ColumnHeader CombatantHdr;

		private ColumnHeader InitHdr;

		private Panel HPPanel;

		private Label HeroNameLbl;

		private NumericUpDown MaxHPBox;

		private Label MaxHPLbl;

		private HitPointGauge HPGauge;

		private NumericUpDown TempHPBox;

		private Label TempHPLbl;

		private NumericUpDown CurrentHPBox;

		private Label CurrentHPLbl;

		private Button FullHealBtn;

		public Hero SelectedHero
		{
			get
			{
				if (this.CombatantList.SelectedItems.Count != 0)
				{
					return this.CombatantList.SelectedItems[0].Tag as Hero;
				}
				return null;
			}
		}

		public GroupHealthForm()
		{
			this.InitializeComponent();
			this.fPlaceholder.Text = "Select a PC from the list to set its current HP";
			this.fPlaceholder.TextAlign = ContentAlignment.MiddleCenter;
			this.fPlaceholder.Dock = DockStyle.Fill;
			this.HPPanel.Controls.Add(this.fPlaceholder);
			this.fPlaceholder.BringToFront();
			this.update_list();
		}

		private void CombatantList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.fUpdating = true;
			this.update_hp_panel();
			this.fUpdating = false;
		}

		private void CombatantList_DoubleClick(object sender, EventArgs e)
		{
		}

		private void MaxHPBox_ValueChanged(object sender, EventArgs e)
		{
			if (this.fUpdating)
			{
				return;
			}
			this.SelectedHero.HP = (int)this.MaxHPBox.Value;
			Session.Modified = true;
			this.CurrentHPBox.Maximum = this.SelectedHero.HP;
			this.update_hp_panel();
			this.update_list_hp(this.SelectedHero);
		}

		private void CurrentHPBox_ValueChanged(object sender, EventArgs e)
		{
			if (this.fUpdating)
			{
				return;
			}
			int damage = this.SelectedHero.HP - (int)this.CurrentHPBox.Value;
			this.SelectedHero.CombatData.Damage = damage;
			Session.Modified = true;
			this.update_hp_panel();
			this.update_list_hp(this.SelectedHero);
		}

		private void TempHPBox_ValueChanged(object sender, EventArgs e)
		{
			if (this.fUpdating)
			{
				return;
			}
			this.SelectedHero.CombatData.TempHP = (int)this.TempHPBox.Value;
			Session.Modified = true;
			this.update_hp_panel();
			this.update_list_hp(this.SelectedHero);
		}

		private void FullHealBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedHero != null)
			{
				this.SelectedHero.CombatData.Damage = 0;
				Session.Modified = true;
				this.update_hp_panel();
				this.update_list_hp(this.SelectedHero);
			}
		}

		private void update_list()
		{
			this.CombatantList.Items.Clear();
			foreach (Hero current in Session.Project.Heroes)
			{
				if (current.HP != 0)
				{
					ListViewItem listViewItem = this.CombatantList.Items.Add(current.Name);
					listViewItem.SubItems.Add("");
					listViewItem.Tag = current;
				}
			}
			foreach (Hero current2 in Session.Project.Heroes)
			{
				this.update_list_hp(current2);
			}
		}

		private void update_hp_panel()
		{
			if (this.SelectedHero != null)
			{
				this.fPlaceholder.Visible = false;
				this.HeroNameLbl.Text = this.SelectedHero.Name;
				this.MaxHPBox.Value = this.SelectedHero.HP;
				this.CurrentHPBox.Value = this.SelectedHero.HP - this.SelectedHero.CombatData.Damage;
				this.TempHPBox.Value = this.SelectedHero.CombatData.TempHP;
				this.HPGauge.FullHP = this.SelectedHero.HP;
				this.HPGauge.Damage = this.SelectedHero.CombatData.Damage;
				this.HPGauge.TempHP = this.SelectedHero.CombatData.TempHP;
				this.FullHealBtn.Enabled = (this.SelectedHero.CombatData.Damage != 0);
				return;
			}
			this.fPlaceholder.Visible = true;
		}

		private void update_list_hp(Hero hero)
		{
			string text = hero.HP.ToString();
			if (hero.CombatData.Damage > 0)
			{
				int num = hero.HP - hero.CombatData.Damage;
				text = num + " / " + hero.HP;
			}
			if (hero.CombatData.TempHP > 0)
			{
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					" (+",
					hero.CombatData.TempHP,
					")"
				});
			}
			ListViewItem listViewItem = null;
			foreach (ListViewItem listViewItem2 in this.CombatantList.Items)
			{
				if (listViewItem2.Tag == hero)
				{
					listViewItem = listViewItem2;
					break;
				}
			}
			if (listViewItem != null)
			{
				listViewItem.SubItems[1].Text = text;
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
			this.CloseBtn = new Button();
			this.CombatantList = new ListView();
			this.CombatantHdr = new ColumnHeader();
			this.InitHdr = new ColumnHeader();
			this.HPPanel = new Panel();
			this.FullHealBtn = new Button();
			this.HeroNameLbl = new Label();
			this.MaxHPBox = new NumericUpDown();
			this.MaxHPLbl = new Label();
			this.HPGauge = new HitPointGauge();
			this.TempHPBox = new NumericUpDown();
			this.TempHPLbl = new Label();
			this.CurrentHPBox = new NumericUpDown();
			this.CurrentHPLbl = new Label();
			this.HPPanel.SuspendLayout();
			((ISupportInitialize)this.MaxHPBox).BeginInit();
			((ISupportInitialize)this.TempHPBox).BeginInit();
			((ISupportInitialize)this.CurrentHPBox).BeginInit();
			base.SuspendLayout();
			this.CloseBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CloseBtn.DialogResult = DialogResult.OK;
			this.CloseBtn.Location = new Point(378, 220);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new Size(75, 23);
			this.CloseBtn.TabIndex = 2;
			this.CloseBtn.Text = "OK";
			this.CloseBtn.UseVisualStyleBackColor = true;
			this.CombatantList.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.CombatantList.Columns.AddRange(new ColumnHeader[]
			{
				this.CombatantHdr,
				this.InitHdr
			});
			this.CombatantList.FullRowSelect = true;
			this.CombatantList.HideSelection = false;
			this.CombatantList.Location = new Point(12, 12);
			this.CombatantList.MultiSelect = false;
			this.CombatantList.Name = "CombatantList";
			this.CombatantList.Size = new Size(238, 202);
			this.CombatantList.TabIndex = 1;
			this.CombatantList.UseCompatibleStateImageBehavior = false;
			this.CombatantList.View = View.Details;
			this.CombatantList.SelectedIndexChanged += new EventHandler(this.CombatantList_SelectedIndexChanged);
			this.CombatantList.DoubleClick += new EventHandler(this.CombatantList_DoubleClick);
			this.CombatantHdr.Text = "PC";
			this.CombatantHdr.Width = 131;
			this.InitHdr.Text = "Hit Points";
			this.InitHdr.TextAlign = HorizontalAlignment.Right;
			this.InitHdr.Width = 76;
			this.HPPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
			this.HPPanel.BorderStyle = BorderStyle.FixedSingle;
			this.HPPanel.Controls.Add(this.FullHealBtn);
			this.HPPanel.Controls.Add(this.HeroNameLbl);
			this.HPPanel.Controls.Add(this.MaxHPBox);
			this.HPPanel.Controls.Add(this.MaxHPLbl);
			this.HPPanel.Controls.Add(this.HPGauge);
			this.HPPanel.Controls.Add(this.TempHPBox);
			this.HPPanel.Controls.Add(this.TempHPLbl);
			this.HPPanel.Controls.Add(this.CurrentHPBox);
			this.HPPanel.Controls.Add(this.CurrentHPLbl);
			this.HPPanel.Location = new Point(256, 12);
			this.HPPanel.Name = "HPPanel";
			this.HPPanel.Size = new Size(197, 202);
			this.HPPanel.TabIndex = 3;
			this.FullHealBtn.Location = new Point(6, 135);
			this.FullHealBtn.Name = "FullHealBtn";
			this.FullHealBtn.Size = new Size(148, 23);
			this.FullHealBtn.TabIndex = 16;
			this.FullHealBtn.Text = "Full Heal";
			this.FullHealBtn.UseVisualStyleBackColor = true;
			this.FullHealBtn.Click += new EventHandler(this.FullHealBtn_Click);
			this.HeroNameLbl.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.HeroNameLbl.Location = new Point(3, 3);
			this.HeroNameLbl.Name = "HeroNameLbl";
			this.HeroNameLbl.Size = new Size(151, 40);
			this.HeroNameLbl.TabIndex = 15;
			this.HeroNameLbl.Text = "[hero]";
			this.HeroNameLbl.TextAlign = ContentAlignment.MiddleCenter;
			this.MaxHPBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.MaxHPBox.Location = new Point(76, 57);
			NumericUpDown arg_4E4_0 = this.MaxHPBox;
			int[] array = new int[4];
			array[0] = 1000;
			arg_4E4_0.Maximum = new decimal(array);
			NumericUpDown arg_500_0 = this.MaxHPBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_500_0.Minimum = new decimal(array2);
			this.MaxHPBox.Name = "MaxHPBox";
			this.MaxHPBox.Size = new Size(78, 20);
			this.MaxHPBox.TabIndex = 9;
			NumericUpDown arg_54D_0 = this.MaxHPBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_54D_0.Value = new decimal(array3);
			this.MaxHPBox.ValueChanged += new EventHandler(this.MaxHPBox_ValueChanged);
			this.MaxHPLbl.AutoSize = true;
			this.MaxHPLbl.Location = new Point(3, 59);
			this.MaxHPLbl.Name = "MaxHPLbl";
			this.MaxHPLbl.Size = new Size(48, 13);
			this.MaxHPLbl.TabIndex = 8;
			this.MaxHPLbl.Text = "Max HP:";
			this.HPGauge.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
			this.HPGauge.Damage = 0;
			this.HPGauge.FullHP = 0;
			this.HPGauge.Location = new Point(160, 3);
			this.HPGauge.Name = "HPGauge";
			this.HPGauge.Size = new Size(32, 194);
			this.HPGauge.TabIndex = 14;
			this.HPGauge.TempHP = 0;
			this.TempHPBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TempHPBox.Location = new Point(76, 109);
			NumericUpDown arg_682_0 = this.TempHPBox;
			int[] array4 = new int[4];
			array4[0] = 1000;
			arg_682_0.Maximum = new decimal(array4);
			this.TempHPBox.Name = "TempHPBox";
			this.TempHPBox.Size = new Size(78, 20);
			this.TempHPBox.TabIndex = 13;
			this.TempHPBox.ValueChanged += new EventHandler(this.TempHPBox_ValueChanged);
			this.TempHPLbl.AutoSize = true;
			this.TempHPLbl.Location = new Point(3, 111);
			this.TempHPLbl.Name = "TempHPLbl";
			this.TempHPLbl.Size = new Size(55, 13);
			this.TempHPLbl.TabIndex = 12;
			this.TempHPLbl.Text = "Temp HP:";
			this.CurrentHPBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.CurrentHPBox.Location = new Point(76, 83);
			NumericUpDown arg_76E_0 = this.CurrentHPBox;
			int[] array5 = new int[4];
			array5[0] = 1000;
			arg_76E_0.Maximum = new decimal(array5);
			this.CurrentHPBox.Minimum = new decimal(new int[]
			{
				1000,
				0,
				0,
				int.MinValue
			});
			this.CurrentHPBox.Name = "CurrentHPBox";
			this.CurrentHPBox.Size = new Size(78, 20);
			this.CurrentHPBox.TabIndex = 11;
			this.CurrentHPBox.ValueChanged += new EventHandler(this.CurrentHPBox_ValueChanged);
			this.CurrentHPLbl.AutoSize = true;
			this.CurrentHPLbl.Location = new Point(3, 85);
			this.CurrentHPLbl.Name = "CurrentHPLbl";
			this.CurrentHPLbl.Size = new Size(62, 13);
			this.CurrentHPLbl.TabIndex = 10;
			this.CurrentHPLbl.Text = "Current HP:";
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(465, 255);
			base.Controls.Add(this.HPPanel);
			base.Controls.Add(this.CombatantList);
			base.Controls.Add(this.CloseBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "GroupHealthForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "PC Hit Points";
			this.HPPanel.ResumeLayout(false);
			this.HPPanel.PerformLayout();
			((ISupportInitialize)this.MaxHPBox).EndInit();
			((ISupportInitialize)this.TempHPBox).EndInit();
			((ISupportInitialize)this.CurrentHPBox).EndInit();
			base.ResumeLayout(false);
		}
	}
}
