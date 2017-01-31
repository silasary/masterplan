using Masterplan.Data;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils.Wizards;

namespace Masterplan.Wizards
{
	internal class VariantTemplatesPage : UserControl, IWizardPage
	{
		private IContainer components;

		private ListView TemplateList;

		private ColumnHeader NameHdr;

		private ColumnHeader TypeHdr;

		private Label InfoLbl;

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
			this.TemplateList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.TypeHdr = new ColumnHeader();
			this.InfoLbl = new Label();
			base.SuspendLayout();
			this.TemplateList.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.TemplateList.CheckBoxes = true;
			this.TemplateList.Columns.AddRange(new ColumnHeader[]
			{
				this.NameHdr,
				this.TypeHdr
			});
			this.TemplateList.FullRowSelect = true;
			this.TemplateList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TemplateList.HideSelection = false;
			this.TemplateList.Location = new Point(3, 43);
			this.TemplateList.MultiSelect = false;
			this.TemplateList.Name = "TemplateList";
			this.TemplateList.Size = new Size(287, 188);
			this.TemplateList.TabIndex = 5;
			this.TemplateList.UseCompatibleStateImageBehavior = false;
			this.TemplateList.View = View.Details;
			this.TemplateList.DoubleClick += new EventHandler(this.TemplateList_DoubleClick);
			this.NameHdr.Text = "Template";
			this.NameHdr.Width = 150;
			this.TypeHdr.Text = "Role";
			this.TypeHdr.Width = 100;
			this.InfoLbl.Dock = DockStyle.Top;
			this.InfoLbl.Location = new Point(0, 0);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(293, 40);
			this.InfoLbl.TabIndex = 3;
			this.InfoLbl.Text = "Select any templates you would like to apply to the new creature.";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.TemplateList);
			base.Controls.Add(this.InfoLbl);
			base.Name = "VariantTemplatesPage";
			base.Size = new Size(293, 234);
			base.ResumeLayout(false);
		}

		public VariantTemplatesPage()
		{
			this.InitializeComponent();
		}

		public void OnShown(object data)
		{
			if (this.fData == null)
			{
				this.fData = (data as VariantData);
				List<CreatureTemplate> templates = Session.Templates;
				foreach (CreatureTemplate current in templates)
				{
					ListViewItem listViewItem = this.TemplateList.Items.Add(current.Name);
					listViewItem.SubItems.Add(current.Info);
					listViewItem.Tag = current;
				}
			}
		}

		public bool OnBack()
		{
			return true;
		}

		public bool OnNext()
		{
			int num = 0;
			ComplexRole complexRole = this.fData.BaseCreature.Role as ComplexRole;
			switch (complexRole.Flag)
			{
			case RoleFlag.Elite:
				num = 1;
				break;
			case RoleFlag.Solo:
				num = 2;
				break;
			}
			num += this.TemplateList.CheckedItems.Count;
			if (num > 2)
			{
				string text = "You can not normally apply that many templates to this creature.";
				text += Environment.NewLine;
				text += "Are you sure you want to continue?";
				DialogResult dialogResult = MessageBox.Show(text, "Creature Builder", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.No)
				{
					return false;
				}
			}
			this.fData.Templates.Clear();
			foreach (ListViewItem listViewItem in this.TemplateList.CheckedItems)
			{
				this.fData.Templates.Add(listViewItem.Tag as CreatureTemplate);
			}
			return true;
		}

		public bool OnFinish()
		{
			return false;
		}

		private void TemplateList_DoubleClick(object sender, EventArgs e)
		{
			if (this.TemplateList.SelectedItems.Count != 0)
			{
				CreatureTemplate creatureTemplate = this.TemplateList.SelectedItems[0].Tag as CreatureTemplate;
				if (creatureTemplate != null)
				{
					CreatureTemplateDetailsForm creatureTemplateDetailsForm = new CreatureTemplateDetailsForm(creatureTemplate);
					creatureTemplateDetailsForm.ShowDialog();
				}
			}
		}
	}
}
