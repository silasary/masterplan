using Masterplan.Properties;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using Utils;

namespace Masterplan.Data
{
	[Serializable]
	public class EncounterCard
	{
		private ICreature fCreature;

		private Guid fCreatureID = Guid.Empty;

		private List<Guid> fTemplateIDs = new List<Guid>();

		private int fLevelAdjustment;

		private Guid fThemeID = Guid.Empty;

		private Guid fThemeAttackPowerID = Guid.Empty;

		private Guid fThemeUtilityPowerID = Guid.Empty;

		private bool fDrawn;

		public Guid CreatureID
		{
			get
			{
				return this.fCreatureID;
			}
			set
			{
				this.fCreatureID = value;
				if (this.fCreatureID != Guid.Empty)
				{
					this.fCreature = Session.FindCreature(this.fCreatureID, SearchType.Global);
				}
			}
		}

		public List<Guid> TemplateIDs
		{
			get
			{
				return this.fTemplateIDs;
			}
			set
			{
				this.fTemplateIDs = value;
			}
		}

		public int LevelAdjustment
		{
			get
			{
				return this.fLevelAdjustment;
			}
			set
			{
				this.fLevelAdjustment = value;
			}
		}

		public Guid ThemeID
		{
			get
			{
				return this.fThemeID;
			}
			set
			{
				this.fThemeID = value;
			}
		}

		public Guid ThemeAttackPowerID
		{
			get
			{
				return this.fThemeAttackPowerID;
			}
			set
			{
				this.fThemeAttackPowerID = value;
			}
		}

		public Guid ThemeUtilityPowerID
		{
			get
			{
				return this.fThemeUtilityPowerID;
			}
			set
			{
				this.fThemeUtilityPowerID = value;
			}
		}

		public bool Drawn
		{
			get
			{
				return this.fDrawn;
			}
			set
			{
				this.fDrawn = value;
			}
		}

		public int XP
		{
			get
			{
				int num = 0;
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature != null)
				{
					if (creature.Role is Minion)
					{
						float num2 = (float)Experience.GetCreatureXP(creature.Level + this.fLevelAdjustment) / 4f;
						num = (int)Math.Round((double)num2, MidpointRounding.AwayFromZero);
					}
					else
					{
						num = Experience.GetCreatureXP(creature.Level + this.fLevelAdjustment);
						switch (this.Flag)
						{
						case RoleFlag.Elite:
							num *= 2;
							break;
						case RoleFlag.Solo:
							num *= 5;
							break;
						}
					}
				}
				if (Session.Project != null)
				{
					num = (int)((double)num * Session.Project.CampaignSettings.XP);
				}
				return num;
			}
		}

