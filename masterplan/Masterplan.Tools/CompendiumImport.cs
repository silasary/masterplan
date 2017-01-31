using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.Xml;
using Utils;

namespace Masterplan.Tools
{
	internal class CompendiumImport
	{
		private static string simplify_html(string source)
		{
			int num = source.IndexOf("<div id=\"detail\">", StringComparison.OrdinalIgnoreCase);
			if (num == -1)
			{
				return "";
			}
			int num2 = source.IndexOf("</div>", num) + 6;
			source = source.Substring(num, num2 - num);
			source = source.Replace("<br>", "<br/>");
			source = source.Replace("<BR>", "<BR/>");
			source = source.Replace("&nbsp;", " ");
			while (true)
			{
				string text = "href=\"";
				int num3 = source.IndexOf(text);
				if (num3 == -1)
				{
					break;
				}
				int num4 = source.IndexOf("\"", num3 + text.Length);
				int length = num4 - num3 + 1;
				string oldValue = source.Substring(num3, length);
				source = source.Replace(oldValue, "");
			}
			return source;
		}

		public static Creature ImportCreatureFromHTML(string html, string url)
		{
			Creature creature = null;
			try
			{
				html = CompendiumImport.simplify_html(html);
				XmlDocument xmlDocument = XMLHelper.LoadSource(html);
				if (xmlDocument == null)
				{
					return null;
				}
				creature = new Creature();
				creature.URL = url;
				XmlNode xmlNode = xmlDocument.DocumentElement.FirstChild;
				try
				{
					CompendiumImport.handle_title_section(xmlNode, creature);
				}
				catch
				{
				}
				xmlNode = xmlNode.NextSibling;
				try
				{
					CompendiumImport.handle_combat_section(xmlNode, creature);
				}
				catch
				{
				}
				while (true)
				{
					xmlNode = xmlNode.NextSibling;
					XmlNode nextSibling = xmlNode.NextSibling;
					XmlAttribute xmlAttribute = nextSibling.Attributes["class"];
					if (xmlAttribute == null || xmlAttribute.Value != "flavorIndent")
					{
						break;
					}
					CreaturePower creaturePower = null;
					try
					{
						creaturePower = CompendiumImport.parse_power(xmlNode);
						while (true)
						{
							XmlAttribute xmlAttribute2 = xmlNode.NextSibling.Attributes["class"];
							if (xmlAttribute2 != null && xmlAttribute2.Value == "flavor alt")
							{
								break;
							}
							xmlNode = xmlNode.NextSibling;
							if (creaturePower.Details != "")
							{
								CreaturePower expr_E6 = creaturePower;
								expr_E6.Details += Environment.NewLine;
							}
							CreaturePower expr_FD = creaturePower;
							expr_FD.Details += xmlNode.FirstChild.Value;
						}
						creaturePower.ExtractAttackDetails();
					}
					catch
					{
					}
					if (creaturePower != null)
					{
						creature.CreaturePowers.Add(creaturePower);
					}
				}
				try
				{
					CompendiumImport.handle_end_section(xmlNode, creature);
				}
				catch
				{
				}
				xmlNode = xmlNode.NextSibling;
				if (xmlNode.FirstChild != null)
				{
					if (xmlNode.FirstChild.FirstChild.Value == "Equipment")
					{
						string text = "";
						for (XmlNode nextSibling2 = xmlNode.FirstChild.NextSibling; nextSibling2 != null; nextSibling2 = nextSibling2.NextSibling)
						{
							if (nextSibling2.FirstChild != null)
							{
								if (text != "")
								{
									text += "; ";
								}
								text += nextSibling2.FirstChild.Value.Trim();
							}
						}
						creature.Equipment = text;
					}
					else if (xmlNode.FirstChild.FirstChild.Value == "Description")
					{
						XmlNode nextSibling3 = xmlNode.FirstChild.NextSibling;
						if (nextSibling3 != null)
						{
							string text2 = nextSibling3.Value;
							if (text2.StartsWith(":"))
							{
								text2 = text2.Substring(1);
							}
							text2 = text2.Trim();
							creature.Details = text2;
						}
					}
				}
			}
			catch
			{
				Console.WriteLine("Problem with creature: " + creature.Name);
				creature = null;
			}
			return creature;
		}

