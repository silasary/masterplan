using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CustomCreatureListForm : Form
	{
		private IContainer components;

		private ToolStrip Toolbar;

		private ListView CreatureList;

		private ColumnHeader NameHdr;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private ColumnHeader InfoHdr;

		private ColumnHeader StatsHdr;

		private StatusStrip Statusbar;

		private ToolStripStatusLabel InfoLbl;

		private ToolStripDropDownButton AddBtn;

		private ToolStripMenuItem AddNPC;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton StatBlockBtn;

		private Panel MainPanel;

		private Button CloseBtn;

		private ToolStripButton EncEntryBtn;

		private ToolStripMenuItem AddCreature;

		public CustomCreature SelectedCreature
		{
			get
			{
				if (this.CreatureList.SelectedItems.Count != 0)
				{
					return this.CreatureList.SelectedItems[0].Tag as CustomCreature;
				}
				return null;
			}
		}

		public NPC SelectedNPC
		{
			get
			{
				if (this.CreatureList.SelectedItems.Count != 0)
				{
					return this.CreatureList.SelectedItems[0].Tag as NPC;
				}
				return null;
			}
		}

		public CustomCreatureListForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.update_creatures();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = (this.SelectedCreature != null || this.SelectedNPC != null);
			this.EditBtn.Enabled = (this.SelectedCreature != null || this.SelectedNPC != null);
			this.StatBlockBtn.Enabled = (this.SelectedCreature != null || this.SelectedNPC != null);
			this.EncEntryBtn.Enabled = (this.SelectedCreature != null || this.SelectedNPC != null);
		}

		private void AddCreature_Click(object sender, EventArgs e)
		{
			CreatureBuilderForm creatureBuilderForm = new CreatureBuilderForm(new CustomCreature
			{
				Name = "New Creature"
			});
			if (creatureBuilderForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.CustomCreatures.Add(creatureBuilderForm.Creature as CustomCreature);
				Session.Modified = true;
				this.update_creatures();
			}
		}

		private void AddNPC_Click(object sender, EventArgs e)
		{
			if (this.class_templates_exist())
			{
				NPC nPC = new NPC();
				nPC.Name = "New NPC";
				foreach (CreatureTemplate current in Session.Templates)
				{
					if (current.Type == CreatureTemplateType.Class)
					{
						nPC.TemplateID = current.ID;
						break;
					}
				}
				CreatureBuilderForm creatureBuilderForm = new CreatureBuilderForm(nPC);
				if (creatureBuilderForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.NPCs.Add(creatureBuilderForm.Creature as NPC);
					Session.Modified = true;
					this.update_creatures();
					return;
				}
			}
			else
			{
				string text = "NPCs require class templates; you have no class templates defined.";
				text += Environment.NewLine;
				text += "You can define templates in the Libraries screen.";
				MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCreature != null)
			{
				string text = "Are you sure you want to delete this creature?";
				DialogResult dialogResult = MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.No)
				{
					return;
				}
				Session.Project.CustomCreatures.Remove(this.SelectedCreature);
				Session.Modified = true;
				this.update_creatures();
			}
			if (this.SelectedNPC != null)
			{
				string text2 = "Are you sure you want to delete this NPC?";
				DialogResult dialogResult2 = MessageBox.Show(text2, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult2 == DialogResult.No)
				{
					return;
				}
				Session.Project.NPCs.Remove(this.SelectedNPC);
				Session.Modified = true;
				this.update_creatures();
			}
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCreature != null)
			{
				int index = Session.Project.CustomCreatures.IndexOf(this.SelectedCreature);
				CreatureBuilderForm creatureBuilderForm = new CreatureBuilderForm(this.SelectedCreature);
				if (creatureBuilderForm.ShowDialog() == DialogResult.OK)
				{
					Session.Project.CustomCreatures[index] = (creatureBuilderForm.Creature as CustomCreature);
					Session.Modified = true;
					this.update_creatures();
				}
			}
			if (this.SelectedNPC != null)
			{
				int index2 = Session.Project.NPCs.IndexOf(this.SelectedNPC);
				CreatureBuilderForm creatureBuilderForm2 = new CreatureBuilderForm(this.SelectedNPC);
				if (creatureBuilderForm2.ShowDialog() == DialogResult.OK)
				{
					Session.Project.NPCs[index2] = (creatureBuilderForm2.Creature as NPC);
					Session.Modified = true;
					this.update_creatures();
				}
			}
		}

		private void StatBlockBtn_Click(object sender, EventArgs e)
		{
			EncounterCard encounterCard = null;
			if (this.SelectedCreature != null)
			{
				encounterCard = new EncounterCard();
				encounterCard.CreatureID = this.SelectedCreature.ID;
			}
			if (this.SelectedNPC != null)
			{
				encounterCard = new EncounterCard();
				encounterCard.CreatureID = this.SelectedNPC.ID;
			}
			CreatureDetailsForm creatureDetailsForm = new CreatureDetailsForm(encounterCard);
			creatureDetailsForm.ShowDialog();
		}

		private void EncEntryBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCreature == null && this.SelectedNPC == null)
			{
				return;
			}
			Guid guid = (this.SelectedNPC != null) ? this.SelectedNPC.ID : this.SelectedCreature.ID;
			string text = (this.SelectedNPC != null) ? this.SelectedNPC.Name : this.SelectedCreature.Name;
			string category = (this.SelectedNPC != null) ? "People" : "Creatures";
			EncyclopediaEntry encyclopediaEntry = Session.Project.Encyclopedia.FindEntryForAttachment(guid);
			if (encyclopediaEntry == null)
			{
				string text2 = "There is no encyclopedia entry associated with " + text + ".";
				text2 += Environment.NewLine;
				text2 += "Would you like to create one now?";
				if (MessageBox.Show(text2, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					return;
				}
				encyclopediaEntry = new EncyclopediaEntry();
				encyclopediaEntry.Name = text;
				encyclopediaEntry.AttachmentID = guid;
				encyclopediaEntry.Category = category;
				Session.Project.Encyclopedia.Entries.Add(encyclopediaEntry);
				Session.Modified = true;
			}
			int index = Session.Project.Encyclopedia.Entries.IndexOf(encyclopediaEntry);
			EncyclopediaEntryForm encyclopediaEntryForm = new EncyclopediaEntryForm(encyclopediaEntry);
			if (encyclopediaEntryForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.Encyclopedia.Entries[index] = encyclopediaEntryForm.Entry;
				Session.Modified = true;
			}
		}

		private void update_creatures()
		{
			this.CreatureList.Items.Clear();
			foreach (CustomCreature current in Session.Project.CustomCreatures)
			{
				if (current == null)
				{
					return;
				}
				ListViewItem listViewItem = this.CreatureList.Items.Add(current.Name);
				listViewItem.SubItems.Add(string.Concat(new object[]
				{
					"Level ",
					current.Level,
					" ",
					current.Role
				}));
				listViewItem.SubItems.Add(string.Concat(new object[]
				{
					current.HP,
					" HP; AC ",
					current.AC,
					", Fort ",
					current.Fortitude,
					", Ref ",
					current.Reflex,
					", Will ",
					current.Will
				}));
				listViewItem.Group = this.CreatureList.Groups[0];
				listViewItem.Tag = current;
			}
			foreach (NPC current2 in Session.Project.NPCs)
			{
				if (current2 == null)
				{
					return;
				}
				ListViewItem listViewItem2 = this.CreatureList.Items.Add(current2.Name);
				listViewItem2.SubItems.Add(string.Concat(new object[]
				{
					"Level ",
					current2.Level,
					" ",
					current2.Role
				}));
				listViewItem2.SubItems.Add(string.Concat(new object[]
				{
					current2.HP,
					" HP; AC ",
					current2.AC,
					", Fort ",
					current2.Fortitude,
					", Ref ",
					current2.Reflex,
					", Will ",
					current2.Will
				}));
				listViewItem2.Group = this.CreatureList.Groups[1];
				listViewItem2.Tag = current2;
			}
			if (this.CreatureList.Groups[0].Items.Count == 0)
			{
				ListViewItem listViewItem3 = this.CreatureList.Items.Add("(no custom creatures)");
				listViewItem3.Group = this.CreatureList.Groups[0];
				listViewItem3.ForeColor = SystemColors.GrayText;
			}
			if (this.CreatureList.Groups[1].Items.Count == 0)
			{
				ListViewItem listViewItem4 = this.CreatureList.Items.Add("(no NPCs)");
				listViewItem4.Group = this.CreatureList.Groups[1];
				listViewItem4.ForeColor = SystemColors.GrayText;
			}
			this.CreatureList.Sort();
		}

		private bool class_templates_exist()
		{
			foreach (Library current in Session.Libraries)
			{
				foreach (CreatureTemplate current2 in current.Templates)
				{
					if (current2.Type == CreatureTemplateType.Class)
					{
						return true;
					}
				}
			}
			return false;
		}

		private void CustomCreatureListForm_Shown(object sender, EventArgs e)
		{
			this.CreatureList.Invalidate();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(CustomCreatureListForm));
			ListViewGroup listViewGroup = new ListViewGroup("Custom Creatures", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("NPCs", HorizontalAlignment.Left);
			this.Toolbar = new ToolStrip();
			this.AddBtn = new ToolStripDropDownButton();
			this.AddCreature = new ToolStripMenuItem();
			this.AddNPC = new ToolStripMenuItem();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.StatBlockBtn = new ToolStripButton();
			this.EncEntryBtn = new ToolStripButton();
			this.CreatureList = new ListView();
			this.NameHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.StatsHdr = new ColumnHeader();
			this.Statusbar = new StatusStrip();
			this.InfoLbl = new ToolStripStatusLabel();
			this.MainPanel = new Panel();
			this.CloseBtn = new Button();
			this.Toolbar.SuspendLayout();
			this.Statusbar.SuspendLayout();
			this.MainPanel.SuspendLayout();
			base.SuspendLayout();
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.AddBtn,
				this.RemoveBtn,
				this.EditBtn,
				this.toolStripSeparator1,
				this.StatBlockBtn,
				this.EncEntryBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(776, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.AddCreature,
				this.AddNPC
			});
			this.AddBtn.Image = (Image)componentResourceManager.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new Size(42, 22);
			this.AddBtn.Text = "Add";
			this.AddCreature.Name = "AddCreature";
			this.AddCreature.Size = new Size(155, 22);
			this.AddCreature.Text = "New Creature...";
			this.AddCreature.Click += new EventHandler(this.AddCreature_Click);
			this.AddNPC.Name = "AddNPC";
			this.AddNPC.Size = new Size(155, 22);
			this.AddNPC.Text = "New NPC...";
			this.AddNPC.Click += new EventHandler(this.AddNPC_Click);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)componentResourceManager.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)componentResourceManager.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new Size(31, 22);
			this.EditBtn.Text = "Edit";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.StatBlockBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.StatBlockBtn.Image = (Image)componentResourceManager.GetObject("StatBlockBtn.Image");
			this.StatBlockBtn.ImageTransparentColor = Color.Magenta;
			this.StatBlockBtn.Name = "StatBlockBtn";
			this.StatBlockBtn.Size = new Size(63, 22);
			this.StatBlockBtn.Text = "Stat Block";
			this.StatBlockBtn.Click += new EventHandler(this.StatBlockBtn_Click);
			this.EncEntryBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EncEntryBtn.Image = (Image)componentResourceManager.GetObject("EncEntryBtn.Image");
			this.EncEntryBtn.ImageTransparentColor = Color.Magenta;
			this.EncEntryBtn.Name = "EncEntryBtn";
			this.EncEntryBtn.Size = new Size(111, 22);
			this.EncEntryBtn.Text = "Encyclopedia Entry";
			this.EncEntryBtn.Click += new EventHandler(this.EncEntryBtn_Click);
			this.CreatureList.Columns.AddRange(new ColumnHeader[]
			{
				this.NameHdr,
				this.InfoHdr,
				this.StatsHdr
			});
			this.CreatureList.Dock = DockStyle.Fill;
			this.CreatureList.FullRowSelect = true;
			listViewGroup.Header = "Custom Creatures";
			listViewGroup.Name = "CustomGroup";
			listViewGroup2.Header = "NPCs";
			listViewGroup2.Name = "NPCGroup";
			this.CreatureList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2
			});
			this.CreatureList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.CreatureList.HideSelection = false;
			this.CreatureList.Location = new Point(0, 25);
			this.CreatureList.MultiSelect = false;
			this.CreatureList.Name = "CreatureList";
			this.CreatureList.Size = new Size(776, 219);
			this.CreatureList.Sorting = SortOrder.Ascending;
			this.CreatureList.TabIndex = 1;
			this.CreatureList.UseCompatibleStateImageBehavior = false;
			this.CreatureList.View = View.Details;
			this.CreatureList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.NameHdr.Text = "Creature";
			this.NameHdr.Width = 287;
			this.InfoHdr.Text = "Information";
			this.InfoHdr.Width = 150;
			this.StatsHdr.Text = "Statistics";
			this.StatsHdr.Width = 311;
			this.Statusbar.Items.AddRange(new ToolStripItem[]
			{
				this.InfoLbl
			});
			this.Statusbar.Location = new Point(0, 244);
			this.Statusbar.Name = "Statusbar";
			this.Statusbar.Size = new Size(776, 22);
			this.Statusbar.SizingGrip = false;
			this.Statusbar.TabIndex = 2;
			this.Statusbar.Text = "statusStrip1";
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(696, 17);
			this.InfoLbl.Text = "This screen is for adding NPCs and unusual creatures to this project only. For reusable creatures, go to Libraries on the Tools menu.";
			this.MainPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.MainPanel.Controls.Add(this.CreatureList);
			this.MainPanel.Controls.Add(this.Toolbar);
			this.MainPanel.Controls.Add(this.Statusbar);
			this.MainPanel.Location = new Point(12, 12);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new Size(776, 266);
			this.MainPanel.TabIndex = 3;
			this.CloseBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CloseBtn.DialogResult = DialogResult.OK;
			this.CloseBtn.Location = new Point(713, 284);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new Size(75, 23);
			this.CloseBtn.TabIndex = 4;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(800, 319);
			base.Controls.Add(this.CloseBtn);
			base.Controls.Add(this.MainPanel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CustomCreatureListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Custom Creatures and NPCs";
			base.Shown += new EventHandler(this.CustomCreatureListForm_Shown);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.Statusbar.ResumeLayout(false);
			this.Statusbar.PerformLayout();
			this.MainPanel.ResumeLayout(false);
			this.MainPanel.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
