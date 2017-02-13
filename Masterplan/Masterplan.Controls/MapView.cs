using Masterplan.Data;
using Masterplan.Events;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Utils;
using Utils.Graphics;

namespace Masterplan.Controls
{
	public class MapView : UserControl
	{
		private class NewTile
		{
			public Tile Tile;

			public Point Location = CombatData.NoPoint;

			public RectangleF Region = RectangleF.Empty;
		}

		private class DraggedTiles
		{
			public List<TileData> Tiles = new List<TileData>();

			public Point Start = CombatData.NoPoint;

			public Size Offset = Size.Empty;

			public RectangleF Region = RectangleF.Empty;
		}

		private class DraggedOutline
		{
			public Point Start = CombatData.NoPoint;

			public Rectangle Region = Rectangle.Empty;
		}

		private class NewToken
		{
			public IToken Token;

			public Point Location = CombatData.NoPoint;
		}

		private class DraggedToken
		{
			public IToken Token;

			public Point Start = CombatData.NoPoint;

			public Size Offset = Size.Empty;

			public Point Location = CombatData.NoPoint;

			public IToken LinkedToken;
		}

		private class ScrollingData
		{
			public Point Start = Point.Empty;
		}

		private class DrawingData
		{
			public MapSketch CurrentSketch;
		}

		private Map fMap;

		private Map fBackgroundMap;

		private Encounter fEncounter;

		private bool fShowAllWaves;

		private Plot fPlot;

		private Rectangle fViewpoint = Rectangle.Empty;

		private MapViewMode fMode;

		private bool fTactical;

		private bool fHighlightAreas;

		private MapGridMode fShowGrid;

		private bool fShowGridLabels;

		private bool fShowPictureTokens = true;

		private int fBorderSize;

		private List<TileData> fSelectedTiles;

		private List<IToken> fBoxedTokens = new List<IToken>();

		private List<IToken> fSelectedTokens = new List<IToken>();

		private IToken fHoverToken;

		private TokenLink fHoverTokenLink;

		private Rectangle fCurrentOutline = Rectangle.Empty;

		private CreatureViewMode fShowCreatures;

		private bool fShowCreatureLabels = true;

		private bool fShowHealthBars;

		private bool fShowConditions = true;

		private bool fShowAuras = true;

		private MapArea fHighlightedArea;

		private MapArea fSelectedArea;

		private bool fAllowLinkCreation;

		private List<TokenLink> fTokenLinks;

		private Dictionary<TokenLink, RectangleF> fTokenLinkRegions = new Dictionary<TokenLink, RectangleF>();

		private bool fAllowScrolling;

		private double fScalingFactor = 1.0;

		private MapDisplayType fFrameType = MapDisplayType.Dimmed;

		private bool fLineOfSight;

		private MapView.DrawingData fDrawing;

		private List<MapSketch> fSketches = new List<MapSketch>();

		private string fCaption = "";

		private MapData fLayoutData;

		private MapView.NewTile fNewTile;

		private TileData fHoverTile;

		private MapView.DraggedTiles fDraggedTiles;

		private MapView.NewToken fNewToken;

		private MapView.DraggedToken fDraggedToken;

		private StringFormat fCentred = new StringFormat();

		private StringFormat fTop = new StringFormat();

		private StringFormat fBottom = new StringFormat();

		private StringFormat fLeft = new StringFormat();

		private StringFormat fRight = new StringFormat();

		private MapView.DraggedOutline fDraggedOutline;

		private MapView.ScrollingData fScrollingData;

		private Dictionary<Guid, List<Rectangle>> fSlotRegions = new Dictionary<Guid, List<Rectangle>>();

		private IContainer components;

        [Category("Action"), Description("Called when a tile or token is removed from the map.")]
        public event EventHandler ItemRemoved;

        [Category("Action"), Description("Called when a tile or token is dropped onto the map.")]
        public event EventHandler ItemDropped;

        [Category("Action"), Description("Called when a tile or token is moved around the map.")]
        public event MovementEventHandler ItemMoved;

        [Category("Action"), Description("Called when an area has been selected.")]
        public event EventHandler RegionSelected;

        [Category("Action"), Description("Called when a context menu should be displayed.")]
        public event EventHandler TileContext;

        [Category("Property Changed"), Description("Occurs when the hovered token has changed.")]
        public event EventHandler HoverTokenChanged;

        [Category("Property Changed"), Description("Occurs when the selected tokens have changed.")]
        public event EventHandler SelectedTokensChanged;

        [Category("Property Changed"), Description("Occurs when the highlighted map area has changed.")]
        public event EventHandler HighlightedAreaChanged;

        [Category("Action"), Description("Occurs when a map token is double-clicked.")]
        public event TokenEventHandler TokenActivated;

        [Category("Action"), Description("Occurs when a map token is dragged.")]
        public event DraggedTokenEventHandler TokenDragged;

        [Category("Action"), Description("Occurs when a map area is clicked.")]
        public event MapAreaEventHandler AreaSelected;

        [Category("Action"), Description("Occurs when a map area is double-clicked.")]
        public event MapAreaEventHandler AreaActivated;

        [Category("Action"), Description("Occurs when a sketch is created.")]
        public event MapSketchEventHandler SketchCreated;

        [Category("Action"), Description("Occurs when the mouse wheel is scrolled.")]
        public event MouseEventHandler MouseZoomed;

        [Category("Action"), Description("Called when the LOS mode is cancelled.")]
        public event EventHandler CancelledLOS;

        [Category("Action"), Description("Called when the drawing mode is cancelled.")]
        public event EventHandler CancelledDrawing;

        [Category("Action"), Description("Called when the scrolling mode is cancelled.")]
        public event EventHandler CancelledScrolling;

        [Category("Action"), Description("Occurs when a link should be created.")]
        public event CreateTokenLinkEventHandler CreateTokenLink;

        [Category("Action"), Description("Occurs when a link should be edited.")]
        public event TokenLinkEventHandler EditTokenLink;

        [Category("Data"), Description("The map to be displayed.")]
		public Map Map
		{
			get
			{
				return this.fMap;
			}
			set
			{
				this.fMap = value;
				this.fLayoutData = null;
				base.Invalidate();
			}
		}

		[Category("Data"), Description("The map to be displayed in the background.")]
		public Map BackgroundMap
		{
			get
			{
				return this.fBackgroundMap;
			}
			set
			{
				this.fBackgroundMap = ((value != null) ? value.Copy() : null);
				this.fLayoutData = null;
				base.Invalidate();
			}
		}

