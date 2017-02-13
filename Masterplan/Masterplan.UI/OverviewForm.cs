using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class OverviewForm : Form
	{
		private IContainer components;

		private ToolStrip Toolbar;

		private ListView ItemList;

		private ColumnHeader PointHdr;

		private ColumnHeader InfoHdr;

		private ToolStripButton EncounterBtn;

		private ToolStripButton ChallengeBtn;

		private ToolStripButton TreasureBtn;

		private ToolStripButton TrapBtn;

		private Panel MainPanel;

		private Button CloseBtn;

		private OverviewType fType;

		private List<PlotPoint> fPoints = new List<PlotPoint>();

		public Pair<PlotPoint, Encounter> SelectedEncounter
		{
			get
			{
				if (this.ItemList.SelectedItems.Count != 0)
				{
					return this.ItemList.SelectedItems[0].Tag as Pair<PlotPoint, Encounter>;
				}
				return null;
			}
		}

		public Pair<PlotPoint, Trap> SelectedTrap
		{
			get
			{
				if (this.ItemList.SelectedItems.Count != 0)
				{
					return this.ItemList.SelectedItems[0].Tag as Pair<PlotPoint, Trap>;
				}
				return null;
			}
		}

		public Pair<PlotPoint, SkillChallenge> SelectedChallenge
		{
			get
			{
				if (this.ItemList.SelectedItems.Count != 0)
				{
					return this.ItemList.SelectedItems[0].Tag as Pair<PlotPoint, SkillChallenge>;
				}
				return null;
			}
		}

		public Pair<PlotPoint, Parcel> SelectedParcel
		{
			get
			{
				if (this.ItemList.SelectedItems.Count != 0)
				{
					return this.ItemList.SelectedItems[0].Tag as Pair<PlotPoint, Parcel>;
				}
				return null;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(OverviewForm));
			this.Toolbar = new ToolStrip();
			this.EncounterBtn = new ToolStripButton();
			this.TrapBtn = new ToolStripButton();
			this.ChallengeBtn = new ToolStripButton();
			this.TreasureBtn = new ToolStripButton();
			this.ItemList = new ListView();
			this.PointHdr = new ColumnHeader();
			this.InfoHdr = new ColumnHeader();
			this.MainPanel = new Panel();
			this.CloseBtn = new Button();
			this.Toolbar.SuspendLayout();
			this.MainPanel.SuspendLayout();
			base.SuspendLayout();
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.EncounterBtn,
				this.TrapBtn,
				this.ChallengeBtn,
				this.TreasureBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(513, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.EncounterBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EncounterBtn.Image = (Image)resources.GetObject("EncounterBtn.Image");
			this.EncounterBtn.ImageTransparentColor = Color.Magenta;
			this.EncounterBtn.Name = "EncounterBtn";
			this.EncounterBtn.Size = new Size(70, 22);
			this.EncounterBtn.Text = "Encounters";
			this.EncounterBtn.Click += new EventHandler(this.EncounterBtn_Click);
			this.TrapBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrapBtn.Image = (Image)resources.GetObject("TrapBtn.Image");
			this.TrapBtn.ImageTransparentColor = Color.Magenta;
			this.TrapBtn.Name = "TrapBtn";
			this.TrapBtn.Size = new Size(40, 22);
			this.TrapBtn.Text = "Traps";
			this.TrapBtn.Click += new EventHandler(this.TrapBtn_Click);
			this.ChallengeBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChallengeBtn.Image = (Image)resources.GetObject("ChallengeBtn.Image");
			this.ChallengeBtn.ImageTransparentColor = Color.Magenta;
			this.ChallengeBtn.Name = "ChallengeBtn";
			this.ChallengeBtn.Size = new Size(93, 22);
			this.ChallengeBtn.Text = "Skill Challenges";
			this.ChallengeBtn.Click += new EventHandler(this.ChallengeBtn_Click);
			this.TreasureBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TreasureBtn.Image = (Image)resources.GetObject("TreasureBtn.Image");
			this.TreasureBtn.ImageTransparentColor = Color.Magenta;
			this.TreasureBtn.Name = "TreasureBtn";
			this.TreasureBtn.Size = new Size(56, 22);
			this.TreasureBtn.Text = "Treasure";
			this.TreasureBtn.Click += new EventHandler(this.TreasureBtn_Click);
			this.ItemList.Columns.AddRange(new ColumnHeader[]
			{
				this.PointHdr,
				this.InfoHdr
			});
			this.ItemList.Dock = DockStyle.Fill;
			this.ItemList.FullRowSelect = true;
			this.ItemList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.ItemList.HideSelection = false;
			this.ItemList.Location = new Point(0, 25);
			this.ItemList.MultiSelect = false;
			this.ItemList.Name = "ItemList";
			this.ItemList.Size = new Size(513, 203);
			this.ItemList.TabIndex = 1;
			this.ItemList.UseCompatibleStateImageBehavior = false;
			this.ItemList.View = View.Details;
			this.ItemList.DoubleClick += new EventHandler(this.ItemList_DoubleClick);
			this.PointHdr.Text = "Point";
			this.PointHdr.Width = 100;
			this.InfoHdr.Text = "Information";
			this.InfoHdr.Width = 384;
			this.MainPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.MainPanel.Controls.Add(this.ItemList);
			this.MainPanel.Controls.Add(this.Toolbar);
			this.MainPanel.Location = new Point(12, 12);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new Size(513, 228);
			this.MainPanel.TabIndex = 2;
			this.CloseBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CloseBtn.DialogResult = DialogResult.OK;
			this.CloseBtn.Location = new Point(450, 246);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new Size(75, 23);
			this.CloseBtn.TabIndex = 3;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(537, 281);
			base.Controls.Add(this.CloseBtn);
			base.Controls.Add(this.MainPanel);
			base.MinimizeBox = false;
			base.Name = "OverviewForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Project Overview";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.MainPanel.ResumeLayout(false);
			this.MainPanel.PerformLayout();
			base.ResumeLayout(false);
		}

		public OverviewForm()
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.add_points(null);
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.EncounterBtn.Checked = (this.fType == OverviewType.Encounters);
			this.TrapBtn.Checked = (this.fType == OverviewType.Traps);
			this.ChallengeBtn.Checked = (this.fType == OverviewType.SkillChallenges);
			this.TreasureBtn.Checked = (this.fType == OverviewType.Treasure);
		}

		private void EncounterBtn_Click(object sender, EventArgs e)
		{
			this.fType = OverviewType.Encounters;
			this.update_list();
		}

		private void TrapBtn_Click(object sender, EventArgs e)
		{
			this.fType = OverviewType.Traps;
			this.update_list();
		}

		private void ChallengeBtn_Click(object sender, EventArgs e)
		{
			this.fType = OverviewType.SkillChallenges;
			this.update_list();
		}

		private void TreasureBtn_Click(object sender, EventArgs e)
		{
			this.fType = OverviewType.Treasure;
			this.update_list();
		}

		private void update_list()
		{
			this.ItemList.Items.Clear();
			switch (this.fType)
			{
			case OverviewType.Encounters:
				using (List<PlotPoint>.Enumerator enumerator = this.fPoints.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PlotPoint current = enumerator.Current;
						if (current.Element != null && current.Element is Encounter)
						{
							Encounter encounter = current.Element as Encounter;
							string text = "";
							foreach (EncounterSlot current2 in encounter.AllSlots)
							{
								if (text != "")
								{
									text += ", ";
								}
								text += current2.Card.Title;
								if (current2.CombatData.Count != 1)
								{
									object obj = text;
									text = string.Concat(new object[]
									{
										obj,
										" (x",
										current2.CombatData.Count,
										")"
									});
								}
							}
							foreach (Trap current3 in encounter.Traps)
							{
								if (text != "")
								{
									text += ", ";
								}
								text += current3.Name;
							}
							ListViewItem listViewItem = this.ItemList.Items.Add(current.Name);
							listViewItem.SubItems.Add(encounter.GetXP() + " XP; " + text);
							listViewItem.Tag = new Pair<PlotPoint, Encounter>(current, encounter);
						}
					}
					goto IL_57B;
				}
				break;
			case OverviewType.Traps:
				break;
			case OverviewType.SkillChallenges:
				goto IL_35B;
			case OverviewType.Treasure:
				goto IL_4A6;
			default:
				goto IL_57B;
			}
			using (List<PlotPoint>.Enumerator enumerator4 = this.fPoints.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					PlotPoint current4 = enumerator4.Current;
					if (current4.Element != null)
					{
						if (current4.Element is TrapElement)
						{
							TrapElement trapElement = current4.Element as TrapElement;
							ListViewItem listViewItem2 = this.ItemList.Items.Add(current4.Name);
							listViewItem2.SubItems.Add(Experience.GetCreatureXP(trapElement.Trap.Level) + " XP; " + trapElement.Trap.Name);
							listViewItem2.Tag = new Pair<PlotPoint, Trap>(current4, trapElement.Trap);
						}
						if (current4.Element is Encounter)
						{
							Encounter encounter2 = current4.Element as Encounter;
							foreach (Trap current5 in encounter2.Traps)
							{
								ListViewItem listViewItem3 = this.ItemList.Items.Add(current4.Name);
								listViewItem3.SubItems.Add(Experience.GetCreatureXP(current5.Level) + " XP; " + current5.Name);
								listViewItem3.Tag = new Pair<PlotPoint, Trap>(current4, current5);
							}
						}
					}
				}
				goto IL_57B;
			}
			IL_35B:
			using (List<PlotPoint>.Enumerator enumerator6 = this.fPoints.GetEnumerator())
			{
				while (enumerator6.MoveNext())
				{
					PlotPoint current6 = enumerator6.Current;
					if (current6.Element != null)
					{
						if (current6.Element is SkillChallenge)
						{
							SkillChallenge skillChallenge = current6.Element as SkillChallenge;
							ListViewItem listViewItem4 = this.ItemList.Items.Add(current6.Name);
							listViewItem4.SubItems.Add(skillChallenge.GetXP() + " XP");
							listViewItem4.Tag = new Pair<PlotPoint, SkillChallenge>(current6, skillChallenge);
						}
						if (current6.Element is Encounter)
						{
							Encounter encounter3 = current6.Element as Encounter;
							foreach (SkillChallenge current7 in encounter3.SkillChallenges)
							{
								ListViewItem listViewItem5 = this.ItemList.Items.Add(current6.Name);
								listViewItem5.SubItems.Add(current7.GetXP() + " XP");
								listViewItem5.Tag = new Pair<PlotPoint, SkillChallenge>(current6, current7);
							}
						}
					}
				}
				goto IL_57B;
			}
			IL_4A6:
			foreach (PlotPoint current8 in this.fPoints)
			{
				foreach (Parcel current9 in current8.Parcels)
				{
					string str = (current9.Name != "") ? current9.Name : "(undefined parcel)";
					ListViewItem listViewItem6 = this.ItemList.Items.Add(current8.Name);
					listViewItem6.SubItems.Add(str + ": " + current9.Details);
					listViewItem6.Tag = new Pair<PlotPoint, Parcel>(current8, current9);
				}
			}
			IL_57B:
			if (this.ItemList.Items.Count == 0)
			{
				ListViewItem listViewItem7 = this.ItemList.Items.Add("(no items)");
				listViewItem7.ForeColor = SystemColors.GrayText;
			}
			this.ItemList.Sort();
		}

		private void add_points(Plot plot)
		{
			List<PlotPoint> list = (plot != null) ? plot.Points : Session.Project.Plot.Points;
			this.fPoints.AddRange(list);
			foreach (PlotPoint current in list)
			{
				this.add_points(current.Subplot);
			}
		}

		private void ItemList_DoubleClick(object sender, EventArgs e)
		{
			switch (this.fType)
			{
			case OverviewType.Encounters:
				if (this.SelectedEncounter != null)
				{
					int partyLevel = Workspace.GetPartyLevel(this.SelectedEncounter.First);
					EncounterBuilderForm encounterBuilderForm = new EncounterBuilderForm(this.SelectedEncounter.Second, partyLevel, false);
					if (encounterBuilderForm.ShowDialog() == DialogResult.OK)
					{
						this.SelectedEncounter.First.Element = encounterBuilderForm.Encounter;
						Session.Modified = true;
						this.update_list();
					}
					return;
				}
				break;
			case OverviewType.Traps:
				if (this.SelectedTrap != null)
				{
					if (this.SelectedTrap.First.Element is TrapElement)
					{
						TrapElement trapElement = this.SelectedTrap.First.Element as TrapElement;
						TrapBuilderForm trapBuilderForm = new TrapBuilderForm(this.SelectedTrap.Second);
						if (trapBuilderForm.ShowDialog() == DialogResult.OK)
						{
							trapElement.Trap = trapBuilderForm.Trap;
							Session.Modified = true;
							this.update_list();
						}
						return;
					}
					if (this.SelectedTrap.First.Element is Encounter)
					{
						Encounter encounter = this.SelectedTrap.First.Element as Encounter;
						int index = encounter.Traps.IndexOf(this.SelectedTrap.Second);
						TrapBuilderForm trapBuilderForm2 = new TrapBuilderForm(this.SelectedTrap.Second);
						if (trapBuilderForm2.ShowDialog() == DialogResult.OK)
						{
							encounter.Traps[index] = trapBuilderForm2.Trap;
							Session.Modified = true;
							this.update_list();
						}
						return;
					}
				}
				break;
			case OverviewType.SkillChallenges:
				if (this.SelectedChallenge != null)
				{
					if (this.SelectedChallenge.First.Element is SkillChallenge)
					{
						SkillChallenge skillChallenge = this.SelectedChallenge.First.Element as SkillChallenge;
						SkillChallengeBuilderForm skillChallengeBuilderForm = new SkillChallengeBuilderForm(this.SelectedChallenge.Second);
						if (skillChallengeBuilderForm.ShowDialog() == DialogResult.OK)
						{
							skillChallenge.Name = skillChallengeBuilderForm.SkillChallenge.Name;
							skillChallenge.Level = skillChallengeBuilderForm.SkillChallenge.Level;
							skillChallenge.Complexity = skillChallengeBuilderForm.SkillChallenge.Complexity;
							skillChallenge.Success = skillChallengeBuilderForm.SkillChallenge.Success;
							skillChallenge.Failure = skillChallengeBuilderForm.SkillChallenge.Failure;
							skillChallenge.Skills.Clear();
							foreach (SkillChallengeData current in skillChallengeBuilderForm.SkillChallenge.Skills)
							{
								skillChallenge.Skills.Add(current.Copy());
							}
							Session.Modified = true;
							this.update_list();
						}
						return;
					}
					if (this.SelectedChallenge.First.Element is Encounter)
					{
						Encounter encounter2 = this.SelectedChallenge.First.Element as Encounter;
						int index2 = encounter2.SkillChallenges.IndexOf(this.SelectedChallenge.Second);
						SkillChallengeBuilderForm skillChallengeBuilderForm2 = new SkillChallengeBuilderForm(this.SelectedChallenge.Second);
						if (skillChallengeBuilderForm2.ShowDialog() == DialogResult.OK)
						{
							encounter2.SkillChallenges[index2] = skillChallengeBuilderForm2.SkillChallenge;
							Session.Modified = true;
							this.update_list();
						}
						return;
					}
				}
				break;
			case OverviewType.Treasure:
				if (this.SelectedParcel != null)
				{
					int index3 = this.SelectedParcel.First.Parcels.IndexOf(this.SelectedParcel.Second);
					ParcelForm parcelForm = new ParcelForm(this.SelectedParcel.Second);
					if (parcelForm.ShowDialog() == DialogResult.OK)
					{
						this.SelectedParcel.First.Parcels[index3] = parcelForm.Parcel;
						Session.Modified = true;
						this.update_list();
					}
				}
				break;
			default:
				return;
			}
		}
	}
}
