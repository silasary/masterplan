using Masterplan.Data;
using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Tools
{
	internal class CreatureHelper
	{
		public static void CopyFields(ICreature copy_from, ICreature copy_to)
		{
			try
			{
				if (copy_from != null)
				{
					copy_to.ID = copy_from.ID;
					copy_to.Name = copy_from.Name;
					copy_to.Details = copy_from.Details;
					copy_to.Size = copy_from.Size;
					copy_to.Origin = copy_from.Origin;
					copy_to.Type = copy_from.Type;
					copy_to.Keywords = copy_from.Keywords;
					copy_to.Level = copy_from.Level;
					copy_to.Role = ((copy_from.Role != null) ? copy_from.Role.Copy() : null);
					copy_to.Senses = copy_from.Senses;
					copy_to.Movement = copy_from.Movement;
					copy_to.Alignment = copy_from.Alignment;
					copy_to.Languages = copy_from.Languages;
					copy_to.Skills = copy_from.Skills;
					copy_to.Equipment = copy_from.Equipment;
					copy_to.Category = copy_from.Category;
					copy_to.Strength = copy_from.Strength.Copy();
					copy_to.Constitution = copy_from.Constitution.Copy();
					copy_to.Dexterity = copy_from.Dexterity.Copy();
					copy_to.Intelligence = copy_from.Intelligence.Copy();
					copy_to.Wisdom = copy_from.Wisdom.Copy();
					copy_to.Charisma = copy_from.Charisma.Copy();
					copy_to.HP = copy_from.HP;
					copy_to.Initiative = copy_from.Initiative;
					copy_to.AC = copy_from.AC;
					copy_to.Fortitude = copy_from.Fortitude;
					copy_to.Reflex = copy_from.Reflex;
					copy_to.Will = copy_from.Will;
					copy_to.Regeneration = ((copy_from.Regeneration != null) ? copy_from.Regeneration.Copy() : null);
					copy_to.Auras.Clear();
					foreach (Aura current in copy_from.Auras)
					{
						copy_to.Auras.Add(current.Copy());
					}
					copy_to.CreaturePowers.Clear();
					foreach (CreaturePower current2 in copy_from.CreaturePowers)
					{
						copy_to.CreaturePowers.Add(current2.Copy());
					}
					copy_to.DamageModifiers.Clear();
					foreach (DamageModifier current3 in copy_from.DamageModifiers)
					{
						copy_to.DamageModifiers.Add(current3.Copy());
					}
					copy_to.Resist = copy_from.Resist;
					copy_to.Vulnerable = copy_from.Vulnerable;
					copy_to.Immune = copy_from.Immune;
					copy_to.Tactics = copy_from.Tactics;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		public static void UpdateRegen(ICreature c)
		{
			Aura aura = CreatureHelper.FindAura(c, "Regeneration");
			if (aura == null)
			{
				aura = CreatureHelper.FindAura(c, "Regen");
			}
			if (aura != null)
			{
				Regeneration regeneration = CreatureHelper.ConvertAura(aura.Details);
				if (regeneration != null)
				{
					c.Regeneration = regeneration;
					c.Auras.Remove(aura);
				}
			}
		}

		public static void UpdatePowerRange(ICreature c, CreaturePower power)
		{
			if (power.Range != null && power.Range != "")
			{
				return;
			}
			List<string> list = new List<string>();
			list.Add("close blast");
			list.Add("close burst");
			list.Add("area burst");
			list.Add("melee");
			list.Add("ranged");
			string text = "";
			string[] array = power.Details.Split(new string[]
			{
				";"
			}, StringSplitOptions.RemoveEmptyEntries);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text2 = array2[i];
				bool flag = false;
				foreach (string current in list)
				{
					if (text2.ToLower().Contains(current))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					power.Range = text2;
				}
				else
				{
					if (text != "")
					{
						text += "; ";
					}
					text += text2;
				}
			}
			power.Details = text;
		}

		public static Aura FindAura(ICreature c, string name)
		{
			foreach (Aura current in c.Auras)
			{
				if (current.Name == name)
				{
					return current;
				}
			}
			return null;
		}

		public static Regeneration ConvertAura(string aura_details)
		{
			aura_details = aura_details.Trim();
			bool flag = true;
			string text = "";
			string text2 = "";
			string text3 = aura_details;
			for (int i = 0; i < text3.Length; i++)
			{
				char c = text3[i];
				if (!char.IsDigit(c))
				{
					flag = false;
				}
				if (flag)
				{
					text += c;
				}
				else
				{
					text2 += c;
				}
			}
			text2 = text2.Trim();
			if (text2.StartsWith("(") && text2.EndsWith(")"))
			{
				text2 = text2.Substring(1);
				text2 = text2.Substring(0, text2.Length - 1);
				text2.Trim();
			}
			Regeneration result;
			try
			{
				int value = (text != "") ? int.Parse(text) : 0;
				result = new Regeneration(value, text2);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
				result = null;
			}
			return result;
		}

		public static List<CreaturePower> CreaturePowersByCategory(ICreature c, CreaturePowerCategory category)
		{
			List<CreaturePower> list = new List<CreaturePower>();
			foreach (CreaturePower current in c.CreaturePowers)
			{
				if (current.Category == category)
				{
					list.Add(current);
				}
			}
			return list;
		}

		public static void AdjustCreatureLevel(ICreature creature, int delta)
		{
			if (creature.Role is ComplexRole)
			{
				ComplexRole complexRole = creature.Role as ComplexRole;
				int num = 8;
				switch (complexRole.Type)
				{
				case RoleType.Artillery:
				case RoleType.Lurker:
					num = 6;
					break;
				case RoleType.Brute:
					num = 10;
					break;
				}
				switch (complexRole.Flag)
				{
				case RoleFlag.Elite:
					num *= 2;
					break;
				case RoleFlag.Solo:
					num *= 5;
					break;
				}
				creature.HP += num * delta;
				creature.HP = Math.Max(creature.HP, 1);
			}
			int num2 = creature.Initiative - creature.Level / 2;
			creature.Initiative = num2 + (creature.Level + delta) / 2;
			creature.AC += delta;
			creature.Fortitude += delta;
			creature.Reflex += delta;
			creature.Will += delta;
			foreach (CreaturePower current in creature.CreaturePowers)
			{
				CreatureHelper.AdjustPowerLevel(current, delta);
			}
			if (creature.Skills != "")
			{
				Dictionary<string, int> dictionary = CreatureHelper.ParseSkills(creature.Skills);
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
					int num3 = dictionary[current3];
					int num4 = num3 - creature.Level / 2;
					num3 = num4 + (creature.Level + delta) / 2;
					if (num3 >= 0)
					{
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							current3,
							" +",
							num3
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
							num3
						});
					}
				}
				creature.Skills = text;
			}
			creature.Level += delta;
		}

		public static void AdjustPowerLevel(CreaturePower cp, int delta)
		{
			if (cp.Attack != null)
			{
				cp.Attack.Bonus += delta;
			}
			string text = AI.ExtractDamage(cp.Details);
			if (text != "")
			{
				DiceExpression diceExpression = DiceExpression.Parse(text);
				if (diceExpression != null)
				{
					DiceExpression diceExpression2 = diceExpression.Adjust(delta);
					if (diceExpression2 != null && diceExpression.ToString() != diceExpression2.ToString())
					{
						cp.Details = cp.Details.Replace(text, diceExpression2 + " damage");
					}
				}
			}
		}

		public static Dictionary<string, int> ParseSkills(string source)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			if (source != null && source != "")
			{
				string[] array = source.Split(new string[]
				{
					",",
					";"
				}, StringSplitOptions.RemoveEmptyEntries);
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i];
					string text2 = text.Trim();
					int num = text2.IndexOf(" ");
					if (num != -1)
					{
						string key = text2.Substring(0, num);
						string s = text2.Substring(num + 1);
						int value = 0;
						try
						{
							value = int.Parse(s);
						}
						catch
						{
							value = 0;
						}
						dictionary[key] = value;
					}
				}
			}
			return dictionary;
		}
	}
}
