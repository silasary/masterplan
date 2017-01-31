using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class RegionalMapListForm : Form
	{
		private IContainer components;

		private ListView MapList;

		private ColumnHeader MapHdr;

		private ToolStrip ListToolbar;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private SplitContainer Splitter;

		private ToolStrip MapToolbar;

		private ToolStripDropDownButton ToolsMenu;

		private ToolStripMenuItem ToolsScreenshot;

		private ToolStripMenuItem ToolsPlayerView;

		private Button CloseBtn;

		private RegionalMapPanel MapPanel;

		private ToolStripSplitButton AddBtn;

		private ToolStripMenuItem AddImportProject;

		private ToolStripDropDownButton LocationMenu;

		private ToolStripMenuItem LocationEntry;

		public RegionalMap SelectedMap
		{
			get
			{
				if (this.MapList.SelectedItems.Count != 0)
				{
					return this.MapList.SelectedItems[0].Tag as RegionalMap;
				}
				return null;
			}
			set
			{
				this.MapList.SelectedItems.Clear();
				foreach (ListViewItem listViewItem in this.MapList.Items)
				{
					if (listViewItem.Tag == value)
					{
						listViewItem.Selected = true;
					}
				}
			}
		}

		public RegionalMapListForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.update_maps();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = (this.SelectedMap != null);
			this.EditBtn.Enabled = (this.SelectedMap != null);
			this.LocationMenu.Enabled = (this.MapPanel.SelectedLocation != null);
			this.ToolsMenu.Enabled = (this.SelectedMap != null);
		}

		private void AddBtn_Click(object sender, EventArgs e)
		{
			RegionalMapForm regionalMapForm = new RegionalMapForm(new RegionalMap
			{
				Name = "New Map"
			}, null);
			if (regionalMapForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.RegionalMaps.Add(regionalMapForm.Map);
				Session.Modified = true;
				this.update_maps();
				this.SelectedMap = regionalMapForm.Map;
			}
		}

		private void AddImportProject_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = Program.ProjectFilter;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Project project = Serialisation<Project>.Load(openFileDialog.FileName, SerialisationMode.Binary);
				if (project != null)
				{
					RegionalMapSelectForm regionalMapSelectForm = new RegionalMapSelectForm(project.RegionalMaps, null, true);
					if (regionalMapSelectForm.ShowDialog(this) != DialogResult.OK)
					{
						return;
					}
					Session.Project.RegionalMaps.AddRange(regionalMapSelectForm.Maps);
					Session.Modified = true;
					this.update_maps();
				}
			}
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMap != null)
			{
				string text = "Are you sure you want to delete this map?";
				DialogResult dialogResult = MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.No)
				{
					return;
				}
				Session.Project.RegionalMaps.Remove(this.SelectedMap);
				Session.Modified = true;
				this.update_maps();
			}
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMap != null)
			{
				int index = Session.Project.RegionalMaps.IndexOf(this.SelectedMap);
				RegionalMapForm regionalMapForm = new RegionalMapForm(this.SelectedMap, null);
				if (regionalMapForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.RegionalMaps[index] = regionalMapForm.Map;
					Session.Modified = true;
					this.update_maps();
					this.SelectedMap = regionalMapForm.Map;
				}
			}
		}

		private void LocationEntry_Click(object sender, EventArgs e)
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

		private void ToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedMap != null)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.FileName = this.SelectedMap.Name;
				saveFileDialog.Filter = "Bitmap Image |*.bmp|JPEG Image|*.jpg|GIF Image|*.gif|PNG Image|*.png";
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					ImageFormat format = ImageFormat.Bmp;
					switch (saveFileDialog.FilterIndex)
					{
					case 1:
						format = ImageFormat.Bmp;
						break;
					case 2:
						format = ImageFormat.Jpeg;
						break;
					case 3:
						format = ImageFormat.Gif;
						break;
					case 4:
						format = ImageFormat.Png;
						break;
					}
					this.MapPanel.Map.Image.Save(saveFileDialog.FileName, format);
				}
			}
		}

		private void ToolsPlayerView_Click(object sender, EventArgs e)
		{
			if (this.SelectedMap != null)
			{
				if (Session.PlayerView == null)
				{
					Session.PlayerView = new PlayerViewForm(this);
				}
				Session.PlayerView.ShowRegionalMap(this.MapPanel);
			}
		}

		private void MapList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.MapPanel.Map = this.SelectedMap;
		}

		private void update_maps()
		{
			this.MapList.Items.Clear();
			foreach (RegionalMap current in Session.Project.RegionalMaps)
			{
				ListViewItem listViewItem = this.MapList.Items.Add(current.Name);
				listViewItem.Tag = current;
			}
			if (this.MapList.Items.Count == 0)
			{
				ListViewItem listViewItem2 = this.MapList.Items.Add("(no maps)");
				listViewItem2.ForeColor = SystemColors.GrayText;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(RegionalMapListForm));
			this.MapList = new ListView();
			this.MapHdr = new ColumnHeader();
			this.ListToolbar = new ToolStrip();
			this.AddBtn = new ToolStripSplitButton();
			this.AddImportProject = new ToolStripMenuItem();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.Splitter = new SplitContainer();
			this.MapPanel = new RegionalMapPanel();
			this.MapToolbar = new ToolStrip();
			this.LocationMenu = new ToolStripDropDownButton();
			this.LocationEntry = new ToolStripMenuItem();
			this.ToolsMenu = new ToolStripDropDownButton();
			this.ToolsScreenshot = new ToolStripMenuItem();
			this.ToolsPlayerView = new ToolStripMenuItem();
			this.CloseBtn = new Button();
			this.ListToolbar.SuspendLayout();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.MapToolbar.SuspendLayout();
			base.SuspendLayout();
			this.MapList.Columns.AddRange(new ColumnHeader[]
			{
				this.MapHdr
			});
			this.MapList.Dock = DockStyle.Fill;
			this.MapList.FullRowSelect = true;
			this.MapList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.MapList.HideSelection = false;
			this.MapList.Location = new Point(0, 25);
			this.MapList.MultiSelect = false;
			this.MapList.Name = "MapList";
			this.MapList.Size = new Size(205, 374);
			this.MapList.TabIndex = 1;
			this.MapList.UseCompatibleStateImageBehavior = false;
			this.MapList.View = View.Details;
			this.MapList.SelectedIndexChanged += new EventHandler(this.MapList_SelectedIndexChanged);
			this.MapList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.MapHdr.Text = "Map";
			this.MapHdr.Width = 172;
			this.ListToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.AddBtn,
				this.RemoveBtn,
				this.EditBtn
			});
			this.ListToolbar.Location = new Point(0, 0);
			this.ListToolbar.Name = "ListToolbar";
			this.ListToolbar.Size = new Size(205, 25);
			this.ListToolbar.TabIndex = 0;
			this.ListToolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.AddImportProject
			});
			this.AddBtn.Image = (Image)componentResourceManager.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new Size(45, 22);
			this.AddBtn.Text = "Add";
			this.AddBtn.ButtonClick += new EventHandler(this.AddBtn_Click);
			this.AddImportProject.Name = "AddImportProject";
			this.AddImportProject.Size = new Size(209, 22);
			this.AddImportProject.Text = "Import from Project File...";
			this.AddImportProject.Click += new EventHandler(this.AddImportProject_Click);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)componentResourceManager.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)componentResourceManager.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new Size(31, 22);
			this.EditBtn.Text = "Edit";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			this.Splitter.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Splitter.FixedPanel = FixedPanel.Panel1;
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.MapList);
			this.Splitter.Panel1.Controls.Add(this.ListToolbar);
			this.Splitter.Panel2.Controls.Add(this.MapPanel);
			this.Splitter.Panel2.Controls.Add(this.MapToolbar);
			this.Splitter.Size = new Size(726, 399);
			this.Splitter.SplitterDistance = 205;
			this.Splitter.TabIndex = 11;
			this.MapPanel.AllowEditing = false;
			this.MapPanel.BorderStyle = BorderStyle.FixedSingle;
			this.MapPanel.Dock = DockStyle.Fill;
			this.MapPanel.HighlightedLocation = null;
			this.MapPanel.Location = new Point(0, 25);
			this.MapPanel.Map = null;
			this.MapPanel.Mode = MapViewMode.Thumbnail;
			this.MapPanel.Name = "MapPanel";
			this.MapPanel.Plot = null;
			this.MapPanel.SelectedLocation = null;
			this.MapPanel.ShowLocations = true;
			this.MapPanel.Size = new Size(517, 374);
			this.MapPanel.TabIndex = 2;
			this.MapPanel.DoubleClick += new EventHandler(this.LocationEntry_Click);
			this.MapToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.LocationMenu,
				this.ToolsMenu
			});
			this.MapToolbar.Location = new Point(0, 0);
			this.MapToolbar.Name = "MapToolbar";
			this.MapToolbar.Size = new Size(517, 25);
			this.MapToolbar.TabIndex = 1;
			this.MapToolbar.Text = "toolStrip1";
			this.LocationMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LocationMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.LocationEntry
			});
			this.LocationMenu.Image = (Image)componentResourceManager.GetObject("LocationMenu.Image");
			this.LocationMenu.ImageTransparentColor = Color.Magenta;
			this.LocationMenu.Name = "LocationMenu";
			this.LocationMenu.Size = new Size(66, 22);
			this.LocationMenu.Text = "Location";
			this.LocationEntry.Name = "LocationEntry";
			this.LocationEntry.Size = new Size(183, 22);
			this.LocationEntry.Text = "Encyclopedia Entry...";
			this.LocationEntry.Click += new EventHandler(this.LocationEntry_Click);
			this.ToolsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ToolsMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ToolsScreenshot,
				this.ToolsPlayerView
			});
			this.ToolsMenu.Image = (Image)componentResourceManager.GetObject("ToolsMenu.Image");
			this.ToolsMenu.ImageTransparentColor = Color.Magenta;
			this.ToolsMenu.Name = "ToolsMenu";
			this.ToolsMenu.Size = new Size(49, 22);
			this.ToolsMenu.Text = "Tools";
			this.ToolsScreenshot.Name = "ToolsScreenshot";
			this.ToolsScreenshot.Size = new Size(177, 22);
			this.ToolsScreenshot.Text = "Export Map";
			this.ToolsScreenshot.Click += new EventHandler(this.ToolsExport_Click);
			this.ToolsPlayerView.Name = "ToolsPlayerView";
			this.ToolsPlayerView.Size = new Size(177, 22);
			this.ToolsPlayerView.Text = "Send to Player View";
			this.ToolsPlayerView.Click += new EventHandler(this.ToolsPlayerView_Click);
			this.CloseBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CloseBtn.DialogResult = DialogResult.OK;
			this.CloseBtn.Location = new Point(663, 417);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new Size(75, 23);
			this.CloseBtn.TabIndex = 12;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(750, 452);
			base.Controls.Add(this.CloseBtn);
			base.Controls.Add(this.Splitter);
			base.MinimizeBox = false;
			base.Name = "RegionalMapListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Regional Maps";
			this.ListToolbar.ResumeLayout(false);
			this.ListToolbar.PerformLayout();
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel1.PerformLayout();
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.Panel2.PerformLayout();
			this.Splitter.ResumeLayout(false);
			this.MapToolbar.ResumeLayout(false);
			this.MapToolbar.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