		private static void handle_title_section(XmlNode node, Creature c)
		{
			string innerText = node.FirstChild.InnerText;
			string text = node.FirstChild.NextSibling.NextSibling.FirstChild.InnerText;
			string innerText2 = node.FirstChild.NextSibling.NextSibling.NextSibling.NextSibling.FirstChild.InnerText;
			c.Name = innerText.Trim();
			int level = 0;
			bool flag = false;
			bool leader = false;
			RoleFlag flag2 = RoleFlag.Standard;
			RoleType type = RoleType.Artillery;
			bool hasRole = false;
			string[] array = innerText2.Split(null);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text2 = array2[i];
				if (!(text2 == ""))
				{
					if (text2.ToLower() == "minion")
					{
						flag = true;
					}
					else if (text2.ToLower() == "(leader)")
					{
						leader = true;
					}
					else
					{
						try
						{
							level = int.Parse(text2);
							goto IL_137;
						}
						catch
						{
						}
						try
						{
							if (text2 != level.ToString())
							{
								flag2 = (RoleFlag)Enum.Parse(typeof(RoleFlag), text2);
								goto IL_137;
							}
						}
						catch
						{
						}
						try
						{
							if (text2 != level.ToString())
							{
								type = (RoleType)Enum.Parse(typeof(RoleType), text2);
								hasRole = true;
							}
						}
						catch
						{
						}
					}
				}
				IL_137:;
			}
			c.Level = level;
			if (flag)
			{
				Minion minion = new Minion();
				minion.HasRole = hasRole;
				if (minion.HasRole)
				{
					minion.Type = type;
				}
				c.Role = minion;
			}
			else
			{
				c.Role = new ComplexRole
				{
					Type = type,
					Flag = flag2,
					Leader = leader
				};
			}
			int num = text.IndexOf("(");
			int num2 = text.IndexOf(")");
			if (num != -1 && num2 != -1)
			{
				int length = num2 - (num + 1);
				string text3 = text.Substring(num + 1, length);
				text = text.Replace(text3, "");
				c.Keywords = text3;
			}
			int num3 = text.IndexOf(",");
			if (num3 == -1)
			{
				num3 = text.IndexOf(";");
			}
			if (num3 != -1)
			{
				string str = text.Substring(num3 + 1).Trim();
				text = text.Substring(0, num3);
				if (c.Keywords != "")
				{
					c.Keywords += "; ";
				}
				c.Keywords += str;
			}
			string[] array3 = text.Split(null);
			string[] array4 = array3;
			for (int j = 0; j < array4.Length; j++)
			{
				string text4 = array4[j];
				if (!(text4 == ""))
				{
					string value = char.ToUpper(text4[0]) + text4.Substring(1);
					try
					{
						c.Size = (CreatureSize)Enum.Parse(typeof(CreatureSize), value);
						goto IL_325;
					}
					catch
					{
					}
					try
					{
						c.Origin = (CreatureOrigin)Enum.Parse(typeof(CreatureOrigin), value);
						goto IL_325;
					}
					catch
					{
					}
					try
					{
						c.Type = (CreatureType)Enum.Parse(typeof(CreatureType), value);
					}
					catch
					{
					}
				}
				IL_325:;
			}
		}

