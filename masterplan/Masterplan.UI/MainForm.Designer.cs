using Masterplan.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Masterplan.UI
{
    partial class MainForm
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            ListViewGroup listViewGroup = new ListViewGroup("Races", HorizontalAlignment.Left);
            ListViewGroup listViewGroup2 = new ListViewGroup("Classes", HorizontalAlignment.Left);
            ListViewGroup listViewGroup3 = new ListViewGroup("Themes", HorizontalAlignment.Left);
            ListViewGroup listViewGroup4 = new ListViewGroup("Paragon Paths", HorizontalAlignment.Left);
            ListViewGroup listViewGroup5 = new ListViewGroup("Epic Destinies", HorizontalAlignment.Left);
            ListViewGroup listViewGroup6 = new ListViewGroup("Backgrounds", HorizontalAlignment.Left);
            ListViewGroup listViewGroup7 = new ListViewGroup("Feats (heroic tier)", HorizontalAlignment.Left);
            ListViewGroup listViewGroup8 = new ListViewGroup("Feats (paragon tier)", HorizontalAlignment.Left);
            ListViewGroup listViewGroup9 = new ListViewGroup("Feats (epic tier)", HorizontalAlignment.Left);
            ListViewGroup listViewGroup10 = new ListViewGroup("Weapons", HorizontalAlignment.Left);
            ListViewGroup listViewGroup11 = new ListViewGroup("Rituals", HorizontalAlignment.Left);
            ListViewGroup listViewGroup12 = new ListViewGroup("Creature Lore", HorizontalAlignment.Left);
            ListViewGroup listViewGroup13 = new ListViewGroup("Diseases", HorizontalAlignment.Left);
            ListViewGroup listViewGroup14 = new ListViewGroup("Poisons", HorizontalAlignment.Left);
            ListViewGroup listViewGroup15 = new ListViewGroup("Issues", HorizontalAlignment.Left);
            ListViewGroup listViewGroup16 = new ListViewGroup("Information", HorizontalAlignment.Left);
            ListViewGroup listViewGroup17 = new ListViewGroup("Notes", HorizontalAlignment.Left);
            this.WorkspaceToolbar = new ToolStrip();
            this.AddBtn = new ToolStripSplitButton();
            this.AddEncounter = new ToolStripMenuItem();
            this.AddChallenge = new ToolStripMenuItem();
            this.AddTrap = new ToolStripMenuItem();
            this.AddQuest = new ToolStripMenuItem();
            this.RemoveBtn = new ToolStripButton();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.PlotCutBtn = new ToolStripButton();
            this.PlotCopyBtn = new ToolStripButton();
            this.PlotPasteBtn = new ToolStripButton();
            this.toolStripSeparator5 = new ToolStripSeparator();
            this.SearchBtn = new ToolStripButton();
            this.toolStripSeparator9 = new ToolStripSeparator();
            this.ViewMenu = new ToolStripDropDownButton();
            this.ViewDefault = new ToolStripMenuItem();
            this.toolStripSeparator7 = new ToolStripSeparator();
            this.ViewEncounters = new ToolStripMenuItem();
            this.ViewTraps = new ToolStripMenuItem();
            this.ViewChallenges = new ToolStripMenuItem();
            this.ViewQuests = new ToolStripMenuItem();
            this.ViewParcels = new ToolStripMenuItem();
            this.toolStripSeparator8 = new ToolStripSeparator();
            this.ViewHighlighting = new ToolStripMenuItem();
            this.toolStripSeparator6 = new ToolStripSeparator();
            this.ViewLinks = new ToolStripMenuItem();
            this.ViewLinksCurved = new ToolStripMenuItem();
            this.ViewLinksAngled = new ToolStripMenuItem();
            this.ViewLinksStraight = new ToolStripMenuItem();
            this.ViewLevelling = new ToolStripMenuItem();
            this.ViewTooltips = new ToolStripMenuItem();
            this.toolStripSeparator11 = new ToolStripSeparator();
            this.ViewNavigation = new ToolStripMenuItem();
            this.ViewPreview = new ToolStripMenuItem();
            this.FlowchartMenu = new ToolStripDropDownButton();
            this.FlowchartPrint = new ToolStripMenuItem();
            this.FlowchartExport = new ToolStripMenuItem();
            this.toolStripSeparator27 = new ToolStripSeparator();
            this.FlowchartAllXP = new ToolStripMenuItem();
            this.AdvancedBtn = new ToolStripDropDownButton();
            this.PlotAdvancedTreasure = new ToolStripMenuItem();
            this.PlotAdvancedIssues = new ToolStripMenuItem();
            this.PlotAdvancedDifficulty = new ToolStripMenuItem();
            this.PointMenu = new ContextMenuStrip(this.components);
            this.ContextAdd = new ToolStripMenuItem();
            this.ContextAddBetween = new ToolStripMenuItem();
            this.toolStripSeparator28 = new ToolStripSeparator();
            this.ContextDisconnectAll = new ToolStripMenuItem();
            this.ContextDisconnect = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.ContextMoveTo = new ToolStripMenuItem();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.ContextState = new ToolStripMenuItem();
            this.ContextStateNormal = new ToolStripMenuItem();
            this.ContextStateCompleted = new ToolStripMenuItem();
            this.ContextStateSkipped = new ToolStripMenuItem();
            this.toolStripSeparator20 = new ToolStripSeparator();
            this.ContextEdit = new ToolStripMenuItem();
            this.ContextRemove = new ToolStripMenuItem();
            this.toolStripSeparator29 = new ToolStripSeparator();
            this.ContextExplore = new ToolStripMenuItem();
            this.MainMenu = new MenuStrip();
            this.FileMenu = new ToolStripMenuItem();
            this.FileNew = new ToolStripMenuItem();
            this.toolStripMenuItem1 = new ToolStripSeparator();
            this.FileOpen = new ToolStripMenuItem();
            this.toolStripMenuItem2 = new ToolStripSeparator();
            this.FileSave = new ToolStripMenuItem();
            this.FileSaveAs = new ToolStripMenuItem();
            this.toolStripMenuItem3 = new ToolStripSeparator();
            this.FileAdvanced = new ToolStripMenuItem();
            this.AdvancedDelve = new ToolStripMenuItem();
            this.AdvancedSample = new ToolStripMenuItem();
            this.toolStripSeparator42 = new ToolStripSeparator();
            this.FileExit = new ToolStripMenuItem();
            this.ProjectMenu = new ToolStripMenuItem();
            this.ProjectProject = new ToolStripMenuItem();
            this.ProjectOverview = new ToolStripMenuItem();
            this.ProjectCampaignSettings = new ToolStripMenuItem();
            this.toolStripSeparator30 = new ToolStripSeparator();
            this.ProjectPassword = new ToolStripMenuItem();
            this.toolStripSeparator10 = new ToolStripSeparator();
            this.ProjectTacticalMaps = new ToolStripMenuItem();
            this.ProjectRegionalMaps = new ToolStripMenuItem();
            this.toolStripSeparator4 = new ToolStripSeparator();
            this.ProjectPlayers = new ToolStripMenuItem();
            this.ProjectParcels = new ToolStripMenuItem();
            this.ProjectDecks = new ToolStripMenuItem();
            this.ProjectCustomCreatures = new ToolStripMenuItem();
            this.ProjectCalendars = new ToolStripMenuItem();
            this.toolStripSeparator37 = new ToolStripSeparator();
            this.ProjectEncounters = new ToolStripMenuItem();
            this.PlayerViewMenu = new ToolStripMenuItem();
            this.PlayerViewShow = new ToolStripMenuItem();
            this.PlayerViewClear = new ToolStripMenuItem();
            this.toolStripMenuItem7 = new ToolStripSeparator();
            this.PlayerViewOtherDisplay = new ToolStripMenuItem();
            this.toolStripSeparator14 = new ToolStripSeparator();
            this.PlayerViewTextSize = new ToolStripMenuItem();
            this.TextSizeSmall = new ToolStripMenuItem();
            this.TextSizeMedium = new ToolStripMenuItem();
            this.TextSizeLarge = new ToolStripMenuItem();
            this.ToolsMenu = new ToolStripMenuItem();
            this.ToolsImportProject = new ToolStripMenuItem();
            this.toolStripSeparator25 = new ToolStripSeparator();
            this.ToolsExportProject = new ToolStripMenuItem();
            this.ToolsExportHandout = new ToolStripMenuItem();
            this.ToolsExportLoot = new ToolStripMenuItem();
            this.toolStripSeparator34 = new ToolStripSeparator();
            this.ToolsTileChecklist = new ToolStripMenuItem();
            this.ToolsMiniChecklist = new ToolStripMenuItem();
            this.toolStripSeparator49 = new ToolStripSeparator();
            this.ToolsIssues = new ToolStripMenuItem();
            this.ToolsPowerStats = new ToolStripMenuItem();
            this.toolStripMenuItem4 = new ToolStripSeparator();
            this.ToolsLibraries = new ToolStripMenuItem();
            this.toolStripMenuItem5 = new ToolStripSeparator();
            this.ToolsAddIns = new ToolStripMenuItem();
            this.addinsToolStripMenuItem = new ToolStripMenuItem();
            this.HelpMenu = new ToolStripMenuItem();
            this.HelpManual = new ToolStripMenuItem();
            this.toolStripSeparator12 = new ToolStripSeparator();
            this.HelpFeedback = new ToolStripMenuItem();
            this.toolStripMenuItem8 = new ToolStripSeparator();
            this.HelpTutorials = new ToolStripMenuItem();
            this.toolStripSeparator47 = new ToolStripSeparator();
            this.HelpWebsite = new ToolStripMenuItem();
            this.HelpFacebook = new ToolStripMenuItem();
            this.HelpTwitter = new ToolStripMenuItem();
            this.toolStripSeparator13 = new ToolStripSeparator();
            this.HelpAbout = new ToolStripMenuItem();
            this.PreviewSplitter = new SplitContainer();
            this.NavigationSplitter = new SplitContainer();
            this.NavigationTree = new TreeView();
            this.PlotPanel = new Panel();
            this.PlotView = new PlotView();
            this.BreadcrumbBar = new StatusStrip();
            this.WorkspaceSearchBar = new ToolStrip();
            this.PlotSearchLbl = new ToolStripLabel();
            this.PlotSearchBox = new ToolStripTextBox();
            this.PlotClearBtn = new ToolStripLabel();
            this.PreviewInfoSplitter = new SplitContainer();
            this.PreviewPanel = new Panel();
            this.Preview = new WebBrowser();
            this.PreviewToolbar = new ToolStrip();
            this.EditBtn = new ToolStripButton();
            this.ExploreBtn = new ToolStripButton();
            this.toolStripSeparator41 = new ToolStripSeparator();
            this.PlotPointMenu = new ToolStripDropDownButton();
            this.PlotPointPlayerView = new ToolStripMenuItem();
            this.toolStripSeparator35 = new ToolStripSeparator();
            this.PlotPointExportHTML = new ToolStripMenuItem();
            this.PlotPointExportFile = new ToolStripMenuItem();
            this.Pages = new TabControl();
            this.WorkspacePage = new TabPage();
            this.BackgroundPage = new TabPage();
            this.splitContainer1 = new SplitContainer();
            this.BackgroundList = new ListView();
            this.InfoHdr = new ColumnHeader();
            this.BackgroundPanel = new Panel();
            this.BackgroundDetails = new WebBrowser();
            this.BackgroundToolbar = new ToolStrip();
            this.BackgroundAddBtn = new ToolStripButton();
            this.BackgroundRemoveBtn = new ToolStripButton();
            this.BackgroundEditBtn = new ToolStripButton();
            this.toolStripSeparator21 = new ToolStripSeparator();
            this.BackgroundUpBtn = new ToolStripButton();
            this.BackgroundDownBtn = new ToolStripButton();
            this.toolStripSeparator23 = new ToolStripSeparator();
            this.BackgroundPlayerView = new ToolStripDropDownButton();
            this.BackgroundPlayerViewSelected = new ToolStripMenuItem();
            this.BackgroundPlayerViewAll = new ToolStripMenuItem();
            this.toolStripSeparator48 = new ToolStripSeparator();
            this.BackgroundShareBtn = new ToolStripDropDownButton();
            this.BackgroundShareExport = new ToolStripMenuItem();
            this.BackgroundShareImport = new ToolStripMenuItem();
            this.toolStripMenuItem10 = new ToolStripSeparator();
            this.BackgroundSharePublish = new ToolStripMenuItem();
            this.EncyclopediaPage = new TabPage();
            this.EncyclopediaSplitter = new SplitContainer();
            this.EntryList = new ListView();
            this.EntryHdr = new ColumnHeader();
            this.EncyclopediaEntrySplitter = new SplitContainer();
            this.EntryPanel = new Panel();
            this.EntryDetails = new WebBrowser();
            this.EntryImageList = new ListView();
            this.EncyclopediaToolbar = new ToolStrip();
            this.EncAddBtn = new ToolStripDropDownButton();
            this.EncAddEntry = new ToolStripMenuItem();
            this.EncAddGroup = new ToolStripMenuItem();
            this.EncRemoveBtn = new ToolStripButton();
            this.EncEditBtn = new ToolStripButton();
            this.toolStripSeparator15 = new ToolStripSeparator();
            this.EncCutBtn = new ToolStripButton();
            this.EncCopyBtn = new ToolStripButton();
            this.EncPasteBtn = new ToolStripButton();
            this.toolStripSeparator17 = new ToolStripSeparator();
            this.EncPlayerView = new ToolStripButton();
            this.toolStripSeparator40 = new ToolStripSeparator();
            this.EncShareBtn = new ToolStripDropDownButton();
            this.EncShareExport = new ToolStripMenuItem();
            this.EncShareImport = new ToolStripMenuItem();
            this.toolStripMenuItem6 = new ToolStripSeparator();
            this.EncSharePublish = new ToolStripMenuItem();
            this.toolStripSeparator22 = new ToolStripSeparator();
            this.EncSearchLbl = new ToolStripLabel();
            this.EncSearchBox = new ToolStripTextBox();
            this.EncClearLbl = new ToolStripLabel();
            this.RulesPage = new TabPage();
            this.RulesSplitter = new SplitContainer();
            this.RulesList = new ListView();
            this.RulesHdr = new ColumnHeader();
            this.RulesToolbar = new ToolStrip();
            this.RulesAddBtn = new ToolStripDropDownButton();
            this.AddRace = new ToolStripMenuItem();
            this.toolStripSeparator31 = new ToolStripSeparator();
            this.AddClass = new ToolStripMenuItem();
            this.AddTheme = new ToolStripMenuItem();
            this.AddParagonPath = new ToolStripMenuItem();
            this.AddEpicDestiny = new ToolStripMenuItem();
            this.toolStripSeparator32 = new ToolStripSeparator();
            this.AddBackground = new ToolStripMenuItem();
            this.AddFeat = new ToolStripMenuItem();
            this.AddWeapon = new ToolStripMenuItem();
            this.AddRitual = new ToolStripMenuItem();
            this.toolStripSeparator39 = new ToolStripSeparator();
            this.AddCreatureLore = new ToolStripMenuItem();
            this.AddDisease = new ToolStripMenuItem();
            this.AddPoison = new ToolStripMenuItem();
            this.toolStripSeparator33 = new ToolStripSeparator();
            this.RulesShareBtn = new ToolStripDropDownButton();
            this.RulesShareExport = new ToolStripMenuItem();
            this.RulesShareImport = new ToolStripMenuItem();
            this.toolStripMenuItem9 = new ToolStripSeparator();
            this.RulesSharePublish = new ToolStripMenuItem();
            this.RulesBrowserPanel = new Panel();
            this.RulesBrowser = new WebBrowser();
            this.EncEntryToolbar = new ToolStrip();
            this.RulesRemoveBtn = new ToolStripButton();
            this.RulesEditBtn = new ToolStripButton();
            this.toolStripSeparator43 = new ToolStripSeparator();
            this.RuleEncyclopediaBtn = new ToolStripButton();
            this.toolStripSeparator36 = new ToolStripSeparator();
            this.RulesPlayerViewBtn = new ToolStripButton();
            this.AttachmentsPage = new TabPage();
            this.AttachmentList = new ListView();
            this.AttachmentHdr = new ColumnHeader();
            this.AttachmentSizeHdr = new ColumnHeader();
            this.AttachmentToolbar = new ToolStrip();
            this.AttachmentImportBtn = new ToolStripButton();
            this.AttachmentRemoveBtn = new ToolStripButton();
            this.toolStripSeparator19 = new ToolStripSeparator();
            this.AttachmentExtract = new ToolStripDropDownButton();
            this.AttachmentExtractSimple = new ToolStripMenuItem();
            this.AttachmentExtractAndRun = new ToolStripMenuItem();
            this.toolStripSeparator24 = new ToolStripSeparator();
            this.AttachmentPlayerView = new ToolStripButton();
            this.JotterPage = new TabPage();
            this.JotterSplitter = new SplitContainer();
            this.NoteList = new ListView();
            this.NoteHdr = new ColumnHeader();
            this.NoteBox = new TextBox();
            this.JotterToolbar = new ToolStrip();
            this.NoteAddBtn = new ToolStripButton();
            this.NoteRemoveBtn = new ToolStripButton();
            this.toolStripSeparator16 = new ToolStripSeparator();
            this.NoteCategoryBtn = new ToolStripButton();
            this.toolStripSeparator38 = new ToolStripSeparator();
            this.NoteCutBtn = new ToolStripButton();
            this.NoteCopyBtn = new ToolStripButton();
            this.NotePasteBtn = new ToolStripButton();
            this.toolStripSeparator18 = new ToolStripSeparator();
            this.NoteSearchLbl = new ToolStripLabel();
            this.NoteSearchBox = new ToolStripTextBox();
            this.NoteClearLbl = new ToolStripLabel();
            this.ReferencePage = new TabPage();
            this.ReferenceSplitter = new SplitContainer();
            this.ReferencePages = new TabControl();
            this.PartyPage = new TabPage();
            this.PartyBrowser = new WebBrowser();
            this.ToolsPage = new TabPage();
            this.ToolBrowserPanel = new Panel();
            this.GeneratorBrowser = new WebBrowser();
            this.GeneratorToolbar = new ToolStrip();
            this.toolStripLabel1 = new ToolStripLabel();
            this.toolStripSeparator26 = new ToolStripSeparator();
            this.ElfNameBtn = new ToolStripButton();
            this.DwarfNameBtn = new ToolStripButton();
            this.HalflingNameBtn = new ToolStripButton();
            this.ExoticNameBtn = new ToolStripButton();
            this.toolStripSeparator44 = new ToolStripSeparator();
            this.TreasureBtn = new ToolStripButton();
            this.BookTitleBtn = new ToolStripButton();
            this.PotionBtn = new ToolStripButton();
            this.toolStripSeparator45 = new ToolStripSeparator();
            this.NPCBtn = new ToolStripButton();
            this.RoomBtn = new ToolStripButton();
            this.toolStripSeparator46 = new ToolStripSeparator();
            this.ElfTextBtn = new ToolStripButton();
            this.DwarfTextBtn = new ToolStripButton();
            this.PrimordialTextBtn = new ToolStripButton();
            this.CompendiumPage = new TabPage();
            this.CompendiumBrowser = new WebBrowser();
            this.InfoPanel = new InfoPanel();
            this.ReferenceToolbar = new ToolStrip();
            this.DieRollerBtn = new ToolStripButton();
            this.WorkspaceToolbar.SuspendLayout();
            this.PointMenu.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.PreviewSplitter.Panel1.SuspendLayout();
            this.PreviewSplitter.Panel2.SuspendLayout();
            this.PreviewSplitter.SuspendLayout();
            this.NavigationSplitter.Panel1.SuspendLayout();
            this.NavigationSplitter.Panel2.SuspendLayout();
            this.NavigationSplitter.SuspendLayout();
            this.PlotPanel.SuspendLayout();
            this.WorkspaceSearchBar.SuspendLayout();
            this.PreviewInfoSplitter.Panel1.SuspendLayout();
            this.PreviewInfoSplitter.SuspendLayout();
            this.PreviewPanel.SuspendLayout();
            this.PreviewToolbar.SuspendLayout();
            this.Pages.SuspendLayout();
            this.WorkspacePage.SuspendLayout();
            this.BackgroundPage.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.BackgroundPanel.SuspendLayout();
            this.BackgroundToolbar.SuspendLayout();
            this.EncyclopediaPage.SuspendLayout();
            this.EncyclopediaSplitter.Panel1.SuspendLayout();
            this.EncyclopediaSplitter.Panel2.SuspendLayout();
            this.EncyclopediaSplitter.SuspendLayout();
            this.EncyclopediaEntrySplitter.Panel1.SuspendLayout();
            this.EncyclopediaEntrySplitter.Panel2.SuspendLayout();
            this.EncyclopediaEntrySplitter.SuspendLayout();
            this.EntryPanel.SuspendLayout();
            this.EncyclopediaToolbar.SuspendLayout();
            this.RulesPage.SuspendLayout();
            this.RulesSplitter.Panel1.SuspendLayout();
            this.RulesSplitter.Panel2.SuspendLayout();
            this.RulesSplitter.SuspendLayout();
            this.RulesToolbar.SuspendLayout();
            this.RulesBrowserPanel.SuspendLayout();
            this.EncEntryToolbar.SuspendLayout();
            this.AttachmentsPage.SuspendLayout();
            this.AttachmentToolbar.SuspendLayout();
            this.JotterPage.SuspendLayout();
            this.JotterSplitter.Panel1.SuspendLayout();
            this.JotterSplitter.Panel2.SuspendLayout();
            this.JotterSplitter.SuspendLayout();
            this.JotterToolbar.SuspendLayout();
            this.ReferencePage.SuspendLayout();
            this.ReferenceSplitter.Panel1.SuspendLayout();
            this.ReferenceSplitter.Panel2.SuspendLayout();
            this.ReferenceSplitter.SuspendLayout();
            this.ReferencePages.SuspendLayout();
            this.PartyPage.SuspendLayout();
            this.ToolsPage.SuspendLayout();
            this.ToolBrowserPanel.SuspendLayout();
            this.GeneratorToolbar.SuspendLayout();
            this.CompendiumPage.SuspendLayout();
            this.ReferenceToolbar.SuspendLayout();
            base.SuspendLayout();
            this.WorkspaceToolbar.Items.AddRange(new ToolStripItem[]
            {
                this.AddBtn,
                this.RemoveBtn,
                this.toolStripSeparator3,
                this.PlotCutBtn,
                this.PlotCopyBtn,
                this.PlotPasteBtn,
                this.toolStripSeparator5,
                this.SearchBtn,
                this.toolStripSeparator9,
                this.ViewMenu,
                this.FlowchartMenu,
                this.AdvancedBtn
            });
            this.WorkspaceToolbar.Location = new Point(0, 0);
            this.WorkspaceToolbar.Name = "WorkspaceToolbar";
            this.WorkspaceToolbar.Size = new Size(508, 25);
            this.WorkspaceToolbar.TabIndex = 1;
            this.WorkspaceToolbar.Text = "toolStrip1";
            this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.AddBtn.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.AddEncounter,
                this.AddChallenge,
                this.AddTrap,
                this.AddQuest
            });
            this.AddBtn.Image = (Image)resources.GetObject("AddBtn.Image");
            this.AddBtn.ImageTransparentColor = Color.Magenta;
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new Size(45, 22);
            this.AddBtn.Text = "Add";
            this.AddBtn.ButtonClick += new EventHandler(this.AddBtn_Click);
            this.AddEncounter.Name = "AddEncounter";
            this.AddEncounter.Size = new Size(160, 22);
            this.AddEncounter.Text = "Encounter...";
            this.AddEncounter.Click += new EventHandler(this.AddEncounter_Click);
            this.AddChallenge.Name = "AddChallenge";
            this.AddChallenge.Size = new Size(160, 22);
            this.AddChallenge.Text = "Skill Challenge...";
            this.AddChallenge.Click += new EventHandler(this.AddChallenge_Click);
            this.AddTrap.Name = "AddTrap";
            this.AddTrap.Size = new Size(160, 22);
            this.AddTrap.Text = "Trap / Hazard...";
            this.AddTrap.Click += new EventHandler(this.AddTrap_Click);
            this.AddQuest.Name = "AddQuest";
            this.AddQuest.Size = new Size(160, 22);
            this.AddQuest.Text = "Quest...";
            this.AddQuest.Click += new EventHandler(this.AddQuest_Click);
            this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.RemoveBtn.Image = (Image)resources.GetObject("RemoveBtn.Image");
            this.RemoveBtn.ImageTransparentColor = Color.Magenta;
            this.RemoveBtn.Name = "RemoveBtn";
            this.RemoveBtn.Size = new Size(54, 22);
            this.RemoveBtn.Text = "Remove";
            this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new Size(6, 25);
            this.PlotCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.PlotCutBtn.Image = (Image)resources.GetObject("PlotCutBtn.Image");
            this.PlotCutBtn.ImageTransparentColor = Color.Magenta;
            this.PlotCutBtn.Name = "PlotCutBtn";
            this.PlotCutBtn.Size = new Size(30, 22);
            this.PlotCutBtn.Text = "Cut";
            this.PlotCutBtn.Click += new EventHandler(this.CutBtn_Click);
            this.PlotCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.PlotCopyBtn.Image = (Image)resources.GetObject("PlotCopyBtn.Image");
            this.PlotCopyBtn.ImageTransparentColor = Color.Magenta;
            this.PlotCopyBtn.Name = "PlotCopyBtn";
            this.PlotCopyBtn.Size = new Size(39, 22);
            this.PlotCopyBtn.Text = "Copy";
            this.PlotCopyBtn.Click += new EventHandler(this.CopyBtn_Click);
            this.PlotPasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.PlotPasteBtn.Image = (Image)resources.GetObject("PlotPasteBtn.Image");
            this.PlotPasteBtn.ImageTransparentColor = Color.Magenta;
            this.PlotPasteBtn.Name = "PlotPasteBtn";
            this.PlotPasteBtn.Size = new Size(39, 22);
            this.PlotPasteBtn.Text = "Paste";
            this.PlotPasteBtn.Click += new EventHandler(this.PasteBtn_Click);
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new Size(6, 25);
            this.SearchBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.SearchBtn.Image = (Image)resources.GetObject("SearchBtn.Image");
            this.SearchBtn.ImageTransparentColor = Color.Magenta;
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new Size(46, 22);
            this.SearchBtn.Text = "Search";
            this.SearchBtn.Click += new EventHandler(this.SearchBtn_Click);
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new Size(6, 25);
            this.ViewMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.ViewDefault,
                this.toolStripSeparator7,
                this.ViewEncounters,
                this.ViewTraps,
                this.ViewChallenges,
                this.ViewQuests,
                this.ViewParcels,
                this.toolStripSeparator8,
                this.ViewHighlighting,
                this.toolStripSeparator6,
                this.ViewLinks,
                this.ViewLevelling,
                this.ViewTooltips,
                this.toolStripSeparator11,
                this.ViewNavigation,
                this.ViewPreview
            });
            this.ViewMenu.Name = "ViewMenu";
            this.ViewMenu.Size = new Size(45, 22);
            this.ViewMenu.Text = "View";
            this.ViewMenu.DropDownOpening += new EventHandler(this.ViewMenu_DropDownOpening);
            this.ViewDefault.Name = "ViewDefault";
            this.ViewDefault.Size = new Size(191, 22);
            this.ViewDefault.Text = "Default View";
            this.ViewDefault.Click += new EventHandler(this.ViewDefault_Click);
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new Size(188, 6);
            this.ViewEncounters.Name = "ViewEncounters";
            this.ViewEncounters.Size = new Size(191, 22);
            this.ViewEncounters.Text = "Show Encounters";
            this.ViewEncounters.Click += new EventHandler(this.ViewEncounters_Click);
            this.ViewTraps.Name = "ViewTraps";
            this.ViewTraps.Size = new Size(191, 22);
            this.ViewTraps.Text = "Show Traps / Hazards";
            this.ViewTraps.Click += new EventHandler(this.ViewTraps_Click);
            this.ViewChallenges.Name = "ViewChallenges";
            this.ViewChallenges.Size = new Size(191, 22);
            this.ViewChallenges.Text = "Show Skill Challenges";
            this.ViewChallenges.Click += new EventHandler(this.ViewChallenges_Click);
            this.ViewQuests.Name = "ViewQuests";
            this.ViewQuests.Size = new Size(191, 22);
            this.ViewQuests.Text = "Show Quests";
            this.ViewQuests.Click += new EventHandler(this.ViewQuests_Click);
            this.ViewParcels.Name = "ViewParcels";
            this.ViewParcels.Size = new Size(191, 22);
            this.ViewParcels.Text = "Show Treasure Parcels";
            this.ViewParcels.Click += new EventHandler(this.ViewParcels_Click);
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new Size(188, 6);
            this.ViewHighlighting.Name = "ViewHighlighting";
            this.ViewHighlighting.Size = new Size(191, 22);
            this.ViewHighlighting.Text = "Highlighting";
            this.ViewHighlighting.Click += new EventHandler(this.ViewHighlighting_Click);
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new Size(188, 6);
            this.ViewLinks.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.ViewLinksCurved,
                this.ViewLinksAngled,
                this.ViewLinksStraight
            });
            this.ViewLinks.Name = "ViewLinks";
            this.ViewLinks.Size = new Size(191, 22);
            this.ViewLinks.Text = "Show Links";
            this.ViewLinks.DropDownOpening += new EventHandler(this.ViewLinks_DropDownOpening);
            this.ViewLinksCurved.Name = "ViewLinksCurved";
            this.ViewLinksCurved.Size = new Size(115, 22);
            this.ViewLinksCurved.Text = "Curved";
            this.ViewLinksCurved.Click += new EventHandler(this.ViewLinksCurved_Click);
            this.ViewLinksAngled.Name = "ViewLinksAngled";
            this.ViewLinksAngled.Size = new Size(115, 22);
            this.ViewLinksAngled.Text = "Angled";
            this.ViewLinksAngled.Click += new EventHandler(this.ViewLinksAngled_Click);
            this.ViewLinksStraight.Name = "ViewLinksStraight";
            this.ViewLinksStraight.Size = new Size(115, 22);
            this.ViewLinksStraight.Text = "Straight";
            this.ViewLinksStraight.Click += new EventHandler(this.ViewLinksStraight_Click);
            this.ViewLevelling.Name = "ViewLevelling";
            this.ViewLevelling.Size = new Size(191, 22);
            this.ViewLevelling.Text = "Show Levelling";
            this.ViewLevelling.Click += new EventHandler(this.ViewLevelling_Click);
            this.ViewTooltips.Name = "ViewTooltips";
            this.ViewTooltips.Size = new Size(191, 22);
            this.ViewTooltips.Text = "Show Tooltips";
            this.ViewTooltips.Click += new EventHandler(this.ViewTooltips_Click);
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new Size(188, 6);
            this.ViewNavigation.Name = "ViewNavigation";
            this.ViewNavigation.Size = new Size(191, 22);
            this.ViewNavigation.Text = "Show Navigation";
            this.ViewNavigation.Click += new EventHandler(this.ViewNavigation_Click);
            this.ViewPreview.Name = "ViewPreview";
            this.ViewPreview.Size = new Size(191, 22);
            this.ViewPreview.Text = "Show Preview";
            this.ViewPreview.Click += new EventHandler(this.ViewPreview_Click);
            this.FlowchartMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.FlowchartMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.FlowchartPrint,
                this.FlowchartExport,
                this.toolStripSeparator27,
                this.FlowchartAllXP
            });
            this.FlowchartMenu.Image = (Image)resources.GetObject("FlowchartMenu.Image");
            this.FlowchartMenu.ImageTransparentColor = Color.Magenta;
            this.FlowchartMenu.Name = "FlowchartMenu";
            this.FlowchartMenu.Size = new Size(72, 22);
            this.FlowchartMenu.Text = "Flowchart";
            this.FlowchartPrint.Name = "FlowchartPrint";
            this.FlowchartPrint.Size = new Size(196, 22);
            this.FlowchartPrint.Text = "Print...";
            this.FlowchartPrint.Click += new EventHandler(this.FlowchartPrint_Click);
            this.FlowchartExport.Name = "FlowchartExport";
            this.FlowchartExport.Size = new Size(196, 22);
            this.FlowchartExport.Text = "Export...";
            this.FlowchartExport.Click += new EventHandler(this.FlowchartExport_Click);
            this.toolStripSeparator27.Name = "toolStripSeparator27";
            this.toolStripSeparator27.Size = new Size(193, 6);
            this.FlowchartAllXP.Name = "FlowchartAllXP";
            this.FlowchartAllXP.Size = new Size(196, 22);
            this.FlowchartAllXP.Text = "Maximum Available XP";
            this.FlowchartAllXP.Click += new EventHandler(this.FlowchartAllXP_Click);
            this.AdvancedBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.AdvancedBtn.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.PlotAdvancedTreasure,
                this.PlotAdvancedIssues,
                this.PlotAdvancedDifficulty
            });
            this.AdvancedBtn.Image = (Image)resources.GetObject("AdvancedBtn.Image");
            this.AdvancedBtn.ImageTransparentColor = Color.Magenta;
            this.AdvancedBtn.Name = "AdvancedBtn";
            this.AdvancedBtn.Size = new Size(73, 22);
            this.AdvancedBtn.Text = "Advanced";
            this.PlotAdvancedTreasure.Name = "PlotAdvancedTreasure";
            this.PlotAdvancedTreasure.Size = new Size(185, 22);
            this.PlotAdvancedTreasure.Text = "Export Treasure List...";
            this.PlotAdvancedTreasure.Click += new EventHandler(this.PlotAdvancedTreasure_Click);
            this.PlotAdvancedIssues.Name = "PlotAdvancedIssues";
            this.PlotAdvancedIssues.Size = new Size(185, 22);
            this.PlotAdvancedIssues.Text = "Plot Design Issues";
            this.PlotAdvancedIssues.Click += new EventHandler(this.PlotAdvancedIssues_Click);
            this.PlotAdvancedDifficulty.Name = "PlotAdvancedDifficulty";
            this.PlotAdvancedDifficulty.Size = new Size(185, 22);
            this.PlotAdvancedDifficulty.Text = "Adjust Difficulty...";
            this.PlotAdvancedDifficulty.Click += new EventHandler(this.PlotAdvancedDifficulty_Click);
            this.PointMenu.Items.AddRange(new ToolStripItem[]
            {
                this.ContextAdd,
                this.ContextAddBetween,
                this.toolStripSeparator28,
                this.ContextDisconnectAll,
                this.ContextDisconnect,
                this.toolStripSeparator1,
                this.ContextMoveTo,
                this.toolStripSeparator2,
                this.ContextState,
                this.toolStripSeparator20,
                this.ContextEdit,
                this.ContextRemove,
                this.toolStripSeparator29,
                this.ContextExplore
            });
            this.PointMenu.Name = "PointMenu";
            this.PointMenu.Size = new Size(166, 232);
            this.PointMenu.Opening += new CancelEventHandler(this.PointMenu_Opening);
            this.ContextAdd.Name = "ContextAdd";
            this.ContextAdd.Size = new Size(165, 22);
            this.ContextAdd.Text = "Add Point...";
            this.ContextAdd.Click += new EventHandler(this.ContextAdd_Click);
            this.ContextAddBetween.Name = "ContextAddBetween";
            this.ContextAddBetween.Size = new Size(165, 22);
            this.ContextAddBetween.Text = "Add Point";
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            this.toolStripSeparator28.Size = new Size(162, 6);
            this.ContextDisconnectAll.Name = "ContextDisconnectAll";
            this.ContextDisconnectAll.Size = new Size(165, 22);
            this.ContextDisconnectAll.Text = "Disconnect Point";
            this.ContextDisconnectAll.Click += new EventHandler(this.ContextDisconnectAll_Click);
            this.ContextDisconnect.Name = "ContextDisconnect";
            this.ContextDisconnect.Size = new Size(165, 22);
            this.ContextDisconnect.Text = "Disconnect From";
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(162, 6);
            this.ContextMoveTo.Name = "ContextMoveTo";
            this.ContextMoveTo.Size = new Size(165, 22);
            this.ContextMoveTo.Text = "Move To Subplot";
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(162, 6);
            this.ContextState.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.ContextStateNormal,
                this.ContextStateCompleted,
                this.ContextStateSkipped
            });
            this.ContextState.Name = "ContextState";
            this.ContextState.Size = new Size(165, 22);
            this.ContextState.Text = "State";
            this.ContextStateNormal.Name = "ContextStateNormal";
            this.ContextStateNormal.Size = new Size(133, 22);
            this.ContextStateNormal.Text = "Normal";
            this.ContextStateNormal.Click += new EventHandler(this.ContextStateNormal_Click);
            this.ContextStateCompleted.Name = "ContextStateCompleted";
            this.ContextStateCompleted.Size = new Size(133, 22);
            this.ContextStateCompleted.Text = "Completed";
            this.ContextStateCompleted.Click += new EventHandler(this.ContextStateCompleted_Click);
            this.ContextStateSkipped.Name = "ContextStateSkipped";
            this.ContextStateSkipped.Size = new Size(133, 22);
            this.ContextStateSkipped.Text = "Skipped";
            this.ContextStateSkipped.Click += new EventHandler(this.ContextStateSkipped_Click);
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new Size(162, 6);
            this.ContextEdit.Name = "ContextEdit";
            this.ContextEdit.Size = new Size(165, 22);
            this.ContextEdit.Text = "Edit";
            this.ContextEdit.Click += new EventHandler(this.EditBtn_Click);
            this.ContextRemove.Name = "ContextRemove";
            this.ContextRemove.Size = new Size(165, 22);
            this.ContextRemove.Text = "Remove";
            this.ContextRemove.Click += new EventHandler(this.RemoveBtn_Click);
            this.toolStripSeparator29.Name = "toolStripSeparator29";
            this.toolStripSeparator29.Size = new Size(162, 6);
            this.ContextExplore.Name = "ContextExplore";
            this.ContextExplore.Size = new Size(165, 22);
            this.ContextExplore.Text = "Explore Subplot";
            this.ContextExplore.Click += new EventHandler(this.ExploreBtn_Click);
            this.MainMenu.Items.AddRange(new ToolStripItem[]
            {
                this.FileMenu,
                this.ProjectMenu,
                this.PlayerViewMenu,
                this.ToolsMenu,
                this.HelpMenu
            });
            this.MainMenu.Location = new Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new Size(864, 24);
            this.MainMenu.TabIndex = 4;
            this.MainMenu.Text = "menuStrip1";
            this.FileMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.FileNew,
                this.toolStripMenuItem1,
                this.FileOpen,
                this.toolStripMenuItem2,
                this.FileSave,
                this.FileSaveAs,
                this.toolStripMenuItem3,
                this.FileAdvanced,
                this.toolStripSeparator42,
                this.FileExit
            });
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new Size(37, 20);
            this.FileMenu.Text = "File";
            this.FileMenu.DropDownOpening += new EventHandler(this.FileMenu_DropDownOpening);
            this.FileNew.Name = "FileNew";
            this.FileNew.ShortcutKeys = Keys.Control | Keys.N;
            this.FileNew.Size = new Size(195, 22);
            this.FileNew.Text = "New Project...";
            this.FileNew.Click += new EventHandler(this.FileNew_Click);
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new Size(192, 6);
            this.FileOpen.Name = "FileOpen";
            this.FileOpen.ShortcutKeys = Keys.Control | Keys.O;
            this.FileOpen.Size = new Size(195, 22);
            this.FileOpen.Text = "Open Project...";
            this.FileOpen.Click += new EventHandler(this.FileOpen_Click);
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new Size(192, 6);
            this.FileSave.Name = "FileSave";
            this.FileSave.ShortcutKeys = Keys.Control | Keys.S;
            this.FileSave.Size = new Size(195, 22);
            this.FileSave.Text = "Save Project";
            this.FileSave.Click += new EventHandler(this.FileSave_Click);
            this.FileSaveAs.Name = "FileSaveAs";
            this.FileSaveAs.Size = new Size(195, 22);
            this.FileSaveAs.Text = "Save Project As...";
            this.FileSaveAs.Click += new EventHandler(this.FileSaveAs_Click);
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new Size(192, 6);
            this.FileAdvanced.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.AdvancedDelve,
                this.AdvancedSample
            });
            this.FileAdvanced.Name = "FileAdvanced";
            this.FileAdvanced.Size = new Size(195, 22);
            this.FileAdvanced.Text = "Advanced";
            this.AdvancedDelve.Name = "AdvancedDelve";
            this.AdvancedDelve.Size = new Size(245, 22);
            this.AdvancedDelve.Text = "Create a Dungeon Delve...";
            this.AdvancedDelve.Click += new EventHandler(this.AdvancedDelve_Click);
            this.AdvancedSample.Name = "AdvancedSample";
            this.AdvancedSample.Size = new Size(245, 22);
            this.AdvancedSample.Text = "Download a Premade Adventure";
            this.AdvancedSample.Click += new EventHandler(this.AdvancedSample_Click);
            this.toolStripSeparator42.Name = "toolStripSeparator42";
            this.toolStripSeparator42.Size = new Size(192, 6);
            this.FileExit.Name = "FileExit";
            this.FileExit.Size = new Size(195, 22);
            this.FileExit.Text = "Exit";
            this.FileExit.Click += new EventHandler(this.FileExit_Click);
            this.ProjectMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.ProjectProject,
                this.ProjectOverview,
                this.ProjectCampaignSettings,
                this.toolStripSeparator30,
                this.ProjectPassword,
                this.toolStripSeparator10,
                this.ProjectTacticalMaps,
                this.ProjectRegionalMaps,
                this.toolStripSeparator4,
                this.ProjectPlayers,
                this.ProjectParcels,
                this.ProjectDecks,
                this.ProjectCustomCreatures,
                this.ProjectCalendars,
                this.toolStripSeparator37,
                this.ProjectEncounters
            });
            this.ProjectMenu.Name = "ProjectMenu";
            this.ProjectMenu.Size = new Size(56, 20);
            this.ProjectMenu.Text = "Project";
            this.ProjectMenu.DropDownOpening += new EventHandler(this.ProjectMenu_DropDownOpening);
            this.ProjectProject.Name = "ProjectProject";
            this.ProjectProject.ShortcutKeys = Keys.Control | Keys.P;
            this.ProjectProject.Size = new Size(243, 22);
            this.ProjectProject.Text = "Project Properties";
            this.ProjectProject.Click += new EventHandler(this.ProjectProject_Click);
            this.ProjectOverview.Name = "ProjectOverview";
            this.ProjectOverview.Size = new Size(243, 22);
            this.ProjectOverview.Text = "Project Overview";
            this.ProjectOverview.Click += new EventHandler(this.ProjectOverview_Click);
            this.ProjectCampaignSettings.Name = "ProjectCampaignSettings";
            this.ProjectCampaignSettings.Size = new Size(243, 22);
            this.ProjectCampaignSettings.Text = "Campaign Settings";
            this.ProjectCampaignSettings.Click += new EventHandler(this.ProjectCampaignSettings_Click);
            this.toolStripSeparator30.Name = "toolStripSeparator30";
            this.toolStripSeparator30.Size = new Size(240, 6);
            this.ProjectPassword.Name = "ProjectPassword";
            this.ProjectPassword.Size = new Size(243, 22);
            this.ProjectPassword.Text = "Password Protection";
            this.ProjectPassword.Click += new EventHandler(this.ProjectPassword_Click);
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new Size(240, 6);
            this.ProjectTacticalMaps.Name = "ProjectTacticalMaps";
            this.ProjectTacticalMaps.ShortcutKeys = Keys.F2;
            this.ProjectTacticalMaps.Size = new Size(243, 22);
            this.ProjectTacticalMaps.Text = "Tactical Maps";
            this.ProjectTacticalMaps.Click += new EventHandler(this.ProjectTacticalMaps_Click);
            this.ProjectRegionalMaps.Name = "ProjectRegionalMaps";
            this.ProjectRegionalMaps.ShortcutKeys = Keys.F3;
            this.ProjectRegionalMaps.Size = new Size(243, 22);
            this.ProjectRegionalMaps.Text = "Regional Maps";
            this.ProjectRegionalMaps.Click += new EventHandler(this.ProjectRegionalMaps_Click);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new Size(240, 6);
            this.ProjectPlayers.Name = "ProjectPlayers";
            this.ProjectPlayers.ShortcutKeys = Keys.F4;
            this.ProjectPlayers.Size = new Size(243, 22);
            this.ProjectPlayers.Text = "Player Characters";
            this.ProjectPlayers.Click += new EventHandler(this.ProjectPlayers_Click);
            this.ProjectParcels.Name = "ProjectParcels";
            this.ProjectParcels.ShortcutKeys = Keys.F5;
            this.ProjectParcels.Size = new Size(243, 22);
            this.ProjectParcels.Text = "Treasure Parcels";
            this.ProjectParcels.Click += new EventHandler(this.ProjectParcels_Click);
            this.ProjectDecks.Name = "ProjectDecks";
            this.ProjectDecks.ShortcutKeys = Keys.F6;
            this.ProjectDecks.Size = new Size(243, 22);
            this.ProjectDecks.Text = "Encounter Decks";
            this.ProjectDecks.Click += new EventHandler(this.ProjectDecks_Click);
            this.ProjectCustomCreatures.Name = "ProjectCustomCreatures";
            this.ProjectCustomCreatures.ShortcutKeys = Keys.F7;
            this.ProjectCustomCreatures.Size = new Size(243, 22);
            this.ProjectCustomCreatures.Text = "Custom Creatures and NPCs";
            this.ProjectCustomCreatures.Click += new EventHandler(this.ProjectCustomCreatures_Click);
            this.ProjectCalendars.Name = "ProjectCalendars";
            this.ProjectCalendars.ShortcutKeys = Keys.F8;
            this.ProjectCalendars.Size = new Size(243, 22);
            this.ProjectCalendars.Text = "Calendars";
            this.ProjectCalendars.Click += new EventHandler(this.ProjectCalendars_Click);
            this.toolStripSeparator37.Name = "toolStripSeparator37";
            this.toolStripSeparator37.Size = new Size(240, 6);
            this.ProjectEncounters.Name = "ProjectEncounters";
            this.ProjectEncounters.Size = new Size(243, 22);
            this.ProjectEncounters.Text = "Paused Encounters";
            this.ProjectEncounters.Click += new EventHandler(this.ProjectEncounters_Click);
            this.PlayerViewMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.PlayerViewShow,
                this.PlayerViewClear,
                this.toolStripMenuItem7,
                this.PlayerViewOtherDisplay,
                this.toolStripSeparator14,
                this.PlayerViewTextSize
            });
            this.PlayerViewMenu.Name = "PlayerViewMenu";
            this.PlayerViewMenu.Size = new Size(79, 20);
            this.PlayerViewMenu.Text = "Player View";
            this.PlayerViewMenu.DropDownOpening += new EventHandler(this.PlayerViewMenu_DropDownOpening);
            this.PlayerViewShow.Name = "PlayerViewShow";
            this.PlayerViewShow.Size = new Size(194, 22);
            this.PlayerViewShow.Text = "Show";
            this.PlayerViewShow.Click += new EventHandler(this.ToolsPlayerView_Click);
            this.PlayerViewClear.Name = "PlayerViewClear";
            this.PlayerViewClear.Size = new Size(194, 22);
            this.PlayerViewClear.Text = "Clear";
            this.PlayerViewClear.Click += new EventHandler(this.ToolsPlayerViewClear_Click);
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new Size(191, 6);
            this.PlayerViewOtherDisplay.Name = "PlayerViewOtherDisplay";
            this.PlayerViewOtherDisplay.Size = new Size(194, 22);
            this.PlayerViewOtherDisplay.Text = "Show on Other Display";
            this.PlayerViewOtherDisplay.Click += new EventHandler(this.ToolsPlayerViewSecondary_Click);
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new Size(191, 6);
            this.PlayerViewTextSize.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.TextSizeSmall,
                this.TextSizeMedium,
                this.TextSizeLarge
            });
            this.PlayerViewTextSize.Name = "PlayerViewTextSize";
            this.PlayerViewTextSize.Size = new Size(194, 22);
            this.PlayerViewTextSize.Text = "Text Size";
            this.TextSizeSmall.Name = "TextSizeSmall";
            this.TextSizeSmall.Size = new Size(119, 22);
            this.TextSizeSmall.Text = "Small";
            this.TextSizeSmall.Click += new EventHandler(this.TextSizeSmall_Click);
            this.TextSizeMedium.Name = "TextSizeMedium";
            this.TextSizeMedium.Size = new Size(119, 22);
            this.TextSizeMedium.Text = "Medium";
            this.TextSizeMedium.Click += new EventHandler(this.TextSizeMedium_Click);
            this.TextSizeLarge.Name = "TextSizeLarge";
            this.TextSizeLarge.Size = new Size(119, 22);
            this.TextSizeLarge.Text = "Large";
            this.TextSizeLarge.Click += new EventHandler(this.TextSizeLarge_Click);
            this.ToolsMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.ToolsImportProject,
                this.toolStripSeparator25,
                this.ToolsExportProject,
                this.ToolsExportHandout,
                this.ToolsExportLoot,
                this.toolStripSeparator34,
                this.ToolsTileChecklist,
                this.ToolsMiniChecklist,
                this.toolStripSeparator49,
                this.ToolsIssues,
                this.ToolsPowerStats,
                this.toolStripMenuItem4,
                this.ToolsLibraries,
                this.toolStripMenuItem5,
                this.ToolsAddIns
            });
            this.ToolsMenu.Name = "ToolsMenu";
            this.ToolsMenu.Size = new Size(48, 20);
            this.ToolsMenu.Text = "Tools";
            this.ToolsMenu.DropDownOpening += new EventHandler(this.ToolsMenu_DropDownOpening);
            this.ToolsImportProject.Name = "ToolsImportProject";
            this.ToolsImportProject.Size = new Size(204, 22);
            this.ToolsImportProject.Text = "Import Project...";
            this.ToolsImportProject.Click += new EventHandler(this.ToolsImportProject_Click);
            this.toolStripSeparator25.Name = "toolStripSeparator25";
            this.toolStripSeparator25.Size = new Size(201, 6);
            this.ToolsExportProject.Name = "ToolsExportProject";
            this.ToolsExportProject.Size = new Size(204, 22);
            this.ToolsExportProject.Text = "Export Project...";
            this.ToolsExportProject.Click += new EventHandler(this.ToolsExportProject_Click);
            this.ToolsExportHandout.Name = "ToolsExportHandout";
            this.ToolsExportHandout.Size = new Size(204, 22);
            this.ToolsExportHandout.Text = "Export Handout...";
            this.ToolsExportHandout.Click += new EventHandler(this.ToolsExportHandout_Click);
            this.ToolsExportLoot.Name = "ToolsExportLoot";
            this.ToolsExportLoot.Size = new Size(204, 22);
            this.ToolsExportLoot.Text = "Export Treasure List...";
            this.ToolsExportLoot.Click += new EventHandler(this.ToolsExportLoot_Click);
            this.toolStripSeparator34.Name = "toolStripSeparator34";
            this.toolStripSeparator34.Size = new Size(201, 6);
            this.ToolsTileChecklist.Name = "ToolsTileChecklist";
            this.ToolsTileChecklist.Size = new Size(204, 22);
            this.ToolsTileChecklist.Text = "Map Tile Checklist...";
            this.ToolsTileChecklist.Click += new EventHandler(this.ToolsTileChecklist_Click);
            this.ToolsMiniChecklist.Name = "ToolsMiniChecklist";
            this.ToolsMiniChecklist.Size = new Size(204, 22);
            this.ToolsMiniChecklist.Text = "Miniature Checklist...";
            this.ToolsMiniChecklist.Click += new EventHandler(this.ToolsMiniChecklist_Click);
            this.toolStripSeparator49.Name = "toolStripSeparator49";
            this.toolStripSeparator49.Size = new Size(201, 6);
            this.ToolsIssues.Name = "ToolsIssues";
            this.ToolsIssues.Size = new Size(204, 22);
            this.ToolsIssues.Text = "Plot Design Issues";
            this.ToolsIssues.Click += new EventHandler(this.ToolsIssues_Click);
            this.ToolsPowerStats.Name = "ToolsPowerStats";
            this.ToolsPowerStats.Size = new Size(204, 22);
            this.ToolsPowerStats.Text = "Creature Power Statistics";
            this.ToolsPowerStats.Click += new EventHandler(this.ToolsPowerStats_Click);
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new Size(201, 6);
            this.ToolsLibraries.Name = "ToolsLibraries";
            this.ToolsLibraries.ShortcutKeys = Keys.Control | Keys.L;
            this.ToolsLibraries.Size = new Size(204, 22);
            this.ToolsLibraries.Text = "Libraries";
            this.ToolsLibraries.Click += new EventHandler(this.ToolsLibraries_Click);
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new Size(201, 6);
            this.ToolsAddIns.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.addinsToolStripMenuItem
            });
            this.ToolsAddIns.Name = "ToolsAddIns";
            this.ToolsAddIns.Size = new Size(204, 22);
            this.ToolsAddIns.Text = "Add-Ins";
            this.addinsToolStripMenuItem.Name = "addinsToolStripMenuItem";
            this.addinsToolStripMenuItem.Size = new Size(122, 22);
            this.addinsToolStripMenuItem.Text = "[add-ins]";
            this.HelpMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.HelpManual,
                this.toolStripSeparator12,
                this.HelpFeedback,
                this.toolStripMenuItem8,
                this.HelpTutorials,
                this.toolStripSeparator47,
                this.HelpWebsite,
                this.HelpFacebook,
                this.HelpTwitter,
                this.toolStripSeparator13,
                this.HelpAbout
            });
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new Size(44, 20);
            this.HelpMenu.Text = "Help";
            this.HelpManual.Name = "HelpManual";
            this.HelpManual.ShortcutKeys = Keys.F1;
            this.HelpManual.Size = new Size(204, 22);
            this.HelpManual.Text = "Manual";
            this.HelpManual.Click += new EventHandler(this.HelpManual_Click);
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new Size(201, 6);
            this.HelpFeedback.Name = "HelpFeedback";
            this.HelpFeedback.Size = new Size(204, 22);
            this.HelpFeedback.Text = "Send Feedback";
            this.HelpFeedback.Click += new EventHandler(this.HelpFeedback_Click);
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new Size(201, 6);
            this.HelpTutorials.Name = "HelpTutorials";
            this.HelpTutorials.Size = new Size(204, 22);
            this.HelpTutorials.Text = "Tutorials";
            this.HelpTutorials.Click += new EventHandler(this.HelpTutorials_Click);
            this.toolStripSeparator47.Name = "toolStripSeparator47";
            this.toolStripSeparator47.Size = new Size(201, 6);
            this.HelpWebsite.Name = "HelpWebsite";
            this.HelpWebsite.Size = new Size(204, 22);
            this.HelpWebsite.Text = "Masterplan Website";
            this.HelpWebsite.Click += new EventHandler(this.HelpWebsite_Click);
            this.HelpFacebook.Name = "HelpFacebook";
            this.HelpFacebook.Size = new Size(204, 22);
            this.HelpFacebook.Text = "Masterplan on Facebook";
            this.HelpFacebook.Click += new EventHandler(this.HelpFacebook_Click);
            this.HelpTwitter.Name = "HelpTwitter";
            this.HelpTwitter.Size = new Size(204, 22);
            this.HelpTwitter.Text = "Masterplan on Twitter";
            this.HelpTwitter.Click += new EventHandler(this.HelpTwitter_Click);
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new Size(201, 6);
            this.HelpAbout.Name = "HelpAbout";
            this.HelpAbout.Size = new Size(204, 22);
            this.HelpAbout.Text = "About";
            this.HelpAbout.Click += new EventHandler(this.HelpAbout_Click);
            this.PreviewSplitter.Dock = DockStyle.Fill;
            this.PreviewSplitter.FixedPanel = FixedPanel.Panel2;
            this.PreviewSplitter.Location = new Point(0, 0);
            this.PreviewSplitter.Name = "PreviewSplitter";
            this.PreviewSplitter.Panel1.Controls.Add(this.NavigationSplitter);
            this.PreviewSplitter.Panel1.Controls.Add(this.WorkspaceToolbar);
            this.PreviewSplitter.Panel2.Controls.Add(this.PreviewInfoSplitter);
            this.PreviewSplitter.Size = new Size(856, 410);
            this.PreviewSplitter.SplitterDistance = 508;
            this.PreviewSplitter.TabIndex = 6;
            this.NavigationSplitter.Dock = DockStyle.Fill;
            this.NavigationSplitter.FixedPanel = FixedPanel.Panel1;
            this.NavigationSplitter.Location = new Point(0, 25);
            this.NavigationSplitter.Name = "NavigationSplitter";
            this.NavigationSplitter.Panel1.Controls.Add(this.NavigationTree);
            this.NavigationSplitter.Panel2.Controls.Add(this.PlotPanel);
            this.NavigationSplitter.Panel2.Controls.Add(this.WorkspaceSearchBar);
            this.NavigationSplitter.Size = new Size(508, 385);
            this.NavigationSplitter.SplitterDistance = 152;
            this.NavigationSplitter.TabIndex = 4;
            this.NavigationTree.AllowDrop = true;
            this.NavigationTree.Dock = DockStyle.Fill;
            this.NavigationTree.HideSelection = false;
            this.NavigationTree.Location = new Point(0, 0);
            this.NavigationTree.Name = "NavigationTree";
            this.NavigationTree.ShowRootLines = false;
            this.NavigationTree.Size = new Size(152, 385);
            this.NavigationTree.TabIndex = 0;
            this.NavigationTree.DragDrop += new DragEventHandler(this.NavigationTree_DragDrop);
            this.NavigationTree.AfterSelect += new TreeViewEventHandler(this.NavigationTree_AfterSelect);
            this.NavigationTree.DragOver += new DragEventHandler(this.NavigationTree_DragOver);
            this.PlotPanel.BorderStyle = BorderStyle.FixedSingle;
            this.PlotPanel.Controls.Add(this.PlotView);
            this.PlotPanel.Controls.Add(this.BreadcrumbBar);
            this.PlotPanel.Dock = DockStyle.Fill;
            this.PlotPanel.Location = new Point(0, 25);
            this.PlotPanel.Name = "PlotPanel";
            this.PlotPanel.Size = new Size(352, 360);
            this.PlotPanel.TabIndex = 5;
            this.PlotView.AllowDrop = true;
            this.PlotView.ContextMenuStrip = this.PointMenu;
            this.PlotView.Dock = DockStyle.Fill;
            this.PlotView.Filter = "";
            this.PlotView.LinkStyle = PlotViewLinkStyle.Curved;
            this.PlotView.Location = new Point(0, 0);
            this.PlotView.Mode = PlotViewMode.Normal;
            this.PlotView.Name = "PlotView";
            this.PlotView.Plot = null;
            this.PlotView.SelectedPoint = null;
            this.PlotView.ShowLevels = true;
            this.PlotView.ShowTooltips = true;
            this.PlotView.Size = new Size(350, 336);
            this.PlotView.TabIndex = 2;
            this.PlotView.PlotLayoutChanged += new EventHandler(this.PlotView_PlotLayoutChanged);
            this.PlotView.DoubleClick += new EventHandler(this.PlotView_DoubleClick);
            this.PlotView.SelectionChanged += new EventHandler(this.PlotView_SelectionChanged);
            this.PlotView.PlotChanged += new EventHandler(this.PlotView_PlotChanged);
            this.BreadcrumbBar.Location = new Point(0, 336);
            this.BreadcrumbBar.Name = "BreadcrumbBar";
            this.BreadcrumbBar.Size = new Size(350, 22);
            this.BreadcrumbBar.SizingGrip = false;
            this.BreadcrumbBar.TabIndex = 4;
            this.BreadcrumbBar.Text = "statusStrip1";
            this.WorkspaceSearchBar.Items.AddRange(new ToolStripItem[]
            {
                this.PlotSearchLbl,
                this.PlotSearchBox,
                this.PlotClearBtn
            });
            this.WorkspaceSearchBar.Location = new Point(0, 0);
            this.WorkspaceSearchBar.Name = "WorkspaceSearchBar";
            this.WorkspaceSearchBar.Size = new Size(352, 25);
            this.WorkspaceSearchBar.TabIndex = 3;
            this.WorkspaceSearchBar.Text = "toolStrip1";
            this.PlotSearchLbl.Name = "PlotSearchLbl";
            this.PlotSearchLbl.Size = new Size(63, 22);
            this.PlotSearchLbl.Text = "Search for:";
            this.PlotSearchBox.BorderStyle = BorderStyle.FixedSingle;
            this.PlotSearchBox.Name = "PlotSearchBox";
            this.PlotSearchBox.Size = new Size(200, 25);
            this.PlotSearchBox.TextChanged += new EventHandler(this.SearchBox_TextChanged);
            this.PlotClearBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.PlotClearBtn.Image = (Image)resources.GetObject("PlotClearBtn.Image");
            this.PlotClearBtn.ImageTransparentColor = Color.Magenta;
            this.PlotClearBtn.IsLink = true;
            this.PlotClearBtn.Name = "PlotClearBtn";
            this.PlotClearBtn.Size = new Size(34, 22);
            this.PlotClearBtn.Text = "Clear";
            this.PlotClearBtn.Click += new EventHandler(this.ClearBtn_Click);
            this.PreviewInfoSplitter.Dock = DockStyle.Fill;
            this.PreviewInfoSplitter.FixedPanel = FixedPanel.Panel2;
            this.PreviewInfoSplitter.Location = new Point(0, 0);
            this.PreviewInfoSplitter.Name = "PreviewInfoSplitter";
            this.PreviewInfoSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.PreviewInfoSplitter.Panel1.Controls.Add(this.PreviewPanel);
            this.PreviewInfoSplitter.Panel1.Controls.Add(this.PreviewToolbar);
            this.PreviewInfoSplitter.Size = new Size(344, 410);
            this.PreviewInfoSplitter.SplitterDistance = 227;
            this.PreviewInfoSplitter.TabIndex = 1;
            this.PreviewPanel.BorderStyle = BorderStyle.FixedSingle;
            this.PreviewPanel.Controls.Add(this.Preview);
            this.PreviewPanel.Dock = DockStyle.Fill;
            this.PreviewPanel.Location = new Point(0, 25);
            this.PreviewPanel.Name = "PreviewPanel";
            this.PreviewPanel.Size = new Size(344, 202);
            this.PreviewPanel.TabIndex = 1;
            this.Preview.AllowWebBrowserDrop = false;
            this.Preview.Dock = DockStyle.Fill;
            this.Preview.IsWebBrowserContextMenuEnabled = false;
            this.Preview.Location = new Point(0, 0);
            this.Preview.MinimumSize = new Size(20, 20);
            this.Preview.Name = "Preview";
            this.Preview.ScriptErrorsSuppressed = true;
            this.Preview.Size = new Size(342, 200);
            this.Preview.TabIndex = 0;
            this.Preview.Navigating += new WebBrowserNavigatingEventHandler(this.Preview_Navigating);
            this.PreviewToolbar.Items.AddRange(new ToolStripItem[]
            {
                this.EditBtn,
                this.ExploreBtn,
                this.toolStripSeparator41,
                this.PlotPointMenu
            });
            this.PreviewToolbar.Location = new Point(0, 0);
            this.PreviewToolbar.Name = "PreviewToolbar";
            this.PreviewToolbar.Size = new Size(344, 25);
            this.PreviewToolbar.TabIndex = 1;
            this.PreviewToolbar.Text = "toolStrip1";
            this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.EditBtn.Image = (Image)resources.GetObject("EditBtn.Image");
            this.EditBtn.ImageTransparentColor = Color.Magenta;
            this.EditBtn.Name = "EditBtn";
            this.EditBtn.Size = new Size(86, 22);
            this.EditBtn.Text = "Edit Plot Point";
            this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
            this.ExploreBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.ExploreBtn.Image = (Image)resources.GetObject("ExploreBtn.Image");
            this.ExploreBtn.ImageTransparentColor = Color.Magenta;
            this.ExploreBtn.Name = "ExploreBtn";
            this.ExploreBtn.Size = new Size(93, 22);
            this.ExploreBtn.Text = "Explore Subplot";
            this.ExploreBtn.Click += new EventHandler(this.ExploreBtn_Click);
            this.toolStripSeparator41.Name = "toolStripSeparator41";
            this.toolStripSeparator41.Size = new Size(6, 25);
            this.PlotPointMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.PlotPointMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.PlotPointPlayerView,
                this.toolStripSeparator35,
                this.PlotPointExportHTML,
                this.PlotPointExportFile
            });
            this.PlotPointMenu.Image = (Image)resources.GetObject("PlotPointMenu.Image");
            this.PlotPointMenu.ImageTransparentColor = Color.Magenta;
            this.PlotPointMenu.Name = "PlotPointMenu";
            this.PlotPointMenu.Size = new Size(49, 22);
            this.PlotPointMenu.Text = "Share";
            this.PlotPointPlayerView.Name = "PlotPointPlayerView";
            this.PlotPointPlayerView.Size = new Size(177, 22);
            this.PlotPointPlayerView.Text = "Send to Player View";
            this.PlotPointPlayerView.Click += new EventHandler(this.PlotPointPlayerView_Click);
            this.toolStripSeparator35.Name = "toolStripSeparator35";
            this.toolStripSeparator35.Size = new Size(174, 6);
            this.PlotPointExportHTML.Name = "PlotPointExportHTML";
            this.PlotPointExportHTML.Size = new Size(177, 22);
            this.PlotPointExportHTML.Text = "Export to HTML...";
            this.PlotPointExportHTML.Click += new EventHandler(this.PlotPointExportHTML_Click);
            this.PlotPointExportFile.Name = "PlotPointExportFile";
            this.PlotPointExportFile.Size = new Size(177, 22);
            this.PlotPointExportFile.Text = "Export to File...";
            this.PlotPointExportFile.Click += new EventHandler(this.PlotPointExportFile_Click);
            this.Pages.Controls.Add(this.WorkspacePage);
            this.Pages.Controls.Add(this.BackgroundPage);
            this.Pages.Controls.Add(this.EncyclopediaPage);
            this.Pages.Controls.Add(this.RulesPage);
            this.Pages.Controls.Add(this.AttachmentsPage);
            this.Pages.Controls.Add(this.JotterPage);
            this.Pages.Controls.Add(this.ReferencePage);
            this.Pages.Dock = DockStyle.Fill;
            this.Pages.Location = new Point(0, 24);
            this.Pages.Name = "Pages";
            this.Pages.SelectedIndex = 0;
            this.Pages.Size = new Size(864, 436);
            this.Pages.TabIndex = 5;
            this.WorkspacePage.Controls.Add(this.PreviewSplitter);
            this.WorkspacePage.Location = new Point(4, 22);
            this.WorkspacePage.Name = "WorkspacePage";
            this.WorkspacePage.Size = new Size(856, 410);
            this.WorkspacePage.TabIndex = 0;
            this.WorkspacePage.Text = "Plot Workspace";
            this.WorkspacePage.UseVisualStyleBackColor = true;
            this.BackgroundPage.Controls.Add(this.splitContainer1);
            this.BackgroundPage.Controls.Add(this.BackgroundToolbar);
            this.BackgroundPage.Location = new Point(4, 22);
            this.BackgroundPage.Name = "BackgroundPage";
            this.BackgroundPage.Size = new Size(856, 410);
            this.BackgroundPage.TabIndex = 4;
            this.BackgroundPage.Text = "Background";
            this.BackgroundPage.UseVisualStyleBackColor = true;
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.FixedPanel = FixedPanel.Panel1;
            this.splitContainer1.Location = new Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.BackgroundList);
            this.splitContainer1.Panel2.Controls.Add(this.BackgroundPanel);
            this.splitContainer1.Size = new Size(856, 385);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.TabIndex = 1;
            this.BackgroundList.Columns.AddRange(new ColumnHeader[]
            {
                this.InfoHdr
            });
            this.BackgroundList.Dock = DockStyle.Fill;
            this.BackgroundList.FullRowSelect = true;
            this.BackgroundList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.BackgroundList.HideSelection = false;
            this.BackgroundList.Location = new Point(0, 0);
            this.BackgroundList.MultiSelect = false;
            this.BackgroundList.Name = "BackgroundList";
            this.BackgroundList.Size = new Size(180, 385);
            this.BackgroundList.TabIndex = 0;
            this.BackgroundList.UseCompatibleStateImageBehavior = false;
            this.BackgroundList.View = View.Details;
            this.BackgroundList.SelectedIndexChanged += new EventHandler(this.BackgroundList_SelectedIndexChanged);
            this.BackgroundList.DoubleClick += new EventHandler(this.BackgroundEditBtn_Click);
            this.InfoHdr.Text = "Information";
            this.InfoHdr.Width = 150;
            this.BackgroundPanel.BorderStyle = BorderStyle.FixedSingle;
            this.BackgroundPanel.Controls.Add(this.BackgroundDetails);
            this.BackgroundPanel.Dock = DockStyle.Fill;
            this.BackgroundPanel.Location = new Point(0, 0);
            this.BackgroundPanel.Name = "BackgroundPanel";
            this.BackgroundPanel.Size = new Size(672, 385);
            this.BackgroundPanel.TabIndex = 0;
            this.BackgroundDetails.Dock = DockStyle.Fill;
            this.BackgroundDetails.IsWebBrowserContextMenuEnabled = false;
            this.BackgroundDetails.Location = new Point(0, 0);
            this.BackgroundDetails.MinimumSize = new Size(20, 20);
            this.BackgroundDetails.Name = "BackgroundDetails";
            this.BackgroundDetails.Size = new Size(670, 383);
            this.BackgroundDetails.TabIndex = 0;
            this.BackgroundDetails.Navigating += new WebBrowserNavigatingEventHandler(this.BackgroundDetails_Navigating);
            this.BackgroundToolbar.Items.AddRange(new ToolStripItem[]
            {
                this.BackgroundAddBtn,
                this.BackgroundRemoveBtn,
                this.BackgroundEditBtn,
                this.toolStripSeparator21,
                this.BackgroundUpBtn,
                this.BackgroundDownBtn,
                this.toolStripSeparator23,
                this.BackgroundPlayerView,
                this.toolStripSeparator48,
                this.BackgroundShareBtn
            });
            this.BackgroundToolbar.Location = new Point(0, 0);
            this.BackgroundToolbar.Name = "BackgroundToolbar";
            this.BackgroundToolbar.Size = new Size(856, 25);
            this.BackgroundToolbar.TabIndex = 0;
            this.BackgroundToolbar.Text = "toolStrip1";
            this.BackgroundAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.BackgroundAddBtn.Image = (Image)resources.GetObject("BackgroundAddBtn.Image");
            this.BackgroundAddBtn.ImageTransparentColor = Color.Magenta;
            this.BackgroundAddBtn.Name = "BackgroundAddBtn";
            this.BackgroundAddBtn.Size = new Size(33, 22);
            this.BackgroundAddBtn.Text = "Add";
            this.BackgroundAddBtn.Click += new EventHandler(this.BackgroundAddBtn_Click);
            this.BackgroundRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.BackgroundRemoveBtn.Image = (Image)resources.GetObject("BackgroundRemoveBtn.Image");
            this.BackgroundRemoveBtn.ImageTransparentColor = Color.Magenta;
            this.BackgroundRemoveBtn.Name = "BackgroundRemoveBtn";
            this.BackgroundRemoveBtn.Size = new Size(54, 22);
            this.BackgroundRemoveBtn.Text = "Remove";
            this.BackgroundRemoveBtn.Click += new EventHandler(this.BackgroundRemoveBtn_Click);
            this.BackgroundEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.BackgroundEditBtn.Image = (Image)resources.GetObject("BackgroundEditBtn.Image");
            this.BackgroundEditBtn.ImageTransparentColor = Color.Magenta;
            this.BackgroundEditBtn.Name = "BackgroundEditBtn";
            this.BackgroundEditBtn.Size = new Size(31, 22);
            this.BackgroundEditBtn.Text = "Edit";
            this.BackgroundEditBtn.Click += new EventHandler(this.BackgroundEditBtn_Click);
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new Size(6, 25);
            this.BackgroundUpBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.BackgroundUpBtn.Image = (Image)resources.GetObject("BackgroundUpBtn.Image");
            this.BackgroundUpBtn.ImageTransparentColor = Color.Magenta;
            this.BackgroundUpBtn.Name = "BackgroundUpBtn";
            this.BackgroundUpBtn.Size = new Size(59, 22);
            this.BackgroundUpBtn.Text = "Move Up";
            this.BackgroundUpBtn.Click += new EventHandler(this.BackgroundUpBtn_Click);
            this.BackgroundDownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.BackgroundDownBtn.Image = (Image)resources.GetObject("BackgroundDownBtn.Image");
            this.BackgroundDownBtn.ImageTransparentColor = Color.Magenta;
            this.BackgroundDownBtn.Name = "BackgroundDownBtn";
            this.BackgroundDownBtn.Size = new Size(75, 22);
            this.BackgroundDownBtn.Text = "Move Down";
            this.BackgroundDownBtn.Click += new EventHandler(this.BackgroundDownBtn_Click);
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new Size(6, 25);
            this.BackgroundPlayerView.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.BackgroundPlayerView.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.BackgroundPlayerViewSelected,
                this.BackgroundPlayerViewAll
            });
            this.BackgroundPlayerView.Image = (Image)resources.GetObject("BackgroundPlayerView.Image");
            this.BackgroundPlayerView.ImageTransparentColor = Color.Magenta;
            this.BackgroundPlayerView.Name = "BackgroundPlayerView";
            this.BackgroundPlayerView.Size = new Size(123, 22);
            this.BackgroundPlayerView.Text = "Send to Player View";
            this.BackgroundPlayerViewSelected.Name = "BackgroundPlayerViewSelected";
            this.BackgroundPlayerViewSelected.Size = new Size(145, 22);
            this.BackgroundPlayerViewSelected.Text = "Selected Item";
            this.BackgroundPlayerViewSelected.Click += new EventHandler(this.BackgroundPlayerViewSelected_Click);
            this.BackgroundPlayerViewAll.Name = "BackgroundPlayerViewAll";
            this.BackgroundPlayerViewAll.Size = new Size(145, 22);
            this.BackgroundPlayerViewAll.Text = "All Items";
            this.BackgroundPlayerViewAll.Click += new EventHandler(this.BackgroundPlayerViewAll_Click);
            this.toolStripSeparator48.Name = "toolStripSeparator48";
            this.toolStripSeparator48.Size = new Size(6, 25);
            this.BackgroundShareBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.BackgroundShareBtn.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.BackgroundShareExport,
                this.BackgroundShareImport,
                this.toolStripMenuItem10,
                this.BackgroundSharePublish
            });
            this.BackgroundShareBtn.Image = (Image)resources.GetObject("BackgroundShareBtn.Image");
            this.BackgroundShareBtn.ImageTransparentColor = Color.Magenta;
            this.BackgroundShareBtn.Name = "BackgroundShareBtn";
            this.BackgroundShareBtn.Size = new Size(49, 22);
            this.BackgroundShareBtn.Text = "Share";
            this.BackgroundShareExport.Name = "BackgroundShareExport";
            this.BackgroundShareExport.Size = new Size(122, 22);
            this.BackgroundShareExport.Text = "Export...";
            this.BackgroundShareExport.Click += new EventHandler(this.BackgroundShareExport_Click);
            this.BackgroundShareImport.Name = "BackgroundShareImport";
            this.BackgroundShareImport.Size = new Size(122, 22);
            this.BackgroundShareImport.Text = "Import...";
            this.BackgroundShareImport.Click += new EventHandler(this.BackgroundShareImport_Click);
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new Size(119, 6);
            this.BackgroundSharePublish.Name = "BackgroundSharePublish";
            this.BackgroundSharePublish.Size = new Size(122, 22);
            this.BackgroundSharePublish.Text = "Publish...";
            this.BackgroundSharePublish.Click += new EventHandler(this.BackgroundSharePublish_Click);
            this.EncyclopediaPage.Controls.Add(this.EncyclopediaSplitter);
            this.EncyclopediaPage.Controls.Add(this.EncyclopediaToolbar);
            this.EncyclopediaPage.Location = new Point(4, 22);
            this.EncyclopediaPage.Name = "EncyclopediaPage";
            this.EncyclopediaPage.Size = new Size(856, 410);
            this.EncyclopediaPage.TabIndex = 1;
            this.EncyclopediaPage.Text = "Encyclopedia";
            this.EncyclopediaPage.UseVisualStyleBackColor = true;
            this.EncyclopediaSplitter.Dock = DockStyle.Fill;
            this.EncyclopediaSplitter.FixedPanel = FixedPanel.Panel1;
            this.EncyclopediaSplitter.Location = new Point(0, 25);
            this.EncyclopediaSplitter.Name = "EncyclopediaSplitter";
            this.EncyclopediaSplitter.Panel1.Controls.Add(this.EntryList);
            this.EncyclopediaSplitter.Panel2.Controls.Add(this.EncyclopediaEntrySplitter);
            this.EncyclopediaSplitter.Size = new Size(856, 385);
            this.EncyclopediaSplitter.SplitterDistance = 255;
            this.EncyclopediaSplitter.TabIndex = 3;
            this.EntryList.Columns.AddRange(new ColumnHeader[]
            {
                this.EntryHdr
            });
            this.EntryList.Dock = DockStyle.Fill;
            this.EntryList.FullRowSelect = true;
            this.EntryList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.EntryList.HideSelection = false;
            this.EntryList.Location = new Point(0, 0);
            this.EntryList.MultiSelect = false;
            this.EntryList.Name = "EntryList";
            this.EntryList.Size = new Size(255, 385);
            this.EntryList.Sorting = SortOrder.Ascending;
            this.EntryList.TabIndex = 0;
            this.EntryList.UseCompatibleStateImageBehavior = false;
            this.EntryList.View = View.Details;
            this.EntryList.SelectedIndexChanged += new EventHandler(this.EntryList_SelectedIndexChanged);
            this.EntryList.DoubleClick += new EventHandler(this.EncEditBtn_Click);
            this.EntryHdr.Text = "Entries";
            this.EntryHdr.Width = 221;
            this.EncyclopediaEntrySplitter.Dock = DockStyle.Fill;
            this.EncyclopediaEntrySplitter.FixedPanel = FixedPanel.Panel2;
            this.EncyclopediaEntrySplitter.Location = new Point(0, 0);
            this.EncyclopediaEntrySplitter.Name = "EncyclopediaEntrySplitter";
            this.EncyclopediaEntrySplitter.Panel1.Controls.Add(this.EntryPanel);
            this.EncyclopediaEntrySplitter.Panel2.Controls.Add(this.EntryImageList);
            this.EncyclopediaEntrySplitter.Size = new Size(597, 385);
            this.EncyclopediaEntrySplitter.SplitterDistance = 465;
            this.EncyclopediaEntrySplitter.TabIndex = 5;
            this.EntryPanel.BorderStyle = BorderStyle.FixedSingle;
            this.EntryPanel.Controls.Add(this.EntryDetails);
            this.EntryPanel.Dock = DockStyle.Fill;
            this.EntryPanel.Location = new Point(0, 0);
            this.EntryPanel.Name = "EntryPanel";
            this.EntryPanel.Size = new Size(465, 385);
            this.EntryPanel.TabIndex = 0;
            this.EntryDetails.Dock = DockStyle.Fill;
            this.EntryDetails.IsWebBrowserContextMenuEnabled = false;
            this.EntryDetails.Location = new Point(0, 0);
            this.EntryDetails.MinimumSize = new Size(20, 20);
            this.EntryDetails.Name = "EntryDetails";
            this.EntryDetails.ScriptErrorsSuppressed = true;
            this.EntryDetails.Size = new Size(463, 383);
            this.EntryDetails.TabIndex = 4;
            this.EntryDetails.WebBrowserShortcutsEnabled = false;
            this.EntryDetails.Navigating += new WebBrowserNavigatingEventHandler(this.EntryDetails_Navigating);
            this.EntryImageList.Dock = DockStyle.Fill;
            this.EntryImageList.Location = new Point(0, 0);
            this.EntryImageList.Name = "EntryImageList";
            this.EntryImageList.Size = new Size(128, 385);
            this.EntryImageList.TabIndex = 0;
            this.EntryImageList.UseCompatibleStateImageBehavior = false;
            this.EntryImageList.DoubleClick += new EventHandler(this.EntryImageList_DoubleClick);
            this.EncyclopediaToolbar.Items.AddRange(new ToolStripItem[]
            {
                this.EncAddBtn,
                this.EncRemoveBtn,
                this.EncEditBtn,
                this.toolStripSeparator15,
                this.EncCutBtn,
                this.EncCopyBtn,
                this.EncPasteBtn,
                this.toolStripSeparator17,
                this.EncPlayerView,
                this.toolStripSeparator40,
                this.EncShareBtn,
                this.toolStripSeparator22,
                this.EncSearchLbl,
                this.EncSearchBox,
                this.EncClearLbl
            });
            this.EncyclopediaToolbar.Location = new Point(0, 0);
            this.EncyclopediaToolbar.Name = "EncyclopediaToolbar";
            this.EncyclopediaToolbar.Size = new Size(856, 25);
            this.EncyclopediaToolbar.TabIndex = 2;
            this.EncyclopediaToolbar.Text = "toolStrip1";
            this.EncAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.EncAddBtn.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.EncAddEntry,
                this.EncAddGroup
            });
            this.EncAddBtn.Image = (Image)resources.GetObject("EncAddBtn.Image");
            this.EncAddBtn.ImageTransparentColor = Color.Magenta;
            this.EncAddBtn.Name = "EncAddBtn";
            this.EncAddBtn.Size = new Size(42, 22);
            this.EncAddBtn.Text = "Add";
            this.EncAddEntry.Name = "EncAddEntry";
            this.EncAddEntry.Size = new Size(142, 22);
            this.EncAddEntry.Text = "Add an Entry";
            this.EncAddEntry.Click += new EventHandler(this.EncAddEntry_Click);
            this.EncAddGroup.Name = "EncAddGroup";
            this.EncAddGroup.Size = new Size(142, 22);
            this.EncAddGroup.Text = "Add a Group";
            this.EncAddGroup.Click += new EventHandler(this.EncAddGroup_Click);
            this.EncRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.EncRemoveBtn.Image = (Image)resources.GetObject("EncRemoveBtn.Image");
            this.EncRemoveBtn.ImageTransparentColor = Color.Magenta;
            this.EncRemoveBtn.Name = "EncRemoveBtn";
            this.EncRemoveBtn.Size = new Size(54, 22);
            this.EncRemoveBtn.Text = "Remove";
            this.EncRemoveBtn.Click += new EventHandler(this.EncRemoveBtn_Click);
            this.EncEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.EncEditBtn.Image = (Image)resources.GetObject("EncEditBtn.Image");
            this.EncEditBtn.ImageTransparentColor = Color.Magenta;
            this.EncEditBtn.Name = "EncEditBtn";
            this.EncEditBtn.Size = new Size(31, 22);
            this.EncEditBtn.Text = "Edit";
            this.EncEditBtn.Click += new EventHandler(this.EncEditBtn_Click);
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new Size(6, 25);
            this.EncCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.EncCutBtn.Image = (Image)resources.GetObject("EncCutBtn.Image");
            this.EncCutBtn.ImageTransparentColor = Color.Magenta;
            this.EncCutBtn.Name = "EncCutBtn";
            this.EncCutBtn.Size = new Size(30, 22);
            this.EncCutBtn.Text = "Cut";
            this.EncCutBtn.Click += new EventHandler(this.EncCutBtn_Click);
            this.EncCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.EncCopyBtn.Image = (Image)resources.GetObject("EncCopyBtn.Image");
            this.EncCopyBtn.ImageTransparentColor = Color.Magenta;
            this.EncCopyBtn.Name = "EncCopyBtn";
            this.EncCopyBtn.Size = new Size(39, 22);
            this.EncCopyBtn.Text = "Copy";
            this.EncCopyBtn.Click += new EventHandler(this.EncCopyBtn_Click);
            this.EncPasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.EncPasteBtn.Image = (Image)resources.GetObject("EncPasteBtn.Image");
            this.EncPasteBtn.ImageTransparentColor = Color.Magenta;
            this.EncPasteBtn.Name = "EncPasteBtn";
            this.EncPasteBtn.Size = new Size(39, 22);
            this.EncPasteBtn.Text = "Paste";
            this.EncPasteBtn.Click += new EventHandler(this.EncPasteBtn_Click);
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new Size(6, 25);
            this.EncPlayerView.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.EncPlayerView.Image = (Image)resources.GetObject("EncPlayerView.Image");
            this.EncPlayerView.ImageTransparentColor = Color.Magenta;
            this.EncPlayerView.Name = "EncPlayerView";
            this.EncPlayerView.Size = new Size(114, 22);
            this.EncPlayerView.Text = "Send to Player View";
            this.EncPlayerView.Click += new EventHandler(this.EncPlayerView_Click);
            this.toolStripSeparator40.Name = "toolStripSeparator40";
            this.toolStripSeparator40.Size = new Size(6, 25);
            this.EncShareBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.EncShareBtn.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.EncShareExport,
                this.EncShareImport,
                this.toolStripMenuItem6,
                this.EncSharePublish
            });
            this.EncShareBtn.Image = (Image)resources.GetObject("EncShareBtn.Image");
            this.EncShareBtn.ImageTransparentColor = Color.Magenta;
            this.EncShareBtn.Name = "EncShareBtn";
            this.EncShareBtn.Size = new Size(49, 22);
            this.EncShareBtn.Text = "Share";
            this.EncShareExport.Name = "EncShareExport";
            this.EncShareExport.Size = new Size(122, 22);
            this.EncShareExport.Text = "Export...";
            this.EncShareExport.Click += new EventHandler(this.EncShareExport_Click);
            this.EncShareImport.Name = "EncShareImport";
            this.EncShareImport.Size = new Size(122, 22);
            this.EncShareImport.Text = "Import...";
            this.EncShareImport.Click += new EventHandler(this.EncShareImport_Click);
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new Size(119, 6);
            this.EncSharePublish.Name = "EncSharePublish";
            this.EncSharePublish.Size = new Size(122, 22);
            this.EncSharePublish.Text = "Publish...";
            this.EncSharePublish.Click += new EventHandler(this.EncSharePublish_Click);
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new Size(6, 25);
            this.EncSearchLbl.Name = "EncSearchLbl";
            this.EncSearchLbl.Size = new Size(45, 22);
            this.EncSearchLbl.Text = "Search:";
            this.EncSearchBox.BorderStyle = BorderStyle.FixedSingle;
            this.EncSearchBox.Name = "EncSearchBox";
            this.EncSearchBox.Size = new Size(150, 25);
            this.EncSearchBox.TextChanged += new EventHandler(this.EncSearchBox_TextChanged);
            this.EncClearLbl.IsLink = true;
            this.EncClearLbl.Name = "EncClearLbl";
            this.EncClearLbl.Size = new Size(34, 22);
            this.EncClearLbl.Text = "Clear";
            this.EncClearLbl.Click += new EventHandler(this.EncClearLbl_Click);
            this.RulesPage.Controls.Add(this.RulesSplitter);
            this.RulesPage.Location = new Point(4, 22);
            this.RulesPage.Name = "RulesPage";
            this.RulesPage.Size = new Size(856, 410);
            this.RulesPage.TabIndex = 5;
            this.RulesPage.Text = "Campaign Rules";
            this.RulesPage.UseVisualStyleBackColor = true;
            this.RulesSplitter.Dock = DockStyle.Fill;
            this.RulesSplitter.FixedPanel = FixedPanel.Panel1;
            this.RulesSplitter.Location = new Point(0, 0);
            this.RulesSplitter.Name = "RulesSplitter";
            this.RulesSplitter.Panel1.Controls.Add(this.RulesList);
            this.RulesSplitter.Panel1.Controls.Add(this.RulesToolbar);
            this.RulesSplitter.Panel2.Controls.Add(this.RulesBrowserPanel);
            this.RulesSplitter.Panel2.Controls.Add(this.EncEntryToolbar);
            this.RulesSplitter.Size = new Size(856, 410);
            this.RulesSplitter.SplitterDistance = 231;
            this.RulesSplitter.TabIndex = 1;
            this.RulesList.Columns.AddRange(new ColumnHeader[]
            {
                this.RulesHdr
            });
            this.RulesList.Dock = DockStyle.Fill;
            this.RulesList.FullRowSelect = true;
            listViewGroup.Header = "Races";
            listViewGroup.Name = "listViewGroup1";
            listViewGroup2.Header = "Classes";
            listViewGroup2.Name = "listViewGroup9";
            listViewGroup3.Header = "Themes";
            listViewGroup3.Name = "listViewGroup14";
            listViewGroup4.Header = "Paragon Paths";
            listViewGroup4.Name = "listViewGroup2";
            listViewGroup5.Header = "Epic Destinies";
            listViewGroup5.Name = "listViewGroup3";
            listViewGroup6.Header = "Backgrounds";
            listViewGroup6.Name = "listViewGroup4";
            listViewGroup7.Header = "Feats (heroic tier)";
            listViewGroup7.Name = "listViewGroup5";
            listViewGroup8.Header = "Feats (paragon tier)";
            listViewGroup8.Name = "listViewGroup6";
            listViewGroup9.Header = "Feats (epic tier)";
            listViewGroup9.Name = "listViewGroup7";
            listViewGroup10.Header = "Weapons";
            listViewGroup10.Name = "listViewGroup10";
            listViewGroup11.Header = "Rituals";
            listViewGroup11.Name = "listViewGroup8";
            listViewGroup12.Header = "Creature Lore";
            listViewGroup12.Name = "listViewGroup11";
            listViewGroup13.Header = "Diseases";
            listViewGroup13.Name = "listViewGroup12";
            listViewGroup14.Header = "Poisons";
            listViewGroup14.Name = "listViewGroup13";
            this.RulesList.Groups.AddRange(new ListViewGroup[]
            {
                listViewGroup,
                listViewGroup2,
                listViewGroup3,
                listViewGroup4,
                listViewGroup5,
                listViewGroup6,
                listViewGroup7,
                listViewGroup8,
                listViewGroup9,
                listViewGroup10,
                listViewGroup11,
                listViewGroup12,
                listViewGroup13,
                listViewGroup14
            });
            this.RulesList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.RulesList.HideSelection = false;
            this.RulesList.Location = new Point(0, 25);
            this.RulesList.MultiSelect = false;
            this.RulesList.Name = "RulesList";
            this.RulesList.Size = new Size(231, 385);
            this.RulesList.Sorting = SortOrder.Ascending;
            this.RulesList.TabIndex = 1;
            this.RulesList.UseCompatibleStateImageBehavior = false;
            this.RulesList.View = View.Details;
            this.RulesList.SelectedIndexChanged += new EventHandler(this.RulesList_SelectedIndexChanged);
            this.RulesList.DoubleClick += new EventHandler(this.RulesEditBtn_Click);
            this.RulesHdr.Text = "Rules Elements";
            this.RulesHdr.Width = 193;
            this.RulesToolbar.Items.AddRange(new ToolStripItem[]
            {
                this.RulesAddBtn,
                this.toolStripSeparator33,
                this.RulesShareBtn
            });
            this.RulesToolbar.Location = new Point(0, 0);
            this.RulesToolbar.Name = "RulesToolbar";
            this.RulesToolbar.Size = new Size(231, 25);
            this.RulesToolbar.TabIndex = 0;
            this.RulesToolbar.Text = "toolStrip1";
            this.RulesAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.RulesAddBtn.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.AddRace,
                this.toolStripSeparator31,
                this.AddClass,
                this.AddTheme,
                this.AddParagonPath,
                this.AddEpicDestiny,
                this.toolStripSeparator32,
                this.AddBackground,
                this.AddFeat,
                this.AddWeapon,
                this.AddRitual,
                this.toolStripSeparator39,
                this.AddCreatureLore,
                this.AddDisease,
                this.AddPoison
            });
            this.RulesAddBtn.Image = (Image)resources.GetObject("RulesAddBtn.Image");
            this.RulesAddBtn.ImageTransparentColor = Color.Magenta;
            this.RulesAddBtn.Name = "RulesAddBtn";
            this.RulesAddBtn.Size = new Size(42, 22);
            this.RulesAddBtn.Text = "Add";
            this.AddRace.Name = "AddRace";
            this.AddRace.Size = new Size(145, 22);
            this.AddRace.Text = "Race";
            this.AddRace.Click += new EventHandler(this.AddRace_Click);
            this.toolStripSeparator31.Name = "toolStripSeparator31";
            this.toolStripSeparator31.Size = new Size(142, 6);
            this.AddClass.Name = "AddClass";
            this.AddClass.Size = new Size(145, 22);
            this.AddClass.Text = "Class";
            this.AddClass.Click += new EventHandler(this.AddClass_Click);
            this.AddTheme.Name = "AddTheme";
            this.AddTheme.Size = new Size(145, 22);
            this.AddTheme.Text = "Theme";
            this.AddTheme.Click += new EventHandler(this.AddTheme_Click);
            this.AddParagonPath.Name = "AddParagonPath";
            this.AddParagonPath.Size = new Size(145, 22);
            this.AddParagonPath.Text = "Paragon Path";
            this.AddParagonPath.Click += new EventHandler(this.AddParagonPath_Click);
            this.AddEpicDestiny.Name = "AddEpicDestiny";
            this.AddEpicDestiny.Size = new Size(145, 22);
            this.AddEpicDestiny.Text = "Epic Destiny";
            this.AddEpicDestiny.Click += new EventHandler(this.AddEpicDestiny_Click);
            this.toolStripSeparator32.Name = "toolStripSeparator32";
            this.toolStripSeparator32.Size = new Size(142, 6);
            this.AddBackground.Name = "AddBackground";
            this.AddBackground.Size = new Size(145, 22);
            this.AddBackground.Text = "Background";
            this.AddBackground.Click += new EventHandler(this.AddBackground_Click);
            this.AddFeat.Name = "AddFeat";
            this.AddFeat.Size = new Size(145, 22);
            this.AddFeat.Text = "Feat";
            this.AddFeat.Click += new EventHandler(this.AddFeat_Click);
            this.AddWeapon.Name = "AddWeapon";
            this.AddWeapon.Size = new Size(145, 22);
            this.AddWeapon.Text = "Weapon";
            this.AddWeapon.Click += new EventHandler(this.AddWeapon_Click);
            this.AddRitual.Name = "AddRitual";
            this.AddRitual.Size = new Size(145, 22);
            this.AddRitual.Text = "Ritual";
            this.AddRitual.Click += new EventHandler(this.AddRitual_Click);
            this.toolStripSeparator39.Name = "toolStripSeparator39";
            this.toolStripSeparator39.Size = new Size(142, 6);
            this.AddCreatureLore.Name = "AddCreatureLore";
            this.AddCreatureLore.Size = new Size(145, 22);
            this.AddCreatureLore.Text = "Creature Lore";
            this.AddCreatureLore.Click += new EventHandler(this.AddCreatureLore_Click);
            this.AddDisease.Name = "AddDisease";
            this.AddDisease.Size = new Size(145, 22);
            this.AddDisease.Text = "Disease";
            this.AddDisease.Click += new EventHandler(this.AddDisease_Click);
            this.AddPoison.Name = "AddPoison";
            this.AddPoison.Size = new Size(145, 22);
            this.AddPoison.Text = "Poison";
            this.AddPoison.Click += new EventHandler(this.AddPoison_Click);
            this.toolStripSeparator33.Name = "toolStripSeparator33";
            this.toolStripSeparator33.Size = new Size(6, 25);
            this.RulesShareBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.RulesShareBtn.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.RulesShareExport,
                this.RulesShareImport,
                this.toolStripMenuItem9,
                this.RulesSharePublish
            });
            this.RulesShareBtn.Image = (Image)resources.GetObject("RulesShareBtn.Image");
            this.RulesShareBtn.ImageTransparentColor = Color.Magenta;
            this.RulesShareBtn.Name = "RulesShareBtn";
            this.RulesShareBtn.Size = new Size(49, 22);
            this.RulesShareBtn.Text = "Share";
            this.RulesShareExport.Name = "RulesShareExport";
            this.RulesShareExport.Size = new Size(122, 22);
            this.RulesShareExport.Text = "Export...";
            this.RulesShareExport.Click += new EventHandler(this.RulesShareExport_Click);
            this.RulesShareImport.Name = "RulesShareImport";
            this.RulesShareImport.Size = new Size(122, 22);
            this.RulesShareImport.Text = "Import...";
            this.RulesShareImport.Click += new EventHandler(this.RulesShareImport_Click);
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new Size(119, 6);
            this.RulesSharePublish.Name = "RulesSharePublish";
            this.RulesSharePublish.Size = new Size(122, 22);
            this.RulesSharePublish.Text = "Publish...";
            this.RulesSharePublish.Click += new EventHandler(this.RulesSharePublish_Click);
            this.RulesBrowserPanel.BorderStyle = BorderStyle.FixedSingle;
            this.RulesBrowserPanel.Controls.Add(this.RulesBrowser);
            this.RulesBrowserPanel.Dock = DockStyle.Fill;
            this.RulesBrowserPanel.Location = new Point(0, 25);
            this.RulesBrowserPanel.Name = "RulesBrowserPanel";
            this.RulesBrowserPanel.Size = new Size(621, 385);
            this.RulesBrowserPanel.TabIndex = 0;
            this.RulesBrowser.Dock = DockStyle.Fill;
            this.RulesBrowser.IsWebBrowserContextMenuEnabled = false;
            this.RulesBrowser.Location = new Point(0, 0);
            this.RulesBrowser.MinimumSize = new Size(20, 20);
            this.RulesBrowser.Name = "RulesBrowser";
            this.RulesBrowser.ScriptErrorsSuppressed = true;
            this.RulesBrowser.Size = new Size(619, 383);
            this.RulesBrowser.TabIndex = 1;
            this.RulesBrowser.WebBrowserShortcutsEnabled = false;
            this.EncEntryToolbar.Items.AddRange(new ToolStripItem[]
            {
                this.RulesRemoveBtn,
                this.RulesEditBtn,
                this.toolStripSeparator43,
                this.RuleEncyclopediaBtn,
                this.toolStripSeparator36,
                this.RulesPlayerViewBtn
            });
            this.EncEntryToolbar.Location = new Point(0, 0);
            this.EncEntryToolbar.Name = "EncEntryToolbar";
            this.EncEntryToolbar.Size = new Size(621, 25);
            this.EncEntryToolbar.TabIndex = 2;
            this.EncEntryToolbar.Text = "toolStrip1";
            this.RulesRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.RulesRemoveBtn.Image = (Image)resources.GetObject("RulesRemoveBtn.Image");
            this.RulesRemoveBtn.ImageTransparentColor = Color.Magenta;
            this.RulesRemoveBtn.Name = "RulesRemoveBtn";
            this.RulesRemoveBtn.Size = new Size(54, 22);
            this.RulesRemoveBtn.Text = "Remove";
            this.RulesRemoveBtn.Click += new EventHandler(this.RulesRemoveBtn_Click);
            this.RulesEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.RulesEditBtn.Image = (Image)resources.GetObject("RulesEditBtn.Image");
            this.RulesEditBtn.ImageTransparentColor = Color.Magenta;
            this.RulesEditBtn.Name = "RulesEditBtn";
            this.RulesEditBtn.Size = new Size(31, 22);
            this.RulesEditBtn.Text = "Edit";
            this.RulesEditBtn.Click += new EventHandler(this.RulesEditBtn_Click);
            this.toolStripSeparator43.Name = "toolStripSeparator43";
            this.toolStripSeparator43.Size = new Size(6, 25);
            this.RuleEncyclopediaBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.RuleEncyclopediaBtn.Image = (Image)resources.GetObject("RuleEncyclopediaBtn.Image");
            this.RuleEncyclopediaBtn.ImageTransparentColor = Color.Magenta;
            this.RuleEncyclopediaBtn.Name = "RuleEncyclopediaBtn";
            this.RuleEncyclopediaBtn.Size = new Size(111, 22);
            this.RuleEncyclopediaBtn.Text = "Encyclopedia Entry";
            this.RuleEncyclopediaBtn.Click += new EventHandler(this.RuleEncyclopediaBtn_Click);
            this.toolStripSeparator36.Name = "toolStripSeparator36";
            this.toolStripSeparator36.Size = new Size(6, 25);
            this.RulesPlayerViewBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.RulesPlayerViewBtn.Image = (Image)resources.GetObject("RulesPlayerViewBtn.Image");
            this.RulesPlayerViewBtn.ImageTransparentColor = Color.Magenta;
            this.RulesPlayerViewBtn.Name = "RulesPlayerViewBtn";
            this.RulesPlayerViewBtn.Size = new Size(114, 22);
            this.RulesPlayerViewBtn.Text = "Send to Player View";
            this.RulesPlayerViewBtn.Click += new EventHandler(this.RulesPlayerViewBtn_Click);
            this.AttachmentsPage.Controls.Add(this.AttachmentList);
            this.AttachmentsPage.Controls.Add(this.AttachmentToolbar);
            this.AttachmentsPage.Location = new Point(4, 22);
            this.AttachmentsPage.Name = "AttachmentsPage";
            this.AttachmentsPage.Size = new Size(856, 410);
            this.AttachmentsPage.TabIndex = 3;
            this.AttachmentsPage.Text = "Attachments";
            this.AttachmentsPage.UseVisualStyleBackColor = true;
            this.AttachmentList.AllowDrop = true;
            this.AttachmentList.Columns.AddRange(new ColumnHeader[]
            {
                this.AttachmentHdr,
                this.AttachmentSizeHdr
            });
            this.AttachmentList.Dock = DockStyle.Fill;
            this.AttachmentList.FullRowSelect = true;
            this.AttachmentList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.AttachmentList.HideSelection = false;
            this.AttachmentList.Location = new Point(0, 25);
            this.AttachmentList.Name = "AttachmentList";
            this.AttachmentList.Size = new Size(856, 385);
            this.AttachmentList.TabIndex = 1;
            this.AttachmentList.UseCompatibleStateImageBehavior = false;
            this.AttachmentList.View = View.Details;
            this.AttachmentList.DoubleClick += new EventHandler(this.AttachmentExtractAndRun_Click);
            this.AttachmentList.DragDrop += new DragEventHandler(this.AttachmentList_DragDrop);
            this.AttachmentList.DragOver += new DragEventHandler(this.AttachmentList_DragOver);
            this.AttachmentHdr.Text = "Attachment";
            this.AttachmentHdr.Width = 500;
            this.AttachmentSizeHdr.Text = "Size";
            this.AttachmentSizeHdr.TextAlign = HorizontalAlignment.Right;
            this.AttachmentSizeHdr.Width = 100;
            this.AttachmentToolbar.Items.AddRange(new ToolStripItem[]
            {
                this.AttachmentImportBtn,
                this.AttachmentRemoveBtn,
                this.toolStripSeparator19,
                this.AttachmentExtract,
                this.toolStripSeparator24,
                this.AttachmentPlayerView
            });
            this.AttachmentToolbar.Location = new Point(0, 0);
            this.AttachmentToolbar.Name = "AttachmentToolbar";
            this.AttachmentToolbar.Size = new Size(856, 25);
            this.AttachmentToolbar.TabIndex = 0;
            this.AttachmentToolbar.Text = "toolStrip1";
            this.AttachmentImportBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.AttachmentImportBtn.Image = (Image)resources.GetObject("AttachmentImportBtn.Image");
            this.AttachmentImportBtn.ImageTransparentColor = Color.Magenta;
            this.AttachmentImportBtn.Name = "AttachmentImportBtn";
            this.AttachmentImportBtn.Size = new Size(47, 22);
            this.AttachmentImportBtn.Text = "Import";
            this.AttachmentImportBtn.Click += new EventHandler(this.AttachmentImportBtn_Click);
            this.AttachmentRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.AttachmentRemoveBtn.Image = (Image)resources.GetObject("AttachmentRemoveBtn.Image");
            this.AttachmentRemoveBtn.ImageTransparentColor = Color.Magenta;
            this.AttachmentRemoveBtn.Name = "AttachmentRemoveBtn";
            this.AttachmentRemoveBtn.Size = new Size(54, 22);
            this.AttachmentRemoveBtn.Text = "Remove";
            this.AttachmentRemoveBtn.Click += new EventHandler(this.AttachmentRemoveBtn_Click);
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new Size(6, 25);
            this.AttachmentExtract.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.AttachmentExtract.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.AttachmentExtractSimple,
                this.AttachmentExtractAndRun
            });
            this.AttachmentExtract.Image = (Image)resources.GetObject("AttachmentExtract.Image");
            this.AttachmentExtract.ImageTransparentColor = Color.Magenta;
            this.AttachmentExtract.Name = "AttachmentExtract";
            this.AttachmentExtract.Size = new Size(55, 22);
            this.AttachmentExtract.Text = "Extract";
            this.AttachmentExtractSimple.Name = "AttachmentExtractSimple";
            this.AttachmentExtractSimple.Size = new Size(224, 22);
            this.AttachmentExtractSimple.Text = "Extract to Desktop";
            this.AttachmentExtractSimple.Click += new EventHandler(this.AttachmentExtractSimple_Click);
            this.AttachmentExtractAndRun.Name = "AttachmentExtractAndRun";
            this.AttachmentExtractAndRun.Size = new Size(224, 22);
            this.AttachmentExtractAndRun.Text = "Extract to Desktop and Open";
            this.AttachmentExtractAndRun.Click += new EventHandler(this.AttachmentExtractAndRun_Click);
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new Size(6, 25);
            this.AttachmentPlayerView.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.AttachmentPlayerView.Image = (Image)resources.GetObject("AttachmentPlayerView.Image");
            this.AttachmentPlayerView.ImageTransparentColor = Color.Magenta;
            this.AttachmentPlayerView.Name = "AttachmentPlayerView";
            this.AttachmentPlayerView.Size = new Size(114, 22);
            this.AttachmentPlayerView.Text = "Send to Player View";
            this.AttachmentPlayerView.Click += new EventHandler(this.AttachmentSendBtn_Click);
            this.JotterPage.Controls.Add(this.JotterSplitter);
            this.JotterPage.Controls.Add(this.JotterToolbar);
            this.JotterPage.Location = new Point(4, 22);
            this.JotterPage.Name = "JotterPage";
            this.JotterPage.Size = new Size(856, 410);
            this.JotterPage.TabIndex = 2;
            this.JotterPage.Text = "Jotter";
            this.JotterPage.UseVisualStyleBackColor = true;
            this.JotterSplitter.Dock = DockStyle.Fill;
            this.JotterSplitter.FixedPanel = FixedPanel.Panel1;
            this.JotterSplitter.Location = new Point(0, 25);
            this.JotterSplitter.Name = "JotterSplitter";
            this.JotterSplitter.Panel1.Controls.Add(this.NoteList);
            this.JotterSplitter.Panel2.Controls.Add(this.NoteBox);
            this.JotterSplitter.Size = new Size(856, 385);
            this.JotterSplitter.SplitterDistance = 180;
            this.JotterSplitter.TabIndex = 1;
            this.NoteList.Columns.AddRange(new ColumnHeader[]
            {
                this.NoteHdr
            });
            this.NoteList.Dock = DockStyle.Fill;
            this.NoteList.FullRowSelect = true;
            listViewGroup15.Header = "Issues";
            listViewGroup15.Name = "IssueGroup";
            listViewGroup16.Header = "Information";
            listViewGroup16.Name = "InfoGroup";
            listViewGroup17.Header = "Notes";
            listViewGroup17.Name = "NoteGroup";
            this.NoteList.Groups.AddRange(new ListViewGroup[]
            {
                listViewGroup15,
                listViewGroup16,
                listViewGroup17
            });
            this.NoteList.HeaderStyle = ColumnHeaderStyle.None;
            this.NoteList.HideSelection = false;
            this.NoteList.Location = new Point(0, 0);
            this.NoteList.MultiSelect = false;
            this.NoteList.Name = "NoteList";
            this.NoteList.Size = new Size(180, 385);
            this.NoteList.Sorting = SortOrder.Ascending;
            this.NoteList.TabIndex = 0;
            this.NoteList.UseCompatibleStateImageBehavior = false;
            this.NoteList.View = View.Details;
            this.NoteList.SelectedIndexChanged += new EventHandler(this.NoteList_SelectedIndexChanged);
            this.NoteHdr.Text = "Notes";
            this.NoteHdr.Width = 150;
            this.NoteBox.BorderStyle = BorderStyle.FixedSingle;
            this.NoteBox.Dock = DockStyle.Fill;
            this.NoteBox.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.NoteBox.Location = new Point(0, 0);
            this.NoteBox.Multiline = true;
            this.NoteBox.Name = "NoteBox";
            this.NoteBox.ScrollBars = ScrollBars.Vertical;
            this.NoteBox.Size = new Size(672, 385);
            this.NoteBox.TabIndex = 0;
            this.NoteBox.TextChanged += new EventHandler(this.NoteBox_TextChanged);
            this.JotterToolbar.Items.AddRange(new ToolStripItem[]
            {
                this.NoteAddBtn,
                this.NoteRemoveBtn,
                this.toolStripSeparator16,
                this.NoteCategoryBtn,
                this.toolStripSeparator38,
                this.NoteCutBtn,
                this.NoteCopyBtn,
                this.NotePasteBtn,
                this.toolStripSeparator18,
                this.NoteSearchLbl,
                this.NoteSearchBox,
                this.NoteClearLbl
            });
            this.JotterToolbar.Location = new Point(0, 0);
            this.JotterToolbar.Name = "JotterToolbar";
            this.JotterToolbar.Size = new Size(856, 25);
            this.JotterToolbar.TabIndex = 0;
            this.JotterToolbar.Text = "toolStrip1";
            this.NoteAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.NoteAddBtn.Image = (Image)resources.GetObject("NoteAddBtn.Image");
            this.NoteAddBtn.ImageTransparentColor = Color.Magenta;
            this.NoteAddBtn.Name = "NoteAddBtn";
            this.NoteAddBtn.Size = new Size(62, 22);
            this.NoteAddBtn.Text = "Add Note";
            this.NoteAddBtn.Click += new EventHandler(this.NoteAddBtn_Click);
            this.NoteRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.NoteRemoveBtn.Image = (Image)resources.GetObject("NoteRemoveBtn.Image");
            this.NoteRemoveBtn.ImageTransparentColor = Color.Magenta;
            this.NoteRemoveBtn.Name = "NoteRemoveBtn";
            this.NoteRemoveBtn.Size = new Size(83, 22);
            this.NoteRemoveBtn.Text = "Remove Note";
            this.NoteRemoveBtn.Click += new EventHandler(this.NoteRemoveBtn_Click);
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new Size(6, 25);
            this.NoteCategoryBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.NoteCategoryBtn.Image = (Image)resources.GetObject("NoteCategoryBtn.Image");
            this.NoteCategoryBtn.ImageTransparentColor = Color.Magenta;
            this.NoteCategoryBtn.Name = "NoteCategoryBtn";
            this.NoteCategoryBtn.Size = new Size(78, 22);
            this.NoteCategoryBtn.Text = "Set Category";
            this.NoteCategoryBtn.Click += new EventHandler(this.NoteCategoryBtn_Click);
            this.toolStripSeparator38.Name = "toolStripSeparator38";
            this.toolStripSeparator38.Size = new Size(6, 25);
            this.NoteCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.NoteCutBtn.Image = (Image)resources.GetObject("NoteCutBtn.Image");
            this.NoteCutBtn.ImageTransparentColor = Color.Magenta;
            this.NoteCutBtn.Name = "NoteCutBtn";
            this.NoteCutBtn.Size = new Size(30, 22);
            this.NoteCutBtn.Text = "Cut";
            this.NoteCutBtn.Click += new EventHandler(this.NoteCutBtn_Click);
            this.NoteCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.NoteCopyBtn.Image = (Image)resources.GetObject("NoteCopyBtn.Image");
            this.NoteCopyBtn.ImageTransparentColor = Color.Magenta;
            this.NoteCopyBtn.Name = "NoteCopyBtn";
            this.NoteCopyBtn.Size = new Size(39, 22);
            this.NoteCopyBtn.Text = "Copy";
            this.NoteCopyBtn.Click += new EventHandler(this.NoteCopyBtn_Click);
            this.NotePasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.NotePasteBtn.Image = (Image)resources.GetObject("NotePasteBtn.Image");
            this.NotePasteBtn.ImageTransparentColor = Color.Magenta;
            this.NotePasteBtn.Name = "NotePasteBtn";
            this.NotePasteBtn.Size = new Size(39, 22);
            this.NotePasteBtn.Text = "Paste";
            this.NotePasteBtn.Click += new EventHandler(this.NotePasteBtn_Click);
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new Size(6, 25);
            this.NoteSearchLbl.Name = "NoteSearchLbl";
            this.NoteSearchLbl.Size = new Size(45, 22);
            this.NoteSearchLbl.Text = "Search:";
            this.NoteSearchBox.BorderStyle = BorderStyle.FixedSingle;
            this.NoteSearchBox.Name = "NoteSearchBox";
            this.NoteSearchBox.Size = new Size(150, 25);
            this.NoteSearchBox.TextChanged += new EventHandler(this.NoteSearchBox_TextChanged);
            this.NoteClearLbl.IsLink = true;
            this.NoteClearLbl.Name = "NoteClearLbl";
            this.NoteClearLbl.Size = new Size(34, 22);
            this.NoteClearLbl.Text = "Clear";
            this.NoteClearLbl.Click += new EventHandler(this.NoteClearLbl_Click);
            this.ReferencePage.Controls.Add(this.ReferenceSplitter);
            this.ReferencePage.Location = new Point(4, 22);
            this.ReferencePage.Name = "ReferencePage";
            this.ReferencePage.Size = new Size(856, 410);
            this.ReferencePage.TabIndex = 6;
            this.ReferencePage.Text = "In-Session Reference";
            this.ReferencePage.UseVisualStyleBackColor = true;
            this.ReferenceSplitter.Dock = DockStyle.Fill;
            this.ReferenceSplitter.FixedPanel = FixedPanel.Panel2;
            this.ReferenceSplitter.Location = new Point(0, 0);
            this.ReferenceSplitter.Name = "ReferenceSplitter";
            this.ReferenceSplitter.Panel1.Controls.Add(this.ReferencePages);
            this.ReferenceSplitter.Panel2.Controls.Add(this.InfoPanel);
            this.ReferenceSplitter.Panel2.Controls.Add(this.ReferenceToolbar);
            this.ReferenceSplitter.Size = new Size(856, 410);
            this.ReferenceSplitter.SplitterDistance = 594;
            this.ReferenceSplitter.TabIndex = 1;
            this.ReferencePages.Alignment = TabAlignment.Left;
            this.ReferencePages.Controls.Add(this.PartyPage);
            this.ReferencePages.Controls.Add(this.ToolsPage);
            this.ReferencePages.Controls.Add(this.CompendiumPage);
            this.ReferencePages.Dock = DockStyle.Fill;
            this.ReferencePages.Location = new Point(0, 0);
            this.ReferencePages.Multiline = true;
            this.ReferencePages.Name = "ReferencePages";
            this.ReferencePages.SelectedIndex = 0;
            this.ReferencePages.Size = new Size(594, 410);
            this.ReferencePages.TabIndex = 0;
            this.ReferencePages.SelectedIndexChanged += new EventHandler(this.ReferencePages_SelectedIndexChanged);
            this.PartyPage.Controls.Add(this.PartyBrowser);
            this.PartyPage.Location = new Point(23, 4);
            this.PartyPage.Name = "PartyPage";
            this.PartyPage.Size = new Size(567, 402);
            this.PartyPage.TabIndex = 0;
            this.PartyPage.Text = "Party Breakdown";
            this.PartyPage.UseVisualStyleBackColor = true;
            this.PartyBrowser.AllowWebBrowserDrop = false;
            this.PartyBrowser.Dock = DockStyle.Fill;
            this.PartyBrowser.IsWebBrowserContextMenuEnabled = false;
            this.PartyBrowser.Location = new Point(0, 0);
            this.PartyBrowser.MinimumSize = new Size(20, 20);
            this.PartyBrowser.Name = "PartyBrowser";
            this.PartyBrowser.ScriptErrorsSuppressed = true;
            this.PartyBrowser.Size = new Size(567, 402);
            this.PartyBrowser.TabIndex = 0;
            this.PartyBrowser.WebBrowserShortcutsEnabled = false;
            this.PartyBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.PartyBrowser_Navigating);
            this.ToolsPage.Controls.Add(this.ToolBrowserPanel);
            this.ToolsPage.Controls.Add(this.GeneratorToolbar);
            this.ToolsPage.Location = new Point(23, 4);
            this.ToolsPage.Name = "ToolsPage";
            this.ToolsPage.Size = new Size(567, 402);
            this.ToolsPage.TabIndex = 1;
            this.ToolsPage.Text = "Random Generators";
            this.ToolsPage.UseVisualStyleBackColor = true;
            this.ToolBrowserPanel.BorderStyle = BorderStyle.FixedSingle;
            this.ToolBrowserPanel.Controls.Add(this.GeneratorBrowser);
            this.ToolBrowserPanel.Dock = DockStyle.Fill;
            this.ToolBrowserPanel.Location = new Point(107, 0);
            this.ToolBrowserPanel.Name = "ToolBrowserPanel";
            this.ToolBrowserPanel.Size = new Size(460, 402);
            this.ToolBrowserPanel.TabIndex = 3;
            this.GeneratorBrowser.AllowWebBrowserDrop = false;
            this.GeneratorBrowser.Dock = DockStyle.Fill;
            this.GeneratorBrowser.IsWebBrowserContextMenuEnabled = false;
            this.GeneratorBrowser.Location = new Point(0, 0);
            this.GeneratorBrowser.MinimumSize = new Size(20, 20);
            this.GeneratorBrowser.Name = "GeneratorBrowser";
            this.GeneratorBrowser.ScriptErrorsSuppressed = true;
            this.GeneratorBrowser.Size = new Size(458, 400);
            this.GeneratorBrowser.TabIndex = 1;
            this.GeneratorBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.GeneratorBrowser_Navigating);
            this.GeneratorToolbar.Dock = DockStyle.Left;
            this.GeneratorToolbar.GripStyle = ToolStripGripStyle.Hidden;
            this.GeneratorToolbar.Items.AddRange(new ToolStripItem[]
            {
                this.toolStripLabel1,
                this.toolStripSeparator26,
                this.ElfNameBtn,
                this.DwarfNameBtn,
                this.HalflingNameBtn,
                this.ExoticNameBtn,
                this.toolStripSeparator44,
                this.TreasureBtn,
                this.BookTitleBtn,
                this.PotionBtn,
                this.toolStripSeparator45,
                this.NPCBtn,
                this.RoomBtn,
                this.toolStripSeparator46,
                this.ElfTextBtn,
                this.DwarfTextBtn,
                this.PrimordialTextBtn
            });
            this.GeneratorToolbar.Location = new Point(0, 0);
            this.GeneratorToolbar.Name = "GeneratorToolbar";
            this.GeneratorToolbar.ShowItemToolTips = false;
            this.GeneratorToolbar.Size = new Size(107, 402);
            this.GeneratorToolbar.TabIndex = 2;
            this.GeneratorToolbar.Text = "toolStrip1";
            this.toolStripLabel1.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new Size(104, 15);
            this.toolStripLabel1.Text = "Generators";
            this.toolStripSeparator26.Name = "toolStripSeparator26";
            this.toolStripSeparator26.Size = new Size(104, 6);
            this.ElfNameBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.ElfNameBtn.Image = (Image)resources.GetObject("ElfNameBtn.Image");
            this.ElfNameBtn.ImageTransparentColor = Color.Magenta;
            this.ElfNameBtn.Name = "ElfNameBtn";
            this.ElfNameBtn.Size = new Size(104, 19);
            this.ElfNameBtn.Text = "Elvish Names";
            this.ElfNameBtn.TextAlign = ContentAlignment.MiddleLeft;
            this.ElfNameBtn.Click += new EventHandler(this.ElfNameBtn_Click);
            this.DwarfNameBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.DwarfNameBtn.Image = (Image)resources.GetObject("DwarfNameBtn.Image");
            this.DwarfNameBtn.ImageTransparentColor = Color.Magenta;
            this.DwarfNameBtn.Name = "DwarfNameBtn";
            this.DwarfNameBtn.Size = new Size(104, 19);
            this.DwarfNameBtn.Text = "Dwarvish Names";
            this.DwarfNameBtn.TextAlign = ContentAlignment.MiddleLeft;
            this.DwarfNameBtn.Click += new EventHandler(this.DwarfNameBtn_Click);
            this.HalflingNameBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.HalflingNameBtn.Image = (Image)resources.GetObject("HalflingNameBtn.Image");
            this.HalflingNameBtn.ImageTransparentColor = Color.Magenta;
            this.HalflingNameBtn.Name = "HalflingNameBtn";
            this.HalflingNameBtn.Size = new Size(104, 19);
            this.HalflingNameBtn.Text = "Halfling Names";
            this.HalflingNameBtn.TextAlign = ContentAlignment.MiddleLeft;
            this.HalflingNameBtn.Click += new EventHandler(this.HalflingNameBtn_Click);
            this.ExoticNameBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.ExoticNameBtn.Image = (Image)resources.GetObject("ExoticNameBtn.Image");
            this.ExoticNameBtn.ImageTransparentColor = Color.Magenta;
            this.ExoticNameBtn.Name = "ExoticNameBtn";
            this.ExoticNameBtn.Size = new Size(104, 19);
            this.ExoticNameBtn.Text = "Exotic Names";
            this.ExoticNameBtn.TextAlign = ContentAlignment.MiddleLeft;
            this.ExoticNameBtn.Click += new EventHandler(this.ExoticNameBtn_Click);
            this.toolStripSeparator44.Name = "toolStripSeparator44";
            this.toolStripSeparator44.Size = new Size(104, 6);
            this.TreasureBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.TreasureBtn.Image = (Image)resources.GetObject("TreasureBtn.Image");
            this.TreasureBtn.ImageTransparentColor = Color.Magenta;
            this.TreasureBtn.Name = "TreasureBtn";
            this.TreasureBtn.Size = new Size(104, 19);
            this.TreasureBtn.Text = "Art Objects";
            this.TreasureBtn.TextAlign = ContentAlignment.MiddleLeft;
            this.TreasureBtn.Click += new EventHandler(this.TreasureBtn_Click);
            this.BookTitleBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.BookTitleBtn.Image = (Image)resources.GetObject("BookTitleBtn.Image");
            this.BookTitleBtn.ImageTransparentColor = Color.Magenta;
            this.BookTitleBtn.Name = "BookTitleBtn";
            this.BookTitleBtn.Size = new Size(104, 19);
            this.BookTitleBtn.Text = "Book Titles";
            this.BookTitleBtn.TextAlign = ContentAlignment.MiddleLeft;
            this.BookTitleBtn.Click += new EventHandler(this.BookTitleBtn_Click);
            this.PotionBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.PotionBtn.Image = (Image)resources.GetObject("PotionBtn.Image");
            this.PotionBtn.ImageTransparentColor = Color.Magenta;
            this.PotionBtn.Name = "PotionBtn";
            this.PotionBtn.Size = new Size(104, 19);
            this.PotionBtn.Text = "Potions";
            this.PotionBtn.TextAlign = ContentAlignment.MiddleLeft;
            this.PotionBtn.Click += new EventHandler(this.PotionBtn_Click);
            this.toolStripSeparator45.Name = "toolStripSeparator45";
            this.toolStripSeparator45.Size = new Size(104, 6);
            this.NPCBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.NPCBtn.Image = (Image)resources.GetObject("NPCBtn.Image");
            this.NPCBtn.ImageTransparentColor = Color.Magenta;
            this.NPCBtn.Name = "NPCBtn";
            this.NPCBtn.Size = new Size(104, 19);
            this.NPCBtn.Text = "NPC Description";
            this.NPCBtn.TextAlign = ContentAlignment.MiddleLeft;
            this.NPCBtn.Click += new EventHandler(this.NPCBtn_Click);
            this.RoomBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.RoomBtn.Image = (Image)resources.GetObject("RoomBtn.Image");
            this.RoomBtn.ImageTransparentColor = Color.Magenta;
            this.RoomBtn.Name = "RoomBtn";
            this.RoomBtn.Size = new Size(104, 19);
            this.RoomBtn.Text = "Room Description";
            this.RoomBtn.TextAlign = ContentAlignment.MiddleLeft;
            this.RoomBtn.Click += new EventHandler(this.RoomBtn_Click);
            this.toolStripSeparator46.Name = "toolStripSeparator46";
            this.toolStripSeparator46.Size = new Size(104, 6);
            this.ElfTextBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.ElfTextBtn.Image = (Image)resources.GetObject("ElfTextBtn.Image");
            this.ElfTextBtn.ImageTransparentColor = Color.Magenta;
            this.ElfTextBtn.Name = "ElfTextBtn";
            this.ElfTextBtn.Size = new Size(104, 19);
            this.ElfTextBtn.Text = "Elvish Text";
            this.ElfTextBtn.TextAlign = ContentAlignment.MiddleLeft;
            this.ElfTextBtn.Click += new EventHandler(this.ElfTextBtn_Click);
            this.DwarfTextBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.DwarfTextBtn.Image = (Image)resources.GetObject("DwarfTextBtn.Image");
            this.DwarfTextBtn.ImageTransparentColor = Color.Magenta;
            this.DwarfTextBtn.Name = "DwarfTextBtn";
            this.DwarfTextBtn.Size = new Size(104, 19);
            this.DwarfTextBtn.Text = "Dwarvish Text";
            this.DwarfTextBtn.TextAlign = ContentAlignment.MiddleLeft;
            this.DwarfTextBtn.Click += new EventHandler(this.DwarfTextBtn_Click);
            this.PrimordialTextBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.PrimordialTextBtn.Image = (Image)resources.GetObject("PrimordialTextBtn.Image");
            this.PrimordialTextBtn.ImageTransparentColor = Color.Magenta;
            this.PrimordialTextBtn.Name = "PrimordialTextBtn";
            this.PrimordialTextBtn.Size = new Size(104, 19);
            this.PrimordialTextBtn.Text = "Primordial Text";
            this.PrimordialTextBtn.TextAlign = ContentAlignment.MiddleLeft;
            this.PrimordialTextBtn.Click += new EventHandler(this.PrimordialTextBtn_Click);
            this.CompendiumPage.Controls.Add(this.CompendiumBrowser);
            this.CompendiumPage.Location = new Point(23, 4);
            this.CompendiumPage.Name = "CompendiumPage";
            this.CompendiumPage.Padding = new Padding(3);
            this.CompendiumPage.Size = new Size(567, 402);
            this.CompendiumPage.TabIndex = 2;
            this.CompendiumPage.Text = "Compendium";
            this.CompendiumPage.UseVisualStyleBackColor = true;
            this.CompendiumBrowser.AllowWebBrowserDrop = false;
            this.CompendiumBrowser.Dock = DockStyle.Fill;
            this.CompendiumBrowser.Location = new Point(3, 3);
            this.CompendiumBrowser.MinimumSize = new Size(20, 20);
            this.CompendiumBrowser.Name = "CompendiumBrowser";
            this.CompendiumBrowser.ScriptErrorsSuppressed = true;
            this.CompendiumBrowser.Size = new Size(561, 396);
            this.CompendiumBrowser.TabIndex = 0;
            this.InfoPanel.Dock = DockStyle.Fill;
            this.InfoPanel.Level = 1;
            this.InfoPanel.Location = new Point(0, 25);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Size = new Size(258, 385);
            this.InfoPanel.TabIndex = 0;
            this.ReferenceToolbar.Items.AddRange(new ToolStripItem[]
            {
                this.DieRollerBtn
            });
            this.ReferenceToolbar.Location = new Point(0, 0);
            this.ReferenceToolbar.Name = "ReferenceToolbar";
            this.ReferenceToolbar.Size = new Size(258, 25);
            this.ReferenceToolbar.TabIndex = 1;
            this.ReferenceToolbar.Text = "toolStrip1";
            this.DieRollerBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.DieRollerBtn.Image = (Image)resources.GetObject("DieRollerBtn.Image");
            this.DieRollerBtn.ImageTransparentColor = Color.Magenta;
            this.DieRollerBtn.Name = "DieRollerBtn";
            this.DieRollerBtn.Size = new Size(61, 22);
            this.DieRollerBtn.Text = "Die Roller";
            this.DieRollerBtn.Click += new EventHandler(this.DieRollerBtn_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(864, 460);
            base.Controls.Add(this.Pages);
            base.Controls.Add(this.MainMenu);
            base.Icon = (Icon)resources.GetObject("$this.Icon");
            base.MainMenuStrip = this.MainMenu;
            base.Name = "MainForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Masterplan";
            base.Shown += new EventHandler(this.MainForm_Shown);
            base.Layout += new LayoutEventHandler(this.MainForm_Layout);
            base.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
            this.WorkspaceToolbar.ResumeLayout(false);
            this.WorkspaceToolbar.PerformLayout();
            this.PointMenu.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.PreviewSplitter.Panel1.ResumeLayout(false);
            this.PreviewSplitter.Panel1.PerformLayout();
            this.PreviewSplitter.Panel2.ResumeLayout(false);
            this.PreviewSplitter.ResumeLayout(false);
            this.NavigationSplitter.Panel1.ResumeLayout(false);
            this.NavigationSplitter.Panel2.ResumeLayout(false);
            this.NavigationSplitter.Panel2.PerformLayout();
            this.NavigationSplitter.ResumeLayout(false);
            this.PlotPanel.ResumeLayout(false);
            this.PlotPanel.PerformLayout();
            this.WorkspaceSearchBar.ResumeLayout(false);
            this.WorkspaceSearchBar.PerformLayout();
            this.PreviewInfoSplitter.Panel1.ResumeLayout(false);
            this.PreviewInfoSplitter.Panel1.PerformLayout();
            this.PreviewInfoSplitter.ResumeLayout(false);
            this.PreviewPanel.ResumeLayout(false);
            this.PreviewToolbar.ResumeLayout(false);
            this.PreviewToolbar.PerformLayout();
            this.Pages.ResumeLayout(false);
            this.WorkspacePage.ResumeLayout(false);
            this.BackgroundPage.ResumeLayout(false);
            this.BackgroundPage.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.BackgroundPanel.ResumeLayout(false);
            this.BackgroundToolbar.ResumeLayout(false);
            this.BackgroundToolbar.PerformLayout();
            this.EncyclopediaPage.ResumeLayout(false);
            this.EncyclopediaPage.PerformLayout();
            this.EncyclopediaSplitter.Panel1.ResumeLayout(false);
            this.EncyclopediaSplitter.Panel2.ResumeLayout(false);
            this.EncyclopediaSplitter.ResumeLayout(false);
            this.EncyclopediaEntrySplitter.Panel1.ResumeLayout(false);
            this.EncyclopediaEntrySplitter.Panel2.ResumeLayout(false);
            this.EncyclopediaEntrySplitter.ResumeLayout(false);
            this.EntryPanel.ResumeLayout(false);
            this.EncyclopediaToolbar.ResumeLayout(false);
            this.EncyclopediaToolbar.PerformLayout();
            this.RulesPage.ResumeLayout(false);
            this.RulesSplitter.Panel1.ResumeLayout(false);
            this.RulesSplitter.Panel1.PerformLayout();
            this.RulesSplitter.Panel2.ResumeLayout(false);
            this.RulesSplitter.Panel2.PerformLayout();
            this.RulesSplitter.ResumeLayout(false);
            this.RulesToolbar.ResumeLayout(false);
            this.RulesToolbar.PerformLayout();
            this.RulesBrowserPanel.ResumeLayout(false);
            this.EncEntryToolbar.ResumeLayout(false);
            this.EncEntryToolbar.PerformLayout();
            this.AttachmentsPage.ResumeLayout(false);
            this.AttachmentsPage.PerformLayout();
            this.AttachmentToolbar.ResumeLayout(false);
            this.AttachmentToolbar.PerformLayout();
            this.JotterPage.ResumeLayout(false);
            this.JotterPage.PerformLayout();
            this.JotterSplitter.Panel1.ResumeLayout(false);
            this.JotterSplitter.Panel2.ResumeLayout(false);
            this.JotterSplitter.Panel2.PerformLayout();
            this.JotterSplitter.ResumeLayout(false);
            this.JotterToolbar.ResumeLayout(false);
            this.JotterToolbar.PerformLayout();
            this.ReferencePage.ResumeLayout(false);
            this.ReferenceSplitter.Panel1.ResumeLayout(false);
            this.ReferenceSplitter.Panel2.ResumeLayout(false);
            this.ReferenceSplitter.Panel2.PerformLayout();
            this.ReferenceSplitter.ResumeLayout(false);
            this.ReferencePages.ResumeLayout(false);
            this.PartyPage.ResumeLayout(false);
            this.ToolsPage.ResumeLayout(false);
            this.ToolsPage.PerformLayout();
            this.ToolBrowserPanel.ResumeLayout(false);
            this.GeneratorToolbar.ResumeLayout(false);
            this.GeneratorToolbar.PerformLayout();
            this.CompendiumPage.ResumeLayout(false);
            this.ReferenceToolbar.ResumeLayout(false);
            this.ReferenceToolbar.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }


        #endregion
    }
}
