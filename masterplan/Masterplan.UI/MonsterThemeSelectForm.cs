using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class MonsterThemeSelectForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private ListView ThemeList;

		private Button CancelBtn;

		private ColumnHeader NameHdr;

		public MonsterTheme MonsterTheme
		{
			get
			{
				if (this.ThemeList.SelectedItems.Count != 0)
				{
					return this.ThemeList.SelectedItems[0].Tag as MonsterTheme;
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
			this.ThemeList = new ListView();
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
			this.ThemeList.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.ThemeList.Columns.AddRange(new ColumnHeader[]
			{
				this.NameHdr
			});
			this.ThemeList.FullRowSelect = true;
			this.ThemeList.HideSelection = false;
			this.ThemeList.Location = new Point(12, 12);
			this.ThemeList.MultiSelect = false;
			this.ThemeList.Name = "ThemeList";
			this.ThemeList.Size = new Size(332, 336);
			this.ThemeList.Sorting = SortOrder.Ascending;
			this.ThemeList.TabIndex = 0;
			this.ThemeList.UseCompatibleStateImageBehavior = false;
			this.ThemeList.View = View.Details;
			this.ThemeList.DoubleClick += new EventHandler(this.TileList_DoubleClick);
			this.NameHdr.Text = "Theme";
			this.NameHdr.Width = 270;
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
			base.Controls.Add(this.ThemeList);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MonsterThemeSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select a Theme";
			base.ResumeLayout(false);
		}

		public MonsterThemeSelectForm()
		{
			this.InitializeComponent();
			List<MonsterTheme> themes = Session.Themes;
			foreach (MonsterTheme current in themes)
			{
				ListViewItem listViewItem = this.ThemeList.Items.Add(current.Name);
				listViewItem.Tag = current;
			}
			Application.Idle += new EventHandler(this.Application_Idle);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = (this.MonsterTheme != null);
		}

		private void TileList_DoubleClick(object sender, EventArgs e)
		{
			if (this.MonsterTheme != null)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}
	}
}
