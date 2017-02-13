using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class CategoryListForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private ListView CatList;

		private ColumnHeader CatHdr;

		private Panel ListPanel;

		private ToolStrip Toolbar;

		private ToolStripButton SelectBtn;

		private ToolStripButton DeselectBtn;

		public List<string> Categories
		{
			get
			{
				if (this.CatList.CheckedItems.Count == this.CatList.Items.Count)
				{
					return null;
				}
				List<string> list = new List<string>();
				foreach (ListViewItem listViewItem in this.CatList.CheckedItems)
				{
					list.Add(listViewItem.Text);
				}
				return list;
			}
		}

		public CategoryListForm(List<string> categories)
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (Creature current in Session.Creatures)
			{
				if (current.Category != "")
				{
					binarySearchTree.Add(current.Category);
				}
			}
			List<string> sortedList = binarySearchTree.SortedList;
			List<string> list = new List<string>();
			foreach (string current2 in sortedList)
			{
				string item = current2.Substring(0, 1);
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			foreach (string current3 in list)
			{
				this.CatList.Groups.Add(current3, current3);
			}
			foreach (string current4 in sortedList)
			{
				string key = current4.Substring(0, 1);
				ListViewItem listViewItem = this.CatList.Items.Add(current4);
				listViewItem.Checked = (categories == null || categories.Contains(current4));
				listViewItem.Group = this.CatList.Groups[key];
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = (this.CatList.CheckedItems.Count != 0);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}

		private void SelectBtn_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem listViewItem in this.CatList.Items)
			{
				listViewItem.Checked = true;
			}
		}

		private void DeselectBtn_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem listViewItem in this.CatList.Items)
			{
				listViewItem.Checked = false;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(CategoryListForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.CatList = new ListView();
			this.CatHdr = new ColumnHeader();
			this.ListPanel = new Panel();
			this.Toolbar = new ToolStrip();
			this.SelectBtn = new ToolStripButton();
			this.DeselectBtn = new ToolStripButton();
			this.ListPanel.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(93, 295);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(174, 295);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.CatList.CheckBoxes = true;
			this.CatList.Columns.AddRange(new ColumnHeader[]
			{
				this.CatHdr
			});
			this.CatList.Dock = DockStyle.Fill;
			this.CatList.FullRowSelect = true;
			this.CatList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.CatList.HideSelection = false;
			this.CatList.Location = new Point(0, 25);
			this.CatList.MultiSelect = false;
			this.CatList.Name = "CatList";
			this.CatList.Size = new Size(237, 252);
			this.CatList.TabIndex = 1;
			this.CatList.UseCompatibleStateImageBehavior = false;
			this.CatList.View = View.Details;
			this.CatHdr.Text = "Category";
			this.CatHdr.Width = 200;
			this.ListPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.ListPanel.Controls.Add(this.CatList);
			this.ListPanel.Controls.Add(this.Toolbar);
			this.ListPanel.Location = new Point(12, 12);
			this.ListPanel.Name = "ListPanel";
			this.ListPanel.Size = new Size(237, 277);
			this.ListPanel.TabIndex = 0;
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.SelectBtn,
				this.DeselectBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(237, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.SelectBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SelectBtn.Image = (Image)resources.GetObject("SelectBtn.Image");
			this.SelectBtn.ImageTransparentColor = Color.Magenta;
			this.SelectBtn.Name = "SelectBtn";
			this.SelectBtn.Size = new Size(59, 22);
			this.SelectBtn.Text = "Select All";
			this.SelectBtn.Click += new EventHandler(this.SelectBtn_Click);
			this.DeselectBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.DeselectBtn.Image = (Image)resources.GetObject("DeselectBtn.Image");
			this.DeselectBtn.ImageTransparentColor = Color.Magenta;
			this.DeselectBtn.Name = "DeselectBtn";
			this.DeselectBtn.Size = new Size(72, 22);
			this.DeselectBtn.Text = "Deselect All";
			this.DeselectBtn.Click += new EventHandler(this.DeselectBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(261, 330);
			base.Controls.Add(this.ListPanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CategoryListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Categories";
			this.ListPanel.ResumeLayout(false);
			this.ListPanel.PerformLayout();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
