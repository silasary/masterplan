using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Events;
using Masterplan.Extensibility;
using Masterplan.Tools;
using Masterplan.Tools.Generators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using Utils;
using Utils.Forms;

namespace Masterplan.UI
{
	internal partial class MainForm : Form
	{
		public enum ViewType
		{
			Flowchart,
			Delve,
			Map
		}

		private ToolStrip WorkspaceToolbar;

		private ToolStripButton RemoveBtn;

		private ContextMenuStrip PointMenu;

		private ToolStripMenuItem ContextAdd;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem ContextRemove;

		private ToolStripMenuItem ContextEdit;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem ContextExplore;

		private MenuStrip MainMenu;

		private ToolStripMenuItem FileMenu;

		private ToolStripMenuItem FileNew;

		private ToolStripSeparator toolStripMenuItem1;

		private ToolStripMenuItem FileOpen;

		private ToolStripSeparator toolStripMenuItem2;

		private ToolStripMenuItem FileSave;

		private ToolStripMenuItem FileSaveAs;

		private ToolStripSeparator toolStripMenuItem3;

		private ToolStripMenuItem FileExit;

		private SplitContainer PreviewSplitter;

		private ToolStripButton PlotCutBtn;

		private ToolStripButton PlotCopyBtn;

		private ToolStripButton PlotPasteBtn;

		private ToolStripMenuItem ProjectMenu;

		private ToolStripMenuItem HelpMenu;

		private ToolStripMenuItem HelpAbout;

		private ToolStripMenuItem HelpFeedback;

		private ToolStripMenuItem ProjectProject;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton SearchBtn;

		private ToolStrip WorkspaceSearchBar;

		private ToolStripLabel PlotSearchLbl;

		private ToolStripTextBox PlotSearchBox;

		private ToolStripMenuItem ContextAddBetween;

		private ToolStripMenuItem ProjectTacticalMaps;

		private ToolStripSeparator toolStripSeparator10;

		private ToolStripMenuItem ProjectDecks;

		private SplitContainer PreviewInfoSplitter;

		private ToolStripMenuItem ProjectOverview;

		private ToolStripMenuItem ProjectCustomCreatures;

		private ToolStripMenuItem HelpManual;

		private ToolStripSeparator toolStripSeparator12;

		private ToolStripMenuItem ProjectPlayers;

		private ToolStripMenuItem ProjectCalendars;

		private ToolStripMenuItem HelpWebsite;

		private ToolStripSeparator toolStripSeparator13;

		private SplitContainer NavigationSplitter;

		private TreeView NavigationTree;

		private ToolStripSeparator toolStripSeparator9;

		private ToolStripDropDownButton ViewMenu;

		private ToolStripMenuItem ViewDefault;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripMenuItem ViewEncounters;

		private ToolStripMenuItem ViewChallenges;

		private ToolStripMenuItem ViewQuests;

		private ToolStripMenuItem ViewParcels;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripMenuItem ViewHighlighting;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem ViewLevelling;

		private ToolStripSeparator toolStripSeparator11;

		private ToolStripMenuItem ViewPreview;

		private ToolStripMenuItem ViewNavigation;

		private StatusStrip BreadcrumbBar;

		private TabControl Pages;

		private TabPage WorkspacePage;

		private Panel PlotPanel;

		private TabPage EncyclopediaPage;

		private SplitContainer EncyclopediaSplitter;

		private ListView EntryList;

		private ColumnHeader EntryHdr;

		private Panel EntryPanel;

		private ToolStrip EncyclopediaToolbar;

		private ToolStripButton EncRemoveBtn;

		private ToolStripButton EncEditBtn;

		private ToolStripSeparator toolStripSeparator15;

		private ToolStripLabel EncSearchLbl;

		private ToolStripTextBox EncSearchBox;

		private ToolStripLabel EncClearLbl;

		private ToolStripLabel PlotClearBtn;

		private TabPage JotterPage;

		private ToolStrip JotterToolbar;

		private SplitContainer JotterSplitter;

		private ListView NoteList;

		private TextBox NoteBox;

		private ToolStripButton NoteAddBtn;

		private ToolStripButton NoteRemoveBtn;

		private ColumnHeader NoteHdr;

		private ToolStripSeparator toolStripSeparator16;

		private ToolStripLabel NoteSearchLbl;

		private ToolStripTextBox NoteSearchBox;

		private ToolStripLabel NoteClearLbl;

		private ToolStripButton EncCutBtn;

		private ToolStripButton EncCopyBtn;

		private ToolStripButton EncPasteBtn;

		private ToolStripSeparator toolStripSeparator17;

		private ToolStripSeparator toolStripSeparator18;

		private ToolStripButton NoteCutBtn;

		private ToolStripButton NoteCopyBtn;

		private ToolStripButton NotePasteBtn;

		private WebBrowser EntryDetails;

		private Panel PreviewPanel;

		private ToolStripMenuItem ContextMoveTo;

		private ToolStripSeparator toolStripSeparator20;

		private ToolStripMenuItem ViewTooltips;

		private ToolStripMenuItem ViewTraps;

		private ToolStripMenuItem ToolsMenu;

		private ToolStripMenuItem ToolsExportProject;

		private ToolStripSeparator toolStripMenuItem4;

		private ToolStripMenuItem ToolsLibraries;

		private ToolStripSeparator toolStripMenuItem5;

		private ToolStripMenuItem ToolsAddIns;

		private ToolStripMenuItem addinsToolStripMenuItem;

		private TabPage AttachmentsPage;

		private ListView AttachmentList;

		private ColumnHeader AttachmentHdr;

		private ToolStrip AttachmentToolbar;

		private ToolStripButton AttachmentRemoveBtn;

		private ToolStripSeparator toolStripSeparator19;

		private ToolStripButton AttachmentPlayerView;

		private TabPage BackgroundPage;

		private SplitContainer splitContainer1;

		private ListView BackgroundList;

		private ToolStrip BackgroundToolbar;

		private ToolStripButton BackgroundAddBtn;

		private ToolStripButton BackgroundRemoveBtn;

		private ToolStripButton BackgroundEditBtn;

		private ToolStripSeparator toolStripSeparator21;

		private ToolStripButton EncPlayerView;

		private ToolStripSeparator toolStripSeparator22;

		private ColumnHeader InfoHdr;

		private Panel BackgroundPanel;

		private WebBrowser BackgroundDetails;

		private ToolStripSeparator toolStripSeparator23;

		private ToolStripButton BackgroundUpBtn;

		private ToolStripButton BackgroundDownBtn;

		private ToolStripDropDownButton AttachmentExtract;

		private ToolStripMenuItem AttachmentExtractSimple;

		private ToolStripMenuItem AttachmentExtractAndRun;

		private ColumnHeader AttachmentSizeHdr;

		private ToolStripSeparator toolStripSeparator24;

		private ToolStripMenuItem ProjectParcels;

		private ToolStripDropDownButton BackgroundPlayerView;

		private ToolStripMenuItem BackgroundPlayerViewSelected;

		private ToolStripMenuItem BackgroundPlayerViewAll;

		private ToolStripMenuItem PlayerViewMenu;

		private ToolStripMenuItem PlayerViewShow;

		private ToolStripMenuItem PlayerViewClear;

		private ToolStripSeparator toolStripMenuItem7;

		private ToolStripMenuItem PlayerViewOtherDisplay;

		private ToolStripMenuItem ToolsImportProject;

		private ToolStripSeparator toolStripSeparator25;

		private ToolStripMenuItem ToolsExportHandout;

		private ToolStrip PreviewToolbar;

		private ToolStripDropDownButton FlowchartMenu;

		private ToolStripMenuItem FlowchartPrint;

		private ToolStripMenuItem FlowchartExport;

		private ToolStripButton EditBtn;

		private ToolStripDropDownButton PlotPointMenu;

		private ToolStripMenuItem PlotPointPlayerView;

		private ToolStripButton ExploreBtn;

		private ToolStripMenuItem PlotPointExportHTML;

		private ToolStripSplitButton AddBtn;

		private ToolStripMenuItem AddEncounter;

		private ToolStripMenuItem AddChallenge;

		private ToolStripMenuItem AddTrap;

		private ToolStripMenuItem AddQuest;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem ProjectEncounters;

		private ToolStripSeparator toolStripSeparator14;

		private ToolStripMenuItem PlayerViewTextSize;

		private ToolStripMenuItem TextSizeSmall;

		private ToolStripMenuItem TextSizeLarge;

		private ToolStripButton AttachmentImportBtn;

		private ToolStripMenuItem TextSizeMedium;

		private ToolStripSeparator toolStripSeparator27;

		private ToolStripSeparator toolStripSeparator28;

		private ToolStripMenuItem ContextState;

		private ToolStripMenuItem ContextStateNormal;

		private ToolStripMenuItem ContextStateCompleted;

		private ToolStripMenuItem ContextStateSkipped;

		private ToolStripMenuItem ContextDisconnectAll;

		private ToolStripMenuItem ContextDisconnect;

		private ToolStripSeparator toolStripSeparator29;

		private ToolStripSeparator toolStripSeparator30;

		private ToolStripMenuItem ProjectPassword;

		private ToolStripMenuItem FlowchartAllXP;

		private TabPage RulesPage;

		private SplitContainer RulesSplitter;

		private ListView RulesList;

		private ColumnHeader RulesHdr;

		private ToolStrip RulesToolbar;

		private ToolStripDropDownButton RulesAddBtn;

		private ToolStripMenuItem AddRace;

		private ToolStripSeparator toolStripSeparator31;

		private ToolStripMenuItem AddClass;

		private ToolStripMenuItem AddParagonPath;

		private ToolStripMenuItem AddEpicDestiny;

		private ToolStripSeparator toolStripSeparator32;

		private ToolStripMenuItem AddBackground;

		private ToolStripMenuItem AddFeat;

		private ToolStripMenuItem AddWeapon;

		private ToolStripMenuItem AddRitual;

		private Panel RulesBrowserPanel;

		private WebBrowser RulesBrowser;

		private ToolStripSeparator toolStripSeparator33;

		private WebBrowser Preview;

		private ToolStripMenuItem ProjectCampaignSettings;

		private ToolStripMenuItem PlotPointExportFile;

		private ToolStripSeparator toolStripSeparator35;

		internal PlotView PlotView;

		private ToolStripMenuItem ProjectRegionalMaps;

		private ToolStripSeparator toolStripSeparator37;

		private ToolStripButton NoteCategoryBtn;

		private ToolStripSeparator toolStripSeparator38;

		private ToolStripSeparator toolStripSeparator39;

		private ToolStripMenuItem AddCreatureLore;

		private ToolStripMenuItem AddDisease;

		private ToolStripMenuItem AddPoison;

		private ToolStripDropDownButton EncShareBtn;

		private ToolStripMenuItem EncShareExport;

		private ToolStripMenuItem EncShareImport;

		private ToolStripSeparator toolStripMenuItem6;

		private ToolStripMenuItem EncSharePublish;

		private ToolStripSeparator toolStripSeparator40;

		private ToolStripSeparator toolStripMenuItem8;

		private ToolStripMenuItem HelpFacebook;

		private ToolStripMenuItem HelpTwitter;

		private ToolStripMenuItem AddTheme;

		private ToolStripDropDownButton RulesShareBtn;

		private ToolStripMenuItem RulesShareExport;

		private ToolStripMenuItem RulesShareImport;

		private ToolStripSeparator toolStripMenuItem9;

		private ToolStripMenuItem RulesSharePublish;

		private ToolStripSeparator toolStripSeparator41;

		private ToolStripMenuItem FileAdvanced;

		private ToolStripMenuItem AdvancedDelve;

		private ToolStripSeparator toolStripSeparator42;

		private ToolStrip EncEntryToolbar;

		private ToolStripButton RulesRemoveBtn;

		private ToolStripButton RulesEditBtn;

		private ToolStripSeparator toolStripSeparator36;

		private ToolStripButton RulesPlayerViewBtn;

		private ToolStripSeparator toolStripSeparator43;

		private ToolStripButton RuleEncyclopediaBtn;

		private TabPage ReferencePage;

		private SplitContainer ReferenceSplitter;

		private TabControl ReferencePages;

		private TabPage PartyPage;

		private WebBrowser PartyBrowser;

		private TabPage ToolsPage;

		private WebBrowser GeneratorBrowser;

		private ToolStrip GeneratorToolbar;

		private ToolStripButton NPCBtn;

		private ToolStripButton RoomBtn;

		private ToolStripButton TreasureBtn;

		private Panel ToolBrowserPanel;

		private ToolStripButton BookTitleBtn;

		private ToolStripButton ExoticNameBtn;

		private ToolStripButton PotionBtn;

		private ToolStripLabel toolStripLabel1;

		private ToolStripSeparator toolStripSeparator26;

		private ToolStripSeparator toolStripSeparator44;

		private ToolStripMenuItem ViewLinks;

		private ToolStripMenuItem ViewLinksCurved;

		private ToolStripMenuItem ViewLinksAngled;

		private ToolStripMenuItem ViewLinksStraight;

		private ToolStripButton ElfNameBtn;

		private ToolStripButton DwarfNameBtn;

		private ToolStripButton HalflingNameBtn;

		private ToolStripSeparator toolStripSeparator45;

		private ToolStripSeparator toolStripSeparator46;

		private ToolStripButton DwarfTextBtn;

		private ToolStripButton ElfTextBtn;

		private ToolStripButton PrimordialTextBtn;

		private SplitContainer EncyclopediaEntrySplitter;

		private ListView EntryImageList;

		private TabPage CompendiumPage;

		private WebBrowser CompendiumBrowser;

		private ToolStripMenuItem HelpTutorials;

		private ToolStripSeparator toolStripSeparator47;

		private ToolStripSeparator toolStripSeparator34;

		private ToolStripMenuItem ToolsIssues;

		private InfoPanel InfoPanel;

		private ToolStrip ReferenceToolbar;

		private ToolStripButton DieRollerBtn;

		private ToolStripMenuItem ToolsExportLoot;

		private ToolStripMenuItem AdvancedSample;

		private ToolStripDropDownButton AdvancedBtn;

		private ToolStripMenuItem PlotAdvancedTreasure;

		private ToolStripMenuItem PlotAdvancedIssues;

		private ToolStripMenuItem PlotAdvancedDifficulty;

		private ToolStripSeparator toolStripSeparator48;

		private ToolStripDropDownButton BackgroundShareBtn;

		private ToolStripMenuItem BackgroundShareExport;

		private ToolStripMenuItem BackgroundShareImport;

		private ToolStripSeparator toolStripMenuItem10;

		private ToolStripMenuItem BackgroundSharePublish;

		private ToolStripDropDownButton EncAddBtn;

		private ToolStripMenuItem EncAddEntry;

		private ToolStripMenuItem EncAddGroup;

		private ToolStripMenuItem ToolsPowerStats;

		private ToolStripMenuItem ToolsTileChecklist;

		private ToolStripMenuItem ToolsMiniChecklist;

		private ToolStripSeparator toolStripSeparator49;

		private WelcomePanel fWelcome;

		private ExtensibilityManager fExtensibility;

		private bool fUpdating;

		private MainForm.ViewType fView;

		private MapView fDelveView;

		private RegionalMapPanel fMapView;

		private string fDownloadedFile = "";

		private string fPartyBreakdownSecondary = "";

		public Background SelectedBackground
		{
			get
			{
				if (this.BackgroundList.SelectedItems.Count != 0)
				{
					return this.BackgroundList.SelectedItems[0].Tag as Background;
				}
				return null;
			}
			set
			{
				this.BackgroundList.SelectedItems.Clear();
				if (value != null)
				{
					foreach (ListViewItem listViewItem in this.BackgroundList.Items)
					{
						Background background = listViewItem.Tag as Background;
						if (background != null && background.ID == value.ID)
						{
							listViewItem.Selected = true;
						}
					}
				}
				this.update_background_item();
			}
		}

		public IEncyclopediaItem SelectedEncyclopediaItem
		{
			get
			{
				if (this.EntryList.SelectedItems.Count != 0)
				{
					return this.EntryList.SelectedItems[0].Tag as IEncyclopediaItem;
				}
				return null;
			}
			set
			{
				this.EntryList.SelectedItems.Clear();
				if (value != null)
				{
					foreach (ListViewItem listViewItem in this.EntryList.Items)
					{
						IEncyclopediaItem encyclopediaItem = listViewItem.Tag as IEncyclopediaItem;
						if (encyclopediaItem != null && encyclopediaItem.ID == value.ID)
						{
							listViewItem.Selected = true;
						}
					}
				}
				this.update_encyclopedia_entry();
			}
		}

		public EncyclopediaImage SelectedEncyclopediaImage
		{
			get
			{
				if (this.EntryImageList.SelectedItems.Count != 0)
				{
					return this.EntryImageList.SelectedItems[0].Tag as EncyclopediaImage;
				}
				return null;
			}
		}

		public IPlayerOption SelectedRule
		{
			get
			{
				if (this.RulesList.SelectedItems.Count != 0)
				{
					return this.RulesList.SelectedItems[0].Tag as IPlayerOption;
				}
				return null;
			}
			set
			{
				this.RulesList.SelectedItems.Clear();
				if (value != null)
				{
					foreach (ListViewItem listViewItem in this.RulesList.Items)
					{
						IPlayerOption playerOption = listViewItem.Tag as IPlayerOption;
						if (playerOption != null && playerOption.ID == value.ID)
						{
							listViewItem.Selected = true;
						}
					}
				}
			}
		}

		public List<Attachment> SelectedAttachments
		{
			get
			{
				List<Attachment> list = new List<Attachment>();
				foreach (ListViewItem listViewItem in this.AttachmentList.SelectedItems)
				{
					Attachment attachment = listViewItem.Tag as Attachment;
					if (attachment != null)
					{
						list.Add(attachment);
					}
				}
				return list;
			}
		}

		public Note SelectedNote
		{
			get
			{
				if (this.NoteList.SelectedItems.Count != 0)
				{
					return this.NoteList.SelectedItems[0].Tag as Note;
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
						Note note = listViewItem.Tag as Note;
						if (note != null && note.ID == value.ID)
						{
							listViewItem.Selected = true;
						}
					}
				}
			}
		}

		public IIssue SelectedIssue
		{
			get
			{
				if (this.NoteList.SelectedItems.Count != 0)
				{
					return this.NoteList.SelectedItems[0].Tag as IIssue;
				}
				return null;
			}
		}

