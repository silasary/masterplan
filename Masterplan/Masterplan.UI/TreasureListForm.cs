using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TreasureListForm : Form
	{
		private Plot fRootPlot;

		private IContainer components;

		private Button CancelBtn;

		private SplitContainer Splitter;

		private TreeView PlotTree;

		private ListView ItemList;

		private ColumnHeader ItemHdr;

		private ToolStrip Toolbar;

		private ToolStripButton SelectAll;

		private ToolStripButton SelectNone;

		private Label InfoLbl;

		private Label PagesLbl;

		private Button ExportBtn;

		public Plot SelectedPlot
		{
			get
			{
				if (this.PlotTree.SelectedNode != null)
				{
					return this.PlotTree.SelectedNode.Tag as Plot;
				}
				return null;
			}
		}

		public MagicItem SelectedMagicItem
		{
			get
			{
				if (this.ItemList.SelectedItems.Count != 0)
				{
					return this.ItemList.SelectedItems[0].Tag as MagicItem;
				}
				return null;
			}
		}

		public TreasureListForm(Plot plot)
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.fRootPlot = plot;
			this.update_plot_tree();
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.SelectAll.Enabled = (this.ItemList.Items.Count != 0);
			this.SelectNone.Enabled = (this.ItemList.Items.Count != 0);
			this.ExportBtn.Enabled = (this.ItemList.CheckedItems.Count != 0);
			this.PagesLbl.Visible = (this.ItemList.CheckedItems.Count > 9);
		}

		private void PlotTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			this.update_list();
		}

		private void ItemList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedMagicItem != null)
			{
				MagicItemDetailsForm magicItemDetailsForm = new MagicItemDetailsForm(this.SelectedMagicItem);
				magicItemDetailsForm.ShowDialog();
			}
		}

		private void update_plot_tree()
		{
			this.PlotTree.Nodes.Clear();
			int num = this.add_nodes(this.PlotTree.Nodes, this.fRootPlot);
			this.PlotTree.ExpandAll();
			this.PlotTree.SelectedNode = this.PlotTree.Nodes[0];
			this.Splitter.Panel1Collapsed = (num == 1);
		}

		private int add_nodes(TreeNodeCollection tnc, Plot p)
		{
			int num = 1;
			PlotPoint plotPoint = Session.Project.FindParent(p);
			string text = (plotPoint != null) ? plotPoint.Name : Session.Project.Name;
			TreeNode treeNode = tnc.Add(text);
			treeNode.Tag = p;
			foreach (PlotPoint current in p.Points)
			{
				if (current.Subplot.Points.Count != 0)
				{
					num += this.add_nodes(treeNode.Nodes, current.Subplot);
				}
			}
			return num;
		}

		private void update_list()
		{
			List<MagicItem> list = new List<MagicItem>();
			List<PlotPoint> list2 = this.get_points(this.SelectedPlot);
			foreach (PlotPoint current in list2)
			{
				foreach (Parcel current2 in current.Parcels)
				{
					if (!(current2.MagicItemID == Guid.Empty))
					{
						MagicItem magicItem = Session.FindMagicItem(current2.MagicItemID, SearchType.Global);
						if (magicItem != null && !list.Contains(magicItem))
						{
							list.Add(magicItem);
						}
					}
				}
			}
			list.Sort();
			this.ItemList.Items.Clear();
			foreach (MagicItem current3 in list)
			{
				ListViewItem listViewItem = this.ItemList.Items.Add(current3.Name);
				listViewItem.Tag = current3;
			}
		}

		private List<PlotPoint> get_points(Plot p)
		{
			List<PlotPoint> list = new List<PlotPoint>();
			list.AddRange(p.Points);
			foreach (PlotPoint current in p.Points)
			{
				list.AddRange(this.get_points(current.Subplot));
			}
			return list;
		}

		private void SelectAll_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem listViewItem in this.ItemList.Items)
			{
				listViewItem.Checked = true;
			}
		}

		private void SelectNone_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem listViewItem in this.ItemList.Items)
			{
				listViewItem.Checked = false;
			}
		}

		private void ExportBtn_Click(object sender, EventArgs e)
		{
			base.Close();
			int num = this.ItemList.CheckedItems.Count / 9;
			int num2 = this.ItemList.CheckedItems.Count % 9;
			if (num2 > 0)
			{
				num++;
			}
			for (int num3 = 0; num3 != num; num3++)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Filter = Program.HTMLFilter;
				saveFileDialog.FileName = Session.Project.Name + " Treasure";
				saveFileDialog.Title = "Export";
				if (num != 1)
				{
					SaveFileDialog expr_78 = saveFileDialog;
					object title = expr_78.Title;
					expr_78.Title = string.Concat(new object[]
					{
						title,
						" (page ",
						num3 + 1,
						")"
					});
				}
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					List<string> head = HTML.GetHead("Loot", "", DisplaySize.Small);
					head.Add("<BODY>");
					head.Add("<P>");
					head.Add("<TABLE class=clear height=100%>");
					for (int num4 = 0; num4 != 3; num4++)
					{
						head.Add("<TR class=clear width=33% height=33%>");
						for (int num5 = 0; num5 != 3; num5++)
						{
							head.Add("<TD width=33% height=33%>");
							int num6 = num3 * 9 + num4 * 3 + num5;
							if (this.ItemList.CheckedItems.Count > num6)
							{
								MagicItem magicItem = this.ItemList.CheckedItems[num6].Tag as MagicItem;
								if (magicItem != null)
								{
									head.Add(HTML.MagicItem(magicItem, DisplaySize.Small, false, false));
								}
							}
							head.Add("</TD>");
						}
						head.Add("</TR>");
					}
					head.Add("</TABLE>");
					head.Add("</P>");
					head.Add("</BODY>");
					head.Add("</HTML>");
					string contents = HTML.Concatenate(head);
					File.WriteAllText(saveFileDialog.FileName, contents);
				}
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(TreasureListForm));
			this.CancelBtn = new Button();
			this.Splitter = new SplitContainer();
			this.PlotTree = new TreeView();
			this.InfoLbl = new Label();
			this.ItemList = new ListView();
			this.ItemHdr = new ColumnHeader();
			this.PagesLbl = new Label();
			this.Toolbar = new ToolStrip();
			this.SelectAll = new ToolStripButton();
			this.SelectNone = new ToolStripButton();
			this.ExportBtn = new Button();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.OK;
			this.CancelBtn.Location = new Point(422, 396);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.Splitter.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.PlotTree);
			this.Splitter.Panel1.Controls.Add(this.InfoLbl);
			this.Splitter.Panel2.Controls.Add(this.ItemList);
			this.Splitter.Panel2.Controls.Add(this.PagesLbl);
			this.Splitter.Panel2.Controls.Add(this.Toolbar);
			this.Splitter.Size = new Size(485, 378);
			this.Splitter.SplitterDistance = 231;
			this.Splitter.TabIndex = 0;
			this.PlotTree.Dock = DockStyle.Fill;
			this.PlotTree.HideSelection = false;
			this.PlotTree.Location = new Point(0, 38);
			this.PlotTree.Name = "PlotTree";
			this.PlotTree.Size = new Size(231, 340);
			this.PlotTree.TabIndex = 0;
			this.PlotTree.AfterSelect += new TreeViewEventHandler(this.PlotTree_AfterSelect);
			this.InfoLbl.Dock = DockStyle.Top;
			this.InfoLbl.Location = new Point(0, 0);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(231, 38);
			this.InfoLbl.TabIndex = 1;
			this.InfoLbl.Text = "Select a plot point here to see treasure parcels from that subplot";
			this.InfoLbl.TextAlign = ContentAlignment.MiddleLeft;
			this.ItemList.CheckBoxes = true;
			this.ItemList.Columns.AddRange(new ColumnHeader[]
			{
				this.ItemHdr
			});
			this.ItemList.Dock = DockStyle.Fill;
			this.ItemList.FullRowSelect = true;
			this.ItemList.Location = new Point(0, 25);
			this.ItemList.Name = "ItemList";
			this.ItemList.Size = new Size(250, 337);
			this.ItemList.TabIndex = 0;
			this.ItemList.UseCompatibleStateImageBehavior = false;
			this.ItemList.View = View.Details;
			this.ItemList.DoubleClick += new EventHandler(this.ItemList_DoubleClick);
			this.ItemHdr.Text = "Parcels";
			this.ItemHdr.Width = 220;
			this.PagesLbl.Dock = DockStyle.Bottom;
			this.PagesLbl.Location = new Point(0, 362);
			this.PagesLbl.Name = "PagesLbl";
			this.PagesLbl.Size = new Size(250, 16);
			this.PagesLbl.TabIndex = 2;
			this.PagesLbl.Text = "Note that this will require multiple pages";
			this.PagesLbl.TextAlign = ContentAlignment.MiddleLeft;
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.SelectAll,
				this.SelectNone
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(250, 25);
			this.Toolbar.TabIndex = 1;
			this.Toolbar.Text = "toolStrip1";
			this.SelectAll.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SelectAll.Image = (Image)resources.GetObject("SelectAll.Image");
			this.SelectAll.ImageTransparentColor = Color.Magenta;
			this.SelectAll.Name = "SelectAll";
			this.SelectAll.Size = new Size(59, 22);
			this.SelectAll.Text = "Select All";
			this.SelectAll.Click += new EventHandler(this.SelectAll_Click);
			this.SelectNone.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SelectNone.Image = (Image)resources.GetObject("SelectNone.Image");
			this.SelectNone.ImageTransparentColor = Color.Magenta;
			this.SelectNone.Name = "SelectNone";
			this.SelectNone.Size = new Size(74, 22);
			this.SelectNone.Text = "Select None";
			this.SelectNone.Click += new EventHandler(this.SelectNone_Click);
			this.ExportBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.ExportBtn.DialogResult = DialogResult.OK;
			this.ExportBtn.Location = new Point(341, 396);
			this.ExportBtn.Name = "ExportBtn";
			this.ExportBtn.Size = new Size(75, 23);
			this.ExportBtn.TabIndex = 1;
			this.ExportBtn.Text = "Export";
			this.ExportBtn.UseVisualStyleBackColor = true;
			this.ExportBtn.Click += new EventHandler(this.ExportBtn_Click);
			base.AcceptButton = this.ExportBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(509, 431);
			base.Controls.Add(this.ExportBtn);
			base.Controls.Add(this.Splitter);
			base.Controls.Add(this.CancelBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TreasureListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Treasure List";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.Panel2.PerformLayout();
			this.Splitter.ResumeLayout(false);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
