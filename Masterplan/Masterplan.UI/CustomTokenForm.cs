using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CustomTokenForm : Form
	{
		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private Button OKBtn;

		private Button CancelBtn;

		private TabControl Pages;

		private TabPage DetailsPage;

		private TextBox DetailsBox;

		private ComboBox SizeBox;

		private Label SizeLbl;

		private TabPage PicturePage;

		private TokenPanel TilePanel;

		private TabPage TerrainPowerPage;

		private WebBrowser PowerBrowser;

		private ToolStrip TerrainPowerToolbar;

		private ToolStripButton EditBtn;

		private ToolStripButton RemoveBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton SelectBtn;

		private CustomToken fToken;

		public CustomToken Token
		{
			get
			{
				return this.fToken;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(CustomTokenForm));
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.Pages = new TabControl();
			this.DetailsPage = new TabPage();
			this.DetailsBox = new TextBox();
			this.PicturePage = new TabPage();
			this.TilePanel = new TokenPanel();
			this.TerrainPowerPage = new TabPage();
			this.PowerBrowser = new WebBrowser();
			this.TerrainPowerToolbar = new ToolStrip();
			this.EditBtn = new ToolStripButton();
			this.RemoveBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.SelectBtn = new ToolStripButton();
			this.SizeBox = new ComboBox();
			this.SizeLbl = new Label();
			this.Pages.SuspendLayout();
			this.DetailsPage.SuspendLayout();
			this.PicturePage.SuspendLayout();
			this.TerrainPowerPage.SuspendLayout();
			this.TerrainPowerToolbar.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(58, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(304, 20);
			this.NameBox.TabIndex = 1;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(206, 283);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 8;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(287, 283);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 9;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.Pages.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Pages.Controls.Add(this.DetailsPage);
			this.Pages.Controls.Add(this.PicturePage);
			this.Pages.Controls.Add(this.TerrainPowerPage);
			this.Pages.Location = new Point(15, 65);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(350, 212);
			this.Pages.TabIndex = 7;
			this.DetailsPage.Controls.Add(this.DetailsBox);
			this.DetailsPage.Location = new Point(4, 22);
			this.DetailsPage.Name = "DetailsPage";
			this.DetailsPage.Padding = new Padding(3);
			this.DetailsPage.Size = new Size(342, 186);
			this.DetailsPage.TabIndex = 0;
			this.DetailsPage.Text = "Details";
			this.DetailsPage.UseVisualStyleBackColor = true;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.Dock = DockStyle.Fill;
			this.DetailsBox.Location = new Point(3, 3);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new Size(336, 180);
			this.DetailsBox.TabIndex = 0;
			this.PicturePage.Controls.Add(this.TilePanel);
			this.PicturePage.Location = new Point(4, 22);
			this.PicturePage.Name = "PicturePage";
			this.PicturePage.Padding = new Padding(3);
			this.PicturePage.Size = new Size(317, 186);
			this.PicturePage.TabIndex = 1;
			this.PicturePage.Text = "Picture";
			this.PicturePage.UseVisualStyleBackColor = true;
			this.TilePanel.Colour = Color.Transparent;
			this.TilePanel.Dock = DockStyle.Fill;
			this.TilePanel.Image = null;
			this.TilePanel.Location = new Point(3, 3);
			this.TilePanel.Name = "TilePanel";
			this.TilePanel.Size = new Size(311, 180);
			this.TilePanel.TabIndex = 0;
			this.TilePanel.TileSize = new Size(2, 2);
			this.TerrainPowerPage.Controls.Add(this.PowerBrowser);
			this.TerrainPowerPage.Controls.Add(this.TerrainPowerToolbar);
			this.TerrainPowerPage.Location = new Point(4, 22);
			this.TerrainPowerPage.Name = "TerrainPowerPage";
			this.TerrainPowerPage.Padding = new Padding(3);
			this.TerrainPowerPage.Size = new Size(317, 186);
			this.TerrainPowerPage.TabIndex = 2;
			this.TerrainPowerPage.Text = "Terrain Power";
			this.TerrainPowerPage.UseVisualStyleBackColor = true;
			this.PowerBrowser.Dock = DockStyle.Fill;
			this.PowerBrowser.Location = new Point(3, 28);
			this.PowerBrowser.MinimumSize = new Size(20, 20);
			this.PowerBrowser.Name = "PowerBrowser";
			this.PowerBrowser.Size = new Size(311, 155);
			this.PowerBrowser.TabIndex = 1;
			this.TerrainPowerToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.EditBtn,
				this.RemoveBtn,
				this.toolStripSeparator1,
				this.SelectBtn
			});
			this.TerrainPowerToolbar.Location = new Point(3, 3);
			this.TerrainPowerToolbar.Name = "TerrainPowerToolbar";
			this.TerrainPowerToolbar.Size = new Size(311, 25);
			this.TerrainPowerToolbar.TabIndex = 0;
			this.TerrainPowerToolbar.Text = "toolStrip1";
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)resources.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new Size(31, 22);
			this.EditBtn.Text = "Edit";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)resources.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.SelectBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SelectBtn.Image = (Image)resources.GetObject("SelectBtn.Image");
			this.SelectBtn.ImageTransparentColor = Color.Magenta;
			this.SelectBtn.Name = "SelectBtn";
			this.SelectBtn.Size = new Size(116, 22);
			this.SelectBtn.Text = "Use Standard Power";
			this.SelectBtn.Click += new EventHandler(this.SelectBtn_Click);
			this.SizeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.SizeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.SizeBox.FormattingEnabled = true;
			this.SizeBox.Location = new Point(58, 38);
			this.SizeBox.Name = "SizeBox";
			this.SizeBox.Size = new Size(304, 21);
			this.SizeBox.TabIndex = 3;
			this.SizeBox.SelectedIndexChanged += new EventHandler(this.SizeBox_SelectedIndexChanged);
			this.SizeLbl.AutoSize = true;
			this.SizeLbl.Location = new Point(12, 41);
			this.SizeLbl.Name = "SizeLbl";
			this.SizeLbl.Size = new Size(30, 13);
			this.SizeLbl.TabIndex = 2;
			this.SizeLbl.Text = "Size:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(374, 318);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.SizeBox);
			base.Controls.Add(this.SizeLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CustomTokenForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Custom Map Token";
			this.Pages.ResumeLayout(false);
			this.DetailsPage.ResumeLayout(false);
			this.DetailsPage.PerformLayout();
			this.PicturePage.ResumeLayout(false);
			this.TerrainPowerPage.ResumeLayout(false);
			this.TerrainPowerPage.PerformLayout();
			this.TerrainPowerToolbar.ResumeLayout(false);
			this.TerrainPowerToolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public CustomTokenForm(CustomToken ct)
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			Array values = Enum.GetValues(typeof(CreatureSize));
			foreach (CreatureSize creatureSize in values)
			{
				this.SizeBox.Items.Add(creatureSize);
			}
			this.fToken = ct.Copy();
			this.NameBox.Text = this.fToken.Name;
			this.SizeBox.SelectedItem = this.fToken.TokenSize;
			this.update_power();
			this.DetailsBox.Text = this.fToken.Details;
			int size = Creature.GetSize((CreatureSize)this.SizeBox.SelectedItem);
			this.TilePanel.TileSize = new Size(size, size);
			this.TilePanel.Image = this.fToken.Image;
			this.TilePanel.Colour = this.fToken.Colour;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = (this.fToken.TerrainPower != null);
			this.SelectBtn.Enabled = (Session.TerrainPowers.Count != 0);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fToken.Name = this.NameBox.Text;
			this.fToken.TokenSize = (CreatureSize)this.SizeBox.SelectedItem;
			this.fToken.Details = this.DetailsBox.Text;
			this.fToken.Image = this.TilePanel.Image;
			this.fToken.Colour = this.TilePanel.Colour;
		}

		private void SizeBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			int size = Creature.GetSize((CreatureSize)this.SizeBox.SelectedItem);
			this.TilePanel.TileSize = new Size(size, size);
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			TerrainPower terrainPower = this.fToken.TerrainPower;
			if (terrainPower == null)
			{
				terrainPower = new TerrainPower();
				terrainPower.Name = this.NameBox.Text;
			}
			TerrainPowerForm terrainPowerForm = new TerrainPowerForm(terrainPower);
			if (terrainPowerForm.ShowDialog() == DialogResult.OK)
			{
				this.fToken.TerrainPower = terrainPowerForm.Power;
				this.NameBox.Text = this.fToken.TerrainPower.Name;
				this.update_power();
			}
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			this.fToken.TerrainPower = null;
			this.update_power();
		}

		private void SelectBtn_Click(object sender, EventArgs e)
		{
			TerrainPowerSelectForm terrainPowerSelectForm = new TerrainPowerSelectForm();
			if (terrainPowerSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fToken.TerrainPower = terrainPowerSelectForm.TerrainPower.Copy();
				this.update_power();
			}
		}

		private void update_power()
		{
			this.PowerBrowser.DocumentText = HTML.TerrainPower(this.fToken.TerrainPower, DisplaySize.Small);
		}
	}
}
