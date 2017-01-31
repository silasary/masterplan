using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class FilterPanel : UserControl
	{
		private ListMode fMode;

		private int fPartyLevel;

		private string[] fNameTokens;

		private string[] fCatTokens;

		private string[] fKeyTokens;

		private bool fSuppressEvents;

		private IContainer components;

		private CheckBox FilterLevelToggle;

		private CheckBox FilterModToggle;

		private ComboBox FilterModBox;

		private CheckBox FilterNameToggle;

		private CheckBox FilterCatToggle;

		private TextBox FilterKeywordBox;

		private CheckBox FilterKeywordToggle;

		private CheckBox FilterTypeToggle;

		private ComboBox FilterTypeBox;

		private CheckBox FilterOriginToggle;

		private ComboBox FilterOriginBox;

		private TextBox FilterCatBox;

		private CheckBox FilterRoleToggle;

		private ComboBox FilterRoleBox;

		private TextBox FilterNameBox;

		private Panel FilterPnl;

		private NumericUpDown LevelToBox;

		private NumericUpDown LevelFromBox;

		private Label ToLbl;

		private Label FromLbl;

		private CheckBox FilterLevelAppropriateToggle;

		private ToolStripStatusLabel InfoLbl;

		private ToolStripStatusLabel EditLbl;

		private StatusStrip Statusbar;

		private ComboBox FilterSourceBox;

		private CheckBox FilterSourceToggle;

        public event EventHandler FilterChanged;

		public ListMode Mode
		{
			get
			{
				return this.fMode;
			}
			set
			{
				this.fMode = value;
				this.update_allowed_filters();
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
			}
		}

		public FilterPanel()
		{
			this.InitializeComponent();
			Array values = Enum.GetValues(typeof(RoleType));
			foreach (RoleType roleType in values)
			{
				this.FilterRoleBox.Items.Add(roleType);
			}
			this.FilterRoleBox.SelectedIndex = 0;
			this.FilterModBox.Items.Add("Standard");
			this.FilterModBox.Items.Add("Elite");
			this.FilterModBox.Items.Add("Solo");
			this.FilterModBox.Items.Add("Minion");
			this.FilterModBox.SelectedIndex = 0;
			Array values2 = Enum.GetValues(typeof(CreatureOrigin));
			foreach (CreatureOrigin creatureOrigin in values2)
			{
				this.FilterOriginBox.Items.Add(creatureOrigin);
			}
			if (this.FilterOriginBox.Items.Count != 0)
			{
				this.FilterOriginBox.SelectedIndex = 0;
			}
			Array values3 = Enum.GetValues(typeof(CreatureType));
			foreach (CreatureType creatureType in values3)
			{
				this.FilterTypeBox.Items.Add(creatureType);
			}
			if (this.FilterTypeBox.Items.Count != 0)
			{
				this.FilterTypeBox.SelectedIndex = 0;
			}
			foreach (Library current in Session.Libraries)
			{
				this.FilterSourceBox.Items.Add(current);
			}
			if (this.FilterSourceBox.Items.Count != 0)
			{
				this.FilterSourceBox.SelectedIndex = 0;
			}
			this.update_allowed_filters();
			this.update_option_state();
			this.open_close_editor(false);
		}

		public bool AllowItem(object obj, out Difficulty diff)
		{
			diff = Difficulty.Moderate;
			bool result;
			if (obj is ICreature)
			{
				ICreature creature = obj as ICreature;
				bool flag = false;
				diff = AI.GetThreatDifficulty(creature.Level, this.fPartyLevel);
				if (diff == Difficulty.Trivial || diff == Difficulty.Extreme)
				{
					flag = true;
				}
				if (flag && this.FilterLevelAppropriateToggle.Checked)
				{
					return false;
				}
				if (this.FilterNameToggle.Checked && this.fNameTokens != null)
				{
					string text = creature.Name.ToLower();
					string[] array = this.fNameTokens;
					for (int i = 0; i < array.Length; i++)
					{
						string value = array[i];
						if (!text.Contains(value))
						{
							result = false;
							return result;
						}
					}
				}
				if (this.FilterCatToggle.Checked && this.fCatTokens != null)
				{
					string text2 = creature.Category.ToLower();
					string[] array2 = this.fCatTokens;
					for (int j = 0; j < array2.Length; j++)
					{
						string value2 = array2[j];
						if (!text2.Contains(value2))
						{
							result = false;
							return result;
						}
					}
				}
				if (this.FilterRoleToggle.Checked)
				{
					RoleType roleType = (RoleType)this.FilterRoleBox.SelectedItem;
					if (creature.Role is ComplexRole)
					{
						ComplexRole complexRole = creature.Role as ComplexRole;
						if (complexRole.Type != roleType)
						{
							return false;
						}
					}
					if (creature.Role is Minion)
					{
						Minion minion = creature.Role as Minion;
						if (!minion.HasRole || minion.Type != roleType)
						{
							return false;
						}
					}
				}
				if (this.FilterModToggle.Checked)
				{
					RoleFlag roleFlag = RoleFlag.Standard;
					bool flag2 = false;
					//this.FilterModBox.Text == "Standard";
					if (this.FilterModBox.Text == "Elite")
					{
						roleFlag = RoleFlag.Elite;
					}
					if (this.FilterModBox.Text == "Solo")
					{
						roleFlag = RoleFlag.Solo;
					}
					if (this.FilterModBox.Text == "Minion")
					{
						flag2 = true;
					}
					ComplexRole complexRole2 = creature.Role as ComplexRole;
					if (complexRole2 != null)
					{
						if (flag2)
						{
							return false;
						}
						if (roleFlag != complexRole2.Flag)
						{
							return false;
						}
					}
					Minion minion2 = creature.Role as Minion;
					if (minion2 != null && !flag2)
					{
						return false;
					}
				}
				if (this.FilterOriginToggle.Checked)
				{
					CreatureOrigin creatureOrigin = (CreatureOrigin)this.FilterOriginBox.SelectedItem;
					if (creature.Origin != creatureOrigin)
					{
						return false;
					}
				}
				if (this.FilterTypeToggle.Checked)
				{
					CreatureType creatureType = (CreatureType)this.FilterTypeBox.SelectedItem;
					if (creature.Type != creatureType)
					{
						return false;
					}
				}
				if (this.FilterKeywordToggle.Checked && this.fKeyTokens != null)
				{
					string text3 = (creature.Keywords != null) ? creature.Keywords.ToLower() : "";
					string[] array3 = this.fKeyTokens;
					for (int k = 0; k < array3.Length; k++)
					{
						string value3 = array3[k];
						if (!text3.Contains(value3))
						{
							result = false;
							return result;
						}
					}
				}
				if (this.FilterLevelToggle.Checked && (creature.Level < this.LevelFromBox.Value || creature.Level > this.LevelToBox.Value))
				{
					return false;
				}
				if (this.FilterSourceToggle.Checked)
				{
					Creature creature2 = creature as Creature;
					if (creature2 == null)
					{
						return false;
					}
					Library library = this.FilterSourceBox.SelectedItem as Library;
					if (!library.Creatures.Contains(creature2))
					{
						return false;
					}
				}
				return true;
			}
			else
			{
				if (obj is CreatureTemplate)
				{
					CreatureTemplate creatureTemplate = obj as CreatureTemplate;
					if (this.FilterNameToggle.Checked && this.fNameTokens != null)
					{
						string text4 = creatureTemplate.Name.ToLower();
						string[] array4 = this.fNameTokens;
						for (int i = 0; i < array4.Length; i++)
						{
							string value4 = array4[i];
							if (!text4.Contains(value4))
							{
								result = false;
								return result;
							}
						}
					}
					if (this.FilterCatToggle.Checked)
					{
						string[] arg_3E1_0 = this.fCatTokens;
					}
					if (this.FilterRoleToggle.Checked)
					{
						RoleType roleType2 = (RoleType)this.FilterRoleBox.SelectedItem;
						if (creatureTemplate.Role != roleType2)
						{
							return false;
						}
					}
					bool arg_419_0 = this.FilterOriginToggle.Checked;
					bool arg_425_0 = this.FilterTypeToggle.Checked;
					if (this.FilterKeywordToggle.Checked)
					{
						string[] arg_439_0 = this.fKeyTokens;
					}
					if (this.FilterSourceToggle.Checked)
					{
						Library library2 = this.FilterSourceBox.SelectedItem as Library;
						if (!library2.Templates.Contains(creatureTemplate))
						{
							return false;
						}
					}
					return true;
				}
				if (obj is NPC)
				{
					NPC nPC = obj as NPC;
					bool flag3 = false;
					diff = AI.GetThreatDifficulty(nPC.Level, this.fPartyLevel);
					if (diff == Difficulty.Trivial || diff == Difficulty.Extreme)
					{
						flag3 = true;
					}
					if (flag3 && this.FilterLevelAppropriateToggle.Checked)
					{
						return false;
					}
					if (this.FilterNameToggle.Checked && this.fNameTokens != null)
					{
						string text5 = nPC.Name.ToLower();
						string[] array = this.fNameTokens;
						for (int i = 0; i < array.Length; i++)
						{
							string value5 = array[i];
							if (!text5.Contains(value5))
							{
								result = false;
								return result;
							}
						}
					}
					if (this.FilterCatToggle.Checked && this.fCatTokens != null)
					{
						string text6 = nPC.Category.ToLower();
						string[] array = this.fCatTokens;
						for (int i = 0; i < array.Length; i++)
						{
							string value6 = array[i];
							if (!text6.Contains(value6))
							{
								result = false;
								return result;
							}
						}
					}
					if (this.FilterRoleToggle.Checked)
					{
						RoleType roleType3 = (RoleType)this.FilterRoleBox.SelectedItem;
						if (nPC.Role is ComplexRole)
						{
							ComplexRole complexRole3 = nPC.Role as ComplexRole;
							if (complexRole3.Type != roleType3)
							{
								return false;
							}
						}
						if (nPC.Role is Minion)
						{
							Minion minion3 = nPC.Role as Minion;
							if (!minion3.HasRole || minion3.Type != roleType3)
							{
								return false;
							}
						}
					}
					if (this.FilterOriginToggle.Checked)
					{
						CreatureOrigin creatureOrigin2 = (CreatureOrigin)this.FilterOriginBox.SelectedItem;
						if (nPC.Origin != creatureOrigin2)
						{
							return false;
						}
					}
					if (this.FilterTypeToggle.Checked)
					{
						CreatureType creatureType2 = (CreatureType)this.FilterTypeBox.SelectedItem;
						if (nPC.Type != creatureType2)
						{
							return false;
						}
					}
					if (this.FilterKeywordToggle.Checked && this.fKeyTokens != null)
					{
						string text7 = (nPC.Keywords != null) ? nPC.Keywords.ToLower() : "";
						string[] array = this.fKeyTokens;
						for (int i = 0; i < array.Length; i++)
						{
							string value7 = array[i];
							if (!text7.Contains(value7))
							{
								result = false;
								return result;
							}
						}
					}
					return !this.FilterLevelToggle.Checked || (!(nPC.Level < this.LevelFromBox.Value) && !(nPC.Level > this.LevelToBox.Value));
				}
				else if (obj is Trap)
				{
					Trap trap = obj as Trap;
					bool flag4 = false;
					diff = AI.GetThreatDifficulty(trap.Level, this.fPartyLevel);
					if (diff == Difficulty.Trivial || diff == Difficulty.Extreme)
					{
						flag4 = true;
					}
					if (flag4 && this.FilterLevelAppropriateToggle.Checked)
					{
						return false;
					}
					if (this.FilterNameToggle.Checked && this.fNameTokens != null)
					{
						string text8 = trap.Name.ToLower();
						string[] array = this.fNameTokens;
						for (int i = 0; i < array.Length; i++)
						{
							string value8 = array[i];
							if (!text8.Contains(value8))
							{
								result = false;
								return result;
							}
						}
					}
					if (this.FilterCatToggle.Checked)
					{
						string[] arg_7A3_0 = this.fCatTokens;
					}
					if (this.FilterRoleToggle.Checked)
					{
						RoleType roleType4 = (RoleType)this.FilterRoleBox.SelectedItem;
						if (trap.Role is ComplexRole)
						{
							ComplexRole complexRole4 = trap.Role as ComplexRole;
							if (complexRole4.Type != roleType4)
							{
								return false;
							}
						}
						if (trap.Role is Minion)
						{
							Minion minion4 = trap.Role as Minion;
							if (!minion4.HasRole || minion4.Type != roleType4)
							{
								return false;
							}
						}
					}
					if (this.FilterModToggle.Checked)
					{
						RoleFlag roleFlag2 = RoleFlag.Standard;
						bool flag5 = false;
						//this.FilterModBox.Text == "Standard";
						if (this.FilterModBox.Text == "Elite")
						{
							roleFlag2 = RoleFlag.Elite;
						}
						if (this.FilterModBox.Text == "Solo")
						{
							roleFlag2 = RoleFlag.Solo;
						}
						if (this.FilterModBox.Text == "Minion")
						{
							flag5 = true;
						}
						ComplexRole complexRole5 = trap.Role as ComplexRole;
						if (complexRole5 != null)
						{
							if (flag5)
							{
								return false;
							}
							if (roleFlag2 != complexRole5.Flag)
							{
								return false;
							}
						}
						Minion minion5 = trap.Role as Minion;
						if (minion5 != null && !flag5)
						{
							return false;
						}
					}
					bool arg_8E0_0 = this.FilterOriginToggle.Checked;
					bool arg_8EC_0 = this.FilterTypeToggle.Checked;
					if (this.FilterKeywordToggle.Checked)
					{
						string[] arg_900_0 = this.fKeyTokens;
					}
					if (this.FilterLevelToggle.Checked && (trap.Level < this.LevelFromBox.Value || trap.Level > this.LevelToBox.Value))
					{
						return false;
					}
					if (this.FilterSourceToggle.Checked)
					{
						Library library3 = this.FilterSourceBox.SelectedItem as Library;
						if (!library3.Traps.Contains(trap))
						{
							return false;
						}
					}
					return true;
				}
				else
				{
					if (obj is SkillChallenge)
					{
						SkillChallenge skillChallenge = obj as SkillChallenge;
						if (this.FilterNameToggle.Checked && this.fNameTokens != null)
						{
							string text9 = skillChallenge.Name.ToLower();
							string[] array = this.fNameTokens;
							for (int i = 0; i < array.Length; i++)
							{
								string value9 = array[i];
								if (!text9.Contains(value9))
								{
									result = false;
									return result;
								}
							}
						}
						if (this.FilterCatToggle.Checked)
						{
							string[] arg_9FD_0 = this.fCatTokens;
						}
						bool arg_A09_0 = this.FilterRoleToggle.Checked;
						bool arg_A15_0 = this.FilterOriginToggle.Checked;
						bool arg_A21_0 = this.FilterTypeToggle.Checked;
						if (this.FilterKeywordToggle.Checked)
						{
							string[] arg_A35_0 = this.fKeyTokens;
						}
						if (this.FilterSourceToggle.Checked)
						{
							Library library4 = this.FilterSourceBox.SelectedItem as Library;
							if (!library4.SkillChallenges.Contains(skillChallenge))
							{
								return false;
							}
						}
						return true;
					}
					return false;
				}
			}
			return result;
		}

		public void Collapse()
		{
			this.open_close_editor(false);
		}

		public void Expand()
		{
			this.open_close_editor(true);
		}

		public void FilterByPartyLevel()
		{
			this.FilterLevelAppropriateToggle.Checked = true;
			this.OnFilterChanged();
			if (!this.FilterPnl.Visible)
			{
				this.InfoLbl.Text = this.get_filter_string();
			}
		}

		public bool SetFilter(int level, IRole role)
		{
			this.fSuppressEvents = true;
			bool flag = false;
			if (level != 0)
			{
				this.FilterLevelToggle.Checked = true;
				this.LevelFromBox.Value = level;
				this.LevelToBox.Value = level;
				flag = true;
			}
			if (role != null)
			{
				if (role is ComplexRole)
				{
					ComplexRole complexRole = role as ComplexRole;
					this.FilterRoleToggle.Checked = true;
					this.FilterRoleBox.SelectedItem = complexRole.Type;
					this.FilterModToggle.Checked = true;
					this.FilterModBox.SelectedItem = complexRole.Flag.ToString();
					flag = true;
				}
				if (role is Minion)
				{
					Minion minion = role as Minion;
					if (minion.HasRole)
					{
						this.FilterRoleToggle.Checked = true;
						this.FilterRoleBox.SelectedItem = minion.Type;
					}
					this.FilterModToggle.Checked = true;
					this.FilterModBox.SelectedItem = "Minion";
					flag = true;
				}
			}
			this.fSuppressEvents = false;
			if (flag)
			{
				this.update_option_state();
				this.OnFilterChanged();
				if (!this.FilterPnl.Visible)
				{
					this.InfoLbl.Text = this.get_filter_string();
				}
			}
			return flag;
		}

		protected void OnFilterChanged()
		{
			if (this.fSuppressEvents)
			{
				return;
			}
			if (this.FilterChanged != null)
			{
				this.FilterChanged(this, new EventArgs());
			}
		}

		private void ToggleChanged(object sender, EventArgs e)
		{
			this.update_option_state();
			if (sender == this.FilterNameToggle && this.FilterNameBox.Text == "")
			{
				return;
			}
			if (sender == this.FilterCatToggle && this.FilterCatBox.Text == "")
			{
				return;
			}
			if (sender == this.FilterKeywordToggle && this.FilterKeywordBox.Text == "")
			{
				return;
			}
			if (sender == this.FilterLevelToggle)
			{
				this.FilterLevelAppropriateToggle.Checked = false;
			}
			if (sender == this.FilterLevelAppropriateToggle)
			{
				this.FilterLevelToggle.Checked = false;
			}
			this.OnFilterChanged();
		}

		private void TextOptionChanged(object sender, EventArgs e)
		{
			this.fNameTokens = this.FilterNameBox.Text.ToLower().Split(null);
			if (this.fNameTokens.Length == 0)
			{
				this.fNameTokens = null;
			}
			this.fCatTokens = this.FilterCatBox.Text.ToLower().Split(null);
			if (this.fCatTokens.Length == 0)
			{
				this.fCatTokens = null;
			}
			this.fKeyTokens = this.FilterKeywordBox.Text.ToLower().Split(null);
			if (this.fKeyTokens.Length == 0)
			{
				this.fKeyTokens = null;
			}
			this.OnFilterChanged();
		}

		private void DropdownOptionChanged(object sender, EventArgs e)
		{
			this.OnFilterChanged();
		}

		private void NumericOptionChanged(object sender, EventArgs e)
		{
			if (sender == this.LevelFromBox)
			{
				this.LevelToBox.Minimum = this.LevelFromBox.Value;
			}
			if (sender == this.LevelToBox)
			{
				this.LevelFromBox.Maximum = this.LevelToBox.Value;
			}
			this.OnFilterChanged();
		}

		private void EditLbl_Click(object sender, EventArgs e)
		{
			this.open_close_editor(!this.FilterPnl.Visible);
		}

		private void update_allowed_filters()
		{
			this.FilterCatToggle.Enabled = (this.fMode == ListMode.Creatures || this.fMode == ListMode.NPCs);
			this.FilterRoleToggle.Enabled = (this.fMode != ListMode.SkillChallenges);
			this.FilterModToggle.Enabled = (this.fMode == ListMode.Creatures || this.fMode == ListMode.Traps);
			this.FilterOriginToggle.Enabled = (this.fMode == ListMode.Creatures || this.fMode == ListMode.NPCs);
			this.FilterTypeToggle.Enabled = (this.fMode == ListMode.Creatures || this.fMode == ListMode.NPCs);
			this.FilterKeywordToggle.Enabled = (this.fMode == ListMode.Creatures || this.fMode == ListMode.NPCs);
			this.FilterLevelToggle.Enabled = (this.fMode == ListMode.Creatures || this.fMode == ListMode.NPCs || this.fMode == ListMode.Traps);
			this.FilterLevelAppropriateToggle.Enabled = (this.fPartyLevel != 0 && (this.fMode == ListMode.Creatures || this.fMode == ListMode.NPCs || this.fMode == ListMode.Traps));
			this.FilterSourceToggle.Enabled = (Session.Libraries.Count != 0 && (this.fMode == ListMode.Creatures || this.fMode == ListMode.Templates || this.fMode == ListMode.Traps || this.fMode == ListMode.SkillChallenges));
		}

		private void update_option_state()
		{
			this.FilterNameBox.Enabled = this.FilterNameToggle.Checked;
			this.FilterCatBox.Enabled = (this.FilterCatToggle.Enabled && this.FilterCatToggle.Checked);
			this.FilterRoleBox.Enabled = (this.FilterRoleToggle.Enabled && this.FilterRoleToggle.Checked);
			this.FilterModBox.Enabled = (this.FilterModToggle.Enabled && this.FilterModToggle.Checked);
			this.FilterOriginBox.Enabled = (this.FilterOriginToggle.Enabled && this.FilterOriginToggle.Checked);
			this.FilterTypeBox.Enabled = (this.FilterTypeToggle.Enabled && this.FilterTypeToggle.Checked);
			this.FilterKeywordBox.Enabled = (this.FilterKeywordToggle.Enabled && this.FilterKeywordToggle.Checked);
			this.FilterSourceBox.Enabled = (this.FilterSourceToggle.Enabled && this.FilterSourceToggle.Checked);
			this.FromLbl.Enabled = (this.FilterLevelToggle.Enabled && this.FilterLevelToggle.Checked);
			this.ToLbl.Enabled = (this.FilterLevelToggle.Enabled && this.FilterLevelToggle.Checked);
			this.LevelFromBox.Enabled = (this.FilterLevelToggle.Enabled && this.FilterLevelToggle.Checked);
			this.LevelToBox.Enabled = (this.FilterLevelToggle.Enabled && this.FilterLevelToggle.Checked);
		}

		private void open_close_editor(bool open)
		{
			if (open)
			{
				this.FilterPnl.Visible = true;
				this.InfoLbl.Text = "";
				this.EditLbl.Text = "Collapse";
				return;
			}
			this.FilterPnl.Visible = false;
			this.InfoLbl.Text = this.get_filter_string();
			this.EditLbl.Text = "Expand";
		}

		private string get_filter_string()
		{
			string text = "";
			if (this.FilterNameToggle.Checked && this.FilterNameToggle.Enabled && this.FilterNameBox.Text != "")
			{
				if (text != "")
				{
					text += "; ";
				}
				text = text + "Name: " + this.FilterNameBox.Text;
			}
			if (this.FilterCatToggle.Checked && this.FilterCatToggle.Enabled && this.FilterCatBox.Text != "")
			{
				if (text != "")
				{
					text += "; ";
				}
				text = text + "Category: " + this.FilterCatBox.Text;
			}
			string text2 = "";
			if (this.FilterModToggle.Checked && this.FilterModToggle.Enabled)
			{
				text2 += this.FilterModBox.SelectedItem;
			}
			if (this.FilterRoleToggle.Checked && this.FilterRoleToggle.Enabled)
			{
				if (text2 != "")
				{
					text2 += " ";
				}
				text2 += this.FilterRoleBox.SelectedItem;
			}
			if (text2 != "")
			{
				if (text != "")
				{
					text += "; ";
				}
				text = text + "Role: " + text2;
			}
			if (this.FilterTypeToggle.Checked && this.FilterTypeToggle.Enabled)
			{
				if (text != "")
				{
					text += "; ";
				}
				text = text + "Type: " + this.FilterTypeBox.SelectedItem;
			}
			if (this.FilterOriginToggle.Checked && this.FilterOriginToggle.Enabled)
			{
				if (text != "")
				{
					text += "; ";
				}
				text = text + "Origin: " + this.FilterOriginBox.SelectedItem;
			}
			if (this.FilterKeywordToggle.Checked && this.FilterKeywordToggle.Enabled && this.FilterKeywordBox.Text != "")
			{
				if (text != "")
				{
					text += "; ";
				}
				text = text + "Keywords: " + this.FilterKeywordBox.Text;
			}
			if (this.FilterLevelToggle.Checked && this.FilterLevelToggle.Enabled)
			{
				if (text != "")
				{
					text += "; ";
				}
				int num = (int)this.LevelFromBox.Value;
				int num2 = (int)this.LevelToBox.Value;
				if (num == num2)
				{
					text = text + "Level: " + num;
				}
				else
				{
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
						"Level: ",
						num,
						"-",
						num2
					});
				}
			}
			if (this.FilterLevelAppropriateToggle.Checked && this.FilterLevelAppropriateToggle.Enabled)
			{
				if (text != "")
				{
					text += "; ";
				}
				text += "Level-appropriate only";
			}
			if (this.FilterSourceToggle.Checked && this.FilterSourceToggle.Enabled)
			{
				if (text != "")
				{
					text += "; ";
				}
				text = text + "Source: " + this.FilterSourceBox.SelectedItem;
			}
			return text;
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
			this.FilterLevelToggle = new CheckBox();
			this.FilterModToggle = new CheckBox();
			this.FilterModBox = new ComboBox();
			this.FilterNameToggle = new CheckBox();
			this.FilterCatToggle = new CheckBox();
			this.FilterKeywordBox = new TextBox();
			this.FilterKeywordToggle = new CheckBox();
			this.FilterTypeToggle = new CheckBox();
			this.FilterTypeBox = new ComboBox();
			this.FilterOriginToggle = new CheckBox();
			this.FilterOriginBox = new ComboBox();
			this.FilterCatBox = new TextBox();
			this.FilterRoleToggle = new CheckBox();
			this.FilterRoleBox = new ComboBox();
			this.FilterNameBox = new TextBox();
			this.FilterPnl = new Panel();
			this.FilterSourceBox = new ComboBox();
			this.FilterSourceToggle = new CheckBox();
			this.FilterLevelAppropriateToggle = new CheckBox();
			this.LevelToBox = new NumericUpDown();
			this.LevelFromBox = new NumericUpDown();
			this.ToLbl = new Label();
			this.FromLbl = new Label();
			this.InfoLbl = new ToolStripStatusLabel();
			this.EditLbl = new ToolStripStatusLabel();
			this.Statusbar = new StatusStrip();
			this.FilterPnl.SuspendLayout();
			((ISupportInitialize)this.LevelToBox).BeginInit();
			((ISupportInitialize)this.LevelFromBox).BeginInit();
			this.Statusbar.SuspendLayout();
			base.SuspendLayout();
			this.FilterLevelToggle.AutoSize = true;
			this.FilterLevelToggle.Location = new Point(3, 190);
			this.FilterLevelToggle.Name = "FilterLevelToggle";
			this.FilterLevelToggle.Size = new Size(55, 17);
			this.FilterLevelToggle.TabIndex = 14;
			this.FilterLevelToggle.Text = "Level:";
			this.FilterLevelToggle.UseVisualStyleBackColor = true;
			this.FilterLevelToggle.Click += new EventHandler(this.ToggleChanged);
			this.FilterModToggle.AutoSize = true;
			this.FilterModToggle.Location = new Point(3, 84);
			this.FilterModToggle.Name = "FilterModToggle";
			this.FilterModToggle.Size = new Size(66, 17);
			this.FilterModToggle.TabIndex = 6;
			this.FilterModToggle.Text = "Modifier:";
			this.FilterModToggle.UseVisualStyleBackColor = true;
			this.FilterModToggle.Click += new EventHandler(this.ToggleChanged);
			this.FilterModBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.FilterModBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.FilterModBox.FormattingEnabled = true;
			this.FilterModBox.Location = new Point(84, 82);
			this.FilterModBox.Name = "FilterModBox";
			this.FilterModBox.Size = new Size(165, 21);
			this.FilterModBox.TabIndex = 7;
			this.FilterModBox.SelectedIndexChanged += new EventHandler(this.DropdownOptionChanged);
			this.FilterNameToggle.AutoSize = true;
			this.FilterNameToggle.Location = new Point(3, 5);
			this.FilterNameToggle.Name = "FilterNameToggle";
			this.FilterNameToggle.Size = new Size(57, 17);
			this.FilterNameToggle.TabIndex = 0;
			this.FilterNameToggle.Text = "Name:";
			this.FilterNameToggle.UseVisualStyleBackColor = true;
			this.FilterNameToggle.Click += new EventHandler(this.ToggleChanged);
			this.FilterCatToggle.AutoSize = true;
			this.FilterCatToggle.Location = new Point(3, 31);
			this.FilterCatToggle.Name = "FilterCatToggle";
			this.FilterCatToggle.Size = new Size(71, 17);
			this.FilterCatToggle.TabIndex = 2;
			this.FilterCatToggle.Text = "Category:";
			this.FilterCatToggle.UseVisualStyleBackColor = true;
			this.FilterCatToggle.Click += new EventHandler(this.ToggleChanged);
			this.FilterKeywordBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.FilterKeywordBox.Location = new Point(84, 163);
			this.FilterKeywordBox.Name = "FilterKeywordBox";
			this.FilterKeywordBox.Size = new Size(165, 20);
			this.FilterKeywordBox.TabIndex = 13;
			this.FilterKeywordBox.TextChanged += new EventHandler(this.TextOptionChanged);
			this.FilterKeywordToggle.AutoSize = true;
			this.FilterKeywordToggle.Location = new Point(3, 165);
			this.FilterKeywordToggle.Name = "FilterKeywordToggle";
			this.FilterKeywordToggle.Size = new Size(75, 17);
			this.FilterKeywordToggle.TabIndex = 12;
			this.FilterKeywordToggle.Text = "Keywords:";
			this.FilterKeywordToggle.UseVisualStyleBackColor = true;
			this.FilterKeywordToggle.Click += new EventHandler(this.ToggleChanged);
			this.FilterTypeToggle.AutoSize = true;
			this.FilterTypeToggle.Location = new Point(3, 138);
			this.FilterTypeToggle.Name = "FilterTypeToggle";
			this.FilterTypeToggle.Size = new Size(53, 17);
			this.FilterTypeToggle.TabIndex = 10;
			this.FilterTypeToggle.Text = "Type:";
			this.FilterTypeToggle.UseVisualStyleBackColor = true;
			this.FilterTypeToggle.Click += new EventHandler(this.ToggleChanged);
			this.FilterTypeBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.FilterTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.FilterTypeBox.FormattingEnabled = true;
			this.FilterTypeBox.Location = new Point(84, 136);
			this.FilterTypeBox.Name = "FilterTypeBox";
			this.FilterTypeBox.Size = new Size(165, 21);
			this.FilterTypeBox.TabIndex = 11;
			this.FilterTypeBox.SelectedIndexChanged += new EventHandler(this.DropdownOptionChanged);
			this.FilterOriginToggle.AutoSize = true;
			this.FilterOriginToggle.Location = new Point(3, 111);
			this.FilterOriginToggle.Name = "FilterOriginToggle";
			this.FilterOriginToggle.Size = new Size(56, 17);
			this.FilterOriginToggle.TabIndex = 8;
			this.FilterOriginToggle.Text = "Origin:";
			this.FilterOriginToggle.UseVisualStyleBackColor = true;
			this.FilterOriginToggle.Click += new EventHandler(this.ToggleChanged);
			this.FilterOriginBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.FilterOriginBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.FilterOriginBox.FormattingEnabled = true;
			this.FilterOriginBox.Location = new Point(84, 109);
			this.FilterOriginBox.Name = "FilterOriginBox";
			this.FilterOriginBox.Size = new Size(165, 21);
			this.FilterOriginBox.TabIndex = 9;
			this.FilterOriginBox.SelectedIndexChanged += new EventHandler(this.DropdownOptionChanged);
			this.FilterCatBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.FilterCatBox.Location = new Point(84, 29);
			this.FilterCatBox.Name = "FilterCatBox";
			this.FilterCatBox.Size = new Size(165, 20);
			this.FilterCatBox.TabIndex = 3;
			this.FilterCatBox.TextChanged += new EventHandler(this.TextOptionChanged);
			this.FilterRoleToggle.AutoSize = true;
			this.FilterRoleToggle.Location = new Point(3, 57);
			this.FilterRoleToggle.Name = "FilterRoleToggle";
			this.FilterRoleToggle.Size = new Size(51, 17);
			this.FilterRoleToggle.TabIndex = 4;
			this.FilterRoleToggle.Text = "Role:";
			this.FilterRoleToggle.UseVisualStyleBackColor = true;
			this.FilterRoleToggle.Click += new EventHandler(this.ToggleChanged);
			this.FilterRoleBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.FilterRoleBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.FilterRoleBox.FormattingEnabled = true;
			this.FilterRoleBox.Location = new Point(84, 55);
			this.FilterRoleBox.Name = "FilterRoleBox";
			this.FilterRoleBox.Size = new Size(165, 21);
			this.FilterRoleBox.TabIndex = 5;
			this.FilterRoleBox.SelectedIndexChanged += new EventHandler(this.DropdownOptionChanged);
			this.FilterNameBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.FilterNameBox.Location = new Point(84, 3);
			this.FilterNameBox.Name = "FilterNameBox";
			this.FilterNameBox.Size = new Size(165, 20);
			this.FilterNameBox.TabIndex = 1;
			this.FilterNameBox.TextChanged += new EventHandler(this.TextOptionChanged);
			this.FilterPnl.Controls.Add(this.FilterSourceBox);
			this.FilterPnl.Controls.Add(this.FilterSourceToggle);
			this.FilterPnl.Controls.Add(this.FilterLevelAppropriateToggle);
			this.FilterPnl.Controls.Add(this.LevelToBox);
			this.FilterPnl.Controls.Add(this.LevelFromBox);
			this.FilterPnl.Controls.Add(this.ToLbl);
			this.FilterPnl.Controls.Add(this.FromLbl);
			this.FilterPnl.Controls.Add(this.FilterNameBox);
			this.FilterPnl.Controls.Add(this.FilterLevelToggle);
			this.FilterPnl.Controls.Add(this.FilterRoleBox);
			this.FilterPnl.Controls.Add(this.FilterModToggle);
			this.FilterPnl.Controls.Add(this.FilterRoleToggle);
			this.FilterPnl.Controls.Add(this.FilterModBox);
			this.FilterPnl.Controls.Add(this.FilterCatBox);
			this.FilterPnl.Controls.Add(this.FilterNameToggle);
			this.FilterPnl.Controls.Add(this.FilterOriginBox);
			this.FilterPnl.Controls.Add(this.FilterCatToggle);
			this.FilterPnl.Controls.Add(this.FilterOriginToggle);
			this.FilterPnl.Controls.Add(this.FilterKeywordBox);
			this.FilterPnl.Controls.Add(this.FilterTypeBox);
			this.FilterPnl.Controls.Add(this.FilterKeywordToggle);
			this.FilterPnl.Controls.Add(this.FilterTypeToggle);
			this.FilterPnl.Dock = DockStyle.Top;
			this.FilterPnl.Location = new Point(0, 0);
			this.FilterPnl.Name = "FilterPnl";
			this.FilterPnl.Size = new Size(252, 294);
			this.FilterPnl.TabIndex = 0;
			this.FilterSourceBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.FilterSourceBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.FilterSourceBox.FormattingEnabled = true;
			this.FilterSourceBox.Location = new Point(84, 264);
			this.FilterSourceBox.Name = "FilterSourceBox";
			this.FilterSourceBox.Size = new Size(165, 21);
			this.FilterSourceBox.TabIndex = 21;
			this.FilterSourceBox.SelectedIndexChanged += new EventHandler(this.DropdownOptionChanged);
			this.FilterSourceToggle.AutoSize = true;
			this.FilterSourceToggle.Location = new Point(3, 266);
			this.FilterSourceToggle.Name = "FilterSourceToggle";
			this.FilterSourceToggle.Size = new Size(63, 17);
			this.FilterSourceToggle.TabIndex = 20;
			this.FilterSourceToggle.Text = "Source:";
			this.FilterSourceToggle.UseVisualStyleBackColor = true;
			this.FilterSourceToggle.CheckedChanged += new EventHandler(this.ToggleChanged);
			this.FilterLevelAppropriateToggle.AutoSize = true;
			this.FilterLevelAppropriateToggle.Location = new Point(3, 241);
			this.FilterLevelAppropriateToggle.Name = "FilterLevelAppropriateToggle";
			this.FilterLevelAppropriateToggle.Size = new Size(183, 17);
			this.FilterLevelAppropriateToggle.TabIndex = 19;
			this.FilterLevelAppropriateToggle.Text = "Show level-appropriate items only";
			this.FilterLevelAppropriateToggle.UseVisualStyleBackColor = true;
			this.FilterLevelAppropriateToggle.Click += new EventHandler(this.ToggleChanged);
			this.LevelToBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LevelToBox.Location = new Point(130, 215);
			NumericUpDown arg_CCD_0 = this.LevelToBox;
			int[] array = new int[4];
			array[0] = 40;
			arg_CCD_0.Maximum = new decimal(array);
			NumericUpDown arg_CE9_0 = this.LevelToBox;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_CE9_0.Minimum = new decimal(array2);
			this.LevelToBox.Name = "LevelToBox";
			this.LevelToBox.Size = new Size(119, 20);
			this.LevelToBox.TabIndex = 18;
			NumericUpDown arg_D37_0 = this.LevelToBox;
			int[] array3 = new int[4];
			array3[0] = 10;
			arg_D37_0.Value = new decimal(array3);
			this.LevelToBox.ValueChanged += new EventHandler(this.NumericOptionChanged);
			this.LevelFromBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.LevelFromBox.Location = new Point(130, 189);
			NumericUpDown arg_D92_0 = this.LevelFromBox;
			int[] array4 = new int[4];
			array4[0] = 40;
			arg_D92_0.Maximum = new decimal(array4);
			NumericUpDown arg_DB1_0 = this.LevelFromBox;
			int[] array5 = new int[4];
			array5[0] = 1;
			arg_DB1_0.Minimum = new decimal(array5);
			this.LevelFromBox.Name = "LevelFromBox";
			this.LevelFromBox.Size = new Size(119, 20);
			this.LevelFromBox.TabIndex = 16;
			NumericUpDown arg_E01_0 = this.LevelFromBox;
			int[] array6 = new int[4];
			array6[0] = 1;
			arg_E01_0.Value = new decimal(array6);
			this.LevelFromBox.ValueChanged += new EventHandler(this.NumericOptionChanged);
			this.ToLbl.AutoSize = true;
			this.ToLbl.Location = new Point(81, 217);
			this.ToLbl.Name = "ToLbl";
			this.ToLbl.Size = new Size(23, 13);
			this.ToLbl.TabIndex = 17;
			this.ToLbl.Text = "To:";
			this.FromLbl.AutoSize = true;
			this.FromLbl.Location = new Point(81, 191);
			this.FromLbl.Name = "FromLbl";
			this.FromLbl.Size = new Size(33, 13);
			this.FromLbl.TabIndex = 15;
			this.FromLbl.Text = "From:";
			this.InfoLbl.AutoToolTip = true;
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new Size(202, 17);
			this.InfoLbl.Spring = true;
			this.InfoLbl.Text = "[info]";
			this.InfoLbl.TextAlign = ContentAlignment.MiddleLeft;
			this.EditLbl.IsLink = true;
			this.EditLbl.Name = "EditLbl";
			this.EditLbl.Size = new Size(35, 17);
			this.EditLbl.Text = "(edit)";
			this.EditLbl.Click += new EventHandler(this.EditLbl_Click);
			this.Statusbar.Dock = DockStyle.Top;
			this.Statusbar.Items.AddRange(new ToolStripItem[]
			{
				this.InfoLbl,
				this.EditLbl
			});
			this.Statusbar.Location = new Point(0, 294);
			this.Statusbar.Name = "Statusbar";
			this.Statusbar.Size = new Size(252, 22);
			this.Statusbar.SizingGrip = false;
			this.Statusbar.TabIndex = 1;
			this.Statusbar.Text = "statusStrip1";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoSize = true;
			base.Controls.Add(this.Statusbar);
			base.Controls.Add(this.FilterPnl);
			base.Name = "FilterPanel";
			base.Size = new Size(252, 331);
			this.FilterPnl.ResumeLayout(false);
			this.FilterPnl.PerformLayout();
			((ISupportInitialize)this.LevelToBox).EndInit();
			((ISupportInitialize)this.LevelFromBox).EndInit();
			this.Statusbar.ResumeLayout(false);
			this.Statusbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
