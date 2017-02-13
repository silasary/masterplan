using Masterplan.Data;
using Masterplan.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class SkillChallengePanel : UserControl
	{
		private IContainer components;

		private ToolStrip Toolbar;

		private ToolStripButton EditBtn;

		private ListView SkillList;

		private ColumnHeader InfoHdr;

		private ToolStripButton ChooseBtn;

		private ToolStripButton LocationBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton AddLibraryBtn;

		private ToolStripSeparator toolStripSeparator2;

		private SkillChallenge fChallenge;

		private int fPartyLevel = Session.Project.Party.Level;

		public SkillChallenge Challenge
		{
			get
			{
				return this.fChallenge;
			}
			set
			{
				this.fChallenge = value;
				this.update_view();
			}
		}

		public int PartyLevel
		{
			get
			{
				return this.fPartyLevel;
			}
			set
			{
				this.fPartyLevel = value;
				this.update_view();
			}
		}

		public SkillChallengeData SelectedSkill
		{
			get
			{
				if (this.SkillList.SelectedItems.Count != 0)
				{
					return this.SkillList.SelectedItems[0].Tag as SkillChallengeData;
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
			ListViewGroup listViewGroup = new ListViewGroup("Info", HorizontalAlignment.Left);
			ListViewGroup listViewGroup2 = new ListViewGroup("Primary Skills", HorizontalAlignment.Left);
			ListViewGroup listViewGroup3 = new ListViewGroup("Other Skills", HorizontalAlignment.Left);
			ListViewGroup listViewGroup4 = new ListViewGroup("Automatic Failure", HorizontalAlignment.Left);
			ComponentResourceManager resources = new ComponentResourceManager(typeof(SkillChallengePanel));
			this.Toolbar = new ToolStrip();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.SkillList = new ListView();
			this.InfoHdr = new ColumnHeader();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.EditBtn = new ToolStripButton();
			this.LocationBtn = new ToolStripButton();
			this.ChooseBtn = new ToolStripButton();
			this.AddLibraryBtn = new ToolStripButton();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.EditBtn,
				this.LocationBtn,
				this.toolStripSeparator1,
				this.ChooseBtn,
				this.toolStripSeparator2,
				this.AddLibraryBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(520, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.SkillList.Columns.AddRange(new ColumnHeader[]
			{
				this.InfoHdr
			});
			this.SkillList.Dock = DockStyle.Fill;
			this.SkillList.FullRowSelect = true;
			listViewGroup.Header = "Info";
			listViewGroup.Name = "InfoHdr";
			listViewGroup2.Header = "Primary Skills";
			listViewGroup2.Name = "PrimaryHdr";
			listViewGroup3.Header = "Other Skills";
			listViewGroup3.Name = "SecondaryHdr";
			listViewGroup4.Header = "Automatic Failure";
			listViewGroup4.Name = "listViewGroup1";
			this.SkillList.Groups.AddRange(new ListViewGroup[]
			{
				listViewGroup,
				listViewGroup2,
				listViewGroup3,
				listViewGroup4
			});
			this.SkillList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.SkillList.HideSelection = false;
			this.SkillList.Location = new Point(0, 25);
			this.SkillList.MultiSelect = false;
			this.SkillList.Name = "SkillList";
			this.SkillList.Size = new Size(520, 155);
			this.SkillList.TabIndex = 1;
			this.SkillList.UseCompatibleStateImageBehavior = false;
			this.SkillList.View = View.Details;
			this.SkillList.DoubleClick += new EventHandler(this.SkillList_DoubleClick);
			this.InfoHdr.Text = "Info";
			this.InfoHdr.Width = 445;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 25);
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)resources.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new Size(128, 22);
			this.EditBtn.Text = "Skill Challenge Builder";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			this.LocationBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LocationBtn.Image = (Image)resources.GetObject("LocationBtn.Image");
			this.LocationBtn.ImageTransparentColor = Color.Magenta;
			this.LocationBtn.Name = "LocationBtn";
			this.LocationBtn.Size = new Size(103, 22);
			this.LocationBtn.Text = "Set Map Location";
			this.LocationBtn.Click += new EventHandler(this.LocationBtn_Click);
			this.ChooseBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ChooseBtn.Image = (Image)resources.GetObject("ChooseBtn.Image");
			this.ChooseBtn.ImageTransparentColor = Color.Magenta;
			this.ChooseBtn.Name = "ChooseBtn";
			this.ChooseBtn.Size = new Size(136, 22);
			this.ChooseBtn.Text = "Use Standard Challenge";
			this.ChooseBtn.Click += new EventHandler(this.ChooseBtn_Click);
			this.AddLibraryBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AddLibraryBtn.Image = (Image)resources.GetObject("AddLibraryBtn.Image");
			this.AddLibraryBtn.ImageTransparentColor = Color.Magenta;
			this.AddLibraryBtn.Name = "AddLibraryBtn";
			this.AddLibraryBtn.Size = new Size(86, 22);
			this.AddLibraryBtn.Text = "Add to Library";
			this.AddLibraryBtn.Click += new EventHandler(this.AddLibraryBtn_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.SkillList);
			base.Controls.Add(this.Toolbar);
			base.Name = "SkillChallengePanel";
			base.Size = new Size(520, 180);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public SkillChallengePanel()
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.ChooseBtn.Enabled = (Session.SkillChallenges.Count != 0);
		}

		public void Edit()
		{
			this.EditBtn_Click(null, null);
		}

		private void EditBtn_Click(object sender, EventArgs e)
		{
			SkillChallengeBuilderForm skillChallengeBuilderForm = new SkillChallengeBuilderForm(this.fChallenge);
			if (skillChallengeBuilderForm.ShowDialog() == DialogResult.OK)
			{
				this.fChallenge.Name = skillChallengeBuilderForm.SkillChallenge.Name;
				this.fChallenge.Complexity = skillChallengeBuilderForm.SkillChallenge.Complexity;
				this.fChallenge.Level = skillChallengeBuilderForm.SkillChallenge.Level;
				this.fChallenge.Success = skillChallengeBuilderForm.SkillChallenge.Success;
				this.fChallenge.Failure = skillChallengeBuilderForm.SkillChallenge.Failure;
				this.fChallenge.Notes = skillChallengeBuilderForm.SkillChallenge.Notes;
				this.fChallenge.Skills.Clear();
				foreach (SkillChallengeData current in skillChallengeBuilderForm.SkillChallenge.Skills)
				{
					this.fChallenge.Skills.Add(current.Copy());
				}
				this.update_view();
			}
		}

		private void LocationBtn_Click(object sender, EventArgs e)
		{
			MapAreaSelectForm mapAreaSelectForm = new MapAreaSelectForm(this.fChallenge.MapID, this.fChallenge.MapAreaID);
			if (mapAreaSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fChallenge.MapID = ((mapAreaSelectForm.Map != null) ? mapAreaSelectForm.Map.ID : Guid.Empty);
				this.fChallenge.MapAreaID = ((mapAreaSelectForm.MapArea != null) ? mapAreaSelectForm.MapArea.ID : Guid.Empty);
				this.update_view();
			}
		}

		private void ChooseBtn_Click(object sender, EventArgs e)
		{
			SkillChallengeSelectForm skillChallengeSelectForm = new SkillChallengeSelectForm();
			if (skillChallengeSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fChallenge.Name = skillChallengeSelectForm.SkillChallenge.Name;
				this.fChallenge.Complexity = skillChallengeSelectForm.SkillChallenge.Complexity;
				this.fChallenge.Success = skillChallengeSelectForm.SkillChallenge.Success;
				this.fChallenge.Failure = skillChallengeSelectForm.SkillChallenge.Failure;
				this.fChallenge.Skills.Clear();
				foreach (SkillChallengeData current in skillChallengeSelectForm.SkillChallenge.Skills)
				{
					this.fChallenge.Skills.Add(current.Copy());
				}
				this.fChallenge.Level = this.fPartyLevel;
				this.update_view();
			}
		}

		private void SkillList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedSkill != null)
			{
				int index = this.fChallenge.Skills.IndexOf(this.SelectedSkill);
				SkillChallengeSkillForm skillChallengeSkillForm = new SkillChallengeSkillForm(this.SelectedSkill);
				if (skillChallengeSkillForm.ShowDialog() == DialogResult.OK)
				{
					this.fChallenge.Skills[index] = skillChallengeSkillForm.SkillData;
					this.update_view();
				}
			}
		}

		private void update_view()
		{
			this.SkillList.Items.Clear();
			ListViewItem listViewItem = this.SkillList.Items.Add(string.Concat(new object[]
			{
				this.fChallenge.Name,
				": ",
				this.fChallenge.GetXP(),
				" XP"
			}));
			listViewItem.Group = this.SkillList.Groups[0];
			ListViewItem listViewItem2 = this.SkillList.Items.Add(this.fChallenge.Info);
			listViewItem2.Group = this.SkillList.Groups[0];
			if (this.fChallenge.MapID != Guid.Empty)
			{
				Map map = Session.Project.FindTacticalMap(this.fChallenge.MapID);
				if (map != null)
				{
					MapArea mapArea = map.FindArea(this.fChallenge.MapAreaID);
					if (mapArea != null)
					{
						string text = "Location: " + map.Name;
						if (mapArea != null)
						{
							text = text + " (" + mapArea.Name + ")";
						}
						ListViewItem listViewItem3 = this.SkillList.Items.Add(text);
						listViewItem3.Group = this.SkillList.Groups[0];
					}
				}
			}
			foreach (SkillChallengeData current in this.fChallenge.Skills)
			{
				string text2 = current.Difficulty.ToString().ToLower() + " DCs";
				if (current.DCModifier != 0)
				{
					if (current.DCModifier > 0)
					{
						text2 = text2 + " +" + current.DCModifier;
					}
					else
					{
						text2 = text2 + " " + current.DCModifier;
					}
				}
				string text3 = current.SkillName + " (" + text2 + ")";
				if (current.Details != "")
				{
					text3 = text3 + ": " + current.Details;
				}
				ListViewItem listViewItem4 = this.SkillList.Items.Add(text3);
				listViewItem4.Tag = current;
				switch (current.Type)
				{
				case SkillType.Primary:
					listViewItem4.Group = this.SkillList.Groups[1];
					break;
				case SkillType.Secondary:
					listViewItem4.Group = this.SkillList.Groups[2];
					break;
				case SkillType.AutoFail:
					listViewItem4.Group = this.SkillList.Groups[3];
					break;
				}
				if (current.Difficulty == Difficulty.Trivial || current.Difficulty == Difficulty.Extreme)
				{
					listViewItem4.ForeColor = Color.Red;
				}
			}
			if (this.SkillList.Groups[1].Items.Count == 0)
			{
				ListViewItem listViewItem5 = this.SkillList.Items.Add("(none)");
				listViewItem5.Group = this.SkillList.Groups[1];
				listViewItem5.ForeColor = SystemColors.GrayText;
			}
			if (this.SkillList.Groups[2].Items.Count == 0)
			{
				ListViewItem listViewItem6 = this.SkillList.Items.Add("(none)");
				listViewItem6.Group = this.SkillList.Groups[2];
				listViewItem6.ForeColor = SystemColors.GrayText;
			}
			this.SkillList.Sort();
		}

		private void AddLibraryBtn_Click(object sender, EventArgs e)
		{
			LibrarySelectForm librarySelectForm = new LibrarySelectForm();
			if (librarySelectForm.ShowDialog() == DialogResult.OK)
			{
				Library selectedLibrary = librarySelectForm.SelectedLibrary;
				selectedLibrary.SkillChallenges.Add(this.fChallenge.Copy() as SkillChallenge);
			}
		}
	}
}
