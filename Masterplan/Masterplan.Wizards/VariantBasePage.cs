using Masterplan.Data;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;
using Utils.Wizards;

namespace Masterplan.Wizards
{
	internal class VariantBasePage : UserControl, IWizardPage
	{
		private VariantData fData;

		private IContainer components;

		private Label InfoLbl;

		private ListView CreatureList;

		private ColumnHeader CreatureHdr;

		private ColumnHeader RoleHdr;

		private Panel panel1;

		private ToolStrip toolStrip1;

		private ToolStripLabel SearchLbl;

		private ToolStripTextBox SearchBox;

		private ToolStripLabel SearchClearBtn;

		public bool AllowNext
		{
			get
			{
				return this.SelectedCreature != null;
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

		public Creature SelectedCreature
		{
			get
			{
				if (this.CreatureList.SelectedItems.Count != 0)
				{
					return this.CreatureList.SelectedItems[0].Tag as Creature;
				}
				return null;
			}
		}

		public VariantBasePage()
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.SearchClearBtn.Enabled = (this.SearchBox.Text != "");
		}

		public void OnShown(object data)
		{
			if (this.fData == null)
			{
				this.fData = (data as VariantData);
				this.update_list();
			}
		}

		public bool OnBack()
		{
			return false;
		}

		public bool OnNext()
		{
			this.fData.BaseCreature = this.SelectedCreature;
			if (this.fData.BaseCreature.Role is Minion)
			{
				this.fData.Templates.Clear();
			}
			return true;
		}

		public bool OnFinish()
		{
			return false;
		}

		private void SearchBox_TextChanged(object sender, EventArgs e)
		{
			this.update_list();
		}

		private void SearchClearBtn_Click(object sender, EventArgs e)
		{
			this.SearchBox.Text = "";
		}

		private void update_list()
		{
			List<Creature> creatures = Session.Creatures;
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (Creature current in creatures)
			{
				if (current.Category != null && current.Category != "")
				{
					binarySearchTree.Add(current.Category);
				}
			}
			List<string> sortedList = binarySearchTree.SortedList;
			sortedList.Add("Miscellaneous Creatures");
			this.CreatureList.BeginUpdate();
			this.CreatureList.Groups.Clear();
			foreach (string current2 in sortedList)
			{
				this.CreatureList.Groups.Add(current2, current2);
			}
			List<ListViewItem> list = new List<ListViewItem>();
			foreach (Creature current3 in creatures)
			{
				if (this.match(current3, this.SearchBox.Text))
				{
					ListViewItem listViewItem = new ListViewItem(current3.Name);
					listViewItem.SubItems.Add(string.Concat(new object[]
					{
						"Level ",
						current3.Level,
						" ",
						current3.Role
					}));
					if (current3.Category != null && current3.Category != "")
					{
						listViewItem.Group = this.CreatureList.Groups[current3.Category];
					}
					else
					{
						listViewItem.Group = this.CreatureList.Groups["Miscellaneous Creatures"];
					}
					listViewItem.Tag = current3;
					list.Add(listViewItem);
				}
			}
			this.CreatureList.Items.Clear();
			this.CreatureList.Items.AddRange(list.ToArray());
			this.CreatureList.EndUpdate();
		}

		private bool match(Creature c, string query)
		{
			string[] array = query.Split(null);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string token = array2[i];
				if (!this.match_token(c, token))
				{
					return false;
				}
			}
			return true;
		}

		private bool match_token(Creature c, string token)
		{
			return c.Name.ToLower().Contains(token.ToLower()) || (c.Category != null && c.Category.ToLower().Contains(token.ToLower())) || c.Info.ToLower().Contains(token.ToLower()) || c.Phenotype.ToLower().Contains(token.ToLower());
		}

		private void CreatureList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedCreature != null)
			{
				EncounterCard card = new EncounterCard(this.SelectedCreature.ID);
				CreatureDetailsForm creatureDetailsForm = new CreatureDetailsForm(card);
				creatureDetailsForm.ShowDialog();
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
			this.CreatureList = new ListView();
			this.CreatureHdr = new ColumnHeader();
			this.RoleHdr = new ColumnHeader();
			this.panel1 = new Panel();
			this.toolStrip1 = new ToolStrip();
			this.SearchLbl = new ToolStripLabel();
			this.SearchBox = new ToolStripTextBox();
			this.SearchClearBtn = new ToolStripLabel();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.InfoLbl.Dock = DockStyle.Top;
			this.InfoLbl.Location = new Point(0, 0);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(342, 40);
			this.InfoLbl.TabIndex = 0;
			this.InfoLbl.Text = "Select the creature you want to create a variant of.";
			this.CreatureList.Columns.AddRange(new ColumnHeader[]
			{
				this.CreatureHdr,
				this.RoleHdr
			});
			this.CreatureList.Dock = DockStyle.Fill;
			this.CreatureList.FullRowSelect = true;
			this.CreatureList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.CreatureList.HideSelection = false;
			this.CreatureList.Location = new Point(0, 25);
			this.CreatureList.MultiSelect = false;
			this.CreatureList.Name = "CreatureList";
			this.CreatureList.Size = new Size(336, 179);
			this.CreatureList.Sorting = SortOrder.Ascending;
			this.CreatureList.TabIndex = 2;
			this.CreatureList.UseCompatibleStateImageBehavior = false;
			this.CreatureList.View = View.Details;
			this.CreatureList.DoubleClick += new EventHandler(this.CreatureList_DoubleClick);
			this.CreatureHdr.Text = "Creature";
			this.CreatureHdr.Width = 150;
			this.RoleHdr.Text = "Role";
			this.RoleHdr.Width = 150;
			this.panel1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.panel1.Controls.Add(this.CreatureList);
			this.panel1.Controls.Add(this.toolStrip1);
			this.panel1.Location = new Point(3, 43);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(336, 204);
			this.panel1.TabIndex = 3;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.SearchLbl,
				this.SearchBox,
				this.SearchClearBtn
			});
			this.toolStrip1.Location = new Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new Size(336, 25);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Text = "toolStrip1";
			this.SearchLbl.Name = "SearchLbl";
			this.SearchLbl.Size = new Size(45, 22);
			this.SearchLbl.Text = "Search:";
			this.SearchBox.BorderStyle = BorderStyle.FixedSingle;
			this.SearchBox.Name = "SearchBox";
			this.SearchBox.Size = new Size(200, 25);
			this.SearchBox.TextChanged += new EventHandler(this.SearchBox_TextChanged);
			this.SearchClearBtn.IsLink = true;
			this.SearchClearBtn.Name = "SearchClearBtn";
			this.SearchClearBtn.Size = new Size(34, 22);
			this.SearchClearBtn.Text = "Clear";
			this.SearchClearBtn.Click += new EventHandler(this.SearchClearBtn_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.InfoLbl);
			base.Name = "VariantBasePage";
			base.Size = new Size(342, 250);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
