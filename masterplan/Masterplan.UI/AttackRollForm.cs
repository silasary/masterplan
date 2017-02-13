using Masterplan.Data;
using Masterplan.Events;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class AttackRollForm : Form
	{
		private CreaturePower fPower;

		private Encounter fEncounter;

		private bool fAddedCombatant;

		private List<Pair<CombatData, int>> fRolls = new List<Pair<CombatData, int>>();

		private IContainer components;

		private Button OKBtn;

		private WebBrowser PowerBrowser;

		private ListView RollList;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private TabControl Pages;

		private TabPage AttackPage;

		private TabPage DamagePage;

		private Label MissLbl;

		private Label CritLbl;

		private NumericUpDown DamageBox;

		private Label HitLbl;

		private Label DamageExpLbl;

		private Label DamageInfoLbl;

		private ColumnHeader columnHeader4;

		private Label MissValueLbl;

		private Label CritValueLbl;

		private Button RollDamageBtn;

		private SplitContainer Splitter;

		private CheckBox ApplyDamageBox;

		public Pair<CombatData, int> SelectedRoll
		{
			get
			{
				if (this.RollList.SelectedItems.Count != 0)
				{
					return this.RollList.SelectedItems[0].Tag as Pair<CombatData, int>;
				}
				return null;
			}
		}

		public AttackRollForm(CreaturePower power, Encounter enc)
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.fPower = power;
			this.fEncounter = enc;
			this.add_attack_roll(null);
			this.update_damage();
			this.RollDamageBtn_Click(null, null);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.ApplyDamageBox.Visible = this.fAddedCombatant;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			if (this.ApplyDamageBox.Visible && this.ApplyDamageBox.Checked)
			{
				this.apply_damage();
			}
		}

		private void PowerBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "opponent")
			{
				e.Cancel = true;
				Guid id = new Guid(e.Url.LocalPath);
				CombatData cd = this.fEncounter.FindCombatData(id);
				this.add_attack_roll(cd);
			}
			if (e.Url.Scheme == "hero")
			{
				e.Cancel = true;
				Guid hero_id = new Guid(e.Url.LocalPath);
				Hero hero = Session.Project.FindHero(hero_id);
				if (hero != null)
				{
					CombatData combatData = hero.CombatData;
					this.add_attack_roll(combatData);
				}
			}
			if (e.Url.Scheme == "target")
			{
				e.Cancel = true;
				this.add_attack_roll(null);
			}
		}

		private void RollList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedRoll != null)
			{
				int second = Session.Dice(1, 20);
				this.SelectedRoll.Second = second;
				this.update_list();
			}
		}

		private void RollDamageBtn_Click(object sender, EventArgs e)
		{
			DiceExpression diceExpression = DiceExpression.Parse(this.DamageExpLbl.Text);
			if (diceExpression != null)
			{
				int value = diceExpression.Evaluate();
				this.DamageBox.Value = value;
			}
		}

		private void DamageBox_ValueChanged(object sender, EventArgs e)
		{
			int num = (int)this.DamageBox.Value;
			int num2 = num / 2;
			this.MissValueLbl.Text = num2.ToString();
		}

		private void update_power()
		{
			List<string> list = new List<string>();
			list.AddRange(HTML.GetHead(this.fPower.Name, "", DisplaySize.Small));
			list.Add("<BODY>");
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			list.AddRange(this.fPower.AsHTML(null, CardMode.View, false));
			list.Add("</TABLE>");
			list.Add("</P>");
			list.Add("<P class=instruction align=left>");
			list.Add("Click to add an attack roll for:");
			string text = "";
			foreach (Hero current in Session.Project.Heroes)
			{
				CombatData combatData = current.CombatData;
				if (!this.roll_exists(current.ID) && current.GetState(combatData.Damage) != CreatureState.Defeated)
				{
					if (text != "")
					{
						text += " | ";
					}
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
						"<A href=hero:",
						current.ID,
						">",
						current.Name,
						"</A>"
					});
				}
			}
			if (text != "")
			{
				list.Add("<BR>");
				list.Add(text);
			}
			string text2 = "";
			foreach (EncounterSlot current2 in this.fEncounter.Slots)
			{
				foreach (CombatData current3 in current2.CombatData)
				{
					if (!this.roll_exists(current3.ID) && current2.GetState(current3) != CreatureState.Defeated)
					{
						if (text2 != "")
						{
							text2 += " | ";
						}
						object obj2 = text2;
						text2 = string.Concat(new object[]
						{
							obj2,
							"<A href=opponent:",
							current3.ID,
							">",
							current3.DisplayName,
							"</A>"
						});
					}
				}
			}
			if (text2 != "")
			{
				list.Add("<BR>");
				list.Add(text2);
			}
			list.Add("<BR>");
			list.Add("<A href=target:blank>An unnamed target</A>");
			list.Add("</P>");
			list.Add("</BODY>");
			list.Add("</HTML>");
			this.PowerBrowser.DocumentText = HTML.Concatenate(list);
		}

		private void update_list()
		{
			this.RollList.Items.Clear();
			foreach (Pair<CombatData, int> current in this.fRolls)
			{
				int second = current.Second;
				int num = (this.fPower.Attack != null) ? this.fPower.Attack.Bonus : 0;
				int num2 = second + num;
				ListViewItem listViewItem = this.RollList.Items.Add((current.First != null) ? current.First.DisplayName : "Roll");
				listViewItem.SubItems.Add(second.ToString());
				listViewItem.SubItems.Add(num.ToString());
				listViewItem.SubItems.Add(num2.ToString());
				bool flag = true;
				if (current.First != null && this.fPower.Attack != null)
				{
					int num3 = 0;
					Hero hero = Session.Project.FindHero(current.First.ID);
					if (hero != null)
					{
						switch (this.fPower.Attack.Defence)
						{
						case DefenceType.AC:
							num3 = hero.AC;
							break;
						case DefenceType.Fortitude:
							num3 = hero.Fortitude;
							break;
						case DefenceType.Reflex:
							num3 = hero.Reflex;
							break;
						case DefenceType.Will:
							num3 = hero.Will;
							break;
						}
					}
					else
					{
						EncounterSlot encounterSlot = this.fEncounter.FindSlot(current.First);
						switch (this.fPower.Attack.Defence)
						{
						case DefenceType.AC:
							num3 = encounterSlot.Card.AC;
							break;
						case DefenceType.Fortitude:
							num3 = encounterSlot.Card.Fortitude;
							break;
						case DefenceType.Reflex:
							num3 = encounterSlot.Card.Reflex;
							break;
						case DefenceType.Will:
							num3 = encounterSlot.Card.Will;
							break;
						}
					}
					foreach (OngoingCondition current2 in current.First.Conditions)
					{
						if (current2.Type == OngoingType.DefenceModifier && current2.Defences.Contains(this.fPower.Attack.Defence))
						{
							num3 += current2.DefenceMod;
						}
					}
					flag = (num2 >= num3);
				}
				if (second == 20)
				{
					listViewItem.Font = new Font(listViewItem.Font, listViewItem.Font.Style | FontStyle.Bold);
				}
				else if (second == 1)
				{
					listViewItem.ForeColor = Color.Red;
				}
				else if (!flag)
				{
					listViewItem.ForeColor = SystemColors.GrayText;
				}
				listViewItem.Tag = current;
			}
		}

		private void update_damage()
		{
			string damage = this.fPower.Damage;
			if (damage == "")
			{
				this.Pages.TabPages.Remove(this.DamagePage);
				return;
			}
			DiceExpression diceExpression = DiceExpression.Parse(damage);
			this.DamageExpLbl.Text = damage;
			this.CritValueLbl.Text = diceExpression.Maximum.ToString();
		}

		private void add_attack_roll(CombatData cd)
		{
			if (cd != null && this.fRolls.Count == 1 && this.fRolls[0].First == null)
			{
				this.fRolls.Clear();
			}
			int second = Session.Dice(1, 20);
			this.fRolls.Add(new Pair<CombatData, int>(cd, second));
			if (cd != null)
			{
				this.fAddedCombatant = true;
			}
			this.update_list();
			this.update_power();
		}

		private bool roll_exists(Guid id)
		{
			foreach (Pair<CombatData, int> current in this.fRolls)
			{
				if (current.First != null && current.First.ID == id)
				{
					return true;
				}
			}
			return false;
		}

		private void apply_damage()
		{
			foreach (ListViewItem listViewItem in this.RollList.Items)
			{
				Pair<CombatData, int> pair = listViewItem.Tag as Pair<CombatData, int>;
				if (pair.First != null)
				{
					int num = 0;
					if (pair.Second == 20)
					{
						num = int.Parse(this.CritValueLbl.Text);
					}
					else if (listViewItem.ForeColor == SystemColors.WindowText)
					{
						num = (int)this.DamageBox.Value;
					}
					if (num != 0)
					{
						Array values = Enum.GetValues(typeof(DamageType));
						List<DamageType> list = new List<DamageType>();
						foreach (DamageType damageType in values)
						{
							string text = this.fPower.Details.ToLower();
							string value = damageType.ToString().ToLower();
							if (text.Contains(value))
							{
								list.Add(damageType);
							}
						}
						EncounterSlot encounterSlot = this.fEncounter.FindSlot(pair.First);
						EncounterCard card = (encounterSlot != null) ? encounterSlot.Card : null;
						DamageForm.DoDamage(pair.First, card, num, list, false);
					}
				}
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
			this.OKBtn = new Button();
			this.PowerBrowser = new WebBrowser();
			this.RollList = new ListView();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.Pages = new TabControl();
			this.AttackPage = new TabPage();
			this.DamagePage = new TabPage();
			this.RollDamageBtn = new Button();
			this.MissValueLbl = new Label();
			this.CritValueLbl = new Label();
			this.MissLbl = new Label();
			this.CritLbl = new Label();
			this.DamageBox = new NumericUpDown();
			this.HitLbl = new Label();
			this.DamageExpLbl = new Label();
			this.DamageInfoLbl = new Label();
			this.Splitter = new SplitContainer();
			this.ApplyDamageBox = new CheckBox();
			this.Pages.SuspendLayout();
			this.AttackPage.SuspendLayout();
			this.DamagePage.SuspendLayout();
			((ISupportInitialize)this.DamageBox).BeginInit();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(280, 345);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 2;
			this.OKBtn.Text = "Close";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.PowerBrowser.AllowWebBrowserDrop = false;
			this.PowerBrowser.Dock = DockStyle.Fill;
			this.PowerBrowser.IsWebBrowserContextMenuEnabled = false;
			this.PowerBrowser.Location = new Point(0, 0);
			this.PowerBrowser.MinimumSize = new Size(20, 20);
			this.PowerBrowser.Name = "PowerBrowser";
			this.PowerBrowser.ScriptErrorsSuppressed = true;
			this.PowerBrowser.Size = new Size(343, 163);
			this.PowerBrowser.TabIndex = 0;
			this.PowerBrowser.WebBrowserShortcutsEnabled = false;
			this.PowerBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.PowerBrowser_Navigating);
			this.RollList.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader4,
				this.columnHeader1,
				this.columnHeader2,
				this.columnHeader3
			});
			this.RollList.Dock = DockStyle.Fill;
			this.RollList.FullRowSelect = true;
			this.RollList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.RollList.HideSelection = false;
			this.RollList.Location = new Point(3, 3);
			this.RollList.Name = "RollList";
			this.RollList.Size = new Size(329, 128);
			this.RollList.TabIndex = 0;
			this.RollList.UseCompatibleStateImageBehavior = false;
			this.RollList.View = View.Details;
			this.RollList.DoubleClick += new EventHandler(this.RollList_DoubleClick);
			this.columnHeader4.Text = "Target";
			this.columnHeader4.Width = 120;
			this.columnHeader1.Text = "Roll";
			this.columnHeader1.TextAlign = HorizontalAlignment.Right;
			this.columnHeader2.Text = "Bonus";
			this.columnHeader2.TextAlign = HorizontalAlignment.Right;
			this.columnHeader3.Text = "Total";
			this.columnHeader3.TextAlign = HorizontalAlignment.Right;
			this.Pages.Controls.Add(this.AttackPage);
			this.Pages.Controls.Add(this.DamagePage);
			this.Pages.Dock = DockStyle.Fill;
			this.Pages.Location = new Point(0, 0);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(343, 160);
			this.Pages.TabIndex = 0;
			this.AttackPage.Controls.Add(this.RollList);
			this.AttackPage.Location = new Point(4, 22);
			this.AttackPage.Name = "AttackPage";
			this.AttackPage.Padding = new Padding(3);
			this.AttackPage.Size = new Size(335, 134);
			this.AttackPage.TabIndex = 0;
			this.AttackPage.Text = "Attack Rolls";
			this.AttackPage.UseVisualStyleBackColor = true;
			this.DamagePage.Controls.Add(this.RollDamageBtn);
			this.DamagePage.Controls.Add(this.MissValueLbl);
			this.DamagePage.Controls.Add(this.CritValueLbl);
			this.DamagePage.Controls.Add(this.MissLbl);
			this.DamagePage.Controls.Add(this.CritLbl);
			this.DamagePage.Controls.Add(this.DamageBox);
			this.DamagePage.Controls.Add(this.HitLbl);
			this.DamagePage.Controls.Add(this.DamageExpLbl);
			this.DamagePage.Controls.Add(this.DamageInfoLbl);
			this.DamagePage.Location = new Point(4, 22);
			this.DamagePage.Name = "DamagePage";
			this.DamagePage.Padding = new Padding(3);
			this.DamagePage.Size = new Size(335, 134);
			this.DamagePage.TabIndex = 1;
			this.DamagePage.Text = "Damage Rolls";
			this.DamagePage.UseVisualStyleBackColor = true;
			this.RollDamageBtn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.RollDamageBtn.Location = new Point(254, 33);
			this.RollDamageBtn.Name = "RollDamageBtn";
			this.RollDamageBtn.Size = new Size(75, 23);
			this.RollDamageBtn.TabIndex = 9;
			this.RollDamageBtn.Text = "Reroll";
			this.RollDamageBtn.UseVisualStyleBackColor = true;
			this.RollDamageBtn.Click += new EventHandler(this.RollDamageBtn_Click);
			this.MissValueLbl.AutoSize = true;
			this.MissValueLbl.Location = new Point(135, 90);
			this.MissValueLbl.Name = "MissValueLbl";
			this.MissValueLbl.Size = new Size(33, 13);
			this.MissValueLbl.TabIndex = 8;
			this.MissValueLbl.Text = "[miss]";
			this.CritValueLbl.AutoSize = true;
			this.CritValueLbl.Location = new Point(135, 64);
			this.CritValueLbl.Name = "CritValueLbl";
			this.CritValueLbl.Size = new Size(27, 13);
			this.CritValueLbl.TabIndex = 7;
			this.CritValueLbl.Text = "[crit]";
			this.MissLbl.AutoSize = true;
			this.MissLbl.Location = new Point(6, 90);
			this.MissLbl.Name = "MissLbl";
			this.MissLbl.Size = new Size(74, 13);
			this.MissLbl.TabIndex = 6;
			this.MissLbl.Text = "On Miss (half):";
			this.CritLbl.AutoSize = true;
			this.CritLbl.Location = new Point(6, 64);
			this.CritLbl.Name = "CritLbl";
			this.CritLbl.Size = new Size(86, 13);
			this.CritLbl.TabIndex = 4;
			this.CritLbl.Text = "On Critical (max):";
			this.DamageBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DamageBox.Location = new Point(138, 36);
			this.DamageBox.Name = "DamageBox";
			this.DamageBox.Size = new Size(110, 20);
			this.DamageBox.TabIndex = 3;
			this.DamageBox.ValueChanged += new EventHandler(this.DamageBox_ValueChanged);
			this.HitLbl.AutoSize = true;
			this.HitLbl.Location = new Point(6, 38);
			this.HitLbl.Name = "HitLbl";
			this.HitLbl.Size = new Size(40, 13);
			this.HitLbl.TabIndex = 2;
			this.HitLbl.Text = "On Hit:";
			this.DamageExpLbl.AutoSize = true;
			this.DamageExpLbl.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.DamageExpLbl.Location = new Point(135, 13);
			this.DamageExpLbl.Name = "DamageExpLbl";
			this.DamageExpLbl.Size = new Size(38, 13);
			this.DamageExpLbl.TabIndex = 1;
			this.DamageExpLbl.Text = "[dmg]";
			this.DamageInfoLbl.AutoSize = true;
			this.DamageInfoLbl.Location = new Point(6, 13);
			this.DamageInfoLbl.Name = "DamageInfoLbl";
			this.DamageInfoLbl.Size = new Size(50, 13);
			this.DamageInfoLbl.TabIndex = 0;
			this.DamageInfoLbl.Text = "Damage:";
			this.Splitter.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Orientation = Orientation.Horizontal;
			this.Splitter.Panel1.Controls.Add(this.PowerBrowser);
			this.Splitter.Panel2.Controls.Add(this.Pages);
			this.Splitter.Size = new Size(343, 327);
			this.Splitter.SplitterDistance = 163;
			this.Splitter.TabIndex = 0;
			this.ApplyDamageBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.ApplyDamageBox.AutoSize = true;
			this.ApplyDamageBox.Location = new Point(12, 349);
			this.ApplyDamageBox.Name = "ApplyDamageBox";
			this.ApplyDamageBox.Size = new Size(136, 17);
			this.ApplyDamageBox.TabIndex = 1;
			this.ApplyDamageBox.Text = "Apply damage on close";
			this.ApplyDamageBox.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(367, 380);
			base.Controls.Add(this.ApplyDamageBox);
			base.Controls.Add(this.Splitter);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AttackRollForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Attack Roll";
			this.Pages.ResumeLayout(false);
			this.AttackPage.ResumeLayout(false);
			this.DamagePage.ResumeLayout(false);
			this.DamagePage.PerformLayout();
			((ISupportInitialize)this.DamageBox).EndInit();
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
