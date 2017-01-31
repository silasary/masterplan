using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.Tools.Generators;
using Masterplan.Wizards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class CreatureBuilderForm : Form
	{
		private enum SidebarType
		{
			Advice,
			Powers
		}

		private const int SAMPLE_POWERS = 5;

		private ICreature fCreature;

		private List<CreaturePower> fSamplePowers = new List<CreaturePower>();

		private CreatureBuilderForm.SidebarType fSidebar;

		private IContainer components;

		private ToolStrip Toolbar;

		private Panel BtnPnl;

		private Button CancelBtn;

		private Button OKBtn;

		private TabControl Pages;

		private TabPage StatBlockPage;

		private WebBrowser StatBlockBrowser;

		private TabPage PicturePage;

		private ToolStripDropDownButton OptionsMenu;

		private ToolStripMenuItem OptionsImport;

		private ToolStripMenuItem OptionsVariant;

		private PictureBox PortraitBox;

		private ToolStrip PictureToolbar;

		private ToolStripButton PictureBrowseBtn;

		private ToolStripButton PicturePasteBtn;

		private ToolStripButton PictureClearBtn;

		private TabPage EntryPage;

		private WebBrowser EntryBrowser;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem OptionsEntry;

		private ToolStripButton LevelDownBtn;

		private ToolStripButton LevelUpBtn;

		private ToolStripLabel LevelLbl;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem OptionsPowerBrowser;

		private ToolStripMenuItem OptionsRandom;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton AdviceBtn;

		private ToolStripButton PowersBtn;

		private ToolStripDropDownButton FileMenu;

		private ToolStripMenuItem FileExport;

		public ICreature Creature
		{
			get
			{
				return this.fCreature;
			}
		}

		public CreatureBuilderForm(ICreature creature)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			if (creature is Creature)
			{
				Creature creature2 = creature as Creature;
				this.fCreature = creature2.Copy();
			}
			if (creature is CustomCreature)
			{
				CustomCreature customCreature = creature as CustomCreature;
				this.fCreature = customCreature.Copy();
			}
			if (creature is NPC)
			{
				NPC nPC = creature as NPC;
				this.fCreature = nPC.Copy();
				this.OptionsImport.Enabled = false;
				this.OptionsVariant.Enabled = false;
			}
			if (Session.Project == null)
			{
				this.Pages.TabPages.Remove(this.EntryPage);
				this.OptionsEntry.Enabled = false;
			}
			else
			{
				this.update_entry();
			}
			this.find_sample_powers();
			this.update_view();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.PicturePasteBtn.Enabled = Clipboard.ContainsImage();
			this.PictureClearBtn.Enabled = (this.fCreature.Image != null);
			this.AdviceBtn.Checked = (this.fSidebar == CreatureBuilderForm.SidebarType.Advice);
			this.PowersBtn.Checked = (this.fSidebar == CreatureBuilderForm.SidebarType.Powers);
			this.LevelDownBtn.Enabled = (this.fCreature.Level > 1);
		}

		private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.Scheme == "build")
			{
				if (e.Url.LocalPath == "profile")
				{
					e.Cancel = true;
					int level = this.fCreature.Level;
					string b = this.fCreature.Role.ToString();
					CreatureProfileForm creatureProfileForm = new CreatureProfileForm(this.fCreature);
					if (creatureProfileForm.ShowDialog() == DialogResult.OK)
					{
						if (this.fCreature.Level != level || this.fCreature.Role.ToString() != b)
						{
							string text = "You've changed this creature's level and/or role.";
							text += Environment.NewLine;
							text += "Do you want to update its combat statistics to match?";
							if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
							{
								if (this.fCreature.Role is ComplexRole)
								{
									this.fCreature.HP = Statistics.HP(this.fCreature.Level, this.fCreature.Role as ComplexRole, this.fCreature.Constitution.Score);
								}
								else
								{
									this.fCreature.HP = 1;
								}
								this.fCreature.Initiative = Statistics.Initiative(this.fCreature.Level, this.fCreature.Role);
								this.fCreature.AC = Statistics.AC(this.fCreature.Level, this.fCreature.Role);
								this.fCreature.Fortitude = Statistics.NAD(this.fCreature.Level, this.fCreature.Role);
								this.fCreature.Reflex = Statistics.NAD(this.fCreature.Level, this.fCreature.Role);
								this.fCreature.Will = Statistics.NAD(this.fCreature.Level, this.fCreature.Role);
							}
						}
						this.find_sample_powers();
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "combat")
				{
					e.Cancel = true;
					CreatureStatsForm creatureStatsForm = new CreatureStatsForm(this.fCreature);
					if (creatureStatsForm.ShowDialog() == DialogResult.OK)
					{
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "ability")
				{
					e.Cancel = true;
					CreatureAbilityForm creatureAbilityForm = new CreatureAbilityForm(this.fCreature);
					if (creatureAbilityForm.ShowDialog() == DialogResult.OK)
					{
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "damage")
				{
					e.Cancel = true;
					DamageModListForm damageModListForm = new DamageModListForm(this.fCreature);
					if (damageModListForm.ShowDialog() == DialogResult.OK)
					{
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "senses")
				{
					e.Cancel = true;
					string hint = "Note: Do not add Perception here; it should be entered as a skill.";
					DetailsForm detailsForm = new DetailsForm(this.fCreature, DetailsField.Senses, hint);
					if (detailsForm.ShowDialog() == DialogResult.OK)
					{
						this.fCreature.Senses = detailsForm.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "movement")
				{
					e.Cancel = true;
					DetailsForm detailsForm2 = new DetailsForm(this.fCreature, DetailsField.Movement, null);
					if (detailsForm2.ShowDialog() == DialogResult.OK)
					{
						this.fCreature.Movement = detailsForm2.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "alignment")
				{
					e.Cancel = true;
					DetailsForm detailsForm3 = new DetailsForm(this.fCreature, DetailsField.Alignment, null);
					if (detailsForm3.ShowDialog() == DialogResult.OK)
					{
						this.fCreature.Alignment = detailsForm3.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "languages")
				{
					e.Cancel = true;
					DetailsForm detailsForm4 = new DetailsForm(this.fCreature, DetailsField.Languages, null);
					if (detailsForm4.ShowDialog() == DialogResult.OK)
					{
						this.fCreature.Languages = detailsForm4.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "skills")
				{
					e.Cancel = true;
					CreatureSkillsForm creatureSkillsForm = new CreatureSkillsForm(this.fCreature);
					if (creatureSkillsForm.ShowDialog() == DialogResult.OK)
					{
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "equipment")
				{
					e.Cancel = true;
					DetailsForm detailsForm5 = new DetailsForm(this.fCreature, DetailsField.Equipment, null);
					if (detailsForm5.ShowDialog() == DialogResult.OK)
					{
						this.fCreature.Equipment = detailsForm5.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "tactics")
				{
					e.Cancel = true;
					DetailsForm detailsForm6 = new DetailsForm(this.fCreature, DetailsField.Tactics, null);
					if (detailsForm6.ShowDialog() == DialogResult.OK)
					{
						this.fCreature.Tactics = detailsForm6.Details;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "import")
				{
					e.Cancel = true;
					this.import_creature();
				}
				if (e.Url.LocalPath == "variant")
				{
					e.Cancel = true;
					this.create_variant();
				}
				if (e.Url.LocalPath == "random")
				{
					e.Cancel = true;
					this.create_random();
				}
				if (e.Url.LocalPath == "hybrid")
				{
					e.Cancel = true;
					this.create_hybrid();
				}
				if (e.Url.LocalPath == "name")
				{
					e.Cancel = true;
					string name = this.fCreature.Name;
					this.fCreature.Name = this.generate_name();
					for (int num = 0; num != this.fCreature.CreaturePowers.Count; num++)
					{
						CreaturePower creaturePower = this.fCreature.CreaturePowers[num];
						creaturePower = this.replace_name(creaturePower, name, "", this.fCreature.Name);
						this.fCreature.CreaturePowers[num] = creaturePower;
					}
					for (int num2 = 0; num2 != this.fSamplePowers.Count; num2++)
					{
						CreaturePower creaturePower2 = this.fSamplePowers[num2];
						creaturePower2 = this.replace_name(creaturePower2, name, "", this.fCreature.Name);
						this.fSamplePowers[num2] = creaturePower2;
					}
					this.update_statblock();
				}
				if (e.Url.LocalPath == "template")
				{
					e.Cancel = true;
					CreatureTemplateSelectForm creatureTemplateSelectForm = new CreatureTemplateSelectForm();
					if (creatureTemplateSelectForm.ShowDialog() == DialogResult.OK && creatureTemplateSelectForm.Template != null)
					{
						EncounterCard encounterCard = new EncounterCard(this.fCreature);
						encounterCard.TemplateIDs.Add(creatureTemplateSelectForm.Template.ID);
						ICreature creature = null;
						if (this.fCreature is Creature)
						{
							creature = new Creature();
						}
						if (this.fCreature is CustomCreature)
						{
							creature = new CustomCreature();
						}
						if (this.fCreature is NPC)
						{
							creature = new NPC();
						}
						creature.Name = encounterCard.Title;
						creature.Level = encounterCard.Level;
						creature.Senses = encounterCard.Senses;
						creature.Movement = encounterCard.Movement;
						creature.Resist = encounterCard.Resist;
						creature.Vulnerable = encounterCard.Vulnerable;
						creature.Immune = encounterCard.Immune;
						creature.Role = new ComplexRole
						{
							Leader = encounterCard.Leader,
							Flag = encounterCard.Flag,
							Type = encounterCard.Roles[0]
						};
						creature.Initiative = encounterCard.Initiative;
						creature.HP = encounterCard.HP;
						creature.AC = encounterCard.AC;
						creature.Fortitude = encounterCard.Fortitude;
						creature.Reflex = encounterCard.Reflex;
						creature.Will = encounterCard.Will;
						creature.Regeneration = ((encounterCard.Regeneration != null) ? encounterCard.Regeneration : null);
						List<Aura> auras = encounterCard.Auras;
						foreach (Aura current in auras)
						{
							creature.Auras.Add(current.Copy());
						}
						List<CreaturePower> creaturePowers = encounterCard.CreaturePowers;
						foreach (CreaturePower current2 in creaturePowers)
						{
							creature.CreaturePowers.Add(current2.Copy());
						}
						List<DamageModifier> damageModifiers = encounterCard.DamageModifiers;
						foreach (DamageModifier current3 in damageModifiers)
						{
							creature.DamageModifiers.Add(current3.Copy());
						}
						Guid iD = this.fCreature.ID;
						CreatureHelper.CopyFields(creature, this.fCreature);
						this.fCreature.ID = iD;
						this.find_sample_powers();
						this.update_view();
					}
				}
			}
			if (e.Url.Scheme == "power")
			{
				if (e.Url.LocalPath == "addpower")
				{
					e.Cancel = true;
					PowerBuilderForm powerBuilderForm = new PowerBuilderForm(new CreaturePower
					{
						Name = "New Power",
						Action = new PowerAction()
					}, this.fCreature, false);
					if (powerBuilderForm.ShowDialog() == DialogResult.OK)
					{
						this.fCreature.CreaturePowers.Add(powerBuilderForm.Power);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "addtrait")
				{
					e.Cancel = true;
					PowerBuilderForm powerBuilderForm2 = new PowerBuilderForm(new CreaturePower
					{
						Name = "New Trait",
						Action = null
					}, this.fCreature, false);
					if (powerBuilderForm2.ShowDialog() == DialogResult.OK)
					{
						this.fCreature.CreaturePowers.Add(powerBuilderForm2.Power);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "addaura")
				{
					e.Cancel = true;
					AuraForm auraForm = new AuraForm(new Aura
					{
						Name = "New Aura",
						Details = "1"
					});
					if (auraForm.ShowDialog() == DialogResult.OK)
					{
						this.fCreature.Auras.Add(auraForm.Aura);
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "browse")
				{
					e.Cancel = true;
					this.OptionsPowerBrowser_Click(null, null);
				}
				if (e.Url.LocalPath == "statistics")
				{
					e.Cancel = true;
					List<ICreature> list = this.find_matching_creatures(this.fCreature.Role, this.fCreature.Level, true);
					List<CreaturePower> list2 = new List<CreaturePower>();
					foreach (ICreature current4 in list)
					{
						list2.AddRange(current4.CreaturePowers);
					}
					PowerStatisticsForm powerStatisticsForm = new PowerStatisticsForm(list2, list.Count);
					powerStatisticsForm.ShowDialog();
				}
				if (e.Url.LocalPath == "regenedit")
				{
					e.Cancel = true;
					Regeneration regeneration = this.fCreature.Regeneration;
					if (regeneration == null)
					{
						regeneration = new Regeneration(5, "");
					}
					RegenerationForm regenerationForm = new RegenerationForm(regeneration);
					if (regenerationForm.ShowDialog() == DialogResult.OK)
					{
						this.fCreature.Regeneration = regenerationForm.Regeneration;
						this.update_statblock();
					}
				}
				if (e.Url.LocalPath == "regenremove")
				{
					e.Cancel = true;
					this.fCreature.Regeneration = null;
					this.update_statblock();
				}
				if (e.Url.LocalPath == "refresh")
				{
					e.Cancel = true;
					this.find_sample_powers();
					this.update_statblock();
				}
			}
			if (e.Url.Scheme == "poweredit")
			{
				CreaturePower creaturePower3 = this.find_power(new Guid(e.Url.LocalPath));
				if (creaturePower3 != null)
				{
					e.Cancel = true;
					int index = this.fCreature.CreaturePowers.IndexOf(creaturePower3);
					PowerBuilderForm powerBuilderForm3 = new PowerBuilderForm(creaturePower3, this.fCreature, false);
					if (powerBuilderForm3.ShowDialog() == DialogResult.OK)
					{
						this.fCreature.CreaturePowers[index] = powerBuilderForm3.Power;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "powerremove")
			{
				CreaturePower creaturePower4 = this.find_power(new Guid(e.Url.LocalPath));
				if (creaturePower4 != null)
				{
					e.Cancel = true;
					this.fCreature.CreaturePowers.Remove(creaturePower4);
					this.update_statblock();
				}
			}
			if (e.Url.Scheme == "powerduplicate")
			{
				CreaturePower creaturePower5 = this.find_power(new Guid(e.Url.LocalPath));
				if (creaturePower5 != null)
				{
					e.Cancel = true;
					int index2 = this.fCreature.CreaturePowers.IndexOf(creaturePower5);
					creaturePower5 = creaturePower5.Copy();
					creaturePower5.ID = Guid.NewGuid();
					this.fCreature.CreaturePowers.Insert(index2, creaturePower5);
					this.update_statblock();
				}
			}
			if (e.Url.Scheme == "auraedit")
			{
				Aura aura = this.find_aura(new Guid(e.Url.LocalPath));
				if (aura != null)
				{
					e.Cancel = true;
					int index3 = this.fCreature.Auras.IndexOf(aura);
					AuraForm auraForm2 = new AuraForm(aura);
					if (auraForm2.ShowDialog() == DialogResult.OK)
					{
						this.fCreature.Auras[index3] = auraForm2.Aura;
						this.update_statblock();
					}
				}
			}
			if (e.Url.Scheme == "auraremove")
			{
				Aura aura2 = this.find_aura(new Guid(e.Url.LocalPath));
				if (aura2 != null)
				{
					e.Cancel = true;
					this.fCreature.Auras.Remove(aura2);
					this.update_statblock();
				}
			}
			if (e.Url.Scheme == "samplepower")
			{
				CreaturePower creaturePower6 = this.find_sample_power(new Guid(e.Url.LocalPath));
				if (creaturePower6 != null)
				{
					e.Cancel = true;
					this.fCreature.CreaturePowers.Add(creaturePower6);
					this.fSamplePowers.Remove(creaturePower6);
					if (this.fSamplePowers.Count == 0)
					{
						this.find_sample_powers();
					}
					this.update_statblock();
				}
			}
		}

		private void OptionsImport_Click(object sender, EventArgs e)
		{
			this.import_creature();
		}

		private void OptionsVariant_Click(object sender, EventArgs e)
		{
			this.create_variant();
		}

		private void OptionsRandom_Click(object sender, EventArgs e)
		{
			this.create_random();
		}

		private void OptionsEntry_Click(object sender, EventArgs e)
		{
			EncyclopediaEntry encyclopediaEntry = Session.Project.Encyclopedia.FindEntryForAttachment(this.fCreature.ID);
			if (encyclopediaEntry == null)
			{
				string text = "There is no encyclopedia entry associated with this creature.";
				text += Environment.NewLine;
				text += "Would you like to create one now?";
				if (MessageBox.Show(text, "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					return;
				}
				encyclopediaEntry = new EncyclopediaEntry();
				encyclopediaEntry.Name = this.fCreature.Name;
				encyclopediaEntry.AttachmentID = this.fCreature.ID;
				encyclopediaEntry.Category = "Creatures";
				Session.Project.Encyclopedia.Entries.Add(encyclopediaEntry);
				Session.Modified = true;
			}
			int index = Session.Project.Encyclopedia.Entries.IndexOf(encyclopediaEntry);
			EncyclopediaEntryForm encyclopediaEntryForm = new EncyclopediaEntryForm(encyclopediaEntry);
			if (encyclopediaEntryForm.ShowDialog() == DialogResult.OK)
			{
				Session.Project.Encyclopedia.Entries[index] = encyclopediaEntryForm.Entry;
				Session.Modified = true;
				this.update_entry();
			}
		}

		private void OptionsPowerBrowser_Click(object sender, EventArgs e)
		{
			PowerBrowserForm powerBrowserForm = new PowerBrowserForm(this.fCreature.Name, this.fCreature.Level, this.fCreature.Role, new PowerCallback(this.add_power));
			powerBrowserForm.ShowDialog();
		}

		private void AdviceBtn_Click(object sender, EventArgs e)
		{
			if (this.fSidebar != CreatureBuilderForm.SidebarType.Advice)
			{
				this.fSidebar = CreatureBuilderForm.SidebarType.Advice;
				this.update_statblock();
			}
		}

		private void PowersBtn_Click(object sender, EventArgs e)
		{
			if (this.fSidebar != CreatureBuilderForm.SidebarType.Powers)
			{
				this.fSidebar = CreatureBuilderForm.SidebarType.Powers;
				this.update_statblock();
			}
		}

		private void LevelUpBtn_Click(object sender, EventArgs e)
		{
			CreatureHelper.AdjustCreatureLevel(this.fCreature, 1);
			this.find_sample_powers();
			this.update_statblock();
		}

		private void LevelDownBtn_Click(object sender, EventArgs e)
		{
			CreatureHelper.AdjustCreatureLevel(this.fCreature, -1);
			this.find_sample_powers();
			this.update_statblock();
		}

		private void PictureBrowseBtn_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = Program.ImageFilter;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.fCreature.Image = Image.FromFile(openFileDialog.FileName);
				Program.SetResolution(this.fCreature.Image);
				this.update_picture();
			}
		}

		private void PicturePasteBtn_Click(object sender, EventArgs e)
		{
			if (Clipboard.ContainsImage())
			{
				this.fCreature.Image = Clipboard.GetImage();
				Program.SetResolution(this.fCreature.Image);
				this.update_picture();
			}
		}

		private void PictureClearBtn_Click(object sender, EventArgs e)
		{
			this.fCreature.Image = null;
			this.update_picture();
		}

		private void update_view()
		{
			this.update_statblock();
			this.update_picture();
		}

		private void update_statblock()
		{
			EncounterCard encounterCard = new EncounterCard(this.fCreature);
			List<string> head = HTML.GetHead("Creature", "", DisplaySize.Small);
			head.Add("<BODY>");
			head.Add("<TABLE class=clear>");
			head.Add("<TR class=clear>");
			head.Add("<TD class=clear>");
			head.Add("<P class=table>");
			head.AddRange(encounterCard.AsText(null, CardMode.Build, true));
			head.Add("</P>");
			head.Add("</TD>");
			switch (this.fSidebar)
			{
			case CreatureBuilderForm.SidebarType.Advice:
			{
				head.Add("<TD class=clear>");
				head.Add("<P class=table>");
				bool flag = Session.Creatures.Count >= 2;
				head.Add("<P class=table>");
				head.Add("<TABLE>");
				head.Add("<TR class=heading>");
				head.Add("<TD><B>Create A New Creature</B></TD>");
				head.Add("</TR>");
				head.Add("<TR>");
				head.Add("<TD>");
				head.Add("Import a <A href=build:import>creature file</A> from Adventure Tools");
				head.Add("</TD>");
				head.Add("</TR>");
				head.Add("<TR>");
				head.Add("<TD>");
				head.Add("Create a <A href=build:variant>variant</A> of an existing creature");
				head.Add("</TD>");
				head.Add("</TR>");
				head.Add("<TR>");
				head.Add("<TD>");
				head.Add("Generate a <A href=build:random>random creature</A>");
				head.Add("</TD>");
				head.Add("</TR>");
				if (flag)
				{
					head.Add("<TR>");
					head.Add("<TD>");
					head.Add("Generate a <A href=build:hybrid>hybrid creature</A>");
					head.Add("</TD>");
					head.Add("</TR>");
				}
				head.Add("</TABLE>");
				head.Add("</P>");
				bool flag2 = false;
				ComplexRole complexRole = this.fCreature.Role as ComplexRole;
				if (complexRole != null)
				{
					flag2 = (complexRole.Flag != RoleFlag.Solo);
				}
				head.Add("<P class=table>");
				head.Add("<TABLE>");
				head.Add("<TR class=heading>");
				head.Add("<TD><B>Modify This Creature</B></TD>");
				head.Add("</TR>");
				head.Add("<TR>");
				head.Add("<TD>");
				head.Add("Generate a <A href=build:name>new name</A> for this creature");
				head.Add("</TD>");
				head.Add("</TR>");
				head.Add("<TR>");
				head.Add("<TD>");
				head.Add("Browse for <A href=power:browse>existing powers</A> for this creature");
				head.Add("</TD>");
				head.Add("</TR>");
				if (flag2)
				{
					head.Add("<TR>");
					head.Add("<TD>");
					head.Add("Apply a <A href=build:template>template</A> to this creature");
					head.Add("</TD>");
				}
				head.Add("</TR>");
				head.Add("<TR>");
				head.Add("<TD>");
				head.Add("See <A href=power:statistics>power statistics</A> for other creatures of this type");
				head.Add("</TD>");
				head.Add("</TR>");
				head.Add("</TABLE>");
				head.Add("</P>");
				head.Add("<P class=table>");
				head.Add("<TABLE>");
				head.Add("<TR class=heading>");
				head.Add("<TD colspan=2><B>Creature Advice</B></TD>");
				head.Add("</TR>");
				int num = Statistics.Initiative(this.fCreature.Level, this.fCreature.Role);
				int num2 = Statistics.AC(this.fCreature.Level, this.fCreature.Role);
				int num3 = Statistics.NAD(this.fCreature.Level, this.fCreature.Role);
				bool flag3 = this.fCreature.Role != null && this.fCreature.Role is Minion;
				if (!flag3)
				{
					if (!flag3)
					{
						Statistics.HP(this.fCreature.Level, this.fCreature.Role as ComplexRole, this.fCreature.Constitution.Score);
					}
					head.Add("<TR>");
					head.Add("<TD>Hit Points</TD>");
					head.Add("<TD align=center>+" + Statistics.AttackBonus(DefenceType.AC, this.fCreature.Level, this.fCreature.Role) + "</TD>");
					head.Add("</TR>");
				}
				head.Add("<TR>");
				head.Add("<TD>Initiative Bonus</TD>");
				head.Add("<TD align=center>+" + num + "</TD>");
				head.Add("</TR>");
				head.Add("<TR>");
				head.Add("<TD>Armour Class</TD>");
				head.Add("<TD align=center>+" + num2 + "</TD>");
				head.Add("</TR>");
				head.Add("<TR>");
				head.Add("<TD>Other Defences</TD>");
				head.Add("<TD align=center>+" + num3 + "</TD>");
				head.Add("</TR>");
				head.Add("<TR>");
				head.Add("<TD>Number of Powers</TD>");
				head.Add("<TD align=center>4 - 6</TD>");
				head.Add("</TR>");
				head.Add("</TABLE>");
				head.Add("</P>");
				head.Add("</TD>");
				break;
			}
			case CreatureBuilderForm.SidebarType.Powers:
				head.Add("<TD class=clear>");
				head.Add("<P class=table>");
				if (this.fSamplePowers.Count != 0)
				{
					head.Add("<P text-align=left>");
					head.Add("The following powers might be suitable for this creature.");
					head.Add("Click <A href=power:refresh>here</A> to resample this list, or <A href=power:browse>here</A> to look for other powers.");
					head.Add("</P>");
					head.Add("<P class=table>");
					head.Add("<TABLE>");
					Dictionary<CreaturePowerCategory, List<CreaturePower>> dictionary = new Dictionary<CreaturePowerCategory, List<CreaturePower>>();
					Array values = Enum.GetValues(typeof(CreaturePowerCategory));
					foreach (CreaturePowerCategory key in values)
					{
						dictionary[key] = new List<CreaturePower>();
					}
					foreach (CreaturePower current in this.fSamplePowers)
					{
						dictionary[current.Category].Add(current);
					}
					foreach (CreaturePowerCategory creaturePowerCategory in values)
					{
						int count = dictionary[creaturePowerCategory].Count;
						if (count != 0)
						{
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
							head.Add("<TR class=creature>");
							head.Add("<TD colspan=3>");
							head.Add("<B>" + str + "</B>");
							head.Add("</TD>");
							head.Add("</TR>");
							foreach (CreaturePower current2 in dictionary[creaturePowerCategory])
							{
								head.AddRange(current2.AsHTML(null, CardMode.View, false));
								head.Add("<TR>");
								head.Add("<TD colspan=3 align=center>");
								head.Add(string.Concat(new object[]
								{
									"<A href=samplepower:",
									current2.ID,
									">copy this power into ",
									this.fCreature.Name,
									"</A>"
								}));
								head.Add("</TD>");
								head.Add("</TR>");
							}
						}
					}
					head.Add("</TABLE>");
					head.Add("</P>");
				}
				head.Add("</TD>");
				break;
			}
			head.Add("</TR>");
			head.Add("</TABLE>");
			head.Add("</BODY>");
			head.Add("</HTML>");
			this.StatBlockBrowser.DocumentText = HTML.Concatenate(head);
		}

		private void update_picture()
		{
			this.PortraitBox.Image = this.fCreature.Image;
		}

		private void update_entry()
		{
			EncyclopediaEntry entry = Session.Project.Encyclopedia.FindEntryForAttachment(this.fCreature.ID);
			this.EntryBrowser.DocumentText = HTML.EncyclopediaEntry(entry, Session.Project.Encyclopedia, DisplaySize.Small, true, false, false, false);
		}

		private string generate_name()
		{
			string text = ExoticName.SingleName();
			List<ICreature> creatures = this.find_matching_creatures(this.fCreature.Role, this.fCreature.Level, false);
			string text2 = this.find_description(creatures);
			if (text2 != "")
			{
				text = text + " " + text2;
			}
			return text;
		}

		private string find_description(List<ICreature> creatures)
		{
			List<string> list = new List<string>();
			list.Add("er");
			list.Add("ist");
			List<string> list2 = new List<string>();
			foreach (ICreature current in creatures)
			{
				string[] array = current.Name.Split(null);
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i];
					bool flag = false;
					foreach (string current2 in list)
					{
						if (text.EndsWith(current2))
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						list2.Add(text);
					}
				}
			}
			if (list2.Count != 0)
			{
				int index = Session.Random.Next(list2.Count);
				return list2[index];
			}
			return "";
		}

		private CreaturePower find_power(Guid id)
		{
			foreach (CreaturePower current in this.fCreature.CreaturePowers)
			{
				if (current.ID == id)
				{
					return current;
				}
			}
			return null;
		}

		private Aura find_aura(Guid id)
		{
			foreach (Aura current in this.fCreature.Auras)
			{
				if (current.ID == id)
				{
					return current;
				}
			}
			return null;
		}

		private void add_power(CreaturePower power)
		{
			this.fCreature.CreaturePowers.Add(power);
			this.update_statblock();
		}

		private void find_sample_powers()
		{
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			List<CreaturePower> list = new List<CreaturePower>();
			foreach (CreaturePower current in this.fCreature.CreaturePowers)
			{
				if (current != null)
				{
					binarySearchTree.Add(current.Name.ToLower());
				}
			}
			List<ICreature> list2 = this.find_matching_creatures(this.fCreature.Role, this.fCreature.Level, false);
			foreach (ICreature current2 in list2)
			{
				foreach (CreaturePower current3 in current2.CreaturePowers)
				{
					if (current3 != null && !binarySearchTree.Contains(current3.Name.ToLower()))
					{
						CreaturePower creaturePower = this.replace_name(current3, current2.Name, current2.Category, this.fCreature.Name);
						creaturePower = this.alter_power_level(creaturePower, current2.Level, this.fCreature.Level);
						if (creaturePower != null)
						{
							list.Add(creaturePower.Copy());
							binarySearchTree.Add(creaturePower.Name);
						}
					}
				}
			}
			int num = Math.Min(list.Count, 5);
			this.fSamplePowers.Clear();
			while (this.fSamplePowers.Count != num)
			{
				int index = Session.Random.Next(list.Count);
				CreaturePower creaturePower2 = list[index];
				if (creaturePower2 != null)
				{
					this.fSamplePowers.Add(creaturePower2);
					list.Remove(creaturePower2);
				}
			}
		}

		private CreaturePower find_sample_power(Guid id)
		{
			foreach (CreaturePower current in this.fSamplePowers)
			{
				if (current.ID == id)
				{
					return current;
				}
			}
			return null;
		}

		private List<ICreature> find_matching_creatures(IRole role, int level, bool exact_match)
		{
			List<ICreature> list = new List<ICreature>();
			int num = exact_match ? 0 : 1;
			List<ICreature> list2 = new List<ICreature>();
			List<Creature> creatures = Session.Creatures;
			foreach (ICreature current in creatures)
			{
				list2.Add(current);
			}
			if (Session.Project != null)
			{
				foreach (ICreature current2 in Session.Project.CustomCreatures)
				{
					list2.Add(current2);
				}
				foreach (ICreature current3 in Session.Project.NPCs)
				{
					list2.Add(current3);
				}
			}
			foreach (ICreature current4 in list2)
			{
				bool flag = Math.Abs(current4.Level - level) <= num;
				bool flag2 = current4.Role.ToString() == role.ToString();
				if (flag && flag2)
				{
					list.Add(current4);
				}
			}
			return list;
		}

		private CreaturePower replace_name(CreaturePower power, string original_name, string original_category, string replacement)
		{
			CreaturePower creaturePower = power.Copy();
			if (!string.IsNullOrEmpty(original_name) && !replacement.Contains(original_name))
			{
				creaturePower.Details = this.replace_text(creaturePower.Details, original_name, replacement);
				creaturePower.Description = this.replace_text(creaturePower.Description, original_name, replacement);
				creaturePower.Condition = this.replace_text(creaturePower.Condition, original_name, replacement);
				creaturePower.Range = this.replace_text(creaturePower.Range, original_name, replacement);
			}
			if (!string.IsNullOrEmpty(original_category) && !replacement.Contains(original_category))
			{
				creaturePower.Details = this.replace_text(creaturePower.Details, original_category, replacement);
				creaturePower.Description = this.replace_text(creaturePower.Description, original_category, replacement);
				creaturePower.Condition = this.replace_text(creaturePower.Condition, original_category, replacement);
				creaturePower.Range = this.replace_text(creaturePower.Range, original_category, replacement);
			}
			return creaturePower;
		}

		private string replace_text(string source, string original, string replacement)
		{
			if (source == null || original == null || replacement == null)
			{
				return source;
			}
			if (replacement.Contains(original))
			{
				return source;
			}
			string text = source;
			while (true)
			{
				int num = text.ToLower().IndexOf(original.ToLower());
				if (num == -1)
				{
					break;
				}
				string str = text.Substring(0, num);
				string str2 = text.Substring(num + original.Length);
				text = str + replacement.ToLower() + str2;
			}
			return text;
		}

		private void import_creature()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = Program.MonsterFilter;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string xml = File.ReadAllText(openFileDialog.FileName);
				Creature creature = AppImport.ImportCreature(xml);
				if (creature != null)
				{
					Guid iD = this.fCreature.ID;
					CreatureHelper.CopyFields(creature, this.fCreature);
					this.fCreature.ID = iD;
					this.find_sample_powers();
					this.update_view();
				}
			}
		}

		private void create_random()
		{
			RandomCreatureForm randomCreatureForm = new RandomCreatureForm(this.fCreature.Level, this.fCreature.Role);
			if (randomCreatureForm.ShowDialog() == DialogResult.OK)
			{
				List<ICreature> list = this.find_matching_creatures(randomCreatureForm.Role, randomCreatureForm.Level, true);
				if (list.Count == 0)
				{
					return;
				}
				this.splice(list);
				this.find_sample_powers();
				this.update_view();
			}
		}

		private void create_variant()
		{
			VariantWizard variantWizard = new VariantWizard();
			if (variantWizard.Show() == DialogResult.OK)
			{
				VariantData variantData = variantWizard.Data as VariantData;
				EncounterCard encounterCard = new EncounterCard();
				encounterCard.CreatureID = variantData.BaseCreature.ID;
				foreach (CreatureTemplate current in variantData.Templates)
				{
					encounterCard.TemplateIDs.Add(current.ID);
				}
				ICreature creature = null;
				if (this.fCreature is Creature)
				{
					creature = new Creature();
				}
				if (this.fCreature is CustomCreature)
				{
					creature = new CustomCreature();
				}
				if (this.fCreature is NPC)
				{
					creature = new NPC();
				}
				creature.Name = "Variant " + encounterCard.Title;
				creature.Details = variantData.BaseCreature.Details;
				creature.Size = variantData.BaseCreature.Size;
				creature.Level = encounterCard.Level;
				if (variantData.BaseCreature.Image != null)
				{
					creature.Image = new Bitmap(variantData.BaseCreature.Image);
				}
				creature.Senses = encounterCard.Senses;
				creature.Movement = encounterCard.Movement;
				creature.Resist = encounterCard.Resist;
				creature.Vulnerable = encounterCard.Vulnerable;
				creature.Immune = encounterCard.Immune;
				if (variantData.BaseCreature.Role is Minion)
				{
					creature.Role = new Minion();
				}
				else
				{
					creature.Role = new ComplexRole
					{
						Type = variantData.Roles[variantData.SelectedRoleIndex],
						Flag = encounterCard.Flag,
						Leader = encounterCard.Leader
					};
				}
				creature.Strength.Score = variantData.BaseCreature.Strength.Score;
				creature.Constitution.Score = variantData.BaseCreature.Constitution.Score;
				creature.Dexterity.Score = variantData.BaseCreature.Dexterity.Score;
				creature.Intelligence.Score = variantData.BaseCreature.Intelligence.Score;
				creature.Wisdom.Score = variantData.BaseCreature.Wisdom.Score;
				creature.Charisma.Score = variantData.BaseCreature.Charisma.Score;
				creature.Initiative = variantData.BaseCreature.Initiative;
				creature.HP = encounterCard.HP;
				creature.AC = encounterCard.AC;
				creature.Fortitude = encounterCard.Fortitude;
				creature.Reflex = encounterCard.Reflex;
				creature.Will = encounterCard.Will;
				creature.Regeneration = ((encounterCard.Regeneration != null) ? encounterCard.Regeneration : null);
				List<Aura> auras = encounterCard.Auras;
				foreach (Aura current2 in auras)
				{
					creature.Auras.Add(current2.Copy());
				}
				List<CreaturePower> creaturePowers = encounterCard.CreaturePowers;
				foreach (CreaturePower current3 in creaturePowers)
				{
					creature.CreaturePowers.Add(current3.Copy());
				}
				List<DamageModifier> damageModifiers = encounterCard.DamageModifiers;
				foreach (DamageModifier current4 in damageModifiers)
				{
					creature.DamageModifiers.Add(current4.Copy());
				}
				Guid iD = this.fCreature.ID;
				CreatureHelper.CopyFields(creature, this.fCreature);
				this.fCreature.ID = iD;
				this.find_sample_powers();
				this.update_view();
			}
		}

		private void create_hybrid()
		{
			CreatureMultipleSelectForm creatureMultipleSelectForm = new CreatureMultipleSelectForm();
			if (creatureMultipleSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.splice(creatureMultipleSelectForm.SelectedCreatures);
				this.find_sample_powers();
				this.update_view();
			}
		}

		private void splice(List<ICreature> genepool)
		{
			if (this.fCreature is Creature)
			{
				this.fCreature = new Creature();
			}
			if (this.fCreature is CustomCreature)
			{
				this.fCreature = new CustomCreature();
			}
			if (this.fCreature is NPC)
			{
				this.fCreature = new NPC();
			}
			int num = 2147483647;
			int num2 = -2147483648;
			using (List<ICreature>.Enumerator enumerator = genepool.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Creature creature = (Creature)enumerator.Current;
					num = Math.Min(num, creature.Level);
					num2 = Math.Max(num2, creature.Level);
				}
			}
			this.fCreature.Level = Session.Random.Next(num, num2 + 1);
			int index = Session.Random.Next(genepool.Count);
			this.fCreature.Role = genepool[index].Role.Copy();
			int index2 = Session.Random.Next(genepool.Count);
			this.fCreature.Size = genepool[index2].Size;
			int index3 = Session.Random.Next(genepool.Count);
			this.fCreature.Origin = genepool[index3].Origin;
			int index4 = Session.Random.Next(genepool.Count);
			this.fCreature.Type = genepool[index4].Type;
			string text = this.find_common_name(genepool);
			if (text == "")
			{
				this.fCreature.Name = this.generate_name();
			}
			else
			{
				this.fCreature.Name = text;
				string text2 = this.find_description(genepool);
				if (text2 != "")
				{
					ICreature expr_1B6 = this.fCreature;
					expr_1B6.Name = expr_1B6.Name + " " + text2;
				}
				else
				{
					this.fCreature.Name = "New " + this.fCreature.Name;
				}
			}
			this.fCreature.Category = "";
			int index5 = Session.Random.Next(genepool.Count);
			this.fCreature.Keywords = genepool[index5].Keywords;
			int index6 = Session.Random.Next(genepool.Count);
			this.fCreature.Strength.Score = genepool[index6].Strength.Score;
			this.fCreature.Constitution.Score = genepool[index6].Constitution.Score;
			this.fCreature.Dexterity.Score = genepool[index6].Dexterity.Score;
			this.fCreature.Intelligence.Score = genepool[index6].Intelligence.Score;
			this.fCreature.Wisdom.Score = genepool[index6].Wisdom.Score;
			this.fCreature.Charisma.Score = genepool[index6].Charisma.Score;
			int index7 = Session.Random.Next(genepool.Count);
			int num3 = Statistics.AC(genepool[index7].Level, genepool[index7].Role);
			int num4 = Statistics.NAD(genepool[index7].Level, genepool[index7].Role);
			int aC = genepool[index7].AC;
			int fortitude = genepool[index7].Fortitude;
			int reflex = genepool[index7].Reflex;
			int will = genepool[index7].Will;
			int num5 = aC - num3;
			int num6 = fortitude - num4;
			int num7 = reflex - num4;
			int num8 = will - num4;
			this.fCreature.AC = Statistics.AC(this.fCreature.Level, this.fCreature.Role) + num5;
			this.fCreature.Fortitude = Statistics.NAD(this.fCreature.Level, this.fCreature.Role) + num6;
			this.fCreature.Reflex = Statistics.NAD(this.fCreature.Level, this.fCreature.Role) + num7;
			this.fCreature.Will = Statistics.NAD(this.fCreature.Level, this.fCreature.Role) + num8;
			if (this.fCreature.Role is ComplexRole)
			{
				List<ICreature> list = new List<ICreature>();
				foreach (ICreature current in genepool)
				{
					if (current.Role is ComplexRole)
					{
						list.Add(current);
					}
				}
				int index8 = Session.Random.Next(list.Count);
				int num9 = Statistics.HP(list[index8].Level, list[index8].Role as ComplexRole, list[index8].Constitution.Score);
				int hP = list[index8].HP;
				int num10 = hP - num9;
				this.fCreature.HP = Statistics.HP(this.fCreature.Level, this.fCreature.Role as ComplexRole, this.fCreature.Constitution.Score) + num10;
			}
			int index9 = Session.Random.Next(genepool.Count);
			int num11 = Statistics.Initiative(genepool[index9].Level, genepool[index9].Role);
			int initiative = genepool[index9].Initiative;
			int num12 = initiative - num11;
			this.fCreature.Initiative = Statistics.Initiative(this.fCreature.Level, this.fCreature.Role) + num12;
			int index10 = Session.Random.Next(genepool.Count);
			this.fCreature.Languages = genepool[index10].Languages;
			int index11 = Session.Random.Next(genepool.Count);
			this.fCreature.Movement = genepool[index11].Movement;
			int index12 = Session.Random.Next(genepool.Count);
			this.fCreature.Senses = genepool[index12].Senses;
			int index13 = Session.Random.Next(genepool.Count);
			this.fCreature.DamageModifiers.Clear();
			foreach (DamageModifier current2 in genepool[index13].DamageModifiers)
			{
				this.fCreature.DamageModifiers.Add(current2.Copy());
			}
			this.fCreature.Resist = genepool[index13].Resist;
			this.fCreature.Vulnerable = genepool[index13].Vulnerable;
			this.fCreature.Immune = genepool[index13].Immune;
			int index14 = Session.Random.Next(genepool.Count);
			this.fCreature.Auras.Clear();
			foreach (Aura current3 in genepool[index14].Auras)
			{
				this.fCreature.Auras.Add(current3.Copy());
			}
			int index15 = Session.Random.Next(genepool.Count);
			this.fCreature.Regeneration = genepool[index15].Regeneration;
			Dictionary<CreaturePowerCategory, List<CreaturePower>> dictionary = new Dictionary<CreaturePowerCategory, List<CreaturePower>>();
			Dictionary<Guid, string> dictionary2 = new Dictionary<Guid, string>();
			Dictionary<Guid, string> dictionary3 = new Dictionary<Guid, string>();
			Array values = Enum.GetValues(typeof(CreaturePowerCategory));
			foreach (CreaturePowerCategory key in values)
			{
				dictionary[key] = new List<CreaturePower>();
			}
			foreach (ICreature current4 in genepool)
			{
				foreach (CreaturePower current5 in current4.CreaturePowers)
				{
					dictionary[current5.Category].Add(current5);
					dictionary2[current5.ID] = current4.Name;
					dictionary3[current5.ID] = current4.Name;
				}
			}
			this.fCreature.CreaturePowers.Clear();
			foreach (CreaturePowerCategory key2 in values)
			{
				if (dictionary[key2].Count != 0)
				{
					int num13 = dictionary[key2].Count / genepool.Count;
					if (Session.Random.Next(6) == 0)
					{
						num13++;
					}
					for (int num14 = 0; num14 != num13; num14++)
					{
						int index16 = Session.Random.Next(dictionary[key2].Count);
						CreaturePower creaturePower = dictionary[key2][index16];
						string original_name = dictionary2[creaturePower.ID];
						string original_category = dictionary3[creaturePower.ID];
						creaturePower = this.replace_name(creaturePower, original_name, original_category, this.fCreature.Name);
						this.fCreature.CreaturePowers.Add(creaturePower);
					}
				}
			}
			int index17 = Session.Random.Next(genepool.Count);
			this.fCreature.Skills = genepool[index17].Skills;
			int index18 = Session.Random.Next(genepool.Count);
			this.fCreature.Alignment = genepool[index18].Alignment;
			this.fCreature.Tactics = "";
			this.fCreature.Equipment = "";
			this.fCreature.Details = "";
			this.fCreature.Image = null;
		}

		private CreaturePower alter_power_level(CreaturePower power, int original_level, int new_level)
		{
			CreaturePower creaturePower = power.Copy();
			int delta = new_level - original_level;
			CreatureHelper.AdjustPowerLevel(creaturePower, delta);
			if (original_level <= 15 && new_level > 15)
			{
				creaturePower.Details = creaturePower.Details.Replace("Dazed", "Stunned");
				creaturePower.Details = creaturePower.Details.Replace("dazed", "stunned");
				creaturePower.Details = creaturePower.Details.Replace("Slowed", "Immobilised");
				creaturePower.Details = creaturePower.Details.Replace("slowed", "immobilised");
			}
			if (original_level > 15 && new_level <= 15)
			{
				creaturePower.Details = creaturePower.Details.Replace("Stunned", "Dazed");
				creaturePower.Details = creaturePower.Details.Replace("Stunned", "Dazed");
				creaturePower.Details = creaturePower.Details.Replace("Immobilised", "Immobilized");
				creaturePower.Details = creaturePower.Details.Replace("immobilised", "immobilized");
				creaturePower.Details = creaturePower.Details.Replace("Immobilized", "Slowed");
				creaturePower.Details = creaturePower.Details.Replace("immobilized", "slowed");
			}
			return creaturePower;
		}

		private string find_common_name(List<ICreature> creatures)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			for (int num = 0; num != creatures.Count; num++)
			{
				string name = creatures[num].Name;
				for (int num2 = num + 1; num2 != creatures.Count; num2++)
				{
					string name2 = creatures[num2].Name;
					string text = StringHelper.LongestCommonToken(name, name2);
					if (text.Length >= 3)
					{
						if (!dictionary.ContainsKey(text))
						{
							dictionary[text] = 0;
						}
						Dictionary<string, int> dictionary2;
						string key;
						(dictionary2 = dictionary)[key = text] = dictionary2[key] + 1;
					}
				}
			}
			string text2 = "";
			if (dictionary.Keys.Count != 0)
			{
				foreach (string current in dictionary.Keys)
				{
					int num3 = dictionary.ContainsKey(text2) ? dictionary[text2] : 0;
					if (dictionary[current] > num3)
					{
						text2 = current;
					}
				}
			}
			return text2;
		}

		private void FileExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Title = "Export Creature";
			saveFileDialog.FileName = this.fCreature.Name;
			saveFileDialog.Filter = Program.CreatureFilter;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				Creature obj = new Creature(this.fCreature);
				if (!Serialisation<Masterplan.Data.Creature>.Save(saveFileDialog.FileName, obj, SerialisationMode.Binary))
				{
					string text = "The creature could not be exported.";
					MessageBox.Show(text, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(CreatureBuilderForm));
			this.Toolbar = new ToolStrip();
			this.FileMenu = new ToolStripDropDownButton();
			this.FileExport = new ToolStripMenuItem();
			this.OptionsMenu = new ToolStripDropDownButton();
			this.OptionsImport = new ToolStripMenuItem();
			this.OptionsVariant = new ToolStripMenuItem();
			this.OptionsRandom = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.OptionsEntry = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.OptionsPowerBrowser = new ToolStripMenuItem();
			this.LevelDownBtn = new ToolStripButton();
			this.LevelUpBtn = new ToolStripButton();
			this.LevelLbl = new ToolStripLabel();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.AdviceBtn = new ToolStripButton();
			this.PowersBtn = new ToolStripButton();
			this.BtnPnl = new Panel();
			this.CancelBtn = new Button();
			this.OKBtn = new Button();
			this.Pages = new TabControl();
			this.StatBlockPage = new TabPage();
			this.StatBlockBrowser = new WebBrowser();
			this.PicturePage = new TabPage();
			this.PictureToolbar = new ToolStrip();
			this.PictureBrowseBtn = new ToolStripButton();
			this.PicturePasteBtn = new ToolStripButton();
			this.PictureClearBtn = new ToolStripButton();
			this.PortraitBox = new PictureBox();
			this.EntryPage = new TabPage();
			this.EntryBrowser = new WebBrowser();
			this.Toolbar.SuspendLayout();
			this.BtnPnl.SuspendLayout();
			this.Pages.SuspendLayout();
			this.StatBlockPage.SuspendLayout();
			this.PicturePage.SuspendLayout();
			this.PictureToolbar.SuspendLayout();
			((ISupportInitialize)this.PortraitBox).BeginInit();
			this.EntryPage.SuspendLayout();
			base.SuspendLayout();
			this.Toolbar.Items.AddRange(new ToolStripItem[]
			{
				this.FileMenu,
				this.OptionsMenu,
				this.LevelDownBtn,
				this.LevelUpBtn,
				this.LevelLbl,
				this.toolStripSeparator3,
				this.AdviceBtn,
				this.PowersBtn
			});
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new Size(684, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.FileMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FileMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.FileExport
			});
			this.FileMenu.Image = (Image)componentResourceManager.GetObject("FileMenu.Image");
			this.FileMenu.ImageTransparentColor = Color.Magenta;
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new Size(38, 22);
			this.FileMenu.Text = "File";
			this.FileExport.Name = "FileExport";
			this.FileExport.Size = new Size(152, 22);
			this.FileExport.Text = "Export...";
			this.FileExport.Click += new EventHandler(this.FileExport_Click);
			this.OptionsMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.OptionsMenu.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.OptionsImport,
				this.OptionsVariant,
				this.OptionsRandom,
				this.toolStripSeparator1,
				this.OptionsEntry,
				this.toolStripSeparator2,
				this.OptionsPowerBrowser
			});
			this.OptionsMenu.Image = (Image)componentResourceManager.GetObject("OptionsMenu.Image");
			this.OptionsMenu.ImageTransparentColor = Color.Magenta;
			this.OptionsMenu.Name = "OptionsMenu";
			this.OptionsMenu.Size = new Size(62, 22);
			this.OptionsMenu.Text = "Options";
			this.OptionsImport.Name = "OptionsImport";
			this.OptionsImport.Size = new Size(242, 22);
			this.OptionsImport.Text = "Import from Adventure Tools...";
			this.OptionsImport.Click += new EventHandler(this.OptionsImport_Click);
			this.OptionsVariant.Name = "OptionsVariant";
			this.OptionsVariant.Size = new Size(242, 22);
			this.OptionsVariant.Text = "Copy an Existing Creature...";
			this.OptionsVariant.Click += new EventHandler(this.OptionsVariant_Click);
			this.OptionsRandom.Name = "OptionsRandom";
			this.OptionsRandom.Size = new Size(242, 22);
			this.OptionsRandom.Text = "Generate a Random Creature...";
			this.OptionsRandom.Click += new EventHandler(this.OptionsRandom_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(239, 6);
			this.OptionsEntry.Name = "OptionsEntry";
			this.OptionsEntry.Size = new Size(242, 22);
			this.OptionsEntry.Text = "Create / Edit Encyclopedia Entry";
			this.OptionsEntry.Click += new EventHandler(this.OptionsEntry_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(239, 6);
			this.OptionsPowerBrowser.Name = "OptionsPowerBrowser";
			this.OptionsPowerBrowser.Size = new Size(242, 22);
			this.OptionsPowerBrowser.Text = "Power Browser";
			this.OptionsPowerBrowser.Click += new EventHandler(this.OptionsPowerBrowser_Click);
			this.LevelDownBtn.Alignment = ToolStripItemAlignment.Right;
			this.LevelDownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LevelDownBtn.Image = (Image)componentResourceManager.GetObject("LevelDownBtn.Image");
			this.LevelDownBtn.ImageTransparentColor = Color.Magenta;
			this.LevelDownBtn.Name = "LevelDownBtn";
			this.LevelDownBtn.Size = new Size(23, 22);
			this.LevelDownBtn.Text = "-";
			this.LevelDownBtn.ToolTipText = "Level Down";
			this.LevelDownBtn.Click += new EventHandler(this.LevelDownBtn_Click);
			this.LevelUpBtn.Alignment = ToolStripItemAlignment.Right;
			this.LevelUpBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LevelUpBtn.Image = (Image)componentResourceManager.GetObject("LevelUpBtn.Image");
			this.LevelUpBtn.ImageTransparentColor = Color.Magenta;
			this.LevelUpBtn.Name = "LevelUpBtn";
			this.LevelUpBtn.Size = new Size(23, 22);
			this.LevelUpBtn.Text = "+";
			this.LevelUpBtn.ToolTipText = "Level Up";
			this.LevelUpBtn.Click += new EventHandler(this.LevelUpBtn_Click);
			this.LevelLbl.Alignment = ToolStripItemAlignment.Right;
			this.LevelLbl.Name = "LevelLbl";
			this.LevelLbl.Size = new Size(37, 22);
			this.LevelLbl.Text = "Level:";
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new Size(6, 25);
			this.AdviceBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AdviceBtn.Image = (Image)componentResourceManager.GetObject("AdviceBtn.Image");
			this.AdviceBtn.ImageTransparentColor = Color.Magenta;
			this.AdviceBtn.Name = "AdviceBtn";
			this.AdviceBtn.Size = new Size(47, 22);
			this.AdviceBtn.Text = "Advice";
			this.AdviceBtn.Click += new EventHandler(this.AdviceBtn_Click);
			this.PowersBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PowersBtn.Image = (Image)componentResourceManager.GetObject("PowersBtn.Image");
			this.PowersBtn.ImageTransparentColor = Color.Magenta;
			this.PowersBtn.Name = "PowersBtn";
			this.PowersBtn.Size = new Size(49, 22);
			this.PowersBtn.Text = "Powers";
			this.PowersBtn.Click += new EventHandler(this.PowersBtn_Click);
			this.BtnPnl.Controls.Add(this.CancelBtn);
			this.BtnPnl.Controls.Add(this.OKBtn);
			this.BtnPnl.Dock = DockStyle.Bottom;
			this.BtnPnl.Location = new Point(0, 443);
			this.BtnPnl.Name = "BtnPnl";
			this.BtnPnl.Size = new Size(684, 35);
			this.BtnPnl.TabIndex = 2;
			this.CancelBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.CancelBtn.DialogResult = DialogResult.Cancel;
			this.CancelBtn.Location = new Point(597, 6);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new Size(75, 23);
			this.CancelBtn.TabIndex = 1;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.OKBtn.DialogResult = DialogResult.OK;
			this.OKBtn.Location = new Point(516, 6);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.Pages.Controls.Add(this.StatBlockPage);
			this.Pages.Controls.Add(this.PicturePage);
			this.Pages.Controls.Add(this.EntryPage);
			this.Pages.Dock = DockStyle.Fill;
			this.Pages.Location = new Point(0, 25);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new Size(684, 418);
			this.Pages.TabIndex = 3;
			this.StatBlockPage.Controls.Add(this.StatBlockBrowser);
			this.StatBlockPage.Location = new Point(4, 22);
			this.StatBlockPage.Name = "StatBlockPage";
			this.StatBlockPage.Padding = new Padding(3);
			this.StatBlockPage.Size = new Size(676, 392);
			this.StatBlockPage.TabIndex = 0;
			this.StatBlockPage.Text = "Stat Block";
			this.StatBlockPage.UseVisualStyleBackColor = true;
			this.StatBlockBrowser.AllowWebBrowserDrop = false;
			this.StatBlockBrowser.Dock = DockStyle.Fill;
			this.StatBlockBrowser.IsWebBrowserContextMenuEnabled = false;
			this.StatBlockBrowser.Location = new Point(3, 3);
			this.StatBlockBrowser.MinimumSize = new Size(20, 20);
			this.StatBlockBrowser.Name = "StatBlockBrowser";
			this.StatBlockBrowser.ScriptErrorsSuppressed = true;
			this.StatBlockBrowser.Size = new Size(670, 386);
			this.StatBlockBrowser.TabIndex = 2;
			this.StatBlockBrowser.WebBrowserShortcutsEnabled = false;
			this.StatBlockBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.Browser_Navigating);
			this.PicturePage.Controls.Add(this.PictureToolbar);
			this.PicturePage.Controls.Add(this.PortraitBox);
			this.PicturePage.Location = new Point(4, 22);
			this.PicturePage.Name = "PicturePage";
			this.PicturePage.Padding = new Padding(3);
			this.PicturePage.Size = new Size(676, 392);
			this.PicturePage.TabIndex = 1;
			this.PicturePage.Text = "Picture";
			this.PicturePage.UseVisualStyleBackColor = true;
			this.PictureToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.PictureBrowseBtn,
				this.PicturePasteBtn,
				this.PictureClearBtn
			});
			this.PictureToolbar.Location = new Point(3, 3);
			this.PictureToolbar.Name = "PictureToolbar";
			this.PictureToolbar.Size = new Size(670, 25);
			this.PictureToolbar.TabIndex = 6;
			this.PictureToolbar.Text = "toolStrip1";
			this.PictureBrowseBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PictureBrowseBtn.Image = (Image)componentResourceManager.GetObject("PictureBrowseBtn.Image");
			this.PictureBrowseBtn.ImageTransparentColor = Color.Magenta;
			this.PictureBrowseBtn.Name = "PictureBrowseBtn";
			this.PictureBrowseBtn.Size = new Size(49, 22);
			this.PictureBrowseBtn.Text = "Browse";
			this.PictureBrowseBtn.Click += new EventHandler(this.PictureBrowseBtn_Click);
			this.PicturePasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PicturePasteBtn.Image = (Image)componentResourceManager.GetObject("PicturePasteBtn.Image");
			this.PicturePasteBtn.ImageTransparentColor = Color.Magenta;
			this.PicturePasteBtn.Name = "PicturePasteBtn";
			this.PicturePasteBtn.Size = new Size(39, 22);
			this.PicturePasteBtn.Text = "Paste";
			this.PicturePasteBtn.Click += new EventHandler(this.PicturePasteBtn_Click);
			this.PictureClearBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PictureClearBtn.Image = (Image)componentResourceManager.GetObject("PictureClearBtn.Image");
			this.PictureClearBtn.ImageTransparentColor = Color.Magenta;
			this.PictureClearBtn.Name = "PictureClearBtn";
			this.PictureClearBtn.Size = new Size(38, 22);
			this.PictureClearBtn.Text = "Clear";
			this.PictureClearBtn.Click += new EventHandler(this.PictureClearBtn_Click);
			this.PortraitBox.BackColor = SystemColors.ControlLight;
			this.PortraitBox.BorderStyle = BorderStyle.FixedSingle;
			this.PortraitBox.Dock = DockStyle.Fill;
			this.PortraitBox.Location = new Point(3, 3);
			this.PortraitBox.Name = "PortraitBox";
			this.PortraitBox.Size = new Size(670, 386);
			this.PortraitBox.SizeMode = PictureBoxSizeMode.Zoom;
			this.PortraitBox.TabIndex = 4;
			this.PortraitBox.TabStop = false;
			this.PortraitBox.DoubleClick += new EventHandler(this.PictureBrowseBtn_Click);
			this.EntryPage.Controls.Add(this.EntryBrowser);
			this.EntryPage.Location = new Point(4, 22);
			this.EntryPage.Name = "EntryPage";
			this.EntryPage.Padding = new Padding(3);
			this.EntryPage.Size = new Size(676, 392);
			this.EntryPage.TabIndex = 2;
			this.EntryPage.Text = "Encyclopedia Entry";
			this.EntryPage.UseVisualStyleBackColor = true;
			this.EntryBrowser.AllowWebBrowserDrop = false;
			this.EntryBrowser.Dock = DockStyle.Fill;
			this.EntryBrowser.IsWebBrowserContextMenuEnabled = false;
			this.EntryBrowser.Location = new Point(3, 3);
			this.EntryBrowser.MinimumSize = new Size(20, 20);
			this.EntryBrowser.Name = "EntryBrowser";
			this.EntryBrowser.ScriptErrorsSuppressed = true;
			this.EntryBrowser.Size = new Size(670, 386);
			this.EntryBrowser.TabIndex = 0;
			this.EntryBrowser.WebBrowserShortcutsEnabled = false;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new Size(684, 478);
			base.Controls.Add(this.Pages);
			base.Controls.Add(this.BtnPnl);
			base.Controls.Add(this.Toolbar);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CreatureBuilderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Creature Builder";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.BtnPnl.ResumeLayout(false);
			this.Pages.ResumeLayout(false);
			this.StatBlockPage.ResumeLayout(false);
			this.PicturePage.ResumeLayout(false);
			this.PicturePage.PerformLayout();
			this.PictureToolbar.ResumeLayout(false);
			this.PictureToolbar.PerformLayout();
			((ISupportInitialize)this.PortraitBox).EndInit();
			this.EntryPage.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
