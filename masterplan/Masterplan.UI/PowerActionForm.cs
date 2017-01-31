using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class PowerActionForm : Form
	{
		private PowerAction fAction;

		private IContainer components;

		private Button CancelBtn;

		private Button OKBtn;

		private Label ActionLbl;

		private ComboBox ActionBox;

		private Label RechargeLbl;

		private ComboBox RechargeBox;

		private TextBox TriggerBox;

		private Label TriggerLbl;

		private ComboBox SustainBox;

		private Label SustainLbl;

		private CheckBox TraitBox;

		private GroupBox UsageGroup;

		private CheckBox BasicAttackBtn;

		private RadioButton DailyBtn;

		private RadioButton EncounterBtn;

		private RadioButton AtWillBtn;

		public PowerAction Action
		{
			get
			{
				return this.fAction;
			}
		}

		public PowerActionForm(PowerAction action)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.RechargeBox.Items.Add("Recharges on 2-6");
			this.RechargeBox.Items.Add("Recharges on 3-6");
			this.RechargeBox.Items.Add("Recharges on 4-6");
			this.RechargeBox.Items.Add("Recharges on 5-6");
			this.RechargeBox.Items.Add("Recharges on 6");
			Array values = Enum.GetValues(typeof(ActionType));
			foreach (ActionType actionType in values)
			{
				this.ActionBox.Items.Add(actionType);
				this.SustainBox.Items.Add(actionType);
			}
			if (action != null)
			{
				this.fAction = action.Copy();
			}
			if (this.fAction != null)
			{
				this.fAction = action.Copy();
				this.TraitBox.Checked = false;
				switch (this.fAction.Use)
				{
				case PowerUseType.Encounter:
					this.EncounterBtn.Checked = true;
					this.RechargeBox.Text = this.fAction.Recharge;
					break;
				case PowerUseType.AtWill:
					this.AtWillBtn.Checked = true;
					this.BasicAttackBtn.Checked = false;
					break;
				case PowerUseType.Basic:
					this.AtWillBtn.Checked = true;
					this.BasicAttackBtn.Checked = true;
					break;
				case PowerUseType.Daily:
					this.DailyBtn.Checked = true;
					break;
				}
				this.ActionBox.SelectedItem = this.fAction.Action;
				this.TriggerBox.Text = this.fAction.Trigger;
				this.SustainBox.SelectedItem = this.fAction.SustainAction;
				return;
			}
			this.TraitBox.Checked = true;
			this.AtWillBtn.Checked = true;
			this.BasicAttackBtn.Checked = false;
			this.ActionBox.SelectedItem = ActionType.Standard;
			this.SustainBox.SelectedItem = ActionType.None;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			bool @checked = this.TraitBox.Checked;
			this.UsageGroup.Enabled = !@checked;
			this.BasicAttackBtn.Enabled = (this.UsageGroup.Enabled && this.AtWillBtn.Checked);
			this.RechargeLbl.Enabled = (this.UsageGroup.Enabled && this.EncounterBtn.Checked);
			this.RechargeBox.Enabled = (this.UsageGroup.Enabled && this.EncounterBtn.Checked);
			this.ActionLbl.Enabled = !@checked;
			this.ActionBox.Enabled = !@checked;
			this.TriggerLbl.Enabled = !@checked;
			this.TriggerBox.Enabled = !@checked;
			this.SustainLbl.Enabled = !@checked;
			this.SustainBox.Enabled = !@checked;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			if (this.TraitBox.Checked)
			{
				this.fAction = null;
				return;
			}
			if (this.fAction == null)
			{
				this.fAction = new PowerAction();
			}
			if (this.AtWillBtn.Checked)
			{
				this.fAction.Use = (this.BasicAttackBtn.Checked ? PowerUseType.Basic : PowerUseType.AtWill);
			}
			if (this.EncounterBtn.Checked)
			{
				this.fAction.Use = PowerUseType.Encounter;
				this.fAction.Recharge = this.RechargeBox.Text;
			}
			if (this.DailyBtn.Checked)
			{
				this.fAction.Use = PowerUseType.Daily;
			}
			this.fAction.Action = (ActionType)this.ActionBox.SelectedItem;
			this.fAction.Trigger = this.TriggerBox.Text;
			this.fAction.SustainAction = (ActionType)this.SustainBox.SelectedItem;
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
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.ActionLbl = new Label();
			this.ActionBox = new ComboBox();
			this.RechargeLbl = new Label();
			this.RechargeBox = new ComboBox();
			this.TriggerBox = new TextBox();
			this.TriggerLbl = new Label();
			this.SustainBox = new ComboBox();
			this.SustainLbl = new Label();
			this.TraitBox = new CheckBox();
			this.UsageGroup = new GroupBox();
			this.BasicAttackBtn = new CheckBox();
			this.DailyBtn = new RadioButton();
			this.EncounterBtn = new RadioButton();
			this.AtWillBtn = new RadioButton();
			this.UsageGroup.SuspendLayout();
			base.SuspendLayout();
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(218, 288);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 9;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(137, 288);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 8;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.ActionLbl.AutoSize = true;
			this.ActionLbl.Location = new Point(12, 199);
			this.ActionLbl.Name = "ActionLbl";
			this.ActionLbl.Size = new Size(40, 13);
			this.ActionLbl.TabIndex = 2;
			this.ActionLbl.Text = "Action:";
			this.ActionBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ActionBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.ActionBox.FormattingEnabled = true;
			this.ActionBox.Location = new Point(75, 196);
			this.ActionBox.Name = "ActionBox";
			this.ActionBox.Size = new Size(218, 21);
			this.ActionBox.TabIndex = 3;
			this.RechargeLbl.AutoSize = true;
			this.RechargeLbl.Location = new Point(22, 96);
			this.RechargeLbl.Name = "RechargeLbl";
			this.RechargeLbl.Size = new Size(57, 13);
			this.RechargeLbl.TabIndex = 3;
			this.RechargeLbl.Text = "Recharge:";
			this.RechargeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RechargeBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.RechargeBox.FormattingEnabled = true;
			this.RechargeBox.Location = new Point(85, 93);
			this.RechargeBox.Name = "RechargeBox";
			this.RechargeBox.Size = new Size(190, 21);
			this.RechargeBox.TabIndex = 4;
			this.TriggerBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TriggerBox.Location = new Point(75, 223);
			this.TriggerBox.Name = "TriggerBox";
			this.TriggerBox.Size = new Size(218, 20);
			this.TriggerBox.TabIndex = 5;
			this.TriggerLbl.AutoSize = true;
			this.TriggerLbl.Location = new Point(12, 226);
			this.TriggerLbl.Name = "TriggerLbl";
			this.TriggerLbl.Size = new Size(43, 13);
			this.TriggerLbl.TabIndex = 4;
			this.TriggerLbl.Text = "Trigger:";
			this.SustainBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SustainBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.SustainBox.FormattingEnabled = true;
			this.SustainBox.Location = new Point(75, 249);
			this.SustainBox.Name = "SustainBox";
			this.SustainBox.Size = new Size(218, 21);
			this.SustainBox.TabIndex = 7;
			this.SustainLbl.AutoSize = true;
			this.SustainLbl.Location = new Point(12, 252);
			this.SustainLbl.Name = "SustainLbl";
			this.SustainLbl.Size = new Size(45, 13);
			this.SustainLbl.TabIndex = 6;
			this.SustainLbl.Text = "Sustain:";
			this.TraitBox.AutoSize = true;
			this.TraitBox.Location = new Point(12, 12);
			this.TraitBox.Name = "TraitBox";
			this.TraitBox.Size = new Size(117, 17);
			this.TraitBox.TabIndex = 0;
			this.TraitBox.Text = "This power is a trait";
			this.TraitBox.UseVisualStyleBackColor = true;
			this.UsageGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.UsageGroup.Controls.Add(this.BasicAttackBtn);
			this.UsageGroup.Controls.Add(this.DailyBtn);
			this.UsageGroup.Controls.Add(this.EncounterBtn);
			this.UsageGroup.Controls.Add(this.AtWillBtn);
			this.UsageGroup.Controls.Add(this.RechargeBox);
			this.UsageGroup.Controls.Add(this.RechargeLbl);
			this.UsageGroup.Location = new Point(12, 35);
			this.UsageGroup.Name = "UsageGroup";
			this.UsageGroup.Size = new Size(281, 155);
			this.UsageGroup.TabIndex = 1;
			this.UsageGroup.TabStop = false;
			this.UsageGroup.Text = "Power Usage";
			this.BasicAttackBtn.AutoSize = true;
			this.BasicAttackBtn.Location = new Point(25, 43);
			this.BasicAttackBtn.Name = "BasicAttackBtn";
			this.BasicAttackBtn.Size = new Size(86, 17);
			this.BasicAttackBtn.TabIndex = 1;
			this.BasicAttackBtn.Text = "Basic Attack";
			this.BasicAttackBtn.UseVisualStyleBackColor = true;
			this.DailyBtn.AutoSize = true;
			this.DailyBtn.Location = new Point(6, 125);
			this.DailyBtn.Name = "DailyBtn";
			this.DailyBtn.Size = new Size(48, 17);
			this.DailyBtn.TabIndex = 5;
			this.DailyBtn.TabStop = true;
			this.DailyBtn.Text = "Daily";
			this.DailyBtn.UseVisualStyleBackColor = true;
			this.EncounterBtn.AutoSize = true;
			this.EncounterBtn.Location = new Point(6, 70);
			this.EncounterBtn.Name = "EncounterBtn";
			this.EncounterBtn.Size = new Size(74, 17);
			this.EncounterBtn.TabIndex = 2;
			this.EncounterBtn.TabStop = true;
			this.EncounterBtn.Text = "Encounter";
			this.EncounterBtn.UseVisualStyleBackColor = true;
			this.AtWillBtn.AutoSize = true;
			this.AtWillBtn.Location = new Point(6, 19);
			this.AtWillBtn.Name = "AtWillBtn";
			this.AtWillBtn.Size = new Size(55, 17);
			this.AtWillBtn.TabIndex = 0;
			this.AtWillBtn.TabStop = true;
			this.AtWillBtn.Text = "At Will";
			this.AtWillBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(305, 323);
			base.Controls.Add(this.UsageGroup);
			base.Controls.Add(this.TraitBox);
			base.Controls.Add(this.SustainBox);
			base.Controls.Add(this.SustainLbl);
			base.Controls.Add(this.TriggerBox);
			base.Controls.Add(this.TriggerLbl);
			base.Controls.Add(this.ActionBox);
			base.Controls.Add(this.ActionLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PowerActionForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Power Action";
			this.UsageGroup.ResumeLayout(false);
			this.UsageGroup.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
