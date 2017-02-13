using Masterplan.Controls;
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
	internal class MagicItemSelectForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private SplitContainer Splitter;

		private ListView ItemList;

		private ColumnHeader NameHdr;

		private ColumnHeader InfoHdr;

		private Panel BrowserPanel;

		private WebBrowser Browser;

		private LevelRangePanel LevelRangePanel;

		public MagicItem MagicItem
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
			this.CancelBtn = new Button();
			this.Splitter = new SplitContainer();
			this.ItemList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.LevelRangePanel = new LevelRangePanel();
			this.BrowserPanel = new Panel();
			this.Browser = new WebBrowser();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
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
			this.Splitter.Panel1.Controls.Add(this.ItemList);
			this.Splitter.Panel1.Controls.Add(this.LevelRangePanel);
			this.Splitter.Panel2.Controls.Add(this.BrowserPanel);
			this.Splitter.Size = new Size(693, 336);
			this.Splitter.SplitterDistance = 330;
			this.Splitter.TabIndex = 5;
			this.ItemList.Columns.AddRange(new ColumnHeader[]
			{
				this.NameHdr,
				this.InfoHdr
			});
			this.ItemList.Dock = DockStyle.Fill;
			this.ItemList.FullRowSelect = true;
			this.ItemList.HideSelection = false;
			this.ItemList.Location = new Point(0, 80);
			this.ItemList.MultiSelect = false;
			this.ItemList.Name = "ItemList";
			this.ItemList.Size = new Size(330, 256);
			this.ItemList.Sorting = SortOrder.Ascending;
			this.ItemList.TabIndex = 1;
			this.ItemList.UseCompatibleStateImageBehavior = false;
			this.ItemList.View = View.Details;
			this.ItemList.SelectedIndexChanged += new EventHandler(this.ItemList_SelectedIndexChanged);
			this.ItemList.DoubleClick += new EventHandler(this.ItemList_DoubleClick);
			this.NameHdr.Text = "Magic Item";
			this.NameHdr.Width = 150;
			this.InfoHdr.Text = "Info";
			this.InfoHdr.Width = 150;
			this.LevelRangePanel.Dock = DockStyle.Top;
			this.LevelRangePanel.Location = new Point(0, 0);
			this.LevelRangePanel.Name = "LevelRangePanel";
			this.LevelRangePanel.Size = new Size(330, 80);
			this.LevelRangePanel.TabIndex = 2;
			this.LevelRangePanel.RangeChanged += new EventHandler(this.LevelRangePanel_RangeChanged);
			this.BrowserPanel.BorderStyle = BorderStyle.FixedSingle;
			this.BrowserPanel.Controls.Add(this.Browser);
			this.BrowserPanel.Dock = DockStyle.Fill;
			this.BrowserPanel.Location = new Point(0, 0);
			this.BrowserPanel.Name = "BrowserPanel";
			this.BrowserPanel.Size = new Size(359, 336);
			this.BrowserPanel.TabIndex = 0;
			this.Browser.AllowWebBrowserDrop = false;
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.IsWebBrowserContextMenuEnabled = false;
			this.Browser.Location = new Point(0, 0);
			this.Browser.MinimumSize = new Size(20, 20);
			this.Browser.Name = "Browser";
			this.Browser.ScriptErrorsSuppressed = true;
			this.Browser.Size = new Size(357, 334);
			this.Browser.TabIndex = 0;
			this.Browser.WebBrowserShortcutsEnabled = false;
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
			base.Name = "MagicItemSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select a Magic Item";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.BrowserPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public MagicItemSelectForm(int level)
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			if (level > 0)
			{
				this.LevelRangePanel.SetLevelRange(level, level);
			}
			this.Browser.DocumentText = "";
			this.ItemList_SelectedIndexChanged(null, null);
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = (this.MagicItem != null);
		}

		private void ItemList_SelectedIndexChanged(object sender, EventArgs e)
		{
			string text = HTML.MagicItem(this.MagicItem, DisplaySize.Small, false, true);
			this.Browser.Document.OpenNew(true);
			this.Browser.Document.Write(text);
		}

		private void ItemList_DoubleClick(object sender, EventArgs e)
		{
			if (this.MagicItem != null)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		private void LevelRangePanel_RangeChanged(object sender, EventArgs e)
		{
			this.update_list();
		}

		private void update_list()
		{
			List<MagicItem> list = new List<MagicItem>();
			List<MagicItem> magicItems = Session.MagicItems;
			foreach (MagicItem current in magicItems)
			{
				if (current.Level >= this.LevelRangePanel.MinimumLevel && current.Level <= this.LevelRangePanel.MaximumLevel && this.match(current, this.LevelRangePanel.NameQuery))
				{
					list.Add(current);
				}
			}
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (MagicItem current2 in list)
			{
				if (current2.Type != "")
				{
					binarySearchTree.Add(current2.Type);
				}
			}
			List<string> sortedList = binarySearchTree.SortedList;
			sortedList.Add("Miscellaneous Items");
			foreach (string current3 in sortedList)
			{
				this.ItemList.Groups.Add(current3, current3);
			}
			List<ListViewItem> list2 = new List<ListViewItem>();
			foreach (MagicItem current4 in list)
			{
				ListViewItem listViewItem = new ListViewItem(current4.Name);
				listViewItem.SubItems.Add(current4.Info);
				listViewItem.Tag = current4;
				if (current4.Type != "")
				{
					listViewItem.Group = this.ItemList.Groups[current4.Type];
				}
				else
				{
					listViewItem.Group = this.ItemList.Groups["Miscellaneous Items"];
				}
				list2.Add(listViewItem);
			}
			this.ItemList.BeginUpdate();
			this.ItemList.Items.Clear();
			this.ItemList.Items.AddRange(list2.ToArray());
			this.ItemList.EndUpdate();
		}

		private bool match(MagicItem item, string query)
		{
			string[] array = query.ToLower().Split(new char[0]);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string token = array2[i];
				if (!this.match_token(item, token))
				{
					return false;
				}
			}
			return true;
		}

		private bool match_token(MagicItem item, string token)
		{
			return item.Name.ToLower().Contains(token);
		}
	}
}
