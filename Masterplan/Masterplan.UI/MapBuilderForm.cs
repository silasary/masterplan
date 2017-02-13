using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Events;
using Masterplan.Tools;
using Masterplan.Tools.Generators;
using Masterplan.Wizards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class MapBuilderForm : Form
	{
		private IContainer components;

		private SplitContainer Splitter;

		private ListView TileList;

		private ColumnHeader TileHdr;

		private MapView MapView;

		private ToolStrip Toolbar;

		private ToolStripLabel NameLbl;

		private ToolStripTextBox NameBox;

		private ToolStripDropDownButton OrderingBtn;

		private ToolStripMenuItem OrderingFront;

		private ToolStripMenuItem OrderingBack;

		private ToolStripDropDownButton at;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStrip TileToolbar;

		private ToolStripMenuItem ToolsHighlightAreas;

		private TabControl Pages;

		private TabPage TilesPage;

		private TabPage AreasPage;

		private ToolStrip AreaToolbar;

		private ListView AreaList;

		private ToolStripButton AreaRemoveBtn;

		private ToolStripButton AreaEditBtn;

		private ColumnHeader AreaHdr;

		private ToolStripDropDownButton TilesViewBtn;

		private ToolStripMenuItem ViewGroupBy;

		private ToolStripMenuItem GroupByTileSet;

		private ToolStripMenuItem GroupBySize;

		private ContextMenuStrip MapContextMenu;

		private ToolStripMenuItem ContextCreate;

		private ToolStripMenuItem ContextClear;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem GroupByCategory;

		private ToolStripMenuItem ToolsAutoBuild;

		private ToolTip Tooltip;

		private ToolStripSeparator toolStripSeparator2;

		private TrackBar ZoomGauge;

		private ToolStripMenuItem ToolsNavigate;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem ToolsReset;

		private ToolStripMenuItem ContextSelect;

		private ToolStripSeparator toolStripMenuItem1;

		private ContextMenuStrip TileContextMenu;

		private ToolStripMenuItem TileContextRemove;

		private ToolStripMenuItem TileContextSwap;

		private ToolStripSeparator toolStripMenuItem2;

		private ToolStripMenuItem ToolsSelectBackground;

		private ToolStripMenuItem ToolsClearBackground;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton FullMapBtn;

		private Panel MainPanel;

		private Button OKBtn;

		private Button CancelBtn;

		private ToolStripMenuItem ToolsImportMap;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripMenuItem TileContextDuplicate;

		private ToolStripSplitButton RemoveBtn;

		private ToolStripSplitButton RotateLeftBtn;

		private ToolStripSplitButton RotateRightBtn;

		private ToolStripMenuItem clearMapToolStripMenuItem;

		private ToolStripMenuItem rotateMapLeftToolStripMenuItem;

		private ToolStripMenuItem rotateMapRightToolStripMenuItem;

		private Panel MapFilterPanel;

		private ToolStripMenuItem ViewSize;

		private ToolStripMenuItem SizeSmall;

		private ToolStripMenuItem SizeMedium;

		private ToolStripMenuItem SizeLarge;

		private ToolStripButton FilterBtn;

		private Button SelectLibrariesBtn;

		private TextBox SearchBox;

		private Label SearchLbl;

		private Map fMap;

		private Dictionary<Guid, bool> fTileSets = new Dictionary<Guid, bool>();

		public Map Map
		{
			get
			{
				return this.fMap;
			}
		}

		public Tile SelectedTile
		{
			get
			{
				if (this.TileList.SelectedItems.Count != 0)
				{
					return this.TileList.SelectedItems[0].Tag as Tile;
				}
				return null;
			}
		}

		public MapArea SelectedArea
		{
			get
			{
				if (this.AreaList.SelectedItems.Count != 0)
				{
					return this.AreaList.SelectedItems[0].Tag as MapArea;
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
			this.components = new Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(MapBuilderForm));
			this.Splitter = new SplitContainer();
			this.MapView = new MapView();
			this.Toolbar = new ToolStrip();
			this.RemoveBtn = new ToolStripSplitButton();
			this.clearMapToolStripMenuItem = new ToolStripMenuItem();
			this.RotateLeftBtn = new ToolStripSplitButton();
			this.rotateMapLeftToolStripMenuItem = new ToolStripMenuItem();
			this.RotateRightBtn = new ToolStripSplitButton();
			this.rotateMapRightToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.OrderingBtn = new ToolStripDropDownButton();
			this.OrderingFront = new ToolStripMenuItem();
			this.OrderingBack = new ToolStripMenuItem();
			this.at = new ToolStripDropDownButton();
			this.ToolsNavigate = new ToolStripMenuItem();
			this.ToolsReset = new ToolStripMenuItem();
			this.toolStripSeparator6 = new ToolStripSeparator();
			this.ToolsHighlightAreas = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.ToolsSelectBackground = new ToolStripMenuItem();
			this.ToolsClearBackground = new ToolStripMenuItem();
			this.toolStripMenuItem2 = new ToolStripSeparator();
			this.ToolsImportMap = new ToolStripMenuItem();
			this.toolStripSeparator8 = new ToolStripSeparator();
			this.ToolsAutoBuild = new ToolStripMenuItem();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.NameLbl = new ToolStripLabel();
			this.NameBox = new ToolStripTextBox();
			this.ZoomGauge = new TrackBar();
			this.Pages = new TabControl();
			this.TilesPage = new TabPage();
			this.TileList = new ListView();
			this.TileHdr = new ColumnHeader();
			this.MapFilterPanel = new Panel();
			this.SelectLibrariesBtn = new Button();
			this.SearchBox = new TextBox();
			this.SearchLbl = new Label();
			this.TileToolbar = new ToolStrip();
			this.TilesViewBtn = new ToolStripDropDownButton();
			this.ViewGroupBy = new ToolStripMenuItem();
			this.GroupByTileSet = new ToolStripMenuItem();
			this.GroupBySize = new ToolStripMenuItem();
			this.GroupByCategory = new ToolStripMenuItem();
			this.ViewSize = new ToolStripMenuItem();
			this.SizeSmall = new ToolStripMenuItem();
			this.SizeMedium = new ToolStripMenuItem();
			this.SizeLarge = new ToolStripMenuItem();
			this.FilterBtn = new ToolStripButton();
			this.AreasPage = new TabPage();
			this.AreaList = new ListView();
			this.AreaHdr = new ColumnHeader();
			this.AreaToolbar = new ToolStrip();
			this.AreaRemoveBtn = new ToolStripButton();
			this.AreaEditBtn = new ToolStripButton();
			this.toolStripSeparator7 = new ToolStripSeparator();
			this.FullMapBtn = new ToolStripButton();
			this.MapContextMenu = new ContextMenuStrip(this.components);
			this.ContextSelect = new ToolStripMenuItem();
			this.ContextClear = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this.ContextCreate = new ToolStripMenuItem();
			this.Tooltip = new ToolTip(this.components);
			this.TileContextMenu = new ContextMenuStrip(this.components);
			this.TileContextRemove = new ToolStripMenuItem();
			this.TileContextSwap = new ToolStripMenuItem();
			this.TileContextDuplicate = new ToolStripMenuItem();
			this.MainPanel = new Panel();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.Toolbar.SuspendLayout();
			((ISupportInitialize)this.ZoomGauge).BeginInit();
			this.Pages.SuspendLayout();
			this.TilesPage.SuspendLayout();
			this.MapFilterPanel.SuspendLayout();
			this.TileToolbar.SuspendLayout();
			this.AreasPage.SuspendLayout();
			this.AreaToolbar.SuspendLayout();
			this.MapContextMenu.SuspendLayout();
			this.TileContextMenu.SuspendLayout();
			this.MainPanel.SuspendLayout();
			base.SuspendLayout();
			this.Splitter.Dock = DockStyle.Fill;
			this.Splitter.FixedPanel = FixedPanel.Panel2;
			this.Splitter.Location = new Point(0, 0);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.MapView);
			this.Splitter.Panel1.Controls.Add(this.Toolbar);
			this.Splitter.Panel1.Controls.Add(this.ZoomGauge);
			this.Splitter.Panel2.Controls.Add(this.Pages);
			this.Splitter.Size = new Size(882, 401);
			this.Splitter.SplitterDistance = 675;
			this.Splitter.TabIndex = 0;
			this.MapView.AllowDrawing = false;
			this.MapView.AllowDrop = true;
			this.MapView.AllowLinkCreation = false;
			this.MapView.AllowScrolling = false;
			this.MapView.BackgroundMap = null;
			this.MapView.BorderSize = 3;
			this.MapView.Caption = "";
			this.MapView.Cursor = Cursors.Default;
			this.MapView.Dock = DockStyle.Fill;
			this.MapView.Encounter = null;
			this.MapView.FrameType = MapDisplayType.Dimmed;
			this.MapView.HighlightAreas = true;
			this.MapView.HoverToken = null;
			this.MapView.LineOfSight = false;
			this.MapView.Location = new Point(0, 25);
			this.MapView.Map = null;
			this.MapView.Mode = MapViewMode.Normal;
			this.MapView.Name = "MapView";
			this.MapView.Plot = null;
			this.MapView.ScalingFactor = 1.0;
			this.MapView.SelectedArea = null;
			this.MapView.SelectedTiles = null;
			this.MapView.Selection = new Rectangle(0, 0, 0, 0);
			this.MapView.ShowAuras = true;
			this.MapView.ShowConditions = true;
			this.MapView.ShowCreatureLabels = true;
			this.MapView.ShowCreatures = CreatureViewMode.All;
			this.MapView.ShowGrid = MapGridMode.Behind;
			this.MapView.ShowGridLabels = false;
			this.MapView.ShowHealthBars = false;
			this.MapView.ShowPictureTokens = true;
			this.MapView.Size = new Size(675, 331);
			this.MapView.TabIndex = 0;
			this.MapView.Tactical = false;
			this.MapView.TokenLinks = null;
			this.MapView.Viewpoint = new Rectangle(0, 0, 0, 0);
			this.MapView.ItemDropped += new EventHandler(this.MapView_ItemDropped);
			this.MapView.RegionSelected += new EventHandler(this.MapView_RegionSelected);
			this.MapView.HighlightedAreaChanged += new EventHandler(this.MapView_HoverAreaChanged);
			this.MapView.TileContext += new EventHandler(this.MapView_TileContext);
			this.MapView.MouseZoomed += new MouseEventHandler(this.MapView_MouseZoomed);
			this.MapView.ItemRemoved += new EventHandler(this.MapView_ItemRemoved);
			this.MapView.ItemMoved += new MovementEventHandler(this.MapView_ItemMoved);
			this.MapView.AreaActivated += new MapAreaEventHandler(this.MapView_AreaActivated);
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.RemoveBtn,
				this.RotateLeftBtn,
				this.RotateRightBtn,
				this.toolStripSeparator1,
				this.OrderingBtn,
				this.at,
				this.toolStripSeparator3,
				this.NameLbl,
				this.NameBox
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(675, 25);
			this.Toolbar.TabIndex = 1;
			this.Toolbar.Text = "toolStrip1";
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.clearMapToolStripMenuItem
			});
			this.RemoveBtn.Image = (Image)resources.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new Size(66, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.ButtonClick += new EventHandler(this.RemoveBtn_Click);
			this.clearMapToolStripMenuItem.Name = "clearMapToolStripMenuItem";
			this.clearMapToolStripMenuItem.Size = new Size(145, 22);
			this.clearMapToolStripMenuItem.Text = "Clear All Tiles";
			this.clearMapToolStripMenuItem.Click += new EventHandler(this.ToolsClearAll_Click);
			this.RotateLeftBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RotateLeftBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.rotateMapLeftToolStripMenuItem
			});
			this.RotateLeftBtn.Image = (Image)resources.GetObject("RotateLeftBtn.Image");
			this.RotateLeftBtn.ImageTransparentColor = Color.Magenta;
			this.RotateLeftBtn.Name = "RotateLeftBtn";
			this.RotateLeftBtn.Size = new Size(80, 22);
			this.RotateLeftBtn.Text = "Rotate Left";
			this.RotateLeftBtn.ButtonClick += new EventHandler(this.RotateLeftBtn_Click);
			this.rotateMapLeftToolStripMenuItem.Name = "rotateMapLeftToolStripMenuItem";
			this.rotateMapLeftToolStripMenuItem.Size = new Size(158, 22);
			this.rotateMapLeftToolStripMenuItem.Text = "Rotate Map Left";
			this.rotateMapLeftToolStripMenuItem.Click += new EventHandler(this.RotateMapLeft_Click);
			this.RotateRightBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RotateRightBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.rotateMapRightToolStripMenuItem
			});
			this.RotateRightBtn.Image = (Image)resources.GetObject("RotateRightBtn.Image");
			this.RotateRightBtn.ImageTransparentColor = Color.Magenta;
			this.RotateRightBtn.Name = "RotateRightBtn";
			this.RotateRightBtn.Size = new Size(88, 22);
			this.RotateRightBtn.Text = "Rotate Right";
			this.RotateRightBtn.ButtonClick += new EventHandler(this.RotateRightBtn_Click);
			this.rotateMapRightToolStripMenuItem.Name = "rotateMapRightToolStripMenuItem";
			this.rotateMapRightToolStripMenuItem.Size = new Size(166, 22);
			this.rotateMapRightToolStripMenuItem.Text = "Rotate Map Right";
			this.rotateMapRightToolStripMenuItem.Click += new EventHandler(this.RotateMapRight_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.OrderingBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.OrderingBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.OrderingFront,
				this.OrderingBack
			});
			this.OrderingBtn.Image = (Image)resources.GetObject("OrderingBtn.Image");
			this.OrderingBtn.ImageTransparentColor = Color.Magenta;
			this.OrderingBtn.Name = "OrderingBtn";
			this.OrderingBtn.Size = new Size(67, 22);
			this.OrderingBtn.Text = "Ordering";
			this.OrderingFront.Name = "OrderingFront";
			this.OrderingFront.Size = new Size(147, 22);
			this.OrderingFront.Text = "Bring to Front";
			this.OrderingFront.Click += new EventHandler(this.OrderingFront_Click);
			this.OrderingBack.Name = "OrderingBack";
			this.OrderingBack.Size = new Size(147, 22);
			this.OrderingBack.Text = "Send to Back";
			this.OrderingBack.Click += new EventHandler(this.OrderingBack_Click);
			this.at.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.at.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ToolsNavigate,
				this.ToolsReset,
				this.toolStripSeparator6,
				this.ToolsHighlightAreas,
				this.toolStripSeparator2,
				this.ToolsSelectBackground,
				this.ToolsClearBackground,
				this.toolStripMenuItem2,
				this.ToolsImportMap,
				this.toolStripSeparator8,
				this.ToolsAutoBuild
			});
			this.at.Image = (Image)resources.GetObject("at.Image");
			this.at.ImageTransparentColor = Color.Magenta;
			this.at.Name = "at";
			this.at.Size = new Size(49, 22);
			this.at.Text = "Tools";
			this.ToolsNavigate.Name = "ToolsNavigate";
			this.ToolsNavigate.Size = new Size(208, 22);
			this.ToolsNavigate.Text = "Scroll and Zoom";
			this.ToolsNavigate.Click += new EventHandler(this.ToolsNavigate_Click);
			this.ToolsReset.Name = "ToolsReset";
			this.ToolsReset.Size = new Size(208, 22);
			this.ToolsReset.Text = "Reset View";
			this.ToolsReset.Click += new EventHandler(this.ToolsReset_Click);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new Size(205, 6);
			this.ToolsHighlightAreas.Name = "ToolsHighlightAreas";
			this.ToolsHighlightAreas.Size = new Size(208, 22);
			this.ToolsHighlightAreas.Text = "Highlight Areas";
			this.ToolsHighlightAreas.Click += new EventHandler(this.ToolsHighlightAreas_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(205, 6);
			this.ToolsSelectBackground.Name = "ToolsSelectBackground";
			this.ToolsSelectBackground.Size = new Size(208, 22);
			this.ToolsSelectBackground.Text = "Select Background Map...";
			this.ToolsSelectBackground.Click += new EventHandler(this.ToolsSelectBackground_Click);
			this.ToolsClearBackground.Name = "ToolsClearBackground";
			this.ToolsClearBackground.Size = new Size(208, 22);
			this.ToolsClearBackground.Text = "Clear Background Map";
			this.ToolsClearBackground.Click += new EventHandler(this.ToolsClearBackground_Click);
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new Size(205, 6);
			this.ToolsImportMap.Name = "ToolsImportMap";
			this.ToolsImportMap.Size = new Size(208, 22);
			this.ToolsImportMap.Text = "Import Map Image...";
			this.ToolsImportMap.Click += new EventHandler(this.ToolsImportMap_Click);
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new Size(205, 6);
			this.ToolsAutoBuild.Name = "ToolsAutoBuild";
			this.ToolsAutoBuild.Size = new Size(208, 22);
			this.ToolsAutoBuild.Text = "AutoBuild...";
			this.ToolsAutoBuild.Click += new EventHandler(this.ToolsAutoBuild_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new Size(6, 25);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(69, 22);
			this.NameLbl.Text = "Map Name:";
			this.NameBox.BorderStyle = BorderStyle.FixedSingle;
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(200, 25);
			this.NameBox.TextChanged += new EventHandler(this.NameBox_TextChanged);
			this.ZoomGauge.Dock = DockStyle.Bottom;
			this.ZoomGauge.Location = new Point(0, 356);
			this.ZoomGauge.Maximum = 100;
			this.ZoomGauge.Name = "ZoomGauge";
			this.ZoomGauge.Size = new Size(675, 45);
			this.ZoomGauge.TabIndex = 2;
			this.ZoomGauge.TickFrequency = 10;
			this.ZoomGauge.TickStyle = TickStyle.Both;
			this.ZoomGauge.Value = 50;
			this.ZoomGauge.Visible = false;
			this.ZoomGauge.Scroll += new EventHandler(this.ZoomGauge_Scroll);
			this.Pages.Controls.Add(this.TilesPage);
			this.Pages.Controls.Add(this.AreasPage);
			this.Pages.Dock = DockStyle.Fill;
			this.Pages.Location = new Point(0, 0);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(203, 401);
			this.Pages.TabIndex = 3;
			this.TilesPage.Controls.Add(this.TileList);
			this.TilesPage.Controls.Add(this.MapFilterPanel);
			this.TilesPage.Controls.Add(this.TileToolbar);
			this.TilesPage.Location = new Point(4, 22);
			this.TilesPage.Name = "TilesPage";
			this.TilesPage.Padding = new Padding(3);
			this.TilesPage.Size = new Size(195, 375);
			this.TilesPage.TabIndex = 0;
			this.TilesPage.Text = "Tiles";
			this.TilesPage.UseVisualStyleBackColor = true;
			this.TileList.Columns.AddRange(new ColumnHeader[]
			{
				this.TileHdr
			});
			this.TileList.Dock = DockStyle.Fill;
			this.TileList.FullRowSelect = true;
			this.TileList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TileList.HideSelection = false;
			this.TileList.Location = new Point(3, 87);
			this.TileList.MultiSelect = false;
			this.TileList.Name = "TileList";
			this.TileList.Size = new Size(189, 285);
			this.TileList.TabIndex = 1;
			this.TileList.UseCompatibleStateImageBehavior = false;
			this.TileList.DoubleClick += new EventHandler(this.TileList_DoubleClick);
			this.TileList.ItemDrag += new ItemDragEventHandler(this.TileList_ItemDrag);
			this.TileHdr.Text = "Tile";
			this.TileHdr.Width = 120;
			this.MapFilterPanel.Controls.Add(this.SelectLibrariesBtn);
			this.MapFilterPanel.Controls.Add(this.SearchBox);
			this.MapFilterPanel.Controls.Add(this.SearchLbl);
			this.MapFilterPanel.Dock = DockStyle.Top;
			this.MapFilterPanel.Location = new Point(3, 28);
			this.MapFilterPanel.Name = "MapFilterPanel";
			this.MapFilterPanel.Size = new Size(189, 59);
			this.MapFilterPanel.TabIndex = 3;
			this.SelectLibrariesBtn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.SelectLibrariesBtn.Location = new Point(53, 29);
			this.SelectLibrariesBtn.Name = "SelectLibrariesBtn";
			this.SelectLibrariesBtn.Size = new Size(133, 23);
			this.SelectLibrariesBtn.TabIndex = 2;
			this.SelectLibrariesBtn.Text = "Select Libraries";
			this.SelectLibrariesBtn.UseVisualStyleBackColor = true;
			this.SelectLibrariesBtn.Click += new EventHandler(this.ViewSelectLibraries_Click);
			this.SearchBox.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.SearchBox.Location = new Point(53, 3);
			this.SearchBox.Name = "SearchBox";
			this.SearchBox.Size = new Size(133, 20);
			this.SearchBox.TabIndex = 1;
			this.SearchBox.TextChanged += new EventHandler(this.SearchBox_TextChanged);
			this.SearchLbl.AutoSize = true;
			this.SearchLbl.Location = new Point(3, 6);
			this.SearchLbl.Name = "SearchLbl";
			this.SearchLbl.Size = new Size(44, 13);
			this.SearchLbl.TabIndex = 0;
			this.SearchLbl.Text = "Search:";
			this.TileToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.TilesViewBtn,
				this.FilterBtn
			});
			this.TileToolbar.Location = new Point(3, 3);
			this.TileToolbar.Name = "TileToolbar";
			this.TileToolbar.Size = new Size(189, 25);
			this.TileToolbar.TabIndex = 2;
			this.TileToolbar.Text = "toolStrip1";
			this.TilesViewBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TilesViewBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ViewGroupBy,
				this.ViewSize
			});
			this.TilesViewBtn.Image = (Image)resources.GetObject("TilesViewBtn.Image");
			this.TilesViewBtn.ImageTransparentColor = Color.Magenta;
			this.TilesViewBtn.Name = "TilesViewBtn";
			this.TilesViewBtn.Size = new Size(45, 22);
			this.TilesViewBtn.Text = "View";
			this.ViewGroupBy.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.GroupByTileSet,
				this.GroupBySize,
				this.GroupByCategory
			});
			this.ViewGroupBy.Name = "ViewGroupBy";
			this.ViewGroupBy.Size = new Size(152, 22);
			this.ViewGroupBy.Text = "Group By";
			this.GroupByTileSet.Name = "GroupByTileSet";
			this.GroupByTileSet.Size = new Size(152, 22);
			this.GroupByTileSet.Text = "Library";
			this.GroupByTileSet.Click += new EventHandler(this.GroupByTileSet_Click);
			this.GroupBySize.Name = "GroupBySize";
			this.GroupBySize.Size = new Size(152, 22);
			this.GroupBySize.Text = "Tile Size";
			this.GroupBySize.Click += new EventHandler(this.GroupBySize_Click);
			this.GroupByCategory.Name = "GroupByCategory";
			this.GroupByCategory.Size = new Size(152, 22);
			this.GroupByCategory.Text = "Tile Category";
			this.GroupByCategory.Click += new EventHandler(this.GroupByCategory_Click);
			this.ViewSize.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.SizeSmall,
				this.SizeMedium,
				this.SizeLarge
			});
			this.ViewSize.Name = "ViewSize";
			this.ViewSize.Size = new Size(152, 22);
			this.ViewSize.Text = "Size";
			this.SizeSmall.Name = "SizeSmall";
			this.SizeSmall.Size = new Size(119, 22);
			this.SizeSmall.Text = "Small";
			this.SizeSmall.Click += new EventHandler(this.SizeSmall_Click);
			this.SizeMedium.Name = "SizeMedium";
			this.SizeMedium.Size = new Size(119, 22);
			this.SizeMedium.Text = "Medium";
			this.SizeMedium.Click += new EventHandler(this.SizeMedium_Click);
			this.SizeLarge.Name = "SizeLarge";
			this.SizeLarge.Size = new Size(119, 22);
			this.SizeLarge.Text = "Large";
			this.SizeLarge.Click += new EventHandler(this.SizeLarge_Click);
			this.FilterBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FilterBtn.Image = (Image)resources.GetObject("FilterBtn.Image");
			this.FilterBtn.ImageTransparentColor = Color.Magenta;
			this.FilterBtn.Name = "FilterBtn";
			this.FilterBtn.Size = new Size(83, 22);
			this.FilterBtn.Text = "Filter This List";
			this.FilterBtn.Click += new EventHandler(this.FilterBtn_Click);
			this.AreasPage.Controls.Add(this.AreaList);
			this.AreasPage.Controls.Add(this.AreaToolbar);
			this.AreasPage.Location = new Point(4, 22);
			this.AreasPage.Name = "AreasPage";
			this.AreasPage.Padding = new Padding(3);
			this.AreasPage.Size = new Size(195, 375);
			this.AreasPage.TabIndex = 1;
			this.AreasPage.Text = "Map Areas";
			this.AreasPage.UseVisualStyleBackColor = true;
			this.AreaList.Columns.AddRange(new ColumnHeader[]
			{
				this.AreaHdr
			});
			this.AreaList.Dock = DockStyle.Fill;
			this.AreaList.FullRowSelect = true;
			this.AreaList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.AreaList.HideSelection = false;
			this.AreaList.Location = new Point(3, 28);
			this.AreaList.MultiSelect = false;
			this.AreaList.Name = "AreaList";
			this.AreaList.Size = new Size(189, 344);
			this.AreaList.TabIndex = 1;
			this.AreaList.UseCompatibleStateImageBehavior = false;
			this.AreaList.View = View.Details;
			this.AreaList.SelectedIndexChanged += new EventHandler(this.AreaList_SelectedIndexChanged);
			this.AreaList.DoubleClick += new EventHandler(this.AreaEditBtn_Click);
			this.AreaHdr.Text = "Area Name";
			this.AreaHdr.Width = 150;
			this.AreaToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.AreaRemoveBtn,
				this.AreaEditBtn,
				this.toolStripSeparator7,
				this.FullMapBtn
			});
			this.AreaToolbar.Location = new Point(3, 3);
			this.AreaToolbar.Name = "AreaToolbar";
			this.AreaToolbar.Size = new Size(189, 25);
			this.AreaToolbar.TabIndex = 0;
			this.AreaToolbar.Text = "toolStrip1";
			this.AreaRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AreaRemoveBtn.Image = (Image)resources.GetObject("AreaRemoveBtn.Image");
			this.AreaRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.AreaRemoveBtn.Name = "AreaRemoveBtn";
			this.AreaRemoveBtn.Size = new Size(54, 22);
			this.AreaRemoveBtn.Text = "Remove";
			this.AreaRemoveBtn.Click += new EventHandler(this.AreaRemoveBtn_Click);
			this.AreaEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AreaEditBtn.Image = (Image)resources.GetObject("AreaEditBtn.Image");
			this.AreaEditBtn.ImageTransparentColor = Color.Magenta;
			this.AreaEditBtn.Name = "AreaEditBtn";
			this.AreaEditBtn.Size = new Size(31, 22);
			this.AreaEditBtn.Text = "Edit";
			this.AreaEditBtn.Click += new EventHandler(this.AreaEditBtn_Click);
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new Size(6, 25);
			this.FullMapBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FullMapBtn.Image = (Image)resources.GetObject("FullMapBtn.Image");
			this.FullMapBtn.ImageTransparentColor = Color.Magenta;
			this.FullMapBtn.Name = "FullMapBtn";
			this.FullMapBtn.Size = new Size(57, 22);
			this.FullMapBtn.Text = "Full Map";
			this.FullMapBtn.Click += new EventHandler(this.FullMapBtn_Click);
			this.MapContextMenu.Items.AddRange(new ToolStripItem[]
			{
				this.ContextSelect,
				this.ContextClear,
				this.toolStripMenuItem1,
				this.ContextCreate
			});
			this.MapContextMenu.Name = "MapContextMenu";
			this.MapContextMenu.Size = new Size(172, 76);
			this.ContextSelect.Name = "ContextSelect";
			this.ContextSelect.Size = new Size(171, 22);
			this.ContextSelect.Text = "Select Tiles";
			this.ContextSelect.Click += new EventHandler(this.ContextSelect_Click);
			this.ContextClear.Name = "ContextClear";
			this.ContextClear.Size = new Size(171, 22);
			this.ContextClear.Text = "Clear Tiles";
			this.ContextClear.Click += new EventHandler(this.ContextClear_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new Size(168, 6);
			this.ContextCreate.Name = "ContextCreate";
			this.ContextCreate.Size = new Size(171, 22);
			this.ContextCreate.Text = "Create Map Area...";
			this.ContextCreate.Click += new EventHandler(this.ContextCreate_Click);
			this.TileContextMenu.Items.AddRange(new ToolStripItem[]
			{
				this.TileContextRemove,
				this.TileContextSwap,
				this.TileContextDuplicate
			});
			this.TileContextMenu.Name = "TileContextMenu";
			this.TileContextMenu.Size = new Size(147, 70);
			this.TileContextRemove.Name = "TileContextRemove";
			this.TileContextRemove.Size = new Size(146, 22);
			this.TileContextRemove.Text = "Remove Tile";
			this.TileContextRemove.Click += new EventHandler(this.RemoveBtn_Click);
			this.TileContextSwap.Name = "TileContextSwap";
			this.TileContextSwap.Size = new Size(146, 22);
			this.TileContextSwap.Text = "Swap Tile";
			this.TileContextSwap.Click += new EventHandler(this.TileContextSwap_Click);
			this.TileContextDuplicate.Name = "TileContextDuplicate";
			this.TileContextDuplicate.Size = new Size(146, 22);
			this.TileContextDuplicate.Text = "Duplicate Tile";
			this.TileContextDuplicate.Click += new EventHandler(this.TileContextDuplicate_Click);
			this.MainPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.MainPanel.Controls.Add(this.Splitter);
			this.MainPanel.Location = new Point(12, 12);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new Size(882, 401);
			this.MainPanel.TabIndex = 3;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(738, 419);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 4;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(819, 419);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 5;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(906, 454);
			base.Controls.Add(this.MainPanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MinimizeBox = false;
			base.Name = "MapForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Map Editor";
			base.FormClosed += new FormClosedEventHandler(this.MapForm_FormClosed);
			base.FormClosing += new FormClosingEventHandler(this.MapForm_FormClosing);
			base.KeyDown += new KeyEventHandler(this.MapForm_KeyDown);
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel1.PerformLayout();
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			((ISupportInitialize)this.ZoomGauge).EndInit();
			this.Pages.ResumeLayout(false);
			this.TilesPage.ResumeLayout(false);
			this.TilesPage.PerformLayout();
			this.MapFilterPanel.ResumeLayout(false);
			this.MapFilterPanel.PerformLayout();
			this.TileToolbar.ResumeLayout(false);
			this.TileToolbar.PerformLayout();
			this.AreasPage.ResumeLayout(false);
			this.AreasPage.PerformLayout();
			this.AreaToolbar.ResumeLayout(false);
			this.AreaToolbar.PerformLayout();
			this.MapContextMenu.ResumeLayout(false);
			this.TileContextMenu.ResumeLayout(false);
			this.MainPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public MapBuilderForm(Map m, bool autobuild)
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			List<Library> list = new List<Library>();
			list.AddRange(Session.Libraries);
			if (Session.Project != null)
			{
				list.Add(Session.Project.Library);
			}
			int num = 0;
			foreach (Library current in list)
			{
				if (Session.Preferences.TileLibraries != null)
				{
					this.fTileSets[current.ID] = Session.Preferences.TileLibraries.Contains(current.ID);
				}
				else
				{
					this.fTileSets[current.ID] = true;
				}
				if (this.fTileSets[current.ID])
				{
					num = current.Tiles.Count;
				}
			}
			if (num == 0)
			{
				foreach (Library current2 in list)
				{
					this.fTileSets[current2.ID] = true;
				}
			}
			this.MapFilterPanel.Visible = false;
			this.populate_tiles();
			this.fMap = m.Copy();
			this.MapView.Map = this.fMap;
			this.NameBox.Text = this.fMap.Name;
			if (autobuild)
			{
				Cursor.Current = Cursors.WaitCursor;
				this.ToolsAutoBuild_Click(null, null);
				foreach (MapArea current3 in this.fMap.Areas)
				{
					current3.Name = RoomBuilder.Name();
					current3.Details = RoomBuilder.Details();
				}
				Cursor.Current = Cursors.Default;
			}
			this.update_areas();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = (this.MapView.SelectedTiles != null && this.MapView.SelectedTiles.Count != 0);
			this.RotateLeftBtn.Enabled = (this.MapView.SelectedTiles != null && this.MapView.SelectedTiles.Count != 0);
			this.RotateRightBtn.Enabled = (this.MapView.SelectedTiles != null && this.MapView.SelectedTiles.Count != 0);
			this.OrderingBtn.Enabled = (this.MapView.SelectedTiles != null && this.MapView.SelectedTiles.Count == 1);
			this.ToolsHighlightAreas.Checked = this.MapView.HighlightAreas;
			this.ToolsNavigate.Checked = this.MapView.AllowScrolling;
			this.ToolsClearBackground.Enabled = (this.MapView.BackgroundMap != null);
			this.AreaRemoveBtn.Enabled = (this.SelectedArea != null);
			this.AreaEditBtn.Enabled = (this.SelectedArea != null);
			this.FullMapBtn.Enabled = (this.MapView.Viewpoint != Rectangle.Empty);
			this.GroupByTileSet.Checked = (Session.Preferences.TileView == TileView.Library);
			this.GroupBySize.Checked = (Session.Preferences.TileView == TileView.Size);
			this.SizeSmall.Checked = (Session.Preferences.TileSize == TileSize.Small);
			this.SizeMedium.Checked = (Session.Preferences.TileSize == TileSize.Medium);
			this.SizeLarge.Checked = (Session.Preferences.TileSize == TileSize.Large);
			this.FilterBtn.Checked = this.MapFilterPanel.Visible;
			this.OKBtn.Enabled = (this.fMap.Name != "" && this.fMap.Tiles.Count != 0);
		}

		private void MapForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Session.Preferences.TileLibraries = new List<Guid>();
			foreach (Guid current in this.fTileSets.Keys)
			{
				if (this.fTileSets[current])
				{
					Session.Preferences.TileLibraries.Add(current);
				}
			}
		}

		protected override bool IsInputKey(Keys key)
		{
			return base.IsInputKey(key) || this.MapView.HandleKey(key);
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTiles.Count != 0)
			{
				foreach (TileData current in this.MapView.SelectedTiles)
				{
					this.fMap.Tiles.Remove(current);
				}
				this.MapView.SelectedTiles.Clear();
				this.MapView.MapChanged();
			}
		}

		private void RotateLeftBtn_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTiles.Count != 0)
			{
				foreach (TileData current in this.MapView.SelectedTiles)
				{
					current.Rotations--;
				}
				this.MapView.MapChanged();
			}
		}

		private void RotateRightBtn_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTiles.Count != 0)
			{
				foreach (TileData current in this.MapView.SelectedTiles)
				{
					current.Rotations++;
				}
				this.MapView.MapChanged();
			}
		}

		private void OrderingFront_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTiles.Count == 1)
			{
				TileData item = this.MapView.SelectedTiles[0];
				this.fMap.Tiles.Remove(item);
				this.fMap.Tiles.Add(item);
				this.MapView.MapChanged();
			}
		}

		private void OrderingBack_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTiles.Count == 1)
			{
				TileData item = this.MapView.SelectedTiles[0];
				this.fMap.Tiles.Remove(item);
				this.fMap.Tiles.Insert(0, item);
				this.MapView.MapChanged();
			}
		}

		private void RotateMapLeft_Click(object sender, EventArgs e)
		{
			foreach (TileData current in this.fMap.Tiles)
			{
				if (this.MapView.LayoutData.Tiles.ContainsKey(current))
				{
					int num = current.Location.X - this.MapView.LayoutData.MinX;
					int x = current.Location.Y - this.MapView.LayoutData.MinY;
					Tile tile = this.MapView.LayoutData.Tiles[current];
					int num2 = (current.Rotations % 2 == 0) ? tile.Size.Width : tile.Size.Height;
					current.Location = new Point(x, this.MapView.LayoutData.Width - num - num2 + 1);
					current.Rotations--;
				}
			}
			foreach (MapArea current2 in this.fMap.Areas)
			{
				int num3 = current2.Region.X - this.MapView.LayoutData.MinX;
				int x2 = current2.Region.Y - this.MapView.LayoutData.MinY;
				Point location = new Point(x2, this.MapView.LayoutData.Width - num3 - current2.Region.Width + 1);
				Size size = new Size(current2.Region.Height, current2.Region.Width);
				current2.Region = new Rectangle(location, size);
			}
			if (this.SelectedArea != null)
			{
				this.MapView.Viewpoint = this.SelectedArea.Region;
			}
			this.MapView.MapChanged();
		}

		private void RotateMapRight_Click(object sender, EventArgs e)
		{
			foreach (TileData current in this.fMap.Tiles)
			{
				if (this.MapView.LayoutData.Tiles.ContainsKey(current))
				{
					int y = current.Location.X - this.MapView.LayoutData.MinX;
					int num = current.Location.Y - this.MapView.LayoutData.MinY;
					Tile tile = this.MapView.LayoutData.Tiles[current];
					int num2 = (current.Rotations % 2 == 0) ? tile.Size.Height : tile.Size.Width;
					current.Location = new Point(this.MapView.LayoutData.Height - num - num2 + 1, y);
					current.Rotations++;
				}
			}
			foreach (MapArea current2 in this.fMap.Areas)
			{
				int y2 = current2.Region.X - this.MapView.LayoutData.MinX;
				int num3 = current2.Region.Y - this.MapView.LayoutData.MinY;
				Point location = new Point(this.MapView.LayoutData.Height - num3 - current2.Region.Height + 1, y2);
				Size size = new Size(current2.Region.Height, current2.Region.Width);
				current2.Region = new Rectangle(location, size);
			}
			if (this.SelectedArea != null)
			{
				this.MapView.Viewpoint = this.SelectedArea.Region;
			}
			this.MapView.MapChanged();
		}

		private void ToolsHighlightAreas_Click(object sender, EventArgs e)
		{
			this.MapView.HighlightAreas = !this.MapView.HighlightAreas;
		}

		private void ToolsClearAll_Click(object sender, EventArgs e)
		{
			this.fMap.Tiles.Clear();
			this.MapView.MapChanged();
		}

		private void NameBox_TextChanged(object sender, EventArgs e)
		{
			this.fMap.Name = this.NameBox.Text;
		}

		private void AreaRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedArea != null)
			{
				this.fMap.Areas.Remove(this.SelectedArea);
				this.update_areas();
				this.MapView.Viewpoint = Rectangle.Empty;
			}
		}

		private void AreaEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedArea != null)
			{
				int index = this.fMap.Areas.IndexOf(this.SelectedArea);
				MapAreaForm mapAreaForm = new MapAreaForm(this.SelectedArea, this.fMap);
				if (mapAreaForm.ShowDialog() == DialogResult.OK)
				{
					this.fMap.Areas[index] = mapAreaForm.Area;
					this.update_areas();
					this.MapView.Viewpoint = this.fMap.Areas[index].Region;
				}
			}
		}

		private void FullMapBtn_Click(object sender, EventArgs e)
		{
			this.MapView.Viewpoint = Rectangle.Empty;
		}

		private void AreaList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.SelectedArea != null)
			{
				this.MapView.Viewpoint = this.SelectedArea.Region;
				return;
			}
			this.MapView.Viewpoint = Rectangle.Empty;
		}

		private void TileSet_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			Library library = toolStripMenuItem.Tag as Library;
			this.fTileSets[library.ID] = !this.fTileSets[library.ID];
			toolStripMenuItem.Checked = this.fTileSets[library.ID];
			this.populate_tiles();
		}

		private void GroupByTileSet_Click(object sender, EventArgs e)
		{
			Session.Preferences.TileView = TileView.Library;
			this.populate_tiles();
		}

		private void GroupBySize_Click(object sender, EventArgs e)
		{
			Session.Preferences.TileView = TileView.Size;
			this.populate_tiles();
		}

		private void GroupByCategory_Click(object sender, EventArgs e)
		{
			Session.Preferences.TileView = TileView.Category;
			this.populate_tiles();
		}

		private void TileList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedTile != null)
			{
				base.DoDragDrop(this.SelectedTile, DragDropEffects.All);
			}
		}

		private void populate_tiles()
		{
			List<Library> list = new List<Library>();
			list.AddRange(Session.Libraries);
			list.Add(Session.Project.Library);
			List<string> list2 = new List<string>();
			switch (Session.Preferences.TileView)
			{
			case TileView.Library:
				foreach (Library current in list)
				{
					if (this.fTileSets[current.ID] && !list2.Contains(current.Name))
					{
						list2.Add(current.Name);
					}
				}
				list2.Sort();
				goto IL_1CF;
			case TileView.Size:
			{
				List<int> list3 = new List<int>();
				foreach (Library current2 in list)
				{
					foreach (Tile current3 in current2.Tiles)
					{
						if (!list3.Contains(current3.Area))
						{
							list3.Add(current3.Area);
						}
					}
				}
				list3.Sort();
				using (List<int>.Enumerator enumerator4 = list3.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						int current4 = enumerator4.Current;
						list2.Add("Size: " + current4);
					}
					goto IL_1CF;
				}
				break;
			}
			case TileView.Category:
				break;
			default:
				goto IL_1CF;
			}
			foreach (TileCategory tileCategory in Enum.GetValues(typeof(TileCategory)))
			{
				list2.Add(tileCategory.ToString());
			}
			IL_1CF:
			int num = 32;
			switch (Session.Preferences.TileSize)
			{
			case TileSize.Small:
				num = 16;
				break;
			case TileSize.Medium:
				num = 32;
				break;
			case TileSize.Large:
				num = 64;
				break;
			}
			this.TileList.BeginUpdate();
			this.TileList.Groups.Clear();
			foreach (string current5 in list2)
			{
				this.TileList.Groups.Add(current5, current5);
			}
			this.TileList.ShowGroups = (this.TileList.Groups.Count != 0);
			List<ListViewItem> list4 = new List<ListViewItem>();
			List<Image> list5 = new List<Image>();
			foreach (Library current6 in list)
			{
				if (this.fTileSets[current6.ID])
				{
					foreach (Tile current7 in current6.Tiles)
					{
						if (this.match(current7, this.SearchBox.Text))
						{
							ListViewItem listViewItem = new ListViewItem(current7.ToString());
							listViewItem.Tag = current7;
							switch (Session.Preferences.TileView)
							{
							case TileView.Library:
								listViewItem.Group = this.TileList.Groups[current6.Name];
								break;
							case TileView.Size:
								listViewItem.Group = this.TileList.Groups["Size: " + current7.Area];
								break;
							case TileView.Category:
								listViewItem.Group = this.TileList.Groups[current7.Category.ToString()];
								break;
							}
							Image image = (current7.Image != null) ? current7.Image : current7.BlankImage;
							if (image != null)
							{
								try
								{
									Bitmap bitmap = new Bitmap(num, num);
									if (current7.Size.Width > current7.Size.Height)
									{
										int num2 = current7.Size.Height * num / current7.Size.Width;
										Rectangle rect = new Rectangle(0, (num - num2) / 2, num, num2);
										Graphics graphics = Graphics.FromImage(bitmap);
										graphics.DrawImage(image, rect);
									}
									else
									{
										int num3 = current7.Size.Width * num / current7.Size.Height;
										Rectangle rect2 = new Rectangle((num - num3) / 2, 0, num3, num);
										Graphics graphics2 = Graphics.FromImage(bitmap);
										graphics2.DrawImage(image, rect2);
									}
									list5.Add(bitmap);
									listViewItem.ImageIndex = list5.Count - 1;
									list4.Add(listViewItem);
								}
								catch (Exception ex)
								{
									LogSystem.Trace(ex);
								}
							}
						}
					}
				}
			}
			this.TileList.LargeImageList = new ImageList();
			this.TileList.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
			this.TileList.LargeImageList.ImageSize = new Size(num, num);
			this.TileList.LargeImageList.Images.AddRange(list5.ToArray());
			this.TileList.Items.Clear();
			this.TileList.Items.AddRange(list4.ToArray());
			if (this.TileList.Items.Count == 0)
			{
				ListViewItem listViewItem2 = this.TileList.Items.Add("(no tiles)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
			this.TileList.EndUpdate();
		}

		private bool match(Tile t, string query)
		{
			string[] array = query.ToLower().Split(new char[0]);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string token = array2[i];
				if (!this.match_token(t, token))
				{
					return false;
				}
			}
			return true;
		}

		private bool match_token(Tile t, string token)
		{
			return t.Keywords.ToLower().Contains(token);
		}

		private void update_areas()
		{
			this.AreaList.Items.Clear();
			foreach (MapArea current in this.fMap.Areas)
			{
				ListViewItem listViewItem = this.AreaList.Items.Add(current.Name);
				listViewItem.Tag = current;
			}
			if (this.AreaList.Items.Count == 0)
			{
				ListViewItem listViewItem2 = this.AreaList.Items.Add("(no areas defined)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
		}

		private void MapView_ItemDropped(object sender, EventArgs e)
		{
		}

		private void MapView_ItemMoved(object sender, MovementEventArgs e)
		{
		}

		private void MapView_ItemRemoved(object sender, EventArgs e)
		{
		}

		private void MapView_RegionSelected(object sender, EventArgs e)
		{
			Point position = this.MapView.PointToClient(Cursor.Position);
			this.MapContextMenu.Show(this.MapView, position);
		}

		private void MapView_TileContext(object sender, EventArgs e)
		{
			Point position = this.MapView.PointToClient(Cursor.Position);
			this.TileContextMenu.Show(this.MapView, position);
		}

		private void ContextCreate_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.MapView.Selection != Rectangle.Empty)
				{
					MapAreaForm mapAreaForm = new MapAreaForm(new MapArea
					{
						Name = "New Area",
						Region = this.MapView.Selection
					}, this.fMap);
					if (mapAreaForm.ShowDialog() == DialogResult.OK)
					{
						this.fMap.Areas.Add(mapAreaForm.Area);
						this.update_areas();
						this.MapView.Selection = Rectangle.Empty;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ContextClear_Click(object sender, EventArgs e)
		{
			try
			{
				List<TileData> list = new List<TileData>();
				foreach (TileData current in this.fMap.Tiles)
				{
					if (this.MapView.LayoutData.TileSquares.ContainsKey(current))
					{
						Rectangle rect = this.MapView.LayoutData.TileSquares[current];
						if (this.MapView.Selection.IntersectsWith(rect))
						{
							list.Add(current);
						}
					}
				}
				foreach (TileData current2 in list)
				{
					this.fMap.Tiles.Remove(current2);
				}
				this.MapView.Selection = Rectangle.Empty;
				this.MapView.MapChanged();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ContextSelect_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.SelectedTiles.Clear();
				foreach (TileData current in this.fMap.Tiles)
				{
					if (this.MapView.LayoutData.TileSquares.ContainsKey(current))
					{
						Rectangle rect = this.MapView.LayoutData.TileSquares[current];
						if (this.MapView.Selection.IntersectsWith(rect))
						{
							this.MapView.SelectedTiles.Add(current);
						}
					}
				}
				this.MapView.Selection = Rectangle.Empty;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void ToolsAutoBuild_Click(object sender, EventArgs e)
		{
			bool delve_only = sender == null;
			MapWizard mapWizard = new MapWizard(delve_only);
			if (sender == null)
			{
				MapBuilderData mapBuilderData = mapWizard.Data as MapBuilderData;
				mapBuilderData.MaxAreaCount = 3;
				mapBuilderData.MinAreaCount = 3;
			}
			if (mapWizard.Show() == DialogResult.OK)
			{
				MapBuilderData mapBuilderData2 = mapWizard.Data as MapBuilderData;
				this.MapView.Viewpoint = Rectangle.Empty;
				int num = 0;
				while (num != 20)
				{
					num++;
					MapBuilder.BuildMap(mapBuilderData2, this.fMap, new EventHandler(this.OnAutoBuild));
					if (mapBuilderData2.Type == MapAutoBuildType.FilledArea || mapBuilderData2.Type == MapAutoBuildType.Freeform || this.fMap.Areas.Count >= mapBuilderData2.MinAreaCount)
					{
						break;
					}
				}
				if (mapBuilderData2.Type == MapAutoBuildType.Warren && this.MapView.LayoutData.Height > this.MapView.LayoutData.Width)
				{
					this.RotateMapLeft_Click(null, null);
				}
				this.MapView.MapChanged();
				this.update_areas();
			}
		}

		private void OnAutoBuild(object sender, EventArgs e)
		{
			this.MapView.MapChanged();
			Application.DoEvents();
		}

		private void MapView_HoverAreaChanged(object sender, EventArgs e)
		{
			string toolTipTitle = "";
			string text = "";
			if (this.MapView.HighlightedArea != null)
			{
				toolTipTitle = this.MapView.HighlightedArea.Name;
				text = TextHelper.Wrap(this.MapView.HighlightedArea.Details);
				if (text != "")
				{
					text += Environment.NewLine;
				}
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					this.MapView.HighlightedArea.Region.Width,
					" sq x ",
					this.MapView.HighlightedArea.Region.Height,
					" sq"
				});
			}
			this.Tooltip.ToolTipTitle = toolTipTitle;
			this.Tooltip.ToolTipIcon = ToolTipIcon.Info;
			this.Tooltip.SetToolTip(this.MapView, text);
		}

		private void SizeSmall_Click(object sender, EventArgs e)
		{
			Session.Preferences.TileSize = TileSize.Small;
			this.populate_tiles();
		}

		private void SizeMedium_Click(object sender, EventArgs e)
		{
			Session.Preferences.TileSize = TileSize.Medium;
			this.populate_tiles();
		}

		private void SizeLarge_Click(object sender, EventArgs e)
		{
			Session.Preferences.TileSize = TileSize.Large;
			this.populate_tiles();
		}

		private void MapForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				this.RemoveBtn_Click(sender, e);
			}
			Keys keyCode = e.KeyCode;
			switch (keyCode)
			{
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				this.MapView.Nudge(e);
				return;
			default:
				if (keyCode != Keys.Delete)
				{
					return;
				}
				this.RemoveBtn_Click(sender, e);
				return;
			}
		}

		private void ZoomGauge_Scroll(object sender, EventArgs e)
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

		private void ToolsNavigate_Click(object sender, EventArgs e)
		{
			this.MapView.AllowScrolling = !this.MapView.AllowScrolling;
			this.ZoomGauge.Visible = this.MapView.AllowScrolling;
		}

		private void ToolsReset_Click(object sender, EventArgs e)
		{
			this.ZoomGauge.Value = 50;
			this.MapView.ScalingFactor = 1.0;
			this.MapView.Viewpoint = Rectangle.Empty;
			this.MapView.MapChanged();
		}

		private void TileContextSwap_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTiles.Count != 1)
			{
				return;
			}
			TileData tileData = this.MapView.SelectedTiles[0];
			Tile tile = Session.FindTile(tileData.TileID, SearchType.Global);
			TileSelectForm tileSelectForm = new TileSelectForm(tile.Size, tile.Category);
			if (tileSelectForm.ShowDialog() == DialogResult.OK)
			{
				int num = (tileData.Rotations % 2 == 0) ? tile.Size.Width : tile.Size.Height;
				int num2 = (tileData.Rotations % 2 == 0) ? tile.Size.Height : tile.Size.Width;
				int rotations = 0;
				if (tileSelectForm.Tile.Size.Width != num || tileSelectForm.Tile.Size.Height != num2)
				{
					rotations = 1;
				}
				tileData.TileID = tileSelectForm.Tile.ID;
				tileData.Rotations = rotations;
				this.MapView.MapChanged();
			}
		}

		private void TileContextDuplicate_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTiles.Count != 1)
			{
				return;
			}
			TileData tileData = this.MapView.SelectedTiles[0];
			tileData = tileData.Copy();
			tileData.ID = Guid.NewGuid();
			tileData.Location = new Point(tileData.Location.X + 1, tileData.Location.Y + 1);
			this.fMap.Tiles.Add(tileData);
			this.MapView.MapChanged();
		}

		private void ViewSelectLibraries_Click(object sender, EventArgs e)
		{
			List<Library> list = new List<Library>();
			list.AddRange(Session.Libraries);
			list.Add(Session.Project.Library);
			List<Library> list2 = new List<Library>();
			foreach (Library current in list)
			{
				if (this.fTileSets[current.ID])
				{
					list2.Add(current);
				}
			}
			TileLibrarySelectForm tileLibrarySelectForm = new TileLibrarySelectForm(list2);
			if (tileLibrarySelectForm.ShowDialog() == DialogResult.OK)
			{
				foreach (Library current2 in list)
				{
					this.fTileSets[current2.ID] = tileLibrarySelectForm.Libraries.Contains(current2);
				}
				this.populate_tiles();
			}
		}

		private void ToolsSelectBackground_Click(object sender, EventArgs e)
		{
			List<Guid> list = new List<Guid>();
			list.Add(this.fMap.ID);
			MapSelectForm mapSelectForm = new MapSelectForm(Session.Project.Maps, list, false);
			if (mapSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.MapView.BackgroundMap = mapSelectForm.Map;
			}
		}

		private void ToolsClearBackground_Click(object sender, EventArgs e)
		{
			this.MapView.BackgroundMap = null;
		}

		private void MapView_AreaActivated(object sender, MapAreaEventArgs e)
		{
			int index = this.fMap.Areas.IndexOf(e.MapArea);
			MapAreaForm mapAreaForm = new MapAreaForm(e.MapArea, this.fMap);
			if (mapAreaForm.ShowDialog() == DialogResult.OK)
			{
				this.fMap.Areas[index] = mapAreaForm.Area;
				this.update_areas();
			}
		}

		private void ToolsImportMap_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = Program.ImageFilter;
			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			Image image = Image.FromFile(openFileDialog.FileName);
			if (image == null)
			{
				return;
			}
			Tile tile = new Tile();
			tile.Image = image;
			TileForm tileForm = new TileForm(tile);
			if (tileForm.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			Session.Project.Library.Tiles.Add(tileForm.Tile);
			TileData tileData = new TileData();
			tileData.TileID = tile.ID;
			this.fMap.Tiles.Add(tileData);
			this.MapView.MapChanged();
		}

		private void TileList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedTile != null)
			{
				Library library = Session.FindLibrary(this.SelectedTile);
				int index = library.Tiles.IndexOf(this.SelectedTile);
				TileForm tileForm = new TileForm(this.SelectedTile);
				if (tileForm.ShowDialog() == DialogResult.OK)
				{
					library.Tiles[index] = tileForm.Tile;
					this.populate_tiles();
				}
			}
		}

		private void MapForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == DialogResult.OK)
			{
				return;
			}
			if (this.fMap.Tiles.Count == 0)
			{
				return;
			}
			if (Session.Project.FindTacticalMap(this.fMap.ID) != null)
			{
				return;
			}
			string text = "Do you want to save this new map?";
			if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				base.DialogResult = DialogResult.OK;
			}
		}

		private void MapView_MouseZoomed(object sender, MouseEventArgs e)
		{
			this.ZoomGauge.Visible = true;
			this.ZoomGauge.Value -= Math.Sign(e.Delta);
			this.ZoomGauge_Scroll(sender, e);
		}

		private void FilterBtn_Click(object sender, EventArgs e)
		{
			this.MapFilterPanel.Visible = !this.MapFilterPanel.Visible;
		}

		private void SearchBox_TextChanged(object sender, EventArgs e)
		{
			this.populate_tiles();
		}
	}
}
