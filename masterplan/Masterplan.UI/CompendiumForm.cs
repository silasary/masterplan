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
	internal class CompendiumForm : Form
	{
		private IContainer components;

		private Button CloseBtn;

		private SplitContainer Splitter;

		private ListView BookList;

		private ColumnHeader BookHdr;

		private ListView ItemList;

		private ColumnHeader ItemHdr;

		private ColumnHeader InfoHdr;

		private List<CompendiumHelper.SourceBook> fBooks = new List<CompendiumHelper.SourceBook>();

		public CompendiumHelper.SourceBook SelectedBook
		{
			get
			{
				if (this.BookList.SelectedItems.Count != 0)
				{
					return this.BookList.SelectedItems[0].Tag as CompendiumHelper.SourceBook;
				}
				return null;
			}
		}

		public CompendiumHelper.CompendiumItem SelectedItem
		{
			get
			{
				if (this.ItemList.SelectedItems.Count != 0)
				{
					return this.ItemList.SelectedItems[0].Tag as CompendiumHelper.CompendiumItem;
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
			ListViewGroup listViewGroup = new ListViewGroup("Books", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Dragon Magazine", HorizontalAlignment.Left);
			ListViewGroup listViewGroup3 = new ListViewGroup("Dungeon Magazine", HorizontalAlignment.Left);
			ListViewGroup listViewGroup4 = new ListViewGroup("RPGA Modules", HorizontalAlignment.Left);
			ListViewGroup listViewGroup5 = new ListViewGroup("Creatures", HorizontalAlignment.Left);
			ListViewGroup listViewGroup6 = new ListViewGroup("Traps / Hazards", HorizontalAlignment.Left);
			ListViewGroup listViewGroup7 = new ListViewGroup("Magic Items", HorizontalAlignment.Left);
			this.CloseBtn = new Button();
			this.Splitter = new SplitContainer();
			this.BookList = new ListView();
			this.BookHdr = new ColumnHeader();
			this.ItemList = new ListView();
			this.ItemHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			base.SuspendLayout();
			this.CloseBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CloseBtn.DialogResult = DialogResult.OK;
			this.CloseBtn.Location = new Point(565, 330);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new Size(75, 23);
			this.CloseBtn.TabIndex = 2;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			this.Splitter.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.BookList);
			this.Splitter.Panel2.Controls.Add(this.ItemList);
			this.Splitter.Size = new Size(628, 312);
			this.Splitter.SplitterDistance = 228;
			this.Splitter.TabIndex = 3;
			this.BookList.Columns.AddRange(new ColumnHeader[]
			{
				this.BookHdr
			});
			this.BookList.Dock = DockStyle.Fill;
			this.BookList.FullRowSelect = true;
			listViewGroup.Header = "Books";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup2.Header = "Dragon Magazine";
			listViewGroup2.Name = "listViewGroup2";
			listViewGroup3.Header = "Dungeon Magazine";
			listViewGroup3.Name = "listViewGroup3";
			listViewGroup4.Header = "RPGA Modules";
			listViewGroup4.Name = "listViewGroup4";
			this.BookList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2,
				listViewGroup3,
				listViewGroup4
			});
			this.BookList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.BookList.HideSelection = false;
			this.BookList.Location = new Point(0, 0);
			this.BookList.MultiSelect = false;
			this.BookList.Name = "BookList";
			this.BookList.Size = new Size(228, 312);
			this.BookList.Sorting = SortOrder.Ascending;
			this.BookList.TabIndex = 3;
			this.BookList.UseCompatibleStateImageBehavior = false;
			this.BookList.View = View.Details;
			this.BookList.SelectedIndexChanged += new EventHandler(this.BookList_SelectedIndexChanged);
			this.BookHdr.Text = "Source Book";
			this.BookHdr.Width = 200;
			this.ItemList.Columns.AddRange(new ColumnHeader[]
			{
				this.ItemHdr,
				this.InfoHdr
			});
			this.ItemList.Dock = DockStyle.Fill;
			this.ItemList.FullRowSelect = true;
			listViewGroup5.Header = "Creatures";
			listViewGroup5.Name = "listViewGroup1";
			listViewGroup6.Header = "Traps / Hazards";
			listViewGroup6.Name = "listViewGroup2";
			listViewGroup7.Header = "Magic Items";
			listViewGroup7.Name = "listViewGroup3";
			this.ItemList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup5,
				listViewGroup6,
				listViewGroup7
			});
			this.ItemList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.ItemList.HideSelection = false;
			this.ItemList.Location = new Point(0, 0);
			this.ItemList.MultiSelect = false;
			this.ItemList.Name = "ItemList";
			this.ItemList.Size = new Size(396, 312);
			this.ItemList.Sorting = SortOrder.Ascending;
			this.ItemList.TabIndex = 1;
			this.ItemList.UseCompatibleStateImageBehavior = false;
			this.ItemList.View = View.Details;
			this.ItemList.DoubleClick += new EventHandler(this.ItemList_DoubleClick);
			this.ItemHdr.Text = "Item";
			this.ItemHdr.Width = 208;
			this.InfoHdr.Text = "Details";
			this.InfoHdr.Width = 158;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(652, 365);
			base.Controls.Add(this.Splitter);
			base.Controls.Add(this.CloseBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SourceBookListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Dungeons & Dragons Compendium";
			base.Shown += new EventHandler(this.SourceBookListForm_Shown);
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public CompendiumForm()
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.update_books();
			this.update_items();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.ItemList.Enabled = (this.SelectedBook != null);
		}

		private void SourceBookListForm_Shown(object sender, EventArgs e)
		{
			Application.DoEvents();
			Cursor.Current = Cursors.WaitCursor;
			Dictionary<string, CompendiumHelper.SourceBook> data = this.get_data();
			this.fBooks.Clear();
			this.fBooks.AddRange(data.Values);
			this.update_books();
			this.update_items();
			Cursor.Current = Cursors.Default;
		}

		private void BookList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.update_items();
		}

		private void ItemList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedItem != null)
			{
				CompendiumItemForm compendiumItemForm = new CompendiumItemForm(this.SelectedItem);
				if (compendiumItemForm.ShowDialog() == DialogResult.OK)
				{
					Library library = Session.FindLibrary(this.SelectedItem.SourceBook);
					if (library == null)
					{
						library = new Library();
						library.Name = this.SelectedItem.SourceBook;
						Session.Libraries.Add(library);
					}
					switch (this.SelectedItem.Type)
					{
					case CompendiumHelper.ItemType.Creature:
					{
						Creature creature = compendiumItemForm.Result as Creature;
						Creature creature2 = library.FindCreature(creature.Name, creature.Level);
						if (creature2 != null)
						{
							creature.ID = creature2.ID;
							creature.Category = creature2.Category;
							library.Creatures.Remove(creature2);
						}
						library.Creatures.Add(creature);
						break;
					}
					case CompendiumHelper.ItemType.Trap:
					{
						Trap trap = compendiumItemForm.Result as Trap;
						Trap trap2 = library.FindTrap(trap.Name, trap.Level, trap.Role.ToString());
						if (trap2 != null)
						{
							trap.ID = trap2.ID;
							library.Traps.Remove(trap2);
						}
						library.Traps.Add(trap);
						break;
					}
					case CompendiumHelper.ItemType.MagicItem:
					{
						MagicItem magicItem = compendiumItemForm.Result as MagicItem;
						MagicItem magicItem2 = library.FindMagicItem(magicItem.Name, magicItem.Level);
						if (magicItem2 != null)
						{
							magicItem.ID = magicItem2.ID;
							library.MagicItems.Remove(magicItem2);
						}
						library.MagicItems.Add(magicItem);
						break;
					}
					}
					string libraryFilename = Session.GetLibraryFilename(library);
					Serialisation<Library>.Save(libraryFilename, library, SerialisationMode.Binary);
				}
			}
		}

		private Dictionary<string, CompendiumHelper.SourceBook> get_data()
		{
			List<CompendiumHelper.CompendiumItem> creatures = CompendiumHelper.GetCreatures();
			List<CompendiumHelper.CompendiumItem> traps = CompendiumHelper.GetTraps();
			List<CompendiumHelper.CompendiumItem> magicItems = CompendiumHelper.GetMagicItems();
			List<CompendiumHelper.CompendiumItem> list = new List<CompendiumHelper.CompendiumItem>();
			if (creatures != null)
			{
				list.AddRange(creatures);
			}
			if (traps != null)
			{
				list.AddRange(traps);
			}
			if (magicItems != null)
			{
				list.AddRange(magicItems);
			}
			Dictionary<string, CompendiumHelper.SourceBook> dictionary = new Dictionary<string, CompendiumHelper.SourceBook>();
			foreach (CompendiumHelper.CompendiumItem current in list)
			{
				if (!dictionary.ContainsKey(current.SourceBook))
				{
					CompendiumHelper.SourceBook sourceBook = new CompendiumHelper.SourceBook();
					sourceBook.Name = current.SourceBook;
					dictionary[current.SourceBook] = sourceBook;
				}
				CompendiumHelper.SourceBook sourceBook2 = dictionary[current.SourceBook];
				if (creatures.Contains(current))
				{
					sourceBook2.Creatures.Add(current);
				}
				if (traps.Contains(current))
				{
					sourceBook2.Traps.Add(current);
				}
				if (magicItems.Contains(current))
				{
					sourceBook2.MagicItems.Add(current);
				}
			}
			return dictionary;
		}

		private void update_books()
		{
			this.BookList.ShowGroups = true;
			List<ListViewItem> list = new List<ListViewItem>();
			foreach (CompendiumHelper.SourceBook current in this.fBooks)
			{
				ListViewItem listViewItem = new ListViewItem(current.Name);
				listViewItem.Tag = current;
				listViewItem.Group = this.BookList.Groups[0];
				if (current.Name.ToLower().StartsWith("dragon magazine"))
				{
					listViewItem.Group = this.BookList.Groups[1];
				}
				if (current.Name.ToLower().StartsWith("dungeon magazine"))
				{
					listViewItem.Group = this.BookList.Groups[2];
				}
				if (current.Name.ToLower().StartsWith("rpga"))
				{
					listViewItem.Group = this.BookList.Groups[3];
				}
				list.Add(listViewItem);
			}
			this.BookList.Items.Clear();
			this.BookList.Items.AddRange(list.ToArray());
			if (this.fBooks.Count == 0)
			{
				this.BookList.ShowGroups = false;
				ListViewItem listViewItem2 = this.BookList.Items.Add("(loading)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
		}

		private void update_items()
		{
			if (this.SelectedBook == null)
			{
				this.ItemList.Items.Clear();
				return;
			}
			this.ItemList.BeginUpdate();
			List<ListViewItem> list = new List<ListViewItem>();
			foreach (CompendiumHelper.CompendiumItem current in this.SelectedBook.Creatures)
			{
				list.Add(new ListViewItem(current.Name)
				{
					SubItems = 
					{
						current.Info
					},
					Tag = current,
					Group = this.ItemList.Groups[0]
				});
			}
			if (this.SelectedBook.Creatures.Count == 0)
			{
				list.Add(new ListViewItem("(none)")
				{
					ForeColor = SystemColors.GrayText,
					Group = this.ItemList.Groups[0]
				});
			}
			foreach (CompendiumHelper.CompendiumItem current2 in this.SelectedBook.Traps)
			{
				list.Add(new ListViewItem(current2.Name)
				{
					SubItems = 
					{
						current2.Info
					},
					Tag = current2,
					Group = this.ItemList.Groups[1]
				});
			}
			if (this.SelectedBook.Traps.Count == 0)
			{
				list.Add(new ListViewItem("(none)")
				{
					ForeColor = SystemColors.GrayText,
					Group = this.ItemList.Groups[1]
				});
			}
			foreach (CompendiumHelper.CompendiumItem current3 in this.SelectedBook.MagicItems)
			{
				list.Add(new ListViewItem(current3.Name)
				{
					SubItems = 
					{
						current3.Info
					},
					Tag = current3,
					Group = this.ItemList.Groups[2]
				});
			}
			if (this.SelectedBook.MagicItems.Count == 0)
			{
				list.Add(new ListViewItem("(none)")
				{
					ForeColor = SystemColors.GrayText,
					Group = this.ItemList.Groups[2]
				});
			}
			this.ItemList.Items.Clear();
			this.ItemList.Items.AddRange(list.ToArray());
			this.ItemList.EndUpdate();
		}
	}
}
