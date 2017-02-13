using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class RegionalMapSelectForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private ListView MapList;

		private Button CancelBtn;

		private ColumnHeader NameHdr;

		public RegionalMap Map
		{
			get
			{
				if (this.MapList.SelectedItems.Count != 0)
				{
					return this.MapList.SelectedItems[0].Tag as RegionalMap;
				}
				return null;
			}
		}

		public List<RegionalMap> Maps
		{
			get
			{
				List<RegionalMap> list = new List<RegionalMap>();
				foreach (ListViewItem listViewItem in this.MapList.CheckedItems)
				{
					RegionalMap regionalMap = listViewItem.Tag as RegionalMap;
					if (regionalMap != null)
					{
						list.Add(regionalMap);
					}
				}
				return list;
			}
		}

		public RegionalMapSelectForm(List<RegionalMap> maps, List<Guid> exclude, bool multi_select)
		{
			this.InitializeComponent();
			foreach (RegionalMap current in maps)
			{
				if (exclude == null || !exclude.Contains(current.ID))
				{
					ListViewItem listViewItem = this.MapList.Items.Add(current.Name);
					listViewItem.Tag = current;
				}
			}
			if (multi_select)
			{
				this.MapList.CheckBoxes = true;
			}
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			if (this.MapList.CheckBoxes)
			{
				this.OKBtn.Enabled = (this.MapList.CheckedItems.Count != 0);
				return;
			}
			this.OKBtn.Enabled = (this.Map != null);
		}

		private void TileList_DoubleClick(object sender, EventArgs e)
		{
			if (this.Map != null)
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
			ListViewGroup listViewGroup = new ListViewGroup("Trap", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Hazard", HorizontalAlignment.Left);
			this.OKBtn = new Button();
			this.MapList = new ListView();
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
			this.MapList.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.MapList.Columns.AddRange(new ColumnHeader[]
			{
				this.NameHdr
			});
			this.MapList.FullRowSelect = true;
			listViewGroup.Header = "Trap";
			listViewGroup.Name = "listViewGroup1";
			listViewGroup2.Header = "Hazard";
			listViewGroup2.Name = "listViewGroup2";
			this.MapList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2
			});
			this.MapList.HideSelection = false;
			this.MapList.Location = new Point(12, 12);
			this.MapList.MultiSelect = false;
			this.MapList.Name = "MapList";
			this.MapList.Size = new Size(332, 336);
			this.MapList.TabIndex = 0;
			this.MapList.UseCompatibleStateImageBehavior = false;
			this.MapList.View = View.Details;
			this.MapList.DoubleClick += new EventHandler(this.TileList_DoubleClick);
			this.NameHdr.Text = "Map";
			this.NameHdr.Width = 281;
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
			base.Controls.Add(this.MapList);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MapSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select a Map";
			base.ResumeLayout(false);
		}
	}
}
