using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class EncyclopediaEntrySelectForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private ListView EntryList;

		private Button CancelBtn;

		private ColumnHeader NameHdr;

		public EncyclopediaEntry EncyclopediaEntry
		{
			get
			{
				if (this.EntryList.SelectedItems.Count != 0)
				{
					return this.EntryList.SelectedItems[0].Tag as EncyclopediaEntry;
				}
				return null;
			}
		}

		public EncyclopediaEntrySelectForm(List<Guid> ignore_ids)
		{
			this.InitializeComponent();
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (EncyclopediaEntry current in Session.Project.Encyclopedia.Entries)
			{
				if (current.Category != null && current.Category != "")
				{
					binarySearchTree.Add(current.Category);
				}
			}
			List<string> sortedList = binarySearchTree.SortedList;
			sortedList.Insert(0, "Miscellaneous Entries");
			foreach (string current2 in sortedList)
			{
				this.EntryList.Groups.Add(new ListViewGroup(current2, current2));
			}
			foreach (EncyclopediaEntry current3 in Session.Project.Encyclopedia.Entries)
			{
				if (!ignore_ids.Contains(current3.ID))
				{
					ListViewItem listViewItem = this.EntryList.Items.Add(current3.Name);
					listViewItem.Tag = current3;
					if (current3.Category != null && current3.Category != "")
					{
						listViewItem.Group = this.EntryList.Groups[current3.Category];
					}
					else
					{
						listViewItem.Group = this.EntryList.Groups["Miscellaneous Entries"];
					}
				}
			}
			if (this.EntryList.Items.Count == 0)
			{
				ListViewItem listViewItem2 = this.EntryList.Items.Add("(no entries)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
			Application.Idle += new EventHandler(this.Application_Idle);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = (this.EncyclopediaEntry != null);
		}

		private void TileList_DoubleClick(object sender, EventArgs e)
		{
			if (this.EncyclopediaEntry != null)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
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
			this.EntryList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.CancelBtn = new Button();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(188, 354);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.EntryList.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.EntryList.Columns.AddRange(new ColumnHeader[]
			{
				this.NameHdr
			});
			this.EntryList.FullRowSelect = true;
			this.EntryList.HideSelection = false;
			this.EntryList.Location = new Point(12, 12);
			this.EntryList.MultiSelect = false;
			this.EntryList.Name = "EntryList";
			this.EntryList.Size = new Size(332, 336);
			this.EntryList.Sorting = SortOrder.Ascending;
			this.EntryList.TabIndex = 0;
			this.EntryList.UseCompatibleStateImageBehavior = false;
			this.EntryList.View = View.Details;
			this.EntryList.DoubleClick += new EventHandler(this.TileList_DoubleClick);
			this.NameHdr.Text = "Entry";
			this.NameHdr.Width = 300;
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(269, 354);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(356, 389);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.EntryList);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "EncyclopediaEntrySelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select an Encyclopedia Entry";
			base.ResumeLayout(false);
		}
	}
}