		public string Title
		{
			get
			{
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				string text = (creature != null) ? creature.Name : "(unknown creature)";
				foreach (Guid current in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current, SearchType.Global);
					if (creatureTemplate != null)
					{
						text = creatureTemplate.Name + " " + text;
					}
				}
				if (this.fThemeID != Guid.Empty)
				{
					MonsterTheme monsterTheme = Session.FindTheme(this.fThemeID, SearchType.Global);
					if (monsterTheme != null)
					{
						text = text + " (" + monsterTheme.Name + ")";
					}
				}
				return text;
			}
		}

		public string Info
		{
			get
			{
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature == null)
				{
					return "";
				}
				int num = creature.Level + this.fLevelAdjustment;
				if (creature.Role is Minion)
				{
					return string.Concat(new object[]
					{
						"Level ",
						num,
						" ",
						creature.Role
					});
				}
				string text = "";
				switch (this.Flag)
				{
				case RoleFlag.Elite:
					text = "Elite ";
					break;
				case RoleFlag.Solo:
					text = "Solo ";
					break;
				}
				string text2 = "";
				foreach (RoleType current in this.Roles)
				{
					if (text2 != "")
					{
						text2 += " / ";
					}
					text2 += current.ToString();
				}
				if (this.Leader)
				{
					text2 += " (L)";
				}
				return string.Concat(new object[]
				{
					"Level ",
					num,
					" ",
					text,
					text2
				});
			}
		}

		public int Level
		{
			get
			{
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature == null)
				{
					return this.fLevelAdjustment;
				}
				return creature.Level + this.fLevelAdjustment;
			}
		}

		public List<RoleType> Roles
		{
			get
			{
				List<RoleType> list = new List<RoleType>();
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature == null || creature.Role is Minion)
				{
					return list;
				}
				ComplexRole complexRole = creature.Role as ComplexRole;
				if (complexRole != null)
				{
					list.Add(complexRole.Type);
				}
				foreach (Guid current in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current, SearchType.Global);
					if (creatureTemplate != null && !list.Contains(creatureTemplate.Role))
					{
						list.Add(creatureTemplate.Role);
					}
				}
				return list;
			}
		}

		public RoleFlag Flag
		{
			get
			{
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature == null || creature.Role is Minion)
				{
					return RoleFlag.Standard;
				}
				int num = this.fTemplateIDs.Count;
				ComplexRole complexRole = creature.Role as ComplexRole;
				if (complexRole == null)
				{
					return RoleFlag.Standard;
				}
				switch (complexRole.Flag)
				{
				case RoleFlag.Elite:
					num++;
					break;
				case RoleFlag.Solo:
					num += 2;
					break;
				}
				if (num == 0)
				{
					return RoleFlag.Standard;
				}
				if (num == 1)
				{
					return RoleFlag.Elite;
				}
				return RoleFlag.Solo;
			}
		}

		public bool Leader
		{
			get
			{
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature == null || creature.Role is Minion)
				{
					return false;
				}
				ComplexRole complexRole = creature.Role as ComplexRole;
				if (complexRole != null && complexRole.Leader)
				{
					return true;
				}
				foreach (Guid current in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current, SearchType.Global);
					if (creatureTemplate != null && creatureTemplate.Leader)
					{
						return true;
					}
				}
				return false;
			}
		}

		public Regeneration Regeneration
		{
			get
			{
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature == null)
				{
					return null;
				}
				Regeneration regeneration = creature.Regeneration;
				foreach (Guid current in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current, SearchType.Global);
					if (creatureTemplate != null && creatureTemplate.Regeneration != null)
					{
						if (regeneration != null)
						{
							if (creatureTemplate.Regeneration.Value > regeneration.Value)
							{
								regeneration = creatureTemplate.Regeneration;
							}
						}
						else
						{
							regeneration = creatureTemplate.Regeneration;
						}
					}
				}
				if (regeneration == null)
				{
					return null;
				}
				return regeneration.Copy();
			}
		}

		public List<Aura> Auras
		{
			get
			{
				List<Aura> list = new List<Aura>();
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature != null)
				{
					list.AddRange(creature.Auras);
				}
				foreach (Guid current in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current, SearchType.Global);
					if (creatureTemplate != null)
					{
						list.AddRange(creatureTemplate.Auras);
					}
				}
				return list;
			}
		}

		public string Senses
		{
			get
			{
				List<string> list = new List<string>();
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature.Senses != "")
				{
					list.Add(creature.Senses);
				}
				foreach (Guid current in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current, SearchType.Global);
					if (creatureTemplate != null && creatureTemplate.Senses != "" && !list.Contains(creatureTemplate.Senses))
					{
						list.Add(creatureTemplate.Senses);
					}
				}
				string text = "";
				foreach (string current2 in list)
				{
					if (text != "")
					{
						text += "; ";
					}
					text += current2;
				}
				return text;
			}
		}

		public string Movement
		{
			get
			{
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				string text = creature.Movement;
				foreach (Guid current in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current, SearchType.Global);
					if (creatureTemplate != null && creatureTemplate.Movement != "")
					{
						if (text != "")
						{
							text += "; ";
						}
						text += creatureTemplate.Movement;
					}
				}
				return text;
			}
		}

		public string Equipment
		{
			get
			{
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature.Equipment == null)
				{
					return "";
				}
				return creature.Equipment;
			}
		}

		public CardCategory Category
		{
			get
			{
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature.Role is Minion)
				{
					return CardCategory.Minion;
				}
				if (this.Flag == RoleFlag.Solo)
				{
					return CardCategory.Solo;
				}
				List<RoleType> roles = this.Roles;
				if (roles.Contains(RoleType.Soldier) || roles.Contains(RoleType.Brute))
				{
					return CardCategory.SoldierBrute;
				}
				if (roles.Contains(RoleType.Skirmisher))
				{
					return CardCategory.Skirmisher;
				}
				if (roles.Contains(RoleType.Artillery))
				{
					return CardCategory.Artillery;
				}
				if (roles.Contains(RoleType.Controller))
				{
					return CardCategory.Controller;
				}
				if (roles.Contains(RoleType.Lurker))
				{
					return CardCategory.Lurker;
				}
				throw new Exception();
			}
		}

		public int HP
		{
			get
			{
				int num = 0;
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature != null)
				{
					num += creature.HP;
				}
				if (this.fTemplateIDs.Count != 0)
				{
					int num2 = 0;
					foreach (Guid current in this.fTemplateIDs)
					{
						CreatureTemplate creatureTemplate = Session.FindTemplate(current, SearchType.Global);
						if (creatureTemplate != null && creatureTemplate.HP > num2)
						{
							num2 = creatureTemplate.HP;
						}
					}
					num += num2 * this.Level;
					num += creature.Constitution.Score;
					if (this.Flag == RoleFlag.Solo)
					{
						num *= 2;
					}
				}
				if (this.fLevelAdjustment != 0 && creature != null && creature.Role is ComplexRole)
				{
					ComplexRole complexRole = creature.Role as ComplexRole;
					int num3 = 1;
					switch (complexRole.Flag)
					{
					case RoleFlag.Elite:
						num3 = 2;
						break;
					case RoleFlag.Solo:
						num3 = 5;
						break;
					}
					switch (complexRole.Type)
					{
					case RoleType.Artillery:
						num += 6 * this.fLevelAdjustment * num3;
						break;
					case RoleType.Brute:
						num += 10 * this.fLevelAdjustment * num3;
						break;
					case RoleType.Controller:
						num += 8 * this.fLevelAdjustment * num3;
						break;
					case RoleType.Lurker:
						num += 6 * this.fLevelAdjustment * num3;
						break;
					case RoleType.Skirmisher:
						num += 8 * this.fLevelAdjustment * num3;
						break;
					case RoleType.Soldier:
						num += 8 * this.fLevelAdjustment * num3;
						break;
					}
				}
				if (Session.Project != null && creature != null && creature.Role is ComplexRole)
				{
					num = (int)((double)num * Session.Project.CampaignSettings.HP);
				}
				return num;
			}
		}

		public int Initiative
		{
			get
			{
				int num = 0;
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature != null)
				{
					num += creature.Initiative;
				}
				foreach (Guid current in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current, SearchType.Global);
					if (creatureTemplate != null)
					{
						num += creatureTemplate.Initiative;
					}
				}
				if (this.fLevelAdjustment != 0)
				{
					num += this.fLevelAdjustment / 2;
				}
				return num;
			}
		}

		public int AC
		{
			get
			{
				int num = 0;
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature != null)
				{
					num += creature.AC;
				}
				foreach (Guid current in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current, SearchType.Global);
					if (creatureTemplate != null)
					{
						num += creatureTemplate.AC;
					}
				}
				num += this.fLevelAdjustment;
				if (Session.Project != null)
				{
					num += Session.Project.CampaignSettings.ACBonus;
				}
				return num;
			}
		}

		public int Fortitude
		{
			get
			{
				int num = 0;
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature != null)
				{
					num += creature.Fortitude;
				}
				foreach (Guid current in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current, SearchType.Global);
					if (creatureTemplate != null)
					{
						num += creatureTemplate.Fortitude;
					}
				}
				num += this.fLevelAdjustment;
				if (Session.Project != null)
				{
					num += Session.Project.CampaignSettings.NADBonus;
				}
				return num;
			}
		}

		public int Reflex
		{
			get
			{
				int num = 0;
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature != null)
				{
					num += creature.Reflex;
				}
				foreach (Guid current in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current, SearchType.Global);
					if (creatureTemplate != null)
					{
						num += creatureTemplate.Reflex;
					}
				}
				num += this.fLevelAdjustment;
				if (Session.Project != null)
				{
					num += Session.Project.CampaignSettings.NADBonus;
				}
				return num;
			}
		}

		public int Will
		{
			get
			{
				int num = 0;
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature != null)
				{
					num += creature.Will;
				}
				foreach (Guid current in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current, SearchType.Global);
					if (creatureTemplate != null)
					{
						num += creatureTemplate.Will;
					}
				}
				num += this.fLevelAdjustment;
				if (Session.Project != null)
				{
					num += Session.Project.CampaignSettings.NADBonus;
				}
				return num;
			}
		}

		public List<CreaturePower> CreaturePowers
		{
			get
			{
				Enum.GetValues(typeof(DamageCategory));
				Enum.GetValues(typeof(DamageDegree));
				List<CreaturePower> list = new List<CreaturePower>();
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature != null)
				{
					foreach (CreaturePower current in creature.CreaturePowers)
					{
						list.Add(current.Copy());
					}
				}
				foreach (Guid current2 in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current2, SearchType.Global);
					if (creatureTemplate != null)
					{
						foreach (CreaturePower current3 in creatureTemplate.CreaturePowers)
						{
							CreaturePower creaturePower = current3.Copy();
							if (creatureTemplate.Type == CreatureTemplateType.Functional && creaturePower.Attack != null)
							{
								creaturePower.Attack.Bonus += this.Level;
							}
							list.Add(creaturePower);
						}
					}
				}
				if (this.fThemeID != Guid.Empty)
				{
					MonsterTheme monsterTheme = Session.FindTheme(this.fThemeID, SearchType.Global);
					if (monsterTheme != null)
					{
						ThemePowerData themePowerData = monsterTheme.FindPower(this.fThemeAttackPowerID);
						if (themePowerData != null)
						{
							list.Add(themePowerData.Power.Copy());
						}
						ThemePowerData themePowerData2 = monsterTheme.FindPower(this.fThemeUtilityPowerID);
						if (themePowerData2 != null)
						{
							list.Add(themePowerData2.Power.Copy());
						}
					}
				}
				if (this.fLevelAdjustment != 0)
				{
					foreach (CreaturePower current4 in list)
					{
						if (current4.Attack != null)
						{
							current4.Attack.Bonus += this.fLevelAdjustment;
							if (Session.Project != null)
							{
								current4.Attack.Bonus += Session.Project.CampaignSettings.AttackBonus;
							}
						}
						string text = AI.ExtractDamage(current4.Details);
						if (text != "")
						{
							DiceExpression diceExpression = DiceExpression.Parse(text);
							if (diceExpression != null)
							{
								DiceExpression diceExpression2 = diceExpression.Adjust(this.fLevelAdjustment);
								if (diceExpression2 != null && diceExpression.ToString() != diceExpression2.ToString())
								{
									current4.Details = current4.Details.Replace(text, string.Concat(new object[]
									{
										diceExpression2,
										" damage (adjusted from ",
										text,
										")"
									}));
								}
							}
						}
					}
				}
				return list;
			}
		}

		public List<DamageModifier> DamageModifiers
		{
			get
			{
				List<DamageModifier> list = new List<DamageModifier>();
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature != null)
				{
					foreach (DamageModifier current in creature.DamageModifiers)
					{
						list.Add(current.Copy());
					}
				}
				foreach (Guid current2 in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current2, SearchType.Global);
					if (creatureTemplate != null)
					{
						foreach (DamageModifierTemplate current3 in creatureTemplate.DamageModifierTemplates)
						{
							DamageModifier damageModifier = null;
							foreach (DamageModifier current4 in list)
							{
								if (current4.Type == current3.Type)
								{
									damageModifier = current4;
									break;
								}
							}
							if (damageModifier == null || damageModifier.Value != 0)
							{
								if (damageModifier == null)
								{
									damageModifier = new DamageModifier();
									damageModifier.Type = current3.Type;
									damageModifier.Value = 0;
									list.Add(damageModifier);
								}
								if (current3.HeroicValue + current3.ParagonValue + current3.EpicValue == 0)
								{
									damageModifier.Value = 0;
								}
								else
								{
									int num = current3.HeroicValue;
									if (creature.Level >= 10)
									{
										num = current3.ParagonValue;
									}
									if (creature.Level >= 20)
									{
										num = current3.EpicValue;
									}
									damageModifier.Value += num;
									if (damageModifier.Value == 0)
									{
										list.Remove(damageModifier);
									}
								}
							}
						}
					}
				}
				return list;
			}
		}

		public string Resist
		{
			get
			{
				string text = "";
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature != null)
				{
					text += creature.Resist;
				}
				foreach (Guid current in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current, SearchType.Global);
					if (creatureTemplate != null && !(creatureTemplate.Resist == ""))
					{
						if (text != "")
						{
							text += ", ";
						}
						text += creatureTemplate.Resist;
					}
				}
				return text;
			}
		}

		public string Vulnerable
		{
			get
			{
				string text = "";
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature != null)
				{
					text += creature.Vulnerable;
				}
				foreach (Guid current in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current, SearchType.Global);
					if (creatureTemplate != null && !(creatureTemplate.Vulnerable == ""))
					{
						if (text != "")
						{
							text += ", ";
						}
						text += creatureTemplate.Vulnerable;
					}
				}
				return text;
			}
		}

		public string Immune
		{
			get
			{
				string text = "";
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature != null)
				{
					text += creature.Immune;
				}
				foreach (Guid current in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current, SearchType.Global);
					if (creatureTemplate != null && !(creatureTemplate.Immune == ""))
					{
						if (text != "")
						{
							text += ", ";
						}
						text += creatureTemplate.Immune;
					}
				}
				return text;
			}
		}

		public string Tactics
		{
			get
			{
				string text = "";
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature != null)
				{
					text += creature.Tactics;
				}
				foreach (Guid current in this.fTemplateIDs)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current, SearchType.Global);
					if (creatureTemplate != null || !(creatureTemplate.Tactics == ""))
					{
						if (text != "")
						{
							text += ", ";
						}
						text += creatureTemplate.Tactics;
					}
				}
				return text;
			}
		}

		public string Skills
		{
			get
			{
				ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
				if (creature == null)
				{
					return "";
				}
				Dictionary<string, int> dictionary = CreatureHelper.ParseSkills(creature.Skills);
				MonsterTheme monsterTheme = (this.fThemeID != Guid.Empty) ? Session.FindTheme(this.fThemeID, SearchType.Global) : null;
				if (monsterTheme != null)
				{
					foreach (Pair<string, int> current in monsterTheme.SkillBonuses)
					{
						if (dictionary.ContainsKey(current.First))
						{
							dictionary[current.First] = dictionary[current.First] + current.Second;
						}
						else
						{
							int num = this.Level / 2;
							string keyAbility = Masterplan.Tools.Skills.GetKeyAbility(current.First);
							if (keyAbility == "Strength")
							{
								num += creature.Strength.Modifier;
							}
							if (keyAbility == "Constitution")
							{
								num += creature.Constitution.Modifier;
							}
							if (keyAbility == "Dexterity")
							{
								num += creature.Dexterity.Modifier;
							}
							if (keyAbility == "Intelligence")
							{
								num += creature.Intelligence.Modifier;
							}
							if (keyAbility == "Wisdom")
							{
								num += creature.Wisdom.Modifier;
							}
							if (keyAbility == "Charisma")
							{
								num += creature.Charisma.Modifier;
							}
							dictionary[current.First] = current.Second + num;
						}
					}
				}
				BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
				foreach (string current2 in dictionary.Keys)
				{
					binarySearchTree.Add(current2);
				}
				string text = "";
				foreach (string current3 in binarySearchTree.SortedList)
				{
					if (text != "")
					{
						text += ", ";
					}
					int num2 = dictionary[current3];
					int num3 = num2 - creature.Level / 2;
					num2 = num3 + (creature.Level + this.fLevelAdjustment) / 2;
					if (num2 >= 0)
					{
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							current3,
							" +",
							num2
						});
					}
					else
					{
						object obj2 = text;
						text = string.Concat(new object[]
						{
							obj2,
							current3,
							" ",
							num2
						});
					}
				}
				return text;
			}
		}

		public EncounterCard()
		{
		}

		public EncounterCard(Guid creature_id)
		{
			this.fCreatureID = creature_id;
			if (this.fCreatureID != Guid.Empty)
			{
				this.fCreature = Session.FindCreature(this.fCreatureID, SearchType.Global);
			}
		}

		public EncounterCard(ICreature creature)
		{
			this.fCreature = creature;
			this.fCreatureID = creature.ID;
		}

		public CreaturePower FindPower(Guid power_id)
		{
			List<CreaturePower> creaturePowers = this.CreaturePowers;
			foreach (CreaturePower current in creaturePowers)
			{
				if (current.ID == power_id)
				{
					return current;
				}
			}
			return null;
		}

		public Difficulty GetDifficulty(int party_level)
		{
			int num = this.Level - party_level;
			if (num < -1)
			{
				return Difficulty.Trivial;
			}
			Difficulty result = Difficulty.Extreme;
			switch (num)
			{
			case -1:
			case 0:
			case 1:
				result = Difficulty.Easy;
				break;
			case 2:
			case 3:
				result = Difficulty.Moderate;
				break;
			case 4:
			case 5:
				result = Difficulty.Hard;
				break;
			}
			return result;
		}

		public int GetDamageModifier(DamageType type, CombatData data)
		{
			List<DamageModifier> list = new List<DamageModifier>();
			list.AddRange(this.DamageModifiers);
			if (data != null)
			{
				foreach (OngoingCondition current in data.Conditions)
				{
					if (current.Type == OngoingType.DamageModifier)
					{
						list.Add(current.DamageModifier);
					}
				}
			}
			if (list.Count == 0)
			{
				return 0;
			}
			List<int> list2 = new List<int>();
			foreach (DamageModifier current2 in list)
			{
				if (current2.Type == type)
				{
					if (current2.Value == 0)
					{
						list2.Add(int.MinValue);
					}
					else
					{
						list2.Add(current2.Value);
					}
				}
			}
			int result;
			if (list2.Contains(int.MinValue))
			{
				result = int.MinValue;
			}
			else
			{
				int num = 0;
				int num2 = 0;
				foreach (int current3 in list2)
				{
					if (current3 > 0 && current3 > num)
					{
						num = current3;
					}
					if (current3 < 0 && current3 < num2)
					{
						num2 = current3;
					}
				}
				result = num + num2;
			}
			return result;
		}

		public int GetDamageModifier(List<DamageType> types, CombatData data)
		{
			if (types == null || types.Count == 0)
			{
				return 0;
			}
			Dictionary<DamageType, int> dictionary = new Dictionary<DamageType, int>();
			foreach (DamageType current in types)
			{
				dictionary[current] = this.GetDamageModifier(current, data);
			}
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			List<int> list3 = new List<int>();
			foreach (DamageType current2 in types)
			{
				int num = dictionary[current2];
				if (num == int.MinValue)
				{
					list.Add(num);
				}
				if (num < 0)
				{
					list2.Add(num);
				}
				if (num > 0)
				{
					list3.Add(num);
				}
			}
			if (list.Count == types.Count)
			{
				return int.MinValue;
			}
			if (list2.Count == types.Count)
			{
				list2.Sort();
				list2.Reverse();
				return list2[0];
			}
			if (list3.Count == types.Count)
			{
				list3.Sort();
				return list3[0];
			}
			return 0;
		}

		public EncounterCard Copy()
		{
			EncounterCard encounterCard = new EncounterCard();
			encounterCard.CreatureID = this.fCreatureID;
			foreach (Guid current in this.fTemplateIDs)
			{
				encounterCard.TemplateIDs.Add(current);
			}
			encounterCard.LevelAdjustment = this.fLevelAdjustment;
			encounterCard.ThemeID = this.fThemeID;
			encounterCard.ThemeAttackPowerID = this.fThemeAttackPowerID;
			encounterCard.ThemeUtilityPowerID = this.fThemeUtilityPowerID;
			encounterCard.Drawn = this.fDrawn;
			return encounterCard;
		}

		public List<string> AsText(CombatData combat_data, CardMode mode, bool full)
		{
			ICreature creature = (this.fCreature != null) ? this.fCreature : Session.FindCreature(this.fCreatureID, SearchType.Global);
			if (creature != null)
			{
				List<string> list = new List<string>();
				if (mode != CardMode.Text)
				{
					string raw_text = (combat_data == null) ? this.Title : combat_data.DisplayName;
					list.Add("<TABLE>");
					if (mode == CardMode.Build)
					{
						bool flag = false;
						foreach (CreaturePower current in this.CreaturePowers)
						{
							if (current.Action != null && current.Action.Use == PowerUseType.Basic && current.Attack != null)
							{
								flag = true;
							}
						}
						if (!flag)
						{
							list.Add("<TR class=warning>");
							list.Add("<TD colspan=3 align=center>");
							list.Add("<B>Warning</B>: This creature has no basic attack");
							list.Add("</TD>");
							list.Add("</TR>");
						}
						if (this.CreaturePowers.Count > 10)
						{
							list.Add("<TR class=warning>");
							list.Add("<TD colspan=3 align=center>");
							list.Add("<B>Warning</B>: This many powers might be slow in play");
							list.Add("</TD>");
							list.Add("</TR>");
						}
					}
					list.Add("<TR class=creature>");
					list.Add("<TD colspan=2>");
					list.Add("<B>" + HTML.Process(raw_text, true) + "</B>");
					list.Add("<BR>");
					list.Add(creature.Phenotype);
					list.Add("</TD>");
					list.Add("<TD>");
					list.Add("<B>" + HTML.Process(this.Info, true) + "</B>");
					list.Add("<BR>");
					list.Add(this.XP + " XP");
					list.Add("</TD>");
					list.Add("</TR>");
					if (mode == CardMode.Build)
					{
						list.Add("<TR class=creature>");
						list.Add("<TD colspan=3 align=center>");
						list.Add("<A href=build:profile style=\"color:white\">(click here to edit this top section)</A>");
						list.Add("</TD>");
						list.Add("</TR>");
					}
				}
				if (mode != CardMode.Text)
				{
					list.Add("<TR>");
				}
				string str = this.HP.ToString();
				if (combat_data != null && combat_data.Damage != 0)
				{
					int num = this.HP - combat_data.Damage;
					if (creature.Role is Minion)
					{
						str = num.ToString();
					}
					else
					{
						str = num + " of " + this.HP;
					}
				}
				string text = (mode != CardMode.Text) ? "<B>HP</B>" : "HP";
				text = text + " " + str;
				if (combat_data != null && mode == CardMode.Combat)
				{
					if (creature.Role is Minion)
					{
						if (combat_data.Damage == 0)
						{
							text = string.Concat(new object[]
							{
								text,
								" (<A href=kill:",
								combat_data.ID,
								">kill</A>)"
							});
						}
						else
						{
							text = string.Concat(new object[]
							{
								text,
								" (<A href=revive:",
								combat_data.ID,
								">revive</A>)"
							});
						}
					}
					else
					{
						text = string.Concat(new object[]
						{
							text,
							" (<A href=dmg:",
							combat_data.ID,
							">dmg</A> | <A href=heal:",
							combat_data.ID,
							">heal</A>)"
						});
					}
				}
				if (!(creature.Role is Minion))
				{
					string text2 = (mode != CardMode.Text) ? "<B>Bloodied</B>" : "Bloodied";
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
						"; ",
						text2,
						" ",
						this.HP / 2
					});
				}
				if (combat_data != null && combat_data.TempHP > 0)
				{
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
						"; ",
						(mode != CardMode.Text) ? "<B>Temp HP</B>" : "Temp HP",
						" ",
						combat_data.TempHP
					});
				}
				if (mode == CardMode.Build)
				{
					text = " <A href=build:combat>" + text + "</A>";
				}
				if (mode != CardMode.Text)
				{
					list.Add("<TD colspan=2>");
					list.Add(text);
					list.Add("</TD>");
				}
				else
				{
					list.Add(text);
				}
				int initiative = this.Initiative;
				string text3 = initiative.ToString();
				if (initiative >= 0)
				{
					text3 = "+" + text3;
				}
				if (combat_data != null && combat_data.Initiative != int.MinValue)
				{
					text3 = string.Concat(new object[]
					{
						combat_data.Initiative,
						" (",
						text3,
						")"
					});
				}
				switch (mode)
				{
				case CardMode.Text:
					list.Add("Initiative " + text3);
					break;
				case CardMode.View:
					list.Add("<TD>");
					list.Add("<B>Initiative</B> " + text3);
					list.Add("</TD>");
					break;
				case CardMode.Combat:
					list.Add("<TD>");
					list.Add(string.Concat(new object[]
					{
						"<B>Initiative</B> <A href=init:",
						combat_data.ID,
						">",
						text3,
						"</A>"
					}));
					list.Add("</TD>");
					break;
				case CardMode.Build:
					list.Add("<TD>");
					list.Add("<A href=build:combat><B>Initiative</B> " + text3 + "</A>");
					list.Add("</TD>");
					break;
				}
				if (mode != CardMode.Text)
				{
					list.Add("</TR>");
					list.Add("<TR>");
				}
				string text4 = (mode != CardMode.Text) ? "<B>AC</B>" : "AC";
				string text5 = (mode != CardMode.Text) ? "<B>Fort</B>" : "Fort";
				string text6 = (mode != CardMode.Text) ? "<B>Ref</B>" : "Ref";
				string text7 = (mode != CardMode.Text) ? "<B>Will</B>" : "Will";
				int num2 = this.AC;
				int num3 = this.Fortitude;
				int num4 = this.Reflex;
				int num5 = this.Will;
				if (combat_data != null)
				{
					foreach (OngoingCondition current2 in combat_data.Conditions)
					{
						if (current2.Type == OngoingType.DefenceModifier)
						{
							if (current2.Defences.Contains(DefenceType.AC))
							{
								num2 += current2.DefenceMod;
							}
							if (current2.Defences.Contains(DefenceType.Fortitude))
							{
								num3 += current2.DefenceMod;
							}
							if (current2.Defences.Contains(DefenceType.Reflex))
							{
								num4 += current2.DefenceMod;
							}
							if (current2.Defences.Contains(DefenceType.Will))
							{
								num5 += current2.DefenceMod;
							}
						}
					}
				}
				if (num2 == this.AC || mode == CardMode.Text)
				{
					text4 = text4 + " " + num2;
				}
				else
				{
					object obj = text4;
					text4 = string.Concat(new object[]
					{
						obj,
						" <B><I>",
						num2,
						"</I></B>"
					});
				}
				if (num3 == this.Fortitude || mode == CardMode.Text)
				{
					text5 = text5 + " " + num3;
				}
				else
				{
					object obj = text5;
					text5 = string.Concat(new object[]
					{
						obj,
						" <B><I>",
						num3,
						"</I></B>"
					});
				}
				if (num4 == this.Reflex || mode == CardMode.Text)
				{
					text6 = text6 + " " + num4;
				}
				else
				{
					object obj = text6;
					text6 = string.Concat(new object[]
					{
						obj,
						" <B><I>",
						num4,
						"</I></B>"
					});
				}
				if (num5 == this.Will || mode == CardMode.Text)
				{
					text7 = text7 + " " + num5;
				}
				else
				{
					object obj = text7;
					text7 = string.Concat(new object[]
					{
						obj,
						" <B><I>",
						num5,
						"</I></B>"
					});
				}
				string text8 = string.Concat(new string[]
				{
					text4,
					"; ",
					text5,
					"; ",
					text6,
					"; ",
					text7
				});
				if (mode != CardMode.Text)
				{
					list.Add("<TD colspan=2>");
				}
				if (mode == CardMode.Build)
				{
					text8 = "<A href=build:combat>" + text8 + "</A>";
				}
				list.Add(text8);
				if (mode != CardMode.Text)
				{
					list.Add("</TD>");
				}
				if (mode != CardMode.Text)
				{
					string text9 = "";
					if (creature.Skills != null && creature.Skills != "")
					{
						string[] array = creature.Skills.Split(new string[]
						{
							";",
							","
						}, StringSplitOptions.RemoveEmptyEntries);
						string[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							string text10 = array2[i];
							string text11 = text10.Trim();
							if (text11.ToLower().Contains("perc"))
							{
								text9 = text11;
							}
						}
					}
					if (text9 == "")
					{
						int num6 = creature.Wisdom.Modifier + this.Level / 2;
						text9 = "Perception ";
						if (num6 >= 0)
						{
							text9 += "+";
						}
						text9 += num6.ToString();
					}
					if (text9 != "")
					{
						list.Add("<TD>");
						if (mode == CardMode.Build)
						{
							text9 = "<A href=build:skills>" + text9 + "</A>";
						}
						list.Add(text9);
						list.Add("</TD>");
					}
				}
				if (mode != CardMode.Text)
				{
					list.Add("</TR>");
					list.Add("<TR>");
				}
				if (mode != CardMode.Text)
				{
					string text12 = HTML.Process(this.Movement, true);
					if (text12 != "")
					{
						text12 = "<B>Speed</B> " + text12;
					}
					if (mode == CardMode.Build && text12 == "")
					{
						text12 = "(specify movement)";
					}
					if (text12 != "")
					{
						list.Add("<TD colspan=2>");
						if (mode == CardMode.Build)
						{
							text12 = "<A href=build:movement>" + text12 + "</A>";
						}
						list.Add(text12);
						list.Add("</TD>");
					}
				}
				if (mode != CardMode.Text)
				{
					string text13 = this.Senses;
					if (text13 == null)
					{
						text13 = "";
					}
					text13 = HTML.Process(text13, true);
					if (text13.ToLower().Contains("perception"))
					{
						string[] array3 = text13.Split(new string[]
						{
							";",
							","
						}, StringSplitOptions.RemoveEmptyEntries);
						text13 = "";
						string[] array2 = array3;
						for (int i = 0; i < array2.Length; i++)
						{
							string text14 = array2[i];
							if (!text14.ToLower().Contains("perception"))
							{
								if (text13 != "")
								{
									text13 += "; ";
								}
								text13 += text14;
							}
						}
					}
					int num7 = (this.Flag == RoleFlag.Standard) ? 1 : 2;
					int num8 = this.DamageModifiers.Count;
					if (combat_data != null)
					{
						foreach (OngoingCondition current3 in combat_data.Conditions)
						{
							if (current3.Type == OngoingType.DamageModifier)
							{
								num8++;
							}
						}
					}
					if (this.Resist != "" || this.Vulnerable != "" || this.Immune != "" || num8 != 0 || mode == CardMode.Build)
					{
						num7++;
					}
					if (mode == CardMode.Build)
					{
						if (text13 == "")
						{
							text13 = "(specify senses)";
						}
						text13 = "<A href=build:senses>" + text13 + "</A>";
					}
					list.Add(string.Concat(new object[]
					{
						"<TD rowspan=",
						num7,
						">",
						text13,
						"</TD>"
					}));
				}
				if (mode != CardMode.Text)
				{
					list.Add("</TR>");
				}
				if (mode != CardMode.Text)
				{
					string text15 = HTML.Process(this.Resist, true);
					string text16 = HTML.Process(this.Vulnerable, true);
					string text17 = HTML.Process(this.Immune, true);
					if (text15 == null)
					{
						text15 = "";
					}
					if (text16 == null)
					{
						text16 = "";
					}
					if (text17 == null)
					{
						text17 = "";
					}
					List<DamageModifier> list2 = new List<DamageModifier>();
					list2.AddRange(this.DamageModifiers);
					if (combat_data != null)
					{
						foreach (OngoingCondition current4 in combat_data.Conditions)
						{
							if (current4.Type == OngoingType.DamageModifier)
							{
								list2.Add(current4.DamageModifier);
							}
						}
					}
					foreach (DamageModifier current5 in list2)
					{
						if (current5.Value == 0)
						{
							if (text17 != "")
							{
								text17 += ", ";
							}
							text17 += current5.Type.ToString().ToLower();
						}
						if (current5.Value > 0)
						{
							if (text16 != "")
							{
								text16 += ", ";
							}
							object obj = text16;
							text16 = string.Concat(new object[]
							{
								obj,
								current5.Value,
								" ",
								current5.Type.ToString().ToLower()
							});
						}
						if (current5.Value < 0)
						{
							if (text15 != "")
							{
								text15 += ", ";
							}
							int num9 = Math.Abs(current5.Value);
							object obj = text15;
							text15 = string.Concat(new object[]
							{
								obj,
								num9,
								" ",
								current5.Type.ToString().ToLower()
							});
						}
					}
					string text18 = "";
					if (text17 != "")
					{
						text18 = text18 + "<B>Immune</B> " + text17;
					}
					if (text15 != "")
					{
						if (text18 != "")
						{
							text18 += "; ";
						}
						text18 = text18 + "<B>Resist</B> " + text15;
					}
					if (text16 != "")
					{
						if (text18 != "")
						{
							text18 += "; ";
						}
						text18 = text18 + "<B>Vulnerable</B> " + text16;
					}
					if (text18 != "")
					{
						if (mode == CardMode.Build)
						{
							text18 = "<A href=build:damage>" + text18 + "</A>";
						}
						list.Add("<TR>");
						list.Add("<TD colspan=2>");
						list.Add(text18);
						list.Add("</TD>");
						list.Add("</TR>");
					}
					else if (mode == CardMode.Build)
					{
						list.Add("<TR>");
						list.Add("<TD colspan=2>");
						list.Add("<A href=build:damage>No resistances / vulnerabilities / immunities</A>");
						list.Add("</TD>");
						list.Add("</TR>");
					}
				}
				bool flag2 = false;
				if (mode != CardMode.Text)
				{
					int num10 = 0;
					int num11 = 0;
					switch (this.Flag)
					{
					case RoleFlag.Elite:
						num10 = 2;
						num11 = 1;
						break;
					case RoleFlag.Solo:
						num10 = 5;
						num11 = 2;
						break;
					}
					if (num11 != 0)
					{
						list.Add("<TD colspan=2>");
						list.Add(string.Concat(new object[]
						{
							"<B>Saving Throws</B> +",
							num10,
							" <B>Action Points</B> ",
							num11
						}));
						list.Add("</TD>");
						flag2 = true;
					}
				}
				if (flag2 && mode != CardMode.Text)
				{
					list.Add("</TR>");
				}
				if (mode == CardMode.Build)
				{
					list.Add("<TR>");
					list.Add("<TD colspan=3 align=center>");
					list.Add("(click on any value in this section to edit it)");
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (mode != CardMode.Text && full)
				{
					if (mode == CardMode.Build)
					{
						list.Add("<TR class=creature>");
						list.Add("<TD colspan=3>");
						list.Add("<B>Powers and Traits</B>");
						list.Add("</TD>");
						list.Add("</TR>");
						list.Add("<TR>");
						list.Add("<TD colspan=3 align=center>");
						list.Add("<A href=power:addtrait>add a trait</A>");
						list.Add("|");
						list.Add("<A href=power:addpower>add a power</A>");
						list.Add("|");
						list.Add("<A href=power:addaura>add an aura</A>");
						if (this.Regeneration == null)
						{
							list.Add("|");
							list.Add("<A href=power:regenedit>add regeneration</A>");
						}
						list.Add("<BR>");
						list.Add("<A href=power:browse>browse for an existing power or trait</A>");
						list.Add("</TD>");
						list.Add("</TR>");
					}
					Dictionary<CreaturePowerCategory, List<CreaturePower>> dictionary = new Dictionary<CreaturePowerCategory, List<CreaturePower>>();
					Array values = Enum.GetValues(typeof(CreaturePowerCategory));
					foreach (CreaturePowerCategory key in values)
					{
						dictionary[key] = new List<CreaturePower>();
					}
					foreach (CreaturePower current6 in this.CreaturePowers)
					{
						dictionary[current6.Category].Add(current6);
					}
					foreach (CreaturePowerCategory key2 in values)
					{
						dictionary[key2].Sort();
					}
					foreach (CreaturePowerCategory creaturePowerCategory in values)
					{
						int num12 = dictionary[creaturePowerCategory].Count;
						if (creaturePowerCategory == CreaturePowerCategory.Trait)
						{
							num12 += this.Auras.Count;
							if (combat_data != null)
							{
								foreach (OngoingCondition current7 in combat_data.Conditions)
								{
									if (current7.Type == OngoingType.Aura)
									{
										num12++;
									}
								}
							}
							bool flag3 = this.Regeneration != null;
							if (combat_data != null)
							{
								foreach (OngoingCondition current8 in combat_data.Conditions)
								{
									if (current8.Type == OngoingType.Regeneration)
									{
										flag3 = true;
									}
								}
							}
							if (flag3)
							{
								num12++;
							}
						}
						if (num12 != 0)
						{
							string str2 = "";
							switch (creaturePowerCategory)
							{
							case CreaturePowerCategory.Trait:
								str2 = "Traits";
								break;
							case CreaturePowerCategory.Standard:
							case CreaturePowerCategory.Move:
							case CreaturePowerCategory.Minor:
							case CreaturePowerCategory.Free:
								str2 = creaturePowerCategory + " Actions";
								break;
							case CreaturePowerCategory.Triggered:
								str2 = "Triggered Actions";
								break;
							case CreaturePowerCategory.Other:
								str2 = "Other Actions";
								break;
							}
							list.Add("<TR class=creature>");
							list.Add("<TD colspan=3>");
							list.Add("<B>" + str2 + "</B>");
							list.Add("</TD>");
							list.Add("</TR>");
							if (creaturePowerCategory == CreaturePowerCategory.Trait)
							{
								List<Aura> list3 = new List<Aura>();
								list3.AddRange(this.Auras);
								if (combat_data != null)
								{
									foreach (OngoingCondition current9 in combat_data.Conditions)
									{
										if (current9.Type == OngoingType.Aura)
										{
											list3.Add(current9.Aura);
										}
									}
								}
								foreach (Aura current10 in list3)
								{
									string text19 = HTML.Process(current10.Description.Trim(), true);
									if (text19.StartsWith("aura", StringComparison.OrdinalIgnoreCase))
									{
										text19 = "A" + text19.Substring(1);
									}
									MemoryStream memoryStream = new MemoryStream();
									Resources.Aura.Save(memoryStream, ImageFormat.Png);
									byte[] inArray = memoryStream.ToArray();
									string str3 = Convert.ToBase64String(inArray);
									list.Add("<TR class=shaded>");
									list.Add("<TD colspan=3>");
									list.Add("<img src=data:image/png;base64," + str3 + ">");
									list.Add("<B>" + HTML.Process(current10.Name, true) + "</B>");
									if (current10.Keywords != "")
									{
										list.Add("(" + current10.Keywords + ")");
									}
									if (current10.Radius > 0)
									{
										list.Add(" &diams; Aura " + current10.Radius);
									}
									list.Add("</TD>");
									list.Add("</TR>");
									list.Add("<TR>");
									list.Add("<TD colspan=3>");
									list.Add(text19);
									list.Add("</TD>");
									list.Add("</TR>");
									if (mode == CardMode.Build)
									{
										list.Add("<TR>");
										list.Add("<TD colspan=3 align=center>");
										list.Add("<A href=auraedit:" + current10.ID + ">edit</A>");
										list.Add("|");
										list.Add("<A href=auraremove:" + current10.ID + ">remove</A>");
										list.Add("this aura");
										list.Add("</TD>");
										list.Add("</TR>");
									}
								}
								List<Regeneration> list4 = new List<Regeneration>();
								if (this.Regeneration != null)
								{
									list4.Add(this.Regeneration);
								}
								if (combat_data != null)
								{
									foreach (OngoingCondition current11 in combat_data.Conditions)
									{
										if (current11.Type == OngoingType.Regeneration)
										{
											list4.Add(current11.Regeneration);
										}
									}
								}
								foreach (Regeneration current12 in list4)
								{
									list.Add("<TR class=shaded>");
									list.Add("<TD colspan=3>");
									list.Add("<B>Regeneration</B>");
									list.Add("</TD>");
									list.Add("</TR>");
									list.Add("<TR>");
									list.Add("<TD colspan=3>");
									list.Add("Regeneration " + HTML.Process(current12.ToString(), true));
									list.Add("</TD>");
									list.Add("</TR>");
									if (mode == CardMode.Build)
									{
										list.Add("<TR>");
										list.Add("<TD colspan=3 align=center>");
										list.Add("<A href=power:regenedit>edit</A>");
										list.Add("|");
										list.Add("<A href=power:regenremove>remove</A>");
										list.Add("this trait");
										list.Add("</TD>");
										list.Add("</TR>");
									}
								}
							}
							foreach (CreaturePower current13 in dictionary[creaturePowerCategory])
							{
								CardMode mode2 = mode;
								if (mode == CardMode.Build)
								{
									mode2 = CardMode.View;
								}
								if (combat_data != null)
								{
									combat_data.UsedPowers.Contains(current13.ID);
								}
								list.AddRange(current13.AsHTML(combat_data, mode2, false));
								if (mode == CardMode.Build)
								{
									list.Add("<TR>");
									list.Add("<TD colspan=3 align=center>");
									list.Add("<A href=\"poweredit:" + current13.ID + "\">edit</A>");
									list.Add("|");
									list.Add("<A href=\"powerremove:" + current13.ID + "\">remove</A>");
									list.Add("|");
									list.Add("<A href=\"powerduplicate:" + current13.ID + "\">duplicate</A>");
									if (creaturePowerCategory == CreaturePowerCategory.Trait)
									{
										list.Add("this trait");
									}
									else
									{
										list.Add("this power");
									}
									list.Add("</TD>");
									list.Add("</TR>");
								}
							}
						}
					}
					string text20 = this.Skills;
					if (text20 != null && text20.ToLower().Contains("perception"))
					{
						string text21 = "";
						string[] array4 = text20.Split(new string[]
						{
							",",
							";"
						}, StringSplitOptions.RemoveEmptyEntries);
						string[] array2 = array4;
						for (int i = 0; i < array2.Length; i++)
						{
							string text22 = array2[i];
							if (!text22.ToLower().Contains("perception"))
							{
								if (text21 != "")
								{
									text21 += "; ";
								}
								text21 += text22;
							}
						}
						text20 = text21;
					}
					if (text20 == null)
					{
						text20 = "";
					}
					if (text20 == "" && mode == CardMode.Build)
					{
						text20 = "(none)";
					}
					if (text20 != "")
					{
						text20 = HTML.Process(text20, true);
						if (mode == CardMode.Build)
						{
							text20 = "<A href=build:skills>" + text20 + "</A>";
						}
						list.Add("<TR class=shaded>");
						list.Add("<TD colspan=3>");
						list.Add("<B>Skills</B> " + text20);
						list.Add("</TD>");
						list.Add("</TR>");
					}
					list.Add("<TR class=shaded>");
					list.Add("<TD>");
					list.Add("<B>Str</B>: " + this.ability(creature.Strength, mode));
					list.Add("<BR>");
					list.Add("<B>Con</B>: " + this.ability(creature.Constitution, mode));
					list.Add("</TD>");
					list.Add("<TD>");
					list.Add("<B>Dex</B>: " + this.ability(creature.Dexterity, mode));
					list.Add("<BR>");
					list.Add("<B>Int</B>: " + this.ability(creature.Intelligence, mode));
					list.Add("</TD>");
					list.Add("<TD>");
					list.Add("<B>Wis</B>: " + this.ability(creature.Wisdom, mode));
					list.Add("<BR>");
					list.Add("<B>Cha</B>: " + this.ability(creature.Charisma, mode));
					list.Add("</TD>");
					list.Add("</TR>");
					string text23 = creature.Alignment;
					if (text23 == null)
					{
						text23 = "";
					}
					if (text23 == "")
					{
						if (mode == CardMode.Build)
						{
							text23 = "(not set)";
						}
						else
						{
							text23 = "Unaligned";
						}
					}
					if (text23 != "")
					{
						text23 = HTML.Process(text23, true);
						if (mode == CardMode.Build)
						{
							text23 = "<A href=build:alignment>" + text23 + "</A>";
						}
						list.Add("<TR>");
						list.Add("<TD colspan=3>");
						list.Add("<B>Alignment</B> " + text23);
						list.Add("</TD>");
						list.Add("</TR>");
					}
					string text24 = creature.Languages;
					if (text24 == null)
					{
						text24 = "";
					}
					if (text24 == "" && mode == CardMode.Build)
					{
						text24 = "(none)";
					}
					if (text24 != "")
					{
						text24 = HTML.Process(text24, true);
						if (mode == CardMode.Build)
						{
							text24 = "<A href=build:languages>" + text24 + "</A>";
						}
						list.Add("<TR>");
						list.Add("<TD colspan=3>");
						list.Add("<B>Languages</B> " + text24);
						list.Add("</TD>");
						list.Add("</TR>");
					}
					string text25 = this.Equipment;
					if (text25 == null)
					{
						text25 = "";
					}
					if (text25 == "" && mode == CardMode.Build)
					{
						text25 = "(none)";
					}
					if (text25 != "")
					{
						text25 = HTML.Process(text25, true);
						if (mode == CardMode.Build)
						{
							text25 = "<A href=build:equipment>" + text25 + "</A>";
						}
						list.Add("<TR>");
						list.Add("<TD colspan=3>");
						list.Add("<B>Equipment</B> " + text25);
						list.Add("</TD>");
						list.Add("</TR>");
					}
					string text26 = this.Tactics;
					if (text26 == null)
					{
						text26 = "";
					}
					if (text26 == "" && mode == CardMode.Build)
					{
						text26 = "(none specified)";
					}
					if (text26 != "")
					{
						text26 = HTML.Process(text26, true);
						if (mode == CardMode.Build)
						{
							text26 = "<A href=build:tactics>" + text26 + "</A>";
						}
						list.Add("<TR>");
						list.Add("<TD colspan=3>");
						list.Add("<B>Tactics</B> " + text26);
						list.Add("</TD>");
						list.Add("</TR>");
					}
					Creature creature2 = creature as Creature;
					List<string> list5 = new List<string>();
					if (creature2 != null)
					{
						Library library = Session.FindLibrary(creature2);
						if (library != null && library.Name != "" && (Session.Project == null || library != Session.Project.Library))
						{
							string item = HTML.Process(library.Name, true);
							list5.Add(item);
						}
					}
					foreach (Guid current14 in this.fTemplateIDs)
					{
						CreatureTemplate creatureTemplate = Session.FindTemplate(current14, SearchType.Global);
						Library library2 = Session.FindLibrary(creatureTemplate);
						if (library2 != null && library2 != Session.Project.Library)
						{
							if (list5.Count != 0)
							{
								list5.Add("<BR>");
							}
							string str4 = HTML.Process(library2.Name, true);
							list5.Add(creatureTemplate.Name + " template: " + str4);
						}
					}
					if (list5.Count != 0)
					{
						list.Add("<TR class=shaded>");
						list.Add("<TD colspan=3>");
						foreach (string current15 in list5)
						{
							list.Add(current15);
						}
						list.Add("</TD>");
						list.Add("</TR>");
					}
					if (creature2 != null && creature2.URL != "")
					{
						list.Add("<TR>");
						list.Add("<TD colspan=3>");
						list.Add("Copyright <A href=\"" + creature2.URL + "\">Wizards of the Coast</A> 2010");
						list.Add("</TD>");
						list.Add("</TR>");
					}
				}
				if (mode != CardMode.Text)
				{
					list.Add("</TABLE>");
				}
				return list;
			}
			if (mode == CardMode.Text)
			{
				return new List<string>
				{
					"(unknown creature)"
				};
			}
			return new List<string>
			{
				"<TABLE>",
				"<TR class=creature>",
				"<TD>",
				"<B>(unknown creature)</B>",
				"</TD>",
				"</TR>",
				"<TR>",
				"<TD>",
				"No details",
				"</TD>",
				"</TR>",
				"</TABLE>"
			};
		}

		private string ability(Ability ab, CardMode mode)
		{
			if (ab == null)
			{
				return "-";
			}
			int num = ab.Modifier + this.Level / 2;
			string text = "";
			switch (mode)
			{
			case CardMode.Combat:
			{
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					"<A href=\"ability:",
					num,
					"\">"
				});
				break;
			}
			case CardMode.Build:
				text += "<A href=build:ability>";
				break;
			}
			text += ab.Score.ToString();
			text += " ";
			string text2 = num.ToString();
			if (num >= 0)
			{
				text2 = "+" + text2;
			}
			text = text + "(" + text2 + ")";
			switch (mode)
			{
			case CardMode.Combat:
				text += "</A>";
				break;
			case CardMode.Build:
				text += "</A>";
				break;
			}
			return text;
		}
	}
}
