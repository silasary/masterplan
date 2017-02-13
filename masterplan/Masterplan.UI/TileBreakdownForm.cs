using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TileBreakdownForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private ListView TileList;

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
			this.TileList = new ListView();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(330, 376);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.TileList.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.TileList.FullRowSelect = true;
			this.TileList.HideSelection = false;
			this.TileList.Location = new Point(12, 12);
			this.TileList.MultiSelect = false;
			this.TileList.Name = "TileList";
			this.TileList.Size = new Size(393, 358);
			this.TileList.TabIndex = 1;
			this.TileList.UseCompatibleStateImageBehavior = false;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(417, 411);
			base.Controls.Add(this.TileList);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TileBreakdownForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Map Tiles Used";
			base.ResumeLayout(false);
		}

		public TileBreakdownForm(Map map)
		{
			this.InitializeComponent();
			Dictionary<Guid, int> dictionary = new Dictionary<Guid, int>();
			Dictionary<Guid, int> dictionary2 = new Dictionary<Guid, int>();
			foreach (TileData current in map.Tiles)
			{
				if (!dictionary2.ContainsKey(current.TileID))
				{
					dictionary2[current.TileID] = 0;
				}
				dictionary2[current.TileID] = dictionary2[current.TileID] + 1;
			}
			foreach (Guid current2 in dictionary2.Keys)
			{
				if (!dictionary.ContainsKey(current2))
				{
					dictionary[current2] = 0;
				}
				if (dictionary2[current2] > dictionary[current2])
				{
					dictionary[current2] = dictionary2[current2];
				}
			}
			List<string> list = new List<string>();
			foreach (Guid current3 in dictionary.Keys)
			{
				Tile t = Session.FindTile(current3, SearchType.Global);
				Library library = Session.FindLibrary(t);
				if (!list.Contains(library.Name))
				{
					list.Add(library.Name);
				}
			}
			list.Sort();
			foreach (string current4 in list)
			{
				this.TileList.Groups.Add(current4, current4);
			}
			this.TileList.LargeImageList = new ImageList();
			this.TileList.LargeImageList.ImageSize = new Size(64, 64);
			foreach (Guid current5 in dictionary.Keys)
			{
				Tile tile = Session.FindTile(current5, SearchType.Global);
				Library library2 = Session.FindLibrary(tile);
				ListViewItem listViewItem = this.TileList.Items.Add("x " + dictionary[current5]);
				listViewItem.Tag = tile;
				listViewItem.Group = this.TileList.Groups[library2.Name];
				Image image = (tile.Image != null) ? tile.Image : tile.BlankImage;
				Bitmap bitmap = new Bitmap(64, 64);
				if (tile.Size.Width > tile.Size.Height)
				{
					int num = tile.Size.Height * 64 / tile.Size.Width;
					Rectangle rect = new Rectangle(0, (64 - num) / 2, 64, num);
					Graphics graphics = Graphics.FromImage(bitmap);
					graphics.DrawImage(image, rect);
				}
				else
				{
					int num2 = tile.Size.Width * 64 / tile.Size.Height;
					Rectangle rect2 = new Rectangle((64 - num2) / 2, 0, num2, 64);
					Graphics graphics2 = Graphics.FromImage(bitmap);
					graphics2.DrawImage(image, rect2);
				}
				this.TileList.LargeImageList.Images.Add(bitmap);
				listViewItem.ImageIndex = this.TileList.LargeImageList.Images.Count - 1;
			}
		}
	}
}
