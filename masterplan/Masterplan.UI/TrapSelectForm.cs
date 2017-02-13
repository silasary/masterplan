using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TrapSelectForm : Form
	{
		public class TrapSorter : IComparer
		{
			private bool fAscending = true;

			private int fColumn;

			public void Set(int column)
			{
				if (this.fColumn == column)
				{
					this.fAscending = !this.fAscending;
				}
				this.fColumn = column;
			}

			public int Compare(object x, object y)
			{
				ListViewItem listViewItem = x as ListViewItem;
				ListViewItem listViewItem2 = y as ListViewItem;
				int num = 0;
				switch (this.fColumn)
				{
				case 0:
				{
					ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems[this.fColumn];
					ListViewItem.ListViewSubItem listViewSubItem2 = listViewItem2.SubItems[this.fColumn];
					string text = listViewSubItem.Text;
					string text2 = listViewSubItem2.Text;
					num = text.CompareTo(text2);
					break;
				}
				case 1:
				{
					Trap trap = listViewItem.Tag as Trap;
					Trap trap2 = listViewItem2.Tag as Trap;
					int level = trap.Level;
					int level2 = trap2.Level;
					num = level.CompareTo(level2);
					break;
				}
				}
				if (!this.fAscending)
				{
					num *= -1;
				}
				return num;
			}
		}

		private IContainer components;

		private Button OKBtn;

		private ListView TrapList;

		private Button CancelBtn;

		private ColumnHeader NameHdr;

		private ColumnHeader InfoHdr;

		private SplitContainer Splitter;

		private Panel BrowserPanel;

		private WebBrowser Browser;

		private LevelRangePanel LevelRangePanel;

		public Trap Trap
		{
			get
			{
				if (this.TrapList.SelectedItems.Count != 0)
				{
					return this.TrapList.SelectedItems[0].Tag as Trap;
				}
				return null;
			}
		}

		public TrapSelectForm()
		{
			this.InitializeComponent();
			this.TrapList.ListViewItemSorter = new TrapSelectForm.TrapSorter();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			if (Session.Project != null)
			{
				int minlevel = Math.Max(1, Session.Project.Party.Level - 4);
				int maxlevel = Session.Project.Party.Level + 5;
				this.LevelRangePanel.SetLevelRange(minlevel, maxlevel);
			}
			this.update_list();
			this.Browser.DocumentText = "";
			this.TrapList_SelectedIndexChanged(null, null);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = (this.Trap != null);
		}

		private void TrapList_DoubleClick(object sender, EventArgs e)
		{
			if (this.Trap != null)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		private void TrapList_SelectedIndexChanged(object sender, EventArgs e)
		{
			string text = HTML.Trap(this.Trap, null, true, false, false, DisplaySize.Small);
			this.Browser.Document.OpenNew(true);
			this.Browser.Document.Write(text);
		}

		private void TrapList_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			TrapSelectForm.TrapSorter trapSorter = this.TrapList.ListViewItemSorter as TrapSelectForm.TrapSorter;
			trapSorter.Set(e.Column);
			this.TrapList.Sort();
		}

		private void LevelRangePanel_RangeChanged(object sender, EventArgs e)
		{
			this.update_list();
		}

		private void update_list()
		{
			List<Trap> traps = Session.Traps;
			this.TrapList.BeginUpdate();
			this.TrapList.Items.Clear();
			foreach (Trap current in traps)
			{
				if (current.Level >= this.LevelRangePanel.MinimumLevel && current.Level <= this.LevelRangePanel.MaximumLevel && this.match(current, this.LevelRangePanel.NameQuery))
				{
					ListViewItem listViewItem = this.TrapList.Items.Add(current.Name);
					listViewItem.SubItems.Add(current.Info);
					listViewItem.Group = this.TrapList.Groups[(current.Type == TrapType.Trap) ? 0 : 1];
					listViewItem.Tag = current;
				}
			}
			this.TrapList.EndUpdate();
		}

		private bool match(Trap trap, string query)
		{
			string[] array = query.ToLower().Split(new char[0]);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string token = array2[i];
				if (!this.match_token(trap, token))
				{
					return false;
				}
			}
			return true;
		}

		private bool match_token(Trap trap, string token)
		{
			return trap.Name.ToLower().Contains(token);
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
			ListViewGroup listViewGroup = new ListViewGroup("Trap", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Hazard", HorizontalAlignment.Left);
			this.OKBtn = new Button();
			this.TrapList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.CancelBtn = new Button();
			this.Splitter = new SplitContainer();
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
			this.TrapList.Columns.AddRange(new ColumnHeader[]
			{
				this.NameHdr,
				this.InfoHdr
			});
			this.TrapList.Dock = DockStyle.Fill;
			this.TrapList.FullRowSelect = true;
			listViewGroup.Header = "Trap";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup2.Header = "Hazard";
			listViewGroup2.Name = "listViewGroup2";
			this.TrapList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2
			});
			this.TrapList.HideSelection = false;
			this.TrapList.Location = new Point(0, 80);
			this.TrapList.MultiSelect = false;
			this.TrapList.Name = "TrapList";
			this.TrapList.Size = new Size(330, 256);
			this.TrapList.Sorting = SortOrder.Ascending;
			this.TrapList.TabIndex = 0;
			this.TrapList.UseCompatibleStateImageBehavior = false;
			this.TrapList.View = View.Details;
			this.TrapList.SelectedIndexChanged += new EventHandler(this.TrapList_SelectedIndexChanged);
			this.TrapList.DoubleClick += new EventHandler(this.TrapList_DoubleClick);
			this.TrapList.ColumnClick += new ColumnClickEventHandler(this.TrapList_ColumnClick);
			this.NameHdr.Text = "Trap / Hazard";
			this.NameHdr.Width = 150;
			this.InfoHdr.Text = "Info";
			this.InfoHdr.Width = 150;
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
			this.Splitter.Panel1.Controls.Add(this.TrapList);
			this.Splitter.Panel1.Controls.Add(this.LevelRangePanel);
			this.Splitter.Panel2.Controls.Add(this.BrowserPanel);
			this.Splitter.Size = new Size(693, 336);
			this.Splitter.SplitterDistance = 330;
			this.Splitter.TabIndex = 3;
			this.LevelRangePanel.Dock = DockStyle.Top;
			this.LevelRangePanel.Location = new Point(0, 0);
			this.LevelRangePanel.Name = "LevelRangePanel";
			this.LevelRangePanel.Size = new Size(330, 80);
			this.LevelRangePanel.TabIndex = 3;
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
			base.Name = "TrapSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select a Trap / Hazard";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.BrowserPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
