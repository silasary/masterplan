using Masterplan.Data;
using Masterplan.Tools.Generators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class ParcelSelectForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private ColumnHeader NameHdr;

		private ColumnHeader DetailsHdr;

		private ListView ParcelList;

		private Panel ListPanel;

		private ToolStrip Toolbar;

		private ToolStripButton ChangeItemBtn;

		private ToolStripButton StatBlockBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton RandomiseAllBtn;

		private ToolStripButton RandomiseBtn;

		public Parcel Parcel
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

		public ParcelSelectForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RandomiseBtn.Enabled = (this.Parcel != null);
			this.ChangeItemBtn.Enabled = (this.Parcel != null && this.Parcel.MagicItemID != Guid.Empty);
			this.StatBlockBtn.Enabled = (this.Parcel != null && this.Parcel.MagicItemID != Guid.Empty && !Treasure.PlaceholderIDs.Contains(this.Parcel.MagicItemID));
			this.OKBtn.Enabled = (this.Parcel != null);
		}

		private void TileList_DoubleClick(object sender, EventArgs e)
		{
			if (this.Parcel != null)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		private void ChangeItemBtn_Click(object sender, EventArgs e)
		{
			if (this.Parcel != null && this.Parcel.MagicItemID != Guid.Empty)
			{
				int num = 0;
				MagicItem magicItem = Session.FindMagicItem(this.Parcel.MagicItemID, SearchType.Global);
				if (magicItem != null)
				{
					num = magicItem.Level;
				}
				else
				{
					int num2 = Treasure.PlaceholderIDs.IndexOf(this.Parcel.MagicItemID);
					if (num2 != -1)
					{
						num = num2 + 1;
					}
				}
				if (num > 0)
				{
					MagicItemSelectForm magicItemSelectForm = new MagicItemSelectForm(num);
					if (magicItemSelectForm.ShowDialog() == DialogResult.OK)
					{
						this.Parcel.SetAsMagicItem(magicItemSelectForm.MagicItem);
						Session.Modified = true;
						this.update_list();
					}
				}
			}
		}

		private void StatBlockBtn_Click(object sender, EventArgs e)
		{
			if (this.Parcel != null && this.Parcel.MagicItemID != Guid.Empty)
			{
				MagicItem magicItem = Session.FindMagicItem(this.Parcel.MagicItemID, SearchType.Global);
				if (magicItem != null)
				{
					MagicItemDetailsForm magicItemDetailsForm = new MagicItemDetailsForm(magicItem);
					magicItemDetailsForm.ShowDialog();
				}
			}
		}

		private void RandomiseBtn_Click(object sender, EventArgs e)
		{
			if (this.Parcel != null)
			{
				this.randomise(this.Parcel);
				this.update_list();
			}
		}

		private void RandomiseAllBtn_Click(object sender, EventArgs e)
		{
			foreach (Parcel current in Session.Project.TreasureParcels)
			{
				this.randomise(current);
			}
			this.update_list();
		}

		private void randomise(Parcel parcel)
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
			else
			{
				parcel.Details = Treasure.RandomMundaneItem(parcel.Value);
			}
		}

		private void update_list()
		{
			this.ParcelList.Items.Clear();
			List<Parcel> treasureParcels = Session.Project.TreasureParcels;
			foreach (Parcel current in treasureParcels)
			{
				string text = (current.Name != "") ? current.Name : "(undefined parcel)";
				ListViewItem listViewItem = this.ParcelList.Items.Add(text);
				listViewItem.SubItems.Add(current.Details);
				listViewItem.Tag = current;
				int index = (current.MagicItemID != Guid.Empty) ? 0 : 1;
				listViewItem.Group = this.ParcelList.Groups[index];
			}
			this.ParcelList.Sort();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}

		private void ParcelSelectForm_FormClosed(object sender, FormClosedEventArgs e)
		{
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
			ListViewGroup listViewGroup = new ListViewGroup("Magic Items", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Mundane Parcels", HorizontalAlignment.Left);
			ComponentResourceManager resources = new ComponentResourceManager(typeof(ParcelSelectForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.NameHdr = new ColumnHeader();
			this.DetailsHdr = new ColumnHeader();
			this.ParcelList = new ListView();
			this.ListPanel = new Panel();
			this.Toolbar = new ToolStrip();
			this.ChangeItemBtn = new ToolStripButton();
			this.StatBlockBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.RandomiseAllBtn = new ToolStripButton();
			this.RandomiseBtn = new ToolStripButton();
			this.ListPanel.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(318, 348);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(399, 348);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.NameHdr.Text = "Parcel";
			this.NameHdr.Width = 150;
			this.DetailsHdr.Text = "Details";
			this.DetailsHdr.Width = 275;
			this.ParcelList.Columns.AddRange(new ColumnHeader[]
			{
				this.NameHdr,
				this.DetailsHdr
			});
			this.ParcelList.Dock = DockStyle.Fill;
			this.ParcelList.FullRowSelect = true;
			listViewGroup.Header = "Magic Items";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup2.Header = "Mundane Parcels";
			listViewGroup2.Name = "listViewGroup2";
			this.ParcelList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2
			});
			this.ParcelList.HideSelection = false;
			this.ParcelList.Location = new Point(0, 25);
			this.ParcelList.MultiSelect = false;
			this.ParcelList.Name = "ParcelList";
			this.ParcelList.Size = new Size(462, 305);
			this.ParcelList.TabIndex = 0;
			this.ParcelList.UseCompatibleStateImageBehavior = false;
			this.ParcelList.View = View.Details;
			this.ParcelList.DoubleClick += new EventHandler(this.TileList_DoubleClick);
			this.ListPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.ListPanel.Controls.Add(this.ParcelList);
			this.ListPanel.Controls.Add(this.Toolbar);
			this.ListPanel.Location = new Point(12, 12);
			this.ListPanel.Name = "ListPanel";
			this.ListPanel.Size = new Size(462, 330);
			this.ListPanel.TabIndex = 5;
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.ChangeItemBtn,
				this.StatBlockBtn,
				this.toolStripSeparator1,
				this.RandomiseBtn,
				this.RandomiseAllBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(462, 25);
			this.Toolbar.TabIndex = 1;
			this.Toolbar.Text = "toolStrip1";
			this.ChangeItemBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChangeItemBtn.Image = (Image)resources.GetObject("ChangeItemBtn.Image");
			this.ChangeItemBtn.ImageTransparentColor = Color.Magenta;
			this.ChangeItemBtn.Name = "ChangeItemBtn";
			this.ChangeItemBtn.Size = new Size(115, 22);
			this.ChangeItemBtn.Text = "Change Magic Item";
			this.ChangeItemBtn.Click += new EventHandler(this.ChangeItemBtn_Click);
			this.StatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.StatBlockBtn.Image = (Image)resources.GetObject("StatBlockBtn.Image");
			this.StatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.StatBlockBtn.Name = "StatBlockBtn";
			this.StatBlockBtn.Size = new Size(63, 22);
			this.StatBlockBtn.Text = "Stat Block";
			this.StatBlockBtn.Click += new EventHandler(this.StatBlockBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.RandomiseAllBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RandomiseAllBtn.Image = (Image)resources.GetObject("RandomiseAllBtn.Image");
			this.RandomiseAllBtn.ImageTransparentColor = Color.Magenta;
			this.RandomiseAllBtn.Name = "RandomiseAllBtn";
			this.RandomiseAllBtn.Size = new Size(87, 22);
			this.RandomiseAllBtn.Text = "Randomise All";
			this.RandomiseAllBtn.Click += new EventHandler(this.RandomiseAllBtn_Click);
			this.RandomiseBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RandomiseBtn.Image = (Image)resources.GetObject("RandomiseBtn.Image");
			this.RandomiseBtn.ImageTransparentColor = Color.Magenta;
			this.RandomiseBtn.Name = "RandomiseBtn";
			this.RandomiseBtn.Size = new Size(70, 22);
			this.RandomiseBtn.Text = "Randomise";
			this.RandomiseBtn.Click += new EventHandler(this.RandomiseBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(486, 383);
			base.Controls.Add(this.ListPanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ParcelSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select a Treasure Parcel";
			base.FormClosed += new FormClosedEventHandler(this.ParcelSelectForm_FormClosed);
			this.ListPanel.ResumeLayout(false);
			this.ListPanel.PerformLayout();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
