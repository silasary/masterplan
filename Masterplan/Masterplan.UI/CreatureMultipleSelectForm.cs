using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class CreatureMultipleSelectForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private ListView CreatureList;

		private Button CancelBtn;

		private ColumnHeader NameHdr;

		private ColumnHeader InfoHdr;

		private SplitContainer Splitter;

		private Panel BrowserPanel;

		private WebBrowser Browser;

		private Panel NamePanel;

		private TextBox NameBox;

		private Label NameLbl;

		private Label DragLbl;

		private List<ICreature> fCreatures = new List<ICreature>();

		public List<ICreature> SelectedCreatures
		{
			get
			{
				return this.fCreatures;
			}
		}

		public ICreature SelectedCreature
		{
			get
			{
				if (this.CreatureList.SelectedItems.Count != 0)
				{
					return this.CreatureList.SelectedItems[0].Tag as ICreature;
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
			this.OKBtn = new Button();
			this.CreatureList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.CancelBtn = new Button();
			this.Splitter = new SplitContainer();
			this.NamePanel = new Panel();
			this.NameBox = new TextBox();
			this.NameLbl = new Label();
			this.BrowserPanel = new Panel();
			this.Browser = new WebBrowser();
			this.DragLbl = new Label();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.NamePanel.SuspendLayout();
			this.BrowserPanel.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(549, 354);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.CreatureList.Columns.AddRange(new ColumnHeader[]
			{
				this.NameHdr,
				this.InfoHdr
			});
			this.CreatureList.Dock = DockStyle.Fill;
			this.CreatureList.FullRowSelect = true;
			this.CreatureList.HideSelection = false;
			this.CreatureList.Location = new Point(0, 27);
			this.CreatureList.Name = "CreatureList";
			this.CreatureList.Size = new Size(330, 309);
			this.CreatureList.TabIndex = 0;
			this.CreatureList.UseCompatibleStateImageBehavior = false;
			this.CreatureList.View = View.Details;
			this.CreatureList.SelectedIndexChanged += new EventHandler(this.CreatureList_SelectedIndexChanged);
			this.CreatureList.DoubleClick += new EventHandler(this.TileList_DoubleClick);
			this.CreatureList.ItemDrag += new ItemDragEventHandler(this.CreatureList_ItemDrag);
			this.NameHdr.Text = "Creature";
			this.NameHdr.Width = 150;
			this.InfoHdr.Text = "Info";
			this.InfoHdr.Width = 150;
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(630, 354);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.Splitter.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.CreatureList);
			this.Splitter.Panel1.Controls.Add(this.NamePanel);
			this.Splitter.Panel2.Controls.Add(this.BrowserPanel);
			this.Splitter.Size = new Size(693, 336);
			this.Splitter.SplitterDistance = 330;
			this.Splitter.TabIndex = 0;
			this.NamePanel.Controls.Add(this.NameBox);
			this.NamePanel.Controls.Add(this.NameLbl);
			this.NamePanel.Dock = DockStyle.Top;
			this.NamePanel.Location = new Point(0, 0);
			this.NamePanel.Name = "NamePanel";
			this.NamePanel.Size = new Size(330, 27);
			this.NamePanel.TabIndex = 0;
			this.NameBox.Location = new Point(47, 3);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(280, 20);
			this.NameBox.TabIndex = 1;
			this.NameBox.TextChanged += new EventHandler(this.NameBox_TextChanged);
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(3, 6);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.BrowserPanel.BorderStyle = BorderStyle.FixedSingle;
			this.BrowserPanel.Controls.Add(this.Browser);
			this.BrowserPanel.Controls.Add(this.DragLbl);
			this.BrowserPanel.Dock = DockStyle.Fill;
			this.BrowserPanel.Location = new Point(0, 0);
			this.BrowserPanel.Name = "BrowserPanel";
			this.BrowserPanel.Size = new Size(359, 336);
			this.BrowserPanel.TabIndex = 0;
			this.Browser.AllowWebBrowserDrop = false;
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.IsWebBrowserContextMenuEnabled = false;
			this.Browser.Location = new Point(0, 51);
			this.Browser.MinimumSize = new Size(20, 20);
			this.Browser.Name = "Browser";
			this.Browser.ScriptErrorsSuppressed = true;
			this.Browser.Size = new Size(357, 283);
			this.Browser.TabIndex = 0;
			this.Browser.WebBrowserShortcutsEnabled = false;
			this.Browser.Navigating += new WebBrowserNavigatingEventHandler(this.Browser_Navigating);
			this.DragLbl.AllowDrop = true;
			this.DragLbl.Dock = DockStyle.Top;
			this.DragLbl.Location = new Point(0, 0);
			this.DragLbl.Name = "DragLbl";
			this.DragLbl.Size = new Size(357, 51);
			this.DragLbl.TabIndex = 1;
			this.DragLbl.Text = "Drag creatures here from the list at the left";
			this.DragLbl.TextAlign = ContentAlignment.MiddleCenter;
			this.DragLbl.DragOver += new DragEventHandler(this.DragLbl_DragOver);
			this.DragLbl.DragDrop += new DragEventHandler(this.DragLbl_DragDrop);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(717, 389);
			base.Controls.Add(this.Splitter);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CreatureMultipleSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select Multiple Creatures";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.NamePanel.ResumeLayout(false);
			this.NamePanel.PerformLayout();
			this.BrowserPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public CreatureMultipleSelectForm()
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.update_list();
			this.Browser.DocumentText = "";
			this.update_stats();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = (this.SelectedCreatures.Count >= 2);
		}

		private void TileList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedCreature != null)
			{
				this.fCreatures.Add(this.SelectedCreature);
				this.update_list();
				this.update_stats();
			}
		}

		private void CreatureList_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void update_list()
		{
			this.CreatureList.BeginUpdate();
			this.CreatureList.Groups.Clear();
			this.CreatureList.Items.Clear();
			List<Creature> creatures = Session.Creatures;
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (Creature current in creatures)
			{
				binarySearchTree.Add(current.Category);
			}
			List<string> sortedList = binarySearchTree.SortedList;
			sortedList.Add("Miscellaneous Creatures");
			foreach (string current2 in sortedList)
			{
				this.CreatureList.Groups.Add(current2, current2);
			}
			List<ListViewItem> list = new List<ListViewItem>();
			foreach (Creature current3 in creatures)
			{
				if (this.match(current3, this.NameBox.Text) && !this.fCreatures.Contains(current3))
				{
					ListViewItem listViewItem = new ListViewItem(current3.Name);
					listViewItem.SubItems.Add(current3.Info);
					listViewItem.Tag = current3;
					if (current3.Category != null && current3.Category != "")
					{
						listViewItem.Group = this.CreatureList.Groups[current3.Category];
					}
					else
					{
						listViewItem.Group = this.CreatureList.Groups["Miscellaneous Creatures"];
					}
					list.Add(listViewItem);
				}
			}
			this.CreatureList.Items.AddRange(list.ToArray());
			this.CreatureList.EndUpdate();
		}

		private void update_stats()
		{
			List<string> head = HTML.GetHead("", "", DisplaySize.Small);
			head.Add("<BODY>");
			if (this.fCreatures.Count != 0)
			{
				head.Add("<P class=table>");
				head.Add("<TABLE>");
				head.Add("<TR class=heading>");
				head.Add("<TD colspan=3><B>Selected Creatures</B></TD>");
				head.Add("</TR>");
				foreach (ICreature current in this.fCreatures)
				{
					head.Add("<TR class=header>");
					head.Add("<TD colspan=2>" + current.Name + "</TD>");
					head.Add("<TD align=center><A href=remove:" + current.ID + ">remove</A></TD>");
					head.Add("</TR>");
				}
				head.Add("</TABLE>");
				head.Add("</P>");
			}
			else
			{
				head.Add("<P class=instruction>");
				head.Add("You have not yet selected any creatures; to select a creature, drag it from the list at the left onto the box above");
				head.Add("</P>");
			}
			foreach (ICreature current2 in this.fCreatures)
			{
				EncounterCard encounterCard = new EncounterCard(current2);
				head.Add("<P class=table>");
				head.AddRange(encounterCard.AsText(null, CardMode.View, false));
				head.Add("</P>");
			}
			head.Add("</BODY>");
			head.Add("</HTML>");
			string text = HTML.Concatenate(head);
			this.Browser.Document.OpenNew(true);
			this.Browser.Document.Write(text);
		}

		private void NameBox_TextChanged(object sender, EventArgs e)
		{
			this.update_list();
		}

		private bool match(ICreature creature, string query)
		{
			string[] array = query.ToLower().Split(new char[0]);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string token = array2[i];
				if (!this.match_token(creature, token))
				{
					return false;
				}
			}
			return true;
		}

		private bool match_token(ICreature creature, string token)
		{
			return creature.Name.ToLower().Contains(token) || creature.Info.ToLower().Contains(token);
		}

		private void CreatureList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedCreature != null)
			{
				this.DragLbl.BackColor = SystemColors.Highlight;
				this.DragLbl.ForeColor = SystemColors.HighlightText;
				if (base.DoDragDrop(this.SelectedCreature, DragDropEffects.Move) == DragDropEffects.Move)
				{
					this.fCreatures.Add(this.SelectedCreature);
					this.update_list();
					this.update_stats();
				}
				this.DragLbl.BackColor = SystemColors.Control;
				this.DragLbl.ForeColor = SystemColors.ControlText;
			}
		}

		private void DragLbl_DragOver(object sender, DragEventArgs e)
		{
			if (this.has_creature(e.Data))
			{
				e.Effect = DragDropEffects.Move;
			}
		}

		private void DragLbl_DragDrop(object sender, DragEventArgs e)
		{
			if (this.has_creature(e.Data))
			{
				e.Effect = DragDropEffects.Move;
			}
		}

		private bool has_creature(IDataObject data)
		{
			Creature creature = data.GetData(typeof(Creature)) as Creature;
			if (creature != null)
			{
				return true;
			}
			CustomCreature customCreature = data.GetData(typeof(CustomCreature)) as CustomCreature;
			return customCreature != null || data.GetData(typeof(NPC)) is NPC;
		}

		private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "remove")
			{
				Guid id = new Guid(e.Url.LocalPath);
				ICreature creature = this.find_creature(id);
				if (creature != null)
				{
					e.Cancel = true;
					this.fCreatures.Remove(creature);
					this.update_list();
					this.update_stats();
				}
			}
		}

		private ICreature find_creature(Guid id)
		{
			foreach (ICreature current in this.fCreatures)
			{
				if (current.ID == id)
				{
					return current;
				}
			}
			return null;
		}
	}
}
