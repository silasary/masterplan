using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class DamageModifierForm : Form
	{
		private DamageModifier fMod;

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private Label DamageTypeLbl;

		private Label ValueLbl;

		private NumericUpDown ValueBox;

		private ComboBox DamageTypeBox;

		private ComboBox TypeBox;

		public DamageModifier Modifier
		{
			get
			{
				return this.fMod;
			}
		}

		public DamageModifierForm(DamageModifier dm)
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
			this.fMod = dm.Copy();
			if (this.fMod.Type == DamageType.Untyped)
			{
				this.DamageTypeBox.SelectedIndex = 0;
			}
			else
			{
				this.DamageTypeBox.SelectedItem = this.fMod.Type;
			}
			if (this.fMod.Value == 0)
			{
				this.TypeBox.SelectedIndex = 0;
			}
			if (this.fMod.Value < 0)
			{
				this.TypeBox.SelectedIndex = 1;
				this.ValueBox.Value = Math.Abs(this.fMod.Value);
			}
			if (this.fMod.Value > 0)
			{
				this.TypeBox.SelectedIndex = 2;
				this.ValueBox.Value = this.fMod.Value;
			}
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.fMod.Type = (DamageType)this.DamageTypeBox.SelectedItem;
			switch (this.TypeBox.SelectedIndex)
			{
			case 0:
				this.fMod.Value = 0;
				return;
			case 1:
			{
				int num = (int)this.ValueBox.Value;
				this.fMod.Value = -num;
				return;
			}
			case 2:
				this.fMod.Value = (int)this.ValueBox.Value;
				return;
			default:
				return;
			}
		}

		private void TypeBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ValueLbl.Enabled = (this.TypeBox.SelectedIndex != 0);
			this.ValueBox.Enabled = (this.TypeBox.SelectedIndex != 0);
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
			this.ValueLbl = new Label();
			this.ValueBox = new NumericUpDown();
			this.DamageTypeBox = new ComboBox();
			this.TypeBox = new ComboBox();
			((ISupportInitialize)this.ValueBox).BeginInit();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(104, 97);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 5;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(185, 97);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 6;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.DamageTypeLbl.AutoSize = true;
			this.DamageTypeLbl.Location = new Point(12, 15);
			this.DamageTypeLbl.Name = "DamageTypeLbl";
			this.DamageTypeLbl.Size = new Size(34, 13);
			this.DamageTypeLbl.TabIndex = 0;
			this.DamageTypeLbl.Text = "Type:";
			this.ValueLbl.AutoSize = true;
			this.ValueLbl.Location = new Point(12, 68);
			this.ValueLbl.Name = "ValueLbl";
			this.ValueLbl.Size = new Size(37, 13);
			this.ValueLbl.TabIndex = 3;
			this.ValueLbl.Text = "Value:";
			this.ValueBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ValueBox.Location = new Point(53, 66);
			NumericUpDown arg_262_0 = this.ValueBox;
			int[] array = new int[4];
			array[0] = 1;
			arg_262_0.Minimum = new decimal(array);
			this.ValueBox.Name = "ValueBox";
			this.ValueBox.Size = new Size(207, 20);
			this.ValueBox.TabIndex = 4;
			NumericUpDown arg_2B1_0 = this.ValueBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_2B1_0.Value = new decimal(array2);
			this.DamageTypeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.DamageTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.DamageTypeBox.FormattingEnabled = true;
			this.DamageTypeBox.Location = new Point(53, 12);
			this.DamageTypeBox.Name = "DamageTypeBox";
			this.DamageTypeBox.Size = new Size(207, 21);
			this.DamageTypeBox.TabIndex = 1;
			this.TypeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.TypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.TypeBox.FormattingEnabled = true;
			this.TypeBox.Location = new Point(53, 39);
			this.TypeBox.Name = "TypeBox";
			this.TypeBox.Size = new Size(207, 21);
			this.TypeBox.TabIndex = 2;
			this.TypeBox.SelectedIndexChanged += new EventHandler(this.TypeBox_SelectedIndexChanged);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(272, 132);
			base.Controls.Add(this.TypeBox);
			base.Controls.Add(this.DamageTypeBox);
			base.Controls.Add(this.ValueBox);
			base.Controls.Add(this.ValueLbl);
			base.Controls.Add(this.DamageTypeLbl);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DamageModifierForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Damage Modifier";
			((ISupportInitialize)this.ValueBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
