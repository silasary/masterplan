using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class DamageModifierTemplateForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label DamageTypeLbl;

		private Label HeroicLbl;

		private NumericUpDown HeroicBox;

		private ComboBox DamageTypeBox;

		private ComboBox TypeBox;

		private NumericUpDown ParagonBox;

		private Label ParagonLbl;

		private NumericUpDown EpicBox;

		private Label EpicLbl;

		private DamageModifierTemplate fMod;

		public DamageModifierTemplate Modifier
		{
			get
			{
				return this.fMod;
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
			this.DamageTypeLbl = new Label();
			this.HeroicLbl = new Label();
			this.HeroicBox = new NumericUpDown();
			this.DamageTypeBox = new ComboBox();
			this.TypeBox = new ComboBox();
			this.ParagonBox = new NumericUpDown();
			this.ParagonLbl = new Label();
			this.EpicBox = new NumericUpDown();
			this.EpicLbl = new Label();
			((ISupportInitialize)this.HeroicBox).BeginInit();
			((ISupportInitialize)this.ParagonBox).BeginInit();
			((ISupportInitialize)this.EpicBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(104, 155);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 9;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(185, 155);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 10;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.DamageTypeLbl.AutoSize = true;
			this.DamageTypeLbl.Location = new Point(12, 15);
			this.DamageTypeLbl.Name = "DamageTypeLbl";
			this.DamageTypeLbl.Size = new Size(34, 13);
			this.DamageTypeLbl.TabIndex = 0;
			this.DamageTypeLbl.Text = "Type:";
			this.HeroicLbl.AutoSize = true;
			this.HeroicLbl.Location = new Point(12, 68);
			this.HeroicLbl.Name = "HeroicLbl";
			this.HeroicLbl.Size = new Size(41, 13);
			this.HeroicLbl.TabIndex = 3;
			this.HeroicLbl.Text = "Heroic:";
			this.HeroicBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.HeroicBox.Location = new Point(68, 66);
			this.HeroicBox.Name = "HeroicBox";
			this.HeroicBox.Size = new Size(192, 20);
			this.HeroicBox.TabIndex = 4;
			this.DamageTypeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DamageTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.DamageTypeBox.FormattingEnabled = true;
			this.DamageTypeBox.Location = new Point(68, 12);
			this.DamageTypeBox.Name = "DamageTypeBox";
			this.DamageTypeBox.Size = new Size(192, 21);
			this.DamageTypeBox.TabIndex = 1;
			this.TypeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.TypeBox.FormattingEnabled = true;
			this.TypeBox.Location = new Point(68, 39);
			this.TypeBox.Name = "TypeBox";
			this.TypeBox.Size = new Size(192, 21);
			this.TypeBox.TabIndex = 2;
			this.TypeBox.SelectedIndexChanged += new EventHandler(this.TypeBox_SelectedIndexChanged);
			this.ParagonBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ParagonBox.Location = new Point(68, 92);
			this.ParagonBox.Name = "ParagonBox";
			this.ParagonBox.Size = new Size(192, 20);
			this.ParagonBox.TabIndex = 6;
			this.ParagonLbl.AutoSize = true;
			this.ParagonLbl.Location = new Point(12, 94);
			this.ParagonLbl.Name = "ParagonLbl";
			this.ParagonLbl.Size = new Size(50, 13);
			this.ParagonLbl.TabIndex = 5;
			this.ParagonLbl.Text = "Paragon:";
			this.EpicBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.EpicBox.Location = new Point(68, 118);
			this.EpicBox.Name = "EpicBox";
			this.EpicBox.Size = new Size(192, 20);
			this.EpicBox.TabIndex = 8;
			this.EpicLbl.AutoSize = true;
			this.EpicLbl.Location = new Point(12, 120);
			this.EpicLbl.Name = "EpicLbl";
			this.EpicLbl.Size = new Size(31, 13);
			this.EpicLbl.TabIndex = 7;
			this.EpicLbl.Text = "Epic:";
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(272, 190);
			base.Controls.Add(this.EpicBox);
			base.Controls.Add(this.EpicLbl);
			base.Controls.Add(this.ParagonBox);
			base.Controls.Add(this.ParagonLbl);
			base.Controls.Add(this.TypeBox);
			base.Controls.Add(this.DamageTypeBox);
			base.Controls.Add(this.HeroicBox);
			base.Controls.Add(this.HeroicLbl);
			base.Controls.Add(this.DamageTypeLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DamageModifierTemplateForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Damage Modifier";
			((ISupportInitialize)this.HeroicBox).EndInit();
			((ISupportInitialize)this.ParagonBox).EndInit();
			((ISupportInitialize)this.EpicBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public DamageModifierTemplateForm(DamageModifierTemplate dmt)
		{
			this.InitializeComponent();
			foreach (DamageType damageType in Enum.GetValues(typeof(DamageType)))
			{
				if (damageType != DamageType.Untyped)
				{
					this.DamageTypeBox.Items.Add(damageType);
				}
			}
			this.TypeBox.Items.Add("Immunity to this damage type");
			this.TypeBox.Items.Add("Resistance to this damage type");
			this.TypeBox.Items.Add("Vulnerability to this damage type");
			this.fMod = dmt.Copy();
			if (this.fMod.Type == DamageType.Untyped)
			{
				this.DamageTypeBox.SelectedIndex = 0;
			}
			else
			{
				this.DamageTypeBox.SelectedItem = this.fMod.Type;
			}
			int num = this.fMod.HeroicValue + this.fMod.ParagonValue + this.fMod.EpicValue;
			if (num == 0)
			{
				this.TypeBox.SelectedIndex = 0;
			}
			if (num < 0)
			{
				this.TypeBox.SelectedIndex = 1;
			}
			if (num > 0)
			{
				this.TypeBox.SelectedIndex = 2;
			}
			this.HeroicBox.Value = Math.Abs(this.fMod.HeroicValue);
			this.ParagonBox.Value = Math.Abs(this.fMod.ParagonValue);
			this.EpicBox.Value = Math.Abs(this.fMod.EpicValue);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fMod.Type = (DamageType)this.DamageTypeBox.SelectedItem;
			switch (this.TypeBox.SelectedIndex)
			{
			case 0:
				this.fMod.HeroicValue = 0;
				this.fMod.ParagonValue = 0;
				this.fMod.EpicValue = 0;
				return;
			case 1:
				this.fMod.HeroicValue = -(int)this.HeroicBox.Value;
				this.fMod.ParagonValue = -(int)this.ParagonBox.Value;
				this.fMod.EpicValue = -(int)this.EpicBox.Value;
				return;
			case 2:
				this.fMod.HeroicValue = (int)this.HeroicBox.Value;
				this.fMod.ParagonValue = (int)this.ParagonBox.Value;
				this.fMod.EpicValue = (int)this.EpicBox.Value;
				return;
			default:
				return;
			}
		}

		private void TypeBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.HeroicLbl.Enabled = (this.TypeBox.SelectedIndex != 0);
			this.HeroicBox.Enabled = (this.TypeBox.SelectedIndex != 0);
			this.ParagonLbl.Enabled = (this.TypeBox.SelectedIndex != 0);
			this.ParagonBox.Enabled = (this.TypeBox.SelectedIndex != 0);
			this.EpicLbl.Enabled = (this.TypeBox.SelectedIndex != 0);
			this.EpicBox.Enabled = (this.TypeBox.SelectedIndex != 0);
		}
	}
}
