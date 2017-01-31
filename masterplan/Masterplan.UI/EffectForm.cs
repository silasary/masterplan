using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class EffectForm : Form
	{
		private const string ENCOUNTER = "Lasts until the end of the encounter";

		private const string SAVE_ENDS = "Save ends";

		private const string START = "Lasts until the start of someone's next turn";

		private const string END = "Lasts until the end of someone's next turn";

		private const string BLANK_EFFECT = "(enter effect name)";

		private const string SOMEONE_ELSE = "(someone else)";

		private const string RESIST = "Resist";

		private const string VULNERABLE = "Vulnerable";

		private const string IMMUNE = "Immune";

		private const int MIN_HEIGHT = 25;

		private Dictionary<RadioButton, int> fHeights = new Dictionary<RadioButton, int>();

		private OngoingCondition fCondition;

		private CombatData fCurrentActor;

		private int fCurrentRound = int.MinValue;

		private IContainer components;

		private Button CancelBtn;

		private Button OKBtn;

		private RadioButton ConditionBtn;

		private RadioButton DamageBtn;

		private ComboBox ConditionBox;

		private NumericUpDown DamageBox;

		private ComboBox DamageTypeBox;

		private ComboBox DurationBox;

		private ComboBox DurationCreatureBox;

		private Label CreatureLbl;

		private Label ModLbl;

		private NumericUpDown ModBox;

		private Label TypeLbl;

		private GroupBox DurationGroup;

		private Label DamageLbl;

		private Label DefenceModLbl;

		private RadioButton DefenceBtn;

		private NumericUpDown DefenceModBox;

		private CheckBox WillBox;

		private CheckBox RefBox;

		private CheckBox FortBox;

		private CheckBox ACBox;

		private LinkLabel NoDefencesLbl;

		private LinkLabel AllDefencesLbl;

		private TextBox RegenConditionsBox;

		private Label RegenConditionsLbl;

		private Label RegenValueLbl;

		private NumericUpDown RegenValueBox;

		private RadioButton RegenBtn;

		private Label DamageModValueLbl;

		private ComboBox DamageModDirBox;

		private NumericUpDown DamageModValueBox;

		private RadioButton DamageModBtn;

		private Label DamageModTypeLbl;

		private ComboBox DamageModTypeBox;

		private Panel ConditionPanel;

		private Panel DamagePanel;

		private Panel DefencePanel;

		private Panel DamageModPanel;

		private Panel RegenPanel;

		private Panel PropertiesPanel;

		private Panel AuraPanel;

		private NumericUpDown AuraRadiusBox;

		private Label AuraRadiusLbl;

		private Label AuraDetailsLbl;

		private TextBox AuraDetailsBox;

		private RadioButton AuraBtn;

		public OngoingCondition Effect
		{
			get
			{
				return this.fCondition;
			}
		}

		public EffectForm(OngoingCondition condition, Encounter enc, CombatData current_actor, int current_round)
		{
			this.InitializeComponent();
			foreach (string current in Conditions.GetConditions())
			{
				this.ConditionBox.Items.Add(current);
			}
			foreach (DamageType damageType in Enum.GetValues(typeof(DamageType)))
			{
				this.DamageTypeBox.Items.Add(damageType);
				this.DamageModTypeBox.Items.Add(damageType);
			}
			this.DamageModDirBox.Items.Add("Resist");
			this.DamageModDirBox.Items.Add("Vulnerable");
			this.DamageModDirBox.Items.Add("Immune");
			this.DurationBox.Items.Add("Lasts until the end of the encounter");
			this.DurationBox.Items.Add("Save ends");
			this.DurationBox.Items.Add("Lasts until the start of someone's next turn");
			this.DurationBox.Items.Add("Lasts until the end of someone's next turn");
			foreach (EncounterSlot current2 in enc.Slots)
			{
				foreach (CombatData current3 in current2.CombatData)
				{
					this.DurationCreatureBox.Items.Add(current3);
				}
			}
			foreach (Hero current4 in Session.Project.Heroes)
			{
				this.DurationCreatureBox.Items.Add(current4);
			}
			foreach (Trap current5 in enc.Traps)
			{
				this.DurationCreatureBox.Items.Add(current5);
			}
			Application.Idle += new EventHandler(this.Application_Idle);
			this.init(condition, current_actor, current_round);
		}

		public EffectForm(OngoingCondition condition, Hero hero)
		{
			this.InitializeComponent();
			foreach (string current in Conditions.GetConditions())
			{
				this.ConditionBox.Items.Add(current);
			}
			foreach (DamageType damageType in Enum.GetValues(typeof(DamageType)))
			{
				this.DamageTypeBox.Items.Add(damageType);
				this.DamageModTypeBox.Items.Add(damageType);
			}
			this.DamageModDirBox.Items.Add("Resist");
			this.DamageModDirBox.Items.Add("Vulnerable");
			this.DamageModDirBox.Items.Add("Immune");
			this.DurationBox.Items.Add("Lasts until the end of the encounter");
			this.DurationBox.Items.Add("Save ends");
			this.DurationBox.Items.Add("Lasts until the start of someone's next turn");
			this.DurationBox.Items.Add("Lasts until the end of someone's next turn");
			this.DurationCreatureBox.Items.Add(hero);
			this.DurationCreatureBox.Items.Add("(someone else)");
			Application.Idle += new EventHandler(this.Application_Idle);
			this.init(condition, null, -1);
		}

		private void init(OngoingCondition condition, CombatData current_actor, int current_round)
		{
			this.fHeights[this.ConditionBtn] = this.ConditionPanel.Height;
			this.fHeights[this.DamageBtn] = this.DamagePanel.Height;
			this.fHeights[this.DefenceBtn] = this.DefencePanel.Height;
			this.fHeights[this.DamageModBtn] = this.DamageModPanel.Height;
			this.fHeights[this.RegenBtn] = this.RegenPanel.Height;
			this.fHeights[this.AuraBtn] = this.AuraPanel.Height;
			this.fCondition = condition.Copy();
			this.fCurrentActor = current_actor;
			this.fCurrentRound = current_round;
			this.ConditionBtn.Checked = (this.fCondition.Type == OngoingType.Condition);
			this.ConditionBox.Text = ((this.fCondition.Data != "") ? this.fCondition.Data : "(enter effect name)");
			this.DamageBtn.Checked = (this.fCondition.Type == OngoingType.Damage);
			this.DamageBox.Value = this.fCondition.Value;
			this.DamageTypeBox.SelectedItem = this.fCondition.DamageType;
			this.DefenceBtn.Checked = (this.fCondition.Type == OngoingType.DefenceModifier);
			this.DefenceModBox.Value = this.fCondition.DefenceMod;
			if (this.fCondition.Defences.Count == 0)
			{
				this.fCondition.Defences.Add(DefenceType.AC);
			}
			this.ACBox.Checked = this.fCondition.Defences.Contains(DefenceType.AC);
			this.FortBox.Checked = this.fCondition.Defences.Contains(DefenceType.Fortitude);
			this.RefBox.Checked = this.fCondition.Defences.Contains(DefenceType.Reflex);
			this.WillBox.Checked = this.fCondition.Defences.Contains(DefenceType.Will);
			if (this.fCondition.DamageModifier.Value < 0)
			{
				this.DamageModDirBox.SelectedItem = "Resist";
			}
			if (this.fCondition.DamageModifier.Value > 0)
			{
				this.DamageModDirBox.SelectedItem = "Vulnerable";
			}
			if (this.fCondition.DamageModifier.Value == 0)
			{
				this.DamageModDirBox.SelectedItem = "Immune";
			}
			this.DamageModValueBox.Value = Math.Abs(this.fCondition.DamageModifier.Value);
			this.DamageModTypeBox.SelectedItem = this.fCondition.DamageModifier.Type;
			this.RegenValueBox.Value = this.fCondition.Regeneration.Value;
			this.RegenConditionsBox.Text = this.fCondition.Regeneration.Details;
			this.AuraRadiusBox.Value = this.fCondition.Aura.Radius;
			this.AuraDetailsBox.Text = this.fCondition.Aura.Description;
			switch (this.fCondition.Duration)
			{
			case DurationType.Encounter:
				this.DurationBox.SelectedItem = "Lasts until the end of the encounter";
				break;
			case DurationType.SaveEnds:
				this.DurationBox.SelectedItem = "Save ends";
				this.ModBox.Value = this.fCondition.SavingThrowModifier;
				break;
			case DurationType.BeginningOfTurn:
				this.DurationBox.SelectedItem = "Lasts until the start of someone's next turn";
				break;
			case DurationType.EndOfTurn:
				this.DurationBox.SelectedItem = "Lasts until the end of someone's next turn";
				break;
			}
			if (this.fCondition.DurationCreatureID != Guid.Empty)
			{
				this.DurationCreatureBox.SelectedItem = this.get_item(this.fCondition.DurationCreatureID);
				return;
			}
			if (this.fCurrentActor != null)
			{
				this.DurationCreatureBox.SelectedItem = this.get_item(this.fCurrentActor.ID);
				return;
			}
			this.DurationCreatureBox.SelectedItem = this.DurationCreatureBox.Items[0];
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.ConditionBox.Enabled = this.ConditionBtn.Checked;
			this.DamageLbl.Enabled = this.DamageBtn.Checked;
			this.DamageBox.Enabled = this.DamageBtn.Checked;
			this.TypeLbl.Enabled = this.DamageBtn.Checked;
			this.DamageTypeBox.Enabled = this.DamageBtn.Checked;
			this.DefenceModLbl.Enabled = this.DefenceBtn.Checked;
			this.DefenceModBox.Enabled = this.DefenceBtn.Checked;
			this.ACBox.Enabled = this.DefenceBtn.Checked;
			this.FortBox.Enabled = this.DefenceBtn.Checked;
			this.RefBox.Enabled = this.DefenceBtn.Checked;
			this.WillBox.Enabled = this.DefenceBtn.Checked;
			this.AllDefencesLbl.Enabled = this.DefenceBtn.Checked;
			this.NoDefencesLbl.Enabled = this.DefenceBtn.Checked;
			this.DamageModDirBox.Enabled = this.DamageModBtn.Checked;
			this.DamageModValueLbl.Enabled = (this.DamageModBtn.Checked && this.DamageModDirBox.SelectedItem.ToString() != "Immune");
			this.DamageModValueBox.Enabled = (this.DamageModBtn.Checked && this.DamageModDirBox.SelectedItem.ToString() != "Immune");
			this.DamageModTypeLbl.Enabled = this.DamageModBtn.Checked;
			this.DamageModTypeBox.Enabled = this.DamageModBtn.Checked;
			this.RegenValueLbl.Enabled = this.RegenBtn.Checked;
			this.RegenValueBox.Enabled = this.RegenBtn.Checked;
			this.RegenConditionsLbl.Enabled = this.RegenBtn.Checked;
			this.RegenConditionsBox.Enabled = this.RegenBtn.Checked;
			this.AuraRadiusLbl.Enabled = this.AuraBtn.Checked;
			this.AuraRadiusBox.Enabled = this.AuraBtn.Checked;
			this.AuraDetailsLbl.Enabled = this.AuraBtn.Checked;
			this.AuraDetailsBox.Enabled = this.AuraBtn.Checked;
			string a = this.DurationBox.SelectedItem as string;
			this.DurationCreatureBox.Enabled = (a == "Lasts until the start of someone's next turn" || a == "Lasts until the end of someone's next turn");
			this.CreatureLbl.Enabled = this.DurationCreatureBox.Enabled;
			this.ModBox.Enabled = (a == "Save ends");
			this.ModLbl.Enabled = this.ModBox.Enabled;
			if (this.ConditionBtn.Checked)
			{
				this.OKBtn.Enabled = (this.ConditionBox.Text != "" && this.ConditionBox.Text != "(enter effect name)");
				return;
			}
			this.OKBtn.Enabled = true;
		}

		private void EffectForm_Shown(object sender, EventArgs e)
		{
			if (this.ConditionBtn.Checked)
			{
				this.ConditionBox.Focus();
				this.ConditionBox.SelectAll();
			}
			if (this.DamageBtn.Checked)
			{
				this.DamageBox.Focus();
				this.DamageBox.Select(0, 1);
			}
			if (this.DefenceBtn.Checked)
			{
				this.DefenceModBox.Focus();
				this.DefenceModBox.Select(0, 1);
			}
			if (this.RegenBtn.Checked)
			{
				this.RegenValueBox.Focus();
				this.RegenValueBox.Select(0, 1);
			}
			if (this.AuraBtn.Checked)
			{
				this.AuraRadiusBox.Focus();
				this.AuraRadiusBox.Select(0, 1);
			}
		}

		private void DurationBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.fCondition.DurationCreatureID == Guid.Empty)
			{
				if (this.fCurrentActor != null)
				{
					this.DurationCreatureBox.SelectedItem = this.get_item(this.fCurrentActor.ID);
					return;
				}
				this.DurationCreatureBox.SelectedItem = this.DurationCreatureBox.Items[0];
			}
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			if (this.ConditionBtn.Checked)
			{
				this.fCondition.Type = OngoingType.Condition;
			}
			if (this.DamageBtn.Checked)
			{
				this.fCondition.Type = OngoingType.Damage;
			}
			if (this.DefenceBtn.Checked)
			{
				this.fCondition.Type = OngoingType.DefenceModifier;
			}
			if (this.DamageModBtn.Checked)
			{
				this.fCondition.Type = OngoingType.DamageModifier;
			}
			if (this.RegenBtn.Checked)
			{
				this.fCondition.Type = OngoingType.Regeneration;
			}
			if (this.AuraBtn.Checked)
			{
				this.fCondition.Type = OngoingType.Aura;
			}
			this.fCondition.Data = this.ConditionBox.Text;
			this.fCondition.Value = (int)this.DamageBox.Value;
			this.fCondition.DamageType = (DamageType)this.DamageTypeBox.SelectedItem;
			this.fCondition.DefenceMod = (int)this.DefenceModBox.Value;
			this.fCondition.Defences.Clear();
			if (this.ACBox.Checked)
			{
				this.fCondition.Defences.Add(DefenceType.AC);
			}
			if (this.FortBox.Checked)
			{
				this.fCondition.Defences.Add(DefenceType.Fortitude);
			}
			if (this.RefBox.Checked)
			{
				this.fCondition.Defences.Add(DefenceType.Reflex);
			}
			if (this.WillBox.Checked)
			{
				this.fCondition.Defences.Add(DefenceType.Will);
			}
			int num = (int)this.DamageModValueBox.Value;
			switch (this.DamageModDirBox.SelectedIndex)
			{
			case 0:
				this.fCondition.DamageModifier.Value = num * -1;
				break;
			case 1:
				this.fCondition.DamageModifier.Value = num;
				break;
			case 2:
				this.fCondition.DamageModifier.Value = 0;
				break;
			}
			this.fCondition.DamageModifier.Type = (DamageType)this.DamageModTypeBox.SelectedItem;
			this.fCondition.Regeneration.Value = (int)this.RegenValueBox.Value;
			this.fCondition.Regeneration.Details = this.RegenConditionsBox.Text;
			int num2 = (int)this.AuraRadiusBox.Value;
			this.fCondition.Aura.Details = num2 + ": " + this.AuraDetailsBox.Text;
			string a = this.DurationBox.SelectedItem as string;
			if (a == "Lasts until the end of the encounter")
			{
				this.fCondition.Duration = DurationType.Encounter;
			}
			else if (a == "Save ends")
			{
				this.fCondition.Duration = DurationType.SaveEnds;
				this.fCondition.SavingThrowModifier = (int)this.ModBox.Value;
			}
			else if (a == "Lasts until the start of someone's next turn")
			{
				this.fCondition.Duration = DurationType.BeginningOfTurn;
				this.fCondition.DurationCreatureID = this.get_id(this.DurationCreatureBox.SelectedItem);
			}
			else if (a == "Lasts until the end of someone's next turn")
			{
				this.fCondition.Duration = DurationType.EndOfTurn;
				this.fCondition.DurationCreatureID = this.get_id(this.DurationCreatureBox.SelectedItem);
			}
			if (a == "Lasts until the start of someone's next turn" || a == "Lasts until the end of someone's next turn")
			{
				this.fCondition.DurationRound = this.fCurrentRound;
				if (this.fCurrentActor != null && this.fCondition.DurationCreatureID == this.fCurrentActor.ID)
				{
					this.fCondition.DurationRound++;
				}
			}
		}

		private object get_item(Guid id)
		{
			foreach (object current in this.DurationCreatureBox.Items)
			{
				if (current is CombatData)
				{
					CombatData combatData = current as CombatData;
					if (combatData.ID == id)
					{
						object result = current;
						return result;
					}
				}
				if (current is Hero)
				{
					Hero hero = current as Hero;
					if (hero.ID == id)
					{
						object result = current;
						return result;
					}
				}
				if (current is Trap)
				{
					Trap trap = current as Trap;
					if (trap.ID == id)
					{
						object result = current;
						return result;
					}
				}
			}
			return null;
		}

		private Guid get_id(object obj)
		{
			if (obj is CombatData)
			{
				CombatData combatData = obj as CombatData;
				return combatData.ID;
			}
			if (obj is Hero)
			{
				Hero hero = obj as Hero;
				return hero.ID;
			}
			if (obj is Trap)
			{
				Trap trap = obj as Trap;
				return trap.ID;
			}
			return Guid.Empty;
		}

		private void AllDefencesLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.set_defences(true);
		}

		private void NoDefencesLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.set_defences(false);
		}

		private void set_defences(bool enabled)
		{
			this.ACBox.Checked = enabled;
			this.FortBox.Checked = enabled;
			this.RefBox.Checked = enabled;
			this.WillBox.Checked = enabled;
		}

		private void EffectTypeChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = sender as RadioButton;
			if (radioButton == null)
			{
				return;
			}
			if (!radioButton.Checked)
			{
				return;
			}
			List<RadioButton> list = new List<RadioButton>();
			list.Add(this.ConditionBtn);
			list.Add(this.DamageBtn);
			list.Add(this.DefenceBtn);
			list.Add(this.DamageModBtn);
			list.Add(this.RegenBtn);
			list.Add(this.AuraBtn);
			list.Remove(radioButton);
			foreach (RadioButton current in list)
			{
				current.Checked = false;
			}
			this.ConditionPanel.Height = ((radioButton == this.ConditionBtn) ? this.fHeights[radioButton] : 25);
			this.DamagePanel.Height = ((radioButton == this.DamageBtn) ? this.fHeights[radioButton] : 25);
			this.DefencePanel.Height = ((radioButton == this.DefenceBtn) ? this.fHeights[radioButton] : 25);
			this.DamageModPanel.Height = ((radioButton == this.DamageModBtn) ? this.fHeights[radioButton] : 25);
			this.RegenPanel.Height = ((radioButton == this.RegenBtn) ? this.fHeights[radioButton] : 25);
			this.AuraPanel.Height = ((radioButton == this.AuraBtn) ? this.fHeights[radioButton] : 25);
			int top = this.PropertiesPanel.Top;
			int num = this.DurationGroup.Top - this.PropertiesPanel.Bottom;
			int num2 = base.Height - this.DurationGroup.Bottom;
			base.Height = top + this.ConditionPanel.Height + this.DamagePanel.Height + this.DefencePanel.Height + this.DamageModPanel.Height + this.RegenPanel.Height + this.AuraPanel.Height + num + this.DurationGroup.Height + num2;
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
			this.ConditionBtn = new RadioButton();
			this.DamageBtn = new RadioButton();
			this.ConditionBox = new ComboBox();
			this.DamageBox = new NumericUpDown();
			this.DamageTypeBox = new ComboBox();
			this.DurationBox = new ComboBox();
			this.DurationCreatureBox = new ComboBox();
			this.CreatureLbl = new Label();
			this.ModLbl = new Label();
			this.ModBox = new NumericUpDown();
			this.TypeLbl = new Label();
			this.DurationGroup = new GroupBox();
			this.DamageModTypeLbl = new Label();
			this.DamageModTypeBox = new ComboBox();
			this.RegenConditionsBox = new TextBox();
			this.RegenConditionsLbl = new Label();
			this.RegenValueLbl = new Label();
			this.RegenValueBox = new NumericUpDown();
			this.RegenBtn = new RadioButton();
			this.DamageModValueLbl = new Label();
			this.DamageModDirBox = new ComboBox();
			this.DamageModValueBox = new NumericUpDown();
			this.DamageModBtn = new RadioButton();
			this.NoDefencesLbl = new LinkLabel();
			this.AllDefencesLbl = new LinkLabel();
			this.WillBox = new CheckBox();
			this.RefBox = new CheckBox();
			this.FortBox = new CheckBox();
			this.ACBox = new CheckBox();
			this.DefenceModLbl = new Label();
			this.DefenceBtn = new RadioButton();
			this.DefenceModBox = new NumericUpDown();
			this.DamageLbl = new Label();
			this.ConditionPanel = new Panel();
			this.DamagePanel = new Panel();
			this.DefencePanel = new Panel();
			this.DamageModPanel = new Panel();
			this.RegenPanel = new Panel();
			this.PropertiesPanel = new Panel();
			this.AuraPanel = new Panel();
			this.AuraRadiusBox = new NumericUpDown();
			this.AuraRadiusLbl = new Label();
			this.AuraDetailsLbl = new Label();
			this.AuraDetailsBox = new TextBox();
			this.AuraBtn = new RadioButton();
			((ISupportInitialize)this.DamageBox).BeginInit();
			((ISupportInitialize)this.ModBox).BeginInit();
			this.DurationGroup.SuspendLayout();
			((ISupportInitialize)this.RegenValueBox).BeginInit();
			((ISupportInitialize)this.DamageModValueBox).BeginInit();
			((ISupportInitialize)this.DefenceModBox).BeginInit();
			this.ConditionPanel.SuspendLayout();
			this.DamagePanel.SuspendLayout();
			this.DefencePanel.SuspendLayout();
			this.DamageModPanel.SuspendLayout();
			this.RegenPanel.SuspendLayout();
			this.PropertiesPanel.SuspendLayout();
			this.AuraPanel.SuspendLayout();
			((ISupportInitialize)this.AuraRadiusBox).BeginInit();
			base.SuspendLayout();
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(306, 624);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 3;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(225, 624);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 2;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.ConditionBtn.AutoSize = true;
			this.ConditionBtn.Location = new Point(3, 3);
			this.ConditionBtn.Name = "ConditionBtn";
			this.ConditionBtn.Size = new Size(106, 17);
			this.ConditionBtn.TabIndex = 0;
			this.ConditionBtn.TabStop = true;
			this.ConditionBtn.Text = "Apply a condition";
			this.ConditionBtn.UseVisualStyleBackColor = true;
			this.ConditionBtn.CheckedChanged += new EventHandler(this.EffectTypeChanged);
			this.DamageBtn.AutoSize = true;
			this.DamageBtn.Location = new Point(3, 3);
			this.DamageBtn.Name = "DamageBtn";
			this.DamageBtn.Size = new Size(133, 17);
			this.DamageBtn.TabIndex = 2;
			this.DamageBtn.TabStop = true;
			this.DamageBtn.Text = "Apply ongoing damage";
			this.DamageBtn.UseVisualStyleBackColor = true;
			this.DamageBtn.CheckedChanged += new EventHandler(this.EffectTypeChanged);
			this.ConditionBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ConditionBox.AutoCompleteMode = AutoCompleteMode.Append;
			this.ConditionBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.ConditionBox.FormattingEnabled = true;
			this.ConditionBox.Location = new Point(12, 26);
			this.ConditionBox.Name = "ConditionBox";
			this.ConditionBox.Size = new Size(345, 21);
			this.ConditionBox.TabIndex = 1;
			this.DamageBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DamageBox.Location = new Point(68, 26);
			this.DamageBox.Name = "DamageBox";
			this.DamageBox.Size = new Size(289, 20);
			this.DamageBox.TabIndex = 4;
			this.DamageTypeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DamageTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.DamageTypeBox.FormattingEnabled = true;
			this.DamageTypeBox.Location = new Point(68, 52);
			this.DamageTypeBox.Name = "DamageTypeBox";
			this.DamageTypeBox.Size = new Size(289, 21);
			this.DamageTypeBox.TabIndex = 6;
			this.DurationBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DurationBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.DurationBox.FormattingEnabled = true;
			this.DurationBox.Location = new Point(6, 19);
			this.DurationBox.Name = "DurationBox";
			this.DurationBox.Size = new Size(351, 21);
			this.DurationBox.TabIndex = 0;
			this.DurationBox.SelectedIndexChanged += new EventHandler(this.DurationBox_SelectedIndexChanged);
			this.DurationCreatureBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DurationCreatureBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.DurationCreatureBox.FormattingEnabled = true;
			this.DurationCreatureBox.Location = new Point(84, 46);
			this.DurationCreatureBox.Name = "DurationCreatureBox";
			this.DurationCreatureBox.Size = new Size(273, 21);
			this.DurationCreatureBox.TabIndex = 2;
			this.CreatureLbl.AutoSize = true;
			this.CreatureLbl.Location = new Point(6, 49);
			this.CreatureLbl.Name = "CreatureLbl";
			this.CreatureLbl.Size = new Size(33, 13);
			this.CreatureLbl.TabIndex = 1;
			this.CreatureLbl.Text = "Who:";
			this.ModLbl.AutoSize = true;
			this.ModLbl.Location = new Point(6, 75);
			this.ModLbl.Name = "ModLbl";
			this.ModLbl.Size = new Size(59, 13);
			this.ModLbl.TabIndex = 3;
			this.ModLbl.Text = "Save Mod:";
			this.ModBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ModBox.Location = new Point(84, 73);
			NumericUpDown arg_7F8_0 = this.ModBox;
			int[] array = new int[4];
			array[0] = 20;
			arg_7F8_0.Maximum = new decimal(array);
			this.ModBox.Minimum = new decimal(new int[]
			{
				20,
				0,
				0,
				int.MinValue
			});
			this.ModBox.Name = "ModBox";
			this.ModBox.Size = new Size(273, 20);
			this.ModBox.TabIndex = 4;
			this.TypeLbl.AutoSize = true;
			this.TypeLbl.Location = new Point(9, 55);
			this.TypeLbl.Name = "TypeLbl";
			this.TypeLbl.Size = new Size(34, 13);
			this.TypeLbl.TabIndex = 5;
			this.TypeLbl.Text = "Type:";
			this.DurationGroup.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.DurationGroup.Controls.Add(this.DurationBox);
			this.DurationGroup.Controls.Add(this.DurationCreatureBox);
			this.DurationGroup.Controls.Add(this.ModBox);
			this.DurationGroup.Controls.Add(this.CreatureLbl);
			this.DurationGroup.Controls.Add(this.ModLbl);
			this.DurationGroup.Location = new Point(12, 517);
			this.DurationGroup.Name = "DurationGroup";
			this.DurationGroup.Size = new Size(369, 101);
			this.DurationGroup.TabIndex = 1;
			this.DurationGroup.TabStop = false;
			this.DurationGroup.Text = "Duration";
			this.DamageModTypeLbl.AutoSize = true;
			this.DamageModTypeLbl.Location = new Point(9, 82);
			this.DamageModTypeLbl.Name = "DamageModTypeLbl";
			this.DamageModTypeLbl.Size = new Size(34, 13);
			this.DamageModTypeLbl.TabIndex = 20;
			this.DamageModTypeLbl.Text = "Type:";
			this.DamageModTypeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DamageModTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.DamageModTypeBox.FormattingEnabled = true;
			this.DamageModTypeBox.Location = new Point(68, 79);
			this.DamageModTypeBox.Name = "DamageModTypeBox";
			this.DamageModTypeBox.Size = new Size(289, 21);
			this.DamageModTypeBox.TabIndex = 21;
			this.RegenConditionsBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RegenConditionsBox.Location = new Point(74, 52);
			this.RegenConditionsBox.Name = "RegenConditionsBox";
			this.RegenConditionsBox.Size = new Size(283, 20);
			this.RegenConditionsBox.TabIndex = 26;
			this.RegenConditionsLbl.AutoSize = true;
			this.RegenConditionsLbl.Location = new Point(9, 55);
			this.RegenConditionsLbl.Name = "RegenConditionsLbl";
			this.RegenConditionsLbl.Size = new Size(59, 13);
			this.RegenConditionsLbl.TabIndex = 25;
			this.RegenConditionsLbl.Text = "Conditions:";
			this.RegenValueLbl.AutoSize = true;
			this.RegenValueLbl.Location = new Point(9, 28);
			this.RegenValueLbl.Name = "RegenValueLbl";
			this.RegenValueLbl.Size = new Size(25, 13);
			this.RegenValueLbl.TabIndex = 23;
			this.RegenValueLbl.Text = "HP:";
			this.RegenValueBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RegenValueBox.Location = new Point(74, 26);
			this.RegenValueBox.Name = "RegenValueBox";
			this.RegenValueBox.Size = new Size(283, 20);
			this.RegenValueBox.TabIndex = 24;
			this.RegenBtn.AutoSize = true;
			this.RegenBtn.Location = new Point(3, 3);
			this.RegenBtn.Name = "RegenBtn";
			this.RegenBtn.Size = new Size(113, 17);
			this.RegenBtn.TabIndex = 22;
			this.RegenBtn.TabStop = true;
			this.RegenBtn.Text = "Apply regeneration";
			this.RegenBtn.UseVisualStyleBackColor = true;
			this.RegenBtn.CheckedChanged += new EventHandler(this.EffectTypeChanged);
			this.DamageModValueLbl.AutoSize = true;
			this.DamageModValueLbl.Location = new Point(9, 55);
			this.DamageModValueLbl.Name = "DamageModValueLbl";
			this.DamageModValueLbl.Size = new Size(37, 13);
			this.DamageModValueLbl.TabIndex = 18;
			this.DamageModValueLbl.Text = "Value:";
			this.DamageModDirBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DamageModDirBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.DamageModDirBox.FormattingEnabled = true;
			this.DamageModDirBox.Location = new Point(12, 26);
			this.DamageModDirBox.Name = "DamageModDirBox";
			this.DamageModDirBox.Size = new Size(345, 21);
			this.DamageModDirBox.TabIndex = 17;
			this.DamageModValueBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DamageModValueBox.Location = new Point(68, 53);
			this.DamageModValueBox.Name = "DamageModValueBox";
			this.DamageModValueBox.Size = new Size(289, 20);
			this.DamageModValueBox.TabIndex = 19;
			this.DamageModBtn.AutoSize = true;
			this.DamageModBtn.Location = new Point(3, 3);
			this.DamageModBtn.Name = "DamageModBtn";
			this.DamageModBtn.Size = new Size(260, 17);
			this.DamageModBtn.TabIndex = 16;
			this.DamageModBtn.TabStop = true;
			this.DamageModBtn.Text = "Apply damage resistance / vulnerability / immunity";
			this.DamageModBtn.UseVisualStyleBackColor = true;
			this.DamageModBtn.CheckedChanged += new EventHandler(this.EffectTypeChanged);
			this.NoDefencesLbl.AutoSize = true;
			this.NoDefencesLbl.Location = new Point(321, 53);
			this.NoDefencesLbl.Name = "NoDefencesLbl";
			this.NoDefencesLbl.Size = new Size(33, 13);
			this.NoDefencesLbl.TabIndex = 15;
			this.NoDefencesLbl.TabStop = true;
			this.NoDefencesLbl.Text = "None";
			this.NoDefencesLbl.LinkClicked += new LinkLabelLinkClickedEventHandler(this.NoDefencesLbl_LinkClicked);
			this.AllDefencesLbl.AutoSize = true;
			this.AllDefencesLbl.Location = new Point(297, 53);
			this.AllDefencesLbl.Name = "AllDefencesLbl";
			this.AllDefencesLbl.Size = new Size(18, 13);
			this.AllDefencesLbl.TabIndex = 14;
			this.AllDefencesLbl.TabStop = true;
			this.AllDefencesLbl.Text = "All";
			this.AllDefencesLbl.LinkClicked += new LinkLabelLinkClickedEventHandler(this.AllDefencesLbl_LinkClicked);
			this.WillBox.AutoSize = true;
			this.WillBox.Location = new Point(248, 52);
			this.WillBox.Name = "WillBox";
			this.WillBox.Size = new Size(43, 17);
			this.WillBox.TabIndex = 13;
			this.WillBox.Text = "Will";
			this.WillBox.UseVisualStyleBackColor = true;
			this.RefBox.AutoSize = true;
			this.RefBox.Location = new Point(186, 52);
			this.RefBox.Name = "RefBox";
			this.RefBox.Size = new Size(56, 17);
			this.RefBox.TabIndex = 12;
			this.RefBox.Text = "Reflex";
			this.RefBox.UseVisualStyleBackColor = true;
			this.FortBox.AutoSize = true;
			this.FortBox.Location = new Point(113, 52);
			this.FortBox.Name = "FortBox";
			this.FortBox.Size = new Size(67, 17);
			this.FortBox.TabIndex = 11;
			this.FortBox.Text = "Fortitude";
			this.FortBox.UseVisualStyleBackColor = true;
			this.ACBox.AutoSize = true;
			this.ACBox.Location = new Point(68, 52);
			this.ACBox.Name = "ACBox";
			this.ACBox.Size = new Size(40, 17);
			this.ACBox.TabIndex = 10;
			this.ACBox.Text = "AC";
			this.ACBox.UseVisualStyleBackColor = true;
			this.DefenceModLbl.AutoSize = true;
			this.DefenceModLbl.Location = new Point(9, 28);
			this.DefenceModLbl.Name = "DefenceModLbl";
			this.DefenceModLbl.Size = new Size(46, 13);
			this.DefenceModLbl.TabIndex = 8;
			this.DefenceModLbl.Text = "Amount:";
			this.DefenceBtn.AutoSize = true;
			this.DefenceBtn.Location = new Point(3, 3);
			this.DefenceBtn.Name = "DefenceBtn";
			this.DefenceBtn.Size = new Size(158, 17);
			this.DefenceBtn.TabIndex = 7;
			this.DefenceBtn.TabStop = true;
			this.DefenceBtn.Text = "Apply a modifier to defences";
			this.DefenceBtn.UseVisualStyleBackColor = true;
			this.DefenceBtn.CheckedChanged += new EventHandler(this.EffectTypeChanged);
			this.DefenceModBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DefenceModBox.Location = new Point(68, 26);
			this.DefenceModBox.Minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				int.MinValue
			});
			this.DefenceModBox.Name = "DefenceModBox";
			this.DefenceModBox.Size = new Size(289, 20);
			this.DefenceModBox.TabIndex = 9;
			this.DamageLbl.AutoSize = true;
			this.DamageLbl.Location = new Point(9, 28);
			this.DamageLbl.Name = "DamageLbl";
			this.DamageLbl.Size = new Size(46, 13);
			this.DamageLbl.TabIndex = 3;
			this.DamageLbl.Text = "Amount:";
			this.ConditionPanel.Controls.Add(this.ConditionBox);
			this.ConditionPanel.Controls.Add(this.ConditionBtn);
			this.ConditionPanel.Dock = DockStyle.Top;
			this.ConditionPanel.Location = new Point(0, 0);
			this.ConditionPanel.Name = "ConditionPanel";
			this.ConditionPanel.Size = new Size(369, 55);
			this.ConditionPanel.TabIndex = 27;
			this.DamagePanel.Controls.Add(this.DamageBox);
			this.DamagePanel.Controls.Add(this.DamageLbl);
			this.DamagePanel.Controls.Add(this.TypeLbl);
			this.DamagePanel.Controls.Add(this.DamageTypeBox);
			this.DamagePanel.Controls.Add(this.DamageBtn);
			this.DamagePanel.Dock = DockStyle.Top;
			this.DamagePanel.Location = new Point(0, 55);
			this.DamagePanel.Name = "DamagePanel";
			this.DamagePanel.Size = new Size(369, 82);
			this.DamagePanel.TabIndex = 28;
			this.DefencePanel.Controls.Add(this.DefenceModBox);
			this.DefencePanel.Controls.Add(this.FortBox);
			this.DefencePanel.Controls.Add(this.ACBox);
			this.DefencePanel.Controls.Add(this.RefBox);
			this.DefencePanel.Controls.Add(this.DefenceModLbl);
			this.DefencePanel.Controls.Add(this.WillBox);
			this.DefencePanel.Controls.Add(this.AllDefencesLbl);
			this.DefencePanel.Controls.Add(this.NoDefencesLbl);
			this.DefencePanel.Controls.Add(this.DefenceBtn);
			this.DefencePanel.Dock = DockStyle.Top;
			this.DefencePanel.Location = new Point(0, 137);
			this.DefencePanel.Name = "DefencePanel";
			this.DefencePanel.Size = new Size(369, 78);
			this.DefencePanel.TabIndex = 29;
			this.DamageModPanel.Controls.Add(this.DamageModDirBox);
			this.DamageModPanel.Controls.Add(this.DamageModValueBox);
			this.DamageModPanel.Controls.Add(this.DamageModValueLbl);
			this.DamageModPanel.Controls.Add(this.DamageModTypeBox);
			this.DamageModPanel.Controls.Add(this.DamageModTypeLbl);
			this.DamageModPanel.Controls.Add(this.DamageModBtn);
			this.DamageModPanel.Dock = DockStyle.Top;
			this.DamageModPanel.Location = new Point(0, 215);
			this.DamageModPanel.Name = "DamageModPanel";
			this.DamageModPanel.Size = new Size(369, 111);
			this.DamageModPanel.TabIndex = 30;
			this.RegenPanel.Controls.Add(this.RegenValueBox);
			this.RegenPanel.Controls.Add(this.RegenValueLbl);
			this.RegenPanel.Controls.Add(this.RegenConditionsLbl);
			this.RegenPanel.Controls.Add(this.RegenConditionsBox);
			this.RegenPanel.Controls.Add(this.RegenBtn);
			this.RegenPanel.Dock = DockStyle.Top;
			this.RegenPanel.Location = new Point(0, 326);
			this.RegenPanel.Name = "RegenPanel";
			this.RegenPanel.Size = new Size(369, 82);
			this.RegenPanel.TabIndex = 31;
			this.PropertiesPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.PropertiesPanel.Controls.Add(this.AuraPanel);
			this.PropertiesPanel.Controls.Add(this.RegenPanel);
			this.PropertiesPanel.Controls.Add(this.DamageModPanel);
			this.PropertiesPanel.Controls.Add(this.DefencePanel);
			this.PropertiesPanel.Controls.Add(this.DamagePanel);
			this.PropertiesPanel.Controls.Add(this.ConditionPanel);
			this.PropertiesPanel.Location = new Point(12, 12);
			this.PropertiesPanel.Name = "PropertiesPanel";
			this.PropertiesPanel.Size = new Size(369, 499);
			this.PropertiesPanel.TabIndex = 32;
			this.AuraPanel.Controls.Add(this.AuraRadiusBox);
			this.AuraPanel.Controls.Add(this.AuraRadiusLbl);
			this.AuraPanel.Controls.Add(this.AuraDetailsLbl);
			this.AuraPanel.Controls.Add(this.AuraDetailsBox);
			this.AuraPanel.Controls.Add(this.AuraBtn);
			this.AuraPanel.Dock = DockStyle.Top;
			this.AuraPanel.Location = new Point(0, 408);
			this.AuraPanel.Name = "AuraPanel";
			this.AuraPanel.Size = new Size(369, 82);
			this.AuraPanel.TabIndex = 32;
			this.AuraRadiusBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.AuraRadiusBox.Location = new Point(74, 26);
			NumericUpDown arg_1876_0 = this.AuraRadiusBox;
			int[] array2 = new int[4];
			array2[0] = 20;
			arg_1876_0.Maximum = new decimal(array2);
			NumericUpDown arg_1895_0 = this.AuraRadiusBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_1895_0.Minimum = new decimal(array3);
			this.AuraRadiusBox.Name = "AuraRadiusBox";
			this.AuraRadiusBox.Size = new Size(283, 20);
			this.AuraRadiusBox.TabIndex = 24;
			NumericUpDown arg_18E8_0 = this.AuraRadiusBox;
			int[] array4 = new int[4];
			array4[0] = 1;
			arg_18E8_0.Value = new decimal(array4);
			this.AuraRadiusLbl.AutoSize = true;
			this.AuraRadiusLbl.Location = new Point(9, 28);
			this.AuraRadiusLbl.Name = "AuraRadiusLbl";
			this.AuraRadiusLbl.Size = new Size(43, 13);
			this.AuraRadiusLbl.TabIndex = 23;
			this.AuraRadiusLbl.Text = "Radius:";
			this.AuraDetailsLbl.AutoSize = true;
			this.AuraDetailsLbl.Location = new Point(9, 55);
			this.AuraDetailsLbl.Name = "AuraDetailsLbl";
			this.AuraDetailsLbl.Size = new Size(42, 13);
			this.AuraDetailsLbl.TabIndex = 25;
			this.AuraDetailsLbl.Text = "Details:";
			this.AuraDetailsBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.AuraDetailsBox.Location = new Point(74, 52);
			this.AuraDetailsBox.Name = "AuraDetailsBox";
			this.AuraDetailsBox.Size = new Size(283, 20);
			this.AuraDetailsBox.TabIndex = 26;
			this.AuraBtn.AutoSize = true;
			this.AuraBtn.Location = new Point(3, 3);
			this.AuraBtn.Name = "AuraBtn";
			this.AuraBtn.Size = new Size(75, 17);
			this.AuraBtn.TabIndex = 22;
			this.AuraBtn.TabStop = true;
			this.AuraBtn.Text = "Apply aura";
			this.AuraBtn.UseVisualStyleBackColor = true;
			this.AuraBtn.CheckedChanged += new EventHandler(this.EffectTypeChanged);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(393, 659);
			base.Controls.Add(this.PropertiesPanel);
			base.Controls.Add(this.DurationGroup);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "EffectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Effect";
			base.Shown += new EventHandler(this.EffectForm_Shown);
			((ISupportInitialize)this.DamageBox).EndInit();
			((ISupportInitialize)this.ModBox).EndInit();
			this.DurationGroup.ResumeLayout(false);
			this.DurationGroup.PerformLayout();
			((ISupportInitialize)this.RegenValueBox).EndInit();
			((ISupportInitialize)this.DamageModValueBox).EndInit();
			((ISupportInitialize)this.DefenceModBox).EndInit();
			this.ConditionPanel.ResumeLayout(false);
			this.ConditionPanel.PerformLayout();
			this.DamagePanel.ResumeLayout(false);
			this.DamagePanel.PerformLayout();
			this.DefencePanel.ResumeLayout(false);
			this.DefencePanel.PerformLayout();
			this.DamageModPanel.ResumeLayout(false);
			this.DamageModPanel.PerformLayout();
			this.RegenPanel.ResumeLayout(false);
			this.RegenPanel.PerformLayout();
			this.PropertiesPanel.ResumeLayout(false);
			this.AuraPanel.ResumeLayout(false);
			this.AuraPanel.PerformLayout();
			((ISupportInitialize)this.AuraRadiusBox).EndInit();
			base.ResumeLayout(false);
		}
	}
}