		private static void handle_combat_section(XmlNode node, Creature c)
		{
			XmlNode nextSibling = node.FirstChild.NextSibling;
			c.Initiative = CompendiumImport.get_score(nextSibling);
			XmlNode nextSibling2 = nextSibling.NextSibling.NextSibling;
			c.Senses = nextSibling2.Value.Trim();
			XmlNode xmlNode = nextSibling2;
			while (true)
			{
				xmlNode = xmlNode.NextSibling.NextSibling;
				if (xmlNode.FirstChild.Value == "HP")
				{
					break;
				}
				Aura aura = new Aura();
				aura.Name = xmlNode.FirstChild.Value;
				aura.Details = xmlNode.NextSibling.Value;
				c.Auras.Add(aura);
				xmlNode = xmlNode.NextSibling;
			}
			XmlNode nextSibling3 = xmlNode.NextSibling;
			c.HP = CompendiumImport.get_score(nextSibling3);
			XmlNode nextSibling4 = nextSibling3.NextSibling.NextSibling.NextSibling.NextSibling;
			if (nextSibling4.FirstChild.Value == "Regeneration")
			{
				nextSibling4 = nextSibling4.NextSibling;
				Regeneration regeneration = CreatureHelper.ConvertAura(nextSibling4.Value);
				if (regeneration != null)
				{
					c.Regeneration = regeneration;
				}
				xmlNode = nextSibling4;
			}
			else
			{
				xmlNode = nextSibling3;
			}
			while (xmlNode.FirstChild == null || !(xmlNode.FirstChild.Value == "AC"))
			{
				xmlNode = xmlNode.NextSibling;
			}
			XmlNode nextSibling5 = xmlNode.NextSibling;
			c.AC = CompendiumImport.get_score(nextSibling5);
			XmlNode nextSibling6 = nextSibling5.NextSibling.NextSibling;
			c.Fortitude = CompendiumImport.get_score(nextSibling6);
			XmlNode nextSibling7 = nextSibling6.NextSibling.NextSibling;
			c.Reflex = CompendiumImport.get_score(nextSibling7);
			XmlNode nextSibling8 = nextSibling7.NextSibling.NextSibling;
			c.Will = CompendiumImport.get_score(nextSibling8);
			xmlNode = nextSibling8.NextSibling;
			XmlNode xmlNode2 = null;
			XmlNode xmlNode3 = null;
			XmlNode xmlNode4 = null;
			XmlNode xmlNode5 = null;
			while (true)
			{
				xmlNode = xmlNode.NextSibling;
				if (xmlNode == null)
				{
					break;
				}
				XmlNode firstChild = xmlNode.FirstChild;
				if (firstChild != null)
				{
					string a = firstChild.Value.Trim();
					if (a == "Immune")
					{
						xmlNode2 = xmlNode.NextSibling;
					}
					if (a == "Resist")
					{
						xmlNode3 = xmlNode.NextSibling;
					}
					if (a == "Vulnerable")
					{
						xmlNode4 = xmlNode.NextSibling;
					}
					if (a == "Saving Throws")
					{
						XmlNode nextSibling9 = xmlNode.NextSibling;
					}
					if (a == "Speed")
					{
						xmlNode5 = xmlNode.NextSibling;
					}
					if (a == "Action Points")
					{
						XmlNode nextSibling10 = xmlNode.NextSibling;
					}
				}
			}
			if (xmlNode2 != null)
			{
				string text = xmlNode2.Value.Trim();
				string[] array = text.Split(new string[]
				{
					",",
					";"
				}, StringSplitOptions.RemoveEmptyEntries);
				string text2 = "";
				string[] array2 = array;
				int i = 0;
				while (i < array2.Length)
				{
					string text3 = array2[i];
					string text4 = text3.Trim();
					int num = text4.IndexOf(" ");
					if (num != -1)
					{
						try
						{
							string s = text4.Substring(0, num);
							int.Parse(s);
							string damage_type = text4.Substring(num + 1);
							DamageModifier damageModifier = DamageModifier.Parse(damage_type, 0);
							if (damageModifier != null)
							{
								c.DamageModifiers.Add(damageModifier);
								goto IL_33D;
							}
						}
						catch
						{
						}
						goto IL_316;
					}
					goto IL_316;
					IL_33D:
					i++;
					continue;
					IL_316:
					if (text2 != "")
					{
						text2 += ", ";
					}
					text2 += text3;
					goto IL_33D;
				}
				c.Immune = text2;
			}
			if (xmlNode3 != null)
			{
				string text5 = xmlNode3.Value.Trim();
				string[] array3 = text5.Split(new string[]
				{
					",",
					";"
				}, StringSplitOptions.RemoveEmptyEntries);
				string text6 = "";
				string[] array4 = array3;
				int j = 0;
				while (j < array4.Length)
				{
					string text7 = array4[j];
					string text8 = text7.Trim();
					int num2 = text8.IndexOf(" ");
					if (num2 != -1)
					{
						try
						{
							string s2 = text8.Substring(0, num2);
							int num3 = int.Parse(s2);
							string damage_type2 = text8.Substring(num2 + 1);
							DamageModifier damageModifier2 = DamageModifier.Parse(damage_type2, -num3);
							if (damageModifier2 != null)
							{
								c.DamageModifiers.Add(damageModifier2);
								goto IL_434;
							}
						}
						catch
						{
						}
						goto IL_40D;
					}
					goto IL_40D;
					IL_434:
					j++;
					continue;
					IL_40D:
					if (text6 != "")
					{
						text6 += ", ";
					}
					text6 += text7;
					goto IL_434;
				}
				c.Resist = text6;
			}
			if (xmlNode4 != null)
			{
				string text9 = xmlNode4.Value.Trim();
				string[] array5 = text9.Split(new string[]
				{
					",",
					";"
				}, StringSplitOptions.RemoveEmptyEntries);
				string text10 = "";
				string[] array6 = array5;
				int k = 0;
				while (k < array6.Length)
				{
					string text11 = array6[k];
					string text12 = text11.Trim();
					int num4 = text12.IndexOf(" ");
					if (num4 != -1)
					{
						try
						{
							string s3 = text12.Substring(0, num4);
							int value = int.Parse(s3);
							string damage_type3 = text12.Substring(num4 + 1);
							DamageModifier damageModifier3 = DamageModifier.Parse(damage_type3, value);
							if (damageModifier3 != null)
							{
								c.DamageModifiers.Add(damageModifier3);
								goto IL_52A;
							}
						}
						catch
						{
						}
						goto IL_503;
					}
					goto IL_503;
					IL_52A:
					k++;
					continue;
					IL_503:
					if (text10 != "")
					{
						text10 += ", ";
					}
					text10 += text11;
					goto IL_52A;
				}
				c.Vulnerable = text10;
			}
			if (xmlNode5 != null)
			{
				string movement = xmlNode5.Value.Trim();
				c.Movement = movement;
			}
		}

