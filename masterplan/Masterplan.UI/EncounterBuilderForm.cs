using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Events;
using Masterplan.Tools;
using Masterplan.Tools.Generators;
using Masterplan.Wizards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class EncounterBuilderForm : Form
	{
		private class SourceSorter : IComparer
		{
			private bool fAscending = true;

			private int fColumn;

			public void Set(int column)
			{
				if (this.fColumn == column)
				{
					this.fAscending = !this.fAscending;
				}
				this.fColumn = column;
			}

			public int Compare(object x, object y)
			{
				ListViewItem listViewItem = x as ListViewItem;
				ListViewItem listViewItem2 = y as ListViewItem;
				int num = 0;
				if (this.fColumn == 1)
				{
					if (listViewItem.Tag is ICreature)
					{
						ICreature creature = listViewItem.Tag as ICreature;
						ICreature creature2 = listViewItem2.Tag as ICreature;
						int level = creature.Level;
						int level2 = creature2.Level;
						num = level.CompareTo(level2);
					}
					if (listViewItem.Tag is Trap)
					{
						Trap trap = listViewItem.Tag as Trap;
						Trap trap2 = listViewItem2.Tag as Trap;
						int level3 = trap.Level;
						int level4 = trap2.Level;
						num = level3.CompareTo(level4);
					}
				}
				if (num == 0)
				{
					ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems[this.fColumn];
					ListViewItem.ListViewSubItem listViewSubItem2 = listViewItem2.SubItems[this.fColumn];
					string text = listViewSubItem.Text;
					string text2 = listViewSubItem2.Text;
					num = text.CompareTo(text2);
				}
				if (!this.fAscending)
				{
					num *= -1;
				}
				return num;
			}
		}

		private Encounter fEncounter = new Encounter();

		private int fPartyLevel = Session.Project.Party.Level;

		private int fPartySize = Session.Project.Party.Size;

		private bool fAddingThreats;

		private ListMode fMode;

		private IContainer components;

		private ListView DifficultyList;

		private ColumnHeader DiffHdr;

		private ColumnHeader DiffXPHdr;

		private ListView SlotList;

		private ToolStrip EncToolbar;

		private ListView SourceItemList;

		private ToolStrip ThreatToolbar;

		private ColumnHeader ThreatHdr;

		private ColumnHeader CountHdr;

		private ColumnHeader NameHdr;

		private ColumnHeader InfoHdr;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton AddBtn;

		private ToolStripButton RemoveBtn;

		private ColumnHeader XPHdr;

		private ColumnHeader ThreatInfoHdr;

		private TabPage ThreatsPage;

		private TabPage MapPage;

		private MapView MapView;

		private ToolStrip MapToolbar;

		private ColumnHeader DiffLevels;

		private ToolStripDropDownButton ViewMenu;

		private ToolStripMenuItem ViewTemplates;

		private SplitContainer HSplitter;

		private SplitContainer VSplitter;

		private ToolStripSeparator toolStripSeparator6;

		private SplitContainer MapSplitter;

		private ListView MapThreatList;

		private ColumnHeader ThreatNameHdr;

		private ToolTip Tooltip;

		public TabControl Pages;

		private ToolStripMenuItem ViewNPCs;

		private StatusStrip XPStatusbar;

		private ToolStripStatusLabel XPLbl;

		private ToolStripStatusLabel DiffLbl;

		private ToolStripStatusLabel CountLbl;

		private ContextMenuStrip MapContextMenu;

		private ToolStripMenuItem MapContextView;

		private ToolStripSeparator toolStripMenuItem4;

		private ToolStripMenuItem MapContextRemove;

		private ToolStripMenuItem MapContextVisible;

		private ToolStripDropDownButton MapToolsMenu;

		private ToolStripMenuItem MapToolsLOS;

		private ToolStripSeparator toolStripMenuItem5;

		private ToolStripMenuItem MapToolsPrint;

		private ToolStripMenuItem MapToolsScreenshot;

		private ToolStripDropDownButton MapCreaturesMenu;

		private ToolStripMenuItem MapCreaturesRemove;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripMenuItem MapCreaturesShowAll;

		private ToolStripMenuItem MapCreaturesHideAll;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripMenuItem CreaturesAddCustom;

		private ToolStripMenuItem CreaturesAddOverlay;

		private ToolStripMenuItem MapContextRemoveEncounter;

		private ToolStripSeparator toolStripSeparator5;

		private EncounterGauge XPGauge;

		private ToolStripStatusLabel LevelLbl;

		private TabPage NotesPage;

		private ToolStrip NoteToolbar;

		private ToolStripButton NoteAddBtn;

		private ToolStripButton NoteRemoveBtn;

		private ToolStripButton NoteEditBtn;

		private ToolStripSeparator toolStripSeparator21;

		private ToolStripButton NoteUpBtn;

		private ToolStripButton NoteDownBtn;

		private SplitContainer NoteSplitter;

		private ListView NoteList;

		private ColumnHeader NoteHdr;

		private Panel BackgroundPanel;

		private WebBrowser NoteDetails;

		private ToolStripButton MapBtn;

		private Button OKBtn;

		private Button CancelBtn;

		private ToolStripDropDownButton ToolsMenu;

		private ToolStripMenuItem ToolsUseTemplate;

		private ToolStripMenuItem ToolsUseDeck;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripSplitButton AutoBuildBtn;

		private ToolStripMenuItem AutoBuildAdvanced;

		private ToolStripMenuItem MapToolsGridlines;

		private ToolStripSeparator toolStripSeparator12;

		private ToolStripMenuItem ViewGroups;

		private ToolStripMenuItem MapToolsGridLabels;

		private ToolStripMenuItem ToolsExport;

		private ToolStripMenuItem MapToolsPictureTokens;

		private ToolStripMenuItem ToolsApplyTheme;

		private ToolStripSeparator toolStripSeparator13;

		private ToolStripSeparator toolStripSeparator15;

		private ToolStripMenuItem MapContextSetPicture;

		private ToolStripMenuItem MapContextCopy;

		private ContextMenuStrip ThreatContextMenu;

		private ToolStripMenuItem EditStatBlock;

		private ToolStripMenuItem EditApplyTheme;

		private ToolStripSeparator toolStripSeparator14;

		private ToolStripMenuItem EditRemoveTemplate;

		private ToolStripMenuItem EditRemoveLevelAdj;

		private ToolStripMenuItem EditClearTheme;

		private ToolStripSeparator toolStripSeparator16;

		private ToolStripMenuItem EditSetFaction;

		private ToolStripMenuItem EditSwap;

		private ToolStripMenuItem SwapStandard;

		private ToolStripSeparator toolStripMenuItem2;

		private ToolStripMenuItem SwapElite;

		private ToolStripMenuItem SwapSolo;

		private ToolStripSeparator toolStripSeparator11;

		private ToolStripMenuItem SwapMinions;

		private ToolStripMenuItem decksToolStripMenuItem;

		private StatusStrip HintStatusbar;

		private ToolStripStatusLabel HintLbl;

		private ToolStripMenuItem ToolsClearAll;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripDropDownButton AddMenu;

		private ToolStripMenuItem ToolsAddCreature;

		private ToolStripMenuItem ToolsAddTrap;

		private ToolStripMenuItem ToolsAddChallenge;

		private ToolStripSeparator toolStripSeparator4;

		private Button InfoBtn;

		private Button DieRollerBtn;

		private FilterPanel FilterPanel;

		private ToolStripButton CreaturesBtn;

		private ToolStripButton TrapsBtn;

		private ToolStripButton ChallengesBtn;

		private ToolStripStatusLabel PartyLbl;

		private ToolStripSplitButton StatBlockBtn;

		private ToolStripMenuItem StatBlockEdit;

		private ToolStripMenuItem EditSetWave;

		private ToolStripSeparator toolStripSeparator9;

		public Encounter Encounter
		{
			get
			{
				return this.fEncounter;
			}
		}

		public EncounterSlot SelectedSlot
		{
			get
			{
				if (this.SlotList.SelectedItems.Count != 0)
				{
					return this.SlotList.SelectedItems[0].Tag as EncounterSlot;
				}
				return null;
			}
		}

		public Trap SelectedSlotTrap
		{
			get
			{
				if (this.SlotList.SelectedItems.Count != 0)
				{
					return this.SlotList.SelectedItems[0].Tag as Trap;
				}
				return null;
			}
		}

		public SkillChallenge SelectedSlotSkillChallenge
		{
			get
			{
				if (this.SlotList.SelectedItems.Count != 0)
				{
					return this.SlotList.SelectedItems[0].Tag as SkillChallenge;
				}
				return null;
			}
		}

		public ICreature SelectedCreature
		{
			get
			{
				if (this.SourceItemList.SelectedItems.Count != 0)
				{
					return this.SourceItemList.SelectedItems[0].Tag as ICreature;
				}
				return null;
			}
		}

		public CreatureTemplate SelectedTemplate
		{
			get
			{
				if (this.SourceItemList.SelectedItems.Count != 0)
				{
					return this.SourceItemList.SelectedItems[0].Tag as CreatureTemplate;
				}
				return null;
			}
		}

		public NPC SelectedNPC
		{
			get
			{
				if (this.SourceItemList.SelectedItems.Count != 0)
				{
					return this.SourceItemList.SelectedItems[0].Tag as NPC;
				}
				return null;
			}
		}

		public Trap SelectedTrap
		{
			get
			{
				if (this.SourceItemList.SelectedItems.Count != 0)
				{
					return this.SourceItemList.SelectedItems[0].Tag as Trap;
				}
				return null;
			}
		}

		public SkillChallenge SelectedSkillChallenge
		{
			get
			{
				if (this.SourceItemList.SelectedItems.Count != 0)
				{
					return this.SourceItemList.SelectedItems[0].Tag as SkillChallenge;
				}
				return null;
			}
		}

		public IToken SelectedMapThreat
		{
			get
			{
				if (this.MapThreatList.SelectedItems.Count != 0)
				{
					return this.MapThreatList.SelectedItems[0].Tag as IToken;
				}
				return null;
			}
		}

		private EncounterNote SelectedNote
		{
			get
			{
				if (this.NoteList.SelectedItems.Count != 0)
				{
					return this.NoteList.SelectedItems[0].Tag as EncounterNote;
				}
				return null;
			}
			set
			{
				this.NoteList.SelectedItems.Clear();
				if (value != null)
				{
					foreach (ListViewItem listViewItem in this.NoteList.Items)
					{
						EncounterNote encounterNote = listViewItem.Tag as EncounterNote;
						if (encounterNote != null && encounterNote.ID == value.ID)
						{
							listViewItem.Selected = true;
						}
					}
				}
				this.update_selected_note();
			}
		}

		public EncounterBuilderForm(Encounter enc, int party_level, bool adding_threats)
		{
			this.InitializeComponent();
			this.fMode = ListMode.Creatures;
			this.fEncounter = (enc.Copy() as Encounter);
			this.fPartyLevel = party_level;
			this.fAddingThreats = adding_threats;
			this.SourceItemList.ListViewItemSorter = new EncounterBuilderForm.SourceSorter();
			this.NoteDetails.DocumentText = "";
			this.ToolsUseDeck.Visible = (Session.Project.Decks.Count != 0);
			this.FilterPanel.Mode = this.fMode;
			this.FilterPanel.PartyLevel = this.fPartyLevel;
			this.FilterPanel.FilterByPartyLevel();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			if (this.fAddingThreats)
			{
				this.Pages.TabPages.Remove(this.MapPage);
				this.Pages.TabPages.Remove(this.NotesPage);
				this.VSplitter.Panel2Collapsed = true;
			}
			else
			{
				Map map = (this.fEncounter.MapID != Guid.Empty) ? Session.Project.FindTacticalMap(this.fEncounter.MapID) : null;
				if (map != null)
				{
					this.MapView.Map = map;
					this.MapView.Encounter = this.fEncounter;
					MapArea mapArea = (this.fEncounter.MapAreaID != Guid.Empty) ? map.FindArea(this.fEncounter.MapAreaID) : null;
					this.MapView.Viewpoint = ((mapArea != null) ? mapArea.Region : Rectangle.Empty);
				}
				this.update_difficulty_list();
				this.update_mapthreats();
				this.update_notes();
				this.update_selected_note();
			}
			this.update_source_list();
			this.update_encounter();
			this.update_party_label();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			try
			{
				if (this.Pages.SelectedTab == this.ThreatsPage)
				{
					bool enabled = this.SelectedSlot != null || this.SelectedSlotTrap != null || this.SelectedSlotSkillChallenge != null;
					this.AddBtn.Enabled = enabled;
					this.AddBtn.Visible = (this.SelectedSlot != null || this.SelectedSlotTrap != null);
					this.RemoveBtn.Enabled = enabled;
					this.StatBlockBtn.Enabled = enabled;
					if (this.SelectedSlotTrap != null || this.SelectedSlotSkillChallenge != null)
					{
						this.RemoveBtn.Text = "Remove";
					}
					else
					{
						this.RemoveBtn.Text = "-";
					}
					this.CreaturesBtn.Visible = (Session.Creatures.Count != 0);
					this.TrapsBtn.Visible = (Session.Traps.Count != 0);
					this.ChallengesBtn.Visible = (Session.SkillChallenges.Count != 0);
					this.CreaturesBtn.Checked = (this.fMode == ListMode.Creatures);
					this.TrapsBtn.Checked = (this.fMode == ListMode.Traps);
					this.ChallengesBtn.Checked = (this.fMode == ListMode.SkillChallenges);
				}
				if (this.Pages.SelectedTab == this.MapPage)
				{
					this.MapToolsLOS.Checked = this.MapView.LineOfSight;
					this.MapToolsGridlines.Checked = (this.MapView.ShowGrid != MapGridMode.None);
					this.MapToolsGridLabels.Checked = this.MapView.ShowGridLabels;
					this.MapToolsPictureTokens.Checked = this.MapView.ShowPictureTokens;
					this.MapToolsPrint.Enabled = (this.MapView.Map != null);
					this.MapToolsScreenshot.Enabled = (this.MapView.Map != null);
					this.MapSplitter.Panel2Collapsed = (this.MapView.Map == null || this.MapThreatList.Items.Count == 0);
					this.MapContextView.Enabled = (this.MapView.SelectedTokens.Count == 1);
					this.MapContextSetPicture.Enabled = (this.MapView.SelectedTokens.Count == 1);
					this.MapContextRemove.Enabled = (this.MapView.SelectedTokens.Count != 0);
					this.MapContextRemoveEncounter.Enabled = (this.MapView.SelectedTokens.Count != 0);
					this.MapContextCopy.Enabled = (this.MapView.SelectedTokens.Count == 1 && this.MapView.SelectedTokens[0] is CustomToken);
					if (this.MapView.SelectedTokens.Count == 1)
					{
						this.MapContextVisible.Enabled = true;
						IToken token = this.MapView.SelectedTokens[0];
						if (token is CreatureToken)
						{
							CreatureToken creatureToken = token as CreatureToken;
							this.MapContextVisible.Checked = creatureToken.Data.Visible;
						}
						if (token is CustomToken)
						{
							CustomToken customToken = token as CustomToken;
							this.MapContextVisible.Checked = customToken.Data.Visible;
						}
					}
					else
					{
						this.MapContextVisible.Enabled = false;
						this.MapContextVisible.Checked = false;
					}
				}
				if (this.Pages.SelectedTab == this.NotesPage)
				{
					this.NoteRemoveBtn.Enabled = (this.SelectedNote != null);
					this.NoteEditBtn.Enabled = (this.SelectedNote != null);
					this.NoteUpBtn.Enabled = (this.SelectedNote != null && this.fEncounter.Notes.IndexOf(this.SelectedNote) != 0);
					this.NoteDownBtn.Enabled = (this.SelectedNote != null && this.fEncounter.Notes.IndexOf(this.SelectedNote) != this.fEncounter.Notes.Count - 1);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSlot != null)
			{
				if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
				{
					this.SelectedSlot.Card.LevelAdjustment++;
					this.update_encounter();
				}
				else
				{
					CombatData item = new CombatData();
					this.SelectedSlot.CombatData.Add(item);
					this.update_encounter();
					this.update_mapthreats();
				}
			}
			if (this.SelectedSlotTrap != null)
			{
				Trap trap = this.SelectedSlotTrap.Copy();
				trap.ID = Guid.NewGuid();
				this.fEncounter.Traps.Add(trap);
				this.update_encounter();
			}
			if (this.SelectedSlotSkillChallenge != null)
			{
				SkillChallenge skillChallenge = this.SelectedSlotSkillChallenge.Copy() as SkillChallenge;
				skillChallenge.ID = Guid.NewGuid();
				this.fEncounter.SkillChallenges.Add(skillChallenge);
				this.update_encounter();
			}
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSlot != null)
			{
				if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
				{
					if (this.SelectedSlot.Card.Level > 1)
					{
						this.SelectedSlot.Card.LevelAdjustment--;
						this.update_encounter();
					}
				}
				else
				{
					this.SelectedSlot.CombatData.RemoveAt(this.SelectedSlot.CombatData.Count - 1);
					if (this.SelectedSlot.CombatData.Count == 0)
					{
						this.fEncounter.Slots.Remove(this.SelectedSlot);
					}
					this.update_encounter();
					this.update_mapthreats();
				}
			}
			if (this.SelectedSlotTrap != null)
			{
				this.fEncounter.Traps.Remove(this.SelectedSlotTrap);
				this.update_encounter();
			}
			if (this.SelectedSlotSkillChallenge != null)
			{
				this.fEncounter.SkillChallenges.Remove(this.SelectedSlotSkillChallenge);
				this.update_encounter();
			}
		}

		private void StatBlockBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSlot != null)
			{
				CreatureDetailsForm creatureDetailsForm = new CreatureDetailsForm(this.SelectedSlot.Card);
				creatureDetailsForm.ShowDialog();
			}
			if (this.SelectedSlotTrap != null)
			{
				TrapDetailsForm trapDetailsForm = new TrapDetailsForm(this.SelectedSlotTrap);
				trapDetailsForm.ShowDialog();
			}
			if (this.SelectedSlotSkillChallenge != null)
			{
				SkillChallengeDetailsForm skillChallengeDetailsForm = new SkillChallengeDetailsForm(this.SelectedSlotSkillChallenge);
				skillChallengeDetailsForm.ShowDialog();
			}
		}

		private void EditStatBlock_Click(object sender, EventArgs e)
		{
			if (this.SelectedSlot != null)
			{
				Guid creatureID = this.SelectedSlot.Card.CreatureID;
				CustomCreature customCreature = Session.Project.FindCustomCreature(creatureID);
				NPC nPC = Session.Project.FindNPC(creatureID);
				if (customCreature != null)
				{
					int index = Session.Project.CustomCreatures.IndexOf(customCreature);
					CreatureBuilderForm creatureBuilderForm = new CreatureBuilderForm(customCreature);
					if (creatureBuilderForm.ShowDialog() == DialogResult.OK)
					{
						this.SelectedSlot.SetDefaultDisplayNames();
						Session.Project.CustomCreatures[index] = (creatureBuilderForm.Creature as CustomCreature);
						Session.Modified = true;
						this.update_encounter();
						this.update_source_list();
						this.update_mapthreats();
					}
				}
				else if (nPC != null)
				{
					int index2 = Session.Project.NPCs.IndexOf(nPC);
					CreatureBuilderForm creatureBuilderForm2 = new CreatureBuilderForm(nPC);
					if (creatureBuilderForm2.ShowDialog() == DialogResult.OK)
					{
						this.SelectedSlot.SetDefaultDisplayNames();
						Session.Project.NPCs[index2] = (creatureBuilderForm2.Creature as NPC);
						Session.Modified = true;
						this.update_encounter();
						this.update_source_list();
						this.update_mapthreats();
					}
				}
				else
				{
					string text = "You're about to edit a creature's stat block. Do you want to change this creature globally?";
					text += Environment.NewLine;
					text += Environment.NewLine;
					text += "Press Yes to apply your changes to this creature, everywhere it appears, even in other projects. Select this option if you're correcting an error in the creature's stat block.";
					text += Environment.NewLine;
					text += Environment.NewLine;
					text += "Press No to apply your changes to a copy of this creature. Select this option if you're modifying or re-skinning the creature for this encounter only, leaving other encounters as they are.";
					switch (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk))
					{
					case DialogResult.Yes:
					{
						Creature creature = Session.FindCreature(creatureID, SearchType.Global) as Creature;
						Library library = Session.FindLibrary(creature);
						int index3 = library.Creatures.IndexOf(creature);
						CreatureBuilderForm creatureBuilderForm3 = new CreatureBuilderForm(creature);
						if (creatureBuilderForm3.ShowDialog() == DialogResult.OK)
						{
							this.SelectedSlot.SetDefaultDisplayNames();
							library.Creatures[index3] = (creatureBuilderForm3.Creature as Creature);
							string libraryFilename = Session.GetLibraryFilename(library);
							Serialisation<Library>.Save(libraryFilename, library, SerialisationMode.Binary);
							this.update_encounter();
							this.update_source_list();
							this.update_mapthreats();
						}
						break;
					}
					case DialogResult.No:
					{
						ICreature c = Session.FindCreature(creatureID, SearchType.Global);
						CustomCreature customCreature2 = new CustomCreature(c);
						CreatureHelper.AdjustCreatureLevel(customCreature2, this.SelectedSlot.Card.LevelAdjustment);
						customCreature2.ID = Guid.NewGuid();
						CreatureBuilderForm creatureBuilderForm4 = new CreatureBuilderForm(customCreature2);
						if (creatureBuilderForm4.ShowDialog() == DialogResult.OK)
						{
							Session.Project.CustomCreatures.Add(creatureBuilderForm4.Creature as CustomCreature);
							Session.Modified = true;
							this.SelectedSlot.Card.CreatureID = creatureBuilderForm4.Creature.ID;
							this.SelectedSlot.Card.LevelAdjustment = 0;
							this.SelectedSlot.SetDefaultDisplayNames();
							this.update_encounter();
							this.update_source_list();
							this.update_mapthreats();
						}
						break;
					}
					}
				}
			}
			if (this.SelectedSlotTrap != null)
			{
				int index4 = this.fEncounter.Traps.IndexOf(this.SelectedSlotTrap);
				TrapBuilderForm trapBuilderForm = new TrapBuilderForm(this.SelectedSlotTrap);
				if (trapBuilderForm.ShowDialog() == DialogResult.OK)
				{
					trapBuilderForm.Trap.ID = Guid.NewGuid();
					this.fEncounter.Traps[index4] = trapBuilderForm.Trap;
					this.update_encounter();
				}
			}
			if (this.SelectedSlotSkillChallenge != null)
			{
				int index5 = this.fEncounter.SkillChallenges.IndexOf(this.SelectedSlotSkillChallenge);
				SkillChallengeBuilderForm skillChallengeBuilderForm = new SkillChallengeBuilderForm(this.SelectedSlotSkillChallenge);
				if (skillChallengeBuilderForm.ShowDialog() == DialogResult.OK)
				{
					skillChallengeBuilderForm.SkillChallenge.ID = Guid.NewGuid();
					this.fEncounter.SkillChallenges[index5] = skillChallengeBuilderForm.SkillChallenge;
					this.update_encounter();
				}
			}
		}

		private void count_slot_as(object sender, EventArgs e)
		{
			if (this.SelectedSlot != null)
			{
				ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
				EncounterSlotType type = (EncounterSlotType)toolStripMenuItem.Tag;
				this.SelectedSlot.Type = type;
				this.update_encounter();
			}
		}

		private void EditRemoveTemplate_Click(object sender, EventArgs e)
		{
			if (this.SelectedSlot != null && this.SelectedSlot.Card.TemplateIDs.Count != 0)
			{
				this.SelectedSlot.Card.TemplateIDs.Clear();
				this.update_encounter();
				this.update_mapthreats();
			}
		}

		private void EditRemoveLevelAdj_Click(object sender, EventArgs e)
		{
			if (this.SelectedSlot != null && this.SelectedSlot.Card.LevelAdjustment != 0)
			{
				this.SelectedSlot.Card.LevelAdjustment = 0;
				this.update_encounter();
				this.update_mapthreats();
			}
		}

		private void SwapStandard_Click(object sender, EventArgs e)
		{
			EncounterCard card = this.SelectedSlot.Card;
			ICreature creature = Session.FindCreature(card.CreatureID, SearchType.Global);
			int count = 1;
			if (creature.Role is Minion)
			{
				count = this.SelectedSlot.CombatData.Count / 4;
			}
			else
			{
				switch (card.Flag)
				{
				case RoleFlag.Standard:
					count = this.SelectedSlot.CombatData.Count;
					break;
				case RoleFlag.Elite:
					count = this.SelectedSlot.CombatData.Count * 2;
					break;
				case RoleFlag.Solo:
					count = this.SelectedSlot.CombatData.Count * 5;
					break;
				}
			}
			List<Creature> list = this.find_creatures(RoleFlag.Standard, card.Level, card.Roles);
			if (list.Count == 0)
			{
				string text = "There are no creatures of this type.";
				MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			Creature creature2 = this.choose_creature(list, creature.Category);
			if (creature2 == null)
			{
				return;
			}
			this.perform_swap(creature2, count, this.SelectedSlot);
		}

		private void SwapElite_Click(object sender, EventArgs e)
		{
			EncounterCard card = this.SelectedSlot.Card;
			ICreature creature = Session.FindCreature(card.CreatureID, SearchType.Global);
			int count = 1;
			if (creature.Role is Minion)
			{
				count = this.SelectedSlot.CombatData.Count / 8;
			}
			else
			{
				switch (card.Flag)
				{
				case RoleFlag.Standard:
					count = this.SelectedSlot.CombatData.Count / 2;
					break;
				case RoleFlag.Elite:
					count = this.SelectedSlot.CombatData.Count;
					break;
				case RoleFlag.Solo:
					count = this.SelectedSlot.CombatData.Count * 5 / 2;
					break;
				}
			}
			List<Creature> list = this.find_creatures(RoleFlag.Elite, card.Level, card.Roles);
			if (list.Count == 0)
			{
				string text = "There are no creatures of this type.";
				MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			Creature creature2 = this.choose_creature(list, creature.Category);
			if (creature2 == null)
			{
				return;
			}
			this.perform_swap(creature2, count, this.SelectedSlot);
		}

		private void SwapSolo_Click(object sender, EventArgs e)
		{
			EncounterCard card = this.SelectedSlot.Card;
			ICreature creature = Session.FindCreature(card.CreatureID, SearchType.Global);
			int count = 1;
			if (creature.Role is Minion)
			{
				count = this.SelectedSlot.CombatData.Count / 20;
			}
			else
			{
				switch (card.Flag)
				{
				case RoleFlag.Standard:
					count = this.SelectedSlot.CombatData.Count / 5;
					break;
				case RoleFlag.Elite:
					count = this.SelectedSlot.CombatData.Count * 2 / 5;
					break;
				case RoleFlag.Solo:
					count = this.SelectedSlot.CombatData.Count;
					break;
				}
			}
			List<Creature> list = this.find_creatures(RoleFlag.Solo, card.Level, card.Roles);
			if (list.Count == 0)
			{
				string text = "There are no creatures of this type.";
				MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			Creature creature2 = this.choose_creature(list, creature.Category);
			if (creature2 == null)
			{
				return;
			}
			this.perform_swap(creature2, count, this.SelectedSlot);
		}

		private void SwapMinions_Click(object sender, EventArgs e)
		{
			EncounterCard card = this.SelectedSlot.Card;
			ICreature creature = Session.FindCreature(card.CreatureID, SearchType.Global);
			int count = 1;
			if (creature.Role is Minion)
			{
				count = this.SelectedSlot.CombatData.Count / 4;
			}
			else
			{
				switch (card.Flag)
				{
				case RoleFlag.Standard:
					count = this.SelectedSlot.CombatData.Count * 4;
					break;
				case RoleFlag.Elite:
					count = this.SelectedSlot.CombatData.Count * 8;
					break;
				case RoleFlag.Solo:
					count = this.SelectedSlot.CombatData.Count * 20;
					break;
				}
			}
			List<Creature> list = this.find_minions(card.Level);
			if (list.Count == 0)
			{
				string text = "There are no creatures of this type.";
				MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			Creature creature2 = this.choose_creature(list, creature.Category);
			if (creature2 == null)
			{
				return;
			}
			this.perform_swap(creature2, count, this.SelectedSlot);
		}

		private List<Creature> find_creatures(RoleFlag flag, int level, List<RoleType> roles)
		{
			List<Creature> list = new List<Creature>();
			foreach (Library current in Session.Libraries)
			{
				foreach (Creature current2 in current.Creatures)
				{
					if (!(current2.Role is Minion))
					{
						ComplexRole complexRole = current2.Role as ComplexRole;
						if (complexRole.Flag == flag && current2.Level == level && (roles.Count == 0 || roles.Contains(complexRole.Type)))
						{
							list.Add(current2);
						}
					}
				}
			}
			return list;
		}

		private List<Creature> find_minions(int level)
		{
			List<Creature> list = new List<Creature>();
			foreach (Library current in Session.Libraries)
			{
				foreach (Creature current2 in current.Creatures)
				{
					if (current2.Role is Minion && current2.Level == level)
					{
						list.Add(current2);
					}
				}
			}
			return list;
		}

		private Creature choose_creature(List<Creature> creatures, string category)
		{
			CreatureSelectForm creatureSelectForm = new CreatureSelectForm(creatures);
			if (creatureSelectForm.ShowDialog() == DialogResult.OK)
			{
				EncounterCard creature = creatureSelectForm.Creature;
				return Session.FindCreature(creature.CreatureID, SearchType.Global) as Creature;
			}
			return null;
		}

		private void perform_swap(Creature creature, int count, EncounterSlot old_slot)
		{
			EncounterSlot encounterSlot = new EncounterSlot();
			encounterSlot.Card.CreatureID = creature.ID;
			for (int num = 0; num != count; num++)
			{
				CombatData item = new CombatData();
				encounterSlot.CombatData.Add(item);
			}
			this.fEncounter.Slots.Remove(old_slot);
			this.fEncounter.Slots.Add(encounterSlot);
			this.update_encounter();
			this.update_mapthreats();
		}

		private void EditApplyTheme_Click(object sender, EventArgs e)
		{
			if (this.SelectedSlot == null)
			{
				return;
			}
			ThemeForm themeForm = new ThemeForm(this.SelectedSlot.Card);
			if (themeForm.ShowDialog() == DialogResult.OK)
			{
				this.SelectedSlot.Card = themeForm.Card;
				this.update_encounter();
				this.update_mapthreats();
			}
		}

		private void EditClearTheme_Click(object sender, EventArgs e)
		{
			if (this.SelectedSlot == null)
			{
				return;
			}
			this.SelectedSlot.Card.ThemeID = Guid.Empty;
			this.SelectedSlot.Card.ThemeAttackPowerID = Guid.Empty;
			this.SelectedSlot.Card.ThemeUtilityPowerID = Guid.Empty;
			this.update_encounter();
			this.update_mapthreats();
		}

		private void ToolsClearAll_Click(object sender, EventArgs e)
		{
			this.fEncounter.Slots.Clear();
			this.fEncounter.Traps.Clear();
			this.fEncounter.SkillChallenges.Clear();
			this.update_encounter();
			this.update_mapthreats();
		}

		private void ToolsUseTemplate_Click(object sender, EventArgs e)
		{
			List<Pair<EncounterTemplateGroup, EncounterTemplate>> list = EncounterBuilder.FindTemplates(this.fEncounter, this.fPartyLevel, true);
			if (list.Count == 0)
			{
				string text = "There are no encounter templates which match the creatures already in the encounter.";
				text += Environment.NewLine;
				text += "This does not mean there is a problem with your encounter.";
				MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			EncounterTemplateWizard encounterTemplateWizard = new EncounterTemplateWizard(list, this.fEncounter, this.fPartyLevel);
			if (encounterTemplateWizard.Show() == DialogResult.OK)
			{
				this.update_encounter();
				this.update_mapthreats();
			}
		}

		private void ToolsMenu_DropDownOpening(object sender, EventArgs e)
		{
			this.ToolsUseDeck.DropDownItems.Clear();
			foreach (EncounterDeck current in Session.Project.Decks)
			{
				if (current.Cards.Count != 0)
				{
					ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(string.Concat(new object[]
					{
						current.Name,
						" (",
						current.Cards.Count,
						" cards)"
					}));
					toolStripMenuItem.Tag = current;
					toolStripMenuItem.Click += new EventHandler(this.use_deck);
					this.ToolsUseDeck.DropDownItems.Add(toolStripMenuItem);
				}
			}
			if (this.ToolsUseDeck.DropDownItems.Count == 0)
			{
				ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem("(no decks)");
				toolStripMenuItem2.ForeColor = SystemColors.GrayText;
				this.ToolsUseDeck.DropDownItems.Add(toolStripMenuItem2);
			}
		}

		private void use_deck(object sender, EventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			EncounterDeck encounterDeck = toolStripMenuItem.Tag as EncounterDeck;
			encounterDeck.DrawEncounter(this.fEncounter);
			if (encounterDeck.Cards.Count == 0)
			{
				Session.Project.Decks.Remove(encounterDeck);
			}
			this.update_encounter();
			this.update_mapthreats();
		}

		private void ToolsAddCreature_Click(object sender, EventArgs e)
		{
			try
			{
				CreatureBuilderForm creatureBuilderForm = new CreatureBuilderForm(new CustomCreature
				{
					Name = "Custom Creature",
					Level = this.fPartyLevel
				});
				if (creatureBuilderForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.CustomCreatures.Add(creatureBuilderForm.Creature as CustomCreature);
					Session.Modified = true;
					this.add_opponent(creatureBuilderForm.Creature);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsAddTrap_Click(object sender, EventArgs e)
		{
			try
			{
				TrapBuilderForm trapBuilderForm = new TrapBuilderForm(new Trap
				{
					Name = "Custom Trap",
					Level = this.fPartyLevel,
					Attacks = 
					{
						new TrapAttack()
					}
				});
				if (trapBuilderForm.ShowDialog() == DialogResult.OK)
				{
					this.fEncounter.Traps.Add(trapBuilderForm.Trap);
					this.update_encounter();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsAddChallenge_Click(object sender, EventArgs e)
		{
			try
			{
				SkillChallengeBuilderForm skillChallengeBuilderForm = new SkillChallengeBuilderForm(new SkillChallenge
				{
					Name = "Custom Skill Challenge",
					Level = this.fPartyLevel
				});
				if (skillChallengeBuilderForm.ShowDialog() == DialogResult.OK)
				{
					this.fEncounter.SkillChallenges.Add(skillChallengeBuilderForm.SkillChallenge);
					this.update_encounter();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsApplyTheme_Click(object sender, EventArgs e)
		{
			MonsterThemeSelectForm monsterThemeSelectForm = new MonsterThemeSelectForm();
			if (monsterThemeSelectForm.ShowDialog() == DialogResult.OK && monsterThemeSelectForm.MonsterTheme != null)
			{
				foreach (EncounterSlot current in this.fEncounter.Slots)
				{
					current.Card.ThemeID = monsterThemeSelectForm.MonsterTheme.ID;
					current.Card.ThemeAttackPowerID = Guid.Empty;
					current.Card.ThemeUtilityPowerID = Guid.Empty;
					List<ThemePowerData> list = monsterThemeSelectForm.MonsterTheme.ListPowers(current.Card.Roles, PowerType.Attack);
					if (list.Count != 0)
					{
						int index = Session.Random.Next() % list.Count;
						ThemePowerData themePowerData = list[index];
						current.Card.ThemeAttackPowerID = themePowerData.Power.ID;
					}
					List<ThemePowerData> list2 = monsterThemeSelectForm.MonsterTheme.ListPowers(current.Card.Roles, PowerType.Utility);
					if (list2.Count != 0)
					{
						int index2 = Session.Random.Next() % list2.Count;
						ThemePowerData themePowerData2 = list2[index2];
						current.Card.ThemeUtilityPowerID = themePowerData2.Power.ID;
					}
				}
				this.update_encounter();
				this.update_mapthreats();
			}
		}

		private void AutoBuildBtn_Click(object sender, EventArgs e)
		{
			this.autobuild(false);
		}

		private void AutoBuildAdvanced_Click(object sender, EventArgs e)
		{
			this.autobuild(true);
		}

		private void autobuild(bool advanced)
		{
			AutoBuildData autoBuildData;
			if (advanced)
			{
				AutoBuildForm autoBuildForm = new AutoBuildForm(AutoBuildForm.Mode.Encounter);
				if (autoBuildForm.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				autoBuildData = autoBuildForm.Data;
			}
			else
			{
				autoBuildData = new AutoBuildData();
			}
			autoBuildData.Level = this.fPartyLevel;
			bool flag = EncounterBuilder.Build(autoBuildData, this.fEncounter, false);
			this.update_encounter();
			this.update_mapthreats();
			if (!flag)
			{
				string text = "AutoBuild was unable to find enough creatures of the appropriate type to build an encounter.";
				MessageBox.Show(text, "Encounter Builder", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private void ViewMenu_DropDownOpening(object sender, EventArgs e)
		{
			this.ViewTemplates.Enabled = (Session.Templates.Count != 0);
			this.ViewNPCs.Enabled = (Session.Project.NPCs.Count != 0);
			this.ViewNPCs.Checked = (this.fMode == ListMode.NPCs);
			this.ViewTemplates.Checked = (this.fMode == ListMode.Templates);
			this.ViewGroups.Checked = this.SourceItemList.ShowGroups;
		}

		private void ViewCreatures_Click(object sender, EventArgs e)
		{
			this.fMode = ListMode.Creatures;
			this.FilterPanel.Mode = ListMode.Creatures;
			this.update_source_list();
		}

		private void ViewTemplates_Click(object sender, EventArgs e)
		{
			this.fMode = ListMode.Templates;
			this.FilterPanel.Mode = ListMode.Templates;
			this.update_source_list();
		}

		private void ViewNPCs_Click(object sender, EventArgs e)
		{
			this.fMode = ListMode.NPCs;
			this.FilterPanel.Mode = ListMode.NPCs;
			this.update_source_list();
		}

		private void ViewTraps_Click(object sender, EventArgs e)
		{
			this.fMode = ListMode.Traps;
			this.FilterPanel.Mode = ListMode.Traps;
			this.update_source_list();
		}

		private void ViewChallenges_Click(object sender, EventArgs e)
		{
			this.fMode = ListMode.SkillChallenges;
			this.FilterPanel.Mode = ListMode.SkillChallenges;
			this.update_source_list();
		}

		private void FilterPanel_FilterChanged(object sender, EventArgs e)
		{
			this.update_source_list();
		}

		private void SlotList_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				if (this.SelectedSlot != null)
				{
					this.fEncounter.Slots.Remove(this.SelectedSlot);
					this.update_encounter();
					e.Handled = true;
					return;
				}
				if (this.SelectedSlotSkillChallenge != null)
				{
					this.fEncounter.SkillChallenges.Remove(this.SelectedSlotSkillChallenge);
					this.update_encounter();
					e.Handled = true;
					return;
				}
				if (this.SelectedSlotTrap != null)
				{
					this.fEncounter.Traps.Remove(this.SelectedSlotTrap);
					this.update_encounter();
					e.Handled = true;
				}
			}
		}

		private void SlotList_DoubleClick(object sender, EventArgs e)
		{
			this.StatBlockBtn_Click(sender, e);
		}

		private void ThreatList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedCreature != null)
			{
				CreatureDetailsForm creatureDetailsForm = new CreatureDetailsForm(new EncounterCard
				{
					CreatureID = this.SelectedCreature.ID
				});
				creatureDetailsForm.ShowDialog();
			}
			if (this.SelectedTemplate != null)
			{
				CreatureTemplateDetailsForm creatureTemplateDetailsForm = new CreatureTemplateDetailsForm(this.SelectedTemplate);
				creatureTemplateDetailsForm.ShowDialog();
			}
			if (this.SelectedTrap != null)
			{
				TrapDetailsForm trapDetailsForm = new TrapDetailsForm(this.SelectedTrap);
				trapDetailsForm.ShowDialog();
			}
			if (this.SelectedSkillChallenge != null)
			{
				SkillChallengeDetailsForm skillChallengeDetailsForm = new SkillChallengeDetailsForm(this.SelectedSkillChallenge);
				skillChallengeDetailsForm.ShowDialog();
			}
		}

		private void OpponentList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedCreature != null)
			{
				base.DoDragDrop(this.SelectedCreature, DragDropEffects.All);
			}
			if (this.SelectedTemplate != null)
			{
				base.DoDragDrop(this.SelectedTemplate, DragDropEffects.All);
			}
			if (this.SelectedNPC != null)
			{
				base.DoDragDrop(this.SelectedNPC, DragDropEffects.All);
			}
			if (this.SelectedTrap != null)
			{
				base.DoDragDrop(this.SelectedTrap, DragDropEffects.All);
			}
			if (this.SelectedSkillChallenge != null)
			{
				base.DoDragDrop(this.SelectedSkillChallenge, DragDropEffects.All);
			}
		}

		private void SlotList_DragOver(object sender, DragEventArgs e)
		{
			Creature creature = e.Data.GetData(typeof(Creature)) as Creature;
			if (creature != null)
			{
				e.Effect = DragDropEffects.Copy;
			}
			CustomCreature customCreature = e.Data.GetData(typeof(CustomCreature)) as CustomCreature;
			if (customCreature != null)
			{
				e.Effect = DragDropEffects.Copy;
			}
			NPC nPC = e.Data.GetData(typeof(NPC)) as NPC;
			if (nPC != null)
			{
				e.Effect = DragDropEffects.Copy;
			}
			Trap trap = e.Data.GetData(typeof(Trap)) as Trap;
			if (trap != null)
			{
				e.Effect = DragDropEffects.Copy;
			}
			SkillChallenge skillChallenge = e.Data.GetData(typeof(SkillChallenge)) as SkillChallenge;
			if (skillChallenge != null)
			{
				e.Effect = DragDropEffects.Copy;
			}
			CreatureTemplate creatureTemplate = e.Data.GetData(typeof(CreatureTemplate)) as CreatureTemplate;
			if (creatureTemplate != null)
			{
				Point point = this.SlotList.PointToClient(Cursor.Position);
				ListViewItem itemAt = this.SlotList.GetItemAt(point.X, point.Y);
				itemAt.Selected = true;
				EncounterSlot encounterSlot = itemAt.Tag as EncounterSlot;
				if (encounterSlot != null && this.allow_template_drop(encounterSlot, creatureTemplate))
				{
					e.Effect = DragDropEffects.Copy;
					return;
				}
				e.Effect = DragDropEffects.None;
			}
		}

		private void SlotList_DragDrop(object sender, DragEventArgs e)
		{
			Creature creature = e.Data.GetData(typeof(Creature)) as Creature;
			if (creature != null)
			{
				this.add_opponent(creature);
			}
			CustomCreature customCreature = e.Data.GetData(typeof(CustomCreature)) as CustomCreature;
			if (customCreature != null)
			{
				this.add_opponent(customCreature);
			}
			NPC nPC = e.Data.GetData(typeof(NPC)) as NPC;
			if (nPC != null)
			{
				this.add_opponent(nPC);
			}
			Trap trap = e.Data.GetData(typeof(Trap)) as Trap;
			if (trap != null)
			{
				this.add_trap(trap);
			}
			SkillChallenge skillChallenge = e.Data.GetData(typeof(SkillChallenge)) as SkillChallenge;
			if (skillChallenge != null)
			{
				this.add_challenge(skillChallenge);
			}
			CreatureTemplate creatureTemplate = e.Data.GetData(typeof(CreatureTemplate)) as CreatureTemplate;
			if (creatureTemplate != null && this.SelectedSlot != null && this.allow_template_drop(this.SelectedSlot, creatureTemplate))
			{
				this.add_template(creatureTemplate, this.SelectedSlot);
			}
		}

		private bool allow_template_drop(EncounterSlot slot, CreatureTemplate template)
		{
			if (slot.Card.TemplateIDs.Contains(template.ID))
			{
				return false;
			}
			ICreature creature = Session.FindCreature(slot.Card.CreatureID, SearchType.Global);
			if (creature.Role is Minion)
			{
				return false;
			}
			ComplexRole complexRole = creature.Role as ComplexRole;
			int num = slot.Card.TemplateIDs.Count;
			switch (complexRole.Flag)
			{
			case RoleFlag.Elite:
				num++;
				break;
			case RoleFlag.Solo:
				num += 2;
				break;
			}
			return num < 2;
		}

		private void add_opponent(ICreature creature)
		{
			EncounterSlot encounterSlot = null;
			foreach (EncounterSlot current in this.fEncounter.Slots)
			{
				if (current.Card.CreatureID == creature.ID && current.Card.TemplateIDs.Count == 0)
				{
					encounterSlot = current;
					break;
				}
			}
			if (encounterSlot == null)
			{
				encounterSlot = new EncounterSlot();
				encounterSlot.Card.CreatureID = creature.ID;
				this.fEncounter.Slots.Add(encounterSlot);
			}
			CombatData combatData = new CombatData();
			combatData.DisplayName = encounterSlot.Card.Title;
			encounterSlot.CombatData.Add(combatData);
			this.update_encounter();
			this.update_mapthreats();
		}

		private void add_template(CreatureTemplate template, EncounterSlot slot)
		{
			slot.Card.TemplateIDs.Add(template.ID);
			this.update_encounter();
			this.update_mapthreats();
		}

		private void add_trap(Trap trap)
		{
			this.fEncounter.Traps.Add(trap.Copy());
			this.update_encounter();
		}

		private void add_challenge(SkillChallenge sc)
		{
			SkillChallenge skillChallenge = sc.Copy() as SkillChallenge;
			skillChallenge.Level = this.fPartyLevel;
			this.fEncounter.SkillChallenges.Add(skillChallenge);
			this.update_encounter();
		}

		private void update_difficulty_list()
		{
			int num = this.fPartySize * (Experience.GetCreatureXP(this.fPartyLevel - 3) + Experience.GetCreatureXP(this.fPartyLevel - 2)) / 2;
			int num2 = this.fPartySize * (Experience.GetCreatureXP(this.fPartyLevel - 1) + Experience.GetCreatureXP(this.fPartyLevel)) / 2;
			int num3 = this.fPartySize * (Experience.GetCreatureXP(this.fPartyLevel + 1) + Experience.GetCreatureXP(this.fPartyLevel + 2)) / 2;
			int num4 = this.fPartySize * (Experience.GetCreatureXP(this.fPartyLevel + 4) + Experience.GetCreatureXP(this.fPartyLevel + 5)) / 2;
			num = Math.Max(1, num);
			num2 = Math.Max(1, num2);
			num3 = Math.Max(1, num3);
			num4 = Math.Max(1, num4);
			this.DifficultyList.Items.Clear();
			ListViewItem listViewItem = this.DifficultyList.Items.Add("Easy");
			listViewItem.SubItems.Add(num + " - " + num2);
			int num5 = Math.Max(1, this.fPartyLevel - 4);
			listViewItem.SubItems.Add(num5 + " - " + (this.fPartyLevel + 3));
			listViewItem.Tag = Difficulty.Easy;
			ListViewItem listViewItem2 = this.DifficultyList.Items.Add("Moderate");
			listViewItem2.SubItems.Add(num2 + " - " + num3);
			int num6 = Math.Max(1, this.fPartyLevel - 3);
			listViewItem2.SubItems.Add(num6 + " - " + (this.fPartyLevel + 3));
			listViewItem2.Tag = Difficulty.Moderate;
			ListViewItem listViewItem3 = this.DifficultyList.Items.Add("Hard");
			listViewItem3.SubItems.Add(num3 + " - " + num4);
			int num7 = Math.Max(1, this.fPartyLevel - 3);
			listViewItem3.SubItems.Add(num7 + " - " + (this.fPartyLevel + 5));
			listViewItem3.Tag = Difficulty.Hard;
			this.XPGauge.Party = new Party(this.fPartySize, this.fPartyLevel);
		}

		private void update_encounter()
		{
			this.SlotList.BeginUpdate();
			ListState state = ListState.GetState(this.SlotList);
			this.SlotList.Groups.Clear();
			this.SlotList.Items.Clear();
			this.SlotList.ShowGroups = (this.fEncounter.Count != 0 || this.fEncounter.Traps.Count != 0 || this.fEncounter.SkillChallenges.Count != 0);
			if (this.fEncounter.Count != 0)
			{
				foreach (EncounterSlot current in this.fEncounter.AllSlots)
				{
					current.SetDefaultDisplayNames();
				}
				this.SlotList.Groups.Add("Combatants", "Combatants");
				foreach (EncounterWave current2 in this.fEncounter.Waves)
				{
					this.SlotList.Groups.Add(current2.Name, current2.Name);
				}
				using (List<EncounterSlot>.Enumerator enumerator3 = this.fEncounter.AllSlots.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						EncounterSlot current3 = enumerator3.Current;
						string title = current3.Card.Title;
						ICreature creature = Session.FindCreature(current3.Card.CreatureID, SearchType.Global);
						ListViewItem listViewItem = this.SlotList.Items.Add(title);
						listViewItem.SubItems.Add(current3.Card.Info);
						listViewItem.SubItems.Add(current3.CombatData.Count.ToString());
						listViewItem.SubItems.Add(current3.XP.ToString());
						listViewItem.Tag = current3;
						if (creature != null)
						{
							EncounterWave encounterWave = this.fEncounter.FindWave(current3);
							listViewItem.Group = this.SlotList.Groups[(encounterWave == null) ? "Combatants" : encounterWave.Name];
						}
						if (creature == null)
						{
							listViewItem.ForeColor = Color.Red;
						}
						else
						{
							Difficulty threatDifficulty = AI.GetThreatDifficulty(creature.Level + current3.Card.LevelAdjustment, this.fPartyLevel);
							if (threatDifficulty == Difficulty.Trivial)
							{
								listViewItem.ForeColor = Color.Green;
							}
							if (threatDifficulty == Difficulty.Extreme)
							{
								listViewItem.ForeColor = Color.Red;
							}
						}
					}
					goto IL_2E7;
				}
			}
			this.SlotList.Groups.Add("Creatures", "Creatures");
			ListViewItem listViewItem2 = this.SlotList.Items.Add("(none)");
			listViewItem2.ForeColor = SystemColors.GrayText;
			listViewItem2.Group = this.SlotList.Groups["Creatures"];
			IL_2E7:
			if (this.fEncounter.Traps.Count != 0)
			{
				this.SlotList.Groups.Add("Traps / Hazards", "Traps / Hazards");
				foreach (Trap current4 in this.fEncounter.Traps)
				{
					ListViewItem listViewItem3 = this.SlotList.Items.Add(current4.Name);
					listViewItem3.SubItems.Add(current4.Info);
					listViewItem3.SubItems.Add("");
					listViewItem3.SubItems.Add(current4.XP.ToString());
					listViewItem3.Tag = current4;
					listViewItem3.Group = this.SlotList.Groups["Traps / Hazards"];
					Difficulty threatDifficulty2 = AI.GetThreatDifficulty(current4.Level, this.fPartyLevel);
					if (threatDifficulty2 == Difficulty.Trivial)
					{
						listViewItem3.ForeColor = Color.Green;
					}
					if (threatDifficulty2 == Difficulty.Extreme)
					{
						listViewItem3.ForeColor = Color.Red;
					}
				}
			}
			if (this.fEncounter.SkillChallenges.Count != 0)
			{
				this.SlotList.Groups.Add("Skill Challenges", "Skill Challenges");
				foreach (SkillChallenge current5 in this.fEncounter.SkillChallenges)
				{
					ListViewItem listViewItem4 = this.SlotList.Items.Add(current5.Name);
					listViewItem4.SubItems.Add(current5.Info);
					listViewItem4.SubItems.Add("");
					listViewItem4.SubItems.Add(current5.GetXP().ToString());
					listViewItem4.Tag = current5;
					listViewItem4.Group = this.SlotList.Groups["Skill Challenges"];
					Difficulty difficulty = current5.GetDifficulty(this.fPartyLevel, this.fPartySize);
					if (difficulty == Difficulty.Trivial)
					{
						listViewItem4.ForeColor = Color.Green;
					}
					if (difficulty == Difficulty.Extreme)
					{
						listViewItem4.ForeColor = Color.Red;
					}
				}
			}
			ListState.SetState(this.SlotList, state);
			this.SlotList.EndUpdate();
			Difficulty difficulty2 = this.fEncounter.GetDifficulty(this.fPartyLevel, this.fPartySize);
			foreach (ListViewItem listViewItem5 in this.DifficultyList.Items)
			{
				Difficulty difficulty3 = (Difficulty)listViewItem5.Tag;
				listViewItem5.BackColor = ((difficulty2 == difficulty3) ? Color.Gray : SystemColors.Window);
				listViewItem5.Font = ((difficulty2 == difficulty3) ? new Font(this.Font, this.Font.Style | FontStyle.Bold) : this.Font);
			}
			int xP = this.fEncounter.GetXP();
			this.XPGauge.XP = xP;
			this.XPLbl.Text = "XP: " + xP;
			int creatureLevel = Experience.GetCreatureLevel(xP / this.fPartySize);
			this.LevelLbl.Text = "Level: " + Math.Max(creatureLevel, 1);
			this.DiffLbl.Text = "Difficulty: " + this.fEncounter.GetDifficulty(this.fPartyLevel, this.fPartySize);
			this.CountLbl.Text = "Opponents: " + this.fEncounter.Count;
		}

		private void update_source_list()
		{
			Cursor.Current = Cursors.WaitCursor;
			this.SourceItemList.BeginUpdate();
			try
			{
				this.SourceItemList.Items.Clear();
				this.SourceItemList.Groups.Clear();
				this.SourceItemList.ShowGroups = true;
				switch (this.fMode)
				{
				case ListMode.Creatures:
				{
					List<Creature> creatures = Session.Creatures;
					BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
					foreach (Creature current in creatures)
					{
						if (current.Category != null && current.Category != "")
						{
							binarySearchTree.Add(current.Category);
						}
					}
					List<string> sortedList = binarySearchTree.SortedList;
					sortedList.Insert(0, "Custom Creatures");
					sortedList.Add("Miscellaneous Creatures");
					foreach (string current2 in sortedList)
					{
						this.SourceItemList.Groups.Add(current2, current2);
					}
					List<ListViewItem> list = new List<ListViewItem>();
					foreach (CustomCreature current3 in Session.Project.CustomCreatures)
					{
						ListViewItem listViewItem = this.add_creature_to_list(current3);
						if (listViewItem != null)
						{
							list.Add(listViewItem);
						}
					}
					foreach (Creature current4 in creatures)
					{
						ListViewItem listViewItem2 = this.add_creature_to_list(current4);
						if (listViewItem2 != null)
						{
							list.Add(listViewItem2);
						}
					}
					this.SourceItemList.Items.AddRange(list.ToArray());
					if (this.SourceItemList.Items.Count == 0)
					{
						this.SourceItemList.ShowGroups = false;
						ListViewItem listViewItem3 = this.SourceItemList.Items.Add("(no creatures)");
						listViewItem3.ForeColor = SystemColors.GrayText;
					}
					break;
				}
				case ListMode.Templates:
				{
					List<CreatureTemplate> templates = Session.Templates;
					ListViewGroup listViewGroup = this.SourceItemList.Groups.Add("Functional Templates", "Functional Templates");
					ListViewGroup listViewGroup2 = this.SourceItemList.Groups.Add("Class Templates", "Class Templates");
					List<ListViewItem> list2 = new List<ListViewItem>();
					foreach (CreatureTemplate current5 in templates)
					{
						ListViewGroup group = (current5.Type == CreatureTemplateType.Functional) ? listViewGroup : listViewGroup2;
						ListViewItem listViewItem4 = this.add_template_to_list(current5, group);
						if (listViewItem4 != null)
						{
							list2.Add(listViewItem4);
						}
					}
					this.SourceItemList.Items.AddRange(list2.ToArray());
					if (this.SourceItemList.Items.Count == 0)
					{
						this.SourceItemList.ShowGroups = false;
						ListViewItem listViewItem5 = this.SourceItemList.Items.Add("(no templates)");
						listViewItem5.ForeColor = SystemColors.GrayText;
					}
					break;
				}
				case ListMode.NPCs:
				{
					ListViewGroup group2 = this.SourceItemList.Groups.Add("NPCs", "NPCs");
					List<ListViewItem> list3 = new List<ListViewItem>();
					foreach (NPC current6 in Session.Project.NPCs)
					{
						ListViewItem listViewItem6 = this.add_npc_to_list(current6, group2);
						if (listViewItem6 != null)
						{
							list3.Add(listViewItem6);
						}
					}
					this.SourceItemList.Items.AddRange(list3.ToArray());
					if (this.SourceItemList.Items.Count == 0)
					{
						this.SourceItemList.ShowGroups = false;
						ListViewItem listViewItem7 = this.SourceItemList.Items.Add("(no npcs)");
						listViewItem7.ForeColor = SystemColors.GrayText;
					}
					break;
				}
				case ListMode.Traps:
				{
					List<Trap> traps = Session.Traps;
					ListViewGroup listViewGroup3 = this.SourceItemList.Groups.Add("Traps", "Traps");
					ListViewGroup listViewGroup4 = this.SourceItemList.Groups.Add("Hazards", "Hazards");
					ListViewGroup listViewGroup5 = this.SourceItemList.Groups.Add("Terrain", "Terrain");
					List<ListViewItem> list4 = new List<ListViewItem>();
					foreach (Trap current7 in traps)
					{
						ListViewGroup lvg = null;
						switch (current7.Type)
						{
						case TrapType.Trap:
							lvg = listViewGroup3;
							break;
						case TrapType.Hazard:
							lvg = listViewGroup4;
							break;
						case TrapType.Terrain:
							lvg = listViewGroup5;
							break;
						}
						ListViewItem listViewItem8 = this.add_trap_to_list(current7, lvg);
						if (listViewItem8 != null)
						{
							list4.Add(listViewItem8);
						}
					}
					this.SourceItemList.Items.AddRange(list4.ToArray());
					if (this.SourceItemList.Items.Count == 0)
					{
						this.SourceItemList.ShowGroups = false;
						ListViewItem listViewItem9 = this.SourceItemList.Items.Add("(no traps)");
						listViewItem9.ForeColor = SystemColors.GrayText;
					}
					break;
				}
				case ListMode.SkillChallenges:
				{
					List<SkillChallenge> skillChallenges = Session.SkillChallenges;
					List<ListViewItem> list5 = new List<ListViewItem>();
					foreach (SkillChallenge current8 in skillChallenges)
					{
						ListViewItem listViewItem10 = this.add_challenge_to_list(current8);
						if (listViewItem10 != null)
						{
							list5.Add(listViewItem10);
						}
					}
					this.SourceItemList.Items.AddRange(list5.ToArray());
					if (this.SourceItemList.Items.Count == 0)
					{
						this.SourceItemList.ShowGroups = false;
						ListViewItem listViewItem11 = this.SourceItemList.Items.Add("(no skill challenges)");
						listViewItem11.ForeColor = SystemColors.GrayText;
					}
					break;
				}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			this.SourceItemList.EndUpdate();
			Cursor.Current = Cursors.Default;
		}

		private ListViewItem add_creature_to_list(ICreature c)
		{
			if (c == null)
			{
				return null;
			}
			Difficulty difficulty;
			if (!this.FilterPanel.AllowItem(c, out difficulty))
			{
				return null;
			}
			ListViewItem listViewItem = new ListViewItem(c.Name);
			ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems.Add(c.Info);
			listViewItem.Tag = c;
			listViewItem.UseItemStyleForSubItems = false;
			listViewSubItem.ForeColor = SystemColors.GrayText;
			Difficulty difficulty2 = difficulty;
			if (difficulty2 != Difficulty.Trivial)
			{
				if (difficulty2 == Difficulty.Extreme)
				{
					listViewItem.ForeColor = Color.Maroon;
				}
			}
			else
			{
				listViewItem.ForeColor = Color.Green;
			}
			if (c is CustomCreature)
			{
				listViewItem.Group = this.SourceItemList.Groups["Custom Creatures"];
			}
			else if (c.Category != null && c.Category != "")
			{
				listViewItem.Group = this.SourceItemList.Groups[c.Category];
			}
			else
			{
				listViewItem.Group = this.SourceItemList.Groups["Miscellaneous Creatures"];
			}
			return listViewItem;
		}

		private ListViewItem add_template_to_list(CreatureTemplate ct, ListViewGroup group)
		{
			if (ct == null)
			{
				return null;
			}
			Difficulty difficulty;
			if (!this.FilterPanel.AllowItem(ct, out difficulty))
			{
				return null;
			}
			ListViewItem listViewItem = new ListViewItem(ct.Name);
			ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems.Add(ct.Info);
			listViewItem.Group = group;
			listViewItem.Tag = ct;
			listViewItem.UseItemStyleForSubItems = false;
			listViewSubItem.ForeColor = SystemColors.GrayText;
			return listViewItem;
		}

		private ListViewItem add_npc_to_list(NPC npc, ListViewGroup group)
		{
			if (npc == null)
			{
				return null;
			}
			Difficulty difficulty;
			if (!this.FilterPanel.AllowItem(npc, out difficulty))
			{
				return null;
			}
			ListViewItem listViewItem = new ListViewItem(npc.Name);
			ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems.Add(npc.Info);
			listViewItem.Group = group;
			listViewItem.Tag = npc;
			listViewItem.UseItemStyleForSubItems = false;
			listViewSubItem.ForeColor = SystemColors.GrayText;
			if (difficulty == Difficulty.Trivial)
			{
				listViewItem.ForeColor = Color.Green;
			}
			if (difficulty == Difficulty.Extreme)
			{
				listViewItem.ForeColor = Color.Red;
			}
			return listViewItem;
		}

		private ListViewItem add_trap_to_list(Trap trap, ListViewGroup lvg)
		{
			if (trap == null)
			{
				return null;
			}
			Difficulty difficulty;
			if (!this.FilterPanel.AllowItem(trap, out difficulty))
			{
				return null;
			}
			ListViewItem listViewItem = new ListViewItem(trap.Name);
			ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems.Add(trap.Info);
			listViewItem.Group = lvg;
			listViewItem.Tag = trap;
			listViewItem.UseItemStyleForSubItems = false;
			listViewSubItem.ForeColor = SystemColors.GrayText;
			if (difficulty == Difficulty.Trivial)
			{
				listViewItem.ForeColor = Color.Green;
			}
			if (difficulty == Difficulty.Extreme)
			{
				listViewItem.ForeColor = Color.Red;
			}
			return listViewItem;
		}

		private ListViewItem add_challenge_to_list(SkillChallenge sc)
		{
			if (sc == null)
			{
				return null;
			}
			Difficulty difficulty;
			if (!this.FilterPanel.AllowItem(sc, out difficulty))
			{
				return null;
			}
			ListViewItem listViewItem = new ListViewItem(sc.Name);
			ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems.Add(sc.Info);
			listViewItem.Tag = sc;
			listViewItem.UseItemStyleForSubItems = false;
			listViewSubItem.ForeColor = SystemColors.GrayText;
			return listViewItem;
		}

		private void AddToken_Click(object sender, EventArgs e)
		{
			try
			{
				CustomTokenForm customTokenForm = new CustomTokenForm(new CustomToken
				{
					Name = "Custom Map Token",
					Type = CustomTokenType.Token
				});
				if (customTokenForm.ShowDialog() == DialogResult.OK)
				{
					this.fEncounter.CustomTokens.Add(customTokenForm.Token);
					this.update_encounter();
					this.update_mapthreats();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void CreaturesAddOverlay_Click(object sender, EventArgs e)
		{
			try
			{
				CustomOverlayForm customOverlayForm = new CustomOverlayForm(new CustomToken
				{
					Name = "Custom Overlay",
					Type = CustomTokenType.Overlay
				});
				if (customOverlayForm.ShowDialog() == DialogResult.OK)
				{
					this.fEncounter.CustomTokens.Add(customOverlayForm.Token);
					this.update_encounter();
					this.update_mapthreats();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapCreaturesRemoveAll_Click(object sender, EventArgs e)
		{
			foreach (EncounterSlot current in this.fEncounter.Slots)
			{
				foreach (CombatData current2 in current.CombatData)
				{
					current2.Location = CombatData.NoPoint;
				}
			}
			foreach (CustomToken current3 in this.fEncounter.CustomTokens)
			{
				current3.Data.Location = CombatData.NoPoint;
			}
			this.MapView.MapChanged();
			this.update_mapthreats();
		}

		private void MapCreaturesShowAll_Click(object sender, EventArgs e)
		{
			foreach (EncounterSlot current in this.fEncounter.Slots)
			{
				foreach (CombatData current2 in current.CombatData)
				{
					current2.Visible = true;
				}
			}
			foreach (CustomToken current3 in this.fEncounter.CustomTokens)
			{
				current3.Data.Visible = true;
			}
			this.MapView.MapChanged();
		}

		private void MapCreaturesHideAll_Click(object sender, EventArgs e)
		{
			foreach (EncounterSlot current in this.fEncounter.Slots)
			{
				foreach (CombatData current2 in current.CombatData)
				{
					current2.Visible = false;
				}
			}
			foreach (CustomToken current3 in this.fEncounter.CustomTokens)
			{
				current3.Data.Visible = false;
			}
			this.MapView.MapChanged();
		}

		private void ExportBtn_Click(object sender, EventArgs e)
		{
			if (this.MapView.Map != null)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.FileName = this.MapView.Name;
				saveFileDialog.Filter = "Bitmap Image|*.bmp|JPEG Image|*.jpg|GIF Image|*.gif|PNG Image|*.png";
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					ImageFormat format = ImageFormat.Bmp;
					switch (saveFileDialog.FilterIndex)
					{
					case 1:
						format = ImageFormat.Bmp;
						break;
					case 2:
						format = ImageFormat.Jpeg;
						break;
					case 3:
						format = ImageFormat.Gif;
						break;
					case 4:
						format = ImageFormat.Png;
						break;
					}
					Bitmap bitmap = Screenshot.Map(this.MapView);
					bitmap.Save(saveFileDialog.FileName, format);
				}
			}
		}

		private void MapThreatList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedMapThreat != null)
			{
				CreatureToken creatureToken = this.SelectedMapThreat as CreatureToken;
				if (creatureToken != null)
				{
					EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
					CreatureDetailsForm creatureDetailsForm = new CreatureDetailsForm(encounterSlot.Card);
					creatureDetailsForm.ShowDialog();
				}
				CustomToken customToken = this.SelectedMapThreat as CustomToken;
				if (customToken != null)
				{
					int index = this.fEncounter.CustomTokens.IndexOf(customToken);
					switch (customToken.Type)
					{
					case CustomTokenType.Token:
					{
						CustomTokenForm customTokenForm = new CustomTokenForm(customToken);
						if (customTokenForm.ShowDialog() == DialogResult.OK)
						{
							this.fEncounter.CustomTokens[index] = customTokenForm.Token;
							this.update_encounter();
							this.update_mapthreats();
							return;
						}
						break;
					}
					case CustomTokenType.Overlay:
					{
						CustomOverlayForm customOverlayForm = new CustomOverlayForm(customToken);
						if (customOverlayForm.ShowDialog() == DialogResult.OK)
						{
							this.fEncounter.CustomTokens[index] = customOverlayForm.Token;
							this.update_encounter();
							this.update_mapthreats();
						}
						break;
					}
					default:
						return;
					}
				}
			}
		}

		private void MapThreatList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedMapThreat != null)
			{
				base.DoDragDrop(this.SelectedMapThreat, DragDropEffects.Move);
			}
		}

		private void MapView_ItemDropped(object sender, EventArgs e)
		{
			this.update_mapthreats();
		}

		private void MapView_ItemMoved(object sender, MovementEventArgs e)
		{
		}

		private void MapView_SelectedTokensChanged(object sender, EventArgs e)
		{
		}

		private void MapView_HoverTokenChanged(object sender, EventArgs e)
		{
			string toolTipTitle = "";
			string text = "";
			CreatureToken creatureToken = this.MapView.HoverToken as CreatureToken;
			if (creatureToken != null)
			{
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
				toolTipTitle = encounterSlot.Card.Title;
				text = encounterSlot.Card.Info;
				text += Environment.NewLine;
				text += "Double-click for more details";
			}
			CustomToken customToken = this.MapView.HoverToken as CustomToken;
			if (customToken != null)
			{
				toolTipTitle = customToken.Name;
				text = "Double-click to edit";
			}
			this.Tooltip.ToolTipTitle = toolTipTitle;
			this.Tooltip.ToolTipIcon = ToolTipIcon.Info;
			this.Tooltip.SetToolTip(this.MapView, text);
		}

		private void MapView_TokenActivated(object sender, TokenEventArgs e)
		{
			CreatureToken creatureToken = e.Token as CreatureToken;
			if (creatureToken != null)
			{
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
				CreatureDetailsForm creatureDetailsForm = new CreatureDetailsForm(encounterSlot.Card);
				creatureDetailsForm.ShowDialog();
			}
			CustomToken customToken = e.Token as CustomToken;
			if (customToken != null)
			{
				int num = this.fEncounter.CustomTokens.IndexOf(customToken);
				if (num != -1)
				{
					switch (customToken.Type)
					{
					case CustomTokenType.Token:
					{
						CustomTokenForm customTokenForm = new CustomTokenForm(customToken);
						if (customTokenForm.ShowDialog() == DialogResult.OK)
						{
							this.fEncounter.CustomTokens[num] = customTokenForm.Token;
							this.update_encounter();
							this.update_mapthreats();
							return;
						}
						break;
					}
					case CustomTokenType.Overlay:
					{
						CustomOverlayForm customOverlayForm = new CustomOverlayForm(customToken);
						if (customOverlayForm.ShowDialog() == DialogResult.OK)
						{
							this.fEncounter.CustomTokens[num] = customOverlayForm.Token;
							this.update_encounter();
							this.update_mapthreats();
						}
						break;
					}
					default:
						return;
					}
				}
			}
		}

		private void MapView_DoubleClick(object sender, EventArgs e)
		{
			if (this.fEncounter.MapID == Guid.Empty)
			{
				this.MapBtn_Click(sender, e);
			}
		}

		private void MapBtn_Click(object sender, EventArgs e)
		{
			MapAreaSelectForm mapAreaSelectForm = new MapAreaSelectForm(this.fEncounter.MapID, this.fEncounter.MapAreaID);
			if (mapAreaSelectForm.ShowDialog() == DialogResult.OK)
			{
				Guid guid = (mapAreaSelectForm.Map != null) ? mapAreaSelectForm.Map.ID : Guid.Empty;
				Guid guid2 = (mapAreaSelectForm.MapArea != null) ? mapAreaSelectForm.MapArea.ID : Guid.Empty;
				if (guid == this.fEncounter.MapID && guid2 == this.fEncounter.MapAreaID)
				{
					return;
				}
				foreach (EncounterSlot current in this.fEncounter.Slots)
				{
					foreach (CombatData current2 in current.CombatData)
					{
						current2.Location = CombatData.NoPoint;
					}
				}
				foreach (CustomToken current3 in this.fEncounter.CustomTokens)
				{
					current3.Data.Location = CombatData.NoPoint;
				}
				this.fEncounter.MapID = guid;
				this.fEncounter.MapAreaID = guid2;
				this.MapView.Map = mapAreaSelectForm.Map;
				this.MapView.Viewpoint = ((mapAreaSelectForm.MapArea != null) ? mapAreaSelectForm.MapArea.Region : Rectangle.Empty);
				this.MapView.Encounter = this.fEncounter;
				this.update_mapthreats();
			}
		}

		private void PrintBtn_Click(object sender, EventArgs e)
		{
			MapPrintingForm mapPrintingForm = new MapPrintingForm(this.MapView);
			mapPrintingForm.ShowDialog();
		}

		private void MapToolsLOS_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.LineOfSight = !this.MapView.LineOfSight;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapToolsGridlines_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowGrid = ((this.MapView.ShowGrid == MapGridMode.None) ? MapGridMode.Overlay : MapGridMode.None);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapToolsGridLabels_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowGridLabels = !this.MapView.ShowGridLabels;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapToolsPictureTokens_Click(object sender, EventArgs e)
		{
			this.MapView.ShowPictureTokens = !this.MapView.ShowPictureTokens;
		}

		private void MapContextView_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTokens.Count != 1)
			{
				return;
			}
			CreatureToken creatureToken = this.MapView.SelectedTokens[0] as CreatureToken;
			if (creatureToken != null)
			{
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
				CreatureDetailsForm creatureDetailsForm = new CreatureDetailsForm(encounterSlot.Card);
				creatureDetailsForm.ShowDialog();
			}
			CustomToken customToken = this.MapView.SelectedTokens[0] as CustomToken;
			if (customToken != null)
			{
				int index = this.fEncounter.CustomTokens.IndexOf(customToken);
				switch (customToken.Type)
				{
				case CustomTokenType.Token:
				{
					CustomTokenForm customTokenForm = new CustomTokenForm(customToken);
					if (customTokenForm.ShowDialog() == DialogResult.OK)
					{
						this.fEncounter.CustomTokens[index] = customTokenForm.Token;
						this.update_encounter();
						this.update_mapthreats();
						return;
					}
					break;
				}
				case CustomTokenType.Overlay:
				{
					CustomOverlayForm customOverlayForm = new CustomOverlayForm(customToken);
					if (customOverlayForm.ShowDialog() == DialogResult.OK)
					{
						this.fEncounter.CustomTokens[index] = customOverlayForm.Token;
						this.update_encounter();
						this.update_mapthreats();
					}
					break;
				}
				default:
					return;
				}
			}
		}

		private void MapContextRemove_Click(object sender, EventArgs e)
		{
			foreach (IToken current in this.MapView.SelectedTokens)
			{
				CreatureToken creatureToken = current as CreatureToken;
				if (creatureToken != null)
				{
					creatureToken.Data.Location = CombatData.NoPoint;
				}
				CustomToken customToken = current as CustomToken;
				if (customToken != null)
				{
					customToken.Data.Location = CombatData.NoPoint;
				}
			}
			this.update_mapthreats();
		}

		private void MapContextRemoveEncounter_Click(object sender, EventArgs e)
		{
			foreach (IToken current in this.MapView.SelectedTokens)
			{
				CreatureToken creatureToken = current as CreatureToken;
				if (creatureToken != null)
				{
					EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
					encounterSlot.CombatData.Remove(creatureToken.Data);
					if (encounterSlot.CombatData.Count == 0)
					{
						this.fEncounter.Slots.Remove(encounterSlot);
					}
				}
				CustomToken customToken = current as CustomToken;
				if (customToken != null)
				{
					this.fEncounter.CustomTokens.Remove(customToken);
				}
			}
			this.update_encounter();
			this.update_mapthreats();
		}

		private void MapContextVisible_Click(object sender, EventArgs e)
		{
			foreach (IToken current in this.MapView.SelectedTokens)
			{
				CreatureToken creatureToken = current as CreatureToken;
				if (creatureToken != null)
				{
					creatureToken.Data.Visible = !creatureToken.Data.Visible;
					this.MapView.Invalidate();
				}
				CustomToken customToken = current as CustomToken;
				if (customToken != null)
				{
					customToken.Data.Visible = !customToken.Data.Visible;
					this.MapView.Invalidate();
				}
			}
		}

		private void MapContextSetPicture_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTokens.Count != 1)
			{
				return;
			}
			CreatureToken creatureToken = this.MapView.SelectedTokens[0] as CreatureToken;
			if (creatureToken != null)
			{
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
				ICreature creature = Session.FindCreature(encounterSlot.Card.CreatureID, SearchType.Global);
				if (creature != null)
				{
					OpenFileDialog openFileDialog = new OpenFileDialog();
					openFileDialog.Filter = Program.ImageFilter;
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						creature.Image = Image.FromFile(openFileDialog.FileName);
						Program.SetResolution(creature.Image);
						if (creature is Creature)
						{
							Creature c = creature as Creature;
							Library library = Session.FindLibrary(c);
							if (library != null)
							{
								string libraryFilename = Session.GetLibraryFilename(library);
								Serialisation<Library>.Save(libraryFilename, library, SerialisationMode.Binary);
							}
						}
						else
						{
							Session.Modified = true;
						}
						this.MapView.Invalidate();
					}
				}
			}
		}

		private void MapContextCopy_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTokens.Count != 1)
			{
				return;
			}
			CustomToken customToken = this.MapView.SelectedTokens[0] as CustomToken;
			if (customToken != null)
			{
				CustomToken customToken2 = customToken.Copy();
				customToken2.ID = Guid.NewGuid();
				customToken2.Data.Location = CombatData.NoPoint;
				this.fEncounter.CustomTokens.Add(customToken2);
			}
			this.update_mapthreats();
		}

		private void update_mapthreats()
		{
			this.MapThreatList.Items.Clear();
			this.MapThreatList.Groups.Clear();
			this.SlotList.Groups.Add("Combatants", "Combatants");
			foreach (EncounterWave current in this.fEncounter.Waves)
			{
				this.SlotList.Groups.Add(current.Name, current.Name);
			}
			this.SlotList.Groups.Add("Custom Tokens / Overlays", "Custom Tokens / Overlays");
			foreach (EncounterSlot current2 in this.fEncounter.AllSlots)
			{
				foreach (CombatData current3 in current2.CombatData)
				{
					if (current3.Location == CombatData.NoPoint)
					{
						ListViewItem listViewItem = this.MapThreatList.Items.Add(current3.DisplayName);
						listViewItem.Tag = new CreatureToken(current2.ID, current3);
						EncounterWave encounterWave = this.fEncounter.FindWave(current2);
						listViewItem.Group = this.MapThreatList.Groups[(encounterWave == null) ? "Combatants" : encounterWave.Name];
					}
				}
			}
			foreach (CustomToken current4 in this.fEncounter.CustomTokens)
			{
				if (current4.Data.Location == CombatData.NoPoint)
				{
					ListViewItem listViewItem2 = this.MapThreatList.Items.Add(current4.Name);
					listViewItem2.Tag = current4;
					listViewItem2.Group = this.MapThreatList.Groups["Custom Tokens / Overlays"];
				}
			}
			if (this.MapThreatList.Items.Count == 0)
			{
				this.MapView.Caption = "";
				return;
			}
			this.MapView.Caption = "Drag creatures from the list to place them on the map";
		}

		private void NoteAddBtn_Click(object sender, EventArgs e)
		{
			try
			{
				EncounterNote bg = new EncounterNote("New Note");
				EncounterNoteForm encounterNoteForm = new EncounterNoteForm(bg);
				if (encounterNoteForm.ShowDialog() == DialogResult.OK)
				{
					this.fEncounter.Notes.Add(encounterNoteForm.Note);
					this.update_notes();
					this.SelectedNote = encounterNoteForm.Note;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void NoteRemoveBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedNote != null)
				{
					string text = "Remove encounter note: are you sure?";
					if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
					{
						this.fEncounter.Notes.Remove(this.SelectedNote);
						this.update_notes();
						this.SelectedNote = null;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void NoteEditBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedNote != null)
				{
					int index = this.fEncounter.Notes.IndexOf(this.SelectedNote);
					EncounterNoteForm encounterNoteForm = new EncounterNoteForm(this.SelectedNote);
					if (encounterNoteForm.ShowDialog() == DialogResult.OK)
					{
						this.fEncounter.Notes[index] = encounterNoteForm.Note;
						this.update_notes();
						this.SelectedNote = encounterNoteForm.Note;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void NoteUpBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedNote != null && this.fEncounter.Notes.IndexOf(this.SelectedNote) != 0)
				{
					int num = this.fEncounter.Notes.IndexOf(this.SelectedNote);
					EncounterNote value = this.fEncounter.Notes[num - 1];
					this.fEncounter.Notes[num - 1] = this.SelectedNote;
					this.fEncounter.Notes[num] = value;
					this.update_notes();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void NoteDownBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedNote != null && this.fEncounter.Notes.IndexOf(this.SelectedNote) != this.fEncounter.Notes.Count - 1)
				{
					int num = this.fEncounter.Notes.IndexOf(this.SelectedNote);
					EncounterNote value = this.fEncounter.Notes[num + 1];
					this.fEncounter.Notes[num + 1] = this.SelectedNote;
					this.fEncounter.Notes[num] = value;
					this.update_notes();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void NoteList_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.update_selected_note();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void NoteDetails_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			try
			{
				if (e.Url.Scheme == "note")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "edit")
					{
						this.NoteEditBtn_Click(sender, e);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void update_notes()
		{
			try
			{
				EncounterNote selectedNote = this.SelectedNote;
				this.NoteList.Items.Clear();
				foreach (EncounterNote current in this.fEncounter.Notes)
				{
					ListViewItem listViewItem = this.NoteList.Items.Add(current.Title);
					listViewItem.Tag = current;
					if (current.Contents == "")
					{
						listViewItem.ForeColor = SystemColors.GrayText;
					}
					if (current == selectedNote)
					{
						listViewItem.Selected = true;
					}
				}
				if (this.NoteList.Items.Count == 0)
				{
					ListViewItem listViewItem2 = this.NoteList.Items.Add("(no notes)");
					listViewItem2.ForeColor = SystemColors.GrayText;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void update_selected_note()
		{
			try
			{
				this.NoteDetails.Document.OpenNew(true);
				this.NoteDetails.Document.Write(HTML.EncounterNote(this.SelectedNote, DisplaySize.Small));
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void SourceItemList_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			EncounterBuilderForm.SourceSorter sourceSorter = this.SourceItemList.ListViewItemSorter as EncounterBuilderForm.SourceSorter;
			sourceSorter.Set(e.Column);
			this.SourceItemList.Sort();
		}

		private void ViewGroups_Click(object sender, EventArgs e)
		{
			this.SourceItemList.ShowGroups = !this.SourceItemList.ShowGroups;
		}

		private void ToolsExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.FileName = "Encounter";
			saveFileDialog.Filter = Program.EncounterFilter;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				string contents = EncounterExporter.ExportXML(this.fEncounter);
				File.WriteAllText(saveFileDialog.FileName, contents);
			}
		}

		private void ThreatContextMenu_Opening(object sender, CancelEventArgs e)
		{
			this.EditStatBlock.Enabled = (this.SelectedSlot != null || this.SelectedSlotTrap != null || this.SelectedSlotSkillChallenge != null);
			this.EditSetFaction.Enabled = (this.SelectedSlot != null);
			this.EditSetFaction.DropDownItems.Clear();
			foreach (EncounterSlotType encounterSlotType in Enum.GetValues(typeof(EncounterSlotType)))
			{
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(encounterSlotType.ToString());
				toolStripMenuItem.Tag = encounterSlotType;
				toolStripMenuItem.Enabled = (this.SelectedSlot != null);
				toolStripMenuItem.Checked = (this.SelectedSlot != null && this.SelectedSlot.Type == encounterSlotType);
				toolStripMenuItem.Click += new EventHandler(this.count_slot_as);
				this.EditSetFaction.DropDownItems.Add(toolStripMenuItem);
			}
			this.EditRemoveTemplate.Enabled = (this.SelectedSlot != null && this.SelectedSlot.Card.TemplateIDs.Count != 0);
			this.EditRemoveLevelAdj.Enabled = (this.SelectedSlot != null && this.SelectedSlot.Card.LevelAdjustment != 0);
			this.EditSwap.Enabled = (this.SelectedSlot != null);
			this.EditSetWave.Enabled = (this.SelectedSlot != null);
			this.EditSetWave.DropDownItems.Clear();
			ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem("Initial Wave");
			toolStripMenuItem2.Tag = this.fEncounter;
			toolStripMenuItem2.Enabled = (this.SelectedSlot != null);
			toolStripMenuItem2.Checked = (this.SelectedSlot != null && this.fEncounter.Slots.Contains(this.SelectedSlot));
			toolStripMenuItem2.Click += new EventHandler(this.wave_initial);
			this.EditSetWave.DropDownItems.Add(toolStripMenuItem2);
			foreach (EncounterWave current in this.fEncounter.Waves)
			{
				ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem(current.Name);
				toolStripMenuItem3.Tag = current;
				toolStripMenuItem3.Enabled = (this.SelectedSlot != null);
				toolStripMenuItem3.Checked = (this.SelectedSlot != null && this.fEncounter.FindWave(this.SelectedSlot) == current);
				toolStripMenuItem3.Click += new EventHandler(this.wave_subsequent);
				this.EditSetWave.DropDownItems.Add(toolStripMenuItem3);
			}
			ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem("New Wave...");
			toolStripMenuItem4.Tag = null;
			toolStripMenuItem4.Enabled = (this.SelectedSlot != null);
			toolStripMenuItem4.Checked = false;
			toolStripMenuItem4.Click += new EventHandler(this.wave_new);
			this.EditSetWave.DropDownItems.Add(toolStripMenuItem4);
			if (this.SelectedSlot == null)
			{
				this.SwapStandard.Enabled = false;
				this.SwapElite.Enabled = false;
				this.SwapSolo.Enabled = false;
				this.SwapMinions.Enabled = false;
			}
			else
			{
				ICreature creature = Session.FindCreature(this.SelectedSlot.Card.CreatureID, SearchType.Global);
				if (creature == null)
				{
					this.SwapStandard.Enabled = false;
					this.SwapElite.Enabled = false;
					this.SwapSolo.Enabled = false;
					this.SwapMinions.Enabled = false;
				}
				else
				{
					bool flag = creature.Role is Minion;
					if (flag)
					{
						this.SwapStandard.Enabled = (this.SelectedSlot.CombatData.Count >= 4 && this.SelectedSlot.CombatData.Count % 4 == 0);
						this.SwapElite.Enabled = (this.SelectedSlot.CombatData.Count >= 8 && this.SelectedSlot.CombatData.Count % 8 == 0);
						this.SwapSolo.Enabled = (this.SelectedSlot.CombatData.Count >= 20 && this.SelectedSlot.CombatData.Count % 20 == 0);
						this.SwapMinions.Enabled = false;
					}
					else
					{
						RoleFlag arg_486_0 = this.SelectedSlot.Card.Flag;
						this.SwapStandard.Enabled = true;
						this.SwapElite.Enabled = (this.SelectedSlot.CombatData.Count >= 2 && this.SelectedSlot.CombatData.Count % 2 == 0);
						this.SwapSolo.Enabled = (this.SelectedSlot.CombatData.Count >= 5 && this.SelectedSlot.CombatData.Count % 5 == 0);
						this.SwapMinions.Enabled = true;
					}
				}
			}
			if (this.SelectedSlot != null)
			{
				this.EditApplyTheme.Enabled = (this.SelectedSlot.Card != null);
				this.EditClearTheme.Enabled = (this.SelectedSlot.Card != null && this.SelectedSlot.Card.ThemeID != Guid.Empty);
				return;
			}
			this.EditApplyTheme.Enabled = false;
			this.EditClearTheme.Enabled = false;
		}

		private void wave_initial(object sender, EventArgs e)
		{
			EncounterWave encounterWave = this.fEncounter.FindWave(this.SelectedSlot);
			if (encounterWave != null)
			{
				encounterWave.Slots.Remove(this.SelectedSlot);
				this.fEncounter.Slots.Add(this.SelectedSlot);
			}
			this.update_encounter();
			this.update_mapthreats();
		}

		private void wave_subsequent(object sender, EventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			EncounterWave encounterWave = toolStripMenuItem.Tag as EncounterWave;
			if (encounterWave != null)
			{
				EncounterWave encounterWave2 = this.fEncounter.FindWave(this.SelectedSlot);
				if (encounterWave2 == null)
				{
					this.fEncounter.Slots.Remove(this.SelectedSlot);
				}
				else
				{
					encounterWave2.Slots.Remove(this.SelectedSlot);
				}
				encounterWave.Slots.Add(this.SelectedSlot);
			}
			this.update_encounter();
			this.update_mapthreats();
		}

		private void wave_new(object sender, EventArgs e)
		{
			EncounterWave encounterWave = new EncounterWave();
			encounterWave.Name = "Wave " + (this.fEncounter.Waves.Count + 2);
			this.fEncounter.Waves.Add(encounterWave);
			EncounterWave encounterWave2 = this.fEncounter.FindWave(this.SelectedSlot);
			if (encounterWave2 == null)
			{
				this.fEncounter.Slots.Remove(this.SelectedSlot);
			}
			else
			{
				encounterWave2.Slots.Remove(this.SelectedSlot);
			}
			encounterWave.Slots.Add(this.SelectedSlot);
			this.update_encounter();
			this.update_mapthreats();
		}

		private void InfoBtn_Click(object sender, EventArgs e)
		{
			new InfoForm
			{
				Level = this.fPartyLevel
			}.ShowDialog();
		}

		private void DieRollerBtn_Click(object sender, EventArgs e)
		{
			DieRollerForm dieRollerForm = new DieRollerForm();
			dieRollerForm.ShowDialog();
		}

		private void PartyLbl_Click(object sender, EventArgs e)
		{
			Party p = new Party(this.fPartySize, this.fPartyLevel);
			PartyForm partyForm = new PartyForm(p);
			if (partyForm.ShowDialog() == DialogResult.OK)
			{
				this.fPartySize = partyForm.Party.Size;
				this.fPartyLevel = partyForm.Party.Level;
				this.update_difficulty_list();
				this.update_encounter();
				this.update_party_label();
			}
		}

		private void update_party_label()
		{
			this.PartyLbl.Text = this.fPartySize + " PCs at level " + this.fPartyLevel;
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
			this.components = new Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(EncounterBuilderForm));
			this.DifficultyList = new ListView();
			this.DiffHdr = new ColumnHeader();
			this.DiffXPHdr = new ColumnHeader();
			this.DiffLevels = new ColumnHeader();
			this.SlotList = new ListView();
			this.ThreatHdr = new ColumnHeader();
			this.ThreatInfoHdr = new ColumnHeader();
			this.CountHdr = new ColumnHeader();
			this.XPHdr = new ColumnHeader();
			this.ThreatContextMenu = new ContextMenuStrip(this.components);
			this.EditStatBlock = new ToolStripMenuItem();
			this.EditApplyTheme = new ToolStripMenuItem();
			this.toolStripSeparator14 = new ToolStripSeparator();
			this.EditRemoveTemplate = new ToolStripMenuItem();
			this.EditRemoveLevelAdj = new ToolStripMenuItem();
			this.EditClearTheme = new ToolStripMenuItem();
			this.toolStripSeparator16 = new ToolStripSeparator();
			this.EditSetFaction = new ToolStripMenuItem();
			this.EditSetWave = new ToolStripMenuItem();
			this.toolStripSeparator9 = new ToolStripSeparator();
			this.EditSwap = new ToolStripMenuItem();
			this.SwapStandard = new ToolStripMenuItem();
			this.toolStripMenuItem2 = new ToolStripSeparator();
			this.SwapElite = new ToolStripMenuItem();
			this.SwapSolo = new ToolStripMenuItem();
			this.toolStripSeparator11 = new ToolStripSeparator();
			this.SwapMinions = new ToolStripMenuItem();
			this.EncToolbar = new ToolStrip();
			this.AddBtn = new ToolStripButton();
			this.RemoveBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.StatBlockBtn = new ToolStripSplitButton();
			this.StatBlockEdit = new ToolStripMenuItem();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.AddMenu = new ToolStripDropDownButton();
			this.ToolsAddCreature = new ToolStripMenuItem();
			this.ToolsAddTrap = new ToolStripMenuItem();
			this.ToolsAddChallenge = new ToolStripMenuItem();
			this.ToolsMenu = new ToolStripDropDownButton();
			this.ToolsClearAll = new ToolStripMenuItem();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.ToolsUseTemplate = new ToolStripMenuItem();
			this.ToolsUseDeck = new ToolStripMenuItem();
			this.decksToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.ToolsApplyTheme = new ToolStripMenuItem();
			this.toolStripSeparator13 = new ToolStripSeparator();
			this.ToolsExport = new ToolStripMenuItem();
			this.AutoBuildBtn = new ToolStripSplitButton();
			this.AutoBuildAdvanced = new ToolStripMenuItem();
			this.SourceItemList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.ThreatToolbar = new ToolStrip();
			this.CreaturesBtn = new ToolStripButton();
			this.TrapsBtn = new ToolStripButton();
			this.ChallengesBtn = new ToolStripButton();
			this.ViewMenu = new ToolStripDropDownButton();
			this.ViewTemplates = new ToolStripMenuItem();
			this.ViewNPCs = new ToolStripMenuItem();
			this.toolStripSeparator12 = new ToolStripSeparator();
			this.ViewGroups = new ToolStripMenuItem();
			this.Pages = new TabControl();
			this.ThreatsPage = new TabPage();
			this.HSplitter = new SplitContainer();
			this.VSplitter = new SplitContainer();
			this.HintStatusbar = new StatusStrip();
			this.HintLbl = new ToolStripStatusLabel();
			this.XPStatusbar = new StatusStrip();
			this.XPLbl = new ToolStripStatusLabel();
			this.LevelLbl = new ToolStripStatusLabel();
			this.DiffLbl = new ToolStripStatusLabel();
			this.CountLbl = new ToolStripStatusLabel();
			this.PartyLbl = new ToolStripStatusLabel();
			this.MapPage = new TabPage();
			this.MapSplitter = new SplitContainer();
			this.MapContextMenu = new ContextMenuStrip(this.components);
			this.MapContextView = new ToolStripMenuItem();
			this.toolStripSeparator15 = new ToolStripSeparator();
			this.MapContextSetPicture = new ToolStripMenuItem();
			this.toolStripMenuItem4 = new ToolStripSeparator();
			this.MapContextRemove = new ToolStripMenuItem();
			this.MapContextRemoveEncounter = new ToolStripMenuItem();
			this.MapContextCopy = new ToolStripMenuItem();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.MapContextVisible = new ToolStripMenuItem();
			this.MapThreatList = new ListView();
			this.ThreatNameHdr = new ColumnHeader();
			this.MapToolbar = new ToolStrip();
			this.MapBtn = new ToolStripButton();
			this.toolStripSeparator6 = new ToolStripSeparator();
			this.MapToolsMenu = new ToolStripDropDownButton();
			this.MapToolsLOS = new ToolStripMenuItem();
			this.MapToolsGridlines = new ToolStripMenuItem();
			this.MapToolsGridLabels = new ToolStripMenuItem();
			this.MapToolsPictureTokens = new ToolStripMenuItem();
			this.toolStripMenuItem5 = new ToolStripSeparator();
			this.MapToolsPrint = new ToolStripMenuItem();
			this.MapToolsScreenshot = new ToolStripMenuItem();
			this.MapCreaturesMenu = new ToolStripDropDownButton();
			this.MapCreaturesRemove = new ToolStripMenuItem();
			this.toolStripSeparator7 = new ToolStripSeparator();
			this.MapCreaturesShowAll = new ToolStripMenuItem();
			this.MapCreaturesHideAll = new ToolStripMenuItem();
			this.toolStripSeparator8 = new ToolStripSeparator();
			this.CreaturesAddCustom = new ToolStripMenuItem();
			this.CreaturesAddOverlay = new ToolStripMenuItem();
			this.NotesPage = new TabPage();
			this.NoteSplitter = new SplitContainer();
			this.NoteList = new ListView();
			this.NoteHdr = new ColumnHeader();
			this.BackgroundPanel = new Panel();
			this.NoteDetails = new WebBrowser();
			this.NoteToolbar = new ToolStrip();
			this.NoteAddBtn = new ToolStripButton();
			this.NoteRemoveBtn = new ToolStripButton();
			this.NoteEditBtn = new ToolStripButton();
			this.toolStripSeparator21 = new ToolStripSeparator();
			this.NoteUpBtn = new ToolStripButton();
			this.NoteDownBtn = new ToolStripButton();
			this.Tooltip = new ToolTip(this.components);
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.InfoBtn = new Button();
			this.DieRollerBtn = new Button();
			this.XPGauge = new EncounterGauge();
			this.FilterPanel = new FilterPanel();
			this.MapView = new MapView();
			this.ThreatContextMenu.SuspendLayout();
			this.EncToolbar.SuspendLayout();
			this.ThreatToolbar.SuspendLayout();
			this.Pages.SuspendLayout();
			this.ThreatsPage.SuspendLayout();
			this.HSplitter.Panel1.SuspendLayout();
			this.HSplitter.Panel2.SuspendLayout();
			this.HSplitter.SuspendLayout();
			this.VSplitter.Panel1.SuspendLayout();
			this.VSplitter.Panel2.SuspendLayout();
			this.VSplitter.SuspendLayout();
			this.HintStatusbar.SuspendLayout();
			this.XPStatusbar.SuspendLayout();
			this.MapPage.SuspendLayout();
			this.MapSplitter.Panel1.SuspendLayout();
			this.MapSplitter.Panel2.SuspendLayout();
			this.MapSplitter.SuspendLayout();
			this.MapContextMenu.SuspendLayout();
			this.MapToolbar.SuspendLayout();
			this.NotesPage.SuspendLayout();
			this.NoteSplitter.Panel1.SuspendLayout();
			this.NoteSplitter.Panel2.SuspendLayout();
			this.NoteSplitter.SuspendLayout();
			this.BackgroundPanel.SuspendLayout();
			this.NoteToolbar.SuspendLayout();
			base.SuspendLayout();
			this.DifficultyList.Columns.AddRange(new ColumnHeader[]
			{
				this.DiffHdr,
				this.DiffXPHdr,
				this.DiffLevels
			});
			this.DifficultyList.Dock = DockStyle.Fill;
			this.DifficultyList.Enabled = false;
			this.DifficultyList.FullRowSelect = true;
			this.DifficultyList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.DifficultyList.HideSelection = false;
			this.DifficultyList.Location = new Point(0, 0);
			this.DifficultyList.MultiSelect = false;
			this.DifficultyList.Name = "DifficultyList";
			this.DifficultyList.Size = new Size(472, 86);
			this.DifficultyList.TabIndex = 0;
			this.DifficultyList.UseCompatibleStateImageBehavior = false;
			this.DifficultyList.View = View.Details;
			this.DiffHdr.Text = "Difficulty";
			this.DiffHdr.Width = 204;
			this.DiffXPHdr.Text = "XP Range";
			this.DiffXPHdr.TextAlign = HorizontalAlignment.Center;
			this.DiffXPHdr.Width = 120;
			this.DiffLevels.Text = "Creature Levels";
			this.DiffLevels.TextAlign = HorizontalAlignment.Center;
			this.DiffLevels.Width = 120;
			this.SlotList.AllowDrop = true;
			this.SlotList.Columns.AddRange(new ColumnHeader[]
			{
				this.ThreatHdr,
				this.ThreatInfoHdr,
				this.CountHdr,
				this.XPHdr
			});
			this.SlotList.ContextMenuStrip = this.ThreatContextMenu;
			this.SlotList.Dock = DockStyle.Fill;
			this.SlotList.FullRowSelect = true;
			this.SlotList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SlotList.HideSelection = false;
			this.SlotList.Location = new Point(0, 25);
			this.SlotList.MultiSelect = false;
			this.SlotList.Name = "SlotList";
			this.SlotList.Size = new Size(472, 206);
			this.SlotList.Sorting = SortOrder.Ascending;
			this.SlotList.TabIndex = 1;
			this.SlotList.UseCompatibleStateImageBehavior = false;
			this.SlotList.View = View.Details;
			this.SlotList.DoubleClick += new EventHandler(this.SlotList_DoubleClick);
			this.SlotList.DragDrop += new DragEventHandler(this.SlotList_DragDrop);
			this.SlotList.KeyDown += new KeyEventHandler(this.SlotList_KeyDown);
			this.SlotList.DragOver += new DragEventHandler(this.SlotList_DragOver);
			this.ThreatHdr.Text = "Threat";
			this.ThreatHdr.Width = 207;
			this.ThreatInfoHdr.Text = "Info";
			this.ThreatInfoHdr.Width = 120;
			this.CountHdr.Text = "Count";
			this.CountHdr.TextAlign = HorizontalAlignment.Right;
			this.XPHdr.Text = "XP";
			this.XPHdr.TextAlign = HorizontalAlignment.Right;
			this.ThreatContextMenu.Items.AddRange(new ToolStripItem[]
			{
				this.EditStatBlock,
				this.EditApplyTheme,
				this.toolStripSeparator14,
				this.EditRemoveTemplate,
				this.EditRemoveLevelAdj,
				this.EditClearTheme,
				this.toolStripSeparator16,
				this.EditSetFaction,
				this.EditSetWave,
				this.toolStripSeparator9,
				this.EditSwap
			});
			this.ThreatContextMenu.Name = "ThreatContextMenu";
			this.ThreatContextMenu.Size = new Size(213, 198);
			this.ThreatContextMenu.Opening += new CancelEventHandler(this.ThreatContextMenu_Opening);
			this.EditStatBlock.Name = "EditStatBlock";
			this.EditStatBlock.Size = new Size(212, 22);
			this.EditStatBlock.Text = "Edit Stat Block...";
			this.EditStatBlock.Click += new EventHandler(this.EditStatBlock_Click);
			this.EditApplyTheme.Name = "EditApplyTheme";
			this.EditApplyTheme.Size = new Size(212, 22);
			this.EditApplyTheme.Text = "Apply Theme...";
			this.EditApplyTheme.Click += new EventHandler(this.EditApplyTheme_Click);
			this.toolStripSeparator14.Name = "toolStripSeparator14";
			this.toolStripSeparator14.Size = new Size(209, 6);
			this.EditRemoveTemplate.Name = "EditRemoveTemplate";
			this.EditRemoveTemplate.Size = new Size(212, 22);
			this.EditRemoveTemplate.Text = "Remove Template";
			this.EditRemoveTemplate.Click += new EventHandler(this.EditRemoveTemplate_Click);
			this.EditRemoveLevelAdj.Name = "EditRemoveLevelAdj";
			this.EditRemoveLevelAdj.Size = new Size(212, 22);
			this.EditRemoveLevelAdj.Text = "Remove Level Adjustment";
			this.EditRemoveLevelAdj.Click += new EventHandler(this.EditRemoveLevelAdj_Click);
			this.EditClearTheme.Name = "EditClearTheme";
			this.EditClearTheme.Size = new Size(212, 22);
			this.EditClearTheme.Text = "Remove Theme";
			this.EditClearTheme.Click += new EventHandler(this.EditClearTheme_Click);
			this.toolStripSeparator16.Name = "toolStripSeparator16";
			this.toolStripSeparator16.Size = new Size(209, 6);
			this.EditSetFaction.Name = "EditSetFaction";
			this.EditSetFaction.Size = new Size(212, 22);
			this.EditSetFaction.Text = "Set Faction";
			this.EditSetWave.Name = "EditSetWave";
			this.EditSetWave.Size = new Size(212, 22);
			this.EditSetWave.Text = "Set Wave";
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new Size(209, 6);
			this.EditSwap.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.SwapStandard,
				this.toolStripMenuItem2,
				this.SwapElite,
				this.SwapSolo,
				this.toolStripSeparator11,
				this.SwapMinions
			});
			this.EditSwap.Name = "EditSwap";
			this.EditSwap.Size = new Size(212, 22);
			this.EditSwap.Text = "Swap For";
			this.SwapStandard.Name = "SwapStandard";
			this.SwapStandard.Size = new Size(169, 22);
			this.SwapStandard.Text = "Standard Creature";
			this.SwapStandard.Click += new EventHandler(this.SwapStandard_Click);
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new Size(166, 6);
			this.SwapElite.Name = "SwapElite";
			this.SwapElite.Size = new Size(169, 22);
			this.SwapElite.Text = "Elite Creature";
			this.SwapElite.Click += new EventHandler(this.SwapElite_Click);
			this.SwapSolo.Name = "SwapSolo";
			this.SwapSolo.Size = new Size(169, 22);
			this.SwapSolo.Text = "Solo Creature";
			this.SwapSolo.Click += new EventHandler(this.SwapSolo_Click);
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new Size(166, 6);
			this.SwapMinions.Name = "SwapMinions";
			this.SwapMinions.Size = new Size(169, 22);
			this.SwapMinions.Text = "Minions";
			this.SwapMinions.Click += new EventHandler(this.SwapMinions_Click);
			this.EncToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.AddBtn,
				this.RemoveBtn,
				this.toolStripSeparator1,
				this.StatBlockBtn,
				this.toolStripSeparator4,
				this.AddMenu,
				this.ToolsMenu,
				this.AutoBuildBtn
			});
			this.EncToolbar.Location = new Point(0, 0);
			this.EncToolbar.Name = "EncToolbar";
			this.EncToolbar.Size = new Size(472, 25);
			this.EncToolbar.TabIndex = 0;
			this.EncToolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddBtn.Image = (Image)resources.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new Size(23, 22);
			this.AddBtn.Text = "+";
			this.AddBtn.ToolTipText = "Adjust number (hold shift to adjust level)";
			this.AddBtn.Click += new EventHandler(this.AddBtn_Click);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)resources.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new Size(23, 22);
			this.RemoveBtn.Text = "-";
			this.RemoveBtn.ToolTipText = "Adjust number (hold shift to adjust level)";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.StatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.StatBlockBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.StatBlockEdit
			});
			this.StatBlockBtn.Image = (Image)resources.GetObject("StatBlockBtn.Image");
			this.StatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.StatBlockBtn.Name = "StatBlockBtn";
			this.StatBlockBtn.Size = new Size(75, 22);
			this.StatBlockBtn.Text = "Stat Block";
			this.StatBlockBtn.ButtonClick += new EventHandler(this.StatBlockBtn_Click);
			this.StatBlockEdit.Name = "StatBlockEdit";
			this.StatBlockEdit.Size = new Size(103, 22);
			this.StatBlockEdit.Text = "Edit...";
			this.StatBlockEdit.Click += new EventHandler(this.EditStatBlock_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new Size(6, 25);
			this.AddMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ToolsAddCreature,
				this.ToolsAddTrap,
				this.ToolsAddChallenge
			});
			this.AddMenu.Image = (Image)resources.GetObject("AddMenu.Image");
			this.AddMenu.ImageTransparentColor = Color.Magenta;
			this.AddMenu.Name = "AddMenu";
			this.AddMenu.Size = new Size(42, 22);
			this.AddMenu.Text = "Add";
			this.ToolsAddCreature.Name = "ToolsAddCreature";
			this.ToolsAddCreature.ShortcutKeys = (Keys)131149;
			this.ToolsAddCreature.Size = new Size(270, 22);
			this.ToolsAddCreature.Text = "Add Custom Creature...";
			this.ToolsAddCreature.Click += new EventHandler(this.ToolsAddCreature_Click);
			this.ToolsAddTrap.Name = "ToolsAddTrap";
			this.ToolsAddTrap.ShortcutKeys = (Keys)131156;
			this.ToolsAddTrap.Size = new Size(270, 22);
			this.ToolsAddTrap.Text = "Add Custom Trap...";
			this.ToolsAddTrap.Click += new EventHandler(this.ToolsAddTrap_Click);
			this.ToolsAddChallenge.Name = "ToolsAddChallenge";
			this.ToolsAddChallenge.ShortcutKeys = (Keys)131155;
			this.ToolsAddChallenge.Size = new Size(270, 22);
			this.ToolsAddChallenge.Text = "Add Custom Skill Challenge...";
			this.ToolsAddChallenge.Click += new EventHandler(this.ToolsAddChallenge_Click);
			this.ToolsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ToolsMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ToolsClearAll,
				this.toolStripSeparator3,
				this.ToolsUseTemplate,
				this.ToolsUseDeck,
				this.toolStripSeparator2,
				this.ToolsApplyTheme,
				this.toolStripSeparator13,
				this.ToolsExport
			});
			this.ToolsMenu.Image = (Image)resources.GetObject("ToolsMenu.Image");
			this.ToolsMenu.ImageTransparentColor = Color.Magenta;
			this.ToolsMenu.Name = "ToolsMenu";
			this.ToolsMenu.Size = new Size(49, 22);
			this.ToolsMenu.Text = "Tools";
			this.ToolsMenu.DropDownOpening += new EventHandler(this.ToolsMenu_DropDownOpening);
			this.ToolsClearAll.Name = "ToolsClearAll";
			this.ToolsClearAll.Size = new Size(212, 22);
			this.ToolsClearAll.Text = "Clear All";
			this.ToolsClearAll.Click += new EventHandler(this.ToolsClearAll_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new Size(209, 6);
			this.ToolsUseTemplate.Name = "ToolsUseTemplate";
			this.ToolsUseTemplate.Size = new Size(212, 22);
			this.ToolsUseTemplate.Text = "Use Encounter Template...";
			this.ToolsUseTemplate.Click += new EventHandler(this.ToolsUseTemplate_Click);
			this.ToolsUseDeck.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.decksToolStripMenuItem
			});
			this.ToolsUseDeck.Name = "ToolsUseDeck";
			this.ToolsUseDeck.Size = new Size(212, 22);
			this.ToolsUseDeck.Text = "Use Encounter Deck";
			this.decksToolStripMenuItem.Name = "decksToolStripMenuItem";
			this.decksToolStripMenuItem.Size = new Size(112, 22);
			this.decksToolStripMenuItem.Text = "(decks)";
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(209, 6);
			this.ToolsApplyTheme.Name = "ToolsApplyTheme";
			this.ToolsApplyTheme.Size = new Size(212, 22);
			this.ToolsApplyTheme.Text = "Apply Theme to All...";
			this.ToolsApplyTheme.Click += new EventHandler(this.ToolsApplyTheme_Click);
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new Size(209, 6);
			this.ToolsExport.Name = "ToolsExport";
			this.ToolsExport.Size = new Size(212, 22);
			this.ToolsExport.Text = "Export Encounter File...";
			this.ToolsExport.Click += new EventHandler(this.ToolsExport_Click);
			this.AutoBuildBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AutoBuildBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.AutoBuildAdvanced
			});
			this.AutoBuildBtn.Image = (Image)resources.GetObject("AutoBuildBtn.Image");
			this.AutoBuildBtn.ImageTransparentColor = Color.Magenta;
			this.AutoBuildBtn.Name = "AutoBuildBtn";
			this.AutoBuildBtn.Size = new Size(76, 22);
			this.AutoBuildBtn.Text = "AutoBuild";
			this.AutoBuildBtn.ButtonClick += new EventHandler(this.AutoBuildBtn_Click);
			this.AutoBuildAdvanced.Name = "AutoBuildAdvanced";
			this.AutoBuildAdvanced.Size = new Size(136, 22);
			this.AutoBuildAdvanced.Text = "Advanced...";
			this.AutoBuildAdvanced.Click += new EventHandler(this.AutoBuildAdvanced_Click);
			this.SourceItemList.Columns.AddRange(new ColumnHeader[]
			{
				this.NameHdr,
				this.InfoHdr
			});
			this.SourceItemList.Dock = DockStyle.Fill;
			this.SourceItemList.FullRowSelect = true;
			this.SourceItemList.HideSelection = false;
			this.SourceItemList.Location = new Point(0, 49);
			this.SourceItemList.MultiSelect = false;
			this.SourceItemList.Name = "SourceItemList";
			this.SourceItemList.Size = new Size(320, 314);
			this.SourceItemList.Sorting = SortOrder.Ascending;
			this.SourceItemList.TabIndex = 2;
			this.SourceItemList.UseCompatibleStateImageBehavior = false;
			this.SourceItemList.View = View.Details;
			this.SourceItemList.DoubleClick += new EventHandler(this.ThreatList_DoubleClick);
			this.SourceItemList.ColumnClick += new ColumnClickEventHandler(this.SourceItemList_ColumnClick);
			this.SourceItemList.ItemDrag += new ItemDragEventHandler(this.OpponentList_ItemDrag);
			this.NameHdr.Text = "Creatures";
			this.NameHdr.Width = 139;
			this.InfoHdr.Text = "Info";
			this.InfoHdr.Width = 140;
			this.ThreatToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.CreaturesBtn,
				this.TrapsBtn,
				this.ChallengesBtn,
				this.ViewMenu
			});
			this.ThreatToolbar.Location = new Point(0, 0);
			this.ThreatToolbar.Name = "ThreatToolbar";
			this.ThreatToolbar.Size = new Size(320, 25);
			this.ThreatToolbar.TabIndex = 0;
			this.ThreatToolbar.Text = "toolStrip2";
			this.CreaturesBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CreaturesBtn.Image = (Image)resources.GetObject("CreaturesBtn.Image");
			this.CreaturesBtn.ImageTransparentColor = Color.Magenta;
			this.CreaturesBtn.Name = "CreaturesBtn";
			this.CreaturesBtn.Size = new Size(61, 22);
			this.CreaturesBtn.Text = "Creatures";
			this.CreaturesBtn.Click += new EventHandler(this.ViewCreatures_Click);
			this.TrapsBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapsBtn.Image = (Image)resources.GetObject("TrapsBtn.Image");
			this.TrapsBtn.ImageTransparentColor = Color.Magenta;
			this.TrapsBtn.Name = "TrapsBtn";
			this.TrapsBtn.Size = new Size(93, 22);
			this.TrapsBtn.Text = "Traps / Hazards";
			this.TrapsBtn.Click += new EventHandler(this.ViewTraps_Click);
			this.ChallengesBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengesBtn.Image = (Image)resources.GetObject("ChallengesBtn.Image");
			this.ChallengesBtn.ImageTransparentColor = Color.Magenta;
			this.ChallengesBtn.Name = "ChallengesBtn";
			this.ChallengesBtn.Size = new Size(93, 22);
			this.ChallengesBtn.Text = "Skill Challenges";
			this.ChallengesBtn.Click += new EventHandler(this.ViewChallenges_Click);
			this.ViewMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ViewMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ViewTemplates,
				this.ViewNPCs,
				this.toolStripSeparator12,
				this.ViewGroups
			});
			this.ViewMenu.Image = (Image)resources.GetObject("ViewMenu.Image");
			this.ViewMenu.ImageTransparentColor = Color.Magenta;
			this.ViewMenu.Name = "ViewMenu";
			this.ViewMenu.Size = new Size(48, 22);
			this.ViewMenu.Text = "More";
			this.ViewMenu.DropDownOpening += new EventHandler(this.ViewMenu_DropDownOpening);
			this.ViewTemplates.Font = new Font("Segoe UI", 9f);
			this.ViewTemplates.Name = "ViewTemplates";
			this.ViewTemplates.Size = new Size(177, 22);
			this.ViewTemplates.Text = "Creature Templates";
			this.ViewTemplates.Click += new EventHandler(this.ViewTemplates_Click);
			this.ViewNPCs.Font = new Font("Segoe UI", 9f);
			this.ViewNPCs.Name = "ViewNPCs";
			this.ViewNPCs.Size = new Size(177, 22);
			this.ViewNPCs.Text = "NPCs";
			this.ViewNPCs.Click += new EventHandler(this.ViewNPCs_Click);
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new Size(174, 6);
			this.ViewGroups.Font = new Font("Segoe UI", 9f);
			this.ViewGroups.Name = "ViewGroups";
			this.ViewGroups.Size = new Size(177, 22);
			this.ViewGroups.Text = "Show in Groups";
			this.ViewGroups.Click += new EventHandler(this.ViewGroups_Click);
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.ThreatsPage);
			this.Pages.Controls.Add(this.MapPage);
			this.Pages.Controls.Add(this.NotesPage);
			this.Pages.Location = new Point(12, 12);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(810, 395);
			this.Pages.TabIndex = 0;
			this.ThreatsPage.Controls.Add(this.HSplitter);
			this.ThreatsPage.Location = new Point(4, 22);
			this.ThreatsPage.Name = "ThreatsPage";
			this.ThreatsPage.Padding = new Padding(3);
			this.ThreatsPage.Size = new Size(802, 369);
			this.ThreatsPage.TabIndex = 0;
			this.ThreatsPage.Text = "Threats";
			this.ThreatsPage.UseVisualStyleBackColor = true;
			this.HSplitter.Dock = DockStyle.Fill;
			this.HSplitter.FixedPanel = FixedPanel.Panel2;
			this.HSplitter.Location = new Point(3, 3);
			this.HSplitter.Name = "HSplitter";
			this.HSplitter.Panel1.Controls.Add(this.VSplitter);
			this.HSplitter.Panel2.Controls.Add(this.SourceItemList);
			this.HSplitter.Panel2.Controls.Add(this.FilterPanel);
			this.HSplitter.Panel2.Controls.Add(this.ThreatToolbar);
			this.HSplitter.Size = new Size(796, 363);
			this.HSplitter.SplitterDistance = 472;
			this.HSplitter.TabIndex = 2;
			this.VSplitter.Dock = DockStyle.Fill;
			this.VSplitter.FixedPanel = FixedPanel.Panel2;
			this.VSplitter.Location = new Point(0, 0);
			this.VSplitter.Name = "VSplitter";
			this.VSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.VSplitter.Panel1.Controls.Add(this.HintStatusbar);
			this.VSplitter.Panel1.Controls.Add(this.SlotList);
			this.VSplitter.Panel1.Controls.Add(this.EncToolbar);
			this.VSplitter.Panel2.Controls.Add(this.DifficultyList);
			this.VSplitter.Panel2.Controls.Add(this.XPGauge);
			this.VSplitter.Panel2.Controls.Add(this.XPStatusbar);
			this.VSplitter.Size = new Size(472, 363);
			this.VSplitter.SplitterDistance = 231;
			this.VSplitter.TabIndex = 0;
			this.HintStatusbar.Items.AddRange(new ToolStripItem[]
			{
				this.HintLbl
			});
			this.HintStatusbar.Location = new Point(0, 209);
			this.HintStatusbar.Name = "HintStatusbar";
			this.HintStatusbar.Size = new Size(472, 22);
			this.HintStatusbar.SizingGrip = false;
			this.HintStatusbar.TabIndex = 2;
			this.HintStatusbar.Text = "statusStrip1";
			this.HintLbl.Name = "HintLbl";
			this.HintLbl.Size = new Size(415, 17);
			this.HintLbl.Text = "Drag items from the right into this list; double-click to view; right-click to edit";
			this.XPStatusbar.Items.AddRange(new ToolStripItem[]
			{
				this.XPLbl,
				this.LevelLbl,
				this.DiffLbl,
				this.CountLbl,
				this.PartyLbl
			});
			this.XPStatusbar.Location = new Point(0, 106);
			this.XPStatusbar.Name = "XPStatusbar";
			this.XPStatusbar.Size = new Size(472, 22);
			this.XPStatusbar.SizingGrip = false;
			this.XPStatusbar.TabIndex = 1;
			this.XPStatusbar.Text = "statusStrip1";
			this.XPLbl.Name = "XPLbl";
			this.XPLbl.Size = new Size(29, 17);
			this.XPLbl.Text = "[XP]";
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(39, 17);
			this.LevelLbl.Text = "[level]";
			this.DiffLbl.Name = "DiffLbl";
			this.DiffLbl.Size = new Size(33, 17);
			this.DiffLbl.Text = "[diff]";
			this.CountLbl.Name = "CountLbl";
			this.CountLbl.Size = new Size(46, 17);
			this.CountLbl.Text = "[count]";
			this.PartyLbl.IsLink = true;
			this.PartyLbl.Name = "PartyLbl";
			this.PartyLbl.Size = new Size(310, 17);
			this.PartyLbl.Spring = true;
			this.PartyLbl.Text = "Change Party";
			this.PartyLbl.TextAlign = ContentAlignment.MiddleRight;
			this.PartyLbl.Click += new EventHandler(this.PartyLbl_Click);
			this.MapPage.Controls.Add(this.MapSplitter);
			this.MapPage.Controls.Add(this.MapToolbar);
			this.MapPage.Location = new Point(4, 22);
			this.MapPage.Name = "MapPage";
			this.MapPage.Padding = new Padding(3);
			this.MapPage.Size = new Size(802, 369);
			this.MapPage.TabIndex = 1;
			this.MapPage.Text = "Encounter Map";
			this.MapPage.UseVisualStyleBackColor = true;
			this.MapSplitter.Dock = DockStyle.Fill;
			this.MapSplitter.FixedPanel = FixedPanel.Panel2;
			this.MapSplitter.Location = new Point(3, 28);
			this.MapSplitter.Name = "MapSplitter";
			this.MapSplitter.Panel1.Controls.Add(this.MapView);
			this.MapSplitter.Panel2.Controls.Add(this.MapThreatList);
			this.MapSplitter.Size = new Size(796, 338);
			this.MapSplitter.SplitterDistance = 586;
			this.MapSplitter.TabIndex = 2;
			this.MapContextMenu.Items.AddRange(new ToolStripItem[]
			{
				this.MapContextView,
				this.toolStripSeparator15,
				this.MapContextSetPicture,
				this.toolStripMenuItem4,
				this.MapContextRemove,
				this.MapContextRemoveEncounter,
				this.MapContextCopy,
				this.toolStripSeparator5,
				this.MapContextVisible
			});
			this.MapContextMenu.Name = "MapContextMenu";
			this.MapContextMenu.Size = new Size(204, 154);
			this.MapContextView.Name = "MapContextView";
			this.MapContextView.Size = new Size(203, 22);
			this.MapContextView.Text = "View Stat Block";
			this.MapContextView.Click += new EventHandler(this.MapContextView_Click);
			this.toolStripSeparator15.Name = "toolStripSeparator15";
			this.toolStripSeparator15.Size = new Size(200, 6);
			this.MapContextSetPicture.Name = "MapContextSetPicture";
			this.MapContextSetPicture.Size = new Size(203, 22);
			this.MapContextSetPicture.Text = "Set Picture...";
			this.MapContextSetPicture.Click += new EventHandler(this.MapContextSetPicture_Click);
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new Size(200, 6);
			this.MapContextRemove.Name = "MapContextRemove";
			this.MapContextRemove.Size = new Size(203, 22);
			this.MapContextRemove.Text = "Remove from Map";
			this.MapContextRemove.Click += new EventHandler(this.MapContextRemove_Click);
			this.MapContextRemoveEncounter.Name = "MapContextRemoveEncounter";
			this.MapContextRemoveEncounter.Size = new Size(203, 22);
			this.MapContextRemoveEncounter.Text = "Remove from Encounter";
			this.MapContextRemoveEncounter.Click += new EventHandler(this.MapContextRemoveEncounter_Click);
			this.MapContextCopy.Name = "MapContextCopy";
			this.MapContextCopy.Size = new Size(203, 22);
			this.MapContextCopy.Text = "Create Copy";
			this.MapContextCopy.Click += new EventHandler(this.MapContextCopy_Click);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new Size(200, 6);
			this.MapContextVisible.Name = "MapContextVisible";
			this.MapContextVisible.Size = new Size(203, 22);
			this.MapContextVisible.Text = "Visible";
			this.MapContextVisible.Click += new EventHandler(this.MapContextVisible_Click);
			this.MapThreatList.Columns.AddRange(new ColumnHeader[]
			{
				this.ThreatNameHdr
			});
			this.MapThreatList.Dock = DockStyle.Fill;
			this.MapThreatList.FullRowSelect = true;
			this.MapThreatList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.MapThreatList.HideSelection = false;
			this.MapThreatList.Location = new Point(0, 0);
			this.MapThreatList.MultiSelect = false;
			this.MapThreatList.Name = "MapThreatList";
			this.MapThreatList.Size = new Size(206, 338);
			this.MapThreatList.TabIndex = 1;
			this.MapThreatList.UseCompatibleStateImageBehavior = false;
			this.MapThreatList.View = View.Details;
			this.MapThreatList.DoubleClick += new EventHandler(this.MapThreatList_DoubleClick);
			this.MapThreatList.ItemDrag += new ItemDragEventHandler(this.MapThreatList_ItemDrag);
			this.ThreatNameHdr.Text = "Creature";
			this.ThreatNameHdr.Width = 171;
			this.MapToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.MapBtn,
				this.toolStripSeparator6,
				this.MapToolsMenu,
				this.MapCreaturesMenu
			});
			this.MapToolbar.Location = new Point(3, 3);
			this.MapToolbar.Name = "MapToolbar";
			this.MapToolbar.Size = new Size(796, 25);
			this.MapToolbar.TabIndex = 0;
			this.MapToolbar.Text = "toolStrip1";
			this.MapBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MapBtn.Image = (Image)resources.GetObject("MapBtn.Image");
			this.MapBtn.ImageTransparentColor = Color.Magenta;
			this.MapBtn.Name = "MapBtn";
			this.MapBtn.Size = new Size(69, 22);
			this.MapBtn.Text = "Select Map";
			this.MapBtn.Click += new EventHandler(this.MapBtn_Click);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new Size(6, 25);
			this.MapToolsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MapToolsMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.MapToolsLOS,
				this.MapToolsGridlines,
				this.MapToolsGridLabels,
				this.MapToolsPictureTokens,
				this.toolStripMenuItem5,
				this.MapToolsPrint,
				this.MapToolsScreenshot
			});
			this.MapToolsMenu.Image = (Image)resources.GetObject("MapToolsMenu.Image");
			this.MapToolsMenu.ImageTransparentColor = Color.Magenta;
			this.MapToolsMenu.Name = "MapToolsMenu";
			this.MapToolsMenu.Size = new Size(49, 22);
			this.MapToolsMenu.Text = "Tools";
			this.MapToolsLOS.Name = "MapToolsLOS";
			this.MapToolsLOS.Size = new Size(168, 22);
			this.MapToolsLOS.Text = "Line of Sight";
			this.MapToolsLOS.Click += new EventHandler(this.MapToolsLOS_Click);
			this.MapToolsGridlines.Name = "MapToolsGridlines";
			this.MapToolsGridlines.Size = new Size(168, 22);
			this.MapToolsGridlines.Text = "Gridlines";
			this.MapToolsGridlines.Click += new EventHandler(this.MapToolsGridlines_Click);
			this.MapToolsGridLabels.Name = "MapToolsGridLabels";
			this.MapToolsGridLabels.Size = new Size(168, 22);
			this.MapToolsGridLabels.Text = "Grid Labels";
			this.MapToolsGridLabels.Click += new EventHandler(this.MapToolsGridLabels_Click);
			this.MapToolsPictureTokens.Name = "MapToolsPictureTokens";
			this.MapToolsPictureTokens.Size = new Size(168, 22);
			this.MapToolsPictureTokens.Text = "Picture Tokens";
			this.MapToolsPictureTokens.Click += new EventHandler(this.MapToolsPictureTokens_Click);
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new Size(165, 6);
			this.MapToolsPrint.Name = "MapToolsPrint";
			this.MapToolsPrint.Size = new Size(168, 22);
			this.MapToolsPrint.Text = "Print";
			this.MapToolsPrint.Click += new EventHandler(this.PrintBtn_Click);
			this.MapToolsScreenshot.Name = "MapToolsScreenshot";
			this.MapToolsScreenshot.Size = new Size(168, 22);
			this.MapToolsScreenshot.Text = "Export Screenshot";
			this.MapToolsScreenshot.Click += new EventHandler(this.ExportBtn_Click);
			this.MapCreaturesMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MapCreaturesMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.MapCreaturesRemove,
				this.toolStripSeparator7,
				this.MapCreaturesShowAll,
				this.MapCreaturesHideAll,
				this.toolStripSeparator8,
				this.CreaturesAddCustom,
				this.CreaturesAddOverlay
			});
			this.MapCreaturesMenu.Image = (Image)resources.GetObject("MapCreaturesMenu.Image");
			this.MapCreaturesMenu.ImageTransparentColor = Color.Magenta;
			this.MapCreaturesMenu.Name = "MapCreaturesMenu";
			this.MapCreaturesMenu.Size = new Size(85, 22);
			this.MapCreaturesMenu.Text = "Map Tokens";
			this.MapCreaturesRemove.Name = "MapCreaturesRemove";
			this.MapCreaturesRemove.Size = new Size(193, 22);
			this.MapCreaturesRemove.Text = "Remove All";
			this.MapCreaturesRemove.Click += new EventHandler(this.MapCreaturesRemoveAll_Click);
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new Size(190, 6);
			this.MapCreaturesShowAll.Name = "MapCreaturesShowAll";
			this.MapCreaturesShowAll.Size = new Size(193, 22);
			this.MapCreaturesShowAll.Text = "Show All";
			this.MapCreaturesShowAll.Click += new EventHandler(this.MapCreaturesShowAll_Click);
			this.MapCreaturesHideAll.Name = "MapCreaturesHideAll";
			this.MapCreaturesHideAll.Size = new Size(193, 22);
			this.MapCreaturesHideAll.Text = "Hide All";
			this.MapCreaturesHideAll.Click += new EventHandler(this.MapCreaturesHideAll_Click);
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new Size(190, 6);
			this.CreaturesAddCustom.Name = "CreaturesAddCustom";
			this.CreaturesAddCustom.Size = new Size(193, 22);
			this.CreaturesAddCustom.Text = "Add Custom Token...";
			this.CreaturesAddCustom.Click += new EventHandler(this.AddToken_Click);
			this.CreaturesAddOverlay.Name = "CreaturesAddOverlay";
			this.CreaturesAddOverlay.Size = new Size(193, 22);
			this.CreaturesAddOverlay.Text = "Add Custom Overlay...";
			this.CreaturesAddOverlay.Click += new EventHandler(this.CreaturesAddOverlay_Click);
			this.NotesPage.Controls.Add(this.NoteSplitter);
			this.NotesPage.Controls.Add(this.NoteToolbar);
			this.NotesPage.Location = new Point(4, 22);
			this.NotesPage.Name = "NotesPage";
			this.NotesPage.Padding = new Padding(3);
			this.NotesPage.Size = new Size(802, 369);
			this.NotesPage.TabIndex = 2;
			this.NotesPage.Text = "Notes";
			this.NotesPage.UseVisualStyleBackColor = true;
			this.NoteSplitter.Dock = DockStyle.Fill;
			this.NoteSplitter.FixedPanel = FixedPanel.Panel1;
			this.NoteSplitter.Location = new Point(3, 28);
			this.NoteSplitter.Name = "NoteSplitter";
			this.NoteSplitter.Panel1.Controls.Add(this.NoteList);
			this.NoteSplitter.Panel2.Controls.Add(this.BackgroundPanel);
			this.NoteSplitter.Size = new Size(796, 338);
			this.NoteSplitter.SplitterDistance = 180;
			this.NoteSplitter.TabIndex = 2;
			this.NoteList.Columns.AddRange(new ColumnHeader[]
			{
				this.NoteHdr
			});
			this.NoteList.Dock = DockStyle.Fill;
			this.NoteList.FullRowSelect = true;
			this.NoteList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.NoteList.HideSelection = false;
			this.NoteList.Location = new Point(0, 0);
			this.NoteList.MultiSelect = false;
			this.NoteList.Name = "NoteList";
			this.NoteList.Size = new Size(180, 338);
			this.NoteList.TabIndex = 0;
			this.NoteList.UseCompatibleStateImageBehavior = false;
			this.NoteList.View = View.Details;
			this.NoteList.SelectedIndexChanged += new EventHandler(this.NoteList_SelectedIndexChanged);
			this.NoteList.DoubleClick += new EventHandler(this.NoteEditBtn_Click);
			this.NoteHdr.Text = "Notes";
			this.NoteHdr.Width = 150;
			this.BackgroundPanel.BorderStyle = BorderStyle.FixedSingle;
			this.BackgroundPanel.Controls.Add(this.NoteDetails);
			this.BackgroundPanel.Dock = DockStyle.Fill;
			this.BackgroundPanel.Location = new Point(0, 0);
			this.BackgroundPanel.Name = "BackgroundPanel";
			this.BackgroundPanel.Size = new Size(612, 338);
			this.BackgroundPanel.TabIndex = 0;
			this.NoteDetails.Dock = DockStyle.Fill;
			this.NoteDetails.IsWebBrowserContextMenuEnabled = false;
			this.NoteDetails.Location = new Point(0, 0);
			this.NoteDetails.MinimumSize = new Size(20, 20);
			this.NoteDetails.Name = "NoteDetails";
			this.NoteDetails.Size = new Size(610, 336);
			this.NoteDetails.TabIndex = 0;
			this.NoteDetails.Navigating += new WebBrowserNavigatingEventHandler(this.NoteDetails_Navigating);
			this.NoteToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.NoteAddBtn,
				this.NoteRemoveBtn,
				this.NoteEditBtn,
				this.toolStripSeparator21,
				this.NoteUpBtn,
				this.NoteDownBtn
			});
			this.NoteToolbar.Location = new Point(3, 3);
			this.NoteToolbar.Name = "NoteToolbar";
			this.NoteToolbar.Size = new Size(796, 25);
			this.NoteToolbar.TabIndex = 1;
			this.NoteToolbar.Text = "toolStrip1";
			this.NoteAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NoteAddBtn.Image = (Image)resources.GetObject("NoteAddBtn.Image");
			this.NoteAddBtn.ImageTransparentColor = Color.Magenta;
			this.NoteAddBtn.Name = "NoteAddBtn";
			this.NoteAddBtn.Size = new Size(33, 22);
			this.NoteAddBtn.Text = "Add";
			this.NoteAddBtn.Click += new EventHandler(this.NoteAddBtn_Click);
			this.NoteRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NoteRemoveBtn.Image = (Image)resources.GetObject("NoteRemoveBtn.Image");
			this.NoteRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.NoteRemoveBtn.Name = "NoteRemoveBtn";
			this.NoteRemoveBtn.Size = new Size(54, 22);
			this.NoteRemoveBtn.Text = "Remove";
			this.NoteRemoveBtn.Click += new EventHandler(this.NoteRemoveBtn_Click);
			this.NoteEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NoteEditBtn.Image = (Image)resources.GetObject("NoteEditBtn.Image");
			this.NoteEditBtn.ImageTransparentColor = Color.Magenta;
			this.NoteEditBtn.Name = "NoteEditBtn";
			this.NoteEditBtn.Size = new Size(31, 22);
			this.NoteEditBtn.Text = "Edit";
			this.NoteEditBtn.Click += new EventHandler(this.NoteEditBtn_Click);
			this.toolStripSeparator21.Name = "toolStripSeparator21";
			this.toolStripSeparator21.Size = new Size(6, 25);
			this.NoteUpBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NoteUpBtn.Image = (Image)resources.GetObject("NoteUpBtn.Image");
			this.NoteUpBtn.ImageTransparentColor = Color.Magenta;
			this.NoteUpBtn.Name = "NoteUpBtn";
			this.NoteUpBtn.Size = new Size(59, 22);
			this.NoteUpBtn.Text = "Move Up";
			this.NoteUpBtn.Click += new EventHandler(this.NoteUpBtn_Click);
			this.NoteDownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NoteDownBtn.Image = (Image)resources.GetObject("NoteDownBtn.Image");
			this.NoteDownBtn.ImageTransparentColor = Color.Magenta;
			this.NoteDownBtn.Name = "NoteDownBtn";
			this.NoteDownBtn.Size = new Size(75, 22);
			this.NoteDownBtn.Text = "Move Down";
			this.NoteDownBtn.Click += new EventHandler(this.NoteDownBtn_Click);
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(666, 413);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(747, 413);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.InfoBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.InfoBtn.Location = new Point(12, 413);
			this.InfoBtn.Name = "InfoBtn";
			this.InfoBtn.Size = new Size(75, 23);
			this.InfoBtn.TabIndex = 1;
			this.InfoBtn.Text = "Information";
			this.InfoBtn.UseVisualStyleBackColor = true;
			this.InfoBtn.Click += new EventHandler(this.InfoBtn_Click);
			this.DieRollerBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.DieRollerBtn.Location = new Point(93, 413);
			this.DieRollerBtn.Name = "DieRollerBtn";
			this.DieRollerBtn.Size = new Size(75, 23);
			this.DieRollerBtn.TabIndex = 2;
			this.DieRollerBtn.Text = "Die Roller";
			this.DieRollerBtn.UseVisualStyleBackColor = true;
			this.DieRollerBtn.Click += new EventHandler(this.DieRollerBtn_Click);
			this.XPGauge.Dock = DockStyle.Bottom;
			this.XPGauge.Location = new Point(0, 86);
			this.XPGauge.Name = "XPGauge";
			this.XPGauge.Party = null;
			this.XPGauge.Size = new Size(472, 20);
			this.XPGauge.TabIndex = 1;
			this.XPGauge.XP = 0;
			this.FilterPanel.AutoSize = true;
			this.FilterPanel.BorderStyle = BorderStyle.FixedSingle;
			this.FilterPanel.Dock = DockStyle.Top;
			this.FilterPanel.Location = new Point(0, 25);
			this.FilterPanel.Mode = ListMode.Creatures;
			this.FilterPanel.Name = "FilterPanel";
			this.FilterPanel.PartyLevel = 1;
			this.FilterPanel.Size = new Size(320, 24);
			this.FilterPanel.TabIndex = 0;
			this.FilterPanel.FilterChanged += new EventHandler(this.FilterPanel_FilterChanged);
			this.MapView.AllowDrawing = false;
			this.MapView.AllowDrop = true;
			this.MapView.AllowLinkCreation = false;
			this.MapView.AllowScrolling = false;
			this.MapView.BackgroundMap = null;
			this.MapView.BorderSize = 1;
			this.MapView.Caption = "";
			this.MapView.ContextMenuStrip = this.MapContextMenu;
			this.MapView.Cursor = Cursors.Default;
			this.MapView.Dock = DockStyle.Fill;
			this.MapView.Encounter = null;
			this.MapView.FrameType = MapDisplayType.Dimmed;
			this.MapView.HighlightAreas = false;
			this.MapView.HoverToken = null;
			this.MapView.HoverTokenLink = null;
			this.MapView.LineOfSight = false;
			this.MapView.Location = new Point(0, 0);
			this.MapView.Map = null;
			this.MapView.Mode = MapViewMode.Thumbnail;
			this.MapView.Name = "MapView";
			this.MapView.Plot = null;
			this.MapView.ScalingFactor = 1.0;
			this.MapView.SelectedArea = null;
			this.MapView.SelectedTiles = null;
			this.MapView.Selection = new Rectangle(0, 0, 0, 0);
			this.MapView.ShowAllWaves = true;
			this.MapView.ShowAuras = true;
			this.MapView.ShowConditions = true;
			this.MapView.ShowCreatureLabels = true;
			this.MapView.ShowCreatures = CreatureViewMode.All;
			this.MapView.ShowGrid = MapGridMode.None;
			this.MapView.ShowGridLabels = false;
			this.MapView.ShowHealthBars = false;
			this.MapView.ShowPictureTokens = true;
			this.MapView.Size = new Size(586, 338);
			this.MapView.TabIndex = 1;
			this.MapView.Tactical = true;
			this.MapView.TokenLinks = null;
			this.MapView.Viewpoint = new Rectangle(0, 0, 0, 0);
			this.MapView.TokenActivated += new TokenEventHandler(this.MapView_TokenActivated);
			this.MapView.ItemDropped += new EventHandler(this.MapView_ItemDropped);
			this.MapView.DoubleClick += new EventHandler(this.MapView_DoubleClick);
			this.MapView.SelectedTokensChanged += new EventHandler(this.MapView_SelectedTokensChanged);
			this.MapView.HoverTokenChanged += new EventHandler(this.MapView_HoverTokenChanged);
			this.MapView.ItemMoved += new MovementEventHandler(this.MapView_ItemMoved);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(834, 448);
			base.Controls.Add(this.DieRollerBtn);
			base.Controls.Add(this.InfoBtn);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.MinimizeBox = false;
			base.Name = "EncounterBuilderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Encounter Builder";
			this.ThreatContextMenu.ResumeLayout(false);
			this.EncToolbar.ResumeLayout(false);
			this.EncToolbar.PerformLayout();
			this.ThreatToolbar.ResumeLayout(false);
			this.ThreatToolbar.PerformLayout();
			this.Pages.ResumeLayout(false);
			this.ThreatsPage.ResumeLayout(false);
			this.HSplitter.Panel1.ResumeLayout(false);
			this.HSplitter.Panel2.ResumeLayout(false);
			this.HSplitter.Panel2.PerformLayout();
			this.HSplitter.ResumeLayout(false);
			this.VSplitter.Panel1.ResumeLayout(false);
			this.VSplitter.Panel1.PerformLayout();
			this.VSplitter.Panel2.ResumeLayout(false);
			this.VSplitter.Panel2.PerformLayout();
			this.VSplitter.ResumeLayout(false);
			this.HintStatusbar.ResumeLayout(false);
			this.HintStatusbar.PerformLayout();
			this.XPStatusbar.ResumeLayout(false);
			this.XPStatusbar.PerformLayout();
			this.MapPage.ResumeLayout(false);
			this.MapPage.PerformLayout();
			this.MapSplitter.Panel1.ResumeLayout(false);
			this.MapSplitter.Panel2.ResumeLayout(false);
			this.MapSplitter.ResumeLayout(false);
			this.MapContextMenu.ResumeLayout(false);
			this.MapToolbar.ResumeLayout(false);
			this.MapToolbar.PerformLayout();
			this.NotesPage.ResumeLayout(false);
			this.NotesPage.PerformLayout();
			this.NoteSplitter.Panel1.ResumeLayout(false);
			this.NoteSplitter.Panel2.ResumeLayout(false);
			this.NoteSplitter.ResumeLayout(false);
			this.BackgroundPanel.ResumeLayout(false);
			this.NoteToolbar.ResumeLayout(false);
			this.NoteToolbar.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
