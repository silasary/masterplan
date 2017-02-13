using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class CreatureSkillsForm : Form
	{
		private class SkillData
		{
			public string SkillName;

			public bool Trained;

			public int Ability;

			public int Level;

			public int Misc;

			public bool Show
			{
				get
				{
					return this.Trained || this.Misc != 0;
				}
			}

			public int Total
			{
				get
				{
					int num = this.Trained ? 5 : 0;
					return num + this.Ability + this.Level + this.Misc;
				}
			}

			public override string ToString()
			{
				string text = (this.Total < 0) ? "-" : "";
				return string.Concat(new object[]
				{
					this.SkillName,
					" ",
					text,
					this.Total
				});
			}
		}

		private IContainer components;

		private Button OKBtn;

		private Button CancelBtn;

		private ListView SkillList;

		private ColumnHeader SkillHdr;

		private ColumnHeader TrainedHdr;

		private ColumnHeader AbilityHdr;

		private ColumnHeader MiscHdr;

		private ColumnHeader TotalHdr;

		private Panel SkillPanel;

		private ToolStrip Toolbar;

		private ToolStripButton TrainedBtn;

		private ToolStripButton EditSkillBtn;

		private ICreature fCreature;

		private List<CreatureSkillsForm.SkillData> fSkills = new List<CreatureSkillsForm.SkillData>();

		private CreatureSkillsForm.SkillData SelectedSkill
		{
			get
			{
				if (this.SkillList.SelectedItems.Count != 0)
				{
					return this.SkillList.SelectedItems[0].Tag as CreatureSkillsForm.SkillData;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(CreatureSkillsForm));
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.SkillList = new ListView();
			this.SkillHdr = new ColumnHeader();
			this.TrainedHdr = new ColumnHeader();
			this.AbilityHdr = new ColumnHeader();
			this.MiscHdr = new ColumnHeader();
			this.TotalHdr = new ColumnHeader();
			this.SkillPanel = new Panel();
			this.Toolbar = new ToolStrip();
			this.TrainedBtn = new ToolStripButton();
			this.EditSkillBtn = new ToolStripButton();
			this.SkillPanel.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(243, 367);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 5;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new EventHandler(this.OKBtn_Click);
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(324, 367);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 6;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.SkillList.Columns.AddRange(new ColumnHeader[]
			{
				this.SkillHdr,
				this.TrainedHdr,
				this.AbilityHdr,
				this.MiscHdr,
				this.TotalHdr
			});
			this.SkillList.Dock = DockStyle.Fill;
			this.SkillList.FullRowSelect = true;
			this.SkillList.HideSelection = false;
			this.SkillList.Location = new Point(0, 25);
			this.SkillList.MultiSelect = false;
			this.SkillList.Name = "SkillList";
			this.SkillList.Size = new Size(387, 324);
			this.SkillList.TabIndex = 7;
			this.SkillList.UseCompatibleStateImageBehavior = false;
			this.SkillList.View = View.Details;
			this.SkillList.DoubleClick += new EventHandler(this.SkillList_DoubleClick);
			this.SkillHdr.Text = "Skill";
			this.SkillHdr.Width = 120;
			this.TrainedHdr.Text = "Trained";
			this.TrainedHdr.TextAlign = HorizontalAlignment.Center;
			this.AbilityHdr.Text = "Ability";
			this.AbilityHdr.TextAlign = HorizontalAlignment.Right;
			this.MiscHdr.Text = "Misc";
			this.MiscHdr.TextAlign = HorizontalAlignment.Right;
			this.TotalHdr.Text = "Total";
			this.TotalHdr.TextAlign = HorizontalAlignment.Right;
			this.SkillPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.SkillPanel.Controls.Add(this.SkillList);
			this.SkillPanel.Controls.Add(this.Toolbar);
			this.SkillPanel.Location = new Point(12, 12);
			this.SkillPanel.Name = "SkillPanel";
			this.SkillPanel.Size = new Size(387, 349);
			this.SkillPanel.TabIndex = 8;
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.TrainedBtn,
				this.EditSkillBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(387, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.TrainedBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.TrainedBtn.Image = (Image)resources.GetObject("TrainedBtn.Image");
			this.TrainedBtn.ImageTransparentColor = Color.Magenta;
			this.TrainedBtn.Name = "TrainedBtn";
			this.TrainedBtn.Size = new Size(51, 22);
			this.TrainedBtn.Text = "Trained";
			this.TrainedBtn.Click += new EventHandler(this.TrainedBtn_Click);
			this.EditSkillBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditSkillBtn.Image = (Image)resources.GetObject("EditSkillBtn.Image");
			this.EditSkillBtn.ImageTransparentColor = Color.Magenta;
			this.EditSkillBtn.Name = "EditSkillBtn";
			this.EditSkillBtn.Size = new Size(55, 22);
			this.EditSkillBtn.Text = "Edit Skill";
			this.EditSkillBtn.Click += new EventHandler(this.EditSkillBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(411, 402);
			base.Controls.Add(this.SkillPanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CreatureSkillsForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Creature Skills";
			this.SkillPanel.ResumeLayout(false);
			this.SkillPanel.PerformLayout();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		public CreatureSkillsForm(ICreature creature)
		{
			this.InitializeComponent();
			Masterplan.Events.ApplicationIdleEventWrapper.Idle += new EventHandler(this.Application_Idle);
			this.fCreature = creature;
			Dictionary<string, int> dictionary = CreatureHelper.ParseSkills(this.fCreature.Skills);
			foreach (string current in Skills.GetSkillNames())
			{
				int num = this.fCreature.Level / 2;
				int num2 = 0;
				string keyAbility = Skills.GetKeyAbility(current);
				string a;
				if ((a = keyAbility) != null)
				{
					if (!(a == "Strength"))
					{
						if (!(a == "Constitution"))
						{
							if (!(a == "Dexterity"))
							{
								if (!(a == "Intelligence"))
								{
									if (!(a == "Wisdom"))
									{
										if (a == "Charisma")
										{
											num2 = this.fCreature.Charisma.Modifier;
										}
									}
									else
									{
										num2 = this.fCreature.Wisdom.Modifier;
									}
								}
								else
								{
									num2 = this.fCreature.Intelligence.Modifier;
								}
							}
							else
							{
								num2 = this.fCreature.Dexterity.Modifier;
							}
						}
						else
						{
							num2 = this.fCreature.Constitution.Modifier;
						}
					}
					else
					{
						num2 = this.fCreature.Strength.Modifier;
					}
				}
				CreatureSkillsForm.SkillData skillData = new CreatureSkillsForm.SkillData();
				skillData.SkillName = current;
				skillData.Ability = num2;
				skillData.Level = num;
				if (dictionary.ContainsKey(current))
				{
					int num3 = dictionary[current];
					int num4 = num3 - (num2 + num);
					if (num4 > 3)
					{
						skillData.Trained = true;
						num4 -= 5;
					}
					skillData.Misc = num4;
				}
				this.fSkills.Add(skillData);
			}
			this.update_list();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.TrainedBtn.Enabled = (this.SelectedSkill != null);
			this.TrainedBtn.Checked = (this.SelectedSkill != null && this.SelectedSkill.Trained);
			this.EditSkillBtn.Enabled = (this.SelectedSkill != null);
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			string text = "";
			foreach (CreatureSkillsForm.SkillData current in this.fSkills)
			{
				if (current.Show)
				{
					if (text != "")
					{
						text += "; ";
					}
					text += current.ToString();
				}
			}
			this.fCreature.Skills = text;
		}

		private void update_list()
		{
			this.SkillList.BeginUpdate();
			this.SkillList.Items.Clear();
			List<ListViewItem> list = new List<ListViewItem>();
			foreach (CreatureSkillsForm.SkillData current in this.fSkills)
			{
				ListViewItem listViewItem = new ListViewItem(current.SkillName);
				listViewItem.SubItems.Add(current.Trained ? "Yes" : "");
				listViewItem.SubItems.Add(current.Ability.ToString());
				listViewItem.SubItems.Add((current.Misc != 0) ? current.Misc.ToString() : "");
				listViewItem.SubItems.Add(current.Total.ToString());
				if (!current.Show)
				{
					listViewItem.ForeColor = SystemColors.GrayText;
					listViewItem.UseItemStyleForSubItems = false;
				}
				listViewItem.Tag = current;
				list.Add(listViewItem);
			}
			this.SkillList.Items.AddRange(list.ToArray());
			this.SkillList.EndUpdate();
		}

		private void SkillList_DoubleClick(object sender, EventArgs e)
		{
			this.TrainedBtn_Click(sender, e);
		}

		private void TrainedBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSkill == null)
			{
				return;
			}
			this.SelectedSkill.Trained = !this.SelectedSkill.Trained;
			this.update_list();
		}

		private void EditSkillBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedSkill == null)
			{
				return;
			}
			string keyAbility = Skills.GetKeyAbility(this.SelectedSkill.SkillName);
			CreatureSkillForm creatureSkillForm = new CreatureSkillForm(this.SelectedSkill.SkillName, keyAbility, this.SelectedSkill.Ability, this.SelectedSkill.Level, this.SelectedSkill.Trained, this.SelectedSkill.Misc);
			if (creatureSkillForm.ShowDialog() == DialogResult.OK)
			{
				this.SelectedSkill.Trained = creatureSkillForm.Trained;
				this.SelectedSkill.Misc = creatureSkillForm.Misc;
				this.update_list();
			}
		}
	}
}
