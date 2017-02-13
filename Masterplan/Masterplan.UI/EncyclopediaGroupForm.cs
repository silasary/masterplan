using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class EncyclopediaGroupForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label TitleLbl;

		private TextBox TitleBox;

		private TabControl Pages;

		private TabPage LinksPage;

		private ListView EntryList;

		private ColumnHeader EntryHdr;

		private EncyclopediaGroup fGroup;

		public EncyclopediaGroup Group
		{
			get
			{
				return this.fGroup;
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
			this.TitleLbl = new Label();
			this.TitleBox = new TextBox();
			this.Pages = new TabControl();
			this.LinksPage = new TabPage();
			this.EntryList = new ListView();
			this.EntryHdr = new ColumnHeader();
			this.Pages.SuspendLayout();
			this.LinksPage.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(338, 268);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 5;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(419, 268);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 6;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.TitleLbl.AutoSize = true;
			this.TitleLbl.Location = new Point(12, 15);
			this.TitleLbl.Name = "TitleLbl";
			this.TitleLbl.Size = new Size(30, 13);
			this.TitleLbl.TabIndex = 0;
			this.TitleLbl.Text = "Title:";
			this.TitleBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TitleBox.Location = new Point(70, 12);
			this.TitleBox.Name = "TitleBox";
			this.TitleBox.Size = new Size(424, 20);
			this.TitleBox.TabIndex = 1;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.LinksPage);
			this.Pages.Location = new Point(12, 38);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(482, 224);
			this.Pages.TabIndex = 4;
			this.LinksPage.Controls.Add(this.EntryList);
			this.LinksPage.Location = new Point(4, 22);
			this.LinksPage.Name = "LinksPage";
			this.LinksPage.Padding = new Padding(3);
			this.LinksPage.Size = new Size(474, 198);
			this.LinksPage.TabIndex = 1;
			this.LinksPage.Text = "Links";
			this.LinksPage.UseVisualStyleBackColor = true;
			this.EntryList.CheckBoxes = true;
			this.EntryList.Columns.AddRange(new ColumnHeader[]
			{
				this.EntryHdr
			});
			this.EntryList.Dock = DockStyle.Fill;
			this.EntryList.FullRowSelect = true;
			this.EntryList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.EntryList.HideSelection = false;
			this.EntryList.Location = new Point(3, 3);
			this.EntryList.MultiSelect = false;
			this.EntryList.Name = "EntryList";
			this.EntryList.Size = new Size(468, 192);
			this.EntryList.Sorting = SortOrder.Ascending;
			this.EntryList.TabIndex = 0;
			this.EntryList.UseCompatibleStateImageBehavior = false;
			this.EntryList.View = View.Details;
			this.EntryHdr.Text = "Entry";
			this.EntryHdr.Width = 300;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(506, 303);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.TitleBox);
			base.Controls.Add(this.TitleLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MinimizeBox = false;
			base.Name = "EncyclopediaGroupForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Encyclopedia Group";
			this.Pages.ResumeLayout(false);
			this.LinksPage.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public EncyclopediaGroupForm(EncyclopediaGroup group)
		{
			this.InitializeComponent();
			this.fGroup = group.Copy();
			this.TitleBox.Text = this.fGroup.Name;
			foreach (EncyclopediaEntry current in Session.Project.Encyclopedia.Entries)
			{
				ListViewItem listViewItem = this.EntryList.Items.Add(current.Name);
				listViewItem.Tag = current;
				listViewItem.Checked = this.fGroup.EntryIDs.Contains(current.ID);
			}
			if (this.EntryList.Items.Count == 0)
			{
				ListViewItem listViewItem2 = this.EntryList.Items.Add("(no entries)");
				listViewItem2.ForeColor = SystemColors.GrayText;
				this.EntryList.CheckBoxes = false;
			}
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fGroup.Name = this.TitleBox.Text;
			this.fGroup.EntryIDs.Clear();
			foreach (ListViewItem listViewItem in this.EntryList.CheckedItems)
			{
				EncyclopediaEntry encyclopediaEntry = listViewItem.Tag as EncyclopediaEntry;
				this.fGroup.EntryIDs.Add(encyclopediaEntry.ID);
			}
		}
	}
}
