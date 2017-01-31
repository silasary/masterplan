using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class ArtifactSelectForm : Form
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

		private Panel NamePanel;

		private TextBox NameBox;

		private Label NameLbl;

		public Artifact Artifact
		{
			get
			{
				if (this.ItemList.SelectedItems.Count != 0)
				{
					return this.ItemList.SelectedItems[0].Tag as Artifact;
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
			this.BrowserPanel = new Panel();
			this.Browser = new WebBrowser();
			this.NamePanel = new Panel();
			this.NameBox = new TextBox();
			this.NameLbl = new Label();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.BrowserPanel.SuspendLayout();
			this.NamePanel.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(549, 354);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(630, 354);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 1;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.Splitter.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.ItemList);
			this.Splitter.Panel1.Controls.Add(this.NamePanel);
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
			this.ItemList.Location = new Point(0, 27);
			this.ItemList.MultiSelect = false;
			this.ItemList.Name = "ItemList";
			this.ItemList.Size = new Size(330, 309);
			this.ItemList.Sorting = SortOrder.Ascending;
			this.ItemList.TabIndex = 1;
			this.ItemList.UseCompatibleStateImageBehavior = false;
			this.ItemList.View = View.Details;
			this.ItemList.SelectedIndexChanged += new EventHandler(this.ItemList_SelectedIndexChanged);
			this.ItemList.DoubleClick += new EventHandler(this.ItemList_DoubleClick);
			this.NameHdr.Text = "Artifact";
			this.NameHdr.Width = 150;
			this.InfoHdr.Text = "Info";
			this.InfoHdr.Width = 150;
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
			base.Name = "ArtifactSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select an Artifact";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.BrowserPanel.ResumeLayout(false);
			this.NamePanel.ResumeLayout(false);
			this.NamePanel.PerformLayout();
			base.ResumeLayout(false);
		}

		public ArtifactSelectForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.Browser.DocumentText = "";
			this.ItemList_SelectedIndexChanged(null, null);
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = (this.Artifact != null);
		}

		private void NameBox_TextChanged(object sender, EventArgs e)
		{
			this.update_list();
		}

		private void ItemList_SelectedIndexChanged(object sender, EventArgs e)
		{
			string text = HTML.Artifact(this.Artifact, DisplaySize.Small, false, true);
			this.Browser.Document.OpenNew(true);
			this.Browser.Document.Write(text);
		}

		private void ItemList_DoubleClick(object sender, EventArgs e)
		{
			if (this.Artifact != null)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		private void update_list()
		{
			List<Artifact> list = new List<Artifact>();
			foreach (Artifact current in Session.Artifacts)
			{
				if (this.match(current, this.NameBox.Text))
				{
					list.Add(current);
				}
			}
			ListViewGroup group = this.ItemList.Groups.Add("Heroic Tier", "Heroic Tier");
			ListViewGroup group2 = this.ItemList.Groups.Add("Paragon Tier", "Paragon Tier");
			ListViewGroup group3 = this.ItemList.Groups.Add("Epic Tier", "Epic Tier");
			List<ListViewItem> list2 = new List<ListViewItem>();
			foreach (Artifact current2 in list)
			{
				ListViewItem listViewItem = new ListViewItem(current2.Name);
				listViewItem.SubItems.Add(current2.Tier + " Tier");
				listViewItem.Tag = current2;
				switch (current2.Tier)
				{
				case Tier.Heroic:
					listViewItem.Group = group;
					break;
				case Tier.Paragon:
					listViewItem.Group = group2;
					break;
				case Tier.Epic:
					listViewItem.Group = group3;
					break;
				}
				list2.Add(listViewItem);
			}
			this.ItemList.BeginUpdate();
			this.ItemList.Items.Clear();
			this.ItemList.Items.AddRange(list2.ToArray());
			this.ItemList.EndUpdate();
		}

		private bool match(Artifact item, string query)
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

		private bool match_token(Artifact item, string token)
		{
			return item.Name.ToLower().Contains(token);
		}
	}
}
