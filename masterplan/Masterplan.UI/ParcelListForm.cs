using Masterplan.Data;
using Masterplan.Tools.Generators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class ParcelListForm : Form
	{
		private bool fViewAssigned;

		private bool fViewUnassigned = true;

		private IContainer components;

		private ToolStrip Toolbar;

		private ListView ParcelList;

		private ColumnHeader ParcelHdr;

		private ColumnHeader DetailsHdr;

		private ToolStripDropDownButton AddBtn;

		private ToolStripMenuItem AddParcel;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private ToolStripMenuItem AddSet;

		private Panel MainPanel;

		private Button CloseBtn;

		private ToolStripMenuItem AddMagicItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripDropDownButton ViewMenu;

		private ToolStripMenuItem ViewAssigned;

		private ToolStripMenuItem ViewUnassigned;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton RandomiseAllBtn;

		private ToolStripSeparator toolStripSeparator4;

		private ColumnHeader HeroHdr;

		private ContextMenuStrip ParcelMenu;

		private ToolStripMenuItem ChangeItem;

		private ToolStripSeparator toolStripMenuItem1;

		private ToolStripMenuItem ChangeAssign;

		private ToolStripMenuItem RandomiseItem;

		private ToolStripSeparator toolStripMenuItem2;

		private ToolStripMenuItem ResetItem;

		private ToolStripMenuItem AddArtifact;

		public Parcel SelectedParcel
		{
			get
			{
				if (this.ParcelList.SelectedItems.Count != 0)
				{
					return this.ParcelList.SelectedItems[0].Tag as Parcel;
				}
				return null;
			}
		}

		public ParcelListForm()
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.AddMagicItem.Enabled = (Session.MagicItems.Count != 0);
			this.AddArtifact.Enabled = (Session.Artifacts.Count != 0);
			this.RemoveBtn.Enabled = (this.SelectedParcel != null);
			this.EditBtn.Enabled = (this.SelectedParcel != null);
			this.RandomiseAllBtn.Enabled = (Session.Project.TreasureParcels.Count != 0 && this.fViewUnassigned);
		}

		private void AddParcel_Click(object sender, EventArgs e)
		{
			ParcelForm parcelForm = new ParcelForm(new Parcel
			{
				Name = "New Treasure Parcel"
			});
			if (parcelForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.TreasureParcels.Add(parcelForm.Parcel);
				Session.Modified = true;
				this.update_list();
			}
		}

		private void AddMagicItem_Click(object sender, EventArgs e)
		{
			MagicItemSelectForm magicItemSelectForm = new MagicItemSelectForm(Session.Project.Party.Level);
			if (magicItemSelectForm.ShowDialog() == DialogResult.OK)
			{
				Parcel item = new Parcel(magicItemSelectForm.MagicItem);
				Session.Project.TreasureParcels.Add(item);
				Session.Modified = true;
				this.update_list();
			}
		}

		private void AddArtifact_Click(object sender, EventArgs e)
		{
			ArtifactSelectForm artifactSelectForm = new ArtifactSelectForm();
			if (artifactSelectForm.ShowDialog() == DialogResult.OK)
			{
				Parcel item = new Parcel(artifactSelectForm.Artifact);
				Session.Project.TreasureParcels.Add(item);
				Session.Modified = true;
				this.update_list();
			}
		}

		private void AddSet_Click(object sender, EventArgs e)
		{
			LevelForm levelForm = new LevelForm(Session.Project.Party.Level);
			if (levelForm.ShowDialog() == DialogResult.OK)
			{
				List<Parcel> collection = Treasure.CreateParcelSet(levelForm.Level, Session.Project.Party.Size, true);
				Session.Project.TreasureParcels.AddRange(collection);
				Session.Modified = true;
				this.update_list();
			}
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedParcel != null)
			{
				List<Parcel> list = this.get_list_containing(this.SelectedParcel);
				list.Remove(this.SelectedParcel);
				Session.Modified = true;
				this.update_list();
			}
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedParcel != null)
			{
				List<Parcel> list = this.get_list_containing(this.SelectedParcel);
				int index = list.IndexOf(this.SelectedParcel);
				ParcelForm parcelForm = new ParcelForm(this.SelectedParcel);
				if (parcelForm.ShowDialog() == DialogResult.OK)
				{
					list[index] = parcelForm.Parcel;
					Session.Modified = true;
					this.update_list();
				}
			}
		}

		private void update_list()
		{
			this.ParcelList.BeginUpdate();
			this.ParcelList.Items.Clear();
			if (this.fViewAssigned)
			{
				List<PlotPoint> allPlotPoints = Session.Project.AllPlotPoints;
				foreach (PlotPoint current in allPlotPoints)
				{
					this.add_list(current.Parcels, 0);
				}
			}
			if (this.fViewUnassigned)
			{
				this.add_list(Session.Project.TreasureParcels, 1);
			}
			if (this.fViewAssigned && this.ParcelList.Groups[0].Items.Count == 0)
			{
				ListViewItem listViewItem = this.ParcelList.Items.Add("(no parcels)");
				listViewItem.ForeColor = SystemColors.GrayText;
				listViewItem.Group = this.ParcelList.Groups[0];
			}
			if (this.fViewUnassigned && this.ParcelList.Groups[1].Items.Count == 0)
			{
				ListViewItem listViewItem2 = this.ParcelList.Items.Add("(no parcels)");
				listViewItem2.ForeColor = SystemColors.GrayText;
				listViewItem2.Group = this.ParcelList.Groups[1];
			}
			this.ParcelList.Sort();
			this.ParcelList.EndUpdate();
		}

		private List<Parcel> get_list_containing(Parcel p)
		{
			if (Session.Project.TreasureParcels.Contains(p))
			{
				return Session.Project.TreasureParcels;
			}
			List<PlotPoint> allPlotPoints = Session.Project.AllPlotPoints;
			foreach (PlotPoint current in allPlotPoints)
			{
				if (current.Parcels.Contains(p))
				{
					return current.Parcels;
				}
			}
			return null;
		}

		private void add_list(List<Parcel> list, int group_index)
		{
			foreach (Parcel current in list)
			{
				string text = (current.Name != "") ? current.Name : "(undefined parcel)";
				ListViewItem listViewItem = this.ParcelList.Items.Add(text);
				listViewItem.SubItems.Add(current.Details);
				listViewItem.Tag = current;
				listViewItem.Group = this.ParcelList.Groups[group_index];
				Hero hero = null;
				if (current.HeroID != Guid.Empty)
				{
					hero = Session.Project.FindHero(current.HeroID);
				}
				ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems.Add((hero != null) ? hero.Name : "");
				if (hero == null)
				{
					listViewSubItem.ForeColor = SystemColors.GrayText;
					listViewItem.UseItemStyleForSubItems = false;
				}
			}
		}

		private void ViewMenu_DropDownOpening(object sender, EventArgs e)
		{
			this.ViewAssigned.Checked = this.fViewAssigned;
			this.ViewUnassigned.Checked = this.fViewUnassigned;
		}

		private void ViewAssigned_Click(object sender, EventArgs e)
		{
			this.fViewAssigned = !this.fViewAssigned;
			this.update_list();
		}

		private void ViewUnassigned_Click(object sender, EventArgs e)
		{
			this.fViewUnassigned = !this.fViewUnassigned;
			this.update_list();
		}

		private void ChangeItemBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedParcel != null)
			{
				if (this.SelectedParcel.MagicItemID != Guid.Empty)
				{
					int num = this.SelectedParcel.FindItemLevel();
					if (num != -1)
					{
						MagicItemSelectForm magicItemSelectForm = new MagicItemSelectForm(num);
						if (magicItemSelectForm.ShowDialog() == DialogResult.OK)
						{
							this.SelectedParcel.SetAsMagicItem(magicItemSelectForm.MagicItem);
							Session.Modified = true;
							this.update_list();
						}
					}
				}
				if (this.SelectedParcel.ArtifactID != Guid.Empty)
				{
					ArtifactSelectForm artifactSelectForm = new ArtifactSelectForm();
					if (artifactSelectForm.ShowDialog() == DialogResult.OK)
					{
						this.SelectedParcel.SetAsArtifact(artifactSelectForm.Artifact);
						Session.Modified = true;
						this.update_list();
					}
				}
			}
		}

		private void assign_to_hero(object sender, EventArgs e)
		{
			if (this.SelectedParcel == null)
			{
				return;
			}
			ToolStripItem toolStripItem = sender as ToolStripItem;
			if (toolStripItem == null)
			{
				return;
			}
			Hero hero = toolStripItem.Tag as Hero;
			this.SelectedParcel.HeroID = ((hero != null) ? hero.ID : Guid.Empty);
			this.update_list();
			Session.Modified = true;
		}

		private void RandomiseBtn_Click(object sender, EventArgs e)
		{
			foreach (Parcel current in Session.Project.TreasureParcels)
			{
				this.randomise_parcel(current);
			}
			this.update_list();
			Session.Modified = true;
		}

		private void RandomiseItem_Click(object sender, EventArgs e)
		{
			if (this.SelectedParcel != null)
			{
				this.randomise_parcel(this.SelectedParcel);
				this.update_list();
			}
		}

		private void ResetItem_Click(object sender, EventArgs e)
		{
			if (this.SelectedParcel != null)
			{
				this.reset_parcel(this.SelectedParcel);
				this.update_list();
			}
		}

		private void randomise_parcel(Parcel parcel)
		{
			if (parcel.MagicItemID != Guid.Empty)
			{
				int num = parcel.FindItemLevel();
				if (num != -1)
				{
					MagicItem magicItem = Treasure.RandomMagicItem(num);
					if (magicItem != null)
					{
						parcel.SetAsMagicItem(magicItem);
						return;
					}
				}
			}
			else if (parcel.ArtifactID != Guid.Empty)
			{
				Tier tier = parcel.FindItemTier();
				Artifact artifact = Treasure.RandomArtifact(tier);
				if (artifact != null)
				{
					parcel.SetAsArtifact(artifact);
					return;
				}
			}
			else if (parcel.Value != 0)
			{
				parcel.Details = Treasure.RandomMundaneItem(parcel.Value);
			}
		}

		private void reset_parcel(Parcel parcel)
		{
			if (parcel.MagicItemID != Guid.Empty)
			{
				int num = parcel.FindItemLevel();
				if (num != -1)
				{
					parcel.MagicItemID = Treasure.PlaceholderIDs[num - 1];
					parcel.Name = "Magic item (level " + num + ")";
				}
				else
				{
					parcel.Name = "Magic item";
				}
			}
			else if (parcel.ArtifactID != Guid.Empty)
			{
				Tier tier = parcel.FindItemTier();
				parcel.ArtifactID = Treasure.PlaceholderIDs[(int)tier];
				parcel.Name = "Artifact ( " + tier.ToString().ToLower() + " tier)";
			}
			else
			{
				parcel.Name = "Items worth " + parcel.Value + " GP";
			}
			parcel.Details = "";
		}

		private void ContextMenu_Opening(object sender, CancelEventArgs e)
		{
			this.ChangeItem.Enabled = (this.SelectedParcel != null && this.SelectedParcel.MagicItemID != Guid.Empty);
			this.ChangeAssign.Enabled = (this.SelectedParcel != null);
			this.RandomiseItem.Enabled = (this.SelectedParcel != null);
			this.ResetItem.Enabled = (this.SelectedParcel != null);
			this.ChangeAssign.DropDownItems.Clear();
			foreach (Hero current in Session.Project.Heroes)
			{
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(current.Name);
				toolStripMenuItem.Tag = current;
				toolStripMenuItem.Click += new EventHandler(this.assign_to_hero);
				if (this.SelectedParcel != null)
				{
					toolStripMenuItem.Checked = (this.SelectedParcel.HeroID == current.ID);
				}
				this.ChangeAssign.DropDownItems.Add(toolStripMenuItem);
			}
			if (Session.Project.Heroes.Count != 0)
			{
				this.ChangeAssign.DropDownItems.Add(new ToolStripSeparator());
			}
			ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem("Not Allocated");
			toolStripMenuItem2.Tag = null;
			toolStripMenuItem2.Click += new EventHandler(this.assign_to_hero);
			if (this.SelectedParcel != null)
			{
				toolStripMenuItem2.Checked = (this.SelectedParcel.HeroID == Guid.Empty);
			}
			this.ChangeAssign.DropDownItems.Add(toolStripMenuItem2);
		}

		private void ParcelListForm_Shown(object sender, EventArgs e)
		{
			this.ParcelList.Invalidate();
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(ParcelListForm));
			ListViewGroup listViewGroup = new ListViewGroup("Parcels which are assigned to a Plot Point", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Parcels which are not yet assigned to a plot point", HorizontalAlignment.Left);
			this.Toolbar = new ToolStrip();
			this.AddBtn = new ToolStripDropDownButton();
			this.AddParcel = new ToolStripMenuItem();
			this.AddMagicItem = new ToolStripMenuItem();
			this.AddArtifact = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.AddSet = new ToolStripMenuItem();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.RandomiseAllBtn = new ToolStripButton();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.ViewMenu = new ToolStripDropDownButton();
			this.ViewAssigned = new ToolStripMenuItem();
			this.ViewUnassigned = new ToolStripMenuItem();
			this.ParcelList = new ListView();
			this.ParcelHdr = new ColumnHeader();
			this.DetailsHdr = new ColumnHeader();
			this.HeroHdr = new ColumnHeader();
			this.ParcelMenu = new ContextMenuStrip(this.components);
			this.ChangeItem = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this.ChangeAssign = new ToolStripMenuItem();
			this.toolStripMenuItem2 = new ToolStripSeparator();
			this.RandomiseItem = new ToolStripMenuItem();
			this.ResetItem = new ToolStripMenuItem();
			this.MainPanel = new Panel();
			this.CloseBtn = new Button();
			this.Toolbar.SuspendLayout();
			this.ParcelMenu.SuspendLayout();
			this.MainPanel.SuspendLayout();
			base.SuspendLayout();
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.AddBtn,
				this.RemoveBtn,
				this.EditBtn,
				this.toolStripSeparator3,
				this.RandomiseAllBtn,
				this.toolStripSeparator4,
				this.ViewMenu
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(665, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.AddParcel,
				this.AddMagicItem,
				this.AddArtifact,
				this.toolStripSeparator1,
				this.AddSet
			});
			this.AddBtn.Image = (Image)resources.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new Size(42, 22);
			this.AddBtn.Text = "Add";
			this.AddParcel.Name = "AddParcel";
			this.AddParcel.Size = new Size(228, 22);
			this.AddParcel.Text = "Add a Mundane Parcel...";
			this.AddParcel.Click += new EventHandler(this.AddParcel_Click);
			this.AddMagicItem.Name = "AddMagicItem";
			this.AddMagicItem.Size = new Size(228, 22);
			this.AddMagicItem.Text = "Add a Magic Item...";
			this.AddMagicItem.Click += new EventHandler(this.AddMagicItem_Click);
			this.AddArtifact.Name = "AddArtifact";
			this.AddArtifact.Size = new Size(228, 22);
			this.AddArtifact.Text = "Add an Artifact...";
			this.AddArtifact.Click += new EventHandler(this.AddArtifact_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(225, 6);
			this.AddSet.Name = "AddSet";
			this.AddSet.Size = new Size(228, 22);
			this.AddSet.Text = "Add a Standard Set of Parcels";
			this.AddSet.Click += new EventHandler(this.AddSet_Click);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)resources.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)resources.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new Size(31, 22);
			this.EditBtn.Text = "Edit";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new Size(6, 25);
			this.RandomiseAllBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RandomiseAllBtn.Image = (Image)resources.GetObject("RandomiseAllBtn.Image");
			this.RandomiseAllBtn.ImageTransparentColor = Color.Magenta;
			this.RandomiseAllBtn.Name = "RandomiseAllBtn";
			this.RandomiseAllBtn.Size = new Size(87, 22);
			this.RandomiseAllBtn.Text = "Randomise All";
			this.RandomiseAllBtn.ToolTipText = "Randomise unassigned parcels";
			this.RandomiseAllBtn.Click += new EventHandler(this.RandomiseBtn_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new Size(6, 25);
			this.ViewMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ViewMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ViewAssigned,
				this.ViewUnassigned
			});
			this.ViewMenu.Image = (Image)resources.GetObject("ViewMenu.Image");
			this.ViewMenu.ImageTransparentColor = Color.Magenta;
			this.ViewMenu.Name = "ViewMenu";
			this.ViewMenu.Size = new Size(45, 22);
			this.ViewMenu.Text = "View";
			this.ViewMenu.DropDownOpening += new EventHandler(this.ViewMenu_DropDownOpening);
			this.ViewAssigned.Name = "ViewAssigned";
			this.ViewAssigned.Size = new Size(135, 22);
			this.ViewAssigned.Text = "Assigned";
			this.ViewAssigned.Click += new EventHandler(this.ViewAssigned_Click);
			this.ViewUnassigned.Name = "ViewUnassigned";
			this.ViewUnassigned.Size = new Size(135, 22);
			this.ViewUnassigned.Text = "Unassigned";
			this.ViewUnassigned.Click += new EventHandler(this.ViewUnassigned_Click);
			this.ParcelList.Columns.AddRange(new ColumnHeader[]
			{
				this.ParcelHdr,
				this.DetailsHdr,
				this.HeroHdr
			});
			this.ParcelList.ContextMenuStrip = this.ParcelMenu;
			this.ParcelList.Dock = DockStyle.Fill;
			this.ParcelList.FullRowSelect = true;
			listViewGroup.Header = "Parcels which are assigned to a Plot Point";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup2.Header = "Parcels which are not yet assigned to a plot point";
			listViewGroup2.Name = "listViewGroup2";
			this.ParcelList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2
			});
			this.ParcelList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.ParcelList.HideSelection = false;
			this.ParcelList.Location = new Point(0, 25);
			this.ParcelList.MultiSelect = false;
			this.ParcelList.Name = "ParcelList";
			this.ParcelList.Size = new Size(665, 314);
			this.ParcelList.Sorting = SortOrder.Ascending;
			this.ParcelList.TabIndex = 1;
			this.ParcelList.UseCompatibleStateImageBehavior = false;
			this.ParcelList.View = View.Details;
			this.ParcelList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.ParcelHdr.Text = "Parcel";
			this.ParcelHdr.Width = 150;
			this.DetailsHdr.Text = "Details";
			this.DetailsHdr.Width = 300;
			this.HeroHdr.Text = "Allocated To";
			this.HeroHdr.Width = 183;
			this.ParcelMenu.Items.AddRange(new ToolStripItem[]
			{
				this.ChangeItem,
				this.toolStripMenuItem1,
				this.ChangeAssign,
				this.toolStripMenuItem2,
				this.RandomiseItem,
				this.ResetItem
			});
			this.ParcelMenu.Name = "contextMenuStrip1";
			this.ParcelMenu.Size = new Size(187, 104);
			this.ParcelMenu.Opening += new CancelEventHandler(this.ContextMenu_Opening);
			this.ChangeItem.Name = "ChangeItem";
			this.ChangeItem.Size = new Size(186, 22);
			this.ChangeItem.Text = "Choose Magic Item...";
			this.ChangeItem.Click += new EventHandler(this.ChangeItemBtn_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new Size(183, 6);
			this.ChangeAssign.Name = "ChangeAssign";
			this.ChangeAssign.Size = new Size(186, 22);
			this.ChangeAssign.Text = "Allocate To PC";
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new Size(183, 6);
			this.RandomiseItem.Name = "RandomiseItem";
			this.RandomiseItem.Size = new Size(186, 22);
			this.RandomiseItem.Text = "Randomise Parcel";
			this.RandomiseItem.Click += new EventHandler(this.RandomiseItem_Click);
			this.ResetItem.Name = "ResetItem";
			this.ResetItem.Size = new Size(186, 22);
			this.ResetItem.Text = "Reset Parcel";
			this.ResetItem.Click += new EventHandler(this.ResetItem_Click);
			this.MainPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.MainPanel.Controls.Add(this.ParcelList);
			this.MainPanel.Controls.Add(this.Toolbar);
			this.MainPanel.Location = new Point(12, 12);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new Size(665, 339);
			this.MainPanel.TabIndex = 2;
			this.CloseBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CloseBtn.DialogResult = DialogResult.OK;
			this.CloseBtn.Location = new Point(602, 357);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new Size(75, 23);
			this.CloseBtn.TabIndex = 3;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(689, 392);
			base.Controls.Add(this.CloseBtn);
			base.Controls.Add(this.MainPanel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ParcelListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Treasure Parcels";
			base.Shown += new EventHandler(this.ParcelListForm_Shown);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.ParcelMenu.ResumeLayout(false);
			this.MainPanel.ResumeLayout(false);
			this.MainPanel.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
