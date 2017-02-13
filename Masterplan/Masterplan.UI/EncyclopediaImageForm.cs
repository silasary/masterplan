using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class EncyclopediaImageForm : Form
	{
		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private Panel Panel;

		private ToolStrip Toolbar;

		private ToolStripButton BrowseBtn;

		private Button OKBtn;

		private Button CancelBtn;

		private PictureBox PictureBox;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton PlayerViewBtn;

		private ToolStripButton PasteBtn;

		private EncyclopediaImage fImage;

		public EncyclopediaImage Image
		{
			get
			{
				return this.fImage;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(EncyclopediaImageForm));
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.Panel = new Panel();
			this.PictureBox = new PictureBox();
			this.Toolbar = new ToolStrip();
			this.BrowseBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.PlayerViewBtn = new ToolStripButton();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.PasteBtn = new ToolStripButton();
			this.Panel.SuspendLayout();
			((ISupportInitialize)this.PictureBox).BeginInit();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(74, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Picture Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(92, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(303, 20);
			this.NameBox.TabIndex = 1;
			this.Panel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Panel.BorderStyle = BorderStyle.FixedSingle;
			this.Panel.Controls.Add(this.PictureBox);
			this.Panel.Controls.Add(this.Toolbar);
			this.Panel.Location = new Point(12, 38);
			this.Panel.Name = "Panel";
			this.Panel.Size = new Size(383, 305);
			this.Panel.TabIndex = 2;
			this.PictureBox.Dock = DockStyle.Fill;
			this.PictureBox.Location = new Point(0, 25);
			this.PictureBox.Name = "PictureBox";
			this.PictureBox.Size = new Size(381, 278);
			this.PictureBox.SizeMode = PictureBoxSizeMode.Zoom;
			this.PictureBox.TabIndex = 1;
			this.PictureBox.TabStop = false;
			this.PictureBox.DoubleClick += new EventHandler(this.BrowseBtn_Click);
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.BrowseBtn,
				this.PasteBtn,
				this.toolStripSeparator1,
				this.PlayerViewBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(381, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.BrowseBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.BrowseBtn.Image = (Image)resources.GetObject("BrowseBtn.Image");
			this.BrowseBtn.ImageTransparentColor = Color.Magenta;
			this.BrowseBtn.Name = "BrowseBtn";
			this.BrowseBtn.Size = new Size(82, 22);
			this.BrowseBtn.Text = "Select Picture";
			this.BrowseBtn.Click += new EventHandler(this.BrowseBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.PlayerViewBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PlayerViewBtn.Image = (Image)resources.GetObject("PlayerViewBtn.Image");
			this.PlayerViewBtn.ImageTransparentColor = Color.Magenta;
			this.PlayerViewBtn.Name = "PlayerViewBtn";
			this.PlayerViewBtn.Size = new Size(114, 22);
			this.PlayerViewBtn.Text = "Send to Player View";
			this.PlayerViewBtn.Click += new EventHandler(this.PlayerViewBtn_Click);
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(239, 349);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(320, 349);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.PasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PasteBtn.Image = (Image)resources.GetObject("PasteBtn.Image");
			this.PasteBtn.ImageTransparentColor = Color.Magenta;
			this.PasteBtn.Name = "PasteBtn";
			this.PasteBtn.Size = new Size(79, 22);
			this.PasteBtn.Text = "Paste Picture";
			this.PasteBtn.Click += new EventHandler(this.PasteBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(407, 384);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.Panel);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MinimizeBox = false;
			base.Name = "EncyclopediaImageForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Encyclopedia Picture";
			this.Panel.ResumeLayout(false);
			this.Panel.PerformLayout();
			((ISupportInitialize)this.PictureBox).EndInit();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public EncyclopediaImageForm(EncyclopediaImage img)
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.fImage = img.Copy();
			this.NameBox.Text = this.fImage.Name;
			this.PictureBox.Image = this.fImage.Image;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.PasteBtn.Enabled = Clipboard.ContainsImage();
			this.PlayerViewBtn.Enabled = (this.fImage.Image != null);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fImage.Name = this.NameBox.Text;
		}

		private void BrowseBtn_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = Program.ImageFilter;
			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			this.fImage.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);
			Program.SetResolution(this.fImage.Image);
			this.PictureBox.Image = this.fImage.Image;
		}

		private void PasteBtn_Click(object sender, EventArgs e)
		{
			if (Clipboard.ContainsImage())
			{
				this.fImage.Image = Clipboard.GetImage();
				Program.SetResolution(this.fImage.Image);
				this.PictureBox.Image = this.fImage.Image;
			}
		}

		private void PlayerViewBtn_Click(object sender, EventArgs e)
		{
			if (Session.PlayerView == null)
			{
				Session.PlayerView = new PlayerViewForm(this);
			}
			Session.PlayerView.ShowImage(this.fImage.Image);
		}
	}
}
