using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TileSelectForm : Form
	{
		private enum GroupBy
		{
			Library,
			Category
		}

		private IContainer components;

		private Button OKBtn;

		private ListView TileList;

		private Button CancelBtn;

		private Panel TilePanel;

		private ToolStrip Toolbar;

		private ToolStripButton LibraryBtn;

		private ToolStripButton CategoryBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton MatchCatBtn;

		private Size fTileSize = Size.Empty;

		private TileCategory fCategory = TileCategory.Map;

		private TileSelectForm.GroupBy fGroupBy;

		private bool fMatchCategory = true;

		public Tile Tile
		{
			get
			{
				if (this.TileList.SelectedItems.Count != 0)
				{
					return this.TileList.SelectedItems[0].Tag as Tile;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(TileSelectForm));
			this.OKBtn = new Button();
			this.TileList = new ListView();
			this.CancelBtn = new Button();
			this.TilePanel = new Panel();
			this.Toolbar = new ToolStrip();
			this.LibraryBtn = new ToolStripButton();
			this.CategoryBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.MatchCatBtn = new ToolStripButton();
			this.TilePanel.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(438, 307);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.TileList.Dock = DockStyle.Fill;
			this.TileList.FullRowSelect = true;
			this.TileList.HideSelection = false;
			this.TileList.Location = new Point(0, 25);
			this.TileList.MultiSelect = false;
			this.TileList.Name = "TileList";
			this.TileList.Size = new Size(582, 264);
			this.TileList.TabIndex = 0;
			this.TileList.UseCompatibleStateImageBehavior = false;
			this.TileList.DoubleClick += new EventHandler(this.TileList_DoubleClick);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(519, 307);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.TilePanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.TilePanel.Controls.Add(this.TileList);
			this.TilePanel.Controls.Add(this.Toolbar);
			this.TilePanel.Location = new Point(12, 12);
			this.TilePanel.Name = "TilePanel";
			this.TilePanel.Size = new Size(582, 289);
			this.TilePanel.TabIndex = 3;
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.LibraryBtn,
				this.CategoryBtn,
				this.toolStripSeparator1,
				this.MatchCatBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(582, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.LibraryBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LibraryBtn.Image = (Image)resources.GetObject("LibraryBtn.Image");
			this.LibraryBtn.ImageTransparentColor = Color.Magenta;
			this.LibraryBtn.Name = "LibraryBtn";
			this.LibraryBtn.Size = new Size(108, 22);
			this.LibraryBtn.Text = "Arrange by Library";
			this.LibraryBtn.Click += new EventHandler(this.LibraryBtn_Click);
			this.CategoryBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CategoryBtn.Image = (Image)resources.GetObject("CategoryBtn.Image");
			this.CategoryBtn.ImageTransparentColor = Color.Magenta;
			this.CategoryBtn.Name = "CategoryBtn";
			this.CategoryBtn.Size = new Size(120, 22);
			this.CategoryBtn.Text = "Arrange by Category";
			this.CategoryBtn.Click += new EventHandler(this.CategoryBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.MatchCatBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MatchCatBtn.Image = (Image)resources.GetObject("MatchCatBtn.Image");
			this.MatchCatBtn.ImageTransparentColor = Color.Magenta;
			this.MatchCatBtn.Name = "MatchCatBtn";
			this.MatchCatBtn.Size = new Size(96, 22);
			this.MatchCatBtn.Text = "Match Category";
			this.MatchCatBtn.Click += new EventHandler(this.MatchCatBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(606, 342);
			base.Controls.Add(this.TilePanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TileSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select Tile";
			this.TilePanel.ResumeLayout(false);
			this.TilePanel.PerformLayout();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		public TileSelectForm(Size tilesize, TileCategory category)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fTileSize = tilesize;
			this.fCategory = category;
			this.MatchCatBtn.Text = "Show only tiles in category: " + this.fCategory;
			this.update_tiles();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = (this.Tile != null);
			this.LibraryBtn.Checked = (this.fGroupBy == TileSelectForm.GroupBy.Library);
			this.CategoryBtn.Checked = (this.fGroupBy == TileSelectForm.GroupBy.Category);
			this.MatchCatBtn.Checked = this.fMatchCategory;
		}

		private void TileList_DoubleClick(object sender, EventArgs e)
		{
			if (this.Tile != null)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		private void LibraryBtn_Click(object sender, EventArgs e)
		{
			this.fGroupBy = TileSelectForm.GroupBy.Library;
			this.update_tiles();
		}

		private void CategoryBtn_Click(object sender, EventArgs e)
		{
			this.fGroupBy = TileSelectForm.GroupBy.Category;
			this.update_tiles();
		}

		private void MatchCatBtn_Click(object sender, EventArgs e)
		{
			this.fMatchCategory = !this.fMatchCategory;
			this.update_tiles();
		}

		private void update_tiles()
		{
			List<Tile> list = new List<Tile>();
			foreach (Library current in Session.Libraries)
			{
				foreach (Tile current2 in current.Tiles)
				{
					if (!this.fMatchCategory || this.fCategory == current2.Category)
					{
						bool flag = false;
						if (this.fTileSize != Size.Empty)
						{
							if (current2.Size.Width == this.fTileSize.Width && current2.Size.Height == this.fTileSize.Height)
							{
								flag = true;
							}
							if (current2.Size.Width == this.fTileSize.Height && current2.Size.Height == this.fTileSize.Width)
							{
								flag = true;
							}
						}
						else
						{
							flag = true;
						}
						if (flag)
						{
							list.Add(current2);
						}
					}
				}
			}
			this.TileList.Groups.Clear();
			switch (this.fGroupBy)
			{
			case TileSelectForm.GroupBy.Library:
				using (List<Library>.Enumerator enumerator3 = Session.Libraries.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						Library current3 = enumerator3.Current;
						this.TileList.Groups.Add(current3.Name, current3.Name);
					}
					goto IL_211;
				}
				break;
			case TileSelectForm.GroupBy.Category:
				break;
			default:
				goto IL_211;
			}
			Array values = Enum.GetValues(typeof(TileCategory));
			foreach (TileCategory tileCategory in values)
			{
				this.TileList.Groups.Add(tileCategory.ToString(), tileCategory.ToString());
			}
			IL_211:
			this.TileList.BeginUpdate();
			this.TileList.LargeImageList = new ImageList();
			this.TileList.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
			this.TileList.LargeImageList.ImageSize = new Size(64, 64);
			List<ListViewItem> list2 = new List<ListViewItem>();
			foreach (Tile current4 in list)
			{
				ListViewItem listViewItem = new ListViewItem(current4.ToString());
				listViewItem.Tag = current4;
				switch (this.fGroupBy)
				{
				case TileSelectForm.GroupBy.Library:
				{
					Library library = Session.FindLibrary(current4);
					listViewItem.Group = this.TileList.Groups[library.Name];
					break;
				}
				case TileSelectForm.GroupBy.Category:
					listViewItem.Group = this.TileList.Groups[current4.Category.ToString()];
					break;
				}
				Image image = (current4.Image != null) ? current4.Image : current4.BlankImage;
				Bitmap bitmap = new Bitmap(64, 64);
				if (current4.Size.Width > current4.Size.Height)
				{
					int num = current4.Size.Height * 64 / current4.Size.Width;
					Rectangle rect = new Rectangle(0, (64 - num) / 2, 64, num);
					Graphics graphics = Graphics.FromImage(bitmap);
					graphics.DrawImage(image, rect);
				}
				else
				{
					int num2 = current4.Size.Width * 64 / current4.Size.Height;
					Rectangle rect2 = new Rectangle((64 - num2) / 2, 0, num2, 64);
					Graphics graphics2 = Graphics.FromImage(bitmap);
					graphics2.DrawImage(image, rect2);
				}
				this.TileList.LargeImageList.Images.Add(bitmap);
				listViewItem.ImageIndex = this.TileList.LargeImageList.Images.Count - 1;
				list2.Add(listViewItem);
			}
			this.TileList.Items.Clear();
			this.TileList.Items.AddRange(list2.ToArray());
			this.TileList.EndUpdate();
		}
	}
}
