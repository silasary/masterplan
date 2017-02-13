using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class RegionalMapForm : Form
	{
		private RegionalMap fMap;

		private PointF fRightClickLocation = PointF.Empty;

		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private Panel Panel;

		private RegionalMapPanel MapPanel;

		private ToolStrip Toolbar;

		private ToolStripButton BrowseBtn;

		private Button OKBtn;

		private Button CancelBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private ContextMenuStrip MapContext;

		private ToolStripMenuItem MapContextAddLocation;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem MapContextRemove;

		private ToolStripMenuItem MapContextEdit;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton EntryBtn;

		private ToolStripButton PasteBtn;

		public RegionalMap Map
		{
			get
			{
				return this.fMap;
			}
		}

		public RegionalMapForm(RegionalMap map, MapLocation loc)
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.fMap = map.Copy();
			this.NameBox.Text = this.fMap.Name;
			this.MapPanel.Map = this.fMap;
			if (loc != null)
			{
				this.NameBox.Enabled = false;
				this.Toolbar.Visible = false;
				this.OKBtn.Visible = false;
				this.CancelBtn.Text = "Close";
				this.MapPanel.Mode = MapViewMode.Plain;
				this.MapPanel.HighlightedLocation = loc;
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.PasteBtn.Enabled = Clipboard.ContainsImage();
			this.RemoveBtn.Enabled = (this.MapPanel.SelectedLocation != null);
			this.EditBtn.Enabled = (this.MapPanel.SelectedLocation != null);
			this.EntryBtn.Enabled = (this.MapPanel.SelectedLocation != null);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fMap.Name = this.NameBox.Text;
		}

		private void BrowseBtn_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = Program.ImageFilter;
			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			this.MapPanel.Map.Image = Image.FromFile(openFileDialog.FileName);
			Program.SetResolution(this.MapPanel.Map.Image);
			this.MapPanel.Invalidate();
		}

		private void PasteBtn_Click(object sender, EventArgs e)
		{
			if (Clipboard.ContainsImage())
			{
				this.MapPanel.Map.Image = Clipboard.GetImage();
				Program.SetResolution(this.MapPanel.Map.Image);
				this.MapPanel.Invalidate();
			}
		}

		private void MapContext_Opening(object sender, CancelEventArgs e)
		{
			this.set_click_location();
			this.MapContextAddLocation.Enabled = (this.fRightClickLocation != PointF.Empty);
			this.MapContextRemove.Enabled = (this.MapPanel.SelectedLocation != null);
			this.MapContextEdit.Enabled = (this.MapPanel.SelectedLocation != null);
		}

		private void MapContextAddLocation_Click(object sender, EventArgs e)
		{
			if (this.fRightClickLocation == PointF.Empty)
			{
				return;
			}
			MapLocation mapLocation = new MapLocation();
			mapLocation.Name = "New Location";
			mapLocation.Point = this.fRightClickLocation;
			MapLocationForm mapLocationForm = new MapLocationForm(mapLocation);
			if (mapLocationForm.ShowDialog() == DialogResult.OK)
			{
				this.fMap.Locations.Add(mapLocation);
				this.MapPanel.Invalidate();
				Session.Modified = true;
			}
		}

		private void MapContextRemove_Click(object sender, EventArgs e)
		{
			if (this.MapPanel.SelectedLocation != null)
			{
				this.MapPanel.Map.Locations.Remove(this.MapPanel.SelectedLocation);
				this.MapPanel.Invalidate();
				Session.Modified = true;
			}
		}

		private void MapContextEdit_Click(object sender, EventArgs e)
		{
			if (this.MapPanel.SelectedLocation != null)
			{
				int index = this.fMap.Locations.IndexOf(this.MapPanel.SelectedLocation);
				MapLocationForm mapLocationForm = new MapLocationForm(this.MapPanel.SelectedLocation);
				if (mapLocationForm.ShowDialog() == DialogResult.OK)
				{
					this.fMap.Locations[index] = mapLocationForm.MapLocation;
					this.MapPanel.Invalidate();
					Session.Modified = true;
				}
			}
		}

		private void MapPanel_DoubleClick(object sender, EventArgs e)
		{
			if (this.MapPanel.SelectedLocation == null)
			{
				this.set_click_location();
				this.MapContextAddLocation_Click(sender, e);
				return;
			}
			this.MapContextEdit_Click(sender, e);
		}

		private void set_click_location()
		{
			this.fRightClickLocation = PointF.Empty;
			Point p = this.MapPanel.PointToClient(Cursor.Position);
			RectangleF mapRectangle = this.MapPanel.MapRectangle;
			if (mapRectangle.Contains(p))
			{
				float x = ((float)p.X - mapRectangle.X) / mapRectangle.Width;
				float y = ((float)p.Y - mapRectangle.Y) / mapRectangle.Height;
				this.fRightClickLocation = new PointF(x, y);
			}
		}

		private void EntryBtn_Click(object sender, EventArgs e)
		{
			if (this.MapPanel.SelectedLocation == null)
			{
				return;
			}
			EncyclopediaEntry encyclopediaEntry = Session.Project.Encyclopedia.FindEntryForAttachment(this.MapPanel.SelectedLocation.ID);
			if (encyclopediaEntry == null)
			{
				string text = "There is no encyclopedia entry associated with this location.";
				text += Environment.NewLine;
				text += "Would you like to create one now?";
				if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					return;
				}
				encyclopediaEntry = new EncyclopediaEntry();
				encyclopediaEntry.Name = this.MapPanel.SelectedLocation.Name;
				encyclopediaEntry.AttachmentID = this.MapPanel.SelectedLocation.ID;
				encyclopediaEntry.Category = this.MapPanel.SelectedLocation.Category;
				if (encyclopediaEntry.Category == "")
				{
					encyclopediaEntry.Category = "Places";
				}
				Session.Project.Encyclopedia.Entries.Add(encyclopediaEntry);
				Session.Modified = true;
			}
			int index = Session.Project.Encyclopedia.Entries.IndexOf(encyclopediaEntry);
			EncyclopediaEntryForm encyclopediaEntryForm = new EncyclopediaEntryForm(encyclopediaEntry);
			if (encyclopediaEntryForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.Encyclopedia.Entries[index] = encyclopediaEntryForm.Entry;
				Session.Modified = true;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(RegionalMapForm));
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.Panel = new Panel();
			this.MapPanel = new RegionalMapPanel();
			this.MapContext = new ContextMenuStrip(this.components);
			this.MapContextAddLocation = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.MapContextRemove = new ToolStripMenuItem();
			this.MapContextEdit = new ToolStripMenuItem();
			this.Toolbar = new ToolStrip();
			this.BrowseBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.EntryBtn = new ToolStripButton();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.PasteBtn = new ToolStripButton();
			this.Panel.SuspendLayout();
			this.MapContext.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(62, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Map Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(80, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(515, 20);
			this.NameBox.TabIndex = 1;
			this.Panel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Panel.BorderStyle = BorderStyle.FixedSingle;
			this.Panel.Controls.Add(this.MapPanel);
			this.Panel.Controls.Add(this.Toolbar);
			this.Panel.Location = new Point(12, 38);
			this.Panel.Name = "Panel";
			this.Panel.Size = new Size(583, 352);
			this.Panel.TabIndex = 2;
			this.MapPanel.AllowEditing = true;
			this.MapPanel.ContextMenuStrip = this.MapContext;
			this.MapPanel.Dock = DockStyle.Fill;
			this.MapPanel.HighlightedLocation = null;
			this.MapPanel.Location = new Point(0, 25);
			this.MapPanel.Map = null;
			this.MapPanel.Mode = MapViewMode.Normal;
			this.MapPanel.Name = "MapPanel";
			this.MapPanel.Plot = null;
			this.MapPanel.SelectedLocation = null;
			this.MapPanel.ShowLocations = true;
			this.MapPanel.Size = new Size(581, 325);
			this.MapPanel.TabIndex = 1;
			this.MapPanel.DoubleClick += new EventHandler(this.MapPanel_DoubleClick);
			this.MapContext.Items.AddRange(new ToolStripItem[]
			{
				this.MapContextAddLocation,
				this.toolStripSeparator2,
				this.MapContextRemove,
				this.MapContextEdit
			});
			this.MapContext.Name = "MapContext";
			this.MapContext.Size = new Size(183, 76);
			this.MapContext.Opening += new CancelEventHandler(this.MapContext_Opening);
			this.MapContextAddLocation.Name = "MapContextAddLocation";
			this.MapContextAddLocation.Size = new Size(182, 22);
			this.MapContextAddLocation.Text = "Add Location Here...";
			this.MapContextAddLocation.Click += new EventHandler(this.MapContextAddLocation_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(179, 6);
			this.MapContextRemove.Name = "MapContextRemove";
			this.MapContextRemove.Size = new Size(182, 22);
			this.MapContextRemove.Text = "Remove Location";
			this.MapContextRemove.Click += new EventHandler(this.MapContextRemove_Click);
			this.MapContextEdit.Name = "MapContextEdit";
			this.MapContextEdit.Size = new Size(182, 22);
			this.MapContextEdit.Text = "Edit Location";
			this.MapContextEdit.Click += new EventHandler(this.MapContextEdit_Click);
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.BrowseBtn,
				this.PasteBtn,
				this.toolStripSeparator1,
				this.RemoveBtn,
				this.EditBtn,
				this.toolStripSeparator3,
				this.EntryBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(581, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.BrowseBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.BrowseBtn.Image = (Image)resources.GetObject("BrowseBtn.Image");
			this.BrowseBtn.ImageTransparentColor = Color.Magenta;
			this.BrowseBtn.Name = "BrowseBtn";
			this.BrowseBtn.Size = new Size(105, 22);
			this.BrowseBtn.Text = "Select Map Image";
			this.BrowseBtn.Click += new EventHandler(this.BrowseBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)resources.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new Size(103, 22);
			this.RemoveBtn.Text = "Remove Location";
			this.RemoveBtn.Click += new EventHandler(this.MapContextRemove_Click);
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)resources.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new Size(80, 22);
			this.EditBtn.Text = "Edit Location";
			this.EditBtn.Click += new EventHandler(this.MapContextEdit_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new Size(6, 25);
			this.EntryBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EntryBtn.Image = (Image)resources.GetObject("EntryBtn.Image");
			this.EntryBtn.ImageTransparentColor = Color.Magenta;
			this.EntryBtn.Name = "EntryBtn";
			this.EntryBtn.Size = new Size(111, 22);
			this.EntryBtn.Text = "Encyclopedia Entry";
			this.EntryBtn.Click += new EventHandler(this.EntryBtn_Click);
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(439, 396);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(520, 396);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.PasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PasteBtn.Image = (Image)resources.GetObject("PasteBtn.Image");
			this.PasteBtn.ImageTransparentColor = Color.Magenta;
			this.PasteBtn.Name = "PasteBtn";
			this.PasteBtn.Size = new Size(102, 22);
			this.PasteBtn.Text = "Paste Map Image";
			this.PasteBtn.Click += new EventHandler(this.PasteBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(607, 431);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Panel);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MinimizeBox = false;
			base.Name = "RegionalMapForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Regional Map";
			this.Panel.ResumeLayout(false);
			this.Panel.PerformLayout();
			this.MapContext.ResumeLayout(false);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
