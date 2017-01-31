using Masterplan.Data;
using Masterplan.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class TrapElementPanel : UserControl
	{
		private IContainer components;

		private ToolStrip Toolbar;

		private ToolStripButton EditBtn;

		private ToolStripButton ChooseBtn;

		private ListView TrapList;

		private ColumnHeader InfoHdr;

		private ToolStripButton LocationBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton AddLibraryBtn;

		private TrapElement fTrapElement;

		public TrapElement Trap
		{
			get
			{
				return this.fTrapElement;
			}
			set
			{
				this.fTrapElement = value;
				this.update_view();
			}
		}

		public TrapSkillData SelectedSkill
		{
			get
			{
				if (this.TrapList.SelectedItems.Count != 0)
				{
					return this.TrapList.SelectedItems[0].Tag as TrapSkillData;
				}
				return null;
			}
		}

		public string SelectedCountermeasure
		{
			get
			{
				if (this.TrapList.SelectedItems.Count != 0)
				{
					return this.TrapList.SelectedItems[0].Tag as string;
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
			ListViewGroup listViewGroup = new ListViewGroup("Info", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Skills", HorizontalAlignment.Left);
			ListViewGroup listViewGroup3 = new ListViewGroup("Countermeasures", HorizontalAlignment.Left);
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(TrapElementPanel));
			this.Toolbar = new ToolStrip();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.TrapList = new ListView();
			this.InfoHdr = new ColumnHeader();
			this.EditBtn = new ToolStripButton();
			this.LocationBtn = new ToolStripButton();
			this.ChooseBtn = new ToolStripButton();
			this.AddLibraryBtn = new ToolStripButton();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.EditBtn,
				this.LocationBtn,
				this.toolStripSeparator1,
				this.ChooseBtn,
				this.toolStripSeparator2,
				this.AddLibraryBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(561, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 25);
			this.TrapList.Columns.AddRange(new ColumnHeader[]
			{
				this.InfoHdr
			});
			this.TrapList.Dock = DockStyle.Fill;
			this.TrapList.FullRowSelect = true;
			listViewGroup.Header = "Info";
			listViewGroup.Name = "listViewGroup3";
			listViewGroup2.Header = "Skills";
			listViewGroup2.Name = "listViewGroup1";
			listViewGroup3.Header = "Countermeasures";
			listViewGroup3.Name = "listViewGroup2";
			this.TrapList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2,
				listViewGroup3
			});
			this.TrapList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TrapList.HideSelection = false;
			this.TrapList.Location = new Point(0, 25);
			this.TrapList.MultiSelect = false;
			this.TrapList.Name = "TrapList";
			this.TrapList.Size = new Size(561, 145);
			this.TrapList.TabIndex = 1;
			this.TrapList.UseCompatibleStateImageBehavior = false;
			this.TrapList.View = View.Details;
			this.TrapList.DoubleClick += new EventHandler(this.TrapList_DoubleClick);
			this.InfoHdr.Text = "Info";
			this.InfoHdr.Width = 448;
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)componentResourceManager.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new Size(106, 22);
			this.EditBtn.Text = "Edit Trap / Hazard";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			this.LocationBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LocationBtn.Image = (Image)componentResourceManager.GetObject("LocationBtn.Image");
			this.LocationBtn.ImageTransparentColor = Color.Magenta;
			this.LocationBtn.Name = "LocationBtn";
			this.LocationBtn.Size = new Size(103, 22);
			this.LocationBtn.Text = "Set Map Location";
			this.LocationBtn.Click += new EventHandler(this.LocationBtn_Click);
			this.ChooseBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChooseBtn.Image = (Image)componentResourceManager.GetObject("ChooseBtn.Image");
			this.ChooseBtn.ImageTransparentColor = Color.Magenta;
			this.ChooseBtn.Name = "ChooseBtn";
			this.ChooseBtn.Size = new Size(155, 22);
			this.ChooseBtn.Text = "Use Standard Trap / Hazard";
			this.ChooseBtn.Click += new EventHandler(this.ChooseBtn_Click);
			this.AddLibraryBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddLibraryBtn.Image = (Image)componentResourceManager.GetObject("AddLibraryBtn.Image");
			this.AddLibraryBtn.ImageTransparentColor = Color.Magenta;
			this.AddLibraryBtn.Name = "AddLibraryBtn";
			this.AddLibraryBtn.Size = new Size(86, 22);
			this.AddLibraryBtn.Text = "Add to Library";
			this.AddLibraryBtn.Click += new EventHandler(this.AddLibraryBtn_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.TrapList);
			base.Controls.Add(this.Toolbar);
			base.Name = "TrapElementPanel";
			base.Size = new Size(561, 170);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public TrapElementPanel()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.update_view();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.ChooseBtn.Enabled = (Session.Traps.Count != 0);
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			TrapBuilderForm trapBuilderForm = new TrapBuilderForm(this.fTrapElement.Trap);
			if (trapBuilderForm.ShowDialog() == DialogResult.OK)
			{
				this.fTrapElement.Trap = trapBuilderForm.Trap;
				this.update_view();
			}
		}

		private void LocationBtn_Click(object sender, EventArgs e)
		{
			MapAreaSelectForm mapAreaSelectForm = new MapAreaSelectForm(this.fTrapElement.MapID, this.fTrapElement.MapAreaID);
			if (mapAreaSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fTrapElement.MapID = ((mapAreaSelectForm.Map != null) ? mapAreaSelectForm.Map.ID : Guid.Empty);
				this.fTrapElement.MapAreaID = ((mapAreaSelectForm.MapArea != null) ? mapAreaSelectForm.MapArea.ID : Guid.Empty);
				this.update_view();
			}
		}

		private void ChooseBtn_Click(object sender, EventArgs e)
		{
			TrapSelectForm trapSelectForm = new TrapSelectForm();
			if (trapSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fTrapElement.Trap = trapSelectForm.Trap.Copy();
				this.update_view();
			}
		}

		private void TrapList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedSkill != null)
			{
				int index = this.fTrapElement.Trap.Skills.IndexOf(this.SelectedSkill);
				TrapSkillForm trapSkillForm = new TrapSkillForm(this.SelectedSkill, this.fTrapElement.Trap.Level);
				if (trapSkillForm.ShowDialog() == DialogResult.OK)
				{
					this.fTrapElement.Trap.Skills[index] = trapSkillForm.SkillData;
					this.update_view();
				}
			}
			if (this.SelectedCountermeasure != null)
			{
				int index2 = this.fTrapElement.Trap.Countermeasures.IndexOf(this.SelectedCountermeasure);
				TrapCountermeasureForm trapCountermeasureForm = new TrapCountermeasureForm(this.SelectedCountermeasure, this.fTrapElement.Trap.Level);
				if (trapCountermeasureForm.ShowDialog() == DialogResult.OK)
				{
					this.fTrapElement.Trap.Countermeasures[index2] = trapCountermeasureForm.Countermeasure;
					this.update_view();
				}
			}
		}

		private void update_view()
		{
			this.TrapList.Items.Clear();
			if (this.fTrapElement == null)
			{
				return;
			}
			ListViewItem listViewItem = this.TrapList.Items.Add(string.Concat(new object[]
			{
				this.fTrapElement.Trap.Name,
				": ",
				this.fTrapElement.GetXP(),
				" XP"
			}));
			listViewItem.Group = this.TrapList.Groups[0];
			ListViewItem listViewItem2 = this.TrapList.Items.Add(this.fTrapElement.Trap.Info);
			listViewItem2.Group = this.TrapList.Groups[0];
			if (this.fTrapElement.MapID != Guid.Empty)
			{
				Map map = Session.Project.FindTacticalMap(this.fTrapElement.MapID);
				MapArea mapArea = map.FindArea(this.fTrapElement.MapAreaID);
				string text = "Location: " + map.Name;
				if (mapArea != null)
				{
					text = text + " (" + mapArea.Name + ")";
				}
				ListViewItem listViewItem3 = this.TrapList.Items.Add(text);
				listViewItem3.Group = this.TrapList.Groups[0];
			}
			foreach (TrapSkillData current in this.fTrapElement.Trap.Skills)
			{
				ListViewItem listViewItem4 = this.TrapList.Items.Add(current.ToString());
				listViewItem4.Group = this.TrapList.Groups[1];
				listViewItem4.Tag = current;
			}
			if (this.fTrapElement.Trap.Skills.Count == 0)
			{
				ListViewItem listViewItem5 = this.TrapList.Items.Add("(no skills)");
				listViewItem5.Group = this.TrapList.Groups[1];
				listViewItem5.ForeColor = SystemColors.GrayText;
			}
			foreach (string current2 in this.fTrapElement.Trap.Countermeasures)
			{
				ListViewItem listViewItem6 = this.TrapList.Items.Add(current2);
				listViewItem6.Group = this.TrapList.Groups[2];
				listViewItem6.Tag = current2;
			}
			if (this.fTrapElement.Trap.Countermeasures.Count == 0)
			{
				ListViewItem listViewItem7 = this.TrapList.Items.Add("(no countermeasures)");
				listViewItem7.Group = this.TrapList.Groups[2];
				listViewItem7.ForeColor = SystemColors.GrayText;
			}
		}

		private void AddLibraryBtn_Click(object sender, EventArgs e)
		{
			LibrarySelectForm librarySelectForm = new LibrarySelectForm();
			if (librarySelectForm.ShowDialog() == DialogResult.OK)
			{
				Library selectedLibrary = librarySelectForm.SelectedLibrary;
				selectedLibrary.Traps.Add(this.fTrapElement.Trap.Copy());
			}
		}
	}
}