		[Category("Data"), Description("The encounter to be displayed.")]
		public Encounter Encounter
		{
			get
			{
				return this.fEncounter;
			}
			set
			{
				this.fEncounter = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("Whether we should show all waves, or only active waves.")]
		public bool ShowAllWaves
		{
			get
			{
				return this.fShowAllWaves;
			}
			set
			{
				this.fShowAllWaves = value;
				base.Invalidate();
			}
		}

		[Category("Data"), Description("The plot to be displayed.")]
		public Plot Plot
		{
			get
			{
				return this.fPlot;
			}
			set
			{
				this.fPlot = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("The tile co-ordinates to view.")]
		public Rectangle Viewpoint
		{
			get
			{
				return this.fViewpoint;
			}
			set
			{
				this.fViewpoint = value;
				this.fLayoutData = null;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("The mode in which to display the map.")]
		public MapViewMode Mode
		{
			get
			{
				return this.fMode;
			}
			set
			{
				this.fMode = value;
				base.Invalidate();
			}
		}

		[Category("Behavior"), Description("Determines whether map tokens can be moved around the map.")]
		public bool Tactical
		{
			get
			{
				return this.fTactical;
			}
			set
			{
				this.fTactical = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("Determines whether areas are highlighted.")]
		public bool HighlightAreas
		{
			get
			{
				return this.fHighlightAreas;
			}
			set
			{
				this.fHighlightAreas = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("Determines how gridlines are shown.")]
		public MapGridMode ShowGrid
		{
			get
			{
				return this.fShowGrid;
			}
			set
			{
				this.fShowGrid = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("Determines whether grid rows and columns are labelled.")]
		public bool ShowGridLabels
		{
			get
			{
				return this.fShowGridLabels;
			}
			set
			{
				this.fShowGridLabels = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("Determines whether token images are shown as tokens.")]
		public bool ShowPictureTokens
		{
			get
			{
				return this.fShowPictureTokens;
			}
			set
			{
				this.fShowPictureTokens = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("The number of squares to be drawn around the viewpoint.")]
		public int BorderSize
		{
			get
			{
				return this.fBorderSize;
			}
			set
			{
				this.fBorderSize = value;
				base.Invalidate();
			}
		}

		[Category("Data"), Description("The list of selected tiles.")]
		public List<TileData> SelectedTiles
		{
			get
			{
				return this.fSelectedTiles;
			}
			set
			{
				this.fSelectedTiles = value;
				base.Invalidate();
			}
		}

		[Category("Data"), Description("The list of boxed map tokens.")]
		public List<IToken> BoxedTokens
		{
			get
			{
				return this.fBoxedTokens;
			}
		}

		[Category("Appearance"), Description("The list of selected map tokens.")]
		public List<IToken> SelectedTokens
		{
			get
			{
				return this.fSelectedTokens;
			}
		}

		[Category("Appearance"), Description("The hovered map token.")]
		public IToken HoverToken
		{
			get
			{
				return this.fHoverToken;
			}
			set
			{
				this.fHoverToken = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("The hovered token link.")]
		public TokenLink HoverTokenLink
		{
			get
			{
				return this.fHoverTokenLink;
			}
			set
			{
				this.fHoverTokenLink = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("The rubber-band selected rectangle.")]
		public Rectangle Selection
		{
			get
			{
				return this.fCurrentOutline;
			}
			set
			{
				this.fCurrentOutline = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("Determines how creatures should be displayed.")]
		public CreatureViewMode ShowCreatures
		{
			get
			{
				return this.fShowCreatures;
			}
			set
			{
				this.fShowCreatures = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("Whether creatures should be shown with abbreviated labels.")]
		public bool ShowCreatureLabels
		{
			get
			{
				return this.fShowCreatureLabels;
			}
			set
			{
				this.fShowCreatureLabels = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("Whether creatures should be shown with an HP bar.")]
		public bool ShowHealthBars
		{
			get
			{
				return this.fShowHealthBars;
			}
			set
			{
				this.fShowHealthBars = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("Whether condition badges should be shown.")]
		public bool ShowConditions
		{
			get
			{
				return this.fShowConditions;
			}
			set
			{
				this.fShowConditions = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("Whether creature auras should be shown.")]
		public bool ShowAuras
		{
			get
			{
				return this.fShowAuras;
			}
			set
			{
				this.fShowAuras = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("The highlighted MapArea.")]
		public MapArea HighlightedArea
		{
			get
			{
				return this.fHighlightedArea;
			}
		}

		[Category("Appearance"), Description("The selected MapArea.")]
		public MapArea SelectedArea
		{
			get
			{
				return this.fSelectedArea;
			}
			set
			{
				this.fSelectedArea = value;
				base.Invalidate();
			}
		}

		[Category("Behavior"), Description("Determines whether links between tokens can be created.")]
		public bool AllowLinkCreation
		{
			get
			{
				return this.fAllowLinkCreation;
			}
			set
			{
				this.fAllowLinkCreation = value;
			}
		}

		[Category("Data"), Description("The list of links between map tokens.")]
		public List<TokenLink> TokenLinks
		{
			get
			{
				return this.fTokenLinks;
			}
			set
			{
				this.fTokenLinks = value;
				base.Invalidate();
			}
		}

		[Category("Behavior"), Description("Determines whether the map can be scrolled.")]
		public bool AllowScrolling
		{
			get
			{
				return this.fAllowScrolling;
			}
			set
			{
				this.fAllowScrolling = value;
				if (!this.fAllowScrolling)
				{
					if (this.fScalingFactor != 1.0)
					{
						this.fViewpoint = this.get_current_zoom_rect(false);
					}
					this.fScalingFactor = 1.0;
				}
				this.Cursor = (this.fAllowScrolling ? Cursors.SizeAll : Cursors.Default);
				base.Invalidate();
				if (!this.fAllowScrolling)
				{
					this.OnCancelledScrolling();
				}
			}
		}

		[Category("Appearance"), Description("The scaling factor for the map; this can be used to zoom in and out.")]
		public double ScalingFactor
		{
			get
			{
				return this.fScalingFactor;
			}
			set
			{
				this.fScalingFactor = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("The appearance of the frame around the viewpoint.")]
		public MapDisplayType FrameType
		{
			get
			{
				return this.fFrameType;
			}
			set
			{
				this.fFrameType = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("How the line of sight is displayed.")]
		public bool LineOfSight
		{
			get
			{
				return this.fLineOfSight;
			}
			set
			{
				this.fLineOfSight = value;
				base.Invalidate();
				if (!this.fLineOfSight)
				{
					this.OnCancelledLOS();
				}
			}
		}

		public bool AllowDrawing
		{
			get
			{
				return this.fDrawing != null;
			}
			set
			{
				if (value)
				{
					this.fDrawing = new MapView.DrawingData();
				}
				else
				{
					this.fDrawing = null;
				}
				this.Cursor = ((this.fDrawing == null) ? Cursors.Default : Cursors.Cross);
				base.Invalidate();
				if (this.fDrawing == null)
				{
					this.OnCancelledDrawing();
				}
			}
		}

		public List<MapSketch> Sketches
		{
			get
			{
				return this.fSketches;
			}
		}

		public string Caption
		{
			get
			{
				return this.fCaption;
			}
			set
			{
				this.fCaption = value;
			}
		}

		internal MapData LayoutData
		{
			get
			{
				if (this.fLayoutData == null)
				{
					this.fLayoutData = new MapData(this, this.fScalingFactor);
				}
				return this.fLayoutData;
			}
		}

		public MapView()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
			this.fTop.Alignment = StringAlignment.Center;
			this.fTop.LineAlignment = StringAlignment.Near;
			this.fTop.Trimming = StringTrimming.EllipsisCharacter;
			this.fBottom.Alignment = StringAlignment.Center;
			this.fBottom.LineAlignment = StringAlignment.Far;
			this.fBottom.Trimming = StringTrimming.EllipsisCharacter;
			this.fLeft.Alignment = StringAlignment.Near;
			this.fLeft.LineAlignment = StringAlignment.Center;
			this.fLeft.Trimming = StringTrimming.EllipsisCharacter;
			this.fRight.Alignment = StringAlignment.Far;
			this.fRight.LineAlignment = StringAlignment.Center;
			this.fRight.Trimming = StringTrimming.EllipsisCharacter;
		}

		public void MapChanged()
		{
			this.fLayoutData = null;
			base.Invalidate();
		}

		public void Nudge(KeyEventArgs e)
		{
			this.OnKeyDown(e);
		}

		protected override void OnResize(EventArgs e)
		{
			this.fLayoutData = null;
			base.Invalidate();
		}

		public void SetDragInfo(Point old_point, Point new_point)
		{
			if (old_point == CombatData.NoPoint)
			{
				this.fDraggedToken = null;
				base.Invalidate();
				return;
			}
			Pair<IToken, Rectangle> pair = this.get_token_at(old_point);
			if (pair != null)
			{
				this.fDraggedToken = new MapView.DraggedToken();
				this.fDraggedToken.Token = pair.First;
				this.fDraggedToken.Start = old_point;
				this.fDraggedToken.Location = new_point;
				base.Invalidate();
			}
		}

		public void SelectTokens(List<IToken> tokens, bool raise_event)
		{
			if (tokens == null)
			{
				this.fSelectedTokens.Clear();
				return;
			}
			foreach (IToken current in tokens)
			{
				if (!this.fSelectedTokens.Contains(current))
				{
					this.fSelectedTokens.Add(current);
				}
			}
			List<IToken> list = new List<IToken>();
			foreach (IToken current2 in this.fSelectedTokens)
			{
				if (!tokens.Contains(current2))
				{
					list.Add(current2);
				}
			}
			foreach (IToken current3 in list)
			{
				this.fSelectedTokens.Remove(current3);
			}
			base.Invalidate();
			if (raise_event)
			{
				this.OnSelectedTokensChanged();
			}
		}

		protected void OnItemRemoved()
		{
			if (this.ItemRemoved != null)
			{
				this.ItemRemoved(this, new EventArgs());
			}
		}

		protected void OnItemDropped()
		{
			if (this.ItemDropped != null)
			{
				this.ItemDropped(this, new EventArgs());
			}
		}

		protected void OnItemMoved(int distance)
		{
			if (this.ItemMoved != null)
			{
				this.ItemMoved(this, new MovementEventArgs(distance));
			}
		}

		protected void OnRegionSelected()
		{
			if (this.RegionSelected != null)
			{
				this.RegionSelected(this, new EventArgs());
			}
		}

		protected void OnTileContext(TileData tile)
		{
			if (this.TileContext != null)
			{
				this.TileContext(this, new TileEventArgs(tile));
			}
		}

		protected void OnHoverTokenChanged()
		{
			if (this.HoverTokenChanged != null)
			{
				this.HoverTokenChanged(this, new EventArgs());
			}
		}

		protected void OnSelectedTokensChanged()
		{
			if (this.SelectedTokensChanged != null)
			{
				this.SelectedTokensChanged(this, new EventArgs());
			}
		}

		protected void OnHighlightedAreaChanged()
		{
			if (!this.fHighlightAreas)
			{
				return;
			}
			if (this.fHighlightedArea != null && this.fViewpoint == this.fHighlightedArea.Region)
			{
				return;
			}
			if (this.HighlightedAreaChanged != null)
			{
				this.HighlightedAreaChanged(this, new EventArgs());
			}
		}

		protected void OnTokenActivated(IToken token)
		{
			if (this.TokenActivated != null)
			{
				this.TokenActivated(this, new TokenEventArgs(token));
			}
		}

		protected void OnTokenDragged()
		{
			if (this.TokenDragged != null)
			{
				Point old_location = (this.fDraggedToken != null) ? this.fDraggedToken.Start : CombatData.NoPoint;
				Point new_location = (this.fDraggedToken != null) ? this.fDraggedToken.Location : CombatData.NoPoint;
				this.TokenDragged(this, new DraggedTokenEventArgs(old_location, new_location));
			}
		}

		protected void OnAreaSelected(MapArea area)
		{
			if (this.AreaSelected != null)
			{
				this.AreaSelected(this, new MapAreaEventArgs(area));
			}
		}

		protected void OnAreaActivated(MapArea area)
		{
			if (this.AreaActivated != null)
			{
				this.AreaActivated(this, new MapAreaEventArgs(area));
			}
		}

		protected TokenLink OnCreateTokenLink(List<IToken> tokens)
		{
			if (this.CreateTokenLink != null)
			{
				return this.CreateTokenLink(this, new TokenListEventArgs(tokens));
			}
			return null;
		}

		protected TokenLink OnEditTokenLink(TokenLink link)
		{
			if (this.EditTokenLink != null)
			{
				return this.EditTokenLink(this, new TokenLinkEventArgs(link));
			}
			return null;
		}

		protected void OnSketchCreated(MapSketch sketch)
		{
			if (this.SketchCreated != null)
			{
				this.SketchCreated(this, new MapSketchEventArgs(sketch));
			}
		}

		protected void OnMouseZoom(MouseEventArgs args)
		{
			if (this.MouseZoomed != null)
			{
				this.MouseZoomed(this, args);
			}
		}

		protected void OnCancelledLOS()
		{
			if (this.CancelledLOS != null)
			{
				this.CancelledLOS(this, new EventArgs());
			}
		}

		protected void OnCancelledDrawing()
		{
			if (this.CancelledDrawing != null)
			{
				this.CancelledDrawing(this, new EventArgs());
			}
		}

		protected void OnCancelledScrolling()
		{
			if (this.CancelledScrolling != null)
			{
				this.CancelledScrolling(this, new EventArgs());
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (this.fLayoutData == null)
			{
				this.fLayoutData = new MapData(this, this.fScalingFactor);
			}
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			switch (this.fMode)
			{
			case MapViewMode.Normal:
				using (Brush brush = new SolidBrush(Color.FromArgb(70, 100, 170)))
				{
					e.Graphics.FillRectangle(brush, base.ClientRectangle);
					goto IL_11C;
				}
				break;
			case MapViewMode.Plain:
				goto IL_EE;
			case MapViewMode.Thumbnail:
				break;
			case MapViewMode.PlayerView:
				e.Graphics.FillRectangle(Brushes.Black, base.ClientRectangle);
				goto IL_11C;
			default:
				goto IL_11C;
			}
			Color color = Color.FromArgb(240, 240, 240);
			Color color2 = Color.FromArgb(170, 170, 170);
			using (Brush brush2 = new LinearGradientBrush(base.ClientRectangle, color, color2, LinearGradientMode.Vertical))
			{
				e.Graphics.FillRectangle(brush2, base.ClientRectangle);
				goto IL_11C;
			}
			IL_EE:
			e.Graphics.FillRectangle(Brushes.White, base.ClientRectangle);
			IL_11C:
			if (this.fMap == null)
			{
				Brush brush3 = SystemBrushes.WindowText;
				if (this.fMode == MapViewMode.Normal)
				{
					brush3 = Brushes.White;
				}
				e.Graphics.DrawString("(no map selected)", this.Font, brush3, base.ClientRectangle, this.fCentred);
				return;
			}
			if (this.fShowGrid == MapGridMode.Behind && this.fLayoutData.SquareSize >= 10f)
			{
				using (Pen pen = new Pen(Color.FromArgb(100, 140, 190)))
				{
					using (Pen pen2 = new Pen(Color.FromArgb(150, 200, 230)))
					{
						float num = 0f;
						float num2 = this.fLayoutData.MapOffset.Width % this.fLayoutData.SquareSize;
						int num3 = 0;
						while (num <= (float)base.ClientRectangle.Width)
						{
							if (num3 % 4 == 0)
							{
								e.Graphics.DrawLine(pen2, num + num2, 0f, num + num2, (float)base.ClientRectangle.Height);
							}
							else
							{
								e.Graphics.DrawLine(pen, num + num2, 0f, num + num2, (float)base.ClientRectangle.Height);
							}
							num += this.fLayoutData.SquareSize;
							num3++;
						}
						float num4 = 0f;
						float num5 = this.fLayoutData.MapOffset.Height % this.fLayoutData.SquareSize;
						int num6 = 0;
						while (num4 <= (float)base.ClientRectangle.Height)
						{
							if (num6 % 4 == 0)
							{
								e.Graphics.DrawLine(pen2, 0f, num4 + num5, (float)base.ClientRectangle.Width, num4 + num5);
							}
							else
							{
								e.Graphics.DrawLine(pen, 0f, num4 + num5, (float)base.ClientRectangle.Width, num4 + num5);
							}
							num4 += this.fLayoutData.SquareSize;
							num6++;
						}
					}
				}
			}
			if (this.fEncounter != null)
			{
				this.fSlotRegions.Clear();
				foreach (EncounterSlot current in this.fEncounter.AllSlots)
				{
					this.fSlotRegions[current.ID] = new List<Rectangle>();
					ICreature creature = Session.FindCreature(current.Card.CreatureID, SearchType.Global);
					if (creature != null)
					{
						int size = Creature.GetSize(creature.Size);
						foreach (CombatData current2 in current.CombatData)
						{
							this.fSlotRegions[current.ID].Add(new Rectangle(current2.Location, new Size(size, size)));
						}
					}
				}
			}
			if (this.fHighlightAreas)
			{
				foreach (MapArea current3 in this.fMap.Areas)
				{
					RectangleF region = this.fLayoutData.GetRegion(current3.Region.Location, current3.Region.Size);
					Brush brush4;
					if (current3 == this.fSelectedArea)
					{
						brush4 = Brushes.LightBlue;
					}
					else
					{
						Color color3 = Color.FromArgb(255, 255, 255);
						Color color4 = Color.FromArgb(210, 210, 210);
						brush4 = new LinearGradientBrush(base.ClientRectangle, color3, color4, LinearGradientMode.Vertical);
					}
					if (this.fPlot != null && this.fPlot.FindPointForMapArea(this.fMap, current3) == null)
					{
						brush4 = null;
					}
					if (brush4 != null)
					{
						e.Graphics.FillRectangle(brush4, region);
					}
				}
			}
			if (this.fCurrentOutline != Rectangle.Empty)
			{
				RectangleF region2 = this.fLayoutData.GetRegion(this.fCurrentOutline.Location, this.fCurrentOutline.Size);
				e.Graphics.FillRectangle(Brushes.LightBlue, region2);
			}
			if (this.fBackgroundMap != null)
			{
				foreach (TileData current4 in this.fBackgroundMap.Tiles)
				{
					if (this.fLayoutData.Tiles.ContainsKey(current4))
					{
						Tile tile = this.fLayoutData.Tiles[current4];
						RectangleF rect = this.fLayoutData.TileRegions[current4];
						this.draw_tile(e.Graphics, tile, current4.Rotations, rect, true);
					}
				}
			}
			foreach (TileData current5 in this.fMap.Tiles)
			{
				if ((this.fDraggedTiles == null || !this.fDraggedTiles.Tiles.Contains(current5)) && this.fLayoutData.Tiles.ContainsKey(current5))
				{
					Tile tile2 = this.fLayoutData.Tiles[current5];
					RectangleF rect2 = this.fLayoutData.TileRegions[current5];
					this.draw_tile(e.Graphics, tile2, current5.Rotations, rect2, false);
					if (this.fSelectedTiles != null && this.fSelectedTiles.Contains(current5))
					{
						e.Graphics.DrawRectangle(Pens.Blue, rect2.X, rect2.Y, rect2.Width, rect2.Height);
					}
					else if (current5 == this.fHoverTile)
					{
						e.Graphics.DrawRectangle(Pens.DarkBlue, rect2.X, rect2.Y, rect2.Width, rect2.Height);
					}
				}
			}
			if (this.fNewTile != null)
			{
				this.draw_tile(e.Graphics, this.fNewTile.Tile, 0, this.fNewTile.Region, false);
			}
			if (this.fDraggedTiles != null)
			{
				foreach (TileData current6 in this.fDraggedTiles.Tiles)
				{
					Tile tile3 = this.fLayoutData.Tiles[current6];
					this.draw_tile(e.Graphics, tile3, current6.Rotations, this.fDraggedTiles.Region, false);
				}
			}
			if (this.fShowGrid == MapGridMode.Overlay && this.fLayoutData.SquareSize >= 10f)
			{
				Pen darkGray = Pens.DarkGray;
				float num7 = 0f;
				float num8 = this.fLayoutData.MapOffset.Width % this.fLayoutData.SquareSize;
				while (num7 <= (float)base.ClientRectangle.Width)
				{
					e.Graphics.DrawLine(darkGray, num7 + num8, 0f, num7 + num8, (float)base.ClientRectangle.Height);
					num7 += this.fLayoutData.SquareSize;
				}
				float num9 = 0f;
				float num10 = this.fLayoutData.MapOffset.Height % this.fLayoutData.SquareSize;
				while (num9 <= (float)base.ClientRectangle.Height)
				{
					e.Graphics.DrawLine(darkGray, 0f, num9 + num10, (float)base.ClientRectangle.Width, num9 + num10);
					num9 += this.fLayoutData.SquareSize;
				}
			}
			if (this.fShowGridLabels)
			{
				string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
				float emSize = this.fLayoutData.SquareSize / 4f;
				Font font = new Font(this.Font.FontFamily, emSize);
				for (int i = this.fLayoutData.MinX; i <= this.fLayoutData.MaxX; i++)
				{
					string str = (i - this.fLayoutData.MinX + 1).ToString();
					RectangleF region3 = this.fLayoutData.GetRegion(new Point(i, this.fLayoutData.MinY), new Size(1, 1));
					this.draw_grid_label(e.Graphics, str, font, region3, this.fTop);
					RectangleF region4 = this.fLayoutData.GetRegion(new Point(i, this.fLayoutData.MaxY), new Size(1, 1));
					this.draw_grid_label(e.Graphics, str, font, region4, this.fBottom);
				}
				for (int j = this.fLayoutData.MinY; j <= this.fLayoutData.MaxY; j++)
				{
					int num11 = j - this.fLayoutData.MinY;
					string text2 = "";
					if (num11 >= text.Length)
					{
						int num12 = num11 / text.Length;
						text2 += text.Substring(num12 - 1, 1);
					}
					int startIndex = num11 % text.Length;
					text2 += text.Substring(startIndex, 1);
					RectangleF region5 = this.fLayoutData.GetRegion(new Point(this.fLayoutData.MinX, j), new Size(1, 1));
					this.draw_grid_label(e.Graphics, text2, font, region5, this.fLeft);
					RectangleF region6 = this.fLayoutData.GetRegion(new Point(this.fLayoutData.MaxX, j), new Size(1, 1));
					this.draw_grid_label(e.Graphics, text2, font, region6, this.fRight);
				}
			}
			if (this.fHighlightAreas)
			{
				using (List<MapArea>.Enumerator enumerator3 = this.fMap.Areas.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						MapArea current7 = enumerator3.Current;
						PlotPointState plotPointState = PlotPointState.Normal;
						if (this.fPlot != null)
						{
							PlotPoint plotPoint = this.fPlot.FindPointForMapArea(this.fMap, current7);
							if (plotPoint != null)
							{
								plotPointState = plotPoint.State;
							}
						}
						RectangleF region7 = this.fLayoutData.GetRegion(current7.Region.Location, current7.Region.Size);
						Pen pen3 = Pens.DarkGray;
						if (current7 == this.fHighlightedArea || current7 == this.fSelectedArea)
						{
							pen3 = Pens.DarkBlue;
						}
						e.Graphics.DrawRectangle(pen3, region7.X, region7.Y, region7.Width, region7.Height);
						if (plotPointState == PlotPointState.Completed || plotPointState == PlotPointState.Skipped)
						{
							PointF pt = new PointF(region7.Left, region7.Top);
							PointF pt2 = new PointF(region7.Right, region7.Top);
							PointF pt3 = new PointF(region7.Left, region7.Bottom);
							PointF pt4 = new PointF(region7.Right, region7.Bottom);
							Pen pen4 = new Pen(Color.DarkGray, 2f);
							e.Graphics.DrawLine(pen4, pt, pt4);
							e.Graphics.DrawLine(pen4, pt3, pt2);
						}
						if (this.fViewpoint == Rectangle.Empty && current7.Name != "" && this.fNewTile == null && this.fDraggedTiles == null)
						{
							Font font2 = this.Font;
							if (plotPointState == PlotPointState.Skipped)
							{
								font2 = new Font(font2, font2.Style | FontStyle.Strikeout);
							}
							float num13 = 8f;
							SizeF sizeF = e.Graphics.MeasureString(current7.Name, font2);
							sizeF = new SizeF(sizeF.Width + num13, sizeF.Height + num13);
							float num14 = (region7.Width - sizeF.Width) / 2f;
							float num15 = (region7.Height - sizeF.Height) / 2f;
							RectangleF rectangleF = new RectangleF(region7.Left + num14, region7.Top + num15, sizeF.Width, sizeF.Height);
							GraphicsPath path = RoundedRectangle.Create(rectangleF, rectangleF.Height / 3f);
							using (Brush brush5 = new SolidBrush(Color.FromArgb(200, Color.Black)))
							{
								e.Graphics.FillPath(brush5, path);
							}
							e.Graphics.DrawPath(Pens.Black, path);
							e.Graphics.DrawString(current7.Name, font2, Brushes.White, rectangleF, this.fCentred);
						}
					}
					goto IL_E7F;
				}
			}
			if (this.fPlot != null)
			{
				foreach (MapArea current8 in this.fMap.Areas)
				{
					PlotPoint plotPoint2 = this.fPlot.FindPointForMapArea(this.fMap, current8);
					if (plotPoint2 == null || plotPoint2.State != PlotPointState.Completed)
					{
						RectangleF region8 = this.fLayoutData.GetRegion(current8.Region.Location, current8.Region.Size);
						e.Graphics.FillRectangle(Brushes.Black, region8);
					}
				}
			}
			IL_E7F:
			if (this.fShowAuras)
			{
				List<IToken> list = new List<IToken>();
				list.AddRange(this.fSelectedTokens);
				if (this.fHoverToken != null)
				{
					list.Add(this.fHoverToken);
				}
				foreach (IToken current9 in list)
				{
					Dictionary<Aura, Rectangle> dictionary = new Dictionary<Aura, Rectangle>();
					CreatureToken creatureToken = current9 as CreatureToken;
					if (creatureToken != null)
					{
						if (creatureToken.Data.Location == CombatData.NoPoint)
						{
							continue;
						}
						List<Aura> list2 = new List<Aura>();
						foreach (OngoingCondition current10 in creatureToken.Data.Conditions)
						{
							if (current10.Type == OngoingType.Aura)
							{
								list2.Add(current10.Aura);
							}
						}
						EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
						if (encounterSlot != null)
						{
							list2.AddRange(encounterSlot.Card.Auras);
						}
						if (encounterSlot != null)
						{
							ICreature creature2 = Session.FindCreature(encounterSlot.Card.CreatureID, SearchType.Global);
							int num16 = (creature2 != null) ? Creature.GetSize(creature2.Size) : 1;
							foreach (Aura current11 in list2)
							{
								int num17 = current11.Radius + num16 + current11.Radius;
								Point location = new Point(creatureToken.Data.Location.X - current11.Radius, creatureToken.Data.Location.Y - current11.Radius);
								Size size2 = new Size(num17, num17);
								dictionary[current11] = new Rectangle(location, size2);
							}
						}
					}
					Hero hero = current9 as Hero;
					if (hero != null)
					{
						int size3 = Creature.GetSize(hero.Size);
						CombatData combatData = hero.CombatData;
						if (combatData != null)
						{
							foreach (OngoingCondition current12 in combatData.Conditions)
							{
								if (current12.Type == OngoingType.Aura)
								{
									int num18 = current12.Aura.Radius + size3 + current12.Aura.Radius;
									Point location2 = new Point(combatData.Location.X - current12.Aura.Radius, combatData.Location.Y - current12.Aura.Radius);
									Size size4 = new Size(num18, num18);
									dictionary[current12.Aura] = new Rectangle(location2, size4);
								}
							}
						}
					}
					foreach (Aura current13 in dictionary.Keys)
					{
						Rectangle rectangle = dictionary[current13];
						RectangleF region9 = this.fLayoutData.GetRegion(rectangle.Location, rectangle.Size);
						float radius = this.fLayoutData.SquareSize * 0.8f;
						GraphicsPath path2 = RoundedRectangle.Create(region9, radius);
						using (Pen pen5 = new Pen(Color.FromArgb(200, Color.Red)))
						{
							e.Graphics.DrawPath(pen5, path2);
						}
						using (Brush brush6 = new SolidBrush(Color.FromArgb(15, Color.Red)))
						{
							e.Graphics.FillPath(brush6, path2);
						}
					}
				}
			}
			if (this.fTokenLinks != null)
			{
				foreach (TokenLink current14 in this.fTokenLinks)
				{
					IToken token = current14.Tokens[0];
					IToken token2 = current14.Tokens[1];
					CombatData combatData2 = this.get_combat_data(token);
					CombatData combatData3 = this.get_combat_data(token2);
					if (combatData2.Visible && combatData3.Visible)
					{
						RectangleF left = this.get_token_rect(token);
						RectangleF left2 = this.get_token_rect(token2);
						if (!(left == RectangleF.Empty) && !(left2 == RectangleF.Empty))
						{
							Color color5 = (current14 == this.fHoverTokenLink) ? Color.Navy : Color.Black;
							PointF pt5 = new PointF((left.Left + left.Right) / 2f, (left.Top + left.Bottom) / 2f);
							PointF pt6 = new PointF((left2.Left + left2.Right) / 2f, (left2.Top + left2.Bottom) / 2f);
							using (Pen pen6 = new Pen(color5, 2f))
							{
								e.Graphics.DrawLine(pen6, pt5, pt6);
							}
						}
					}
				}
			}
			if (this.fEncounter != null)
			{
				foreach (CustomToken current15 in this.fEncounter.CustomTokens)
				{
					if (current15.Type == CustomTokenType.Overlay && this.is_visible(current15.Data))
					{
						if (current15.CreatureID != Guid.Empty)
						{
							CreatureSize size5 = CreatureSize.Medium;
							CombatData combatData4 = this.fEncounter.FindCombatData(current15.CreatureID);
							if (combatData4 != null)
							{
								current15.Data.Location = combatData4.Location;
								EncounterSlot encounterSlot2 = this.fEncounter.FindSlot(combatData4);
								ICreature creature3 = Session.FindCreature(encounterSlot2.Card.CreatureID, SearchType.Global);
								size5 = creature3.Size;
							}
							Hero hero2 = Session.Project.FindHero(current15.CreatureID);
							if (hero2 != null)
							{
								current15.Data.Location = hero2.CombatData.Location;
								size5 = hero2.Size;
							}
							if (current15.Data.Location != CombatData.NoPoint)
							{
								int num19 = (Creature.GetSize(size5) + 1) / 2;
								int x = current15.Data.Location.X - (current15.OverlaySize.Width - num19) / 2;
								int y = current15.Data.Location.Y - (current15.OverlaySize.Height - num19) / 2;
								current15.Data.Location = new Point(x, y);
							}
						}
						if (!(current15.Data.Location == CombatData.NoPoint))
						{
							bool selected = this.fSelectedTokens.Contains(current15);
							bool hovered = false;
							if (this.fHoverToken != null)
							{
								hovered = (this.get_combat_data(this.fHoverToken).ID == current15.Data.ID);
							}
							this.draw_custom(e.Graphics, current15.Data.Location, current15, selected, hovered, false);
						}
					}
				}
				foreach (CustomToken current16 in this.fEncounter.CustomTokens)
				{
					if (current16.Type == CustomTokenType.Token && !(current16.Data.Location == CombatData.NoPoint) && this.is_visible(current16.Data))
					{
						if (this.fDraggedToken != null && this.fDraggedToken.Token is CustomToken)
						{
							CustomToken customToken = this.fDraggedToken.Token as CustomToken;
							if (customToken.Type == CustomTokenType.Token && current16.ID == customToken.ID && current16.Data.Location == this.fDraggedToken.Start)
							{
								if (current16.Data.Location != this.fDraggedToken.Location)
								{
									this.draw_token_placeholder(e.Graphics, current16.Data.Location, this.fDraggedToken.Location, current16.TokenSize, false);
									continue;
								}
								continue;
							}
						}
						bool selected2 = this.fSelectedTokens.Contains(current16);
						bool hovered2 = false;
						if (this.fHoverToken != null)
						{
							hovered2 = (this.get_combat_data(this.fHoverToken).ID == current16.Data.ID);
						}
						this.draw_custom(e.Graphics, current16.Data.Location, current16, selected2, hovered2, false);
					}
				}
				foreach (EncounterSlot current17 in this.fEncounter.AllSlots)
				{
					EncounterWave encounterWave = this.fEncounter.FindWave(current17);
					if (encounterWave == null || encounterWave.Active || this.fShowAllWaves)
					{
						foreach (CombatData current18 in current17.CombatData)
						{
							if (!(current18.Location == CombatData.NoPoint) && this.is_visible(current18))
							{
								if (this.fDraggedToken != null && this.fDraggedToken.Token is CreatureToken)
								{
									CreatureToken creatureToken2 = this.fDraggedToken.Token as CreatureToken;
									if (current17.ID == creatureToken2.SlotID && current18.Location == this.fDraggedToken.Start)
									{
										if (current18.Location != this.fDraggedToken.Location)
										{
											ICreature creature4 = Session.FindCreature(current17.Card.CreatureID, SearchType.Global);
											bool has_picture = creature4.Image != null;
											this.draw_token_placeholder(e.Graphics, current18.Location, this.fDraggedToken.Location, creature4.Size, has_picture);
											continue;
										}
										continue;
									}
								}
								bool selected3 = false;
								foreach (IToken current19 in this.fSelectedTokens)
								{
									CreatureToken creatureToken3 = current19 as CreatureToken;
									if (creatureToken3 != null && current18 == creatureToken3.Data)
									{
										selected3 = true;
									}
								}
								bool hovered3 = false;
								CreatureToken creatureToken4 = this.fHoverToken as CreatureToken;
								if (creatureToken4 != null && current18 == creatureToken4.Data)
								{
									hovered3 = true;
								}
								this.draw_creature(e.Graphics, current18.Location, current17.Card, current18, selected3, hovered3, false);
							}
						}
					}
				}
			}
			if (this.fEncounter != null)
			{
				foreach (Hero current20 in Session.Project.Heroes)
				{
					if (current20 != null && !(current20.CombatData.Location == CombatData.NoPoint))
					{
						if (this.fDraggedToken != null && this.fDraggedToken.Token is Hero)
						{
							Hero hero3 = this.fDraggedToken.Token as Hero;
							if (current20.ID == hero3.ID && current20.CombatData.Location == this.fDraggedToken.Start)
							{
								if (current20.CombatData.Location != this.fDraggedToken.Location)
								{
									bool has_picture2 = current20.Portrait != null;
									this.draw_token_placeholder(e.Graphics, current20.CombatData.Location, this.fDraggedToken.Location, current20.Size, has_picture2);
									continue;
								}
								continue;
							}
						}
						bool selected4 = this.fSelectedTokens.Contains(current20);
						bool hovered4 = current20 == this.fHoverToken;
						this.draw_hero(e.Graphics, current20.CombatData.Location, current20, selected4, hovered4, false);
					}
				}
			}
			if (this.fNewToken != null)
			{
				if (this.fNewToken.Token is CreatureToken)
				{
					CreatureToken creatureToken5 = this.fNewToken.Token as CreatureToken;
					EncounterSlot encounterSlot3 = this.fEncounter.FindSlot(creatureToken5.SlotID);
					Session.FindCreature(encounterSlot3.Card.CreatureID, SearchType.Global);
					this.draw_creature(e.Graphics, this.fNewToken.Location, encounterSlot3.Card, creatureToken5.Data, true, true, true);
				}
				if (this.fNewToken.Token is Hero)
				{
					Hero hero4 = this.fNewToken.Token as Hero;
					this.draw_hero(e.Graphics, this.fNewToken.Location, hero4, true, true, true);
				}
				if (this.fNewToken.Token is CustomToken)
				{
					CustomToken ct = this.fNewToken.Token as CustomToken;
					this.draw_custom(e.Graphics, this.fNewToken.Location, ct, true, true, true);
				}
			}
			if (this.fDraggedToken != null)
			{
				if (this.fDraggedToken.Token is CreatureToken)
				{
					CreatureToken creatureToken6 = this.fDraggedToken.Token as CreatureToken;
					EncounterSlot encounterSlot4 = this.fEncounter.FindSlot(creatureToken6.SlotID);
					CombatData data = encounterSlot4.FindCombatData(this.fDraggedToken.Start);
					this.draw_creature(e.Graphics, this.fDraggedToken.Location, encounterSlot4.Card, data, true, true, true);
				}
				if (this.fDraggedToken.Token is Hero)
				{
					Hero hero5 = this.fDraggedToken.Token as Hero;
					this.draw_hero(e.Graphics, this.fDraggedToken.Location, hero5, true, true, true);
				}
				if (this.fDraggedToken.Token is CustomToken)
				{
					CustomToken ct2 = this.fDraggedToken.Token as CustomToken;
					this.draw_custom(e.Graphics, this.fDraggedToken.Location, ct2, true, true, true);
				}
				if (this.fDraggedToken.LinkedToken != null)
				{
					Pen pen7 = new Pen(Color.Red, 2f);
					RectangleF rectangleF2 = this.get_token_rect(this.fDraggedToken.LinkedToken);
					e.Graphics.DrawRectangle(pen7, rectangleF2.X, rectangleF2.Y, rectangleF2.Width, rectangleF2.Height);
				}
			}
			this.fTokenLinkRegions.Clear();
			if (this.fTokenLinks != null)
			{
				foreach (TokenLink current21 in this.fTokenLinks)
				{
					if (!(current21.Text == ""))
					{
						IToken token3 = current21.Tokens[0];
						IToken token4 = current21.Tokens[1];
						CombatData combatData5 = this.get_combat_data(token3);
						CombatData combatData6 = this.get_combat_data(token4);
						if (combatData5.Visible && combatData6.Visible)
						{
							Point left3 = this.get_token_location(token3);
							Point left4 = this.get_token_location(token4);
							if (!(left3 == CombatData.NoPoint) && !(left4 == CombatData.NoPoint))
							{
								RectangleF rectangleF3 = this.get_token_rect(token3);
								RectangleF rectangleF4 = this.get_token_rect(token4);
								PointF pointF = new PointF((rectangleF3.Left + rectangleF3.Right) / 2f, (rectangleF3.Top + rectangleF3.Bottom) / 2f);
								PointF pointF2 = new PointF((rectangleF4.Left + rectangleF4.Right) / 2f, (rectangleF4.Top + rectangleF4.Bottom) / 2f);
								string text3 = current21.Text;
								float emSize2 = Math.Min(this.Font.Size, this.fLayoutData.SquareSize / 4f);
								using (Font font3 = new Font(this.Font.FontFamily, emSize2))
								{
									SizeF size6 = e.Graphics.MeasureString(text3, font3);
									size6 = new SizeF(size6.Width * 1.2f, size6.Height * 1.2f);
									PointF pointF3 = new PointF((pointF.X + pointF2.X) / 2f, (pointF.Y + pointF2.Y) / 2f);
									PointF location3 = new PointF(pointF3.X - size6.Width / 2f, pointF3.Y - size6.Height / 2f);
									RectangleF rectangleF5 = new RectangleF(location3, size6);
									Pen pen8 = (current21 == this.fHoverTokenLink) ? Pens.Blue : Pens.Navy;
									e.Graphics.FillRectangle(Brushes.White, rectangleF5);
									e.Graphics.DrawString(text3, font3, Brushes.Navy, rectangleF5, this.fCentred);
									e.Graphics.DrawRectangle(pen8, rectangleF5.X, rectangleF5.Y, rectangleF5.Width, rectangleF5.Height);
									this.fTokenLinkRegions[current21] = rectangleF5;
								}
							}
						}
					}
				}
			}
			foreach (MapSketch current22 in this.fSketches)
			{
				this.draw_sketch(e.Graphics, current22);
			}
			if (this.fDrawing != null && this.fDrawing.CurrentSketch != null)
			{
				this.draw_sketch(e.Graphics, this.fDrawing.CurrentSketch);
			}
			if (this.fLineOfSight)
			{
				Point pt7 = base.PointToClient(Cursor.Position);
				if (base.ClientRectangle.Contains(pt7))
				{
					PointF pt8 = this.get_closest_vertex(pt7);
					float num20 = Math.Max(this.fLayoutData.SquareSize / 10f, 3f);
					foreach (IToken current23 in this.fSelectedTokens)
					{
						RectangleF rectangleF6 = this.get_token_rect(current23);
						foreach (PointF current24 in new List<PointF>
						{
							new PointF(rectangleF6.Left, rectangleF6.Top),
							new PointF(rectangleF6.Left, rectangleF6.Bottom),
							new PointF(rectangleF6.Right, rectangleF6.Top),
							new PointF(rectangleF6.Right, rectangleF6.Bottom)
						})
						{
							e.Graphics.DrawLine(Pens.Blue, pt8, current24);
							RectangleF rect3 = new RectangleF(current24.X - num20, current24.Y - num20, num20 * 2f, num20 * 2f);
							e.Graphics.FillEllipse(Brushes.LightBlue, rect3);
							e.Graphics.DrawEllipse(Pens.Blue, rect3);
						}
					}
					RectangleF rect4 = new RectangleF(pt8.X - num20, pt8.Y - num20, num20 * 2f, num20 * 2f);
					e.Graphics.FillEllipse(Brushes.LightBlue, rect4);
					e.Graphics.DrawEllipse(Pens.Blue, rect4);
				}
			}
			if (this.fDraggedOutline != null)
			{
				RectangleF region10 = this.fLayoutData.GetRegion(this.fDraggedOutline.Region.Location, this.fDraggedOutline.Region.Size);
				e.Graphics.DrawRectangle(Pens.DarkBlue, region10.X, region10.Y, region10.Width, region10.Height);
				string text4 = this.fDraggedOutline.Region.Width + "x" + this.fDraggedOutline.Region.Height;
				SizeF sizeF2 = e.Graphics.MeasureString(text4, this.Font);
				sizeF2.Width = Math.Min(region10.Width, sizeF2.Width);
				sizeF2.Height = Math.Min(region10.Height, sizeF2.Height);
				float num21 = (region10.Width - sizeF2.Width) / 2f;
				float num22 = (region10.Height - sizeF2.Height) / 2f;
				RectangleF rect5 = new RectangleF(region10.X + num21, region10.Y + num22, sizeF2.Width, sizeF2.Height);
				using (Brush brush7 = new SolidBrush(Color.FromArgb(150, Color.White)))
				{
					e.Graphics.FillRectangle(brush7, rect5);
				}
				e.Graphics.DrawString(text4, this.Font, Brushes.DarkBlue, region10, this.fCentred);
			}
			RectangleF region11 = this.fLayoutData.GetRegion(this.fCurrentOutline.Location, this.fCurrentOutline.Size);
			e.Graphics.DrawRectangle(Pens.LightBlue, region11.X, region11.Y, region11.Width, region11.Height);
			if (this.fFrameType != MapDisplayType.None && this.fViewpoint != Rectangle.Empty && !this.fAllowScrolling)
			{
				Color baseColor = Color.Black;
				if (this.fMode == MapViewMode.Plain)
				{
					baseColor = Color.White;
				}
				int alpha = 255;
				switch (this.fFrameType)
				{
				case MapDisplayType.Dimmed:
					alpha = 160;
					break;
				case MapDisplayType.Opaque:
					alpha = 255;
					break;
				}
				RectangleF region12 = this.fLayoutData.GetRegion(this.fViewpoint.Location, this.fViewpoint.Size);
				using (Brush brush8 = new SolidBrush(Color.FromArgb(alpha, baseColor)))
				{
					e.Graphics.FillRectangle(brush8, 0f, 0f, (float)base.ClientRectangle.Width, region12.Top);
					e.Graphics.FillRectangle(brush8, 0f, region12.Bottom, (float)base.ClientRectangle.Width, (float)base.ClientRectangle.Height);
					e.Graphics.FillRectangle(brush8, 0f, region12.Top, region12.Left, region12.Height);
					e.Graphics.FillRectangle(brush8, region12.Right, region12.Top, (float)base.ClientRectangle.Width - region12.Right, region12.Height);
				}
				if (this.fHighlightAreas)
				{
					e.Graphics.DrawRectangle(SystemPens.ControlLight, region12.X, region12.Y, region12.Width, region12.Height);
				}
			}
			string text5 = this.fCaption;
			if (text5 == "")
			{
				if (this.fMode == MapViewMode.Normal && this.fMap.Areas.Count == 0)
				{
					text5 = "To create map areas (rooms etc), right-click on the map and drag.";
				}
				if (this.fMap.Name == "")
				{
					text5 = "You need to give this map a name.";
				}
				if (this.fMode == MapViewMode.Normal && this.fMap.Tiles.Count == 0)
				{
					text5 = "To begin, drag tiles from the list on the right onto the blueprint.";
				}
				if (this.fAllowScrolling)
				{
					text5 = "Map is in scroll / zoom mode; double-click to return to tactical mode.";
				}
				if (this.fDrawing != null)
				{
					text5 = "Map is in drawing mode; double-click to return to tactical mode.";
				}
				if (this.fLineOfSight)
				{
					text5 = "Map is in line of sight mode; select tokens to check sightlines, or double-click to return to tactical mode.";
				}
				if (this.fDraggedToken != null && this.fDraggedToken.LinkedToken != null)
				{
					TokenLink tokenLink = this.find_link(this.fDraggedToken.Token, this.fDraggedToken.LinkedToken);
					if (tokenLink == null)
					{
						text5 = "Release here to create a link.";
					}
					else
					{
						string str2 = (tokenLink.Text == "") ? "link" : ("'" + tokenLink.Text + "' link");
						text5 = "Release here to remove the " + str2 + ".";
					}
				}
			}
			if (text5 != "")
			{
				float num23 = 10f;
				float num24 = (float)base.ClientRectangle.Width - 2f * num23;
				float num25 = e.Graphics.MeasureString(text5, this.Font, (int)num24).Height * 2f;
				RectangleF rectangleF7 = new RectangleF(num23, num23, num24, num25);
				GraphicsPath path3 = RoundedRectangle.Create(rectangleF7, num25 / 3f);
				using (Brush brush9 = new SolidBrush(Color.FromArgb(200, Color.Black)))
				{
					e.Graphics.FillPath(brush9, path3);
				}
				e.Graphics.DrawPath(Pens.Black, path3);
				e.Graphics.DrawString(text5, this.Font, Brushes.White, rectangleF7, this.fCentred);
			}
		}

		private void draw_grid_label(Graphics g, string str, Font font, RectangleF rect, StringFormat sf)
		{
			for (int i = -2; i <= 2; i++)
			{
				for (int j = -2; j <= 2; j++)
				{
					RectangleF layoutRectangle = new RectangleF(rect.X + (float)i, rect.Y + (float)j, rect.Width, rect.Height);
					using (Brush brush = new SolidBrush(Color.FromArgb(50, Color.White)))
					{
						g.DrawString(str, font, brush, layoutRectangle, sf);
					}
				}
			}
			g.DrawString(str, font, Brushes.Black, rect, sf);
		}

		private void draw_tile(Graphics g, Tile tile, int rotation, RectangleF rect, bool ghost)
		{
			try
			{
				Image image = tile.Image;
				if (image == null)
				{
					image = tile.BlankImage;
				}
				ImageAttributes imageAttributes = new ImageAttributes();
				if (ghost)
				{
					imageAttributes.SetColorMatrix(new ColorMatrix
					{
						Matrix33 = 0.4f
					});
				}
				Rectangle destRect = new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
				switch (rotation % 4)
				{
				case 0:
					using (Bitmap bitmap = new Bitmap(image))
					{
						g.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttributes);
						goto IL_15E;
					}
					break;
				case 1:
					break;
				case 2:
					goto IL_EC;
				case 3:
					goto IL_125;
				default:
					goto IL_15E;
				}
				using (Bitmap bitmap2 = new Bitmap(image))
				{
					bitmap2.RotateFlip(RotateFlipType.Rotate90FlipNone);
					g.DrawImage(bitmap2, destRect, 0, 0, bitmap2.Width, bitmap2.Height, GraphicsUnit.Pixel, imageAttributes);
					goto IL_15E;
				}
				IL_EC:
				using (Bitmap bitmap3 = new Bitmap(image))
				{
					bitmap3.RotateFlip(RotateFlipType.Rotate180FlipNone);
					g.DrawImage(bitmap3, destRect, 0, 0, bitmap3.Width, bitmap3.Height, GraphicsUnit.Pixel, imageAttributes);
					goto IL_15E;
				}
				IL_125:
				using (Bitmap bitmap4 = new Bitmap(image))
				{
					bitmap4.RotateFlip(RotateFlipType.Rotate270FlipNone);
					g.DrawImage(bitmap4, destRect, 0, 0, bitmap4.Width, bitmap4.Height, GraphicsUnit.Pixel, imageAttributes);
				}
				IL_15E:;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void draw_creature(Graphics g, Point pt, EncounterCard card, CombatData data, bool selected, bool hovered, bool ghost)
		{
			ICreature creature = Session.FindCreature(card.CreatureID, SearchType.Global);
			if (creature == null)
			{
				return;
			}
			Color c = Color.Black;
			if (creature is NPC)
			{
				c = Color.DarkBlue;
			}
			if (data != null)
			{
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(data);
				switch (encounterSlot.GetState(data))
				{
				case CreatureState.Bloodied:
					c = Color.Maroon;
					break;
				case CreatureState.Defeated:
					c = Color.Gray;
					break;
				}
			}
			bool boxed = false;
			foreach (IToken current in this.fBoxedTokens)
			{
				if (current is CreatureToken)
				{
					CreatureToken creatureToken = current as CreatureToken;
					if (creatureToken.Data.ID == data.ID)
					{
						boxed = true;
						break;
					}
				}
			}
			bool visible = data.Visible;
			if (!visible && this.fShowCreatures == CreatureViewMode.Visible)
			{
				return;
			}
			string title;
			if (this.fShowCreatureLabels)
			{
				title = data.DisplayName;
			}
			else
			{
				title = creature.Category;
			}
			string text = TextHelper.Abbreviation(title);
			ghost = (ghost || !visible);
			this.draw_token(g, pt, creature.Size, creature.Image, c, text, selected, boxed, hovered, ghost, data.Conditions, data.Altitude);
			if (this.fShowHealthBars && data != null)
			{
				int size = Creature.GetSize(creature.Size);
				Size size2 = new Size(size, size);
				RectangleF region = this.fLayoutData.GetRegion(pt, size2);
				this.draw_health_bar(g, region, data, card.HP);
			}
		}

		private void draw_hero(Graphics g, Point pt, Hero hero, bool selected, bool hovered, bool ghost)
		{
			Color c = Color.FromArgb(0, 80, 0);
			int hP = hero.HP;
			if (hP != 0)
			{
				int arg_17_0 = hP / 2;
				int num = hP + hero.CombatData.TempHP - hero.CombatData.Damage;
				if (num <= 0)
				{
					c = Color.Gray;
				}
				else if (num <= hP / 2)
				{
					c = Color.Maroon;
				}
			}
			bool boxed = this.fBoxedTokens.Contains(hero);
			string text = TextHelper.Abbreviation(hero.Name);
			bool flag = true;
			if (!flag && this.fShowCreatures == CreatureViewMode.Visible)
			{
				return;
			}
			ghost = (ghost || !flag);
			this.draw_token(g, pt, hero.Size, hero.Portrait, c, text, selected, boxed, hovered, ghost, hero.CombatData.Conditions, hero.CombatData.Altitude);
			if (this.fShowHealthBars && hero.CombatData != null && hero.HP != 0)
			{
				int size = Creature.GetSize(hero.Size);
				Size size2 = new Size(size, size);
				RectangleF region = this.fLayoutData.GetRegion(pt, size2);
				this.draw_health_bar(g, region, hero.CombatData, hero.HP);
			}
		}

		private void draw_custom(Graphics g, Point pt, CustomToken ct, bool selected, bool hovered, bool ghost)
		{
			bool flag = this.fBoxedTokens.Contains(ct);
			string text = TextHelper.Abbreviation(ct.Name);
			bool visible = ct.Data.Visible;
			if (!visible && this.fShowCreatures == CreatureViewMode.Visible)
			{
				return;
			}
			switch (ct.Type)
			{
			case CustomTokenType.Token:
			{
				ghost = (ghost || !visible);
				List<OngoingCondition> conditions = new List<OngoingCondition>();
				this.draw_token(g, pt, ct.TokenSize, ct.Image, ct.Colour, text, selected, flag, hovered, ghost, conditions, 0);
				return;
			}
			case CustomTokenType.Overlay:
			{
				RectangleF region = this.fLayoutData.GetRegion(pt, ct.OverlaySize);
				RoundedRectangle.RectangleCorners corners = RoundedRectangle.RectangleCorners.All;
				int alpha = flag ? 220 : 140;
				if (ct.OverlayStyle == OverlayStyle.Block)
				{
					corners = RoundedRectangle.RectangleCorners.None;
					alpha = 255;
				}
				float radius = this.fLayoutData.SquareSize * 0.3f;
				GraphicsPath graphicsPath = RoundedRectangle.Create(region, radius, corners);
				if (ct.Image == null)
				{
					using (Brush brush = new SolidBrush(Color.FromArgb(alpha, ct.Colour)))
					{
						g.FillPath(brush, graphicsPath);
					}
					if (this.fShowCreatureLabels)
					{
						Pen pen = (selected || hovered) ? Pens.White : new Pen(ct.Colour);
						g.DrawPath(pen, graphicsPath);
					}
				}
				else
				{
					if (ct.OverlayStyle == OverlayStyle.Rounded)
					{
						ColorMatrix colorMatrix = new ColorMatrix();
						colorMatrix.Matrix33 = 0.4f;
						ImageAttributes imageAttributes = new ImageAttributes();
						imageAttributes.SetColorMatrix(colorMatrix);
						Rectangle destRect = new Rectangle((int)region.X, (int)region.Y, (int)region.Width, (int)region.Height);
						g.SetClip(graphicsPath);
						g.DrawImage(ct.Image, destRect, 0, 0, ct.Image.Width, ct.Image.Height, GraphicsUnit.Pixel, imageAttributes);
						g.ResetClip();
					}
					else
					{
						g.DrawImage(ct.Image, region);
					}
					bool flag2 = selected || hovered;
					if (flag2 && this.fShowCreatureLabels)
					{
						using (Pen pen2 = new Pen(Color.FromArgb(alpha, Color.White)))
						{
							g.DrawPath(pen2, graphicsPath);
						}
					}
				}
				if (ct.DifficultTerrain)
				{
					for (int i = pt.X; i < pt.X + ct.OverlaySize.Width; i++)
					{
						for (int j = pt.Y; j < pt.Y + ct.OverlaySize.Height; j++)
						{
							RectangleF region2 = this.fLayoutData.GetRegion(new Point(i, j), new Size(1, 1));
							float num = region2.Width / 10f;
							float num2 = region2.Width / 5f;
							float num3 = region2.X + region2.Width - num;
							float num4 = region2.Y + num2 + num;
							PointF pointF = new PointF(num3 - num2 / 2f, num4 - num2);
							PointF pointF2 = new PointF(num3 - num2, num4);
							PointF pointF3 = new PointF(num3, num4);
							using (Brush brush2 = new SolidBrush(Color.FromArgb(180, Color.White)))
							{
								g.FillPolygon(brush2, new PointF[]
								{
									pointF,
									pointF2,
									pointF3
								});
							}
							g.DrawPolygon(Pens.DarkGray, new PointF[]
							{
								pointF,
								pointF2,
								pointF3
							});
						}
					}
				}
				if (this.fSelectedTokens.Contains(ct) && this.fShowCreatureLabels)
				{
					StringFormat stringFormat = this.fCentred;
					if (region.Height > region.Width)
					{
						stringFormat = new StringFormat(this.fCentred);
						stringFormat.FormatFlags |= StringFormatFlags.DirectionVertical;
					}
					using (Font font = new Font(this.Font.FontFamily, this.fLayoutData.SquareSize / 5f))
					{
						SizeF sz = g.MeasureString(ct.Name, font, region.Size, stringFormat);
						sz += new SizeF(4f, 4f);
						RectangleF rectangleF = new RectangleF(region.X + (region.Width - sz.Width) / 2f, region.Y + (region.Height - sz.Height) / 2f, sz.Width, sz.Height);
						g.DrawRectangle(Pens.Black, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
						using (Brush brush3 = new SolidBrush(Color.FromArgb(210, Color.White)))
						{
							g.FillRectangle(brush3, rectangleF);
						}
						g.DrawString(ct.Name, font, Brushes.Black, rectangleF, stringFormat);
					}
				}
				return;
			}
			default:
				return;
			}
		}

		private void draw_token(Graphics g, Point pt, CreatureSize size, Image img, Color c, string text, bool selected, bool boxed, bool hovered, bool ghost, List<OngoingCondition> conditions, int altitude)
		{
			try
			{
				int size2 = Creature.GetSize(size);
				RectangleF region = this.fLayoutData.GetRegion(pt, new Size(size2, size2));
				RectangleF rectangleF = region;
				if (boxed)
				{
					g.FillRectangle(Brushes.Blue, rectangleF);
				}
				if (size == CreatureSize.Small || size == CreatureSize.Tiny)
				{
					float num = rectangleF.Width / 7f;
					rectangleF = new RectangleF(rectangleF.X + num, rectangleF.Y + num, rectangleF.Width - 2f * num, rectangleF.Height - 2f * num);
				}
				if (img == null)
				{
					Pen pen = Pens.White;
					if (selected || hovered)
					{
						pen = Pens.Red;
					}
					float num2 = 2f;
					RectangleF rect = new RectangleF(rectangleF.X + num2, rectangleF.Y + num2, rectangleF.Width - 2f * num2, rectangleF.Height - 2f * num2);
					int alpha = ghost ? 140 : 255;
					using (Brush brush = new SolidBrush(Color.FromArgb(alpha, c)))
					{
						g.FillEllipse(brush, rectangleF);
					}
					g.DrawEllipse(pen, rect);
					float num3 = this.fLayoutData.SquareSize * (float)size2 / 6f;
					if (num3 <= 0f)
					{
						goto IL_306;
					}
					using (Font font = new Font(this.Font.FontFamily, num3))
					{
						g.DrawString(text, font, Brushes.White, rectangleF, this.fCentred);
						goto IL_306;
					}
				}
				ImageAttributes imageAttributes = new ImageAttributes();
				if (ghost)
				{
					imageAttributes.SetColorMatrix(new ColorMatrix
					{
						Matrix33 = 0.4f
					});
				}
				Rectangle rectangle = new Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height);
				if (this.fShowPictureTokens)
				{
					float radius = Math.Min(rectangleF.Width, this.fLayoutData.SquareSize) * 0.5f;
					GraphicsPath graphicsPath = RoundedRectangle.Create(rectangleF, radius);
					g.SetClip(graphicsPath);
					g.DrawImage(img, rectangle, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imageAttributes);
					g.ResetClip();
					Color color = Color.Black;
					float width = 2f;
					if (selected || hovered)
					{
						color = Color.Red;
						width = 1f;
					}
					using (Pen pen2 = new Pen(color, width))
					{
						g.DrawPath(pen2, graphicsPath);
					}
					if (!(c == Color.Maroon))
					{
						goto IL_306;
					}
					Color color2 = Color.FromArgb(100, Color.Red);
					using (Brush brush2 = new SolidBrush(color2))
					{
						g.FillPath(brush2, graphicsPath);
						goto IL_306;
					}
				}
				g.DrawImage(img, rectangle, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imageAttributes);
				if (c == Color.Maroon)
				{
					Color color3 = Color.FromArgb(100, Color.Red);
					using (Brush brush3 = new SolidBrush(color3))
					{
						g.FillRectangle(brush3, rectangle);
					}
				}
				IL_306:
				if (boxed)
				{
					using (Pen pen3 = new Pen(Color.White, 2f))
					{
						g.DrawRectangle(pen3, region.X, region.Y, region.Width, region.Height);
					}
				}
				if (this.fShowConditions && conditions.Count != 0)
				{
					float num4 = this.fLayoutData.SquareSize * 0.4f;
					PointF pointF = new PointF(region.Right - num4, region.Top);
					RectangleF rectangleF2 = new RectangleF(pointF.X, pointF.Y, num4, num4);
					using (Brush brush4 = new SolidBrush(Color.FromArgb(200, 0, 0)))
					{
						g.FillEllipse(brush4, rectangleF2);
					}
					using (Font font2 = new Font(this.Font.FontFamily, num4 / 3f, this.Font.Style | FontStyle.Bold))
					{
						g.DrawString(conditions.Count.ToString(), font2, Brushes.White, rectangleF2, this.fCentred);
					}
					using (Pen pen4 = new Pen(Color.White, 2f))
					{
						g.DrawEllipse(pen4, rectangleF2);
					}
				}
				if (altitude != 0)
				{
					float num5 = this.fLayoutData.SquareSize * 0.4f;
					PointF pointF2 = new PointF(region.Left, region.Top);
					RectangleF rectangleF3 = new RectangleF(pointF2.X, pointF2.Y, num5, num5);
					string s = ((altitude > 0) ? "↑" : "↓") + Math.Abs(altitude);
					using (Brush brush5 = new SolidBrush(Color.FromArgb(80, 80, 80)))
					{
						g.FillEllipse(brush5, rectangleF3);
					}
					using (Font font3 = new Font(this.Font.FontFamily, num5 / 3f, this.Font.Style | FontStyle.Bold))
					{
						g.DrawString(s, font3, Brushes.White, rectangleF3, this.fCentred);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void draw_token_placeholder(Graphics g, Point start_location, Point new_location, CreatureSize size, bool has_picture)
		{
			int size2 = Creature.GetSize(size);
			RectangleF region = this.fLayoutData.GetRegion(start_location, new Size(size2, size2));
			RectangleF region2 = this.fLayoutData.GetRegion(new_location, new Size(size2, size2));
			if (size == CreatureSize.Small || size == CreatureSize.Tiny)
			{
				float num = region.Width / 7f;
				region = new RectangleF(region.X + num, region.Y + num, region.Width - 2f * num, region.Height - 2f * num);
			}
			int num2 = 2;
			RectangleF rectangleF = new RectangleF(region.X + (float)num2, region.Y + (float)num2, region.Width - (float)(2 * num2), region.Height - (float)(2 * num2));
			if (has_picture)
			{
				float radius = Math.Min(region.Width, this.fLayoutData.SquareSize) * 0.5f;
				GraphicsPath path = RoundedRectangle.Create(region, radius);
				using (Brush brush = new SolidBrush(Color.FromArgb(180, Color.White)))
				{
					g.FillPath(brush, path);
					g.DrawPath(Pens.Red, path);
					goto IL_15B;
				}
			}
			using (Brush brush2 = new SolidBrush(Color.FromArgb(180, Color.White)))
			{
				g.FillEllipse(brush2, rectangleF);
				g.DrawEllipse(Pens.Red, rectangleF);
			}
			IL_15B:
			using (Font font = new Font(this.Font.FontFamily, this.fLayoutData.SquareSize * (float)size2 / 4f))
			{
				g.DrawString(this.get_distance(start_location, new_location).ToString(), font, Brushes.Red, rectangleF, this.fCentred);
			}
			PointF pt = new PointF(region.X + region.Width / 2f, region.Y + region.Height / 2f);
			PointF pt2 = new PointF(region2.X + region2.Width / 2f, region2.Y + region2.Height / 2f);
			double num3 = (double)(rectangleF.Width / 2f);
			if (new_location.X == start_location.X)
			{
				pt.Y += ((new_location.Y > start_location.Y) ? ((float)num3) : (-(float)num3));
				pt2.Y += ((new_location.Y > start_location.Y) ? (-(float)num3) : ((float)num3));
			}
			else
			{
				double num4 = Math.Atan((double)(new_location.Y - start_location.Y) / (double)(new_location.X - start_location.X));
				float num5 = (float)(num3 * Math.Cos(num4));
				float num6 = (float)(num3 * Math.Sin(num4));
				pt.X += ((new_location.X > start_location.X) ? num5 : (-num5));
				pt.Y += ((new_location.X > start_location.X) ? num6 : (-num6));
				pt2.X += ((new_location.X > start_location.X) ? (-num5) : num5);
				pt2.Y += ((new_location.X > start_location.X) ? (-num6) : num6);
			}
			g.DrawLine(Pens.Red, pt, pt2);
		}

		private void draw_sketch(Graphics g, MapSketch sketch)
		{
			using (Pen pen = new Pen(sketch.Colour, (float)sketch.Width))
			{
				for (int i = 1; i < sketch.Points.Count; i++)
				{
					PointF pt = this.get_point(sketch.Points[i - 1]);
					PointF pt2 = this.get_point(sketch.Points[i]);
					g.DrawLine(pen, pt, pt2);
				}
			}
		}

		private void draw_health_bar(Graphics g, RectangleF rect, CombatData data, int hp_full)
		{
			int num = hp_full + data.TempHP;
			int num2 = hp_full - data.Damage;
			Color color = Color.Green;
			if (num2 <= 0)
			{
				color = Color.Black;
			}
			int num3 = hp_full / 2;
			if (num2 <= num3)
			{
				color = Color.Maroon;
			}
			num2 = Math.Max(num2, 0);
			float num4 = (float)(num2 + data.TempHP) / (float)num;
			float num5 = (float)num2 / (float)num;
			float num6 = Math.Max(rect.Height / 12f, 4f);
			RectangleF rect2 = new RectangleF(rect.X, rect.Bottom - num6, rect.Width, num6);
			g.FillRectangle(Brushes.White, rect2);
			if (data.TempHP > 0)
			{
				RectangleF rect3 = new RectangleF(rect2.X, rect2.Y, rect2.Width * num4, rect2.Height);
				g.FillRectangle(Brushes.Blue, rect3);
			}
			using (Brush brush = new SolidBrush(color))
			{
				RectangleF rect4 = new RectangleF(rect2.X, rect2.Y, rect2.Width * num5, rect2.Height);
				g.FillRectangle(brush, rect4);
			}
			g.DrawRectangle(Pens.Black, rect2.X, rect2.Y, rect2.Width, rect2.Height);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			try
			{
				base.Focus();
				if (this.fMap != null)
				{
					if (this.fLayoutData == null)
					{
						this.fLayoutData = new MapData(this, this.fScalingFactor);
					}
					if (this.fDrawing != null)
					{
						if (this.fDrawing.CurrentSketch == null)
						{
							this.fDrawing.CurrentSketch = new MapSketch();
						}
					}
					else
					{
						Point point = base.PointToClient(Cursor.Position);
						Point squareAtPoint = this.fLayoutData.GetSquareAtPoint(point);
						if (this.fAllowScrolling)
						{
							if (this.fViewpoint == Rectangle.Empty)
							{
								this.fViewpoint = this.get_current_zoom_rect(true);
								this.fLayoutData = null;
								base.Invalidate();
							}
							this.fScrollingData = new MapView.ScrollingData();
							this.fScrollingData.Start = squareAtPoint;
						}
						else
						{
							if (this.fTactical && this.fEncounter != null)
							{
								Pair<IToken, Rectangle> pair = this.get_token_at(squareAtPoint);
								if (pair != null)
								{
									bool flag = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
									bool flag2 = (Control.ModifierKeys & Keys.Control) == Keys.Control;
									bool flag3 = e.Button == MouseButtons.Right;
									CreatureToken creatureToken = pair.First as CreatureToken;
									CustomToken customToken = pair.First as CustomToken;
									if (creatureToken != null && !this.is_visible(creatureToken.Data))
									{
										if (!flag && !flag2 && !flag3)
										{
											this.fSelectedTokens.Clear();
										}
									}
									else if (customToken != null && !this.is_visible(customToken.Data))
									{
										if (!flag && !flag2 && !flag3)
										{
											this.fSelectedTokens.Clear();
										}
									}
									else if (customToken != null && customToken.CreatureID != Guid.Empty)
									{
										if (!flag && !flag2 && !flag3)
										{
											this.fSelectedTokens.Clear();
										}
									}
									else if (flag || flag2)
									{
										if (e.Button == MouseButtons.Left)
										{
											bool flag4 = false;
											foreach (IToken current in this.fSelectedTokens)
											{
												if (this.get_token_location(current) == this.get_token_location(pair.First))
												{
													flag4 = true;
													this.fSelectedTokens.Remove(current);
													break;
												}
											}
											if (!flag4)
											{
												this.fSelectedTokens.Add(pair.First);
											}
											this.OnSelectedTokensChanged();
										}
									}
									else
									{
										this.fDraggedToken = new MapView.DraggedToken();
										this.fDraggedToken.Token = pair.First;
										this.fDraggedToken.Start = pair.Second.Location;
										this.fDraggedToken.Location = this.fDraggedToken.Start;
										this.fDraggedToken.Offset = new Size(squareAtPoint.X - pair.Second.Location.X, squareAtPoint.Y - pair.Second.Location.Y);
										bool flag5 = false;
										CombatData combatData = this.get_combat_data(pair.First);
										foreach (IToken current2 in this.fSelectedTokens)
										{
											CombatData combatData2 = this.get_combat_data(current2);
											if (combatData.ID == combatData2.ID)
											{
												flag5 = true;
												break;
											}
										}
										if (!flag5)
										{
											this.fSelectedTokens.Clear();
											this.fSelectedTokens.Add(pair.First);
											this.OnSelectedTokensChanged();
										}
									}
								}
								else
								{
									this.fSelectedTokens.Clear();
									this.OnSelectedTokensChanged();
								}
							}
							if (this.fMode == MapViewMode.Normal)
							{
								if (this.fSelectedTiles == null)
								{
									this.fSelectedTiles = new List<TileData>();
								}
								if (Control.ModifierKeys != Keys.Control && Control.ModifierKeys != Keys.Shift)
								{
									this.fSelectedTiles.Clear();
								}
								TileData tileAtSquare = this.fLayoutData.GetTileAtSquare(squareAtPoint);
								if (tileAtSquare != null && this.fMap.Tiles.Contains(tileAtSquare))
								{
									this.fSelectedTiles.Add(tileAtSquare);
								}
								MouseButtons button = e.Button;
								if (button != MouseButtons.Left)
								{
									if (button == MouseButtons.Right)
									{
										this.fCurrentOutline = Rectangle.Empty;
										this.fDraggedOutline = new MapView.DraggedOutline();
										this.fDraggedOutline.Start = point;
										this.fDraggedOutline.Region = new Rectangle(squareAtPoint, new Size(1, 1));
									}
								}
								else if (this.fSelectedTiles.Count != 0)
								{
									this.fDraggedTiles = new MapView.DraggedTiles();
									this.fDraggedTiles.Tiles = this.fSelectedTiles;
									this.fDraggedTiles.Start = point;
								}
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

		protected override void OnMouseMove(MouseEventArgs e)
		{
			try
			{
				if (this.fMap != null)
				{
					if (this.fLayoutData == null)
					{
						this.fLayoutData = new MapData(this, this.fScalingFactor);
					}
					Point point = base.PointToClient(Cursor.Position);
					Point squareAtPoint = this.fLayoutData.GetSquareAtPoint(point);
					if (this.fDrawing != null)
					{
						if (this.fDrawing.CurrentSketch != null)
						{
							RectangleF region = this.fLayoutData.GetRegion(squareAtPoint, new Size(1, 1));
							float x = ((float)point.X - region.X) / region.Width;
							float y = ((float)point.Y - region.Y) / region.Height;
							MapSketchPoint mapSketchPoint = new MapSketchPoint();
							mapSketchPoint.Square = squareAtPoint;
							mapSketchPoint.Location = new PointF(x, y);
							PointF pointF = this.get_point(mapSketchPoint);
							Console.WriteLine(pointF);
							this.fDrawing.CurrentSketch.Points.Add(mapSketchPoint);
							base.Invalidate();
						}
					}
					else if (this.fAllowScrolling)
					{
						if (this.fScrollingData != null && this.fViewpoint != Rectangle.Empty && this.fScrollingData.Start != squareAtPoint)
						{
							int num = this.fScrollingData.Start.X - squareAtPoint.X;
							int num2 = this.fScrollingData.Start.Y - squareAtPoint.Y;
							this.fViewpoint.X = this.fViewpoint.X + num;
							this.fViewpoint.Y = this.fViewpoint.Y + num2;
							this.fLayoutData = null;
							base.Invalidate();
						}
					}
					else
					{
						if (this.fTactical)
						{
							bool flag = false;
							if (this.fDraggedToken == null && (Control.ModifierKeys & Keys.Control) != Keys.Control)
							{
								this.fHoverTokenLink = null;
								foreach (TokenLink current in this.fTokenLinkRegions.Keys)
								{
									if (this.fTokenLinkRegions[current].Contains(point))
									{
										this.fHoverTokenLink = current;
									}
								}
								Pair<IToken, Rectangle> pair = this.get_token_at(squareAtPoint);
								if (pair != null)
								{
									CreatureToken creatureToken = pair.First as CreatureToken;
									CustomToken customToken = pair.First as CustomToken;
									if ((creatureToken == null || this.is_visible(creatureToken.Data)) && (customToken == null || this.is_visible(customToken.Data)))
									{
										if (this.fHoverToken == null)
										{
											flag = true;
										}
										else
										{
											if (pair.First is CreatureToken)
											{
												CreatureToken creatureToken2 = this.fHoverToken as CreatureToken;
												CreatureToken creatureToken3 = pair.First as CreatureToken;
												flag = (creatureToken2 == null || creatureToken2.Data.ID != creatureToken3.Data.ID);
											}
											if (pair.First is CustomToken)
											{
												CustomToken customToken2 = this.fHoverToken as CustomToken;
												CustomToken customToken3 = pair.First as CustomToken;
												flag = (customToken2 == null || customToken2.Data.ID != customToken3.Data.ID);
											}
											if (pair.First is Hero)
											{
												Hero hero = this.fHoverToken as Hero;
												Hero hero2 = pair.First as Hero;
												flag = (hero == null || hero.ID != hero2.ID);
											}
										}
										this.fHoverToken = pair.First;
									}
								}
								else if (this.fHoverToken != null)
								{
									flag = true;
									this.fHoverToken = null;
								}
							}
							if (this.fDraggedToken != null)
							{
								this.fDraggedToken.LinkedToken = null;
								Point location = squareAtPoint - this.fDraggedToken.Offset;
								Size size = this.get_token_size(this.fDraggedToken.Token);
								Rectangle target_rect = new Rectangle(location, size);
								CustomToken customToken4 = this.fDraggedToken.Token as CustomToken;
								if ((customToken4 != null && customToken4.Type == CustomTokenType.Overlay) || this.allow_creature_move(target_rect, this.fDraggedToken.Start))
								{
									this.fDraggedToken.Location = location;
									this.OnTokenDragged();
								}
								else if (this.fAllowLinkCreation)
								{
									Pair<IToken, Rectangle> pair2 = this.get_token_at(squareAtPoint);
									if (pair2 != null)
									{
										this.fDraggedToken.Location = this.fDraggedToken.Start;
										this.fDraggedToken.LinkedToken = pair2.First;
										this.OnTokenDragged();
									}
								}
							}
							if (flag)
							{
								this.OnHoverTokenChanged();
							}
							base.Invalidate();
						}
						MapArea mapArea = null;
						foreach (MapArea current2 in this.fMap.Areas)
						{
							if (current2.Region.Contains(squareAtPoint))
							{
								mapArea = current2;
							}
						}
						if (this.fHighlightedArea != mapArea)
						{
							this.fHighlightedArea = mapArea;
							this.OnHighlightedAreaChanged();
							base.Invalidate();
						}
						if (this.fMode == MapViewMode.Normal)
						{
							if (this.fDraggedTiles != null)
							{
								foreach (TileData current3 in this.fDraggedTiles.Tiles)
								{
									Tile tile = this.fLayoutData.Tiles[current3];
									int num3 = (int)((float)(point.X - this.fDraggedTiles.Start.X) / this.fLayoutData.SquareSize);
									int num4 = (int)((float)(point.Y - this.fDraggedTiles.Start.Y) / this.fLayoutData.SquareSize);
									this.fDraggedTiles.Offset = new Size(num3, num4);
									Point square = new Point(current3.Location.X + num3, current3.Location.Y + num4);
									Size size2 = tile.Size;
									if (current3.Rotations % 2 != 0)
									{
										size2 = new Size(tile.Size.Height, tile.Size.Width);
									}
									this.fDraggedTiles.Region = this.fLayoutData.GetRegion(square, size2);
								}
								base.Invalidate();
							}
							else if (this.fDraggedOutline != null)
							{
								Point squareAtPoint2 = this.fLayoutData.GetSquareAtPoint(this.fDraggedOutline.Start);
								Point squareAtPoint3 = this.fLayoutData.GetSquareAtPoint(point);
								int x2 = Math.Min(squareAtPoint3.X, squareAtPoint2.X);
								int y2 = Math.Min(squareAtPoint3.Y, squareAtPoint2.Y);
								int width = Math.Abs(squareAtPoint3.X - squareAtPoint2.X) + 1;
								int height = Math.Abs(squareAtPoint3.Y - squareAtPoint2.Y) + 1;
								this.fDraggedOutline.Region = new Rectangle(x2, y2, width, height);
								base.Invalidate();
							}
							else
							{
								this.fHoverTile = this.fLayoutData.GetTileAtSquare(squareAtPoint);
								base.Invalidate();
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

		protected override void OnMouseUp(MouseEventArgs e)
		{
			try
			{
				if (this.fMap != null)
				{
					if (this.fLayoutData == null)
					{
						this.fLayoutData = new MapData(this, this.fScalingFactor);
					}
					if (this.fDrawing != null)
					{
						if (this.fDrawing.CurrentSketch != null)
						{
							this.fSketches.Add(this.fDrawing.CurrentSketch);
							this.OnSketchCreated(this.fDrawing.CurrentSketch);
						}
						this.fDrawing.CurrentSketch = null;
						base.Invalidate();
					}
					else
					{
						Point point = base.PointToClient(Cursor.Position);
						if (this.fScrollingData != null)
						{
							if (this.fViewpoint != Rectangle.Empty)
							{
								this.fViewpoint = this.get_current_zoom_rect(true);
								this.fLayoutData = null;
								base.Invalidate();
							}
							this.fScrollingData = null;
						}
						else
						{
							if (this.fTactical)
							{
								if (this.fDraggedToken != null && this.fDraggedToken.Location != this.fDraggedToken.Start)
								{
									int distance = this.get_distance(this.fDraggedToken.Location, this.fDraggedToken.Start);
									if (this.fDraggedToken.Token is CreatureToken)
									{
										CreatureToken creatureToken = this.fDraggedToken.Token as CreatureToken;
										EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
										CombatData combatData = encounterSlot.FindCombatData(this.fDraggedToken.Start);
										combatData.Location = this.fDraggedToken.Location;
									}
									if (this.fDraggedToken.Token is Hero)
									{
										Hero hero = this.fDraggedToken.Token as Hero;
										hero.CombatData.Location = this.fDraggedToken.Location;
									}
									if (this.fDraggedToken.Token is CustomToken)
									{
										CustomToken customToken = this.fDraggedToken.Token as CustomToken;
										customToken.Data.Location = this.fDraggedToken.Location;
									}
									this.fDraggedToken = null;
									this.OnItemMoved(distance);
								}
								if (this.fDraggedToken != null && this.fDraggedToken.LinkedToken != null)
								{
									TokenLink tokenLink = this.find_link(this.fDraggedToken.Token, this.fDraggedToken.LinkedToken);
									if (tokenLink == null)
									{
										if (this.fDraggedToken.Token != this.fDraggedToken.LinkedToken)
										{
											TokenLink tokenLink2 = this.OnCreateTokenLink(new List<IToken>
											{
												this.fDraggedToken.Token,
												this.fDraggedToken.LinkedToken
											});
											if (tokenLink2 != null)
											{
												this.fTokenLinks.Add(tokenLink2);
											}
										}
									}
									else
									{
										this.fTokenLinks.Remove(tokenLink);
									}
									this.fDraggedToken = null;
								}
								this.fDraggedToken = null;
								this.OnTokenDragged();
								base.Invalidate();
							}
							Point squareAtPoint = this.fLayoutData.GetSquareAtPoint(point);
							MapArea mapArea = null;
							foreach (MapArea current in this.fMap.Areas)
							{
								if (current.Region.Contains(squareAtPoint))
								{
									mapArea = current;
								}
							}
							if (this.fSelectedArea != mapArea)
							{
								this.fSelectedArea = mapArea;
								this.OnAreaSelected(this.fSelectedArea);
								base.Invalidate();
							}
							if (this.fMode == MapViewMode.Normal)
							{
								if (this.fDraggedTiles != null)
								{
									if (point != this.fDraggedTiles.Start)
									{
										int distance2 = this.get_distance(point, this.fDraggedTiles.Start);
										foreach (TileData current2 in this.fDraggedTiles.Tiles)
										{
											int x = current2.Location.X + this.fDraggedTiles.Offset.Width;
											int y = current2.Location.Y + this.fDraggedTiles.Offset.Height;
											current2.Location = new Point(x, y);
											this.fMap.Tiles.Remove(current2);
											this.fMap.Tiles.Add(current2);
										}
										this.OnItemMoved(distance2);
									}
									this.fDraggedTiles = null;
									this.fLayoutData = null;
									base.Invalidate();
								}
								else if (this.fDraggedOutline != null)
								{
									if (point != this.fDraggedOutline.Start)
									{
										this.fCurrentOutline = this.fDraggedOutline.Region;
										this.OnRegionSelected();
									}
									else
									{
										Point squareAtPoint2 = this.fLayoutData.GetSquareAtPoint(point);
										TileData tileAtSquare = this.fLayoutData.GetTileAtSquare(squareAtPoint2);
										if (tileAtSquare != null)
										{
											this.fSelectedTiles = new List<TileData>();
											this.fSelectedTiles.Add(tileAtSquare);
											this.OnTileContext(tileAtSquare);
										}
									}
									this.fDraggedOutline = null;
									base.Invalidate();
								}
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

		protected override void OnMouseLeave(EventArgs e)
		{
			try
			{
				if (this.fDrawing != null)
				{
					if (this.fDrawing.CurrentSketch != null)
					{
						this.fSketches.Add(this.fDrawing.CurrentSketch);
						this.OnSketchCreated(this.fDrawing.CurrentSketch);
					}
					this.fDrawing.CurrentSketch = null;
					base.Invalidate();
				}
				else if (!this.fAllowScrolling)
				{
					if (this.fTactical)
					{
						this.fDraggedToken = null;
						this.OnTokenDragged();
						base.Invalidate();
					}
					if (this.fMode == MapViewMode.Normal)
					{
						this.fHoverTile = null;
						this.fHoverToken = null;
						this.fHoverTokenLink = null;
						if (this.fSelectedTokens.Count != 0)
						{
							this.fSelectedTokens.Clear();
							this.OnSelectedTokensChanged();
						}
						if (this.fHighlightedArea != null)
						{
							this.fHighlightedArea = null;
							this.OnHighlightedAreaChanged();
						}
						this.fDraggedTiles = null;
						this.fDraggedOutline = null;
						base.Invalidate();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		protected override void OnDoubleClick(EventArgs e)
		{
			try
			{
				if (this.fLineOfSight)
				{
					this.LineOfSight = false;
				}
				else if (this.fDrawing != null)
				{
					this.AllowDrawing = false;
				}
				else if (this.fAllowScrolling)
				{
					this.AllowScrolling = false;
				}
				else
				{
					if (this.fSelectedTokens.Count == 1)
					{
						this.OnTokenActivated(this.fSelectedTokens[0]);
					}
					if (this.fHighlightedArea != null)
					{
						this.OnAreaActivated(this.fHighlightedArea);
					}
					if (this.fHoverTokenLink != null)
					{
						int index = this.fTokenLinks.IndexOf(this.fHoverTokenLink);
						TokenLink tokenLink = this.OnEditTokenLink(this.fHoverTokenLink);
						if (tokenLink != null)
						{
							this.fTokenLinks[index] = tokenLink;
							base.Invalidate();
						}
					}
					base.OnDoubleClick(e);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			this.fAllowScrolling = true;
			this.OnMouseZoom(e);
		}

		public bool HandleKey(Keys key)
		{
			return key == Keys.Left || key == Keys.Right || key == (Keys.LButton | Keys.MButton | Keys.Space | Keys.Shift) || key == (Keys.LButton | Keys.RButton | Keys.MButton | Keys.Space | Keys.Shift) || key == Keys.Up || key == Keys.Down || key == Keys.Delete;
		}

		protected override bool IsInputKey(Keys key)
		{
			return this.HandleKey(key) || base.IsInputKey(key);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			bool flag = false;
			bool flag2 = false;
			Keys keyCode = e.KeyCode;
			switch (keyCode)
			{
			case Keys.Left:
				if (this.fSelectedTiles != null && this.fSelectedTiles.Count != 0)
				{
					if (e.Shift)
					{
						foreach (TileData current in this.fSelectedTiles)
						{
							current.Rotations--;
						}
						flag2 = true;
					}
					else
					{
						foreach (TileData current2 in this.fSelectedTiles)
						{
							current2.Location = new Point(current2.Location.X - 1, current2.Location.Y);
						}
						flag2 = true;
					}
				}
				break;
			case Keys.Up:
				if (this.fSelectedTiles != null && this.fSelectedTiles.Count != 0)
				{
					foreach (TileData current3 in this.fSelectedTiles)
					{
						current3.Location = new Point(current3.Location.X, current3.Location.Y - 1);
					}
					flag2 = true;
				}
				break;
			case Keys.Right:
				if (this.fSelectedTiles != null && this.fSelectedTiles.Count != 0)
				{
					if (e.Shift)
					{
						foreach (TileData current4 in this.fSelectedTiles)
						{
							current4.Rotations++;
						}
						flag2 = true;
					}
					else
					{
						foreach (TileData current5 in this.fSelectedTiles)
						{
							current5.Location = new Point(current5.Location.X + 1, current5.Location.Y);
						}
						flag2 = true;
					}
				}
				break;
			case Keys.Down:
				if (this.fSelectedTiles != null && this.fSelectedTiles.Count != 0)
				{
					foreach (TileData current6 in this.fSelectedTiles)
					{
						current6.Location = new Point(current6.Location.X, current6.Location.Y + 1);
					}
					flag2 = true;
				}
				break;
			default:
				if (keyCode == Keys.Delete)
				{
					if (this.fSelectedTiles != null && this.fSelectedTiles.Count != 0)
					{
						foreach (TileData current7 in this.fSelectedTiles)
						{
							this.fMap.Tiles.Remove(current7);
						}
						flag = true;
					}
				}
				break;
			}
			this.fLayoutData = null;
			base.Invalidate();
			if (flag2)
			{
				this.OnItemMoved(1);
			}
			if (flag)
			{
				this.OnItemRemoved();
			}
		}

		protected override void OnDragOver(DragEventArgs e)
		{
			Point pt = base.PointToClient(Cursor.Position);
			Point squareAtPoint = this.fLayoutData.GetSquareAtPoint(pt);
			Tile tile = e.Data.GetData(typeof(Tile)) as Tile;
			if (tile != null)
			{
				e.Effect = DragDropEffects.Copy;
				this.fNewTile = new MapView.NewTile();
				this.fNewTile.Tile = tile;
				this.fNewTile.Location = this.fLayoutData.GetSquareAtPoint(pt);
				this.fNewTile.Region = this.fLayoutData.GetRegion(this.fNewTile.Location, tile.Size);
				base.Invalidate();
			}
			CreatureToken creatureToken = e.Data.GetData(typeof(CreatureToken)) as CreatureToken;
			if (creatureToken != null)
			{
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
				ICreature creature = Session.FindCreature(encounterSlot.Card.CreatureID, SearchType.Global);
				int size = Creature.GetSize(creature.Size);
				if (this.allow_creature_move(new Rectangle(squareAtPoint, new Size(size, size)), CombatData.NoPoint))
				{
					this.fNewToken = new MapView.NewToken();
					this.fNewToken.Token = creatureToken;
					this.fNewToken.Location = squareAtPoint;
					e.Effect = DragDropEffects.Move;
					base.Invalidate();
				}
			}
			Hero hero = e.Data.GetData(typeof(Hero)) as Hero;
			if (hero != null)
			{
				int size2 = Creature.GetSize(hero.Size);
				if (this.allow_creature_move(new Rectangle(squareAtPoint, new Size(size2, size2)), CombatData.NoPoint))
				{
					this.fNewToken = new MapView.NewToken();
					this.fNewToken.Token = hero;
					this.fNewToken.Location = squareAtPoint;
					e.Effect = DragDropEffects.Move;
					base.Invalidate();
				}
			}
			CustomToken customToken = e.Data.GetData(typeof(CustomToken)) as CustomToken;
			if (customToken != null)
			{
				this.fNewToken = new MapView.NewToken();
				this.fNewToken.Token = customToken;
				this.fNewToken.Location = squareAtPoint;
				e.Effect = DragDropEffects.Move;
				base.Invalidate();
			}
		}

		protected override void OnDragLeave(EventArgs e)
		{
			this.fNewTile = null;
			this.fNewToken = null;
			base.Invalidate();
		}

		protected override void OnDragDrop(DragEventArgs e)
		{
			Tile tile = e.Data.GetData(typeof(Tile)) as Tile;
			if (tile != null)
			{
				TileData tileData = new TileData();
				tileData.TileID = this.fNewTile.Tile.ID;
				tileData.Location = this.fNewTile.Location;
				this.fNewTile = null;
				this.fMap.Tiles.Add(tileData);
				this.fSelectedTiles = new List<TileData>();
				this.fSelectedTiles.Add(tileData);
				this.fLayoutData = null;
				base.Invalidate();
				this.OnItemDropped();
			}
			CreatureToken creatureToken = e.Data.GetData(typeof(CreatureToken)) as CreatureToken;
			if (creatureToken != null)
			{
				creatureToken.Data.Location = this.fNewToken.Location;
				this.fNewToken = null;
				base.Invalidate();
				this.OnItemDropped();
			}
			Hero hero = e.Data.GetData(typeof(Hero)) as Hero;
			if (hero != null)
			{
				Hero hero2 = this.fNewToken.Token as Hero;
				hero2.CombatData.Location = this.fNewToken.Location;
				this.fNewToken = null;
				base.Invalidate();
				this.OnItemDropped();
			}
			CustomToken customToken = e.Data.GetData(typeof(CustomToken)) as CustomToken;
			if (customToken != null)
			{
				customToken.Data.Location = this.fNewToken.Location;
				this.fNewToken = null;
				base.Invalidate();
				this.OnItemDropped();
			}
		}

		private int get_distance(Point from, Point to)
		{
			int val = Math.Abs(from.X - to.X);
			int val2 = Math.Abs(from.Y - to.Y);
			return Math.Max(val, val2);
		}

		private Pair<IToken, Rectangle> get_token_at(Point square)
		{
			if (this.fEncounter == null)
			{
				return null;
			}
			foreach (Guid current in this.fSlotRegions.Keys)
			{
				List<Rectangle> list = this.fSlotRegions[current];
				foreach (Rectangle current2 in list)
				{
					if (current2.Contains(square))
					{
						EncounterSlot encounterSlot = this.fEncounter.FindSlot(current);
						CombatData data = encounterSlot.FindCombatData(current2.Location);
						Pair<IToken, Rectangle> result = new Pair<IToken, Rectangle>(new CreatureToken
						{
							SlotID = current,
							Data = data
						}, current2);
						return result;
					}
				}
			}
			foreach (Hero current3 in Session.Project.Heroes)
			{
				if (current3 == null)
				{
					Pair<IToken, Rectangle> result = null;
					return result;
				}
				int size = Creature.GetSize(current3.Size);
				Rectangle second = new Rectangle(current3.CombatData.Location, new Size(size, size));
				if (second.Contains(square))
				{
					Pair<IToken, Rectangle> result = new Pair<IToken, Rectangle>(current3, second);
					return result;
				}
			}
			foreach (CustomToken current4 in this.fEncounter.CustomTokens)
			{
				if (current4.Type == CustomTokenType.Token)
				{
					Size overlaySize = current4.OverlaySize;
					if (current4.Type == CustomTokenType.Token)
					{
						int size2 = Creature.GetSize(current4.TokenSize);
						overlaySize = new Size(size2, size2);
					}
					Rectangle second2 = new Rectangle(current4.Data.Location, overlaySize);
					if (second2.Contains(square))
					{
						Pair<IToken, Rectangle> result = new Pair<IToken, Rectangle>(current4, second2);
						return result;
					}
				}
			}
			foreach (CustomToken current5 in this.fEncounter.CustomTokens)
			{
				if (current5.Type == CustomTokenType.Overlay)
				{
					Size overlaySize2 = current5.OverlaySize;
					if (current5.Type == CustomTokenType.Token)
					{
						int size3 = Creature.GetSize(current5.TokenSize);
						overlaySize2 = new Size(size3, size3);
					}
					Rectangle second3 = new Rectangle(current5.Data.Location, overlaySize2);
					if (second3.Contains(square))
					{
						Pair<IToken, Rectangle> result = new Pair<IToken, Rectangle>(current5, second3);
						return result;
					}
				}
			}
			return null;
		}

		private bool allow_creature_move(Rectangle target_rect, Point initial_location)
		{
			for (int num = 0; num != target_rect.Width; num++)
			{
				for (int num2 = 0; num2 != target_rect.Height; num2++)
				{
					Point point = new Point(num + target_rect.X, num2 + target_rect.Y);
					if (this.fViewpoint != Rectangle.Empty && !this.fViewpoint.Contains(point))
					{
						return false;
					}
					if (this.fLayoutData.GetTileAtSquare(point) == null)
					{
						return false;
					}
					Pair<IToken, Rectangle> pair = this.get_token_at(point);
					if (pair != null && pair.Second.Location != initial_location)
					{
						CustomToken customToken = pair.First as CustomToken;
						if (customToken == null || customToken.Type != CustomTokenType.Overlay)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		private bool is_visible(CombatData cd)
		{
			if (cd == null)
			{
				return false;
			}
			switch (this.fShowCreatures)
			{
			case CreatureViewMode.All:
				return true;
			case CreatureViewMode.Visible:
				return cd.Visible;
			case CreatureViewMode.None:
				return false;
			default:
				return false;
			}
		}

		private Point get_token_location(IToken token)
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

		private Size get_token_size(IToken token)
		{
			if (token is CreatureToken)
			{
				CreatureToken creatureToken = token as CreatureToken;
				EncounterSlot encounterSlot = this.fEncounter.FindSlot(creatureToken.SlotID);
				ICreature creature = Session.FindCreature(encounterSlot.Card.CreatureID, SearchType.Global);
				int size = Creature.GetSize(creature.Size);
				return new Size(size, size);
			}
			if (token is Hero)
			{
				Hero hero = token as Hero;
				int size2 = Creature.GetSize(hero.Size);
				return new Size(size2, size2);
			}
			if (token is CustomToken)
			{
				CustomToken customToken = token as CustomToken;
				if (customToken.Type == CustomTokenType.Token)
				{
					int size3 = Creature.GetSize(customToken.TokenSize);
					return new Size(size3, size3);
				}
				if (customToken.Type == CustomTokenType.Overlay)
				{
					return customToken.OverlaySize;
				}
			}
			return new Size(1, 1);
		}

		private RectangleF get_token_rect(IToken token)
		{
			Point point = this.get_token_location(token);
			if (point == CombatData.NoPoint)
			{
				return RectangleF.Empty;
			}
			Size size = this.get_token_size(token);
			return this.fLayoutData.GetRegion(point, size);
		}

		private Rectangle get_current_zoom_rect(bool use_scaling)
		{
			MapData mapData = use_scaling ? new MapData(this, 1.0) : this.fLayoutData;
			Point squareAtPoint = mapData.GetSquareAtPoint(new Point(1, 1));
			Point squareAtPoint2 = mapData.GetSquareAtPoint(new Point(base.ClientRectangle.Right - 1, base.ClientRectangle.Bottom - 1));
			int width = 1 + (squareAtPoint2.X - squareAtPoint.X);
			int height = 1 + (squareAtPoint2.Y - squareAtPoint.Y);
			Size size = new Size(width, height);
			return new Rectangle(squareAtPoint, size);
		}

		private PointF get_point(MapSketchPoint msp)
		{
			RectangleF region = this.fLayoutData.GetRegion(msp.Square, new Size(1, 1));
			float num = region.Width * msp.Location.X;
			float num2 = region.Height * msp.Location.Y;
			return new PointF(region.X + num, region.Y + num2);
		}

		private CombatData get_combat_data(IToken token)
		{
			if (token is CreatureToken)
			{
				CreatureToken creatureToken = token as CreatureToken;
				return creatureToken.Data;
			}
			if (token is CustomToken)
			{
				CustomToken customToken = token as CustomToken;
				return customToken.Data;
			}
			if (token is Hero)
			{
				Hero hero = token as Hero;
				return hero.CombatData;
			}
			return null;
		}

		private TokenLink find_link(IToken t1, IToken t2)
		{
			RectangleF left = this.get_token_rect(t1);
			RectangleF left2 = this.get_token_rect(t2);
			foreach (TokenLink current in this.fTokenLinks)
			{
				RectangleF right = this.get_token_rect(current.Tokens[0]);
				RectangleF right2 = this.get_token_rect(current.Tokens[1]);
				bool flag = left == right || left2 == right;
				bool flag2 = left == right2 || left2 == right2;
				if (flag && flag2)
				{
					return current;
				}
			}
			return null;
		}

		private PointF get_closest_vertex(Point pt)
		{
			Point squareAtPoint = this.fLayoutData.GetSquareAtPoint(pt);
			RectangleF region = this.fLayoutData.GetRegion(squareAtPoint, new Size(1, 1));
			List<PointF> list = new List<PointF>();
			list.Add(new PointF(region.Left, region.Top));
			list.Add(new PointF(region.Left, region.Bottom - 1f));
			list.Add(new PointF(region.Right - 1f, region.Top));
			list.Add(new PointF(region.Right - 1f, region.Bottom - 1f));
			double num = 1.7976931348623157E+308;
			PointF result = PointF.Empty;
			foreach (PointF current in list)
			{
				float num2 = current.X - (float)pt.X;
				float num3 = current.Y - (float)pt.Y;
				double num4 = Math.Sqrt((double)(num2 * num2 + num3 * num3));
				if (num4 < num)
				{
					result = current;
					num = num4;
				}
			}
			return result;
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
			base.SuspendLayout();
			this.AllowDrop = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "MapView";
			base.ResumeLayout(false);
		}
	}
}
