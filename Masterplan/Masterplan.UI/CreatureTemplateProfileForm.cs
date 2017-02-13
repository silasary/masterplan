using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CreatureTemplateProfileForm : Form
	{
		private CreatureTemplate fTemplate;

		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private Button OKBtn;

		private Button CancelBtn;

		private ComboBox RoleBox;

		private Label RoleLbl;

		private CheckBox LeaderBox;

		private ComboBox TypeBox;

		private Label TypeLbl;

		public CreatureTemplate Template
		{
			get
			{
				return this.fTemplate;
			}
		}

		public CreatureTemplateProfileForm(CreatureTemplate t)
		{
			this.InitializeComponent();
			this.TypeBox.Items.Add(CreatureTemplateType.Functional);
			this.TypeBox.Items.Add(CreatureTemplateType.Class);
			this.RoleBox.Items.Add(RoleType.Artillery);
			this.RoleBox.Items.Add(RoleType.Brute);
			this.RoleBox.Items.Add(RoleType.Controller);
			this.RoleBox.Items.Add(RoleType.Lurker);
			this.RoleBox.Items.Add(RoleType.Skirmisher);
			this.RoleBox.Items.Add(RoleType.Soldier);
			this.fTemplate = t.Copy();
			this.NameBox.Text = this.fTemplate.Name;
			this.TypeBox.SelectedItem = this.fTemplate.Type;
			this.RoleBox.SelectedItem = this.fTemplate.Role;
			this.LeaderBox.Checked = this.fTemplate.Leader;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fTemplate.Name = this.NameBox.Text;
			this.fTemplate.Type = (CreatureTemplateType)this.TypeBox.SelectedItem;
			this.fTemplate.Role = (RoleType)this.RoleBox.SelectedItem;
			this.fTemplate.Leader = this.LeaderBox.Checked;
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
			this.RoleBox = new ComboBox();
			this.RoleLbl = new Label();
			this.LeaderBox = new CheckBox();
			this.TypeBox = new ComboBox();
			this.TypeLbl = new Label();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(78, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(222, 20);
			this.NameBox.TabIndex = 1;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(144, 123);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 14;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(225, 123);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 15;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.RoleBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RoleBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.RoleBox.FormattingEnabled = true;
			this.RoleBox.Location = new Point(78, 65);
			this.RoleBox.Name = "RoleBox";
			this.RoleBox.Size = new Size(222, 21);
			this.RoleBox.TabIndex = 5;
			this.RoleLbl.AutoSize = true;
			this.RoleLbl.Location = new Point(12, 68);
			this.RoleLbl.Name = "RoleLbl";
			this.RoleLbl.Size = new Size(32, 13);
			this.RoleLbl.TabIndex = 4;
			this.RoleLbl.Text = "Role:";
			this.LeaderBox.AutoSize = true;
			this.LeaderBox.Location = new Point(78, 92);
			this.LeaderBox.Name = "LeaderBox";
			this.LeaderBox.Size = new Size(139, 17);
			this.LeaderBox.TabIndex = 6;
			this.LeaderBox.Text = "This creature is a leader";
			this.LeaderBox.UseVisualStyleBackColor = true;
			this.TypeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.TypeBox.FormattingEnabled = true;
			this.TypeBox.Location = new Point(78, 38);
			this.TypeBox.Name = "TypeBox";
			this.TypeBox.Size = new Size(222, 21);
			this.TypeBox.TabIndex = 3;
			this.TypeLbl.AutoSize = true;
			this.TypeLbl.Location = new Point(12, 41);
			this.TypeLbl.Name = "TypeLbl";
			this.TypeLbl.Size = new Size(34, 13);
			this.TypeLbl.TabIndex = 2;
			this.TypeLbl.Text = "Type:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(312, 158);
			base.Controls.Add(this.TypeBox);
			base.Controls.Add(this.TypeLbl);
			base.Controls.Add(this.LeaderBox);
			base.Controls.Add(this.RoleBox);
			base.Controls.Add(this.RoleLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CreatureTemplateProfileForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Creature Template Profile";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
