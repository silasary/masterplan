using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class MiniChecklistForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private ListView TileList;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private SplitContainer Splitter;

		private TreeView PlotTree;

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
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.Splitter = new SplitContainer();
			this.PlotTree = new TreeView();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(454, 324);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.TileList.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2,
				this.columnHeader3
			});
			this.TileList.Dock = DockStyle.Fill;
			this.TileList.FullRowSelect = true;
			this.TileList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TileList.HideSelection = false;
			this.TileList.Location = new Point(0, 0);
			this.TileList.MultiSelect = false;
			this.TileList.Name = "TileList";
			this.TileList.Size = new Size(517, 213);
			this.TileList.TabIndex = 1;
			this.TileList.UseCompatibleStateImageBehavior = false;
			this.TileList.View = View.Details;
			this.columnHeader1.Text = "Creature";
			this.columnHeader1.Width = 148;
			this.columnHeader2.Text = "Info";
			this.columnHeader2.Width = 280;
			this.columnHeader3.Text = "Count";
			this.columnHeader3.TextAlign = HorizontalAlignment.Right;
			this.Splitter.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Orientation = Orientation.Horizontal;
			this.Splitter.Panel1.Controls.Add(this.PlotTree);
			this.Splitter.Panel2.Controls.Add(this.TileList);
			this.Splitter.Size = new Size(517, 306);
			this.Splitter.SplitterDistance = 89;
			this.Splitter.TabIndex = 2;
			this.PlotTree.Dock = DockStyle.Fill;
			this.PlotTree.Location = new Point(0, 0);
			this.PlotTree.Name = "PlotTree";
			this.PlotTree.Size = new Size(517, 89);
			this.PlotTree.TabIndex = 0;
			this.PlotTree.AfterSelect += new TreeViewEventHandler(this.PlotTree_AfterSelect);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(541, 359);
			base.Controls.Add(this.Splitter);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MiniChecklistForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Miniature Checklist";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public MiniChecklistForm()
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
			List<Encounter> list = new List<Encounter>();
			List<PlotPoint> allPlotPoints = plot.AllPlotPoints;
			foreach (PlotPoint current in allPlotPoints)
			{
				Encounter encounter = current.Element as Encounter;
				if (encounter != null)
				{
					list.Add(encounter);
				}
			}
			Dictionary<Guid, int> dictionary = new Dictionary<Guid, int>();
			foreach (Encounter current2 in list)
			{
				Dictionary<Guid, int> dictionary2 = new Dictionary<Guid, int>();
				foreach (EncounterSlot current3 in current2.Slots)
				{
					if (!dictionary2.ContainsKey(current3.Card.CreatureID))
					{
						dictionary2[current3.Card.CreatureID] = 0;
					}
					Dictionary<Guid, int> dictionary3;
					Guid creatureID;
					(dictionary3 = dictionary2)[creatureID = current3.Card.CreatureID] = dictionary3[creatureID] + current3.CombatData.Count;
				}
				foreach (Guid current4 in dictionary2.Keys)
				{
					if (!dictionary.ContainsKey(current4))
					{
						dictionary[current4] = 0;
					}
					if (dictionary2[current4] > dictionary[current4])
					{
						dictionary[current4] = dictionary2[current4];
					}
				}
			}
			this.TileList.Items.Clear();
			foreach (Guid current5 in dictionary.Keys)
			{
				ICreature creature = Session.FindCreature(current5, SearchType.Global);
				int num = dictionary[current5];
				if (creature != null)
				{
					ListViewItem listViewItem = this.TileList.Items.Add(creature.Name);
					string text = creature.Size + " size";
					if (creature.Keywords != "")
					{
						text = text + ", " + creature.Keywords;
					}
					foreach (CreaturePower current6 in creature.CreaturePowers)
					{
						text = text + ", " + current6.Name;
					}
					listViewItem.SubItems.Add(text);
					if (num > 1)
					{
						listViewItem.SubItems.Add("x" + num);
					}
					else
					{
						listViewItem.SubItems.Add("");
					}
				}
			}
		}
	}
}
