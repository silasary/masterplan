using Masterplan.Controls;
using Masterplan.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Masterplan.UI
{
    partial class CombatForm
    {       
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
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

        #endregion
    }
}
