using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils.Wizards;

namespace Masterplan.Wizards
{
	internal class VariantRolePage : UserControl, IWizardPage
	{
		private IContainer components;

		private Label InfoLbl;

		private ComboBox RoleBox;

		private VariantData fData;

		public bool AllowNext
		{
			get
			{
				return true;
			}
		}

		public bool AllowBack
		{
			get
			{
				return true;
			}
		}

		public bool AllowFinish
		{
			get
			{
				return false;
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
			this.InfoLbl = new Label();
			this.RoleBox = new ComboBox();
			base.SuspendLayout();
			this.InfoLbl.Dock = DockStyle.Top;
			this.InfoLbl.Location = new Point(0, 0);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(268, 40);
			this.InfoLbl.TabIndex = 1;
			this.InfoLbl.Text = "Select the role that your new creature will fill.";
			this.RoleBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.RoleBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.RoleBox.FormattingEnabled = true;
			this.RoleBox.Location = new Point(3, 43);
			this.RoleBox.Name = "RoleBox";
			this.RoleBox.Size = new Size(262, 21);
			this.RoleBox.TabIndex = 2;
			this.RoleBox.SelectedIndexChanged += new EventHandler(this.RoleBox_SelectedIndexChanged);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.RoleBox);
			base.Controls.Add(this.InfoLbl);
			base.Name = "VariantRolePage";
			base.Size = new Size(268, 93);
			base.ResumeLayout(false);
		}

		public VariantRolePage()
		{
			this.InitializeComponent();
		}

		public void OnShown(object data)
		{
			if (this.fData == null)
			{
				this.fData = (data as VariantData);
			}
			this.RoleBox.Items.Clear();
			foreach (RoleType current in this.fData.Roles)
			{
				this.RoleBox.Items.Add(current);
			}
			this.RoleBox.SelectedIndex = this.fData.SelectedRoleIndex;
		}

		public bool OnBack()
		{
			return true;
		}

		public bool OnNext()
		{
			return true;
		}

		public bool OnFinish()
		{
			return false;
		}

		private void RoleBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.fData.SelectedRoleIndex = this.RoleBox.SelectedIndex;
		}
	}
}