		private static void handle_end_section(XmlNode node, Creature c)
		{
			XmlNode nextSibling = node.FirstChild.NextSibling;
			c.Alignment = nextSibling.Value.Trim();
			XmlNode nextSibling2 = nextSibling.NextSibling.NextSibling;
			string text = nextSibling2.Value.Trim();
			text = text.Replace("-", "");
			c.Languages = text;
			XmlNode xmlNode = nextSibling2;
			if (nextSibling2.NextSibling.NextSibling.FirstChild.Value == "Skills")
			{
				XmlNode nextSibling3 = nextSibling2.NextSibling.NextSibling.NextSibling;
				c.Skills = nextSibling3.Value.Trim();
				xmlNode = nextSibling3;
			}
			XmlNode nextSibling4 = xmlNode.NextSibling.NextSibling.NextSibling;
			c.Strength.Score = CompendiumImport.get_score(nextSibling4);
			XmlNode nextSibling5 = nextSibling4.NextSibling.NextSibling;
			c.Dexterity.Score = CompendiumImport.get_score(nextSibling5);
			XmlNode nextSibling6 = nextSibling5.NextSibling.NextSibling;
			c.Wisdom.Score = CompendiumImport.get_score(nextSibling6);
			XmlNode nextSibling7 = nextSibling6.NextSibling.NextSibling.NextSibling;
			c.Constitution.Score = CompendiumImport.get_score(nextSibling7);
			XmlNode nextSibling8 = nextSibling7.NextSibling.NextSibling;
			c.Intelligence.Score = CompendiumImport.get_score(nextSibling8);
			XmlNode nextSibling9 = nextSibling8.NextSibling.NextSibling;
			c.Charisma.Score = CompendiumImport.get_score(nextSibling9);
		}

