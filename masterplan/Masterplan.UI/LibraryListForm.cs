using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class LibraryListForm : Form
	{
		private IContainer components;

		private SplitContainer Splitter;

		private ToolStrip LibraryToolbar;

		private ToolStripButton LibraryRemoveBtn;

		private ToolStripButton LibraryEditBtn;

		private ListView CreatureList;

		private ToolStrip CreatureToolbar;

		private ToolStripButton OppRemoveBtn;

		private ToolStripButton OppEditBtn;

		private ColumnHeader CreatureNameHdr;

		private ColumnHeader CreatureInfoHdr;

		private TabControl Pages;

		private TabPage CreaturesPage;

		private TabPage TemplatesPage;

		private ToolStrip TemplateToolbar;

		private ToolStripButton TemplateRemoveBtn;

		private ToolStripButton TemplateEditBtn;

		private ListView TemplateList;

		private ColumnHeader TemplateNameHdr;

		private TabPage TilesPage;

		private ListView TileList;

		private ColumnHeader TileSetNameHdr;

		private ToolStrip TileToolbar;

		private ToolStripButton TileRemoveBtn;

		private ToolStripButton TileEditBtn;

		private ColumnHeader TemplateInfoHdr;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton CreatureCutBtn;

		private ToolStripButton CreatureCopyBtn;

		private ToolStripButton CreaturePasteBtn;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton TemplateCutBtn;

		private ToolStripButton TemplateCopyBtn;

		private ToolStripButton TemplatePasteBtn;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton TileCutBtn;

		private ToolStripButton TileCopyBtn;

		private ToolStripButton TilePasteBtn;

		private ToolStripSeparator toolStripSeparator4;

		private TabPage TrapsPage;

		private ListView TrapList;

		private ColumnHeader TrapNameHdr;

		private ColumnHeader TrapInfoHdr;

		private ToolStrip TrapToolbar;

		private ToolStripButton TrapRemoveBtn;

		private ToolStripButton TrapEditBtn;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripButton TrapCutBtn;

		private ToolStripButton TrapCopyBtn;

		private ToolStripButton TrapPasteBtn;

		private TabPage ChallengePage;

		private ListView ChallengeList;

		private ColumnHeader ChallengeNameHdr;

		private ColumnHeader ChallengeInfoHdr;

		private ToolStrip ChallengeToolbar;

		private ToolStripButton ChallengeRemoveBtn;

		private ToolStripButton ChallengeEditBtn;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton ChallengeCutBtn;

		private ToolStripButton ChallengeCopyBtn;

		private ToolStripButton ChallengePasteBtn;

		private ContextMenuStrip CreatureContext;

		private ToolStripMenuItem CreatureContextRemove;

		private ToolStripMenuItem CreatureContextCategory;

		private ContextMenuStrip TileContext;

		private ToolStripMenuItem TileContextRemove;

		private ToolStripMenuItem TileContextCategory;

		private ToolStripMenuItem TilePlain;

		private ToolStripMenuItem TileDoorway;

		private ToolStripMenuItem TileStairway;

		private ToolStripMenuItem TileFeature;

		private ToolStripMenuItem TileSpecial;

		private ContextMenuStrip TemplateContext;

		private ToolStripMenuItem TemplateContextRemove;

		private ToolStripMenuItem TemplateContextType;

		private ToolStripMenuItem TemplateFunctional;

		private ToolStripMenuItem TemplateClass;

		private ContextMenuStrip TrapContext;

		private ToolStripMenuItem TrapContextRemove;

		private ToolStripMenuItem TrapContextType;

		private ToolStripMenuItem TrapTrap;

		private ToolStripMenuItem TrapHazard;

		private ContextMenuStrip ChallengeContext;

		private ToolStripMenuItem ChallengeContextRemove;

		private ToolStripButton CreatureStatBlockBtn;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripButton TrapStatBlockBtn;

		private ToolStripSeparator toolStripSeparator9;

		private ToolStripButton ChallengeStatBlockBtn;

		private ToolStripSeparator toolStripSeparator10;

		private ToolStrip CreatureSearchToolbar;

		private ToolStripLabel SearchLbl;

		private ToolStripTextBox SearchBox;

		private ToolStripSeparator toolStripSeparator11;

		private ToolStripButton CategorisedBtn;

		private ToolStripButton UncategorisedBtn;

		private TabPage MagicItemsPage;

		private ListView MagicItemList;

		private ColumnHeader MagicItemHdr;

		private ToolStrip MagicItemToolbar;

		private ContextMenuStrip MagicItemContext;

		private ToolStripMenuItem MagicItemContextRemove;

		private SplitContainer splitContainer1;

		private ListView MagicItemVersionList;

		private ColumnHeader MagicItemInfoHdr;

		private ToolStrip MagicItemVersionToolbar;

		private ToolStripButton MagicItemRemoveBtn;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton MagicItemEditBtn;

		private ToolStripButton MagicItemCutBtn;

		private ToolStripButton MagicItemCopyBtn;

		private ToolStripButton MagicItemPasteBtn;

		private ToolStripSeparator toolStripSeparator12;

		private ToolStripButton MagicItemStatBlockBtn;

		private ToolStripSeparator toolStripSeparator13;

		private ToolStripSeparator toolStripSeparator14;

		private TreeView LibraryTree;

		private ToolStripSeparator toolStripSeparator15;

		private ToolStripMenuItem TileMap;

		private ToolStripSeparator toolStripSeparator16;

		private ToolStripMenuItem TileContextSize;

		private Button CompendiumBtn;

		private Button HelpBtn;

		private ToolStripSeparator toolStripSeparator17;

		private ToolStripButton LibraryMergeBtn;

		private ToolStripDropDownButton FileMenu;

		private ToolStripMenuItem FileNew;

		private ToolStripMenuItem FileClose;

		private LibraryHelpPanel HelpPanel;

		private ToolStripMenuItem FileOpen;

		private ToolStripDropDownButton TemplateAddBtn;

		private ToolStripMenuItem addTemplateToolStripMenuItem;

		private ToolStripMenuItem TemplateAddTheme;

		private ToolStripDropDownButton TileAddBtn;

		private ToolStripMenuItem addTileToolStripMenuItem;

		private ToolStripMenuItem TileAddFolder;

		private ToolStripSeparator toolStripSeparator18;

		private ToolStripButton TemplateStatBlock;

		private ToolStripDropDownButton CreatureAddBtn;

		private ToolStripMenuItem CreatureAddSingle;

		private ToolStripDropDownButton CreatureTools;

		private ToolStripMenuItem CreatureToolsDemographics;

		private ToolStripMenuItem CreatureToolsPowerStatistics;

		private ToolStripMenuItem CreatureToolsFilterList;

		private ToolStripDropDownButton TrapTools;

		private ToolStripMenuItem TrapToolsDemographics;

		private ToolStripMenuItem CreatureToolsExport;

		private ToolStripSeparator toolStripSeparator19;

		private ToolStripMenuItem CreatureImport;

		private ToolStripSeparator toolStripSeparator20;

		private ToolStripMenuItem TemplateImport;

		private ToolStripSeparator toolStripSeparator21;

		private ToolStripDropDownButton TemplateTools;

		private ToolStripMenuItem TemplateToolsExport;

		private ToolStripMenuItem TrapToolsExport;

		private ToolStripDropDownButton TrapAdd;

		private ToolStripMenuItem TrapAddAdd;

		private ToolStripMenuItem TrapAddImport;

		private ToolStripDropDownButton ChallengeAdd;

		private ToolStripMenuItem ChallengeAddAdd;

		private ToolStripMenuItem ChallengeAddImport;

		private ToolStripSeparator toolStripSeparator22;

		private ToolStripDropDownButton ChallengeTools;

		private ToolStripMenuItem ChallengeToolsExport;

		private ToolStripDropDownButton MagicItemAdd;

		private ToolStripMenuItem MagicItemAddAdd;

		private ToolStripMenuItem MagicItemAddImport;

		private ToolStripDropDownButton MagicItemTools;

		private ToolStripMenuItem MagicItemToolsDemographics;

		private ToolStripMenuItem MagicItemToolsExport;

		private ToolStripSeparator toolStripSeparator24;

		private ToolStripMenuItem TileAddImport;

		private ToolStripSeparator toolStripSeparator23;

		private ToolStripDropDownButton TileTools;

		private ToolStripMenuItem TileToolsExport;

		private ToolStripSeparator toolStripSeparator25;

		private ToolStripSeparator toolStripSeparator26;

		private ToolStripSeparator toolStripSeparator27;

		private TabPage TerrainPowersPage;

		private ListView TerrainPowerList;

		private ColumnHeader TPNameHdr;

		private ColumnHeader TPInfoHdr;

		private ToolStrip TerrainPowerToolbar;

		private ToolStripDropDownButton TPAdd;

		private ToolStripMenuItem TPAddTerrainPower;

		private ToolStripSeparator toolStripSeparator28;

		private ToolStripMenuItem TPAddImport;

		private ToolStripButton TPRemoveBtn;

		private ToolStripButton TPEditBtn;

		private ToolStripSeparator toolStripSeparator29;

		private ToolStripButton TPCutBtn;

		private ToolStripButton TPCopyBtn;

		private ToolStripButton TPPasteBtn;

		private ToolStripSeparator toolStripSeparator30;

		private ToolStripDropDownButton TPTools;

		private ToolStripMenuItem TPToolsExport;

		private ContextMenuStrip TPContext;

		private ToolStripMenuItem TPContextRemove;

		private TabPage ArtifactPage;

		private ListView ArtifactList;

		private ColumnHeader ArtifactHdr;

		private ColumnHeader ArtifactInfoHdr;

		private ToolStrip ArtifactToolbar;

		private ToolStripDropDownButton ArtifactAdd;

		private ToolStripMenuItem ArtifactAddAdd;

		private ToolStripSeparator toolStripSeparator31;

		private ToolStripMenuItem ArtifactAddImport;

		private ToolStripButton ArtifactRemove;

		private ToolStripButton ArtifactEdit;

		private ToolStripSeparator toolStripSeparator32;

		private ToolStripButton ArtifactCut;

		private ToolStripButton ArtifactCopy;

		private ToolStripButton ArtifactPaste;

		private ToolStripSeparator toolStripSeparator33;

		private ToolStripDropDownButton ArtifactTools;

		private ToolStripMenuItem ArtifactToolsExport;

		private ContextMenuStrip ArtifactContext;

		private ToolStripMenuItem ArtifactContextRemove;

		private ToolStripButton ArtifactStatBlockBtn;

		private ToolStripSeparator toolStripSeparator34;

		private ToolStripButton TPStatBlockBtn;

		private ToolStripSeparator toolStripSeparator35;

		private Dictionary<Library, bool> fModified = new Dictionary<Library, bool>();

		private List<TabPage> fCleanPages = new List<TabPage>();

		private bool fShowCategorised = true;

		private bool fShowUncategorised = true;

		public Library SelectedLibrary
		{
			get
			{
				if (this.LibraryTree.SelectedNode != null)
				{
					return this.LibraryTree.SelectedNode.Tag as Library;
				}
				return null;
			}
			set
			{
				List<TreeNode> list = new List<TreeNode>();
				foreach (TreeNode tn in this.LibraryTree.Nodes)
				{
					this.get_nodes(tn, list);
				}
				foreach (TreeNode current in list)
				{
					if (current.Tag == value)
					{
						this.LibraryTree.SelectedNode = current;
						break;
					}
				}
			}
		}

		public List<Creature> SelectedCreatures
		{
			get
			{
				List<Creature> list = new List<Creature>();
				foreach (ListViewItem listViewItem in this.CreatureList.SelectedItems)
				{
					Creature creature = listViewItem.Tag as Creature;
					if (creature != null)
					{
						list.Add(creature);
					}
				}
				return list;
			}
		}

		public List<CreatureTemplate> SelectedTemplates
		{
			get
			{
				List<CreatureTemplate> list = new List<CreatureTemplate>();
				foreach (ListViewItem listViewItem in this.TemplateList.SelectedItems)
				{
					CreatureTemplate creatureTemplate = listViewItem.Tag as CreatureTemplate;
					if (creatureTemplate != null)
					{
						list.Add(creatureTemplate);
					}
				}
				return list;
			}
		}

		public List<MonsterTheme> SelectedThemes
		{
			get
			{
				List<MonsterTheme> list = new List<MonsterTheme>();
				foreach (ListViewItem listViewItem in this.TemplateList.SelectedItems)
				{
					MonsterTheme monsterTheme = listViewItem.Tag as MonsterTheme;
					if (monsterTheme != null)
					{
						list.Add(monsterTheme);
					}
				}
				return list;
			}
		}

		public List<Trap> SelectedTraps
		{
			get
			{
				List<Trap> list = new List<Trap>();
				foreach (ListViewItem listViewItem in this.TrapList.SelectedItems)
				{
					Trap trap = listViewItem.Tag as Trap;
					if (trap != null)
					{
						list.Add(trap);
					}
				}
				return list;
			}
		}

		public List<SkillChallenge> SelectedChallenges
		{
			get
			{
				List<SkillChallenge> list = new List<SkillChallenge>();
				foreach (ListViewItem listViewItem in this.ChallengeList.SelectedItems)
				{
					SkillChallenge skillChallenge = listViewItem.Tag as SkillChallenge;
					if (skillChallenge != null)
					{
						list.Add(skillChallenge);
					}
				}
				return list;
			}
		}

		public string SelectedMagicItemSet
		{
			get
			{
				if (this.MagicItemList.SelectedItems.Count != 0)
				{
					return this.MagicItemList.SelectedItems[0].Text;
				}
				return "";
			}
		}

		public List<MagicItem> SelectedMagicItems
		{
			get
			{
				List<MagicItem> list = new List<MagicItem>();
				foreach (ListViewItem listViewItem in this.MagicItemVersionList.SelectedItems)
				{
					MagicItem magicItem = listViewItem.Tag as MagicItem;
					if (magicItem != null)
					{
						list.Add(magicItem);
					}
				}
				return list;
			}
		}

		public List<Tile> SelectedTiles
		{
			get
			{
				List<Tile> list = new List<Tile>();
				foreach (ListViewItem listViewItem in this.TileList.SelectedItems)
				{
					Tile tile = listViewItem.Tag as Tile;
					if (tile != null)
					{
						list.Add(tile);
					}
				}
				return list;
			}
		}

		public List<TerrainPower> SelectedTerrainPowers
		{
			get
			{
				List<TerrainPower> list = new List<TerrainPower>();
				foreach (ListViewItem listViewItem in this.TerrainPowerList.SelectedItems)
				{
					TerrainPower terrainPower = listViewItem.Tag as TerrainPower;
					if (terrainPower != null)
					{
						list.Add(terrainPower);
					}
				}
				return list;
			}
		}

		public List<Artifact> SelectedArtifacts
		{
			get
			{
				List<Artifact> list = new List<Artifact>();
				foreach (ListViewItem listViewItem in this.ArtifactList.SelectedItems)
				{
					Artifact artifact = listViewItem.Tag as Artifact;
					if (artifact != null)
					{
						list.Add(artifact);
					}
				}
				return list;
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(LibraryListForm));
			ListViewGroup listViewGroup = new ListViewGroup("Functional Templates", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Class Templates", HorizontalAlignment.Left);
			ListViewGroup listViewGroup3 = new ListViewGroup("Themes", HorizontalAlignment.Left);
			ListViewGroup listViewGroup4 = new ListViewGroup("Traps", HorizontalAlignment.Left);
			ListViewGroup listViewGroup5 = new ListViewGroup("Hazards", HorizontalAlignment.Left);
			ListViewGroup listViewGroup6 = new ListViewGroup("Traps", HorizontalAlignment.Left);
			ListViewGroup listViewGroup7 = new ListViewGroup("Hazards", HorizontalAlignment.Left);
			ListViewGroup listViewGroup8 = new ListViewGroup("Heroic Tier", HorizontalAlignment.Left);
			ListViewGroup listViewGroup9 = new ListViewGroup("Paragon Tier", HorizontalAlignment.Left);
			ListViewGroup listViewGroup10 = new ListViewGroup("Epic Tier", HorizontalAlignment.Left);
			ListViewGroup listViewGroup11 = new ListViewGroup("Traps", HorizontalAlignment.Left);
			ListViewGroup listViewGroup12 = new ListViewGroup("Hazards", HorizontalAlignment.Left);
			ListViewGroup listViewGroup13 = new ListViewGroup("Traps", HorizontalAlignment.Left);
			ListViewGroup listViewGroup14 = new ListViewGroup("Hazards", HorizontalAlignment.Left);
			this.Splitter = new SplitContainer();
			this.LibraryTree = new TreeView();
			this.LibraryToolbar = new ToolStrip();
			this.FileMenu = new ToolStripDropDownButton();
			this.FileNew = new ToolStripMenuItem();
			this.FileOpen = new ToolStripMenuItem();
			this.FileClose = new ToolStripMenuItem();
			this.LibraryRemoveBtn = new ToolStripButton();
			this.LibraryEditBtn = new ToolStripButton();
			this.toolStripSeparator17 = new ToolStripSeparator();
			this.LibraryMergeBtn = new ToolStripButton();
			this.CompendiumBtn = new Button();
			this.HelpBtn = new Button();
			this.Pages = new TabControl();
			this.CreaturesPage = new TabPage();
			this.CreatureList = new ListView();
			this.CreatureNameHdr = new ColumnHeader();
			this.CreatureInfoHdr = new ColumnHeader();
			this.CreatureContext = new ContextMenuStrip(this.components);
			this.CreatureContextRemove = new ToolStripMenuItem();
			this.CreatureContextCategory = new ToolStripMenuItem();
			this.CreatureSearchToolbar = new ToolStrip();
			this.SearchLbl = new ToolStripLabel();
			this.SearchBox = new ToolStripTextBox();
			this.toolStripSeparator11 = new ToolStripSeparator();
			this.CategorisedBtn = new ToolStripButton();
			this.UncategorisedBtn = new ToolStripButton();
			this.CreatureToolbar = new ToolStrip();
			this.CreatureAddBtn = new ToolStripDropDownButton();
			this.CreatureAddSingle = new ToolStripMenuItem();
			this.toolStripSeparator19 = new ToolStripSeparator();
			this.CreatureImport = new ToolStripMenuItem();
			this.OppRemoveBtn = new ToolStripButton();
			this.OppEditBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.CreatureCutBtn = new ToolStripButton();
			this.CreatureCopyBtn = new ToolStripButton();
			this.CreaturePasteBtn = new ToolStripButton();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.CreatureStatBlockBtn = new ToolStripButton();
			this.toolStripSeparator10 = new ToolStripSeparator();
			this.CreatureTools = new ToolStripDropDownButton();
			this.CreatureToolsDemographics = new ToolStripMenuItem();
			this.CreatureToolsPowerStatistics = new ToolStripMenuItem();
			this.CreatureToolsFilterList = new ToolStripMenuItem();
			this.CreatureToolsExport = new ToolStripMenuItem();
			this.TemplatesPage = new TabPage();
			this.TemplateList = new ListView();
			this.TemplateNameHdr = new ColumnHeader();
			this.TemplateInfoHdr = new ColumnHeader();
			this.TemplateContext = new ContextMenuStrip(this.components);
			this.TemplateContextRemove = new ToolStripMenuItem();
			this.TemplateContextType = new ToolStripMenuItem();
			this.TemplateFunctional = new ToolStripMenuItem();
			this.TemplateClass = new ToolStripMenuItem();
			this.TemplateToolbar = new ToolStrip();
			this.TemplateAddBtn = new ToolStripDropDownButton();
			this.addTemplateToolStripMenuItem = new ToolStripMenuItem();
			this.TemplateAddTheme = new ToolStripMenuItem();
			this.toolStripSeparator20 = new ToolStripSeparator();
			this.TemplateImport = new ToolStripMenuItem();
			this.TemplateRemoveBtn = new ToolStripButton();
			this.TemplateEditBtn = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.TemplateCutBtn = new ToolStripButton();
			this.TemplateCopyBtn = new ToolStripButton();
			this.TemplatePasteBtn = new ToolStripButton();
			this.toolStripSeparator18 = new ToolStripSeparator();
			this.TemplateStatBlock = new ToolStripButton();
			this.toolStripSeparator21 = new ToolStripSeparator();
			this.TemplateTools = new ToolStripDropDownButton();
			this.TemplateToolsExport = new ToolStripMenuItem();
			this.TrapsPage = new TabPage();
			this.TrapList = new ListView();
			this.TrapNameHdr = new ColumnHeader();
			this.TrapInfoHdr = new ColumnHeader();
			this.TrapContext = new ContextMenuStrip(this.components);
			this.TrapContextRemove = new ToolStripMenuItem();
			this.TrapContextType = new ToolStripMenuItem();
			this.TrapTrap = new ToolStripMenuItem();
			this.TrapHazard = new ToolStripMenuItem();
			this.TrapToolbar = new ToolStrip();
			this.TrapAdd = new ToolStripDropDownButton();
			this.TrapAddAdd = new ToolStripMenuItem();
			this.toolStripSeparator25 = new ToolStripSeparator();
			this.TrapAddImport = new ToolStripMenuItem();
			this.TrapRemoveBtn = new ToolStripButton();
			this.TrapEditBtn = new ToolStripButton();
			this.toolStripSeparator6 = new ToolStripSeparator();
			this.TrapCutBtn = new ToolStripButton();
			this.TrapCopyBtn = new ToolStripButton();
			this.TrapPasteBtn = new ToolStripButton();
			this.toolStripSeparator8 = new ToolStripSeparator();
			this.TrapStatBlockBtn = new ToolStripButton();
			this.toolStripSeparator13 = new ToolStripSeparator();
			this.TrapTools = new ToolStripDropDownButton();
			this.TrapToolsDemographics = new ToolStripMenuItem();
			this.TrapToolsExport = new ToolStripMenuItem();
			this.ChallengePage = new TabPage();
			this.ChallengeList = new ListView();
			this.ChallengeNameHdr = new ColumnHeader();
			this.ChallengeInfoHdr = new ColumnHeader();
			this.ChallengeToolbar = new ToolStrip();
			this.ChallengeAdd = new ToolStripDropDownButton();
			this.ChallengeAddAdd = new ToolStripMenuItem();
			this.toolStripSeparator26 = new ToolStripSeparator();
			this.ChallengeAddImport = new ToolStripMenuItem();
			this.ChallengeRemoveBtn = new ToolStripButton();
			this.ChallengeEditBtn = new ToolStripButton();
			this.toolStripSeparator7 = new ToolStripSeparator();
			this.ChallengeCutBtn = new ToolStripButton();
			this.ChallengeCopyBtn = new ToolStripButton();
			this.ChallengePasteBtn = new ToolStripButton();
			this.toolStripSeparator9 = new ToolStripSeparator();
			this.ChallengeStatBlockBtn = new ToolStripButton();
			this.toolStripSeparator22 = new ToolStripSeparator();
			this.ChallengeTools = new ToolStripDropDownButton();
			this.ChallengeToolsExport = new ToolStripMenuItem();
			this.MagicItemsPage = new TabPage();
			this.splitContainer1 = new SplitContainer();
			this.MagicItemList = new ListView();
			this.MagicItemHdr = new ColumnHeader();
			this.MagicItemContext = new ContextMenuStrip(this.components);
			this.MagicItemContextRemove = new ToolStripMenuItem();
			this.MagicItemToolbar = new ToolStrip();
			this.MagicItemAdd = new ToolStripDropDownButton();
			this.MagicItemAddAdd = new ToolStripMenuItem();
			this.toolStripSeparator27 = new ToolStripSeparator();
			this.MagicItemAddImport = new ToolStripMenuItem();
			this.toolStripSeparator14 = new ToolStripSeparator();
			this.MagicItemTools = new ToolStripDropDownButton();
			this.MagicItemToolsDemographics = new ToolStripMenuItem();
			this.MagicItemToolsExport = new ToolStripMenuItem();
			this.MagicItemVersionList = new ListView();
			this.MagicItemInfoHdr = new ColumnHeader();
			this.MagicItemVersionToolbar = new ToolStrip();
			this.MagicItemRemoveBtn = new ToolStripButton();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.MagicItemEditBtn = new ToolStripButton();
			this.MagicItemCutBtn = new ToolStripButton();
			this.MagicItemCopyBtn = new ToolStripButton();
			this.MagicItemPasteBtn = new ToolStripButton();
			this.toolStripSeparator12 = new ToolStripSeparator();
			this.MagicItemStatBlockBtn = new ToolStripButton();
			this.TilesPage = new TabPage();
			this.TileList = new ListView();
			this.TileSetNameHdr = new ColumnHeader();
			this.TileContext = new ContextMenuStrip(this.components);
			this.TileContextRemove = new ToolStripMenuItem();
			this.TileContextCategory = new ToolStripMenuItem();
			this.TilePlain = new ToolStripMenuItem();
			this.TileDoorway = new ToolStripMenuItem();
			this.TileStairway = new ToolStripMenuItem();
			this.TileFeature = new ToolStripMenuItem();
			this.toolStripSeparator15 = new ToolStripSeparator();
			this.TileSpecial = new ToolStripMenuItem();
			this.toolStripSeparator16 = new ToolStripSeparator();
			this.TileMap = new ToolStripMenuItem();
			this.TileContextSize = new ToolStripMenuItem();
			this.TileToolbar = new ToolStrip();
			this.TileAddBtn = new ToolStripDropDownButton();
			this.addTileToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator24 = new ToolStripSeparator();
			this.TileAddImport = new ToolStripMenuItem();
			this.TileAddFolder = new ToolStripMenuItem();
			this.TileRemoveBtn = new ToolStripButton();
			this.TileEditBtn = new ToolStripButton();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.TileCutBtn = new ToolStripButton();
			this.TileCopyBtn = new ToolStripButton();
			this.TilePasteBtn = new ToolStripButton();
			this.toolStripSeparator23 = new ToolStripSeparator();
			this.TileTools = new ToolStripDropDownButton();
			this.TileToolsExport = new ToolStripMenuItem();
			this.TerrainPowersPage = new TabPage();
			this.TerrainPowerList = new ListView();
			this.TPNameHdr = new ColumnHeader();
			this.TPInfoHdr = new ColumnHeader();
			this.TPContext = new ContextMenuStrip(this.components);
			this.TPContextRemove = new ToolStripMenuItem();
			this.TerrainPowerToolbar = new ToolStrip();
			this.TPAdd = new ToolStripDropDownButton();
			this.TPAddTerrainPower = new ToolStripMenuItem();
			this.toolStripSeparator28 = new ToolStripSeparator();
			this.TPAddImport = new ToolStripMenuItem();
			this.TPRemoveBtn = new ToolStripButton();
			this.TPEditBtn = new ToolStripButton();
			this.toolStripSeparator29 = new ToolStripSeparator();
			this.TPCutBtn = new ToolStripButton();
			this.TPCopyBtn = new ToolStripButton();
			this.TPPasteBtn = new ToolStripButton();
			this.toolStripSeparator30 = new ToolStripSeparator();
			this.TPStatBlockBtn = new ToolStripButton();
			this.toolStripSeparator35 = new ToolStripSeparator();
			this.TPTools = new ToolStripDropDownButton();
			this.TPToolsExport = new ToolStripMenuItem();
			this.ArtifactPage = new TabPage();
			this.ArtifactList = new ListView();
			this.ArtifactHdr = new ColumnHeader();
			this.ArtifactInfoHdr = new ColumnHeader();
			this.ArtifactContext = new ContextMenuStrip(this.components);
			this.ArtifactContextRemove = new ToolStripMenuItem();
			this.ArtifactToolbar = new ToolStrip();
			this.ArtifactAdd = new ToolStripDropDownButton();
			this.ArtifactAddAdd = new ToolStripMenuItem();
			this.toolStripSeparator31 = new ToolStripSeparator();
			this.ArtifactAddImport = new ToolStripMenuItem();
			this.ArtifactRemove = new ToolStripButton();
			this.ArtifactEdit = new ToolStripButton();
			this.toolStripSeparator32 = new ToolStripSeparator();
			this.ArtifactCut = new ToolStripButton();
			this.ArtifactCopy = new ToolStripButton();
			this.ArtifactPaste = new ToolStripButton();
			this.toolStripSeparator33 = new ToolStripSeparator();
			this.ArtifactStatBlockBtn = new ToolStripButton();
			this.toolStripSeparator34 = new ToolStripSeparator();
			this.ArtifactTools = new ToolStripDropDownButton();
			this.ArtifactToolsExport = new ToolStripMenuItem();
			this.HelpPanel = new LibraryHelpPanel();
			this.ChallengeContext = new ContextMenuStrip(this.components);
			this.ChallengeContextRemove = new ToolStripMenuItem();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.LibraryToolbar.SuspendLayout();
			this.Pages.SuspendLayout();
			this.CreaturesPage.SuspendLayout();
			this.CreatureContext.SuspendLayout();
			this.CreatureSearchToolbar.SuspendLayout();
			this.CreatureToolbar.SuspendLayout();
			this.TemplatesPage.SuspendLayout();
			this.TemplateContext.SuspendLayout();
			this.TemplateToolbar.SuspendLayout();
			this.TrapsPage.SuspendLayout();
			this.TrapContext.SuspendLayout();
			this.TrapToolbar.SuspendLayout();
			this.ChallengePage.SuspendLayout();
			this.ChallengeToolbar.SuspendLayout();
			this.MagicItemsPage.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.MagicItemContext.SuspendLayout();
			this.MagicItemToolbar.SuspendLayout();
			this.MagicItemVersionToolbar.SuspendLayout();
			this.TilesPage.SuspendLayout();
			this.TileContext.SuspendLayout();
			this.TileToolbar.SuspendLayout();
			this.TerrainPowersPage.SuspendLayout();
			this.TPContext.SuspendLayout();
			this.TerrainPowerToolbar.SuspendLayout();
			this.ArtifactPage.SuspendLayout();
			this.ArtifactContext.SuspendLayout();
			this.ArtifactToolbar.SuspendLayout();
			this.ChallengeContext.SuspendLayout();
			base.SuspendLayout();
			this.Splitter.Dock = DockStyle.Fill;
			this.Splitter.FixedPanel = FixedPanel.Panel1;
			this.Splitter.Location = new Point(0, 0);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.LibraryTree);
			this.Splitter.Panel1.Controls.Add(this.LibraryToolbar);
			this.Splitter.Panel1.Controls.Add(this.CompendiumBtn);
			this.Splitter.Panel1.Controls.Add(this.HelpBtn);
			this.Splitter.Panel2.Controls.Add(this.Pages);
			this.Splitter.Panel2.Controls.Add(this.HelpPanel);
			this.Splitter.Size = new Size(879, 431);
			this.Splitter.SplitterDistance = 249;
			this.Splitter.TabIndex = 0;
			this.LibraryTree.AllowDrop = true;
			this.LibraryTree.Dock = DockStyle.Fill;
			this.LibraryTree.FullRowSelect = true;
			this.LibraryTree.HideSelection = false;
			this.LibraryTree.Location = new Point(0, 25);
			this.LibraryTree.Name = "LibraryTree";
			this.LibraryTree.ShowPlusMinus = false;
			this.LibraryTree.ShowRootLines = false;
			this.LibraryTree.Size = new Size(249, 360);
			this.LibraryTree.TabIndex = 1;
			this.LibraryTree.DoubleClick += new EventHandler(this.LibraryEditBtn_Click);
			this.LibraryTree.DragDrop += new DragEventHandler(this.LibraryList_DragDrop);
			this.LibraryTree.AfterSelect += new TreeViewEventHandler(this.LibraryTree_AfterSelect);
			this.LibraryTree.ItemDrag += new ItemDragEventHandler(this.LibraryList_ItemDrag);
			this.LibraryTree.DragOver += new DragEventHandler(this.LibraryList_DragOver);
			this.LibraryToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.FileMenu,
				this.LibraryRemoveBtn,
				this.LibraryEditBtn,
				this.toolStripSeparator17,
				this.LibraryMergeBtn
			});
			this.LibraryToolbar.Location = new Point(0, 0);
			this.LibraryToolbar.Name = "LibraryToolbar";
			this.LibraryToolbar.Size = new Size(249, 25);
			this.LibraryToolbar.TabIndex = 0;
			this.LibraryToolbar.Text = "toolStrip1";
			this.FileMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FileMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.FileNew,
				this.FileOpen,
				this.FileClose
			});
			this.FileMenu.Image = (Image)componentResourceManager.GetObject("FileMenu.Image");
			this.FileMenu.ImageTransparentColor = Color.Magenta;
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new Size(38, 22);
			this.FileMenu.Text = "File";
			this.FileNew.Name = "FileNew";
			this.FileNew.Size = new Size(183, 22);
			this.FileNew.Text = "Create New Library...";
			this.FileNew.Click += new EventHandler(this.FileNew_Click);
			this.FileOpen.Name = "FileOpen";
			this.FileOpen.Size = new Size(183, 22);
			this.FileOpen.Text = "Open Library...";
			this.FileOpen.Click += new EventHandler(this.FileOpen_Click);
			this.FileClose.Name = "FileClose";
			this.FileClose.Size = new Size(183, 22);
			this.FileClose.Text = "Close All Libraries";
			this.FileClose.Click += new EventHandler(this.FileClose_Click);
			this.LibraryRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LibraryRemoveBtn.Image = (Image)componentResourceManager.GetObject("LibraryRemoveBtn.Image");
			this.LibraryRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.LibraryRemoveBtn.Name = "LibraryRemoveBtn";
			this.LibraryRemoveBtn.Size = new Size(54, 22);
			this.LibraryRemoveBtn.Text = "Remove";
			this.LibraryRemoveBtn.Click += new EventHandler(this.LibraryRemoveBtn_Click);
			this.LibraryEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LibraryEditBtn.Image = (Image)componentResourceManager.GetObject("LibraryEditBtn.Image");
			this.LibraryEditBtn.ImageTransparentColor = Color.Magenta;
			this.LibraryEditBtn.Name = "LibraryEditBtn";
			this.LibraryEditBtn.Size = new Size(31, 22);
			this.LibraryEditBtn.Text = "Edit";
			this.LibraryEditBtn.Click += new EventHandler(this.LibraryEditBtn_Click);
			this.toolStripSeparator17.Name = "toolStripSeparator17";
			this.toolStripSeparator17.Size = new Size(6, 25);
			this.LibraryMergeBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LibraryMergeBtn.Image = (Image)componentResourceManager.GetObject("LibraryMergeBtn.Image");
			this.LibraryMergeBtn.ImageTransparentColor = Color.Magenta;
			this.LibraryMergeBtn.Name = "LibraryMergeBtn";
			this.LibraryMergeBtn.Size = new Size(45, 22);
			this.LibraryMergeBtn.Text = "Merge";
			this.LibraryMergeBtn.Click += new EventHandler(this.LibraryMergeBtn_Click);
			this.CompendiumBtn.Dock = DockStyle.Bottom;
			this.CompendiumBtn.Location = new Point(0, 385);
			this.CompendiumBtn.Name = "CompendiumBtn";
			this.CompendiumBtn.Size = new Size(249, 23);
			this.CompendiumBtn.TabIndex = 2;
			this.CompendiumBtn.Text = "Download Compendium Items";
			this.CompendiumBtn.UseVisualStyleBackColor = true;
			this.CompendiumBtn.Click += new EventHandler(this.CompendiumBtn_Click);
			this.HelpBtn.Dock = DockStyle.Bottom;
			this.HelpBtn.Location = new Point(0, 408);
			this.HelpBtn.Name = "HelpBtn";
			this.HelpBtn.Size = new Size(249, 23);
			this.HelpBtn.TabIndex = 3;
			this.HelpBtn.Text = "Show Help";
			this.HelpBtn.UseVisualStyleBackColor = true;
			this.HelpBtn.Click += new EventHandler(this.HelpBtn_Click);
			this.Pages.Controls.Add(this.CreaturesPage);
			this.Pages.Controls.Add(this.TemplatesPage);
			this.Pages.Controls.Add(this.TrapsPage);
			this.Pages.Controls.Add(this.ChallengePage);
			this.Pages.Controls.Add(this.MagicItemsPage);
			this.Pages.Controls.Add(this.TilesPage);
			this.Pages.Controls.Add(this.TerrainPowersPage);
			this.Pages.Controls.Add(this.ArtifactPage);
			this.Pages.Dock = DockStyle.Fill;
			this.Pages.Location = new Point(0, 0);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(626, 272);
			this.Pages.TabIndex = 2;
			this.Pages.SelectedIndexChanged += new EventHandler(this.Pages_SelectedIndexChanged);
			this.CreaturesPage.Controls.Add(this.CreatureList);
			this.CreaturesPage.Controls.Add(this.CreatureSearchToolbar);
			this.CreaturesPage.Controls.Add(this.CreatureToolbar);
			this.CreaturesPage.Location = new Point(4, 22);
			this.CreaturesPage.Name = "CreaturesPage";
			this.CreaturesPage.Padding = new Padding(3);
			this.CreaturesPage.Size = new Size(618, 246);
			this.CreaturesPage.TabIndex = 0;
			this.CreaturesPage.Text = "Creatures";
			this.CreaturesPage.UseVisualStyleBackColor = true;
			this.CreatureList.Columns.AddRange(new ColumnHeader[]
			{
				this.CreatureNameHdr,
				this.CreatureInfoHdr
			});
			this.CreatureList.ContextMenuStrip = this.CreatureContext;
			this.CreatureList.Dock = DockStyle.Fill;
			this.CreatureList.FullRowSelect = true;
			this.CreatureList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.CreatureList.HideSelection = false;
			this.CreatureList.Location = new Point(3, 53);
			this.CreatureList.Name = "CreatureList";
			this.CreatureList.Size = new Size(612, 190);
			this.CreatureList.Sorting = SortOrder.Ascending;
			this.CreatureList.TabIndex = 1;
			this.CreatureList.UseCompatibleStateImageBehavior = false;
			this.CreatureList.View = View.Details;
			this.CreatureList.DoubleClick += new EventHandler(this.OppEditBtn_Click);
			this.CreatureList.ItemDrag += new ItemDragEventHandler(this.OppList_ItemDrag);
			this.CreatureNameHdr.Text = "Creature";
			this.CreatureNameHdr.Width = 300;
			this.CreatureInfoHdr.Text = "Info";
			this.CreatureInfoHdr.Width = 150;
			this.CreatureContext.Items.AddRange(new ToolStripItem[]
			{
				this.CreatureContextRemove,
				this.CreatureContextCategory
			});
			this.CreatureContext.Name = "CreatureContext";
			this.CreatureContext.Size = new Size(151, 48);
			this.CreatureContextRemove.Name = "CreatureContextRemove";
			this.CreatureContextRemove.Size = new Size(150, 22);
			this.CreatureContextRemove.Text = "Remove";
			this.CreatureContextRemove.Click += new EventHandler(this.CreatureContextRemove_Click);
			this.CreatureContextCategory.Name = "CreatureContextCategory";
			this.CreatureContextCategory.Size = new Size(150, 22);
			this.CreatureContextCategory.Text = "Set Category...";
			this.CreatureContextCategory.Click += new EventHandler(this.CreatureContextCategory_Click);
			this.CreatureSearchToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.SearchLbl,
				this.SearchBox,
				this.toolStripSeparator11,
				this.CategorisedBtn,
				this.UncategorisedBtn
			});
			this.CreatureSearchToolbar.Location = new Point(3, 28);
			this.CreatureSearchToolbar.Name = "CreatureSearchToolbar";
			this.CreatureSearchToolbar.Size = new Size(612, 25);
			this.CreatureSearchToolbar.TabIndex = 2;
			this.CreatureSearchToolbar.Text = "toolStrip1";
			this.SearchLbl.Name = "SearchLbl";
			this.SearchLbl.Size = new Size(45, 22);
			this.SearchLbl.Text = "Search:";
			this.SearchBox.BorderStyle = BorderStyle.FixedSingle;
			this.SearchBox.Name = "SearchBox";
			this.SearchBox.Size = new Size(150, 25);
			this.SearchBox.TextChanged += new EventHandler(this.SearchBox_TextChanged);
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new Size(6, 25);
			this.CategorisedBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CategorisedBtn.Image = (Image)componentResourceManager.GetObject("CategorisedBtn.Image");
			this.CategorisedBtn.ImageTransparentColor = Color.Magenta;
			this.CategorisedBtn.Name = "CategorisedBtn";
			this.CategorisedBtn.Size = new Size(74, 22);
			this.CategorisedBtn.Text = "Categorised";
			this.CategorisedBtn.Click += new EventHandler(this.CreatureFilterCategorised_Click);
			this.UncategorisedBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.UncategorisedBtn.Image = (Image)componentResourceManager.GetObject("UncategorisedBtn.Image");
			this.UncategorisedBtn.ImageTransparentColor = Color.Magenta;
			this.UncategorisedBtn.Name = "UncategorisedBtn";
			this.UncategorisedBtn.Size = new Size(87, 22);
			this.UncategorisedBtn.Text = "Uncategorised";
			this.UncategorisedBtn.Click += new EventHandler(this.CreatureFilterUncategorised_Click);
			this.CreatureToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.CreatureAddBtn,
				this.OppRemoveBtn,
				this.OppEditBtn,
				this.toolStripSeparator1,
				this.CreatureCutBtn,
				this.CreatureCopyBtn,
				this.CreaturePasteBtn,
				this.toolStripSeparator4,
				this.CreatureStatBlockBtn,
				this.toolStripSeparator10,
				this.CreatureTools
			});
			this.CreatureToolbar.Location = new Point(3, 3);
			this.CreatureToolbar.Name = "CreatureToolbar";
			this.CreatureToolbar.Size = new Size(612, 25);
			this.CreatureToolbar.TabIndex = 0;
			this.CreatureToolbar.Text = "toolStrip2";
			this.CreatureAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CreatureAddBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.CreatureAddSingle,
				this.toolStripSeparator19,
				this.CreatureImport
			});
			this.CreatureAddBtn.Image = (Image)componentResourceManager.GetObject("CreatureAddBtn.Image");
			this.CreatureAddBtn.ImageTransparentColor = Color.Magenta;
			this.CreatureAddBtn.Name = "CreatureAddBtn";
			this.CreatureAddBtn.Size = new Size(42, 22);
			this.CreatureAddBtn.Text = "Add";
			this.CreatureAddSingle.Name = "CreatureAddSingle";
			this.CreatureAddSingle.Size = new Size(162, 22);
			this.CreatureAddSingle.Text = "Add a Creature...";
			this.CreatureAddSingle.Click += new EventHandler(this.CreatureAddBtn_Click);
			this.toolStripSeparator19.Name = "toolStripSeparator19";
			this.toolStripSeparator19.Size = new Size(159, 6);
			this.CreatureImport.Name = "CreatureImport";
			this.CreatureImport.Size = new Size(162, 22);
			this.CreatureImport.Text = "Import...";
			this.CreatureImport.Click += new EventHandler(this.CreatureImport_Click);
			this.OppRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.OppRemoveBtn.Image = (Image)componentResourceManager.GetObject("OppRemoveBtn.Image");
			this.OppRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.OppRemoveBtn.Name = "OppRemoveBtn";
			this.OppRemoveBtn.Size = new Size(54, 22);
			this.OppRemoveBtn.Text = "Remove";
			this.OppRemoveBtn.Click += new EventHandler(this.OppRemoveBtn_Click);
			this.OppEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.OppEditBtn.Image = (Image)componentResourceManager.GetObject("OppEditBtn.Image");
			this.OppEditBtn.ImageTransparentColor = Color.Magenta;
			this.OppEditBtn.Name = "OppEditBtn";
			this.OppEditBtn.Size = new Size(31, 22);
			this.OppEditBtn.Text = "Edit";
			this.OppEditBtn.Click += new EventHandler(this.OppEditBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.CreatureCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CreatureCutBtn.Image = (Image)componentResourceManager.GetObject("CreatureCutBtn.Image");
			this.CreatureCutBtn.ImageTransparentColor = Color.Magenta;
			this.CreatureCutBtn.Name = "CreatureCutBtn";
			this.CreatureCutBtn.Size = new Size(30, 22);
			this.CreatureCutBtn.Text = "Cut";
			this.CreatureCutBtn.Click += new EventHandler(this.CreatureCutBtn_Click);
			this.CreatureCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CreatureCopyBtn.Image = (Image)componentResourceManager.GetObject("CreatureCopyBtn.Image");
			this.CreatureCopyBtn.ImageTransparentColor = Color.Magenta;
			this.CreatureCopyBtn.Name = "CreatureCopyBtn";
			this.CreatureCopyBtn.Size = new Size(39, 22);
			this.CreatureCopyBtn.Text = "Copy";
			this.CreatureCopyBtn.Click += new EventHandler(this.CreatureCopyBtn_Click);
			this.CreaturePasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CreaturePasteBtn.Image = (Image)componentResourceManager.GetObject("CreaturePasteBtn.Image");
			this.CreaturePasteBtn.ImageTransparentColor = Color.Magenta;
			this.CreaturePasteBtn.Name = "CreaturePasteBtn";
			this.CreaturePasteBtn.Size = new Size(39, 22);
			this.CreaturePasteBtn.Text = "Paste";
			this.CreaturePasteBtn.Click += new EventHandler(this.CreaturePasteBtn_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new Size(6, 25);
			this.CreatureStatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CreatureStatBlockBtn.Image = (Image)componentResourceManager.GetObject("CreatureStatBlockBtn.Image");
			this.CreatureStatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.CreatureStatBlockBtn.Name = "CreatureStatBlockBtn";
			this.CreatureStatBlockBtn.Size = new Size(63, 22);
			this.CreatureStatBlockBtn.Text = "Stat Block";
			this.CreatureStatBlockBtn.Click += new EventHandler(this.CreatureStatBlockBtn_Click);
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new Size(6, 25);
			this.CreatureTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CreatureTools.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.CreatureToolsDemographics,
				this.CreatureToolsPowerStatistics,
				this.CreatureToolsFilterList,
				this.CreatureToolsExport
			});
			this.CreatureTools.Image = (Image)componentResourceManager.GetObject("CreatureTools.Image");
			this.CreatureTools.ImageTransparentColor = Color.Magenta;
			this.CreatureTools.Name = "CreatureTools";
			this.CreatureTools.Size = new Size(49, 22);
			this.CreatureTools.Text = "Tools";
			this.CreatureToolsDemographics.Name = "CreatureToolsDemographics";
			this.CreatureToolsDemographics.Size = new Size(165, 22);
			this.CreatureToolsDemographics.Text = "Demographics";
			this.CreatureToolsDemographics.Click += new EventHandler(this.CreatureDemoBtn_Click);
			this.CreatureToolsPowerStatistics.Name = "CreatureToolsPowerStatistics";
			this.CreatureToolsPowerStatistics.Size = new Size(165, 22);
			this.CreatureToolsPowerStatistics.Text = "Power Statistics...";
			this.CreatureToolsPowerStatistics.Click += new EventHandler(this.PowerStatsBtn_Click);
			this.CreatureToolsFilterList.Name = "CreatureToolsFilterList";
			this.CreatureToolsFilterList.Size = new Size(165, 22);
			this.CreatureToolsFilterList.Text = "Filter List";
			this.CreatureToolsFilterList.Click += new EventHandler(this.FilterBtn_Click);
			this.CreatureToolsExport.Name = "CreatureToolsExport";
			this.CreatureToolsExport.Size = new Size(165, 22);
			this.CreatureToolsExport.Text = "Export...";
			this.CreatureToolsExport.Click += new EventHandler(this.CreatureToolsExport_Click);
			this.TemplatesPage.Controls.Add(this.TemplateList);
			this.TemplatesPage.Controls.Add(this.TemplateToolbar);
			this.TemplatesPage.Location = new Point(4, 22);
			this.TemplatesPage.Name = "TemplatesPage";
			this.TemplatesPage.Padding = new Padding(3);
			this.TemplatesPage.Size = new Size(618, 246);
			this.TemplatesPage.TabIndex = 1;
			this.TemplatesPage.Text = "Templates";
			this.TemplatesPage.UseVisualStyleBackColor = true;
			this.TemplateList.Columns.AddRange(new ColumnHeader[]
			{
				this.TemplateNameHdr,
				this.TemplateInfoHdr
			});
			this.TemplateList.ContextMenuStrip = this.TemplateContext;
			this.TemplateList.Dock = DockStyle.Fill;
			this.TemplateList.FullRowSelect = true;
			listViewGroup.Header = "Functional Templates";
			listViewGroup.Name = "FunctionalGroup";
			listViewGroup2.Header = "Class Templates";
			listViewGroup2.Name = "ClassGroup";
			listViewGroup3.Header = "Themes";
			listViewGroup3.Name = "ThemeGroup";
			this.TemplateList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2,
				listViewGroup3
			});
			this.TemplateList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TemplateList.HideSelection = false;
			this.TemplateList.Location = new Point(3, 28);
			this.TemplateList.Name = "TemplateList";
			this.TemplateList.Size = new Size(612, 215);
			this.TemplateList.Sorting = SortOrder.Ascending;
			this.TemplateList.TabIndex = 2;
			this.TemplateList.UseCompatibleStateImageBehavior = false;
			this.TemplateList.View = View.Details;
			this.TemplateList.DoubleClick += new EventHandler(this.TemplateEditBtn_Click);
			this.TemplateList.ItemDrag += new ItemDragEventHandler(this.TemplateList_ItemDrag);
			this.TemplateNameHdr.Text = "Template";
			this.TemplateNameHdr.Width = 300;
			this.TemplateInfoHdr.Text = "Role";
			this.TemplateInfoHdr.Width = 150;
			this.TemplateContext.Items.AddRange(new ToolStripItem[]
			{
				this.TemplateContextRemove,
				this.TemplateContextType
			});
			this.TemplateContext.Name = "TemplateContext";
			this.TemplateContext.Size = new Size(118, 48);
			this.TemplateContextRemove.Name = "TemplateContextRemove";
			this.TemplateContextRemove.Size = new Size(117, 22);
			this.TemplateContextRemove.Text = "Remove";
			this.TemplateContextRemove.Click += new EventHandler(this.TemplateContextRemove_Click);
			this.TemplateContextType.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.TemplateFunctional,
				this.TemplateClass
			});
			this.TemplateContextType.Name = "TemplateContextType";
			this.TemplateContextType.Size = new Size(117, 22);
			this.TemplateContextType.Text = "Type";
			this.TemplateFunctional.Name = "TemplateFunctional";
			this.TemplateFunctional.Size = new Size(130, 22);
			this.TemplateFunctional.Text = "Functional";
			this.TemplateFunctional.Click += new EventHandler(this.TemplateFunctional_Click);
			this.TemplateClass.Name = "TemplateClass";
			this.TemplateClass.Size = new Size(130, 22);
			this.TemplateClass.Text = "Class";
			this.TemplateClass.Click += new EventHandler(this.TemplateClass_Click);
			this.TemplateToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.TemplateAddBtn,
				this.TemplateRemoveBtn,
				this.TemplateEditBtn,
				this.toolStripSeparator2,
				this.TemplateCutBtn,
				this.TemplateCopyBtn,
				this.TemplatePasteBtn,
				this.toolStripSeparator18,
				this.TemplateStatBlock,
				this.toolStripSeparator21,
				this.TemplateTools
			});
			this.TemplateToolbar.Location = new Point(3, 3);
			this.TemplateToolbar.Name = "TemplateToolbar";
			this.TemplateToolbar.Size = new Size(612, 25);
			this.TemplateToolbar.TabIndex = 1;
			this.TemplateToolbar.Text = "toolStrip2";
			this.TemplateAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TemplateAddBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.addTemplateToolStripMenuItem,
				this.TemplateAddTheme,
				this.toolStripSeparator20,
				this.TemplateImport
			});
			this.TemplateAddBtn.Image = (Image)componentResourceManager.GetObject("TemplateAddBtn.Image");
			this.TemplateAddBtn.ImageTransparentColor = Color.Magenta;
			this.TemplateAddBtn.Name = "TemplateAddBtn";
			this.TemplateAddBtn.Size = new Size(42, 22);
			this.TemplateAddBtn.Text = "Add";
			this.addTemplateToolStripMenuItem.Name = "addTemplateToolStripMenuItem";
			this.addTemplateToolStripMenuItem.Size = new Size(167, 22);
			this.addTemplateToolStripMenuItem.Text = "Add a Template...";
			this.addTemplateToolStripMenuItem.Click += new EventHandler(this.TemplateAddBtn_Click);
			this.TemplateAddTheme.Name = "TemplateAddTheme";
			this.TemplateAddTheme.Size = new Size(167, 22);
			this.TemplateAddTheme.Text = "Add a Theme...";
			this.TemplateAddTheme.Click += new EventHandler(this.TemplateAddTheme_Click);
			this.toolStripSeparator20.Name = "toolStripSeparator20";
			this.toolStripSeparator20.Size = new Size(164, 6);
			this.TemplateImport.Name = "TemplateImport";
			this.TemplateImport.Size = new Size(167, 22);
			this.TemplateImport.Text = "Import...";
			this.TemplateImport.Click += new EventHandler(this.TemplateImport_Click);
			this.TemplateRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TemplateRemoveBtn.Image = (Image)componentResourceManager.GetObject("TemplateRemoveBtn.Image");
			this.TemplateRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.TemplateRemoveBtn.Name = "TemplateRemoveBtn";
			this.TemplateRemoveBtn.Size = new Size(54, 22);
			this.TemplateRemoveBtn.Text = "Remove";
			this.TemplateRemoveBtn.Click += new EventHandler(this.TemplateRemoveBtn_Click);
			this.TemplateEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TemplateEditBtn.Image = (Image)componentResourceManager.GetObject("TemplateEditBtn.Image");
			this.TemplateEditBtn.ImageTransparentColor = Color.Magenta;
			this.TemplateEditBtn.Name = "TemplateEditBtn";
			this.TemplateEditBtn.Size = new Size(31, 22);
			this.TemplateEditBtn.Text = "Edit";
			this.TemplateEditBtn.Click += new EventHandler(this.TemplateEditBtn_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 25);
			this.TemplateCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TemplateCutBtn.Image = (Image)componentResourceManager.GetObject("TemplateCutBtn.Image");
			this.TemplateCutBtn.ImageTransparentColor = Color.Magenta;
			this.TemplateCutBtn.Name = "TemplateCutBtn";
			this.TemplateCutBtn.Size = new Size(30, 22);
			this.TemplateCutBtn.Text = "Cut";
			this.TemplateCutBtn.Click += new EventHandler(this.TemplateCutBtn_Click);
			this.TemplateCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TemplateCopyBtn.Image = (Image)componentResourceManager.GetObject("TemplateCopyBtn.Image");
			this.TemplateCopyBtn.ImageTransparentColor = Color.Magenta;
			this.TemplateCopyBtn.Name = "TemplateCopyBtn";
			this.TemplateCopyBtn.Size = new Size(39, 22);
			this.TemplateCopyBtn.Text = "Copy";
			this.TemplateCopyBtn.Click += new EventHandler(this.TemplateCopyBtn_Click);
			this.TemplatePasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TemplatePasteBtn.Image = (Image)componentResourceManager.GetObject("TemplatePasteBtn.Image");
			this.TemplatePasteBtn.ImageTransparentColor = Color.Magenta;
			this.TemplatePasteBtn.Name = "TemplatePasteBtn";
			this.TemplatePasteBtn.Size = new Size(39, 22);
			this.TemplatePasteBtn.Text = "Paste";
			this.TemplatePasteBtn.Click += new EventHandler(this.TemplatePasteBtn_Click);
			this.toolStripSeparator18.Name = "toolStripSeparator18";
			this.toolStripSeparator18.Size = new Size(6, 25);
			this.TemplateStatBlock.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TemplateStatBlock.Image = (Image)componentResourceManager.GetObject("TemplateStatBlock.Image");
			this.TemplateStatBlock.ImageTransparentColor = Color.Magenta;
			this.TemplateStatBlock.Name = "TemplateStatBlock";
			this.TemplateStatBlock.Size = new Size(63, 22);
			this.TemplateStatBlock.Text = "Stat Block";
			this.TemplateStatBlock.Click += new EventHandler(this.TemplateStatBlock_Click);
			this.toolStripSeparator21.Name = "toolStripSeparator21";
			this.toolStripSeparator21.Size = new Size(6, 25);
			this.TemplateTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TemplateTools.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.TemplateToolsExport
			});
			this.TemplateTools.Image = (Image)componentResourceManager.GetObject("TemplateTools.Image");
			this.TemplateTools.ImageTransparentColor = Color.Magenta;
			this.TemplateTools.Name = "TemplateTools";
			this.TemplateTools.Size = new Size(49, 22);
			this.TemplateTools.Text = "Tools";
			this.TemplateToolsExport.Name = "TemplateToolsExport";
			this.TemplateToolsExport.Size = new Size(116, 22);
			this.TemplateToolsExport.Text = "Export...";
			this.TemplateToolsExport.Click += new EventHandler(this.TemplateToolsExport_Click);
			this.TrapsPage.Controls.Add(this.TrapList);
			this.TrapsPage.Controls.Add(this.TrapToolbar);
			this.TrapsPage.Location = new Point(4, 22);
			this.TrapsPage.Name = "TrapsPage";
			this.TrapsPage.Padding = new Padding(3);
			this.TrapsPage.Size = new Size(618, 246);
			this.TrapsPage.TabIndex = 3;
			this.TrapsPage.Text = "Traps / Hazards";
			this.TrapsPage.UseVisualStyleBackColor = true;
			this.TrapList.Columns.AddRange(new ColumnHeader[]
			{
				this.TrapNameHdr,
				this.TrapInfoHdr
			});
			this.TrapList.ContextMenuStrip = this.TrapContext;
			this.TrapList.Dock = DockStyle.Fill;
			this.TrapList.FullRowSelect = true;
			listViewGroup4.Header = "Traps";
			listViewGroup4.Name = "TrapGroup";
			listViewGroup5.Header = "Hazards";
			listViewGroup5.Name = "HazardGroup";
			this.TrapList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup4,
				listViewGroup5
			});
			this.TrapList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TrapList.HideSelection = false;
			this.TrapList.Location = new Point(3, 28);
			this.TrapList.Name = "TrapList";
			this.TrapList.Size = new Size(612, 215);
			this.TrapList.Sorting = SortOrder.Ascending;
			this.TrapList.TabIndex = 4;
			this.TrapList.UseCompatibleStateImageBehavior = false;
			this.TrapList.View = View.Details;
			this.TrapList.DoubleClick += new EventHandler(this.TrapEditBtn_Click);
			this.TrapList.ItemDrag += new ItemDragEventHandler(this.TrapList_ItemDrag);
			this.TrapNameHdr.Text = "Trap";
			this.TrapNameHdr.Width = 300;
			this.TrapInfoHdr.Text = "Role";
			this.TrapInfoHdr.Width = 150;
			this.TrapContext.Items.AddRange(new ToolStripItem[]
			{
				this.TrapContextRemove,
				this.TrapContextType
			});
			this.TrapContext.Name = "TrapContext";
			this.TrapContext.Size = new Size(118, 48);
			this.TrapContextRemove.Name = "TrapContextRemove";
			this.TrapContextRemove.Size = new Size(117, 22);
			this.TrapContextRemove.Text = "Remove";
			this.TrapContextRemove.Click += new EventHandler(this.TrapContextRemove_Click);
			this.TrapContextType.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.TrapTrap,
				this.TrapHazard
			});
			this.TrapContextType.Name = "TrapContextType";
			this.TrapContextType.Size = new Size(117, 22);
			this.TrapContextType.Text = "Type";
			this.TrapTrap.Name = "TrapTrap";
			this.TrapTrap.Size = new Size(111, 22);
			this.TrapTrap.Text = "Trap";
			this.TrapTrap.Click += new EventHandler(this.TrapTrap_Click);
			this.TrapHazard.Name = "TrapHazard";
			this.TrapHazard.Size = new Size(111, 22);
			this.TrapHazard.Text = "Hazard";
			this.TrapHazard.Click += new EventHandler(this.TrapHazard_Click);
			this.TrapToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.TrapAdd,
				this.TrapRemoveBtn,
				this.TrapEditBtn,
				this.toolStripSeparator6,
				this.TrapCutBtn,
				this.TrapCopyBtn,
				this.TrapPasteBtn,
				this.toolStripSeparator8,
				this.TrapStatBlockBtn,
				this.toolStripSeparator13,
				this.TrapTools
			});
			this.TrapToolbar.Location = new Point(3, 3);
			this.TrapToolbar.Name = "TrapToolbar";
			this.TrapToolbar.Size = new Size(612, 25);
			this.TrapToolbar.TabIndex = 3;
			this.TrapToolbar.Text = "toolStrip2";
			this.TrapAdd.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapAdd.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.TrapAddAdd,
				this.toolStripSeparator25,
				this.TrapAddImport
			});
			this.TrapAdd.Image = (Image)componentResourceManager.GetObject("TrapAdd.Image");
			this.TrapAdd.ImageTransparentColor = Color.Magenta;
			this.TrapAdd.Name = "TrapAdd";
			this.TrapAdd.Size = new Size(42, 22);
			this.TrapAdd.Text = "Add";
			this.TrapAddAdd.Name = "TrapAddAdd";
			this.TrapAddAdd.Size = new Size(141, 22);
			this.TrapAddAdd.Text = "Add a Trap...";
			this.TrapAddAdd.Click += new EventHandler(this.TrapAddBtn_Click);
			this.toolStripSeparator25.Name = "toolStripSeparator25";
			this.toolStripSeparator25.Size = new Size(138, 6);
			this.TrapAddImport.Name = "TrapAddImport";
			this.TrapAddImport.Size = new Size(141, 22);
			this.TrapAddImport.Text = "Import...";
			this.TrapAddImport.Click += new EventHandler(this.TrapAddImport_Click);
			this.TrapRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapRemoveBtn.Image = (Image)componentResourceManager.GetObject("TrapRemoveBtn.Image");
			this.TrapRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.TrapRemoveBtn.Name = "TrapRemoveBtn";
			this.TrapRemoveBtn.Size = new Size(54, 22);
			this.TrapRemoveBtn.Text = "Remove";
			this.TrapRemoveBtn.Click += new EventHandler(this.TrapRemoveBtn_Click);
			this.TrapEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapEditBtn.Image = (Image)componentResourceManager.GetObject("TrapEditBtn.Image");
			this.TrapEditBtn.ImageTransparentColor = Color.Magenta;
			this.TrapEditBtn.Name = "TrapEditBtn";
			this.TrapEditBtn.Size = new Size(31, 22);
			this.TrapEditBtn.Text = "Edit";
			this.TrapEditBtn.Click += new EventHandler(this.TrapEditBtn_Click);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new Size(6, 25);
			this.TrapCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapCutBtn.Image = (Image)componentResourceManager.GetObject("TrapCutBtn.Image");
			this.TrapCutBtn.ImageTransparentColor = Color.Magenta;
			this.TrapCutBtn.Name = "TrapCutBtn";
			this.TrapCutBtn.Size = new Size(30, 22);
			this.TrapCutBtn.Text = "Cut";
			this.TrapCutBtn.Click += new EventHandler(this.TrapCutBtn_Click);
			this.TrapCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapCopyBtn.Image = (Image)componentResourceManager.GetObject("TrapCopyBtn.Image");
			this.TrapCopyBtn.ImageTransparentColor = Color.Magenta;
			this.TrapCopyBtn.Name = "TrapCopyBtn";
			this.TrapCopyBtn.Size = new Size(39, 22);
			this.TrapCopyBtn.Text = "Copy";
			this.TrapCopyBtn.Click += new EventHandler(this.TrapCopyBtn_Click);
			this.TrapPasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapPasteBtn.Image = (Image)componentResourceManager.GetObject("TrapPasteBtn.Image");
			this.TrapPasteBtn.ImageTransparentColor = Color.Magenta;
			this.TrapPasteBtn.Name = "TrapPasteBtn";
			this.TrapPasteBtn.Size = new Size(39, 22);
			this.TrapPasteBtn.Text = "Paste";
			this.TrapPasteBtn.Click += new EventHandler(this.TrapPasteBtn_Click);
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new Size(6, 25);
			this.TrapStatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapStatBlockBtn.Image = (Image)componentResourceManager.GetObject("TrapStatBlockBtn.Image");
			this.TrapStatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.TrapStatBlockBtn.Name = "TrapStatBlockBtn";
			this.TrapStatBlockBtn.Size = new Size(63, 22);
			this.TrapStatBlockBtn.Text = "Stat Block";
			this.TrapStatBlockBtn.Click += new EventHandler(this.TrapStatBlockBtn_Click);
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new Size(6, 25);
			this.TrapTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapTools.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.TrapToolsDemographics,
				this.TrapToolsExport
			});
			this.TrapTools.Image = (Image)componentResourceManager.GetObject("TrapTools.Image");
			this.TrapTools.ImageTransparentColor = Color.Magenta;
			this.TrapTools.Name = "TrapTools";
			this.TrapTools.Size = new Size(49, 22);
			this.TrapTools.Text = "Tools";
			this.TrapToolsDemographics.Name = "TrapToolsDemographics";
			this.TrapToolsDemographics.Size = new Size(151, 22);
			this.TrapToolsDemographics.Text = "Demographics";
			this.TrapToolsDemographics.Click += new EventHandler(this.TrapDemoBtn_Click);
			this.TrapToolsExport.Name = "TrapToolsExport";
			this.TrapToolsExport.Size = new Size(151, 22);
			this.TrapToolsExport.Text = "Export...";
			this.TrapToolsExport.Click += new EventHandler(this.TrapToolsExport_Click);
			this.ChallengePage.Controls.Add(this.ChallengeList);
			this.ChallengePage.Controls.Add(this.ChallengeToolbar);
			this.ChallengePage.Location = new Point(4, 22);
			this.ChallengePage.Name = "ChallengePage";
			this.ChallengePage.Padding = new Padding(3);
			this.ChallengePage.Size = new Size(618, 246);
			this.ChallengePage.TabIndex = 4;
			this.ChallengePage.Text = "Skill Challenges";
			this.ChallengePage.UseVisualStyleBackColor = true;
			this.ChallengeList.Columns.AddRange(new ColumnHeader[]
			{
				this.ChallengeNameHdr,
				this.ChallengeInfoHdr
			});
			this.ChallengeList.Dock = DockStyle.Fill;
			this.ChallengeList.FullRowSelect = true;
			listViewGroup6.Header = "Traps";
			listViewGroup6.Name = "TrapGroup";
			listViewGroup7.Header = "Hazards";
			listViewGroup7.Name = "HazardGroup";
			this.ChallengeList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup6,
				listViewGroup7
			});
			this.ChallengeList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.ChallengeList.HideSelection = false;
			this.ChallengeList.Location = new Point(3, 28);
			this.ChallengeList.Name = "ChallengeList";
			this.ChallengeList.Size = new Size(612, 215);
			this.ChallengeList.Sorting = SortOrder.Ascending;
			this.ChallengeList.TabIndex = 6;
			this.ChallengeList.UseCompatibleStateImageBehavior = false;
			this.ChallengeList.View = View.Details;
			this.ChallengeList.DoubleClick += new EventHandler(this.ChallengeEditBtn_Click);
			this.ChallengeList.ItemDrag += new ItemDragEventHandler(this.ChallengeList_ItemDrag);
			this.ChallengeNameHdr.Text = "Challenge";
			this.ChallengeNameHdr.Width = 300;
			this.ChallengeInfoHdr.Text = "Complexity";
			this.ChallengeInfoHdr.Width = 150;
			this.ChallengeToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.ChallengeAdd,
				this.ChallengeRemoveBtn,
				this.ChallengeEditBtn,
				this.toolStripSeparator7,
				this.ChallengeCutBtn,
				this.ChallengeCopyBtn,
				this.ChallengePasteBtn,
				this.toolStripSeparator9,
				this.ChallengeStatBlockBtn,
				this.toolStripSeparator22,
				this.ChallengeTools
			});
			this.ChallengeToolbar.Location = new Point(3, 3);
			this.ChallengeToolbar.Name = "ChallengeToolbar";
			this.ChallengeToolbar.Size = new Size(612, 25);
			this.ChallengeToolbar.TabIndex = 5;
			this.ChallengeToolbar.Text = "toolStrip2";
			this.ChallengeAdd.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengeAdd.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ChallengeAddAdd,
				this.toolStripSeparator26,
				this.ChallengeAddImport
			});
			this.ChallengeAdd.Image = (Image)componentResourceManager.GetObject("ChallengeAdd.Image");
			this.ChallengeAdd.ImageTransparentColor = Color.Magenta;
			this.ChallengeAdd.Name = "ChallengeAdd";
			this.ChallengeAdd.Size = new Size(42, 22);
			this.ChallengeAdd.Text = "Add";
			this.ChallengeAddAdd.Name = "ChallengeAddAdd";
			this.ChallengeAddAdd.Size = new Size(194, 22);
			this.ChallengeAddAdd.Text = "Add a Skill Challenge...";
			this.ChallengeAddAdd.Click += new EventHandler(this.ChallengeAddBtn_Click);
			this.toolStripSeparator26.Name = "toolStripSeparator26";
			this.toolStripSeparator26.Size = new Size(191, 6);
			this.ChallengeAddImport.Name = "ChallengeAddImport";
			this.ChallengeAddImport.Size = new Size(194, 22);
			this.ChallengeAddImport.Text = "Import...";
			this.ChallengeAddImport.Click += new EventHandler(this.ChallengeAddImport_Click);
			this.ChallengeRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengeRemoveBtn.Image = (Image)componentResourceManager.GetObject("ChallengeRemoveBtn.Image");
			this.ChallengeRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.ChallengeRemoveBtn.Name = "ChallengeRemoveBtn";
			this.ChallengeRemoveBtn.Size = new Size(54, 22);
			this.ChallengeRemoveBtn.Text = "Remove";
			this.ChallengeRemoveBtn.Click += new EventHandler(this.ChallengeRemoveBtn_Click);
			this.ChallengeEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengeEditBtn.Image = (Image)componentResourceManager.GetObject("ChallengeEditBtn.Image");
			this.ChallengeEditBtn.ImageTransparentColor = Color.Magenta;
			this.ChallengeEditBtn.Name = "ChallengeEditBtn";
			this.ChallengeEditBtn.Size = new Size(31, 22);
			this.ChallengeEditBtn.Text = "Edit";
			this.ChallengeEditBtn.Click += new EventHandler(this.ChallengeEditBtn_Click);
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new Size(6, 25);
			this.ChallengeCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengeCutBtn.Image = (Image)componentResourceManager.GetObject("ChallengeCutBtn.Image");
			this.ChallengeCutBtn.ImageTransparentColor = Color.Magenta;
			this.ChallengeCutBtn.Name = "ChallengeCutBtn";
			this.ChallengeCutBtn.Size = new Size(30, 22);
			this.ChallengeCutBtn.Text = "Cut";
			this.ChallengeCutBtn.Click += new EventHandler(this.ChallengeCutBtn_Click);
			this.ChallengeCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengeCopyBtn.Image = (Image)componentResourceManager.GetObject("ChallengeCopyBtn.Image");
			this.ChallengeCopyBtn.ImageTransparentColor = Color.Magenta;
			this.ChallengeCopyBtn.Name = "ChallengeCopyBtn";
			this.ChallengeCopyBtn.Size = new Size(39, 22);
			this.ChallengeCopyBtn.Text = "Copy";
			this.ChallengeCopyBtn.Click += new EventHandler(this.ChallengeCopyBtn_Click);
			this.ChallengePasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengePasteBtn.Image = (Image)componentResourceManager.GetObject("ChallengePasteBtn.Image");
			this.ChallengePasteBtn.ImageTransparentColor = Color.Magenta;
			this.ChallengePasteBtn.Name = "ChallengePasteBtn";
			this.ChallengePasteBtn.Size = new Size(39, 22);
			this.ChallengePasteBtn.Text = "Paste";
			this.ChallengePasteBtn.Click += new EventHandler(this.ChallengePasteBtn_Click);
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new Size(6, 25);
			this.ChallengeStatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengeStatBlockBtn.Image = (Image)componentResourceManager.GetObject("ChallengeStatBlockBtn.Image");
			this.ChallengeStatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.ChallengeStatBlockBtn.Name = "ChallengeStatBlockBtn";
			this.ChallengeStatBlockBtn.Size = new Size(63, 22);
			this.ChallengeStatBlockBtn.Text = "Stat Block";
			this.ChallengeStatBlockBtn.Click += new EventHandler(this.ChallengeStatBlockBtn_Click);
			this.toolStripSeparator22.Name = "toolStripSeparator22";
			this.toolStripSeparator22.Size = new Size(6, 25);
			this.ChallengeTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengeTools.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ChallengeToolsExport
			});
			this.ChallengeTools.Image = (Image)componentResourceManager.GetObject("ChallengeTools.Image");
			this.ChallengeTools.ImageTransparentColor = Color.Magenta;
			this.ChallengeTools.Name = "ChallengeTools";
			this.ChallengeTools.Size = new Size(49, 22);
			this.ChallengeTools.Text = "Tools";
			this.ChallengeToolsExport.Name = "ChallengeToolsExport";
			this.ChallengeToolsExport.Size = new Size(116, 22);
			this.ChallengeToolsExport.Text = "Export...";
			this.ChallengeToolsExport.Click += new EventHandler(this.ChallengeToolsExport_Click);
			this.MagicItemsPage.Controls.Add(this.splitContainer1);
			this.MagicItemsPage.Location = new Point(4, 22);
			this.MagicItemsPage.Name = "MagicItemsPage";
			this.MagicItemsPage.Padding = new Padding(3);
			this.MagicItemsPage.Size = new Size(618, 246);
			this.MagicItemsPage.TabIndex = 6;
			this.MagicItemsPage.Text = "Magic Items";
			this.MagicItemsPage.UseVisualStyleBackColor = true;
			this.splitContainer1.Dock = DockStyle.Fill;
			this.splitContainer1.Location = new Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.MagicItemList);
			this.splitContainer1.Panel1.Controls.Add(this.MagicItemToolbar);
			this.splitContainer1.Panel2.Controls.Add(this.MagicItemVersionList);
			this.splitContainer1.Panel2.Controls.Add(this.MagicItemVersionToolbar);
			this.splitContainer1.Size = new Size(612, 240);
			this.splitContainer1.SplitterDistance = 309;
			this.splitContainer1.TabIndex = 7;
			this.MagicItemList.Columns.AddRange(new ColumnHeader[]
			{
				this.MagicItemHdr
			});
			this.MagicItemList.ContextMenuStrip = this.MagicItemContext;
			this.MagicItemList.Dock = DockStyle.Fill;
			this.MagicItemList.FullRowSelect = true;
			this.MagicItemList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.MagicItemList.HideSelection = false;
			this.MagicItemList.Location = new Point(0, 25);
			this.MagicItemList.MultiSelect = false;
			this.MagicItemList.Name = "MagicItemList";
			this.MagicItemList.Size = new Size(309, 215);
			this.MagicItemList.Sorting = SortOrder.Ascending;
			this.MagicItemList.TabIndex = 6;
			this.MagicItemList.UseCompatibleStateImageBehavior = false;
			this.MagicItemList.View = View.Details;
			this.MagicItemList.SelectedIndexChanged += new EventHandler(this.MagicItemList_SelectedIndexChanged);
			this.MagicItemHdr.Text = "Magic Item";
			this.MagicItemHdr.Width = 273;
			this.MagicItemContext.Items.AddRange(new ToolStripItem[]
			{
				this.MagicItemContextRemove
			});
			this.MagicItemContext.Name = "ChallengeContext";
			this.MagicItemContext.Size = new Size(118, 26);
			this.MagicItemContextRemove.Name = "MagicItemContextRemove";
			this.MagicItemContextRemove.Size = new Size(117, 22);
			this.MagicItemContextRemove.Text = "Remove";
			this.MagicItemContextRemove.Click += new EventHandler(this.MagicItemContextRemove_Click);
			this.MagicItemToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.MagicItemAdd,
				this.toolStripSeparator14,
				this.MagicItemTools
			});
			this.MagicItemToolbar.Location = new Point(0, 0);
			this.MagicItemToolbar.Name = "MagicItemToolbar";
			this.MagicItemToolbar.Size = new Size(309, 25);
			this.MagicItemToolbar.TabIndex = 5;
			this.MagicItemToolbar.Text = "toolStrip2";
			this.MagicItemAdd.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MagicItemAdd.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.MagicItemAddAdd,
				this.toolStripSeparator27,
				this.MagicItemAddImport
			});
			this.MagicItemAdd.Image = (Image)componentResourceManager.GetObject("MagicItemAdd.Image");
			this.MagicItemAdd.ImageTransparentColor = Color.Magenta;
			this.MagicItemAdd.Name = "MagicItemAdd";
			this.MagicItemAdd.Size = new Size(42, 22);
			this.MagicItemAdd.Text = "Add";
			this.MagicItemAddAdd.Name = "MagicItemAddAdd";
			this.MagicItemAddAdd.Size = new Size(177, 22);
			this.MagicItemAddAdd.Text = "Add a Magic Item...";
			this.MagicItemAddAdd.Click += new EventHandler(this.MagicItemAddBtn_Click);
			this.toolStripSeparator27.Name = "toolStripSeparator27";
			this.toolStripSeparator27.Size = new Size(174, 6);
			this.MagicItemAddImport.Name = "MagicItemAddImport";
			this.MagicItemAddImport.Size = new Size(177, 22);
			this.MagicItemAddImport.Text = "Import...";
			this.MagicItemAddImport.Click += new EventHandler(this.MagicItemAddImport_Click);
			this.toolStripSeparator14.Name = "toolStripSeparator14";
			this.toolStripSeparator14.Size = new Size(6, 25);
			this.MagicItemTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MagicItemTools.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.MagicItemToolsDemographics,
				this.MagicItemToolsExport
			});
			this.MagicItemTools.Image = (Image)componentResourceManager.GetObject("MagicItemTools.Image");
			this.MagicItemTools.ImageTransparentColor = Color.Magenta;
			this.MagicItemTools.Name = "MagicItemTools";
			this.MagicItemTools.Size = new Size(49, 22);
			this.MagicItemTools.Text = "Tools";
			this.MagicItemToolsDemographics.Name = "MagicItemToolsDemographics";
			this.MagicItemToolsDemographics.Size = new Size(151, 22);
			this.MagicItemToolsDemographics.Text = "Demographics";
			this.MagicItemToolsDemographics.Click += new EventHandler(this.MagicItemDemoBtn_Click);
			this.MagicItemToolsExport.Name = "MagicItemToolsExport";
			this.MagicItemToolsExport.Size = new Size(151, 22);
			this.MagicItemToolsExport.Text = "Export...";
			this.MagicItemToolsExport.Click += new EventHandler(this.MagicItemsToolsExport_Click);
			this.MagicItemVersionList.Columns.AddRange(new ColumnHeader[]
			{
				this.MagicItemInfoHdr
			});
			this.MagicItemVersionList.Dock = DockStyle.Fill;
			this.MagicItemVersionList.FullRowSelect = true;
			listViewGroup8.Header = "Heroic Tier";
			listViewGroup8.Name = "listViewGroup1";
			listViewGroup9.Header = "Paragon Tier";
			listViewGroup9.Name = "listViewGroup2";
			listViewGroup10.Header = "Epic Tier";
			listViewGroup10.Name = "listViewGroup3";
			this.MagicItemVersionList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup8,
				listViewGroup9,
				listViewGroup10
			});
			this.MagicItemVersionList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.MagicItemVersionList.HideSelection = false;
			this.MagicItemVersionList.Location = new Point(0, 25);
			this.MagicItemVersionList.Name = "MagicItemVersionList";
			this.MagicItemVersionList.Size = new Size(299, 215);
			this.MagicItemVersionList.TabIndex = 1;
			this.MagicItemVersionList.UseCompatibleStateImageBehavior = false;
			this.MagicItemVersionList.View = View.Details;
			this.MagicItemVersionList.DoubleClick += new EventHandler(this.MagicItemEditBtn_Click);
			this.MagicItemVersionList.ItemDrag += new ItemDragEventHandler(this.MagicItemList_ItemDrag);
			this.MagicItemInfoHdr.Text = "Version";
			this.MagicItemInfoHdr.Width = 250;
			this.MagicItemVersionToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.MagicItemRemoveBtn,
				this.toolStripSeparator5,
				this.MagicItemEditBtn,
				this.MagicItemCutBtn,
				this.MagicItemCopyBtn,
				this.MagicItemPasteBtn,
				this.toolStripSeparator12,
				this.MagicItemStatBlockBtn
			});
			this.MagicItemVersionToolbar.Location = new Point(0, 0);
			this.MagicItemVersionToolbar.Name = "MagicItemVersionToolbar";
			this.MagicItemVersionToolbar.Size = new Size(299, 25);
			this.MagicItemVersionToolbar.TabIndex = 0;
			this.MagicItemVersionToolbar.Text = "toolStrip1";
			this.MagicItemRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MagicItemRemoveBtn.Image = (Image)componentResourceManager.GetObject("MagicItemRemoveBtn.Image");
			this.MagicItemRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.MagicItemRemoveBtn.Name = "MagicItemRemoveBtn";
			this.MagicItemRemoveBtn.Size = new Size(54, 22);
			this.MagicItemRemoveBtn.Text = "Remove";
			this.MagicItemRemoveBtn.Click += new EventHandler(this.MagicItemRemoveBtn_Click);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new Size(6, 25);
			this.MagicItemEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MagicItemEditBtn.Image = (Image)componentResourceManager.GetObject("MagicItemEditBtn.Image");
			this.MagicItemEditBtn.ImageTransparentColor = Color.Magenta;
			this.MagicItemEditBtn.Name = "MagicItemEditBtn";
			this.MagicItemEditBtn.Size = new Size(31, 22);
			this.MagicItemEditBtn.Text = "Edit";
			this.MagicItemEditBtn.Click += new EventHandler(this.MagicItemEditBtn_Click);
			this.MagicItemCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MagicItemCutBtn.Image = (Image)componentResourceManager.GetObject("MagicItemCutBtn.Image");
			this.MagicItemCutBtn.ImageTransparentColor = Color.Magenta;
			this.MagicItemCutBtn.Name = "MagicItemCutBtn";
			this.MagicItemCutBtn.Size = new Size(30, 22);
			this.MagicItemCutBtn.Text = "Cut";
			this.MagicItemCutBtn.Click += new EventHandler(this.MagicItemCutBtn_Click);
			this.MagicItemCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MagicItemCopyBtn.Image = (Image)componentResourceManager.GetObject("MagicItemCopyBtn.Image");
			this.MagicItemCopyBtn.ImageTransparentColor = Color.Magenta;
			this.MagicItemCopyBtn.Name = "MagicItemCopyBtn";
			this.MagicItemCopyBtn.Size = new Size(39, 22);
			this.MagicItemCopyBtn.Text = "Copy";
			this.MagicItemCopyBtn.Click += new EventHandler(this.MagicItemCopyBtn_Click);
			this.MagicItemPasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MagicItemPasteBtn.Image = (Image)componentResourceManager.GetObject("MagicItemPasteBtn.Image");
			this.MagicItemPasteBtn.ImageTransparentColor = Color.Magenta;
			this.MagicItemPasteBtn.Name = "MagicItemPasteBtn";
			this.MagicItemPasteBtn.Size = new Size(39, 22);
			this.MagicItemPasteBtn.Text = "Paste";
			this.MagicItemPasteBtn.Click += new EventHandler(this.MagicItemPasteBtn_Click);
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new Size(6, 25);
			this.MagicItemStatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MagicItemStatBlockBtn.Image = (Image)componentResourceManager.GetObject("MagicItemStatBlockBtn.Image");
			this.MagicItemStatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.MagicItemStatBlockBtn.Name = "MagicItemStatBlockBtn";
			this.MagicItemStatBlockBtn.Size = new Size(63, 22);
			this.MagicItemStatBlockBtn.Text = "Stat Block";
			this.MagicItemStatBlockBtn.Click += new EventHandler(this.MagicItemStatBlockBtn_Click);
			this.TilesPage.Controls.Add(this.TileList);
			this.TilesPage.Controls.Add(this.TileToolbar);
			this.TilesPage.Location = new Point(4, 22);
			this.TilesPage.Name = "TilesPage";
			this.TilesPage.Padding = new Padding(3);
			this.TilesPage.Size = new Size(618, 246);
			this.TilesPage.TabIndex = 2;
			this.TilesPage.Text = "Map Tiles";
			this.TilesPage.UseVisualStyleBackColor = true;
			this.TileList.Columns.AddRange(new ColumnHeader[]
			{
				this.TileSetNameHdr
			});
			this.TileList.ContextMenuStrip = this.TileContext;
			this.TileList.Dock = DockStyle.Fill;
			this.TileList.FullRowSelect = true;
			this.TileList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TileList.HideSelection = false;
			this.TileList.Location = new Point(3, 28);
			this.TileList.Name = "TileList";
			this.TileList.Size = new Size(612, 215);
			this.TileList.Sorting = SortOrder.Ascending;
			this.TileList.TabIndex = 4;
			this.TileList.UseCompatibleStateImageBehavior = false;
			this.TileList.DoubleClick += new EventHandler(this.TileSetEditBtn_Click);
			this.TileList.ItemDrag += new ItemDragEventHandler(this.TileSetView_ItemDrag);
			this.TileSetNameHdr.Text = "Tile Set";
			this.TileSetNameHdr.Width = 299;
			this.TileContext.Items.AddRange(new ToolStripItem[]
			{
				this.TileContextRemove,
				this.TileContextCategory,
				this.TileContextSize
			});
			this.TileContext.Name = "TileContext";
			this.TileContext.Size = new Size(142, 70);
			this.TileContextRemove.Name = "TileContextRemove";
			this.TileContextRemove.Size = new Size(141, 22);
			this.TileContextRemove.Text = "Remove";
			this.TileContextRemove.Click += new EventHandler(this.TileContextRemove_Click);
			this.TileContextCategory.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.TilePlain,
				this.TileDoorway,
				this.TileStairway,
				this.TileFeature,
				this.toolStripSeparator15,
				this.TileSpecial,
				this.toolStripSeparator16,
				this.TileMap
			});
			this.TileContextCategory.Name = "TileContextCategory";
			this.TileContextCategory.Size = new Size(141, 22);
			this.TileContextCategory.Text = "Set Category";
			this.TilePlain.Name = "TilePlain";
			this.TilePlain.Size = new Size(130, 22);
			this.TilePlain.Text = "Plain Floor";
			this.TilePlain.Click += new EventHandler(this.TilePlain_Click);
			this.TileDoorway.Name = "TileDoorway";
			this.TileDoorway.Size = new Size(130, 22);
			this.TileDoorway.Text = "Doorway";
			this.TileDoorway.Click += new EventHandler(this.TileDoorway_Click);
			this.TileStairway.Name = "TileStairway";
			this.TileStairway.Size = new Size(130, 22);
			this.TileStairway.Text = "Stairway";
			this.TileStairway.Click += new EventHandler(this.TileStairway_Click);
			this.TileFeature.Name = "TileFeature";
			this.TileFeature.Size = new Size(130, 22);
			this.TileFeature.Text = "Feature";
			this.TileFeature.Click += new EventHandler(this.TileFeature_Click);
			this.toolStripSeparator15.Name = "toolStripSeparator15";
			this.toolStripSeparator15.Size = new Size(127, 6);
			this.TileSpecial.Name = "TileSpecial";
			this.TileSpecial.Size = new Size(130, 22);
			this.TileSpecial.Text = "Special";
			this.TileSpecial.Click += new EventHandler(this.TileSpecial_Click);
			this.toolStripSeparator16.Name = "toolStripSeparator16";
			this.toolStripSeparator16.Size = new Size(127, 6);
			this.TileMap.Name = "TileMap";
			this.TileMap.Size = new Size(130, 22);
			this.TileMap.Text = "Full Map";
			this.TileMap.Click += new EventHandler(this.TileMap_Click);
			this.TileContextSize.Name = "TileContextSize";
			this.TileContextSize.Size = new Size(141, 22);
			this.TileContextSize.Text = "Set Size...";
			this.TileContextSize.Click += new EventHandler(this.TileContextSize_Click);
			this.TileToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.TileAddBtn,
				this.TileRemoveBtn,
				this.TileEditBtn,
				this.toolStripSeparator3,
				this.TileCutBtn,
				this.TileCopyBtn,
				this.TilePasteBtn,
				this.toolStripSeparator23,
				this.TileTools
			});
			this.TileToolbar.Location = new Point(3, 3);
			this.TileToolbar.Name = "TileToolbar";
			this.TileToolbar.Size = new Size(612, 25);
			this.TileToolbar.TabIndex = 3;
			this.TileToolbar.Text = "toolStrip2";
			this.TileAddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TileAddBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.addTileToolStripMenuItem,
				this.toolStripSeparator24,
				this.TileAddImport,
				this.TileAddFolder
			});
			this.TileAddBtn.Image = (Image)componentResourceManager.GetObject("TileAddBtn.Image");
			this.TileAddBtn.ImageTransparentColor = Color.Magenta;
			this.TileAddBtn.Name = "TileAddBtn";
			this.TileAddBtn.Size = new Size(42, 22);
			this.TileAddBtn.Text = "Add";
			this.addTileToolStripMenuItem.Name = "addTileToolStripMenuItem";
			this.addTileToolStripMenuItem.Size = new Size(164, 22);
			this.addTileToolStripMenuItem.Text = "Add a Tile...";
			this.addTileToolStripMenuItem.Click += new EventHandler(this.TileAddBtn_Click);
			this.toolStripSeparator24.Name = "toolStripSeparator24";
			this.toolStripSeparator24.Size = new Size(161, 6);
			this.TileAddImport.Name = "TileAddImport";
			this.TileAddImport.Size = new Size(164, 22);
			this.TileAddImport.Text = "Import...";
			this.TileAddImport.Click += new EventHandler(this.TileAddImport_Click);
			this.TileAddFolder.Name = "TileAddFolder";
			this.TileAddFolder.Size = new Size(164, 22);
			this.TileAddFolder.Text = "Import a Folder...";
			this.TileAddFolder.Click += new EventHandler(this.TileAddFolderBtn_Click);
			this.TileRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TileRemoveBtn.Image = (Image)componentResourceManager.GetObject("TileRemoveBtn.Image");
			this.TileRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.TileRemoveBtn.Name = "TileRemoveBtn";
			this.TileRemoveBtn.Size = new Size(54, 22);
			this.TileRemoveBtn.Text = "Remove";
			this.TileRemoveBtn.Click += new EventHandler(this.TileSetRemoveBtn_Click);
			this.TileEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TileEditBtn.Image = (Image)componentResourceManager.GetObject("TileEditBtn.Image");
			this.TileEditBtn.ImageTransparentColor = Color.Magenta;
			this.TileEditBtn.Name = "TileEditBtn";
			this.TileEditBtn.Size = new Size(31, 22);
			this.TileEditBtn.Text = "Edit";
			this.TileEditBtn.Click += new EventHandler(this.TileSetEditBtn_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new Size(6, 25);
			this.TileCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TileCutBtn.Image = (Image)componentResourceManager.GetObject("TileCutBtn.Image");
			this.TileCutBtn.ImageTransparentColor = Color.Magenta;
			this.TileCutBtn.Name = "TileCutBtn";
			this.TileCutBtn.Size = new Size(30, 22);
			this.TileCutBtn.Text = "Cut";
			this.TileCutBtn.Click += new EventHandler(this.TileCutBtn_Click);
			this.TileCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TileCopyBtn.Image = (Image)componentResourceManager.GetObject("TileCopyBtn.Image");
			this.TileCopyBtn.ImageTransparentColor = Color.Magenta;
			this.TileCopyBtn.Name = "TileCopyBtn";
			this.TileCopyBtn.Size = new Size(39, 22);
			this.TileCopyBtn.Text = "Copy";
			this.TileCopyBtn.Click += new EventHandler(this.TileCopyBtn_Click);
			this.TilePasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TilePasteBtn.Image = (Image)componentResourceManager.GetObject("TilePasteBtn.Image");
			this.TilePasteBtn.ImageTransparentColor = Color.Magenta;
			this.TilePasteBtn.Name = "TilePasteBtn";
			this.TilePasteBtn.Size = new Size(39, 22);
			this.TilePasteBtn.Text = "Paste";
			this.TilePasteBtn.Click += new EventHandler(this.TilePasteBtn_Click);
			this.toolStripSeparator23.Name = "toolStripSeparator23";
			this.toolStripSeparator23.Size = new Size(6, 25);
			this.TileTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TileTools.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.TileToolsExport
			});
			this.TileTools.Image = (Image)componentResourceManager.GetObject("TileTools.Image");
			this.TileTools.ImageTransparentColor = Color.Magenta;
			this.TileTools.Name = "TileTools";
			this.TileTools.Size = new Size(49, 22);
			this.TileTools.Text = "Tools";
			this.TileToolsExport.Name = "TileToolsExport";
			this.TileToolsExport.Size = new Size(116, 22);
			this.TileToolsExport.Text = "Export...";
			this.TileToolsExport.Click += new EventHandler(this.TileToolsExport_Click);
			this.TerrainPowersPage.Controls.Add(this.TerrainPowerList);
			this.TerrainPowersPage.Controls.Add(this.TerrainPowerToolbar);
			this.TerrainPowersPage.Location = new Point(4, 22);
			this.TerrainPowersPage.Name = "TerrainPowersPage";
			this.TerrainPowersPage.Padding = new Padding(3);
			this.TerrainPowersPage.Size = new Size(618, 246);
			this.TerrainPowersPage.TabIndex = 7;
			this.TerrainPowersPage.Text = "Terrain Powers";
			this.TerrainPowersPage.UseVisualStyleBackColor = true;
			this.TerrainPowerList.Columns.AddRange(new ColumnHeader[]
			{
				this.TPNameHdr,
				this.TPInfoHdr
			});
			this.TerrainPowerList.ContextMenuStrip = this.TPContext;
			this.TerrainPowerList.Dock = DockStyle.Fill;
			this.TerrainPowerList.FullRowSelect = true;
			listViewGroup11.Header = "Traps";
			listViewGroup11.Name = "TrapGroup";
			listViewGroup12.Header = "Hazards";
			listViewGroup12.Name = "HazardGroup";
			this.TerrainPowerList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup11,
				listViewGroup12
			});
			this.TerrainPowerList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TerrainPowerList.HideSelection = false;
			this.TerrainPowerList.Location = new Point(3, 28);
			this.TerrainPowerList.Name = "TerrainPowerList";
			this.TerrainPowerList.Size = new Size(612, 215);
			this.TerrainPowerList.Sorting = SortOrder.Ascending;
			this.TerrainPowerList.TabIndex = 6;
			this.TerrainPowerList.UseCompatibleStateImageBehavior = false;
			this.TerrainPowerList.View = View.Details;
			this.TerrainPowerList.DoubleClick += new EventHandler(this.TPEditBtn_Click);
			this.TerrainPowerList.ItemDrag += new ItemDragEventHandler(this.TPList_ItemDrag);
			this.TPNameHdr.Text = "Terrain Power";
			this.TPNameHdr.Width = 300;
			this.TPInfoHdr.Text = "Info";
			this.TPInfoHdr.Width = 150;
			this.TPContext.Items.AddRange(new ToolStripItem[]
			{
				this.TPContextRemove
			});
			this.TPContext.Name = "ChallengeContext";
			this.TPContext.Size = new Size(118, 26);
			this.TPContextRemove.Name = "TPContextRemove";
			this.TPContextRemove.Size = new Size(117, 22);
			this.TPContextRemove.Text = "Remove";
			this.TPContextRemove.Click += new EventHandler(this.TPContextRemove_Click);
			this.TerrainPowerToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.TPAdd,
				this.TPRemoveBtn,
				this.TPEditBtn,
				this.toolStripSeparator29,
				this.TPCutBtn,
				this.TPCopyBtn,
				this.TPPasteBtn,
				this.toolStripSeparator30,
				this.TPStatBlockBtn,
				this.toolStripSeparator35,
				this.TPTools
			});
			this.TerrainPowerToolbar.Location = new Point(3, 3);
			this.TerrainPowerToolbar.Name = "TerrainPowerToolbar";
			this.TerrainPowerToolbar.Size = new Size(612, 25);
			this.TerrainPowerToolbar.TabIndex = 5;
			this.TerrainPowerToolbar.Text = "toolStrip2";
			this.TPAdd.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TPAdd.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.TPAddTerrainPower,
				this.toolStripSeparator28,
				this.TPAddImport
			});
			this.TPAdd.Image = (Image)componentResourceManager.GetObject("TPAdd.Image");
			this.TPAdd.ImageTransparentColor = Color.Magenta;
			this.TPAdd.Name = "TPAdd";
			this.TPAdd.Size = new Size(42, 22);
			this.TPAdd.Text = "Add";
			this.TPAddTerrainPower.Name = "TPAddTerrainPower";
			this.TPAddTerrainPower.Size = new Size(190, 22);
			this.TPAddTerrainPower.Text = "Add a Terrain Power...";
			this.TPAddTerrainPower.Click += new EventHandler(this.TPAddBtn_Click);
			this.toolStripSeparator28.Name = "toolStripSeparator28";
			this.toolStripSeparator28.Size = new Size(187, 6);
			this.TPAddImport.Name = "TPAddImport";
			this.TPAddImport.Size = new Size(190, 22);
			this.TPAddImport.Text = "Import...";
			this.TPAddImport.Click += new EventHandler(this.TPAddImport_Click);
			this.TPRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TPRemoveBtn.Image = (Image)componentResourceManager.GetObject("TPRemoveBtn.Image");
			this.TPRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.TPRemoveBtn.Name = "TPRemoveBtn";
			this.TPRemoveBtn.Size = new Size(54, 22);
			this.TPRemoveBtn.Text = "Remove";
			this.TPRemoveBtn.Click += new EventHandler(this.TPRemoveBtn_Click);
			this.TPEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TPEditBtn.Image = (Image)componentResourceManager.GetObject("TPEditBtn.Image");
			this.TPEditBtn.ImageTransparentColor = Color.Magenta;
			this.TPEditBtn.Name = "TPEditBtn";
			this.TPEditBtn.Size = new Size(31, 22);
			this.TPEditBtn.Text = "Edit";
			this.TPEditBtn.Click += new EventHandler(this.TPEditBtn_Click);
			this.toolStripSeparator29.Name = "toolStripSeparator29";
			this.toolStripSeparator29.Size = new Size(6, 25);
			this.TPCutBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TPCutBtn.Image = (Image)componentResourceManager.GetObject("TPCutBtn.Image");
			this.TPCutBtn.ImageTransparentColor = Color.Magenta;
			this.TPCutBtn.Name = "TPCutBtn";
			this.TPCutBtn.Size = new Size(30, 22);
			this.TPCutBtn.Text = "Cut";
			this.TPCutBtn.Click += new EventHandler(this.TPCutBtn_Click);
			this.TPCopyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TPCopyBtn.Image = (Image)componentResourceManager.GetObject("TPCopyBtn.Image");
			this.TPCopyBtn.ImageTransparentColor = Color.Magenta;
			this.TPCopyBtn.Name = "TPCopyBtn";
			this.TPCopyBtn.Size = new Size(39, 22);
			this.TPCopyBtn.Text = "Copy";
			this.TPCopyBtn.Click += new EventHandler(this.TPCopyBtn_Click);
			this.TPPasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TPPasteBtn.Image = (Image)componentResourceManager.GetObject("TPPasteBtn.Image");
			this.TPPasteBtn.ImageTransparentColor = Color.Magenta;
			this.TPPasteBtn.Name = "TPPasteBtn";
			this.TPPasteBtn.Size = new Size(39, 22);
			this.TPPasteBtn.Text = "Paste";
			this.TPPasteBtn.Click += new EventHandler(this.TPPasteBtn_Click);
			this.toolStripSeparator30.Name = "toolStripSeparator30";
			this.toolStripSeparator30.Size = new Size(6, 25);
			this.TPStatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TPStatBlockBtn.Image = (Image)componentResourceManager.GetObject("TPStatBlockBtn.Image");
			this.TPStatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.TPStatBlockBtn.Name = "TPStatBlockBtn";
			this.TPStatBlockBtn.Size = new Size(63, 22);
			this.TPStatBlockBtn.Text = "Stat Block";
			this.TPStatBlockBtn.Click += new EventHandler(this.TPStatBlockBtn_Click);
			this.toolStripSeparator35.Name = "toolStripSeparator35";
			this.toolStripSeparator35.Size = new Size(6, 25);
			this.TPTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TPTools.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.TPToolsExport
			});
			this.TPTools.Image = (Image)componentResourceManager.GetObject("TPTools.Image");
			this.TPTools.ImageTransparentColor = Color.Magenta;
			this.TPTools.Name = "TPTools";
			this.TPTools.Size = new Size(49, 22);
			this.TPTools.Text = "Tools";
			this.TPToolsExport.Name = "TPToolsExport";
			this.TPToolsExport.Size = new Size(116, 22);
			this.TPToolsExport.Text = "Export...";
			this.TPToolsExport.Click += new EventHandler(this.TPToolsExport_Click);
			this.ArtifactPage.Controls.Add(this.ArtifactList);
			this.ArtifactPage.Controls.Add(this.ArtifactToolbar);
			this.ArtifactPage.Location = new Point(4, 22);
			this.ArtifactPage.Name = "ArtifactPage";
			this.ArtifactPage.Padding = new Padding(3);
			this.ArtifactPage.Size = new Size(618, 246);
			this.ArtifactPage.TabIndex = 8;
			this.ArtifactPage.Text = "Artifacts";
			this.ArtifactPage.UseVisualStyleBackColor = true;
			this.ArtifactList.Columns.AddRange(new ColumnHeader[]
			{
				this.ArtifactHdr,
				this.ArtifactInfoHdr
			});
			this.ArtifactList.ContextMenuStrip = this.ArtifactContext;
			this.ArtifactList.Dock = DockStyle.Fill;
			this.ArtifactList.FullRowSelect = true;
			listViewGroup13.Header = "Traps";
			listViewGroup13.Name = "TrapGroup";
			listViewGroup14.Header = "Hazards";
			listViewGroup14.Name = "HazardGroup";
			this.ArtifactList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup13,
				listViewGroup14
			});
			this.ArtifactList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.ArtifactList.HideSelection = false;
			this.ArtifactList.Location = new Point(3, 28);
			this.ArtifactList.Name = "ArtifactList";
			this.ArtifactList.Size = new Size(612, 215);
			this.ArtifactList.Sorting = SortOrder.Ascending;
			this.ArtifactList.TabIndex = 6;
			this.ArtifactList.UseCompatibleStateImageBehavior = false;
			this.ArtifactList.View = View.Details;
			this.ArtifactList.DoubleClick += new EventHandler(this.ArtifactEdit_Click);
			this.ArtifactList.ItemDrag += new ItemDragEventHandler(this.ArtifactList_ItemDrag);
			this.ArtifactHdr.Text = "Artifact";
			this.ArtifactHdr.Width = 300;
			this.ArtifactInfoHdr.Text = "Info";
			this.ArtifactInfoHdr.Width = 150;
			this.ArtifactContext.Items.AddRange(new ToolStripItem[]
			{
				this.ArtifactContextRemove
			});
			this.ArtifactContext.Name = "ChallengeContext";
			this.ArtifactContext.Size = new Size(118, 26);
			this.ArtifactContextRemove.Name = "ArtifactContextRemove";
			this.ArtifactContextRemove.Size = new Size(117, 22);
			this.ArtifactContextRemove.Text = "Remove";
			this.ArtifactContextRemove.Click += new EventHandler(this.ArtifactRemove_Click);
			this.ArtifactToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.ArtifactAdd,
				this.ArtifactRemove,
				this.ArtifactEdit,
				this.toolStripSeparator32,
				this.ArtifactCut,
				this.ArtifactCopy,
				this.ArtifactPaste,
				this.toolStripSeparator33,
				this.ArtifactStatBlockBtn,
				this.toolStripSeparator34,
				this.ArtifactTools
			});
			this.ArtifactToolbar.Location = new Point(3, 3);
			this.ArtifactToolbar.Name = "ArtifactToolbar";
			this.ArtifactToolbar.Size = new Size(612, 25);
			this.ArtifactToolbar.TabIndex = 5;
			this.ArtifactToolbar.Text = "toolStrip2";
			this.ArtifactAdd.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ArtifactAdd.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ArtifactAddAdd,
				this.toolStripSeparator31,
				this.ArtifactAddImport
			});
			this.ArtifactAdd.Image = (Image)componentResourceManager.GetObject("ArtifactAdd.Image");
			this.ArtifactAdd.ImageTransparentColor = Color.Magenta;
			this.ArtifactAdd.Name = "ArtifactAdd";
			this.ArtifactAdd.Size = new Size(42, 22);
			this.ArtifactAdd.Text = "Add";
			this.ArtifactAddAdd.Name = "ArtifactAddAdd";
			this.ArtifactAddAdd.Size = new Size(163, 22);
			this.ArtifactAddAdd.Text = "Add an Artifact...";
			this.ArtifactAddAdd.Click += new EventHandler(this.ArtifactAddAdd_Click);
			this.toolStripSeparator31.Name = "toolStripSeparator31";
			this.toolStripSeparator31.Size = new Size(160, 6);
			this.ArtifactAddImport.Name = "ArtifactAddImport";
			this.ArtifactAddImport.Size = new Size(163, 22);
			this.ArtifactAddImport.Text = "Import...";
			this.ArtifactAddImport.Click += new EventHandler(this.ArtifactAddImport_Click);
			this.ArtifactRemove.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ArtifactRemove.Image = (Image)componentResourceManager.GetObject("ArtifactRemove.Image");
			this.ArtifactRemove.ImageTransparentColor = Color.Magenta;
			this.ArtifactRemove.Name = "ArtifactRemove";
			this.ArtifactRemove.Size = new Size(54, 22);
			this.ArtifactRemove.Text = "Remove";
			this.ArtifactRemove.Click += new EventHandler(this.ArtifactRemove_Click);
			this.ArtifactEdit.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ArtifactEdit.Image = (Image)componentResourceManager.GetObject("ArtifactEdit.Image");
			this.ArtifactEdit.ImageTransparentColor = Color.Magenta;
			this.ArtifactEdit.Name = "ArtifactEdit";
			this.ArtifactEdit.Size = new Size(31, 22);
			this.ArtifactEdit.Text = "Edit";
			this.ArtifactEdit.Click += new EventHandler(this.ArtifactEdit_Click);
			this.toolStripSeparator32.Name = "toolStripSeparator32";
			this.toolStripSeparator32.Size = new Size(6, 25);
			this.ArtifactCut.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ArtifactCut.Image = (Image)componentResourceManager.GetObject("ArtifactCut.Image");
			this.ArtifactCut.ImageTransparentColor = Color.Magenta;
			this.ArtifactCut.Name = "ArtifactCut";
			this.ArtifactCut.Size = new Size(30, 22);
			this.ArtifactCut.Text = "Cut";
			this.ArtifactCut.Click += new EventHandler(this.ArtifactCut_Click);
			this.ArtifactCopy.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ArtifactCopy.Image = (Image)componentResourceManager.GetObject("ArtifactCopy.Image");
			this.ArtifactCopy.ImageTransparentColor = Color.Magenta;
			this.ArtifactCopy.Name = "ArtifactCopy";
			this.ArtifactCopy.Size = new Size(39, 22);
			this.ArtifactCopy.Text = "Copy";
			this.ArtifactCopy.Click += new EventHandler(this.ArtifactCopy_Click);
			this.ArtifactPaste.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ArtifactPaste.Image = (Image)componentResourceManager.GetObject("ArtifactPaste.Image");
			this.ArtifactPaste.ImageTransparentColor = Color.Magenta;
			this.ArtifactPaste.Name = "ArtifactPaste";
			this.ArtifactPaste.Size = new Size(39, 22);
			this.ArtifactPaste.Text = "Paste";
			this.ArtifactPaste.Click += new EventHandler(this.ArtifactPaste_Click);
			this.toolStripSeparator33.Name = "toolStripSeparator33";
			this.toolStripSeparator33.Size = new Size(6, 25);
			this.ArtifactStatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ArtifactStatBlockBtn.Image = (Image)componentResourceManager.GetObject("ArtifactStatBlockBtn.Image");
			this.ArtifactStatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.ArtifactStatBlockBtn.Name = "ArtifactStatBlockBtn";
			this.ArtifactStatBlockBtn.Size = new Size(63, 22);
			this.ArtifactStatBlockBtn.Text = "Stat Block";
			this.ArtifactStatBlockBtn.Click += new EventHandler(this.ArtifactStatBlockBtn_Click);
			this.toolStripSeparator34.Name = "toolStripSeparator34";
			this.toolStripSeparator34.Size = new Size(6, 25);
			this.ArtifactTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ArtifactTools.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ArtifactToolsExport
			});
			this.ArtifactTools.Image = (Image)componentResourceManager.GetObject("ArtifactTools.Image");
			this.ArtifactTools.ImageTransparentColor = Color.Magenta;
			this.ArtifactTools.Name = "ArtifactTools";
			this.ArtifactTools.Size = new Size(49, 22);
			this.ArtifactTools.Text = "Tools";
			this.ArtifactToolsExport.Name = "ArtifactToolsExport";
			this.ArtifactToolsExport.Size = new Size(116, 22);
			this.ArtifactToolsExport.Text = "Export...";
			this.ArtifactToolsExport.Click += new EventHandler(this.ArtifactToolsExport_Click);
			this.HelpPanel.BorderStyle = BorderStyle.FixedSingle;
			this.HelpPanel.Dock = DockStyle.Bottom;
			this.HelpPanel.Location = new Point(0, 272);
			this.HelpPanel.Name = "HelpPanel";
			this.HelpPanel.Size = new Size(626, 159);
			this.HelpPanel.TabIndex = 3;
			this.HelpPanel.Visible = false;
			this.ChallengeContext.Items.AddRange(new ToolStripItem[]
			{
				this.ChallengeContextRemove
			});
			this.ChallengeContext.Name = "ChallengeContext";
			this.ChallengeContext.Size = new Size(118, 26);
			this.ChallengeContextRemove.Name = "ChallengeContextRemove";
			this.ChallengeContextRemove.Size = new Size(117, 22);
			this.ChallengeContextRemove.Text = "Remove";
			this.ChallengeContextRemove.Click += new EventHandler(this.ChallengeContextRemove_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(879, 431);
			base.Controls.Add(this.Splitter);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MinimizeBox = false;
			base.Name = "LibraryListForm";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Libraries";
			base.FormClosed += new FormClosedEventHandler(this.LibrariesForm_FormClosed);
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel1.PerformLayout();
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.LibraryToolbar.ResumeLayout(false);
			this.LibraryToolbar.PerformLayout();
			this.Pages.ResumeLayout(false);
			this.CreaturesPage.ResumeLayout(false);
			this.CreaturesPage.PerformLayout();
			this.CreatureContext.ResumeLayout(false);
			this.CreatureSearchToolbar.ResumeLayout(false);
			this.CreatureSearchToolbar.PerformLayout();
			this.CreatureToolbar.ResumeLayout(false);
			this.CreatureToolbar.PerformLayout();
			this.TemplatesPage.ResumeLayout(false);
			this.TemplatesPage.PerformLayout();
			this.TemplateContext.ResumeLayout(false);
			this.TemplateToolbar.ResumeLayout(false);
			this.TemplateToolbar.PerformLayout();
			this.TrapsPage.ResumeLayout(false);
			this.TrapsPage.PerformLayout();
			this.TrapContext.ResumeLayout(false);
			this.TrapToolbar.ResumeLayout(false);
			this.TrapToolbar.PerformLayout();
			this.ChallengePage.ResumeLayout(false);
			this.ChallengePage.PerformLayout();
			this.ChallengeToolbar.ResumeLayout(false);
			this.ChallengeToolbar.PerformLayout();
			this.MagicItemsPage.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			this.MagicItemContext.ResumeLayout(false);
			this.MagicItemToolbar.ResumeLayout(false);
			this.MagicItemToolbar.PerformLayout();
			this.MagicItemVersionToolbar.ResumeLayout(false);
			this.MagicItemVersionToolbar.PerformLayout();
			this.TilesPage.ResumeLayout(false);
			this.TilesPage.PerformLayout();
			this.TileContext.ResumeLayout(false);
			this.TileToolbar.ResumeLayout(false);
			this.TileToolbar.PerformLayout();
			this.TerrainPowersPage.ResumeLayout(false);
			this.TerrainPowersPage.PerformLayout();
			this.TPContext.ResumeLayout(false);
			this.TerrainPowerToolbar.ResumeLayout(false);
			this.TerrainPowerToolbar.PerformLayout();
			this.ArtifactPage.ResumeLayout(false);
			this.ArtifactPage.PerformLayout();
			this.ArtifactContext.ResumeLayout(false);
			this.ArtifactToolbar.ResumeLayout(false);
			this.ArtifactToolbar.PerformLayout();
			this.ChallengeContext.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public LibraryListForm()
		{
			this.InitializeComponent();
			this.CreatureSearchToolbar.Visible = false;
			this.CompendiumBtn.Visible = Program.IsBeta;
			foreach (Library current in Session.Libraries)
			{
				this.fModified[current] = false;
			}
			Application.Idle += new EventHandler(this.Application_Idle);
			this.update_libraries();
		}

		private void get_nodes(TreeNode tn, List<TreeNode> nodes)
		{
			nodes.Add(tn);
			foreach (TreeNode tn2 in tn.Nodes)
			{
				this.get_nodes(tn2, nodes);
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.HelpBtn.Text = (this.HelpPanel.Visible ? "Hide Help" : "Show Help");
			if (this.SelectedLibrary != null && Session.Project != null && this.SelectedLibrary == Session.Project.Library)
			{
				this.LibraryRemoveBtn.Enabled = false;
				this.LibraryEditBtn.Enabled = false;
				this.CreatureAddBtn.Enabled = false;
				this.OppRemoveBtn.Enabled = false;
				this.OppEditBtn.Enabled = false;
				this.CreatureCopyBtn.Enabled = (this.SelectedCreatures.Count != 0);
				this.CreatureCutBtn.Enabled = false;
				this.CreaturePasteBtn.Enabled = false;
				this.CreatureStatBlockBtn.Enabled = (this.SelectedCreatures.Count == 1);
				this.CreatureToolsExport.Enabled = (this.SelectedCreatures.Count == 1);
				this.TemplateAddBtn.Enabled = false;
				this.TemplateRemoveBtn.Enabled = false;
				this.TemplateEditBtn.Enabled = false;
				this.TemplateCopyBtn.Enabled = (this.SelectedTemplates.Count != 0 || this.SelectedThemes.Count != 0);
				this.TemplateCutBtn.Enabled = false;
				this.TemplatePasteBtn.Enabled = false;
				this.TemplateStatBlock.Enabled = (this.SelectedTemplates.Count == 1 || this.SelectedThemes.Count == 1);
				this.TemplateToolsExport.Enabled = (this.SelectedTemplates.Count == 1 || this.SelectedThemes.Count == 1);
				this.TrapAdd.Enabled = false;
				this.TrapRemoveBtn.Enabled = false;
				this.TrapEditBtn.Enabled = false;
				this.TrapCopyBtn.Enabled = (this.SelectedTraps.Count != 0);
				this.TrapCutBtn.Enabled = false;
				this.TrapPasteBtn.Enabled = false;
				this.TrapStatBlockBtn.Enabled = (this.SelectedTraps.Count == 1);
				this.TrapToolsExport.Enabled = (this.SelectedTraps.Count == 1);
				this.ChallengeAdd.Enabled = false;
				this.ChallengeRemoveBtn.Enabled = false;
				this.ChallengeEditBtn.Enabled = false;
				this.ChallengeCopyBtn.Enabled = (this.SelectedChallenges.Count != 0);
				this.ChallengeCutBtn.Enabled = false;
				this.ChallengePasteBtn.Enabled = false;
				this.ChallengeStatBlockBtn.Enabled = (this.SelectedChallenges.Count == 1);
				this.ChallengeToolsExport.Enabled = (this.SelectedChallenges.Count == 1);
				this.MagicItemAdd.Enabled = false;
				this.MagicItemRemoveBtn.Enabled = false;
				this.MagicItemEditBtn.Enabled = false;
				this.MagicItemCopyBtn.Enabled = (this.SelectedMagicItems.Count != 0);
				this.MagicItemCutBtn.Enabled = false;
				this.MagicItemPasteBtn.Enabled = false;
				this.MagicItemStatBlockBtn.Enabled = (this.SelectedMagicItems.Count == 1);
				this.MagicItemToolsExport.Enabled = (this.SelectedMagicItemSet != "");
				this.TileAddBtn.Enabled = false;
				this.TileRemoveBtn.Enabled = false;
				this.TileEditBtn.Enabled = false;
				this.TileCopyBtn.Enabled = (this.SelectedTiles.Count != 0);
				this.TileCutBtn.Enabled = false;
				this.TilePasteBtn.Enabled = false;
				this.TileToolsExport.Enabled = (this.SelectedTiles.Count == 1);
				this.TPAdd.Enabled = false;
				this.TPRemoveBtn.Enabled = false;
				this.TPEditBtn.Enabled = false;
				this.TPCopyBtn.Enabled = (this.SelectedTerrainPowers.Count != 0);
				this.TPCutBtn.Enabled = false;
				this.TPPasteBtn.Enabled = false;
				this.TPStatBlockBtn.Enabled = (this.SelectedTerrainPowers.Count == 1);
				this.TPToolsExport.Enabled = (this.SelectedTerrainPowers.Count == 1);
				this.ArtifactAdd.Enabled = false;
				this.ArtifactRemove.Enabled = false;
				this.ArtifactEdit.Enabled = false;
				this.ArtifactCopy.Enabled = (this.SelectedArtifacts.Count != 0);
				this.ArtifactCut.Enabled = false;
				this.ArtifactPaste.Enabled = false;
				this.ArtifactStatBlockBtn.Enabled = (this.SelectedArtifacts.Count == 1);
				this.ArtifactToolsExport.Enabled = (this.SelectedArtifacts.Count == 1);
			}
			else
			{
				this.LibraryRemoveBtn.Enabled = (this.SelectedLibrary != null);
				this.LibraryEditBtn.Enabled = (this.SelectedLibrary != null);
				this.CreatureAddBtn.Enabled = (this.SelectedLibrary != null);
				this.OppRemoveBtn.Enabled = (this.SelectedCreatures.Count != 0);
				this.OppEditBtn.Enabled = (this.SelectedCreatures.Count == 1);
				this.CreatureCopyBtn.Enabled = (this.SelectedCreatures.Count != 0);
				this.CreatureCutBtn.Enabled = (this.SelectedCreatures.Count != 0);
				this.CreaturePasteBtn.Enabled = (this.SelectedLibrary != null && Clipboard.ContainsData(typeof(List<Creature>).ToString()));
				this.CreatureStatBlockBtn.Enabled = (this.SelectedCreatures.Count == 1);
				this.CreatureToolsExport.Enabled = (this.SelectedCreatures.Count == 1);
				this.TemplateAddBtn.Enabled = (this.SelectedLibrary != null);
				this.TemplateRemoveBtn.Enabled = (this.SelectedTemplates.Count != 0 || this.SelectedThemes.Count != 0);
				this.TemplateEditBtn.Enabled = (this.SelectedTemplates.Count != 0 || this.SelectedThemes.Count != 0);
				this.TemplateCopyBtn.Enabled = (this.SelectedTemplates.Count != 0 || this.SelectedThemes.Count != 0);
				this.TemplateCutBtn.Enabled = (this.SelectedTemplates.Count != 0 || this.SelectedThemes.Count != 0);
				this.TemplatePasteBtn.Enabled = (this.SelectedLibrary != null && (Clipboard.ContainsData(typeof(List<CreatureTemplate>).ToString()) || Clipboard.ContainsData(typeof(List<MonsterTheme>).ToString())));
				this.TemplateStatBlock.Enabled = (this.SelectedTemplates.Count == 1);
				this.TemplateToolsExport.Enabled = (this.SelectedTemplates.Count == 1 || this.SelectedThemes.Count == 1);
				this.TrapAdd.Enabled = (this.SelectedLibrary != null);
				this.TrapRemoveBtn.Enabled = (this.SelectedTraps.Count != 0);
				this.TrapEditBtn.Enabled = (this.SelectedTraps.Count == 1);
				this.TrapCopyBtn.Enabled = (this.SelectedTraps.Count != 0);
				this.TrapCutBtn.Enabled = (this.SelectedTraps.Count != 0);
				this.TrapPasteBtn.Enabled = (this.SelectedLibrary != null && Clipboard.ContainsData(typeof(List<Trap>).ToString()));
				this.TrapStatBlockBtn.Enabled = (this.SelectedTraps.Count == 1);
				this.TrapToolsExport.Enabled = (this.SelectedTraps.Count == 1);
				this.ChallengeAdd.Enabled = (this.SelectedLibrary != null);
				this.ChallengeRemoveBtn.Enabled = (this.SelectedChallenges.Count != 0);
				this.ChallengeEditBtn.Enabled = (this.SelectedChallenges.Count == 1);
				this.ChallengeCopyBtn.Enabled = (this.SelectedChallenges.Count != 0);
				this.ChallengeCutBtn.Enabled = (this.SelectedChallenges.Count != 0);
				this.ChallengePasteBtn.Enabled = (this.SelectedLibrary != null && Clipboard.ContainsData(typeof(List<SkillChallenge>).ToString()));
				this.ChallengeStatBlockBtn.Enabled = (this.SelectedChallenges.Count == 1);
				this.ChallengeToolsExport.Enabled = (this.SelectedChallenges.Count == 1);
				this.MagicItemAdd.Enabled = (this.SelectedLibrary != null);
				this.MagicItemRemoveBtn.Enabled = (this.SelectedMagicItems.Count != 0);
				this.MagicItemEditBtn.Enabled = (this.SelectedMagicItems.Count == 1);
				this.MagicItemCopyBtn.Enabled = (this.SelectedMagicItems.Count != 0);
				this.MagicItemCutBtn.Enabled = (this.SelectedMagicItems.Count != 0);
				this.MagicItemPasteBtn.Enabled = (this.SelectedLibrary != null && Clipboard.ContainsData(typeof(List<MagicItem>).ToString()));
				this.MagicItemStatBlockBtn.Enabled = (this.SelectedMagicItems.Count == 1);
				this.MagicItemToolsExport.Enabled = (this.SelectedMagicItemSet != "");
				this.TileAddBtn.Enabled = (this.SelectedLibrary != null);
				this.TileRemoveBtn.Enabled = (this.SelectedTiles.Count != 0);
				this.TileEditBtn.Enabled = (this.SelectedTiles.Count == 1);
				this.TileCopyBtn.Enabled = (this.SelectedTiles.Count != 0);
				this.TileCutBtn.Enabled = (this.SelectedTiles.Count != 0);
				this.TilePasteBtn.Enabled = (this.SelectedLibrary != null && Clipboard.ContainsData(typeof(List<Tile>).ToString()));
				this.TileToolsExport.Enabled = (this.SelectedTiles.Count == 1);
				this.TPAdd.Enabled = (this.SelectedLibrary != null);
				this.TPRemoveBtn.Enabled = (this.SelectedTerrainPowers.Count != 0);
				this.TPEditBtn.Enabled = (this.SelectedTerrainPowers.Count == 1);
				this.TPCopyBtn.Enabled = (this.SelectedTerrainPowers.Count != 0);
				this.TPCutBtn.Enabled = (this.SelectedTerrainPowers.Count != 0);
				this.TPPasteBtn.Enabled = (this.SelectedLibrary != null && Clipboard.ContainsData(typeof(List<TerrainPower>).ToString()));
				this.TPStatBlockBtn.Enabled = (this.SelectedTerrainPowers.Count == 1);
				this.TPToolsExport.Enabled = (this.SelectedTerrainPowers.Count == 1);
				this.ArtifactAdd.Enabled = (this.SelectedLibrary != null);
				this.ArtifactRemove.Enabled = (this.SelectedArtifacts.Count != 0);
				this.ArtifactEdit.Enabled = (this.SelectedArtifacts.Count == 1);
				this.ArtifactCopy.Enabled = (this.SelectedArtifacts.Count != 0);
				this.ArtifactCut.Enabled = (this.SelectedArtifacts.Count != 0);
				this.ArtifactPaste.Enabled = (this.SelectedLibrary != null && Clipboard.ContainsData(typeof(List<Artifact>).ToString()));
				this.ArtifactStatBlockBtn.Enabled = (this.SelectedArtifacts.Count == 1);
				this.ArtifactToolsExport.Enabled = (this.SelectedArtifacts.Count == 1);
			}
			this.CreatureToolsFilterList.Checked = this.CreatureSearchToolbar.Visible;
			this.CategorisedBtn.Checked = this.fShowCategorised;
			this.UncategorisedBtn.Checked = this.fShowUncategorised;
			this.CreatureContextRemove.Enabled = (this.SelectedCreatures.Count != 0);
			this.CreatureContextCategory.Enabled = (this.SelectedCreatures.Count != 0);
			this.TemplateContextRemove.Enabled = (this.SelectedTemplates.Count != 0);
			this.TemplateContextType.Enabled = (this.SelectedTemplates.Count != 0);
			this.TrapContextRemove.Enabled = (this.SelectedTraps.Count != 0);
			this.TrapContextType.Enabled = (this.SelectedTraps.Count != 0);
			this.ChallengeContextRemove.Enabled = (this.SelectedChallenges.Count != 0);
			this.MagicItemContextRemove.Enabled = (this.SelectedMagicItems.Count != 0);
			this.TileContextRemove.Enabled = (this.SelectedTiles.Count != 0);
			this.TileContextCategory.Enabled = (this.SelectedTiles.Count != 0);
			this.TileContextSize.Enabled = (this.SelectedTiles.Count != 0);
			this.TPContextRemove.Enabled = (this.SelectedTerrainPowers.Count != 0);
			this.ArtifactContextRemove.Enabled = (this.SelectedArtifacts.Count != 0);
		}

		private void LibrariesForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			foreach (Library current in Session.Libraries)
			{
				if (!this.fModified.ContainsKey(current) || this.fModified[current])
				{
					this.save(current);
				}
			}
		}

		private void FileNew_Click(object sender, EventArgs e)
		{
			LibraryForm libraryForm = new LibraryForm(new Library
			{
				Name = "New Library"
			});
			if (libraryForm.ShowDialog() == DialogResult.OK)
			{
				Session.Libraries.Add(libraryForm.Library);
				Session.Libraries.Sort();
				this.save(libraryForm.Library);
				this.fModified[libraryForm.Library] = true;
				this.update_libraries();
				this.SelectedLibrary = libraryForm.Library;
				this.update_content(true);
			}
		}

		private void FileOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = Program.LibraryFilter;
			openFileDialog.Multiselect = true;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string libraryFolder = Session.LibraryFolder;
				List<string> list = new List<string>();
				string[] fileNames = openFileDialog.FileNames;
				for (int i = 0; i < fileNames.Length; i++)
				{
					string text = fileNames[i];
					string text2 = FileName.Directory(text);
					if (!text2.Contains(libraryFolder))
					{
						list.Add(text);
					}
				}
				if (list.Count != 0)
				{
					string text3 = "Do you want to move these libraries into the Libraries folder?";
					text3 += Environment.NewLine;
					text3 += "They will then be loaded automatically the next time Masterplan starts up.";
					DialogResult dialogResult = MessageBox.Show(text3, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dialogResult == DialogResult.Yes)
					{
						foreach (string current in list)
						{
							string destFileName = libraryFolder + FileName.Name(current) + ".library";
							File.Copy(current, destFileName);
						}
					}
				}
				string[] fileNames2 = openFileDialog.FileNames;
				for (int j = 0; j < fileNames2.Length; j++)
				{
					string text4 = fileNames2[j];
					if (!list.Contains(text4))
					{
						Library key = Session.LoadLibrary(text4);
						this.fModified[key] = false;
					}
				}
				foreach (string current2 in list)
				{
					Library key2 = Session.LoadLibrary(current2);
					this.fModified[key2] = false;
				}
				if (Session.Project != null)
				{
					Session.Project.SimplifyProjectLibrary();
				}
				Session.Libraries.Sort();
				this.update_libraries();
				this.update_content(true);
			}
		}

		private void FileClose_Click(object sender, EventArgs e)
		{
			if (Session.Project != null)
			{
				Session.Project.PopulateProjectLibrary();
			}
			foreach (Library current in Session.Libraries)
			{
				current.Creatures.Clear();
				current.Templates.Clear();
				current.Themes.Clear();
				current.Traps.Clear();
				current.SkillChallenges.Clear();
				current.MagicItems.Clear();
				current.Tiles.Clear();
				current.TerrainPowers.Clear();
				current.Artifacts.Clear();
			}
			Session.Libraries.Clear();
			if (Session.Project != null)
			{
				Session.Project.SimplifyProjectLibrary();
			}
			this.update_libraries();
			this.update_content(true);
			GC.Collect();
		}

		private void LibraryRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				string text = "You are about to delete a library; are you sure you want to do this?";
				if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					return;
				}
				Session.DeleteLibrary(this.SelectedLibrary);
				this.update_libraries();
				this.SelectedLibrary = null;
				this.update_content(true);
			}
		}

		private void LibraryEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				int index = Session.Libraries.IndexOf(this.SelectedLibrary);
				string libraryFilename = Session.GetLibraryFilename(this.SelectedLibrary);
				if (!File.Exists(libraryFilename))
				{
					string text = "This library cannot be renamed.";
					MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				LibraryForm libraryForm = new LibraryForm(this.SelectedLibrary);
				if (libraryForm.ShowDialog() == DialogResult.OK)
				{
					Session.Libraries[index] = libraryForm.Library;
					Session.Libraries.Sort();
					string libraryFilename2 = Session.GetLibraryFilename(libraryForm.Library);
					if (libraryFilename != libraryFilename2)
					{
						FileInfo fileInfo = new FileInfo(libraryFilename);
						fileInfo.MoveTo(libraryFilename2);
					}
					this.fModified[libraryForm.Library] = true;
					this.update_libraries();
					this.update_content(true);
				}
			}
		}

		private void LibraryMergeBtn_Click(object sender, EventArgs e)
		{
			MergeLibrariesForm mergeLibrariesForm = new MergeLibrariesForm();
			if (mergeLibrariesForm.ShowDialog() == DialogResult.OK)
			{
				Library library = new Library();
				library.Name = mergeLibrariesForm.LibraryName;
				library.SecurityData = Program.SecurityData;
				foreach (Library current in mergeLibrariesForm.SelectedLibraries)
				{
					library.Import(current);
					Session.DeleteLibrary(current);
				}
				Session.Libraries.Add(library);
				Session.Libraries.Sort();
				this.save(library);
				this.update_libraries();
				this.SelectedLibrary = library;
				this.update_content(true);
			}
		}

		private void LibraryTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			this.update_content(true);
		}

		private void LibraryList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			Library selectedLibrary = this.SelectedLibrary;
			if (selectedLibrary == null)
			{
				return;
			}
			if (Session.Project != null && Session.Project.Library == selectedLibrary)
			{
				return;
			}
			base.DoDragDrop(selectedLibrary, DragDropEffects.Move);
		}

		private void CompendiumBtn_Click(object sender, EventArgs e)
		{
			CompendiumForm compendiumForm = new CompendiumForm();
			if (compendiumForm.ShowDialog() == DialogResult.OK)
			{
				this.update_content(true);
			}
		}

		private void HelpBtn_Click(object sender, EventArgs e)
		{
			this.show_help(!this.HelpPanel.Visible);
		}

		private void Pages_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.update_content(false);
		}

		private void LibraryList_DragOver(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.None;
			Point pt = this.LibraryTree.PointToClient(Cursor.Position);
			TreeNode nodeAt = this.LibraryTree.GetNodeAt(pt);
			this.LibraryTree.SelectedNode = nodeAt;
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
			{
				return;
			}
			Library library = e.Data.GetData(typeof(Library)) as Library;
			if (library != null)
			{
				if (library != this.SelectedLibrary)
				{
					e.Effect = DragDropEffects.Move;
				}
				return;
			}
			List<Creature> list = e.Data.GetData(typeof(List<Creature>)) as List<Creature>;
			if (list != null)
			{
				bool flag = false;
				foreach (Creature current in list)
				{
					if (!this.SelectedLibrary.Creatures.Contains(current))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					e.Effect = DragDropEffects.Move;
				}
				return;
			}
			List<CreatureTemplate> list2 = e.Data.GetData(typeof(List<CreatureTemplate>)) as List<CreatureTemplate>;
			if (list2 != null)
			{
				bool flag2 = false;
				foreach (CreatureTemplate current2 in list2)
				{
					if (!this.SelectedLibrary.Templates.Contains(current2))
					{
						flag2 = true;
						break;
					}
				}
				if (flag2)
				{
					e.Effect = DragDropEffects.Move;
				}
				return;
			}
			List<Trap> list3 = e.Data.GetData(typeof(List<Trap>)) as List<Trap>;
			if (list3 != null)
			{
				bool flag3 = false;
				foreach (Trap current3 in list3)
				{
					if (!this.SelectedLibrary.Traps.Contains(current3))
					{
						flag3 = true;
						break;
					}
				}
				if (flag3)
				{
					e.Effect = DragDropEffects.Move;
				}
				return;
			}
			List<SkillChallenge> list4 = e.Data.GetData(typeof(List<SkillChallenge>)) as List<SkillChallenge>;
			if (list4 != null)
			{
				bool flag4 = false;
				foreach (SkillChallenge current4 in list4)
				{
					if (!this.SelectedLibrary.SkillChallenges.Contains(current4))
					{
						flag4 = true;
						break;
					}
				}
				if (flag4)
				{
					e.Effect = DragDropEffects.Move;
				}
				return;
			}
			List<MagicItem> list5 = e.Data.GetData(typeof(List<MagicItem>)) as List<MagicItem>;
			if (list4 != null)
			{
				bool flag5 = false;
				foreach (MagicItem current5 in list5)
				{
					if (!this.SelectedLibrary.MagicItems.Contains(current5))
					{
						flag5 = true;
						break;
					}
				}
				if (flag5)
				{
					e.Effect = DragDropEffects.Move;
				}
				return;
			}
			List<Tile> list6 = e.Data.GetData(typeof(List<Tile>)) as List<Tile>;
			if (list6 != null)
			{
				bool flag6 = false;
				foreach (Tile current6 in list6)
				{
					if (!this.SelectedLibrary.Tiles.Contains(current6))
					{
						flag6 = true;
						break;
					}
				}
				if (flag6)
				{
					e.Effect = DragDropEffects.Move;
				}
				return;
			}
			List<TerrainPower> list7 = e.Data.GetData(typeof(List<TerrainPower>)) as List<TerrainPower>;
			if (list7 != null)
			{
				bool flag7 = false;
				foreach (TerrainPower current7 in list7)
				{
					if (!this.SelectedLibrary.TerrainPowers.Contains(current7))
					{
						flag7 = true;
						break;
					}
				}
				if (flag7)
				{
					e.Effect = DragDropEffects.Move;
				}
				return;
			}
			List<Artifact> list8 = e.Data.GetData(typeof(List<Artifact>)) as List<Artifact>;
			if (list8 != null)
			{
				bool flag8 = false;
				foreach (Artifact current8 in list8)
				{
					if (!this.SelectedLibrary.Artifacts.Contains(current8))
					{
						flag8 = true;
						break;
					}
				}
				if (flag8)
				{
					e.Effect = DragDropEffects.Move;
				}
			}
		}

		private void LibraryList_DragDrop(object sender, DragEventArgs e)
		{
			Point pt = this.LibraryTree.PointToClient(Cursor.Position);
			TreeNode nodeAt = this.LibraryTree.GetNodeAt(pt);
			this.LibraryTree.SelectedNode = nodeAt;
			if (this.SelectedLibrary == null)
			{
				return;
			}
			Library library = e.Data.GetData(typeof(Library)) as Library;
			if (library != null)
			{
				this.SelectedLibrary.Import(library);
				this.fModified[this.SelectedLibrary] = true;
				Session.DeleteLibrary(library);
				this.update_content(true);
			}
			List<Creature> list = e.Data.GetData(typeof(List<Creature>)) as List<Creature>;
			if (list != null)
			{
				foreach (Creature current in list)
				{
					foreach (Library current2 in Session.Libraries)
					{
						if (current2.Creatures.Contains(current))
						{
							current2.Creatures.Remove(current);
							this.fModified[current2] = true;
						}
					}
					this.SelectedLibrary.Creatures.Add(current);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_creatures();
			}
			List<CreatureTemplate> list2 = e.Data.GetData(typeof(List<CreatureTemplate>)) as List<CreatureTemplate>;
			if (list2 != null)
			{
				foreach (CreatureTemplate current3 in list2)
				{
					foreach (Library current4 in Session.Libraries)
					{
						if (current4.Templates.Contains(current3))
						{
							current4.Templates.Remove(current3);
							this.fModified[current4] = true;
						}
					}
					this.SelectedLibrary.Templates.Add(current3);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_templates();
			}
			List<Trap> list3 = e.Data.GetData(typeof(List<Trap>)) as List<Trap>;
			if (list3 != null)
			{
				foreach (Trap current5 in list3)
				{
					foreach (Library current6 in Session.Libraries)
					{
						if (current6.Traps.Contains(current5))
						{
							current6.Traps.Remove(current5);
							this.fModified[current6] = true;
						}
					}
					this.SelectedLibrary.Traps.Add(current5);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_traps();
			}
			List<SkillChallenge> list4 = e.Data.GetData(typeof(List<SkillChallenge>)) as List<SkillChallenge>;
			if (list4 != null)
			{
				foreach (SkillChallenge current7 in list4)
				{
					foreach (Library current8 in Session.Libraries)
					{
						if (current8.SkillChallenges.Contains(current7))
						{
							current8.SkillChallenges.Remove(current7);
							this.fModified[current8] = true;
						}
					}
					this.SelectedLibrary.SkillChallenges.Add(current7);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_challenges();
			}
			List<MagicItem> list5 = e.Data.GetData(typeof(List<MagicItem>)) as List<MagicItem>;
			if (list5 != null)
			{
				foreach (MagicItem current9 in list5)
				{
					foreach (Library current10 in Session.Libraries)
					{
						if (current10.MagicItems.Contains(current9))
						{
							current10.MagicItems.Remove(current9);
							this.fModified[current10] = true;
						}
					}
					this.SelectedLibrary.MagicItems.Add(current9);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_magic_item_sets();
			}
			List<Tile> list6 = e.Data.GetData(typeof(List<Tile>)) as List<Tile>;
			if (list6 != null)
			{
				foreach (Tile current11 in list6)
				{
					foreach (Library current12 in Session.Libraries)
					{
						if (current12.Tiles.Contains(current11))
						{
							current12.Tiles.Remove(current11);
							this.fModified[current12] = true;
						}
					}
					this.SelectedLibrary.Tiles.Add(current11);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_tiles();
			}
			List<TerrainPower> list7 = e.Data.GetData(typeof(List<TerrainPower>)) as List<TerrainPower>;
			if (list7 != null)
			{
				foreach (TerrainPower current13 in list7)
				{
					foreach (Library current14 in Session.Libraries)
					{
						if (current14.TerrainPowers.Contains(current13))
						{
							current14.TerrainPowers.Remove(current13);
							this.fModified[current14] = true;
						}
					}
					this.SelectedLibrary.TerrainPowers.Add(current13);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_terrain_powers();
			}
			List<Artifact> list8 = e.Data.GetData(typeof(List<Artifact>)) as List<Artifact>;
			if (list8 != null)
			{
				foreach (Artifact current15 in list8)
				{
					foreach (Library current16 in Session.Libraries)
					{
						if (current16.Artifacts.Contains(current15))
						{
							current16.Artifacts.Remove(current15);
							this.fModified[current16] = true;
						}
					}
					this.SelectedLibrary.Artifacts.Add(current15);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_artifacts();
			}
		}

		private void CreatureAddBtn_Click(object sender, EventArgs e)
		{
			CreatureBuilderForm creatureBuilderForm = new CreatureBuilderForm(new Creature
			{
				Name = "New Creature"
			});
			if (creatureBuilderForm.ShowDialog() == DialogResult.OK)
			{
				this.SelectedLibrary.Creatures.Add(creatureBuilderForm.Creature as Creature);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void CreatureImport_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					return;
				}
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = Program.CreatureAndMonsterFilter;
				openFileDialog.Multiselect = true;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < fileNames.Length; i++)
					{
						string text = fileNames[i];
						Creature creature = null;
						if (text.EndsWith("creature"))
						{
							creature = Serialisation<Creature>.Load(text, SerialisationMode.Binary);
						}
						if (text.EndsWith("monster"))
						{
							string xml = File.ReadAllText(text);
							creature = AppImport.ImportCreature(xml);
						}
						if (creature != null)
						{
							Creature creature2 = this.SelectedLibrary.FindCreature(creature.Name, creature.Level);
							if (creature2 != null)
							{
								creature.ID = creature2.ID;
								creature.Category = creature2.Category;
								this.SelectedLibrary.Creatures.Remove(creature2);
							}
							this.SelectedLibrary.Creatures.Add(creature);
							this.fModified[this.SelectedLibrary] = true;
							this.update_content(true);
						}
					}
				}
			}
		}

		private void OppRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCreatures.Count != 0)
			{
				string text = "Are you sure you want to delete your selection?";
				if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					return;
				}
				foreach (Creature current in this.SelectedCreatures)
				{
					Library library = this.SelectedLibrary;
					if (library == null)
					{
						library = Session.FindLibrary(current);
					}
					library.Creatures.Remove(current);
					this.fModified[library] = true;
				}
				this.update_content(true);
			}
		}

		private void OppEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCreatures.Count == 1)
			{
				Library library = Session.FindLibrary(this.SelectedCreatures[0]);
				if (library == null)
				{
					return;
				}
				int index = library.Creatures.IndexOf(this.SelectedCreatures[0]);
				CreatureBuilderForm creatureBuilderForm = new CreatureBuilderForm(this.SelectedCreatures[0]);
				if (creatureBuilderForm.ShowDialog() == DialogResult.OK)
				{
					library.Creatures[index] = (creatureBuilderForm.Creature as Creature);
					this.fModified[library] = true;
					this.update_content(true);
				}
			}
		}

		private void CreatureCutBtn_Click(object sender, EventArgs e)
		{
			this.CreatureCopyBtn_Click(sender, e);
			this.OppRemoveBtn_Click(sender, e);
		}

		private void CreatureCopyBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCreatures.Count != 0)
			{
				Clipboard.SetData(typeof(List<Creature>).ToString(), this.SelectedCreatures);
			}
		}

		private void CreaturePasteBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Clipboard.ContainsData(typeof(List<Creature>).ToString()))
			{
				List<Creature> list = Clipboard.GetData(typeof(List<Creature>).ToString()) as List<Creature>;
				foreach (Creature current in list)
				{
					Creature creature = current.Copy();
					creature.ID = Guid.NewGuid();
					this.SelectedLibrary.Creatures.Add(creature);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void CreatureStatBlockBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCreatures.Count != 1)
			{
				return;
			}
			CreatureDetailsForm creatureDetailsForm = new CreatureDetailsForm(new EncounterCard
			{
				CreatureID = this.SelectedCreatures[0].ID
			});
			creatureDetailsForm.ShowDialog();
		}

		private void CreatureDemoBtn_Click(object sender, EventArgs e)
		{
			try
			{
				DemographicsForm demographicsForm = new DemographicsForm(this.SelectedLibrary, DemographicsSource.Creatures);
				demographicsForm.ShowDialog();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void PowerStatsBtn_Click(object sender, EventArgs e)
		{
			List<CreaturePower> list = new List<CreaturePower>();
			int num = 0;
			if (this.SelectedLibrary == null)
			{
				using (List<Library>.Enumerator enumerator = Session.Libraries.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Library current = enumerator.Current;
						num += current.Creatures.Count;
						foreach (Creature current2 in current.Creatures)
						{
							list.AddRange(current2.CreaturePowers);
						}
					}
					goto IL_155;
				}
			}
			num = this.SelectedLibrary.Creatures.Count;
			foreach (ICreature current3 in this.SelectedLibrary.Creatures)
			{
				if (current3 != null)
				{
					list.AddRange(current3.CreaturePowers);
				}
			}
			if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
			{
				num += Session.Project.CustomCreatures.Count;
				foreach (ICreature current4 in Session.Project.CustomCreatures)
				{
					if (current4 != null)
					{
						list.AddRange(current4.CreaturePowers);
					}
				}
			}
			IL_155:
			PowerStatisticsForm powerStatisticsForm = new PowerStatisticsForm(list, num);
			powerStatisticsForm.ShowDialog();
		}

		private void FilterBtn_Click(object sender, EventArgs e)
		{
			this.CreatureSearchToolbar.Visible = !this.CreatureSearchToolbar.Visible;
			this.update_content(true);
		}

		private void CreatureToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedCreatures.Count == 1)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Export";
				saveFileDialog.Filter = Program.CreatureFilter;
				saveFileDialog.FileName = this.SelectedCreatures[0].Name;
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					Serialisation<Creature>.Save(saveFileDialog.FileName, this.SelectedCreatures[0], SerialisationMode.Binary);
				}
			}
		}

		private void SearchBox_TextChanged(object sender, EventArgs e)
		{
			this.update_content(true);
		}

		private void CreatureFilterCategorised_Click(object sender, EventArgs e)
		{
			this.fShowCategorised = !this.fShowCategorised;
			this.update_content(true);
		}

		private void CreatureFilterUncategorised_Click(object sender, EventArgs e)
		{
			this.fShowUncategorised = !this.fShowUncategorised;
			this.update_content(true);
		}

		private void OppList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedCreatures.Count != 0)
			{
				base.DoDragDrop(this.SelectedCreatures, DragDropEffects.Move);
			}
		}

		private void CreatureContextRemove_Click(object sender, EventArgs e)
		{
			this.OppRemoveBtn_Click(sender, e);
		}

		private void CreatureContextCategory_Click(object sender, EventArgs e)
		{
			List<string> list = new List<string>();
			foreach (Creature current in Session.Creatures)
			{
				if (current.Category != null && !(current.Category == "") && !list.Contains(current.Category))
				{
					list.Add(current.Category);
				}
			}
			List<string> list2 = new List<string>();
			foreach (Creature current2 in this.SelectedCreatures)
			{
				if (current2.Category != null && !(current2.Category == "") && !list2.Contains(current2.Category))
				{
					list2.Add(current2.Category);
				}
			}
			string text = "";
			if (list2.Count == 1)
			{
				text = list2[0];
			}
			if (list2.Count == 0)
			{
				if (this.SelectedCreatures.Count == 1)
				{
					text = this.SelectedCreatures[0].Name;
				}
				else
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>();
					for (int num = 0; num != this.SelectedCreatures.Count; num++)
					{
						string name = this.SelectedCreatures[num].Name;
						for (int num2 = num + 1; num2 != this.SelectedCreatures.Count; num2++)
						{
							string name2 = this.SelectedCreatures[num2].Name;
							string text2 = StringHelper.LongestCommonToken(name, name2);
							if (text2.Length >= 3)
							{
								if (!dictionary.ContainsKey(text2))
								{
									dictionary[text2] = 0;
								}
								Dictionary<string, int> dictionary2;
								string key;
								(dictionary2 = dictionary)[key = text2] = dictionary2[key] + 1;
							}
						}
					}
					if (dictionary.Keys.Count != 0)
					{
						foreach (string current3 in dictionary.Keys)
						{
							int num3 = dictionary.ContainsKey(text) ? dictionary[text] : 0;
							if (dictionary[current3] > num3)
							{
								text = current3;
							}
						}
						if (!list.Contains(text))
						{
							list.Add(text);
						}
					}
				}
			}
			CategoryForm categoryForm = new CategoryForm(list, text);
			if (categoryForm.ShowDialog() == DialogResult.OK)
			{
				foreach (Creature current4 in this.SelectedCreatures)
				{
					current4.Category = categoryForm.Category;
					Library library = Session.FindLibrary(current4);
					if (library != null)
					{
						this.fModified[library] = true;
					}
				}
				this.update_creatures();
			}
		}

		private void update_creatures()
		{
			Cursor.Current = Cursors.WaitCursor;
			this.CreatureList.BeginUpdate();
			ListState state = ListState.GetState(this.CreatureList);
			List<Creature> list = new List<Creature>();
			if (this.SelectedLibrary != null)
			{
				list.AddRange(this.SelectedLibrary.Creatures);
				if (Session.Project == null || this.SelectedLibrary != Session.Project.Library)
				{
					goto IL_F5;
				}
				foreach (CustomCreature current in Session.Project.CustomCreatures)
				{
					Creature item = new Creature(current);
					list.Add(item);
				}
				using (List<NPC>.Enumerator enumerator2 = Session.Project.NPCs.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						NPC current2 = enumerator2.Current;
						Creature item2 = new Creature(current2);
						list.Add(item2);
					}
					goto IL_F5;
				}
			}
			list.AddRange(Session.Creatures);
			IL_F5:
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (Creature current3 in list)
			{
				if (current3 != null && current3.Category != "")
				{
					binarySearchTree.Add(current3.Category);
				}
			}
			List<string> sortedList = binarySearchTree.SortedList;
			sortedList.Insert(0, "Custom Creatures");
			sortedList.Insert(1, "NPCs");
			sortedList.Add("Miscellaneous Creatures");
			this.CreatureList.Groups.Clear();
			foreach (string current4 in sortedList)
			{
				this.CreatureList.Groups.Add(current4, current4);
			}
			this.CreatureList.ShowGroups = true;
			List<ListViewItem> list2 = new List<ListViewItem>();
			foreach (Creature current5 in list)
			{
				if (current5 != null)
				{
					if (this.CreatureSearchToolbar.Visible)
					{
						if (this.SearchBox.Text != "")
						{
							string value = this.SearchBox.Text.ToLower();
							bool flag = current5.Name.ToLower().Contains(value);
							if (!flag && current5.Category != null && current5.Category.ToLower().Contains(value))
							{
								flag = true;
							}
							if (!flag)
							{
								continue;
							}
						}
						bool flag2 = false;
						bool flag3 = current5.Category != null && current5.Category != "";
						if (this.fShowCategorised && flag3)
						{
							flag2 = true;
						}
						if (this.fShowUncategorised && !flag3)
						{
							flag2 = true;
						}
						if (!flag2)
						{
							continue;
						}
					}
					ListViewItem listViewItem = new ListViewItem(current5.Name);
					listViewItem.SubItems.Add(current5.Info);
					listViewItem.Tag = current5;
					if (current5.Category != "")
					{
						listViewItem.Group = this.CreatureList.Groups[current5.Category];
					}
					else
					{
						listViewItem.Group = this.CreatureList.Groups["Miscellaneous Creatures"];
					}
					list2.Add(listViewItem);
				}
			}
			this.CreatureList.Items.Clear();
			this.CreatureList.Items.AddRange(list2.ToArray());
			if (this.CreatureList.Items.Count == 0)
			{
				this.CreatureList.ShowGroups = false;
				ListViewItem listViewItem2 = this.CreatureList.Items.Add("(no creatures)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
			this.CreatureList.Sort();
			ListState.SetState(this.CreatureList, state);
			this.CreatureList.EndUpdate();
			Cursor.Current = Cursors.Default;
		}

		private void TemplateAddBtn_Click(object sender, EventArgs e)
		{
			CreatureTemplateBuilderForm creatureTemplateBuilderForm = new CreatureTemplateBuilderForm(new CreatureTemplate
			{
				Name = "New Template"
			});
			if (creatureTemplateBuilderForm.ShowDialog() == DialogResult.OK)
			{
				this.SelectedLibrary.Templates.Add(creatureTemplateBuilderForm.Template);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void TemplateAddTheme_Click(object sender, EventArgs e)
		{
			MonsterThemeForm monsterThemeForm = new MonsterThemeForm(new MonsterTheme
			{
				Name = "New Theme"
			});
			if (monsterThemeForm.ShowDialog() == DialogResult.OK)
			{
				this.SelectedLibrary.Themes.Add(monsterThemeForm.Theme);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void TemplateImport_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					return;
				}
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = Program.CreatureTemplateAndThemeFilter;
				openFileDialog.Multiselect = true;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < fileNames.Length; i++)
					{
						string text = fileNames[i];
						if (text.EndsWith("creaturetemplate"))
						{
							CreatureTemplate creatureTemplate = Serialisation<CreatureTemplate>.Load(text, SerialisationMode.Binary);
							if (creatureTemplate != null)
							{
								this.SelectedLibrary.Templates.Add(creatureTemplate);
								this.fModified[this.SelectedLibrary] = true;
								this.update_content(true);
							}
						}
						if (text.EndsWith("theme"))
						{
							MonsterTheme monsterTheme = Serialisation<MonsterTheme>.Load(text, SerialisationMode.Binary);
							if (monsterTheme != null)
							{
								this.SelectedLibrary.Themes.Add(monsterTheme);
								this.fModified[this.SelectedLibrary] = true;
								this.update_content(true);
							}
						}
					}
				}
			}
		}

		private void TemplateRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTemplates.Count != 0)
			{
				string text = "Are you sure you want to delete your selection?";
				if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					return;
				}
				foreach (CreatureTemplate current in this.SelectedTemplates)
				{
					Library library = this.SelectedLibrary;
					if (library == null)
					{
						library = Session.FindLibrary(current);
					}
					library.Templates.Remove(current);
					this.fModified[library] = true;
				}
				this.update_content(true);
			}
			if (this.SelectedThemes.Count != 0)
			{
				foreach (MonsterTheme current2 in this.SelectedThemes)
				{
					Library library2 = this.SelectedLibrary;
					if (library2 == null)
					{
						library2 = Session.FindLibrary(current2);
					}
					library2.Themes.Remove(current2);
					this.fModified[library2] = true;
				}
				this.update_content(true);
			}
		}

		private void TemplateEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTemplates.Count == 1)
			{
				Library library = Session.FindLibrary(this.SelectedTemplates[0]);
				if (library == null)
				{
					return;
				}
				int index = library.Templates.IndexOf(this.SelectedTemplates[0]);
				CreatureTemplateBuilderForm creatureTemplateBuilderForm = new CreatureTemplateBuilderForm(this.SelectedTemplates[0]);
				if (creatureTemplateBuilderForm.ShowDialog() == DialogResult.OK)
				{
					library.Templates[index] = creatureTemplateBuilderForm.Template;
					this.fModified[library] = true;
					this.update_content(true);
				}
			}
			if (this.SelectedThemes.Count == 1)
			{
				Library library2 = Session.FindLibrary(this.SelectedThemes[0]);
				if (library2 == null)
				{
					return;
				}
				int index2 = library2.Themes.IndexOf(this.SelectedThemes[0]);
				MonsterThemeForm monsterThemeForm = new MonsterThemeForm(this.SelectedThemes[0]);
				if (monsterThemeForm.ShowDialog() == DialogResult.OK)
				{
					library2.Themes[index2] = monsterThemeForm.Theme;
					this.fModified[library2] = true;
					this.update_content(true);
				}
			}
		}

		private void TemplateCutBtn_Click(object sender, EventArgs e)
		{
			this.TemplateCopyBtn_Click(sender, e);
			this.TemplateRemoveBtn_Click(sender, e);
		}

		private void TemplateCopyBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTemplates.Count != 0)
			{
				Clipboard.SetData(typeof(List<CreatureTemplate>).ToString(), this.SelectedTemplates);
			}
			if (this.SelectedThemes.Count != 0)
			{
				Clipboard.SetData(typeof(List<MonsterTheme>).ToString(), this.SelectedThemes);
			}
		}

		private void TemplatePasteBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Clipboard.ContainsData(typeof(List<CreatureTemplate>).ToString()))
			{
				List<CreatureTemplate> list = Clipboard.GetData(typeof(List<CreatureTemplate>).ToString()) as List<CreatureTemplate>;
				foreach (CreatureTemplate current in list)
				{
					CreatureTemplate creatureTemplate = current.Copy();
					creatureTemplate.ID = Guid.NewGuid();
					this.SelectedLibrary.Templates.Add(creatureTemplate);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
			if (Clipboard.ContainsData(typeof(List<MonsterTheme>).ToString()))
			{
				List<MonsterTheme> list2 = Clipboard.GetData(typeof(List<MonsterTheme>).ToString()) as List<MonsterTheme>;
				foreach (MonsterTheme current2 in list2)
				{
					MonsterTheme monsterTheme = current2.Copy();
					monsterTheme.ID = Guid.NewGuid();
					this.SelectedLibrary.Themes.Add(monsterTheme);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void TemplateStatBlock_Click(object sender, EventArgs e)
		{
			if (this.SelectedTemplates.Count != 1)
			{
				return;
			}
			CreatureTemplateDetailsForm creatureTemplateDetailsForm = new CreatureTemplateDetailsForm(this.SelectedTemplates[0]);
			creatureTemplateDetailsForm.ShowDialog();
		}

		private void TemplateToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedTemplates.Count == 1)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Export";
				saveFileDialog.Filter = Program.CreatureTemplateFilter;
				saveFileDialog.FileName = this.SelectedTemplates[0].Name;
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					Serialisation<CreatureTemplate>.Save(saveFileDialog.FileName, this.SelectedTemplates[0], SerialisationMode.Binary);
					return;
				}
			}
			else if (this.SelectedThemes.Count == 1)
			{
				SaveFileDialog saveFileDialog2 = new SaveFileDialog();
				saveFileDialog2.Title = "Export";
				saveFileDialog2.Filter = Program.ThemeFilter;
				saveFileDialog2.FileName = this.SelectedThemes[0].Name;
				if (saveFileDialog2.ShowDialog() == DialogResult.OK)
				{
					Serialisation<MonsterTheme>.Save(saveFileDialog2.FileName, this.SelectedThemes[0], SerialisationMode.Binary);
				}
			}
		}

		private void TemplateList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedTemplates.Count != 0)
			{
				base.DoDragDrop(this.SelectedTemplates, DragDropEffects.Move);
			}
		}

		private void TemplateContextRemove_Click(object sender, EventArgs e)
		{
			this.TemplateRemoveBtn_Click(sender, e);
		}

		private void TemplateFunctional_Click(object sender, EventArgs e)
		{
			foreach (CreatureTemplate current in this.SelectedTemplates)
			{
				current.Type = CreatureTemplateType.Functional;
				Library library = Session.FindLibrary(current);
				if (library != null)
				{
					this.fModified[library] = true;
				}
			}
			this.update_templates();
		}

		private void TemplateClass_Click(object sender, EventArgs e)
		{
			foreach (CreatureTemplate current in this.SelectedTemplates)
			{
				current.Type = CreatureTemplateType.Class;
				Library library = Session.FindLibrary(current);
				if (library != null)
				{
					this.fModified[library] = true;
				}
			}
			this.update_templates();
		}

		private void update_templates()
		{
			Cursor.Current = Cursors.WaitCursor;
			this.TemplateList.BeginUpdate();
			ListState state = ListState.GetState(this.TemplateList);
			List<CreatureTemplate> list = new List<CreatureTemplate>();
			List<MonsterTheme> list2 = new List<MonsterTheme>();
			if (this.SelectedLibrary != null)
			{
				list.AddRange(this.SelectedLibrary.Templates);
				list2.AddRange(this.SelectedLibrary.Themes);
			}
			else
			{
				foreach (Library current in Session.Libraries)
				{
					list.AddRange(current.Templates);
					list2.AddRange(current.Themes);
				}
			}
			this.TemplateList.Items.Clear();
			this.TemplateList.ShowGroups = true;
			foreach (CreatureTemplate current2 in list)
			{
				if (current2 != null)
				{
					ListViewItem listViewItem = this.TemplateList.Items.Add(current2.Name);
					listViewItem.SubItems.Add(current2.Info);
					listViewItem.Tag = current2;
					listViewItem.Group = this.TemplateList.Groups[(current2.Type == CreatureTemplateType.Functional) ? 0 : 1];
				}
			}
			foreach (MonsterTheme current3 in list2)
			{
				if (current3 != null)
				{
					ListViewItem listViewItem2 = this.TemplateList.Items.Add(current3.Name);
					listViewItem2.Tag = current3;
					listViewItem2.Group = this.TemplateList.Groups[2];
				}
			}
			if (this.TemplateList.Items.Count == 0)
			{
				this.TemplateList.ShowGroups = false;
				ListViewItem listViewItem3 = this.TemplateList.Items.Add("(no templates or themes)");
				listViewItem3.ForeColor = SystemColors.GrayText;
			}
			this.TemplateList.Sort();
			ListState.SetState(this.TemplateList, state);
			this.TemplateList.EndUpdate();
			Cursor.Current = Cursors.Default;
		}

		private void TrapAddBtn_Click(object sender, EventArgs e)
		{
			TrapBuilderForm trapBuilderForm = new TrapBuilderForm(new Trap
			{
				Name = "New Trap",
				Attacks = 
				{
					new TrapAttack()
				}
			});
			if (trapBuilderForm.ShowDialog() == DialogResult.OK)
			{
				this.SelectedLibrary.Traps.Add(trapBuilderForm.Trap);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void TrapAddImport_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					return;
				}
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = Program.TrapFilter;
				openFileDialog.Multiselect = true;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < fileNames.Length; i++)
					{
						string filename = fileNames[i];
						Trap trap = Serialisation<Trap>.Load(filename, SerialisationMode.Binary);
						if (trap != null)
						{
							Trap trap2 = this.SelectedLibrary.FindTrap(trap.Name, trap.Level, trap.Role.ToString());
							if (trap2 != null)
							{
								trap.ID = trap2.ID;
								this.SelectedLibrary.Traps.Remove(trap2);
							}
							this.SelectedLibrary.Traps.Add(trap);
							this.fModified[this.SelectedLibrary] = true;
							this.update_content(true);
						}
					}
				}
			}
		}

		private void TrapRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTraps.Count != 0)
			{
				string text = "Are you sure you want to delete your selection?";
				if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					return;
				}
				foreach (Trap current in this.SelectedTraps)
				{
					Library library = this.SelectedLibrary;
					if (library == null)
					{
						library = Session.FindLibrary(current);
					}
					library.Traps.Remove(current);
					this.fModified[library] = true;
				}
				this.update_content(true);
			}
		}

		private void TrapEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTraps.Count == 1)
			{
				Library library = Session.FindLibrary(this.SelectedTraps[0]);
				if (library == null)
				{
					return;
				}
				int index = library.Traps.IndexOf(this.SelectedTraps[0]);
				TrapBuilderForm trapBuilderForm = new TrapBuilderForm(this.SelectedTraps[0]);
				if (trapBuilderForm.ShowDialog() == DialogResult.OK)
				{
					library.Traps[index] = trapBuilderForm.Trap;
					this.fModified[library] = true;
					this.update_content(true);
				}
			}
		}

		private void TrapCutBtn_Click(object sender, EventArgs e)
		{
			this.TrapCopyBtn_Click(sender, e);
			this.TrapRemoveBtn_Click(sender, e);
		}

		private void TrapCopyBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTraps.Count != 0)
			{
				Clipboard.SetData(typeof(List<Trap>).ToString(), this.SelectedTraps);
			}
		}

		private void TrapPasteBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Clipboard.ContainsData(typeof(List<Trap>).ToString()))
			{
				List<Trap> list = Clipboard.GetData(typeof(List<Trap>).ToString()) as List<Trap>;
				foreach (Trap current in list)
				{
					Trap trap = current.Copy();
					trap.ID = Guid.NewGuid();
					this.SelectedLibrary.Traps.Add(trap);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void TrapStatBlockBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTraps.Count != 1)
			{
				return;
			}
			TrapDetailsForm trapDetailsForm = new TrapDetailsForm(this.SelectedTraps[0]);
			trapDetailsForm.ShowDialog();
		}

		private void TrapDemoBtn_Click(object sender, EventArgs e)
		{
			try
			{
				DemographicsForm demographicsForm = new DemographicsForm(this.SelectedLibrary, DemographicsSource.Traps);
				demographicsForm.ShowDialog();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void TrapToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedTraps.Count == 1)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Export";
				saveFileDialog.Filter = Program.TrapFilter;
				saveFileDialog.FileName = this.SelectedTraps[0].Name;
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					Serialisation<Trap>.Save(saveFileDialog.FileName, this.SelectedTraps[0], SerialisationMode.Binary);
				}
			}
		}

		private void TrapList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedTraps.Count != 0)
			{
				base.DoDragDrop(this.SelectedTraps, DragDropEffects.Move);
			}
		}

		private void TrapContextRemove_Click(object sender, EventArgs e)
		{
			this.TrapRemoveBtn_Click(sender, e);
		}

		private void TrapTrap_Click(object sender, EventArgs e)
		{
			foreach (Trap current in this.SelectedTraps)
			{
				current.Type = TrapType.Trap;
				Library library = Session.FindLibrary(current);
				if (library != null)
				{
					this.fModified[library] = true;
				}
			}
			this.update_traps();
		}

		private void TrapHazard_Click(object sender, EventArgs e)
		{
			foreach (Trap current in this.SelectedTraps)
			{
				current.Type = TrapType.Hazard;
				Library library = Session.FindLibrary(current);
				if (library != null)
				{
					this.fModified[library] = true;
				}
			}
			this.update_traps();
		}

		private void update_traps()
		{
			Cursor.Current = Cursors.WaitCursor;
			this.TrapList.BeginUpdate();
			ListState state = ListState.GetState(this.TrapList);
			List<Trap> list = new List<Trap>();
			if (this.SelectedLibrary != null)
			{
				list.AddRange(this.SelectedLibrary.Traps);
				if (Session.Project == null || this.SelectedLibrary != Session.Project.Library)
				{
					goto IL_11A;
				}
				List<PlotPoint> allPlotPoints = Session.Project.AllPlotPoints;
				using (List<PlotPoint>.Enumerator enumerator = allPlotPoints.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PlotPoint current = enumerator.Current;
						if (current.Element is Encounter)
						{
							Encounter encounter = current.Element as Encounter;
							list.AddRange(encounter.Traps);
						}
						if (current.Element is Trap)
						{
							list.Add(current.Element as Trap);
						}
					}
					goto IL_11A;
				}
			}
			foreach (Library current2 in Session.Libraries)
			{
				list.AddRange(current2.Traps);
			}
			IL_11A:
			this.TrapList.Items.Clear();
			this.TrapList.ShowGroups = true;
			List<ListViewItem> list2 = new List<ListViewItem>();
			foreach (Trap current3 in list)
			{
				if (current3 != null)
				{
					list2.Add(new ListViewItem(current3.Name)
					{
						SubItems = 
						{
							current3.Info
						},
						Tag = current3,
						Group = this.TrapList.Groups[(current3.Type == TrapType.Trap) ? 0 : 1]
					});
				}
			}
			this.TrapList.Items.AddRange(list2.ToArray());
			if (this.TrapList.Items.Count == 0)
			{
				this.TrapList.ShowGroups = false;
				ListViewItem listViewItem = this.TrapList.Items.Add("(no traps)");
				listViewItem.ForeColor = SystemColors.GrayText;
			}
			this.TrapList.Sort();
			ListState.SetState(this.TrapList, state);
			this.TrapList.EndUpdate();
			Cursor.Current = Cursors.Default;
		}

		private void ChallengeAddBtn_Click(object sender, EventArgs e)
		{
			SkillChallengeBuilderForm skillChallengeBuilderForm = new SkillChallengeBuilderForm(new SkillChallenge
			{
				Name = "New Skill Challenge"
			});
			if (skillChallengeBuilderForm.ShowDialog() == DialogResult.OK)
			{
				this.SelectedLibrary.SkillChallenges.Add(skillChallengeBuilderForm.SkillChallenge);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void ChallengeAddImport_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					return;
				}
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = Program.SkillChallengeFilter;
				openFileDialog.Multiselect = true;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < fileNames.Length; i++)
					{
						string filename = fileNames[i];
						SkillChallenge skillChallenge = Serialisation<SkillChallenge>.Load(filename, SerialisationMode.Binary);
						if (skillChallenge != null)
						{
							this.SelectedLibrary.SkillChallenges.Add(skillChallenge);
							this.fModified[this.SelectedLibrary] = true;
							this.update_content(true);
						}
					}
				}
			}
		}

		private void ChallengeRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedChallenges.Count != 0)
			{
				string text = "Are you sure you want to delete your selection?";
				if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					return;
				}
				foreach (SkillChallenge current in this.SelectedChallenges)
				{
					Library library = this.SelectedLibrary;
					if (library == null)
					{
						library = Session.FindLibrary(current);
					}
					library.SkillChallenges.Remove(current);
					this.fModified[library] = true;
				}
				this.update_content(true);
			}
		}

		private void ChallengeEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedChallenges.Count == 1)
			{
				Library library = Session.FindLibrary(this.SelectedChallenges[0]);
				if (library == null)
				{
					return;
				}
				int index = library.SkillChallenges.IndexOf(this.SelectedChallenges[0]);
				SkillChallengeBuilderForm skillChallengeBuilderForm = new SkillChallengeBuilderForm(this.SelectedChallenges[0]);
				if (skillChallengeBuilderForm.ShowDialog() == DialogResult.OK)
				{
					library.SkillChallenges[index] = skillChallengeBuilderForm.SkillChallenge;
					this.fModified[library] = true;
					this.update_content(true);
				}
			}
		}

		private void ChallengeCutBtn_Click(object sender, EventArgs e)
		{
			this.ChallengeCopyBtn_Click(sender, e);
			this.ChallengeRemoveBtn_Click(sender, e);
		}

		private void ChallengeCopyBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedChallenges != null)
			{
				Clipboard.SetData(typeof(List<SkillChallenge>).ToString(), this.SelectedChallenges);
			}
		}

		private void ChallengePasteBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Clipboard.ContainsData(typeof(List<SkillChallenge>).ToString()))
			{
				List<SkillChallenge> list = Clipboard.GetData(typeof(List<SkillChallenge>).ToString()) as List<SkillChallenge>;
				foreach (SkillChallenge current in list)
				{
					SkillChallenge skillChallenge = current.Copy() as SkillChallenge;
					skillChallenge.ID = Guid.NewGuid();
					this.SelectedLibrary.SkillChallenges.Add(skillChallenge);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void ChallengeStatBlockBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedChallenges.Count != 1)
			{
				return;
			}
			SkillChallengeDetailsForm skillChallengeDetailsForm = new SkillChallengeDetailsForm(this.SelectedChallenges[0]);
			skillChallengeDetailsForm.ShowDialog();
		}

		private void ChallengeToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedChallenges.Count == 1)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Export";
				saveFileDialog.Filter = Program.SkillChallengeFilter;
				saveFileDialog.FileName = this.SelectedChallenges[0].Name;
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					Serialisation<SkillChallenge>.Save(saveFileDialog.FileName, this.SelectedChallenges[0], SerialisationMode.Binary);
				}
			}
		}

		private void ChallengeList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedChallenges.Count != 0)
			{
				base.DoDragDrop(this.SelectedChallenges, DragDropEffects.Move);
			}
		}

		private void ChallengeContextRemove_Click(object sender, EventArgs e)
		{
			this.ChallengeRemoveBtn_Click(sender, e);
		}

		private void update_challenges()
		{
			Cursor.Current = Cursors.WaitCursor;
			this.ChallengeList.BeginUpdate();
			ListState state = ListState.GetState(this.ChallengeList);
			List<SkillChallenge> list = new List<SkillChallenge>();
			if (this.SelectedLibrary != null)
			{
				list.AddRange(this.SelectedLibrary.SkillChallenges);
				if (Session.Project == null || this.SelectedLibrary != Session.Project.Library)
				{
					goto IL_11A;
				}
				List<PlotPoint> allPlotPoints = Session.Project.AllPlotPoints;
				using (List<PlotPoint>.Enumerator enumerator = allPlotPoints.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PlotPoint current = enumerator.Current;
						if (current.Element is Encounter)
						{
							Encounter encounter = current.Element as Encounter;
							list.AddRange(encounter.SkillChallenges);
						}
						if (current.Element is SkillChallenge)
						{
							list.Add(current.Element as SkillChallenge);
						}
					}
					goto IL_11A;
				}
			}
			foreach (Library current2 in Session.Libraries)
			{
				list.AddRange(current2.SkillChallenges);
			}
			IL_11A:
			this.ChallengeList.Items.Clear();
			this.ChallengeList.ShowGroups = false;
			foreach (SkillChallenge current3 in list)
			{
				if (current3 != null)
				{
					ListViewItem listViewItem = this.ChallengeList.Items.Add(current3.Name);
					listViewItem.SubItems.Add(current3.Info);
					listViewItem.Tag = current3;
				}
			}
			if (this.ChallengeList.Items.Count == 0)
			{
				ListViewItem listViewItem2 = this.ChallengeList.Items.Add("(no skill challenges)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
			this.ChallengeList.Sort();
			ListState.SetState(this.ChallengeList, state);
			this.ChallengeList.EndUpdate();
			Cursor.Current = Cursors.Default;
		}

		private void MagicItemAddBtn_Click(object sender, EventArgs e)
		{
			MagicItemBuilderForm magicItemBuilderForm = new MagicItemBuilderForm(new MagicItem
			{
				Name = "New Magic Item"
			});
			if (magicItemBuilderForm.ShowDialog() == DialogResult.OK)
			{
				this.SelectedLibrary.MagicItems.Add(magicItemBuilderForm.MagicItem);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void MagicItemAddImport_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					return;
				}
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = Program.MagicItemFilter;
				openFileDialog.Multiselect = true;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < fileNames.Length; i++)
					{
						string filename = fileNames[i];
						List<MagicItem> list = Serialisation<List<MagicItem>>.Load(filename, SerialisationMode.Binary);
						foreach (MagicItem current in list)
						{
							if (current != null)
							{
								MagicItem magicItem = this.SelectedLibrary.FindMagicItem(current.Name, current.Level);
								if (magicItem != null)
								{
									current.ID = magicItem.ID;
									this.SelectedLibrary.MagicItems.Remove(magicItem);
								}
								this.SelectedLibrary.MagicItems.Add(current);
								this.fModified[this.SelectedLibrary] = true;
								this.update_content(true);
							}
						}
					}
				}
			}
		}

		private void MagicItemRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMagicItems.Count != 0)
			{
				string text = "Are you sure you want to delete your selection?";
				if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					return;
				}
				foreach (MagicItem current in this.SelectedMagicItems)
				{
					Library library = this.SelectedLibrary;
					if (library == null)
					{
						library = Session.FindLibrary(current);
					}
					library.MagicItems.Remove(current);
					this.fModified[library] = true;
				}
				this.update_content(true);
			}
		}

		private void MagicItemEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMagicItems.Count == 1)
			{
				Library library = Session.FindLibrary(this.SelectedMagicItems[0]);
				if (library == null)
				{
					return;
				}
				int index = library.MagicItems.IndexOf(this.SelectedMagicItems[0]);
				MagicItemBuilderForm magicItemBuilderForm = new MagicItemBuilderForm(this.SelectedMagicItems[0]);
				if (magicItemBuilderForm.ShowDialog() == DialogResult.OK)
				{
					library.MagicItems[index] = magicItemBuilderForm.MagicItem;
					this.fModified[library] = true;
					this.update_content(true);
				}
			}
		}

		private void MagicItemCutBtn_Click(object sender, EventArgs e)
		{
			this.MagicItemCopyBtn_Click(sender, e);
			this.MagicItemRemoveBtn_Click(sender, e);
		}

		private void MagicItemCopyBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMagicItems.Count != 0)
			{
				Clipboard.SetData(typeof(List<MagicItem>).ToString(), this.SelectedMagicItems);
			}
		}

		private void MagicItemPasteBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Clipboard.ContainsData(typeof(List<MagicItem>).ToString()))
			{
				List<MagicItem> list = Clipboard.GetData(typeof(List<MagicItem>).ToString()) as List<MagicItem>;
				foreach (MagicItem current in list)
				{
					MagicItem magicItem = current.Copy();
					magicItem.ID = Guid.NewGuid();
					this.SelectedLibrary.MagicItems.Add(magicItem);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void MagicItemStatBlockBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMagicItems.Count != 1)
			{
				return;
			}
			MagicItemDetailsForm magicItemDetailsForm = new MagicItemDetailsForm(this.SelectedMagicItems[0]);
			magicItemDetailsForm.ShowDialog();
		}

		private void MagicItemDemoBtn_Click(object sender, EventArgs e)
		{
			try
			{
				DemographicsForm demographicsForm = new DemographicsForm(this.SelectedLibrary, DemographicsSource.MagicItems);
				demographicsForm.ShowDialog();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void MagicItemList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.update_magic_item_versions();
		}

		private void MagicItemsToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedMagicItemSet != "")
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Export";
				saveFileDialog.Filter = Program.MagicItemFilter;
				saveFileDialog.FileName = this.SelectedMagicItemSet;
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					List<MagicItem> list = new List<MagicItem>();
					if (this.SelectedLibrary != null)
					{
						list.AddRange(this.SelectedLibrary.MagicItems);
					}
					else
					{
						foreach (Library current in Session.Libraries)
						{
							list.AddRange(current.MagicItems);
						}
					}
					List<MagicItem> list2 = new List<MagicItem>();
					foreach (MagicItem current2 in list)
					{
						if (current2.Name == this.SelectedMagicItemSet)
						{
							list2.Add(current2);
						}
					}
					Serialisation<List<MagicItem>>.Save(saveFileDialog.FileName, list2, SerialisationMode.Binary);
				}
			}
		}

		private void MagicItemList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedMagicItems.Count != 0)
			{
				base.DoDragDrop(this.SelectedMagicItems, DragDropEffects.Move);
			}
		}

		private void MagicItemContextRemove_Click(object sender, EventArgs e)
		{
			this.MagicItemRemoveBtn_Click(sender, e);
		}

		private void update_magic_item_sets()
		{
			Cursor.Current = Cursors.WaitCursor;
			this.MagicItemList.BeginUpdate();
			ListState state = ListState.GetState(this.MagicItemList);
			List<MagicItem> list = new List<MagicItem>();
			if (this.SelectedLibrary != null)
			{
				list.AddRange(this.SelectedLibrary.MagicItems);
			}
			else
			{
				foreach (Library current in Session.Libraries)
				{
					list.AddRange(current.MagicItems);
				}
			}
			Dictionary<string, BinarySearchTree<string>> dictionary = new Dictionary<string, BinarySearchTree<string>>();
			foreach (MagicItem current2 in list)
			{
				string text = current2.Type;
				if (text == "")
				{
					text = "Miscallaneous Items";
				}
				if (!dictionary.ContainsKey(text))
				{
					dictionary[text] = new BinarySearchTree<string>();
				}
				dictionary[text].Add(current2.Name);
			}
			List<string> list2 = new List<string>();
			list2.AddRange(dictionary.Keys);
			list2.Sort();
			int num = list2.IndexOf("Miscellaneous Items");
			if (num != -1)
			{
				list2.RemoveAt(num);
				list2.Add("Miscellaneous Items");
			}
			this.MagicItemList.Groups.Clear();
			this.MagicItemList.ShowGroups = true;
			List<ListViewItem> list3 = new List<ListViewItem>();
			foreach (string current3 in list2)
			{
				ListViewGroup group = this.MagicItemList.Groups.Add(current3, current3);
				List<string> sortedList = dictionary[current3].SortedList;
				foreach (string current4 in sortedList)
				{
					list3.Add(new ListViewItem(current4)
					{
						Group = group
					});
				}
			}
			this.MagicItemList.Items.Clear();
			this.MagicItemList.Items.AddRange(list3.ToArray());
			if (this.MagicItemList.Items.Count == 0)
			{
				this.MagicItemList.ShowGroups = false;
				ListViewItem listViewItem = this.MagicItemList.Items.Add("(no magic items)");
				listViewItem.ForeColor = SystemColors.GrayText;
			}
			ListState.SetState(this.MagicItemList, state);
			this.MagicItemList.EndUpdate();
			Cursor.Current = Cursors.Default;
		}

		private void update_magic_item_versions()
		{
			if (this.SelectedMagicItemSet != "")
			{
				this.MagicItemVersionList.Enabled = true;
				this.MagicItemVersionList.ShowGroups = true;
				this.MagicItemVersionList.Items.Clear();
				List<MagicItem> list = new List<MagicItem>();
				if (this.SelectedLibrary != null)
				{
					list.AddRange(this.SelectedLibrary.MagicItems);
				}
				else
				{
					foreach (Library current in Session.Libraries)
					{
						list.AddRange(current.MagicItems);
					}
				}
				using (List<MagicItem>.Enumerator enumerator2 = list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						MagicItem current2 = enumerator2.Current;
						if (!(current2.Name != this.SelectedMagicItemSet))
						{
							ListViewItem listViewItem = this.MagicItemVersionList.Items.Add("Level " + current2.Level);
							listViewItem.Tag = current2;
							if (current2.Level < 11)
							{
								listViewItem.Group = this.MagicItemVersionList.Groups[0];
							}
							else if (current2.Level < 21)
							{
								listViewItem.Group = this.MagicItemVersionList.Groups[1];
							}
							else
							{
								listViewItem.Group = this.MagicItemVersionList.Groups[2];
							}
						}
					}
					return;
				}
			}
			this.MagicItemVersionList.Enabled = false;
			this.MagicItemVersionList.ShowGroups = false;
			this.MagicItemVersionList.Items.Clear();
		}

		private void TileAddBtn_Click(object sender, EventArgs e)
		{
			Tile t = new Tile();
			TileForm tileForm = new TileForm(t);
			if (tileForm.ShowDialog() == DialogResult.OK)
			{
				this.SelectedLibrary.Tiles.Add(tileForm.Tile);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void TileAddFolderBtn_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.Description = "Choose the folder to open.";
			folderBrowserDialog.ShowNewFolderButton = false;
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(folderBrowserDialog.SelectedPath);
				List<string> list = new List<string>();
				list.Add("jpg");
				list.Add("jpeg");
				list.Add("bmp");
				list.Add("gif");
				list.Add("png");
				list.Add("tga");
				List<FileInfo> list2 = new List<FileInfo>();
				foreach (string current in list)
				{
					list2.AddRange(directoryInfo.GetFiles("*." + current));
				}
				int num = 2147483647;
				int num2 = 2147483647;
				foreach (FileInfo current2 in list2)
				{
					Image image = Image.FromFile(current2.FullName);
					if (image.Width < num)
					{
						num = image.Width;
					}
					if (image.Height < num2)
					{
						num2 = image.Height;
					}
				}
				int num3 = Math.Min(num, num2);
				foreach (FileInfo current3 in list2)
				{
					Tile tile = new Tile();
					tile.Image = Image.FromFile(current3.FullName);
					tile.Size = new Size(tile.Image.Width / num3, tile.Image.Height / num3);
					Program.SetResolution(tile.Image);
					this.SelectedLibrary.Tiles.Add(tile);
				}
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void TileAddImport_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					return;
				}
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = Program.MapTileFilter;
				openFileDialog.Multiselect = true;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < fileNames.Length; i++)
					{
						string filename = fileNames[i];
						Tile tile = Serialisation<Tile>.Load(filename, SerialisationMode.Binary);
						if (tile != null)
						{
							this.SelectedLibrary.Tiles.Add(tile);
							this.fModified[this.SelectedLibrary] = true;
							this.update_content(true);
						}
					}
				}
			}
		}

		private void TileSetRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTiles.Count != 0)
			{
				string text = "Are you sure you want to delete your selection?";
				if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					return;
				}
				foreach (Tile current in this.SelectedTiles)
				{
					Library library = this.SelectedLibrary;
					if (library == null)
					{
						library = Session.FindLibrary(current);
					}
					library.Tiles.Remove(current);
					this.fModified[library] = true;
				}
				this.update_content(true);
			}
		}

		private void TileSetEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTiles.Count == 1)
			{
				Library library = Session.FindLibrary(this.SelectedTiles[0]);
				if (library == null)
				{
					return;
				}
				int index = library.Tiles.IndexOf(this.SelectedTiles[0]);
				TileForm tileForm = new TileForm(this.SelectedTiles[0]);
				if (tileForm.ShowDialog() == DialogResult.OK)
				{
					library.Tiles[index] = tileForm.Tile;
					this.fModified[library] = true;
					this.update_content(true);
				}
			}
		}

		private void TileCutBtn_Click(object sender, EventArgs e)
		{
			this.TileCopyBtn_Click(sender, e);
			this.TileSetRemoveBtn_Click(sender, e);
		}

		private void TileCopyBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTiles.Count != 0)
			{
				Clipboard.SetData(typeof(List<Tile>).ToString(), this.SelectedTiles);
			}
		}

		private void TileToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedTiles.Count == 1)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Export";
				saveFileDialog.Filter = Program.MapTileFilter;
				saveFileDialog.FileName = this.SelectedTiles[0].ToString();
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					Serialisation<Tile>.Save(saveFileDialog.FileName, this.SelectedTiles[0], SerialisationMode.Binary);
				}
			}
		}

		private void TilePasteBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Clipboard.ContainsData(typeof(List<Tile>).ToString()))
			{
				List<Tile> list = Clipboard.GetData(typeof(List<Tile>).ToString()) as List<Tile>;
				foreach (Tile current in list)
				{
					Tile tile = current.Copy();
					tile.ID = Guid.NewGuid();
					this.SelectedLibrary.Tiles.Add(tile);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void TileSetView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedTiles.Count != 0)
			{
				base.DoDragDrop(this.SelectedTiles, DragDropEffects.Move);
			}
		}

		private void TileContextRemove_Click(object sender, EventArgs e)
		{
			this.TileSetRemoveBtn_Click(sender, e);
		}

		private void TilePlain_Click(object sender, EventArgs e)
		{
			foreach (Tile current in this.SelectedTiles)
			{
				current.Category = TileCategory.Plain;
				Library library = Session.FindLibrary(current);
				if (library != null)
				{
					this.fModified[library] = true;
				}
			}
			this.update_tiles();
		}

		private void TileDoorway_Click(object sender, EventArgs e)
		{
			foreach (Tile current in this.SelectedTiles)
			{
				current.Category = TileCategory.Doorway;
				Library library = Session.FindLibrary(current);
				if (library != null)
				{
					this.fModified[library] = true;
				}
			}
			this.update_tiles();
		}

		private void TileStairway_Click(object sender, EventArgs e)
		{
			foreach (Tile current in this.SelectedTiles)
			{
				current.Category = TileCategory.Stairway;
				Library library = Session.FindLibrary(current);
				if (library != null)
				{
					this.fModified[library] = true;
				}
			}
			this.update_tiles();
		}

		private void TileFeature_Click(object sender, EventArgs e)
		{
			foreach (Tile current in this.SelectedTiles)
			{
				current.Category = TileCategory.Feature;
				Library library = Session.FindLibrary(current);
				if (library != null)
				{
					this.fModified[library] = true;
				}
			}
			this.update_tiles();
		}

		private void TileMap_Click(object sender, EventArgs e)
		{
			foreach (Tile current in this.SelectedTiles)
			{
				current.Category = TileCategory.Map;
				Library library = Session.FindLibrary(current);
				if (library != null)
				{
					this.fModified[library] = true;
				}
			}
			this.update_tiles();
		}

		private void TileSpecial_Click(object sender, EventArgs e)
		{
			foreach (Tile current in this.SelectedTiles)
			{
				current.Category = TileCategory.Special;
				Library library = Session.FindLibrary(current);
				if (library != null)
				{
					this.fModified[library] = true;
				}
			}
			this.update_tiles();
		}

		private void TileContextSize_Click(object sender, EventArgs e)
		{
			TileSizeForm tileSizeForm = new TileSizeForm(this.SelectedTiles);
			if (tileSizeForm.ShowDialog() == DialogResult.OK)
			{
				foreach (Tile current in this.SelectedTiles)
				{
					current.Size = tileSizeForm.TileSize;
					Library library = Session.FindLibrary(current);
					if (library != null)
					{
						this.fModified[library] = true;
					}
				}
				this.update_tiles();
			}
		}

		private void update_tiles()
		{
			Cursor.Current = Cursors.WaitCursor;
			this.TileList.BeginUpdate();
			List<Tile> list = new List<Tile>();
			if (this.SelectedLibrary != null)
			{
				list.AddRange(this.SelectedLibrary.Tiles);
			}
			else
			{
				foreach (Library current in Session.Libraries)
				{
					list.AddRange(current.Tiles);
				}
			}
			this.TileList.Groups.Clear();
			this.TileList.ShowGroups = true;
			Array values = Enum.GetValues(typeof(TileCategory));
			foreach (TileCategory tileCategory in values)
			{
				this.TileList.Groups.Add(tileCategory.ToString(), tileCategory.ToString());
			}
			this.TileList.Items.Clear();
			List<ListViewItem> list2 = new List<ListViewItem>();
			this.TileList.LargeImageList = new ImageList();
			this.TileList.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
			this.TileList.LargeImageList.ImageSize = new Size(32, 32);
			foreach (Tile current2 in list)
			{
				ListViewItem listViewItem = new ListViewItem(current2.ToString());
				listViewItem.Tag = current2;
				listViewItem.Group = this.TileList.Groups[current2.Category.ToString()];
				Image image = (current2.Image != null) ? current2.Image : current2.BlankImage;
				Image image2 = new Bitmap(32, 32);
				Graphics graphics = Graphics.FromImage(image2);
				if (current2.Size.Width > current2.Size.Height)
				{
					int num = current2.Size.Height * 32 / current2.Size.Width;
					Rectangle rect = new Rectangle(0, (32 - num) / 2, 32, num);
					graphics.DrawImage(image, rect);
				}
				else
				{
					int num2 = current2.Size.Width * 32 / current2.Size.Height;
					Rectangle rect2 = new Rectangle((32 - num2) / 2, 0, num2, 32);
					graphics.DrawImage(image, rect2);
				}
				this.TileList.LargeImageList.Images.Add(image2);
				listViewItem.ImageIndex = this.TileList.LargeImageList.Images.Count - 1;
				list2.Add(listViewItem);
			}
			this.TileList.Items.AddRange(list2.ToArray());
			if (this.TileList.Items.Count == 0)
			{
				this.TileList.ShowGroups = false;
				ListViewItem listViewItem2 = this.TileList.Items.Add("(no tiles)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
			this.TileList.EndUpdate();
			Cursor.Current = Cursors.Default;
		}

		private void TPAddBtn_Click(object sender, EventArgs e)
		{
			TerrainPowerForm terrainPowerForm = new TerrainPowerForm(new TerrainPower
			{
				Name = "New Terrain Power"
			});
			if (terrainPowerForm.ShowDialog() == DialogResult.OK)
			{
				this.SelectedLibrary.TerrainPowers.Add(terrainPowerForm.Power);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void TPAddImport_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					return;
				}
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = Program.TerrainPowerFilter;
				openFileDialog.Multiselect = true;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < fileNames.Length; i++)
					{
						string filename = fileNames[i];
						TerrainPower terrainPower = Serialisation<TerrainPower>.Load(filename, SerialisationMode.Binary);
						if (terrainPower != null)
						{
							this.SelectedLibrary.TerrainPowers.Add(terrainPower);
							this.fModified[this.SelectedLibrary] = true;
							this.update_content(true);
						}
					}
				}
			}
		}

		private void TPRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTerrainPowers.Count != 0)
			{
				string text = "Are you sure you want to delete your selection?";
				if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					return;
				}
				foreach (TerrainPower current in this.SelectedTerrainPowers)
				{
					Library library = this.SelectedLibrary;
					if (library == null)
					{
						library = Session.FindLibrary(current);
					}
					library.TerrainPowers.Remove(current);
					this.fModified[library] = true;
				}
				this.update_content(true);
			}
		}

		private void TPEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTerrainPowers.Count == 1)
			{
				Library library = Session.FindLibrary(this.SelectedTerrainPowers[0]);
				if (library == null)
				{
					return;
				}
				int index = library.TerrainPowers.IndexOf(this.SelectedTerrainPowers[0]);
				TerrainPowerForm terrainPowerForm = new TerrainPowerForm(this.SelectedTerrainPowers[0]);
				if (terrainPowerForm.ShowDialog() == DialogResult.OK)
				{
					library.TerrainPowers[index] = terrainPowerForm.Power;
					this.fModified[library] = true;
					this.update_content(true);
				}
			}
		}

		private void TPCutBtn_Click(object sender, EventArgs e)
		{
			this.TPCopyBtn_Click(sender, e);
			this.TPRemoveBtn_Click(sender, e);
		}

		private void TPCopyBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTerrainPowers != null)
			{
				Clipboard.SetData(typeof(List<TerrainPower>).ToString(), this.SelectedTerrainPowers);
			}
		}

		private void TPPasteBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Clipboard.ContainsData(typeof(List<TerrainPower>).ToString()))
			{
				List<TerrainPower> list = Clipboard.GetData(typeof(List<TerrainPower>).ToString()) as List<TerrainPower>;
				foreach (TerrainPower current in list)
				{
					TerrainPower terrainPower = current.Copy();
					terrainPower.ID = Guid.NewGuid();
					this.SelectedLibrary.TerrainPowers.Add(terrainPower);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void TPStatBlockBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedTerrainPowers.Count != 1)
			{
				return;
			}
			TerrainPowerDetailsForm terrainPowerDetailsForm = new TerrainPowerDetailsForm(this.SelectedTerrainPowers[0]);
			terrainPowerDetailsForm.ShowDialog();
		}

		private void TPToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedTerrainPowers.Count == 1)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Export";
				saveFileDialog.Filter = Program.TerrainPowerFilter;
				saveFileDialog.FileName = this.SelectedTerrainPowers[0].Name;
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					Serialisation<TerrainPower>.Save(saveFileDialog.FileName, this.SelectedTerrainPowers[0], SerialisationMode.Binary);
				}
			}
		}

		private void TPList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedTerrainPowers.Count != 0)
			{
				base.DoDragDrop(this.SelectedTerrainPowers, DragDropEffects.Move);
			}
		}

		private void TPContextRemove_Click(object sender, EventArgs e)
		{
			this.TPRemoveBtn_Click(sender, e);
		}

		private void update_terrain_powers()
		{
			Cursor.Current = Cursors.WaitCursor;
			this.TerrainPowerList.BeginUpdate();
			ListState state = ListState.GetState(this.TerrainPowerList);
			List<TerrainPower> list = new List<TerrainPower>();
			if (this.SelectedLibrary != null)
			{
				list.AddRange(this.SelectedLibrary.TerrainPowers);
				if (Session.Project == null || this.SelectedLibrary != Session.Project.Library)
				{
					goto IL_137;
				}
				List<PlotPoint> allPlotPoints = Session.Project.AllPlotPoints;
				using (List<PlotPoint>.Enumerator enumerator = allPlotPoints.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PlotPoint current = enumerator.Current;
						if (current.Element is Encounter)
						{
							Encounter encounter = current.Element as Encounter;
							foreach (CustomToken current2 in encounter.CustomTokens)
							{
								if (current2.TerrainPower != null)
								{
									list.Add(current2.TerrainPower);
								}
							}
						}
					}
					goto IL_137;
				}
			}
			foreach (Library current3 in Session.Libraries)
			{
				list.AddRange(current3.TerrainPowers);
			}
			IL_137:
			this.TerrainPowerList.Items.Clear();
			this.TerrainPowerList.ShowGroups = false;
			foreach (TerrainPower current4 in list)
			{
				if (current4 != null)
				{
					ListViewItem listViewItem = this.TerrainPowerList.Items.Add(current4.Name);
					listViewItem.SubItems.Add(current4.Type.ToString());
					listViewItem.Tag = current4;
				}
			}
			if (this.TerrainPowerList.Items.Count == 0)
			{
				ListViewItem listViewItem2 = this.TerrainPowerList.Items.Add("(no terrain powers)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
			this.TerrainPowerList.Sort();
			ListState.SetState(this.TerrainPowerList, state);
			this.TerrainPowerList.EndUpdate();
			Cursor.Current = Cursors.Default;
		}

		private void ArtifactAddAdd_Click(object sender, EventArgs e)
		{
			ArtifactBuilderForm artifactBuilderForm = new ArtifactBuilderForm(new Artifact
			{
				Name = "New Artifact"
			});
			if (artifactBuilderForm.ShowDialog() == DialogResult.OK)
			{
				this.SelectedLibrary.Artifacts.Add(artifactBuilderForm.Artifact);
				this.fModified[this.SelectedLibrary] = true;
				this.update_content(true);
			}
		}

		private void ArtifactAddImport_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary != null)
			{
				if (Session.Project != null && this.SelectedLibrary == Session.Project.Library)
				{
					return;
				}
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = Program.ArtifactFilter;
				openFileDialog.Multiselect = true;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string[] fileNames = openFileDialog.FileNames;
					for (int i = 0; i < fileNames.Length; i++)
					{
						string filename = fileNames[i];
						Artifact artifact = Serialisation<Artifact>.Load(filename, SerialisationMode.Binary);
						if (artifact != null)
						{
							this.SelectedLibrary.Artifacts.Add(artifact);
							this.fModified[this.SelectedLibrary] = true;
							this.update_content(true);
						}
					}
				}
			}
		}

		private void ArtifactRemove_Click(object sender, EventArgs e)
		{
			if (this.SelectedArtifacts.Count != 0)
			{
				string text = "Are you sure you want to delete your selection?";
				if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					return;
				}
				foreach (Artifact current in this.SelectedArtifacts)
				{
					Library library = this.SelectedLibrary;
					if (library == null)
					{
						library = Session.FindLibrary(current);
					}
					library.Artifacts.Remove(current);
					this.fModified[library] = true;
				}
				this.update_content(true);
			}
		}

		private void ArtifactEdit_Click(object sender, EventArgs e)
		{
			if (this.SelectedArtifacts.Count == 1)
			{
				Library library = Session.FindLibrary(this.SelectedArtifacts[0]);
				if (library == null)
				{
					return;
				}
				int index = library.Artifacts.IndexOf(this.SelectedArtifacts[0]);
				ArtifactBuilderForm artifactBuilderForm = new ArtifactBuilderForm(this.SelectedArtifacts[0]);
				if (artifactBuilderForm.ShowDialog() == DialogResult.OK)
				{
					library.Artifacts[index] = artifactBuilderForm.Artifact;
					this.fModified[library] = true;
					this.update_content(true);
				}
			}
		}

		private void ArtifactCut_Click(object sender, EventArgs e)
		{
			this.ArtifactCopy_Click(sender, e);
			this.ArtifactRemove_Click(sender, e);
		}

		private void ArtifactCopy_Click(object sender, EventArgs e)
		{
			if (this.SelectedArtifacts != null)
			{
				Clipboard.SetData(typeof(List<Artifact>).ToString(), this.SelectedArtifacts);
			}
		}

		private void ArtifactPaste_Click(object sender, EventArgs e)
		{
			if (this.SelectedLibrary == null)
			{
				return;
			}
			if (Clipboard.ContainsData(typeof(List<Artifact>).ToString()))
			{
				List<Artifact> list = Clipboard.GetData(typeof(List<Artifact>).ToString()) as List<Artifact>;
				foreach (Artifact current in list)
				{
					Artifact artifact = current.Copy();
					artifact.ID = Guid.NewGuid();
					this.SelectedLibrary.Artifacts.Add(artifact);
					this.fModified[this.SelectedLibrary] = true;
				}
				this.update_content(true);
			}
		}

		private void ArtifactStatBlockBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedArtifacts.Count != 1)
			{
				return;
			}
			ArtifactDetailsForm artifactDetailsForm = new ArtifactDetailsForm(this.SelectedArtifacts[0]);
			artifactDetailsForm.ShowDialog();
		}

		private void ArtifactToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedArtifacts.Count == 1)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Export";
				saveFileDialog.Filter = Program.ArtifactFilter;
				saveFileDialog.FileName = this.SelectedArtifacts[0].Name;
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					Serialisation<Artifact>.Save(saveFileDialog.FileName, this.SelectedArtifacts[0], SerialisationMode.Binary);
				}
			}
		}

		private void ArtifactList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedArtifacts.Count != 0)
			{
				base.DoDragDrop(this.SelectedArtifacts, DragDropEffects.Move);
			}
		}

		private void update_artifacts()
		{
			Cursor.Current = Cursors.WaitCursor;
			this.ArtifactList.BeginUpdate();
			ListState state = ListState.GetState(this.ArtifactList);
			List<Artifact> list = new List<Artifact>();
			if (this.SelectedLibrary != null)
			{
				list.AddRange(this.SelectedLibrary.Artifacts);
				if (Session.Project == null || this.SelectedLibrary != Session.Project.Library)
				{
					goto IL_195;
				}
				foreach (Parcel current in Session.Project.TreasureParcels)
				{
					if (current.ArtifactID != Guid.Empty)
					{
						Artifact artifact = Session.FindArtifact(current.ArtifactID, SearchType.Global);
						if (artifact != null)
						{
							list.Add(artifact);
						}
					}
				}
				List<PlotPoint> allPlotPoints = Session.Project.AllPlotPoints;
				using (List<PlotPoint>.Enumerator enumerator2 = allPlotPoints.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						PlotPoint current2 = enumerator2.Current;
						foreach (Parcel current3 in current2.Parcels)
						{
							if (current3.ArtifactID != Guid.Empty)
							{
								Artifact artifact2 = Session.FindArtifact(current3.ArtifactID, SearchType.Global);
								if (artifact2 != null)
								{
									list.Add(artifact2);
								}
							}
						}
					}
					goto IL_195;
				}
			}
			foreach (Library current4 in Session.Libraries)
			{
				list.AddRange(current4.Artifacts);
			}
			IL_195:
			this.ArtifactList.Items.Clear();
			this.ArtifactList.ShowGroups = false;
			foreach (Artifact current5 in list)
			{
				if (current5 != null)
				{
					ListViewItem listViewItem = this.ArtifactList.Items.Add(current5.Name);
					listViewItem.SubItems.Add(current5.Tier.ToString());
					listViewItem.Tag = current5;
				}
			}
			if (this.ArtifactList.Items.Count == 0)
			{
				ListViewItem listViewItem2 = this.ArtifactList.Items.Add("(no artifacts)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
			this.ArtifactList.Sort();
			ListState.SetState(this.ArtifactList, state);
			this.ArtifactList.EndUpdate();
			Cursor.Current = Cursors.Default;
		}

		private void save(Library lib)
		{
			GC.Collect();
			string libraryFilename = Session.GetLibraryFilename(lib);
			Serialisation<Library>.Save(libraryFilename, lib, SerialisationMode.Binary);
		}

		private void show_help(bool show)
		{
			this.HelpPanel.Visible = show;
		}

		private void update_libraries()
		{
			this.LibraryTree.Nodes.Clear();
			if (Session.Libraries.Count != 0)
			{
				TreeNode treeNode = this.LibraryTree.Nodes.Add("All Libraries");
				treeNode.ImageIndex = 0;
				foreach (Library current in Session.Libraries)
				{
					TreeNode treeNode2 = treeNode.Nodes.Add(current.Name);
					treeNode2.Tag = current;
				}
				treeNode.Expand();
			}
			else if (Session.Project == null)
			{
				TreeNode treeNode3 = this.LibraryTree.Nodes.Add("(no libraries installed)");
				treeNode3.ForeColor = SystemColors.GrayText;
				this.show_help(true);
			}
			if (Session.Project != null)
			{
				TreeNode treeNode4 = this.LibraryTree.Nodes.Add(Session.Project.Name);
				treeNode4.Tag = Session.Project.Library;
			}
		}

		private void update_content(bool force_refresh)
		{
			if (force_refresh)
			{
				this.fCleanPages.Clear();
			}
			if (this.Pages.SelectedTab == this.CreaturesPage && !this.fCleanPages.Contains(this.CreaturesPage))
			{
				this.update_creatures();
				this.fCleanPages.Add(this.CreaturesPage);
			}
			if (this.Pages.SelectedTab == this.TemplatesPage && !this.fCleanPages.Contains(this.TemplatesPage))
			{
				this.update_templates();
				this.fCleanPages.Add(this.TemplatesPage);
			}
			if (this.Pages.SelectedTab == this.TrapsPage && !this.fCleanPages.Contains(this.TrapsPage))
			{
				this.update_traps();
				this.fCleanPages.Add(this.TrapsPage);
			}
			if (this.Pages.SelectedTab == this.ChallengePage && !this.fCleanPages.Contains(this.ChallengePage))
			{
				this.update_challenges();
				this.fCleanPages.Add(this.ChallengePage);
			}
			if (this.Pages.SelectedTab == this.MagicItemsPage && !this.fCleanPages.Contains(this.MagicItemsPage))
			{
				this.update_magic_item_sets();
				this.update_magic_item_versions();
				this.fCleanPages.Add(this.MagicItemsPage);
			}
			if (this.Pages.SelectedTab == this.TilesPage && !this.fCleanPages.Contains(this.TilesPage))
			{
				this.update_tiles();
				this.fCleanPages.Add(this.TilesPage);
			}
			if (this.Pages.SelectedTab == this.TerrainPowersPage && !this.fCleanPages.Contains(this.TerrainPowersPage))
			{
				this.update_terrain_powers();
				this.fCleanPages.Add(this.TerrainPowersPage);
			}
			if (this.Pages.SelectedTab == this.ArtifactPage && !this.fCleanPages.Contains(this.ArtifactPage))
			{
				this.update_artifacts();
				this.fCleanPages.Add(this.ArtifactPage);
			}
		}
	}
}
