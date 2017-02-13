using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Tools.Generators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class DeckBuilderForm : Form
	{
		private EncounterDeck fDeck;

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label NameLbl;

		private TextBox NameBox;

		private Label LevelLbl;

		private NumericUpDown LevelBox;

		private Panel panel1;

		private SplitContainer Splitter;

		private SplitContainer DeckSplitter;

		private DeckGrid DeckView;

		private ToolStrip DeckToolbar;

		private ListView CardList;

		private ListView CreatureList;

		private Button AutoBuildBtn;

		private ColumnHeader CardHdr;

		private ColumnHeader CardInfoHdr;

		private ColumnHeader CreatureHdr;

		private Panel PropertiesPanel;

		private Label InfoLbl;

		private ToolStripButton DuplicateBtn;

		private ToolStripButton RemoveBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton ViewBtn;

		private Button RefillBtn;

		private ToolStripButton RefreshBtn;

		private ToolStripSeparator toolStripSeparator2;

		public EncounterDeck Deck
		{
			get
			{
				return this.fDeck;
			}
		}

		public EncounterCard SelectedCard
		{
			get
			{
				if (this.CardList.SelectedItems.Count != 0)
				{
					return this.CardList.SelectedItems[0].Tag as EncounterCard;
				}
				return null;
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

		public DeckBuilderForm(EncounterDeck deck)
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.fDeck = deck.Copy();
			this.NameBox.Text = this.fDeck.Name;
			this.LevelBox.Value = this.fDeck.Level;
			this.DeckView.Deck = this.fDeck;
			this.update_groups();
			this.DeckView_SelectedCellChanged(null, null);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.DuplicateBtn.Enabled = (this.SelectedCard != null);
			this.RemoveBtn.Enabled = (this.SelectedCard != null);
			this.RefreshBtn.Enabled = (this.SelectedCard != null && this.SelectedCard.Drawn);
			bool enabled = false;
			foreach (EncounterCard current in this.fDeck.Cards)
			{
				if (current.Drawn)
				{
					enabled = true;
					break;
				}
			}
			this.RefillBtn.Enabled = enabled;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fDeck.Name = this.NameBox.Text;
		}

		private void DuplicateBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCard != null)
			{
				EncounterCard item = this.SelectedCard.Copy();
				this.fDeck.Cards.Add(item);
				this.DeckView.Invalidate();
				this.update_card_list();
			}
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCard != null)
			{
				if (this.fDeck.Cards.Contains(this.SelectedCard))
				{
					this.fDeck.Cards.Remove(this.SelectedCard);
				}
				this.DeckView.Invalidate();
				this.update_card_list();
			}
		}

		private void RefreshBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCard != null && this.SelectedCard.Drawn)
			{
				this.SelectedCard.Drawn = false;
				this.DeckView.Invalidate();
				this.update_card_list();
			}
		}

		private void ViewBtn_Click(object sender, EventArgs e)
		{
			List<EncounterCard> list = new List<EncounterCard>();
			foreach (EncounterCard current in this.fDeck.Cards)
			{
				if (this.DeckView.InSelectedCell(current))
				{
					list.Add(current);
				}
			}
			if (list.Count != 0)
			{
				DeckViewForm deckViewForm = new DeckViewForm(list);
				deckViewForm.ShowDialog();
			}
		}

		private void AutoBuildBtn_Click(object sender, EventArgs e)
		{
			AutoBuildForm autoBuildForm = new AutoBuildForm(AutoBuildForm.Mode.Deck);
			if (autoBuildForm.ShowDialog() == DialogResult.OK)
			{
				EncounterDeck encounterDeck = EncounterBuilder.BuildDeck(autoBuildForm.Data.Level, autoBuildForm.Data.Categories, autoBuildForm.Data.Keywords);
				if (encounterDeck != null)
				{
					this.fDeck = encounterDeck;
					this.LevelBox.Value = this.fDeck.Level;
					this.DeckView.Deck = this.fDeck;
					this.DeckView_SelectedCellChanged(null, null);
				}
			}
		}

		private void RefillBtn_Click(object sender, EventArgs e)
		{
			foreach (EncounterCard current in this.fDeck.Cards)
			{
				current.Drawn = false;
			}
			this.DeckView.Invalidate();
			this.update_card_list();
		}

		private void CreatureList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedCreature != null)
			{
				CreatureDetailsForm creatureDetailsForm = new CreatureDetailsForm(new EncounterCard
				{
					CreatureID = this.SelectedCreature.ID
				});
				creatureDetailsForm.ShowDialog();
			}
		}

		private void CardList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedCard != null)
			{
				CreatureDetailsForm creatureDetailsForm = new CreatureDetailsForm(this.SelectedCard);
				creatureDetailsForm.ShowDialog();
			}
		}

		private void CreatureList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedCreature != null && base.DoDragDrop(this.SelectedCreature, DragDropEffects.Copy) == DragDropEffects.Copy)
			{
				EncounterCard encounterCard = new EncounterCard();
				encounterCard.CreatureID = this.SelectedCreature.ID;
				this.fDeck.Cards.Add(encounterCard);
				this.DeckView.Invalidate();
				this.update_card_list();
			}
		}

		private void DeckView_DragOver(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.None;
			Creature creature = e.Data.GetData(typeof(Creature)) as Creature;
			if (creature != null)
			{
				e.Effect = DragDropEffects.Copy;
			}
			CustomCreature customCreature = e.Data.GetData(typeof(CustomCreature)) as CustomCreature;
			if (customCreature != null)
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void DeckView_DragDrop(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.None;
			Creature creature = e.Data.GetData(typeof(Creature)) as Creature;
			if (creature != null)
			{
				e.Effect = DragDropEffects.Copy;
			}
			CustomCreature customCreature = e.Data.GetData(typeof(CustomCreature)) as CustomCreature;
			if (customCreature != null)
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void LevelBox_ValueChanged(object sender, EventArgs e)
		{
			this.fDeck.Level = (int)this.LevelBox.Value;
			this.DeckView.Invalidate();
		}

		private void DeckView_SelectedCellChanged(object sender, EventArgs e)
		{
			if (this.DeckView.IsCellSelected)
			{
				this.InfoLbl.Text = "Drag creatures from this list onto the grid to the left to add them into your deck.";
			}
			else
			{
				this.InfoLbl.Text = "Select a cell on the grid to display the list of creatures that can be added to the deck.";
			}
			Cursor.Current = Cursors.WaitCursor;
			this.update_card_list();
			this.update_creature_list();
			Cursor.Current = Cursors.Default;
		}

		private void DeckView_CellActivated(object sender, EventArgs e)
		{
			this.ViewBtn_Click(null, null);
		}

		private void update_groups()
		{
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (Creature current in Session.Creatures)
			{
				if (current.Category != "")
				{
					binarySearchTree.Add(current.Category);
				}
			}
			List<string> sortedList = binarySearchTree.SortedList;
			sortedList.Insert(0, "Custom Creatures");
			sortedList.Add("Miscellaneous Creatures");
			foreach (string current2 in sortedList)
			{
				this.CardList.Groups.Add(current2, current2);
				this.CreatureList.Groups.Add(current2, current2);
			}
		}

		private void update_card_list()
		{
			this.CardList.BeginUpdate();
			this.CardList.ShowGroups = true;
			List<ListViewItem> list = new List<ListViewItem>();
			if (this.DeckView.IsCellSelected)
			{
				foreach (EncounterCard current in this.fDeck.Cards)
				{
					if (this.DeckView.InSelectedCell(current))
					{
						list.Add(this.add_card(current));
					}
				}
				if (list.Count == 0)
				{
					this.CardList.ShowGroups = false;
					list.Add(new ListViewItem("(no cards)")
					{
						ForeColor = SystemColors.GrayText
					});
				}
			}
			else
			{
				this.CardList.ShowGroups = false;
				list.Add(new ListViewItem("(no cell selected)")
				{
					ForeColor = SystemColors.GrayText
				});
			}
			this.CardList.Items.Clear();
			this.CardList.Items.AddRange(list.ToArray());
			this.CardList.EndUpdate();
			this.ViewBtn.Enabled = (list.Count != 0);
		}

		private ListViewItem add_card(EncounterCard card)
		{
			ListViewItem listViewItem = new ListViewItem(card.Title);
			listViewItem.SubItems.Add(card.Info);
			listViewItem.Tag = card;
			if (card.Drawn)
			{
				listViewItem.ForeColor = SystemColors.GrayText;
			}
			ICreature creature = Session.FindCreature(card.CreatureID, SearchType.Global);
			string key = (creature.Category != "") ? creature.Category : "Miscellaneous Creatures";
			listViewItem.Group = this.CardList.Groups[key];
			return listViewItem;
		}

		private void update_creature_list()
		{
			this.CreatureList.BeginUpdate();
			this.CreatureList.ShowGroups = true;
			List<ListViewItem> list = new List<ListViewItem>();
			if (this.DeckView.IsCellSelected)
			{
				List<ICreature> list2 = new List<ICreature>();
				foreach (Creature current in Session.Creatures)
				{
					list2.Add(current);
				}
				foreach (CustomCreature current2 in Session.Project.CustomCreatures)
				{
					list2.Add(current2);
				}
				foreach (ICreature current3 in list2)
				{
					EncounterCard encounterCard = new EncounterCard();
					encounterCard.CreatureID = current3.ID;
					if (this.DeckView.InSelectedCell(encounterCard))
					{
						ListViewItem listViewItem = new ListViewItem(current3.Name);
						listViewItem.Tag = current3;
						string key = (current3.Category != "") ? current3.Category : "Miscellaneous Creatures";
						listViewItem.Group = this.CreatureList.Groups[key];
						list.Add(listViewItem);
					}
				}
				if (list.Count == 0)
				{
					this.CreatureList.ShowGroups = false;
					list.Add(new ListViewItem("(no creatures)")
					{
						ForeColor = SystemColors.GrayText
					});
				}
			}
			else
			{
				this.CreatureList.ShowGroups = false;
				list.Add(new ListViewItem("(no cell selected)")
				{
					ForeColor = SystemColors.GrayText
				});
			}
			this.CreatureList.Items.Clear();
			this.CreatureList.Items.AddRange(list.ToArray());
			this.CreatureList.EndUpdate();
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(DeckBuilderForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.LevelLbl = new Label();
			this.LevelBox = new NumericUpDown();
			this.panel1 = new Panel();
			this.Splitter = new SplitContainer();
			this.DeckSplitter = new SplitContainer();
			this.DeckView = new DeckGrid();
			this.PropertiesPanel = new Panel();
			this.CardList = new ListView();
			this.CardHdr = new ColumnHeader();
			this.CardInfoHdr = new ColumnHeader();
			this.DeckToolbar = new ToolStrip();
			this.DuplicateBtn = new ToolStripButton();
			this.RemoveBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.RefreshBtn = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.ViewBtn = new ToolStripButton();
			this.CreatureList = new ListView();
			this.CreatureHdr = new ColumnHeader();
			this.InfoLbl = new Label();
			this.AutoBuildBtn = new Button();
			this.RefillBtn = new Button();
			((ISupportInitialize)this.LevelBox).BeginInit();
			this.panel1.SuspendLayout();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.DeckSplitter.Panel1.SuspendLayout();
			this.DeckSplitter.Panel2.SuspendLayout();
			this.DeckSplitter.SuspendLayout();
			this.PropertiesPanel.SuspendLayout();
			this.DeckToolbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(542, 397);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(623, 397);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(3, 6);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(47, 3);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(420, 20);
			this.NameBox.TabIndex = 1;
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(3, 31);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(36, 13);
			this.LevelLbl.TabIndex = 2;
			this.LevelLbl.Text = "Level:";
			this.LevelBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LevelBox.Location = new Point(47, 29);
			NumericUpDown arg_414_0 = this.LevelBox;
			int[] array = new int[4];
			array[0] = 30;
			arg_414_0.Maximum = new decimal(array);
			NumericUpDown arg_430_0 = this.LevelBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_430_0.Minimum = new decimal(array2);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new Size(420, 20);
			this.LevelBox.TabIndex = 3;
			NumericUpDown arg_47F_0 = this.LevelBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_47F_0.Value = new decimal(array3);
			this.LevelBox.ValueChanged += new EventHandler(this.LevelBox_ValueChanged);
			this.panel1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.panel1.Controls.Add(this.Splitter);
			this.panel1.Location = new Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(686, 379);
			this.panel1.TabIndex = 0;
			this.Splitter.Dock = DockStyle.Fill;
			this.Splitter.FixedPanel = FixedPanel.Panel2;
			this.Splitter.Location = new Point(0, 0);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.DeckSplitter);
			this.Splitter.Panel2.Controls.Add(this.CreatureList);
			this.Splitter.Panel2.Controls.Add(this.InfoLbl);
			this.Splitter.Size = new Size(686, 379);
			this.Splitter.SplitterDistance = 470;
			this.Splitter.TabIndex = 0;
			this.DeckSplitter.Dock = DockStyle.Fill;
			this.DeckSplitter.FixedPanel = FixedPanel.Panel1;
			this.DeckSplitter.Location = new Point(0, 0);
			this.DeckSplitter.Name = "DeckSplitter";
			this.DeckSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.DeckSplitter.Panel1.Controls.Add(this.DeckView);
			this.DeckSplitter.Panel1.Controls.Add(this.PropertiesPanel);
			this.DeckSplitter.Panel2.Controls.Add(this.CardList);
			this.DeckSplitter.Panel2.Controls.Add(this.DeckToolbar);
			this.DeckSplitter.Size = new Size(470, 379);
			this.DeckSplitter.SplitterDistance = 203;
			this.DeckSplitter.TabIndex = 0;
			this.DeckView.AllowDrop = true;
			this.DeckView.BorderStyle = BorderStyle.FixedSingle;
			this.DeckView.Deck = null;
			this.DeckView.Dock = DockStyle.Fill;
			this.DeckView.Location = new Point(0, 54);
			this.DeckView.Name = "DeckView";
			this.DeckView.Size = new Size(470, 149);
			this.DeckView.TabIndex = 1;
			this.DeckView.DragOver += new DragEventHandler(this.DeckView_DragOver);
			this.DeckView.CellActivated += new EventHandler(this.DeckView_CellActivated);
			this.DeckView.DragDrop += new DragEventHandler(this.DeckView_DragDrop);
			this.DeckView.SelectedCellChanged += new EventHandler(this.DeckView_SelectedCellChanged);
			this.PropertiesPanel.Controls.Add(this.NameBox);
			this.PropertiesPanel.Controls.Add(this.LevelLbl);
			this.PropertiesPanel.Controls.Add(this.NameLbl);
			this.PropertiesPanel.Controls.Add(this.LevelBox);
			this.PropertiesPanel.Dock = DockStyle.Top;
			this.PropertiesPanel.Location = new Point(0, 0);
			this.PropertiesPanel.Name = "PropertiesPanel";
			this.PropertiesPanel.Size = new Size(470, 54);
			this.PropertiesPanel.TabIndex = 0;
			this.CardList.Columns.AddRange(new ColumnHeader[]
			{
				this.CardHdr,
				this.CardInfoHdr
			});
			this.CardList.Dock = DockStyle.Fill;
			this.CardList.FullRowSelect = true;
			this.CardList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.CardList.HideSelection = false;
			this.CardList.Location = new Point(0, 25);
			this.CardList.MultiSelect = false;
			this.CardList.Name = "CardList";
			this.CardList.Size = new Size(470, 147);
			this.CardList.Sorting = SortOrder.Ascending;
			this.CardList.TabIndex = 1;
			this.CardList.UseCompatibleStateImageBehavior = false;
			this.CardList.View = View.Details;
			this.CardList.DoubleClick += new EventHandler(this.CardList_DoubleClick);
			this.CardHdr.Text = "Creature";
			this.CardHdr.Width = 227;
			this.CardInfoHdr.Text = "Info";
			this.CardInfoHdr.Width = 196;
			this.DeckToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.DuplicateBtn,
				this.RemoveBtn,
				this.toolStripSeparator1,
				this.RefreshBtn,
				this.toolStripSeparator2,
				this.ViewBtn
			});
			this.DeckToolbar.Location = new Point(0, 0);
			this.DeckToolbar.Name = "DeckToolbar";
			this.DeckToolbar.Size = new Size(470, 25);
			this.DeckToolbar.TabIndex = 0;
			this.DeckToolbar.Text = "toolStrip1";
			this.DuplicateBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DuplicateBtn.Image = (Image)resources.GetObject("DuplicateBtn.Image");
			this.DuplicateBtn.ImageTransparentColor = Color.Magenta;
			this.DuplicateBtn.Name = "DuplicateBtn";
			this.DuplicateBtn.Size = new Size(61, 22);
			this.DuplicateBtn.Text = "Duplicate";
			this.DuplicateBtn.Click += new EventHandler(this.DuplicateBtn_Click);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)resources.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.RefreshBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RefreshBtn.Image = (Image)resources.GetObject("RefreshBtn.Image");
			this.RefreshBtn.ImageTransparentColor = Color.Magenta;
			this.RefreshBtn.Name = "RefreshBtn";
			this.RefreshBtn.Size = new Size(51, 22);
			this.RefreshBtn.Text = "Re-Add";
			this.RefreshBtn.Click += new EventHandler(this.RefreshBtn_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 25);
			this.ViewBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ViewBtn.Image = (Image)resources.GetObject("ViewBtn.Image");
			this.ViewBtn.ImageTransparentColor = Color.Magenta;
			this.ViewBtn.Name = "ViewBtn";
			this.ViewBtn.Size = new Size(69, 22);
			this.ViewBtn.Text = "View Cards";
			this.ViewBtn.Click += new EventHandler(this.ViewBtn_Click);
			this.CreatureList.Columns.AddRange(new ColumnHeader[]
			{
				this.CreatureHdr
			});
			this.CreatureList.Dock = DockStyle.Fill;
			this.CreatureList.FullRowSelect = true;
			this.CreatureList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.CreatureList.HideSelection = false;
			this.CreatureList.Location = new Point(0, 0);
			this.CreatureList.MultiSelect = false;
			this.CreatureList.Name = "CreatureList";
			this.CreatureList.Size = new Size(212, 340);
			this.CreatureList.Sorting = SortOrder.Ascending;
			this.CreatureList.TabIndex = 0;
			this.CreatureList.UseCompatibleStateImageBehavior = false;
			this.CreatureList.View = View.Details;
			this.CreatureList.DoubleClick += new EventHandler(this.CreatureList_DoubleClick);
			this.CreatureList.ItemDrag += new ItemDragEventHandler(this.CreatureList_ItemDrag);
			this.CreatureHdr.Text = "Creature";
			this.CreatureHdr.Width = 180;
			this.InfoLbl.BorderStyle = BorderStyle.FixedSingle;
			this.InfoLbl.Dock = DockStyle.Bottom;
			this.InfoLbl.Location = new Point(0, 340);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(212, 39);
			this.InfoLbl.TabIndex = 1;
			this.InfoLbl.Text = "[info]";
			this.InfoLbl.TextAlign = ContentAlignment.MiddleLeft;
			this.AutoBuildBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.AutoBuildBtn.Location = new Point(12, 397);
			this.AutoBuildBtn.Name = "AutoBuildBtn";
			this.AutoBuildBtn.Size = new Size(100, 23);
			this.AutoBuildBtn.TabIndex = 1;
			this.AutoBuildBtn.Text = "AutoBuild";
			this.AutoBuildBtn.UseVisualStyleBackColor = true;
			this.AutoBuildBtn.Click += new EventHandler(this.AutoBuildBtn_Click);
			this.RefillBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.RefillBtn.Location = new Point(118, 397);
			this.RefillBtn.Name = "RefillBtn";
			this.RefillBtn.Size = new Size(100, 23);
			this.RefillBtn.TabIndex = 2;
			this.RefillBtn.Text = "Refill Deck";
			this.RefillBtn.UseVisualStyleBackColor = true;
			this.RefillBtn.Click += new EventHandler(this.RefillBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(710, 432);
			base.Controls.Add(this.RefillBtn);
			base.Controls.Add(this.AutoBuildBtn);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DeckBuilderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Encounter Deck Builder";
			((ISupportInitialize)this.LevelBox).EndInit();
			this.panel1.ResumeLayout(false);
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.DeckSplitter.Panel1.ResumeLayout(false);
			this.DeckSplitter.Panel2.ResumeLayout(false);
			this.DeckSplitter.Panel2.PerformLayout();
			this.DeckSplitter.ResumeLayout(false);
			this.PropertiesPanel.ResumeLayout(false);
			this.PropertiesPanel.PerformLayout();
			this.DeckToolbar.ResumeLayout(false);
			this.DeckToolbar.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
