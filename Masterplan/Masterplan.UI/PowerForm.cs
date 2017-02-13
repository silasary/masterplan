using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class PowerForm : Form
	{
		private CreaturePower fPower;

		private int fLevel;

		private IRole fRole;

		private bool fFunctionalTemplate;

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label NameLbl;

		private TextBox NameBox;

		private Label KeywordLbl;

		private TextBox KeywordBox;

		private Label ActionLbl;

		private Button ActionBtn;

		private LinkLabel ActionClearLbl;

		private Label AttackLbl;

		private Button AttackBtn;

		private LinkLabel AttackClearLbl;

		private Label RangeLbl;

		private ComboBox RangeBox;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TextBox DetailsBox;

		private TabPage AdvicePage;

		private ListView AdviceList;

		private ColumnHeader AdviceHdr;

		private ColumnHeader InfoHdr;

		private TextBox ConditionBox;

		private Label ConditionLbl;

		private TabPage DescPage;

		private TextBox DescBox;

		public CreaturePower Power
		{
			get
			{
				return this.fPower;
			}
		}

		public PowerForm(CreaturePower power, bool functional_template, bool unused)
		{
			this.InitializeComponent();
			this.Pages.TabPages.Remove(this.AdvicePage);
			this.RangeBox.Items.Add("Melee");
			this.RangeBox.Items.Add("Melee Touch");
			this.RangeBox.Items.Add("Melee Weapon");
			this.RangeBox.Items.Add("Melee N");
			this.RangeBox.Items.Add("Reach N");
			this.RangeBox.Items.Add("Ranged N");
			this.RangeBox.Items.Add("Close Blast N");
			this.RangeBox.Items.Add("Close Burst N");
			this.RangeBox.Items.Add("Area Burst N within N");
			this.RangeBox.Items.Add("Personal");
			this.fPower = power.Copy();
			this.fFunctionalTemplate = functional_template;
			this.NameBox.Text = this.fPower.Name;
			this.KeywordBox.Text = this.fPower.Keywords;
			this.ConditionBox.Text = this.fPower.Condition;
			this.update_action();
			this.update_attack();
			this.RangeBox.Text = this.fPower.Range;
			this.DetailsBox.Text = this.fPower.Details;
			this.DescBox.Text = this.fPower.Description;
		}

		public void ShowAdvicePage(int level, IRole role)
		{
			this.fLevel = level;
			this.fRole = role;
			this.Pages.TabPages.Add(this.AdvicePage);
			this.update_advice();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fPower.Name = this.NameBox.Text;
			this.fPower.Keywords = this.KeywordBox.Text;
			this.fPower.Condition = this.ConditionBox.Text;
			this.fPower.Range = this.RangeBox.Text;
			this.fPower.Details = this.DetailsBox.Text;
			this.fPower.Description = this.DescBox.Text;
		}

		private void ActionBtn_Click(object sender, EventArgs e)
		{
			PowerAction powerAction = this.fPower.Action;
			if (powerAction == null)
			{
				powerAction = new PowerAction();
			}
			PowerActionForm powerActionForm = new PowerActionForm(powerAction);
			if (powerActionForm.ShowDialog() == DialogResult.OK)
			{
				this.fPower.Action = powerActionForm.Action;
				this.update_action();
				this.update_advice();
			}
		}

		private void AttackBtn_Click(object sender, EventArgs e)
		{
			PowerAttack powerAttack = this.fPower.Attack;
			if (powerAttack == null)
			{
				powerAttack = new PowerAttack();
			}
			PowerAttackForm powerAttackForm = new PowerAttackForm(powerAttack, this.fFunctionalTemplate, 0, null);
			if (powerAttackForm.ShowDialog() == DialogResult.OK)
			{
				this.fPower.Attack = powerAttackForm.Attack;
				this.update_attack();
				this.update_advice();
			}
		}

		private void ActionClearLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.fPower.Action = null;
			this.update_action();
			this.update_advice();
		}

		private void AttackClearLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.fPower.Attack = null;
			this.update_attack();
			this.update_advice();
		}

		private void update_action()
		{
			this.ActionBtn.Text = ((this.fPower.Action != null) ? this.fPower.Action.ToString() : "(not defined)");
			this.ActionClearLbl.Enabled = (this.fPower.Action != null);
		}

		private void update_attack()
		{
			this.AttackBtn.Text = ((this.fPower.Attack != null) ? this.fPower.Attack.ToString() : "(not defined)");
			this.AttackClearLbl.Enabled = (this.fPower.Attack != null);
		}

		private void update_advice()
		{
			if (!this.Pages.TabPages.Contains(this.AdvicePage))
			{
				return;
			}
			this.AdviceList.Items.Clear();
			if (this.fPower.Attack != null && this.fPower.Action != null)
			{
				this.AdviceList.ShowGroups = true;
				string str = (this.fPower.Attack.Defence == DefenceType.AC) ? "AC" : "non-AC defence";
				ListViewItem listViewItem = new ListViewItem("Attack vs " + str + ": ");
				listViewItem.SubItems.Add("+" + Statistics.AttackBonus(this.fPower.Attack.Defence, this.fLevel, this.fRole));
				listViewItem.Group = this.AdviceList.Groups[0];
				this.AdviceList.Items.Add(listViewItem);
				if (this.fRole is ComplexRole)
				{
					ListViewItem listViewItem2 = new ListViewItem("Damage:");
					listViewItem2.SubItems.Add(Statistics.NormalDamage(this.fLevel));
					listViewItem2.Group = this.AdviceList.Groups[1];
					this.AdviceList.Items.Add(listViewItem2);
				}
				else if (this.fRole is Minion)
				{
					ListViewItem listViewItem3 = new ListViewItem("Minion damage:");
					listViewItem3.SubItems.Add(Statistics.MinionDamage(this.fLevel).ToString());
					listViewItem3.Group = this.AdviceList.Groups[1];
					this.AdviceList.Items.Add(listViewItem3);
				}
			}
			if (this.AdviceList.Items.Count == 0)
			{
				this.AdviceList.ShowGroups = false;
				ListViewItem listViewItem4 = new ListViewItem("(no advice)");
				listViewItem4.ForeColor = SystemColors.GrayText;
				this.AdviceList.Items.Add(listViewItem4);
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
			ListViewGroup listViewGroup = new ListViewGroup("Attack Advice", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Damage Advice", HorizontalAlignment.Left);
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.KeywordLbl = new Label();
			this.KeywordBox = new TextBox();
			this.ActionLbl = new Label();
			this.ActionBtn = new Button();
			this.ActionClearLbl = new LinkLabel();
			this.AttackLbl = new Label();
			this.AttackBtn = new Button();
			this.AttackClearLbl = new LinkLabel();
			this.RangeLbl = new Label();
			this.RangeBox = new ComboBox();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.DetailsBox = new TextBox();
			this.AdvicePage = new TabPage();
			this.AdviceList = new ListView();
			this.AdviceHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.ConditionBox = new TextBox();
			this.ConditionLbl = new Label();
			this.DescPage = new TabPage();
			this.DescBox = new TextBox();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.AdvicePage.SuspendLayout();
			this.DescPage.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(192, 326);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 15;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(273, 326);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 16;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(94, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(254, 20);
			this.NameBox.TabIndex = 1;
			this.KeywordLbl.AutoSize = true;
			this.KeywordLbl.Location = new Point(12, 41);
			this.KeywordLbl.Name = "KeywordLbl";
			this.KeywordLbl.Size = new Size(56, 13);
			this.KeywordLbl.TabIndex = 2;
			this.KeywordLbl.Text = "Keywords:";
			this.KeywordBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.KeywordBox.Location = new Point(94, 38);
			this.KeywordBox.Name = "KeywordBox";
			this.KeywordBox.Size = new Size(254, 20);
			this.KeywordBox.TabIndex = 3;
			this.ActionLbl.AutoSize = true;
			this.ActionLbl.Location = new Point(12, 95);
			this.ActionLbl.Name = "ActionLbl";
			this.ActionLbl.Size = new Size(40, 13);
			this.ActionLbl.TabIndex = 6;
			this.ActionLbl.Text = "Action:";
			this.ActionBtn.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ActionBtn.Location = new Point(94, 90);
			this.ActionBtn.Name = "ActionBtn";
			this.ActionBtn.Size = new Size(217, 23);
			this.ActionBtn.TabIndex = 7;
			this.ActionBtn.Text = "[action]";
			this.ActionBtn.UseVisualStyleBackColor = true;
			this.ActionBtn.Click += new EventHandler(this.ActionBtn_Click);
			this.ActionClearLbl.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.ActionClearLbl.AutoSize = true;
			this.ActionClearLbl.Location = new Point(317, 95);
			this.ActionClearLbl.Name = "ActionClearLbl";
			this.ActionClearLbl.Size = new Size(31, 13);
			this.ActionClearLbl.TabIndex = 8;
			this.ActionClearLbl.TabStop = true;
			this.ActionClearLbl.Text = "Clear";
			this.ActionClearLbl.LinkClicked += new LinkLabelLinkClickedEventHandler(this.ActionClearLbl_LinkClicked);
			this.AttackLbl.AutoSize = true;
			this.AttackLbl.Location = new Point(12, 124);
			this.AttackLbl.Name = "AttackLbl";
			this.AttackLbl.Size = new Size(41, 13);
			this.AttackLbl.TabIndex = 9;
			this.AttackLbl.Text = "Attack:";
			this.AttackBtn.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.AttackBtn.Location = new Point(94, 119);
			this.AttackBtn.Name = "AttackBtn";
			this.AttackBtn.Size = new Size(217, 23);
			this.AttackBtn.TabIndex = 10;
			this.AttackBtn.Text = "[attack]";
			this.AttackBtn.UseVisualStyleBackColor = true;
			this.AttackBtn.Click += new EventHandler(this.AttackBtn_Click);
			this.AttackClearLbl.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.AttackClearLbl.AutoSize = true;
			this.AttackClearLbl.Location = new Point(317, 124);
			this.AttackClearLbl.Name = "AttackClearLbl";
			this.AttackClearLbl.Size = new Size(31, 13);
			this.AttackClearLbl.TabIndex = 11;
			this.AttackClearLbl.TabStop = true;
			this.AttackClearLbl.Text = "Clear";
			this.AttackClearLbl.LinkClicked += new LinkLabelLinkClickedEventHandler(this.AttackClearLbl_LinkClicked);
			this.RangeLbl.AutoSize = true;
			this.RangeLbl.Location = new Point(12, 151);
			this.RangeLbl.Name = "RangeLbl";
			this.RangeLbl.Size = new Size(42, 13);
			this.RangeLbl.TabIndex = 12;
			this.RangeLbl.Text = "Range:";
			this.RangeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RangeBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.RangeBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.RangeBox.FormattingEnabled = true;
			this.RangeBox.Location = new Point(94, 148);
			this.RangeBox.Name = "RangeBox";
			this.RangeBox.Size = new Size(254, 21);
			this.RangeBox.Sorted = true;
			this.RangeBox.TabIndex = 13;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Controls.Add(this.DescPage);
			this.Pages.Controls.Add(this.AdvicePage);
			this.Pages.Location = new Point(12, 175);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(336, 145);
			this.Pages.TabIndex = 14;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new Padding(3);
			this.DetailsPage.Size = new Size(328, 119);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.Dock = DockStyle.Fill;
			this.DetailsBox.Location = new Point(3, 3);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new Size(322, 113);
			this.DetailsBox.TabIndex = 0;
			this.AdvicePage.Controls.Add(this.AdviceList);
			this.AdvicePage.Location = new Point(4, 22);
			this.AdvicePage.Name = "AdvicePage";
			this.AdvicePage.Padding = new Padding(3);
			this.AdvicePage.Size = new Size(328, 119);
			this.AdvicePage.TabIndex = 1;
			this.AdvicePage.Text = "Advice";
			this.AdvicePage.UseVisualStyleBackColor = true;
			this.AdviceList.Columns.AddRange(new ColumnHeader[]
			{
				this.AdviceHdr,
				this.InfoHdr
			});
			this.AdviceList.Dock = DockStyle.Fill;
			this.AdviceList.FullRowSelect = true;
			listViewGroup.Header = "Attack Advice";
			listViewGroup.Name = "AtkGrp";
			listViewGroup2.Header = "Damage Advice";
			listViewGroup2.Name = "DmgGrp";
			this.AdviceList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2
			});
			this.AdviceList.HeaderStyle = ColumnHeaderStyle.None;
			this.AdviceList.HideSelection = false;
			this.AdviceList.Location = new Point(3, 3);
			this.AdviceList.MultiSelect = false;
			this.AdviceList.Name = "AdviceList";
			this.AdviceList.Size = new Size(322, 113);
			this.AdviceList.TabIndex = 0;
			this.AdviceList.UseCompatibleStateImageBehavior = false;
			this.AdviceList.View = View.Details;
			this.AdviceHdr.Text = "Advice";
			this.AdviceHdr.Width = 150;
			this.InfoHdr.Text = "Information";
			this.InfoHdr.Width = 100;
			this.ConditionBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ConditionBox.Location = new Point(94, 64);
			this.ConditionBox.Name = "ConditionBox";
			this.ConditionBox.Size = new Size(254, 20);
			this.ConditionBox.TabIndex = 5;
			this.ConditionLbl.AutoSize = true;
			this.ConditionLbl.Location = new Point(12, 67);
			this.ConditionLbl.Name = "ConditionLbl";
			this.ConditionLbl.Size = new Size(59, 13);
			this.ConditionLbl.TabIndex = 4;
			this.ConditionLbl.Text = "Conditions:";
			this.DescPage.Controls.Add(this.DescBox);
			this.DescPage.Location = new Point(4, 22);
			this.DescPage.Name = "DescPage";
			this.DescPage.Padding = new Padding(3);
			this.DescPage.Size = new Size(328, 119);
			this.DescPage.TabIndex = 2;
			this.DescPage.Text = "Description";
			this.DescPage.UseVisualStyleBackColor = true;
			this.DescBox.AcceptsReturn = true;
			this.DescBox.Dock = DockStyle.Fill;
			this.DescBox.Location = new Point(3, 3);
			this.DescBox.Multiline = true;
			this.DescBox.Name = "DescBox";
			this.DescBox.ScrollBars = ScrollBars.Vertical;
			this.DescBox.Size = new Size(322, 113);
			this.DescBox.TabIndex = 1;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(360, 361);
			base.Controls.Add(this.ConditionBox);
			base.Controls.Add(this.ConditionLbl);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.RangeBox);
			base.Controls.Add(this.RangeLbl);
			base.Controls.Add(this.AttackClearLbl);
			base.Controls.Add(this.AttackBtn);
			base.Controls.Add(this.AttackLbl);
			base.Controls.Add(this.ActionClearLbl);
			base.Controls.Add(this.ActionBtn);
			base.Controls.Add(this.ActionLbl);
			base.Controls.Add(this.KeywordBox);
			base.Controls.Add(this.KeywordLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PowerForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Power";
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.AdvicePage.ResumeLayout(false);
			this.DescPage.ResumeLayout(false);
			this.DescPage.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
