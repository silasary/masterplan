using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class GroupInitiativeForm : Form
	{
		private IContainer components;

		private Button CloseBtn;

		private Label InfoLbl;

		private ListView CombatantList;

		private ColumnHeader CombatantHdr;

		private ColumnHeader InitHdr;

		private Dictionary<string, List<CombatData>> fCombatants;

		private Encounter fEncounter;

		public Dictionary<string, List<CombatData>> Combatants
		{
			get
			{
				return this.fCombatants;
			}
		}

		public List<CombatData> SelectedCombatantGroup
		{
			get
			{
				if (this.CombatantList.SelectedItems.Count != 0)
				{
					return this.CombatantList.SelectedItems[0].Tag as List<CombatData>;
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
			this.CloseBtn = new Button();
			this.InfoLbl = new Label();
			this.CombatantList = new ListView();
			this.CombatantHdr = new ColumnHeader();
			this.InitHdr = new ColumnHeader();
			base.SuspendLayout();
			this.CloseBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CloseBtn.DialogResult = DialogResult.OK;
			this.CloseBtn.Location = new Point(292, 301);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new Size(75, 23);
			this.CloseBtn.TabIndex = 2;
			this.CloseBtn.Text = "OK";
			this.CloseBtn.UseVisualStyleBackColor = true;
			this.InfoLbl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.InfoLbl.Location = new Point(12, 9);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(355, 22);
			this.InfoLbl.TabIndex = 0;
			this.InfoLbl.Text = "Double-click on a combatant in the list to set its initiative score.";
			this.CombatantList.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.CombatantList.Columns.AddRange(new ColumnHeader[]
			{
				this.CombatantHdr,
				this.InitHdr
			});
			this.CombatantList.FullRowSelect = true;
			this.CombatantList.HideSelection = false;
			this.CombatantList.Location = new Point(12, 34);
			this.CombatantList.MultiSelect = false;
			this.CombatantList.Name = "CombatantList";
			this.CombatantList.Size = new Size(355, 261);
			this.CombatantList.TabIndex = 1;
			this.CombatantList.UseCompatibleStateImageBehavior = false;
			this.CombatantList.View = View.Details;
			this.CombatantList.DoubleClick += new EventHandler(this.CombatantList_DoubleClick);
			this.CombatantHdr.Text = "Combatant";
			this.CombatantHdr.Width = 234;
			this.InitHdr.Text = "Initiative";
			this.InitHdr.TextAlign = HorizontalAlignment.Right;
			this.InitHdr.Width = 68;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(379, 336);
			base.Controls.Add(this.CombatantList);
			base.Controls.Add(this.InfoLbl);
			base.Controls.Add(this.CloseBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "GroupInitiativeForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Set Initiative Scores";
			base.ResumeLayout(false);
		}

		public GroupInitiativeForm(Dictionary<string, List<CombatData>> combatants, Encounter enc)
		{
			this.InitializeComponent();
			this.fCombatants = combatants;
			this.fEncounter = enc;
			this.update_list();
		}

		private void CombatantList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedCombatantGroup != null)
			{
				int bonus = 0;
				CombatData combatData = this.SelectedCombatantGroup[0];
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(combatData);
				if (encounterSlot != null)
				{
					bonus = encounterSlot.Card.Initiative;
				}
				else
				{
					Hero hero = Session.Project.FindHero(combatData.ID);
					if (hero != null)
					{
						bonus = hero.InitBonus;
					}
					Trap trap = this.fEncounter.FindTrap(combatData.ID);
					if (trap != null)
					{
						bonus = trap.Initiative;
					}
				}
				InitiativeForm initiativeForm = new InitiativeForm(bonus, combatData.Initiative);
				if (initiativeForm.ShowDialog() == DialogResult.OK)
				{
					foreach (CombatData current in this.SelectedCombatantGroup)
					{
						current.Initiative = initiativeForm.Score;
					}
					this.update_list();
				}
			}
		}

		private void update_list()
		{
			this.CombatantList.Items.Clear();
			foreach (string current in this.fCombatants.Keys)
			{
				ListViewItem listViewItem = this.CombatantList.Items.Add(current);
				List<CombatData> list = this.fCombatants[current];
				CombatData combatData = list[0];
				if (combatData.Initiative == -2147483648)
				{
					ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems.Add("(not set)");
					listViewSubItem.ForeColor = SystemColors.GrayText;
				}
				else
				{
					listViewItem.SubItems.Add(combatData.Initiative.ToString());
				}
				listViewItem.UseItemStyleForSubItems = false;
				listViewItem.Tag = list;
			}
		}
	}
}
