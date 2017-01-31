using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class DamageTypesForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private ListView TypeList;

		private ColumnHeader TypeHdr;

		private List<DamageType> fTypes;

		public List<DamageType> Types
		{
			get
			{
				return this.fTypes;
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
			this.TypeList = new ListView();
			this.TypeHdr = new ColumnHeader();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(94, 308);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(175, 308);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.TypeList.CheckBoxes = true;
			this.TypeList.Columns.AddRange(new ColumnHeader[]
			{
				this.TypeHdr
			});
			this.TypeList.FullRowSelect = true;
			this.TypeList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TypeList.HideSelection = false;
			this.TypeList.Location = new Point(12, 12);
			this.TypeList.MultiSelect = false;
			this.TypeList.Name = "TypeList";
			this.TypeList.Size = new Size(238, 290);
			this.TypeList.TabIndex = 0;
			this.TypeList.UseCompatibleStateImageBehavior = false;
			this.TypeList.View = View.Details;
			this.TypeHdr.Text = "Type";
			this.TypeHdr.Width = 200;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(262, 343);
			base.Controls.Add(this.TypeList);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DamageTypesForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Damage Type";
			base.ResumeLayout(false);
		}

		public DamageTypesForm(List<DamageType> types)
		{
			this.InitializeComponent();
			this.fTypes = types;
			Array values = Enum.GetValues(typeof(DamageType));
			foreach (DamageType damageType in values)
			{
				if (damageType != DamageType.Untyped)
				{
					ListViewItem listViewItem = this.TypeList.Items.Add(damageType.ToString());
					listViewItem.Checked = this.fTypes.Contains(damageType);
					listViewItem.Tag = damageType;
				}
			}
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			List<DamageType> list = new List<DamageType>();
			foreach (ListViewItem listViewItem in this.TypeList.CheckedItems)
			{
				DamageType item = (DamageType)listViewItem.Tag;
				list.Add(item);
			}
			this.fTypes = list;
		}
	}
}
