using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.Tools.Generators;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class ParcelForm : Form
	{
		private Parcel fParcel;

		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private Button OKBtn;

		private Button CancelBtn;

		private TextBox DetailsBox;

		private ToolStrip Toolbar;

		private WebBrowser Browser;

		private ToolStripDropDownButton ChangeTo;

		private ToolStripMenuItem ChangeToMundaneParcel;

		private ToolStripMenuItem ChangeToMagicItem;

		private ToolStripMenuItem ChangeToArtifact;

		private ToolStripButton SelectBtn;

		private Panel MainPanel;

		private Panel DetailsPanel;

		private ToolStripButton RandomiseBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripSeparator toolStripSeparator2;

		public Parcel Parcel
		{
			get
			{
				return this.fParcel;
			}
		}

		public ParcelForm(Parcel p)
		{
			this.InitializeComponent();
			this.fParcel = p.Copy();
			this.set_controls();
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			if (this.fParcel.MagicItemID == Guid.Empty && this.fParcel.ArtifactID == Guid.Empty)
			{
				this.fParcel.Name = this.NameBox.Text;
				this.fParcel.Details = this.DetailsBox.Text;
			}
		}

		private void ChangeToMundaneParcel_Click(object sender, EventArgs e)
		{
			this.fParcel.MagicItemID = Guid.Empty;
			this.fParcel.ArtifactID = Guid.Empty;
			this.fParcel.Name = "";
			this.fParcel.Details = "";
			this.set_controls();
		}

		private void ChangeToMagicItem_Click(object sender, EventArgs e)
		{
			MagicItemSelectForm magicItemSelectForm = new MagicItemSelectForm(this.fParcel.FindItemLevel());
			if (magicItemSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fParcel.SetAsMagicItem(magicItemSelectForm.MagicItem);
				this.NameBox.Text = this.fParcel.Name;
				this.DetailsBox.Text = this.fParcel.Details;
				this.set_controls();
			}
		}

		private void ChangeToArtifact_Click(object sender, EventArgs e)
		{
			ArtifactSelectForm artifactSelectForm = new ArtifactSelectForm();
			if (artifactSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fParcel.SetAsArtifact(artifactSelectForm.Artifact);
				this.NameBox.Text = this.fParcel.Name;
				this.DetailsBox.Text = this.fParcel.Details;
				this.set_controls();
			}
		}

		private void SelectBtn_Click(object sender, EventArgs e)
		{
			if (this.fParcel.MagicItemID != Guid.Empty)
			{
				this.ChangeToMagicItem_Click(this, e);
				return;
			}
			if (this.fParcel.ArtifactID != Guid.Empty)
			{
				this.ChangeToArtifact_Click(this, e);
			}
		}

		private void RandomiseBtn_Click(object sender, EventArgs e)
		{
			if (this.fParcel.MagicItemID != Guid.Empty)
			{
				MagicItem magicItem = Treasure.RandomMagicItem(this.fParcel.FindItemLevel());
				if (magicItem != null)
				{
					this.fParcel.SetAsMagicItem(magicItem);
				}
				this.set_controls();
				return;
			}
			if (this.fParcel.ArtifactID != Guid.Empty)
			{
				Artifact artifact = Treasure.RandomArtifact(this.fParcel.FindItemTier());
				if (artifact != null)
				{
					this.fParcel.SetAsArtifact(artifact);
				}
				this.set_controls();
				return;
			}
			int num = this.fParcel.Value;
			if (num == 0)
			{
				num = Treasure.GetItemValue(Session.Project.Party.Level);
			}
			this.fParcel = Treasure.CreateParcel(num, false);
			this.NameBox.Text = this.fParcel.Name;
			this.DetailsBox.Text = this.fParcel.Details;
			this.set_controls();
		}

		private void set_controls()
		{
			bool flag = this.fParcel.MagicItemID != Guid.Empty;
			bool flag2 = this.fParcel.ArtifactID != Guid.Empty;
			bool flag3 = !flag && !flag2;
			this.ChangeToMundaneParcel.Enabled = !flag3;
			this.ChangeToMagicItem.Enabled = (!flag && Session.MagicItems.Count != 0);
			this.ChangeToArtifact.Enabled = (!flag2 && Session.Artifacts.Count != 0);
			this.Browser.Visible = !flag3;
			this.DetailsPanel.Visible = flag3;
			this.SelectBtn.Enabled = (flag || flag2);
			if (flag3)
			{
				this.NameBox.Text = this.fParcel.Name;
				this.DetailsBox.Text = this.fParcel.Details;
				return;
			}
			MagicItem magicItem = Session.FindMagicItem(this.fParcel.MagicItemID, SearchType.Global);
			if (magicItem != null)
			{
				string documentText = HTML.MagicItem(magicItem, DisplaySize.Small, false, true);
				this.Browser.DocumentText = documentText;
			}
			Artifact artifact = Session.FindArtifact(this.fParcel.ArtifactID, SearchType.Global);
			if (artifact != null)
			{
				string documentText2 = HTML.Artifact(artifact, DisplaySize.Small, false, true);
				this.Browser.DocumentText = documentText2;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ParcelForm));
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.DetailsBox = new TextBox();
			this.Toolbar = new ToolStrip();
			this.ChangeTo = new ToolStripDropDownButton();
			this.ChangeToMundaneParcel = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.ChangeToMagicItem = new ToolStripMenuItem();
			this.ChangeToArtifact = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.SelectBtn = new ToolStripButton();
			this.RandomiseBtn = new ToolStripButton();
			this.Browser = new WebBrowser();
			this.MainPanel = new Panel();
			this.DetailsPanel = new Panel();
			this.Toolbar.SuspendLayout();
			this.MainPanel.SuspendLayout();
			this.DetailsPanel.SuspendLayout();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(3, 6);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(47, 3);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(297, 20);
			this.NameBox.TabIndex = 1;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(203, 359);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 5;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(284, 359);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 6;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.DetailsBox.AcceptsReturn = true;
			this.DetailsBox.AcceptsTab = true;
			this.DetailsBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.DetailsBox.Location = new Point(3, 29);
			this.DetailsBox.Multiline = true;
			this.DetailsBox.Name = "DetailsBox";
			this.DetailsBox.ScrollBars = ScrollBars.Vertical;
			this.DetailsBox.Size = new Size(341, 284);
			this.DetailsBox.TabIndex = 0;
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.ChangeTo,
				this.toolStripSeparator1,
				this.SelectBtn,
				this.RandomiseBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(347, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.ChangeTo.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChangeTo.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ChangeToMundaneParcel,
				this.toolStripSeparator2,
				this.ChangeToMagicItem,
				this.ChangeToArtifact
			});
			this.ChangeTo.Image = (Image)componentResourceManager.GetObject("ChangeTo.Image");
			this.ChangeTo.ImageTransparentColor = Color.Magenta;
			this.ChangeTo.Name = "ChangeTo";
			this.ChangeTo.Size = new Size(78, 22);
			this.ChangeTo.Text = "Change To";
			this.ChangeToMundaneParcel.Name = "ChangeToMundaneParcel";
			this.ChangeToMundaneParcel.Size = new Size(160, 22);
			this.ChangeToMundaneParcel.Text = "Mundane Parcel";
			this.ChangeToMundaneParcel.Click += new EventHandler(this.ChangeToMundaneParcel_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(157, 6);
			this.ChangeToMagicItem.Name = "ChangeToMagicItem";
			this.ChangeToMagicItem.Size = new Size(160, 22);
			this.ChangeToMagicItem.Text = "Magic Item...";
			this.ChangeToMagicItem.Click += new EventHandler(this.ChangeToMagicItem_Click);
			this.ChangeToArtifact.Name = "ChangeToArtifact";
			this.ChangeToArtifact.Size = new Size(160, 22);
			this.ChangeToArtifact.Text = "Artifact...";
			this.ChangeToArtifact.Click += new EventHandler(this.ChangeToArtifact_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.SelectBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SelectBtn.Image = (Image)componentResourceManager.GetObject("SelectBtn.Image");
			this.SelectBtn.ImageTransparentColor = Color.Magenta;
			this.SelectBtn.Name = "SelectBtn";
			this.SelectBtn.Size = new Size(42, 22);
			this.SelectBtn.Text = "Select";
			this.SelectBtn.Click += new EventHandler(this.SelectBtn_Click);
			this.RandomiseBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RandomiseBtn.Image = (Image)componentResourceManager.GetObject("RandomiseBtn.Image");
			this.RandomiseBtn.ImageTransparentColor = Color.Magenta;
			this.RandomiseBtn.Name = "RandomiseBtn";
			this.RandomiseBtn.Size = new Size(70, 22);
			this.RandomiseBtn.Text = "Randomise";
			this.RandomiseBtn.Click += new EventHandler(this.RandomiseBtn_Click);
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.Location = new Point(0, 25);
			this.Browser.MinimumSize = new Size(20, 20);
			this.Browser.Name = "Browser";
			this.Browser.Size = new Size(347, 316);
			this.Browser.TabIndex = 1;
			this.MainPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.MainPanel.Controls.Add(this.Browser);
			this.MainPanel.Controls.Add(this.DetailsPanel);
			this.MainPanel.Controls.Add(this.Toolbar);
			this.MainPanel.Location = new Point(12, 12);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new Size(347, 341);
			this.MainPanel.TabIndex = 9;
			this.DetailsPanel.Controls.Add(this.DetailsBox);
			this.DetailsPanel.Controls.Add(this.NameBox);
			this.DetailsPanel.Controls.Add(this.NameLbl);
			this.DetailsPanel.Dock = DockStyle.Fill;
			this.DetailsPanel.Location = new Point(0, 25);
			this.DetailsPanel.Name = "DetailsPanel";
			this.DetailsPanel.Size = new Size(347, 316);
			this.DetailsPanel.TabIndex = 2;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(371, 394);
			base.Controls.Add(this.MainPanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ParcelForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Treasure Parcel";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.MainPanel.ResumeLayout(false);
			this.MainPanel.PerformLayout();
			this.DetailsPanel.ResumeLayout(false);
			this.DetailsPanel.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
