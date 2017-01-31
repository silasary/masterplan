using Masterplan.Data;
using Masterplan.Tools.Generators;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils.Wizards;

namespace Masterplan.Wizards
{
	internal class MapLibrariesPage : UserControl, IWizardPage
	{
		private MapBuilderData fData;

		private IContainer components;

		private Label InfoLbl;

		private ListView LibraryList;

		private ColumnHeader LibHdr;

		private ToolStrip Toolbar;

		private ToolStripButton SelectAllBtn;

		private ToolStripButton DeselectAllBtn;

		private LinkLabel InfoLinkLbl;

		public bool AllowNext
		{
			get
			{
				return this.LibraryList.CheckedItems.Count != 0;
			}
		}

		public bool AllowBack
		{
			get
			{
				return !this.fData.DelveOnly;
			}
		}

		public bool AllowFinish
		{
			get
			{
				return this.LibraryList.CheckedItems.Count != 0;
			}
		}

		public MapLibrariesPage()
		{
			this.InitializeComponent();
		}

		public void OnShown(object data)
		{
			if (this.fData == null)
			{
				this.fData = (data as MapBuilderData);
				this.LibraryList.Items.Clear();
				foreach (Library current in Session.Libraries)
				{
					if (current.ShowInAutoBuild)
					{
						ListViewItem listViewItem = this.LibraryList.Items.Add(current.Name);
						listViewItem.Checked = this.fData.Libraries.Contains(current);
						listViewItem.Tag = current;
					}
				}
				if (this.LibraryList.Items.Count == 0)
				{
					ListViewItem listViewItem2 = this.LibraryList.Items.Add("(no libraries)");
					listViewItem2.ForeColor = SystemColors.GrayText;
					this.LibraryList.CheckBoxes = false;
				}
			}
		}

		public bool OnBack()
		{
			return true;
		}

		public bool OnNext()
		{
			this.set_selected_libraries();
			return true;
		}

		public bool OnFinish()
		{
			this.set_selected_libraries();
			return true;
		}

		private void SelectAllBtn_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem listViewItem in this.LibraryList.Items)
			{
				listViewItem.Checked = true;
			}
		}

		private void DeselectAllBtn_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem listViewItem in this.LibraryList.Items)
			{
				listViewItem.Checked = false;
			}
		}

		private void set_selected_libraries()
		{
			this.fData.Libraries.Clear();
			foreach (ListViewItem listViewItem in this.LibraryList.CheckedItems)
			{
				Library item = listViewItem.Tag as Library;
				this.fData.Libraries.Add(item);
			}
		}

		private void InfoLinkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string text = "In order to be used with AutoBuild, map tiles need to be categorised (as doors, stairs, etc), so that they can be placed intelligently.";
			text += Environment.NewLine;
			text += Environment.NewLine;
			text += "Libraries which do not have categorised tiles cannot be used, and so are not shown in the list.";
			text += Environment.NewLine;
			text += Environment.NewLine;
			text += "You can set tile categories in the Libraries screen.";
			MessageBox.Show(this, text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MapLibrariesPage));
			this.InfoLbl = new Label();
			this.LibraryList = new ListView();
			this.LibHdr = new ColumnHeader();
			this.Toolbar = new ToolStrip();
			this.SelectAllBtn = new ToolStripButton();
			this.DeselectAllBtn = new ToolStripButton();
			this.InfoLinkLbl = new LinkLabel();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.InfoLbl.Dock = DockStyle.Top;
			this.InfoLbl.Location = new Point(0, 0);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(372, 40);
			this.InfoLbl.TabIndex = 1;
			this.InfoLbl.Text = "Select the libraries you want to use to create the map.";
			this.LibraryList.CheckBoxes = true;
			this.LibraryList.Columns.AddRange(new ColumnHeader[]
			{
				this.LibHdr
			});
			this.LibraryList.Dock = DockStyle.Fill;
			this.LibraryList.FullRowSelect = true;
			this.LibraryList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.LibraryList.HideSelection = false;
			this.LibraryList.Location = new Point(0, 65);
			this.LibraryList.MultiSelect = false;
			this.LibraryList.Name = "LibraryList";
			this.LibraryList.Size = new Size(372, 158);
			this.LibraryList.TabIndex = 2;
			this.LibraryList.UseCompatibleStateImageBehavior = false;
			this.LibraryList.View = View.Details;
			this.LibHdr.Text = "Library";
			this.LibHdr.Width = 300;
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.SelectAllBtn,
				this.DeselectAllBtn
			});
			this.Toolbar.Location = new Point(0, 40);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(372, 25);
			this.Toolbar.TabIndex = 3;
			this.Toolbar.Text = "toolStrip1";
			this.SelectAllBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SelectAllBtn.Image = (Image)componentResourceManager.GetObject("SelectAllBtn.Image");
			this.SelectAllBtn.ImageTransparentColor = Color.Magenta;
			this.SelectAllBtn.Name = "SelectAllBtn";
			this.SelectAllBtn.Size = new Size(59, 22);
			this.SelectAllBtn.Text = "Select All";
			this.SelectAllBtn.Click += new EventHandler(this.SelectAllBtn_Click);
			this.DeselectAllBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DeselectAllBtn.Image = (Image)componentResourceManager.GetObject("DeselectAllBtn.Image");
			this.DeselectAllBtn.ImageTransparentColor = Color.Magenta;
			this.DeselectAllBtn.Name = "DeselectAllBtn";
			this.DeselectAllBtn.Size = new Size(72, 22);
			this.DeselectAllBtn.Text = "Deselect All";
			this.DeselectAllBtn.Click += new EventHandler(this.DeselectAllBtn_Click);
			this.InfoLinkLbl.Dock = DockStyle.Bottom;
			this.InfoLinkLbl.Location = new Point(0, 223);
			this.InfoLinkLbl.Name = "InfoLinkLbl";
			this.InfoLinkLbl.Size = new Size(372, 23);
			this.InfoLinkLbl.TabIndex = 4;
			this.InfoLinkLbl.TabStop = true;
			this.InfoLinkLbl.Text = "Why are my libraries not shown?";
			this.InfoLinkLbl.TextAlign = ContentAlignment.MiddleLeft;
			this.InfoLinkLbl.LinkClicked += new LinkLabelLinkClickedEventHandler(this.InfoLinkLbl_LinkClicked);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.LibraryList);
			base.Controls.Add(this.Toolbar);
			base.Controls.Add(this.InfoLinkLbl);
			base.Controls.Add(this.InfoLbl);
			base.Name = "MapLibrariesPage";
			base.Size = new Size(372, 246);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