		public MainForm()
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			try
			{
				this.Preview.DocumentText = "";
				this.BackgroundDetails.DocumentText = "";
				this.EntryDetails.DocumentText = "";
				this.RulesBrowser.DocumentText = "";
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			try
			{
				this.fExtensibility = new ExtensibilityManager(this);
				foreach (IAddIn current in this.fExtensibility.AddIns)
				{
					foreach (IPage current2 in current.Pages)
					{
						TabPage tabPage = new TabPage(current2.Name);
						this.Pages.TabPages.Add(tabPage);
						tabPage.Controls.Add(current2.Control);
						current2.Control.Dock = DockStyle.Fill;
					}
					foreach (IPage current3 in current.QuickReferencePages)
					{
						TabPage tabPage2 = new TabPage();
						tabPage2.Text = current3.Name;
						tabPage2.Controls.Add(current3.Control);
						current3.Control.Dock = DockStyle.Fill;
						this.ReferencePages.TabPages.Add(tabPage2);
					}
				}
			}
			catch (Exception ex2)
			{
				LogSystem.Trace(ex2);
			}
			try
			{
				if (Session.Project == null)
				{
					base.Controls.Clear();
					this.fWelcome = new WelcomePanel(Session.Preferences.ShowHeadlines);
					this.fWelcome.Dock = DockStyle.Fill;
					this.fWelcome.NewProjectClicked += new EventHandler(this.Welcome_NewProjectClicked);
					this.fWelcome.OpenProjectClicked += new EventHandler(this.Welcome_OpenProjectClicked);
					this.fWelcome.OpenLastProjectClicked += new EventHandler(this.Welcome_OpenLastProjectClicked);
					this.fWelcome.DelveClicked += new EventHandler(this.Welcome_DelveClicked);
					this.fWelcome.PremadeClicked += new EventHandler(this.Welcome_PremadeClicked);
					base.Controls.Add(this.fWelcome);
					base.Controls.Add(this.MainMenu);
				}
				else
				{
					this.PlotView.Plot = Session.Project.Plot;
				}
			}
			catch (Exception ex3)
			{
				LogSystem.Trace(ex3);
			}
			try
			{
				this.NavigationSplitter.Panel1Collapsed = !Session.Preferences.ShowNavigation;
				this.PreviewSplitter.Panel2Collapsed = !Session.Preferences.ShowPreview;
				this.PlotView.LinkStyle = Session.Preferences.LinkStyle;
				this.WorkspaceSearchBar.Visible = false;
				this.update_encyclopedia_templates();
			}
			catch (Exception ex4)
			{
				LogSystem.Trace(ex4);
			}
			try
			{
				if (Session.Preferences.Maximised)
				{
					base.WindowState = FormWindowState.Maximized;
				}
				else if (Session.Preferences.Size != Size.Empty && Session.Preferences.Location != Point.Empty)
				{
					base.StartPosition = FormStartPosition.Manual;
					int width = Math.Max(base.Width, Session.Preferences.Size.Width);
					int height = Math.Max(base.Height, Session.Preferences.Size.Height);
					base.Size = new Size(width, height);
					int x = Math.Max(base.Left, Session.Preferences.Location.X);
					int y = Math.Max(base.Top, Session.Preferences.Location.Y);
					base.Location = new Point(x, y);
				}
				else
				{
					base.StartPosition = FormStartPosition.CenterScreen;
				}
			}
			catch (Exception ex5)
			{
				LogSystem.Trace(ex5);
			}
			this.update_title();
			this.UpdateView();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			try
			{
				if (this.Pages.SelectedTab == this.WorkspacePage)
				{
					PlotPoint selected_point = this.get_selected_point();
					this.RemoveBtn.Enabled = (selected_point != null);
					this.PlotCutBtn.Enabled = (selected_point != null);
					this.PlotCopyBtn.Enabled = (selected_point != null);
					this.PlotPasteBtn.Enabled = (Clipboard.ContainsData(typeof(PlotPoint).ToString()) || Clipboard.ContainsText());
					this.SearchBtn.Checked = this.WorkspaceSearchBar.Visible;
					this.PlotClearBtn.Visible = (this.PlotSearchBox.Text != "");
					this.EditBtn.Enabled = (selected_point != null);
					this.ExploreBtn.Enabled = (selected_point != null);
					this.PlotPointMenu.Enabled = (selected_point != null);
					this.PlotPointPlayerView.Enabled = (selected_point != null && selected_point.ReadAloud != "");
					this.PlotPointExportHTML.Enabled = (selected_point != null);
					this.ContextRemove.Enabled = this.RemoveBtn.Enabled;
					this.ContextEdit.Enabled = this.EditBtn.Enabled;
					this.ContextExplore.Enabled = this.EditBtn.Enabled;
					this.ContextState.Enabled = (selected_point != null);
					this.FlowchartAllXP.Checked = Session.Preferences.AllXP;
				}
				if (this.Pages.SelectedTab == this.BackgroundPage)
				{
					this.BackgroundRemoveBtn.Enabled = (this.SelectedBackground != null);
					this.BackgroundEditBtn.Enabled = (this.SelectedBackground != null);
					this.BackgroundUpBtn.Enabled = (this.SelectedBackground != null && Session.Project.Backgrounds.IndexOf(this.SelectedBackground) != 0);
					this.BackgroundDownBtn.Enabled = (this.SelectedBackground != null && Session.Project.Backgrounds.IndexOf(this.SelectedBackground) != Session.Project.Backgrounds.Count - 1);
					this.BackgroundPlayerViewSelected.Enabled = (this.SelectedBackground != null && this.SelectedBackground.Details != "");
					this.BackgroundPlayerViewAll.Enabled = (Session.Project != null && Session.Project.Backgrounds.Count != 0);
				}
				if (this.Pages.SelectedTab == this.EncyclopediaPage)
				{
					this.EncAddGroup.Enabled = (Session.Project != null && Session.Project.Encyclopedia.Entries.Count != 0);
					this.EncRemoveBtn.Enabled = (this.SelectedEncyclopediaItem != null);
					this.EncEditBtn.Enabled = (this.SelectedEncyclopediaItem != null);
					this.EncCutBtn.Enabled = (this.SelectedEncyclopediaItem != null && this.SelectedEncyclopediaItem is EncyclopediaEntry);
					this.EncCopyBtn.Enabled = (this.SelectedEncyclopediaItem != null && this.SelectedEncyclopediaItem is EncyclopediaEntry);
					this.EncPasteBtn.Enabled = (Clipboard.ContainsData(typeof(EncyclopediaEntry).ToString()) || Clipboard.ContainsText());
					this.EncPlayerView.Enabled = (this.SelectedEncyclopediaItem != null);
					this.EncShareExport.Enabled = (Session.Project != null && Session.Project.Encyclopedia.Entries.Count != 0);
					this.EncSharePublish.Enabled = (Session.Project != null && Session.Project.Encyclopedia.Entries.Count != 0);
					this.EncClearLbl.Visible = (this.EncSearchBox.Text != "");
				}
				if (this.Pages.SelectedTab == this.RulesPage)
				{
					this.RulesRemoveBtn.Enabled = (this.SelectedRule != null);
					this.RulesEditBtn.Enabled = (this.SelectedRule != null);
					this.RulesPlayerViewBtn.Enabled = (this.SelectedRule != null);
					this.RuleEncyclopediaBtn.Enabled = (this.SelectedRule != null);
					this.RulesShareExport.Enabled = (Session.Project != null && Session.Project.PlayerOptions.Count != 0);
					this.RulesSharePublish.Enabled = (Session.Project != null && Session.Project.PlayerOptions.Count != 0);
				}
				if (this.Pages.SelectedTab == this.AttachmentsPage)
				{
					this.AttachmentImportBtn.Enabled = true;
					this.AttachmentRemoveBtn.Enabled = (this.SelectedAttachments.Count != 0);
					this.AttachmentExtract.Enabled = (this.SelectedAttachments.Count != 0);
					this.AttachmentPlayerView.Enabled = (this.SelectedAttachments.Count == 1 && this.SelectedAttachments[0].Type != AttachmentType.Miscellaneous);
				}
				if (this.Pages.SelectedTab == this.JotterPage)
				{
					this.NoteRemoveBtn.Enabled = (this.SelectedNote != null);
					this.NoteCategoryBtn.Enabled = (this.SelectedNote != null);
					this.NoteCutBtn.Enabled = (this.SelectedNote != null);
					this.NoteCopyBtn.Enabled = (this.SelectedNote != null);
					this.NotePasteBtn.Enabled = (Clipboard.ContainsData(typeof(Note).ToString()) || Clipboard.ContainsText());
					this.NoteClearLbl.Visible = (this.NoteSearchBox.Text != "");
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys key)
		{
			if (Session.Project != null)
			{
				if (this.Pages.SelectedTab == this.WorkspacePage)
				{
					if (key == (Keys)131137)
					{
						this.AddBtn_Click(null, null);
						return true;
					}
					if (key == (Keys)131160)
					{
						this.CutBtn_Click(null, null);
						return true;
					}
					if (key == (Keys)131139)
					{
						this.CopyBtn_Click(null, null);
						return true;
					}
					if (key == (Keys)131158)
					{
						this.PasteBtn_Click(null, null);
						return true;
					}
					bool flag = this.PlotView.Navigate(key);
					if (flag)
					{
						return true;
					}
				}
				if (this.Pages.SelectedTab == this.BackgroundPage && key == (Keys)131137)
				{
					this.BackgroundAddBtn_Click(null, null);
					return true;
				}
				if (this.Pages.SelectedTab == this.EncyclopediaPage)
				{
					if (key == (Keys)131137)
					{
						this.EncAddEntry_Click(null, null);
						return true;
					}
					if (key == (Keys)131160)
					{
						this.EncCutBtn_Click(null, null);
						return true;
					}
					if (key == (Keys)131139)
					{
						this.EncCopyBtn_Click(null, null);
						return true;
					}
					if (key == (Keys)131158)
					{
						this.EncPasteBtn_Click(null, null);
						return true;
					}
				}
				if (this.Pages.SelectedTab == this.AttachmentsPage && key == (Keys)131137)
				{
					this.AttachmentImportBtn_Click(null, null);
					return true;
				}
				if (this.Pages.SelectedTab == this.JotterPage)
				{
					if (key == (Keys)131160)
					{
						this.NoteCutBtn_Click(null, null);
						return true;
					}
					if (key == (Keys)131139)
					{
						this.NoteCopyBtn_Click(null, null);
						return true;
					}
					if (key == (Keys)131158)
					{
						this.NotePasteBtn_Click(null, null);
						return true;
					}
				}
			}
			return base.ProcessCmdKey(ref msg, key);
		}

		private void MainForm_Layout(object sender, LayoutEventArgs e)
		{
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			try
			{
				Session.MainForm = this;
				if (Program.SplashScreen != null)
				{
					Program.SplashScreen.Close();
					Program.SplashScreen = null;
				}
				this.PlotView_SelectionChanged(null, null);
				this.NoteList_SelectedIndexChanged(null, null);
				if (Session.DisabledLibraries != null && Session.DisabledLibraries.Count != 0)
				{
					string text = "Due to copy protection, some libraries were not loaded:";
					text += Environment.NewLine;
					List<string> list = new List<string>(Session.DisabledLibraries);
					int num = Math.Min(list.Count, 6);
					for (int num2 = 0; num2 != num; num2++)
					{
						int index = Session.Random.Next(list.Count);
						string text2 = list[index];
						list.Remove(text2);
						text += Environment.NewLine;
						text = text + "* " + text2;
					}
					if (list.Count > 0)
					{
						text = text + Environment.NewLine + Environment.NewLine;
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							"... and ",
							list.Count,
							" others."
						});
					}
					MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				if (Session.Project == null && Session.Creatures.Count == 0)
				{
					string text3 = FileName.Directory(Application.ExecutablePath);
					if (text3.Contains("Program Files"))
					{
						string text4 = "You're running Masterplan from the Program Files folder.";
						text4 = text4 + Environment.NewLine + Environment.NewLine;
						text4 += "Although Masterplan will run, this is a protected folder, and Masterplan won't be able to save any changes that you make to your libraries.";
						text4 = text4 + Environment.NewLine + Environment.NewLine;
						text4 += "If you move Masterplan to a new location (like My Documents or the Desktop), you won't have this problem.";
						MessageBox.Show(text4, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (!this.check_modified())
				{
					e.Cancel = true;
				}
				if (Session.FileName != "")
				{
					Session.Preferences.LastFile = Session.FileName;
				}
				Session.Preferences.Maximised = (base.WindowState == FormWindowState.Maximized);
				if (!Session.Preferences.Maximised)
				{
					Session.Preferences.Maximised = false;
					Session.Preferences.Size = base.Size;
					Session.Preferences.Location = base.Location;
				}
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
				if (e.Url.Scheme == "plot")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "add")
					{
						this.AddBtn_Click(sender, e);
					}
					if (e.Url.LocalPath == "encounter")
					{
						if (this.PlotView.SelectedPoint == null)
						{
							this.AddEncounter_Click(sender, e);
						}
						else
						{
							this.PlotView.SelectedPoint.Element = new Encounter();
							if (!this.edit_element(null, null))
							{
								this.PlotView.SelectedPoint.Element = null;
							}
						}
					}
					if (e.Url.LocalPath == "challenge")
					{
						if (this.PlotView.SelectedPoint == null)
						{
							this.AddChallenge_Click(sender, e);
						}
						else
						{
							SkillChallenge skillChallenge = new SkillChallenge();
							skillChallenge.Level = Session.Project.Party.Level;
							this.PlotView.SelectedPoint.Element = skillChallenge;
							if (!this.edit_element(null, null))
							{
								this.PlotView.SelectedPoint.Element = null;
							}
						}
					}
					if (e.Url.LocalPath == "edit")
					{
						this.EditBtn_Click(sender, e);
					}
					if (e.Url.LocalPath == "explore")
					{
						this.ExploreBtn_Click(sender, e);
					}
					if (e.Url.LocalPath == "properties")
					{
						this.ProjectProject_Click(sender, e);
					}
					if (e.Url.LocalPath == "up")
					{
						PlotPoint plotPoint = Session.Project.FindParent(this.PlotView.Plot);
						if (plotPoint != null)
						{
							Plot plot = Session.Project.FindParent(plotPoint);
							if (plot != null)
							{
								if (this.fView != MainForm.ViewType.Flowchart)
								{
									this.flowchart_view();
								}
								this.PlotView.Plot = plot;
								this.PlotView.SelectedPoint = plotPoint;
								this.UpdateView();
							}
						}
					}
					if (e.Url.LocalPath == "goals")
					{
						GoalListForm goalListForm = new GoalListForm(this.PlotView.Plot.Goals);
						if (goalListForm.ShowDialog() == DialogResult.OK)
						{
							this.PlotView.Plot.Goals = goalListForm.Goals;
							Session.Modified = true;
							if (goalListForm.CreatePlot)
							{
								this.PlotView.Plot.Points.Clear();
								GoalBuilder.Build(this.PlotView.Plot);
								this.PlotView.RecalculateLayout();
							}
							this.UpdateView();
						}
					}
					if (e.Url.LocalPath == "5x5")
					{
						if (this.PlotView.Plot.FiveByFive.Columns.Count == 0)
						{
							this.PlotView.Plot.FiveByFive.Initialise();
						}
						FiveByFiveForm fiveByFiveForm = new FiveByFiveForm(this.PlotView.Plot.FiveByFive);
						if (fiveByFiveForm.ShowDialog() == DialogResult.OK)
						{
							this.PlotView.Plot.FiveByFive = fiveByFiveForm.FiveByFive;
							Session.Modified = true;
							if (fiveByFiveForm.CreatePlot)
							{
								this.PlotView.Plot.Points.Clear();
								FiveByFive.Build(this.PlotView.Plot);
								this.PlotView.RecalculateLayout();
							}
							this.UpdateView();
						}
					}
					if (e.Url.LocalPath == "element")
					{
						this.edit_element(sender, e);
					}
					if (e.Url.LocalPath == "run")
					{
						this.run_encounter(sender, e);
					}
					if (e.Url.LocalPath == "maparea")
					{
						PlotPoint selected_point = this.get_selected_point();
						Map map = null;
						MapArea map_area = null;
						selected_point.GetTacticalMapArea(ref map, ref map_area);
						this.edit_map_area(map, map_area, null);
					}
					if (e.Url.LocalPath == "maploc")
					{
						PlotPoint selected_point2 = this.get_selected_point();
						RegionalMap map2 = null;
						MapLocation loc = null;
						selected_point2.GetRegionalMapArea(ref map2, ref loc, Session.Project);
						this.show_map_location(map2, loc);
					}
				}
				if (e.Url.Scheme == "entry")
				{
					e.Cancel = true;
					Guid entry_id = new Guid(e.Url.LocalPath);
					EncyclopediaEntry encyclopediaEntry = Session.Project.Encyclopedia.FindEntry(entry_id);
					if (encyclopediaEntry != null)
					{
						EncyclopediaEntryDetailsForm encyclopediaEntryDetailsForm = new EncyclopediaEntryDetailsForm(encyclopediaEntry);
						encyclopediaEntryDetailsForm.ShowDialog();
					}
				}
				if (e.Url.Scheme == "item")
				{
					e.Cancel = true;
					Guid item_id = new Guid(e.Url.LocalPath);
					MagicItem magicItem = Session.FindMagicItem(item_id, SearchType.Global);
					if (magicItem != null)
					{
						MagicItemDetailsForm magicItemDetailsForm = new MagicItemDetailsForm(magicItem);
						magicItemDetailsForm.ShowDialog();
					}
				}
				if (e.Url.Scheme == "delveview")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "select")
					{
						MapSelectForm mapSelectForm = new MapSelectForm(Session.Project.Maps, null, false);
						if (mapSelectForm.ShowDialog() == DialogResult.OK)
						{
							this.delve_view(mapSelectForm.Map);
						}
					}
					else if (e.Url.LocalPath == "off")
					{
						this.flowchart_view();
					}
					else if (e.Url.LocalPath == "edit")
					{
						this.delve_view_edit();
					}
					else if (e.Url.LocalPath == "build")
					{
						MapBuilderForm mapBuilderForm = new MapBuilderForm(new Map
						{
							Name = "New Map"
						}, false);
						if (mapBuilderForm.ShowDialog() == DialogResult.OK)
						{
							Session.Project.Maps.Add(mapBuilderForm.Map);
							this.delve_view(mapBuilderForm.Map);
						}
					}
					else if (e.Url.LocalPath == "playerview")
					{
						MapView mapView = new MapView();
						mapView.Map = this.fDelveView.Map;
						mapView.Plot = this.PlotView.Plot;
						mapView.Mode = MapViewMode.PlayerView;
						mapView.LineOfSight = false;
						mapView.BorderSize = 1;
						mapView.HighlightAreas = false;
						bool flag = false;
						int num = 2147483647;
						int num2 = int.MinValue;
						int num3 = 2147483647;
						int num4 = int.MinValue;
						foreach (MapArea current in this.fDelveView.Map.Areas)
						{
							PlotPoint plotPoint2 = this.PlotView.Plot.FindPointForMapArea(this.fDelveView.Map, current);
							if (plotPoint2 != null && plotPoint2.State == PlotPointState.Completed)
							{
								flag = true;
								num = Math.Min(num, current.Region.Left);
								num2 = Math.Max(num2, current.Region.Right);
								num3 = Math.Min(num3, current.Region.Top);
								num4 = Math.Max(num4, current.Region.Bottom);
							}
						}
						if (flag)
						{
							mapView.Viewpoint = new Rectangle(num, num3, num2 - num, num4 - num3);
						}
						if (Session.PlayerView == null)
						{
							Session.PlayerView = new PlayerViewForm(this);
						}
						Session.PlayerView.ShowTacticalMap(mapView, null);
					}
					else
					{
						Guid map_id = new Guid(e.Url.LocalPath);
						Map map3 = Session.Project.FindTacticalMap(map_id);
						if (map3 != null)
						{
							this.delve_view(map3);
						}
					}
				}
				if (e.Url.Scheme == "mapview")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "select")
					{
						RegionalMapSelectForm regionalMapSelectForm = new RegionalMapSelectForm(Session.Project.RegionalMaps, null, false);
						if (regionalMapSelectForm.ShowDialog() == DialogResult.OK)
						{
							this.map_view(regionalMapSelectForm.Map);
						}
					}
					else if (e.Url.LocalPath == "off")
					{
						this.flowchart_view();
					}
					else if (e.Url.LocalPath == "edit")
					{
						this.map_view_edit();
					}
					else if (e.Url.LocalPath == "build")
					{
						RegionalMapForm regionalMapForm = new RegionalMapForm(new RegionalMap
						{
							Name = "New Map"
						}, null);
						if (regionalMapForm.ShowDialog() == DialogResult.OK)
						{
							Session.Project.RegionalMaps.Add(regionalMapForm.Map);
							this.map_view(regionalMapForm.Map);
						}
					}
					else if (e.Url.LocalPath == "playerview")
					{
						RegionalMapPanel regionalMapPanel = new RegionalMapPanel();
						regionalMapPanel.Map = this.fMapView.Map;
						regionalMapPanel.Plot = this.PlotView.Plot;
						regionalMapPanel.Mode = MapViewMode.PlayerView;
						if (Session.PlayerView == null)
						{
							Session.PlayerView = new PlayerViewForm(this);
						}
						Session.PlayerView.ShowRegionalMap(regionalMapPanel);
					}
					else
					{
						Guid map_id2 = new Guid(e.Url.LocalPath);
						RegionalMap regionalMap = Session.Project.FindRegionalMap(map_id2);
						if (regionalMap != null)
						{
							this.map_view(regionalMap);
						}
					}
				}
				if (e.Url.Scheme == "maparea")
				{
					e.Cancel = true;
					MapView mapView2 = null;
					foreach (Control control in this.PreviewSplitter.Panel1.Controls)
					{
						if (control is MapView)
						{
							mapView2 = (control as MapView);
							break;
						}
					}
					if (mapView2 == null || mapView2.SelectedArea == null)
					{
						return;
					}
					if (e.Url.LocalPath == "edit")
					{
						this.edit_map_area(mapView2.Map, mapView2.SelectedArea, mapView2);
					}
					if (e.Url.LocalPath == "create")
					{
						PlotPointForm plotPointForm = new PlotPointForm(new PlotPoint(mapView2.SelectedArea.Name)
						{
							Element = new MapElement(mapView2.Map.ID, mapView2.SelectedArea.ID)
						}, this.PlotView.Plot, false);
						if (plotPointForm.ShowDialog() == DialogResult.OK)
						{
							this.PlotView.Plot.Points.Add(plotPointForm.PlotPoint);
							this.UpdateView();
							Session.Modified = true;
						}
					}
					if (e.Url.LocalPath == "encounter")
					{
						Encounter encounter = new Encounter();
						encounter.MapID = mapView2.Map.ID;
						encounter.MapAreaID = mapView2.SelectedArea.ID;
						encounter.SetStandardEncounterNotes();
						PlotPointForm plotPointForm2 = new PlotPointForm(new PlotPoint(mapView2.SelectedArea.Name)
						{
							Element = encounter
						}, this.PlotView.Plot, true);
						if (plotPointForm2.ShowDialog() == DialogResult.OK)
						{
							this.PlotView.Plot.Points.Add(plotPointForm2.PlotPoint);
							this.UpdateView();
							Session.Modified = true;
						}
					}
					if (e.Url.LocalPath == "trap")
					{
						TrapElement trapElement = new TrapElement();
						trapElement.Trap.Name = mapView2.SelectedArea.Name;
						trapElement.MapID = mapView2.Map.ID;
						trapElement.MapAreaID = mapView2.SelectedArea.ID;
						PlotPointForm plotPointForm3 = new PlotPointForm(new PlotPoint(mapView2.SelectedArea.Name)
						{
							Element = trapElement
						}, this.PlotView.Plot, true);
						if (plotPointForm3.ShowDialog() == DialogResult.OK)
						{
							this.PlotView.Plot.Points.Add(plotPointForm3.PlotPoint);
							this.UpdateView();
							Session.Modified = true;
						}
					}
					if (e.Url.LocalPath == "challenge")
					{
						SkillChallenge skillChallenge2 = new SkillChallenge();
						skillChallenge2.Name = mapView2.SelectedArea.Name;
						skillChallenge2.MapID = mapView2.Map.ID;
						skillChallenge2.MapAreaID = mapView2.SelectedArea.ID;
						skillChallenge2.Level = Session.Project.Party.Level;
						PlotPointForm plotPointForm4 = new PlotPointForm(new PlotPoint(mapView2.SelectedArea.Name)
						{
							Element = skillChallenge2
						}, this.PlotView.Plot, true);
						if (plotPointForm4.ShowDialog() == DialogResult.OK)
						{
							this.PlotView.Plot.Points.Add(plotPointForm4.PlotPoint);
							this.UpdateView();
							Session.Modified = true;
						}
					}
				}
				if (e.Url.Scheme == "maploc")
				{
					e.Cancel = true;
					RegionalMapPanel regionalMapPanel2 = null;
					foreach (Control control2 in this.PreviewSplitter.Panel1.Controls)
					{
						if (control2 is RegionalMapPanel)
						{
							regionalMapPanel2 = (control2 as RegionalMapPanel);
							break;
						}
					}
					if (regionalMapPanel2 == null || regionalMapPanel2.SelectedLocation == null)
					{
						return;
					}
					if (e.Url.LocalPath == "edit")
					{
						this.edit_map_location(regionalMapPanel2.Map, regionalMapPanel2.SelectedLocation, regionalMapPanel2);
					}
					if (e.Url.LocalPath == "create")
					{
						PlotPointForm plotPointForm5 = new PlotPointForm(new PlotPoint(regionalMapPanel2.SelectedLocation.Name)
						{
							RegionalMapID = regionalMapPanel2.Map.ID,
							MapLocationID = regionalMapPanel2.SelectedLocation.ID
						}, this.PlotView.Plot, false);
						if (plotPointForm5.ShowDialog() == DialogResult.OK)
						{
							this.PlotView.Plot.Points.Add(plotPointForm5.PlotPoint);
							this.UpdateView();
							Session.Modified = true;
						}
					}
					if (e.Url.LocalPath == "encounter")
					{
						Encounter encounter2 = new Encounter();
						encounter2.SetStandardEncounterNotes();
						PlotPointForm plotPointForm6 = new PlotPointForm(new PlotPoint(regionalMapPanel2.SelectedLocation.Name)
						{
							RegionalMapID = regionalMapPanel2.Map.ID,
							MapLocationID = regionalMapPanel2.SelectedLocation.ID,
							Element = encounter2
						}, this.PlotView.Plot, true);
						if (plotPointForm6.ShowDialog() == DialogResult.OK)
						{
							this.PlotView.Plot.Points.Add(plotPointForm6.PlotPoint);
							this.UpdateView();
							Session.Modified = true;
						}
					}
					if (e.Url.LocalPath == "trap")
					{
						TrapElement trapElement2 = new TrapElement();
						trapElement2.Trap.Name = regionalMapPanel2.SelectedLocation.Name;
						PlotPointForm plotPointForm7 = new PlotPointForm(new PlotPoint(regionalMapPanel2.SelectedLocation.Name)
						{
							RegionalMapID = regionalMapPanel2.Map.ID,
							MapLocationID = regionalMapPanel2.SelectedLocation.ID,
							Element = trapElement2
						}, this.PlotView.Plot, true);
						if (plotPointForm7.ShowDialog() == DialogResult.OK)
						{
							this.PlotView.Plot.Points.Add(plotPointForm7.PlotPoint);
							this.UpdateView();
							Session.Modified = true;
						}
					}
					if (e.Url.LocalPath == "challenge")
					{
						SkillChallenge skillChallenge3 = new SkillChallenge();
						skillChallenge3.Name = regionalMapPanel2.SelectedLocation.Name;
						skillChallenge3.Level = Session.Project.Party.Level;
						PlotPointForm plotPointForm8 = new PlotPointForm(new PlotPoint(regionalMapPanel2.SelectedLocation.Name)
						{
							RegionalMapID = regionalMapPanel2.Map.ID,
							MapLocationID = regionalMapPanel2.SelectedLocation.ID,
							Element = skillChallenge3
						}, this.PlotView.Plot, true);
						if (plotPointForm8.ShowDialog() == DialogResult.OK)
						{
							this.PlotView.Plot.Points.Add(plotPointForm8.PlotPoint);
							this.UpdateView();
							Session.Modified = true;
						}
					}
				}
				if (e.Url.Scheme == "sc")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "reset")
					{
						PlotPoint selected_point3 = this.get_selected_point();
						SkillChallenge skillChallenge4 = selected_point3.Element as SkillChallenge;
						if (skillChallenge4 != null)
						{
							foreach (SkillChallengeData current2 in skillChallenge4.Skills)
							{
								current2.Results.Successes = 0;
								current2.Results.Fails = 0;
								Session.Modified = true;
								this.UpdateView();
							}
						}
					}
				}
				if (e.Url.Scheme == "success")
				{
					e.Cancel = true;
					PlotPoint selected_point4 = this.get_selected_point();
					SkillChallenge skillChallenge5 = selected_point4.Element as SkillChallenge;
					if (skillChallenge5 != null)
					{
						SkillChallengeData skillChallengeData = skillChallenge5.FindSkill(e.Url.LocalPath);
						skillChallengeData.Results.Successes++;
						Session.Modified = true;
						this.UpdateView();
					}
				}
				if (e.Url.Scheme == "failure")
				{
					e.Cancel = true;
					PlotPoint selected_point5 = this.get_selected_point();
					SkillChallenge skillChallenge6 = selected_point5.Element as SkillChallenge;
					if (skillChallenge6 != null)
					{
						SkillChallengeData skillChallengeData2 = skillChallenge6.FindSkill(e.Url.LocalPath);
						skillChallengeData2.Results.Fails++;
						Session.Modified = true;
						this.UpdateView();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private PlotPoint get_selected_point()
		{
			switch (this.fView)
			{
			case MainForm.ViewType.Flowchart:
				return this.PlotView.SelectedPoint;
			case MainForm.ViewType.Delve:
			{
				MapView mapView = null;
				foreach (Control control in this.PreviewSplitter.Panel1.Controls)
				{
					if (control is MapView)
					{
						mapView = (control as MapView);
					}
				}
				if (mapView == null)
				{
					goto IL_303;
				}
				MapArea selectedArea = mapView.SelectedArea;
				if (selectedArea == null)
				{
					goto IL_303;
				}
				using (List<PlotPoint>.Enumerator enumerator2 = this.PlotView.Plot.Points.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						PlotPoint current = enumerator2.Current;
						if (current.Element != null)
						{
							if (current.Element is Encounter)
							{
								Encounter encounter = current.Element as Encounter;
								if (encounter.MapID == mapView.Map.ID && encounter.MapAreaID == selectedArea.ID)
								{
									PlotPoint result = current;
									return result;
								}
							}
							if (current.Element is TrapElement)
							{
								TrapElement trapElement = current.Element as TrapElement;
								if (trapElement.MapID == mapView.Map.ID && trapElement.MapAreaID == selectedArea.ID)
								{
									PlotPoint result = current;
									return result;
								}
							}
							if (current.Element is SkillChallenge)
							{
								SkillChallenge skillChallenge = current.Element as SkillChallenge;
								if (skillChallenge.MapID == mapView.Map.ID && skillChallenge.MapAreaID == selectedArea.ID)
								{
									PlotPoint result = current;
									return result;
								}
							}
							if (current.Element is MapElement)
							{
								MapElement mapElement = current.Element as MapElement;
								if (mapElement.MapID == mapView.Map.ID && mapElement.MapAreaID == selectedArea.ID)
								{
									PlotPoint result = current;
									return result;
								}
							}
						}
					}
					goto IL_303;
				}
				break;
			}
			case MainForm.ViewType.Map:
				break;
			default:
				goto IL_303;
			}
			RegionalMapPanel regionalMapPanel = null;
			foreach (Control control2 in this.PreviewSplitter.Panel1.Controls)
			{
				if (control2 is RegionalMapPanel)
				{
					regionalMapPanel = (control2 as RegionalMapPanel);
				}
			}
			if (regionalMapPanel != null)
			{
				if (regionalMapPanel.SelectedLocation == null)
				{
					return null;
				}
				foreach (PlotPoint current2 in this.PlotView.Plot.Points)
				{
					if (current2.RegionalMapID == regionalMapPanel.Map.ID && current2.MapLocationID == regionalMapPanel.SelectedLocation.ID)
					{
						PlotPoint result = current2;
						return result;
					}
				}
			}
			IL_303:
			return null;
		}

		private void set_selected_point(PlotPoint point)
		{
			switch (this.fView)
			{
			case MainForm.ViewType.Flowchart:
				this.PlotView.SelectedPoint = point;
				return;
			case MainForm.ViewType.Delve:
			{
				MapView mapView = null;
				foreach (Control control in this.PreviewSplitter.Panel1.Controls)
				{
					if (control is MapView)
					{
						mapView = (control as MapView);
					}
				}
				if (mapView != null)
				{
					mapView.SelectedArea = null;
					if (point.Element != null)
					{
						if (point.Element is Encounter)
						{
							Encounter encounter = point.Element as Encounter;
							if (encounter.MapID == mapView.Map.ID)
							{
								MapArea selectedArea = mapView.Map.FindArea(encounter.MapAreaID);
								mapView.SelectedArea = selectedArea;
							}
						}
						if (point.Element is TrapElement)
						{
							TrapElement trapElement = point.Element as TrapElement;
							if (trapElement.MapID == mapView.Map.ID)
							{
								MapArea selectedArea2 = mapView.Map.FindArea(trapElement.MapAreaID);
								mapView.SelectedArea = selectedArea2;
							}
						}
						if (point.Element is SkillChallenge)
						{
							SkillChallenge skillChallenge = point.Element as SkillChallenge;
							if (skillChallenge.MapID == mapView.Map.ID)
							{
								MapArea selectedArea3 = mapView.Map.FindArea(skillChallenge.MapAreaID);
								mapView.SelectedArea = selectedArea3;
							}
						}
						if (point.Element is MapElement)
						{
							MapElement mapElement = point.Element as MapElement;
							if (mapElement.MapID == mapView.Map.ID)
							{
								MapArea selectedArea4 = mapView.Map.FindArea(mapElement.MapAreaID);
								mapView.SelectedArea = selectedArea4;
								return;
							}
						}
					}
				}
				break;
			}
			case MainForm.ViewType.Map:
			{
				RegionalMapPanel regionalMapPanel = null;
				foreach (Control control2 in this.PreviewSplitter.Panel1.Controls)
				{
					if (control2 is RegionalMapPanel)
					{
						regionalMapPanel = (control2 as RegionalMapPanel);
					}
				}
				if (regionalMapPanel != null)
				{
					regionalMapPanel.SelectedLocation = null;
					if (point.RegionalMapID != regionalMapPanel.Map.ID)
					{
						MapLocation selectedLocation = regionalMapPanel.Map.FindLocation(point.MapLocationID);
						regionalMapPanel.SelectedLocation = selectedLocation;
					}
				}
				break;
			}
			default:
				return;
			}
		}

		private void update_preview()
		{
			try
			{
				this.Preview.Document.OpenNew(true);
				bool flag = false;
				PlotPoint selected_point = this.get_selected_point();
				Map map = null;
				MapArea mapArea = null;
				MapLocation mapLocation = null;
				if (selected_point == null && this.fView == MainForm.ViewType.Delve)
				{
					MapView mapView = null;
					foreach (Control control in this.PreviewSplitter.Panel1.Controls)
					{
						if (control is MapView)
						{
							mapView = (control as MapView);
							break;
						}
					}
					if (mapView != null)
					{
						map = mapView.Map;
						mapArea = mapView.SelectedArea;
					}
				}
				if (selected_point == null && this.fView == MainForm.ViewType.Map)
				{
					RegionalMapPanel regionalMapPanel = null;
					foreach (Control control2 in this.PreviewSplitter.Panel1.Controls)
					{
						if (control2 is RegionalMapPanel)
						{
							regionalMapPanel = (control2 as RegionalMapPanel);
							break;
						}
					}
					if (regionalMapPanel != null)
					{
						RegionalMap arg_115_0 = regionalMapPanel.Map;
						mapLocation = regionalMapPanel.SelectedLocation;
					}
				}
				if (mapArea != null)
				{
					this.Preview.Document.Write(HTML.MapArea(mapArea, DisplaySize.Small));
				}
				else if (mapLocation != null)
				{
					this.Preview.Document.Write(HTML.MapLocation(mapLocation, DisplaySize.Small));
				}
				else
				{
					int party_level = (selected_point != null) ? Workspace.GetPartyLevel(selected_point) : 0;
					this.Preview.Document.Write(HTML.PlotPoint(selected_point, this.PlotView.Plot, party_level, true, this.fView, DisplaySize.Small));
				}
				this.PreviewInfoSplitter.Panel2.Controls.Clear();
				if (selected_point != null)
				{
					if (selected_point.Element is Encounter)
					{
						Encounter encounter = selected_point.Element as Encounter;
						if (encounter.MapID != Guid.Empty)
						{
							this.set_tmap_preview(encounter.MapID, encounter.MapAreaID, encounter);
							flag = true;
						}
					}
					if (selected_point.Element is MapElement)
					{
						MapElement mapElement = selected_point.Element as MapElement;
						if (mapElement.MapID != Guid.Empty)
						{
							this.set_tmap_preview(mapElement.MapID, mapElement.MapAreaID, null);
							flag = true;
						}
					}
					if (!flag)
					{
						RegionalMap map2 = null;
						MapLocation mapLocation2 = null;
						selected_point.GetRegionalMapArea(ref map2, ref mapLocation2, Session.Project);
						if (mapLocation2 != null)
						{
							this.set_rmap_preview(map2, mapLocation2);
							flag = true;
						}
					}
					if (!flag && selected_point.Subplot.Points.Count > 0)
					{
						this.set_subplot_preview(selected_point.Subplot);
						flag = true;
					}
				}
				else if (mapArea != null)
				{
					this.set_tmap_preview(map.ID, mapArea.ID, null);
					flag = true;
				}
				if (!flag)
				{
					this.PreviewInfoSplitter.Panel2.Controls.Clear();
				}
				this.PreviewInfoSplitter.Panel2Collapsed = !flag;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void set_tmap_preview(Guid map_id, Guid area_id, Encounter enc)
		{
			try
			{
				Map map = Session.Project.FindTacticalMap(map_id);
				if (map != null)
				{
					MapView mapView = new MapView();
					mapView.Mode = MapViewMode.Plain;
					mapView.FrameType = MapDisplayType.Dimmed;
					mapView.LineOfSight = false;
					mapView.ShowGrid = MapGridMode.None;
					mapView.BorderSize = 1;
					mapView.Map = map;
					if (area_id != Guid.Empty)
					{
						MapArea mapArea = map.FindArea(area_id);
						if (mapArea != null)
						{
							mapView.Viewpoint = mapArea.Region;
						}
					}
					if (enc != null)
					{
						mapView.Encounter = enc;
						mapView.DoubleClick += new EventHandler(this.run_encounter);
					}
					else
					{
						mapView.DoubleClick += new EventHandler(this.show_tmap);
					}
					mapView.BorderStyle = BorderStyle.Fixed3D;
					mapView.Dock = DockStyle.Fill;
					this.PreviewInfoSplitter.Panel2.Controls.Add(mapView);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void set_rmap_preview(RegionalMap map, MapLocation loc)
		{
			try
			{
				RegionalMapPanel regionalMapPanel = new RegionalMapPanel();
				regionalMapPanel.Mode = MapViewMode.Plain;
				regionalMapPanel.Map = map;
				regionalMapPanel.HighlightedLocation = loc;
				regionalMapPanel.DoubleClick += new EventHandler(this.show_rmap);
				regionalMapPanel.BorderStyle = BorderStyle.Fixed3D;
				regionalMapPanel.Dock = DockStyle.Fill;
				this.PreviewInfoSplitter.Panel2.Controls.Add(regionalMapPanel);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void set_subplot_preview(Plot p)
		{
			try
			{
				PlotView plotView = new PlotView();
				plotView.Plot = p;
				plotView.Mode = PlotViewMode.Plain;
				plotView.LinkStyle = Session.Preferences.LinkStyle;
				plotView.DoubleClick += new EventHandler(this.ExploreBtn_Click);
				plotView.BorderStyle = BorderStyle.Fixed3D;
				plotView.Dock = DockStyle.Fill;
				this.PreviewInfoSplitter.Panel2.Controls.Add(plotView);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private bool edit_element(object sender, EventArgs e)
		{
			try
			{
				PlotPoint selected_point = this.get_selected_point();
				if (selected_point != null)
				{
					int partyLevel = Workspace.GetPartyLevel(selected_point);
					Encounter encounter = selected_point.Element as Encounter;
					if (encounter != null)
					{
						EncounterBuilderForm encounterBuilderForm = new EncounterBuilderForm(encounter, partyLevel, false);
						if (encounterBuilderForm.ShowDialog() == DialogResult.OK)
						{
							selected_point.Element = encounterBuilderForm.Encounter;
							Session.Modified = true;
							this.UpdateView();
							bool result = true;
							return result;
						}
					}
					SkillChallenge skillChallenge = selected_point.Element as SkillChallenge;
					if (skillChallenge != null)
					{
						SkillChallengeBuilderForm skillChallengeBuilderForm = new SkillChallengeBuilderForm(skillChallenge);
						if (skillChallengeBuilderForm.ShowDialog() == DialogResult.OK)
						{
							selected_point.Element = skillChallengeBuilderForm.SkillChallenge;
							Session.Modified = true;
							this.UpdateView();
							bool result = true;
							return result;
						}
					}
					TrapElement trapElement = selected_point.Element as TrapElement;
					if (trapElement != null)
					{
						TrapBuilderForm trapBuilderForm = new TrapBuilderForm(trapElement.Trap);
						if (trapBuilderForm.ShowDialog() == DialogResult.OK)
						{
							trapElement.Trap = trapBuilderForm.Trap;
							Session.Modified = true;
							this.UpdateView();
							bool result = true;
							return result;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return false;
		}

		private void run_encounter(object sender, EventArgs e)
		{
			try
			{
				PlotPoint selected_point = this.get_selected_point();
				if (selected_point != null)
				{
					Encounter encounter = selected_point.Element as Encounter;
					if (encounter != null)
					{
						CombatForm combatForm = new CombatForm(new CombatState
						{
							Encounter = encounter,
							PartyLevel = Workspace.GetPartyLevel(selected_point)
						});
						combatForm.Show();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void edit_map_area(Map map, MapArea map_area, MapView mapview)
		{
			try
			{
				if (map != null && map_area != null)
				{
					int index = map.Areas.IndexOf(map_area);
					MapAreaForm mapAreaForm = new MapAreaForm(map_area, map);
					if (mapAreaForm.ShowDialog() == DialogResult.OK)
					{
						map.Areas[index] = mapAreaForm.Area;
						Session.Modified = true;
						if (mapview != null)
						{
							mapview.SelectedArea = mapAreaForm.Area;
						}
						this.UpdateView();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void edit_map_location(RegionalMap map, MapLocation loc, RegionalMapPanel mappanel)
		{
			try
			{
				if (map != null && loc != null)
				{
					int index = map.Locations.IndexOf(loc);
					MapLocationForm mapLocationForm = new MapLocationForm(loc);
					if (mapLocationForm.ShowDialog() == DialogResult.OK)
					{
						map.Locations[index] = mapLocationForm.MapLocation;
						Session.Modified = true;
						if (mappanel != null)
						{
							mappanel.SelectedLocation = mapLocationForm.MapLocation;
						}
						this.UpdateView();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void show_map_location(RegionalMap map, MapLocation loc)
		{
			RegionalMapForm regionalMapForm = new RegionalMapForm(map, loc);
			regionalMapForm.ShowDialog();
		}

		private void show_tmap(object sender, EventArgs e)
		{
			MapView mapview = sender as MapView;
			if (Session.PlayerView == null)
			{
				Session.PlayerView = new PlayerViewForm(this);
			}
			Session.PlayerView.ShowTacticalMap(mapview, null);
		}

		private void show_rmap(object sender, EventArgs e)
		{
			RegionalMapPanel panel = sender as RegionalMapPanel;
			if (Session.PlayerView == null)
			{
				Session.PlayerView = new PlayerViewForm(this);
			}
			Session.PlayerView.ShowRegionalMap(panel);
		}

		private void NavigationTree_DragOver(object sender, DragEventArgs e)
		{
			try
			{
				e.Effect = DragDropEffects.None;
				PlotPoint plotPoint = e.Data.GetData(typeof(PlotPoint)) as PlotPoint;
				if (plotPoint != null)
				{
					Point pt = this.NavigationTree.PointToClient(Cursor.Position);
					TreeNode nodeAt = this.NavigationTree.GetNodeAt(pt);
					if (nodeAt != null)
					{
						Plot plot = nodeAt.Tag as Plot;
						if (!plot.Points.Contains(plotPoint))
						{
							if (plotPoint != Session.Project.FindParent(plot))
							{
								this.NavigationTree.SelectedNode = nodeAt;
								e.Effect = DragDropEffects.Move;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void NavigationTree_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				PlotPoint plotPoint = e.Data.GetData(typeof(PlotPoint)) as PlotPoint;
				if (plotPoint != null)
				{
					Point pt = this.NavigationTree.PointToClient(Cursor.Position);
					TreeNode nodeAt = this.NavigationTree.GetNodeAt(pt);
					if (nodeAt != null)
					{
						Plot plot = nodeAt.Tag as Plot;
						this.NavigationTree.SelectedNode = nodeAt;
						if (!plot.Points.Contains(plotPoint))
						{
							if (plotPoint != Session.Project.FindParent(plot))
							{
								Plot plot2 = Session.Project.FindParent(plotPoint);
								plot2.RemovePoint(plotPoint);
								plotPoint.Links.Clear();
								plot2.Points.Remove(plotPoint);
								plot.Points.Add(plotPoint);
								Session.Modified = true;
								this.UpdateView();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void NavigationTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			try
			{
				if (!this.fUpdating)
				{
					Plot plot = e.Node.Tag as Plot;
					if (this.PlotView.Plot != plot)
					{
						this.PlotView.Plot = plot;
						this.UpdateView();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void Welcome_NewProjectClicked(object sender, EventArgs e)
		{
			try
			{
				this.FileNew_Click(sender, e);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void Welcome_OpenProjectClicked(object sender, EventArgs e)
		{
			try
			{
				this.FileOpen_Click(sender, e);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void Welcome_OpenLastProjectClicked(object sender, EventArgs e)
		{
			try
			{
				this.open_file(Session.Preferences.LastFile);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void Welcome_DelveClicked(object sender, EventArgs e)
		{
			try
			{
				this.AdvancedDelve_Click(null, null);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void Welcome_PremadeClicked(object sender, EventArgs e)
		{
			try
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Filter = Program.ProjectFilter;
				saveFileDialog.FileName = "Example";
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					this.fDownloadedFile = saveFileDialog.FileName;
					Program.SplashScreen = new ProgressScreen("Downloading example adventure...", 100);
					Program.SplashScreen.Show();
					WebClient webClient = new WebClient();
					webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.wc_DownloadProgressChanged);
					webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.wc_DownloadFileCompleted);
					webClient.DownloadFileAsync(new Uri("http://www.habitualindolence.net/masterplan/downloads/example.masterplan"), this.fDownloadedFile);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			Program.SplashScreen.Progress = e.ProgressPercentage;
		}

		private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			Program.SplashScreen.Progress = Program.SplashScreen.Actions;
			Program.SplashScreen.CurrentAction = "Opening sample adventure...";
			this.open_file(this.fDownloadedFile);
			Program.SplashScreen.Close();
			Program.SplashScreen = null;
			this.fDownloadedFile = "";
		}

		private void PlotView_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (this.PlotView.SelectedPoint == null)
				{
					this.AddBtn_Click(sender, e);
				}
				else if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
				{
					this.ExploreBtn_Click(sender, e);
				}
				else
				{
					this.EditBtn_Click(sender, e);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlotView_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				this.update_preview();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlotView_PlotLayoutChanged(object sender, EventArgs e)
		{
			Session.Modified = true;
		}

		private void PlotView_PlotChanged(object sender, EventArgs e)
		{
			this.UpdateView();
		}

		private void PointMenu_Opening(object sender, CancelEventArgs e)
		{
			try
			{
				this.ContextAddBetween.DropDownItems.Clear();
				this.ContextDisconnect.DropDownItems.Clear();
				if (this.PlotView.SelectedPoint != null)
				{
					foreach (PlotPoint current in this.PlotView.Plot.Points)
					{
						if (current.Links.Contains(this.PlotView.SelectedPoint.ID))
						{
							ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("After \"" + current.Name + "\"");
							toolStripMenuItem.Click += new EventHandler(this.add_between);
							toolStripMenuItem.Tag = new Pair<PlotPoint, PlotPoint>(current, this.PlotView.SelectedPoint);
							this.ContextAddBetween.DropDownItems.Add(toolStripMenuItem);
							ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(current.Name);
							toolStripMenuItem2.Click += new EventHandler(this.disconnect_points);
							toolStripMenuItem2.Tag = new Pair<PlotPoint, PlotPoint>(current, this.PlotView.SelectedPoint);
							this.ContextDisconnect.DropDownItems.Add(toolStripMenuItem2);
						}
					}
					foreach (Guid current2 in this.PlotView.SelectedPoint.Links)
					{
						PlotPoint plotPoint = this.PlotView.Plot.FindPoint(current2);
						ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem("Before \"" + plotPoint.Name + "\"");
						toolStripMenuItem3.Click += new EventHandler(this.add_between);
						toolStripMenuItem3.Tag = new Pair<PlotPoint, PlotPoint>(this.PlotView.SelectedPoint, plotPoint);
						this.ContextAddBetween.DropDownItems.Add(toolStripMenuItem3);
						ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem(plotPoint.Name);
						toolStripMenuItem4.Click += new EventHandler(this.disconnect_points);
						toolStripMenuItem4.Tag = new Pair<PlotPoint, PlotPoint>(this.PlotView.SelectedPoint, plotPoint);
						this.ContextDisconnect.DropDownItems.Add(toolStripMenuItem4);
					}
				}
				this.ContextAddBetween.Enabled = (this.ContextAddBetween.DropDownItems.Count != 0);
				this.ContextDisconnect.Enabled = (this.ContextDisconnect.DropDownItems.Count != 0);
				this.ContextDisconnectAll.Enabled = this.ContextDisconnect.Enabled;
				this.ContextMoveTo.DropDownItems.Clear();
				if (this.PlotView.SelectedPoint != null)
				{
					foreach (PlotPoint current3 in this.PlotView.Plot.Points)
					{
						if (current3.Links.Contains(this.PlotView.SelectedPoint.ID))
						{
							ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem(current3.Name);
							toolStripMenuItem5.Click += new EventHandler(this.move_to_subplot);
							toolStripMenuItem5.Tag = new Pair<PlotPoint, PlotPoint>(current3, this.PlotView.SelectedPoint);
							this.ContextMoveTo.DropDownItems.Add(toolStripMenuItem5);
						}
					}
					this.ContextStateNormal.Checked = (this.PlotView.SelectedPoint.State == PlotPointState.Normal);
					this.ContextStateCompleted.Checked = (this.PlotView.SelectedPoint.State == PlotPointState.Completed);
					this.ContextStateSkipped.Checked = (this.PlotView.SelectedPoint.State == PlotPointState.Skipped);
				}
				this.ContextMoveTo.Enabled = (this.ContextMoveTo.DropDownItems.Count != 0);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void delve_view(Map map)
		{
			if (map == null)
			{
				return;
			}
			foreach (Control control in this.PreviewSplitter.Panel1.Controls)
			{
				control.Visible = false;
			}
			MapView mapView = new MapView();
			mapView.Map = map;
			mapView.Plot = this.PlotView.Plot;
			mapView.Mode = MapViewMode.Thumbnail;
			mapView.HighlightAreas = true;
			mapView.LineOfSight = false;
			mapView.BorderSize = 1;
			mapView.BorderStyle = BorderStyle.FixedSingle;
			mapView.Dock = DockStyle.Fill;
			this.PreviewSplitter.Panel1.Controls.Add(mapView);
			mapView.AreaSelected += new MapAreaEventHandler(this.select_maparea);
			mapView.DoubleClick += new EventHandler(this.edit_maparea);
			this.fDelveView = mapView;
			this.fView = MainForm.ViewType.Delve;
			this.update_preview();
		}

		private void delve_view_edit()
		{
			Map map = this.fDelveView.Map;
			int index = Session.Project.Maps.IndexOf(map);
			MapBuilderForm mapBuilderForm = new MapBuilderForm(map, false);
			if (mapBuilderForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.Maps[index] = mapBuilderForm.Map;
				Session.Modified = true;
				this.fDelveView.Map = mapBuilderForm.Map;
			}
		}

		private void select_maparea(object sender, MapAreaEventArgs e)
		{
			this.update_preview();
		}

		private void edit_maparea(object sender, EventArgs e)
		{
			if (this.fDelveView.SelectedArea != null)
			{
				int index = this.fDelveView.Map.Areas.IndexOf(this.fDelveView.SelectedArea);
				MapAreaForm mapAreaForm = new MapAreaForm(this.fDelveView.SelectedArea, this.fDelveView.Map);
				if (mapAreaForm.ShowDialog() == DialogResult.OK)
				{
					this.fDelveView.Map.Areas[index] = mapAreaForm.Area;
					Session.Modified = true;
					this.fDelveView.MapChanged();
				}
			}
		}

		private void map_view(RegionalMap map)
		{
			if (map == null)
			{
				return;
			}
			foreach (Control control in this.PreviewSplitter.Panel1.Controls)
			{
				control.Visible = false;
			}
			RegionalMapPanel regionalMapPanel = new RegionalMapPanel();
			regionalMapPanel.Map = map;
			regionalMapPanel.Plot = this.PlotView.Plot;
			regionalMapPanel.Mode = MapViewMode.Thumbnail;
			regionalMapPanel.BorderStyle = BorderStyle.FixedSingle;
			regionalMapPanel.Dock = DockStyle.Fill;
			this.PreviewSplitter.Panel1.Controls.Add(regionalMapPanel);
			regionalMapPanel.SelectedLocationModified += new EventHandler(this.select_maplocation);
			regionalMapPanel.DoubleClick += new EventHandler(this.edit_maplocation);
			this.fMapView = regionalMapPanel;
			this.fView = MainForm.ViewType.Map;
			this.update_preview();
		}

		private void map_view_edit()
		{
			RegionalMap map = this.fMapView.Map;
			int index = Session.Project.RegionalMaps.IndexOf(map);
			RegionalMapForm regionalMapForm = new RegionalMapForm(map, null);
			if (regionalMapForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.RegionalMaps[index] = regionalMapForm.Map;
				Session.Modified = true;
				this.fMapView.Map = regionalMapForm.Map;
			}
		}

		private void select_maplocation(object sender, EventArgs e)
		{
			this.update_preview();
		}

		private void edit_maplocation(object sender, EventArgs e)
		{
			if (this.fMapView.SelectedLocation != null)
			{
				int index = this.fMapView.Map.Locations.IndexOf(this.fMapView.SelectedLocation);
				MapLocationForm mapLocationForm = new MapLocationForm(this.fMapView.SelectedLocation);
				if (mapLocationForm.ShowDialog() == DialogResult.OK)
				{
					this.fMapView.Map.Locations[index] = mapLocationForm.MapLocation;
					Session.Modified = true;
					this.fMapView.Invalidate();
				}
			}
		}

		private void flowchart_view()
		{
			List<Control> list = new List<Control>();
			foreach (Control control in this.PreviewSplitter.Panel1.Controls)
			{
				if (control.Visible)
				{
					list.Add(control);
				}
			}
			foreach (Control current in list)
			{
				this.PreviewSplitter.Panel1.Controls.Remove(current);
			}
			foreach (Control control2 in this.PreviewSplitter.Panel1.Controls)
			{
				control2.Visible = true;
			}
			this.fView = MainForm.ViewType.Flowchart;
			this.update_preview();
		}

		private void BackgroundList_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.update_background_item();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void BackgroundDetails_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			try
			{
				if (e.Url.Scheme == "background")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "edit")
					{
						this.BackgroundEditBtn_Click(sender, e);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void EntryList_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.update_encyclopedia_entry();
				this.EntryList.Focus();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void EntryDetails_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			try
			{
				if (e.Url.Scheme == "entry")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "edit")
					{
						this.EncEditBtn_Click(sender, e);
					}
					else
					{
						Guid entry_id = new Guid(e.Url.LocalPath);
						this.SelectedEncyclopediaItem = Session.Project.Encyclopedia.FindEntry(entry_id);
					}
				}
				if (e.Url.Scheme == "missing")
				{
					e.Cancel = true;
					string localPath = e.Url.LocalPath;
					EncyclopediaEntry encyclopediaEntry = this.create_entry(localPath, "");
					if (encyclopediaEntry != null)
					{
						this.update_encyclopedia_list();
						this.SelectedEncyclopediaItem = encyclopediaEntry;
					}
				}
				if (e.Url.Scheme == "group")
				{
					e.Cancel = true;
					if (e.Url.LocalPath == "edit")
					{
						this.EncEditBtn_Click(sender, e);
					}
					else
					{
						Guid entry_id2 = new Guid(e.Url.LocalPath);
						this.SelectedEncyclopediaItem = Session.Project.Encyclopedia.FindGroup(entry_id2);
					}
				}
				if (e.Url.Scheme == "map")
				{
					e.Cancel = true;
					Guid location_id = new Guid(e.Url.LocalPath);
					foreach (RegionalMap current in Session.Project.RegionalMaps)
					{
						MapLocation mapLocation = current.FindLocation(location_id);
						if (mapLocation != null)
						{
							RegionalMapForm regionalMapForm = new RegionalMapForm(current, mapLocation);
							regionalMapForm.ShowDialog();
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void RulesList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.update_selected_rule();
		}

		private void AttachmentList_DragOver(object sender, DragEventArgs e)
		{
			try
			{
				string[] array = e.Data.GetData("FileDrop") as string[];
				if (array != null)
				{
					e.Effect = DragDropEffects.All;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void AttachmentList_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				string[] array = e.Data.GetData("FileDrop") as string[];
				if (array != null)
				{
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string filename = array2[i];
						this.add_attachment(filename);
					}
					this.update_attachments();
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
				this.fUpdating = true;
				this.NoteBox.Text = "(no note selected)";
				this.NoteBox.Enabled = false;
				this.NoteBox.ReadOnly = true;
				if (this.SelectedNote != null)
				{
					this.NoteBox.Text = this.SelectedNote.Content;
					this.NoteBox.Enabled = true;
					this.NoteBox.ReadOnly = false;
				}
				if (this.SelectedIssue != null)
				{
					this.NoteBox.Text = this.SelectedIssue.ToString();
					this.NoteBox.Enabled = true;
				}
				this.fUpdating = false;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void NoteBox_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (!this.fUpdating)
				{
					if (this.SelectedNote != null)
					{
						this.SelectedNote.Content = this.NoteBox.Text;
						this.NoteList.SelectedItems[0].Text = this.SelectedNote.Name;
						this.NoteList.SelectedItems[0].ForeColor = ((this.SelectedNote.Content != "") ? SystemColors.WindowText : SystemColors.GrayText);
						this.NoteList.Sort();
						Session.Modified = true;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PartyBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "party" && e.Url.LocalPath == "edit")
			{
				e.Cancel = true;
				this.ProjectPlayers_Click(sender, e);
			}
			if (e.Url.Scheme == "show")
			{
				e.Cancel = true;
				this.fPartyBreakdownSecondary = e.Url.LocalPath;
				this.update_party();
			}
		}

		private void GeneratorBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "entry")
			{
				e.Cancel = true;
				string localPath = e.Url.LocalPath;
				EncyclopediaEntry encyclopediaEntry = Session.Project.Encyclopedia.FindEntry(localPath);
				if (encyclopediaEntry == null)
				{
					encyclopediaEntry = new EncyclopediaEntry();
					encyclopediaEntry.Name = localPath;
					encyclopediaEntry.Category = "People";
				}
				Session.Project.Encyclopedia.Entries.IndexOf(encyclopediaEntry);
				EncyclopediaEntryForm encyclopediaEntryForm = new EncyclopediaEntryForm(encyclopediaEntry);
				if (encyclopediaEntryForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.Encyclopedia.Entries.Add(encyclopediaEntryForm.Entry);
					Session.Modified = true;
					this.update_encyclopedia_list();
				}
			}
			if (e.Url.Scheme == "parcel")
			{
				e.Cancel = true;
				string localPath2 = e.Url.LocalPath;
				Parcel parcel = new Parcel();
				parcel.Name = "Item";
				parcel.Details = localPath2;
				Session.Project.TreasureParcels.Add(parcel);
				Session.Modified = true;
				ParcelListForm parcelListForm = new ParcelListForm();
				parcelListForm.ShowDialog();
			}
		}

		private void ReferencePages_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.ReferencePages.SelectedTab == this.CompendiumPage && this.CompendiumBrowser.Url == null)
			{
				List<string> list = new List<string>();
				list.AddRange(HTML.GetHead(null, null, DisplaySize.Small));
				list.Add("<BODY>");
				list.Add("<P class=instruction>");
				list.Add("No PC details have been entered; click <A href=\"party:edit\">here</A> to do this now.");
				list.Add("</P>");
				list.Add("<P class=instruction>");
				list.Add("When PCs have been entered, you will see a useful breakdown of their defences, passive skills and known languages here.");
				list.Add("</P>");
				list.Add("</BODY>");
				list.Add("</HTML>");
				this.CompendiumBrowser.DocumentText = HTML.Concatenate(list);
				this.CompendiumBrowser.Navigate("http://www.wizards.com/dndinsider/compendium/database.aspx");
			}
		}

		private void ContextStateNormal_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlotView.SelectedPoint != null)
				{
					List<PlotPoint> subtree = this.PlotView.SelectedPoint.Subtree;
					foreach (PlotPoint current in subtree)
					{
						current.State = PlotPointState.Normal;
					}
					Session.Modified = true;
					this.update_workspace();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ContextStateCompleted_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlotView.SelectedPoint != null)
				{
					List<PlotPoint> subtree = this.PlotView.SelectedPoint.Subtree;
					foreach (PlotPoint current in subtree)
					{
						current.State = PlotPointState.Completed;
					}
					Session.Modified = true;
					this.update_workspace();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ContextStateSkipped_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlotView.SelectedPoint != null)
				{
					List<PlotPoint> subtree = this.PlotView.SelectedPoint.Subtree;
					foreach (PlotPoint current in subtree)
					{
						current.State = PlotPointState.Skipped;
					}
					Session.Modified = true;
					this.update_workspace();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ContextDisconnectAll_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.SelectedPoint.Links.Clear();
				Guid iD = this.PlotView.SelectedPoint.ID;
				foreach (PlotPoint current in this.PlotView.Plot.Points)
				{
					while (current.Links.Contains(iD))
					{
						current.Links.Remove(iD);
					}
				}
				this.PlotView.RecalculateLayout();
				Session.Modified = true;
				this.update_workspace();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void FileMenu_DropDownOpening(object sender, EventArgs e)
		{
			try
			{
				this.FileSave.Enabled = (Session.Project != null);
				this.FileSaveAs.Enabled = (Session.Project != null);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ProjectMenu_DropDownOpening(object sender, EventArgs e)
		{
			try
			{
				this.ProjectProject.Enabled = (Session.Project != null);
				this.ProjectOverview.Enabled = (Session.Project != null);
				this.ProjectCampaignSettings.Enabled = (Session.Project != null);
				this.ProjectPassword.Enabled = (Session.Project != null);
				this.ProjectTacticalMaps.Enabled = (Session.Project != null);
				this.ProjectRegionalMaps.Enabled = (Session.Project != null);
				this.ProjectPlayers.Enabled = (Session.Project != null);
				this.ProjectParcels.Enabled = (Session.Project != null);
				this.ProjectDecks.Enabled = (Session.Project != null);
				this.ProjectCustomCreatures.Enabled = (Session.Project != null);
				this.ProjectCalendars.Enabled = (Session.Project != null);
				this.ProjectEncounters.Enabled = (Session.Project != null && Session.Project.SavedCombats.Count != 0);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlayerViewMenu_DropDownOpening(object sender, EventArgs e)
		{
			try
			{
				this.PlayerViewShow.Enabled = (Session.Project != null);
				this.PlayerViewShow.Checked = (Session.PlayerView != null);
				this.PlayerViewClear.Enabled = (Session.PlayerView != null && Session.PlayerView.Mode != PlayerViewMode.Blank);
				this.PlayerViewOtherDisplay.Enabled = (Screen.AllScreens.Length > 1);
				this.PlayerViewOtherDisplay.Checked = (Screen.AllScreens.Length > 1 && PlayerViewForm.UseOtherDisplay);
				this.TextSizeSmall.Checked = (PlayerViewForm.DisplaySize == DisplaySize.Small);
				this.TextSizeMedium.Checked = (PlayerViewForm.DisplaySize == DisplaySize.Medium);
				this.TextSizeLarge.Checked = (PlayerViewForm.DisplaySize == DisplaySize.Large);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsMenu_DropDownOpening(object sender, EventArgs e)
		{
			try
			{
				this.ToolsImportProject.Enabled = (Session.Project != null);
				this.ToolsExportProject.Enabled = (Session.Project != null);
				this.ToolsExportHandout.Enabled = (Session.Project != null);
				this.ToolsExportLoot.Enabled = (Session.Project != null);
				this.ToolsIssues.Enabled = (Session.Project != null);
				this.ToolsTileChecklist.Enabled = (Session.Project != null);
				this.ToolsMiniChecklist.Enabled = (Session.Project != null);
				this.ToolsAddIns.DropDownItems.Clear();
				foreach (IAddIn current in Session.AddIns)
				{
					ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(current.Name);
					toolStripMenuItem.ToolTipText = TextHelper.Wrap(current.Description);
					toolStripMenuItem.Tag = current;
					this.ToolsAddIns.DropDownItems.Add(toolStripMenuItem);
					foreach (ICommand current2 in current.Commands)
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
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ViewMenu_DropDownOpening(object sender, EventArgs e)
		{
			this.ViewDefault.Checked = (this.PlotView.Mode == PlotViewMode.Normal);
			this.ViewHighlighting.Checked = (this.PlotView.Mode == PlotViewMode.HighlightSelected);
			this.ViewEncounters.Checked = (this.PlotView.Mode == PlotViewMode.HighlightEncounter);
			this.ViewTraps.Checked = (this.PlotView.Mode == PlotViewMode.HighlightTrap);
			this.ViewChallenges.Checked = (this.PlotView.Mode == PlotViewMode.HighlightChallenge);
			this.ViewQuests.Checked = (this.PlotView.Mode == PlotViewMode.HighlightQuest);
			this.ViewParcels.Checked = (this.PlotView.Mode == PlotViewMode.HighlightParcel);
			this.ViewLevelling.Checked = this.PlotView.ShowLevels;
			this.ViewTooltips.Checked = this.PlotView.ShowTooltips;
			this.ViewPreview.Checked = !this.PreviewSplitter.Panel2Collapsed;
			this.ViewNavigation.Checked = !this.NavigationSplitter.Panel1Collapsed;
		}

		private void FileNew_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.fView != MainForm.ViewType.Flowchart)
				{
					this.flowchart_view();
				}
				if (this.check_modified())
				{
					Project project = new Project();
					project.Name = ((sender != null) ? "Untitled Campaign" : "Random Delve");
					project.Author = Environment.UserName;
					ProjectForm projectForm = new ProjectForm(project);
					if (projectForm.ShowDialog() == DialogResult.OK)
					{
						Session.Project = project;
						Session.Project.SetStandardBackgroundItems();
						Session.Project.TreasureParcels.AddRange(Treasure.CreateParcelSet(Session.Project.Party.Level, Session.Project.Party.Size, true));
						Session.FileName = "";
						this.PlotView.Plot = Session.Project.Plot;
						this.update_title();
						this.UpdateView();
						if (base.Controls.Contains(this.fWelcome))
						{
							base.Controls.Clear();
							this.fWelcome = null;
							base.Controls.Add(this.Pages);
							base.Controls.Add(this.MainMenu);
							this.Pages.Focus();
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void FileOpen_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.fView != MainForm.ViewType.Flowchart)
				{
					this.flowchart_view();
				}
				if (this.check_modified())
				{
					OpenFileDialog openFileDialog = new OpenFileDialog();
					openFileDialog.Filter = Program.ProjectFilter;
					openFileDialog.FileName = Session.FileName;
					if (openFileDialog.ShowDialog() == DialogResult.OK)
					{
						this.open_file(openFileDialog.FileName);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void open_file(string filename)
		{
			GC.Collect();
			Project project = Serialisation<Project>.Load(filename, SerialisationMode.Binary);
			if (project != null)
			{
				Session.CreateBackup(filename);
			}
			else
			{
				project = Session.LoadBackup(filename);
			}
			if (project != null)
			{
				if (Session.CheckPassword(project))
				{
					Session.Project = project;
					Session.FileName = filename;
					Session.Modified = false;
					Session.Project.Update();
					Session.Project.SimplifyProjectLibrary();
					this.PlotView.Plot = Session.Project.Plot;
					this.update_title();
					this.UpdateView();
					if (base.Controls.Contains(this.fWelcome))
					{
						base.Controls.Clear();
						this.fWelcome = null;
						base.Controls.Add(this.Pages);
						base.Controls.Add(this.MainMenu);
						this.Pages.Focus();
						return;
					}
				}
			}
			else
			{
				string text = "The file '" + FileName.Name(filename) + "' could not be opened.";
				MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private void FileSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session.FileName == "")
				{
					this.FileSaveAs_Click(sender, e);
				}
				else
				{
					GC.Collect();
					Session.Project.PopulateProjectLibrary();
					bool flag = Serialisation<Project>.Save(Session.FileName, Session.Project, SerialisationMode.Binary);
					if (flag)
					{
						Session.Modified = false;
					}
					else
					{
						string text = "The file could not be saved; check the filename and drive permissions and try again.";
						MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					Session.Project.SimplifyProjectLibrary();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void FileSaveAs_Click(object sender, EventArgs e)
		{
			try
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Filter = Program.ProjectFilter;
				saveFileDialog.FileName = FileName.TrimInvalidCharacters(Session.Project.Name);
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					GC.Collect();
					Session.Project.PopulateProjectLibrary();
					bool flag = Serialisation<Project>.Save(saveFileDialog.FileName, Session.Project, SerialisationMode.Binary);
					if (flag)
					{
						Session.FileName = saveFileDialog.FileName;
						Session.Modified = false;
					}
					else
					{
						string text = "The file could not be saved; check the filename and drive permissions and try again.";
						MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					Session.Project.SimplifyProjectLibrary();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void AdvancedDelve_Click(object sender, EventArgs e)
		{
			try
			{
				Project project = Session.Project;
				string fileName = Session.FileName;
				MainForm.ViewType viewType = this.fView;
				if (!this.create_delve())
				{
					Session.Project = project;
					Session.FileName = fileName;
					this.fView = viewType;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private bool create_delve()
		{
			this.FileNew_Click(null, null);
			if (Session.Project == null)
			{
				return false;
			}
			Map map = new Map();
			map.Name = "Random Dungeon";
			MapBuilderForm mapBuilderForm = new MapBuilderForm(map, true);
			if (mapBuilderForm.ShowDialog() != DialogResult.OK)
			{
				return false;
			}
			Cursor.Current = Cursors.WaitCursor;
			map = mapBuilderForm.Map;
			AutoBuildData data = new AutoBuildData();
			PlotPoint plotPoint = DelveBuilder.AutoBuild(map, data);
			if (plotPoint == null)
			{
				return false;
			}
			Session.Project.Maps.Add(map);
			foreach (PlotPoint current in plotPoint.Subplot.Points)
			{
				Session.Project.Plot.Points.Add(current);
			}
			Session.Modified = true;
			this.UpdateView();
			this.delve_view(map);
			Cursor.Current = Cursors.Default;
			return true;
		}

		private void AdvancedSample_Click(object sender, EventArgs e)
		{
			try
			{
				Project arg_05_0 = Session.Project;
				string arg_0B_0 = Session.FileName;
				this.Welcome_PremadeClicked(sender, e);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void FileExit_Click(object sender, EventArgs e)
		{
			try
			{
				base.Close();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ProjectProject_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session.Project != null)
				{
					ProjectForm projectForm = new ProjectForm(Session.Project);
					if (projectForm.ShowDialog() == DialogResult.OK)
					{
						Session.Modified = true;
						this.update_title();
						this.UpdateView();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ProjectOverview_Click(object sender, EventArgs e)
		{
			try
			{
				OverviewForm overviewForm = new OverviewForm();
				overviewForm.ShowDialog();
				this.UpdateView();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ProjectCampaignSettings_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session.Project != null)
				{
					CampaignSettingsForm campaignSettingsForm = new CampaignSettingsForm(Session.Project.CampaignSettings);
					if (campaignSettingsForm.ShowDialog() == DialogResult.OK)
					{
						Session.Modified = true;
						this.UpdateView();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ProjectPassword_Click(object sender, EventArgs e)
		{
			if (Session.CheckPassword(Session.Project))
			{
				PasswordSetForm passwordSetForm = new PasswordSetForm();
				if (passwordSetForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.Password = passwordSetForm.Password;
					Session.Project.PasswordHint = passwordSetForm.PasswordHint;
					Session.Modified = true;
				}
			}
		}

		private void ProjectPlayers_Click(object sender, EventArgs e)
		{
			try
			{
				HeroListForm heroListForm = new HeroListForm();
				heroListForm.ShowDialog();
				this.UpdateView();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ProjectTacticalMaps_Click(object sender, EventArgs e)
		{
			try
			{
				MapListForm mapListForm = new MapListForm();
				mapListForm.ShowDialog();
				this.UpdateView();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ProjectRegionalMaps_Click(object sender, EventArgs e)
		{
			try
			{
				RegionalMapListForm regionalMapListForm = new RegionalMapListForm();
				regionalMapListForm.ShowDialog();
				this.UpdateView();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ProjectParcels_Click(object sender, EventArgs e)
		{
			try
			{
				ParcelListForm parcelListForm = new ParcelListForm();
				parcelListForm.ShowDialog();
				this.UpdateView();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ProjectDecks_Click(object sender, EventArgs e)
		{
			try
			{
				DeckListForm deckListForm = new DeckListForm();
				deckListForm.ShowDialog();
				this.UpdateView();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ProjectCustomCreatures_Click(object sender, EventArgs e)
		{
			try
			{
				CustomCreatureListForm customCreatureListForm = new CustomCreatureListForm();
				customCreatureListForm.ShowDialog();
				this.UpdateView();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ProjectCalendars_Click(object sender, EventArgs e)
		{
			try
			{
				CalendarListForm calendarListForm = new CalendarListForm();
				calendarListForm.ShowDialog();
				this.UpdateView();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ProjectEncounters_Click(object sender, EventArgs e)
		{
			try
			{
				PausedCombatListForm pausedCombatListForm = new PausedCombatListForm();
				pausedCombatListForm.ShowDialog();
				foreach (Form form in Application.OpenForms)
				{
					if (form is CombatForm)
					{
						form.Activate();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsPlayerView_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session.PlayerView == null)
				{
					Session.PlayerView = new PlayerViewForm(this);
					Session.PlayerView.ShowDefault();
				}
				else
				{
					Session.PlayerView.Close();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsPlayerViewSecondary_Click(object sender, EventArgs e)
		{
			try
			{
				PlayerViewForm.UseOtherDisplay = !PlayerViewForm.UseOtherDisplay;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsPlayerViewClear_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session.PlayerView != null)
				{
					Session.PlayerView.ShowDefault();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void TextSizeSmall_Click(object sender, EventArgs e)
		{
			try
			{
				PlayerViewForm.DisplaySize = DisplaySize.Small;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void TextSizeMedium_Click(object sender, EventArgs e)
		{
			try
			{
				PlayerViewForm.DisplaySize = DisplaySize.Medium;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void TextSizeLarge_Click(object sender, EventArgs e)
		{
			try
			{
				PlayerViewForm.DisplaySize = DisplaySize.Large;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsImportProject_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = Program.ProjectFilter;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					GC.Collect();
					Project project = Serialisation<Project>.Load(openFileDialog.FileName, SerialisationMode.Binary);
					if (project != null)
					{
						Session.Project.PopulateProjectLibrary();
						Session.Project.Import(project);
						Session.Project.SimplifyProjectLibrary();
						Session.Modified = true;
						this.PlotView.RecalculateLayout();
						this.UpdateView();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsExportProject_Click(object sender, EventArgs e)
		{
			try
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Filter = Program.HTMLFilter;
				saveFileDialog.FileName = FileName.TrimInvalidCharacters(Session.Project.Name);
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					HTML hTML = new HTML();
					bool flag = hTML.ExportProject(saveFileDialog.FileName);
					if (flag)
					{
						Process.Start(saveFileDialog.FileName);
					}
					else
					{
						string text = "The file could not be saved; check the filename and drive permissions and try again.";
						MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsExportHandout_Click(object sender, EventArgs e)
		{
			try
			{
				HandoutForm handoutForm = new HandoutForm();
				handoutForm.ShowDialog();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsExportLoot_Click(object sender, EventArgs e)
		{
			try
			{
				TreasureListForm treasureListForm = new TreasureListForm(Session.Project.Plot);
				treasureListForm.ShowDialog();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsTileChecklist_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session.Project != null)
				{
					TileChecklistForm tileChecklistForm = new TileChecklistForm();
					tileChecklistForm.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsMiniChecklist_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session.Project != null)
				{
					MiniChecklistForm miniChecklistForm = new MiniChecklistForm();
					miniChecklistForm.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsIssues_Click(object sender, EventArgs e)
		{
			try
			{
				IssuesForm issuesForm = new IssuesForm(Session.Project.Plot);
				issuesForm.ShowDialog();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsPowerStats_Click(object sender, EventArgs e)
		{
			try
			{
				List<ICreature> list = new List<ICreature>();
				List<Creature> creatures = Session.Creatures;
				foreach (ICreature current in creatures)
				{
					list.Add(current);
				}
				if (Session.Project != null)
				{
					foreach (ICreature current2 in Session.Project.CustomCreatures)
					{
						list.Add(current2);
					}
					foreach (ICreature current3 in Session.Project.NPCs)
					{
						list.Add(current3);
					}
				}
				List<CreaturePower> list2 = new List<CreaturePower>();
				foreach (ICreature current4 in list)
				{
					if (current4 != null)
					{
						list2.AddRange(current4.CreaturePowers);
					}
				}
				PowerStatisticsForm powerStatisticsForm = new PowerStatisticsForm(list2, list.Count);
				powerStatisticsForm.ShowDialog();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsLibraries_Click(object sender, EventArgs e)
		{
			try
			{
				LibraryListForm libraryListForm = new LibraryListForm();
				libraryListForm.ShowDialog();
				this.UpdateView();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
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

		private void HelpManual_Click(object sender, EventArgs e)
		{
			try
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				string text = FileName.Directory(entryAssembly.FullName) + "Manual.pdf";
				if (File.Exists(text))
				{
					Process.Start(text);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void HelpFeedback_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("mailto:masterplan@habitualindolence.net?subject=Masterplan Feedback");
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void HelpTutorials_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("http://www.habitualindolence.net/masterplan/tutorials.htm");
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void HelpWebsite_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("http://www.habitualindolence.net/masterplan/");
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void HelpFacebook_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("http://www.facebook.com/masterplanstudio/");
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void HelpTwitter_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("http://www.twitter.com/Masterplan_4E/");
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void HelpAbout_Click(object sender, EventArgs e)
		{
			try
			{
				new AboutBox().ShowDialog();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			try
			{
				this.add_point(null, null);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ContextAdd_Click(object sender, EventArgs e)
		{
			try
			{
				this.add_point(this.PlotView.SelectedPoint, null);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void add_point(PlotPoint lhs, PlotPoint rhs)
		{
			try
			{
				PlotPoint pp = new PlotPoint("New Point");
				PlotPointForm plotPointForm = new PlotPointForm(pp, this.PlotView.Plot, false);
				if (plotPointForm.ShowDialog() == DialogResult.OK)
				{
					this.PlotView.Plot.Points.Add(plotPointForm.PlotPoint);
					this.PlotView.RecalculateLayout();
					if (lhs != null && rhs != null)
					{
						lhs.Links.Remove(rhs.ID);
					}
					if (lhs != null)
					{
						lhs.Links.Add(plotPointForm.PlotPoint.ID);
					}
					if (rhs != null)
					{
						plotPointForm.PlotPoint.Links.Add(rhs.ID);
					}
					Session.Modified = true;
					this.UpdateView();
					this.PlotView.SelectedPoint = plotPointForm.PlotPoint;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void AddEncounter_Click(object sender, EventArgs e)
		{
			try
			{
				Encounter encounter = new Encounter();
				encounter.SetStandardEncounterNotes();
				PlotPointForm plotPointForm = new PlotPointForm(new PlotPoint("New Encounter Point")
				{
					Element = encounter
				}, this.PlotView.Plot, true);
				if (plotPointForm.ShowDialog() == DialogResult.OK)
				{
					this.PlotView.Plot.Points.Add(plotPointForm.PlotPoint);
					this.PlotView.RecalculateLayout();
					Session.Modified = true;
					this.UpdateView();
					this.PlotView.SelectedPoint = plotPointForm.PlotPoint;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void AddChallenge_Click(object sender, EventArgs e)
		{
			try
			{
				SkillChallenge skillChallenge = new SkillChallenge();
				skillChallenge.Name = "Unnamed Skill Challenge";
				skillChallenge.Level = Session.Project.Party.Level;
				PlotPointForm plotPointForm = new PlotPointForm(new PlotPoint("New Skill Challenge Point")
				{
					Element = skillChallenge
				}, this.PlotView.Plot, true);
				if (plotPointForm.ShowDialog() == DialogResult.OK)
				{
					this.PlotView.Plot.Points.Add(plotPointForm.PlotPoint);
					this.PlotView.RecalculateLayout();
					Session.Modified = true;
					this.UpdateView();
					this.PlotView.SelectedPoint = plotPointForm.PlotPoint;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void AddTrap_Click(object sender, EventArgs e)
		{
			try
			{
				TrapElement trapElement = new TrapElement();
				trapElement.Trap.Name = "Unnamed Trap";
				trapElement.Trap.Level = Session.Project.Party.Level;
				PlotPointForm plotPointForm = new PlotPointForm(new PlotPoint("New Trap / Hazard Point")
				{
					Element = trapElement
				}, this.PlotView.Plot, true);
				if (plotPointForm.ShowDialog() == DialogResult.OK)
				{
					this.PlotView.Plot.Points.Add(plotPointForm.PlotPoint);
					this.PlotView.RecalculateLayout();
					Session.Modified = true;
					this.UpdateView();
					this.PlotView.SelectedPoint = plotPointForm.PlotPoint;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void AddQuest_Click(object sender, EventArgs e)
		{
			try
			{
				PlotPointForm plotPointForm = new PlotPointForm(new PlotPoint("New Quest Point")
				{
					Element = new Quest()
				}, this.PlotView.Plot, true);
				if (plotPointForm.ShowDialog() == DialogResult.OK)
				{
					this.PlotView.Plot.Points.Add(plotPointForm.PlotPoint);
					this.PlotView.RecalculateLayout();
					Session.Modified = true;
					this.UpdateView();
					this.PlotView.SelectedPoint = plotPointForm.PlotPoint;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlotView.SelectedPoint != null)
				{
					string text = "Are you sure you want to delete this plot point?";
					DialogResult dialogResult = MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dialogResult != DialogResult.No)
					{
						if (this.PlotView.SelectedPoint.Subplot.Points.Count != 0)
						{
							string text2 = "This plot point has a subplot.";
							text2 += Environment.NewLine;
							text2 += "Do you want to keep the subplot points?";
							dialogResult = MessageBox.Show(text2, "Masterplan", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
							if (dialogResult == DialogResult.Cancel)
							{
								return;
							}
							if (dialogResult == DialogResult.Yes)
							{
								foreach (PlotPoint current in this.PlotView.SelectedPoint.Subplot.Points)
								{
									this.PlotView.Plot.Points.Add(current);
								}
							}
						}
						this.PlotView.Plot.RemovePoint(this.PlotView.SelectedPoint);
						this.PlotView.RecalculateLayout();
						this.PlotView.SelectedPoint = null;
						Session.Modified = true;
						this.UpdateView();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void CutBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlotView.SelectedPoint != null)
				{
					Clipboard.SetData(typeof(PlotPoint).ToString(), this.PlotView.SelectedPoint.Copy());
					this.PlotView.Plot.RemovePoint(this.PlotView.SelectedPoint);
					this.PlotView.RecalculateLayout();
					this.PlotView.SelectedPoint = null;
					Session.Modified = true;
					this.PlotView.Invalidate();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void CopyBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.PlotView.SelectedPoint != null)
				{
					Clipboard.SetData(typeof(PlotPoint).ToString(), this.PlotView.SelectedPoint.Copy());
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PasteBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (Clipboard.ContainsData(typeof(PlotPoint).ToString()))
				{
					PlotPoint plotPoint = Clipboard.GetData(typeof(PlotPoint).ToString()) as PlotPoint;
					if (plotPoint != null)
					{
						if (this.PlotView.Plot.FindPoint(plotPoint.ID) != null)
						{
							plotPoint = plotPoint.Copy();
							plotPoint.Links.Clear();
							plotPoint.ID = Guid.NewGuid();
						}
						List<Guid> list = new List<Guid>();
						foreach (Guid current in plotPoint.Links)
						{
							if (this.PlotView.Plot.FindPoint(current) == null)
							{
								list.Add(current);
							}
						}
						foreach (Guid current2 in list)
						{
							plotPoint.Links.Remove(current2);
						}
						this.PlotView.Plot.Points.Add(plotPoint);
						this.PlotView.RecalculateLayout();
						if (this.PlotView.SelectedPoint != null)
						{
							this.PlotView.SelectedPoint.Links.Add(plotPoint.ID);
						}
						Session.Modified = true;
						this.PlotView.SelectedPoint = plotPoint;
						this.PlotView.Invalidate();
					}
				}
				else if (Clipboard.ContainsText())
				{
					string text = Clipboard.GetText();
					PlotPoint plotPoint2 = new PlotPoint();
					plotPoint2.Name = text.Trim().Substring(0, 12) + "...";
					plotPoint2.Details = text;
					this.PlotView.Plot.Points.Add(plotPoint2);
					this.PlotView.RecalculateLayout();
					if (this.PlotView.SelectedPoint != null)
					{
						this.PlotView.SelectedPoint.Links.Add(plotPoint2.ID);
					}
					Session.Modified = true;
					this.PlotView.SelectedPoint = plotPoint2;
					this.PlotView.Invalidate();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void SearchBtn_Click(object sender, EventArgs e)
		{
			try
			{
				this.WorkspaceSearchBar.Visible = !this.WorkspaceSearchBar.Visible;
				if (!this.WorkspaceSearchBar.Visible)
				{
					this.PlotSearchBox.Text = "";
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void SearchBox_TextChanged(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.Filter = this.PlotSearchBox.Text;
				this.PlotSearchBox.Focus();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ClearBtn_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotSearchBox.Text = "";
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ViewDefault_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.Mode = PlotViewMode.Normal;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ViewHighlighting_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.Mode = PlotViewMode.HighlightSelected;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ViewEncounters_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.Mode = PlotViewMode.HighlightEncounter;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ViewTraps_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.Mode = PlotViewMode.HighlightTrap;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ViewChallenges_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.Mode = PlotViewMode.HighlightChallenge;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ViewQuests_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.Mode = PlotViewMode.HighlightQuest;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ViewParcels_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.Mode = PlotViewMode.HighlightParcel;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ViewLevelling_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.ShowLevels = !this.PlotView.ShowLevels;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ViewTooltips_Click(object sender, EventArgs e)
		{
			try
			{
				this.PlotView.ShowTooltips = !this.PlotView.ShowTooltips;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ViewPreview_Click(object sender, EventArgs e)
		{
			try
			{
				this.PreviewSplitter.Panel2Collapsed = !this.PreviewSplitter.Panel2Collapsed;
				Session.Preferences.ShowPreview = !Session.Preferences.ShowPreview;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ViewNavigation_Click(object sender, EventArgs e)
		{
			try
			{
				this.NavigationSplitter.Panel1Collapsed = !this.NavigationSplitter.Panel1Collapsed;
				Session.Preferences.ShowNavigation = !Session.Preferences.ShowNavigation;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ViewLinks_DropDownOpening(object sender, EventArgs e)
		{
			this.ViewLinksCurved.Checked = (this.PlotView.LinkStyle == PlotViewLinkStyle.Curved);
			this.ViewLinksAngled.Checked = (this.PlotView.LinkStyle == PlotViewLinkStyle.Angled);
			this.ViewLinksStraight.Checked = (this.PlotView.LinkStyle == PlotViewLinkStyle.Straight);
		}

		private void ViewLinksCurved_Click(object sender, EventArgs e)
		{
			this.PlotView.LinkStyle = PlotViewLinkStyle.Curved;
			Session.Preferences.LinkStyle = PlotViewLinkStyle.Curved;
		}

		private void ViewLinksAngled_Click(object sender, EventArgs e)
		{
			this.PlotView.LinkStyle = PlotViewLinkStyle.Angled;
			Session.Preferences.LinkStyle = PlotViewLinkStyle.Angled;
		}

		private void ViewLinksStraight_Click(object sender, EventArgs e)
		{
			this.PlotView.LinkStyle = PlotViewLinkStyle.Straight;
			Session.Preferences.LinkStyle = PlotViewLinkStyle.Straight;
		}

		private void FlowchartPrint_Click(object sender, EventArgs e)
		{
			try
			{
				PrintDialog printDialog = new PrintDialog();
				if (printDialog.ShowDialog() == DialogResult.OK)
				{
					PrintDocument printDocument = new PrintDocument();
					printDocument.DocumentName = Session.Project.Name;
					printDocument.PrinterSettings = printDialog.PrinterSettings;
					printDocument.PrintPage += new PrintPageEventHandler(this.print_page);
					printDocument.Print();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void FlowchartExport_Click(object sender, EventArgs e)
		{
			try
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.FileName = Session.Project.Name;
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
					Bitmap bitmap = Screenshot.Plot(this.PlotView.Plot, new Size(800, 600));
					bitmap.Save(saveFileDialog.FileName, format);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void FlowchartAllXP_Click(object sender, EventArgs e)
		{
			Session.Preferences.AllXP = !Session.Preferences.AllXP;
			this.update_workspace();
			this.update_preview();
		}

		private void PlotAdvancedTreasure_Click(object sender, EventArgs e)
		{
			TreasureListForm treasureListForm = new TreasureListForm(this.PlotView.Plot);
			treasureListForm.ShowDialog();
		}

		private void PlotAdvancedIssues_Click(object sender, EventArgs e)
		{
			IssuesForm issuesForm = new IssuesForm(this.PlotView.Plot);
			issuesForm.ShowDialog();
		}

		private void PlotAdvancedDifficulty_Click(object sender, EventArgs e)
		{
			LevelAdjustmentForm levelAdjustmentForm = new LevelAdjustmentForm();
			if (levelAdjustmentForm.ShowDialog() == DialogResult.OK)
			{
				int levelAdjustment = levelAdjustmentForm.LevelAdjustment;
				List<PlotPoint> allPlotPoints = this.PlotView.Plot.AllPlotPoints;
				foreach (PlotPoint current in allPlotPoints)
				{
					if (current.Element is Encounter)
					{
						Encounter encounter = current.Element as Encounter;
						foreach (EncounterSlot current2 in encounter.Slots)
						{
							current2.Card.LevelAdjustment += levelAdjustment;
						}
						foreach (Trap current3 in encounter.Traps)
						{
							current3.AdjustLevel(levelAdjustment);
						}
						foreach (SkillChallenge current4 in encounter.SkillChallenges)
						{
							current4.Level += levelAdjustment;
							current4.Level = Math.Max(1, current4.Level);
						}
					}
					if (current.Element is Trap)
					{
						Trap trap = current.Element as Trap;
						trap.AdjustLevel(levelAdjustment);
					}
					if (current.Element is SkillChallenge)
					{
						SkillChallenge skillChallenge = current.Element as SkillChallenge;
						skillChallenge.Level += levelAdjustment;
						skillChallenge.Level = Math.Max(1, skillChallenge.Level);
					}
					if (current.Element is Quest)
					{
						Quest quest = current.Element as Quest;
						quest.Level += levelAdjustment;
						quest.Level = Math.Max(1, quest.Level);
					}
				}
				Session.Modified = true;
				this.PlotView.Invalidate();
			}
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			try
			{
				PlotPoint selected_point = this.get_selected_point();
				if (selected_point != null)
				{
					int index = this.PlotView.Plot.Points.IndexOf(selected_point);
					Plot plot = Session.Project.FindParent(selected_point);
					PlotPointForm plotPointForm = new PlotPointForm(selected_point, plot, false);
					if (plotPointForm.ShowDialog() == DialogResult.OK)
					{
						plot.Points[index] = plotPointForm.PlotPoint;
						Session.Modified = true;
						this.set_selected_point(plotPointForm.PlotPoint);
						this.PlotView.RecalculateLayout();
						this.UpdateView();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ExploreBtn_Click(object sender, EventArgs e)
		{
			try
			{
				PlotPoint selected_point = this.get_selected_point();
				if (selected_point != null)
				{
					if (this.fView != MainForm.ViewType.Flowchart)
					{
						this.flowchart_view();
					}
					this.PlotView.Plot = selected_point.Subplot;
					this.UpdateView();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlotPointPlayerView_Click(object sender, EventArgs e)
		{
			try
			{
				PlotPoint selected_point = this.get_selected_point();
				if (selected_point != null)
				{
					if (Session.PlayerView == null)
					{
						Session.PlayerView = new PlayerViewForm(this);
					}
					Session.PlayerView.ShowPlotPoint(selected_point);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlotPointExportHTML_Click(object sender, EventArgs e)
		{
			try
			{
				PlotPoint selected_point = this.get_selected_point();
				if (selected_point != null)
				{
					SaveFileDialog saveFileDialog = new SaveFileDialog();
					saveFileDialog.FileName = selected_point.Name;
					saveFileDialog.Filter = Program.HTMLFilter;
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						int partyLevel = Workspace.GetPartyLevel(selected_point);
						File.WriteAllText(saveFileDialog.FileName, HTML.PlotPoint(selected_point, this.PlotView.Plot, partyLevel, false, this.fView, DisplaySize.Small));
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PlotPointExportFile_Click(object sender, EventArgs e)
		{
			try
			{
				PlotPoint selected_point = this.get_selected_point();
				if (selected_point != null)
				{
					Project project = new Project();
					project.Name = selected_point.Name;
					project.Party.Size = Session.Project.Party.Size;
					project.Party.Level = Workspace.GetPartyLevel(selected_point);
					project.Plot.Points.Add(selected_point.Copy());
					foreach (PlotPoint current in project.AllPlotPoints)
					{
						current.EncyclopediaEntryIDs.Clear();
					}
					List<Guid> list = project.Plot.FindTacticalMaps();
					foreach (Guid current2 in list)
					{
						Map map = Session.Project.FindTacticalMap(current2);
						if (map != null)
						{
							project.Maps.Add(map.Copy());
						}
					}
					List<Guid> list2 = project.Plot.FindRegionalMaps();
					foreach (Guid current3 in list2)
					{
						RegionalMap regionalMap = Session.Project.FindRegionalMap(current3);
						if (regionalMap != null)
						{
							project.RegionalMaps.Add(regionalMap.Copy());
						}
					}
					GC.Collect();
					project.PopulateProjectLibrary();
					SaveFileDialog saveFileDialog = new SaveFileDialog();
					saveFileDialog.FileName = selected_point.Name;
					saveFileDialog.Filter = Program.ProjectFilter;
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						Serialisation<Project>.Save(saveFileDialog.FileName, project, SerialisationMode.Binary);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void BackgroundAddBtn_Click(object sender, EventArgs e)
		{
			try
			{
				Background bg = new Background("New Background Item");
				BackgroundForm backgroundForm = new BackgroundForm(bg);
				if (backgroundForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.Backgrounds.Add(backgroundForm.Background);
					Session.Modified = true;
					this.update_background_list();
					this.SelectedBackground = backgroundForm.Background;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void BackgroundRemoveBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedBackground != null)
				{
					string text = "Are you sure you want to delete this background?";
					DialogResult dialogResult = MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dialogResult != DialogResult.No)
					{
						Session.Project.Backgrounds.Remove(this.SelectedBackground);
						Session.Modified = true;
						this.update_background_list();
						this.SelectedBackground = null;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void BackgroundEditBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedBackground != null)
				{
					int index = Session.Project.Backgrounds.IndexOf(this.SelectedBackground);
					BackgroundForm backgroundForm = new BackgroundForm(this.SelectedBackground);
					if (backgroundForm.ShowDialog() == DialogResult.OK)
					{
						Session.Project.Backgrounds[index] = backgroundForm.Background;
						Session.Modified = true;
						this.update_background_list();
						this.SelectedBackground = backgroundForm.Background;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void BackgroundUpBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedBackground != null && Session.Project.Backgrounds.IndexOf(this.SelectedBackground) != 0)
				{
					int num = Session.Project.Backgrounds.IndexOf(this.SelectedBackground);
					Background value = Session.Project.Backgrounds[num - 1];
					Session.Project.Backgrounds[num - 1] = this.SelectedBackground;
					Session.Project.Backgrounds[num] = value;
					Session.Modified = true;
					this.update_background_list();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void BackgroundDownBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedBackground != null && Session.Project.Backgrounds.IndexOf(this.SelectedBackground) != Session.Project.Backgrounds.Count - 1)
				{
					int num = Session.Project.Backgrounds.IndexOf(this.SelectedBackground);
					Background value = Session.Project.Backgrounds[num + 1];
					Session.Project.Backgrounds[num + 1] = this.SelectedBackground;
					Session.Project.Backgrounds[num] = value;
					Session.Modified = true;
					this.update_background_list();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void BackgroundPlayerViewSelected_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedBackground != null)
				{
					if (Session.PlayerView == null)
					{
						Session.PlayerView = new PlayerViewForm(this);
					}
					Session.PlayerView.ShowBackground(this.SelectedBackground);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void BackgroundPlayerViewAll_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session.PlayerView == null)
				{
					Session.PlayerView = new PlayerViewForm(this);
				}
				Session.PlayerView.ShowBackground(Session.Project.Backgrounds);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void BackgroundShareExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = Program.BackgroundFilter;
			saveFileDialog.FileName = Session.Project.Name;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				Serialisation<List<Background>>.Save(saveFileDialog.FileName, Session.Project.Backgrounds, SerialisationMode.XML);
			}
		}

		private void BackgroundShareImport_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = Program.BackgroundFilter;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				List<Background> collection = Serialisation<List<Background>>.Load(openFileDialog.FileName, SerialisationMode.XML);
				Session.Project.Backgrounds.AddRange(collection);
				Session.Modified = true;
				this.UpdateView();
			}
		}

		private void BackgroundSharePublish_Click(object sender, EventArgs e)
		{
			HandoutForm handoutForm = new HandoutForm();
			handoutForm.AddBackgroundEntries();
			handoutForm.ShowDialog();
		}

		private void EncAddEntry_Click(object sender, EventArgs e)
		{
			try
			{
				EncyclopediaEntry encyclopediaEntry = this.create_entry("New Entry", "");
				if (encyclopediaEntry != null)
				{
					this.UpdateView();
					this.SelectedEncyclopediaItem = encyclopediaEntry;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private EncyclopediaEntry create_entry(string name, string content)
		{
			try
			{
				EncyclopediaEntryForm encyclopediaEntryForm = new EncyclopediaEntryForm(new EncyclopediaEntry
				{
					Name = name,
					Details = content
				});
				if (encyclopediaEntryForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.Encyclopedia.Entries.Add(encyclopediaEntryForm.Entry);
					Session.Project.Encyclopedia.Entries.Sort();
					Session.Modified = true;
					return encyclopediaEntryForm.Entry;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return null;
		}

		private void encyclopedia_template(object sender, EventArgs e)
		{
			try
			{
				if (sender is ToolStripMenuItem)
				{
					ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
					string text = toolStripMenuItem.Tag as string;
					string name = FileName.Name(text);
					string content = File.ReadAllText(text);
					EncyclopediaEntry encyclopediaEntry = this.create_entry(name, content);
					if (encyclopediaEntry != null)
					{
						this.UpdateView();
						this.SelectedEncyclopediaItem = encyclopediaEntry;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void EncAddGroup_Click(object sender, EventArgs e)
		{
			try
			{
				EncyclopediaGroup encyclopediaGroup = new EncyclopediaGroup();
				encyclopediaGroup.Name = "New Group";
				EncyclopediaGroupForm encyclopediaGroupForm = new EncyclopediaGroupForm(encyclopediaGroup);
				if (encyclopediaGroupForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.Encyclopedia.Groups.Add(encyclopediaGroupForm.Group);
					Session.Modified = true;
					this.UpdateView();
					this.SelectedEncyclopediaItem = encyclopediaGroup;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void EncRemoveBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedEncyclopediaItem != null && this.SelectedEncyclopediaItem is EncyclopediaEntry)
				{
					string text = "Are you sure you want to delete this encyclopedia entry?";
					DialogResult dialogResult = MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dialogResult == DialogResult.No)
					{
						return;
					}
					Session.Project.Encyclopedia.Entries.Remove(this.SelectedEncyclopediaItem as EncyclopediaEntry);
					List<EncyclopediaLink> list = new List<EncyclopediaLink>();
					foreach (EncyclopediaLink current in Session.Project.Encyclopedia.Links)
					{
						if (current.EntryIDs.Contains(this.SelectedEncyclopediaItem.ID))
						{
							list.Add(current);
						}
					}
					foreach (EncyclopediaLink current2 in list)
					{
						Session.Project.Encyclopedia.Links.Remove(current2);
					}
					foreach (EncyclopediaGroup current3 in Session.Project.Encyclopedia.Groups)
					{
						if (current3.EntryIDs.Contains(this.SelectedEncyclopediaItem.ID))
						{
							current3.EntryIDs.Remove(this.SelectedEncyclopediaItem.ID);
						}
					}
					Session.Modified = true;
					this.update_encyclopedia_list();
					this.SelectedEncyclopediaItem = null;
				}
				if (this.SelectedEncyclopediaItem != null && this.SelectedEncyclopediaItem is EncyclopediaGroup)
				{
					string text2 = "Are you sure you want to delete this encyclopedia group?";
					DialogResult dialogResult2 = MessageBox.Show(text2, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dialogResult2 != DialogResult.No)
					{
						Session.Project.Encyclopedia.Groups.Remove(this.SelectedEncyclopediaItem as EncyclopediaGroup);
						this.UpdateView();
						this.SelectedEncyclopediaItem = null;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void EncEditBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedEncyclopediaItem != null && this.SelectedEncyclopediaItem is EncyclopediaEntry)
				{
					int index = Session.Project.Encyclopedia.Entries.IndexOf(this.SelectedEncyclopediaItem as EncyclopediaEntry);
					EncyclopediaEntryForm encyclopediaEntryForm = new EncyclopediaEntryForm(this.SelectedEncyclopediaItem as EncyclopediaEntry);
					if (encyclopediaEntryForm.ShowDialog() == DialogResult.OK)
					{
						Session.Project.Encyclopedia.Entries[index] = encyclopediaEntryForm.Entry;
						Session.Modified = true;
						this.UpdateView();
						this.SelectedEncyclopediaItem = encyclopediaEntryForm.Entry;
					}
				}
				if (this.SelectedEncyclopediaItem != null && this.SelectedEncyclopediaItem is EncyclopediaGroup)
				{
					int index2 = Session.Project.Encyclopedia.Groups.IndexOf(this.SelectedEncyclopediaItem as EncyclopediaGroup);
					EncyclopediaGroupForm encyclopediaGroupForm = new EncyclopediaGroupForm(this.SelectedEncyclopediaItem as EncyclopediaGroup);
					if (encyclopediaGroupForm.ShowDialog() == DialogResult.OK)
					{
						Session.Project.Encyclopedia.Groups[index2] = encyclopediaGroupForm.Group;
						Session.Modified = true;
						this.UpdateView();
						this.SelectedEncyclopediaItem = encyclopediaGroupForm.Group;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void EncPlayerView_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedEncyclopediaItem != null)
				{
					if (Session.PlayerView == null)
					{
						Session.PlayerView = new PlayerViewForm(this);
					}
					Session.PlayerView.ShowEncyclopediaItem(this.SelectedEncyclopediaItem);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void EncShareExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = Program.EncyclopediaFilter;
			saveFileDialog.FileName = Session.Project.Name;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				Serialisation<Encyclopedia>.Save(saveFileDialog.FileName, Session.Project.Encyclopedia, SerialisationMode.XML);
			}
		}

		private void EncShareImport_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = Program.EncyclopediaFilter;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Encyclopedia enc = Serialisation<Encyclopedia>.Load(openFileDialog.FileName, SerialisationMode.XML);
				Session.Project.Encyclopedia.Import(enc);
				Session.Modified = true;
				this.UpdateView();
			}
		}

		private void EncSharePublish_Click(object sender, EventArgs e)
		{
			HandoutForm handoutForm = new HandoutForm();
			handoutForm.AddEncyclopediaEntries();
			handoutForm.ShowDialog();
		}

		private void EncCutBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedEncyclopediaItem != null && this.SelectedEncyclopediaItem is EncyclopediaEntry)
				{
					EncyclopediaEntry encyclopediaEntry = this.SelectedEncyclopediaItem as EncyclopediaEntry;
					Clipboard.SetData(typeof(EncyclopediaEntry).ToString(), encyclopediaEntry.Copy());
					Session.Project.Encyclopedia.Entries.Remove(encyclopediaEntry);
					Session.Modified = true;
					this.update_encyclopedia_list();
					this.SelectedEncyclopediaItem = null;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void EncCopyBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedEncyclopediaItem != null && this.SelectedEncyclopediaItem is EncyclopediaEntry)
				{
					EncyclopediaEntry encyclopediaEntry = this.SelectedEncyclopediaItem as EncyclopediaEntry;
					Clipboard.SetData(typeof(EncyclopediaEntry).ToString(), encyclopediaEntry.Copy());
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void EncPasteBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (Clipboard.ContainsData(typeof(EncyclopediaEntry).ToString()))
				{
					EncyclopediaEntry encyclopediaEntry = Clipboard.GetData(typeof(EncyclopediaEntry).ToString()) as EncyclopediaEntry;
					if (encyclopediaEntry != null)
					{
						if (Session.Project.Encyclopedia.FindEntry(encyclopediaEntry.ID) != null)
						{
							Guid iD = encyclopediaEntry.ID;
							encyclopediaEntry.ID = Guid.NewGuid();
							List<EncyclopediaLink> list = new List<EncyclopediaLink>();
							foreach (EncyclopediaLink current in Session.Project.Encyclopedia.Links)
							{
								if (current.EntryIDs.Contains(iD))
								{
									EncyclopediaLink encyclopediaLink = current.Copy();
									int index = encyclopediaLink.EntryIDs.IndexOf(iD);
									encyclopediaLink.EntryIDs[index] = encyclopediaEntry.ID;
									list.Add(encyclopediaLink);
								}
							}
							Session.Project.Encyclopedia.Links.AddRange(list);
						}
						Session.Project.Encyclopedia.Entries.Add(encyclopediaEntry);
						Session.Modified = true;
						this.update_encyclopedia_list();
						this.SelectedEncyclopediaItem = encyclopediaEntry;
					}
				}
				else if (Clipboard.ContainsText())
				{
					string text = Clipboard.GetText();
					EncyclopediaEntry encyclopediaEntry2 = new EncyclopediaEntry();
					encyclopediaEntry2.Name = text.Trim().Substring(0, 12) + "...";
					encyclopediaEntry2.Details = text;
					Session.Project.Encyclopedia.Entries.Add(encyclopediaEntry2);
					Session.Modified = true;
					this.update_encyclopedia_list();
					this.SelectedEncyclopediaItem = encyclopediaEntry2;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void EncSearchBox_TextChanged(object sender, EventArgs e)
		{
			try
			{
				this.update_encyclopedia_list();
				if (this.EntryList.Items.Count != 0)
				{
					this.SelectedEncyclopediaItem = (this.EntryList.Items[0].Tag as EncyclopediaEntry);
				}
				else
				{
					this.SelectedEncyclopediaItem = null;
				}
				this.EncSearchBox.Focus();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void EncClearLbl_Click(object sender, EventArgs e)
		{
			try
			{
				this.EncSearchBox.Text = "";
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void EntryImageList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedEncyclopediaImage != null)
			{
				EncyclopediaEntry encyclopediaEntry = this.SelectedEncyclopediaItem as EncyclopediaEntry;
				if (encyclopediaEntry == null)
				{
					return;
				}
				int index = encyclopediaEntry.Images.IndexOf(this.SelectedEncyclopediaImage);
				EncyclopediaImageForm encyclopediaImageForm = new EncyclopediaImageForm(this.SelectedEncyclopediaImage);
				if (encyclopediaImageForm.ShowDialog() == DialogResult.OK)
				{
					encyclopediaEntry.Images[index] = encyclopediaImageForm.Image;
					this.update_encyclopedia_images();
					Session.Modified = true;
				}
			}
		}

		private void AddRace_Click(object sender, EventArgs e)
		{
			OptionRaceForm optionRaceForm = new OptionRaceForm(new Race
			{
				Name = "New Race"
			});
			if (optionRaceForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionRaceForm.Race);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddClass_Click(object sender, EventArgs e)
		{
			Class @class = new Class();
			@class.Name = "New Class";
			for (int i = 1; i <= 30; i++)
			{
				LevelData levelData = new LevelData();
				levelData.Level = i;
				@class.Levels.Add(levelData);
			}
			OptionClassForm optionClassForm = new OptionClassForm(@class);
			if (optionClassForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionClassForm.Class);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddTheme_Click(object sender, EventArgs e)
		{
			Theme theme = new Theme();
			theme.Name = "New Theme";
			for (int i = 1; i <= 10; i++)
			{
				LevelData levelData = new LevelData();
				levelData.Level = i;
				theme.Levels.Add(levelData);
			}
			OptionThemeForm optionThemeForm = new OptionThemeForm(theme);
			if (optionThemeForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionThemeForm.Theme);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddParagonPath_Click(object sender, EventArgs e)
		{
			ParagonPath paragonPath = new ParagonPath();
			paragonPath.Name = "New Paragon Path";
			for (int i = 11; i <= 20; i++)
			{
				LevelData levelData = new LevelData();
				levelData.Level = i;
				paragonPath.Levels.Add(levelData);
			}
			OptionParagonPathForm optionParagonPathForm = new OptionParagonPathForm(paragonPath);
			if (optionParagonPathForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionParagonPathForm.ParagonPath);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddEpicDestiny_Click(object sender, EventArgs e)
		{
			EpicDestiny epicDestiny = new EpicDestiny();
			epicDestiny.Name = "New Epic Destiny";
			for (int i = 21; i <= 30; i++)
			{
				LevelData levelData = new LevelData();
				levelData.Level = i;
				epicDestiny.Levels.Add(levelData);
			}
			OptionEpicDestinyForm optionEpicDestinyForm = new OptionEpicDestinyForm(epicDestiny);
			if (optionEpicDestinyForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionEpicDestinyForm.EpicDestiny);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddBackground_Click(object sender, EventArgs e)
		{
			OptionBackgroundForm optionBackgroundForm = new OptionBackgroundForm(new PlayerBackground
			{
				Name = "New Background"
			});
			if (optionBackgroundForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionBackgroundForm.Background);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddFeat_Click(object sender, EventArgs e)
		{
			OptionFeatForm optionFeatForm = new OptionFeatForm(new Feat
			{
				Name = "New Feat"
			});
			if (optionFeatForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionFeatForm.Feat);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddWeapon_Click(object sender, EventArgs e)
		{
			OptionWeaponForm optionWeaponForm = new OptionWeaponForm(new Weapon
			{
				Name = "New Weapon"
			});
			if (optionWeaponForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionWeaponForm.Weapon);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddRitual_Click(object sender, EventArgs e)
		{
			OptionRitualForm optionRitualForm = new OptionRitualForm(new Ritual
			{
				Name = "New Ritual"
			});
			if (optionRitualForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionRitualForm.Ritual);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddCreatureLore_Click(object sender, EventArgs e)
		{
			OptionCreatureLoreForm optionCreatureLoreForm = new OptionCreatureLoreForm(new CreatureLore
			{
				Name = "Creature",
				SkillName = "Nature"
			});
			if (optionCreatureLoreForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionCreatureLoreForm.CreatureLore);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddDisease_Click(object sender, EventArgs e)
		{
			OptionDiseaseForm optionDiseaseForm = new OptionDiseaseForm(new Disease
			{
				Name = "New Disease"
			});
			if (optionDiseaseForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionDiseaseForm.Disease);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void AddPoison_Click(object sender, EventArgs e)
		{
			OptionPoisonForm optionPoisonForm = new OptionPoisonForm(new Poison
			{
				Name = "New Poison"
			});
			if (optionPoisonForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.PlayerOptions.Add(optionPoisonForm.Poison);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void RulesRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedRule != null)
			{
				Session.Project.PlayerOptions.Remove(this.SelectedRule);
				Session.Modified = true;
				this.update_rules_list();
				this.update_selected_rule();
			}
		}

		private void RulesEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedRule == null)
			{
				return;
			}
			int index = Session.Project.PlayerOptions.IndexOf(this.SelectedRule);
			if (this.SelectedRule is Race)
			{
				OptionRaceForm optionRaceForm = new OptionRaceForm(this.SelectedRule as Race);
				if (optionRaceForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.PlayerOptions[index] = optionRaceForm.Race;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is Class)
			{
				OptionClassForm optionClassForm = new OptionClassForm(this.SelectedRule as Class);
				if (optionClassForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.PlayerOptions[index] = optionClassForm.Class;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is Theme)
			{
				OptionThemeForm optionThemeForm = new OptionThemeForm(this.SelectedRule as Theme);
				if (optionThemeForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.PlayerOptions[index] = optionThemeForm.Theme;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is ParagonPath)
			{
				OptionParagonPathForm optionParagonPathForm = new OptionParagonPathForm(this.SelectedRule as ParagonPath);
				if (optionParagonPathForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.PlayerOptions[index] = optionParagonPathForm.ParagonPath;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is EpicDestiny)
			{
				OptionEpicDestinyForm optionEpicDestinyForm = new OptionEpicDestinyForm(this.SelectedRule as EpicDestiny);
				if (optionEpicDestinyForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.PlayerOptions[index] = optionEpicDestinyForm.EpicDestiny;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is PlayerBackground)
			{
				OptionBackgroundForm optionBackgroundForm = new OptionBackgroundForm(this.SelectedRule as PlayerBackground);
				if (optionBackgroundForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.PlayerOptions[index] = optionBackgroundForm.Background;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is Feat)
			{
				OptionFeatForm optionFeatForm = new OptionFeatForm(this.SelectedRule as Feat);
				if (optionFeatForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.PlayerOptions[index] = optionFeatForm.Feat;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is Weapon)
			{
				OptionWeaponForm optionWeaponForm = new OptionWeaponForm(this.SelectedRule as Weapon);
				if (optionWeaponForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.PlayerOptions[index] = optionWeaponForm.Weapon;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is Ritual)
			{
				OptionRitualForm optionRitualForm = new OptionRitualForm(this.SelectedRule as Ritual);
				if (optionRitualForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.PlayerOptions[index] = optionRitualForm.Ritual;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is CreatureLore)
			{
				OptionCreatureLoreForm optionCreatureLoreForm = new OptionCreatureLoreForm(this.SelectedRule as CreatureLore);
				if (optionCreatureLoreForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.PlayerOptions[index] = optionCreatureLoreForm.CreatureLore;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is Disease)
			{
				OptionDiseaseForm optionDiseaseForm = new OptionDiseaseForm(this.SelectedRule as Disease);
				if (optionDiseaseForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.PlayerOptions[index] = optionDiseaseForm.Disease;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
			if (this.SelectedRule is Poison)
			{
				OptionPoisonForm optionPoisonForm = new OptionPoisonForm(this.SelectedRule as Poison);
				if (optionPoisonForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.PlayerOptions[index] = optionPoisonForm.Poison;
					Session.Modified = true;
					this.update_rules_list();
					this.update_selected_rule();
				}
			}
		}

		private void RulesPlayerViewBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedRule != null)
			{
				if (Session.PlayerView == null)
				{
					Session.PlayerView = new PlayerViewForm(this);
				}
				Session.PlayerView.ShowPlayerOption(this.SelectedRule);
			}
		}

		private void RuleEncyclopediaBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedRule == null)
			{
				return;
			}
			EncyclopediaEntry encyclopediaEntry = Session.Project.Encyclopedia.FindEntryForAttachment(this.SelectedRule.ID);
			if (encyclopediaEntry == null)
			{
				string text = "There is no encyclopedia entry associated with this item.";
				text += Environment.NewLine;
				text += "Would you like to create one now?";
				if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					return;
				}
				encyclopediaEntry = new EncyclopediaEntry();
				encyclopediaEntry.Name = this.SelectedRule.Name;
				encyclopediaEntry.AttachmentID = this.SelectedRule.ID;
				encyclopediaEntry.Category = "";
				Session.Project.Encyclopedia.Entries.Add(encyclopediaEntry);
				Session.Modified = true;
			}
			int index = Session.Project.Encyclopedia.Entries.IndexOf(encyclopediaEntry);
			EncyclopediaEntryForm encyclopediaEntryForm = new EncyclopediaEntryForm(encyclopediaEntry);
			if (encyclopediaEntryForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.Encyclopedia.Entries[index] = encyclopediaEntryForm.Entry;
				Session.Modified = true;
				this.UpdateView();
				this.Pages.SelectedTab = this.EncyclopediaPage;
				this.SelectedEncyclopediaItem = encyclopediaEntryForm.Entry;
			}
		}

		private void RulesShareExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = Program.RulesFilter;
			saveFileDialog.FileName = Session.Project.Name;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				Serialisation<List<IPlayerOption>>.Save(saveFileDialog.FileName, Session.Project.PlayerOptions, SerialisationMode.Binary);
			}
		}

		private void RulesShareImport_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = Program.RulesFilter;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				List<IPlayerOption> collection = Serialisation<List<IPlayerOption>>.Load(openFileDialog.FileName, SerialisationMode.Binary);
				Session.Project.PlayerOptions.AddRange(collection);
				this.UpdateView();
			}
		}

		private void RulesSharePublish_Click(object sender, EventArgs e)
		{
			HandoutForm handoutForm = new HandoutForm();
			handoutForm.AddRulesEntries();
			handoutForm.ShowDialog();
		}

		private void AttachmentImportBtn_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "All Files|*.*";
				openFileDialog.Multiselect = true;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < fileNames.Length; i++)
					{
						string filename = fileNames[i];
						this.add_attachment(filename);
					}
					this.update_attachments();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void AttachmentRemoveBtn_Click(object sender, EventArgs e)
		{
			try
			{
				List<Attachment> selectedAttachments = this.SelectedAttachments;
				if (selectedAttachments.Count != 0)
				{
					string text = "You are about to remove one or more attachments from this project.";
					text += Environment.NewLine;
					text += "Are you sure you want to do this?";
					if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						foreach (Attachment current in selectedAttachments)
						{
							Session.Project.Attachments.Remove(current);
						}
						Session.Modified = true;
						this.update_attachments();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void AttachmentExtractSimple_Click(object sender, EventArgs e)
		{
			try
			{
				List<Attachment> selectedAttachments = this.SelectedAttachments;
				foreach (Attachment current in selectedAttachments)
				{
					this.extract_attachment(current, false);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void AttachmentExtractAndRun_Click(object sender, EventArgs e)
		{
			try
			{
				List<Attachment> selectedAttachments = this.SelectedAttachments;
				foreach (Attachment current in selectedAttachments)
				{
					this.extract_attachment(current, true);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void extract_attachment(Attachment att, bool run)
		{
			try
			{
				string text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				if (!text.EndsWith("\\"))
				{
					text += "\\";
				}
				string text2 = text + att.Name;
				int num = 1;
				string text3 = text2;
				while (File.Exists(text3))
				{
					num++;
					text3 = string.Concat(new object[]
					{
						text,
						FileName.Name(text2),
						" ",
						num,
						".",
						FileName.Extension(text2)
					});
				}
				File.WriteAllBytes(text3, att.Contents);
				if (run)
				{
					Process.Start(text3);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void AttachmentSendBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedAttachments.Count == 1)
				{
					Attachment attachment = this.SelectedAttachments[0];
					if (attachment.Type != AttachmentType.Miscellaneous)
					{
						if (Session.PlayerView == null)
						{
							Session.PlayerView = new PlayerViewForm(this);
						}
						if (attachment.Type == AttachmentType.PlainText)
						{
							Session.PlayerView.ShowPlainText(attachment);
						}
						if (attachment.Type == AttachmentType.RichText)
						{
							Session.PlayerView.ShowRichText(attachment);
						}
						if (attachment.Type == AttachmentType.Image)
						{
							Session.PlayerView.ShowImage(attachment);
						}
						if (attachment.Type == AttachmentType.URL)
						{
							Session.PlayerView.ShowWebPage(attachment);
						}
						if (attachment.Type == AttachmentType.HTML)
						{
							Session.PlayerView.ShowWebPage(attachment);
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void NoteAddBtn_Click(object sender, EventArgs e)
		{
			try
			{
				Note note = new Note();
				Session.Project.Notes.Add(note);
				Session.Modified = true;
				this.update_notes();
				this.SelectedNote = note;
				this.NoteBox.Focus();
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
					string text = "Are you sure you want to delete this note?";
					DialogResult dialogResult = MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dialogResult != DialogResult.No)
					{
						Session.Project.Notes.Remove(this.SelectedNote);
						Session.Modified = true;
						this.update_notes();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void NoteCategoryBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedNote == null)
			{
				return;
			}
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (Note current in Session.Project.Notes)
			{
				if (current.Category != "")
				{
					binarySearchTree.Add(current.Category);
				}
			}
			CategoryForm categoryForm = new CategoryForm(binarySearchTree.SortedList, this.SelectedNote.Category);
			if (categoryForm.ShowDialog() == DialogResult.OK)
			{
				this.SelectedNote.Category = categoryForm.Category;
				Session.Modified = true;
				this.update_notes();
			}
		}

		private void NoteCutBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedNote != null)
				{
					Clipboard.SetData(typeof(Note).ToString(), this.SelectedNote.Copy());
					Session.Project.Notes.Remove(this.SelectedNote);
					Session.Modified = true;
					this.update_notes();
					this.SelectedNote = null;
				}
				else if (this.NoteBox.SelectedText != "")
				{
					this.NoteBox.Cut();
					Session.Modified = true;
					this.update_notes();
					this.SelectedNote = null;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void NoteCopyBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.SelectedNote != null)
				{
					Clipboard.SetData(typeof(Note).ToString(), this.SelectedNote.Copy());
				}
				else if (this.NoteBox.SelectedText != "")
				{
					this.NoteBox.Copy();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void NotePasteBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (Clipboard.ContainsData(typeof(Note).ToString()))
				{
					Note note = Clipboard.GetData(typeof(Note).ToString()) as Note;
					if (note != null)
					{
						if (Session.Project.FindNote(note.ID) != null)
						{
							note.ID = Guid.NewGuid();
						}
						Session.Project.Notes.Add(note);
						Session.Modified = true;
						this.update_notes();
						this.SelectedNote = note;
					}
				}
				else if (Clipboard.ContainsText())
				{
					Clipboard.GetText();
					if (this.NoteBox.Focused && this.SelectedNote != null)
					{
						this.NoteBox.Paste();
						Session.Modified = true;
						this.update_notes();
						this.NoteBox.Focus();
					}
					else
					{
						Note note2 = new Note();
						note2.Content = Clipboard.GetText();
						Session.Project.Notes.Add(note2);
						Session.Modified = true;
						this.update_notes();
						this.SelectedNote = note2;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void NoteSearchBox_TextChanged(object sender, EventArgs e)
		{
			try
			{
				this.update_notes();
				if (this.NoteList.Groups[1].Items.Count != 0)
				{
					Note selectedNote = this.NoteList.Groups[1].Items[0].Tag as Note;
					this.SelectedNote = selectedNote;
				}
				else
				{
					this.SelectedNote = null;
				}
				this.NoteSearchBox.Focus();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void NoteClearLbl_Click(object sender, EventArgs e)
		{
			try
			{
				this.NoteSearchBox.Text = "";
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void DieRollerBtn_Click(object sender, EventArgs e)
		{
			DieRollerForm dieRollerForm = new DieRollerForm();
			dieRollerForm.ShowDialog();
		}

		private void ElfNameBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<H3>Elvish Names</H3>");
			head.Add("<P class=instruction>Click on any name to create an encyclopedia entry for it.</P>");
			head.Add("<P class=table>");
			head.Add("<TABLE>");
			head.Add("<TR class=heading>");
			head.Add("<TD><B>Male</B></TD>");
			head.Add("<TD><B>Female</B></TD>");
			head.Add("</TR>");
			for (int num = 0; num != 10; num++)
			{
				string text = ElfName.MaleName();
				string text2 = ElfName.FemaleName();
				head.Add("<TR>");
				head.Add("<TD>");
				head.Add(string.Concat(new string[]
				{
					"<P><A href=entry:",
					text.Replace(" ", "%20"),
					">",
					text,
					"</A></P>"
				}));
				head.Add("</TD>");
				head.Add("<TD>");
				head.Add(string.Concat(new string[]
				{
					"<P><A href=entry:",
					text2.Replace(" ", "%20"),
					">",
					text2,
					"</A></P>"
				}));
				head.Add("</TD>");
				head.Add("</TR>");
			}
			head.Add("</TABLE>");
			head.Add("</P>");
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void DwarfNameBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<H3>Dwarvish Names</H3>");
			head.Add("<P class=instruction>Click on any name to create an encyclopedia entry for it.</P>");
			head.Add("<P class=table>");
			head.Add("<TABLE>");
			head.Add("<TR class=heading>");
			head.Add("<TD><B>Male</B></TD>");
			head.Add("<TD><B>Female</B></TD>");
			head.Add("</TR>");
			for (int num = 0; num != 10; num++)
			{
				string text = DwarfName.MaleName();
				string text2 = DwarfName.FemaleName();
				head.Add("<TR>");
				head.Add("<TD>");
				head.Add(string.Concat(new string[]
				{
					"<P><A href=entry:",
					text.Replace(" ", "%20"),
					">",
					text,
					"</A></P>"
				}));
				head.Add("</TD>");
				head.Add("<TD>");
				head.Add(string.Concat(new string[]
				{
					"<P><A href=entry:",
					text2.Replace(" ", "%20"),
					">",
					text2,
					"</A></P>"
				}));
				head.Add("</TD>");
				head.Add("</TR>");
			}
			head.Add("</TABLE>");
			head.Add("</P>");
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void HalflingNameBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<H3>Halfling Names</H3>");
			head.Add("<P class=instruction>Click on any name to create an encyclopedia entry for it.</P>");
			head.Add("<P class=table>");
			head.Add("<TABLE>");
			head.Add("<TR class=heading>");
			head.Add("<TD><B>Male</B></TD>");
			head.Add("<TD><B>Female</B></TD>");
			head.Add("</TR>");
			for (int num = 0; num != 10; num++)
			{
				string text = HalflingName.MaleName();
				string text2 = HalflingName.FemaleName();
				head.Add("<TR>");
				head.Add("<TD>");
				head.Add(string.Concat(new string[]
				{
					"<P><A href=entry:",
					text.Replace(" ", "%20"),
					">",
					text,
					"</A></P>"
				}));
				head.Add("</TD>");
				head.Add("<TD>");
				head.Add(string.Concat(new string[]
				{
					"<P><A href=entry:",
					text2.Replace(" ", "%20"),
					">",
					text2,
					"</A></P>"
				}));
				head.Add("</TD>");
				head.Add("</TR>");
			}
			head.Add("</TABLE>");
			head.Add("</P>");
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void ExoticNameBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<H3>Exotic Names</H3>");
			head.Add("<P class=instruction>Click on any name to create an encyclopedia entry for it.</P>");
			head.Add("<P class=table>");
			head.Add("<TABLE>");
			head.Add("<TR class=heading>");
			head.Add("<TD colspan=2><B>Names</B></TD>");
			head.Add("</TR>");
			for (int num = 0; num != 10; num++)
			{
				string text = ExoticName.FullName();
				string text2 = ExoticName.FullName();
				head.Add("<TR>");
				head.Add("<TD>");
				head.Add(string.Concat(new string[]
				{
					"<P><A href=entry:",
					text.Replace(" ", "%20"),
					">",
					text,
					"</A></P>"
				}));
				head.Add("</TD>");
				head.Add("<TD>");
				head.Add(string.Concat(new string[]
				{
					"<P><A href=entry:",
					text2.Replace(" ", "%20"),
					">",
					text2,
					"</A></P>"
				}));
				head.Add("</TD>");
				head.Add("</TR>");
			}
			head.Add("</TABLE>");
			head.Add("</P>");
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void TreasureBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<H3>Art Objects</H3>");
			head.Add("<P class=instruction>Click on any item to make it available as a treasure parcel.</P>");
			head.Add("<P class=table>");
			head.Add("<TABLE>");
			head.Add("<TR class=heading>");
			head.Add("<TD><B>Items</B></TD>");
			head.Add("</TR>");
			for (int num = 0; num != 10; num++)
			{
				string text = Treasure.ArtObject();
				head.Add("<TR>");
				head.Add("<TD>");
				head.Add(string.Concat(new string[]
				{
					"<P><A href=parcel:",
					text.Replace(" ", "%20"),
					">",
					text,
					"</A></P>"
				}));
				head.Add("</TD>");
				head.Add("</TR>");
			}
			head.Add("</TABLE>");
			head.Add("</P>");
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void BookTitleBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<H3>Book Titles</H3>");
			for (int num = 0; num != 10; num++)
			{
				head.Add("<P>" + Book.Title() + "</P>");
			}
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void PotionBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<H3>Potions</H3>");
			for (int num = 0; num != 10; num++)
			{
				head.Add("<P>" + Potion.Description() + "</P>");
			}
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void NPCBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<H3>NPC Description</H3>");
			head.Add("<P>" + NPCBuilder.Description() + "</P>");
			head.Add("<P class=table>");
			head.Add("<TABLE>");
			head.Add("<TR class=heading>");
			head.Add("<TD colspan=3>");
			head.Add("<B>NPC Details</B>");
			head.Add("</TD>");
			head.Add("</TR>");
			string text = NPCBuilder.Physical();
			if (text != "")
			{
				head.Add("<TR>");
				head.Add("<TD>");
				head.Add("<B>Physical Traits</B>");
				head.Add("</TD>");
				head.Add("<TD colspan=2>");
				head.Add(text);
				head.Add("</TD>");
				head.Add("</TR>");
			}
			string text2 = NPCBuilder.Personality();
			if (text2 != "")
			{
				head.Add("<TR>");
				head.Add("<TD>");
				head.Add("<B>Personality</B>");
				head.Add("</TD>");
				head.Add("<TD colspan=2>");
				head.Add(text2);
				head.Add("</TD>");
				head.Add("</TR>");
			}
			string text3 = NPCBuilder.Speech();
			if (text3 != "")
			{
				head.Add("<TR>");
				head.Add("<TD>");
				head.Add("<B>Speech</B>");
				head.Add("</TD>");
				head.Add("<TD colspan=2>");
				head.Add(text3);
				head.Add("</TD>");
				head.Add("</TR>");
			}
			head.Add("</TABLE>");
			head.Add("</P>");
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void RoomBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<H3>" + RoomBuilder.Name() + "</H3>");
			head.Add("<P>" + RoomBuilder.Details() + "</P>");
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void ElfTextBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			int num = Session.Dice(1, 6);
			for (int num2 = 0; num2 != num; num2++)
			{
				head.Add("<P>" + ElfName.Sentence() + "</P>");
			}
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void DwarfTextBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			int num = Session.Dice(1, 6);
			for (int num2 = 0; num2 != num; num2++)
			{
				head.Add("<P>" + DwarfName.Sentence() + "</P>");
			}
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void PrimordialTextBtn_Click(object sender, EventArgs e)
		{
			List<string> head = HTML.GetHead(null, null, DisplaySize.Small);
			head.Add("<BODY>");
			int num = Session.Dice(1, 6);
			for (int num2 = 0; num2 != num; num2++)
			{
				head.Add("<P>" + ExoticName.Sentence() + "</P>");
			}
			head.Add("</BODY>");
			this.GeneratorBrowser.DocumentText = HTML.Concatenate(head);
		}

		public void UpdateView()
		{
			try
			{
				this.fUpdating = true;
				this.update_workspace();
				this.update_background_list();
				this.update_background_item();
				this.update_encyclopedia_list();
				this.update_encyclopedia_entry();
				this.update_rules_list();
				this.update_selected_rule();
				this.update_attachments();
				this.update_notes();
				this.update_reference();
				foreach (IAddIn current in this.fExtensibility.AddIns)
				{
					foreach (IPage current2 in current.Pages)
					{
						current2.UpdateView();
					}
				}
				if (this.fView == MainForm.ViewType.Delve)
				{
					foreach (Control control in this.PreviewSplitter.Panel1.Controls)
					{
						if (control is MapView)
						{
							MapView mapView = control as MapView;
							mapView.Map = Session.Project.FindTacticalMap(mapView.Map.ID);
							break;
						}
					}
				}
				if (this.fView == MainForm.ViewType.Map)
				{
					foreach (Control control2 in this.PreviewSplitter.Panel1.Controls)
					{
						if (control2 is RegionalMapPanel)
						{
							RegionalMapPanel regionalMapPanel = control2 as RegionalMapPanel;
							regionalMapPanel.Map = Session.Project.FindRegionalMap(regionalMapPanel.Map.ID);
							break;
						}
					}
				}
				this.fUpdating = false;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void update_title()
		{
			try
			{
				string text = "Masterplan";
				if (Session.Project != null)
				{
					text = Session.Project.Name + " - Masterplan";
				}
				this.Text = text;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void update_workspace()
		{
			try
			{
				this.update_navigation();
				this.update_preview();
				this.update_breadcrumbs();
				this.PlotView.Invalidate();
				if (this.fView == MainForm.ViewType.Delve)
				{
					MapView mapView = null;
					foreach (Control control in this.PreviewSplitter.Panel1.Controls)
					{
						if (control is MapView && control.Visible)
						{
							mapView = (control as MapView);
							break;
						}
					}
					if (mapView != null)
					{
						mapView.MapChanged();
					}
				}
				if (this.fView == MainForm.ViewType.Map)
				{
					RegionalMapPanel regionalMapPanel = null;
					foreach (Control control2 in this.PreviewSplitter.Panel1.Controls)
					{
						if (control2 is RegionalMapPanel && control2.Visible)
						{
							regionalMapPanel = (control2 as RegionalMapPanel);
							break;
						}
					}
					if (regionalMapPanel != null)
					{
						regionalMapPanel.Invalidate();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void update_navigation()
		{
			try
			{
				this.NavigationTree.BeginUpdate();
				this.NavigationTree.Nodes.Clear();
				if (Session.Project != null)
				{
					this.add_navigation_node(null, null);
					this.NavigationTree.ExpandAll();
				}
				this.NavigationTree.EndUpdate();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void add_navigation_node(PlotPoint pp, TreeNode parent)
		{
			try
			{
				string text = (pp != null) ? pp.Name : Session.Project.Name;
				TreeNodeCollection treeNodeCollection = (parent != null) ? parent.Nodes : this.NavigationTree.Nodes;
				TreeNode treeNode = treeNodeCollection.Add(text);
				Plot plot = (pp != null) ? pp.Subplot : Session.Project.Plot;
				treeNode.Tag = plot;
				if (this.PlotView.Plot == plot)
				{
					this.NavigationTree.SelectedNode = treeNode;
				}
				List<PlotPoint> list = (pp != null) ? pp.Subplot.Points : Session.Project.Plot.Points;
				foreach (PlotPoint current in list)
				{
					if (current.Subplot.Points.Count != 0 || current.Subplot == this.PlotView.Plot)
					{
						this.add_navigation_node(current, treeNode);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void update_breadcrumbs()
		{
			try
			{
				this.BreadcrumbBar.Items.Clear();
				if (Session.Project != null)
				{
					List<PlotPoint> list = new List<PlotPoint>();
					Plot p = this.PlotView.Plot;
					while (p != null)
					{
						PlotPoint plotPoint = Session.Project.FindParent(p);
						p = ((plotPoint != null) ? Session.Project.FindParent(plotPoint) : null);
						list.Add(plotPoint);
					}
					list.Reverse();
					foreach (PlotPoint current in list)
					{
						bool link = list.IndexOf(current) != list.Count - 1;
						this.add_breadcrumb(current, link);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void add_breadcrumb(PlotPoint pp, bool link)
		{
			try
			{
				string text = (pp != null) ? pp.Name : Session.Project.Name;
				ToolStripLabel toolStripLabel = new ToolStripLabel(text);
				toolStripLabel.IsLink = link;
				toolStripLabel.Tag = pp;
				toolStripLabel.Click += new EventHandler(this.Breadcrumb_Click);
				this.BreadcrumbBar.Items.Add(toolStripLabel);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void Breadcrumb_Click(object sender, EventArgs e)
		{
			try
			{
				ToolStripLabel toolStripLabel = sender as ToolStripLabel;
				PlotPoint plotPoint = toolStripLabel.Tag as PlotPoint;
				if (plotPoint == null)
				{
					this.PlotView.Plot = Session.Project.Plot;
					this.UpdateView();
				}
				else
				{
					this.PlotView.Plot = plotPoint.Subplot;
					this.UpdateView();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void update_background_list()
		{
			try
			{
				Background selectedBackground = this.SelectedBackground;
				this.BackgroundList.Items.Clear();
				if (Session.Project != null)
				{
					foreach (Background current in Session.Project.Backgrounds)
					{
						ListViewItem listViewItem = this.BackgroundList.Items.Add(current.Title);
						listViewItem.Tag = current;
						if (current.Details == "")
						{
							listViewItem.ForeColor = SystemColors.GrayText;
						}
						if (current == selectedBackground)
						{
							listViewItem.Selected = true;
						}
					}
					if (Session.Project.Backgrounds.Count == 0)
					{
						ListViewItem listViewItem2 = this.BackgroundList.Items.Add("(no backgrounds)");
						listViewItem2.ForeColor = SystemColors.GrayText;
					}
				}
				else
				{
					ListViewItem listViewItem3 = this.BackgroundList.Items.Add("(no project)");
					listViewItem3.ForeColor = SystemColors.GrayText;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void update_background_item()
		{
			try
			{
				this.BackgroundDetails.Document.OpenNew(true);
				this.BackgroundDetails.Document.Write(HTML.Background(this.SelectedBackground, DisplaySize.Small));
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void update_encyclopedia_templates()
		{
			try
			{
				string path = Application.StartupPath + "\\Encyclopedia";
				if (Directory.Exists(path))
				{
					List<string> list = new List<string>();
					list.AddRange(Directory.GetFiles(path, "*.txt"));
					list.AddRange(Directory.GetFiles(path, "*.htm"));
					list.AddRange(Directory.GetFiles(path, "*.html"));
					if (list.Count > 0)
					{
						this.EncAddBtn.DropDownItems.Add(new ToolStripSeparator());
						foreach (string current in list)
						{
							string text = FileName.Name(current);
							ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(text);
							toolStripMenuItem.Tag = current;
							toolStripMenuItem.Click += new EventHandler(this.encyclopedia_template);
							this.EncAddBtn.DropDownItems.Add(toolStripMenuItem);
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void update_encyclopedia_list()
		{
			try
			{
				string[] separator = null;
				string[] array = this.EncSearchBox.Text.ToLower().Split(separator, StringSplitOptions.RemoveEmptyEntries);
				this.EntryList.BeginUpdate();
				if (Session.Project != null)
				{
					this.EntryList.ShowGroups = true;
					BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
					foreach (EncyclopediaEntry current in Session.Project.Encyclopedia.Entries)
					{
						if (current.Category != null && current.Category != "")
						{
							binarySearchTree.Add(current.Category);
						}
					}
					List<string> sortedList = binarySearchTree.SortedList;
					sortedList.Insert(0, "Groups");
					sortedList.Add("Miscellaneous Entries");
					this.EntryList.Groups.Clear();
					foreach (string current2 in sortedList)
					{
						this.EntryList.Groups.Add(current2, current2);
					}
					List<ListViewItem> list = new List<ListViewItem>();
					if (array.Length == 0)
					{
						List<EncyclopediaGroup> list2 = new List<EncyclopediaGroup>();
						list2.AddRange(Session.Project.Encyclopedia.Groups);
						list2.Sort();
						foreach (EncyclopediaGroup current3 in list2)
						{
							list.Add(new ListViewItem(current3.Name)
							{
								Tag = current3,
								Group = this.EntryList.Groups["Groups"]
							});
						}
					}
					foreach (EncyclopediaEntry current4 in Session.Project.Encyclopedia.Entries)
					{
						if (this.match(current4, array))
						{
							ListViewItem listViewItem = new ListViewItem(current4.Name);
							listViewItem.Tag = current4;
							if (current4.Category != null && current4.Category != "")
							{
								listViewItem.Group = this.EntryList.Groups[current4.Category];
							}
							else
							{
								listViewItem.Group = this.EntryList.Groups["Miscellaneous Entries"];
							}
							if (current4.Details == "" && current4.DMInfo == "")
							{
								listViewItem.ForeColor = SystemColors.GrayText;
							}
							list.Add(listViewItem);
						}
					}
					if (list.Count == 0)
					{
						this.EntryList.ShowGroups = false;
						string text = (this.EncSearchBox.Text == "") ? "(no entries)" : "(no matching entries)";
						list.Add(new ListViewItem(text)
						{
							ForeColor = SystemColors.GrayText
						});
					}
					this.EntryList.Items.Clear();
					this.EntryList.Items.AddRange(list.ToArray());
				}
				else
				{
					ListViewItem listViewItem2 = this.EntryList.Items.Add("(no project)");
					listViewItem2.ForeColor = SystemColors.GrayText;
				}
				this.EntryList.EndUpdate();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private bool match(EncyclopediaEntry entry, string[] tokens)
		{
			try
			{
				bool result;
				for (int i = 0; i < tokens.Length; i++)
				{
					string token = tokens[i];
					if (!this.match(entry, token))
					{
						result = false;
						return result;
					}
				}
				result = true;
				return result;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return false;
		}

		private bool match(EncyclopediaEntry entry, string token)
		{
			try
			{
				bool result;
				if (entry.Name.ToLower().Contains(token))
				{
					result = true;
					return result;
				}
				if (entry.Details.ToLower().Contains(token))
				{
					result = true;
					return result;
				}
				result = false;
				return result;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return false;
		}

		private void update_encyclopedia_entry()
		{
			try
			{
				Encyclopedia encyclopedia = (Session.Project != null) ? Session.Project.Encyclopedia : null;
				string text = "";
				if (this.SelectedEncyclopediaItem != null)
				{
					if (this.SelectedEncyclopediaItem is EncyclopediaEntry)
					{
						text = HTML.EncyclopediaEntry(this.SelectedEncyclopediaItem as EncyclopediaEntry, encyclopedia, DisplaySize.Small, true, true, true, false);
					}
					if (this.SelectedEncyclopediaItem is EncyclopediaGroup)
					{
						text = HTML.EncyclopediaGroup(this.SelectedEncyclopediaItem as EncyclopediaGroup, encyclopedia, DisplaySize.Small, true, true);
					}
				}
				else
				{
					text = HTML.EncyclopediaEntry(null, encyclopedia, DisplaySize.Small, true, true, true, false);
				}
				this.EntryDetails.Document.OpenNew(true);
				this.EntryDetails.Document.Write(text);
				this.update_encyclopedia_images();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void update_encyclopedia_images()
		{
			try
			{
				EncyclopediaEntry encyclopediaEntry = null;
				if (this.SelectedEncyclopediaItem != null && this.SelectedEncyclopediaItem is EncyclopediaEntry)
				{
					encyclopediaEntry = (this.SelectedEncyclopediaItem as EncyclopediaEntry);
				}
				bool flag = false;
				if (encyclopediaEntry != null)
				{
					flag = (encyclopediaEntry.Images.Count > 0);
				}
				if (flag)
				{
					this.EntryImageList.Items.Clear();
					this.EntryImageList.LargeImageList = null;
					ImageList imageList = new ImageList();
					imageList.ImageSize = new Size(64, 64);
					imageList.ColorDepth = ColorDepth.Depth32Bit;
					this.EntryImageList.LargeImageList = imageList;
					foreach (EncyclopediaImage current in encyclopediaEntry.Images)
					{
						if (current.Image != null)
						{
							ListViewItem listViewItem = this.EntryImageList.Items.Add(current.Name);
							listViewItem.Tag = current;
							Image image = new Bitmap(64, 64);
							Graphics graphics = Graphics.FromImage(image);
							if (current.Image.Size.Width > current.Image.Size.Height)
							{
								int num = current.Image.Size.Height * 64 / current.Image.Size.Width;
								Rectangle rect = new Rectangle(0, (64 - num) / 2, 64, num);
								graphics.DrawImage(current.Image, rect);
							}
							else
							{
								int num2 = current.Image.Size.Width * 64 / current.Image.Size.Height;
								Rectangle rect2 = new Rectangle((64 - num2) / 2, 0, num2, 64);
								graphics.DrawImage(current.Image, rect2);
							}
							imageList.Images.Add(image);
							listViewItem.ImageIndex = imageList.Images.Count - 1;
						}
					}
					this.EncyclopediaEntrySplitter.Panel2Collapsed = false;
				}
				else
				{
					this.EntryImageList.Items.Clear();
					this.EntryImageList.LargeImageList = null;
					this.EncyclopediaEntrySplitter.Panel2Collapsed = true;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void update_rules_list()
		{
			this.RulesList.Items.Clear();
			this.RulesList.ShowGroups = true;
			if (Session.Project != null)
			{
				foreach (IPlayerOption current in Session.Project.PlayerOptions)
				{
					int index = 0;
					if (current is Race)
					{
						index = 0;
					}
					if (current is Class)
					{
						index = 1;
					}
					if (current is Theme)
					{
						index = 2;
					}
					if (current is ParagonPath)
					{
						index = 3;
					}
					if (current is EpicDestiny)
					{
						index = 4;
					}
					if (current is PlayerBackground)
					{
						index = 5;
					}
					if (current is Feat)
					{
						Feat feat = current as Feat;
						switch (feat.Tier)
						{
						case Tier.Heroic:
							index = 6;
							break;
						case Tier.Paragon:
							index = 7;
							break;
						case Tier.Epic:
							index = 8;
							break;
						}
					}
					if (current is Weapon)
					{
						index = 9;
					}
					if (current is Ritual)
					{
						index = 10;
					}
					if (current is CreatureLore)
					{
						index = 11;
					}
					if (current is Disease)
					{
						index = 12;
					}
					if (current is Poison)
					{
						index = 13;
					}
					ListViewItem listViewItem = this.RulesList.Items.Add(current.Name);
					listViewItem.Tag = current;
					listViewItem.Group = this.RulesList.Groups[index];
				}
				if (this.RulesList.Items.Count == 0)
				{
					this.RulesList.ShowGroups = false;
					ListViewItem listViewItem2 = this.RulesList.Items.Add("(none)");
					listViewItem2.ForeColor = SystemColors.GrayText;
				}
			}
		}

		private void update_selected_rule()
		{
			if (this.SelectedRule != null)
			{
				this.RulesBrowser.Document.OpenNew(true);
				this.RulesBrowser.Document.Write(HTML.PlayerOption(this.SelectedRule, DisplaySize.Small));
				return;
			}
			List<string> list = new List<string>();
			list.Add("<HTML>");
			list.AddRange(HTML.GetHead(null, null, DisplaySize.Small));
			list.Add("<BODY>");
			list.Add("<P class=instruction>On this page you can create and manage campaign-specific rules elements.</P>");
			list.Add("</BODY>");
			list.Add("</HTML>");
			this.RulesBrowser.Document.OpenNew(true);
			this.RulesBrowser.Document.Write(HTML.Concatenate(list));
		}

		private void update_attachments()
		{
			try
			{
				if (Session.Project != null)
				{
					BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
					foreach (Attachment current in Session.Project.Attachments)
					{
						string item = FileName.Extension(current.Name).ToUpper() + " Files";
						binarySearchTree.Add(item);
					}
					List<string> sortedList = binarySearchTree.SortedList;
					this.AttachmentList.Groups.Clear();
					foreach (string current2 in sortedList)
					{
						this.AttachmentList.Groups.Add(current2, current2);
					}
					this.AttachmentList.Items.Clear();
					foreach (Attachment current3 in Session.Project.Attachments)
					{
						int num = current3.Contents.Length;
						string text = num + " B";
						float num2 = (float)num / 1024f;
						if (num2 >= 1f)
						{
							text = num2.ToString("F1") + " KB";
						}
						float num3 = num2 / 1024f;
						if (num3 >= 1f)
						{
							text = num3.ToString("F1") + " MB";
						}
						float num4 = num3 / 1024f;
						if (num4 >= 1f)
						{
							text = num4.ToString("F1") + " GB";
						}
						string key = FileName.Extension(current3.Name).ToUpper() + " Files";
						ListViewItem listViewItem = this.AttachmentList.Items.Add(current3.Name);
						listViewItem.SubItems.Add(text);
						listViewItem.Group = this.AttachmentList.Groups[key];
						listViewItem.Tag = current3;
					}
					if (Session.Project.Attachments.Count == 0)
					{
						ListViewItem listViewItem2 = this.AttachmentList.Items.Add("(no attachments)");
						listViewItem2.ForeColor = SystemColors.GrayText;
					}
				}
				else
				{
					ListViewItem listViewItem3 = this.AttachmentList.Items.Add("(no project)");
					listViewItem3.ForeColor = SystemColors.GrayText;
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
				this.NoteList.BeginUpdate();
				Note selectedNote = this.SelectedNote;
				this.NoteList.Items.Clear();
				this.NoteBox.Text = "";
				BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
				if (Session.Project != null)
				{
					foreach (Note current in Session.Project.Notes)
					{
						if (current.Category != "")
						{
							binarySearchTree.Add(current.Category);
						}
					}
				}
				List<string> sortedList = binarySearchTree.SortedList;
				sortedList.Add("Notes");
				this.NoteList.Groups.Clear();
				foreach (string current2 in sortedList)
				{
					this.NoteList.Groups.Add(current2, current2);
				}
				string[] tokens = this.NoteSearchBox.Text.ToLower().Split(new char[0]);
				if (Session.Project != null)
				{
					foreach (Note current3 in Session.Project.Notes)
					{
						if (this.match(current3, tokens))
						{
							ListViewItem listViewItem = this.NoteList.Items.Add(current3.Name);
							listViewItem.Tag = current3;
							if (current3.Category == "")
							{
								listViewItem.Group = this.NoteList.Groups["Notes"];
							}
							else
							{
								listViewItem.Group = this.NoteList.Groups[current3.Category];
							}
							if (current3.Content == "")
							{
								listViewItem.ForeColor = SystemColors.GrayText;
							}
							if (current3 == selectedNote)
							{
								listViewItem.Selected = true;
							}
						}
					}
				}
				if (this.NoteList.Groups["Notes"].Items.Count == 0)
				{
					string text = (this.NoteSearchBox.Text == "") ? "(no notes)" : "(no matching notes)";
					ListViewItem listViewItem2 = this.NoteList.Items.Add(text);
					listViewItem2.ForeColor = SystemColors.GrayText;
					listViewItem2.Group = this.NoteList.Groups["Notes"];
				}
				this.NoteList.Sort();
				this.NoteList.EndUpdate();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private bool match(Note n, string[] tokens)
		{
			try
			{
				bool result;
				for (int i = 0; i < tokens.Length; i++)
				{
					string token = tokens[i];
					if (!this.match(n, token))
					{
						result = false;
						return result;
					}
				}
				result = true;
				return result;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return false;
		}

		private bool match(Note n, string token)
		{
			try
			{
				return n.Content.ToLower().Contains(token);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return false;
		}

		private void update_reference()
		{
			if (Session.Project != null)
			{
				this.InfoPanel.Level = Session.Project.Party.Level;
			}
			this.update_party();
			if (this.GeneratorBrowser.DocumentText == "")
			{
				List<string> list = new List<string>();
				list.AddRange(HTML.GetHead(null, null, DisplaySize.Small));
				list.Add("<BODY>");
				list.Add("<P class=instruction>");
				list.Add("Use the buttons to the left to generate random names etc.");
				list.Add("</P>");
				list.Add("</BODY>");
				this.GeneratorBrowser.DocumentText = HTML.Concatenate(list);
			}
			foreach (IAddIn current in this.fExtensibility.AddIns)
			{
				foreach (IPage current2 in current.QuickReferencePages)
				{
					current2.UpdateView();
				}
			}
		}

		private void update_party()
		{
			if (this.PartyBrowser.Document == null)
			{
				this.PartyBrowser.DocumentText = "";
			}
			this.PartyBrowser.Document.OpenNew(true);
			this.PartyBrowser.Document.Write(HTML.PCs(this.fPartyBreakdownSecondary, DisplaySize.Small));
		}

		private void add_between(object sender, EventArgs e)
		{
			try
			{
				ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
				Pair<PlotPoint, PlotPoint> pair = toolStripMenuItem.Tag as Pair<PlotPoint, PlotPoint>;
				this.add_point(pair.First, pair.Second);
				this.UpdateView();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void disconnect_points(object sender, EventArgs e)
		{
			try
			{
				ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
				Pair<PlotPoint, PlotPoint> pair = toolStripMenuItem.Tag as Pair<PlotPoint, PlotPoint>;
				Guid iD = pair.Second.ID;
				while (pair.First.Links.Contains(iD))
				{
					pair.First.Links.Remove(iD);
				}
				this.PlotView.RecalculateLayout();
				Session.Modified = true;
				this.update_workspace();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void move_to_subplot(object sender, EventArgs e)
		{
			try
			{
				ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
				Pair<PlotPoint, PlotPoint> pair = toolStripMenuItem.Tag as Pair<PlotPoint, PlotPoint>;
				this.PlotView.Plot.RemovePoint(pair.Second);
				pair.First.Subplot.Points.Add(pair.Second);
				Session.Modified = true;
				this.PlotView.RecalculateLayout();
				this.UpdateView();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void add_attachment(string filename)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(filename);
				Attachment attachment = new Attachment();
				attachment.Name = fileInfo.Name;
				attachment.Contents = File.ReadAllBytes(filename);
				Attachment attachment2 = Session.Project.FindAttachment(attachment.Name);
				if (attachment2 != null)
				{
					string text = "An attachment with this name already exists.";
					text += Environment.NewLine;
					text += "Do you want to replace it?";
					DialogResult dialogResult = MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					DialogResult dialogResult2 = dialogResult;
					if (dialogResult2 == DialogResult.Cancel)
					{
						return;
					}
					switch (dialogResult2)
					{
					case DialogResult.Yes:
						Session.Project.Attachments.Remove(attachment2);
						break;
					case DialogResult.No:
					{
						int num = 1;
						while (Session.Project.FindAttachment(attachment.Name) != null)
						{
							num++;
							attachment.Name = string.Concat(new object[]
							{
								FileName.Name(filename),
								" ",
								num,
								".",
								FileName.Extension(filename)
							});
						}
						break;
					}
					}
				}
				Session.Project.Attachments.Add(attachment);
				Session.Project.Attachments.Sort();
				Session.Modified = true;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private bool check_modified()
		{
			try
			{
				if (Session.Modified)
				{
					string text = "The project has been modified.\nDo you want to save it now?";
					DialogResult dialogResult = MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					DialogResult dialogResult2 = dialogResult;
					if (dialogResult2 == DialogResult.Cancel)
					{
						bool result = false;
						return result;
					}
					switch (dialogResult2)
					{
					case DialogResult.Yes:
						if (Session.FileName != "")
						{
							GC.Collect();
							Session.Project.PopulateProjectLibrary();
							bool flag = Serialisation<Project>.Save(Session.FileName, Session.Project, SerialisationMode.Binary);
							Session.Project.SimplifyProjectLibrary();
							if (!flag)
							{
								bool result = false;
								return result;
							}
							Session.Modified = false;
						}
						else
						{
							SaveFileDialog saveFileDialog = new SaveFileDialog();
							saveFileDialog.Filter = Program.ProjectFilter;
							saveFileDialog.FileName = Session.Project.Name;
							if (saveFileDialog.ShowDialog() != DialogResult.OK)
							{
								bool result = false;
								return result;
							}
							GC.Collect();
							Session.Project.PopulateProjectLibrary();
							bool flag2 = Serialisation<Project>.Save(saveFileDialog.FileName, Session.Project, SerialisationMode.Binary);
							Session.Project.SimplifyProjectLibrary();
							if (!flag2)
							{
								bool result = false;
								return result;
							}
							Session.FileName = saveFileDialog.FileName;
							Session.Modified = false;
						}
						break;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return true;
		}

		private void print_page(object sender, PrintPageEventArgs e)
		{
			try
			{
				Bitmap image = Screenshot.Plot(this.PlotView.Plot, e.MarginBounds.Size);
				e.Graphics.DrawImage(image, e.MarginBounds);
				e.HasMorePages = false;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}
	}
}