		private static CreaturePower parse_power(XmlNode node)
		{
			CreaturePower creaturePower = new CreaturePower();
			foreach (XmlNode xmlNode in node.ChildNodes)
			{
				if (xmlNode.Name == "b")
				{
					creaturePower.Name = xmlNode.FirstChild.Value.Trim();
					break;
				}
			}
			string text = "";
			bool flag = false;
			foreach (XmlNode xmlNode2 in node.ChildNodes)
			{
				if (xmlNode2.Name == "img")
				{
					XmlAttribute xmlAttribute = xmlNode2.Attributes["src"];
					string value = xmlAttribute.Value;
					if (value.EndsWith("S2.gif"))
					{
						creaturePower.Action = new PowerAction();
						creaturePower.Action.Use = PowerUseType.Basic;
						flag = true;
					}
					else if (value.EndsWith("x.gif"))
					{
						XmlNode firstChild = xmlNode2.NextSibling.FirstChild;
						creaturePower.Keywords = firstChild.Value;
					}
					else if (!value.EndsWith("Z1a.gif") && !value.EndsWith("Z2a.gif") && !value.EndsWith("Z3a.gif") && !value.EndsWith("Z4a.gif"))
					{
						text = text + " " + value.Substring(value.Length - 6, 1);
					}
				}
				if (xmlNode2.Name == "#text")
				{
					string str = xmlNode2.Value.Trim();
					text += str;
				}
			}
			if (text != "")
			{
				text = text.Trim();
				text = text.Substring(1, text.Length - 2);
				text = text.Trim();
				text = text.Replace(",", ";");
				List<string> list = new List<string>(text.Split(new string[]
				{
					";"
				}, StringSplitOptions.RemoveEmptyEntries));
				if (list.Count != 0)
				{
					for (int num = 0; num != list.Count; num++)
					{
						list[num] = list[num].Trim();
					}
					string text2 = list[0].ToLower();
					if (!text2.StartsWith("standard") && !text2.StartsWith("move") && !text2.StartsWith("minor") && !text2.StartsWith("free") && !text2.StartsWith("immediate"))
					{
						creaturePower.Condition = list[0];
						list.RemoveAt(0);
					}
					if (list.Count != 0 && creaturePower.Action == null)
					{
						creaturePower.Action = new PowerAction();
						creaturePower.Action.Action = ActionType.None;
					}
					for (int num2 = 0; num2 != list.Count; num2++)
					{
						string text3 = list[num2];
						string text4 = text3.ToLower();
						if (text4.StartsWith("standard"))
						{
							creaturePower.Action.Action = ActionType.Standard;
						}
						else if (text4.StartsWith("move"))
						{
							creaturePower.Action.Action = ActionType.Move;
						}
						else if (text4.StartsWith("minor"))
						{
							creaturePower.Action.Action = ActionType.Minor;
						}
						else if (text4.StartsWith("free"))
						{
							creaturePower.Action.Action = ActionType.Free;
						}
						else if (text4 == "immediate interrupt")
						{
							creaturePower.Action.Action = ActionType.Interrupt;
						}
						else if (text4 == "immediate reaction")
						{
							creaturePower.Action.Action = ActionType.Reaction;
						}
						else if (text4 == "at-will")
						{
							if (!flag)
							{
								creaturePower.Action.Use = PowerUseType.AtWill;
							}
						}
						else if (text4 == "encounter")
						{
							creaturePower.Action.Use = PowerUseType.Encounter;
						}
						else if (text4 == "daily")
						{
							creaturePower.Action.Use = PowerUseType.Daily;
						}
						else
						{
							string text5 = "recharge ";
							if (text4.StartsWith(text5))
							{
								creaturePower.Action.Use = PowerUseType.Encounter;
								string a = text3.Substring(text5.Length);
								if (a == "6")
								{
									creaturePower.Action.Recharge = "Recharges on 6";
								}
								else if (a == "5 6")
								{
									creaturePower.Action.Recharge = "Recharges on 5-6";
								}
								else if (a == "4 5 6")
								{
									creaturePower.Action.Recharge = "Recharges on 4-6";
								}
								else if (a == "3 4 5 6")
								{
									creaturePower.Action.Recharge = "Recharges on 3-6";
								}
								else if (a == "2 3 4 5 6")
								{
									creaturePower.Action.Recharge = "Recharges on 2-6";
								}
								else
								{
									creaturePower.Action.Recharge = text3;
								}
							}
							else
							{
								string value2 = "recharge";
								if (text4.StartsWith(value2))
								{
									creaturePower.Action.Recharge = text3;
								}
								else
								{
									string text6 = "sustain ";
									if (text4.StartsWith(text6))
									{
										try
										{
											string text7 = text3.Substring(text6.Length);
											text7 = char.ToUpper(text7[0]) + text7.Substring(1);
											ActionType sustainAction = (ActionType)Enum.Parse(typeof(ActionType), text7);
											creaturePower.Action.SustainAction = sustainAction;
											goto IL_5DA;
										}
										catch
										{
											goto IL_5DA;
										}
									}
									if (text4.StartsWith("when") || text4.StartsWith("if"))
									{
										creaturePower.Action.Trigger = text3;
									}
									else
									{
										creaturePower.Condition = text3;
									}
								}
							}
						}
						IL_5DA:;
					}
				}
			}
			return creaturePower;
		}

		private static int get_score(XmlNode node)
		{
			string text = "";
			bool flag = false;
			string value = node.Value;
			for (int i = 0; i < value.Length; i++)
			{
				char c = value[i];
				if (char.IsNumber(c))
				{
					flag = true;
					text += c;
				}
				else if (flag)
				{
					break;
				}
			}
			try
			{
				int num = int.Parse(text);
				int result = num;
				return result;
			}
			catch
			{
			}
			string value2 = node.Value;
			string[] array = value2.Split(new string[0], StringSplitOptions.RemoveEmptyEntries);
			try
			{
				string s = array[0];
				int num2 = int.Parse(s);
				int result = num2;
				return result;
			}
			catch
			{
			}
			return 0;
		}

		private static ActionType parse_action(string str)
		{
			if (str == "standard" || str == "standard action")
			{
				return ActionType.Standard;
			}
			if (str == "move" || str == "move action")
			{
				return ActionType.Move;
			}
			if (str == "minor" || str == "minor action")
			{
				return ActionType.Minor;
			}
			if (str == "free" || str == "free action")
			{
				return ActionType.Free;
			}
			if (str == "immediate interrupt")
			{
				return ActionType.Interrupt;
			}
			if (str == "immediate reaction")
			{
				return ActionType.Reaction;
			}
			return ActionType.Standard;
		}

