using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TrapProfileForm : Form
	{
		private Trap fTrap;

		private IContainer components;

		private Label NameLbl;

		private TextBox NameBox;

		private Label LevelLbl;

		private NumericUpDown LevelBox;

		private Label RoleLbl;

		private Button RoleBtn;

		private Button OKBtn;

		private Button CancelBtn;

		private Label TypeLbl;

		private ComboBox TypeBox;

		private CheckBox HasInitBox;

		private NumericUpDown InitBox;

		public Trap Trap
		{
			get
			{
				return this.fTrap;
			}
		}

		public TrapProfileForm(Trap trap)
		{
			this.InitializeComponent();
			Array values = Enum.GetValues(typeof(TrapType));
			foreach (TrapType trapType in values)
			{
				this.TypeBox.Items.Add(trapType);
			}
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fTrap = trap.Copy();
			this.NameBox.Text = this.fTrap.Name;
			this.TypeBox.SelectedItem = this.fTrap.Type;
			this.LevelBox.Value = this.fTrap.Level;
			this.RoleBtn.Text = this.fTrap.Role.ToString();
			if (this.fTrap.Initiative == -2147483648)
			{
				this.HasInitBox.Checked = false;
				this.InitBox.Value = 0m;
				return;
			}
			this.HasInitBox.Checked = true;
			this.InitBox.Value = this.fTrap.Initiative;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.InitBox.Enabled = this.HasInitBox.Checked;
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fTrap.Name = this.NameBox.Text;
			this.fTrap.Type = (TrapType)this.TypeBox.SelectedItem;
			this.fTrap.Level = (int)this.LevelBox.Value;
			this.fTrap.Initiative = (this.HasInitBox.Checked ? ((int)this.InitBox.Value) : -2147483648);
		}

		private void RoleBtn_Click(object sender, EventArgs e)
		{
			RoleForm roleForm = new RoleForm(this.fTrap.Role, ThreatType.Trap);
			if (roleForm.ShowDialog() == DialogResult.OK)
			{
				this.fTrap.Role = roleForm.Role;
				this.RoleBtn.Text = this.fTrap.Role.ToString();
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
			this.NameLbl = new Label();
			this.NameBox = new TextBox();
			this.LevelLbl = new Label();
			this.LevelBox = new NumericUpDown();
			this.RoleLbl = new Label();
			this.RoleBtn = new Button();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.TypeLbl = new Label();
			this.TypeBox = new ComboBox();
			this.HasInitBox = new CheckBox();
			this.InitBox = new NumericUpDown();
			((ISupportInitialize)this.LevelBox).BeginInit();
			((ISupportInitialize)this.InitBox).BeginInit();
			base.SuspendLayout();
			this.NameLbl.AutoSize = true;
			this.NameLbl.Location = new Point(12, 15);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new Size(38, 13);
			this.NameLbl.TabIndex = 0;
			this.NameLbl.Text = "Name:";
			this.NameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.NameBox.Location = new Point(86, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new Size(198, 20);
			this.NameBox.TabIndex = 1;
			this.LevelLbl.AutoSize = true;
			this.LevelLbl.Location = new Point(12, 67);
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(36, 13);
			this.LevelLbl.TabIndex = 4;
			this.LevelLbl.Text = "Level:";
			this.LevelBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LevelBox.Location = new Point(86, 65);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.Size = new Size(198, 20);
			this.LevelBox.TabIndex = 5;
			this.RoleLbl.AutoSize = true;
			this.RoleLbl.Location = new Point(12, 96);
			this.RoleLbl.Name = "RoleLbl";
			this.RoleLbl.Size = new Size(32, 13);
			this.RoleLbl.TabIndex = 6;
			this.RoleLbl.Text = "Role:";
			this.RoleBtn.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RoleBtn.Location = new Point(86, 91);
			this.RoleBtn.Name = "RoleBtn";
			this.RoleBtn.Size = new Size(198, 23);
			this.RoleBtn.TabIndex = 7;
			this.RoleBtn.Text = "[role]";
			this.RoleBtn.UseVisualStyleBackColor = true;
			this.RoleBtn.Click += new EventHandler(this.RoleBtn_Click);
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(128, 157);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 10;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(209, 157);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 11;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.TypeLbl.AutoSize = true;
			this.TypeLbl.Location = new Point(12, 41);
			this.TypeLbl.Name = "TypeLbl";
			this.TypeLbl.Size = new Size(34, 13);
			this.TypeLbl.TabIndex = 2;
			this.TypeLbl.Text = "Type:";
			this.TypeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.TypeBox.FormattingEnabled = true;
			this.TypeBox.Location = new Point(86, 38);
			this.TypeBox.Name = "TypeBox";
			this.TypeBox.Size = new Size(198, 21);
			this.TypeBox.TabIndex = 3;
			this.HasInitBox.AutoSize = true;
			this.HasInitBox.Location = new Point(12, 121);
			this.HasInitBox.Name = "HasInitBox";
			this.HasInitBox.Size = new Size(68, 17);
			this.HasInitBox.TabIndex = 8;
			this.HasInitBox.Text = "Initiative:";
			this.HasInitBox.UseVisualStyleBackColor = true;
			this.InitBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.InitBox.Location = new Point(86, 120);
			this.InitBox.Minimum = new decimal(new int[]
			{
				100,
				0,
				0,
				-2147483648
			});
			this.InitBox.Name = "InitBox";
			this.InitBox.Size = new Size(198, 20);
			this.InitBox.TabIndex = 9;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(296, 192);
			base.Controls.Add(this.InitBox);
			base.Controls.Add(this.HasInitBox);
			base.Controls.Add(this.TypeBox);
			base.Controls.Add(this.TypeLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.RoleBtn);
			base.Controls.Add(this.RoleLbl);
			base.Controls.Add(this.LevelBox);
			base.Controls.Add(this.LevelLbl);
			base.Controls.Add(this.NameBox);
			base.Controls.Add(this.NameLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TrapProfileForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Trap / Hazard Profile";
			((ISupportInitialize)this.LevelBox).EndInit();
			((ISupportInitialize)this.InitBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
