using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Tools.Generators;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class MapAreaForm : Form
	{
		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private Button OKBtn;

		private Button CancelBtn;

		private MapView MapView;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TextBox DetailsBox;

		private TabPage ResizePage;

		private TableLayoutPanel ResizeTable;

		private Button TopMoreBtn;

		private Button TopLessBtn;

		private Button LeftMoreBtn;

		private Button LeftLessBtn;

		private Button RightLessBtn;

		private Button RightMoreBtn;

		private Button BottomLessBtn;

		private Button BottomMoreBtn;

		private TabPage MovePage;

		private TableLayoutPanel MoveTable;

		private Button MoveUpBtn;

		private Button MoveDownBtn;

		private Button MoveLeftBtn;

		private Button MoveRightBtn;

		private ToolStrip DetailsToolbar;

		private ToolStripButton RandomDescBtn;

		private ToolStripButton RandomNameBtn;

		private MapArea fArea;

		public MapArea Area
		{
			get
			{
				return this.fArea;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(MapAreaForm));
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.DetailsBox = new TextBox();
			this.DetailsToolbar = new ToolStrip();
			this.RandomDescBtn = new ToolStripButton();
			this.MovePage = new TabPage();
			this.MoveTable = new TableLayoutPanel();
			this.MoveUpBtn = new Button();
			this.MoveDownBtn = new Button();
			this.MoveLeftBtn = new Button();
			this.MoveRightBtn = new Button();
			this.ResizePage = new TabPage();
			this.ResizeTable = new TableLayoutPanel();
			this.TopMoreBtn = new Button();
			this.TopLessBtn = new Button();
			this.LeftMoreBtn = new Button();
			this.LeftLessBtn = new Button();
			this.RightLessBtn = new Button();
			this.RightMoreBtn = new Button();
			this.BottomLessBtn = new Button();
			this.BottomMoreBtn = new Button();
			this.MapView = new MapView();
			this.RandomNameBtn = new ToolStripButton();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.DetailsToolbar.SuspendLayout();
			this.MovePage.SuspendLayout();
			this.MoveTable.SuspendLayout();
			this.ResizePage.SuspendLayout();
			this.ResizeTable.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(56, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(343, 20);
			this.NameBox.TabIndex = 1;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(594, 382);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 4;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(675, 382);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 5;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Controls.Add(this.MovePage);
			this.Pages.Controls.Add(this.ResizePage);
			this.Pages.Location = new Point(12, 38);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(387, 338);
			this.Pages.TabIndex = 6;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Controls.Add(this.DetailsToolbar);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new Padding(3);
			this.DetailsPage.Size = new Size(379, 312);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.Dock = DockStyle.Fill;
			this.DetailsBox.Location = new Point(3, 28);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new Size(373, 281);
			this.DetailsBox.TabIndex = 0;
			this.DetailsToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.RandomNameBtn,
				this.RandomDescBtn
			});
			this.DetailsToolbar.Location = new Point(3, 3);
			this.DetailsToolbar.Name = "DetailsToolbar";
			this.DetailsToolbar.Size = new Size(373, 25);
			this.DetailsToolbar.TabIndex = 1;
			this.DetailsToolbar.Text = "toolStrip1";
			this.RandomDescBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RandomDescBtn.Image = (Image)resources.GetObject("RandomDescBtn.Image");
			this.RandomDescBtn.ImageTransparentColor = Color.Magenta;
			this.RandomDescBtn.Name = "RandomDescBtn";
			this.RandomDescBtn.Size = new Size(119, 22);
			this.RandomDescBtn.Text = "Random Description";
			this.RandomDescBtn.Click += new EventHandler(this.RandomDescBtn_Click);
			this.MovePage.Controls.Add(this.MoveTable);
			this.MovePage.Location = new Point(4, 22);
			this.MovePage.Name = "MovePage";
			this.MovePage.Padding = new Padding(3);
			this.MovePage.Size = new Size(379, 312);
			this.MovePage.TabIndex = 2;
			this.MovePage.Text = "Move";
			this.MovePage.UseVisualStyleBackColor = true;
			this.MoveTable.ColumnCount = 3;
			this.MoveTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
			this.MoveTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34f));
			this.MoveTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
			this.MoveTable.Controls.Add(this.MoveUpBtn, 1, 0);
			this.MoveTable.Controls.Add(this.MoveDownBtn, 1, 2);
			this.MoveTable.Controls.Add(this.MoveLeftBtn, 0, 1);
			this.MoveTable.Controls.Add(this.MoveRightBtn, 2, 1);
			this.MoveTable.Dock = DockStyle.Fill;
			this.MoveTable.Location = new Point(3, 3);
			this.MoveTable.Name = "MoveTable";
			this.MoveTable.RowCount = 3;
			this.MoveTable.RowStyles.Add(new RowStyle(SizeType.Percent, 33f));
			this.MoveTable.RowStyles.Add(new RowStyle(SizeType.Percent, 34f));
			this.MoveTable.RowStyles.Add(new RowStyle(SizeType.Percent, 33f));
			this.MoveTable.Size = new Size(373, 306);
			this.MoveTable.TabIndex = 0;
			this.MoveUpBtn.Dock = DockStyle.Fill;
			this.MoveUpBtn.Location = new Point(126, 3);
			this.MoveUpBtn.Name = "MoveUpBtn";
			this.MoveUpBtn.Size = new Size(120, 94);
			this.MoveUpBtn.TabIndex = 0;
			this.MoveUpBtn.Text = "Up";
			this.MoveUpBtn.UseVisualStyleBackColor = true;
			this.MoveUpBtn.Click += new EventHandler(this.MoveUpBtn_Click);
			this.MoveDownBtn.Dock = DockStyle.Fill;
			this.MoveDownBtn.Location = new Point(126, 207);
			this.MoveDownBtn.Name = "MoveDownBtn";
			this.MoveDownBtn.Size = new Size(120, 96);
			this.MoveDownBtn.TabIndex = 3;
			this.MoveDownBtn.Text = "Down";
			this.MoveDownBtn.UseVisualStyleBackColor = true;
			this.MoveDownBtn.Click += new EventHandler(this.MoveDownBtn_Click);
			this.MoveLeftBtn.Dock = DockStyle.Fill;
			this.MoveLeftBtn.Location = new Point(3, 103);
			this.MoveLeftBtn.Name = "MoveLeftBtn";
			this.MoveLeftBtn.Size = new Size(117, 98);
			this.MoveLeftBtn.TabIndex = 1;
			this.MoveLeftBtn.Text = "Left";
			this.MoveLeftBtn.UseVisualStyleBackColor = true;
			this.MoveLeftBtn.Click += new EventHandler(this.MoveLeftBtn_Click);
			this.MoveRightBtn.Dock = DockStyle.Fill;
			this.MoveRightBtn.Location = new Point(252, 103);
			this.MoveRightBtn.Name = "MoveRightBtn";
			this.MoveRightBtn.Size = new Size(118, 98);
			this.MoveRightBtn.TabIndex = 2;
			this.MoveRightBtn.Text = "Right";
			this.MoveRightBtn.UseVisualStyleBackColor = true;
			this.MoveRightBtn.Click += new EventHandler(this.MoveRightBtn_Click);
			this.ResizePage.Controls.Add(this.ResizeTable);
			this.ResizePage.Location = new Point(4, 22);
			this.ResizePage.Name = "ResizePage";
			this.ResizePage.Padding = new Padding(3);
			this.ResizePage.Size = new Size(391, 312);
			this.ResizePage.TabIndex = 1;
			this.ResizePage.Text = "Resize";
			this.ResizePage.UseVisualStyleBackColor = true;
			this.ResizeTable.ColumnCount = 4;
			this.ResizeTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.ResizeTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.ResizeTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.ResizeTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
			this.ResizeTable.Controls.Add(this.TopMoreBtn, 1, 0);
			this.ResizeTable.Controls.Add(this.TopLessBtn, 2, 0);
			this.ResizeTable.Controls.Add(this.LeftMoreBtn, 0, 1);
			this.ResizeTable.Controls.Add(this.LeftLessBtn, 0, 2);
			this.ResizeTable.Controls.Add(this.RightLessBtn, 3, 2);
			this.ResizeTable.Controls.Add(this.RightMoreBtn, 3, 1);
			this.ResizeTable.Controls.Add(this.BottomLessBtn, 2, 3);
			this.ResizeTable.Controls.Add(this.BottomMoreBtn, 1, 3);
			this.ResizeTable.Dock = DockStyle.Fill;
			this.ResizeTable.Location = new Point(3, 3);
			this.ResizeTable.Name = "ResizeTable";
			this.ResizeTable.RowCount = 4;
			this.ResizeTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
			this.ResizeTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
			this.ResizeTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
			this.ResizeTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
			this.ResizeTable.Size = new Size(385, 306);
			this.ResizeTable.TabIndex = 0;
			this.TopMoreBtn.Dock = DockStyle.Fill;
			this.TopMoreBtn.Location = new Point(99, 3);
			this.TopMoreBtn.Name = "TopMoreBtn";
			this.TopMoreBtn.Size = new Size(90, 70);
			this.TopMoreBtn.TabIndex = 0;
			this.TopMoreBtn.Text = "More";
			this.TopMoreBtn.UseVisualStyleBackColor = true;
			this.TopMoreBtn.Click += new EventHandler(this.TopMoreBtn_Click);
			this.TopLessBtn.Dock = DockStyle.Fill;
			this.TopLessBtn.Location = new Point(195, 3);
			this.TopLessBtn.Name = "TopLessBtn";
			this.TopLessBtn.Size = new Size(90, 70);
			this.TopLessBtn.TabIndex = 1;
			this.TopLessBtn.Text = "Less";
			this.TopLessBtn.UseVisualStyleBackColor = true;
			this.TopLessBtn.Click += new EventHandler(this.TopLessBtn_Click);
			this.LeftMoreBtn.Dock = DockStyle.Fill;
			this.LeftMoreBtn.Location = new Point(3, 79);
			this.LeftMoreBtn.Name = "LeftMoreBtn";
			this.LeftMoreBtn.Size = new Size(90, 70);
			this.LeftMoreBtn.TabIndex = 2;
			this.LeftMoreBtn.Text = "More";
			this.LeftMoreBtn.UseVisualStyleBackColor = true;
			this.LeftMoreBtn.Click += new EventHandler(this.LeftMoreBtn_Click);
			this.LeftLessBtn.Dock = DockStyle.Fill;
			this.LeftLessBtn.Location = new Point(3, 155);
			this.LeftLessBtn.Name = "LeftLessBtn";
			this.LeftLessBtn.Size = new Size(90, 70);
			this.LeftLessBtn.TabIndex = 3;
			this.LeftLessBtn.Text = "Less";
			this.LeftLessBtn.UseVisualStyleBackColor = true;
			this.LeftLessBtn.Click += new EventHandler(this.LeftLessBtn_Click);
			this.RightLessBtn.Dock = DockStyle.Fill;
			this.RightLessBtn.Location = new Point(291, 155);
			this.RightLessBtn.Name = "RightLessBtn";
			this.RightLessBtn.Size = new Size(91, 70);
			this.RightLessBtn.TabIndex = 4;
			this.RightLessBtn.Text = "Less";
			this.RightLessBtn.UseVisualStyleBackColor = true;
			this.RightLessBtn.Click += new EventHandler(this.RightLessBtn_Click);
			this.RightMoreBtn.Dock = DockStyle.Fill;
			this.RightMoreBtn.Location = new Point(291, 79);
			this.RightMoreBtn.Name = "RightMoreBtn";
			this.RightMoreBtn.Size = new Size(91, 70);
			this.RightMoreBtn.TabIndex = 5;
			this.RightMoreBtn.Text = "More";
			this.RightMoreBtn.UseVisualStyleBackColor = true;
			this.RightMoreBtn.Click += new EventHandler(this.RightMoreBtn_Click);
			this.BottomLessBtn.Dock = DockStyle.Fill;
			this.BottomLessBtn.Location = new Point(195, 231);
			this.BottomLessBtn.Name = "BottomLessBtn";
			this.BottomLessBtn.Size = new Size(90, 72);
			this.BottomLessBtn.TabIndex = 6;
			this.BottomLessBtn.Text = "Less";
			this.BottomLessBtn.UseVisualStyleBackColor = true;
			this.BottomLessBtn.Click += new EventHandler(this.BottomLessBtn_Click);
			this.BottomMoreBtn.Dock = DockStyle.Fill;
			this.BottomMoreBtn.Location = new Point(99, 231);
			this.BottomMoreBtn.Name = "BottomMoreBtn";
			this.BottomMoreBtn.Size = new Size(90, 72);
			this.BottomMoreBtn.TabIndex = 7;
			this.BottomMoreBtn.Text = "More";
			this.BottomMoreBtn.UseVisualStyleBackColor = true;
			this.BottomMoreBtn.Click += new EventHandler(this.BottomMoreBtn_Click);
			this.MapView.AllowDrawing = false;
			this.MapView.AllowDrop = true;
			this.MapView.AllowLinkCreation = false;
			this.MapView.AllowScrolling = false;
			this.MapView.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
			this.MapView.BackgroundMap = null;
			this.MapView.BorderSize = 0;
			this.MapView.BorderStyle = BorderStyle.FixedSingle;
			this.MapView.Cursor = Cursors.Default;
			this.MapView.Encounter = null;
			this.MapView.FrameType = MapDisplayType.Dimmed;
			this.MapView.HighlightAreas = false;
			this.MapView.HoverToken = null;
			this.MapView.LineOfSight = false;
			this.MapView.Location = new Point(405, 12);
			this.MapView.Map = null;
			this.MapView.Mode = MapViewMode.Thumbnail;
			this.MapView.Name = "MapView";
			this.MapView.ScalingFactor = 1.0;
			this.MapView.SelectedArea = null;
			this.MapView.SelectedTiles = null;
			this.MapView.Selection = new Rectangle(0, 0, 0, 0);
			this.MapView.ShowAuras = true;
			this.MapView.ShowConditions = true;
			this.MapView.ShowCreatureLabels = true;
			this.MapView.ShowCreatures = CreatureViewMode.All;
			this.MapView.ShowGrid = MapGridMode.None;
			this.MapView.ShowGridLabels = false;
			this.MapView.ShowHealthBars = false;
			this.MapView.ShowPictureTokens = true;
			this.MapView.Size = new Size(345, 364);
			this.MapView.TabIndex = 3;
			this.MapView.Tactical = false;
			this.MapView.TokenLinks = null;
			this.MapView.Viewpoint = new Rectangle(0, 0, 0, 0);
			this.RandomNameBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RandomNameBtn.Image = (Image)resources.GetObject("RandomNameBtn.Image");
			this.RandomNameBtn.ImageTransparentColor = Color.Magenta;
			this.RandomNameBtn.Name = "RandomNameBtn";
			this.RandomNameBtn.Size = new Size(91, 22);
			this.RandomNameBtn.Text = "Random Name";
			this.RandomNameBtn.Click += new EventHandler(this.RandomNameBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(762, 417);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.MapView);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MapAreaForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Map Area";
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.DetailsToolbar.ResumeLayout(false);
			this.DetailsToolbar.PerformLayout();
			this.MovePage.ResumeLayout(false);
			this.MoveTable.ResumeLayout(false);
			this.ResizePage.ResumeLayout(false);
			this.ResizeTable.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public MapAreaForm(MapArea area, Map m)
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.fArea = area.Copy();
			this.MapView.Map = m;
			this.MapView.Viewpoint = this.fArea.Region;
			this.NameBox.Text = this.fArea.Name;
			this.DetailsBox.Text = this.fArea.Details;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.LeftLessBtn.Enabled = (this.MapView.Viewpoint.Width != 1);
			this.RightLessBtn.Enabled = (this.MapView.Viewpoint.Width != 1);
			this.TopLessBtn.Enabled = (this.MapView.Viewpoint.Height != 1);
			this.BottomLessBtn.Enabled = (this.MapView.Viewpoint.Height != 1);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fArea.Name = this.NameBox.Text;
			this.fArea.Details = this.DetailsBox.Text;
			this.fArea.Region = this.MapView.Viewpoint;
		}

		private void MoveUpBtn_Click(object sender, EventArgs e)
		{
			this.change(0, -1, 0, 0);
		}

		private void MoveLeftBtn_Click(object sender, EventArgs e)
		{
			this.change(-1, 0, 0, 0);
		}

		private void MoveRightBtn_Click(object sender, EventArgs e)
		{
			this.change(1, 0, 0, 0);
		}

		private void MoveDownBtn_Click(object sender, EventArgs e)
		{
			this.change(0, 1, 0, 0);
		}

		private void TopMoreBtn_Click(object sender, EventArgs e)
		{
			this.change(0, -1, 0, 1);
		}

		private void TopLessBtn_Click(object sender, EventArgs e)
		{
			this.change(0, 1, 0, -1);
		}

		private void LeftMoreBtn_Click(object sender, EventArgs e)
		{
			this.change(-1, 0, 1, 0);
		}

		private void LeftLessBtn_Click(object sender, EventArgs e)
		{
			this.change(1, 0, -1, 0);
		}

		private void RightMoreBtn_Click(object sender, EventArgs e)
		{
			this.change(0, 0, 1, 0);
		}

		private void RightLessBtn_Click(object sender, EventArgs e)
		{
			this.change(0, 0, -1, 0);
		}

		private void BottomMoreBtn_Click(object sender, EventArgs e)
		{
			this.change(0, 0, 0, 1);
		}

		private void BottomLessBtn_Click(object sender, EventArgs e)
		{
			this.change(0, 0, 0, -1);
		}

		private void change(int x, int y, int width, int height)
		{
			x += this.MapView.Viewpoint.X;
			y += this.MapView.Viewpoint.Y;
			width += this.MapView.Viewpoint.Width;
			height += this.MapView.Viewpoint.Height;
			this.MapView.Viewpoint = new Rectangle(x, y, width, height);
		}

		private void RandomNameBtn_Click(object sender, EventArgs e)
		{
			string text = "";
			while (text == "")
			{
				text = RoomBuilder.Name();
			}
			this.NameBox.Text = text;
		}

		private void RandomDescBtn_Click(object sender, EventArgs e)
		{
			string text = "";
			while (text == "")
			{
				text = RoomBuilder.Details();
			}
			this.DetailsBox.Text = text;
		}
	}
}
