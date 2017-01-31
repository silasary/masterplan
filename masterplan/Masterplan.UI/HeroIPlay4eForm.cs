using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class HeroIPlay4eForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label KeyLbl;

		private TextBox KeyBox;

		private PictureBox LogoBox;

		private LinkLabel WebsiteLink;

		public string Key
		{
			get
			{
				return this.KeyBox.Text;
			}
		}

		public HeroIPlay4eForm(string key, bool character)
		{
			this.InitializeComponent();
			this.KeyBox.Text = key;
			if (!character)
			{
				this.Text = "iPlay4e Campaign";
				this.KeyLbl.Text = "Campaign Key:";
			}
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
		}

		private void WebsiteLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string fileName = "www.iplay4e.com";
			Process.Start(fileName);
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(HeroIPlay4eForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.KeyLbl = new Label();
			this.KeyBox = new TextBox();
			this.LogoBox = new PictureBox();
			this.WebsiteLink = new LinkLabel();
			((ISupportInitialize)this.LogoBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(233, 116);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "Import";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(314, 116);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.KeyLbl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.KeyLbl.AutoSize = true;
			this.KeyLbl.Location = new Point(12, 93);
			this.KeyLbl.Name = "KeyLbl";
			this.KeyLbl.Size = new Size(77, 13);
			this.KeyLbl.TabIndex = 0;
			this.KeyLbl.Text = "Character Key:";
			this.KeyBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.KeyBox.Location = new Point(95, 90);
			this.KeyBox.Name = "KeyBox";
			this.KeyBox.Size = new Size(294, 20);
			this.KeyBox.TabIndex = 1;
			this.LogoBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.LogoBox.Image = (Image)componentResourceManager.GetObject("LogoBox.Image");
			this.LogoBox.Location = new Point(12, 12);
			this.LogoBox.Name = "LogoBox";
			this.LogoBox.Size = new Size(377, 72);
			this.LogoBox.SizeMode = PictureBoxSizeMode.CenterImage;
			this.LogoBox.TabIndex = 4;
			this.LogoBox.TabStop = false;
			this.WebsiteLink.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.WebsiteLink.AutoSize = true;
			this.WebsiteLink.Location = new Point(12, 121);
			this.WebsiteLink.Name = "WebsiteLink";
			this.WebsiteLink.Size = new Size(109, 13);
			this.WebsiteLink.TabIndex = 2;
			this.WebsiteLink.TabStop = true;
			this.WebsiteLink.Text = "Go to iPlay4e website";
			this.WebsiteLink.LinkClicked += new LinkLabelLinkClickedEventHandler(this.WebsiteLink_LinkClicked);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(401, 151);
			base.Controls.Add(this.WebsiteLink);
			base.Controls.Add(this.LogoBox);
			base.Controls.Add(this.KeyBox);
			base.Controls.Add(this.KeyLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "HeroIPlay4eForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "iPlay4e Character";
			((ISupportInitialize)this.LogoBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
