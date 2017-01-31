using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TrapAttackForm : Form
	{
		private IContainer components;

		private Button CancelBtn;

		private Button OKBtn;

		private Label ActionLbl;

		private ComboBox ActionBox;

		private Label RangeLbl;

		private TextBox RangeBox;

		private Label TargetLbl;

		private TextBox TargetBox;

		private NumericUpDown InitBox;

		private Label AttackLbl;

		private Button AttackBtn;

		private TabControl Pages;

		private TabPage HitPage;

		private TabPage MissPage;

		private CheckBox InitBtn;

		private TextBox HitBox;

		private TextBox MissBox;

		private TabPage TriggerPage;

		private TextBox TriggerBox;

		private TabPage EffectPage;

		private TextBox EffectBox;

		private TabPage StatsPage;

		private TabPage AdvicePage;

		private ListView AdviceList;

		private ColumnHeader AdviceHdr;

		private ColumnHeader InfoHdr;

		private TrapAttack fAttack;

		private int fLevel = 1;

		private bool fElite;

		public TrapAttack Attack
		{
			get
			{
				return this.fAttack;
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
			ListViewGroup listViewGroup = new ListViewGroup("Initiative", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Attack Bonus", HorizontalAlignment.Left);
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.ActionLbl = new Label();
			this.ActionBox = new ComboBox();
			this.RangeLbl = new Label();
			this.RangeBox = new TextBox();
			this.TargetLbl = new Label();
			this.TargetBox = new TextBox();
			this.InitBox = new NumericUpDown();
			this.AttackLbl = new Label();
			this.AttackBtn = new Button();
			this.Pages = new TabControl();
			this.StatsPage = new TabPage();
			this.InitBtn = new CheckBox();
			this.TriggerPage = new TabPage();
			this.TriggerBox = new TextBox();
			this.HitPage = new TabPage();
			this.HitBox = new TextBox();
			this.MissPage = new TabPage();
			this.MissBox = new TextBox();
			this.EffectPage = new TabPage();
			this.EffectBox = new TextBox();
			this.AdvicePage = new TabPage();
			this.AdviceList = new ListView();
			this.AdviceHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			((ISupportInitialize)this.InitBox).BeginInit();
			this.Pages.SuspendLayout();
			this.StatsPage.SuspendLayout();
			this.TriggerPage.SuspendLayout();
			this.HitPage.SuspendLayout();
			this.MissPage.SuspendLayout();
			this.EffectPage.SuspendLayout();
			this.AdvicePage.SuspendLayout();
			base.SuspendLayout();
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(254, 185);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 14;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(173, 185);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 13;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.ActionLbl.AutoSize = true;
			this.ActionLbl.Location = new Point(6, 9);
			this.ActionLbl.Name = "ActionLbl";
			this.ActionLbl.Size = new Size(40, 13);
			this.ActionLbl.TabIndex = 2;
			this.ActionLbl.Text = "Action:";
			this.ActionBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ActionBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.ActionBox.FormattingEnabled = true;
			this.ActionBox.Location = new Point(80, 6);
			this.ActionBox.Name = "ActionBox";
			this.ActionBox.Size = new Size(223, 21);
			this.ActionBox.TabIndex = 3;
			this.RangeLbl.AutoSize = true;
			this.RangeLbl.Location = new Point(6, 36);
			this.RangeLbl.Name = "RangeLbl";
			this.RangeLbl.Size = new Size(42, 13);
			this.RangeLbl.TabIndex = 4;
			this.RangeLbl.Text = "Range:";
			this.RangeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RangeBox.Location = new Point(80, 33);
			this.RangeBox.Name = "RangeBox";
			this.RangeBox.Size = new Size(223, 20);
			this.RangeBox.TabIndex = 5;
			this.TargetLbl.AutoSize = true;
			this.TargetLbl.Location = new Point(6, 62);
			this.TargetLbl.Name = "TargetLbl";
			this.TargetLbl.Size = new Size(41, 13);
			this.TargetLbl.TabIndex = 6;
			this.TargetLbl.Text = "Target:";
			this.TargetBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TargetBox.Location = new Point(80, 59);
			this.TargetBox.Name = "TargetBox";
			this.TargetBox.Size = new Size(223, 20);
			this.TargetBox.TabIndex = 7;
			this.InitBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.InitBox.Location = new Point(80, 85);
			this.InitBox.Name = "InitBox";
			this.InitBox.Size = new Size(223, 20);
			this.InitBox.TabIndex = 9;
			this.AttackLbl.AutoSize = true;
			this.AttackLbl.Location = new Point(6, 116);
			this.AttackLbl.Name = "AttackLbl";
			this.AttackLbl.Size = new Size(41, 13);
			this.AttackLbl.TabIndex = 10;
			this.AttackLbl.Text = "Attack:";
			this.AttackBtn.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.AttackBtn.Location = new Point(80, 111);
			this.AttackBtn.Name = "AttackBtn";
			this.AttackBtn.Size = new Size(223, 23);
			this.AttackBtn.TabIndex = 11;
			this.AttackBtn.Text = "[attack]";
			this.AttackBtn.UseVisualStyleBackColor = true;
			this.AttackBtn.Click += new EventHandler(this.AttackBtn_Click);
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.StatsPage);
			this.Pages.Controls.Add(this.TriggerPage);
			this.Pages.Controls.Add(this.HitPage);
			this.Pages.Controls.Add(this.MissPage);
			this.Pages.Controls.Add(this.EffectPage);
			this.Pages.Controls.Add(this.AdvicePage);
			this.Pages.Location = new Point(12, 12);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(317, 167);
			this.Pages.TabIndex = 12;
			this.StatsPage.Controls.Add(this.ActionBox);
			this.StatsPage.Controls.Add(this.AttackBtn);
			this.StatsPage.Controls.Add(this.ActionLbl);
			this.StatsPage.Controls.Add(this.AttackLbl);
			this.StatsPage.Controls.Add(this.RangeLbl);
			this.StatsPage.Controls.Add(this.InitBox);
			this.StatsPage.Controls.Add(this.RangeBox);
			this.StatsPage.Controls.Add(this.InitBtn);
			this.StatsPage.Controls.Add(this.TargetLbl);
			this.StatsPage.Controls.Add(this.TargetBox);
			this.StatsPage.Location = new Point(4, 22);
			this.StatsPage.Name = "StatsPage";
			this.StatsPage.Padding = new Padding(3);
			this.StatsPage.Size = new Size(309, 141);
			this.StatsPage.TabIndex = 4;
			this.StatsPage.Text = "Statistics";
			this.StatsPage.UseVisualStyleBackColor = true;
			this.InitBtn.AutoSize = true;
			this.InitBtn.Location = new Point(6, 86);
			this.InitBtn.Name = "InitBtn";
			this.InitBtn.Size = new Size(68, 17);
			this.InitBtn.TabIndex = 8;
			this.InitBtn.Text = "Initiative:";
			this.InitBtn.UseVisualStyleBackColor = true;
			this.TriggerPage.Controls.Add(this.TriggerBox);
			this.TriggerPage.Location = new Point(4, 22);
			this.TriggerPage.Name = "TriggerPage";
			this.TriggerPage.Padding = new Padding(3);
			this.TriggerPage.Size = new Size(309, 141);
			this.TriggerPage.TabIndex = 2;
			this.TriggerPage.Text = "Trigger";
			this.TriggerPage.UseVisualStyleBackColor = true;
			this.TriggerBox.AcceptsReturn = true;
			this.TriggerBox.AcceptsTab = true;
			this.TriggerBox.Dock = DockStyle.Fill;
			this.TriggerBox.Location = new Point(3, 3);
			this.TriggerBox.Multiline = true;
			this.TriggerBox.Name = "TriggerBox";
			this.TriggerBox.ScrollBars = ScrollBars.Vertical;
			this.TriggerBox.Size = new Size(303, 135);
			this.TriggerBox.TabIndex = 0;
			this.HitPage.Controls.Add(this.HitBox);
			this.HitPage.Location = new Point(4, 22);
			this.HitPage.Name = "HitPage";
			this.HitPage.Padding = new Padding(3);
			this.HitPage.Size = new Size(309, 141);
			this.HitPage.TabIndex = 0;
			this.HitPage.Text = "On Hit";
			this.HitPage.UseVisualStyleBackColor = true;
			this.HitBox.AcceptsReturn = true;
			this.HitBox.AcceptsTab = true;
			this.HitBox.Dock = DockStyle.Fill;
			this.HitBox.Location = new Point(3, 3);
			this.HitBox.Multiline = true;
			this.HitBox.Name = "HitBox";
			this.HitBox.ScrollBars = ScrollBars.Vertical;
			this.HitBox.Size = new Size(303, 135);
			this.HitBox.TabIndex = 0;
			this.MissPage.Controls.Add(this.MissBox);
			this.MissPage.Location = new Point(4, 22);
			this.MissPage.Name = "MissPage";
			this.MissPage.Padding = new Padding(3);
			this.MissPage.Size = new Size(309, 141);
			this.MissPage.TabIndex = 1;
			this.MissPage.Text = "On Miss";
			this.MissPage.UseVisualStyleBackColor = true;
			this.MissBox.AcceptsReturn = true;
			this.MissBox.AcceptsTab = true;
			this.MissBox.Dock = DockStyle.Fill;
			this.MissBox.Location = new Point(3, 3);
			this.MissBox.Multiline = true;
			this.MissBox.Name = "MissBox";
			this.MissBox.ScrollBars = ScrollBars.Vertical;
			this.MissBox.Size = new Size(303, 135);
			this.MissBox.TabIndex = 1;
			this.EffectPage.Controls.Add(this.EffectBox);
			this.EffectPage.Location = new Point(4, 22);
			this.EffectPage.Name = "EffectPage";
			this.EffectPage.Padding = new Padding(3);
			this.EffectPage.Size = new Size(309, 141);
			this.EffectPage.TabIndex = 3;
			this.EffectPage.Text = "Effect";
			this.EffectPage.UseVisualStyleBackColor = true;
			this.EffectBox.AcceptsReturn = true;
			this.EffectBox.AcceptsTab = true;
			this.EffectBox.Dock = DockStyle.Fill;
			this.EffectBox.Location = new Point(3, 3);
			this.EffectBox.Multiline = true;
			this.EffectBox.Name = "EffectBox";
			this.EffectBox.ScrollBars = ScrollBars.Vertical;
			this.EffectBox.Size = new Size(303, 135);
			this.EffectBox.TabIndex = 2;
			this.AdvicePage.Controls.Add(this.AdviceList);
			this.AdvicePage.Location = new Point(4, 22);
			this.AdvicePage.Name = "AdvicePage";
			this.AdvicePage.Padding = new Padding(3);
			this.AdvicePage.Size = new Size(309, 141);
			this.AdvicePage.TabIndex = 5;
			this.AdvicePage.Text = "Advice";
			this.AdvicePage.UseVisualStyleBackColor = true;
			this.AdviceList.Columns.AddRange(new ColumnHeader[]
			{
				this.AdviceHdr,
				this.InfoHdr
			});
			this.AdviceList.Dock = DockStyle.Fill;
			this.AdviceList.FullRowSelect = true;
			listViewGroup.Header = "Initiative";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup2.Header = "Attack Bonus";
			listViewGroup2.Name = "listViewGroup2";
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
			this.AdviceList.Size = new Size(303, 135);
			this.AdviceList.TabIndex = 1;
			this.AdviceList.UseCompatibleStateImageBehavior = false;
			this.AdviceList.View = View.Details;
			this.AdviceHdr.Text = "Advice";
			this.AdviceHdr.Width = 150;
			this.InfoHdr.Text = "Information";
			this.InfoHdr.Width = 100;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(341, 220);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TrapAttackForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Trap Attack";
			((ISupportInitialize)this.InitBox).EndInit();
			this.Pages.ResumeLayout(false);
			this.StatsPage.ResumeLayout(false);
			this.StatsPage.PerformLayout();
			this.TriggerPage.ResumeLayout(false);
			this.TriggerPage.PerformLayout();
			this.HitPage.ResumeLayout(false);
			this.HitPage.PerformLayout();
			this.MissPage.ResumeLayout(false);
			this.MissPage.PerformLayout();
			this.EffectPage.ResumeLayout(false);
			this.EffectPage.PerformLayout();
			this.AdvicePage.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public TrapAttackForm(TrapAttack attack, int level, bool elite)
		{
			this.InitializeComponent();
			Array values = Enum.GetValues(typeof(ActionType));
			foreach (ActionType actionType in values)
			{
				this.ActionBox.Items.Add(actionType);
			}
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fAttack = attack.Copy();
			this.fLevel = level;
			this.fElite = elite;
			this.TriggerBox.Text = this.fAttack.Trigger;
			this.ActionBox.SelectedItem = this.fAttack.Action;
			this.RangeBox.Text = this.fAttack.Range;
			this.TargetBox.Text = this.fAttack.Target;
			this.InitBtn.Checked = this.fAttack.HasInitiative;
			this.InitBox.Value = this.fAttack.Initiative;
			this.AttackBtn.Text = this.fAttack.Attack.ToString();
			this.HitBox.Text = this.fAttack.OnHit;
			this.MissBox.Text = this.fAttack.OnMiss;
			this.EffectBox.Text = this.fAttack.Effect;
			this.update_advice();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.InitBox.Enabled = this.InitBtn.Checked;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fAttack.Trigger = this.TriggerBox.Text;
			this.fAttack.Action = (ActionType)this.ActionBox.SelectedItem;
			this.fAttack.Range = this.RangeBox.Text;
			this.fAttack.Target = this.TargetBox.Text;
			this.fAttack.HasInitiative = this.InitBtn.Checked;
			this.fAttack.Initiative = (int)this.InitBox.Value;
			this.fAttack.OnHit = this.HitBox.Text;
			this.fAttack.OnMiss = this.MissBox.Text;
			this.fAttack.Effect = this.EffectBox.Text;
		}

		private void AttackBtn_Click(object sender, EventArgs e)
		{
			PowerAttackForm powerAttackForm = new PowerAttackForm(this.fAttack.Attack, false, 0, null);
			if (powerAttackForm.ShowDialog() == DialogResult.OK)
			{
				this.fAttack.Attack = powerAttackForm.Attack;
				this.AttackBtn.Text = this.fAttack.Attack.ToString();
			}
		}

		private void update_advice()
		{
			int num = 2;
			int num2 = this.fLevel + 5;
			int num3 = this.fLevel + 3;
			if (this.fElite)
			{
				num += 2;
				num2 += 2;
				num3 += 2;
			}
			ListViewItem listViewItem = this.AdviceList.Items.Add("Initiative");
			listViewItem.SubItems.Add("+" + num);
			listViewItem.Group = this.AdviceList.Groups[0];
			ListViewItem listViewItem2 = this.AdviceList.Items.Add("Attack vs AC");
			listViewItem2.SubItems.Add("+" + num2);
			listViewItem2.Group = this.AdviceList.Groups[1];
			ListViewItem listViewItem3 = this.AdviceList.Items.Add("Attack vs other defence");
			listViewItem3.SubItems.Add("+" + num3);
			listViewItem3.Group = this.AdviceList.Groups[1];
		}
	}
}
