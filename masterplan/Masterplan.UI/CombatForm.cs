using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Events;
using Masterplan.Extensibility;
using Masterplan.Properties;
using Masterplan.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class CombatForm : Form
	{
		private class InitiativeSorter : IComparer, IComparer<ListViewItem>
		{
			private Dictionary<Guid, CombatData> fTrapData;

			private Encounter fEncounter;

			public InitiativeSorter(Dictionary<Guid, CombatData> trap_data, Encounter enc)
			{
				this.fTrapData = trap_data;
				this.fEncounter = enc;
			}

			public int Compare(object x, object y)
			{
				ListViewItem listViewItem = x as ListViewItem;
				ListViewItem listViewItem2 = y as ListViewItem;
				if (listViewItem == null || listViewItem2 == null)
				{
					return 0;
				}
				return this.Compare(listViewItem, listViewItem2);
			}

			public int Compare(ListViewItem lvi_x, ListViewItem lvi_y)
			{
				int num = this.get_score(lvi_x);
				int value = this.get_score(lvi_y);
				int num2 = num.CompareTo(value);
				if (num2 == 0)
				{
					int num3 = this.get_bonus(lvi_x);
					int value2 = this.get_bonus(lvi_y);
					num2 = num3.CompareTo(value2);
				}
				if (num2 == 0)
				{
					string text = lvi_x.Text;
					string text2 = lvi_y.Text;
					num2 = text.CompareTo(text2) * -1;
				}
				return -num2;
			}

			private int get_score(ListViewItem lvi)
			{
				try
				{
					if (lvi.Tag is Hero)
					{
						Hero hero = lvi.Tag as Hero;
						int result = hero.CombatData.Initiative;
						return result;
					}
					if (lvi.Tag is CreatureToken)
					{
						CreatureToken creatureToken = lvi.Tag as CreatureToken;
						int result = creatureToken.Data.Initiative;
						return result;
					}
					if (lvi.Tag is Trap)
					{
						Trap trap = lvi.Tag as Trap;
						int result;
						if (this.fTrapData.ContainsKey(trap.ID))
						{
							result = this.fTrapData[trap.ID].Initiative;
							return result;
						}
						result = -2147483648;
						return result;
					}
				}
				catch (Exception ex)
				{
					LogSystem.Trace(ex);
				}
				return 0;
			}

			private int get_bonus(ListViewItem lvi)
			{
				try
				{
					if (lvi.Tag is Hero)
					{
						Hero hero = lvi.Tag as Hero;
						int result = hero.InitBonus;
						return result;
					}
					if (lvi.Tag is CreatureToken)
					{
						CreatureToken creatureToken = lvi.Tag as CreatureToken;
						EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
						int result = (encounterSlot != null) ? encounterSlot.Card.Initiative : 0;
						return result;
					}
					if (lvi.Tag is Trap)
					{
						Trap trap = lvi.Tag as Trap;
						int result = (trap.Initiative != -2147483648) ? trap.Initiative : 0;
						return result;
					}
				}
				catch (Exception ex)
				{
					LogSystem.Trace(ex);
				}
				return 0;
			}
		}

		public class CombatListControl : ListView
		{
			public CombatListControl()
			{
				this.DoubleBuffered = true;
			}
		}

		private Encounter fEncounter;

		private int fPartyLevel = Session.Project.Party.Level;

		private Dictionary<Guid, CombatData> fTrapData;

		private bool fCombatStarted;

		private CombatData fCurrentActor;

		private int fCurrentRound = 1;

		private int fRemovedCreatureXP;

		private List<OngoingCondition> fEffects = new List<OngoingCondition>();

		private EncounterLog fLog = new EncounterLog();

		private bool fUpdatingList;

		private bool fPromptOnClose = true;

		private StringFormat fLeft = new StringFormat();

		private StringFormat fRight = new StringFormat();

		private IContainer components;

		private ToolStrip Toolbar;

		private SplitContainer MapSplitter;

		private CombatForm.CombatListControl CombatList;

		private ColumnHeader NameHdr;

		private ColumnHeader InitHdr;

		private ColumnHeader HPHdr;

		private ToolTip MapTooltip;

		private ToolStripButton DetailsBtn;

		private ToolStripSeparator toolStripSeparator1;

		private SplitContainer ListSplitter;

		private StatusStrip Statusbar;

		private ToolStripStatusLabel XPLbl;

		private ContextMenuStrip MapContext;

		private ToolStripMenuItem MapDetails;

		private ToolStripMenuItem MapVisible;

		private ToolStripSeparator toolStripMenuItem1;

		private ToolStripButton DamageBtn;

		private ToolStripMenuItem MapDamage;

		private ToolStripSeparator toolStripSeparator2;

		private ContextMenuStrip ListContext;

		private ToolStripMenuItem ListDetails;

		private ToolStripMenuItem ListDamage;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem ListVisible;

		public MapView MapView;

		private ToolStripDropDownButton CombatantsBtn;

		private ToolStripMenuItem CombatantsAdd;

		private ToolStripMenuItem CombatantsRemove;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem CombatantsAddToken;

		private TrackBar ZoomGauge;

		private ToolStripDropDownButton MapMenu;

		private ToolStripMenuItem MapReset;

		private ToolStripMenuItem MapNavigate;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripMenuItem MapExport;

		private WebBrowser Preview;

		private Panel PreviewPanel;

		private ToolStripButton NextInitBtn;

		private ToolStripMenuItem ShowMap;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripDropDownButton PlayerViewMapMenu;

		private ToolStripMenuItem PlayerViewMap;

		private ToolStripMenuItem PlayerLabels;

		private ToolStripMenuItem MapFog;

		private ToolStripMenuItem MapFogAllCreatures;

		private ToolStripMenuItem MapFogVisibleCreatures;

		private ToolStripMenuItem MapFogHideCreatures;

		private ToolStripSeparator toolStripSeparator10;

		private ToolStripSeparator toolStripSeparator9;

		private ToolStripMenuItem PlayerViewFog;

		private ToolStripMenuItem PlayerFogAll;

		private ToolStripMenuItem PlayerFogVisible;

		private ToolStripMenuItem PlayerFogNone;

		private ToolStripMenuItem MapGrid;

		private ToolStripMenuItem PlayerViewGrid;

		private ToolStripMenuItem MapPrint;

		private ToolStripMenuItem MapLOS;

		private ToolStripMenuItem PlayerViewLOS;

		private ToolStripSeparator toolStripSeparator12;

		private ToolStripMenuItem CombatantsHideAll;

		private ToolStripMenuItem CombatantsShowAll;

		private ToolStripDropDownButton OptionsMenu;

		private ToolStripSeparator toolStripSeparator13;

		private ToolStripMenuItem OneColumn;

		private ToolStripMenuItem TwoColumns;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem ToolsAutoRemove;

		private ToolStripStatusLabel LevelLbl;

		private ToolStripMenuItem CombatantsAddOverlay;

		private ToolStripSeparator toolStripSeparator14;

		private ToolStripSeparator toolStripMenuItem2;

		private ToolStripMenuItem PlayerHealth;

		private ToolStripMenuItem MapHealth;

		private ToolStripSeparator toolStripSeparator15;

		private ToolStripSeparator toolStripSeparator16;

		private ToolStripSeparator toolStripSeparator17;

		private Panel MainPanel;

		private Button CloseBtn;

		private ToolStripButton DelayBtn;

		private ToolStripSeparator toolStripSeparator18;

		private ToolStripMenuItem ListDelay;

		private ToolStripMenuItem MapDelay;

		private ToolStripStatusLabel RoundLbl;

		private ToolStripSeparator toolStripSeparator20;

		private ToolStripMenuItem MapRight;

		private ToolStripMenuItem MapBelow;

		private ToolStripSeparator toolStripSeparator21;

		private ToolStripMenuItem OptionsLandscape;

		private ToolStripMenuItem OptionsPortrait;

		private ToolStripMenuItem MapDrawing;

		private ToolStripSeparator toolStripSeparator19;

		private ToolStripMenuItem MapClearDrawings;

		private Button PauseBtn;

		private ToolStripDropDownButton EffectMenu;

		private ToolStripMenuItem ListCondition;

		private ToolStripMenuItem MapAddEffect;

		private ToolStripMenuItem effectToolStripMenuItem;

		private ToolStripMenuItem effectToolStripMenuItem1;

		private ToolStripMenuItem effectToolStripMenuItem2;

		private ToolStripMenuItem OptionsIPlay4e;

		private ToolStripMenuItem ListRemoveEffect;

		private ToolStripMenuItem effectToolStripMenuItem3;

		private ToolStripMenuItem MapRemoveEffect;

		private ToolStripMenuItem effectToolStripMenuItem4;

		private ToolStripSeparator toolStripSeparator22;

		private ToolStripMenuItem MapContextDrawing;

		private ToolStripMenuItem MapContextClearDrawings;

		private ToolStripSeparator toolStripSeparator24;

		private ToolStripMenuItem MapContextOverlay;

		private ToolStripMenuItem MapGridLabels;

		private ToolStripMenuItem PlayerViewGridLabels;

		private ToolStripMenuItem ListHeal;

		private ToolStripMenuItem MapHeal;

		private ToolStripButton HealBtn;

		private ToolStripMenuItem MapPictureTokens;

		private ToolStripMenuItem PlayerPictureTokens;

		private ToolStripDropDownButton ToolsMenu;

		private ToolStripMenuItem ToolsEffects;

		private ToolStripMenuItem ToolsLinks;

		private ToolStripSeparator toolStripSeparator11;

		private ToolStripMenuItem ToolsAddIns;

		private ToolStripMenuItem addinsToolStripMenuItem;

		private ToolStripMenuItem ListCreateCopy;

		private ToolStripMenuItem MapCreateCopy;

		private ToolStripMenuItem PlayerViewInitList;

		private ToolStripMenuItem MapSetPicture;

		private ToolStripDropDownButton PlayerViewNoMapMenu;

		private ToolStripMenuItem PlayerViewNoMapShowInitiativeList;

		private ToolStripMenuItem MapConditions;

		private ToolStripMenuItem PlayerConditions;

		private TabControl Pages;

		private TabPage CombatantsPage;

		private TabPage TemplatesPage;

		private ListView TemplateList;

		private ColumnHeader TemplateHdr;

		private Button InfoBtn;

		private InitiativePanel InitiativePanel;

		private ToolStripMenuItem OptionsShowInit;

		private ToolStripMenuItem PlayerViewNoMapShowLabels;

		private ColumnHeader DefHdr;

		private ToolStripMenuItem ListRemove;

		private ToolStripMenuItem MapRemove;

		private ToolStripMenuItem ListRemoveMap;

		private ToolStripMenuItem ListRemoveCombat;

		private ToolStripMenuItem MapRemoveMap;

		private ToolStripMenuItem MapRemoveCombat;

		private Button DieRollerBtn;

		private ColumnHeader EffectsHdr;

		private ToolStripSeparator toolStripSeparator23;

		private ToolStripMenuItem ToolsColumns;

		private ToolStripMenuItem ToolsColumnsInit;

		private ToolStripMenuItem ToolsColumnsHP;

		private ToolStripMenuItem ToolsColumnsDefences;

		private ToolStripMenuItem ToolsColumnsConditions;

		private TabPage LogPage;

		private WebBrowser LogBrowser;

		private ToolStripSeparator toolStripSeparator25;

		private ToolStripMenuItem MapContextLOS;

		private Button ReportBtn;

		private ToolStripSeparator toolStripSeparator26;

		private ToolStripMenuItem CombatantsWaves;

		public List<IToken> SelectedTokens
		{
			get
			{
				List<IToken> list = new List<IToken>();
				foreach (ListViewItem listViewItem in this.CombatList.SelectedItems)
				{
					IToken token = listViewItem.Tag as IToken;
					if (token != null)
					{
						list.Add(token);
					}
				}
				return list;
			}
		}

		public Trap SelectedTrap
		{
			get
			{
				if (this.CombatList.SelectedItems.Count != 0)
				{
					return this.CombatList.SelectedItems[0].Tag as Trap;
				}
				return null;
			}
		}

		public SkillChallenge SelectedChallenge
		{
			get
			{
				if (this.CombatList.SelectedItems.Count != 0)
				{
					return this.CombatList.SelectedItems[0].Tag as SkillChallenge;
				}
				return null;
			}
		}

		public MapView PlayerMap
		{
			get
			{
				if (Session.PlayerView == null)
				{
					return null;
				}
				if (Session.PlayerView.Controls.Count == 0)
				{
					return null;
				}
				SplitContainer splitContainer = Session.PlayerView.Controls[0] as SplitContainer;
				if (splitContainer == null)
				{
					return null;
				}
				if (splitContainer.Panel1Collapsed)
				{
					return null;
				}
				if (splitContainer.Panel1.Controls.Count == 0)
				{
					return null;
				}
				return splitContainer.Panel1.Controls[0] as MapView;
			}
		}

		public WebBrowser PlayerInitiative
		{
			get
			{
				if (Session.PlayerView == null)
				{
					return null;
				}
				if (Session.PlayerView.Controls.Count == 0)
				{
					return null;
				}
				SplitContainer splitContainer = Session.PlayerView.Controls[0] as SplitContainer;
				if (splitContainer == null)
				{
					return null;
				}
				if (splitContainer.Panel2Collapsed)
				{
					return null;
				}
				if (splitContainer.Panel2.Controls.Count == 0)
				{
					return null;
				}
				foreach (Control control in splitContainer.Panel2.Controls)
				{
					WebBrowser webBrowser = control as WebBrowser;
					if (webBrowser != null)
					{
						return webBrowser;
					}
				}
				return null;
			}
		}

		public bool TwoColumnPreview
		{
			get
			{
				return this.fCurrentActor != null && this.Preview.Width > 630;
			}
		}

		public CombatForm(CombatState cs)
		{
			this.InitializeComponent();
			this.Preview.DocumentText = "";
			this.LogBrowser.DocumentText = "";
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fLeft.Alignment = StringAlignment.Near;
			this.fLeft.LineAlignment = StringAlignment.Center;
			this.fRight.Alignment = StringAlignment.Far;
			this.fRight.LineAlignment = StringAlignment.Center;
			this.fEncounter = (cs.Encounter.Copy() as Encounter);
			this.fPartyLevel = cs.PartyLevel;
			this.fRemovedCreatureXP = cs.RemovedCreatureXP;
			this.fCurrentRound = cs.CurrentRound;
			this.RoundLbl.Text = "Round " + this.fCurrentRound;
			if (cs.QuickEffects != null)
			{
				foreach (OngoingCondition current in cs.QuickEffects)
				{
					this.add_quick_effect(current);
				}
			}
			if (cs.HeroData != null)
			{
				using (List<Hero>.Enumerator enumerator2 = Session.Project.Heroes.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Hero current2 = enumerator2.Current;
						if (cs.HeroData.ContainsKey(current2.ID))
						{
							current2.CombatData = cs.HeroData[current2.ID];
						}
					}
					goto IL_1FF;
				}
			}
			foreach (Hero current3 in Session.Project.Heroes)
			{
				current3.CombatData.Location = CombatData.NoPoint;
			}
			IL_1FF:
			foreach (Hero current4 in Session.Project.Heroes)
			{
				current4.CombatData.ID = current4.ID;
				current4.CombatData.DisplayName = current4.Name;
			}
			if (cs.TrapData != null)
			{
				this.fTrapData = cs.TrapData;
			}
			else
			{
				this.fTrapData = new Dictionary<Guid, CombatData>();
			}
			foreach (Trap current5 in this.fEncounter.Traps)
			{
				if (!this.fTrapData.ContainsKey(current5.ID))
				{
					CombatData combatData = new CombatData();
					combatData.DisplayName = current5.Name;
					combatData.ID = current5.ID;
					this.fTrapData[current5.ID] = combatData;
				}
			}
			if (this.fEncounter.MapID != Guid.Empty)
			{
				foreach (Hero current6 in Session.Project.Heroes)
				{
					foreach (CustomToken current7 in current6.Tokens)
					{
						string text = current6.Name + ": " + current7.Name;
						ListViewItem listViewItem = this.TemplateList.Items.Add(text);
						listViewItem.Tag = current7;
						listViewItem.Group = this.TemplateList.Groups[0];
					}
				}
				Array values = Enum.GetValues(typeof(CreatureSize));
				foreach (CreatureSize creatureSize in values)
				{
					CustomToken customToken = new CustomToken();
					customToken.Type = CustomTokenType.Token;
					customToken.TokenSize = creatureSize;
					customToken.Colour = Color.Black;
					customToken.Name = creatureSize + " Token";
					ListViewItem listViewItem2 = this.TemplateList.Items.Add(customToken.Name);
					listViewItem2.Tag = customToken;
					listViewItem2.Group = this.TemplateList.Groups[1];
				}
				for (int i = 2; i <= 10; i++)
				{
					CustomToken customToken2 = new CustomToken();
					customToken2.Type = CustomTokenType.Overlay;
					customToken2.OverlaySize = new Size(i, i);
					customToken2.Name = string.Concat(new object[]
					{
						i,
						" x ",
						i,
						" Zone"
					});
					customToken2.Colour = Color.Transparent;
					ListViewItem listViewItem3 = this.TemplateList.Items.Add(customToken2.Name);
					listViewItem3.Tag = customToken2;
					listViewItem3.Group = this.TemplateList.Groups[2];
				}
			}
			else
			{
				this.Pages.TabPages.Remove(this.TemplatesPage);
			}
			this.fLog = cs.Log;
			this.fLog.Active = false;
			if (this.fLog.Entries.Count != 0)
			{
				this.fLog.Active = true;
				this.fLog.AddResumeEntry();
			}
			this.update_log();
			if (cs.CurrentActor != Guid.Empty)
			{
				this.fCombatStarted = true;
				Hero hero = Session.Project.FindHero(cs.CurrentActor);
				if (hero != null)
				{
					this.fCurrentActor = hero.CombatData;
				}
				else if (this.fTrapData.ContainsKey(cs.CurrentActor))
				{
					this.fCurrentActor = this.fTrapData[cs.CurrentActor];
				}
				else
				{
					CombatData combatData2 = this.fEncounter.FindCombatData(cs.CurrentActor);
					if (combatData2 != null)
					{
						this.fCurrentActor = combatData2;
					}
				}
			}
			this.CombatList.ListViewItemSorter = new CombatForm.InitiativeSorter(this.fTrapData, this.fEncounter);
			this.set_map(cs.TokenLinks, cs.Viewpoint, cs.Sketches);
			this.MapMenu.Visible = (this.fEncounter.MapID != Guid.Empty);
			this.InitiativePanel.InitiativeScores = this.get_initiatives();
			this.InitiativePanel.CurrentInitiative = this.InitiativePanel.Maximum;
			this.PlayerViewMapMenu.Visible = (this.fEncounter.MapID != Guid.Empty);
			this.PlayerViewNoMapMenu.Visible = (this.fEncounter.MapID == Guid.Empty);
			if (!Session.Preferences.CombatColumnInitiative)
			{
				this.InitHdr.Width = 0;
			}
			if (!Session.Preferences.CombatColumnHP)
			{
				this.HPHdr.Width = 0;
			}
			if (!Session.Preferences.CombatColumnDefences)
			{
				this.DefHdr.Width = 0;
			}
			if (!Session.Preferences.CombatColumnEffects)
			{
				this.EffectsHdr.Width = 0;
			}
			Screen screen = Screen.FromControl(this);
			if (screen.Bounds.Height > screen.Bounds.Width)
			{
				this.OptionsPortrait_Click(null, null);
			}
			Session.CurrentEncounter = this.fEncounter;
			this.update_list();
			this.update_log();
			this.update_preview_panel();
			this.update_maps();
			this.update_statusbar();
		}

		private void DetailsBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedTokens.Count == 1)
				{
					this.edit_token(this.SelectedTokens[0]);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void DamageBtn_Click(object sender, EventArgs e)
		{
			try
			{
				this.do_damage(this.SelectedTokens);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void HealBtn_Click(object sender, EventArgs e)
		{
			try
			{
				this.do_heal(this.SelectedTokens);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void DelayBtn_Click(object sender, EventArgs e)
		{
			try
			{
				this.set_delay(this.SelectedTokens);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void NextInitBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.fCombatStarted)
				{
					this.start_combat();
				}
				else
				{
					List<int> initiatives = this.get_initiatives();
					if (initiatives.Count != 0)
					{
						this.handle_ended_effects(false);
						this.handle_saves();
						this.fCurrentActor = this.get_next_actor(this.fCurrentActor);
						this.fLog.AddStartTurnEntry(this.fCurrentActor.ID);
						if (this.fCurrentActor.Initiative > this.InitiativePanel.CurrentInitiative)
						{
							this.fCurrentRound++;
							this.RoundLbl.Text = "Round: " + this.fCurrentRound;
							this.fLog.AddStartRoundEntry(this.fCurrentRound);
						}
						this.InitiativePanel.CurrentInitiative = this.fCurrentActor.Initiative;
						this.handle_regen();
						this.handle_ended_effects(true);
						this.handle_ongoing_damage();
						this.handle_recharge();
						if (this.fCurrentActor != null && !this.TwoColumnPreview)
						{
							this.select_current_actor();
						}
						this.update_list();
						this.update_log();
						this.update_preview_panel();
						this.highlight_current_actor();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void CombatantsAdd_Click(object sender, EventArgs e)
		{
			try
			{
				Encounter enc = new Encounter();
				EncounterBuilderForm encounterBuilderForm = new EncounterBuilderForm(enc, this.fPartyLevel, true);
				if (encounterBuilderForm.ShowDialog() == DialogResult.OK)
				{
					foreach (EncounterSlot current in encounterBuilderForm.Encounter.Slots)
					{
						this.fEncounter.Slots.Add(current);
						if (this.fCombatStarted)
						{
							this.roll_initiative();
						}
					}
					foreach (Trap current2 in encounterBuilderForm.Encounter.Traps)
					{
						if (current2.Initiative != -2147483648)
						{
							this.fTrapData[current2.ID] = new CombatData();
							if (this.fCombatStarted)
							{
								this.roll_initiative();
							}
						}
						this.fEncounter.Traps.Add(current2);
					}
					foreach (SkillChallenge current3 in encounterBuilderForm.Encounter.SkillChallenges)
					{
						this.fEncounter.SkillChallenges.Add(current3);
					}
					this.update_list();
					this.update_preview_panel();
					this.update_statusbar();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void CombatantsAddCustom_Click(object sender, EventArgs e)
		{
			try
			{
				CustomTokenForm customTokenForm = new CustomTokenForm(new CustomToken
				{
					Name = "Custom Token",
					Type = CustomTokenType.Token
				});
				if (customTokenForm.ShowDialog() == DialogResult.OK)
				{
					this.fEncounter.CustomTokens.Add(customTokenForm.Token);
					this.update_list();
					this.update_maps();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void CombatantsAddOverlay_Click(object sender, EventArgs e)
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
					this.update_list();
					this.update_maps();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void CombatantsRemove_Click(object sender, EventArgs e)
		{
			if (this.SelectedTokens.Count != 0)
			{
				this.remove_from_combat(this.SelectedTokens);
			}
		}

		private void CombatantsHideAll_Click(object sender, EventArgs e)
		{
			this.show_or_hide_all(false);
		}

		private void CombatantsShowAll_Click(object sender, EventArgs e)
		{
			this.show_or_hide_all(true);
		}

		private void ShowMap_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapSplitter.Panel2Collapsed = !this.MapSplitter.Panel2Collapsed;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapLOS_Click(object sender, EventArgs e)
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

		private void MapGrid_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowGrid = ((this.MapView.ShowGrid == MapGridMode.None) ? MapGridMode.Overlay : MapGridMode.None);
				Session.Preferences.CombatGrid = (this.MapView.ShowGrid == MapGridMode.Overlay);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapGridLabels_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowGridLabels = !this.MapView.ShowGridLabels;
				Session.Preferences.CombatGridLabels = this.MapView.ShowGridLabels;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapHealth_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowHealthBars = !this.MapView.ShowHealthBars;
				Session.Preferences.CombatHealthBars = this.MapView.ShowHealthBars;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapConditions_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowConditions = !this.MapView.ShowConditions;
				Session.Preferences.CombatConditionBadges = this.MapView.ShowConditions;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapPictureTokens_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowPictureTokens = !this.MapView.ShowPictureTokens;
				Session.Preferences.CombatPictureTokens = this.MapView.ShowPictureTokens;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapFogAllCreatures_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowCreatures = CreatureViewMode.All;
				Session.Preferences.CombatFog = CreatureViewMode.All;
				this.update_list();
				this.update_preview_panel();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapFogVisibleCreatures_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowCreatures = CreatureViewMode.Visible;
				Session.Preferences.CombatFog = CreatureViewMode.Visible;
				this.update_list();
				this.update_preview_panel();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapFogHideCreatures_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.ShowCreatures = CreatureViewMode.None;
				Session.Preferences.CombatFog = CreatureViewMode.None;
				this.update_list();
				this.update_preview_panel();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapNavigate_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.AllowScrolling = !this.MapView.AllowScrolling;
				this.ZoomGauge.Visible = this.MapView.AllowScrolling;
				if (Session.PlayerView != null)
				{
					if (!this.MapView.AllowScrolling)
					{
						this.cancelled_scrolling();
					}
					else
					{
						Session.Preferences.PlayerViewMap = (this.PlayerMap != null);
						Session.Preferences.PlayerViewInitiative = (this.PlayerInitiative != null);
						Session.PlayerView.ShowMessage("DM is editing the map; please wait");
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapReset_Click(object sender, EventArgs e)
		{
			try
			{
				this.ZoomGauge.Value = 50;
				this.MapView.ScalingFactor = 1.0;
				if (this.fEncounter.MapAreaID != Guid.Empty)
				{
					MapArea mapArea = this.MapView.Map.FindArea(this.fEncounter.MapAreaID);
					this.MapView.Viewpoint = mapArea.Region;
				}
				else
				{
					this.MapView.Viewpoint = Rectangle.Empty;
				}
				if (this.PlayerMap != null)
				{
					this.PlayerMap.Viewpoint = this.MapView.Viewpoint;
				}
				this.MapView.MapChanged();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapPrint_Click(object sender, EventArgs e)
		{
			try
			{
				MapPrintingForm mapPrintingForm = new MapPrintingForm(this.MapView);
				mapPrintingForm.ShowDialog();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapExport_Click(object sender, EventArgs e)
		{
			try
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.FileName = this.MapView.Map.Name;
				if (this.fEncounter.MapAreaID != Guid.Empty)
				{
					MapArea mapArea = this.MapView.Map.FindArea(this.fEncounter.MapAreaID);
					SaveFileDialog expr_50 = saveFileDialog;
					expr_50.FileName = expr_50.FileName + " - " + mapArea.Name;
				}
				saveFileDialog.Filter = "Bitmap Image |*.bmp|JPEG Image|*.jpg|GIF Image|*.gif|PNG Image|*.png";
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
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlayerViewMap_Click(object sender, EventArgs e)
		{
			try
			{
				this.show_player_view(this.PlayerMap == null, this.PlayerInitiative != null);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlayerViewInitList_Click(object sender, EventArgs e)
		{
			try
			{
				this.show_player_view(this.PlayerMap != null, this.PlayerInitiative == null);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlayerLabels_Click(object sender, EventArgs e)
		{
			try
			{
				Session.Preferences.PlayerViewCreatureLabels = !Session.Preferences.PlayerViewCreatureLabels;
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowCreatureLabels = !this.PlayerMap.ShowCreatureLabels;
				}
				if (this.PlayerInitiative != null)
				{
					this.PlayerInitiative.DocumentText = this.InitiativeView();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlayerHealth_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowHealthBars = !this.PlayerMap.ShowHealthBars;
					Session.Preferences.PlayerViewHealthBars = this.PlayerMap.ShowHealthBars;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlayerConditions_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowConditions = !this.PlayerMap.ShowConditions;
					Session.Preferences.PlayerViewConditionBadges = this.PlayerMap.ShowConditions;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlayerPictureTokens_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowPictureTokens = !this.PlayerMap.ShowPictureTokens;
					Session.Preferences.PlayerViewPictureTokens = this.PlayerMap.ShowPictureTokens;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlayerViewLOS_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.LineOfSight = !this.PlayerMap.LineOfSight;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlayerViewGrid_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowGrid = ((this.PlayerMap.ShowGrid == MapGridMode.None) ? MapGridMode.Overlay : MapGridMode.None);
					Session.Preferences.PlayerViewGrid = (this.PlayerMap.ShowGrid == MapGridMode.Overlay);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlayerViewGridLabels_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowGridLabels = !this.PlayerMap.ShowGridLabels;
					Session.Preferences.PlayerViewGridLabels = this.PlayerMap.ShowGridLabels;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlayerFogAll_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowCreatures = CreatureViewMode.All;
					Session.Preferences.PlayerViewFog = CreatureViewMode.All;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlayerFogVisible_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowCreatures = CreatureViewMode.Visible;
					Session.Preferences.PlayerViewFog = CreatureViewMode.Visible;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlayerFogNone_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlayerMap != null)
				{
					this.PlayerMap.ShowCreatures = CreatureViewMode.None;
					Session.Preferences.PlayerViewFog = CreatureViewMode.None;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void OneColumn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.ListSplitter.Orientation != Orientation.Horizontal)
				{
					if (this.fEncounter.MapID != Guid.Empty)
					{
						Session.Preferences.CombatTwoColumns = false;
					}
					else
					{
						Session.Preferences.CombatTwoColumnsNoMap = false;
					}
					this.ListSplitter.Orientation = Orientation.Horizontal;
					this.ListSplitter.SplitterDistance = this.ListSplitter.Height / 2;
					this.MapSplitter.SplitterDistance = 350;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void TwoColumns_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.fEncounter.MapID != Guid.Empty)
				{
					Session.Preferences.CombatTwoColumns = true;
				}
				else
				{
					Session.Preferences.CombatTwoColumnsNoMap = true;
				}
				this.ListSplitter.Orientation = Orientation.Vertical;
				if (!this.MapSplitter.Panel2Collapsed && this.MapSplitter.Orientation == Orientation.Vertical)
				{
					this.MapSplitter.SplitterDistance = 700;
					this.ListSplitter.SplitterDistance = 350;
				}
				else
				{
					this.ListSplitter.SplitterDistance = this.ListSplitter.Width / 2;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsAutoRemove_Click(object sender, EventArgs e)
		{
			Session.Preferences.CreatureAutoRemove = !Session.Preferences.CreatureAutoRemove;
		}

		private void OptionsIPlay4e_Click(object sender, EventArgs e)
		{
			Session.Preferences.iPlay4E = !Session.Preferences.iPlay4E;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			try
			{
				bool enabled = false;
				bool @checked = false;
				if (this.SelectedTokens.Count != 0)
				{
					enabled = true;
					@checked = true;
					foreach (IToken current in this.SelectedTokens)
					{
						if (!(current is CreatureToken) && !(current is Hero))
						{
							enabled = false;
							@checked = false;
						}
						if (current is CreatureToken)
						{
							CreatureToken creatureToken = current as CreatureToken;
							if (!creatureToken.Data.Delaying)
							{
								@checked = false;
							}
						}
						if (current is Hero)
						{
							Hero hero = current as Hero;
							CombatData combatData = hero.CombatData;
							if (!combatData.Delaying)
							{
								@checked = false;
							}
						}
					}
				}
				this.DetailsBtn.Enabled = (this.SelectedTokens.Count == 1);
				this.DamageBtn.Enabled = enabled;
				this.HealBtn.Enabled = enabled;
				this.EffectMenu.Enabled = enabled;
				this.NextInitBtn.Text = (this.fCombatStarted ? "Next Turn" : "Start Encounter");
				this.DelayBtn.Visible = this.fCombatStarted;
				this.DelayBtn.Enabled = enabled;
				this.DelayBtn.Checked = @checked;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void CombatForm_Shown(object sender, EventArgs e)
		{
			try
			{
				if (!Session.Preferences.CombatMapRight)
				{
					this.MapSplitter.SplitterDistance = this.MapSplitter.Height / 2;
				}
				if (this.fCurrentActor == null)
				{
					foreach (Hero current in Session.Project.Heroes)
					{
						current.CombatData.Reset(false);
					}
					this.update_list();
					this.update_maps();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void CombatForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (this.fPromptOnClose)
				{
					bool flag = false;
					foreach (EncounterSlot current in this.fEncounter.AllSlots)
					{
						int hP = current.Card.HP;
						foreach (CombatData current2 in current.CombatData)
						{
							if (current2.Initiative != -2147483648)
							{
								int num = hP + current2.TempHP - current2.Damage;
								if (num > 0)
								{
									flag = true;
								}
							}
						}
					}
					if (flag)
					{
						string text = "There are creatures remaining; are you sure you want to end the encounter?";
						if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
						{
							e.Cancel = true;
							return;
						}
					}
				}
				if (this.PlayerMap != null || this.PlayerInitiative != null)
				{
					Session.PlayerView.ShowDefault();
				}
				Session.CurrentEncounter = null;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void CombatList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			try
			{
				if (this.SelectedTokens.Count == 1)
				{
					IToken token = this.SelectedTokens[0];
					if (token is CreatureToken)
					{
						CreatureToken creatureToken = token as CreatureToken;
						if (creatureToken.Data.Location == CombatData.NoPoint)
						{
							base.DoDragDrop(creatureToken, DragDropEffects.Move);
							this.update_list();
							this.update_preview_panel();
							this.update_maps();
						}
					}
					if (token is Hero)
					{
						Hero hero = token as Hero;
						if (hero.CombatData.Location == CombatData.NoPoint)
						{
							base.DoDragDrop(hero, DragDropEffects.Move);
							if (hero.CombatData.Location != CombatData.NoPoint)
							{
								this.update_list();
								this.update_preview_panel();
								this.update_maps();
							}
						}
					}
					if (token is CustomToken)
					{
						CustomToken customToken = token as CustomToken;
						if (customToken.Data.Location == CombatData.NoPoint)
						{
							base.DoDragDrop(customToken, DragDropEffects.Move);
							this.update_list();
							this.update_preview_panel();
							this.update_maps();
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void CombatList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			try
			{
				if (!this.fUpdatingList)
				{
					if (this.SelectedTokens.Count == 0)
					{
						this.MapView.SelectTokens(null, false);
						if (this.PlayerMap != null)
						{
							this.PlayerMap.SelectTokens(null, false);
						}
						this.update_preview_panel();
					}
					else
					{
						this.MapView.SelectTokens(this.SelectedTokens, false);
						if (this.PlayerMap != null)
						{
							this.PlayerMap.SelectTokens(this.SelectedTokens, false);
						}
						this.update_preview_panel();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void CombatList_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void CombatList_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedTokens.Count == 1)
				{
					this.edit_token(this.SelectedTokens[0]);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapView_ItemMoved(object sender, MovementEventArgs e)
		{
			try
			{
				this.update_maps();
				foreach (IToken current in this.MapView.SelectedTokens)
				{
					Guid id = Guid.Empty;
					CreatureToken creatureToken = current as CreatureToken;
					if (creatureToken != null)
					{
						id = creatureToken.Data.ID;
					}
					Hero hero = current as Hero;
					if (hero != null)
					{
						id = hero.ID;
					}
					this.fLog.AddMoveEntry(id, e.Distance, "");
				}
				this.update_log();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapView_SelectedTokensChanged(object sender, EventArgs e)
		{
			try
			{
				this.fUpdatingList = true;
				this.CombatList.SelectedItems.Clear();
				foreach (IToken current in this.MapView.SelectedTokens)
				{
					this.select_token(current);
				}
				this.fUpdatingList = false;
				this.update_preview_panel();
				if (this.PlayerMap != null)
				{
					this.PlayerMap.SelectTokens(this.MapView.SelectedTokens, false);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapView_HoverTokenChanged(object sender, EventArgs e)
		{
			try
			{
				this.set_tooltip(this.MapView.HoverToken, this.MapView);
				if (this.PlayerMap != null)
				{
					this.PlayerMap.HoverToken = this.MapView.HoverToken;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapView_TokenActivated(object sender, TokenEventArgs e)
		{
			try
			{
				if (e.Token is CreatureToken || e.Token is Hero)
				{
					this.do_damage(new List<IToken>
					{
						e.Token
					});
				}
				if (e.Token is CustomToken)
				{
					this.edit_token(e.Token);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private TokenLink MapView_CreateTokenLink(object sender, TokenListEventArgs e)
		{
			try
			{
				TokenLink tokenLink = new TokenLink();
				tokenLink.Tokens.AddRange(e.Tokens);
				TokenLinkForm tokenLinkForm = new TokenLinkForm(tokenLink);
				TokenLink result;
				if (tokenLinkForm.ShowDialog() == DialogResult.OK)
				{
					result = tokenLinkForm.Link;
					return result;
				}
				result = null;
				return result;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return null;
		}

		private TokenLink MapView_EditTokenLink(object sender, TokenLinkEventArgs e)
		{
			TokenLinkForm tokenLinkForm = new TokenLinkForm(e.Link);
			if (tokenLinkForm.ShowDialog() == DialogResult.OK)
			{
				return tokenLinkForm.Link;
			}
			return null;
		}

		private void MapView_TokenDragged(object sender, DraggedTokenEventArgs e)
		{
			if (this.PlayerMap != null)
			{
				this.PlayerMap.SetDragInfo(e.OldLocation, e.NewLocation);
			}
		}

		private void ZoomGauge_Scroll(object sender, EventArgs e)
		{
			try
			{
				double num = 10.0;
				double num2 = 1.0;
				double num3 = 0.1;
				double num4 = (double)(this.ZoomGauge.Value - this.ZoomGauge.Minimum) / (double)(this.ZoomGauge.Maximum - this.ZoomGauge.Minimum);
				if (num4 >= 0.5)
				{
					num4 -= 0.5;
					num4 *= 2.0;
					this.MapView.ScalingFactor = num2 + num4 * (num - num2);
				}
				else
				{
					num4 *= 2.0;
					this.MapView.ScalingFactor = num3 + num4 * (num2 - num3);
				}
				this.MapView.MapChanged();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void Preview_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			try
			{
				if (e.Url.Scheme == "power")
				{
					e.Cancel = true;
					string[] array = e.Url.LocalPath.Split(new string[]
					{
						";"
					}, StringSplitOptions.RemoveEmptyEntries);
					Guid id = new Guid(array[0]);
					CombatData combatData = this.fEncounter.FindCombatData(id);
					if (combatData != null)
					{
						EncounterSlot encounterSlot = this.fEncounter.FindSlot(combatData);
						if (encounterSlot != null)
						{
							List<CreaturePower> arg_81_0 = encounterSlot.Card.CreaturePowers;
							Guid power_id = new Guid(array[1]);
							CreaturePower creaturePower = encounterSlot.Card.FindPower(power_id);
							if (creaturePower == null)
							{
								return;
							}
							if (creaturePower.Attack != null)
							{
								this.roll_attack(creaturePower);
							}
							this.fLog.AddPowerEntry(combatData.ID, creaturePower.Name, true);
							this.update_log();
							if (creaturePower.Action != null && !combatData.UsedPowers.Contains(creaturePower.ID) && (creaturePower.Action.Use == PowerUseType.Encounter || creaturePower.Action.Use == PowerUseType.Daily))
							{
								string str = "per-encounter";
								if (creaturePower.Action.Use == PowerUseType.Daily)
								{
									str = "daily";
								}
								string text = "This is a " + str + " power. Do you want to mark it as expended?";
								if (MessageBox.Show(text, creaturePower.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
								{
									combatData.UsedPowers.Add(creaturePower.ID);
									this.update_preview_panel();
								}
							}
						}
					}
					else
					{
						foreach (Trap current in this.fEncounter.Traps)
						{
							TrapAttack trapAttack = current.FindAttack(id);
							if (trapAttack != null)
							{
								this.roll_check(trapAttack.Attack.ToString(), trapAttack.Attack.Bonus);
							}
						}
					}
				}
				if (e.Url.Scheme == "refresh")
				{
					e.Cancel = true;
					string[] array2 = e.Url.LocalPath.Split(new string[]
					{
						";"
					}, StringSplitOptions.RemoveEmptyEntries);
					Guid id2 = new Guid(array2[0]);
					Guid guid = new Guid(array2[1]);
					CombatData combatData2 = this.fEncounter.FindCombatData(id2);
					string text2 = "";
					EncounterSlot encounterSlot2 = this.fEncounter.FindSlot(combatData2);
					if (encounterSlot2 != null)
					{
						ICreature creature = Session.FindCreature(encounterSlot2.Card.CreatureID, SearchType.Global);
						if (creature != null)
						{
							foreach (CreaturePower current2 in creature.CreaturePowers)
							{
								if (current2.ID == guid)
								{
									text2 = current2.Name;
									break;
								}
							}
						}
					}
					if (combatData2.UsedPowers.Contains(guid))
					{
						combatData2.UsedPowers.Remove(guid);
						this.fLog.AddPowerEntry(combatData2.ID, text2, false);
					}
					else
					{
						combatData2.UsedPowers.Add(guid);
						this.fLog.AddPowerEntry(combatData2.ID, text2, true);
					}
					this.update_preview_panel();
					this.update_log();
				}
				if (e.Url.Scheme == "ability")
				{
					e.Cancel = true;
					int mod = int.Parse(e.Url.LocalPath);
					this.roll_check("Ability", mod);
				}
				if (e.Url.Scheme == "sc")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "reset")
					{
						SkillChallenge selectedChallenge = this.SelectedChallenge;
						if (selectedChallenge != null)
						{
							foreach (SkillChallengeData current3 in selectedChallenge.Skills)
							{
								current3.Results.Successes = 0;
								current3.Results.Fails = 0;
								this.update_list();
								this.update_preview_panel();
							}
						}
					}
				}
				if (e.Url.Scheme == "success")
				{
					e.Cancel = true;
					SkillChallenge selectedChallenge2 = this.SelectedChallenge;
					if (selectedChallenge2 != null)
					{
						SkillChallengeData skillChallengeData = selectedChallenge2.FindSkill(e.Url.LocalPath);
						skillChallengeData.Results.Successes++;
						this.fLog.AddSkillEntry(this.fCurrentActor.ID, e.Url.LocalPath);
						this.fLog.AddSkillChallengeEntry(this.fCurrentActor.ID, true);
						this.update_list();
						this.update_preview_panel();
						this.update_log();
					}
				}
				if (e.Url.Scheme == "failure")
				{
					e.Cancel = true;
					SkillChallenge selectedChallenge3 = this.SelectedChallenge;
					if (selectedChallenge3 != null)
					{
						SkillChallengeData skillChallengeData2 = selectedChallenge3.FindSkill(e.Url.LocalPath);
						skillChallengeData2.Results.Fails++;
						this.fLog.AddSkillEntry(this.fCurrentActor.ID, e.Url.LocalPath);
						this.fLog.AddSkillChallengeEntry(this.fCurrentActor.ID, false);
						this.update_list();
						this.update_preview_panel();
						this.update_log();
					}
				}
				if (e.Url.Scheme == "dmg")
				{
					e.Cancel = true;
					Guid guid2 = new Guid(e.Url.LocalPath);
					List<IToken> list = new List<IToken>();
					Hero hero = Session.Project.FindHero(guid2);
					if (hero != null)
					{
						list.Add(hero);
					}
					CombatData combatData3 = this.fEncounter.FindCombatData(guid2);
					if (combatData3 != null)
					{
						EncounterSlot encounterSlot3 = this.fEncounter.FindSlot(combatData3);
						CreatureToken item = new CreatureToken(encounterSlot3.ID, combatData3);
						list.Add(item);
					}
					if (list.Count != 0)
					{
						this.do_damage(list);
					}
				}
				if (e.Url.Scheme == "kill")
				{
					e.Cancel = true;
					Guid id3 = new Guid(e.Url.LocalPath);
					CombatData combatData4 = this.fEncounter.FindCombatData(id3);
					if (combatData4 != null)
					{
						combatData4.Damage = 1;
						this.fLog.AddStateEntry(combatData4.ID, CreatureState.Defeated);
						this.update_list();
						this.update_preview_panel();
						this.update_log();
						this.update_maps();
					}
				}
				if (e.Url.Scheme == "revive")
				{
					e.Cancel = true;
					Guid id4 = new Guid(e.Url.LocalPath);
					CombatData combatData5 = this.fEncounter.FindCombatData(id4);
					if (combatData5 != null)
					{
						combatData5.Damage = 0;
						this.fLog.AddStateEntry(combatData5.ID, CreatureState.Active);
						this.update_list();
						this.update_preview_panel();
						this.update_log();
						this.update_maps();
					}
				}
				if (e.Url.Scheme == "heal")
				{
					e.Cancel = true;
					Guid guid3 = new Guid(e.Url.LocalPath);
					List<IToken> list2 = new List<IToken>();
					Hero hero2 = Session.Project.FindHero(guid3);
					if (hero2 != null)
					{
						list2.Add(hero2);
					}
					CombatData combatData6 = this.fEncounter.FindCombatData(guid3);
					if (combatData6 != null)
					{
						EncounterSlot encounterSlot4 = this.fEncounter.FindSlot(combatData6);
						CreatureToken item2 = new CreatureToken(encounterSlot4.ID, combatData6);
						list2.Add(item2);
					}
					if (list2.Count != 0)
					{
						this.do_heal(list2);
					}
				}
				if (e.Url.Scheme == "init")
				{
					e.Cancel = true;
					Guid guid4 = new Guid(e.Url.LocalPath);
					int num = -2147483648;
					CombatData combatData7 = this.fEncounter.FindCombatData(guid4);
					if (combatData7 != null)
					{
						EncounterSlot encounterSlot5 = this.fEncounter.FindSlot(combatData7);
						if (encounterSlot5 != null)
						{
							num = encounterSlot5.Card.Initiative;
						}
					}
					if (combatData7 == null)
					{
						Hero hero3 = Session.Project.FindHero(guid4);
						if (hero3 != null)
						{
							combatData7 = hero3.CombatData;
							num = hero3.InitBonus;
						}
					}
					if (combatData7 == null)
					{
						if (this.fTrapData.ContainsKey(guid4))
						{
							combatData7 = this.fTrapData[guid4];
						}
						Trap trap = this.fEncounter.FindTrap(guid4);
						if (trap != null)
						{
							num = trap.Initiative;
						}
					}
					if (combatData7 != null && num != -2147483648)
					{
						InitiativeForm initiativeForm = new InitiativeForm(num, combatData7.Initiative);
						if (initiativeForm.ShowDialog() == DialogResult.OK)
						{
							combatData7.Initiative = initiativeForm.Score;
							this.InitiativePanel.InitiativeScores = this.get_initiatives();
							if (this.fCurrentActor != null)
							{
								this.InitiativePanel.CurrentInitiative = this.fCurrentActor.Initiative;
							}
							this.update_list();
							this.update_preview_panel();
							this.update_maps();
						}
					}
				}
				if (e.Url.Scheme == "effect")
				{
					e.Cancel = true;
					string[] array3 = e.Url.LocalPath.Split(new string[]
					{
						":"
					}, StringSplitOptions.RemoveEmptyEntries);
					if (array3.Length == 2)
					{
						Guid guid5 = new Guid(array3[0]);
						int num2 = int.Parse(array3[1]);
						CombatData combatData8 = this.fEncounter.FindCombatData(guid5);
						if (combatData8 == null)
						{
							Hero hero4 = Session.Project.FindHero(guid5);
							if (hero4 != null)
							{
								combatData8 = hero4.CombatData;
							}
						}
						if (combatData8 != null && num2 >= 0 && num2 <= combatData8.Conditions.Count - 1)
						{
							OngoingCondition ongoingCondition = combatData8.Conditions[num2];
							combatData8.Conditions.RemoveAt(num2);
							this.fLog.AddEffectEntry(combatData8.ID, ongoingCondition.ToString(this.fEncounter, false), false);
							this.update_list();
							this.update_preview_panel();
							this.update_log();
							this.update_maps();
						}
					}
				}
				if (e.Url.Scheme == "addeffect")
				{
					Hero hero5 = Session.Project.FindHero(this.fCurrentActor.ID);
					int index = int.Parse(e.Url.LocalPath);
					OngoingCondition ongoingCondition2 = hero5.Effects[index];
					this.apply_effect(ongoingCondition2.Copy(), this.SelectedTokens, false);
					this.update_list();
					this.update_preview_panel();
					this.update_log();
					this.update_maps();
				}
				if (e.Url.Scheme == "creatureinit")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "auto")
					{
						switch (Session.Preferences.InitiativeMode)
						{
						case InitiativeMode.ManualIndividual:
							Session.Preferences.InitiativeMode = InitiativeMode.AutoIndividual;
							break;
						case InitiativeMode.ManualGroup:
							Session.Preferences.InitiativeMode = InitiativeMode.AutoGroup;
							break;
						}
						this.update_preview_panel();
					}
					if (e.Url.LocalPath == "manual")
					{
						switch (Session.Preferences.InitiativeMode)
						{
						case InitiativeMode.AutoGroup:
							Session.Preferences.InitiativeMode = InitiativeMode.ManualGroup;
							break;
						case InitiativeMode.AutoIndividual:
							Session.Preferences.InitiativeMode = InitiativeMode.ManualIndividual;
							break;
						}
						this.update_preview_panel();
					}
					if (e.Url.LocalPath == "group")
					{
						switch (Session.Preferences.InitiativeMode)
						{
						case InitiativeMode.AutoIndividual:
							Session.Preferences.InitiativeMode = InitiativeMode.AutoGroup;
							break;
						case InitiativeMode.ManualIndividual:
							Session.Preferences.InitiativeMode = InitiativeMode.ManualGroup;
							break;
						}
						this.update_preview_panel();
					}
					if (e.Url.LocalPath == "individual")
					{
						InitiativeMode initiativeMode = Session.Preferences.InitiativeMode;
						if (initiativeMode != InitiativeMode.AutoGroup)
						{
							if (initiativeMode == InitiativeMode.ManualGroup)
							{
								Session.Preferences.InitiativeMode = InitiativeMode.ManualIndividual;
							}
						}
						else
						{
							Session.Preferences.InitiativeMode = InitiativeMode.AutoIndividual;
						}
						this.update_preview_panel();
					}
				}
				if (e.Url.Scheme == "heroinit")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "auto")
					{
						Session.Preferences.HeroInitiativeMode = InitiativeMode.AutoIndividual;
						this.update_preview_panel();
					}
					if (e.Url.LocalPath == "manual")
					{
						Session.Preferences.HeroInitiativeMode = InitiativeMode.ManualIndividual;
						this.update_preview_panel();
					}
				}
				if (e.Url.Scheme == "trapinit")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "auto")
					{
						Session.Preferences.TrapInitiativeMode = InitiativeMode.AutoIndividual;
						this.update_preview_panel();
					}
					if (e.Url.LocalPath == "manual")
					{
						Session.Preferences.TrapInitiativeMode = InitiativeMode.ManualIndividual;
						this.update_preview_panel();
					}
				}
				if (e.Url.Scheme == "combat")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "sync")
					{
						Cursor.Current = Cursors.WaitCursor;
						foreach (Hero current4 in Session.Project.Heroes)
						{
							if (current4.Key != null && !(current4.Key == ""))
							{
								AppImport.ImportIPlay4e(current4);
								Session.Modified = true;
							}
						}
						Cursor.Current = Cursors.Default;
					}
					if (e.Url.LocalPath == "hp")
					{
						GroupHealthForm groupHealthForm = new GroupHealthForm();
						groupHealthForm.ShowDialog();
						this.update_list();
						this.update_preview_panel();
						this.update_maps();
					}
					if (e.Url.LocalPath == "rename")
					{
						List<CombatData> list3 = new List<CombatData>();
						foreach (EncounterSlot current5 in this.fEncounter.AllSlots)
						{
							list3.AddRange(current5.CombatData);
						}
						DisplayNameForm displayNameForm = new DisplayNameForm(list3, this.fEncounter);
						displayNameForm.ShowDialog();
						this.update_list();
						this.update_preview_panel();
						this.update_maps();
					}
					if (e.Url.LocalPath == "start")
					{
						this.start_combat();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void InitiativePanel_InitiativeChanged(object sender, EventArgs e)
		{
			try
			{
				Guid iD = this.fCurrentActor.ID;
				this.fCurrentActor = null;
				this.fCurrentActor = this.get_next_actor(null);
				if (this.fCurrentActor.ID != iD)
				{
					this.fLog.AddStartTurnEntry(this.fCurrentActor.ID);
				}
				this.update_list();
				this.update_log();
				this.update_preview_panel();
				this.update_maps();
				this.highlight_current_actor();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void CombatantsEffects_Click(object sender, EventArgs e)
		{
			EffectListForm effectListForm = new EffectListForm(this.fEncounter, this.fCurrentActor, this.fCurrentRound);
			effectListForm.ShowDialog();
			this.update_list();
			this.update_preview_panel();
			this.update_maps();
		}

		private void CombatantsLinks_Click(object sender, EventArgs e)
		{
			TokenLinkListForm tokenLinkListForm = new TokenLinkListForm(this.MapView.TokenLinks);
			tokenLinkListForm.ShowDialog();
			this.update_list();
			this.update_preview_panel();
			this.update_maps();
		}

		private void add_in_command_clicked(object sender, EventArgs e)
		{
			try
			{
				ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
				ICommand command = toolStripMenuItem.Tag as ICommand;
				command.Execute();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ListDelay_Click(object sender, EventArgs e)
		{
			this.set_delay(this.SelectedTokens);
		}

		private void MapDelay_Click(object sender, EventArgs e)
		{
			this.set_delay(this.MapView.SelectedTokens);
		}

		private void OptionsMapRight_Click(object sender, EventArgs e)
		{
			if (this.MapSplitter.Orientation == Orientation.Vertical)
			{
				return;
			}
			bool flag = this.ListSplitter.Orientation == Orientation.Horizontal;
			this.MapSplitter.Orientation = Orientation.Vertical;
			this.MapSplitter.SplitterDistance = (flag ? 355 : 700);
			this.MapSplitter.FixedPanel = FixedPanel.Panel1;
			Session.Preferences.CombatMapRight = true;
		}

		private void OptionsMapBelow_Click(object sender, EventArgs e)
		{
			if (this.MapSplitter.Orientation == Orientation.Horizontal)
			{
				return;
			}
			this.MapSplitter.Orientation = Orientation.Horizontal;
			this.MapSplitter.SplitterDistance = this.MapSplitter.Height / 2;
			this.MapSplitter.FixedPanel = FixedPanel.None;
			Session.Preferences.CombatMapRight = false;
		}

		private void OptionsLandscape_Click(object sender, EventArgs e)
		{
			base.SuspendLayout();
			this.OneColumn_Click(sender, e);
			this.OptionsMapRight_Click(sender, e);
			base.ResumeLayout();
		}

		private void OptionsPortrait_Click(object sender, EventArgs e)
		{
			base.SuspendLayout();
			this.TwoColumns_Click(sender, e);
			this.OptionsMapBelow_Click(sender, e);
			base.ResumeLayout();
		}

		private void MapDrawing_Click(object sender, EventArgs e)
		{
			this.MapView.AllowDrawing = !this.MapView.AllowDrawing;
			if (this.PlayerMap != null)
			{
				this.PlayerMap.AllowDrawing = this.MapView.AllowDrawing;
			}
		}

		private void MapClearDrawings_Click(object sender, EventArgs e)
		{
			this.MapView.Sketches.Clear();
			this.MapView.Invalidate();
			if (this.PlayerMap != null)
			{
				this.PlayerMap.Sketches.Clear();
				this.PlayerMap.Invalidate();
			}
		}

		private void MapView_SketchCreated(object sender, MapSketchEventArgs e)
		{
			if (this.PlayerMap != null)
			{
				this.PlayerMap.Sketches.Add(e.Sketch);
				this.PlayerMap.Invalidate();
			}
		}

		private void MapContextOverlay_Click(object sender, EventArgs e)
		{
			CustomToken customToken = new CustomToken();
			customToken.Name = "New Overlay";
			customToken.Type = CustomTokenType.Overlay;
			if (this.MapView.SelectedTokens.Count == 1)
			{
				IToken token = this.MapView.SelectedTokens[0];
				CreatureToken creatureToken = token as CreatureToken;
				if (creatureToken != null)
				{
					customToken.Name = "Zone: " + creatureToken.Data.DisplayName;
					customToken.CreatureID = creatureToken.Data.ID;
					customToken.Colour = Color.Red;
				}
				Hero hero = token as Hero;
				if (hero != null)
				{
					customToken.Name = hero.Name + " zone";
					customToken.CreatureID = hero.ID;
					customToken.Colour = Color.DarkGreen;
				}
			}
			CustomOverlayForm customOverlayForm = new CustomOverlayForm(customToken);
			if (customOverlayForm.ShowDialog() == DialogResult.OK)
			{
				customToken = customOverlayForm.Token;
				if (customToken.CreatureID == Guid.Empty)
				{
					Point p = new Point(this.MapContext.Left, this.MapContext.Top);
					Point pt = this.MapView.PointToClient(p);
					Point squareAtPoint = this.MapView.LayoutData.GetSquareAtPoint(pt);
					int x = squareAtPoint.X - (customToken.OverlaySize.Width - 1) / 2;
					int y = squareAtPoint.Y - (customToken.OverlaySize.Height - 1) / 2;
					customToken.Data.Location = new Point(x, y);
				}
				this.fEncounter.CustomTokens.Add(customToken);
				this.update_list();
				this.update_maps();
			}
		}

		private void MapHeal_Click(object sender, EventArgs e)
		{
			try
			{
				this.do_heal(this.MapView.SelectedTokens);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ListHeal_Click(object sender, EventArgs e)
		{
			try
			{
				this.do_heal(this.SelectedTokens);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ListCreateCopy_Click(object sender, EventArgs e)
		{
			this.copy_custom_token();
		}

		private void MapCreateCopy_Click(object sender, EventArgs e)
		{
			this.copy_custom_token();
		}

		private void MapSetPicture_Click(object sender, EventArgs e)
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
						this.update_list();
					}
				}
			}
			Hero hero = this.MapView.SelectedTokens[0] as Hero;
			if (hero != null)
			{
				OpenFileDialog openFileDialog2 = new OpenFileDialog();
				openFileDialog2.Filter = Program.ImageFilter;
				if (openFileDialog2.ShowDialog() == DialogResult.OK)
				{
					hero.Portrait = Image.FromFile(openFileDialog2.FileName);
					Program.SetResolution(hero.Portrait);
					Session.Modified = true;
					this.update_list();
				}
			}
		}

		private void PlayerViewNoMapShowInitiativeList_Click(object sender, EventArgs e)
		{
			try
			{
				this.show_player_view(false, this.PlayerInitiative == null);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlayerViewNoMapShowLabels_Click(object sender, EventArgs e)
		{
			Session.Preferences.PlayerViewCreatureLabels = !Session.Preferences.PlayerViewCreatureLabels;
			if (this.PlayerInitiative != null)
			{
				this.PlayerInitiative.DocumentText = this.InitiativeView();
			}
		}

		private void TemplateList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			CustomToken customToken = this.TemplateList.SelectedItems[0].Tag as CustomToken;
			customToken = customToken.Copy();
			if (customToken.Data.Location == CombatData.NoPoint && base.DoDragDrop(customToken, DragDropEffects.Move) == DragDropEffects.Move)
			{
				this.fEncounter.CustomTokens.Add(customToken);
				this.update_list();
				this.update_preview_panel();
				this.update_maps();
			}
		}

		private void OptionsShowInit_Click(object sender, EventArgs e)
		{
			this.InitiativePanel.Visible = !this.InitiativePanel.Visible;
		}

		private void ListSplitter_SplitterMoved(object sender, SplitterEventArgs e)
		{
			this.list_splitter_changed();
		}

		private void ListSplitter_Resize(object sender, EventArgs e)
		{
			this.list_splitter_changed();
		}

		private void MapView_MouseZoomed(object sender, MouseEventArgs e)
		{
			this.ZoomGauge.Visible = true;
			this.ZoomGauge.Value -= Math.Sign(e.Delta);
			this.ZoomGauge_Scroll(sender, e);
		}

		private void MapView_CancelledScrolling(object sender, EventArgs e)
		{
			this.cancelled_scrolling();
		}

		private void CombatList_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
		{
			e.DrawDefault = true;
		}

		private void CombatList_DrawItem(object sender, DrawListViewItemEventArgs e)
		{
		}

		private void CombatList_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		{
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			Brush brush = e.Item.Selected ? SystemBrushes.HighlightText : new SolidBrush(e.Item.ForeColor);
			Brush brush2 = e.Item.Selected ? SystemBrushes.Highlight : new SolidBrush(e.Item.BackColor);
			StringFormat format = (e.Header.TextAlign == HorizontalAlignment.Left) ? this.fLeft : this.fRight;
			e.Graphics.FillRectangle(brush2, e.Bounds);
			if (e.ColumnIndex == 0)
			{
				CreatureState creatureState = CreatureState.Defeated;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				if (e.Item.Tag is CreatureToken)
				{
					CreatureToken creatureToken = e.Item.Tag as CreatureToken;
					CombatData data = creatureToken.Data;
					EncounterSlot encounterSlot = this.fEncounter.FindSlot(data);
					creatureState = encounterSlot.GetState(data);
					num = encounterSlot.Card.HP;
					num2 = num - data.Damage;
					num3 = data.TempHP;
				}
				if (e.Item.Tag is Hero)
				{
					Hero hero = e.Item.Tag as Hero;
					CombatData combatData = hero.CombatData;
					creatureState = CreatureState.Active;
					num = hero.HP;
					num2 = num - combatData.Damage;
					num3 = combatData.TempHP;
				}
				if (e.Item.Tag is SkillChallenge)
				{
					SkillChallenge skillChallenge = e.Item.Tag as SkillChallenge;
					if (skillChallenge.Results.Fails >= 3)
					{
						creatureState = CreatureState.Bloodied;
						num2 = 3;
						num = 3;
					}
					else if (skillChallenge.Results.Successes >= skillChallenge.Successes)
					{
						creatureState = CreatureState.Defeated;
						num2 = skillChallenge.Successes;
						num = skillChallenge.Successes;
					}
					else
					{
						creatureState = CreatureState.Active;
						num = skillChallenge.Successes;
						num2 = skillChallenge.Successes - skillChallenge.Results.Successes;
					}
				}
				if (num2 < 0)
				{
					num2 = 0;
				}
				if (num2 > num)
				{
					num2 = num;
				}
				if (num > 1 && creatureState != CreatureState.Defeated)
				{
					int num4 = e.Bounds.Width - 1;
					int num5 = e.Bounds.Height / 4;
					Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Bottom - num5, num4, num5);
					Color color = (creatureState == CreatureState.Bloodied) ? Color.Red : Color.DarkGray;
					Brush brush3 = new LinearGradientBrush(rect, Color.White, Color.FromArgb(10, color), LinearGradientMode.Vertical);
					e.Graphics.FillRectangle(brush3, rect);
					e.Graphics.DrawRectangle(Pens.DarkGray, rect);
					int width = num4 * num2 / (num + num3);
					Rectangle rect2 = new Rectangle(rect.X, rect.Y, width, num5);
					Brush brush4 = new LinearGradientBrush(rect2, Color.Transparent, color, LinearGradientMode.Vertical);
					e.Graphics.FillRectangle(brush4, rect2);
					if (num3 > 0)
					{
						int width2 = num4 * num3 / (num + num3);
						Rectangle rect3 = new Rectangle(rect2.Right, rect2.Y, width2, num5);
						Brush brush5 = new LinearGradientBrush(rect3, Color.Transparent, Color.Blue, LinearGradientMode.Vertical);
						e.Graphics.FillRectangle(brush5, rect3);
					}
				}
				else
				{
					e.Graphics.DrawLine(Pens.DarkGray, e.Bounds.Left, e.Bounds.Bottom, e.Bounds.Right, e.Bounds.Bottom);
				}
			}
			e.Graphics.DrawString(e.SubItem.Text, e.Item.Font, brush, e.Bounds, format);
		}

		private void ToolsColumnsInit_Click(object sender, EventArgs e)
		{
			this.InitHdr.Width = ((this.InitHdr.Width > 0) ? 0 : 60);
			Session.Preferences.CombatColumnInitiative = (this.InitHdr.Width > 0);
		}

		private void ToolsColumnsHP_Click(object sender, EventArgs e)
		{
			this.HPHdr.Width = ((this.HPHdr.Width > 0) ? 0 : 60);
			Session.Preferences.CombatColumnHP = (this.HPHdr.Width > 0);
		}

		private void ToolsColumnsDefences_Click(object sender, EventArgs e)
		{
			this.DefHdr.Width = ((this.DefHdr.Width > 0) ? 0 : 200);
			Session.Preferences.CombatColumnDefences = (this.DefHdr.Width > 0);
		}

		private void ToolsColumnsConditions_Click(object sender, EventArgs e)
		{
			this.EffectsHdr.Width = ((this.EffectsHdr.Width > 0) ? 0 : 175);
			Session.Preferences.CombatColumnEffects = (this.EffectsHdr.Width > 0);
		}

		private void ListContext_Opening(object sender, CancelEventArgs e)
		{
			try
			{
				bool enabled = false;
				bool @checked = false;
				bool enabled2 = false;
				bool enabled3 = false;
				if (this.SelectedTokens.Count != 0)
				{
					enabled = true;
					@checked = true;
					enabled2 = true;
					foreach (IToken current in this.SelectedTokens)
					{
						if (!(current is CreatureToken) && !(current is Hero))
						{
							enabled = false;
						}
						if (current is CreatureToken)
						{
							CreatureToken creatureToken = current as CreatureToken;
							if (!creatureToken.Data.Delaying)
							{
								@checked = false;
							}
							if (this.MapView.Map != null && creatureToken.Data.Location == CombatData.NoPoint)
							{
								enabled2 = false;
							}
						}
						if (current is Hero)
						{
							Hero hero = current as Hero;
							CombatData combatData = hero.CombatData;
							if (!combatData.Delaying)
							{
								@checked = false;
							}
							if (this.MapView.Map != null && combatData.Location == CombatData.NoPoint)
							{
								enabled2 = false;
							}
						}
						if (current is CustomToken)
						{
							CustomToken customToken = current as CustomToken;
							enabled3 = true;
							if (this.MapView.Map != null && customToken.Data.Location == CombatData.NoPoint)
							{
								enabled2 = false;
							}
							if (customToken.CreatureID != Guid.Empty)
							{
								enabled2 = false;
							}
						}
					}
				}
				bool enabled4 = false;
				foreach (IToken current2 in this.SelectedTokens)
				{
					if (!(current2 is Hero))
					{
						enabled4 = true;
					}
				}
				this.ListDetails.Enabled = (this.SelectedTokens.Count == 1);
				this.ListDamage.Enabled = enabled;
				this.ListHeal.Enabled = enabled;
				this.ListCondition.Enabled = enabled;
				this.ListRemoveEffect.Enabled = (this.SelectedTokens.Count == 1);
				this.ListRemoveMap.Enabled = enabled2;
				this.ListRemoveCombat.Enabled = (this.SelectedTokens.Count != 0);
				this.ListCreateCopy.Enabled = enabled3;
				this.ListVisible.Enabled = enabled4;
				if (this.ListVisible.Enabled && this.SelectedTokens.Count == 1)
				{
					if (this.SelectedTokens[0] is CreatureToken)
					{
						CreatureToken creatureToken2 = this.SelectedTokens[0] as CreatureToken;
						this.ListVisible.Checked = creatureToken2.Data.Visible;
					}
					if (this.SelectedTokens[0] is CustomToken)
					{
						CustomToken customToken2 = this.SelectedTokens[0] as CustomToken;
						this.ListVisible.Checked = customToken2.Data.Visible;
					}
				}
				else
				{
					this.ListVisible.Checked = false;
				}
				this.ListDelay.Enabled = enabled;
				this.ListDelay.Checked = @checked;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ListDetails_Click(object sender, EventArgs e)
		{
			try
			{
				this.edit_token(this.SelectedTokens[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ListDamage_Click(object sender, EventArgs e)
		{
			try
			{
				this.do_damage(this.SelectedTokens);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ListRemoveMap_Click(object sender, EventArgs e)
		{
			this.remove_from_map(this.SelectedTokens);
		}

		private void ListRemoveCombat_Click(object sender, EventArgs e)
		{
			this.remove_from_combat(this.SelectedTokens);
		}

		private void ListVisible_Click(object sender, EventArgs e)
		{
			this.toggle_visibility(this.SelectedTokens);
		}

		private void MapContext_Opening(object sender, CancelEventArgs e)
		{
			try
			{
				bool enabled = false;
				bool @checked = false;
				bool enabled2 = false;
				bool enabled3 = false;
				if (this.MapView.SelectedTokens.Count != 0)
				{
					enabled = true;
					@checked = true;
					enabled2 = true;
					foreach (IToken current in this.MapView.SelectedTokens)
					{
						if (!(current is CreatureToken) && !(current is Hero))
						{
							enabled = false;
						}
						if (current is CreatureToken)
						{
							CreatureToken creatureToken = current as CreatureToken;
							if (!creatureToken.Data.Delaying)
							{
								@checked = false;
							}
							if (creatureToken.Data.Location == CombatData.NoPoint)
							{
								enabled2 = false;
							}
						}
						if (current is Hero)
						{
							Hero hero = current as Hero;
							CombatData combatData = hero.CombatData;
							if (!combatData.Delaying)
							{
								@checked = false;
							}
							if (combatData.Location == CombatData.NoPoint)
							{
								enabled2 = false;
							}
						}
						if (current is CustomToken)
						{
							CustomToken customToken = current as CustomToken;
							enabled3 = true;
							if (customToken.Data.Location == CombatData.NoPoint)
							{
								enabled2 = false;
							}
						}
					}
				}
				bool enabled4 = false;
				foreach (IToken current2 in this.MapView.SelectedTokens)
				{
					if (!(current2 is Hero))
					{
						enabled4 = true;
					}
				}
				this.MapDetails.Enabled = (this.MapView.SelectedTokens.Count == 1);
				this.MapDamage.Enabled = enabled;
				this.MapHeal.Enabled = enabled;
				this.MapAddEffect.Enabled = enabled;
				this.MapRemoveEffect.Enabled = enabled;
				this.MapRemoveMap.Enabled = enabled2;
				this.MapRemoveCombat.Enabled = (this.MapView.SelectedTokens.Count != 0);
				this.MapCreateCopy.Enabled = enabled3;
				this.MapVisible.Enabled = enabled4;
				if (this.MapVisible.Enabled && this.MapView.SelectedTokens.Count == 1)
				{
					if (this.MapView.SelectedTokens[0] is CreatureToken)
					{
						CreatureToken creatureToken2 = this.MapView.SelectedTokens[0] as CreatureToken;
						this.MapVisible.Checked = creatureToken2.Data.Visible;
					}
					if (this.MapView.SelectedTokens[0] is CustomToken)
					{
						CustomToken customToken2 = this.MapView.SelectedTokens[0] as CustomToken;
						this.MapVisible.Checked = customToken2.Data.Visible;
					}
				}
				else
				{
					this.MapVisible.Checked = false;
				}
				this.MapDelay.Enabled = enabled;
				this.MapDelay.Checked = @checked;
				this.MapContextDrawing.Checked = this.MapView.AllowDrawing;
				this.MapContextClearDrawings.Enabled = (this.MapView.Sketches.Count != 0);
				this.MapContextLOS.Checked = this.MapView.LineOfSight;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapDetails_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.MapView.SelectedTokens.Count != 0)
				{
					this.edit_token(this.MapView.SelectedTokens[0]);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapDamage_Click(object sender, EventArgs e)
		{
			try
			{
				this.do_damage(this.MapView.SelectedTokens);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MapRemoveMap_Click(object sender, EventArgs e)
		{
			this.remove_from_map(this.MapView.SelectedTokens);
		}

		private void MapRemoveCombat_Click(object sender, EventArgs e)
		{
			this.remove_from_combat(this.MapView.SelectedTokens);
		}

		private void MapVisible_Click(object sender, EventArgs e)
		{
			this.toggle_visibility(this.MapView.SelectedTokens);
		}

		private void CombatantsBtn_DropDownOpening(object sender, EventArgs e)
		{
			this.CombatantsAddToken.Visible = (this.fEncounter.MapID != Guid.Empty);
			this.CombatantsAddOverlay.Visible = (this.fEncounter.MapID != Guid.Empty);
			this.CombatantsRemove.Enabled = (this.SelectedTokens.Count != 0);
			this.CombatantsWaves.DropDownItems.Clear();
			foreach (EncounterWave current in this.fEncounter.Waves)
			{
				if (current.Count != 0)
				{
					ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(current.Name);
					toolStripMenuItem.Checked = current.Active;
					toolStripMenuItem.Tag = current;
					toolStripMenuItem.Click += new EventHandler(this.wave_activated);
					this.CombatantsWaves.DropDownItems.Add(toolStripMenuItem);
				}
			}
			if (this.CombatantsWaves.DropDownItems.Count == 0)
			{
				ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem("(none set)");
				toolStripMenuItem2.Enabled = false;
				this.CombatantsWaves.DropDownItems.Add(toolStripMenuItem2);
			}
		}

		private void wave_activated(object sender, EventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			EncounterWave encounterWave = toolStripMenuItem.Tag as EncounterWave;
			encounterWave.Active = !encounterWave.Active;
			this.update_list();
			this.update_maps();
			this.update_statusbar();
		}

		private void MapMenu_DropDownOpening(object sender, EventArgs e)
		{
			this.ShowMap.Checked = !this.MapSplitter.Panel2Collapsed;
			this.MapLOS.Checked = this.MapView.LineOfSight;
			this.MapGrid.Checked = (this.MapView.ShowGrid != MapGridMode.None);
			this.MapGridLabels.Checked = this.MapView.ShowGridLabels;
			this.MapHealth.Checked = this.MapView.ShowHealthBars;
			this.MapConditions.Checked = this.MapView.ShowConditions;
			this.MapPictureTokens.Checked = this.MapView.ShowPictureTokens;
			this.MapNavigate.Checked = this.MapView.AllowScrolling;
			this.MapFogAllCreatures.Checked = (this.MapView.ShowCreatures == CreatureViewMode.All);
			this.MapFogVisibleCreatures.Checked = (this.MapView.ShowCreatures == CreatureViewMode.Visible);
			this.MapFogHideCreatures.Checked = (this.MapView.ShowCreatures == CreatureViewMode.None);
			this.MapDrawing.Checked = this.MapView.AllowDrawing;
			this.MapClearDrawings.Enabled = (this.MapView.Sketches.Count != 0);
		}

		private void PlayerViewMapMenu_DropDownOpening(object sender, EventArgs e)
		{
			this.PlayerViewMap.Checked = (this.PlayerMap != null);
			this.PlayerViewInitList.Checked = (this.PlayerInitiative != null);
			this.PlayerViewLOS.Enabled = (this.PlayerMap != null);
			this.PlayerViewLOS.Checked = (this.PlayerMap != null && this.PlayerMap.LineOfSight);
			this.PlayerViewGrid.Enabled = (this.PlayerMap != null);
			this.PlayerViewGrid.Checked = (this.PlayerMap != null && this.PlayerMap.ShowGrid != MapGridMode.None);
			this.PlayerViewGridLabels.Enabled = (this.PlayerMap != null);
			this.PlayerViewGridLabels.Checked = (this.PlayerMap != null && this.PlayerMap.ShowGridLabels);
			this.PlayerHealth.Enabled = (this.PlayerMap != null);
			this.PlayerHealth.Checked = (this.PlayerMap != null && this.PlayerMap.ShowHealthBars);
			this.PlayerConditions.Enabled = (this.PlayerMap != null);
			this.PlayerConditions.Checked = (this.PlayerMap != null && this.PlayerMap.ShowConditions);
			this.PlayerPictureTokens.Enabled = (this.PlayerMap != null);
			this.PlayerPictureTokens.Checked = (this.PlayerMap != null && this.PlayerMap.ShowPictureTokens);
			this.PlayerLabels.Enabled = (this.PlayerMap != null || this.PlayerInitiative != null);
			this.PlayerLabels.Checked = ((this.PlayerMap != null && this.PlayerMap.ShowCreatureLabels) || (this.PlayerInitiative != null && Session.Preferences.PlayerViewCreatureLabels));
			this.PlayerViewFog.Enabled = (this.PlayerMap != null);
			this.PlayerFogAll.Checked = (this.PlayerMap != null && this.PlayerMap.ShowCreatures == CreatureViewMode.All);
			this.PlayerFogVisible.Checked = (this.PlayerMap != null && this.PlayerMap.ShowCreatures == CreatureViewMode.Visible);
			this.PlayerFogNone.Checked = (this.PlayerMap != null && this.PlayerMap.ShowCreatures == CreatureViewMode.None);
		}

		private void ToolsMenu_DopDownOpening(object sender, EventArgs e)
		{
			this.ToolsAddIns.DropDownItems.Clear();
			foreach (IAddIn current in Session.AddIns)
			{
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(current.Name);
				toolStripMenuItem.ToolTipText = TextHelper.Wrap(current.Description);
				toolStripMenuItem.Tag = current;
				this.ToolsAddIns.DropDownItems.Add(toolStripMenuItem);
				foreach (ICommand current2 in current.CombatCommands)
				{
					ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(current2.Name);
					toolStripMenuItem2.ToolTipText = TextHelper.Wrap(current2.Description);
					toolStripMenuItem2.Enabled = current2.Available;
					toolStripMenuItem2.Checked = current2.Active;
					toolStripMenuItem2.Click += new EventHandler(this.add_in_command_clicked);
					toolStripMenuItem2.Tag = current2;
					toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
				}
				if (current.Commands.Count == 0)
				{
					ToolStripItem toolStripItem = this.ToolsAddIns.DropDownItems.Add("(no commands)");
					toolStripItem.Enabled = false;
				}
			}
			if (Session.AddIns.Count == 0)
			{
				ToolStripItem toolStripItem2 = this.ToolsAddIns.DropDownItems.Add("(none)");
				toolStripItem2.Enabled = false;
			}
		}

		private void OptionsMenu_DropDownOpening(object sender, EventArgs e)
		{
			bool enabled = !this.MapSplitter.Panel2Collapsed;
			this.OptionsShowInit.Checked = this.InitiativePanel.Visible;
			this.OneColumn.Checked = (this.ListSplitter.Orientation == Orientation.Horizontal);
			this.TwoColumns.Checked = (this.ListSplitter.Orientation == Orientation.Vertical);
			this.OneColumn.Enabled = enabled;
			this.TwoColumns.Enabled = enabled;
			this.MapRight.Enabled = enabled;
			this.MapBelow.Enabled = enabled;
			this.MapRight.Checked = (this.MapSplitter.Orientation == Orientation.Vertical);
			this.MapBelow.Checked = (this.MapSplitter.Orientation == Orientation.Horizontal);
			this.OptionsLandscape.Enabled = enabled;
			this.OptionsPortrait.Enabled = enabled;
			this.OptionsLandscape.Checked = (this.OneColumn.Checked && this.MapRight.Checked);
			this.OptionsPortrait.Checked = (this.TwoColumns.Checked && this.MapBelow.Checked);
			this.ToolsAutoRemove.Checked = Session.Preferences.CreatureAutoRemove;
			this.OptionsIPlay4e.Checked = Session.Preferences.iPlay4E;
		}

		private void EffectMenu_DropDownOpening(object sender, EventArgs e)
		{
			this.update_effects_list(this.EffectMenu, true);
		}

		private void ListCondition_DropDownOpening(object sender, EventArgs e)
		{
			this.update_effects_list(this.ListCondition, true);
		}

		private void MapCondition_DropDownOpening(object sender, EventArgs e)
		{
			this.update_effects_list(this.MapAddEffect, false);
		}

		private void ListRemoveEffect_DropDownOpening(object sender, EventArgs e)
		{
			this.update_remove_effect_list(this.ListRemoveEffect, true);
		}

		private void MapRemoveEffect_DropDownOpening(object sender, EventArgs e)
		{
			this.update_remove_effect_list(this.MapRemoveEffect, false);
		}

		private void PlayerViewNoMapMenu_DropDownOpening(object sender, EventArgs e)
		{
			this.PlayerViewNoMapShowInitiativeList.Checked = (this.PlayerInitiative != null);
			this.PlayerViewNoMapShowLabels.Enabled = (this.PlayerInitiative != null);
			this.PlayerViewNoMapShowLabels.Checked = Session.Preferences.PlayerViewCreatureLabels;
		}

		private void ToolsColumns_DropDownOpening(object sender, EventArgs e)
		{
			this.ToolsColumnsInit.Checked = (this.InitHdr.Width > 0);
			this.ToolsColumnsHP.Checked = (this.HPHdr.Width > 0);
			this.ToolsColumnsDefences.Checked = (this.DefHdr.Width > 0);
			this.ToolsColumnsConditions.Checked = (this.EffectsHdr.Width > 0);
		}

		private void CloseBtn_Click(object sender, EventArgs e)
		{
			this.fPromptOnClose = false;
			base.Close();
		}

		private void PauseBtn_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "Would you like to be able to resume this encounter later?";
				text += Environment.NewLine;
				text += Environment.NewLine;
				text += "If you click Yes, the encounter can be restarted by selecting Paused Encounters from the Project menu.";
				if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					CombatState combatState = new CombatState();
					this.fLog.AddPauseEntry();
					Dictionary<Guid, CombatData> dictionary = new Dictionary<Guid, CombatData>();
					foreach (Hero current in Session.Project.Heroes)
					{
						dictionary[current.ID] = current.CombatData;
					}
					combatState.Encounter = this.fEncounter;
					combatState.CurrentRound = this.fCurrentRound;
					combatState.PartyLevel = this.fPartyLevel;
					combatState.HeroData = dictionary;
					combatState.TrapData = this.fTrapData;
					combatState.TokenLinks = this.MapView.TokenLinks;
					combatState.RemovedCreatureXP = this.fRemovedCreatureXP;
					combatState.Viewpoint = this.MapView.Viewpoint;
					combatState.Log = this.fLog;
					if (this.fCurrentActor != null)
					{
						combatState.CurrentActor = this.fCurrentActor.ID;
					}
					foreach (MapSketch current2 in this.MapView.Sketches)
					{
						combatState.Sketches.Add(current2.Copy());
					}
					foreach (OngoingCondition current3 in this.fEffects)
					{
						combatState.QuickEffects.Add(current3.Copy());
					}
					Session.Project.SavedCombats.Add(combatState);
					Session.Modified = true;
					foreach (Form form in Application.OpenForms)
					{
						PausedCombatListForm pausedCombatListForm = form as PausedCombatListForm;
						if (pausedCombatListForm != null)
						{
							pausedCombatListForm.UpdateEncounters();
						}
					}
					this.fPromptOnClose = false;
					base.Close();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
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

		private void ReportBtn_Click(object sender, EventArgs e)
		{
			EncounterReportForm encounterReportForm = new EncounterReportForm(this.fLog, this.fEncounter);
			encounterReportForm.ShowDialog();
		}

		private void start_combat()
		{
			this.roll_initiative();
			List<int> initiatives = this.get_initiatives();
			if (initiatives.Count != 0)
			{
				int init = initiatives[0];
				List<CombatData> list = this.get_combatants(init, false);
				if (list.Count != 0)
				{
					this.fCurrentActor = list[0];
				}
				if (this.fCurrentActor != null)
				{
					this.fCombatStarted = true;
					this.InitiativePanel.CurrentInitiative = this.fCurrentActor.Initiative;
					this.select_current_actor();
					this.update_list();
					this.update_maps();
					this.update_statusbar();
					this.update_preview_panel();
					this.highlight_current_actor();
					this.fLog.Active = true;
					this.fLog.AddStartRoundEntry(this.fCurrentRound);
					this.fLog.AddStartTurnEntry(this.fCurrentActor.ID);
					this.update_log();
				}
			}
		}

		private void roll_initiative()
		{
			List<Pair<List<CombatData>, int>> list = new List<Pair<List<CombatData>, int>>();
			Dictionary<string, List<CombatData>> dictionary = new Dictionary<string, List<CombatData>>();
			foreach (Hero current in Session.Project.Heroes)
			{
				if (current.CombatData.Initiative == -2147483648)
				{
					switch (Session.Preferences.HeroInitiativeMode)
					{
					case InitiativeMode.AutoIndividual:
						list.Add(new Pair<List<CombatData>, int>(new List<CombatData>
						{
							current.CombatData
						}, current.InitBonus));
						break;
					case InitiativeMode.ManualIndividual:
						dictionary[current.Name] = new List<CombatData>();
						dictionary[current.Name].Add(current.CombatData);
						break;
					}
				}
			}
			foreach (EncounterSlot current2 in this.fEncounter.Slots)
			{
				switch (Session.Preferences.InitiativeMode)
				{
				case InitiativeMode.AutoGroup:
				{
					List<CombatData> list2 = new List<CombatData>();
					foreach (CombatData current3 in current2.CombatData)
					{
						if (current3.Initiative == -2147483648)
						{
							list2.Add(current3);
						}
					}
					if (list2.Count != 0)
					{
						list.Add(new Pair<List<CombatData>, int>(list2, current2.Card.Initiative));
						continue;
					}
					continue;
				}
				case InitiativeMode.AutoIndividual:
					using (List<CombatData>.Enumerator enumerator4 = current2.CombatData.GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							CombatData current4 = enumerator4.Current;
							if (current4.Initiative == -2147483648)
							{
								list.Add(new Pair<List<CombatData>, int>(new List<CombatData>
								{
									current4
								}, current2.Card.Initiative));
							}
						}
						continue;
					}
					break;
				case InitiativeMode.ManualIndividual:
					foreach (CombatData current5 in current2.CombatData)
					{
						if (current5.Initiative == -2147483648)
						{
							dictionary[current5.DisplayName] = new List<CombatData>();
							dictionary[current5.DisplayName].Add(current5);
						}
					}
					continue;
				case InitiativeMode.ManualGroup:
					break;
				default:
					continue;
				}
				List<CombatData> list3 = new List<CombatData>();
				foreach (CombatData current6 in current2.CombatData)
				{
					if (current6.Initiative == -2147483648)
					{
						list3.Add(current6);
					}
				}
				if (list3.Count != 0)
				{
					dictionary[current2.Card.Title] = list3;
				}
			}
			foreach (Trap current7 in this.fEncounter.Traps)
			{
				bool flag = current7.Initiative != -2147483648;
				if (flag)
				{
					CombatData combatData = this.fTrapData[current7.ID];
					if (combatData.Initiative == -2147483648)
					{
						switch (Session.Preferences.TrapInitiativeMode)
						{
						case InitiativeMode.AutoIndividual:
							list.Add(new Pair<List<CombatData>, int>(new List<CombatData>
							{
								combatData
							}, current7.Initiative));
							break;
						case InitiativeMode.ManualIndividual:
							dictionary[current7.Name] = new List<CombatData>();
							dictionary[current7.Name].Add(combatData);
							break;
						}
					}
				}
			}
			foreach (Pair<List<CombatData>, int> current8 in list)
			{
				int initiative = Session.Dice(1, 20) + current8.Second;
				foreach (CombatData current9 in current8.First)
				{
					current9.Initiative = initiative;
				}
			}
			if (dictionary.Count != 0)
			{
				GroupInitiativeForm groupInitiativeForm = new GroupInitiativeForm(dictionary, this.fEncounter);
				groupInitiativeForm.ShowDialog();
			}
			this.InitiativePanel.InitiativeScores = this.get_initiatives();
		}

		private void select_current_actor()
		{
			foreach (ListViewItem listViewItem in this.CombatList.Items)
			{
				listViewItem.Selected = false;
			}
			ListViewItem listViewItem2 = this.get_combatant(this.fCurrentActor.ID);
			if (listViewItem2 != null)
			{
				listViewItem2.Selected = true;
			}
		}

		private void set_map(List<TokenLink> token_links, Rectangle viewpoint, List<MapSketch> sketches)
		{
			Map map = Session.Project.FindTacticalMap(this.fEncounter.MapID);
			this.MapView.Map = map;
			if (token_links != null)
			{
				this.MapView.TokenLinks = token_links;
				using (List<TokenLink>.Enumerator enumerator = this.MapView.TokenLinks.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TokenLink current = enumerator.Current;
						foreach (IToken current2 in current.Tokens)
						{
							CreatureToken creatureToken = current2 as CreatureToken;
							if (creatureToken != null)
							{
								EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
								if (encounterSlot != null)
								{
									creatureToken.Data = encounterSlot.FindCombatData(creatureToken.Data.Location);
								}
							}
						}
					}
					goto IL_E2;
				}
			}
			this.MapView.TokenLinks = new List<TokenLink>();
			IL_E2:
			if (viewpoint != Rectangle.Empty)
			{
				this.MapView.Viewpoint = viewpoint;
			}
			else if (this.fEncounter.MapAreaID != Guid.Empty)
			{
				MapArea mapArea = map.FindArea(this.fEncounter.MapAreaID);
				if (mapArea != null)
				{
					this.MapView.Viewpoint = mapArea.Region;
				}
			}
			foreach (MapSketch current3 in sketches)
			{
				this.MapView.Sketches.Add(current3.Copy());
			}
			this.MapView.Encounter = this.fEncounter;
			this.MapView.ShowHealthBars = Session.Preferences.CombatHealthBars;
			this.MapView.ShowCreatures = Session.Preferences.CombatFog;
			this.MapView.ShowPictureTokens = Session.Preferences.CombatPictureTokens;
			this.MapView.ShowGrid = (Session.Preferences.CombatGrid ? MapGridMode.Overlay : MapGridMode.None);
			this.MapView.ShowGridLabels = Session.Preferences.CombatGridLabels;
			if (this.fEncounter.MapID == Guid.Empty)
			{
				this.MapSplitter.Panel2Collapsed = true;
				this.CombatList.Groups[5].Header = "Non-Combatants";
			}
			else if (!Session.Preferences.CombatMapRight)
			{
				this.OptionsMapBelow_Click(null, null);
			}
			if (this.fEncounter.MapID != Guid.Empty && Session.Preferences.CombatTwoColumns)
			{
				this.TwoColumns_Click(null, null);
			}
			if (this.fEncounter.MapID == Guid.Empty && Session.Preferences.CombatTwoColumnsNoMap)
			{
				this.TwoColumns_Click(null, null);
			}
		}

		private void do_damage(List<IToken> tokens)
		{
			List<Pair<CombatData, EncounterCard>> list = new List<Pair<CombatData, EncounterCard>>();
			foreach (IToken current in tokens)
			{
				CombatData first = null;
				EncounterCard second = null;
				if (current is CreatureToken)
				{
					CreatureToken creatureToken = current as CreatureToken;
					first = creatureToken.Data;
					EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
					second = encounterSlot.Card;
				}
				if (current is Hero)
				{
					Hero hero = current as Hero;
					first = hero.CombatData;
				}
				list.Add(new Pair<CombatData, EncounterCard>(first, second));
			}
			Dictionary<CombatData, int> dictionary = new Dictionary<CombatData, int>();
			Dictionary<CombatData, CreatureState> dictionary2 = new Dictionary<CombatData, CreatureState>();
			foreach (Pair<CombatData, EncounterCard> current2 in list)
			{
				dictionary[current2.First] = current2.First.Damage;
			}
			foreach (Pair<CombatData, EncounterCard> current3 in list)
			{
				dictionary2[current3.First] = this.get_state(current3.First);
			}
			DamageForm damageForm = new DamageForm(list, 0);
			if (damageForm.ShowDialog() == DialogResult.OK)
			{
				foreach (Pair<CombatData, EncounterCard> current4 in list)
				{
					int num = current4.First.Damage - dictionary[current4.First];
					if (num != 0)
					{
						this.fLog.AddDamageEntry(current4.First.ID, num, damageForm.Types);
					}
					CreatureState creatureState = this.get_state(current4.First);
					if (creatureState != dictionary2[current4.First])
					{
						this.fLog.AddStateEntry(current4.First.ID, creatureState);
					}
				}
				this.update_list();
				this.update_log();
				this.update_preview_panel();
				this.update_maps();
			}
		}

		private void do_heal(List<IToken> tokens)
		{
			List<Pair<CombatData, EncounterCard>> list = new List<Pair<CombatData, EncounterCard>>();
			foreach (IToken current in tokens)
			{
				CombatData first = null;
				EncounterCard second = null;
				if (current is CreatureToken)
				{
					CreatureToken creatureToken = current as CreatureToken;
					first = creatureToken.Data;
					EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
					second = encounterSlot.Card;
				}
				if (current is Hero)
				{
					Hero hero = current as Hero;
					first = hero.CombatData;
				}
				list.Add(new Pair<CombatData, EncounterCard>(first, second));
			}
			Dictionary<CombatData, int> dictionary = new Dictionary<CombatData, int>();
			Dictionary<CombatData, CreatureState> dictionary2 = new Dictionary<CombatData, CreatureState>();
			foreach (Pair<CombatData, EncounterCard> current2 in list)
			{
				dictionary[current2.First] = current2.First.Damage;
			}
			foreach (Pair<CombatData, EncounterCard> current3 in list)
			{
				dictionary2[current3.First] = this.get_state(current3.First);
			}
			HealForm healForm = new HealForm(list);
			if (healForm.ShowDialog() == DialogResult.OK)
			{
				foreach (Pair<CombatData, EncounterCard> current4 in list)
				{
					int num = current4.First.Damage - dictionary[current4.First];
					if (num != 0)
					{
						this.fLog.AddDamageEntry(current4.First.ID, num, null);
					}
					CreatureState creatureState = this.get_state(current4.First);
					if (creatureState != dictionary2[current4.First])
					{
						this.fLog.AddStateEntry(current4.First.ID, creatureState);
					}
				}
				this.update_list();
				this.update_log();
				this.update_preview_panel();
				this.update_maps();
			}
		}

		private void copy_custom_token()
		{
			foreach (IToken current in this.SelectedTokens)
			{
				if (current is CustomToken)
				{
					CustomToken customToken = current as CustomToken;
					CustomToken customToken2 = customToken.Copy();
					customToken2.ID = Guid.NewGuid();
					customToken2.Data.Location = CombatData.NoPoint;
					this.fEncounter.CustomTokens.Add(customToken2);
				}
			}
			this.update_list();
		}

		private void show_player_view(bool map, bool initiative)
		{
			try
			{
				if (!map && !initiative)
				{
					Session.PlayerView.ShowDefault();
				}
				else
				{
					if (Session.PlayerView == null)
					{
						Session.PlayerView = new PlayerViewForm(this);
					}
					if (this.PlayerMap == null && this.PlayerInitiative == null)
					{
						Session.PlayerView.ShowTacticalMap(this.MapView, this.InitiativeView());
					}
					SplitContainer splitContainer = Session.PlayerView.Controls[0] as SplitContainer;
					if (splitContainer != null)
					{
						splitContainer.Panel1Collapsed = !map;
						splitContainer.Panel2Collapsed = !initiative;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void list_splitter_changed()
		{
			try
			{
				if (base.Visible)
				{
					this.update_preview_panel();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void select_token(IToken token)
		{
			foreach (ListViewItem listViewItem in this.CombatList.Items)
			{
				if (token is CreatureToken && listViewItem.Tag is CreatureToken)
				{
					CreatureToken creatureToken = token as CreatureToken;
					CreatureToken creatureToken2 = listViewItem.Tag as CreatureToken;
					if (creatureToken.Data == creatureToken2.Data)
					{
						listViewItem.Selected = true;
					}
				}
				if (token is CustomToken && listViewItem.Tag is CustomToken)
				{
					CustomToken customToken = token as CustomToken;
					CustomToken customToken2 = listViewItem.Tag as CustomToken;
					if (customToken.Data == customToken2.Data)
					{
						listViewItem.Selected = true;
					}
				}
				if (token is Hero && listViewItem.Tag is Hero)
				{
					Hero hero = token as Hero;
					Hero hero2 = listViewItem.Tag as Hero;
					if (hero == hero2)
					{
						listViewItem.Selected = true;
					}
				}
			}
		}

		private void set_delay(List<IToken> tokens)
		{
			try
			{
				foreach (IToken current in tokens)
				{
					CombatData combatData = null;
					if (current is CreatureToken)
					{
						CreatureToken creatureToken = current as CreatureToken;
						combatData = creatureToken.Data;
					}
					if (current is Hero)
					{
						Hero hero = current as Hero;
						combatData = hero.CombatData;
					}
					if (combatData != null)
					{
						combatData.Delaying = !combatData.Delaying;
						if (combatData.Delaying)
						{
							this.InitiativePanel.InitiativeScores = this.get_initiatives();
						}
						else
						{
							combatData.Initiative = this.InitiativePanel.CurrentInitiative;
						}
					}
				}
				this.update_list();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private string get_info(CreatureToken token)
		{
			string text = "";
			EncounterSlot encounterSlot = this.fEncounter.FindSlot(token.SlotID);
			List<string> list = encounterSlot.Card.AsText(token.Data, CardMode.Text, true);
			foreach (string current in list)
			{
				if (text != "")
				{
					text += Environment.NewLine;
				}
				text += current;
			}
			if (token.Data.Conditions.Count != 0)
			{
				text += Environment.NewLine;
				foreach (OngoingCondition current2 in token.Data.Conditions)
				{
					text += Environment.NewLine;
					text += current2.ToString(this.fEncounter, false);
				}
			}
			return text;
		}

		private string get_info(Hero hero)
		{
			string text = hero.Race + " " + hero.Class;
			if (hero.Player != "")
			{
				text += Environment.NewLine;
				text = text + "Player: " + hero.Player;
			}
			CombatData combatData = hero.CombatData;
			if (combatData != null && combatData.Conditions.Count != 0)
			{
				text += Environment.NewLine;
				foreach (OngoingCondition current in combatData.Conditions)
				{
					text += Environment.NewLine;
					text += current.ToString(this.fEncounter, false);
				}
			}
			return text;
		}

		private string get_info(CustomToken token)
		{
			if (!(token.Details != ""))
			{
				return "(no details)";
			}
			return token.Details;
		}

		private void edit_token(IToken token)
		{
			if (token is CreatureToken)
			{
				CreatureToken creatureToken = token as CreatureToken;
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
				int index = encounterSlot.CombatData.IndexOf(creatureToken.Data);
				int num = creatureToken.Data.Damage;
				CreatureState state = encounterSlot.GetState(creatureToken.Data);
				List<string> list = new List<string>();
				foreach (OngoingCondition current in creatureToken.Data.Conditions)
				{
					list.Add(current.ToString(this.fEncounter, false));
				}
				CombatDataForm combatDataForm = new CombatDataForm(creatureToken.Data, encounterSlot.Card, this.fEncounter, this.fCurrentActor, this.fCurrentRound, true);
				if (combatDataForm.ShowDialog() == DialogResult.OK)
				{
					encounterSlot.CombatData[index] = combatDataForm.Data;
					if (num != combatDataForm.Data.Damage)
					{
						num = combatDataForm.Data.Damage - num;
						this.fLog.AddDamageEntry(combatDataForm.Data.ID, num, null);
					}
					if (encounterSlot.GetState(combatDataForm.Data) != state)
					{
						state = encounterSlot.GetState(combatDataForm.Data);
						this.fLog.AddStateEntry(combatDataForm.Data.ID, state);
					}
					List<string> list2 = new List<string>();
					foreach (OngoingCondition current2 in combatDataForm.Data.Conditions)
					{
						list2.Add(current2.ToString(this.fEncounter, false));
					}
					foreach (string current3 in list)
					{
						if (!list2.Contains(current3))
						{
							this.fLog.AddEffectEntry(combatDataForm.Data.ID, current3, false);
						}
					}
					foreach (string current4 in list2)
					{
						if (!list.Contains(current4))
						{
							this.fLog.AddEffectEntry(combatDataForm.Data.ID, current4, true);
						}
					}
					this.update_list();
					this.update_log();
					this.update_preview_panel();
					this.update_maps();
					this.InitiativePanel.InitiativeScores = this.get_initiatives();
				}
			}
			if (token is Hero)
			{
				Hero hero = token as Hero;
				if (hero.CombatData.Initiative == -2147483648)
				{
					this.edit_initiative(hero);
				}
				else
				{
					CombatData combatData = hero.CombatData;
					int num2 = combatData.Damage;
					CreatureState state2 = hero.GetState(combatData.Damage);
					List<string> list3 = new List<string>();
					foreach (OngoingCondition current5 in combatData.Conditions)
					{
						list3.Add(current5.ToString(this.fEncounter, false));
					}
					CombatDataForm combatDataForm2 = new CombatDataForm(combatData, null, this.fEncounter, this.fCurrentActor, this.fCurrentRound, false);
					if (combatDataForm2.ShowDialog() == DialogResult.OK)
					{
						hero.CombatData = combatDataForm2.Data;
						if (num2 != combatDataForm2.Data.Damage)
						{
							num2 = combatDataForm2.Data.Damage - num2;
							this.fLog.AddDamageEntry(combatDataForm2.Data.ID, num2, null);
						}
						if (hero.GetState(combatDataForm2.Data.Damage) != state2)
						{
							state2 = hero.GetState(combatDataForm2.Data.Damage);
							this.fLog.AddStateEntry(combatDataForm2.Data.ID, state2);
						}
						List<string> list4 = new List<string>();
						foreach (OngoingCondition current6 in combatDataForm2.Data.Conditions)
						{
							list4.Add(current6.ToString(this.fEncounter, false));
						}
						foreach (string current7 in list3)
						{
							if (!list4.Contains(current7))
							{
								this.fLog.AddEffectEntry(combatDataForm2.Data.ID, current7, false);
							}
						}
						foreach (string current8 in list4)
						{
							if (!list3.Contains(current8))
							{
								this.fLog.AddEffectEntry(combatDataForm2.Data.ID, current8, true);
							}
						}
						this.update_list();
						this.update_log();
						this.update_preview_panel();
						this.update_maps();
						this.InitiativePanel.InitiativeScores = this.get_initiatives();
					}
				}
			}
			if (token is CustomToken)
			{
				CustomToken customToken = token as CustomToken;
				int num3 = this.fEncounter.CustomTokens.IndexOf(customToken);
				if (num3 != -1)
				{
					switch (customToken.Type)
					{
					case CustomTokenType.Token:
					{
						CustomTokenForm customTokenForm = new CustomTokenForm(customToken);
						if (customTokenForm.ShowDialog() == DialogResult.OK)
						{
							this.fEncounter.CustomTokens[num3] = customTokenForm.Token;
							this.update_list();
							this.update_preview_panel();
							this.update_maps();
							return;
						}
						break;
					}
					case CustomTokenType.Overlay:
					{
						CustomOverlayForm customOverlayForm = new CustomOverlayForm(customToken);
						if (customOverlayForm.ShowDialog() == DialogResult.OK)
						{
							this.fEncounter.CustomTokens[num3] = customOverlayForm.Token;
							this.update_list();
							this.update_preview_panel();
							this.update_maps();
						}
						break;
					}
					default:
						return;
					}
				}
			}
		}

		private void set_tooltip(IToken token, Control ctrl)
		{
			string toolTipTitle = "";
			string caption = null;
			if (token is CreatureToken)
			{
				CreatureToken creatureToken = token as CreatureToken;
				toolTipTitle = creatureToken.Data.DisplayName;
				caption = this.get_info(creatureToken);
			}
			if (token is Hero)
			{
				Hero hero = token as Hero;
				toolTipTitle = hero.Name;
				caption = this.get_info(hero);
			}
			if (token is CustomToken)
			{
				CustomToken customToken = token as CustomToken;
				toolTipTitle = customToken.Name;
				caption = this.get_info(customToken);
			}
			this.MapTooltip.ToolTipTitle = toolTipTitle;
			this.MapTooltip.SetToolTip(ctrl, caption);
		}

		private void remove_from_map(List<IToken> tokens)
		{
			try
			{
				foreach (IToken current in tokens)
				{
					if (current is CreatureToken)
					{
						CreatureToken creatureToken = current as CreatureToken;
						creatureToken.Data.Location = CombatData.NoPoint;
						this.remove_effects(current);
						this.remove_links(current);
					}
					if (current is Hero)
					{
						Hero hero = current as Hero;
						hero.CombatData.Location = CombatData.NoPoint;
						this.remove_effects(current);
						this.remove_links(current);
					}
					if (current is CustomToken)
					{
						CustomToken customToken = current as CustomToken;
						customToken.Data.Location = CombatData.NoPoint;
						if (customToken.Type == CustomTokenType.Token)
						{
							this.remove_links(current);
						}
					}
				}
				this.update_list();
				this.update_preview_panel();
				this.update_maps();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void remove_from_combat(List<IToken> tokens)
		{
			try
			{
				foreach (IToken current in tokens)
				{
					if (current is CreatureToken)
					{
						CreatureToken creatureToken = current as CreatureToken;
						EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
						encounterSlot.CombatData.Remove(creatureToken.Data);
						this.fRemovedCreatureXP += encounterSlot.Card.XP;
						this.remove_effects(current);
						this.remove_links(current);
					}
					if (current is Hero)
					{
						Hero hero = current as Hero;
						hero.CombatData.Initiative = -2147483648;
						hero.CombatData.Location = CombatData.NoPoint;
						this.remove_effects(current);
						this.remove_links(current);
					}
					if (current is CustomToken)
					{
						CustomToken customToken = current as CustomToken;
						this.fEncounter.CustomTokens.Remove(customToken);
						if (customToken.Type == CustomTokenType.Token)
						{
							this.remove_links(current);
						}
					}
				}
				this.update_list();
				this.update_preview_panel();
				this.update_maps();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void remove_effects(IToken token)
		{
			Guid guid = Guid.Empty;
			if (token is CreatureToken)
			{
				CreatureToken creatureToken = token as CreatureToken;
				guid = creatureToken.Data.ID;
			}
			if (token is Hero)
			{
				Hero hero = token as Hero;
				guid = hero.ID;
			}
			if (guid == Guid.Empty)
			{
				return;
			}
			foreach (Hero current in Session.Project.Heroes)
			{
				CombatData combatData = current.CombatData;
				this.remove_effects(guid, combatData);
			}
			foreach (EncounterSlot current2 in this.fEncounter.AllSlots)
			{
				foreach (CombatData current3 in current2.CombatData)
				{
					this.remove_effects(guid, current3);
				}
			}
		}

		private void remove_effects(Guid token_id, CombatData data)
		{
			List<OngoingCondition> list = new List<OngoingCondition>();
			foreach (OngoingCondition current in data.Conditions)
			{
				if (!(current.DurationCreatureID != token_id) && (current.Duration == DurationType.BeginningOfTurn || current.Duration == DurationType.EndOfTurn))
				{
					list.Add(current);
				}
			}
			foreach (OngoingCondition current2 in list)
			{
				data.Conditions.Remove(current2);
			}
		}

		private void remove_links(IToken token)
		{
			Point right = this.get_location(token);
			List<TokenLink> list = new List<TokenLink>();
			foreach (TokenLink current in this.MapView.TokenLinks)
			{
				foreach (IToken current2 in current.Tokens)
				{
					if (this.get_location(current2) == right)
					{
						list.Add(current);
						break;
					}
				}
			}
			foreach (TokenLink current3 in list)
			{
				this.MapView.TokenLinks.Remove(current3);
			}
			this.update_maps();
		}

		private Point get_location(IToken token)
		{
			if (token is CreatureToken)
			{
				CreatureToken creatureToken = token as CreatureToken;
				return creatureToken.Data.Location;
			}
			if (token is Hero)
			{
				Hero hero = token as Hero;
				return hero.CombatData.Location;
			}
			if (token is CustomToken)
			{
				CustomToken customToken = token as CustomToken;
				return customToken.Data.Location;
			}
			return CombatData.NoPoint;
		}

		private void toggle_visibility(List<IToken> tokens)
		{
			try
			{
				foreach (IToken current in tokens)
				{
					if (current is CreatureToken)
					{
						CreatureToken creatureToken = current as CreatureToken;
						creatureToken.Data.Visible = !creatureToken.Data.Visible;
					}
					if (current is CustomToken)
					{
						CustomToken customToken = current as CustomToken;
						customToken.Data.Visible = !customToken.Data.Visible;
					}
				}
				this.update_list();
				this.update_preview_panel();
				this.update_maps();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void show_or_hide_all(bool visible)
		{
			foreach (EncounterSlot current in this.fEncounter.AllSlots)
			{
				foreach (CombatData current2 in current.CombatData)
				{
					current2.Visible = visible;
				}
			}
			foreach (CustomToken current3 in this.fEncounter.CustomTokens)
			{
				current3.Data.Visible = visible;
			}
			this.update_list();
			this.update_preview_panel();
			this.update_maps();
		}

		private void roll_attack(CreaturePower power)
		{
			AttackRollForm attackRollForm = new AttackRollForm(power, this.fEncounter);
			attackRollForm.ShowDialog();
			this.update_list();
			this.update_log();
			this.update_preview_panel();
			this.update_maps();
		}

		private void roll_check(string name, int mod)
		{
			int num = Session.Dice(1, 20);
			int num2 = num + mod;
			string text = num.ToString();
			if (num == 1 || num == 20)
			{
				text = "Natural " + text;
			}
			string text2 = string.Concat(new object[]
			{
				"Bonus:\t",
				mod,
				Environment.NewLine,
				"Roll:\t",
				text,
				Environment.NewLine,
				Environment.NewLine,
				"Result:\t",
				num2
			});
			MessageBox.Show(text2, name, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		private bool edit_initiative(Hero hero)
		{
			int initiative = hero.CombatData.Initiative;
			InitiativeForm initiativeForm = new InitiativeForm(hero.InitBonus, initiative);
			if (initiativeForm.ShowDialog() == DialogResult.OK)
			{
				hero.CombatData.Initiative = initiativeForm.Score;
				this.update_list();
				this.update_preview_panel();
				this.update_maps();
				this.update_statusbar();
				List<int> initiatives = this.get_initiatives();
				this.InitiativePanel.InitiativeScores = initiatives;
				int arg_67_0 = initiatives[0];
				return true;
			}
			return false;
		}

		private int next_init(int current_init)
		{
			List<int> initiatives = this.get_initiatives();
			if (!initiatives.Contains(current_init))
			{
				initiatives.Add(current_init);
			}
			initiatives.Sort();
			initiatives.Reverse();
			int num = initiatives.IndexOf(current_init) + 1;
			if (num == initiatives.Count)
			{
				num = 0;
			}
			return initiatives[num];
		}

		private int find_max_init()
		{
			List<int> initiatives = this.get_initiatives();
			if (initiatives.Count != 0)
			{
				return initiatives[0];
			}
			return 0;
		}

		private int find_min_init()
		{
			List<int> initiatives = this.get_initiatives();
			if (initiatives.Count != 0)
			{
				return initiatives[initiatives.Count - 1];
			}
			return 0;
		}

		private List<int> get_initiatives()
		{
			List<int> list = new List<int>();
			foreach (EncounterSlot current in this.fEncounter.AllSlots)
			{
				foreach (CombatData current2 in current.CombatData)
				{
					if (current.GetState(current2) != CreatureState.Defeated)
					{
						int initiative = current2.Initiative;
						if (initiative != -2147483648 && !list.Contains(initiative))
						{
							list.Add(initiative);
						}
					}
				}
			}
			foreach (Hero current3 in Session.Project.Heroes)
			{
				CombatData combatData = current3.CombatData;
				int initiative2 = combatData.Initiative;
				if (initiative2 != -2147483648 && !list.Contains(initiative2))
				{
					list.Add(initiative2);
				}
			}
			foreach (CombatData current4 in this.fTrapData.Values)
			{
				if (!current4.Delaying)
				{
					int initiative3 = current4.Initiative;
					if (initiative3 != -2147483648 && !list.Contains(initiative3))
					{
						list.Add(initiative3);
					}
				}
			}
			list.Sort();
			list.Reverse();
			return list;
		}

		private void handle_regen()
		{
			if (this.fCurrentActor == null)
			{
				return;
			}
			if (this.fCurrentActor.Damage <= 0)
			{
				return;
			}
			EncounterSlot encounterSlot = this.fEncounter.FindSlot(this.fCurrentActor);
			if (encounterSlot == null)
			{
				return;
			}
			Regeneration regeneration = new Regeneration();
			regeneration.Value = 0;
			if (encounterSlot.Card.Regeneration != null)
			{
				regeneration.Value = encounterSlot.Card.Regeneration.Value;
				regeneration.Details = encounterSlot.Card.Regeneration.Details;
			}
			foreach (OngoingCondition current in this.fCurrentActor.Conditions)
			{
				if (current.Type == OngoingType.Regeneration)
				{
					regeneration.Value = Math.Max(regeneration.Value, current.Regeneration.Value);
					if (current.Regeneration.Details != "")
					{
						if (regeneration.Details != "")
						{
							Regeneration expr_E2 = regeneration;
							expr_E2.Details += Environment.NewLine;
						}
						Regeneration expr_F8 = regeneration;
						expr_F8.Details += current.Regeneration.Details;
					}
				}
			}
			if (regeneration.Value == 0)
			{
				return;
			}
			string text = this.fCurrentActor.DisplayName + " has regeneration:";
			text = text + Environment.NewLine + Environment.NewLine;
			object obj = text;
			text = string.Concat(new object[]
			{
				obj,
				"Value: ",
				regeneration.Value,
				Environment.NewLine
			});
			if (regeneration.Details != "")
			{
				text = text + regeneration.Details + Environment.NewLine;
			}
			text += Environment.NewLine;
			text += "Do you want to apply it now?";
			if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
			{
				this.fCurrentActor.Damage -= regeneration.Value;
				this.fCurrentActor.Damage = Math.Max(0, this.fCurrentActor.Damage);
			}
		}

		private void handle_ended_effects(bool beginning_of_turn)
		{
			if (this.fCurrentActor == null)
			{
				return;
			}
			DurationType durationType = beginning_of_turn ? DurationType.BeginningOfTurn : DurationType.EndOfTurn;
			List<Pair<CombatData, OngoingCondition>> list = new List<Pair<CombatData, OngoingCondition>>();
			foreach (EncounterSlot current in this.fEncounter.AllSlots)
			{
				foreach (CombatData current2 in current.CombatData)
				{
					foreach (OngoingCondition current3 in current2.Conditions)
					{
						if (current3.Duration == durationType && current3.DurationRound <= this.fCurrentRound && this.fCurrentActor.ID == current3.DurationCreatureID)
						{
							list.Add(new Pair<CombatData, OngoingCondition>(current2, current3));
						}
					}
				}
			}
			foreach (Hero current4 in Session.Project.Heroes)
			{
				CombatData combatData = current4.CombatData;
				foreach (OngoingCondition current5 in combatData.Conditions)
				{
					if (current5.Duration == durationType && current5.DurationRound <= this.fCurrentRound && this.fCurrentActor.ID == current5.DurationCreatureID)
					{
						list.Add(new Pair<CombatData, OngoingCondition>(combatData, current5));
					}
				}
			}
			foreach (Guid current6 in this.fTrapData.Keys)
			{
				CombatData combatData2 = this.fTrapData[current6];
				foreach (OngoingCondition current7 in combatData2.Conditions)
				{
					if (current7.Duration == durationType && current7.DurationRound <= this.fCurrentRound && this.fCurrentActor.ID == current7.DurationCreatureID)
					{
						list.Add(new Pair<CombatData, OngoingCondition>(combatData2, current7));
					}
				}
			}
			if (list.Count > 0)
			{
				EndedEffectsForm endedEffectsForm = new EndedEffectsForm(list, this.fEncounter);
				endedEffectsForm.ShowDialog();
				this.update_list();
			}
		}

		private void handle_saves()
		{
			if (this.fCurrentActor == null)
			{
				return;
			}
			if (this.fCurrentActor.Delaying)
			{
				return;
			}
			List<OngoingCondition> list = new List<OngoingCondition>();
			foreach (OngoingCondition current in this.fCurrentActor.Conditions)
			{
				if (current.Duration == DurationType.SaveEnds)
				{
					list.Add(current);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			EncounterCard card = null;
			EncounterSlot encounterSlot = this.fEncounter.FindSlot(this.fCurrentActor);
			if (encounterSlot != null)
			{
				card = encounterSlot.Card;
			}
			SavingThrowForm savingThrowForm = new SavingThrowForm(this.fCurrentActor, card, this.fEncounter);
			if (savingThrowForm.ShowDialog() == DialogResult.OK)
			{
				this.update_list();
			}
		}

		private void handle_ongoing_damage()
		{
			if (this.fCurrentActor == null)
			{
				return;
			}
			List<OngoingCondition> list = new List<OngoingCondition>();
			foreach (OngoingCondition current in this.fCurrentActor.Conditions)
			{
				if (current.Type == OngoingType.Damage && current.Value > 0)
				{
					list.Add(current);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			EncounterCard card = null;
			EncounterSlot encounterSlot = this.fEncounter.FindSlot(this.fCurrentActor);
			if (encounterSlot != null)
			{
				card = encounterSlot.Card;
			}
			int damage = this.fCurrentActor.Damage;
			CreatureState creatureState = CreatureState.Active;
			if (encounterSlot != null)
			{
				creatureState = encounterSlot.GetState(this.fCurrentActor);
			}
			if (encounterSlot == null)
			{
				Hero hero = Session.Project.FindHero(this.fCurrentActor.ID);
				creatureState = hero.GetState(damage);
			}
			OngoingDamageForm ongoingDamageForm = new OngoingDamageForm(this.fCurrentActor, card, this.fEncounter);
			if (ongoingDamageForm.ShowDialog() == DialogResult.OK)
			{
				if (this.fCurrentActor.Damage != damage)
				{
					this.fLog.AddDamageEntry(this.fCurrentActor.ID, this.fCurrentActor.Damage - damage, null);
				}
				if (encounterSlot != null)
				{
					if (encounterSlot.GetState(this.fCurrentActor) != creatureState)
					{
						this.fLog.AddStateEntry(this.fCurrentActor.ID, encounterSlot.GetState(this.fCurrentActor));
					}
				}
				else
				{
					Hero hero2 = Session.Project.FindHero(this.fCurrentActor.ID);
					if (hero2.GetState(this.fCurrentActor.Damage) != creatureState)
					{
						this.fLog.AddStateEntry(this.fCurrentActor.ID, hero2.GetState(this.fCurrentActor.Damage));
					}
				}
				this.update_list();
				this.update_log();
			}
		}

		private void handle_recharge()
		{
			if (this.fCurrentActor == null)
			{
				return;
			}
			EncounterSlot encounterSlot = this.fEncounter.FindSlot(this.fCurrentActor);
			if (encounterSlot == null)
			{
				return;
			}
			List<CreaturePower> creaturePowers = encounterSlot.Card.CreaturePowers;
			List<CreaturePower> list = new List<CreaturePower>();
			foreach (Guid current in this.fCurrentActor.UsedPowers)
			{
				foreach (CreaturePower current2 in creaturePowers)
				{
					if (current2.ID == current && current2.Action != null && current2.Action.Recharge != "")
					{
						list.Add(current2);
					}
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			RechargeForm rechargeForm = new RechargeForm(this.fCurrentActor, encounterSlot.Card);
			if (rechargeForm.ShowDialog() == DialogResult.OK)
			{
				this.update_list();
			}
		}

		private CombatData get_next_actor(CombatData current_actor)
		{
			int num = (current_actor != null) ? current_actor.Initiative : this.InitiativePanel.CurrentInitiative;
			List<int> initiatives = this.get_initiatives();
			if (!initiatives.Contains(num))
			{
				num = this.next_init(num);
			}
			List<CombatData> list = this.get_combatants(num, true);
			int num2 = list.IndexOf(current_actor);
			CombatData combatData;
			if (num2 == -1)
			{
				combatData = list[0];
			}
			else if (num2 == list.Count - 1)
			{
				num = this.next_init(num);
				list = this.get_combatants(num, false);
				combatData = list[0];
			}
			else
			{
				combatData = list[num2 + 1];
			}
			bool flag = this.get_state(combatData) == CreatureState.Defeated;
			bool flag2 = combatData != null && combatData.Delaying;
			if (flag || flag2)
			{
				combatData = this.get_next_actor(combatData);
			}
			return combatData;
		}

		private CreatureState get_state(CombatData cd)
		{
			EncounterSlot encounterSlot = this.fEncounter.FindSlot(cd);
			if (encounterSlot != null)
			{
				return encounterSlot.GetState(cd);
			}
			Hero hero = Session.Project.FindHero(cd.ID);
			if (hero != null)
			{
				return hero.GetState(cd.Damage);
			}
			Trap trap = this.fEncounter.FindTrap(cd.ID);
			if (trap != null)
			{
				return CreatureState.Active;
			}
			return CreatureState.Active;
		}

		private List<CombatData> get_combatants(int init, bool include_defeated)
		{
			Dictionary<int, List<CombatData>> dictionary = new Dictionary<int, List<CombatData>>();
			foreach (EncounterSlot current in this.fEncounter.AllSlots)
			{
				int initiative = current.Card.Initiative;
				if (!dictionary.ContainsKey(initiative))
				{
					dictionary[initiative] = new List<CombatData>();
				}
				foreach (CombatData current2 in current.CombatData)
				{
					if ((current.GetState(current2) != CreatureState.Defeated || include_defeated) && current2.Initiative == init)
					{
						dictionary[initiative].Add(current2);
					}
				}
			}
			foreach (Hero current3 in Session.Project.Heroes)
			{
				if (!dictionary.ContainsKey(current3.InitBonus))
				{
					dictionary[current3.InitBonus] = new List<CombatData>();
				}
				CombatData combatData = current3.CombatData;
				if (combatData.Initiative == init)
				{
					dictionary[current3.InitBonus].Add(combatData);
				}
			}
			foreach (Trap current4 in this.fEncounter.Traps)
			{
				if (current4.Initiative != -2147483648 && this.fTrapData.ContainsKey(current4.ID))
				{
					if (!dictionary.ContainsKey(current4.Initiative))
					{
						dictionary[current4.Initiative] = new List<CombatData>();
					}
					CombatData combatData2 = this.fTrapData[current4.ID];
					if (combatData2.Initiative == init)
					{
						dictionary[current4.Initiative].Add(combatData2);
					}
				}
			}
			List<int> list = new List<int>();
			list.AddRange(dictionary.Keys);
			list.Sort();
			list.Reverse();
			List<CombatData> list2 = new List<CombatData>();
			foreach (int current5 in list)
			{
				dictionary[current5].Sort();
				list2.AddRange(dictionary[current5]);
			}
			return list2;
		}

		private void highlight_current_actor()
		{
			this.MapView.BoxedTokens.Clear();
			if (this.fCurrentActor != null)
			{
				Hero hero = Session.Project.FindHero(this.fCurrentActor.ID);
				if (hero != null)
				{
					this.MapView.BoxedTokens.Add(hero);
				}
				else
				{
					EncounterSlot encounterSlot = this.fEncounter.FindSlot(this.fCurrentActor);
					if (encounterSlot != null)
					{
						CreatureToken item = new CreatureToken(encounterSlot.ID, this.fCurrentActor);
						this.MapView.BoxedTokens.Add(item);
					}
				}
				this.MapView.MapChanged();
			}
		}

		private ListViewItem get_combatant(Guid id)
		{
			foreach (ListViewItem listViewItem in this.CombatList.Items)
			{
				CreatureToken creatureToken = listViewItem.Tag as CreatureToken;
				if (creatureToken != null && creatureToken.Data.ID == id)
				{
					ListViewItem result = listViewItem;
					return result;
				}
				Hero hero = listViewItem.Tag as Hero;
				if (hero != null && hero.ID == id)
				{
					ListViewItem result = listViewItem;
					return result;
				}
				Trap trap = listViewItem.Tag as Trap;
				if (trap != null && trap.ID == id)
				{
					ListViewItem result = listViewItem;
					return result;
				}
			}
			return null;
		}

		private void cancelled_scrolling()
		{
			MapView mapview = Session.Preferences.PlayerViewMap ? this.MapView : null;
			string initiative = Session.Preferences.PlayerViewInitiative ? this.InitiativeView() : null;
			Session.PlayerView.ShowTacticalMap(mapview, initiative);
			this.PlayerMap.ScalingFactor = this.MapView.ScalingFactor;
		}

		private void update_list()
		{
			List<CombatData> list = new List<CombatData>();
			if (Session.Preferences.CreatureAutoRemove)
			{
				foreach (EncounterSlot current in this.fEncounter.AllSlots)
				{
					int hP = current.Card.HP;
					if (hP != 0)
					{
						int arg_51_0 = current.Card.XP;
						List<CombatData> list2 = new List<CombatData>();
						foreach (CombatData current2 in current.CombatData)
						{
							CreatureState state = current.GetState(current2);
							if (state == CreatureState.Defeated)
							{
								list2.Add(current2);
								list.Add(current2);
							}
						}
						foreach (CombatData current3 in list2)
						{
							if (current3 == this.fCurrentActor)
							{
								Guid iD = this.fCurrentActor.ID;
								this.fCurrentActor = this.get_next_actor(this.fCurrentActor);
								if (this.fCurrentActor.ID != iD)
								{
									this.fLog.AddStartTurnEntry(this.fCurrentActor.ID);
									this.update_log();
								}
							}
							CreatureToken token = new CreatureToken(current.ID, current3);
							this.remove_effects(token);
							this.remove_links(token);
							current3.Location = CombatData.NoPoint;
						}
					}
				}
			}
			int num = 0;
			int num2 = 1;
			int index = 2;
			int index2 = 3;
			int num3 = 4;
			int num4 = 5;
			int num5 = 6;
			List<IToken> selectedTokens = this.SelectedTokens;
			Trap selectedTrap = this.SelectedTrap;
			SkillChallenge selectedChallenge = this.SelectedChallenge;
			this.CombatList.BeginUpdate();
			this.CombatList.Items.Clear();
			this.CombatList.SmallImageList = new ImageList();
			this.CombatList.SmallImageList.ImageSize = new Size(16, 16);
			foreach (EncounterSlot current4 in this.fEncounter.AllSlots)
			{
				EncounterWave encounterWave = this.fEncounter.FindWave(current4);
				if (encounterWave == null || encounterWave.Active)
				{
					int hP2 = current4.Card.HP;
					ICreature creature = Session.FindCreature(current4.Card.CreatureID, SearchType.Global);
					foreach (CombatData current5 in current4.CombatData)
					{
						int num6 = hP2 - current5.Damage;
						string text = num6.ToString();
						if (current5.TempHP > 0)
						{
							object obj = text;
							text = string.Concat(new object[]
							{
								obj,
								" (+",
								current5.TempHP,
								")"
							});
						}
						if (num6 != hP2)
						{
							text = text + " / " + hP2;
						}
						string text2 = current5.Initiative.ToString();
						if (current5.Delaying)
						{
							text2 = "(" + text2 + ")";
						}
						ListViewItem listViewItem = this.CombatList.Items.Add(current5.DisplayName);
						listViewItem.Tag = new CreatureToken(current4.ID, current5);
						if (current5.Initiative == -2147483648)
						{
							listViewItem.ForeColor = SystemColors.GrayText;
							text2 = "-";
						}
						int num7 = current4.Card.AC;
						int num8 = current4.Card.Fortitude;
						int num9 = current4.Card.Reflex;
						int num10 = current4.Card.Will;
						foreach (OngoingCondition current6 in current5.Conditions)
						{
							if (current6.Type == OngoingType.DefenceModifier)
							{
								if (current6.Defences.Contains(DefenceType.AC))
								{
									num7 += current6.DefenceMod;
								}
								if (current6.Defences.Contains(DefenceType.Fortitude))
								{
									num8 += current6.DefenceMod;
								}
								if (current6.Defences.Contains(DefenceType.Reflex))
								{
									num9 += current6.DefenceMod;
								}
								if (current6.Defences.Contains(DefenceType.Will))
								{
									num10 += current6.DefenceMod;
								}
							}
						}
						string text3 = string.Concat(new object[]
						{
							"AC ",
							num7,
							", Fort ",
							num8,
							", Ref ",
							num9,
							", Will ",
							num10
						});
						string text4 = this.get_conditions(current5);
						listViewItem.SubItems.Add(text2);
						listViewItem.SubItems.Add(text);
						listViewItem.SubItems.Add(text3);
						listViewItem.SubItems.Add(text4);
						switch (current4.GetState(current5))
						{
						case CreatureState.Bloodied:
							listViewItem.ForeColor = Color.Maroon;
							break;
						case CreatureState.Defeated:
							listViewItem.ForeColor = SystemColors.GrayText;
							break;
						}
						if (!current5.Visible)
						{
							listViewItem.ForeColor = Color.FromArgb(80, listViewItem.ForeColor);
							ListViewItem expr_53E = listViewItem;
							expr_53E.Text += " (hidden)";
						}
						if (creature != null && creature.Image != null)
						{
							this.CombatList.SmallImageList.Images.Add(new Bitmap(creature.Image, 16, 16));
							listViewItem.ImageIndex = this.CombatList.SmallImageList.Images.Count - 1;
						}
						else
						{
							this.add_icon(listViewItem, listViewItem.ForeColor);
						}
						if (current5.Conditions.Count != 0)
						{
							this.add_condition_hint(listViewItem);
						}
						int index3 = num;
						if (current5.Initiative == -2147483648)
						{
							index3 = num4;
						}
						if (current5.Delaying)
						{
							index3 = num2;
						}
						if (this.MapView.Map != null && current5.Location == CombatData.NoPoint)
						{
							index3 = num4;
						}
						if (current4.GetState(current5) == CreatureState.Defeated)
						{
							index3 = num5;
						}
						listViewItem.Group = this.CombatList.Groups[index3];
						if (current5 == this.fCurrentActor)
						{
							listViewItem.Font = new Font(listViewItem.Font, listViewItem.Font.Style | FontStyle.Bold);
							listViewItem.UseItemStyleForSubItems = false;
							listViewItem.BackColor = Color.LightBlue;
							this.add_initiative_hint(listViewItem);
						}
						foreach (IToken current7 in selectedTokens)
						{
							CreatureToken creatureToken = current7 as CreatureToken;
							if (creatureToken != null && creatureToken.Data == current5)
							{
								listViewItem.Selected = true;
							}
						}
					}
				}
			}
			foreach (Trap current8 in this.fEncounter.Traps)
			{
				ListViewItem listViewItem2 = this.CombatList.Items.Add(current8.Name);
				listViewItem2.Tag = current8;
				this.add_icon(listViewItem2, Color.White);
				if (current8.Initiative != -2147483648)
				{
					CombatData combatData = this.fTrapData[current8.ID];
					if (combatData != null && combatData.Initiative != -2147483648)
					{
						string text5 = combatData.Initiative.ToString();
						listViewItem2.SubItems.Add(text5);
						listViewItem2.Group = this.CombatList.Groups[num];
					}
					else
					{
						listViewItem2.SubItems.Add("-");
						listViewItem2.Group = this.CombatList.Groups[num4];
					}
					if (combatData == this.fCurrentActor)
					{
						listViewItem2.Font = new Font(listViewItem2.Font, listViewItem2.Font.Style | FontStyle.Bold);
						listViewItem2.UseItemStyleForSubItems = false;
						listViewItem2.BackColor = Color.LightBlue;
						this.add_initiative_hint(listViewItem2);
					}
				}
				else
				{
					listViewItem2.SubItems.Add("-");
					listViewItem2.Group = this.CombatList.Groups[index];
				}
				listViewItem2.SubItems.Add("-");
				listViewItem2.SubItems.Add("-");
				listViewItem2.SubItems.Add("-");
				if (current8 == selectedTrap)
				{
					listViewItem2.Selected = true;
				}
			}
			foreach (SkillChallenge current9 in this.fEncounter.SkillChallenges)
			{
				ListViewItem listViewItem3 = this.CombatList.Items.Add(current9.Name);
				listViewItem3.SubItems.Add("-");
				listViewItem3.SubItems.Add("-");
				listViewItem3.SubItems.Add("-");
				listViewItem3.SubItems.Add(string.Concat(new object[]
				{
					current9.Results.Successes,
					" / ",
					current9.Successes,
					" successes; ",
					current9.Results.Fails,
					" / 3 failures"
				}));
				this.add_icon(listViewItem3, Color.White);
				listViewItem3.Tag = current9;
				listViewItem3.Group = this.CombatList.Groups[index2];
				if (current9 == selectedChallenge)
				{
					listViewItem3.Selected = true;
				}
			}
			foreach (Hero current10 in Session.Project.Heroes)
			{
				int index4 = num;
				ListViewItem listViewItem4 = this.CombatList.Items.Add(current10.Name);
				listViewItem4.Tag = current10;
				CombatData combatData2 = current10.CombatData;
				switch (current10.GetState(combatData2.Damage))
				{
				case CreatureState.Active:
					listViewItem4.ForeColor = SystemColors.WindowText;
					break;
				case CreatureState.Bloodied:
					listViewItem4.ForeColor = Color.Maroon;
					break;
				case CreatureState.Defeated:
					listViewItem4.ForeColor = SystemColors.GrayText;
					break;
				}
				if (current10.Portrait != null)
				{
					this.CombatList.SmallImageList.Images.Add(new Bitmap(current10.Portrait, 16, 16));
					listViewItem4.ImageIndex = this.CombatList.SmallImageList.Images.Count - 1;
				}
				else if (current10.Key != "")
				{
					this.CombatList.SmallImageList.Images.Add(new Bitmap(Resources.Purpled20, 16, 16));
					listViewItem4.ImageIndex = this.CombatList.SmallImageList.Images.Count - 1;
				}
				else
				{
					this.add_icon(listViewItem4, Color.Green);
				}
				if (combatData2.Conditions.Count != 0)
				{
					this.add_condition_hint(listViewItem4);
				}
				int initiative = combatData2.Initiative;
				string text6;
				if (initiative == -2147483648)
				{
					listViewItem4.ForeColor = SystemColors.GrayText;
					index4 = num4;
					text6 = "-";
				}
				else
				{
					text6 = initiative.ToString();
					if (combatData2.Delaying)
					{
						text6 = "(" + text6 + ")";
						index4 = num2;
					}
					if (combatData2 == this.fCurrentActor)
					{
						listViewItem4.Font = new Font(listViewItem4.Font, listViewItem4.Font.Style | FontStyle.Bold);
						listViewItem4.UseItemStyleForSubItems = false;
						listViewItem4.BackColor = Color.LightBlue;
						this.add_initiative_hint(listViewItem4);
					}
				}
				string text7;
				if (current10.HP != 0)
				{
					int num11 = current10.HP - combatData2.Damage;
					text7 = num11.ToString();
					if (combatData2.TempHP > 0)
					{
						object obj = text7;
						text7 = string.Concat(new object[]
						{
							obj,
							" (+",
							combatData2.TempHP,
							")"
						});
					}
					if (num11 != current10.HP)
					{
						text7 = text7 + " / " + current10.HP;
					}
				}
				else
				{
					text7 = "-";
				}
				listViewItem4.SubItems.Add(text6);
				listViewItem4.SubItems.Add(text7);
				if (current10.AC != 0 && current10.Fortitude != 0 && current10.Reflex != 0 && current10.Will != 0)
				{
					int num12 = current10.AC;
					int num13 = current10.Fortitude;
					int num14 = current10.Reflex;
					int num15 = current10.Will;
					foreach (OngoingCondition current11 in combatData2.Conditions)
					{
						if (current11.Type == OngoingType.DefenceModifier)
						{
							if (current11.Defences.Contains(DefenceType.AC))
							{
								num12 += current11.DefenceMod;
							}
							if (current11.Defences.Contains(DefenceType.Fortitude))
							{
								num13 += current11.DefenceMod;
							}
							if (current11.Defences.Contains(DefenceType.Reflex))
							{
								num14 += current11.DefenceMod;
							}
							if (current11.Defences.Contains(DefenceType.Will))
							{
								num15 += current11.DefenceMod;
							}
						}
					}
					string text8 = string.Concat(new object[]
					{
						"AC ",
						num12,
						", Fort ",
						num13,
						", Ref ",
						num14,
						", Will ",
						num15
					});
					listViewItem4.SubItems.Add(text8);
				}
				else
				{
					listViewItem4.SubItems.Add("-");
				}
				listViewItem4.SubItems.Add(this.get_conditions(combatData2));
				if (this.MapView.Map != null && current10.CombatData.Location == CombatData.NoPoint)
				{
					index4 = num4;
				}
				listViewItem4.Group = this.CombatList.Groups[index4];
				if (selectedTokens.Contains(current10))
				{
					listViewItem4.Selected = true;
				}
			}
			foreach (CustomToken current12 in this.fEncounter.CustomTokens)
			{
				ListViewItem listViewItem5 = this.CombatList.Items.Add(current12.Name);
				listViewItem5.Tag = current12;
				this.add_icon(listViewItem5, Color.Blue);
				int index5 = num3;
				if (this.MapView.Map != null && current12.Data.Location == CombatData.NoPoint && current12.CreatureID == Guid.Empty)
				{
					index5 = num4;
					listViewItem5.ForeColor = SystemColors.GrayText;
				}
				listViewItem5.Group = this.CombatList.Groups[index5];
				if (selectedTokens.Contains(current12))
				{
					listViewItem5.Selected = true;
				}
			}
			this.CombatList.Sort();
			this.CombatList.EndUpdate();
			if (this.PlayerInitiative != null)
			{
				this.PlayerInitiative.DocumentText = this.InitiativeView();
			}
		}

		private string get_conditions(CombatData cd)
		{
			string text = "";
			bool flag = false;
			foreach (OngoingCondition current in cd.Conditions)
			{
				if (current.Type == OngoingType.Damage)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				if (text != "")
				{
					text += "; ";
				}
				text += "Damage";
			}
			foreach (OngoingCondition current2 in cd.Conditions)
			{
				if (current2.Type != OngoingType.Damage)
				{
					if (text != "")
					{
						text += "; ";
					}
					switch (current2.Type)
					{
					case OngoingType.Condition:
						text += current2.Data;
						break;
					case OngoingType.DefenceModifier:
						text += current2.ToString(this.fEncounter, false);
						break;
					}
				}
			}
			return text;
		}

		private void add_icon(ListViewItem lvi, Color c)
		{
			Image image = new Bitmap(16, 16);
			Graphics graphics = Graphics.FromImage(image);
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.FillEllipse(new SolidBrush(c), 2, 2, 12, 12);
			if (c == Color.White)
			{
				graphics.DrawEllipse(Pens.Black, 2, 2, 12, 12);
			}
			this.CombatList.SmallImageList.Images.Add(image);
			lvi.ImageIndex = this.CombatList.SmallImageList.Images.Count - 1;
		}

		private void add_condition_hint(ListViewItem lvi)
		{
			if (lvi.ImageIndex == -1)
			{
				return;
			}
			Image image = this.CombatList.SmallImageList.Images[lvi.ImageIndex];
			Graphics graphics = Graphics.FromImage(image);
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.FillEllipse(Brushes.White, 5, 5, 6, 6);
			graphics.DrawEllipse(Pens.DarkGray, 5, 5, 6, 6);
			this.CombatList.SmallImageList.Images[lvi.ImageIndex] = image;
		}

		private void add_initiative_hint(ListViewItem lvi)
		{
			if (lvi.ImageIndex == -1)
			{
				return;
			}
			Image image = this.CombatList.SmallImageList.Images[lvi.ImageIndex];
			Graphics graphics = Graphics.FromImage(image);
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			Pen pen = new Pen(Color.Blue, 3f);
			graphics.DrawRectangle(pen, 0, 0, 16, 16);
			this.CombatList.SmallImageList.Images[lvi.ImageIndex] = image;
		}

		private void update_log()
		{
			string text = this.EncounterLogView(false);
			this.LogBrowser.Document.OpenNew(true);
			this.LogBrowser.Document.Write(text);
		}

		private void update_preview_panel()
		{
			if (Session.Preferences.iPlay4E && this.SelectedTokens.Count == 1)
			{
				Hero hero = this.SelectedTokens[0] as Hero;
				if (hero != null && hero.Key != "")
				{
					this.Preview.Document.OpenNew(true);
					this.Preview.Document.Write(HTML.Text("Loading iPlay4e character, please wait...", true, true, DisplaySize.Small));
					string urlString = "http://iplay4e.appspot.com/view?xsl=jPint&key=" + hero.Key;
					this.Preview.Navigate(urlString);
					return;
				}
			}
			string text = "";
			text += "<HTML>";
			text += HTML.Concatenate(HTML.GetHead("", "", DisplaySize.Small));
			text += "<BODY>";
			if (this.fCombatStarted)
			{
				List<IToken> selectedTokens = this.SelectedTokens;
				if (this.TwoColumnPreview)
				{
					List<IToken> list = new List<IToken>();
					foreach (IToken current in selectedTokens)
					{
						CreatureToken creatureToken = current as CreatureToken;
						if (creatureToken != null && creatureToken.Data.ID == this.fCurrentActor.ID)
						{
							list.Add(current);
						}
						Hero hero2 = current as Hero;
						if (hero2 != null && hero2.ID == this.fCurrentActor.ID)
						{
							list.Add(current);
						}
					}
					foreach (IToken current2 in list)
					{
						selectedTokens.Remove(current2);
					}
				}
				if (this.TwoColumnPreview)
				{
					text += "<P class=table>";
					text += "<TABLE class=clear>";
					text += "<TR class=clear>";
					text += "<TD class=clear>";
					EncounterSlot encounterSlot = this.fEncounter.FindSlot(this.fCurrentActor);
					if (encounterSlot != null)
					{
						text += HTML.StatBlock(encounterSlot.Card, this.fCurrentActor, this.fEncounter, false, true, true, CardMode.Combat, DisplaySize.Small);
					}
					Hero hero3 = Session.Project.FindHero(this.fCurrentActor.ID);
					if (hero3 != null)
					{
						bool show_effects = selectedTokens.Count != 0;
						text += HTML.StatBlock(hero3, this.fEncounter, false, true, show_effects, DisplaySize.Small);
					}
					Trap trap = this.fEncounter.FindTrap(this.fCurrentActor.ID);
					if (trap != null)
					{
						text += HTML.Trap(trap, this.fCurrentActor, false, true, false, DisplaySize.Small);
					}
					text += "</TD>";
					text += "<TD class=clear>";
				}
				string text2 = "";
				if (selectedTokens.Count != 0)
				{
					text2 = this.html_tokens(selectedTokens);
				}
				else if (this.SelectedTrap != null)
				{
					text2 = this.html_trap();
				}
				else if (this.SelectedChallenge != null)
				{
					text2 = this.html_skill_challenge();
				}
				if (text2 != "")
				{
					text += text2;
				}
				else
				{
					text += this.html_encounter_overview();
				}
				if (this.TwoColumnPreview)
				{
					text += "</TD>";
					text += "</TR>";
					text += "</TABLE>";
					text += "</P>";
				}
			}
			else
			{
				text += this.html_encounter_start();
			}
			text += "</BODY>";
			text += "</HTML>";
			this.Preview.Document.OpenNew(true);
			this.Preview.Document.Write(text);
		}

		private void update_maps()
		{
			this.MapView.Invalidate();
			if (this.PlayerMap != null)
			{
				this.PlayerMap.Invalidate();
			}
		}

		private void update_statusbar()
		{
			int num = this.fEncounter.GetXP() + this.fRemovedCreatureXP;
			this.XPLbl.Text = num + " XP";
			int num2 = 0;
			foreach (Hero current in Session.Project.Heroes)
			{
				if (current.CombatData.Initiative != -2147483648)
				{
					num2++;
				}
			}
			if (num2 > 1)
			{
				ToolStripStatusLabel expr_84 = this.XPLbl;
				object text = expr_84.Text;
				expr_84.Text = string.Concat(new object[]
				{
					text,
					" (",
					num / num2,
					" XP each)"
				});
			}
			int level = this.fEncounter.GetLevel(num2);
			this.LevelLbl.Text = ((level != -1) ? ("Encounter Level: " + level) : "");
		}

		private void add_quick_effect(OngoingCondition effect)
		{
			string b = effect.ToString(this.fEncounter, false);
			foreach (OngoingCondition current in this.fEffects)
			{
				if (current.ToString(this.fEncounter, false) == b)
				{
					return;
				}
			}
			this.fEffects.Add(effect.Copy());
			this.fEffects.Sort();
		}

		private void update_effects_list(ToolStripDropDownItem tsddi, bool use_list_selection)
		{
			tsddi.DropDownItems.Clear();
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Standard Conditions");
			tsddi.DropDownItems.Add(toolStripMenuItem);
			foreach (string current in Conditions.GetConditions())
			{
				OngoingCondition ongoingCondition = new OngoingCondition();
				ongoingCondition.Data = current;
				ongoingCondition.Duration = DurationType.Encounter;
				ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(ongoingCondition.ToString(this.fEncounter, false));
				toolStripMenuItem2.Tag = ongoingCondition;
				if (use_list_selection)
				{
					toolStripMenuItem2.Click += new EventHandler(this.apply_quick_effect_from_toolbar);
				}
				else
				{
					toolStripMenuItem2.Click += new EventHandler(this.apply_quick_effect_from_map);
				}
				toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
			}
			tsddi.DropDownItems.Add(new ToolStripSeparator());
			bool flag = false;
			foreach (Hero current2 in Session.Project.Heroes)
			{
				if (current2.Effects.Count != 0)
				{
					ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem(current2.Name);
					tsddi.DropDownItems.Add(toolStripMenuItem3);
					foreach (OngoingCondition current3 in current2.Effects)
					{
						ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem(current3.ToString(this.fEncounter, false));
						toolStripMenuItem4.Tag = current3.Copy();
						if (use_list_selection)
						{
							toolStripMenuItem4.Click += new EventHandler(this.apply_quick_effect_from_toolbar);
						}
						else
						{
							toolStripMenuItem4.Click += new EventHandler(this.apply_quick_effect_from_map);
						}
						toolStripMenuItem3.DropDownItems.Add(toolStripMenuItem4);
						flag = true;
					}
				}
			}
			if (flag)
			{
				tsddi.DropDownItems.Add(new ToolStripSeparator());
			}
			foreach (OngoingCondition current4 in this.fEffects)
			{
				ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem(current4.ToString(this.fEncounter, false));
				toolStripMenuItem5.Tag = current4.Copy();
				if (use_list_selection)
				{
					toolStripMenuItem5.Click += new EventHandler(this.apply_quick_effect_from_toolbar);
				}
				else
				{
					toolStripMenuItem5.Click += new EventHandler(this.apply_quick_effect_from_map);
				}
				tsddi.DropDownItems.Add(toolStripMenuItem5);
			}
			if (this.fEffects.Count != 0)
			{
				tsddi.DropDownItems.Add(new ToolStripSeparator());
			}
			ToolStripMenuItem toolStripMenuItem6 = new ToolStripMenuItem("Add a New Effect...");
			if (use_list_selection)
			{
				toolStripMenuItem6.Click += new EventHandler(this.apply_effect_from_toolbar);
			}
			else
			{
				toolStripMenuItem6.Click += new EventHandler(this.apply_effect_from_map);
			}
			tsddi.DropDownItems.Add(toolStripMenuItem6);
		}

		private void update_remove_effect_list(ToolStripDropDownItem tsddi, bool use_list_selection)
		{
			tsddi.DropDownItems.Clear();
			List<IToken> list = use_list_selection ? this.SelectedTokens : this.MapView.SelectedTokens;
			if (list.Count != 1)
			{
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("(multiple selection)");
				toolStripMenuItem.Enabled = false;
				tsddi.DropDownItems.Add(toolStripMenuItem);
				return;
			}
			CombatData combatData = null;
			CreatureToken creatureToken = list[0] as CreatureToken;
			if (creatureToken != null)
			{
				combatData = creatureToken.Data;
			}
			Hero hero = list[0] as Hero;
			if (hero != null)
			{
				combatData = hero.CombatData;
			}
			if (combatData != null)
			{
				foreach (OngoingCondition current in combatData.Conditions)
				{
					ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(current.ToString(this.fEncounter, false));
					toolStripMenuItem2.Tag = current;
					if (use_list_selection)
					{
						toolStripMenuItem2.Click += new EventHandler(this.remove_effect_from_list);
					}
					else
					{
						toolStripMenuItem2.Click += new EventHandler(this.remove_effect_from_map);
					}
					tsddi.DropDownItems.Add(toolStripMenuItem2);
				}
			}
			if (tsddi.DropDownItems.Count == 0)
			{
				ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem("(no effects)");
				toolStripMenuItem3.Enabled = false;
				tsddi.DropDownItems.Add(toolStripMenuItem3);
			}
		}

		private void apply_quick_effect_from_toolbar(object sender, EventArgs e)
		{
			ToolStripItem toolStripItem = sender as ToolStripItem;
			OngoingCondition ongoingCondition = toolStripItem.Tag as OngoingCondition;
			if (ongoingCondition == null)
			{
				return;
			}
			this.apply_effect(ongoingCondition.Copy(), this.SelectedTokens, false);
		}

		private void apply_quick_effect_from_map(object sender, EventArgs e)
		{
			ToolStripItem toolStripItem = sender as ToolStripItem;
			OngoingCondition ongoingCondition = toolStripItem.Tag as OngoingCondition;
			if (ongoingCondition == null)
			{
				return;
			}
			this.apply_effect(ongoingCondition.Copy(), this.MapView.SelectedTokens, false);
		}

		private void apply_effect_from_toolbar(object sender, EventArgs e)
		{
			OngoingCondition condition = new OngoingCondition();
			EffectForm effectForm = new EffectForm(condition, this.fEncounter, this.fCurrentActor, this.fCurrentRound);
			if (effectForm.ShowDialog() == DialogResult.OK)
			{
				this.apply_effect(effectForm.Effect, this.SelectedTokens, true);
			}
		}

		private void apply_effect_from_map(object sender, EventArgs e)
		{
			OngoingCondition condition = new OngoingCondition();
			EffectForm effectForm = new EffectForm(condition, this.fEncounter, this.fCurrentActor, this.fCurrentRound);
			if (effectForm.ShowDialog() == DialogResult.OK)
			{
				this.apply_effect(effectForm.Effect, this.MapView.SelectedTokens, true);
			}
		}

		private void apply_effect(OngoingCondition oc, List<IToken> tokens, bool add_to_quick_list)
		{
			try
			{
				if (oc.Duration == DurationType.BeginningOfTurn || oc.Duration == DurationType.EndOfTurn)
				{
					if (oc.DurationCreatureID == Guid.Empty)
					{
						CombatantSelectForm combatantSelectForm = new CombatantSelectForm(this.fEncounter, this.fTrapData);
						if (combatantSelectForm.ShowDialog() == DialogResult.OK)
						{
							if (combatantSelectForm.SelectedCombatant == null)
							{
								return;
							}
							oc.DurationCreatureID = combatantSelectForm.SelectedCombatant.ID;
						}
					}
					oc.DurationRound = this.fCurrentRound;
					if (this.fCurrentActor != null && oc.DurationCreatureID == this.fCurrentActor.ID)
					{
						oc.DurationRound++;
					}
				}
				foreach (IToken current in tokens)
				{
					CreatureToken creatureToken = current as CreatureToken;
					if (creatureToken != null)
					{
						CombatData data = creatureToken.Data;
						data.Conditions.Add(oc.Copy());
						this.fLog.AddEffectEntry(data.ID, oc.ToString(this.fEncounter, false), true);
					}
					Hero hero = current as Hero;
					if (hero != null)
					{
						CombatData combatData = hero.CombatData;
						combatData.Conditions.Add(oc.Copy());
						this.fLog.AddEffectEntry(combatData.ID, oc.ToString(this.fEncounter, false), true);
					}
				}
				if (add_to_quick_list)
				{
					bool flag = false;
					OngoingCondition ongoingCondition = oc.Copy();
					if (Session.Project.Heroes.Count != 0)
					{
						Hero selected = Session.Project.FindHero(this.fCurrentActor.ID);
						HeroSelectForm heroSelectForm = new HeroSelectForm(selected);
						if (heroSelectForm.ShowDialog() == DialogResult.OK && heroSelectForm.SelectedHero != null)
						{
							if (ongoingCondition.DurationCreatureID != heroSelectForm.SelectedHero.ID)
							{
								ongoingCondition.DurationCreatureID = Guid.Empty;
							}
							heroSelectForm.SelectedHero.Effects.Add(ongoingCondition);
							Session.Modified = true;
							flag = true;
						}
					}
					if (!flag)
					{
						this.add_quick_effect(ongoingCondition);
					}
				}
				this.update_list();
				this.update_log();
				this.update_preview_panel();
				this.MapView.MapChanged();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void remove_effect_from_list(object sender, EventArgs e)
		{
			ToolStripItem toolStripItem = sender as ToolStripItem;
			OngoingCondition ongoingCondition = toolStripItem.Tag as OngoingCondition;
			if (ongoingCondition == null)
			{
				return;
			}
			if (this.SelectedTokens.Count != 1)
			{
				return;
			}
			CombatData combatData = null;
			CreatureToken creatureToken = this.SelectedTokens[0] as CreatureToken;
			if (creatureToken != null)
			{
				combatData = creatureToken.Data;
			}
			Hero hero = this.SelectedTokens[0] as Hero;
			if (hero != null)
			{
				combatData = hero.CombatData;
			}
			if (combatData == null)
			{
				return;
			}
			combatData.Conditions.Remove(ongoingCondition);
			this.fLog.AddEffectEntry(combatData.ID, ongoingCondition.ToString(this.fEncounter, false), false);
			this.update_list();
			this.update_log();
			this.update_preview_panel();
		}

		private void remove_effect_from_map(object sender, EventArgs e)
		{
			ToolStripItem toolStripItem = sender as ToolStripItem;
			OngoingCondition ongoingCondition = toolStripItem.Tag as OngoingCondition;
			if (ongoingCondition == null)
			{
				return;
			}
			if (this.MapView.SelectedTokens.Count != 1)
			{
				return;
			}
			CombatData combatData = null;
			CreatureToken creatureToken = this.MapView.SelectedTokens[0] as CreatureToken;
			if (creatureToken != null)
			{
				combatData = creatureToken.Data;
			}
			Hero hero = this.MapView.SelectedTokens[0] as Hero;
			if (hero != null)
			{
				combatData = hero.CombatData;
			}
			if (combatData == null)
			{
				return;
			}
			combatData.Conditions.Remove(ongoingCondition);
			this.fLog.AddEffectEntry(combatData.ID, ongoingCondition.ToString(this.fEncounter, false), false);
			this.update_list();
			this.update_log();
			this.update_preview_panel();
		}

		private string html_tokens(List<IToken> tokens)
		{
			string result;
			if (tokens.Count == 1)
			{
				IToken token = tokens[0];
				result = this.html_token(token, true);
			}
			else
			{
				List<string> list = new List<string>();
				foreach (IToken current in tokens)
				{
					list.Add(this.html_token(current, false));
				}
				result = HTML.Concatenate(list);
			}
			return result;
		}

		private string html_token(IToken token, bool full)
		{
			string result = "";
			if (token is Hero)
			{
				Hero hero = token as Hero;
				CombatData combatData = hero.CombatData;
				if (this.TwoColumnPreview && combatData == this.fCurrentActor)
				{
					result = "";
				}
				else
				{
					result = HTML.StatBlock(hero, this.fEncounter, false, false, false, DisplaySize.Small);
				}
			}
			if (token is CreatureToken)
			{
				CreatureToken creatureToken = token as CreatureToken;
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
				CombatData data = creatureToken.Data;
				if (this.TwoColumnPreview && data == this.fCurrentActor)
				{
					result = "";
				}
				else
				{
					result = HTML.StatBlock(encounterSlot.Card, creatureToken.Data, this.fEncounter, false, false, full, CardMode.Combat, DisplaySize.Small);
				}
			}
			if (token is CustomToken)
			{
				CustomToken customToken = token as CustomToken;
				bool drag = this.fEncounter.MapID != Guid.Empty && customToken.Data.Location == CombatData.NoPoint;
				result = HTML.CustomMapToken(customToken, drag, false, DisplaySize.Small);
			}
			return result;
		}

		private string html_trap()
		{
			CombatData combatData = null;
			if (this.fTrapData.ContainsKey(this.SelectedTrap.ID))
			{
				combatData = this.fTrapData[this.SelectedTrap.ID];
				if (this.TwoColumnPreview && combatData == this.fCurrentActor)
				{
					return "";
				}
			}
			return HTML.Trap(this.SelectedTrap, combatData, false, false, false, DisplaySize.Small);
		}

		private string html_skill_challenge()
		{
			return HTML.SkillChallenge(this.SelectedChallenge, true, false, DisplaySize.Small);
		}

		private string html_encounter_start()
		{
			List<string> list = new List<string>();
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			list.Add("<TR class=heading>");
			list.Add("<TD>");
			list.Add("<B>Starting the Encounter</B>");
			list.Add("</TD>");
			list.Add("</TR>");
			string text = "automatically";
			string text2 = "manually";
			string text3 = "individually";
			string text4 = "in groups";
			string text5 = "calculated automatically";
			string text6 = "entered manually";
			string str = " (grouped by type)";
			list.Add("<TR class=shaded>");
			list.Add("<TD>");
			list.Add("<B>How do you want to roll initiative?</B>");
			list.Add("</TD>");
			list.Add("</TR>");
			if (Session.Project.Heroes.Count != 0)
			{
				string str2 = "";
				string text7 = text;
				string text8 = text2;
				switch (Session.Preferences.HeroInitiativeMode)
				{
				case InitiativeMode.AutoGroup:
				case InitiativeMode.AutoIndividual:
					str2 = text5;
					text8 = "<A href=heroinit:manual>" + text8 + "</A>";
					break;
				case InitiativeMode.ManualIndividual:
				case InitiativeMode.ManualGroup:
					str2 = text6;
					text7 = "<A href=heroinit:auto>" + text7 + "</A>";
					break;
				}
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("For <B>PCs</B>: " + str2);
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD class=indent>");
				list.Add(text7 + " / " + text8);
				list.Add("</TD>");
				list.Add("</TR>");
			}
			if (this.fEncounter.Count != 0)
			{
				string str3 = "";
				string text9 = text;
				string text10 = text2;
				string text11 = text3;
				string text12 = text4;
				switch (Session.Preferences.InitiativeMode)
				{
				case InitiativeMode.AutoGroup:
					str3 = text5 + str;
					text10 = "<A href=creatureinit:manual>" + text10 + "</A>";
					text11 = "<A href=creatureinit:individual>" + text11 + "</A>";
					break;
				case InitiativeMode.AutoIndividual:
					str3 = text5;
					text10 = "<A href=creatureinit:manual>" + text10 + "</A>";
					text12 = "<A href=creatureinit:group>" + text12 + "</A>";
					break;
				case InitiativeMode.ManualIndividual:
					str3 = text6;
					text9 = "<A href=creatureinit:auto>" + text9 + "</A>";
					text12 = "<A href=creatureinit:group>" + text12 + "</A>";
					break;
				case InitiativeMode.ManualGroup:
					str3 = text6 + str;
					text9 = "<A href=creatureinit:auto>" + text9 + "</A>";
					text11 = "<A href=creatureinit:individual>" + text11 + "</A>";
					break;
				}
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("For <B>creatures</B>: " + str3);
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD class=indent>");
				list.Add(text9 + " / " + text10);
				list.Add("</TD>");
				list.Add("</TR>");
				bool flag = false;
				foreach (EncounterSlot current in this.fEncounter.AllSlots)
				{
					if (current.CombatData.Count > 1)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					list.Add("<TR>");
					list.Add("<TD class=indent>");
					list.Add(text11 + " / " + text12);
					list.Add("</TD>");
					list.Add("</TR>");
				}
			}
			bool flag2 = false;
			foreach (Trap current2 in this.fEncounter.Traps)
			{
				if (current2.Initiative != -2147483648)
				{
					flag2 = true;
					break;
				}
			}
			if (flag2)
			{
				string str4 = "";
				string text13 = text;
				string text14 = text2;
				switch (Session.Preferences.TrapInitiativeMode)
				{
				case InitiativeMode.AutoGroup:
				case InitiativeMode.AutoIndividual:
					str4 = text5;
					text14 = "<A href=trapinit:manual>" + text14 + "</A>";
					break;
				case InitiativeMode.ManualIndividual:
				case InitiativeMode.ManualGroup:
					str4 = text6;
					text13 = "<A href=trapinit:auto>" + text13 + "</A>";
					break;
				}
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("For <B>traps</B>: " + str4);
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD class=indent>");
				list.Add(text13 + " / " + text14);
				list.Add("</TD>");
				list.Add("</TR>");
			}
			list.Add("<TR class=shaded>");
			list.Add("<TD>");
			list.Add("<B>Preparing for the encounter</B>");
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD>");
			list.Add("<A href=combat:hp>Update PC hit points</A>");
			list.Add("- if they've healed or taken damage since their last encounter");
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD>");
			list.Add("<A href=combat:rename>Rename combatants</A>");
			list.Add("- if you need to indicate which mini is which creature");
			list.Add("</TD>");
			list.Add("</TR>");
			if (this.fEncounter.MapID != Guid.Empty)
			{
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("Place PCs on the map - drag PCs from the list into their starting positions on the map");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			bool flag3 = false;
			foreach (Hero current3 in Session.Project.Heroes)
			{
				if (current3.Key != null && current3.Key != "")
				{
					flag3 = true;
					break;
				}
			}
			if (flag3)
			{
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<A href=combat:sync>Update iPlay4E characters</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			list.Add("<TR class=shaded>");
			list.Add("<TD>");
			list.Add("<B>Everything ready?</B>");
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD>");
			list.Add("<A href=combat:start>Click here to roll initiative and start the encounter!</A>");
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("</TABLE>");
			list.Add("</P>");
			return HTML.Concatenate(list);
		}

		private string html_encounter_overview()
		{
			List<string> list = new List<string>();
			list.Add("<P class=instruction>Select a combatant from the list to see its stat block here.</P>");
			list.Add("<P class=instruction></P>");
			List<EncounterCard> list2 = new List<EncounterCard>();
			List<EncounterCard> list3 = new List<EncounterCard>();
			List<EncounterCard> list4 = new List<EncounterCard>();
			foreach (EncounterSlot current in this.fEncounter.AllSlots)
			{
				if (current.Card.Auras.Count != 0)
				{
					list2.Add(current.Card);
				}
				if (current.Card.Tactics != "")
				{
					list3.Add(current.Card);
				}
				bool flag = false;
				List<CreaturePower> creaturePowers = current.Card.CreaturePowers;
				foreach (CreaturePower current2 in creaturePowers)
				{
					if (current2.Action != null && current2.Action.Trigger != "")
					{
						flag = true;
					}
				}
				if (flag)
				{
					list4.Add(current.Card);
				}
			}
			if (list2.Count != 0 || list3.Count != 0 || list4.Count != 0)
			{
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=heading>");
				list.Add("<TD>");
				list.Add("<B>Remember</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				if (list2.Count != 0)
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD>");
					list.Add("<B>Auras</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					foreach (EncounterCard current3 in list2)
					{
						list.Add("<TR>");
						list.Add("<TD>");
						list.Add("<B>" + current3.Title + "</B>");
						list.Add("</TD>");
						list.Add("</TR>");
						foreach (Aura current4 in current3.Auras)
						{
							list.Add("<TR>");
							list.Add("<TD class=indent>");
							list.Add("<B>" + current4.Name + "</B>: " + current4.Details);
							list.Add("</TD>");
							list.Add("</TR>");
						}
					}
				}
				if (list3.Count != 0)
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD>");
					list.Add("<B>Tactics</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					foreach (EncounterCard current5 in list3)
					{
						list.Add("<TR>");
						list.Add("<TD class=indent>");
						list.Add("<B>" + current5.Title + "</B>: " + current5.Tactics);
						list.Add("</TD>");
						list.Add("</TR>");
					}
				}
				if (list4.Count != 0)
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD>");
					list.Add("<B>Triggered Powers</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					foreach (EncounterCard current6 in list4)
					{
						list.Add("<TR>");
						list.Add("<TD>");
						list.Add("<B>" + current6.Title + "</B>:");
						list.Add("</TD>");
						list.Add("</TR>");
						List<CreaturePower> creaturePowers2 = current6.CreaturePowers;
						foreach (CreaturePower current7 in creaturePowers2)
						{
							if (current7.Action != null && !(current7.Action.Trigger == ""))
							{
								list.Add("<TR>");
								list.Add("<TD class=indent>");
								list.Add("<B>" + current7.Name + "</B>: " + current7.Action.Trigger);
								list.Add("</TD>");
								list.Add("</TR>");
							}
						}
					}
				}
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			if (this.fEncounter.MapAreaID != Guid.Empty)
			{
				MapArea mapArea = this.MapView.Map.FindArea(this.fEncounter.MapAreaID);
				if (mapArea != null && mapArea.Details != "")
				{
					list.Add("<P class=encounter_note><B>" + HTML.Process(mapArea.Name, true) + "</B>:</P>");
					list.Add("<P class=encounter_note>" + HTML.Process(mapArea.Details, true) + "</P>");
				}
			}
			foreach (EncounterNote current8 in this.fEncounter.Notes)
			{
				if (!(current8.Contents == ""))
				{
					list.Add("<P class=encounter_note><B>" + HTML.Process(current8.Title, true) + "</B>:</P>");
					list.Add("<P class=encounter_note>" + HTML.Process(current8.Contents, false) + "</P>");
				}
			}
			return HTML.Concatenate(list);
		}

		public string InitiativeView()
		{
			List<ListViewItem> list = new List<ListViewItem>();
			foreach (ListViewItem item in this.CombatList.Groups[0].Items)
			{
				list.Add(item);
			}
			list.Sort(this.CombatList.ListViewItemSorter as IComparer<ListViewItem>);
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>();
			bool flag = false;
			list2.AddRange(HTML.GetHead(null, null, PlayerViewForm.DisplaySize));
			list2.Add("<BODY bgcolor=black>");
			list2.Add("<P class=table>");
			list2.Add("<TABLE class=initiative>");
			foreach (ListViewItem current in list)
			{
				CombatData combatData = null;
				string text = "";
				if (current.Tag is CreatureToken)
				{
					CreatureToken creatureToken = current.Tag as CreatureToken;
					combatData = creatureToken.Data;
					text = combatData.DisplayName;
					if (!Session.Preferences.PlayerViewCreatureLabels)
					{
						EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
						ICreature creature = Session.FindCreature(encounterSlot.Card.CreatureID, SearchType.Global);
						text = creature.Category;
						if (text == "")
						{
							text = "Creature";
						}
					}
				}
				if (current.Tag is Trap)
				{
					Trap trap = current.Tag as Trap;
					if (trap.Initiative != -2147483648)
					{
						combatData = this.fTrapData[trap.ID];
						text = combatData.DisplayName;
						if (!Session.Preferences.PlayerViewCreatureLabels)
						{
							text = trap.Type.ToString();
						}
					}
				}
				if (current.Tag is Hero)
				{
					Hero hero = current.Tag as Hero;
					combatData = hero.CombatData;
					text = hero.Name;
				}
				if (current.Tag is CustomToken)
				{
					CustomToken customToken = current.Tag as CustomToken;
					combatData = customToken.Data;
					text = combatData.DisplayName;
				}
				if (combatData != null && combatData.Visible && combatData.Initiative != -2147483648)
				{
					string text2 = "white";
					if (combatData == this.fCurrentActor)
					{
						flag = true;
						text = "<B>" + text + "</B>";
					}
					EncounterSlot encounterSlot2 = this.fEncounter.FindSlot(combatData);
					if (encounterSlot2 != null)
					{
						switch (encounterSlot2.GetState(combatData))
						{
						case CreatureState.Bloodied:
							text2 = "red";
							break;
						case CreatureState.Defeated:
							text2 = "darkgrey";
							text = "<S>" + text + "</S>";
							break;
						}
					}
					string text3 = string.Concat(new string[]
					{
						"<FONT color=",
						text2,
						">",
						text,
						"</FONT>"
					});
					if (combatData.Conditions.Count != 0)
					{
						string text4 = "";
						foreach (OngoingCondition current2 in combatData.Conditions)
						{
							if (text4 != "")
							{
								text4 += "; ";
							}
							text4 += current2.ToString(this.fEncounter, true);
						}
						text3 = text3 + "<BR><FONT color=grey>" + text4 + "</FONT>";
					}
					List<string> list4 = flag ? list2 : list3;
					list4.Add("<TR>");
					list4.Add("<TD align=center bgcolor=black width=50><FONT color=lightgrey>" + combatData.Initiative + "</FONT></TD>");
					list4.Add("<TD bgcolor=black>" + text3 + "</TD>");
					list4.Add("</TR>");
				}
			}
			list2.AddRange(list3);
			list2.Add("</TABLE>");
			list2.Add("</P>");
			list2.Add("<HR>");
			list2.Add(this.EncounterLogView(true));
			list2.Add("</BODY>");
			list2.Add("</HTML>");
			return HTML.Concatenate(list2);
		}

		public string EncounterLogView(bool player_view)
		{
			List<string> list = new List<string>();
			if (!player_view)
			{
				list.AddRange(HTML.GetHead("Encounter Log", "", DisplaySize.Small));
				list.Add("<BODY>");
			}
			if (this.fLog != null)
			{
				list.Add("<P class=table>");
				list.Add("<TABLE class=wide>");
				list.Add("<TR class=encounterlog>");
				list.Add("<TD colspan=2>");
				list.Add("<B>Encounter Log</B>");
				list.Add("</TD>");
				list.Add("<TD align=right>");
				list.Add("<B>Round " + this.fCurrentRound + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				if (!this.fLog.Active)
				{
					list.Add("<TR class=warning>");
					list.Add("<TD colspan=3>");
					list.Add("The log is not yet active as the encounter has not started.");
					list.Add("</TD>");
					list.Add("</TR>");
				}
				EncounterReport encounterReport = this.fLog.CreateReport(this.fEncounter, !player_view);
				foreach (RoundLog current in encounterReport.Rounds)
				{
					list.Add("<TR class=shaded>");
					if (player_view)
					{
						list.Add("<TD class=pvlogentry colspan=3>");
					}
					else
					{
						list.Add("<TD colspan=3>");
					}
					list.Add("<B>Round " + current.Round + "</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					if (current.Count == 0)
					{
						list.Add("<TR>");
						if (player_view)
						{
							list.Add("<TD class=pvlogentry align=center colspan=3>");
						}
						else
						{
							list.Add("<TD align=center colspan=3>");
						}
						list.Add("(nothing)");
						list.Add("</TD>");
						list.Add("</TR>");
					}
					bool detailed = !player_view || Session.Preferences.PlayerViewCreatureLabels;
					foreach (TurnLog current2 in current.Turns)
					{
						if (current2.Entries.Count != 0)
						{
							list.Add("<TR>");
							if (player_view)
							{
								list.Add("<TD class=pvlogentry colspan=3>");
							}
							else
							{
								list.Add("<TD colspan=2>");
							}
							list.Add("<B>" + EncounterLog.GetName(current2.ID, this.fEncounter, detailed) + "</B>");
							list.Add("</TD>");
							if (!player_view)
							{
								list.Add("<TD align=right>");
								list.Add(current2.Start.ToString("h:mm:ss"));
								list.Add("</TD>");
							}
							list.Add("</TR>");
							foreach (IEncounterLogEntry current3 in current2.Entries)
							{
								list.Add("<TR>");
								if (player_view)
								{
									list.Add("<TD class=pvlogindent colspan=3>");
								}
								else
								{
									list.Add("<TD class=indent colspan=3>");
								}
								list.Add(current3.Description(this.fEncounter, detailed));
								list.Add("</TD>");
								list.Add("</TR>");
							}
						}
					}
				}
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			if (!player_view)
			{
				list.Add("</BODY>");
				list.Add("</HTML>");
			}
			return HTML.Concatenate(list);
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(CombatForm));
			ListViewGroup listViewGroup = new ListViewGroup("Combatants", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Delayed / Readied", HorizontalAlignment.Left);
			ListViewGroup listViewGroup3 = new ListViewGroup("Traps", HorizontalAlignment.Left);
			ListViewGroup listViewGroup4 = new ListViewGroup("Skill Challenges", HorizontalAlignment.Left);
			ListViewGroup listViewGroup5 = new ListViewGroup("Custom Tokens and Overlays", HorizontalAlignment.Left);
			ListViewGroup listViewGroup6 = new ListViewGroup("Not In Play", HorizontalAlignment.Left);
			ListViewGroup listViewGroup7 = new ListViewGroup("Defeated", HorizontalAlignment.Left);
			ListViewGroup listViewGroup8 = new ListViewGroup("Predefined", HorizontalAlignment.Left);
			ListViewGroup listViewGroup9 = new ListViewGroup("Custom Tokens", HorizontalAlignment.Left);
			ListViewGroup listViewGroup10 = new ListViewGroup("Custom Overlays", HorizontalAlignment.Left);
			this.Toolbar = new ToolStrip();
			this.DetailsBtn = new ToolStripButton();
			this.DamageBtn = new ToolStripButton();
			this.HealBtn = new ToolStripButton();
			this.EffectMenu = new ToolStripDropDownButton();
			this.effectToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator18 = new ToolStripSeparator();
			this.NextInitBtn = new ToolStripButton();
			this.DelayBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.CombatantsBtn = new ToolStripDropDownButton();
			this.CombatantsAdd = new ToolStripMenuItem();
			this.CombatantsAddToken = new ToolStripMenuItem();
			this.CombatantsAddOverlay = new ToolStripMenuItem();
			this.toolStripSeparator6 = new ToolStripSeparator();
			this.CombatantsRemove = new ToolStripMenuItem();
			this.toolStripSeparator12 = new ToolStripSeparator();
			this.CombatantsHideAll = new ToolStripMenuItem();
			this.CombatantsShowAll = new ToolStripMenuItem();
			this.toolStripSeparator26 = new ToolStripSeparator();
			this.CombatantsWaves = new ToolStripMenuItem();
			this.MapMenu = new ToolStripDropDownButton();
			this.ShowMap = new ToolStripMenuItem();
			this.toolStripSeparator10 = new ToolStripSeparator();
			this.MapFog = new ToolStripMenuItem();
			this.MapFogAllCreatures = new ToolStripMenuItem();
			this.MapFogVisibleCreatures = new ToolStripMenuItem();
			this.MapFogHideCreatures = new ToolStripMenuItem();
			this.toolStripSeparator15 = new ToolStripSeparator();
			this.MapLOS = new ToolStripMenuItem();
			this.MapGrid = new ToolStripMenuItem();
			this.MapGridLabels = new ToolStripMenuItem();
			this.MapHealth = new ToolStripMenuItem();
			this.MapConditions = new ToolStripMenuItem();
			this.MapPictureTokens = new ToolStripMenuItem();
			this.toolStripSeparator7 = new ToolStripSeparator();
			this.MapNavigate = new ToolStripMenuItem();
			this.MapReset = new ToolStripMenuItem();
			this.toolStripSeparator8 = new ToolStripSeparator();
			this.MapDrawing = new ToolStripMenuItem();
			this.MapClearDrawings = new ToolStripMenuItem();
			this.toolStripSeparator19 = new ToolStripSeparator();
			this.MapPrint = new ToolStripMenuItem();
			this.MapExport = new ToolStripMenuItem();
			this.PlayerViewMapMenu = new ToolStripDropDownButton();
			this.PlayerViewMap = new ToolStripMenuItem();
			this.PlayerViewInitList = new ToolStripMenuItem();
			this.toolStripSeparator9 = new ToolStripSeparator();
			this.PlayerViewFog = new ToolStripMenuItem();
			this.PlayerFogAll = new ToolStripMenuItem();
			this.PlayerFogVisible = new ToolStripMenuItem();
			this.PlayerFogNone = new ToolStripMenuItem();
			this.toolStripSeparator16 = new ToolStripSeparator();
			this.PlayerViewLOS = new ToolStripMenuItem();
			this.PlayerViewGrid = new ToolStripMenuItem();
			this.PlayerViewGridLabels = new ToolStripMenuItem();
			this.PlayerHealth = new ToolStripMenuItem();
			this.PlayerConditions = new ToolStripMenuItem();
			this.PlayerPictureTokens = new ToolStripMenuItem();
			this.toolStripSeparator17 = new ToolStripSeparator();
			this.PlayerLabels = new ToolStripMenuItem();
			this.PlayerViewNoMapMenu = new ToolStripDropDownButton();
			this.PlayerViewNoMapShowInitiativeList = new ToolStripMenuItem();
			this.PlayerViewNoMapShowLabels = new ToolStripMenuItem();
			this.ToolsMenu = new ToolStripDropDownButton();
			this.ToolsEffects = new ToolStripMenuItem();
			this.ToolsLinks = new ToolStripMenuItem();
			this.toolStripSeparator11 = new ToolStripSeparator();
			this.ToolsAddIns = new ToolStripMenuItem();
			this.addinsToolStripMenuItem = new ToolStripMenuItem();
			this.OptionsMenu = new ToolStripDropDownButton();
			this.OptionsShowInit = new ToolStripMenuItem();
			this.toolStripSeparator13 = new ToolStripSeparator();
			this.OneColumn = new ToolStripMenuItem();
			this.TwoColumns = new ToolStripMenuItem();
			this.toolStripSeparator20 = new ToolStripSeparator();
			this.MapRight = new ToolStripMenuItem();
			this.MapBelow = new ToolStripMenuItem();
			this.toolStripSeparator21 = new ToolStripSeparator();
			this.OptionsLandscape = new ToolStripMenuItem();
			this.OptionsPortrait = new ToolStripMenuItem();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.ToolsAutoRemove = new ToolStripMenuItem();
			this.OptionsIPlay4e = new ToolStripMenuItem();
			this.toolStripSeparator23 = new ToolStripSeparator();
			this.ToolsColumns = new ToolStripMenuItem();
			this.ToolsColumnsInit = new ToolStripMenuItem();
			this.ToolsColumnsHP = new ToolStripMenuItem();
			this.ToolsColumnsDefences = new ToolStripMenuItem();
			this.ToolsColumnsConditions = new ToolStripMenuItem();
			this.MapSplitter = new SplitContainer();
			this.Pages = new TabControl();
			this.CombatantsPage = new TabPage();
			this.ListSplitter = new SplitContainer();
			this.CombatList = new CombatForm.CombatListControl();
			this.NameHdr = new ColumnHeader();
			this.InitHdr = new ColumnHeader();
			this.HPHdr = new ColumnHeader();
			this.DefHdr = new ColumnHeader();
			this.EffectsHdr = new ColumnHeader();
			this.ListContext = new ContextMenuStrip(this.components);
			this.ListDetails = new ToolStripMenuItem();
			this.toolStripSeparator14 = new ToolStripSeparator();
			this.ListDamage = new ToolStripMenuItem();
			this.ListHeal = new ToolStripMenuItem();
			this.ListCondition = new ToolStripMenuItem();
			this.effectToolStripMenuItem1 = new ToolStripMenuItem();
			this.ListRemoveEffect = new ToolStripMenuItem();
			this.effectToolStripMenuItem3 = new ToolStripMenuItem();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.ListRemove = new ToolStripMenuItem();
			this.ListRemoveMap = new ToolStripMenuItem();
			this.ListRemoveCombat = new ToolStripMenuItem();
			this.ListCreateCopy = new ToolStripMenuItem();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.ListVisible = new ToolStripMenuItem();
			this.ListDelay = new ToolStripMenuItem();
			this.PreviewPanel = new Panel();
			this.Preview = new WebBrowser();
			this.TemplatesPage = new TabPage();
			this.TemplateList = new ListView();
			this.TemplateHdr = new ColumnHeader();
			this.LogPage = new TabPage();
			this.LogBrowser = new WebBrowser();
			this.MapView = new MapView();
			this.MapContext = new ContextMenuStrip(this.components);
			this.MapDetails = new ToolStripMenuItem();
			this.toolStripMenuItem2 = new ToolStripSeparator();
			this.MapDamage = new ToolStripMenuItem();
			this.MapHeal = new ToolStripMenuItem();
			this.MapAddEffect = new ToolStripMenuItem();
			this.effectToolStripMenuItem2 = new ToolStripMenuItem();
			this.MapRemoveEffect = new ToolStripMenuItem();
			this.effectToolStripMenuItem4 = new ToolStripMenuItem();
			this.MapSetPicture = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this.MapRemove = new ToolStripMenuItem();
			this.MapRemoveMap = new ToolStripMenuItem();
			this.MapRemoveCombat = new ToolStripMenuItem();
			this.MapCreateCopy = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.MapVisible = new ToolStripMenuItem();
			this.MapDelay = new ToolStripMenuItem();
			this.toolStripSeparator22 = new ToolStripSeparator();
			this.MapContextDrawing = new ToolStripMenuItem();
			this.MapContextClearDrawings = new ToolStripMenuItem();
			this.toolStripSeparator25 = new ToolStripSeparator();
			this.MapContextLOS = new ToolStripMenuItem();
			this.toolStripSeparator24 = new ToolStripSeparator();
			this.MapContextOverlay = new ToolStripMenuItem();
			this.ZoomGauge = new TrackBar();
			this.MapTooltip = new ToolTip(this.components);
			this.Statusbar = new StatusStrip();
			this.RoundLbl = new ToolStripStatusLabel();
			this.XPLbl = new ToolStripStatusLabel();
			this.LevelLbl = new ToolStripStatusLabel();
			this.MainPanel = new Panel();
			this.InitiativePanel = new InitiativePanel();
			this.CloseBtn = new Button();
			this.PauseBtn = new Button();
			this.InfoBtn = new Button();
			this.DieRollerBtn = new Button();
			this.ReportBtn = new Button();
			this.Toolbar.SuspendLayout();
			this.MapSplitter.Panel1.SuspendLayout();
			this.MapSplitter.Panel2.SuspendLayout();
			this.MapSplitter.SuspendLayout();
			this.Pages.SuspendLayout();
			this.CombatantsPage.SuspendLayout();
			this.ListSplitter.Panel1.SuspendLayout();
			this.ListSplitter.Panel2.SuspendLayout();
			this.ListSplitter.SuspendLayout();
			this.ListContext.SuspendLayout();
			this.PreviewPanel.SuspendLayout();
			this.TemplatesPage.SuspendLayout();
			this.LogPage.SuspendLayout();
			this.MapContext.SuspendLayout();
			((ISupportInitialize)this.ZoomGauge).BeginInit();
			this.Statusbar.SuspendLayout();
			this.MainPanel.SuspendLayout();
			base.SuspendLayout();
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.DetailsBtn,
				this.DamageBtn,
				this.HealBtn,
				this.EffectMenu,
				this.toolStripSeparator18,
				this.NextInitBtn,
				this.DelayBtn,
				this.toolStripSeparator1,
				this.CombatantsBtn,
				this.MapMenu,
				this.PlayerViewMapMenu,
				this.PlayerViewNoMapMenu,
				this.ToolsMenu,
				this.OptionsMenu
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(850, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.DetailsBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DetailsBtn.Image = (Image)resources.GetObject("DetailsBtn.Image");
			this.DetailsBtn.ImageTransparentColor = Color.Magenta;
			this.DetailsBtn.Name = "DetailsBtn";
			this.DetailsBtn.Size = new Size(46, 22);
			this.DetailsBtn.Text = "Details";
			this.DetailsBtn.Click += new EventHandler(this.DetailsBtn_Click);
			this.DamageBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DamageBtn.Image = (Image)resources.GetObject("DamageBtn.Image");
			this.DamageBtn.ImageTransparentColor = Color.Magenta;
			this.DamageBtn.Name = "DamageBtn";
			this.DamageBtn.Size = new Size(55, 22);
			this.DamageBtn.Text = "Damage";
			this.DamageBtn.Click += new EventHandler(this.DamageBtn_Click);
			this.HealBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.HealBtn.Image = (Image)resources.GetObject("HealBtn.Image");
			this.HealBtn.ImageTransparentColor = Color.Magenta;
			this.HealBtn.Name = "HealBtn";
			this.HealBtn.Size = new Size(35, 22);
			this.HealBtn.Text = "Heal";
			this.HealBtn.Click += new EventHandler(this.HealBtn_Click);
			this.EffectMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EffectMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.effectToolStripMenuItem
			});
			this.EffectMenu.Image = (Image)resources.GetObject("EffectMenu.Image");
			this.EffectMenu.ImageTransparentColor = Color.Magenta;
			this.EffectMenu.Name = "EffectMenu";
			this.EffectMenu.Size = new Size(75, 22);
			this.EffectMenu.Text = "Add Effect";
			this.EffectMenu.DropDownOpening += new EventHandler(this.EffectMenu_DropDownOpening);
			this.effectToolStripMenuItem.Name = "effectToolStripMenuItem";
			this.effectToolStripMenuItem.Size = new Size(112, 22);
			this.effectToolStripMenuItem.Text = "[effect]";
			this.toolStripSeparator18.Name = "toolStripSeparator18";
			this.toolStripSeparator18.Size = new Size(6, 25);
			this.NextInitBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.NextInitBtn.Image = (Image)resources.GetObject("NextInitBtn.Image");
			this.NextInitBtn.ImageTransparentColor = Color.Magenta;
			this.NextInitBtn.Name = "NextInitBtn";
			this.NextInitBtn.Size = new Size(63, 22);
			this.NextInitBtn.Text = "Next Turn";
			this.NextInitBtn.Click += new EventHandler(this.NextInitBtn_Click);
			this.DelayBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DelayBtn.Image = (Image)resources.GetObject("DelayBtn.Image");
			this.DelayBtn.ImageTransparentColor = Color.Magenta;
			this.DelayBtn.Name = "DelayBtn";
			this.DelayBtn.Size = new Size(78, 22);
			this.DelayBtn.Text = "Delay Action";
			this.DelayBtn.Click += new EventHandler(this.DelayBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.CombatantsBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CombatantsBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.CombatantsAdd,
				this.CombatantsAddToken,
				this.CombatantsAddOverlay,
				this.toolStripSeparator6,
				this.CombatantsWaves,
				this.toolStripSeparator26,
				this.CombatantsRemove,
				this.toolStripSeparator12,
				this.CombatantsHideAll,
				this.CombatantsShowAll
			});
			this.CombatantsBtn.Image = (Image)resources.GetObject("CombatantsBtn.Image");
			this.CombatantsBtn.ImageTransparentColor = Color.Magenta;
			this.CombatantsBtn.Name = "CombatantsBtn";
			this.CombatantsBtn.Size = new Size(85, 22);
			this.CombatantsBtn.Text = "Combatants";
			this.CombatantsBtn.DropDownOpening += new EventHandler(this.CombatantsBtn_DropDownOpening);
			this.CombatantsAdd.Name = "CombatantsAdd";
			this.CombatantsAdd.Size = new Size(175, 22);
			this.CombatantsAdd.Text = "Add Combatant...";
			this.CombatantsAdd.Click += new EventHandler(this.CombatantsAdd_Click);
			this.CombatantsAddToken.Name = "CombatantsAddToken";
			this.CombatantsAddToken.Size = new Size(175, 22);
			this.CombatantsAddToken.Text = "Add Map Token...";
			this.CombatantsAddToken.Click += new EventHandler(this.CombatantsAddCustom_Click);
			this.CombatantsAddOverlay.Name = "CombatantsAddOverlay";
			this.CombatantsAddOverlay.Size = new Size(175, 22);
			this.CombatantsAddOverlay.Text = "Add Map Overlay...";
			this.CombatantsAddOverlay.Click += new EventHandler(this.CombatantsAddOverlay_Click);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new Size(172, 6);
			this.CombatantsRemove.Name = "CombatantsRemove";
			this.CombatantsRemove.Size = new Size(175, 22);
			this.CombatantsRemove.Text = "Remove Selected";
			this.CombatantsRemove.Click += new EventHandler(this.CombatantsRemove_Click);
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new Size(172, 6);
			this.CombatantsHideAll.Name = "CombatantsHideAll";
			this.CombatantsHideAll.Size = new Size(175, 22);
			this.CombatantsHideAll.Text = "Hide All";
			this.CombatantsHideAll.Click += new EventHandler(this.CombatantsHideAll_Click);
			this.CombatantsShowAll.Name = "CombatantsShowAll";
			this.CombatantsShowAll.Size = new Size(175, 22);
			this.CombatantsShowAll.Text = "Show All";
			this.CombatantsShowAll.Click += new EventHandler(this.CombatantsShowAll_Click);
			this.toolStripSeparator26.Name = "toolStripSeparator26";
			this.toolStripSeparator26.Size = new Size(172, 6);
			this.CombatantsWaves.Name = "CombatantsWaves";
			this.CombatantsWaves.Size = new Size(175, 22);
			this.CombatantsWaves.Text = "Waves";
			this.MapMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MapMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ShowMap,
				this.toolStripSeparator10,
				this.MapFog,
				this.toolStripSeparator15,
				this.MapLOS,
				this.MapGrid,
				this.MapGridLabels,
				this.MapHealth,
				this.MapConditions,
				this.MapPictureTokens,
				this.toolStripSeparator7,
				this.MapNavigate,
				this.MapReset,
				this.toolStripSeparator8,
				this.MapDrawing,
				this.MapClearDrawings,
				this.toolStripSeparator19,
				this.MapPrint,
				this.MapExport
			});
			this.MapMenu.Image = (Image)resources.GetObject("MapMenu.Image");
			this.MapMenu.ImageTransparentColor = Color.Magenta;
			this.MapMenu.Name = "MapMenu";
			this.MapMenu.Size = new Size(44, 22);
			this.MapMenu.Text = "Map";
			this.MapMenu.DropDownOpening += new EventHandler(this.MapMenu_DropDownOpening);
			this.ShowMap.Name = "ShowMap";
			this.ShowMap.Size = new Size(184, 22);
			this.ShowMap.Text = "Show Map";
			this.ShowMap.Click += new EventHandler(this.ShowMap_Click);
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new Size(181, 6);
			this.MapFog.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MapFog.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.MapFogAllCreatures,
				this.MapFogVisibleCreatures,
				this.MapFogHideCreatures
			});
			this.MapFog.Image = (Image)resources.GetObject("MapFog.Image");
			this.MapFog.ImageTransparentColor = Color.Magenta;
			this.MapFog.Name = "MapFog";
			this.MapFog.Size = new Size(184, 22);
			this.MapFog.Text = "Fog of War";
			this.MapFogAllCreatures.Name = "MapFogAllCreatures";
			this.MapFogAllCreatures.Size = new Size(221, 22);
			this.MapFogAllCreatures.Text = "Show All Creatures";
			this.MapFogAllCreatures.Click += new EventHandler(this.MapFogAllCreatures_Click);
			this.MapFogVisibleCreatures.Name = "MapFogVisibleCreatures";
			this.MapFogVisibleCreatures.Size = new Size(221, 22);
			this.MapFogVisibleCreatures.Text = "Show Visible Creatures Only";
			this.MapFogVisibleCreatures.Click += new EventHandler(this.MapFogVisibleCreatures_Click);
			this.MapFogHideCreatures.Name = "MapFogHideCreatures";
			this.MapFogHideCreatures.Size = new Size(221, 22);
			this.MapFogHideCreatures.Text = "Hide All Creatures";
			this.MapFogHideCreatures.Click += new EventHandler(this.MapFogHideCreatures_Click);
			this.toolStripSeparator15.Name = "toolStripSeparator15";
			this.toolStripSeparator15.Size = new Size(181, 6);
			this.MapLOS.Name = "MapLOS";
			this.MapLOS.Size = new Size(184, 22);
			this.MapLOS.Text = "Show Line of Sight";
			this.MapLOS.Click += new EventHandler(this.MapLOS_Click);
			this.MapGrid.Name = "MapGrid";
			this.MapGrid.Size = new Size(184, 22);
			this.MapGrid.Text = "Show Grid";
			this.MapGrid.Click += new EventHandler(this.MapGrid_Click);
			this.MapGridLabels.Name = "MapGridLabels";
			this.MapGridLabels.Size = new Size(184, 22);
			this.MapGridLabels.Text = "Show Grid Labels";
			this.MapGridLabels.Click += new EventHandler(this.MapGridLabels_Click);
			this.MapHealth.Name = "MapHealth";
			this.MapHealth.Size = new Size(184, 22);
			this.MapHealth.Text = "Show Health Bars";
			this.MapHealth.Click += new EventHandler(this.MapHealth_Click);
			this.MapConditions.Name = "MapConditions";
			this.MapConditions.Size = new Size(184, 22);
			this.MapConditions.Text = "Show Conditions";
			this.MapConditions.Click += new EventHandler(this.MapConditions_Click);
			this.MapPictureTokens.Name = "MapPictureTokens";
			this.MapPictureTokens.Size = new Size(184, 22);
			this.MapPictureTokens.Text = "Show Picture Tokens";
			this.MapPictureTokens.Click += new EventHandler(this.MapPictureTokens_Click);
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new Size(181, 6);
			this.MapNavigate.Name = "MapNavigate";
			this.MapNavigate.Size = new Size(184, 22);
			this.MapNavigate.Text = "Scroll and Zoom";
			this.MapNavigate.Click += new EventHandler(this.MapNavigate_Click);
			this.MapReset.Name = "MapReset";
			this.MapReset.Size = new Size(184, 22);
			this.MapReset.Text = "Reset View";
			this.MapReset.Click += new EventHandler(this.MapReset_Click);
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new Size(181, 6);
			this.MapDrawing.Name = "MapDrawing";
			this.MapDrawing.Size = new Size(184, 22);
			this.MapDrawing.Text = "Allow Drawing";
			this.MapDrawing.Click += new EventHandler(this.MapDrawing_Click);
			this.MapClearDrawings.Name = "MapClearDrawings";
			this.MapClearDrawings.Size = new Size(184, 22);
			this.MapClearDrawings.Text = "Clear Drawings";
			this.MapClearDrawings.Click += new EventHandler(this.MapClearDrawings_Click);
			this.toolStripSeparator19.Name = "toolStripSeparator19";
			this.toolStripSeparator19.Size = new Size(181, 6);
			this.MapPrint.Name = "MapPrint";
			this.MapPrint.Size = new Size(184, 22);
			this.MapPrint.Text = "Print";
			this.MapPrint.Click += new EventHandler(this.MapPrint_Click);
			this.MapExport.Name = "MapExport";
			this.MapExport.Size = new Size(184, 22);
			this.MapExport.Text = "Export Screenshot";
			this.MapExport.Click += new EventHandler(this.MapExport_Click);
			this.PlayerViewMapMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PlayerViewMapMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.PlayerViewMap,
				this.PlayerViewInitList,
				this.toolStripSeparator9,
				this.PlayerViewFog,
				this.toolStripSeparator16,
				this.PlayerViewLOS,
				this.PlayerViewGrid,
				this.PlayerViewGridLabels,
				this.PlayerHealth,
				this.PlayerConditions,
				this.PlayerPictureTokens,
				this.toolStripSeparator17,
				this.PlayerLabels
			});
			this.PlayerViewMapMenu.Image = (Image)resources.GetObject("PlayerViewMapMenu.Image");
			this.PlayerViewMapMenu.ImageTransparentColor = Color.Magenta;
			this.PlayerViewMapMenu.Name = "PlayerViewMapMenu";
			this.PlayerViewMapMenu.Size = new Size(80, 22);
			this.PlayerViewMapMenu.Text = "Player View";
			this.PlayerViewMapMenu.DropDownOpening += new EventHandler(this.PlayerViewMapMenu_DropDownOpening);
			this.PlayerViewMap.Name = "PlayerViewMap";
			this.PlayerViewMap.Size = new Size(215, 22);
			this.PlayerViewMap.Text = "Show Map";
			this.PlayerViewMap.Click += new EventHandler(this.PlayerViewMap_Click);
			this.PlayerViewInitList.Name = "PlayerViewInitList";
			this.PlayerViewInitList.Size = new Size(215, 22);
			this.PlayerViewInitList.Text = "Show Initiative List";
			this.PlayerViewInitList.Click += new EventHandler(this.PlayerViewInitList_Click);
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new Size(212, 6);
			this.PlayerViewFog.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PlayerViewFog.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.PlayerFogAll,
				this.PlayerFogVisible,
				this.PlayerFogNone
			});
			this.PlayerViewFog.Image = (Image)resources.GetObject("PlayerViewFog.Image");
			this.PlayerViewFog.ImageTransparentColor = Color.Magenta;
			this.PlayerViewFog.Name = "PlayerViewFog";
			this.PlayerViewFog.Size = new Size(215, 22);
			this.PlayerViewFog.Text = "Fog of War";
			this.PlayerFogAll.Name = "PlayerFogAll";
			this.PlayerFogAll.Size = new Size(221, 22);
			this.PlayerFogAll.Text = "Show All Creatures";
			this.PlayerFogAll.Click += new EventHandler(this.PlayerFogAll_Click);
			this.PlayerFogVisible.Name = "PlayerFogVisible";
			this.PlayerFogVisible.Size = new Size(221, 22);
			this.PlayerFogVisible.Text = "Show Visible Creatures Only";
			this.PlayerFogVisible.Click += new EventHandler(this.PlayerFogVisible_Click);
			this.PlayerFogNone.Name = "PlayerFogNone";
			this.PlayerFogNone.Size = new Size(221, 22);
			this.PlayerFogNone.Text = "Hide All Creatures";
			this.PlayerFogNone.Click += new EventHandler(this.PlayerFogNone_Click);
			this.toolStripSeparator16.Name = "toolStripSeparator16";
			this.toolStripSeparator16.Size = new Size(212, 6);
			this.PlayerViewLOS.Name = "PlayerViewLOS";
			this.PlayerViewLOS.Size = new Size(215, 22);
			this.PlayerViewLOS.Text = "Show Line of Sight";
			this.PlayerViewLOS.Click += new EventHandler(this.PlayerViewLOS_Click);
			this.PlayerViewGrid.Name = "PlayerViewGrid";
			this.PlayerViewGrid.Size = new Size(215, 22);
			this.PlayerViewGrid.Text = "Show Grid";
			this.PlayerViewGrid.Click += new EventHandler(this.PlayerViewGrid_Click);
			this.PlayerViewGridLabels.Name = "PlayerViewGridLabels";
			this.PlayerViewGridLabels.Size = new Size(215, 22);
			this.PlayerViewGridLabels.Text = "Show Grid Labels";
			this.PlayerViewGridLabels.Click += new EventHandler(this.PlayerViewGridLabels_Click);
			this.PlayerHealth.Name = "PlayerHealth";
			this.PlayerHealth.Size = new Size(215, 22);
			this.PlayerHealth.Text = "Show Health Bars";
			this.PlayerHealth.Click += new EventHandler(this.PlayerHealth_Click);
			this.PlayerConditions.Name = "PlayerConditions";
			this.PlayerConditions.Size = new Size(215, 22);
			this.PlayerConditions.Text = "Show Conditions";
			this.PlayerConditions.Click += new EventHandler(this.PlayerConditions_Click);
			this.PlayerPictureTokens.Name = "PlayerPictureTokens";
			this.PlayerPictureTokens.Size = new Size(215, 22);
			this.PlayerPictureTokens.Text = "Show Picture Tokens";
			this.PlayerPictureTokens.Click += new EventHandler(this.PlayerPictureTokens_Click);
			this.toolStripSeparator17.Name = "toolStripSeparator17";
			this.toolStripSeparator17.Size = new Size(212, 6);
			this.PlayerLabels.Name = "PlayerLabels";
			this.PlayerLabels.Size = new Size(215, 22);
			this.PlayerLabels.Text = "Show Detailed Information";
			this.PlayerLabels.Click += new EventHandler(this.PlayerLabels_Click);
			this.PlayerViewNoMapMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PlayerViewNoMapMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.PlayerViewNoMapShowInitiativeList,
				this.PlayerViewNoMapShowLabels
			});
			this.PlayerViewNoMapMenu.Image = (Image)resources.GetObject("PlayerViewNoMapMenu.Image");
			this.PlayerViewNoMapMenu.ImageTransparentColor = Color.Magenta;
			this.PlayerViewNoMapMenu.Name = "PlayerViewNoMapMenu";
			this.PlayerViewNoMapMenu.Size = new Size(80, 22);
			this.PlayerViewNoMapMenu.Text = "Player View";
			this.PlayerViewNoMapMenu.DropDownOpening += new EventHandler(this.PlayerViewNoMapMenu_DropDownOpening);
			this.PlayerViewNoMapShowInitiativeList.Name = "PlayerViewNoMapShowInitiativeList";
			this.PlayerViewNoMapShowInitiativeList.Size = new Size(215, 22);
			this.PlayerViewNoMapShowInitiativeList.Text = "Show Initiative List";
			this.PlayerViewNoMapShowInitiativeList.Click += new EventHandler(this.PlayerViewNoMapShowInitiativeList_Click);
			this.PlayerViewNoMapShowLabels.Name = "PlayerViewNoMapShowLabels";
			this.PlayerViewNoMapShowLabels.Size = new Size(215, 22);
			this.PlayerViewNoMapShowLabels.Text = "Show Detailed Information";
			this.PlayerViewNoMapShowLabels.Click += new EventHandler(this.PlayerViewNoMapShowLabels_Click);
			this.ToolsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ToolsMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ToolsEffects,
				this.ToolsLinks,
				this.toolStripSeparator11,
				this.ToolsAddIns
			});
			this.ToolsMenu.Image = (Image)resources.GetObject("ToolsMenu.Image");
			this.ToolsMenu.ImageTransparentColor = Color.Magenta;
			this.ToolsMenu.Name = "ToolsMenu";
			this.ToolsMenu.Size = new Size(49, 22);
			this.ToolsMenu.Text = "Tools";
			this.ToolsMenu.Click += new EventHandler(this.ToolsMenu_DopDownOpening);
			this.ToolsEffects.Name = "ToolsEffects";
			this.ToolsEffects.Size = new Size(159, 22);
			this.ToolsEffects.Text = "Ongoing Effects";
			this.ToolsEffects.Click += new EventHandler(this.CombatantsEffects_Click);
			this.ToolsLinks.Name = "ToolsLinks";
			this.ToolsLinks.Size = new Size(159, 22);
			this.ToolsLinks.Text = "Token Links";
			this.ToolsLinks.Click += new EventHandler(this.CombatantsLinks_Click);
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new Size(156, 6);
			this.ToolsAddIns.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.addinsToolStripMenuItem
			});
			this.ToolsAddIns.Name = "ToolsAddIns";
			this.ToolsAddIns.Size = new Size(159, 22);
			this.ToolsAddIns.Text = "Add-Ins";
			this.addinsToolStripMenuItem.Name = "addinsToolStripMenuItem";
			this.addinsToolStripMenuItem.Size = new Size(122, 22);
			this.addinsToolStripMenuItem.Text = "[add-ins]";
			this.OptionsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.OptionsMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.OptionsShowInit,
				this.toolStripSeparator13,
				this.OneColumn,
				this.TwoColumns,
				this.toolStripSeparator20,
				this.MapRight,
				this.MapBelow,
				this.toolStripSeparator21,
				this.OptionsLandscape,
				this.OptionsPortrait,
				this.toolStripSeparator5,
				this.ToolsAutoRemove,
				this.OptionsIPlay4e,
				this.toolStripSeparator23,
				this.ToolsColumns
			});
			this.OptionsMenu.Image = (Image)resources.GetObject("OptionsMenu.Image");
			this.OptionsMenu.ImageTransparentColor = Color.Magenta;
			this.OptionsMenu.Name = "OptionsMenu";
			this.OptionsMenu.Size = new Size(62, 22);
			this.OptionsMenu.Text = "Options";
			this.OptionsMenu.DropDownOpening += new EventHandler(this.OptionsMenu_DropDownOpening);
			this.OptionsShowInit.Name = "OptionsShowInit";
			this.OptionsShowInit.Size = new Size(229, 22);
			this.OptionsShowInit.Text = "Show Initiative Gauge";
			this.OptionsShowInit.Click += new EventHandler(this.OptionsShowInit_Click);
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new Size(226, 6);
			this.OneColumn.Name = "OneColumn";
			this.OneColumn.Size = new Size(229, 22);
			this.OneColumn.Text = "One Column";
			this.OneColumn.Click += new EventHandler(this.OneColumn_Click);
			this.TwoColumns.Name = "TwoColumns";
			this.TwoColumns.Size = new Size(229, 22);
			this.TwoColumns.Text = "Two Columns";
			this.TwoColumns.Click += new EventHandler(this.TwoColumns_Click);
			this.toolStripSeparator20.Name = "toolStripSeparator20";
			this.toolStripSeparator20.Size = new Size(226, 6);
			this.MapRight.Name = "MapRight";
			this.MapRight.Size = new Size(229, 22);
			this.MapRight.Text = "Map at Right";
			this.MapRight.Click += new EventHandler(this.OptionsMapRight_Click);
			this.MapBelow.Name = "MapBelow";
			this.MapBelow.Size = new Size(229, 22);
			this.MapBelow.Text = "Map Below";
			this.MapBelow.Click += new EventHandler(this.OptionsMapBelow_Click);
			this.toolStripSeparator21.Name = "toolStripSeparator21";
			this.toolStripSeparator21.Size = new Size(226, 6);
			this.OptionsLandscape.Name = "OptionsLandscape";
			this.OptionsLandscape.Size = new Size(229, 22);
			this.OptionsLandscape.Text = "Landscape";
			this.OptionsLandscape.Click += new EventHandler(this.OptionsLandscape_Click);
			this.OptionsPortrait.Name = "OptionsPortrait";
			this.OptionsPortrait.Size = new Size(229, 22);
			this.OptionsPortrait.Text = "Portrait";
			this.OptionsPortrait.Click += new EventHandler(this.OptionsPortrait_Click);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new Size(226, 6);
			this.ToolsAutoRemove.Name = "ToolsAutoRemove";
			this.ToolsAutoRemove.Size = new Size(229, 22);
			this.ToolsAutoRemove.Text = "Remove Defeated Opponents";
			this.ToolsAutoRemove.Click += new EventHandler(this.ToolsAutoRemove_Click);
			this.OptionsIPlay4e.Name = "OptionsIPlay4e";
			this.OptionsIPlay4e.Size = new Size(229, 22);
			this.OptionsIPlay4e.Text = "iPlay4e Integration";
			this.OptionsIPlay4e.Click += new EventHandler(this.OptionsIPlay4e_Click);
			this.toolStripSeparator23.Name = "toolStripSeparator23";
			this.toolStripSeparator23.Size = new Size(226, 6);
			this.ToolsColumns.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ToolsColumnsInit,
				this.ToolsColumnsHP,
				this.ToolsColumnsDefences,
				this.ToolsColumnsConditions
			});
			this.ToolsColumns.Name = "ToolsColumns";
			this.ToolsColumns.Size = new Size(229, 22);
			this.ToolsColumns.Text = "Columns";
			this.ToolsColumns.DropDownOpening += new EventHandler(this.ToolsColumns_DropDownOpening);
			this.ToolsColumnsInit.Name = "ToolsColumnsInit";
			this.ToolsColumnsInit.Size = new Size(126, 22);
			this.ToolsColumnsInit.Text = "Initiative";
			this.ToolsColumnsInit.Click += new EventHandler(this.ToolsColumnsInit_Click);
			this.ToolsColumnsHP.Name = "ToolsColumnsHP";
			this.ToolsColumnsHP.Size = new Size(126, 22);
			this.ToolsColumnsHP.Text = "Hit Points";
			this.ToolsColumnsHP.Click += new EventHandler(this.ToolsColumnsHP_Click);
			this.ToolsColumnsDefences.Name = "ToolsColumnsDefences";
			this.ToolsColumnsDefences.Size = new Size(126, 22);
			this.ToolsColumnsDefences.Text = "Defences";
			this.ToolsColumnsDefences.Click += new EventHandler(this.ToolsColumnsDefences_Click);
			this.ToolsColumnsConditions.Name = "ToolsColumnsConditions";
			this.ToolsColumnsConditions.Size = new Size(126, 22);
			this.ToolsColumnsConditions.Text = "Effects";
			this.ToolsColumnsConditions.Click += new EventHandler(this.ToolsColumnsConditions_Click);
			this.MapSplitter.Dock = DockStyle.Fill;
			this.MapSplitter.FixedPanel = FixedPanel.Panel1;
			this.MapSplitter.Location = new Point(0, 0);
			this.MapSplitter.Name = "MapSplitter";
			this.MapSplitter.Panel1.Controls.Add(this.Pages);
			this.MapSplitter.Panel2.Controls.Add(this.MapView);
			this.MapSplitter.Panel2.Controls.Add(this.ZoomGauge);
			this.MapSplitter.Size = new Size(786, 362);
			this.MapSplitter.SplitterDistance = 368;
			this.MapSplitter.TabIndex = 1;
			this.Pages.Controls.Add(this.CombatantsPage);
			this.Pages.Controls.Add(this.TemplatesPage);
			this.Pages.Controls.Add(this.LogPage);
			this.Pages.Dock = DockStyle.Fill;
			this.Pages.Location = new Point(0, 0);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(368, 362);
			this.Pages.TabIndex = 2;
			this.CombatantsPage.Controls.Add(this.ListSplitter);
			this.CombatantsPage.Location = new Point(4, 22);
			this.CombatantsPage.Name = "CombatantsPage";
			this.CombatantsPage.Padding = new Padding(3);
			this.CombatantsPage.Size = new Size(360, 336);
			this.CombatantsPage.TabIndex = 0;
			this.CombatantsPage.Text = "Combatants";
			this.CombatantsPage.UseVisualStyleBackColor = true;
			this.ListSplitter.Dock = DockStyle.Fill;
			this.ListSplitter.Location = new Point(3, 3);
			this.ListSplitter.Name = "ListSplitter";
			this.ListSplitter.Orientation = Orientation.Horizontal;
			this.ListSplitter.Panel1.Controls.Add(this.CombatList);
			this.ListSplitter.Panel2.Controls.Add(this.PreviewPanel);
			this.ListSplitter.Size = new Size(354, 330);
			this.ListSplitter.SplitterDistance = 159;
			this.ListSplitter.TabIndex = 1;
			this.ListSplitter.Resize += new EventHandler(this.ListSplitter_Resize);
			this.ListSplitter.SplitterMoved += new SplitterEventHandler(this.ListSplitter_SplitterMoved);
			this.CombatList.Columns.AddRange(new ColumnHeader[]
			{
				this.NameHdr,
				this.InitHdr,
				this.HPHdr,
				this.DefHdr,
				this.EffectsHdr
			});
			this.CombatList.ContextMenuStrip = this.ListContext;
			this.CombatList.Dock = DockStyle.Fill;
			this.CombatList.FullRowSelect = true;
			listViewGroup.Header = "Combatants";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup2.Header = "Delayed / Readied";
			listViewGroup2.Name = "listViewGroup5";
			listViewGroup3.Header = "Traps";
			listViewGroup3.Name = "listViewGroup3";
			listViewGroup4.Header = "Skill Challenges";
			listViewGroup4.Name = "listViewGroup4";
			listViewGroup5.Header = "Custom Tokens and Overlays";
			listViewGroup5.Name = "listViewGroup6";
			listViewGroup6.Header = "Not In Play";
			listViewGroup6.Name = "listViewGroup2";
			listViewGroup7.Header = "Defeated";
			listViewGroup7.Name = "listViewGroup7";
			this.CombatList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2,
				listViewGroup3,
				listViewGroup4,
				listViewGroup5,
				listViewGroup6,
				listViewGroup7
			});
			this.CombatList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.CombatList.HideSelection = false;
			this.CombatList.Location = new Point(0, 0);
			this.CombatList.Name = "CombatList";
			this.CombatList.OwnerDraw = true;
			this.CombatList.Size = new Size(354, 159);
			this.CombatList.TabIndex = 0;
			this.CombatList.TileSize = new Size(300, 45);
			this.CombatList.UseCompatibleStateImageBehavior = false;
			this.CombatList.View = View.Details;
			this.CombatList.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(this.CombatList_DrawColumnHeader);
			this.CombatList.DrawItem += new DrawListViewItemEventHandler(this.CombatList_DrawItem);
			this.CombatList.SelectedIndexChanged += new EventHandler(this.CombatList_SelectedIndexChanged);
			this.CombatList.DoubleClick += new EventHandler(this.CombatList_DoubleClick);
			this.CombatList.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.CombatList_ItemSelectionChanged);
			this.CombatList.ItemDrag += new ItemDragEventHandler(this.CombatList_ItemDrag);
			this.CombatList.DrawSubItem += new DrawListViewSubItemEventHandler(this.CombatList_DrawSubItem);
			this.NameHdr.Text = "Name";
			this.NameHdr.Width = 185;
			this.InitHdr.Text = "Init";
			this.InitHdr.TextAlign = HorizontalAlignment.Right;
			this.HPHdr.Text = "HP";
			this.HPHdr.TextAlign = HorizontalAlignment.Right;
			this.DefHdr.Text = "Defences";
			this.DefHdr.Width = 200;
			this.EffectsHdr.Text = "Effects";
			this.EffectsHdr.Width = 175;
			this.ListContext.Items.AddRange(new ToolStripItem[]
			{
				this.ListDetails,
				this.toolStripSeparator14,
				this.ListDamage,
				this.ListHeal,
				this.ListCondition,
				this.ListRemoveEffect,
				this.toolStripSeparator3,
				this.ListRemove,
				this.ListCreateCopy,
				this.toolStripSeparator4,
				this.ListVisible,
				this.ListDelay
			});
			this.ListContext.Name = "MapContext";
			this.ListContext.Size = new Size(185, 220);
			this.ListContext.Opening += new CancelEventHandler(this.ListContext_Opening);
			this.ListDetails.Name = "ListDetails";
			this.ListDetails.Size = new Size(184, 22);
			this.ListDetails.Text = "Details";
			this.ListDetails.Click += new EventHandler(this.ListDetails_Click);
			this.toolStripSeparator14.Name = "toolStripSeparator14";
			this.toolStripSeparator14.Size = new Size(181, 6);
			this.ListDamage.Name = "ListDamage";
			this.ListDamage.Size = new Size(184, 22);
			this.ListDamage.Text = "Damage...";
			this.ListDamage.Click += new EventHandler(this.ListDamage_Click);
			this.ListHeal.Name = "ListHeal";
			this.ListHeal.Size = new Size(184, 22);
			this.ListHeal.Text = "Heal...";
			this.ListHeal.Click += new EventHandler(this.ListHeal_Click);
			this.ListCondition.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.effectToolStripMenuItem1
			});
			this.ListCondition.Name = "ListCondition";
			this.ListCondition.Size = new Size(184, 22);
			this.ListCondition.Text = "Add Effect";
			this.ListCondition.DropDownOpening += new EventHandler(this.ListCondition_DropDownOpening);
			this.effectToolStripMenuItem1.Name = "effectToolStripMenuItem1";
			this.effectToolStripMenuItem1.Size = new Size(112, 22);
			this.effectToolStripMenuItem1.Text = "[effect]";
			this.ListRemoveEffect.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.effectToolStripMenuItem3
			});
			this.ListRemoveEffect.Name = "ListRemoveEffect";
			this.ListRemoveEffect.Size = new Size(184, 22);
			this.ListRemoveEffect.Text = "Remove Effect";
			this.ListRemoveEffect.DropDownOpening += new EventHandler(this.ListRemoveEffect_DropDownOpening);
			this.effectToolStripMenuItem3.Name = "effectToolStripMenuItem3";
			this.effectToolStripMenuItem3.Size = new Size(112, 22);
			this.effectToolStripMenuItem3.Text = "[effect]";
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new Size(181, 6);
			this.ListRemove.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ListRemoveMap,
				this.ListRemoveCombat
			});
			this.ListRemove.Name = "ListRemove";
			this.ListRemove.Size = new Size(184, 22);
			this.ListRemove.Text = "Remove";
			this.ListRemoveMap.Name = "ListRemoveMap";
			this.ListRemoveMap.Size = new Size(192, 22);
			this.ListRemoveMap.Text = "Remove from Map";
			this.ListRemoveMap.Click += new EventHandler(this.ListRemoveMap_Click);
			this.ListRemoveCombat.Name = "ListRemoveCombat";
			this.ListRemoveCombat.Size = new Size(192, 22);
			this.ListRemoveCombat.Text = "Remove from Combat";
			this.ListRemoveCombat.Click += new EventHandler(this.ListRemoveCombat_Click);
			this.ListCreateCopy.Name = "ListCreateCopy";
			this.ListCreateCopy.Size = new Size(184, 22);
			this.ListCreateCopy.Text = "Create Duplicate";
			this.ListCreateCopy.Click += new EventHandler(this.ListCreateCopy_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new Size(181, 6);
			this.ListVisible.Name = "ListVisible";
			this.ListVisible.Size = new Size(184, 22);
			this.ListVisible.Text = "Visible";
			this.ListVisible.Click += new EventHandler(this.ListVisible_Click);
			this.ListDelay.Name = "ListDelay";
			this.ListDelay.Size = new Size(184, 22);
			this.ListDelay.Text = "Delay / Ready Action";
			this.ListDelay.Click += new EventHandler(this.ListDelay_Click);
			this.PreviewPanel.BorderStyle = BorderStyle.Fixed3D;
			this.PreviewPanel.Controls.Add(this.Preview);
			this.PreviewPanel.Dock = DockStyle.Fill;
			this.PreviewPanel.Location = new Point(0, 0);
			this.PreviewPanel.Name = "PreviewPanel";
			this.PreviewPanel.Size = new Size(354, 167);
			this.PreviewPanel.TabIndex = 1;
			this.Preview.Dock = DockStyle.Fill;
			this.Preview.IsWebBrowserContextMenuEnabled = false;
			this.Preview.Location = new Point(0, 0);
			this.Preview.MinimumSize = new Size(20, 20);
			this.Preview.Name = "Preview";
			this.Preview.ScriptErrorsSuppressed = true;
			this.Preview.Size = new Size(350, 163);
			this.Preview.TabIndex = 0;
			this.Preview.WebBrowserShortcutsEnabled = false;
			this.Preview.Navigating += new WebBrowserNavigatingEventHandler(this.Preview_Navigating);
			this.TemplatesPage.Controls.Add(this.TemplateList);
			this.TemplatesPage.Location = new Point(4, 22);
			this.TemplatesPage.Name = "TemplatesPage";
			this.TemplatesPage.Padding = new Padding(3);
			this.TemplatesPage.Size = new Size(360, 336);
			this.TemplatesPage.TabIndex = 1;
			this.TemplatesPage.Text = "Tokens and Overlays";
			this.TemplatesPage.UseVisualStyleBackColor = true;
			this.TemplateList.Columns.AddRange(new ColumnHeader[]
			{
				this.TemplateHdr
			});
			this.TemplateList.Dock = DockStyle.Fill;
			this.TemplateList.FullRowSelect = true;
			listViewGroup8.Header = "Predefined";
			listViewGroup8.Name = "listViewGroup3";
			listViewGroup9.Header = "Custom Tokens";
			listViewGroup9.Name = "listViewGroup1";
			listViewGroup10.Header = "Custom Overlays";
			listViewGroup10.Name = "listViewGroup2";
			this.TemplateList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup8,
				listViewGroup9,
				listViewGroup10
			});
			this.TemplateList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TemplateList.HideSelection = false;
			this.TemplateList.Location = new Point(3, 3);
			this.TemplateList.MultiSelect = false;
			this.TemplateList.Name = "TemplateList";
			this.TemplateList.Size = new Size(354, 330);
			this.TemplateList.TabIndex = 0;
			this.TemplateList.UseCompatibleStateImageBehavior = false;
			this.TemplateList.View = View.Details;
			this.TemplateList.ItemDrag += new ItemDragEventHandler(this.TemplateList_ItemDrag);
			this.TemplateHdr.Text = "Templates";
			this.TemplateHdr.Width = 283;
			this.LogPage.Controls.Add(this.LogBrowser);
			this.LogPage.Location = new Point(4, 22);
			this.LogPage.Name = "LogPage";
			this.LogPage.Padding = new Padding(3);
			this.LogPage.Size = new Size(360, 336);
			this.LogPage.TabIndex = 2;
			this.LogPage.Text = "Encounter Log";
			this.LogPage.UseVisualStyleBackColor = true;
			this.LogBrowser.Dock = DockStyle.Fill;
			this.LogBrowser.IsWebBrowserContextMenuEnabled = false;
			this.LogBrowser.Location = new Point(3, 3);
			this.LogBrowser.MinimumSize = new Size(20, 20);
			this.LogBrowser.Name = "LogBrowser";
			this.LogBrowser.ScriptErrorsSuppressed = true;
			this.LogBrowser.Size = new Size(354, 330);
			this.LogBrowser.TabIndex = 1;
			this.LogBrowser.WebBrowserShortcutsEnabled = false;
			this.MapView.AllowDrawing = false;
			this.MapView.AllowDrop = true;
			this.MapView.AllowLinkCreation = true;
			this.MapView.AllowScrolling = false;
			this.MapView.BackgroundMap = null;
			this.MapView.BorderSize = 0;
			this.MapView.BorderStyle = BorderStyle.Fixed3D;
			this.MapView.Caption = "";
			this.MapView.ContextMenuStrip = this.MapContext;
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
			this.MapView.ShowAllWaves = false;
			this.MapView.ShowAuras = true;
			this.MapView.ShowConditions = true;
			this.MapView.ShowCreatureLabels = true;
			this.MapView.ShowCreatures = CreatureViewMode.All;
			this.MapView.ShowGrid = MapGridMode.None;
			this.MapView.ShowGridLabels = false;
			this.MapView.ShowHealthBars = false;
			this.MapView.ShowPictureTokens = true;
			this.MapView.Size = new Size(414, 317);
			this.MapView.TabIndex = 0;
			this.MapView.Tactical = true;
			this.MapView.TokenLinks = null;
			this.MapView.Viewpoint = new Rectangle(0, 0, 0, 0);
			this.MapView.TokenDragged += new DraggedTokenEventHandler(this.MapView_TokenDragged);
			this.MapView.CancelledScrolling += new EventHandler(this.MapView_CancelledScrolling);
			this.MapView.TokenActivated += new TokenEventHandler(this.MapView_TokenActivated);
			this.MapView.CreateTokenLink += new CreateTokenLinkEventHandler(this.MapView_CreateTokenLink);
			this.MapView.EditTokenLink += new TokenLinkEventHandler(this.MapView_EditTokenLink);
			this.MapView.MouseZoomed += new MouseEventHandler(this.MapView_MouseZoomed);
			this.MapView.SelectedTokensChanged += new EventHandler(this.MapView_SelectedTokensChanged);
			this.MapView.HoverTokenChanged += new EventHandler(this.MapView_HoverTokenChanged);
			this.MapView.ItemMoved += new MovementEventHandler(this.MapView_ItemMoved);
			this.MapView.SketchCreated += new MapSketchEventHandler(this.MapView_SketchCreated);
			this.MapContext.Items.AddRange(new ToolStripItem[]
			{
				this.MapDetails,
				this.toolStripMenuItem2,
				this.MapDamage,
				this.MapHeal,
				this.MapAddEffect,
				this.MapRemoveEffect,
				this.MapSetPicture,
				this.toolStripMenuItem1,
				this.MapRemove,
				this.MapCreateCopy,
				this.toolStripSeparator2,
				this.MapVisible,
				this.MapDelay,
				this.toolStripSeparator22,
				this.MapContextDrawing,
				this.MapContextClearDrawings,
				this.toolStripSeparator25,
				this.MapContextLOS,
				this.toolStripSeparator24,
				this.MapContextOverlay
			});
			this.MapContext.Name = "MapContext";
			this.MapContext.Size = new Size(185, 348);
			this.MapContext.Opening += new CancelEventHandler(this.MapContext_Opening);
			this.MapDetails.Name = "MapDetails";
			this.MapDetails.Size = new Size(184, 22);
			this.MapDetails.Text = "Details";
			this.MapDetails.Click += new EventHandler(this.MapDetails_Click);
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new Size(181, 6);
			this.MapDamage.Name = "MapDamage";
			this.MapDamage.Size = new Size(184, 22);
			this.MapDamage.Text = "Damage...";
			this.MapDamage.Click += new EventHandler(this.MapDamage_Click);
			this.MapHeal.Name = "MapHeal";
			this.MapHeal.Size = new Size(184, 22);
			this.MapHeal.Text = "Heal...";
			this.MapHeal.Click += new EventHandler(this.MapHeal_Click);
			this.MapAddEffect.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.effectToolStripMenuItem2
			});
			this.MapAddEffect.Name = "MapAddEffect";
			this.MapAddEffect.Size = new Size(184, 22);
			this.MapAddEffect.Text = "Add Effect";
			this.MapAddEffect.DropDownOpening += new EventHandler(this.MapCondition_DropDownOpening);
			this.effectToolStripMenuItem2.Name = "effectToolStripMenuItem2";
			this.effectToolStripMenuItem2.Size = new Size(112, 22);
			this.effectToolStripMenuItem2.Text = "[effect]";
			this.MapRemoveEffect.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.effectToolStripMenuItem4
			});
			this.MapRemoveEffect.Name = "MapRemoveEffect";
			this.MapRemoveEffect.Size = new Size(184, 22);
			this.MapRemoveEffect.Text = "Remove Effect";
			this.MapRemoveEffect.DropDownOpening += new EventHandler(this.MapRemoveEffect_DropDownOpening);
			this.effectToolStripMenuItem4.Name = "effectToolStripMenuItem4";
			this.effectToolStripMenuItem4.Size = new Size(112, 22);
			this.effectToolStripMenuItem4.Text = "[effect]";
			this.MapSetPicture.Name = "MapSetPicture";
			this.MapSetPicture.Size = new Size(184, 22);
			this.MapSetPicture.Text = "Set Picture...";
			this.MapSetPicture.Click += new EventHandler(this.MapSetPicture_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new Size(181, 6);
			this.MapRemove.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.MapRemoveMap,
				this.MapRemoveCombat
			});
			this.MapRemove.Name = "MapRemove";
			this.MapRemove.Size = new Size(184, 22);
			this.MapRemove.Text = "Remove";
			this.MapRemoveMap.Name = "MapRemoveMap";
			this.MapRemoveMap.Size = new Size(192, 22);
			this.MapRemoveMap.Text = "Remove from Map";
			this.MapRemoveMap.Click += new EventHandler(this.MapRemoveMap_Click);
			this.MapRemoveCombat.Name = "MapRemoveCombat";
			this.MapRemoveCombat.Size = new Size(192, 22);
			this.MapRemoveCombat.Text = "Remove from Combat";
			this.MapRemoveCombat.Click += new EventHandler(this.MapRemoveCombat_Click);
			this.MapCreateCopy.Name = "MapCreateCopy";
			this.MapCreateCopy.Size = new Size(184, 22);
			this.MapCreateCopy.Text = "Create Duplicate";
			this.MapCreateCopy.Click += new EventHandler(this.MapCreateCopy_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(181, 6);
			this.MapVisible.Name = "MapVisible";
			this.MapVisible.Size = new Size(184, 22);
			this.MapVisible.Text = "Visible";
			this.MapVisible.Click += new EventHandler(this.MapVisible_Click);
			this.MapDelay.Name = "MapDelay";
			this.MapDelay.Size = new Size(184, 22);
			this.MapDelay.Text = "Delay / Ready Action";
			this.MapDelay.Click += new EventHandler(this.MapDelay_Click);
			this.toolStripSeparator22.Name = "toolStripSeparator22";
			this.toolStripSeparator22.Size = new Size(181, 6);
			this.MapContextDrawing.Name = "MapContextDrawing";
			this.MapContextDrawing.Size = new Size(184, 22);
			this.MapContextDrawing.Text = "Allow Drawing";
			this.MapContextDrawing.Click += new EventHandler(this.MapDrawing_Click);
			this.MapContextClearDrawings.Name = "MapContextClearDrawings";
			this.MapContextClearDrawings.Size = new Size(184, 22);
			this.MapContextClearDrawings.Text = "Clear Drawings";
			this.MapContextClearDrawings.Click += new EventHandler(this.MapClearDrawings_Click);
			this.toolStripSeparator25.Name = "toolStripSeparator25";
			this.toolStripSeparator25.Size = new Size(181, 6);
			this.MapContextLOS.Name = "MapContextLOS";
			this.MapContextLOS.Size = new Size(184, 22);
			this.MapContextLOS.Text = "Line of Sight";
			this.MapContextLOS.Click += new EventHandler(this.MapLOS_Click);
			this.toolStripSeparator24.Name = "toolStripSeparator24";
			this.toolStripSeparator24.Size = new Size(181, 6);
			this.MapContextOverlay.Name = "MapContextOverlay";
			this.MapContextOverlay.Size = new Size(184, 22);
			this.MapContextOverlay.Text = "Add Overlay...";
			this.MapContextOverlay.Click += new EventHandler(this.MapContextOverlay_Click);
			this.ZoomGauge.Dock = DockStyle.Bottom;
			this.ZoomGauge.Location = new Point(0, 317);
			this.ZoomGauge.Maximum = 100;
			this.ZoomGauge.Name = "ZoomGauge";
			this.ZoomGauge.Size = new Size(414, 45);
			this.ZoomGauge.TabIndex = 1;
			this.ZoomGauge.TickFrequency = 10;
			this.ZoomGauge.TickStyle = TickStyle.Both;
			this.ZoomGauge.Value = 50;
			this.ZoomGauge.Visible = false;
			this.ZoomGauge.Scroll += new EventHandler(this.ZoomGauge_Scroll);
			this.MapTooltip.ToolTipIcon = ToolTipIcon.Info;
			this.Statusbar.Items.AddRange(new ToolStripItem[]
			{
				this.RoundLbl,
				this.XPLbl,
				this.LevelLbl
			});
			this.Statusbar.Location = new Point(0, 362);
			this.Statusbar.Name = "Statusbar";
			this.Statusbar.Size = new Size(826, 22);
			this.Statusbar.SizingGrip = false;
			this.Statusbar.TabIndex = 0;
			this.Statusbar.Text = "statusStrip1";
			this.RoundLbl.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
			this.RoundLbl.Name = "RoundLbl";
			this.RoundLbl.Size = new Size(48, 17);
			this.RoundLbl.Text = "[round]";
			this.XPLbl.Name = "XPLbl";
			this.XPLbl.Size = new Size(27, 17);
			this.XPLbl.Text = "[xp]";
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(39, 17);
			this.LevelLbl.Text = "[level]";
			this.MainPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.MainPanel.Controls.Add(this.MapSplitter);
			this.MainPanel.Controls.Add(this.InitiativePanel);
			this.MainPanel.Controls.Add(this.Statusbar);
			this.MainPanel.Location = new Point(12, 28);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new Size(826, 384);
			this.MainPanel.TabIndex = 1;
			this.InitiativePanel.BorderStyle = BorderStyle.Fixed3D;
			this.InitiativePanel.CurrentInitiative = 0;
			this.InitiativePanel.Dock = DockStyle.Right;
			this.InitiativePanel.InitiativeScores = (List<int>)resources.GetObject("InitiativePanel.InitiativeScores");
			this.InitiativePanel.Location = new Point(786, 0);
			this.InitiativePanel.Name = "InitiativePanel";
			this.InitiativePanel.Size = new Size(40, 362);
			this.InitiativePanel.TabIndex = 2;
			this.InitiativePanel.InitiativeChanged += new EventHandler(this.InitiativePanel_InitiativeChanged);
			this.CloseBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CloseBtn.DialogResult = DialogResult.OK;
			this.CloseBtn.Location = new Point(718, 418);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new Size(120, 23);
			this.CloseBtn.TabIndex = 6;
			this.CloseBtn.Text = "End Encounter";
			this.CloseBtn.UseVisualStyleBackColor = true;
			this.CloseBtn.Click += new EventHandler(this.CloseBtn_Click);
			this.PauseBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.PauseBtn.Location = new Point(592, 418);
			this.PauseBtn.Name = "PauseBtn";
			this.PauseBtn.Size = new Size(120, 23);
			this.PauseBtn.TabIndex = 5;
			this.PauseBtn.Text = "Pause Encounter";
			this.PauseBtn.UseVisualStyleBackColor = true;
			this.PauseBtn.Click += new EventHandler(this.PauseBtn_Click);
			this.InfoBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.InfoBtn.Location = new Point(12, 418);
			this.InfoBtn.Name = "InfoBtn";
			this.InfoBtn.Size = new Size(75, 23);
			this.InfoBtn.TabIndex = 2;
			this.InfoBtn.Text = "Information";
			this.InfoBtn.UseVisualStyleBackColor = true;
			this.InfoBtn.Click += new EventHandler(this.InfoBtn_Click);
			this.DieRollerBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.DieRollerBtn.Location = new Point(93, 418);
			this.DieRollerBtn.Name = "DieRollerBtn";
			this.DieRollerBtn.Size = new Size(75, 23);
			this.DieRollerBtn.TabIndex = 3;
			this.DieRollerBtn.Text = "Die Roller";
			this.DieRollerBtn.UseVisualStyleBackColor = true;
			this.DieRollerBtn.Click += new EventHandler(this.DieRollerBtn_Click);
			this.ReportBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.ReportBtn.Location = new Point(174, 418);
			this.ReportBtn.Name = "ReportBtn";
			this.ReportBtn.Size = new Size(75, 23);
			this.ReportBtn.TabIndex = 4;
			this.ReportBtn.Text = "Report";
			this.ReportBtn.UseVisualStyleBackColor = true;
			this.ReportBtn.Click += new EventHandler(this.ReportBtn_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(850, 453);
			base.Controls.Add(this.ReportBtn);
			base.Controls.Add(this.DieRollerBtn);
			base.Controls.Add(this.Toolbar);
			base.Controls.Add(this.InfoBtn);
			base.Controls.Add(this.MainPanel);
			base.Controls.Add(this.CloseBtn);
			base.Controls.Add(this.PauseBtn);
			base.Icon = (Icon)resources.GetObject("$this.Icon");
			base.Name = "CombatForm";
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Combat Encounter";
			base.Shown += new EventHandler(this.CombatForm_Shown);
			base.FormClosing += new FormClosingEventHandler(this.CombatForm_FormClosing);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.MapSplitter.Panel1.ResumeLayout(false);
			this.MapSplitter.Panel2.ResumeLayout(false);
			this.MapSplitter.Panel2.PerformLayout();
			this.MapSplitter.ResumeLayout(false);
			this.Pages.ResumeLayout(false);
			this.CombatantsPage.ResumeLayout(false);
			this.ListSplitter.Panel1.ResumeLayout(false);
			this.ListSplitter.Panel2.ResumeLayout(false);
			this.ListSplitter.ResumeLayout(false);
			this.ListContext.ResumeLayout(false);
			this.PreviewPanel.ResumeLayout(false);
			this.TemplatesPage.ResumeLayout(false);
			this.LogPage.ResumeLayout(false);
			this.MapContext.ResumeLayout(false);
			((ISupportInitialize)this.ZoomGauge).EndInit();
			this.Statusbar.ResumeLayout(false);
			this.Statusbar.PerformLayout();
			this.MainPanel.ResumeLayout(false);
			this.MainPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
