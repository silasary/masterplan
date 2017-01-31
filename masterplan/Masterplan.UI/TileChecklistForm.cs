using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class TileChecklistForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private ListView TileList;

		private TreeView PlotTree;

		private SplitContainer Splitter;

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
			this.OKBtn = new Button();
			this.TileList = new ListView();
			this.PlotTree = new TreeView();
			this.Splitter = new SplitContainer();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(458, 376);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.TileList.Dock = DockStyle.Fill;
			this.TileList.FullRowSelect = true;
			this.TileList.HideSelection = false;
			this.TileList.Location = new Point(0, 0);
			this.TileList.MultiSelect = false;
			this.TileList.Name = "TileList";
			this.TileList.Size = new Size(521, 249);
			this.TileList.TabIndex = 1;
			this.TileList.UseCompatibleStateImageBehavior = false;
			this.PlotTree.Dock = DockStyle.Fill;
			this.PlotTree.Location = new Point(0, 0);
			this.PlotTree.Name = "PlotTree";
			this.PlotTree.Size = new Size(521, 105);
			this.PlotTree.TabIndex = 2;
			this.PlotTree.AfterSelect += new TreeViewEventHandler(this.PlotTree_AfterSelect);
			this.Splitter.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Orientation = Orientation.Horizontal;
			this.Splitter.Panel1.Controls.Add(this.PlotTree);
			this.Splitter.Panel2.Controls.Add(this.TileList);
			this.Splitter.Size = new Size(521, 358);
			this.Splitter.SplitterDistance = 105;
			this.Splitter.TabIndex = 3;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(545, 411);
			base.Controls.Add(this.Splitter);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TileChecklistForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Map Tile Checklist";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public TileChecklistForm()
		{
			this.InitializeComponent();
			this.update_tree();
			if (this.PlotTree.Nodes[0].Nodes.Count == 0)
			{
				this.Splitter.Panel1Collapsed = true;
			}
			this.PlotTree.SelectedNode = this.PlotTree.Nodes[0];
		}

		private void PlotTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			Plot plot = e.Node.Tag as Plot;
			if (plot != null)
			{
				this.update_list(plot);
			}
		}

		private void update_tree()
		{
			this.add_navigation_node(null, null);
			this.PlotTree.ExpandAll();
		}

		private void add_navigation_node(PlotPoint pp, TreeNode parent)
		{
			try
			{
				string text = (pp != null) ? pp.Name : Session.Project.Name;
				TreeNodeCollection treeNodeCollection = (parent != null) ? parent.Nodes : this.PlotTree.Nodes;
				TreeNode treeNode = treeNodeCollection.Add(text);
				Plot tag = (pp != null) ? pp.Subplot : Session.Project.Plot;
				treeNode.Tag = tag;
				List<PlotPoint> list = (pp != null) ? pp.Subplot.Points : Session.Project.Plot.Points;
				foreach (PlotPoint current in list)
				{
					if (current.Subplot.Points.Count != 0)
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

		private void update_list(Plot plot)
		{
			List<Map> list = new List<Map>();
			List<PlotPoint> allPlotPoints = plot.AllPlotPoints;
			List<Guid> list2 = new List<Guid>();
			foreach (PlotPoint current in allPlotPoints)
			{
				Encounter encounter = current.Element as Encounter;
				if (encounter != null && encounter.MapID != Guid.Empty)
				{
					list2.Add(encounter.MapID);
				}
				MapElement mapElement = current.Element as MapElement;
				if (mapElement != null)
				{
					list2.Add(mapElement.MapID);
				}
			}
			foreach (Guid current2 in list2)
			{
				Map map = Session.Project.FindTacticalMap(current2);
				if (map != null)
				{
					list.Add(map);
				}
			}
			Dictionary<Guid, int> dictionary = new Dictionary<Guid, int>();
			foreach (Map current3 in list)
			{
				Dictionary<Guid, int> dictionary2 = new Dictionary<Guid, int>();
				foreach (TileData current4 in current3.Tiles)
				{
					if (!dictionary2.ContainsKey(current4.TileID))
					{
						dictionary2[current4.TileID] = 0;
					}
					Dictionary<Guid, int> dictionary3;
					Guid tileID;
					(dictionary3 = dictionary2)[tileID = current4.TileID] = dictionary3[tileID] + 1;
				}
				foreach (Guid current5 in dictionary2.Keys)
				{
					if (!dictionary.ContainsKey(current5))
					{
						dictionary[current5] = 0;
					}
					if (dictionary2[current5] > dictionary[current5])
					{
						dictionary[current5] = dictionary2[current5];
					}
				}
			}
			List<string> list3 = new List<string>();
			foreach (Guid current6 in dictionary.Keys)
			{
				Tile t = Session.FindTile(current6, SearchType.Global);
				Library library = Session.FindLibrary(t);
				if (!list3.Contains(library.Name))
				{
					list3.Add(library.Name);
				}
			}
			list3.Sort();
			this.TileList.Groups.Clear();
			foreach (string current7 in list3)
			{
				this.TileList.Groups.Add(current7, current7);
			}
			this.TileList.LargeImageList = new ImageList();
			this.TileList.LargeImageList.ImageSize = new Size(64, 64);
			this.TileList.Items.Clear();
			foreach (Guid current8 in dictionary.Keys)
			{
				Tile tile = Session.FindTile(current8, SearchType.Global);
				Library library2 = Session.FindLibrary(tile);
				ListViewItem listViewItem = this.TileList.Items.Add("x " + dictionary[current8]);
				listViewItem.Tag = tile;
				listViewItem.Group = this.TileList.Groups[library2.Name];
				Image image = (tile.Image != null) ? tile.Image : tile.BlankImage;
				Bitmap bitmap = new Bitmap(64, 64);
				if (tile.Size.Width > tile.Size.Height)
				{
					int num = tile.Size.Height * 64 / tile.Size.Width;
					Rectangle rect = new Rectangle(0, (64 - num) / 2, 64, num);
					Graphics graphics = Graphics.FromImage(bitmap);
					graphics.DrawImage(image, rect);
				}
				else
				{
					int num2 = tile.Size.Width * 64 / tile.Size.Height;
					Rectangle rect2 = new Rectangle((64 - num2) / 2, 0, num2, 64);
					Graphics graphics2 = Graphics.FromImage(bitmap);
					graphics2.DrawImage(image, rect2);
				}
				this.TileList.LargeImageList.Images.Add(bitmap);
				listViewItem.ImageIndex = this.TileList.LargeImageList.Images.Count - 1;
			}
		}
	}
}
