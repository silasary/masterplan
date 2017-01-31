using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class PowerBrowserForm : Form
	{
		private IContainer components;

		private SplitContainer Splitter;

		private ListView CreatureList;

		private ColumnHeader CreatureHdr;

		private WebBrowser PowerDisplay;

		private ToolStrip CreatureListToolbar;

		private ColumnHeader CreatureInfoHdr;

		private Panel DisplayPanel;

		private Button CloseBtn;

		private ToolStrip PowerToolbar;

		private ToolStripDropDownButton ModeBtn;

		private ToolStripMenuItem ModeAll;

		private ToolStripMenuItem ModeSelection;

		private ToolStripButton StatsBtn;

		private FilterPanel FilterPanel;

		private string fName = "";

		private int fLevel;

		private IRole fRole;

		private bool fShowAll = true;

		private PowerCallback fCallback;

		private List<string> fAddedPowers = new List<string>();

		private List<CreaturePower> fPowers;

		private CreaturePower fSelectedPower;

		public List<ICreature> SelectedCreatures
		{
			get
			{
				List<ICreature> list = new List<ICreature>();
				if (this.fShowAll)
				{
                    IEnumerator enumerator = this.CreatureList.Items.GetEnumerator();
					{
						while (enumerator.MoveNext())
						{
							ListViewItem listViewItem = (ListViewItem)enumerator.Current;
							ICreature creature = listViewItem.Tag as ICreature;
							if (creature != null)
							{
								list.Add(creature);
							}
						}
						return list;
					}
				}
				foreach (ListViewItem listViewItem2 in this.CreatureList.SelectedItems)
				{
					ICreature creature2 = listViewItem2.Tag as ICreature;
					if (creature2 != null)
					{
						list.Add(creature2);
					}
				}
				return list;
			}
		}

		public List<CreaturePower> ShownPowers
		{
			get
			{
				return this.fPowers;
			}
		}

		public CreaturePower SelectedPower
		{
			get
			{
				return this.fSelectedPower;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(PowerBrowserForm));
			this.Splitter = new SplitContainer();
			this.CreatureList = new ListView();
			this.CreatureHdr = new ColumnHeader();
			this.CreatureInfoHdr = new ColumnHeader();
			this.FilterPanel = new FilterPanel();
			this.CreatureListToolbar = new ToolStrip();
			this.ModeBtn = new ToolStripDropDownButton();
			this.ModeAll = new ToolStripMenuItem();
			this.ModeSelection = new ToolStripMenuItem();
			this.DisplayPanel = new Panel();
			this.PowerDisplay = new WebBrowser();
			this.PowerToolbar = new ToolStrip();
			this.StatsBtn = new ToolStripButton();
			this.CloseBtn = new Button();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.CreatureListToolbar.SuspendLayout();
			this.DisplayPanel.SuspendLayout();
			this.PowerToolbar.SuspendLayout();
			base.SuspendLayout();
			this.Splitter.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.Splitter.FixedPanel = FixedPanel.Panel2;
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.CreatureList);
			this.Splitter.Panel1.Controls.Add(this.FilterPanel);
			this.Splitter.Panel1.Controls.Add(this.CreatureListToolbar);
			this.Splitter.Panel2.Controls.Add(this.DisplayPanel);
			this.Splitter.Size = new Size(734, 377);
			this.Splitter.SplitterDistance = 379;
			this.Splitter.TabIndex = 14;
			this.CreatureList.Columns.AddRange(new ColumnHeader[]
			{
				this.CreatureHdr,
				this.CreatureInfoHdr
			});
			this.CreatureList.Dock = DockStyle.Fill;
			this.CreatureList.FullRowSelect = true;
			this.CreatureList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.CreatureList.HideSelection = false;
			this.CreatureList.Location = new Point(0, 47);
			this.CreatureList.Name = "CreatureList";
			this.CreatureList.Size = new Size(379, 330);
			this.CreatureList.Sorting = SortOrder.Ascending;
			this.CreatureList.TabIndex = 2;
			this.CreatureList.UseCompatibleStateImageBehavior = false;
			this.CreatureList.View = View.Details;
			this.CreatureList.SelectedIndexChanged += new EventHandler(this.CreatureList_SelectedIndexChanged);
			this.CreatureHdr.Text = "Creature";
			this.CreatureHdr.Width = 218;
			this.CreatureInfoHdr.Text = "Info";
			this.CreatureInfoHdr.Width = 123;
			this.FilterPanel.AutoSize = true;
			this.FilterPanel.Dock = DockStyle.Top;
			this.FilterPanel.Location = new Point(0, 25);
			this.FilterPanel.Mode = ListMode.Creatures;
			this.FilterPanel.Name = "FilterPanel";
			this.FilterPanel.PartyLevel = 0;
			this.FilterPanel.Size = new Size(379, 22);
			this.FilterPanel.TabIndex = 17;
			this.FilterPanel.FilterChanged += new EventHandler(this.FilterPanel_FilterChanged);
			this.CreatureListToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.ModeBtn
			});
			this.CreatureListToolbar.Location = new Point(0, 0);
			this.CreatureListToolbar.Name = "CreatureListToolbar";
			this.CreatureListToolbar.Size = new Size(379, 25);
			this.CreatureListToolbar.TabIndex = 15;
			this.CreatureListToolbar.Text = "toolStrip1";
			this.ModeBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ModeBtn.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ModeAll,
				this.ModeSelection
			});
			this.ModeBtn.Image = (Image)resources.GetObject("ModeBtn.Image");
			this.ModeBtn.ImageTransparentColor = Color.Magenta;
			this.ModeBtn.Name = "ModeBtn";
			this.ModeBtn.Size = new Size(51, 22);
			this.ModeBtn.Text = "Mode";
			this.ModeAll.Name = "ModeAll";
			this.ModeAll.Size = new Size(290, 22);
			this.ModeAll.Text = "List Powers from All Creatures";
			this.ModeAll.Click += new EventHandler(this.ModeAll_Click);
			this.ModeSelection.Name = "ModeSelection";
			this.ModeSelection.Size = new Size(290, 22);
			this.ModeSelection.Text = "List Powers from Selected Creatures Only";
			this.ModeSelection.Click += new EventHandler(this.ModeSelection_Click);
			this.DisplayPanel.BorderStyle = BorderStyle.FixedSingle;
			this.DisplayPanel.Controls.Add(this.PowerDisplay);
			this.DisplayPanel.Controls.Add(this.PowerToolbar);
			this.DisplayPanel.Dock = DockStyle.Fill;
			this.DisplayPanel.Location = new Point(0, 0);
			this.DisplayPanel.Name = "DisplayPanel";
			this.DisplayPanel.Size = new Size(351, 377);
			this.DisplayPanel.TabIndex = 0;
			this.PowerDisplay.Dock = DockStyle.Fill;
			this.PowerDisplay.IsWebBrowserContextMenuEnabled = false;
			this.PowerDisplay.Location = new Point(0, 25);
			this.PowerDisplay.MinimumSize = new Size(20, 20);
			this.PowerDisplay.Name = "PowerDisplay";
			this.PowerDisplay.ScriptErrorsSuppressed = true;
			this.PowerDisplay.Size = new Size(349, 350);
			this.PowerDisplay.TabIndex = 2;
			this.PowerDisplay.WebBrowserShortcutsEnabled = false;
			this.PowerDisplay.Navigating += new WebBrowserNavigatingEventHandler(this.PowerDisplay_Navigating);
			this.PowerToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.StatsBtn
			});
			this.PowerToolbar.Location = new Point(0, 0);
			this.PowerToolbar.Name = "PowerToolbar";
			this.PowerToolbar.Size = new Size(349, 25);
			this.PowerToolbar.TabIndex = 3;
			this.PowerToolbar.Text = "toolStrip1";
			this.StatsBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.StatsBtn.Image = (Image)resources.GetObject("StatsBtn.Image");
			this.StatsBtn.ImageTransparentColor = Color.Magenta;
			this.StatsBtn.Name = "StatsBtn";
			this.StatsBtn.Size = new Size(93, 22);
			this.StatsBtn.Text = "Power Statistics";
			this.StatsBtn.Click += new EventHandler(this.StatsBtn_Click);
			this.CloseBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CloseBtn.DialogResult = DialogResult.OK;
			this.CloseBtn.Location = new Point(671, 395);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new Size(75, 23);
			this.CloseBtn.TabIndex = 15;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CloseBtn;
			base.ClientSize = new Size(758, 430);
			base.Controls.Add(this.CloseBtn);
			base.Controls.Add(this.Splitter);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PowerBrowserForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Power Browser";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel1.PerformLayout();
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.CreatureListToolbar.ResumeLayout(false);
			this.CreatureListToolbar.PerformLayout();
			this.DisplayPanel.ResumeLayout(false);
			this.DisplayPanel.PerformLayout();
			this.PowerToolbar.ResumeLayout(false);
			this.PowerToolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		public PowerBrowserForm(string name, int level, IRole role, PowerCallback callback)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fName = name;
			this.fLevel = level;
			this.fRole = role;
			this.fCallback = callback;
			if (!this.FilterPanel.SetFilter(this.fLevel, this.fRole))
			{
				this.update_creature_list();
				if (this.SelectedCreatures.Count > 100)
				{
					this.fShowAll = false;
				}
				this.update_powers();
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.ModeAll.Checked = this.fShowAll;
			this.ModeSelection.Checked = !this.fShowAll;
		}

		private void ModeAll_Click(object sender, EventArgs e)
		{
			this.fShowAll = true;
			this.update_powers();
		}

		private void ModeSelection_Click(object sender, EventArgs e)
		{
			this.fShowAll = false;
			this.update_powers();
		}

		private void FilterPanel_FilterChanged(object sender, EventArgs e)
		{
			this.update_creature_list();
			this.update_powers();
		}

		private void StatsBtn_Click(object sender, EventArgs e)
		{
			PowerStatisticsForm powerStatisticsForm = new PowerStatisticsForm(this.fPowers, this.SelectedCreatures.Count);
			powerStatisticsForm.ShowDialog();
		}

		private void CreatureList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.fShowAll)
			{
				this.update_powers();
			}
		}

		private void PowerDisplay_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "copy")
			{
				Guid id = new Guid(e.Url.LocalPath);
				CreaturePower creaturePower = this.find_power(id);
				if (creaturePower != null)
				{
					e.Cancel = true;
					this.copy_power(creaturePower);
				}
			}
		}

		private void update_creature_list()
		{
			this.CreatureList.BeginUpdate();
			List<ICreature> list = new List<ICreature>();
			List<Creature> creatures = Session.Creatures;
			foreach (ICreature current in creatures)
			{
				list.Add(current);
			}
			if (Session.Project != null)
			{
				foreach (ICreature current2 in Session.Project.CustomCreatures)
				{
					list.Add(current2);
				}
				foreach (ICreature current3 in Session.Project.NPCs)
				{
					list.Add(current3);
				}
			}
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (ICreature current4 in list)
			{
				if (current4.Category != "")
				{
					binarySearchTree.Add(current4.Category);
				}
			}
			List<string> sortedList = binarySearchTree.SortedList;
			sortedList.Insert(0, "Custom Creatures");
			sortedList.Insert(1, "NPCs");
			sortedList.Add("Miscellaneous Creatures");
			this.CreatureList.Groups.Clear();
			foreach (string current5 in sortedList)
			{
				this.CreatureList.Groups.Add(current5, current5);
			}
			this.CreatureList.ShowGroups = true;
			List<ListViewItem> list2 = new List<ListViewItem>();
			foreach (ICreature current6 in list)
			{
				Difficulty difficulty;
				if (current6 != null && this.FilterPanel.AllowItem(current6, out difficulty))
				{
					ListViewItem listViewItem = new ListViewItem(current6.Name);
					listViewItem.SubItems.Add(current6.Info);
					listViewItem.Tag = current6;
					if (current6.Category != "")
					{
						listViewItem.Group = this.CreatureList.Groups[current6.Category];
					}
					else
					{
						listViewItem.Group = this.CreatureList.Groups["Miscellaneous Creatures"];
					}
					list2.Add(listViewItem);
				}
			}
			this.CreatureList.Items.Clear();
			this.CreatureList.Items.AddRange(list2.ToArray());
			if (this.CreatureList.Items.Count == 0)
			{
				this.CreatureList.ShowGroups = false;
				ListViewItem listViewItem2 = this.CreatureList.Items.Add("(no creatures)");
				listViewItem2.ForeColor = SystemColors.GrayText;
			}
			this.CreatureList.EndUpdate();
		}

		private bool match(Creature c, string token)
		{
			return c.Name.ToLower().Contains(token.ToLower()) || c.Category.ToLower().Contains(token.ToLower()) || c.Info.ToLower().Contains(token.ToLower()) || c.Phenotype.ToLower().Contains(token.ToLower());
		}

		private bool role_matches(IRole role_a, IRole role_b)
		{
			if (role_a is ComplexRole && role_b is ComplexRole)
			{
				ComplexRole complexRole = role_a as ComplexRole;
				ComplexRole complexRole2 = role_b as ComplexRole;
				return complexRole.Type == complexRole2.Type;
			}
			if (role_a is Minion && role_b is Minion)
			{
				Minion minion = role_a as Minion;
				Minion minion2 = role_b as Minion;
				return (!minion.HasRole && !minion2.HasRole) || (minion.HasRole && minion2.HasRole && minion.Type == minion2.Type);
			}
			return false;
		}

		private void update_powers()
		{
			Cursor.Current = Cursors.WaitCursor;
			List<string> list = new List<string>();
			this.fPowers = new List<CreaturePower>();
			list.AddRange(HTML.GetHead(null, null, DisplaySize.Small));
			list.Add("<BODY>");
			List<ICreature> selectedCreatures = this.SelectedCreatures;
			if (selectedCreatures != null && selectedCreatures.Count != 0)
			{
				list.Add("<P class=instruction>");
				if (this.fShowAll)
				{
					list.Add("These creatures have the following powers:");
				}
				else
				{
					list.Add("The selected creatures have the following powers:");
				}
				list.Add("</P>");
				Dictionary<CreaturePowerCategory, List<CreaturePower>> dictionary = new Dictionary<CreaturePowerCategory, List<CreaturePower>>();
				Array values = Enum.GetValues(typeof(CreaturePowerCategory));
				foreach (CreaturePowerCategory key in values)
				{
					dictionary[key] = new List<CreaturePower>();
				}
				foreach (ICreature current in selectedCreatures)
				{
					foreach (CreaturePower current2 in current.CreaturePowers)
					{
						dictionary[current2.Category].Add(current2);
						this.fPowers.Add(current2);
					}
				}
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				foreach (CreaturePowerCategory creaturePowerCategory in values)
				{
					int count = dictionary[creaturePowerCategory].Count;
					if (count != 0)
					{
						dictionary[creaturePowerCategory].Sort();
						string str = "";
						switch (creaturePowerCategory)
						{
						case CreaturePowerCategory.Trait:
							str = "Traits";
							break;
						case CreaturePowerCategory.Standard:
						case CreaturePowerCategory.Move:
						case CreaturePowerCategory.Minor:
						case CreaturePowerCategory.Free:
							str = creaturePowerCategory + " Actions";
							break;
						case CreaturePowerCategory.Triggered:
							str = "Triggered Actions";
							break;
						case CreaturePowerCategory.Other:
							str = "Other Actions";
							break;
						}
						list.Add("<TR class=creature>");
						list.Add("<TD colspan=3>");
						list.Add("<B>" + str + "</B>");
						list.Add("</TD>");
						list.Add("</TR>");
						foreach (CreaturePower current3 in dictionary[creaturePowerCategory])
						{
							list.AddRange(current3.AsHTML(null, CardMode.View, false));
							list.Add("<TR>");
							list.Add("<TD colspan=3 align=center>");
							if (this.fName != null && this.fName != "")
							{
								list.Add(string.Concat(new object[]
								{
									"<A href=copy:",
									current3.ID,
									">copy this power into ",
									this.fName,
									"</A>"
								}));
							}
							else
							{
								list.Add("<A href=copy:" + current3.ID + ">select this power</A>");
							}
							list.Add("</TD>");
							list.Add("</TR>");
						}
					}
				}
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			else
			{
				list.Add("<P class=instruction>");
				list.Add("(no creatures selected)");
				list.Add("</P>");
			}
			list.Add("</BODY>");
			list.Add("</HTML>");
			this.PowerDisplay.DocumentText = HTML.Concatenate(list);
			Cursor.Current = Cursors.Default;
		}

		private CreaturePower find_power(Guid id)
		{
			foreach (CreaturePower current in this.fPowers)
			{
				if (current.ID == id)
				{
					return current;
				}
			}
			return null;
		}

		private void copy_power(CreaturePower power)
		{
			CreaturePower creaturePower = power.Copy();
			creaturePower.ID = Guid.NewGuid();
			if (this.fCallback != null)
			{
				this.fCallback(creaturePower);
				this.fAddedPowers.Add(creaturePower.Name);
				this.update_powers();
				return;
			}
			this.fSelectedPower = power;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}
	}
}
