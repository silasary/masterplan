using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class MonsterThemePowerForm : Form
	{
		private ThemePowerData fPower;

		private IContainer components;

		private Label TypeLbl;

		private ComboBox TypeBox;

		private CheckBox ArtilleryBox;

		private CheckBox BruteBox;

		private CheckBox ControllerBox;

		private CheckBox LurkerBox;

		private CheckBox SkirmisherBox;

		private CheckBox SoldierBox;

		private Button OKBtn;

		private Button CancelBtn;

		private GroupBox RoleGroup;

		public ThemePowerData Power
		{
			get
			{
				return this.fPower;
			}
		}

		public MonsterThemePowerForm(ThemePowerData power)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			Array values = Enum.GetValues(typeof(PowerType));
			foreach (PowerType powerType in values)
			{
				this.TypeBox.Items.Add(powerType);
			}
			this.fPower = power.Copy();
			this.TypeBox.SelectedItem = this.fPower.Type;
			this.ArtilleryBox.Checked = this.fPower.Roles.Contains(RoleType.Artillery);
			this.BruteBox.Checked = this.fPower.Roles.Contains(RoleType.Brute);
			this.ControllerBox.Checked = this.fPower.Roles.Contains(RoleType.Controller);
			this.LurkerBox.Checked = this.fPower.Roles.Contains(RoleType.Lurker);
			this.SkirmisherBox.Checked = this.fPower.Roles.Contains(RoleType.Skirmisher);
			this.SoldierBox.Checked = this.fPower.Roles.Contains(RoleType.Soldier);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			List<RoleType> roles = this.get_roles();
			this.OKBtn.Enabled = (roles.Count != 0);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fPower.Type = (PowerType)this.TypeBox.SelectedItem;
			this.fPower.Roles = this.get_roles();
		}

		private List<RoleType> get_roles()
		{
			List<RoleType> list = new List<RoleType>();
			if (this.ArtilleryBox.Checked)
			{
				list.Add(RoleType.Artillery);
			}
			if (this.BruteBox.Checked)
			{
				list.Add(RoleType.Brute);
			}
			if (this.ControllerBox.Checked)
			{
				list.Add(RoleType.Controller);
			}
			if (this.LurkerBox.Checked)
			{
				list.Add(RoleType.Lurker);
			}
			if (this.SkirmisherBox.Checked)
			{
				list.Add(RoleType.Skirmisher);
			}
			if (this.SoldierBox.Checked)
			{
				list.Add(RoleType.Soldier);
			}
			return list;
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
			this.TypeLbl = new Label();
			this.TypeBox = new ComboBox();
			this.ArtilleryBox = new CheckBox();
			this.BruteBox = new CheckBox();
			this.ControllerBox = new CheckBox();
			this.LurkerBox = new CheckBox();
			this.SkirmisherBox = new CheckBox();
			this.SoldierBox = new CheckBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.RoleGroup = new GroupBox();
			this.RoleGroup.SuspendLayout();
			base.SuspendLayout();
			this.TypeLbl.AutoSize = true;
			this.TypeLbl.Location = new Point(12, 15);
			this.TypeLbl.Name = "TypeLbl";
			this.TypeLbl.Size = new Size(63, 13);
			this.TypeLbl.TabIndex = 0;
			this.TypeLbl.Text = "Power type:";
			this.TypeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.TypeBox.FormattingEnabled = true;
			this.TypeBox.Location = new Point(81, 12);
			this.TypeBox.Name = "TypeBox";
			this.TypeBox.Size = new Size(147, 21);
			this.TypeBox.TabIndex = 1;
			this.ArtilleryBox.AutoSize = true;
			this.ArtilleryBox.Location = new Point(6, 19);
			this.ArtilleryBox.Name = "ArtilleryBox";
			this.ArtilleryBox.Size = new Size(59, 17);
			this.ArtilleryBox.TabIndex = 0;
			this.ArtilleryBox.Text = "Artillery";
			this.ArtilleryBox.UseVisualStyleBackColor = true;
			this.BruteBox.AutoSize = true;
			this.BruteBox.Location = new Point(6, 42);
			this.BruteBox.Name = "BruteBox";
			this.BruteBox.Size = new Size(51, 17);
			this.BruteBox.TabIndex = 1;
			this.BruteBox.Text = "Brute";
			this.BruteBox.UseVisualStyleBackColor = true;
			this.ControllerBox.AutoSize = true;
			this.ControllerBox.Location = new Point(6, 65);
			this.ControllerBox.Name = "ControllerBox";
			this.ControllerBox.Size = new Size(70, 17);
			this.ControllerBox.TabIndex = 2;
			this.ControllerBox.Text = "Controller";
			this.ControllerBox.UseVisualStyleBackColor = true;
			this.LurkerBox.AutoSize = true;
			this.LurkerBox.Location = new Point(6, 88);
			this.LurkerBox.Name = "LurkerBox";
			this.LurkerBox.Size = new Size(56, 17);
			this.LurkerBox.TabIndex = 3;
			this.LurkerBox.Text = "Lurker";
			this.LurkerBox.UseVisualStyleBackColor = true;
			this.SkirmisherBox.AutoSize = true;
			this.SkirmisherBox.Location = new Point(6, 111);
			this.SkirmisherBox.Name = "SkirmisherBox";
			this.SkirmisherBox.Size = new Size(74, 17);
			this.SkirmisherBox.TabIndex = 4;
			this.SkirmisherBox.Text = "Skirmisher";
			this.SkirmisherBox.UseVisualStyleBackColor = true;
			this.SoldierBox.AutoSize = true;
			this.SoldierBox.Location = new Point(6, 134);
			this.SoldierBox.Name = "SoldierBox";
			this.SoldierBox.Size = new Size(58, 17);
			this.SoldierBox.TabIndex = 5;
			this.SoldierBox.Text = "Soldier";
			this.SoldierBox.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(72, 210);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 3;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(153, 210);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 4;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.RoleGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.RoleGroup.Controls.Add(this.LurkerBox);
			this.RoleGroup.Controls.Add(this.ArtilleryBox);
			this.RoleGroup.Controls.Add(this.BruteBox);
			this.RoleGroup.Controls.Add(this.SoldierBox);
			this.RoleGroup.Controls.Add(this.ControllerBox);
			this.RoleGroup.Controls.Add(this.SkirmisherBox);
			this.RoleGroup.Location = new Point(12, 39);
			this.RoleGroup.Name = "RoleGroup";
			this.RoleGroup.Size = new Size(216, 165);
			this.RoleGroup.TabIndex = 2;
			this.RoleGroup.TabStop = false;
			this.RoleGroup.Text = "Roles";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(240, 245);
			base.Controls.Add(this.RoleGroup);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.TypeBox);
			base.Controls.Add(this.TypeLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MonsterThemePowerForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Theme Power Information";
			this.RoleGroup.ResumeLayout(false);
			this.RoleGroup.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
