using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class MergeLibrariesForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private ListView ThemeList;

		private Button CancelBtn;

		private ColumnHeader NameHdr;

		private Label NameLbl;

		private TextBox NameBox;

		public List<Library> SelectedLibraries
		{
			get
			{
				List<Library> list = new List<Library>();
				foreach (ListViewItem listViewItem in this.ThemeList.CheckedItems)
				{
					Library library = listViewItem.Tag as Library;
					if (library != null)
					{
						list.Add(library);
					}
				}
				return list;
			}
		}

		public string LibraryName
		{
			get
			{
				return this.NameBox.Text;
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
			this.ThemeList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.CancelBtn = new Button();
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(166, 354);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.ThemeList.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.ThemeList.CheckBoxes = true;
			this.ThemeList.Columns.AddRange(new ColumnHeader[]
			{
				this.NameHdr
			});
			this.ThemeList.FullRowSelect = true;
			this.ThemeList.HideSelection = false;
			this.ThemeList.Location = new Point(12, 12);
			this.ThemeList.MultiSelect = false;
			this.ThemeList.Name = "ThemeList";
			this.ThemeList.Size = new Size(310, 310);
			this.ThemeList.Sorting = SortOrder.Ascending;
			this.ThemeList.TabIndex = 0;
			this.ThemeList.UseCompatibleStateImageBehavior = false;
			this.ThemeList.View = View.Details;
			this.ThemeList.DoubleClick += new EventHandler(this.TileList_DoubleClick);
			this.NameHdr.Text = "Library";
			this.NameHdr.Width = 270;
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(247, 354);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.NameLbl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 331);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(97, 13);
			this.NameLbl.TabIndex = 1;
			this.NameLbl.Text = "New Library Name:";
			this.NameBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(115, 328);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(207, 20);
			this.NameBox.TabIndex = 2;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(334, 389);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.ThemeList);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MergeLibrariesForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Merge Libraries";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public MergeLibrariesForm()
		{
			this.InitializeComponent();
			foreach (Library current in Session.Libraries)
			{
				ListViewItem listViewItem = this.ThemeList.Items.Add(current.Name);
				listViewItem.Tag = current;
			}
			this.NameBox.Text = "Merged Library";
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = (this.SelectedLibraries.Count >= 2);
		}

		private void TileList_DoubleClick(object sender, EventArgs e)
		{
		}
	}
}