		public static Trap ImportTrapFromHTML(string html, string url)
		{
			Trap trap = null;
			try
			{
				string xml = CompendiumImport.simplify_html(html);
				XmlDocument xmlDocument = XMLHelper.LoadSource(xml);
				if (xmlDocument == null)
				{
					return null;
				}
				trap = new Trap();
				trap.URL = url;
				if (xmlDocument.InnerText.ToLower().Contains("hazard"))
				{
					trap.Type = TrapType.Hazard;
				}
				else
				{
					trap.Type = TrapType.Trap;
				}
				XmlNode xmlNode = xmlDocument.DocumentElement.FirstChild;
				try
				{
					CompendiumImport.handle_title_section(xmlNode, trap);
				}
				catch
				{
				}
				string text = xmlNode.NextSibling.InnerText.ToLower();
				if (!text.StartsWith("trap") && !text.StartsWith("hazard"))
				{
					xmlNode = xmlNode.NextSibling;
					trap.ReadAloud = xmlNode.InnerText;
				}
				if (xmlNode.FirstChild.NextSibling != null)
				{
					xmlNode = xmlNode.NextSibling;
					trap.Details = xmlNode.FirstChild.NextSibling.InnerText;
				}
				try
				{
					xmlNode = xmlNode.NextSibling;
					while (true)
					{
						string innerText = xmlNode.InnerText;
						if (innerText.StartsWith("Trap") || innerText.StartsWith("Hazard"))
						{
							xmlNode = xmlNode.NextSibling;
						}
						else
						{
							if (innerText.StartsWith("Trigger") || innerText.StartsWith("Initiative") || innerText.StartsWith("Target"))
							{
								break;
							}
							TrapSkillData trapSkillData = new TrapSkillData();
							trapSkillData.SkillName = innerText;
							trapSkillData.DC = 0;
							foreach (XmlNode xmlNode2 in xmlNode.NextSibling.ChildNodes)
							{
								string text2 = xmlNode2.InnerText;
								if (text2.StartsWith("DC "))
								{
									text2 = text2.Substring(3);
									int num = text2.IndexOf(":");
									string s = text2.Substring(0, num);
									try
									{
										trapSkillData.DC = int.Parse(s);
									}
									catch
									{
									}
									text2 = text2.Substring(num + 1);
									text2 = text2.Trim();
								}
								if (text2 != "")
								{
									if (trapSkillData.Details != "")
									{
										TrapSkillData expr_1FD = trapSkillData;
										expr_1FD.Details += Environment.NewLine;
									}
									TrapSkillData expr_214 = trapSkillData;
									expr_214.Details += text2;
								}
							}
							trap.Skills.Add(trapSkillData);
							xmlNode = xmlNode.NextSibling.NextSibling;
						}
					}
				}
				catch
				{
				}
				if (xmlNode.InnerText == "Initiative")
				{
					try
					{
						string innerText2 = xmlNode.FirstChild.NextSibling.InnerText;
						int initiative = int.Parse(innerText2);
						trap.Attack.HasInitiative = true;
						trap.Attack.Initiative = initiative;
					}
					catch
					{
					}
				}
				xmlNode = xmlNode.NextSibling;
				if (xmlNode.FirstChild != null && xmlNode.FirstChild.InnerText == "Trigger")
				{
					while (xmlNode.NextSibling.FirstChild == null)
					{
						xmlNode = xmlNode.NextSibling;
					}
					trap.Attack.Trigger = xmlNode.NextSibling.FirstChild.InnerText;
				}
				xmlNode = xmlNode.NextSibling.NextSibling;
				while (true)
				{
					xmlNode = xmlNode.NextSibling;
					if (xmlNode.Name.ToLower() == "p")
					{
						break;
					}
					string text3 = xmlNode.FirstChild.InnerText.ToLower();
					if (text3.StartsWith("countermeasure"))
					{
						break;
					}
					if (text3.StartsWith("target"))
					{
						trap.Attack.Target = xmlNode.FirstChild.NextSibling.InnerText;
					}
					else if (text3.StartsWith("attack"))
					{
						if (xmlNode.FirstChild.NextSibling != null)
						{
							string innerText3 = xmlNode.FirstChild.NextSibling.InnerText;
							string[] array = innerText3.Split(null);
							int bonus = 0;
							DefenceType defence = DefenceType.AC;
							try
							{
								bonus = int.Parse(array[0]);
								defence = (DefenceType)Enum.Parse(typeof(DefenceType), array[2]);
							}
							catch
							{
							}
							trap.Attack.Attack.Bonus = bonus;
							trap.Attack.Attack.Defence = defence;
						}
					}
					else if (text3.StartsWith("hit"))
					{
						if (xmlNode.FirstChild.NextSibling != null)
						{
							trap.Attack.OnHit = xmlNode.FirstChild.NextSibling.InnerText;
						}
						else
						{
							foreach (XmlNode xmlNode3 in xmlNode.NextSibling.ChildNodes)
							{
								if (trap.Attack.OnHit != "")
								{
									TrapAttack expr_49C = trap.Attack;
									expr_49C.OnHit += Environment.NewLine;
								}
								TrapAttack expr_4B7 = trap.Attack;
								expr_4B7.OnHit += xmlNode3.InnerText;
							}
							xmlNode = xmlNode.NextSibling;
						}
					}
					else if (text3.StartsWith("miss"))
					{
						if (xmlNode.FirstChild.NextSibling != null)
						{
							trap.Attack.OnMiss = xmlNode.FirstChild.NextSibling.InnerText;
						}
						else
						{
							foreach (XmlNode xmlNode4 in xmlNode.NextSibling.ChildNodes)
							{
								if (trap.Attack.OnMiss != "")
								{
									TrapAttack expr_577 = trap.Attack;
									expr_577.OnMiss += Environment.NewLine;
								}
								TrapAttack expr_592 = trap.Attack;
								expr_592.OnMiss += xmlNode4.InnerText;
							}
							xmlNode = xmlNode.NextSibling;
						}
					}
					else if (text3.StartsWith("effect"))
					{
						if (xmlNode.FirstChild.NextSibling != null)
						{
							trap.Attack.Effect = xmlNode.FirstChild.NextSibling.InnerText;
						}
						else
						{
							foreach (XmlNode xmlNode5 in xmlNode.NextSibling.ChildNodes)
							{
								if (trap.Attack.Effect != "")
								{
									TrapAttack expr_652 = trap.Attack;
									expr_652.Effect += Environment.NewLine;
								}
								TrapAttack expr_66D = trap.Attack;
								expr_66D.Effect += xmlNode5.InnerText;
							}
							xmlNode = xmlNode.NextSibling;
						}
					}
					else if (text3.Contains(":"))
					{
						if (xmlNode.FirstChild.NextSibling != null)
						{
							string innerText4 = xmlNode.FirstChild.NextSibling.InnerText;
							trap.Details = text3 + innerText4 + Environment.NewLine + trap.Details;
						}
					}
					else
					{
						XmlNode xmlNode6 = xmlNode.FirstChild;
						trap.Attack.Action = CompendiumImport.parse_action(xmlNode6.InnerText);
						xmlNode6 = xmlNode6.NextSibling;
						if (xmlNode6 != null)
						{
							string text4 = xmlNode6.InnerText;
							xmlNode6 = xmlNode6.NextSibling;
							if (xmlNode6 != null)
							{
								text4 += xmlNode6.InnerText;
							}
							trap.Attack.Range = text4;
						}
					}
				}
				if (xmlNode.InnerText == "Countermeasures")
				{
					for (XmlNode xmlNode7 = xmlNode.NextSibling.FirstChild; xmlNode7 != null; xmlNode7 = xmlNode7.NextSibling)
					{
						string text5 = xmlNode7.InnerText.Trim();
						if (text5 != "")
						{
							trap.Countermeasures.Add(text5);
						}
					}
				}
				else
				{
					if (xmlNode.NextSibling != null)
					{
						xmlNode = xmlNode.NextSibling;
					}
					if (xmlNode.FirstChild != null)
					{
						xmlNode = xmlNode.FirstChild;
					}
					while (xmlNode != null && xmlNode.Name.ToUpper() != "P" && xmlNode.InnerText != "Encounter Uses")
					{
						string innerText5 = xmlNode.InnerText;
						trap.Countermeasures.Add(innerText5);
						xmlNode = xmlNode.NextSibling;
					}
				}
			}
			catch
			{
				Console.WriteLine("Problem with trap: " + trap.Name);
				trap = null;
			}
			return trap;
		}

