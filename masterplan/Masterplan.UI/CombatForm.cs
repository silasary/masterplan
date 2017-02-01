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
    internal partial class CombatForm : Form
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
                int num = this.GetScore(lvi_x);
                int value = this.GetScore(lvi_y);
                int num2 = num.CompareTo(value);
                if (num2 == 0)
                {
                    int num3 = this.GetBonus(lvi_x);
                    int value2 = this.GetBonus(lvi_y);
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

            private int GetScore(ListViewItem lvi)
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
                        result = int.MinValue;
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    LogSystem.Trace(ex);
                }
                return 0;
            }

            private int GetBonus(ListViewItem lvi)
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
                        int result = (trap.Initiative != int.MinValue) ? trap.Initiative : 0;
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
                foreach (var hero in Session.Project.Heroes)
                {
                    if (cs.HeroData.ContainsKey(hero.ID))
                    {
                        hero.CombatData = cs.HeroData[hero.ID];
                    }
                }
            }
            else
            {
                foreach (Hero current3 in Session.Project.Heroes)
                {
                    current3.CombatData.Location = CombatData.NoPoint;
                }
            }
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
                    CombatData combatData = new CombatData()
                    {
                        DisplayName = current5.Name,
                        ID = current5.ID
                    };
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
                    CustomToken customToken = new CustomToken()
                    {
                        Type = CustomTokenType.Token,
                        TokenSize = creatureSize,
                        Colour = Color.Black,
                        Name = creatureSize + " Token"
                    };
                    ListViewItem listViewItem2 = this.TemplateList.Items.Add(customToken.Name);
                    listViewItem2.Tag = customToken;
                    listViewItem2.Group = this.TemplateList.Groups[1];
                }
                for (int i = 2; i <= 10; i++)
                {
                    CustomToken customToken2 = new CustomToken()
                    {
                        Type = CustomTokenType.Overlay,
                        OverlaySize = new Size(i, i),
                        Name = $"{i}x{i} Zone",
                        Colour = Color.Transparent
                    };
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
            this.SetMap(cs.TokenLinks, cs.Viewpoint, cs.Sketches);
            this.MapMenu.Visible = (this.fEncounter.MapID != Guid.Empty);
            this.InitiativePanel.InitiativeScores = this.GetInitiatives();
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
            this.UpdateList();
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
                    this.EditToken(this.SelectedTokens[0]);
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
                this.DoDamage(this.SelectedTokens);
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
                this.DoHeal(this.SelectedTokens);
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
                this.SetDelay(this.SelectedTokens);
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
                    this.StartCombat();
                }
                else
                {
                    List<int> initiatives = this.GetInitiatives();
                    if (initiatives.Count != 0)
                    {
                        this.handle_ended_effects(false);
                        this.HandleSaves();
                        this.fCurrentActor = this.GetNextActor(this.fCurrentActor);
                        this.fLog.AddStartTurnEntry(this.fCurrentActor.ID);
                        if (this.fCurrentActor.Initiative > this.InitiativePanel.CurrentInitiative)
                        {
                            this.fCurrentRound++;
                            this.RoundLbl.Text = "Round: " + this.fCurrentRound;
                            this.fLog.AddStartRoundEntry(this.fCurrentRound);
                        }
                        this.InitiativePanel.CurrentInitiative = this.fCurrentActor.Initiative;
                        this.HandleRegen();
                        this.handle_ended_effects(true);
                        this.handle_ongoing_damage();
                        this.handle_recharge();
                        if (this.fCurrentActor != null && !this.TwoColumnPreview)
                        {
                            this.SelectCurrentActor();
                        }
                        this.UpdateList();
                        this.update_log();
                        this.update_preview_panel();
                        this.HighlightCurrentActor();
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
                            this.RollInitiative();
                        }
                    }
                    foreach (Trap current2 in encounterBuilderForm.Encounter.Traps)
                    {
                        if (current2.Initiative != int.MinValue)
                        {
                            this.fTrapData[current2.ID] = new CombatData();
                            if (this.fCombatStarted)
                            {
                                this.RollInitiative();
                            }
                        }
                        this.fEncounter.Traps.Add(current2);
                    }
                    foreach (SkillChallenge current3 in encounterBuilderForm.Encounter.SkillChallenges)
                    {
                        this.fEncounter.SkillChallenges.Add(current3);
                    }
                    this.UpdateList();
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
                    this.UpdateList();
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
                    this.UpdateList();
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
                this.RemoveFromCombat(this.SelectedTokens);
            }
        }

        private void CombatantsHideAll_Click(object sender, EventArgs e)
        {
            this.ShowOrHideAll(false);
        }

        private void CombatantsShowAll_Click(object sender, EventArgs e)
        {
            this.ShowOrHideAll(true);
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
                this.UpdateList();
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
                this.UpdateList();
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
                this.UpdateList();
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
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    FileName = this.MapView.Map.Name
                };
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
                this.ShowPlayerView(this.PlayerMap == null, this.PlayerInitiative != null);
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
                this.ShowPlayerView(this.PlayerMap != null, this.PlayerInitiative == null);
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
                    this.UpdateList();
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
                            if (current2.Initiative != int.MinValue)
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
                            this.UpdateList();
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
                                this.UpdateList();
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
                            this.UpdateList();
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
                    this.EditToken(this.SelectedTokens[0]);
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
                    this.SelectToken(current);
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
                this.SetTooltip(this.MapView.HoverToken, this.MapView);
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
                    this.DoDamage(new List<IToken>
                    {
                        e.Token
                    });
                }
                if (e.Token is CustomToken)
                {
                    this.EditToken(e.Token);
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
                                this.RollAttack(creaturePower);
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
                                this.RollCheck(trapAttack.Attack.ToString(), trapAttack.Attack.Bonus);
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
                    this.RollCheck("Ability", mod);
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
                                this.UpdateList();
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
                        this.UpdateList();
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
                        this.UpdateList();
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
                        this.DoDamage(list);
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
                        this.UpdateList();
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
                        this.UpdateList();
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
                        this.DoHeal(list2);
                    }
                }
                if (e.Url.Scheme == "init")
                {
                    e.Cancel = true;
                    Guid guid4 = new Guid(e.Url.LocalPath);
                    int num = int.MinValue;
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
                    if (combatData7 != null && num != int.MinValue)
                    {
                        InitiativeForm initiativeForm = new InitiativeForm(num, combatData7.Initiative);
                        if (initiativeForm.ShowDialog() == DialogResult.OK)
                        {
                            combatData7.Initiative = initiativeForm.Score;
                            this.InitiativePanel.InitiativeScores = this.GetInitiatives();
                            if (this.fCurrentActor != null)
                            {
                                this.InitiativePanel.CurrentInitiative = this.fCurrentActor.Initiative;
                            }
                            this.UpdateList();
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
                            this.UpdateList();
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
                    this.UpdateList();
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
                        this.UpdateList();
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
                        this.UpdateList();
                        this.update_preview_panel();
                        this.update_maps();
                    }
                    if (e.Url.LocalPath == "start")
                    {
                        this.StartCombat();
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
                this.fCurrentActor = this.GetNextActor(null);
                if (this.fCurrentActor.ID != iD)
                {
                    this.fLog.AddStartTurnEntry(this.fCurrentActor.ID);
                }
                this.UpdateList();
                this.update_log();
                this.update_preview_panel();
                this.update_maps();
                this.HighlightCurrentActor();
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
            this.UpdateList();
            this.update_preview_panel();
            this.update_maps();
        }

        private void CombatantsLinks_Click(object sender, EventArgs e)
        {
            TokenLinkListForm tokenLinkListForm = new TokenLinkListForm(this.MapView.TokenLinks);
            tokenLinkListForm.ShowDialog();
            this.UpdateList();
            this.update_preview_panel();
            this.update_maps();
        }

        private void AddInCommand_clicked(object sender, EventArgs e)
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
            this.SetDelay(this.SelectedTokens);
        }

        private void MapDelay_Click(object sender, EventArgs e)
        {
            this.SetDelay(this.MapView.SelectedTokens);
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
            CustomToken customToken = new CustomToken()
            {
                Name = "New Overlay",
                Type = CustomTokenType.Overlay
            };
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
                this.UpdateList();
                this.update_maps();
            }
        }

        private void MapHeal_Click(object sender, EventArgs e)
        {
            try
            {
                this.DoHeal(this.MapView.SelectedTokens);
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
                this.DoHeal(this.SelectedTokens);
            }
            catch (Exception ex)
            {
                LogSystem.Trace(ex);
            }
        }

        private void ListCreateCopy_Click(object sender, EventArgs e)
        {
            this.CopyCustomToken();
        }

        private void MapCreateCopy_Click(object sender, EventArgs e)
        {
            this.CopyCustomToken();
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
                    OpenFileDialog openFileDialog = new OpenFileDialog()
                    {
                        Filter = Program.ImageFilter
                    };
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
                        this.UpdateList();
                    }
                }
            }
            Hero hero = this.MapView.SelectedTokens[0] as Hero;
            if (hero != null)
            {
                OpenFileDialog openFileDialog2 = new OpenFileDialog()
                {
                    Filter = Program.ImageFilter
                };
                if (openFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    hero.Portrait = Image.FromFile(openFileDialog2.FileName);
                    Program.SetResolution(hero.Portrait);
                    Session.Modified = true;
                    this.UpdateList();
                }
            }
        }

        private void PlayerViewNoMapShowInitiativeList_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowPlayerView(false, this.PlayerInitiative == null);
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
                this.UpdateList();
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
            this.ListSplitterChanged();
        }

        private void ListSplitter_Resize(object sender, EventArgs e)
        {
            this.ListSplitterChanged();
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
                this.EditToken(this.SelectedTokens[0]);
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
                this.DoDamage(this.SelectedTokens);
            }
            catch (Exception ex)
            {
                LogSystem.Trace(ex);
            }
        }

        private void ListRemoveMap_Click(object sender, EventArgs e)
        {
            this.RemoveFromMap(this.SelectedTokens);
        }

        private void ListRemoveCombat_Click(object sender, EventArgs e)
        {
            this.RemoveFromCombat(this.SelectedTokens);
        }

        private void ListVisible_Click(object sender, EventArgs e)
        {
            this.ToggleVisibility(this.SelectedTokens);
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
                    this.EditToken(this.MapView.SelectedTokens[0]);
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
                this.DoDamage(this.MapView.SelectedTokens);
            }
            catch (Exception ex)
            {
                LogSystem.Trace(ex);
            }
        }

        private void MapRemoveMap_Click(object sender, EventArgs e)
        {
            this.RemoveFromMap(this.MapView.SelectedTokens);
        }

        private void MapRemoveCombat_Click(object sender, EventArgs e)
        {
            this.RemoveFromCombat(this.MapView.SelectedTokens);
        }

        private void MapVisible_Click(object sender, EventArgs e)
        {
            this.ToggleVisibility(this.MapView.SelectedTokens);
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
                    ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(current.Name)
                    {
                        Checked = current.Active,
                        Tag = current
                    };
                    toolStripMenuItem.Click += new EventHandler(WaveActivated);
                    this.CombatantsWaves.DropDownItems.Add(toolStripMenuItem);
                }
            }
            if (this.CombatantsWaves.DropDownItems.Count == 0)
            {
                ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem("(none set)")
                {
                    Enabled = false
                };
                this.CombatantsWaves.DropDownItems.Add(toolStripMenuItem2);
            }
        }

        private void WaveActivated(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
            EncounterWave encounterWave = toolStripMenuItem.Tag as EncounterWave;
            encounterWave.Active = !encounterWave.Active;
            this.UpdateList();
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
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(current.Name)
                {
                    ToolTipText = TextHelper.Wrap(current.Description),
                    Tag = current
                };
                this.ToolsAddIns.DropDownItems.Add(toolStripMenuItem);
                foreach (ICommand current2 in current.CombatCommands)
                {
                    ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(current2.Name)
                    {
                        ToolTipText = TextHelper.Wrap(current2.Description),
                        Enabled = current2.Available,
                        Checked = current2.Active
                    };
                    toolStripMenuItem2.Click += new EventHandler(this.AddInCommand_clicked);
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
            this.UpdateRemoveEffectList(this.ListRemoveEffect, true);
        }

        private void MapRemoveEffect_DropDownOpening(object sender, EventArgs e)
        {
            this.UpdateRemoveEffectList(this.MapRemoveEffect, false);
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

        private void StartCombat()
        {
            this.RollInitiative();
            List<int> initiatives = this.GetInitiatives();
            if (initiatives.Count != 0)
            {
                int init = initiatives[0];
                List<CombatData> list = this.GetCombatants(init, false);
                if (list.Count != 0)
                {
                    this.fCurrentActor = list[0];
                }
                if (this.fCurrentActor != null)
                {
                    this.fCombatStarted = true;
                    this.InitiativePanel.CurrentInitiative = this.fCurrentActor.Initiative;
                    this.SelectCurrentActor();
                    this.UpdateList();
                    this.update_maps();
                    this.update_statusbar();
                    this.update_preview_panel();
                    this.HighlightCurrentActor();
                    this.fLog.Active = true;
                    this.fLog.AddStartRoundEntry(this.fCurrentRound);
                    this.fLog.AddStartTurnEntry(this.fCurrentActor.ID);
                    this.update_log();
                }
            }
        }

        private void RollInitiative()
        {
            List<Pair<List<CombatData>, int>> list = new List<Pair<List<CombatData>, int>>();
            Dictionary<string, List<CombatData>> dictionary = new Dictionary<string, List<CombatData>>();
            foreach (Hero current in Session.Project.Heroes)
            {
                if (current.CombatData.Initiative == int.MinValue)
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
                                if (current3.Initiative == int.MinValue)
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
                                if (current4.Initiative == int.MinValue)
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
                            if (current5.Initiative == int.MinValue)
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
                    if (current6.Initiative == int.MinValue)
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
                bool flag = current7.Initiative != int.MinValue;
                if (flag)
                {
                    CombatData combatData = this.fTrapData[current7.ID];
                    if (combatData.Initiative == int.MinValue)
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
            this.InitiativePanel.InitiativeScores = this.GetInitiatives();
        }

        private void SelectCurrentActor()
        {
            foreach (ListViewItem listViewItem in this.CombatList.Items)
            {
                listViewItem.Selected = false;
            }
            ListViewItem listViewItem2 = this.GetCombatant(this.fCurrentActor.ID);
            if (listViewItem2 != null)
            {
                listViewItem2.Selected = true;
            }
        }

        private void SetMap(List<TokenLink> token_links, Rectangle viewpoint, List<MapSketch> sketches)
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

        private void DoDamage(List<IToken> tokens)
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
                dictionary2[current3.First] = this.GetState(current3.First);
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
                    CreatureState creatureState = this.GetState(current4.First);
                    if (creatureState != dictionary2[current4.First])
                    {
                        this.fLog.AddStateEntry(current4.First.ID, creatureState);
                    }
                }
                this.UpdateList();
                this.update_log();
                this.update_preview_panel();
                this.update_maps();
            }
        }

        private void DoHeal(List<IToken> tokens)
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
                dictionary2[current3.First] = this.GetState(current3.First);
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
                    CreatureState creatureState = this.GetState(current4.First);
                    if (creatureState != dictionary2[current4.First])
                    {
                        this.fLog.AddStateEntry(current4.First.ID, creatureState);
                    }
                }
                this.UpdateList();
                this.update_log();
                this.update_preview_panel();
                this.update_maps();
            }
        }

        private void CopyCustomToken()
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
            this.UpdateList();
        }

        private void ShowPlayerView(bool map, bool initiative)
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

        private void ListSplitterChanged()
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

        private void SelectToken(IToken token)
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

        private void SetDelay(List<IToken> tokens)
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
                            this.InitiativePanel.InitiativeScores = this.GetInitiatives();
                        }
                        else
                        {
                            combatData.Initiative = this.InitiativePanel.CurrentInitiative;
                        }
                    }
                }
                this.UpdateList();
            }
            catch (Exception ex)
            {
                LogSystem.Trace(ex);
            }
        }

        private string GetInfo(CreatureToken token)
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

        private string GetInfo(Hero hero)
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

        private string GetInfo(CustomToken token)
        {
            if (!(token.Details != ""))
            {
                return "(no details)";
            }
            return token.Details;
        }

        private void EditToken(IToken token)
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
                    this.UpdateList();
                    this.update_log();
                    this.update_preview_panel();
                    this.update_maps();
                    this.InitiativePanel.InitiativeScores = this.GetInitiatives();
                }
            }
            if (token is Hero)
            {
                Hero hero = token as Hero;
                if (hero.CombatData.Initiative == int.MinValue)
                {
                    this.EditInitiative(hero);
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
                        this.UpdateList();
                        this.update_log();
                        this.update_preview_panel();
                        this.update_maps();
                        this.InitiativePanel.InitiativeScores = this.GetInitiatives();
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
                                    this.UpdateList();
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
                                    this.UpdateList();
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

        private void SetTooltip(IToken token, Control ctrl)
        {
            string toolTipTitle = "";
            string caption = null;
            if (token is CreatureToken)
            {
                CreatureToken creatureToken = token as CreatureToken;
                toolTipTitle = creatureToken.Data.DisplayName;
                caption = this.GetInfo(creatureToken);
            }
            if (token is Hero)
            {
                Hero hero = token as Hero;
                toolTipTitle = hero.Name;
                caption = this.GetInfo(hero);
            }
            if (token is CustomToken)
            {
                CustomToken customToken = token as CustomToken;
                toolTipTitle = customToken.Name;
                caption = this.GetInfo(customToken);
            }
            this.MapTooltip.ToolTipTitle = toolTipTitle;
            this.MapTooltip.SetToolTip(ctrl, caption);
        }

        private void RemoveFromMap(List<IToken> tokens)
        {
            try
            {
                foreach (IToken current in tokens)
                {
                    if (current is CreatureToken)
                    {
                        CreatureToken creatureToken = current as CreatureToken;
                        creatureToken.Data.Location = CombatData.NoPoint;
                        this.RemoveEffects(current);
                        this.RemoveLinks(current);
                    }
                    if (current is Hero)
                    {
                        Hero hero = current as Hero;
                        hero.CombatData.Location = CombatData.NoPoint;
                        this.RemoveEffects(current);
                        this.RemoveLinks(current);
                    }
                    if (current is CustomToken)
                    {
                        CustomToken customToken = current as CustomToken;
                        customToken.Data.Location = CombatData.NoPoint;
                        if (customToken.Type == CustomTokenType.Token)
                        {
                            this.RemoveLinks(current);
                        }
                    }
                }
                this.UpdateList();
                this.update_preview_panel();
                this.update_maps();
            }
            catch (Exception ex)
            {
                LogSystem.Trace(ex);
            }
        }

        private void RemoveFromCombat(List<IToken> tokens)
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
                        this.RemoveEffects(current);
                        this.RemoveLinks(current);
                    }
                    if (current is Hero)
                    {
                        Hero hero = current as Hero;
                        hero.CombatData.Initiative = int.MinValue;
                        hero.CombatData.Location = CombatData.NoPoint;
                        this.RemoveEffects(current);
                        this.RemoveLinks(current);
                    }
                    if (current is CustomToken)
                    {
                        CustomToken customToken = current as CustomToken;
                        this.fEncounter.CustomTokens.Remove(customToken);
                        if (customToken.Type == CustomTokenType.Token)
                        {
                            this.RemoveLinks(current);
                        }
                    }
                }
                this.UpdateList();
                this.update_preview_panel();
                this.update_maps();
            }
            catch (Exception ex)
            {
                LogSystem.Trace(ex);
            }
        }

        private void RemoveEffects(IToken token)
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
                RemoveEffects(guid, combatData);
            }
            foreach (EncounterSlot current2 in this.fEncounter.AllSlots)
            {
                foreach (CombatData current3 in current2.CombatData)
                {
                    RemoveEffects(guid, current3);
                }
            }
        }

        private void RemoveEffects(Guid token_id, CombatData data)
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

        private void RemoveLinks(IToken token)
        {
            Point right = this.GetLocation(token);
            List<TokenLink> list = new List<TokenLink>();
            foreach (TokenLink current in this.MapView.TokenLinks)
            {
                foreach (IToken current2 in current.Tokens)
                {
                    if (this.GetLocation(current2) == right)
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

        private Point GetLocation(IToken token)
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

        private void ToggleVisibility(List<IToken> tokens)
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
                this.UpdateList();
                this.update_preview_panel();
                this.update_maps();
            }
            catch (Exception ex)
            {
                LogSystem.Trace(ex);
            }
        }

        private void ShowOrHideAll(bool visible)
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
            this.UpdateList();
            this.update_preview_panel();
            this.update_maps();
        }

        private void RollAttack(CreaturePower power)
        {
            AttackRollForm attackRollForm = new AttackRollForm(power, this.fEncounter);
            attackRollForm.ShowDialog();
            this.UpdateList();
            this.update_log();
            this.update_preview_panel();
            this.update_maps();
        }

        private void RollCheck(string name, int mod)
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

        private bool EditInitiative(Hero hero)
        {
            int initiative = hero.CombatData.Initiative;
            InitiativeForm initiativeForm = new InitiativeForm(hero.InitBonus, initiative);
            if (initiativeForm.ShowDialog() == DialogResult.OK)
            {
                hero.CombatData.Initiative = initiativeForm.Score;
                this.UpdateList();
                this.update_preview_panel();
                this.update_maps();
                this.update_statusbar();
                List<int> initiatives = this.GetInitiatives();
                this.InitiativePanel.InitiativeScores = initiatives;
                int arg_67_0 = initiatives[0];
                return true;
            }
            return false;
        }

        private int NextInit(int current_init)
        {
            List<int> initiatives = this.GetInitiatives();
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

        private int FindMaxInitiative()
        {
            List<int> initiatives = this.GetInitiatives();
            if (initiatives.Count != 0)
            {
                return initiatives[0];
            }
            return 0;
        }

        private int FindMinInitiative()
        {
            List<int> initiatives = this.GetInitiatives();
            if (initiatives.Count != 0)
            {
                return initiatives[initiatives.Count - 1];
            }
            return 0;
        }

        private List<int> GetInitiatives()
        {
            List<int> list = new List<int>();
            foreach (EncounterSlot current in this.fEncounter.AllSlots)
            {
                foreach (CombatData current2 in current.CombatData)
                {
                    if (current.GetState(current2) != CreatureState.Defeated)
                    {
                        int initiative = current2.Initiative;
                        if (initiative != int.MinValue && !list.Contains(initiative))
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
                if (initiative2 != int.MinValue && !list.Contains(initiative2))
                {
                    list.Add(initiative2);
                }
            }
            foreach (CombatData current4 in this.fTrapData.Values)
            {
                if (!current4.Delaying)
                {
                    int initiative3 = current4.Initiative;
                    if (initiative3 != int.MinValue && !list.Contains(initiative3))
                    {
                        list.Add(initiative3);
                    }
                }
            }
            list.Sort();
            list.Reverse();
            return list;
        }

        private void HandleRegen()
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
            Regeneration regeneration = new Regeneration()
            {
                Value = 0
            };
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
                this.UpdateList();
            }
        }

        private void HandleSaves()
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
                this.UpdateList();
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
                this.UpdateList();
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
                this.UpdateList();
            }
        }

        private CombatData GetNextActor(CombatData current_actor)
        {
            int num = (current_actor != null) ? current_actor.Initiative : this.InitiativePanel.CurrentInitiative;
            List<int> initiatives = this.GetInitiatives();
            if (!initiatives.Contains(num))
            {
                num = this.NextInit(num);
            }
            List<CombatData> list = this.GetCombatants(num, true);
            int num2 = list.IndexOf(current_actor);
            CombatData combatData;
            if (num2 == -1)
            {
                combatData = list[0];
            }
            else if (num2 == list.Count - 1)
            {
                num = this.NextInit(num);
                list = this.GetCombatants(num, false);
                combatData = list[0];
            }
            else
            {
                combatData = list[num2 + 1];
            }
            bool flag = this.GetState(combatData) == CreatureState.Defeated;
            bool flag2 = combatData != null && combatData.Delaying;
            if (flag || flag2)
            {
                combatData = this.GetNextActor(combatData);
            }
            return combatData;
        }

        private CreatureState GetState(CombatData cd)
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

        private List<CombatData> GetCombatants(int init, bool include_defeated)
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
                if (current4.Initiative != int.MinValue && this.fTrapData.ContainsKey(current4.ID))
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

        private void HighlightCurrentActor()
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

        private ListViewItem GetCombatant(Guid id)
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

        private void UpdateList()
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
                                this.fCurrentActor = this.GetNextActor(this.fCurrentActor);
                                if (this.fCurrentActor.ID != iD)
                                {
                                    this.fLog.AddStartTurnEntry(this.fCurrentActor.ID);
                                    this.update_log();
                                }
                            }
                            CreatureToken token = new CreatureToken(current.ID, current3);
                            this.RemoveEffects(token);
                            this.RemoveLinks(token);
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
            List<IToken> selectedTokens = SelectedTokens;
            Trap selectedTrap = SelectedTrap;
            SkillChallenge selectedChallenge = SelectedChallenge;
            CombatList.BeginUpdate();
            CombatList.Items.Clear();
            CombatList.SmallImageList = new ImageList()
            {
                ImageSize = new Size(16, 16)
            };
            foreach (EncounterSlot current4 in this.fEncounter.AllSlots)
            {
                EncounterWave encounterWave = this.fEncounter.FindWave(current4);
                if (encounterWave == null || encounterWave.Active)
                {
                    int maxHp = current4.Card.HP;
                    ICreature creature = Session.FindCreature(current4.Card.CreatureID, SearchType.Global);
                    foreach (CombatData current5 in current4.CombatData)
                    {
                        int currentHp = maxHp - current5.Damage;
                        string hpString = currentHp.ToString();
                        if (current5.TempHP > 0)
                        {
                            hpString = $"{currentHp} (+{current5.TempHP})";
                        }
                        if (currentHp != maxHp)
                        {
                            hpString = $"{currentHp} / {maxHp}";
                        }
                        string initiativeString = current5.Initiative.ToString();
                        if (current5.Delaying)
                        {
                            initiativeString = "(" + initiativeString + ")";
                        }
                        ListViewItem listViewItem = this.CombatList.Items.Add(current5.DisplayName);
                        listViewItem.Tag = new CreatureToken(current4.ID, current5);
                        if (current5.Initiative == int.MinValue)
                        {
                            listViewItem.ForeColor = SystemColors.GrayText;
                            initiativeString = "-";
                        }
                        int defAC = current4.Card.AC;
                        int defFort = current4.Card.Fortitude;
                        int defRef = current4.Card.Reflex;
                        int defWill = current4.Card.Will;
                        foreach (OngoingCondition current6 in current5.Conditions)
                        {
                            if (current6.Type == OngoingType.DefenceModifier)
                            {
                                if (current6.Defences.Contains(DefenceType.AC))
                                {
                                    defAC += current6.DefenceMod;
                                }
                                if (current6.Defences.Contains(DefenceType.Fortitude))
                                {
                                    defFort += current6.DefenceMod;
                                }
                                if (current6.Defences.Contains(DefenceType.Reflex))
                                {
                                    defRef += current6.DefenceMod;
                                }
                                if (current6.Defences.Contains(DefenceType.Will))
                                {
                                    defWill += current6.DefenceMod;
                                }
                            }
                        }
                        string text3 = string.Concat(new object[]
                        {
                            "AC ",
                            defAC,
                            ", Fort ",
                            defFort,
                            ", Ref ",
                            defRef,
                            ", Will ",
                            defWill
                        });
                        string text4 = this.GetConditions(current5);
                        listViewItem.SubItems.Add(initiativeString);
                        listViewItem.SubItems.Add(hpString);
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
                        if (current5.Initiative == int.MinValue)
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
                        foreach (IToken token in selectedTokens)
                        {
                            CreatureToken creatureToken = token as CreatureToken;
                            if (creatureToken != null && creatureToken.Data == current5)
                            {
                                listViewItem.Selected = true;
                            }
                        }
                    }
                }
            }
            foreach (Trap trap in this.fEncounter.Traps)
            {
                ListViewItem listViewItem2 = this.CombatList.Items.Add(trap.Name);
                listViewItem2.Tag = trap;
                this.add_icon(listViewItem2, Color.White);
                if (trap.Initiative != int.MinValue)
                {
                    CombatData combatData = this.fTrapData[trap.ID];
                    if (combatData != null && combatData.Initiative != int.MinValue)
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
                if (trap == selectedTrap)
                {
                    listViewItem2.Selected = true;
                }
            }
            foreach (SkillChallenge skillChallenge in this.fEncounter.SkillChallenges)
            {
                ListViewItem listViewItem3 = this.CombatList.Items.Add(skillChallenge.Name);
                listViewItem3.SubItems.Add("-");
                listViewItem3.SubItems.Add("-");
                listViewItem3.SubItems.Add("-");
                listViewItem3.SubItems.Add(string.Concat(new object[]
                {
                    skillChallenge.Results.Successes,
                    " / ",
                    skillChallenge.Successes,
                    " successes; ",
                    skillChallenge.Results.Fails,
                    " / 3 failures"
                }));
                this.add_icon(listViewItem3, Color.White);
                listViewItem3.Tag = skillChallenge;
                listViewItem3.Group = this.CombatList.Groups[index2];
                if (skillChallenge == selectedChallenge)
                {
                    listViewItem3.Selected = true;
                }
            }
            foreach (Hero hero in Session.Project.Heroes)
            {
                int index4 = num;
                ListViewItem listViewItem4 = this.CombatList.Items.Add(hero.Name);
                listViewItem4.Tag = hero;
                CombatData combatData2 = hero.CombatData;
                switch (hero.GetState(combatData2.Damage))
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
                if (hero.Portrait != null)
                {
                    this.CombatList.SmallImageList.Images.Add(new Bitmap(hero.Portrait, 16, 16));
                    listViewItem4.ImageIndex = this.CombatList.SmallImageList.Images.Count - 1;
                }
                else if (hero.Key != "")
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
                if (initiative == int.MinValue)
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
                if (hero.HP != 0)
                {
                    int num11 = hero.HP - combatData2.Damage;
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
                    if (num11 != hero.HP)
                    {
                        text7 = text7 + " / " + hero.HP;
                    }
                }
                else
                {
                    text7 = "-";
                }
                listViewItem4.SubItems.Add(text6);
                listViewItem4.SubItems.Add(text7);
                if (hero.AC != 0 && hero.Fortitude != 0 && hero.Reflex != 0 && hero.Will != 0)
                {
                    int num12 = hero.AC;
                    int num13 = hero.Fortitude;
                    int num14 = hero.Reflex;
                    int num15 = hero.Will;
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
                listViewItem4.SubItems.Add(this.GetConditions(combatData2));
                if (this.MapView.Map != null && hero.CombatData.Location == CombatData.NoPoint)
                {
                    index4 = num4;
                }
                listViewItem4.Group = this.CombatList.Groups[index4];
                if (selectedTokens.Contains(hero))
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

        private string GetConditions(CombatData cd)
        {
            string text = "";
            bool hasOngoingDmg = false;
            foreach (OngoingCondition condition in cd.Conditions)
            {
                if (condition.Type == OngoingType.Damage)
                {
                    hasOngoingDmg = true;
                    break;
                }
            }
            if (hasOngoingDmg)
            {
                if (text != "")
                {
                    text += "; ";
                }
                text += "Damage";
            }
            foreach (OngoingCondition condition in cd.Conditions)
            {
                if (condition.Type != OngoingType.Damage)
                {
                    if (text != "")
                    {
                        text += "; ";
                    }
                    switch (condition.Type)
                    {
                        case OngoingType.Condition:
                            text += condition.Data;
                            break;
                        case OngoingType.DefenceModifier:
                            text += condition.ToString(this.fEncounter, false);
                            break;
                    }
                }
            }
            return text;
        }

        private void add_icon(ListViewItem lvi, Color c)
        {
            Image icon = new Bitmap(16, 16);
            Graphics graphics = Graphics.FromImage(icon);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.FillEllipse(new SolidBrush(c), 2, 2, 12, 12);
            if (c == Color.White)
            {
                graphics.DrawEllipse(Pens.Black, 2, 2, 12, 12);
            }
            this.CombatList.SmallImageList.Images.Add(icon);
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
                if (current.CombatData.Initiative != int.MinValue)
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
                OngoingCondition ongoingCondition = new OngoingCondition()
                {
                    Data = current,
                    Duration = DurationType.Encounter
                };
                ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(ongoingCondition.ToString(fEncounter, false))
                {
                    Tag = ongoingCondition
                };
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
                        ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem(current3.ToString(fEncounter, false))
                        {
                            Tag = current3.Copy()
                        };
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
                ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem(current4.ToString(this.fEncounter, false))
                {
                    Tag = current4.Copy()
                };
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

        private void UpdateRemoveEffectList(ToolStripDropDownItem tsddi, bool use_list_selection)
        {
            tsddi.DropDownItems.Clear();
            List<IToken> list = use_list_selection ? this.SelectedTokens : this.MapView.SelectedTokens;
            if (list.Count != 1)
            {
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("(multiple selection)")
                {
                    Enabled = false
                };
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
                    ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(current.ToString(fEncounter, false))
                    {
                        Tag = current
                    };
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
                ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem("(no effects)")
                {
                    Enabled = false
                };
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
                this.UpdateList();
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
            this.UpdateList();
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
            this.UpdateList();
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
                if (current2.Initiative != int.MinValue)
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
                    if (trap.Initiative != int.MinValue)
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
                if (combatData != null && combatData.Visible && combatData.Initiative != int.MinValue)
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F1:
                    DamageBtn_Click(null, null);
                    return true;
                case Keys.F2:
                    NextInitBtn_Click(null, null);
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
}
