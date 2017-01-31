using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class EncounterPanel : UserControl
	{
		private IContainer components;

		private ToolStrip Toolbar;

		private ToolStripButton EditBtn;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripLabel XPLbl;

		private ToolStripLabel DiffLbl;

		private ListView ItemList;

		private ColumnHeader CreatureHdr;

		private ColumnHeader CountHdr;

		private ColumnHeader RoleHdr;

		private ColumnHeader XPHdr;

		private ToolStripButton RunBtn;

		private Encounter fEncounter;

		private int fPartyLevel = Session.Project.Party.Level;

		public Encounter Encounter
		{
			get
			{
				return this.fEncounter;
			}
			set
			{
				this.fEncounter = value;
				this.update_view();
			}
		}

		public int PartyLevel
		{
			get
			{
				return this.fPartyLevel;
			}
			set
			{
				this.fPartyLevel = value;
				this.update_view();
			}
		}

		public EncounterSlot SelectedSlot
		{
			get
			{
				if (this.ItemList.SelectedItems.Count != 0)
				{
					return this.ItemList.SelectedItems[0].Tag as EncounterSlot;
				}
				return null;
			}
		}

		public Trap SelectedTrap
		{
			get
			{
				if (this.ItemList.SelectedItems.Count != 0)
				{
					return this.ItemList.SelectedItems[0].Tag as Trap;
				}
				return null;
			}
		}

		public SkillChallenge SelectedChallenge
		{
			get
			{
				if (this.ItemList.SelectedItems.Count != 0)
				{
					return this.ItemList.SelectedItems[0].Tag as SkillChallenge;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(EncounterPanel));
			this.Toolbar = new ToolStrip();
			this.EditBtn = new ToolStripButton();
			this.RunBtn = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.XPLbl = new ToolStripLabel();
			this.DiffLbl = new ToolStripLabel();
			this.ItemList = new ListView();
			this.CreatureHdr = new ColumnHeader();
			this.CountHdr = new ColumnHeader();
			this.RoleHdr = new ColumnHeader();
			this.XPHdr = new ColumnHeader();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.EditBtn,
				this.RunBtn,
				this.toolStripSeparator2,
				this.XPLbl,
				this.DiffLbl
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(435, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)componentResourceManager.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new Size(105, 22);
			this.EditBtn.Text = "Encounter Builder";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			this.RunBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RunBtn.Image = (Image)componentResourceManager.GetObject("RunBtn.Image");
			this.RunBtn.ImageTransparentColor = Color.Magenta;
			this.RunBtn.Name = "RunBtn";
			this.RunBtn.Size = new Size(89, 22);
			this.RunBtn.Text = "Run Encounter";
			this.RunBtn.Click += new EventHandler(this.RunBtn_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 25);
			this.XPLbl.Name = "XPLbl";
			this.XPLbl.Size = new Size(78, 22);
			this.XPLbl.Text = "[N XP (CL N)]";
			this.DiffLbl.Name = "DiffLbl";
			this.DiffLbl.Size = new Size(62, 22);
			this.DiffLbl.Text = "[difficulty]";
			this.ItemList.Columns.AddRange(new ColumnHeader[]
			{
				this.CreatureHdr,
				this.RoleHdr,
				this.CountHdr,
				this.XPHdr
			});
			this.ItemList.Dock = DockStyle.Fill;
			this.ItemList.FullRowSelect = true;
			this.ItemList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.ItemList.HideSelection = false;
			this.ItemList.Location = new Point(0, 25);
			this.ItemList.MultiSelect = false;
			this.ItemList.Name = "ItemList";
			this.ItemList.Size = new Size(435, 181);
			this.ItemList.TabIndex = 1;
			this.ItemList.UseCompatibleStateImageBehavior = false;
			this.ItemList.View = View.Details;
			this.ItemList.DoubleClick += new EventHandler(this.CreatureList_DoubleClick);
			this.CreatureHdr.Text = "Creature";
			this.CreatureHdr.Width = 150;
			this.CountHdr.Text = "Count";
			this.CountHdr.TextAlign = HorizontalAlignment.Right;
			this.RoleHdr.Text = "Role";
			this.RoleHdr.Width = 150;
			this.XPHdr.Text = "XP";
			this.XPHdr.TextAlign = HorizontalAlignment.Right;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.ItemList);
			base.Controls.Add(this.Toolbar);
			base.Name = "EncounterPanel";
			base.Size = new Size(435, 206);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public EncounterPanel()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RunBtn.Enabled = (this.fEncounter.Count != 0 || this.fEncounter.Traps.Count != 0 || this.fEncounter.SkillChallenges.Count != 0);
		}

		public void Edit()
		{
			this.EditBtn_Click(null, null);
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			EncounterBuilderForm encounterBuilderForm = new EncounterBuilderForm(this.fEncounter, this.fPartyLevel, false);
			if (encounterBuilderForm.ShowDialog() == DialogResult.OK)
			{
				this.fEncounter.Slots = encounterBuilderForm.Encounter.Slots;
				this.fEncounter.Traps = encounterBuilderForm.Encounter.Traps;
				this.fEncounter.SkillChallenges = encounterBuilderForm.Encounter.SkillChallenges;
				this.fEncounter.CustomTokens = encounterBuilderForm.Encounter.CustomTokens;
				this.fEncounter.MapID = encounterBuilderForm.Encounter.MapID;
				this.fEncounter.MapAreaID = encounterBuilderForm.Encounter.MapAreaID;
				this.fEncounter.Notes = encounterBuilderForm.Encounter.Notes;
				this.fEncounter.Waves = encounterBuilderForm.Encounter.Waves;
				this.update_view();
			}
		}

		private void RunBtn_Click(object sender, EventArgs e)
		{
			CombatForm combatForm = new CombatForm(new CombatState
			{
				Encounter = this.fEncounter,
				PartyLevel = this.fPartyLevel
			});
			combatForm.Show();
		}

		private void update_view()
		{
			this.ItemList.Items.Clear();
			foreach (EncounterSlot current in this.fEncounter.Slots)
			{
				ListViewItem listViewItem = this.ItemList.Items.Add(current.Card.Title);
				listViewItem.SubItems.Add(current.Card.Info);
				listViewItem.SubItems.Add(current.CombatData.Count.ToString());
				listViewItem.SubItems.Add(current.XP.ToString());
				listViewItem.Tag = current;
				ICreature creature = Session.FindCreature(current.Card.CreatureID, SearchType.Global);
				Difficulty threatDifficulty = AI.GetThreatDifficulty(creature.Level + current.Card.LevelAdjustment, this.fPartyLevel);
				if (threatDifficulty == Difficulty.Trivial)
				{
					listViewItem.ForeColor = Color.Green;
				}
				if (threatDifficulty == Difficulty.Extreme)
				{
					listViewItem.ForeColor = Color.Red;
				}
			}
			foreach (Trap current2 in this.fEncounter.Traps)
			{
				ListViewItem listViewItem2 = this.ItemList.Items.Add(current2.Name);
				listViewItem2.SubItems.Add(current2.Info);
				listViewItem2.SubItems.Add("1");
				listViewItem2.SubItems.Add(Experience.GetCreatureXP(current2.Level).ToString());
				listViewItem2.Tag = current2;
			}
			foreach (SkillChallenge current3 in this.fEncounter.SkillChallenges)
			{
				ListViewItem listViewItem3 = this.ItemList.Items.Add(current3.Name);
				listViewItem3.SubItems.Add(current3.Info);
				listViewItem3.SubItems.Add("1");
				listViewItem3.SubItems.Add(current3.GetXP().ToString());
				listViewItem3.Tag = current3;
			}
			if (this.ItemList.Items.Count == 0)
			{
				ListViewItem listViewItem4 = this.ItemList.Items.Add("(none)");
				listViewItem4.ForeColor = SystemColors.GrayText;
			}
			this.ItemList.Sort();
			this.XPLbl.Text = this.fEncounter.GetXP() + " XP";
			this.DiffLbl.Text = "Difficulty: " + this.fEncounter.GetDifficulty(this.fPartyLevel, Session.Project.Party.Size);
		}

		private void CreatureList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedSlot != null)
			{
				CreatureDetailsForm creatureDetailsForm = new CreatureDetailsForm(this.SelectedSlot.Card);
				creatureDetailsForm.ShowDialog();
			}
			if (this.SelectedTrap != null)
			{
				TrapDetailsForm trapDetailsForm = new TrapDetailsForm(this.SelectedTrap);
				trapDetailsForm.ShowDialog();
			}
			if (this.SelectedChallenge != null)
			{
				SkillChallengeDetailsForm skillChallengeDetailsForm = new SkillChallengeDetailsForm(this.SelectedChallenge);
				skillChallengeDetailsForm.ShowDialog();
			}
		}
	}
}