		private static void handle_title_section(XmlNode node, Trap t)
		{
			string innerText = node.FirstChild.InnerText;
			string innerText2 = node.FirstChild.NextSibling.NextSibling.NextSibling.NextSibling.FirstChild.InnerText;
			t.Name = innerText;
			int level = 0;
			bool flag = false;
			bool leader = false;
			RoleFlag flag2 = RoleFlag.Standard;
			RoleType type = RoleType.Artillery;
			bool hasRole = false;
			string[] array = innerText2.Split(null);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (!(text == ""))
				{
					if (text.ToLower() == "minion")
					{
						flag = true;
					}
					else if (text.ToLower() == "(leader)")
					{
						leader = true;
					}
					else
					{
						try
						{
							level = int.Parse(text);
						}
						catch
						{
						}
						try
						{
							if (text != level.ToString())
							{
								flag2 = (RoleFlag)Enum.Parse(typeof(RoleFlag), text);
							}
						}
						catch
						{
						}
						try
						{
							if (text != level.ToString())
							{
								type = (RoleType)Enum.Parse(typeof(RoleType), text);
								hasRole = true;
							}
						}
						catch
						{
						}
					}
				}
			}
			t.Level = level;
			if (flag)
			{
				Minion minion = new Minion();
				minion.HasRole = hasRole;
				if (minion.HasRole)
				{
					minion.Type = type;
				}
				t.Role = minion;
				return;
			}
			t.Role = new ComplexRole
			{
				Type = type,
				Flag = flag2,
				Leader = leader
			};
		}

		public static MagicItem ImportItemFromHTML(string html, string url)
		{
			MagicItem magicItem = null;
			try
			{
				string xml = CompendiumImport.simplify_html(html);
				XmlDocument xmlDocument = XMLHelper.LoadSource(xml);
				if (xmlDocument == null)
				{
					return null;
				}
				magicItem = new MagicItem();
				magicItem.URL = url;
				XmlNode firstChild = xmlDocument.DocumentElement.FirstChild;
				magicItem.Name = firstChild.InnerText.Trim();
				XmlNode nextSibling = firstChild.NextSibling;
				magicItem.Description = nextSibling.InnerText.Trim();
				try
				{
					XmlNode xmlNode = nextSibling.NextSibling.FirstChild;
					while (xmlNode.NextSibling != null)
					{
						if (xmlNode.NextSibling.NextSibling == null)
						{
							break;
						}
						while (xmlNode.Name != "b")
						{
							xmlNode = xmlNode.NextSibling;
						}
						MagicItemSection magicItemSection = new MagicItemSection();
						magicItemSection.Header = xmlNode.InnerText;
						magicItemSection.Details = xmlNode.NextSibling.InnerText;
						if (magicItemSection.Header == "Level" && magicItemSection.Details != "")
						{
							try
							{
								string s = magicItemSection.Details.Substring(1).Trim();
								magicItem.Level = int.Parse(s);
								goto IL_1F6;
							}
							catch
							{
								goto IL_1F6;
							}
							goto IL_122;
						}
						goto IL_122;
						IL_1F6:
						xmlNode = xmlNode.NextSibling.NextSibling;
						continue;
						IL_122:
						if (magicItemSection.Details.StartsWith(":"))
						{
							magicItemSection.Details = magicItemSection.Details.Substring(1).Trim();
						}
						if (magicItemSection.Details == "")
						{
							magicItem.Type = magicItemSection.ToString();
						}
						else if (magicItemSection.Header == "Item Slot")
						{
							magicItem.Type = magicItemSection.Header + " (" + magicItemSection.Details.ToLower() + ")";
						}
						else
						{
							magicItem.Sections.Add(magicItemSection);
						}
						if (magicItemSection.Header == "Weapon")
						{
							magicItem.Type = "Weapon";
						}
						if (magicItemSection.Header == "Armor")
						{
							magicItem.Type = "Armour";
							goto IL_1F6;
						}
						goto IL_1F6;
					}
				}
				catch
				{
				}
				try
				{
					XmlNode nextSibling2 = nextSibling.NextSibling.NextSibling;
					while (nextSibling2 != null && !nextSibling2.InnerXml.ToLower().Contains("<a"))
					{
						string innerText = nextSibling2.InnerText;
						int num = innerText.IndexOf(":");
						if (num != -1)
						{
							MagicItemSection magicItemSection2 = new MagicItemSection();
							magicItemSection2.Header = innerText.Substring(0, num).Trim();
							magicItemSection2.Details = innerText.Substring(num).Trim();
							if (magicItemSection2.Details.StartsWith(":"))
							{
								magicItemSection2.Details = magicItemSection2.Details.Substring(1).Trim();
							}
							magicItem.Sections.Add(magicItemSection2);
						}
						nextSibling2 = nextSibling2.NextSibling;
					}
				}
				catch
				{
				}
			}
			catch
			{
				Console.WriteLine("Problem with magic item: " + magicItem.Name);
				magicItem = null;
			}
			if (magicItem.Type == "")
			{
				magicItem = null;
			}
			return magicItem;
		}
	}
}
