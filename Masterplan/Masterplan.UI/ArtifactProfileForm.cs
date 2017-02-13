using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class ArtifactProfileForm : Form
	{
		private Artifact fArtifact;

		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private Button OKBtn;

		private Button CancelBtn;

		private ComboBox TierBox;

		private Label TierLbl;

		public Artifact Artifact
		{
			get
			{
				return this.fArtifact;
			}
		}

		public ArtifactProfileForm(Artifact artifact)
		{
			this.InitializeComponent();
			this.fArtifact = artifact;
			foreach (Tier tier in Enum.GetValues(typeof(Tier)))
			{
				this.TierBox.Items.Add(tier);
			}
			this.NameBox.Text = this.fArtifact.Name;
			this.TierBox.SelectedItem = this.fArtifact.Tier;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fArtifact.Name = this.NameBox.Text;
			this.fArtifact.Tier = (Tier)this.TierBox.SelectedItem;
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
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.TierBox = new ComboBox();
			this.TierLbl = new Label();
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
			this.NameBox.Size = new Size(260, 20);
			this.NameBox.TabIndex = 1;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(160, 76);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 4;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(241, 76);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 5;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.TierBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TierBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.TierBox.FormattingEnabled = true;
			this.TierBox.Location = new Point(56, 38);
			this.TierBox.Name = "TierBox";
			this.TierBox.Size = new Size(260, 21);
			this.TierBox.TabIndex = 3;
			this.TierLbl.AutoSize = true;
			this.TierLbl.Location = new Point(12, 41);
			this.TierLbl.Name = "TierLbl";
			this.TierLbl.Size = new Size(28, 13);
			this.TierLbl.TabIndex = 2;
			this.TierLbl.Text = "Tier:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(328, 111);
			base.Controls.Add(this.TierBox);
			base.Controls.Add(this.TierLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ArtifactProfileForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Artifact";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
