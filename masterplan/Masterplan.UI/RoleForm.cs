using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class RoleForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label RoleLbl;

		private ComboBox RoleBox;

		private Label ModLbl;

		private ComboBox ModBox;

		private CheckBox LeaderBox;

		private RadioButton StandardBtn;

		private RadioButton MinionBtn;

		private CheckBox HasRoleBox;

		private ComboBox MinionRoleBox;

		private Label MinionRoleLbl;

		private IRole fRole;

		public IRole Role
		{
			get
			{
				return this.fRole;
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
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.RoleLbl = new Label();
			this.RoleBox = new ComboBox();
			this.ModLbl = new Label();
			this.ModBox = new ComboBox();
			this.LeaderBox = new CheckBox();
			this.StandardBtn = new RadioButton();
			this.MinionBtn = new RadioButton();
			this.HasRoleBox = new CheckBox();
			this.MinionRoleBox = new ComboBox();
			this.MinionRoleLbl = new Label();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(66, 216);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 10;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(147, 216);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 11;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.RoleLbl.AutoSize = true;
			this.RoleLbl.Location = new Point(32, 38);
			this.RoleLbl.Name = "RoleLbl";
			this.RoleLbl.Size = new Size(32, 13);
			this.RoleLbl.TabIndex = 1;
			this.RoleLbl.Text = "Role:";
			this.RoleBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RoleBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.RoleBox.FormattingEnabled = true;
			this.RoleBox.Location = new Point(85, 35);
			this.RoleBox.Name = "RoleBox";
			this.RoleBox.Size = new Size(137, 21);
			this.RoleBox.TabIndex = 2;
			this.ModLbl.AutoSize = true;
			this.ModLbl.Location = new Point(32, 65);
			this.ModLbl.Name = "ModLbl";
			this.ModLbl.Size = new Size(47, 13);
			this.ModLbl.TabIndex = 3;
			this.ModLbl.Text = "Modifier:";
			this.ModBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ModBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.ModBox.FormattingEnabled = true;
			this.ModBox.Location = new Point(85, 62);
			this.ModBox.Name = "ModBox";
			this.ModBox.Size = new Size(137, 21);
			this.ModBox.TabIndex = 4;
			this.LeaderBox.AutoSize = true;
			this.LeaderBox.Location = new Point(35, 89);
			this.LeaderBox.Name = "LeaderBox";
			this.LeaderBox.Size = new Size(139, 17);
			this.LeaderBox.TabIndex = 5;
			this.LeaderBox.Text = "This creature is a leader";
			this.LeaderBox.UseVisualStyleBackColor = true;
			this.StandardBtn.AutoSize = true;
			this.StandardBtn.Location = new Point(12, 12);
			this.StandardBtn.Name = "StandardBtn";
			this.StandardBtn.Size = new Size(93, 17);
			this.StandardBtn.TabIndex = 0;
			this.StandardBtn.TabStop = true;
			this.StandardBtn.Text = "Standard Role";
			this.StandardBtn.UseVisualStyleBackColor = true;
			this.MinionBtn.AutoSize = true;
			this.MinionBtn.Location = new Point(12, 126);
			this.MinionBtn.Name = "MinionBtn";
			this.MinionBtn.Size = new Size(56, 17);
			this.MinionBtn.TabIndex = 6;
			this.MinionBtn.TabStop = true;
			this.MinionBtn.Text = "Minion";
			this.MinionBtn.UseVisualStyleBackColor = true;
			this.HasRoleBox.AutoSize = true;
			this.HasRoleBox.Location = new Point(35, 149);
			this.HasRoleBox.Name = "HasRoleBox";
			this.HasRoleBox.Size = new Size(99, 17);
			this.HasRoleBox.TabIndex = 7;
			this.HasRoleBox.Text = "Minion with role";
			this.HasRoleBox.UseVisualStyleBackColor = true;
			this.MinionRoleBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.MinionRoleBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.MinionRoleBox.FormattingEnabled = true;
			this.MinionRoleBox.Location = new Point(85, 172);
			this.MinionRoleBox.Name = "MinionRoleBox";
			this.MinionRoleBox.Size = new Size(137, 21);
			this.MinionRoleBox.TabIndex = 9;
			this.MinionRoleLbl.AutoSize = true;
			this.MinionRoleLbl.Location = new Point(32, 175);
			this.MinionRoleLbl.Name = "MinionRoleLbl";
			this.MinionRoleLbl.Size = new Size(32, 13);
			this.MinionRoleLbl.TabIndex = 8;
			this.MinionRoleLbl.Text = "Role:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(234, 251);
			base.Controls.Add(this.MinionRoleLbl);
			base.Controls.Add(this.MinionRoleBox);
			base.Controls.Add(this.HasRoleBox);
			base.Controls.Add(this.MinionBtn);
			base.Controls.Add(this.StandardBtn);
			base.Controls.Add(this.LeaderBox);
			base.Controls.Add(this.ModBox);
			base.Controls.Add(this.ModLbl);
			base.Controls.Add(this.RoleBox);
			base.Controls.Add(this.RoleLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "RoleForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Role";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public RoleForm(IRole r, ThreatType type)
		{
			this.InitializeComponent();
			List<RoleType> list = new List<RoleType>();
			switch (type)
			{
			case ThreatType.Creature:
				list.Add(RoleType.Artillery);
				list.Add(RoleType.Brute);
				list.Add(RoleType.Controller);
				list.Add(RoleType.Lurker);
				list.Add(RoleType.Skirmisher);
				list.Add(RoleType.Soldier);
				break;
			case ThreatType.Trap:
				list.Add(RoleType.Blaster);
				list.Add(RoleType.Lurker);
				list.Add(RoleType.Obstacle);
				list.Add(RoleType.Warder);
				this.LeaderBox.Text = "This trap is a leader";
				break;
			}
			foreach (RoleType current in list)
			{
				this.RoleBox.Items.Add(current);
				this.MinionRoleBox.Items.Add(current);
			}
			foreach (RoleFlag roleFlag in Enum.GetValues(typeof(RoleFlag)))
			{
				this.ModBox.Items.Add(roleFlag);
			}
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fRole = r.Copy();
			if (this.fRole is ComplexRole)
			{
				this.StandardBtn.Checked = true;
				ComplexRole complexRole = this.fRole as ComplexRole;
				this.RoleBox.SelectedItem = complexRole.Type;
				this.MinionRoleBox.SelectedItem = complexRole.Type;
				this.ModBox.SelectedItem = complexRole.Flag;
				this.LeaderBox.Checked = complexRole.Leader;
				this.HasRoleBox.Checked = false;
			}
			if (this.fRole is Minion)
			{
				this.MinionBtn.Checked = true;
				Minion minion = this.fRole as Minion;
				this.RoleBox.SelectedItem = minion.Type;
				this.MinionRoleBox.SelectedItem = minion.Type;
				this.ModBox.SelectedItem = RoleFlag.Standard;
				this.LeaderBox.Checked = false;
				this.HasRoleBox.Checked = minion.HasRole;
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RoleLbl.Enabled = this.StandardBtn.Checked;
			this.RoleBox.Enabled = this.StandardBtn.Checked;
			this.ModLbl.Enabled = this.StandardBtn.Checked;
			this.ModBox.Enabled = this.StandardBtn.Checked;
			this.LeaderBox.Enabled = this.StandardBtn.Checked;
			this.HasRoleBox.Enabled = this.MinionBtn.Checked;
			this.MinionRoleLbl.Enabled = (this.MinionBtn.Checked && this.HasRoleBox.Checked);
			this.MinionRoleBox.Enabled = (this.MinionBtn.Checked && this.HasRoleBox.Checked);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			if (this.StandardBtn.Checked)
			{
				this.fRole = new ComplexRole
				{
					Type = (RoleType)this.RoleBox.SelectedItem,
					Flag = (RoleFlag)this.ModBox.SelectedItem,
					Leader = this.LeaderBox.Checked
				};
			}
			if (this.MinionBtn.Checked)
			{
				this.fRole = new Minion
				{
					HasRole = this.HasRoleBox.Checked,
					Type = (RoleType)this.MinionRoleBox.SelectedItem
				};
			}
		}
	}
}
