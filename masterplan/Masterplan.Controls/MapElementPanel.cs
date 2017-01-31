using Masterplan.Data;
using Masterplan.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class MapElementPanel : UserControl
	{
		private MapElement fMapElement;

		private IContainer components;

		private ToolStrip Toolbar;

		private MapView MapView;

		private ToolStripButton toolStripButton1;

		public MapElement MapElement
		{
			get
			{
				return this.fMapElement;
			}
			set
			{
				this.fMapElement = value;
				this.update_view();
			}
		}

		public MapElementPanel()
		{
			this.InitializeComponent();
		}

		private void MapSelectBtn_Click(object sender, EventArgs e)
		{
			MapAreaSelectForm mapAreaSelectForm = new MapAreaSelectForm(this.fMapElement.MapID, this.fMapElement.MapAreaID);
			if (mapAreaSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fMapElement.MapID = ((mapAreaSelectForm.Map != null) ? mapAreaSelectForm.Map.ID : Guid.Empty);
				this.fMapElement.MapAreaID = ((mapAreaSelectForm.MapArea != null) ? mapAreaSelectForm.MapArea.ID : Guid.Empty);
				this.update_view();
			}
		}

		private void update_view()
		{
			Map map = Session.Project.FindTacticalMap(this.fMapElement.MapID);
			if (map == null)
			{
				this.MapView.Map = null;
				this.MapView.Viewpoint = Rectangle.Empty;
				return;
			}
			this.MapView.Map = map;
			MapArea mapArea = map.FindArea(this.fMapElement.MapAreaID);
			if (mapArea != null)
			{
				this.MapView.Viewpoint = mapArea.Region;
				return;
			}
			this.MapView.Viewpoint = Rectangle.Empty;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MapElementPanel));
			this.Toolbar = new ToolStrip();
			this.toolStripButton1 = new ToolStripButton();
			this.MapView = new MapView();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripButton1
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(410, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.toolStripButton1.Image = (Image)componentResourceManager.GetObject("toolStripButton1.Image");
			this.toolStripButton1.ImageTransparentColor = Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new Size(69, 22);
			this.toolStripButton1.Text = "Select Map";
			this.toolStripButton1.Click += new EventHandler(this.MapSelectBtn_Click);
			this.MapView.AllowDrop = true;
			this.MapView.AllowLinkCreation = false;
			this.MapView.AllowScrolling = false;
			this.MapView.BackgroundMap = null;
			this.MapView.BorderSize = 0;
			this.MapView.BorderStyle = BorderStyle.FixedSingle;
			this.MapView.Dock = DockStyle.Fill;
			this.MapView.Encounter = null;
			this.MapView.FrameType = MapDisplayType.Dimmed;
			this.MapView.HighlightAreas = false;
			this.MapView.LineOfSight = false;
			this.MapView.Location = new Point(0, 25);
			this.MapView.Map = null;
			this.MapView.Mode = MapViewMode.Thumbnail;
			this.MapView.Name = "MapView";
			this.MapView.ScalingFactor = 1.0;
			this.MapView.SelectedTiles = null;
			this.MapView.Selection = new Rectangle(0, 0, 0, 0);
			this.MapView.ShowCreatureLabels = false;
			this.MapView.ShowCreatures = CreatureViewMode.None;
			this.MapView.ShowGrid = MapGridMode.None;
			this.MapView.ShowHealthBars = false;
			this.MapView.Size = new Size(410, 216);
			this.MapView.TabIndex = 1;
			this.MapView.Tactical = false;
			this.MapView.TokenLinks = null;
			this.MapView.Viewpoint = new Rectangle(0, 0, 0, 0);
			this.MapView.DoubleClick += new EventHandler(this.MapSelectBtn_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.MapView);
			base.Controls.Add(this.Toolbar);
			base.Name = "MapElementPanel";
			base.Size = new Size(410, 241);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
