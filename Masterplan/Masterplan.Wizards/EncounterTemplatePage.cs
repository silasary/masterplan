using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;
using Utils.Wizards;

namespace Masterplan.Wizards
{
	internal class EncounterTemplatePage : UserControl, IWizardPage
	{
		private IContainer components;

		private Label InfoLbl;

		private ListView TemplatesList;

		private ColumnHeader TemplateHdr;

		private AdviceData fData;

		public EncounterTemplate SelectedTemplate
		{
			get
			{
				if (this.TemplatesList.SelectedItems.Count != 0)
				{
					return this.TemplatesList.SelectedItems[0].Tag as EncounterTemplate;
				}
				return null;
			}
		}

		public bool AllowNext
		{
			get
			{
				return this.SelectedTemplate != null;
			}
		}

		public bool AllowBack
		{
			get
			{
				return false;
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
			this.TemplatesList = new ListView();
			this.TemplateHdr = new ColumnHeader();
			base.SuspendLayout();
			this.InfoLbl.Dock = DockStyle.Top;
			this.InfoLbl.Location = new Point(0, 0);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(372, 40);
			this.InfoLbl.TabIndex = 1;
			this.InfoLbl.Text = "The following templates fit the creatures you have added to the encounter so far. Select one to continue.";
			this.TemplatesList.Columns.AddRange(new ColumnHeader[]
			{
				this.TemplateHdr
			});
			this.TemplatesList.Dock = DockStyle.Fill;
			this.TemplatesList.FullRowSelect = true;
			this.TemplatesList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TemplatesList.HideSelection = false;
			this.TemplatesList.Location = new Point(0, 40);
			this.TemplatesList.Name = "TemplatesList";
			this.TemplatesList.Size = new Size(372, 206);
			this.TemplatesList.TabIndex = 2;
			this.TemplatesList.UseCompatibleStateImageBehavior = false;
			this.TemplatesList.View = View.Details;
			this.TemplateHdr.Text = "Encounter Template";
			this.TemplateHdr.Width = 300;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.TemplatesList);
			base.Controls.Add(this.InfoLbl);
			base.Name = "EncounterTemplatePage";
			base.Size = new Size(372, 246);
			base.ResumeLayout(false);
		}

		public EncounterTemplatePage()
		{
			this.InitializeComponent();
		}

		public void OnShown(object data)
		{
			if (this.fData == null)
			{
				this.fData = (data as AdviceData);
				if (this.fData.TabulaRasa)
				{
					this.InfoLbl.Text = "The following encounter templates are available. Select one to continue.";
				}
				else
				{
					this.InfoLbl.Text = "The following encounter templates fit the creatures you have added to the encounter so far. Select one to continue.";
				}
				BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
				foreach (Pair<EncounterTemplateGroup, EncounterTemplate> current in this.fData.Templates)
				{
					binarySearchTree.Add(current.First.Category);
				}
				List<string> sortedList = binarySearchTree.SortedList;
				foreach (string current2 in sortedList)
				{
					this.TemplatesList.Groups.Add(current2, current2);
				}
				this.TemplatesList.Items.Clear();
				foreach (Pair<EncounterTemplateGroup, EncounterTemplate> current3 in this.fData.Templates)
				{
					ListViewItem listViewItem = this.TemplatesList.Items.Add(current3.First.Name + " (" + current3.Second.Difficulty.ToString().ToLower() + ")");
					listViewItem.Tag = current3.Second;
					listViewItem.Group = this.TemplatesList.Groups[current3.First.Category];
				}
				if (this.TemplatesList.Items.Count == 0)
				{
					this.TemplatesList.ShowGroups = false;
					ListViewItem listViewItem2 = this.TemplatesList.Items.Add("(no templates)");
					listViewItem2.ForeColor = SystemColors.GrayText;
				}
			}
		}

		public bool OnBack()
		{
			return true;
		}

		public bool OnNext()
		{
			if (this.fData.SelectedTemplate != this.SelectedTemplate)
			{
				this.fData.SelectedTemplate = this.SelectedTemplate;
				this.fData.FilledSlots.Clear();
			}
			return true;
		}

		public bool OnFinish()
		{
			return true;
		}
	}
}
