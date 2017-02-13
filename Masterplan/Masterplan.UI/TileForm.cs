using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TileForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label WidthLbl;

		private NumericUpDown WidthBox;

		private Label HeightLbl;

		private NumericUpDown HeightBox;

		private Panel ImagePanel;

		private ToolStrip Toolbar;

		private ToolStripButton BrowseBtn;

		private ToolStripButton ClearBtn;

		private Label CatLbl;

		private ComboBox CatBox;

		private ToolStripButton SetColourBtn;

		private ToolStripSeparator toolStripSeparator1;

		private TilePanel TilePanel;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton GridBtn;

		private ToolStripButton PasteBtn;

		private Label KeywordLbl;

		private TextBox KeywordBox;

		private Tile fTile;

		public Tile Tile
		{
			get
			{
				return this.fTile;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(TileForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.WidthLbl = new Label();
			this.WidthBox = new NumericUpDown();
			this.HeightLbl = new Label();
			this.HeightBox = new NumericUpDown();
			this.ImagePanel = new Panel();
			this.TilePanel = new TilePanel();
			this.Toolbar = new ToolStrip();
			this.BrowseBtn = new ToolStripButton();
			this.PasteBtn = new ToolStripButton();
			this.ClearBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.SetColourBtn = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.GridBtn = new ToolStripButton();
			this.CatLbl = new Label();
			this.CatBox = new ComboBox();
			this.KeywordLbl = new Label();
			this.KeywordBox = new TextBox();
			((ISupportInitialize)this.WidthBox).BeginInit();
			((ISupportInitialize)this.HeightBox).BeginInit();
			this.ImagePanel.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(169, 391);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 9;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(250, 391);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 10;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.WidthLbl.AutoSize = true;
			this.WidthLbl.Location = new Point(12, 14);
			this.WidthLbl.Name = "WidthLbl";
			this.WidthLbl.Size = new Size(38, 13);
			this.WidthLbl.TabIndex = 0;
			this.WidthLbl.Text = "Width:";
			this.WidthBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.WidthBox.Location = new Point(74, 12);
			NumericUpDown arg_2CD_0 = this.WidthBox;
			int[] array = new int[4];
			array[0] = 1;
			arg_2CD_0.Minimum = new decimal(array);
			this.WidthBox.Name = "WidthBox";
			this.WidthBox.Size = new Size(251, 20);
			this.WidthBox.TabIndex = 1;
			NumericUpDown arg_31C_0 = this.WidthBox;
			int[] array2 = new int[4];
			array2[0] = 2;
			arg_31C_0.Value = new decimal(array2);
			this.WidthBox.ValueChanged += new EventHandler(this.WidthBox_ValueChanged);
			this.HeightLbl.AutoSize = true;
			this.HeightLbl.Location = new Point(12, 40);
			this.HeightLbl.Name = "HeightLbl";
			this.HeightLbl.Size = new Size(41, 13);
			this.HeightLbl.TabIndex = 2;
			this.HeightLbl.Text = "Height:";
			this.HeightBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.HeightBox.Location = new Point(74, 38);
			NumericUpDown arg_3D0_0 = this.HeightBox;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_3D0_0.Minimum = new decimal(array3);
			this.HeightBox.Name = "HeightBox";
			this.HeightBox.Size = new Size(251, 20);
			this.HeightBox.TabIndex = 3;
			NumericUpDown arg_422_0 = this.HeightBox;
			int[] array4 = new int[4];
			array4[0] = 2;
			arg_422_0.Value = new decimal(array4);
			this.HeightBox.ValueChanged += new EventHandler(this.HeightBox_ValueChanged);
			this.ImagePanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.ImagePanel.BorderStyle = BorderStyle.FixedSingle;
			this.ImagePanel.Controls.Add(this.TilePanel);
			this.ImagePanel.Controls.Add(this.Toolbar);
			this.ImagePanel.Location = new Point(13, 117);
			this.ImagePanel.Name = "ImagePanel";
			this.ImagePanel.Size = new Size(313, 268);
			this.ImagePanel.TabIndex = 8;
			this.TilePanel.Dock = DockStyle.Fill;
			this.TilePanel.Location = new Point(0, 25);
			this.TilePanel.Name = "TilePanel";
			this.TilePanel.ShowGridlines = true;
			this.TilePanel.Size = new Size(311, 241);
			this.TilePanel.TabIndex = 1;
			this.TilePanel.TileColour = Color.White;
			this.TilePanel.TileImage = null;
			this.TilePanel.TileSize = new Size(2, 2);
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.BrowseBtn,
				this.PasteBtn,
				this.ClearBtn,
				this.toolStripSeparator1,
				this.SetColourBtn,
				this.toolStripSeparator2,
				this.GridBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(311, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.BrowseBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.BrowseBtn.Image = (Image)resources.GetObject("BrowseBtn.Image");
			this.BrowseBtn.ImageTransparentColor = Color.Magenta;
			this.BrowseBtn.Name = "BrowseBtn";
			this.BrowseBtn.Size = new Size(49, 22);
			this.BrowseBtn.Text = "Browse";
			this.BrowseBtn.Click += new EventHandler(this.BrowseBtn_Click);
			this.PasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PasteBtn.Image = (Image)resources.GetObject("PasteBtn.Image");
			this.PasteBtn.ImageTransparentColor = Color.Magenta;
			this.PasteBtn.Name = "PasteBtn";
			this.PasteBtn.Size = new Size(39, 22);
			this.PasteBtn.Text = "Paste";
			this.PasteBtn.Click += new EventHandler(this.PasteBtn_Click);
			this.ClearBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ClearBtn.Image = (Image)resources.GetObject("ClearBtn.Image");
			this.ClearBtn.ImageTransparentColor = Color.Magenta;
			this.ClearBtn.Name = "ClearBtn";
			this.ClearBtn.Size = new Size(38, 22);
			this.ClearBtn.Text = "Clear";
			this.ClearBtn.Click += new EventHandler(this.ClearBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.SetColourBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SetColourBtn.Image = (Image)resources.GetObject("SetColourBtn.Image");
			this.SetColourBtn.ImageTransparentColor = Color.Magenta;
			this.SetColourBtn.Name = "SetColourBtn";
			this.SetColourBtn.Size = new Size(66, 22);
			this.SetColourBtn.Text = "Set Colour";
			this.SetColourBtn.Click += new EventHandler(this.SetColourBtn_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 25);
			this.GridBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.GridBtn.Image = (Image)resources.GetObject("GridBtn.Image");
			this.GridBtn.ImageTransparentColor = Color.Magenta;
			this.GridBtn.Name = "GridBtn";
			this.GridBtn.Size = new Size(57, 22);
			this.GridBtn.Text = "Gridlines";
			this.GridBtn.Click += new EventHandler(this.GridBtn_Click);
			this.CatLbl.AutoSize = true;
			this.CatLbl.Location = new Point(12, 67);
			this.CatLbl.Name = "CatLbl";
			this.CatLbl.Size = new Size(52, 13);
			this.CatLbl.TabIndex = 4;
			this.CatLbl.Text = "Category:";
			this.CatBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.CatBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.CatBox.FormattingEnabled = true;
			this.CatBox.Location = new Point(74, 64);
			this.CatBox.Name = "CatBox";
			this.CatBox.Size = new Size(251, 21);
			this.CatBox.TabIndex = 5;
			this.KeywordLbl.AutoSize = true;
			this.KeywordLbl.Location = new Point(12, 94);
			this.KeywordLbl.Name = "KeywordLbl";
			this.KeywordLbl.Size = new Size(56, 13);
			this.KeywordLbl.TabIndex = 6;
			this.KeywordLbl.Text = "Keywords:";
			this.KeywordBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.KeywordBox.Location = new Point(74, 91);
			this.KeywordBox.Name = "KeywordBox";
			this.KeywordBox.Size = new Size(251, 20);
			this.KeywordBox.TabIndex = 7;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(337, 426);
			base.Controls.Add(this.KeywordBox);
			base.Controls.Add(this.KeywordLbl);
			base.Controls.Add(this.CatBox);
			base.Controls.Add(this.CatLbl);
			base.Controls.Add(this.WidthBox);
			base.Controls.Add(this.WidthLbl);
			base.Controls.Add(this.HeightLbl);
			base.Controls.Add(this.ImagePanel);
			base.Controls.Add(this.HeightBox);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TileForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Tile";
			((ISupportInitialize)this.WidthBox).EndInit();
			((ISupportInitialize)this.HeightBox).EndInit();
			this.ImagePanel.ResumeLayout(false);
			this.ImagePanel.PerformLayout();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public TileForm(Tile t)
		{
			this.InitializeComponent();
			foreach (TileCategory tileCategory in Enum.GetValues(typeof(TileCategory)))
			{
				this.CatBox.Items.Add(tileCategory);
			}
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.fTile = t.Copy();
			this.WidthBox.Value = this.fTile.Size.Width;
			this.HeightBox.Value = this.fTile.Size.Height;
			this.CatBox.SelectedItem = this.fTile.Category;
			this.KeywordBox.Text = this.fTile.Keywords;
			this.image_changed();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.PasteBtn.Enabled = Clipboard.ContainsImage();
			this.ClearBtn.Enabled = (this.fTile.Image != null);
			this.SetColourBtn.Enabled = (this.fTile.Image == null);
			this.GridBtn.Checked = this.TilePanel.ShowGridlines;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			int width = (int)this.WidthBox.Value;
			int height = (int)this.HeightBox.Value;
			this.fTile.Size = new Size(width, height);
			this.fTile.Category = (TileCategory)this.CatBox.SelectedItem;
			this.fTile.Keywords = this.KeywordBox.Text;
		}

		private void WidthBox_ValueChanged(object sender, EventArgs e)
		{
			this.image_changed();
		}

		private void HeightBox_ValueChanged(object sender, EventArgs e)
		{
			this.image_changed();
		}

		private void BrowseBtn_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = Program.ImageFilter;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.fTile.Image = Image.FromFile(openFileDialog.FileName);
				Program.SetResolution(this.fTile.Image);
				this.image_changed();
			}
		}

		private void PasteBtn_Click(object sender, EventArgs e)
		{
			if (Clipboard.ContainsImage())
			{
				this.fTile.Image = Clipboard.GetImage();
				Program.SetResolution(this.fTile.Image);
				this.image_changed();
			}
		}

		private void ClearBtn_Click(object sender, EventArgs e)
		{
			this.fTile.Image = null;
			this.image_changed();
		}

		private void SetColourBtn_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			colorDialog.AllowFullOpen = false;
			colorDialog.Color = this.fTile.BlankColour;
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				this.fTile.BlankColour = colorDialog.Color;
				this.image_changed();
			}
		}

		private void GridBtn_Click(object sender, EventArgs e)
		{
			this.TilePanel.ShowGridlines = !this.TilePanel.ShowGridlines;
		}

		private void image_changed()
		{
			int width = (int)this.WidthBox.Value;
			int height = (int)this.HeightBox.Value;
			this.TilePanel.TileImage = this.fTile.Image;
			this.TilePanel.TileColour = this.fTile.BlankColour;
			this.TilePanel.TileSize = new Size(width, height);
		}
	}
}
