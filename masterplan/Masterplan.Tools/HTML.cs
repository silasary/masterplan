using Masterplan.Data;
using Masterplan.Properties;
using Masterplan.Tools.Generators;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using Utils;
using Utils.Text;

namespace Masterplan.Tools
{
	internal class HTML
	{
		private static Markdown fMarkdown = new Markdown();

		private string fRelativePath = "";

		private string fFullPath = "";

		private List<Pair<string, Plot>> fPlots = new List<Pair<string, Plot>>();

		private Dictionary<Guid, List<Guid>> fMaps = new Dictionary<Guid, List<Guid>>();

		private static Dictionary<DisplaySize, List<string>> fStyles = new Dictionary<DisplaySize, List<string>>();

		public static string Text(string str, bool strip_html, bool centred, DisplaySize size)
		{
			List<string> list = new List<string>();
			list.Add("<HTML>");
			list.AddRange(HTML.GetHead(null, null, size));
			list.Add("<BODY style=\"background-color=black\">");
			string text = HTML.Process(str, strip_html);
			if (text != "")
			{
				if (centred)
				{
					list.Add("<P class=instruction style=\"color=white\">" + text + "</P>");
				}
				else
				{
					list.Add("<P style=\"color=white\">" + text + "</P>");
				}
			}
			list.Add("</BODY>");
			list.Add("</HTML>");
			return HTML.Concatenate(list);
		}

		public static string StatBlock(EncounterCard card, CombatData data, Encounter enc, bool include_wrapper, bool initiative_holder, bool full, CardMode mode, DisplaySize size)
		{
			List<string> list = new List<string>();
			if (include_wrapper)
			{
				list.Add("<HTML>");
				list.AddRange(HTML.GetStyle(DisplaySize.Small));
				list.Add("<BODY>");
			}
			if (full)
			{
				if (data != null && data.Location == CombatData.NoPoint && enc != null && enc.MapID != Guid.Empty)
				{
					list.Add("<P class=instruction>Drag this creature from the list onto the map.</P>");
				}
				if (data != null)
				{
					list.AddRange(HTML.get_combat_data(data, card.HP, enc, initiative_holder));
				}
			}
			if (card != null)
			{
				list.Add("<P class=table>");
				list.AddRange(card.AsText(data, mode, full));
				list.Add("</P>");
			}
			else
			{
				list.Add("<P class=instruction>(no creature selected)</P>");
			}
			if (include_wrapper)
			{
				list.Add("</BODY>");
				list.Add("</HTML>");
			}
			return HTML.Concatenate(list);
		}

		public static string StatBlock(Hero hero, Encounter enc, bool include_wrapper, bool initiative_holder, bool show_effects, DisplaySize size)
		{
			List<string> list = new List<string>();
			if (include_wrapper)
			{
				list.Add("<HTML>");
				list.AddRange(HTML.GetStyle(DisplaySize.Small));
				list.Add("<BODY>");
			}
			if (enc != null)
			{
				if (enc.MapID == Guid.Empty && hero.CombatData.Initiative == -2147483648)
				{
					list.Add("<P class=instruction>Double-click this character on the list to set its initiative score.</P>");
				}
				else if (enc.MapID != Guid.Empty && hero.CombatData.Location == CombatData.NoPoint)
				{
					list.Add("<P class=instruction>Drag this character from the list onto the map.</P>");
				}
			}
			list.AddRange(HTML.get_hero(hero, enc, initiative_holder, show_effects));
			if (include_wrapper)
			{
				list.Add("</BODY>");
				list.Add("</HTML>");
			}
			return HTML.Concatenate(list);
		}

		public static string CustomMapToken(CustomToken ct, bool drag, bool include_wrapper, DisplaySize size)
		{
			List<string> list = new List<string>();
			if (include_wrapper)
			{
				list.Add("<HTML>");
				list.AddRange(HTML.GetStyle(size));
				list.Add("<BODY>");
			}
			if (drag)
			{
				list.Add("<P class=instruction>Drag this item from the list onto the map.</P>");
			}
			list.AddRange(HTML.get_custom_token(ct));
			if (include_wrapper)
			{
				list.Add("</BODY>");
				list.Add("</HTML>");
			}
			return HTML.Concatenate(list);
		}

		public static string Trap(Trap trap, CombatData cd, bool include_wrapper, bool initiative_holder, bool builder, DisplaySize size)
		{
			List<string> list = new List<string>();
			if (include_wrapper)
			{
				list.Add("<HTML>");
				list.AddRange(HTML.GetStyle(DisplaySize.Small));
				list.Add("<BODY>");
			}
			if (trap != null)
			{
				list.AddRange(HTML.get_trap(trap, cd, initiative_holder, builder));
			}
			else
			{
				list.Add("<P class=instruction>(no trap / hazard selected)</P>");
			}
			if (include_wrapper)
			{
				list.Add("</BODY>");
				list.Add("</HTML>");
			}
			return HTML.Concatenate(list);
		}

		public static string SkillChallenge(SkillChallenge challenge, bool include_links, bool include_wrapper, DisplaySize size)
		{
			List<string> list = new List<string>();
			if (include_wrapper)
			{
				list.Add("<HTML>");
				list.AddRange(HTML.GetStyle(DisplaySize.Small));
				list.Add("<BODY>");
			}
			if (challenge != null)
			{
				list.AddRange(HTML.get_skill_challenge(challenge, include_links));
			}
			else
			{
				list.Add("<P class=instruction>(no skill challenge selected)</P>");
			}
			if (include_wrapper)
			{
				list.Add("</BODY>");
				list.Add("</HTML>");
			}
			return HTML.Concatenate(list);
		}

		public static string CreatureTemplate(CreatureTemplate template, DisplaySize size, bool builder)
		{
			List<string> list = new List<string>();
			list.Add("<HTML>");
			list.AddRange(HTML.GetHead(null, null, size));
			list.Add("<BODY>");
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			list.Add("<TR class=template>");
			list.Add("<TD colspan=2>");
			list.Add("<B>" + HTML.Process(template.Name, true) + "</B>");
			list.Add("</TD>");
			list.Add("<TD>");
			list.Add("<B>" + HTML.Process(template.Info, true) + "</B>");
			list.Add("</TD>");
			if (builder)
			{
				list.Add("<TR class=template>");
				list.Add("<TD colspan=3 align=center>");
				list.Add("<A href=build:profile style=\"color:white\">(click here to edit this top section)</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			list.Add("</TR>");
			string str = template.Initiative.ToString();
			if (template.Initiative >= 0)
			{
				str = "+" + str;
			}
			if (builder)
			{
				str = "<A href=build:combat>" + str + "</A>";
			}
			list.Add("<TR>");
			list.Add("<TD colspan=3>");
			list.Add("<B>Initiative</B> " + str);
			list.Add("</TD>");
			list.Add("</TR>");
			string text = HTML.Process(template.Movement, true);
			if (text != "" || builder)
			{
				if (builder)
				{
					if (text == "")
					{
						text = "no additional movement modes";
					}
					text = "<A href=build:movement>" + text + "</A>";
				}
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Movement</B> " + text);
				list.Add("</TD>");
				list.Add("</TR>");
			}
			if (template.Senses != "" || builder)
			{
				int num = 2;
				if (template.Resist != "" || template.Vulnerable != "" || template.Immune != "" || template.DamageModifierTemplates.Count != 0)
				{
					num++;
				}
				string text2 = HTML.Process(template.Senses, true);
				if (builder)
				{
					if (text2 == "")
					{
						text2 = "no additional senses";
					}
					text2 = "<A href=build:senses>" + text2 + "</A>";
				}
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Senses</B> " + text2);
				list.Add("</TD>");
				list.Add("</TR>");
			}
			string text3 = "";
			if (template.AC != 0)
			{
				string text4 = (template.AC > 0) ? "+" : "";
				object obj = text3;
				text3 = string.Concat(new object[]
				{
					obj,
					text4,
					template.AC,
					" AC"
				});
			}
			if (template.Fortitude != 0)
			{
				if (text3 != "")
				{
					text3 += "; ";
				}
				string text5 = (template.Fortitude > 0) ? "+" : "";
				object obj2 = text3;
				text3 = string.Concat(new object[]
				{
					obj2,
					text5,
					template.Fortitude,
					" Fort"
				});
			}
			if (template.Reflex != 0)
			{
				if (text3 != "")
				{
					text3 += "; ";
				}
				string text6 = (template.Reflex > 0) ? "+" : "";
				object obj3 = text3;
				text3 = string.Concat(new object[]
				{
					obj3,
					text6,
					template.Reflex,
					" Ref"
				});
			}
			if (template.Will != 0)
			{
				if (text3 != "")
				{
					text3 += "; ";
				}
				string text7 = (template.Will > 0) ? "+" : "";
				object obj4 = text3;
				text3 = string.Concat(new object[]
				{
					obj4,
					text7,
					template.Will,
					" Will"
				});
			}
			if (text3 != "" || builder)
			{
				if (builder)
				{
					if (text3 == "")
					{
						text3 = "no defence bonuses";
					}
					text3 = "<A href=build:combat>" + text3 + "</A>";
				}
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Defences</B> " + text3);
				list.Add("</TD>");
				list.Add("</TR>");
			}
			string text8 = HTML.Process(template.Resist, true);
			string text9 = HTML.Process(template.Vulnerable, true);
			string text10 = HTML.Process(template.Immune, true);
			if (text8 == null)
			{
				text8 = "";
			}
			if (text9 == null)
			{
				text9 = "";
			}
			if (text10 == null)
			{
				text10 = "";
			}
			foreach (DamageModifierTemplate current in template.DamageModifierTemplates)
			{
				int num2 = current.HeroicValue + current.ParagonValue + current.EpicValue;
				if (num2 == 0)
				{
					if (text10 != "")
					{
						text10 += ", ";
					}
					text10 += current.Type.ToString().ToLower();
				}
				if (num2 > 0)
				{
					if (text9 != "")
					{
						text9 += ", ";
					}
					object obj5 = text9;
					text9 = string.Concat(new object[]
					{
						obj5,
						current.HeroicValue,
						"/",
						current.ParagonValue,
						"/",
						current.EpicValue,
						" ",
						current.Type.ToString().ToLower()
					});
				}
				if (num2 < 0)
				{
					if (text8 != "")
					{
						text8 += ", ";
					}
					int num3 = Math.Abs(current.HeroicValue);
					int num4 = Math.Abs(current.ParagonValue);
					int num5 = Math.Abs(current.EpicValue);
					object obj6 = text8;
					text8 = string.Concat(new object[]
					{
						obj6,
						num3,
						"/",
						num4,
						"/",
						num5,
						" ",
						current.Type.ToString().ToLower()
					});
				}
			}
			string text11 = "";
			if (text10 != "")
			{
				if (text11 != "")
				{
					text11 += " ";
				}
				text11 = text11 + "<B>Immune</B> " + text10;
			}
			if (text8 != "")
			{
				if (text11 != "")
				{
					text11 += " ";
				}
				text11 = text11 + "<B>Resist</B> " + text8;
			}
			if (text9 != "")
			{
				if (text11 != "")
				{
					text11 += " ";
				}
				text11 = text11 + "<B>Vulnerable</B> " + text9;
			}
			if (text11 != "" || builder)
			{
				if (builder)
				{
					if (text11 == "")
					{
						text11 = "Set resistances / vulnerabilities / immunities";
					}
					text11 = "<A href=build:damage>" + text11 + "</A>";
				}
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add(text11);
				list.Add("</TD>");
				list.Add("</TR>");
			}
			list.Add("<TR>");
			list.Add("<TD colspan=3>");
			list.Add("<B>Saving Throws</B> +2");
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD colspan=3>");
			list.Add("<B>Action Point</B> 1");
			list.Add("</TD>");
			list.Add("</TR>");
			string str2 = "+" + template.HP + " per level + Constitution score";
			if (builder)
			{
				str2 = "<A href=build:combat>" + str2 + "</A>";
			}
			list.Add("<TR>");
			list.Add("<TD colspan=3>");
			list.Add("<B>HP</B> " + str2);
			list.Add("</TD>");
			list.Add("</TR>");
			if (builder)
			{
				list.Add("<TR class=template>");
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
				if (template.Regeneration == null)
				{
					list.Add("|");
					list.Add("<A href=power:regenedit>add regeneration</A>");
				}
				list.Add("</TD>");
				list.Add("</TR>");
			}
			Dictionary<CreaturePowerCategory, List<CreaturePower>> dictionary = new Dictionary<CreaturePowerCategory, List<CreaturePower>>();
			Array values = Enum.GetValues(typeof(CreaturePowerCategory));
			foreach (CreaturePowerCategory key in values)
			{
				dictionary[key] = new List<CreaturePower>();
			}
			foreach (CreaturePower current2 in template.CreaturePowers)
			{
				dictionary[current2.Category].Add(current2);
			}
			foreach (CreaturePowerCategory creaturePowerCategory in values)
			{
				int num6 = dictionary[creaturePowerCategory].Count;
				if (creaturePowerCategory == CreaturePowerCategory.Trait)
				{
					num6 += template.Auras.Count;
					Regeneration regeneration = template.Regeneration;
					if (regeneration != null)
					{
						num6++;
					}
				}
				if (num6 != 0)
				{
					string str3 = "";
					switch (creaturePowerCategory)
					{
					case CreaturePowerCategory.Trait:
						str3 = "Traits";
						break;
					case CreaturePowerCategory.Standard:
					case CreaturePowerCategory.Move:
					case CreaturePowerCategory.Minor:
					case CreaturePowerCategory.Free:
						str3 = creaturePowerCategory + " Actions";
						break;
					case CreaturePowerCategory.Triggered:
						str3 = "Triggered Actions";
						break;
					case CreaturePowerCategory.Other:
						str3 = "Other Actions";
						break;
					}
					list.Add("<TR class=creature>");
					list.Add("<TD colspan=3>");
					list.Add("<B>" + str3 + "</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					if (creaturePowerCategory == CreaturePowerCategory.Trait)
					{
						foreach (Aura current3 in template.Auras)
						{
							string text12 = HTML.Process(current3.Details.Trim(), true);
							if (!text12.StartsWith("aura", StringComparison.OrdinalIgnoreCase))
							{
								text12 = "Aura " + text12;
							}
							else
							{
								text12 = "A" + text12.Substring(1);
							}
							MemoryStream memoryStream = new MemoryStream();
							Resources.Aura.Save(memoryStream, ImageFormat.Png);
							byte[] inArray = memoryStream.ToArray();
							string str4 = Convert.ToBase64String(inArray);
							list.Add("<TR class=shaded>");
							list.Add("<TD colspan=3>");
							list.Add("<img src=data:image/png;base64," + str4 + ">");
							list.Add("<B>" + HTML.Process(current3.Name, true) + "</B>");
							if (current3.Keywords != "")
							{
								list.Add("(" + current3.Keywords + ")");
							}
							if (current3.Radius > 0)
							{
								list.Add(" &diams; Aura " + current3.Radius);
							}
							list.Add("</TD>");
							list.Add("</TR>");
							list.Add("<TR>");
							list.Add("<TD colspan=3>");
							list.Add(text12);
							list.Add("</TD>");
							list.Add("</TR>");
							if (builder)
							{
								list.Add("<TR>");
								list.Add("<TD colspan=3 align=center>");
								list.Add("<A href=auraedit:" + current3.ID + ">edit</A>");
								list.Add("|");
								list.Add("<A href=auraremove:" + current3.ID + ">remove</A>");
								list.Add("</TD>");
								list.Add("</TR>");
							}
						}
						if (template.Regeneration != null)
						{
							list.Add("<TR class=shaded>");
							list.Add("<TD colspan=3>");
							list.Add("<B>Regeneration</B>");
							list.Add("</TD>");
							list.Add("</TR>");
							list.Add("<TR>");
							list.Add("<TD colspan=3>");
							list.Add("Regeneration " + HTML.Process(template.Regeneration.ToString(), true));
							list.Add("</TD>");
							list.Add("</TR>");
							if (builder)
							{
								list.Add("<TR>");
								list.Add("<TD colspan=3 align=center>");
								list.Add("<A href=power:regenedit>edit</A>");
								list.Add("|");
								list.Add("<A href=power:regenremove>remove</A>");
								list.Add("</TD>");
								list.Add("</TR>");
							}
						}
					}
					foreach (CreaturePower current4 in dictionary[creaturePowerCategory])
					{
						list.AddRange(current4.AsHTML(null, CardMode.View, false));
						if (builder)
						{
							list.Add("<TR>");
							list.Add("<TD colspan=3 align=center>");
							list.Add("<A href=\"poweredit:" + current4.ID + "\">edit this power</A>");
							list.Add("|");
							list.Add("<A href=\"powerremove:" + current4.ID + "\">remove this power</A>");
							list.Add("</TD>");
							list.Add("</TR>");
						}
					}
				}
			}
			if (template.Tactics != "" || builder)
			{
				string text13 = HTML.Process(template.Tactics, true);
				if (builder)
				{
					if (text13 == "")
					{
						text13 = "no tactics specified";
					}
					text13 = "<A href=build:tactics>" + text13 + "</A>";
				}
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Tactics</B> " + text13);
				list.Add("</TD>");
				list.Add("</TR>");
			}
			Library library = Session.FindLibrary(template);
			if (library != null && library.Name != "" && (Session.Project == null || library != Session.Project.Library))
			{
				string item = HTML.Process(library.Name, true);
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=3>");
				list.Add(item);
				list.Add("</TD>");
				list.Add("</TR>");
			}
			list.Add("</TABLE>");
			list.Add("</P>");
			list.Add("</BODY>");
			list.Add("</HTML>");
			return HTML.Concatenate(list);
		}

		public static string MagicItem(MagicItem item, DisplaySize size, bool builder, bool wrapper)
		{
			List<string> list = new List<string>();
			if (wrapper)
			{
				list.Add("<HTML>");
				list.AddRange(HTML.GetHead(null, null, size));
			}
			list.Add("<BODY>");
			if (item != null)
			{
				list.AddRange(HTML.get_magic_item(item, builder));
			}
			else
			{
				list.Add("<P class=instruction>(no item selected)</P>");
			}
			if (wrapper)
			{
				list.Add("</BODY>");
				list.Add("</HTML>");
			}
			return HTML.Concatenate(list);
		}

		public static string Artifact(Artifact artifact, DisplaySize size, bool builder, bool wrapper)
		{
			List<string> list = new List<string>();
			if (wrapper)
			{
				list.Add("<HTML>");
				list.AddRange(HTML.GetHead(null, null, size));
			}
			list.Add("<BODY>");
			if (artifact != null)
			{
				list.AddRange(HTML.get_artifact(artifact, builder));
			}
			else
			{
				list.Add("<P class=instruction>(no item selected)</P>");
			}
			if (wrapper)
			{
				list.Add("</BODY>");
				list.Add("</HTML>");
			}
			return HTML.Concatenate(list);
		}

		public static string PlotPoint(PlotPoint pp, Plot plot, int party_level, bool links, MainForm.ViewType view, DisplaySize size)
		{
			if (Session.Project == null)
			{
				return null;
			}
			List<string> list = new List<string>();
			list.Add("<HTML>");
			list.AddRange(HTML.GetHead(null, null, size));
			list.Add("<BODY>");
			if (pp != null)
			{
				list.Add("<H3>" + HTML.Process(pp.Name, true) + "</H3>");
				switch (pp.State)
				{
				case PlotPointState.Completed:
					list.Add("<P class=instruction>(completed)</P>");
					break;
				case PlotPointState.Skipped:
					list.Add("<P class=instruction>(skipped)</P>");
					break;
				}
				if (links)
				{
					List<string> list2 = new List<string>();
					if (view == MainForm.ViewType.Flowchart)
					{
						list2.Add("<A href=\"plot:edit\">Open</A> this plot point.");
					}
					if (pp.Element == null)
					{
						list2.Add("Turn this point into a <A href=plot:encounter>combat encounter</A>.");
						list2.Add("Turn this point into a <A href=plot:challenge>skill challenge</A>.");
					}
					if (pp.Subplot.Points.Count != 0)
					{
						list2.Add("This plot point has a <A href=\"plot:explore\">subplot</A>.");
					}
					else
					{
						list2.Add("Create a <A href=\"plot:explore\">subplot</A> for this point.");
					}
					Encounter encounter = pp.Element as Encounter;
					if (encounter != null)
					{
						list2.Add("This plot point contains an <A href=plot:element>encounter</A> (<A href=plot:run>run it</a>).");
					}
					SkillChallenge skillChallenge = pp.Element as SkillChallenge;
					if (skillChallenge != null)
					{
						list2.Add("This plot point contains a <A href=plot:element>skill challenge</A>.");
					}
					TrapElement trapElement = pp.Element as TrapElement;
					if (trapElement != null)
					{
						string str = (trapElement.Trap.Type == TrapType.Trap) ? "trap" : "hazard";
						list2.Add("This plot point contains a <A href=plot:element>" + str + "</A>.");
					}
					Map map = null;
					MapArea mapArea = null;
					pp.GetTacticalMapArea(ref map, ref mapArea);
					if (map != null && mapArea != null)
					{
						string str2 = HTML.Process(mapArea.Name, true);
						list2.Add("This plot point occurs in <A href=plot:maparea>" + str2 + "</A>.");
					}
					RegionalMap regionalMap = null;
					MapLocation mapLocation = null;
					pp.GetRegionalMapArea(ref regionalMap, ref mapLocation, Session.Project);
					if (regionalMap != null && mapLocation != null)
					{
						string str3 = HTML.Process(mapLocation.Name, true);
						list2.Add("This plot point occurs at <A href=plot:maploc>" + str3 + "</A>.");
					}
					if (list2.Count != 0)
					{
						list.Add("<P class=table>");
						list.Add("<TABLE>");
						list.Add("<TR class=heading>");
						list.Add("<TD><B>Options</B></TD>");
						list.Add("</TR>");
						for (int num = 0; num != list2.Count; num++)
						{
							list.Add("<TR>");
							list.Add("<TD>");
							list.Add(list2[num]);
							list.Add("</TD>");
							list.Add("</TR>");
						}
						list.Add("</TABLE>");
						list.Add("</P>");
					}
				}
				string text = HTML.Process(pp.ReadAloud, false);
				if (text != "")
				{
					text = HTML.fMarkdown.Transform(text);
					text = text.Replace("<p>", "<p class=readaloud>");
					list.Add(text);
				}
				string text2 = HTML.Process(pp.Details, false);
				if (text2 != "")
				{
					text2 = HTML.fMarkdown.Transform(text2);
					list.Add(text2);
				}
				if (party_level != Session.Project.Party.Level)
				{
					list.Add("<P><B>Party level</B>: " + party_level + "</P>");
				}
				if (pp.Date != null)
				{
					list.Add("<P><B>Date</B>: " + pp.Date + "</P>");
				}
				list.AddRange(HTML.get_map_area_details(pp));
				if (links)
				{
					BinarySearchTree<EncyclopediaEntry> binarySearchTree = new BinarySearchTree<EncyclopediaEntry>();
					foreach (Guid current in pp.EncyclopediaEntryIDs)
					{
						EncyclopediaEntry encyclopediaEntry = Session.Project.Encyclopedia.FindEntry(current);
						if (encyclopediaEntry != null)
						{
							binarySearchTree.Add(encyclopediaEntry);
						}
					}
					if (pp.MapLocationID != Guid.Empty)
					{
						EncyclopediaEntry encyclopediaEntry2 = Session.Project.Encyclopedia.FindEntryForAttachment(pp.MapLocationID);
						if (encyclopediaEntry2 != null)
						{
							binarySearchTree.Add(encyclopediaEntry2);
						}
					}
					if (pp.Element != null && pp.Element is Encounter)
					{
						Encounter encounter2 = pp.Element as Encounter;
						foreach (NPC current2 in Session.Project.NPCs)
						{
							EncyclopediaEntry encyclopediaEntry3 = Session.Project.Encyclopedia.FindEntryForAttachment(current2.ID);
							if (encyclopediaEntry3 != null && encounter2.Contains(current2.ID))
							{
								binarySearchTree.Add(encyclopediaEntry3);
							}
						}
					}
					List<EncyclopediaEntry> sortedList = binarySearchTree.SortedList;
					if (sortedList.Count != 0)
					{
						list.Add("<P><B>See also</B>:</P>");
						list.Add("<UL>");
						foreach (EncyclopediaEntry current3 in sortedList)
						{
							list.Add(string.Concat(new object[]
							{
								"<LI><A href=\"entry:",
								current3.ID,
								"\">",
								current3.Name,
								"</A></LI>"
							}));
						}
						list.Add("</UL>");
					}
				}
				if (pp.Element != null)
				{
					Encounter encounter3 = pp.Element as Encounter;
					if (encounter3 != null)
					{
						list.AddRange(HTML.get_encounter(encounter3));
					}
					TrapElement trapElement2 = pp.Element as TrapElement;
					if (trapElement2 != null)
					{
						list.AddRange(HTML.get_trap(trapElement2.Trap, null, false, false));
					}
					SkillChallenge skillChallenge2 = pp.Element as SkillChallenge;
					if (skillChallenge2 != null)
					{
						list.AddRange(HTML.get_skill_challenge(skillChallenge2, links));
					}
					Quest quest = pp.Element as Quest;
					if (quest != null)
					{
						list.AddRange(HTML.get_quest(quest));
					}
				}
				if (pp.Parcels.Count != 0)
				{
					list.AddRange(HTML.get_parcels(pp, links));
				}
			}
			else
			{
				PlotPoint plotPoint = Session.Project.FindParent(plot);
				string raw_text = (plotPoint != null) ? plotPoint.Name : Session.Project.Name;
				list.Add("<H2>" + HTML.Process(raw_text, true) + "</H2>");
				if (plotPoint != null)
				{
					if (plotPoint.Date != null)
					{
						list.Add("<P>" + plotPoint.Date + "</P>");
					}
					if (plotPoint.Details != "")
					{
						list.Add("<P>" + HTML.Process(plotPoint.Details, false) + "</P>");
					}
				}
				else
				{
					if (Session.Project.Author != "")
					{
						list.Add("<P class=instruction>by " + Session.Project.Author + "</P>");
					}
					int size2 = Session.Project.Party.Size;
					int level = Session.Project.Party.Level;
					int xP = Session.Project.Party.XP;
					int heroXP = Experience.GetHeroXP(level);
					string text3 = string.Concat(new object[]
					{
						"<B>",
						HTML.Process(Session.Project.Name, true),
						"</B> is designed for a party of ",
						size2,
						" characters at level ",
						level
					});
					if (xP != heroXP)
					{
						object obj = text3;
						text3 = string.Concat(new object[]
						{
							obj,
							", starting with ",
							xP,
							" XP"
						});
					}
					text3 += ".";
					list.Add("<P>" + text3 + "</P>");
				}
				int num2 = 0;
				List<List<PlotPoint>> list3 = Workspace.FindLayers(plot);
				foreach (List<PlotPoint> current4 in list3)
				{
					num2 += Workspace.GetLayerXP(current4);
				}
				if (num2 != 0)
				{
					string text4 = "XP available: " + num2 + ".";
					if (plot == Session.Project.Plot)
					{
						int level2 = Session.Project.Party.Level;
						int heroXP2 = Experience.GetHeroXP(level2);
						int xp = heroXP2 + num2 / Session.Project.Party.Size;
						int heroLevel = Experience.GetHeroLevel(xp);
						if (heroLevel != -1 && heroLevel != level2)
						{
							text4 += "<BR>";
							object obj2 = text4;
							text4 = string.Concat(new object[]
							{
								obj2,
								"The party will reach level ",
								heroLevel,
								"."
							});
						}
					}
					list.Add("<P>" + text4 + "</P>");
				}
				if (links)
				{
					list.Add("<P class=table>");
					list.Add("<TABLE>");
					list.Add("<TR class=heading>");
					list.Add("<TD><B>Options</B></TD>");
					list.Add("</TR>");
					if (view == MainForm.ViewType.Flowchart)
					{
						if (plot.Points.Count == 0)
						{
							list.Add("<TR>");
							list.Add("<TD>This plot is empty:</TD>");
							list.Add("</TR>");
							list.Add("<TR>");
							list.Add("<TD class=indent>Add a <A href=\"plot:add\">plot point</A>.</TD>");
							list.Add("</TR>");
							list.Add("<TR>");
							list.Add("<TD class=indent>Add a <A href=\"plot:encounter\">combat encounter</A>.</TD>");
							list.Add("</TR>");
							list.Add("<TR>");
							list.Add("<TD class=indent>Add a <A href=\"plot:challenge\">skill challenge</A>.</TD>");
							list.Add("</TR>");
							list.Add("<TR>");
							list.Add("<TD>Build a plot by setting the <A href=\"plot:goals\">party goals</A>.</TD>");
							list.Add("</TR>");
						}
						if (plotPoint != null)
						{
							list.Add("<TR>");
							list.Add("<TD>Move up <A href=\"plot:up\">one plot level</A>.</TD>");
							list.Add("</TR>");
						}
						List<Guid> list4 = plot.FindTacticalMaps();
						if (list4.Count == 0)
						{
							if (Session.Project.Maps.Count == 0)
							{
								list.Add("<TR>");
								list.Add("<TD>Create a <A href=\"delveview:build\">tactical map</A> to use as the basis of this plot.</TD>");
								list.Add("</TR>");
							}
							else
							{
								list.Add("<TR>");
								list.Add("<TD>Use a tactical map as the basis of this plot:</TD>");
								list.Add("</TR>");
								list.Add("<TR>");
								list.Add("<TD class=indent>Build a <A href=\"delveview:build\">new map</A>.</TD>");
								list.Add("</TR>");
								list.Add("<TR>");
								list.Add("<TD class=indent>Select an <A href=\"delveview:select\">existing map</A>.</TD>");
								list.Add("</TR>");
							}
						}
						else if (list4.Count == 1)
						{
							list.Add("<TR>");
							list.Add("<TD>Switch to <A href=\"delveview:" + list4[0] + "\">delve view</A>.</TD>");
							list.Add("</TR>");
						}
						else
						{
							list.Add("<TR>");
							list.Add("<TD>Switch to delve view using one of the following maps:</TD>");
							list.Add("</TR>");
							foreach (Guid current5 in list4)
							{
								if (!(current5 == Guid.Empty))
								{
									Map map2 = Session.Project.FindTacticalMap(current5);
									if (map2 != null)
									{
										list.Add("<TR>");
										list.Add(string.Concat(new object[]
										{
											"<TD class=indent><A href=\"delveview:",
											current5,
											"\">",
											HTML.Process(map2.Name, true),
											"</A></TD>"
										}));
										list.Add("</TR>");
									}
								}
							}
							list.Add("<TR>");
							list.Add("<TD class=indent><A href=\"delveview:select\">Select (or create) a map</A></TD>");
							list.Add("</TR>");
						}
						List<Guid> list5 = plot.FindRegionalMaps();
						if (list5.Count == 0)
						{
							if (Session.Project.RegionalMaps.Count == 0)
							{
								list.Add("<TR>");
								list.Add("<TD>Create a <A href=\"mapview:build\">regional map</A> to use as the basis of this plot.</TD>");
								list.Add("</TR>");
							}
							else
							{
								list.Add("<TR>");
								list.Add("<TD>Use a regional map as the basis of this plot:</TD>");
								list.Add("</TR>");
								list.Add("<TR>");
								list.Add("<TD class=indent>Build a <A href=\"mapview:build\">new map</A>.</TD>");
								list.Add("</TR>");
								list.Add("<TR>");
								list.Add("<TD class=indent>Select an <A href=\"mapview:select\">existing map</A>.</TD>");
								list.Add("</TR>");
							}
						}
						else if (list5.Count == 1)
						{
							list.Add("<TR>");
							list.Add("<TD>Switch to <A href=\"mapview:" + list5[0] + "\">map view</A>.</TD>");
							list.Add("</TR>");
						}
						else
						{
							list.Add("<TR>");
							list.Add("<TD>Switch to map view using one of the following maps:</TD>");
							list.Add("</TR>");
							foreach (Guid current6 in list5)
							{
								if (!(current6 == Guid.Empty))
								{
									RegionalMap regionalMap2 = Session.Project.FindRegionalMap(current6);
									if (regionalMap2 != null)
									{
										list.Add("<TR>");
										list.Add(string.Concat(new object[]
										{
											"<TD class=indent><A href=\"mapview:",
											current6,
											"\">",
											HTML.Process(regionalMap2.Name, true),
											"</A></TD>"
										}));
										list.Add("</TR>");
									}
								}
							}
							list.Add("<TR>");
							list.Add("<TD class=indent><A href=\"mapview:select\">Select (or create) a map</A></TD>");
							list.Add("</TR>");
						}
						if (plotPoint == null)
						{
							list.Add("<TR>");
							list.Add("<TD>Edit the <A href=\"plot:properties\">project properties</A>.</TD>");
							list.Add("</TR>");
						}
					}
					else if (view == MainForm.ViewType.Delve)
					{
						list.Add("<TR>");
						list.Add("<TD>Switch to <A href=\"delveview:off\">flowchart view</A>.</TD>");
						list.Add("</TR>");
						list.Add("<TR>");
						list.Add("<TD><A href=\"delveview:edit\">Edit this map</A>.</TD>");
						list.Add("</TR>");
						list.Add("<TR>");
						list.Add("<TD>Send this map to the <A href=\"delveview:playerview\">player view</A>.</TD>");
						list.Add("</TR>");
					}
					else if (view == MainForm.ViewType.Map)
					{
						list.Add("<TR>");
						list.Add("<TD>Switch to <A href=\"mapview:off\">flowchart view</A>.</TD>");
						list.Add("</TR>");
						list.Add("<TR>");
						list.Add("<TD><A href=\"mapview:edit\">Edit this map</A>.</TD>");
						list.Add("</TR>");
						list.Add("<TR>");
						list.Add("<TD>Send this map to the <A href=\"mapview:playerview\">player view</A>.</TD>");
						list.Add("</TR>");
					}
					list.Add("</TR>");
					list.Add("</TABLE>");
					list.Add("</P>");
				}
			}
			list.Add("</BODY>");
			list.Add("</HTML>");
			return HTML.Concatenate(list);
		}

		public static string MapArea(MapArea area, DisplaySize size)
		{
			List<string> list = new List<string>();
			list.Add("<HTML>");
			list.AddRange(HTML.GetHead(null, null, size));
			list.Add("<BODY>");
			if (area != null)
			{
				string str = HTML.Process(area.Name, true);
				list.Add("<H3>" + str + "</H3>");
				if (area.Details != "")
				{
					list.Add("<P>");
					list.Add(HTML.Process(area.Details, true));
					list.Add("</P>");
				}
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=heading>");
				list.Add("<TD><B>Options</B></TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<A href=\"maparea:edit\">View information</A> about this map area.");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<A href=\"maparea:create\">Create a plot point</A> here.");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD class=indent>");
				list.Add("... containing a <A href=\"maparea:encounter\">combat encounter</A>.");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD class=indent>");
				list.Add("... containing a <A href=\"maparea:trap\">trap or hazard</A>.");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD class=indent>");
				list.Add("... containing a <A href=\"maparea:challenge\">skill challenge</A>.");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			return HTML.Concatenate(list);
		}

		public static string MapLocation(MapLocation loc, DisplaySize size)
		{
			List<string> list = new List<string>();
			list.Add("<HTML>");
			list.AddRange(HTML.GetHead(null, null, size));
			list.Add("<BODY>");
			if (loc != null)
			{
				string str = HTML.Process(loc.Name, true);
				list.Add("<H3>" + str + "</H3>");
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=heading>");
				list.Add("<TD><B>Options</B></TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<A href=\"maploc:edit\">View information</A> about this map location.");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<A href=\"maploc:create\">Create a plot point</A> here.");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD class=indent>");
				list.Add("... containing a <A href=\"maploc:encounter\">combat encounter</A>.");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD class=indent>");
				list.Add("... containing a <A href=\"maploc:trap\">trap or hazard</A>.");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD class=indent>");
				list.Add("... containing a <A href=\"maploc:challenge\">skill challenge</A>.");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			return HTML.Concatenate(list);
		}

		public static string EncounterNote(EncounterNote en, DisplaySize size)
		{
			List<string> list = new List<string>();
			list.Add("<HTML>");
			list.AddRange(HTML.GetHead(null, null, size));
			list.Add("<BODY>");
			if (en != null)
			{
				list.Add("<H3>" + HTML.Process(en.Title, true) + "</H3>");
				string text = HTML.Process(en.Contents, false);
				if (text != "")
				{
					text = HTML.fMarkdown.Transform(text);
					text = text.Replace("<p>", "<p class=encounter_note>");
					list.Add(text);
				}
				else
				{
					list.Add("<P class=instruction>This note has no contents.</P>");
					list.Add("<P class=instruction>Press <A href=\"note:edit\">Edit</A> to add information to this note.</P>");
				}
			}
			else
			{
				list.Add("<P class=instruction>(no note selected)</P>");
			}
			list.Add("</BODY>");
			list.Add("</HTML>");
			return HTML.Concatenate(list);
		}

		public static string Background(Background bg, DisplaySize size)
		{
			List<string> list = new List<string>();
			list.Add("<HTML>");
			list.AddRange(HTML.GetHead(null, null, size));
			list.Add("<BODY>");
			if (bg != null)
			{
				string text = HTML.Process(bg.Details, false);
				if (text != "")
				{
					text = HTML.fMarkdown.Transform(text);
					text = text.Replace("<p>", "<p class=background>");
					list.Add(text);
				}
				else
				{
					list.Add("<P class=instruction>Press <A href=\"background:edit\">Edit</A> to add information to this item.</P>");
				}
			}
			else
			{
				list.Add("<P class=instruction>(no background selected)</P>");
			}
			list.Add("</BODY>");
			list.Add("</HTML>");
			return HTML.Concatenate(list);
		}

		public static string Background(List<Background> backgrounds, DisplaySize size)
		{
			List<string> list = new List<string>();
			list.Add("<HTML>");
			list.AddRange(HTML.GetHead(null, null, size));
			list.Add("<BODY>");
			foreach (Background current in backgrounds)
			{
				string text = HTML.Process(current.Title, false);
				string text2 = HTML.Process(current.Details, false);
				if (text != "" && text2 != "")
				{
					list.Add("<H3>" + text + "</H3>");
					text2 = HTML.fMarkdown.Transform(text2);
					text2 = text2.Replace("<p>", "<p class=background>");
					list.Add(text2);
				}
			}
			list.Add("</BODY>");
			list.Add("</HTML>");
			return HTML.Concatenate(list);
		}

		public static string EncyclopediaEntry(EncyclopediaEntry entry, Encyclopedia encyclopedia, DisplaySize size, bool include_dm_info, bool include_entry_links, bool include_attachment, bool include_picture_links)
		{
			List<string> list = new List<string>();
			list.Add("<HTML>");
			list.AddRange(HTML.GetHead(null, null, size));
			list.Add("<BODY>");
			if (entry != null)
			{
				list.Add("<H4>" + HTML.Process(entry.Name, true) + "</H4>");
				list.Add("<HR>");
			}
			if (entry != null)
			{
				if (include_attachment && entry.AttachmentID != Guid.Empty)
				{
					MapLocation mapLocation = null;
					foreach (RegionalMap current in Session.Project.RegionalMaps)
					{
						mapLocation = current.FindLocation(entry.AttachmentID);
						if (mapLocation != null)
						{
							break;
						}
					}
					if (mapLocation != null)
					{
						list.Add("<P class=instruction><A href=\"map:" + entry.AttachmentID + "\">View location on map</A>.</P>");
						list.Add("<HR>");
					}
				}
				string text = HTML.process_encyclopedia_info(entry.Details, encyclopedia, include_entry_links, include_dm_info);
				string text2 = HTML.process_encyclopedia_info(entry.DMInfo, encyclopedia, include_entry_links, include_dm_info);
				if (text == "" && text2 == "")
				{
					list.Add("<P class=instruction>Press <A href=\"entry:edit\">Edit</A> to add information to this entry.</P>");
				}
				if (text != "")
				{
					list.Add("<P class=encyclopedia_entry>" + HTML.Process(text, false) + "</P>");
				}
				if (include_dm_info && text2 != "")
				{
					list.Add("<H4>For DMs Only</H4>");
					list.Add("<P class=encyclopedia_entry>" + HTML.Process(text2, false) + "</P>");
				}
				if (include_picture_links && entry.Images.Count != 0)
				{
					list.Add("<H4>Pictures</H4>");
					list.Add("<UL>");
					foreach (EncyclopediaImage current2 in entry.Images)
					{
						list.Add("<LI>");
						list.Add(string.Concat(new object[]
						{
							"<A href=picture:",
							current2.ID,
							">",
							current2.Name,
							"</A>"
						}));
						list.Add("</LI>");
					}
					list.Add("</UL>");
				}
				if (include_attachment && entry.AttachmentID != Guid.Empty)
				{
					Hero hero = Session.Project.FindHero(entry.AttachmentID);
					if (hero != null)
					{
						list.AddRange(HTML.get_hero(hero, null, false, false));
					}
					ICreature creature = Session.FindCreature(entry.AttachmentID, SearchType.Global);
					if (creature != null)
					{
						EncounterCard encounterCard = new EncounterCard(creature.ID);
						list.Add("<P class=table>");
						list.AddRange(encounterCard.AsText(null, CardMode.View, true));
						list.Add("</P>");
					}
					IPlayerOption playerOption = Session.Project.FindPlayerOption(entry.AttachmentID);
					if (playerOption != null)
					{
						list.AddRange(HTML.get_player_option(playerOption));
					}
				}
				if (!include_entry_links || encyclopedia == null)
				{
					goto IL_6F1;
				}
				List<EncyclopediaLink> list2 = new List<EncyclopediaLink>();
				foreach (EncyclopediaLink current3 in encyclopedia.Links)
				{
					if (current3.EntryIDs.Contains(entry.ID))
					{
						list2.Add(current3);
					}
				}
				if (list2.Count != 0)
				{
					list.Add("<HR>");
					list.Add("<P><B>See also</B>:</P>");
					list.Add("<UL>");
					foreach (EncyclopediaLink current4 in list2)
					{
						foreach (Guid current5 in current4.EntryIDs)
						{
							if (!(current5 == entry.ID))
							{
								EncyclopediaEntry encyclopediaEntry = encyclopedia.FindEntry(current5);
								list.Add(string.Concat(new object[]
								{
									"<LI><A href=\"entry:",
									current5,
									"\">",
									HTML.Process(encyclopediaEntry.Name, true),
									"</A></LI>"
								}));
							}
						}
					}
					list.Add("</UL>");
				}
				List<EncyclopediaGroup> list3 = new List<EncyclopediaGroup>();
				foreach (EncyclopediaGroup current6 in encyclopedia.Groups)
				{
					if (current6.EntryIDs.Contains(entry.ID))
					{
						list3.Add(current6);
					}
				}
				if (list3.Count == 0)
				{
					goto IL_6F1;
				}
				list.Add("<HR>");
				list.Add("<P><B>Groups</B>:</P>");
				using (List<EncyclopediaGroup>.Enumerator enumerator7 = list3.GetEnumerator())
				{
					while (enumerator7.MoveNext())
					{
						EncyclopediaGroup current7 = enumerator7.Current;
						list.Add("<P class=table>");
						list.Add("<TABLE class=wide>");
						list.Add("<TR class=shaded align=center>");
						list.Add("<TD>");
						list.Add(string.Concat(new object[]
						{
							"<B><A href=\"group:",
							current7.ID,
							"\">",
							HTML.Process(current7.Name, true),
							"</A></B>"
						}));
						list.Add("</TD>");
						list.Add("</TR>");
						list.Add("<TR>");
						list.Add("<TD>");
						List<EncyclopediaEntry> list4 = new List<EncyclopediaEntry>();
						foreach (Guid current8 in current7.EntryIDs)
						{
							EncyclopediaEntry encyclopediaEntry2 = encyclopedia.FindEntry(current8);
							if (encyclopediaEntry2 != null)
							{
								list4.Add(encyclopediaEntry2);
							}
						}
						list4.Sort();
						foreach (EncyclopediaEntry current9 in list4)
						{
							if (current9 != entry)
							{
								list.Add(string.Concat(new object[]
								{
									"<A href=\"entry:",
									current9.ID,
									"\">",
									HTML.Process(current9.Name, true),
									"</A>"
								}));
							}
							else
							{
								list.Add("<B>" + HTML.Process(current9.Name, true) + "</B>");
							}
						}
						list.Add("</TD>");
						list.Add("</TR>");
						list.Add("</TABLE>");
					}
					goto IL_6F1;
				}
			}
			list.Add("<P class=instruction>(no entry selected)</P>");
			IL_6F1:
			list.Add("</BODY>");
			list.Add("</HTML>");
			return HTML.Concatenate(list);
		}

		public static string EncyclopediaGroup(EncyclopediaGroup group, Encyclopedia encyclopedia, DisplaySize size, bool include_dm_info, bool include_entry_links)
		{
			List<string> list = new List<string>();
			list.Add("<HTML>");
			list.AddRange(HTML.GetHead(null, null, size));
			list.Add("<BODY>");
			if (group != null)
			{
				if (encyclopedia != null)
				{
					List<EncyclopediaEntry> list2 = new List<EncyclopediaEntry>();
					foreach (Guid current in group.EntryIDs)
					{
						EncyclopediaEntry encyclopediaEntry = encyclopedia.FindEntry(current);
						if (encyclopediaEntry != null)
						{
							list2.Add(encyclopediaEntry);
						}
					}
					if (list2.Count != 0)
					{
						using (List<EncyclopediaEntry>.Enumerator enumerator2 = list2.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								EncyclopediaEntry current2 = enumerator2.Current;
								list.Add("<H3>" + HTML.Process(current2.Name, true) + "</H3>");
								string raw_text = HTML.process_encyclopedia_info(current2.Details, encyclopedia, include_entry_links, include_dm_info);
								list.Add("<P class=encyclopedia_entry>" + HTML.Process(raw_text, false) + "</P>");
							}
							goto IL_11B;
						}
					}
					list.Add("<P class=instruction>(no entries in group)</P>");
				}
			}
			else
			{
				list.Add("<P class=instruction>(no group selected)</P>");
			}
			IL_11B:
			list.Add("</BODY>");
			list.Add("</HTML>");
			return HTML.Concatenate(list);
		}

		public static string Handout(List<object> items, DisplaySize size, bool include_dm_info)
		{
			List<string> list = new List<string>();
			list.Add("<HTML>");
			list.AddRange(HTML.GetHead(Session.Project.Name, "Handout", size));
			list.Add("<BODY>");
			if (items.Count != 0)
			{
				using (List<object>.Enumerator enumerator = items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object current = enumerator.Current;
						if (current is Background)
						{
							Background background = current as Background;
							string text = HTML.Process(background.Details, false);
							text = HTML.fMarkdown.Transform(text);
							text = text.Replace("<p>", "<p class=background>");
							list.Add("<H3>" + HTML.Process(background.Title, true) + "</H3>");
							list.Add(text);
						}
						if (current is EncyclopediaEntry)
						{
							EncyclopediaEntry encyclopediaEntry = current as EncyclopediaEntry;
							list.Add("<H3>" + HTML.Process(encyclopediaEntry.Name, true) + "</H3>");
							string raw_text = HTML.process_encyclopedia_info(encyclopediaEntry.Details, Session.Project.Encyclopedia, false, include_dm_info);
							list.Add("<P class=encyclopedia_entry>" + HTML.Process(raw_text, false) + "</P>");
							if (include_dm_info && encyclopediaEntry.DMInfo != "")
							{
								string raw_text2 = HTML.process_encyclopedia_info(encyclopediaEntry.DMInfo, Session.Project.Encyclopedia, false, include_dm_info);
								list.Add("<H4>For DMs Only</H4>");
								list.Add("<P class=encyclopedia_entry>" + HTML.Process(raw_text2, false) + "</P>");
							}
						}
						if (current is IPlayerOption)
						{
							IPlayerOption playerOption = current as IPlayerOption;
							list.Add("<H3>" + HTML.Process(playerOption.Name, true) + "</H3>");
							list.AddRange(HTML.get_player_option(playerOption));
						}
					}
					goto IL_1E2;
				}
			}
			list.Add("<P class=instruction>(no items selected)</P>");
			IL_1E2:
			list.Add("</BODY>");
			list.Add("</HTML>");
			return HTML.Concatenate(list);
		}

		public static string PlayerOption(IPlayerOption option, DisplaySize size)
		{
			List<string> list = new List<string>();
			list.Add("<HTML>");
			list.AddRange(HTML.GetHead(null, null, size));
			list.Add("<BODY>");
			if (option != null)
			{
				list.AddRange(HTML.get_player_option(option));
			}
			else
			{
				list.Add("<P class=instruction>(no item selected)</P>");
			}
			list.Add("</BODY>");
			list.Add("</HTML>");
			return HTML.Concatenate(list);
		}

		public static string PartyBreakdown(DisplaySize size)
		{
			List<string> list = new List<string>();
			list.Add("<HTML>");
			list.AddRange(HTML.GetHead("Party", null, size));
			list.Add("<BODY>");
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			list.Add("<TR class=heading>");
			list.Add("<TD colspan=2>");
			list.Add("<B>Party</B>");
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("<TR class=shaded>");
			list.Add("<TD colspan=2>");
			list.Add("<B>PCs</B>");
			list.Add("</TD>");
			list.Add("</TR>");
			Dictionary<HeroRoleType, int> dictionary = new Dictionary<HeroRoleType, int>();
			foreach (HeroRoleType key in Enum.GetValues(typeof(HeroRoleType)))
			{
				dictionary[key] = 0;
			}
			foreach (Hero current in Session.Project.Heroes)
			{
				string text = "<B>" + current.Name + "</B>";
				if (current.Player != "")
				{
					text = text + " (" + current.Player + ")";
				}
				string text2 = current.Race;
				if (current.Class != null && current.Class != "")
				{
					text2 = text2 + " " + current.Class;
				}
				if (current.ParagonPath != null && current.ParagonPath != "")
				{
					text2 = text2 + " / " + current.ParagonPath;
				}
				if (current.EpicDestiny != null && current.EpicDestiny != "")
				{
					text2 = text2 + " / " + current.EpicDestiny;
				}
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add(text);
				list.Add("</TD>");
				list.Add("<TD>");
				list.Add(text2);
				list.Add("</TD>");
				list.Add("</TR>");
				Dictionary<HeroRoleType, int> dictionary2;
				HeroRoleType role;
				(dictionary2 = dictionary)[role = current.Role] = dictionary2[role] + 1;
			}
			list.Add("<TR class=shaded>");
			list.Add("<TD colspan=2>");
			list.Add("<B>Roles</B>");
			list.Add("</TD>");
			list.Add("</TR>");
			foreach (HeroRoleType current2 in dictionary.Keys)
			{
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<B>" + current2 + "</B>");
				list.Add("</TD>");
				list.Add("<TD>");
				list.Add(dictionary[current2].ToString());
				list.Add("</TD>");
				list.Add("</TR>");
			}
			list.Add("</TABLE>");
			list.Add("</P>");
			list.Add("</BODY>");
			list.Add("</HTML>");
			return HTML.Concatenate(list);
		}

		public static string PCs(string secondary, DisplaySize size)
		{
			List<string> list = new List<string>();
			list.AddRange(HTML.GetHead(null, null, size));
			list.Add("<BODY>");
			if (Session.Project != null)
			{
				if (Session.Project.Heroes.Count == 0)
				{
					list.Add("<P class=instruction>");
					list.Add("No PC details have been entered; click <A href=\"party:edit\">here</A> to do this now.");
					list.Add("</P>");
					list.Add("<P class=instruction>");
					list.Add("When PCs have been entered, you will see a useful breakdown of their defences, passive skills and known languages here.");
					list.Add("</P>");
				}
				else
				{
					int num = 2147483647;
					int num2 = 2147483647;
					int num3 = 2147483647;
					int num4 = 2147483647;
					int num5 = -2147483648;
					int num6 = -2147483648;
					int num7 = -2147483648;
					int num8 = -2147483648;
					int num9 = 2147483647;
					int num10 = 2147483647;
					int num11 = -2147483648;
					int num12 = -2147483648;
					BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
					foreach (Hero current in Session.Project.Heroes)
					{
						num = Math.Min(num, current.AC);
						num2 = Math.Min(num2, current.Fortitude);
						num3 = Math.Min(num3, current.Reflex);
						num4 = Math.Min(num4, current.Will);
						num5 = Math.Max(num5, current.AC);
						num6 = Math.Max(num6, current.Fortitude);
						num7 = Math.Max(num7, current.Reflex);
						num8 = Math.Max(num8, current.Will);
						num9 = Math.Min(num9, current.PassivePerception);
						num10 = Math.Min(num10, current.PassiveInsight);
						num11 = Math.Max(num11, current.PassivePerception);
						num12 = Math.Max(num12, current.PassiveInsight);
						string text = current.Languages;
						text = text.Replace(".", "");
						text = text.Replace(",", "");
						text = text.Replace(";", "");
						text = text.Replace(":", "");
						text = text.Replace("/", "");
						string[] array = text.Split(null);
						string[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							string text2 = array2[i];
							if (text2 != "")
							{
								binarySearchTree.Add(text2);
							}
						}
					}
					list.Add("<P class=table>");
					list.Add("<TABLE class=clear>");
					list.Add("<TR class=clear>");
					list.Add("<TD class=clear>");
					list.Add("<P class=table>");
					list.Add("<TABLE>");
					list.Add("<TR class=heading>");
					list.Add("<TD colspan=3>");
					list.Add("<B>Party Breakdown</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					list.Add("<TR class=shaded>");
					list.Add("<TD colspan=3>");
					list.Add("<B>The Party</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					foreach (Hero current2 in Session.Project.Heroes)
					{
						list.Add("<TR>");
						list.Add(string.Concat(new object[]
						{
							"<TD><A href=show:",
							current2.ID,
							">",
							current2.Name,
							"</A></TD>"
						}));
						list.Add("<TD colspan=2>" + current2.Info + "</TD>");
						list.Add("</TR>");
					}
					list.Add("<TR class=shaded>");
					list.Add("<TD colspan=3>");
					list.Add("<B>Defences</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					list.Add("<TR>");
					list.Add("<TD><A href=show:ac>Armour Class</A></TD>");
					list.Add("<TD colspan=2>");
					if (num == num5)
					{
						list.Add(num.ToString());
					}
					else
					{
						list.Add(num + " - " + num5);
					}
					list.Add("</TD>");
					list.Add("</TR>");
					list.Add("<TR>");
					list.Add("<TD><A href=show:fort>Fortitude</A></TD>");
					list.Add("<TD colspan=2>");
					if (num2 == num6)
					{
						list.Add(num2.ToString());
					}
					else
					{
						list.Add(num2 + " - " + num6);
					}
					list.Add("</TD>");
					list.Add("</TR>");
					list.Add("<TR>");
					list.Add("<TD><A href=show:ref>Reflex</A></TD>");
					list.Add("<TD colspan=2>");
					if (num3 == num7)
					{
						list.Add(num3.ToString());
					}
					else
					{
						list.Add(num3 + " - " + num7);
					}
					list.Add("</TD>");
					list.Add("</TR>");
					list.Add("<TR>");
					list.Add("<TD><A href=show:will>Will</A></TD>");
					list.Add("<TD colspan=2>");
					if (num4 == num8)
					{
						list.Add(num4.ToString());
					}
					else
					{
						list.Add(num4 + " - " + num8);
					}
					list.Add("</TD>");
					list.Add("</TR>");
					list.Add("<TR class=shaded>");
					list.Add("<TD colspan=3>");
					list.Add("<B>Passive Skills</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					list.Add("<TR>");
					list.Add("<TD><A href=show:passiveinsight>Insight</A></TD>");
					list.Add("<TD colspan=2>");
					if (num10 == num12)
					{
						list.Add(num10.ToString());
					}
					else
					{
						list.Add(num10 + " - " + num12);
					}
					list.Add("</TD>");
					list.Add("</TR>");
					list.Add("<TR>");
					list.Add("<TD><A href=show:passiveperception>Perception</A></TD>");
					list.Add("<TD colspan=2>");
					if (num9 == num11)
					{
						list.Add(num9.ToString());
					}
					else
					{
						list.Add(num9 + " - " + num11);
					}
					list.Add("</TD>");
					list.Add("</TR>");
					if (binarySearchTree.Count != 0)
					{
						list.Add("<TR class=shaded>");
						list.Add("<TD colspan=3>");
						list.Add("<B>Known Languages</B>");
						list.Add("</TD>");
						list.Add("</TR>");
						List<string> sortedList = binarySearchTree.SortedList;
						foreach (string current3 in sortedList)
						{
							string text3 = "";
							foreach (Hero current4 in Session.Project.Heroes)
							{
								if (current4.Languages.Contains(current3))
								{
									if (text3 != "")
									{
										text3 += ", ";
									}
									text3 += current4.Name;
								}
							}
							list.Add("<TR>");
							list.Add("<TD>" + current3 + "</TD>");
							list.Add("<TD colspan=2>" + text3 + "</TD>");
							list.Add("</TR>");
						}
					}
					list.Add("</TABLE>");
					list.Add("</P>");
					list.Add("</TD>");
					list.Add("<TD class=clear>");
					if (secondary == "")
					{
						list.Add("<P class=instruction>");
						list.Add("Click on a link to the right to show details here");
						list.Add("</P>");
					}
					else
					{
						Guid guid = Guid.Empty;
						try
						{
							guid = new Guid(secondary);
						}
						catch
						{
							guid = Guid.Empty;
						}
						if (guid != Guid.Empty)
						{
							Hero hero = Session.Project.FindHero(guid);
							list.Add(HTML.StatBlock(hero, null, false, false, false, size));
						}
						else
						{
							string str = "";
							Dictionary<int, string> dictionary = new Dictionary<int, string>();
							if (secondary == "ac")
							{
								str = "Armour Class";
							}
							if (secondary == "fort")
							{
								str = "Fortitude";
							}
							if (secondary == "ref")
							{
								str = "Reflex";
							}
							if (secondary == "will")
							{
								str = "Will";
							}
							if (secondary == "passiveinsight")
							{
								str = "Passive Insight";
							}
							if (secondary == "passiveperception")
							{
								str = "Passive Perception";
							}
							foreach (Hero current5 in Session.Project.Heroes)
							{
								int num13 = 0;
								if (secondary == "ac")
								{
									num13 = current5.AC;
								}
								if (secondary == "fort")
								{
									num13 = current5.Fortitude;
								}
								if (secondary == "ref")
								{
									num13 = current5.Reflex;
								}
								if (secondary == "will")
								{
									num13 = current5.Will;
								}
								if (secondary == "passiveinsight")
								{
									num13 = current5.PassiveInsight;
								}
								if (secondary == "passiveperception")
								{
									num13 = current5.PassivePerception;
								}
								string text4 = string.Concat(new object[]
								{
									"<A href=show:",
									current5.ID,
									">",
									current5.Name,
									"</A>"
								});
								if (dictionary.ContainsKey(num13))
								{
									Dictionary<int, string> dictionary2;
									int key;
									(dictionary2 = dictionary)[key = num13] = dictionary2[key] + ", " + text4;
								}
								else
								{
									dictionary[num13] = text4;
								}
							}
							list.Add("<P class=table>");
							list.Add("<TABLE>");
							list.Add("<TR class=heading>");
							list.Add("<TD colspan=3>");
							list.Add("<B>" + str + "</B>");
							list.Add("</TD>");
							list.Add("</TR>");
							List<int> list2 = new List<int>(dictionary.Keys);
							list2.Sort();
							list2.Reverse();
							foreach (int current6 in list2)
							{
								list.Add("<TR>");
								list.Add("<TD>" + current6 + "</TD>");
								list.Add("<TD colspan=2>" + dictionary[current6] + "</TD>");
								list.Add("</TR>");
							}
							list.Add("</TABLE>");
							list.Add("</P>");
						}
					}
					list.Add("</TD>");
					list.Add("</TR>");
					list.Add("</TABLE>");
					list.Add("</P>");
				}
			}
			else
			{
				list.Add("<P class=instruction>");
				list.Add("(no project loaded)");
				list.Add("</P>");
			}
			list.Add("</BODY>");
			return HTML.Concatenate(list);
		}

		public static string Goal(Goal goal)
		{
			List<string> list = new List<string>();
			if (goal != null)
			{
				list.AddRange(HTML.GetHead(null, null, DisplaySize.Small));
				list.Add("<H3>" + HTML.Process(goal.Name, true) + "</H3>");
				if (goal.Details == "")
				{
					list.Add("<P class=instruction>(no details)</P>");
				}
				else
				{
					string text = HTML.Process(goal.Details, true);
					text = HTML.fMarkdown.Transform(text);
					list.Add(text);
				}
				if (goal.Prerequisites.Count != 0)
				{
					list.Add("<P><B>Prerequisite Goals</B>:</P>");
					list.Add("<UL>");
					foreach (Goal current in goal.Prerequisites)
					{
						list.Add("<LI>" + HTML.Process(current.Name, true) + "</LI>");
					}
					list.Add("</UL>");
				}
			}
			else
			{
				list.AddRange(HTML.GetHead(null, null, DisplaySize.Small));
				list.Add("<BODY>");
				list.Add("<P>On this screen you can define <B>party goals</B>.</P>");
				list.Add("<P>Party goals specify the challenges the party will face during the adventure - for example, <I>rescuing the princess</I>.</P>");
				list.Add("<P>Goals can have sub-goals - for example, <I>finding where the princess is being held</I>, <I>cracking the code that unlocks the door</I>, <I>obtaining the right tools</I>, and so on. This can go as many levels deep as you like, and you can reorder your goals by dragging them around.</P>");
				list.Add("<P>When you have finished, press <B>OK</B>; an outline plot will be built for you.</P>");
				list.Add("</BODY>");
				list.Add("</HTML>");
			}
			return HTML.Concatenate(list);
		}

		public static string EncounterReportTable(ReportTable table, DisplaySize size)
		{
			List<string> list = new List<string>();
			list.AddRange(HTML.GetHead("Encounter Log", "", DisplaySize.Small));
			list.Add("<BODY>");
			string text = "";
			switch (table.ReportType)
			{
			case ReportType.Time:
				text = "Time Taken";
				break;
			case ReportType.DamageToEnemies:
				text = "Damage (to opponents)";
				break;
			case ReportType.DamageToAllies:
				text = "Damage (to allies)";
				break;
			case ReportType.Movement:
				text = "Movement";
				break;
			}
			switch (table.BreakdownType)
			{
			case BreakdownType.Controller:
				text += " (by controller)";
				break;
			case BreakdownType.Faction:
				text += " (by faction)";
				break;
			}
			list.Add("<H3>");
			list.Add(text);
			list.Add("</H3>");
			list.Add("<P class=table>");
			list.Add("<TABLE class=wide>");
			list.Add("<TR class=encounterlog>");
			list.Add("<TD align=center>");
			list.Add("<B>Combatant</B>");
			list.Add("</TD>");
			for (int i = 1; i <= table.Rounds; i++)
			{
				list.Add("<TD align=right>");
				list.Add("<B>Round " + i + "</B>");
				list.Add("</TD>");
			}
			list.Add("<TD align=right>");
			list.Add("<B>Total</B>");
			list.Add("</TD>");
			list.Add("</TR>");
			foreach (ReportRow current in table.Rows)
			{
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<B>" + current.Heading + "</B>");
				list.Add("</TD>");
				for (int j = 0; j <= table.Rounds - 1; j++)
				{
					list.Add("<TD align=right>");
					switch (table.ReportType)
					{
					case ReportType.Time:
					{
						TimeSpan ts = new TimeSpan(0, 0, current.Values[j]);
						if (ts.TotalSeconds >= 1.0)
						{
							list.Add(HTML.get_time(ts));
						}
						else
						{
							list.Add("-");
						}
						break;
					}
					case ReportType.DamageToEnemies:
					case ReportType.DamageToAllies:
					case ReportType.Movement:
					{
						int num = current.Values[j];
						if (num != 0)
						{
							list.Add(num.ToString());
						}
						else
						{
							list.Add("-");
						}
						break;
					}
					}
					list.Add("</TD>");
				}
				list.Add("<TD align=right>");
				switch (table.ReportType)
				{
				case ReportType.Time:
				{
					TimeSpan ts2 = new TimeSpan(0, 0, current.Total);
					if (ts2.TotalSeconds >= 1.0)
					{
						list.Add(HTML.get_time(ts2));
					}
					else
					{
						list.Add("-");
					}
					break;
				}
				case ReportType.DamageToEnemies:
				case ReportType.DamageToAllies:
				case ReportType.Movement:
				{
					int total = current.Total;
					if (total != 0)
					{
						list.Add(total.ToString());
					}
					else
					{
						list.Add("-");
					}
					break;
				}
				}
				list.Add("</TD>");
				list.Add("</TR>");
			}
			list.Add("<TR>");
			list.Add("<TD>");
			list.Add("<B>Totals</B>");
			list.Add("</TD>");
			for (int k = 0; k <= table.Rounds - 1; k++)
			{
				list.Add("<TD align=right>");
				switch (table.ReportType)
				{
				case ReportType.Time:
				{
					TimeSpan ts3 = new TimeSpan(0, 0, table.Rows[k].Total);
					if (ts3.TotalSeconds >= 1.0)
					{
						list.Add(HTML.get_time(ts3));
					}
					else
					{
						list.Add("-");
					}
					break;
				}
				case ReportType.DamageToEnemies:
				case ReportType.DamageToAllies:
				case ReportType.Movement:
				{
					int total2 = table.Rows[k].Total;
					if (total2 != 0)
					{
						list.Add(total2.ToString());
					}
					else
					{
						list.Add("-");
					}
					break;
				}
				}
				list.Add("</TD>");
			}
			list.Add("<TD align=right>");
			switch (table.ReportType)
			{
			case ReportType.Time:
			{
				TimeSpan ts4 = new TimeSpan(0, 0, table.GrandTotal);
				if (ts4.TotalSeconds >= 1.0)
				{
					list.Add(HTML.get_time(ts4));
				}
				else
				{
					list.Add("-");
				}
				break;
			}
			case ReportType.DamageToEnemies:
			case ReportType.DamageToAllies:
			case ReportType.Movement:
			{
				int grandTotal = table.GrandTotal;
				if (grandTotal != 0)
				{
					list.Add(grandTotal.ToString());
				}
				else
				{
					list.Add("-");
				}
				break;
			}
			}
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("</TABLE>");
			list.Add("</P>");
			list.Add("</BODY>");
			list.Add("</HTML>");
			return HTML.Concatenate(list);
		}

		public static string TerrainPower(TerrainPower tp, DisplaySize size)
		{
			List<string> list = new List<string>();
			list.AddRange(HTML.GetHead(null, null, size));
			list.Add("<BODY>");
			list.AddRange(HTML.get_terrain_power(tp));
			list.Add("</BODY>");
			list.Add("</HTML>");
			return HTML.Concatenate(list);
		}

		public bool ExportProject(string filename)
		{
			try
			{
				string str = FileName.Directory(filename);
				this.fRelativePath = FileName.Name(filename) + " Files" + Path.DirectorySeparatorChar;
				this.fFullPath = str + this.fRelativePath;
				StreamWriter streamWriter = new StreamWriter(filename);
				List<string> content = this.get_content();
				foreach (string current in content)
				{
					streamWriter.WriteLine(current);
				}
				streamWriter.Close();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
				bool result = false;
				return result;
			}
			if (this.fPlots.Count != 0 || this.fMaps.Keys.Count != 0)
			{
				Directory.CreateDirectory(this.fFullPath);
			}
			foreach (Pair<string, Plot> current2 in this.fPlots)
			{
				try
				{
					Bitmap bitmap = Screenshot.Plot(current2.Second, new Size(800, 600));
					string filename2 = this.get_filename(current2.First, "jpg", true);
					bitmap.Save(filename2, ImageFormat.Jpeg);
				}
				catch (Exception ex2)
				{
					LogSystem.Trace(ex2);
					bool result = false;
					return result;
				}
			}
			foreach (Guid current3 in this.fMaps.Keys)
			{
				try
				{
					Map map = Session.Project.FindTacticalMap(current3);
					foreach (Guid current4 in this.fMaps[current3])
					{
						Rectangle view = Rectangle.Empty;
						if (current4 != Guid.Empty)
						{
							MapArea mapArea = map.FindArea(current4);
							view = mapArea.Region;
						}
						Bitmap bitmap2 = Screenshot.Map(map, view, null, null, null);
						string item_name = this.get_map_name(current3, current4);
						string filename3 = this.get_filename(item_name, "jpg", true);
						bitmap2.Save(filename3, ImageFormat.Jpeg);
					}
				}
				catch (Exception ex3)
				{
					LogSystem.Trace(ex3);
					bool result = false;
					return result;
				}
			}
			return true;
		}

		public static string Concatenate(List<string> lines)
		{
			string text = "";
			foreach (string current in lines)
			{
				if (text != "")
				{
					text += Environment.NewLine;
				}
				text += current;
			}
			return text;
		}

		public static string Process(string raw_text, bool strip_html)
		{
			List<Pair<string, string>> list = new List<Pair<string, string>>();
			list.Add(new Pair<string, string>("&", "&amp;"));
			list.Add(new Pair<string, string>("Á", "&Aacute;"));
			list.Add(new Pair<string, string>("á", "&aacute;"));
			list.Add(new Pair<string, string>("À", "&Agrave;"));
			list.Add(new Pair<string, string>("Â", "&Acirc;"));
			list.Add(new Pair<string, string>("à", "&agrave;"));
			list.Add(new Pair<string, string>("Â", "&Acirc;"));
			list.Add(new Pair<string, string>("â", "&acirc;"));
			list.Add(new Pair<string, string>("Ä", "&Auml;"));
			list.Add(new Pair<string, string>("ä", "&auml;"));
			list.Add(new Pair<string, string>("Ã", "&Atilde;"));
			list.Add(new Pair<string, string>("ã", "&atilde;"));
			list.Add(new Pair<string, string>("Å", "&Aring;"));
			list.Add(new Pair<string, string>("å", "&aring;"));
			list.Add(new Pair<string, string>("Æ", "&Aelig;"));
			list.Add(new Pair<string, string>("æ", "&aelig;"));
			list.Add(new Pair<string, string>("Ç", "&Ccedil;"));
			list.Add(new Pair<string, string>("ç", "&ccedil;"));
			list.Add(new Pair<string, string>("Ð", "&Eth;"));
			list.Add(new Pair<string, string>("ð", "&eth;"));
			list.Add(new Pair<string, string>("É", "&Eacute;"));
			list.Add(new Pair<string, string>("é", "&eacute;"));
			list.Add(new Pair<string, string>("È", "&Egrave;"));
			list.Add(new Pair<string, string>("è", "&egrave;"));
			list.Add(new Pair<string, string>("Ê", "&Ecirc;"));
			list.Add(new Pair<string, string>("ê", "&ecirc;"));
			list.Add(new Pair<string, string>("Ë", "&Euml;"));
			list.Add(new Pair<string, string>("ë", "&euml;"));
			list.Add(new Pair<string, string>("Í", "&Iacute;"));
			list.Add(new Pair<string, string>("í", "&iacute;"));
			list.Add(new Pair<string, string>("Ì", "&Igrave;"));
			list.Add(new Pair<string, string>("ì", "&igrave;"));
			list.Add(new Pair<string, string>("Î", "&Icirc;"));
			list.Add(new Pair<string, string>("î", "&icirc;"));
			list.Add(new Pair<string, string>("Ï", "&Iuml;"));
			list.Add(new Pair<string, string>("ï", "&iuml;"));
			list.Add(new Pair<string, string>("Ñ", "&Ntilde;"));
			list.Add(new Pair<string, string>("ñ", "&ntilde;"));
			list.Add(new Pair<string, string>("Ó", "&Oacute;"));
			list.Add(new Pair<string, string>("ó", "&oacute;"));
			list.Add(new Pair<string, string>("Ò", "&Ograve;"));
			list.Add(new Pair<string, string>("ò", "&ograve;"));
			list.Add(new Pair<string, string>("Ô", "&Ocirc;"));
			list.Add(new Pair<string, string>("ô", "&ocirc;"));
			list.Add(new Pair<string, string>("Ö", "&Ouml;"));
			list.Add(new Pair<string, string>("ö", "&ouml;"));
			list.Add(new Pair<string, string>("Õ", "&Otilde;"));
			list.Add(new Pair<string, string>("õ", "&otilde;"));
			list.Add(new Pair<string, string>("Ø", "&Oslash;"));
			list.Add(new Pair<string, string>("ø", "&oslash;"));
			list.Add(new Pair<string, string>("ß", "&szlig;"));
			list.Add(new Pair<string, string>("Þ", "&Thorn;"));
			list.Add(new Pair<string, string>("þ", "&thorn;"));
			list.Add(new Pair<string, string>("Ú", "&Uacute;"));
			list.Add(new Pair<string, string>("ú", "&uacute;"));
			list.Add(new Pair<string, string>("Ù", "&Ugrave;"));
			list.Add(new Pair<string, string>("ù", "&ugrave;"));
			list.Add(new Pair<string, string>("Û", "&Ucirc;"));
			list.Add(new Pair<string, string>("û", "&ucirc;"));
			list.Add(new Pair<string, string>("Ü", "&Uuml;"));
			list.Add(new Pair<string, string>("ü", "&uuml;"));
			list.Add(new Pair<string, string>("Ý", "&Yacute;"));
			list.Add(new Pair<string, string>("ý", "&yacute;"));
			list.Add(new Pair<string, string>("ÿ", "&yuml;"));
			list.Add(new Pair<string, string>("©", "&copy;"));
			list.Add(new Pair<string, string>("®", "&reg;"));
			list.Add(new Pair<string, string>("™", "&trade;"));
			list.Add(new Pair<string, string>("€", "&euro;"));
			list.Add(new Pair<string, string>("¢", "&cent;"));
			list.Add(new Pair<string, string>("£", "&pound;"));
			list.Add(new Pair<string, string>("‘", "&lsquo;"));
			list.Add(new Pair<string, string>("’", "&rsquo;"));
			list.Add(new Pair<string, string>("“", "&ldquo;"));
			list.Add(new Pair<string, string>("”", "&rdquo;"));
			list.Add(new Pair<string, string>("«", "&laquo;"));
			list.Add(new Pair<string, string>("»", "&raquo;"));
			list.Add(new Pair<string, string>("—", "&mdash;"));
			list.Add(new Pair<string, string>("–", "&ndash;"));
			list.Add(new Pair<string, string>("°", "&deg;"));
			list.Add(new Pair<string, string>("±", "&plusmn;"));
			list.Add(new Pair<string, string>("¼", "&frac14;"));
			list.Add(new Pair<string, string>("½", "&frac12;"));
			list.Add(new Pair<string, string>("¾", "&frac34;"));
			list.Add(new Pair<string, string>("×", "&times;"));
			list.Add(new Pair<string, string>("÷", "&divide;"));
			list.Add(new Pair<string, string>("α", "&alpha;"));
			list.Add(new Pair<string, string>("β", "&beta;"));
			list.Add(new Pair<string, string>("∞", "&infin;"));
			if (strip_html)
			{
				list.Add(new Pair<string, string>("\"", "&quot;"));
				list.Add(new Pair<string, string>("<", "&lt;"));
				list.Add(new Pair<string, string>(">", "&gt;"));
			}
			string text = raw_text;
			foreach (Pair<string, string> current in list)
			{
				text = text.Replace(current.First, current.Second);
			}
			return text;
		}

		public static List<string> GetHead(string title, string description, DisplaySize size)
		{
			List<string> list = new List<string>();
			list.Add("<HEAD>");
			if (title != null)
			{
				list.Add(HTML.wrap(title, "title"));
			}
			if (description != null)
			{
				list.Add("<META name=\"Description\" content=\"" + description + "\">");
			}
			list.Add("<META name=\"Generator\" content=\"Masterplan\">");
			list.Add("<META name=\"Originator\" content=\"Masterplan\">");
			list.AddRange(HTML.GetStyle(size));
			list.Add("</HEAD>");
			return list;
		}

		public static List<string> GetStyle(DisplaySize size)
		{
			if (HTML.fStyles.ContainsKey(size))
			{
				return HTML.fStyles[size];
			}
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			switch (size)
			{
			case DisplaySize.Small:
				dictionary[8] = 8;
				dictionary[9] = 9;
				dictionary[12] = 12;
				dictionary[16] = 16;
				dictionary[24] = 24;
				break;
			case DisplaySize.Medium:
				dictionary[8] = 16;
				dictionary[9] = 18;
				dictionary[12] = 24;
				dictionary[16] = 32;
				dictionary[24] = 48;
				break;
			case DisplaySize.Large:
				dictionary[8] = 25;
				dictionary[9] = 30;
				dictionary[12] = 40;
				dictionary[16] = 50;
				dictionary[24] = 72;
				break;
			}
			Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
			switch (size)
			{
			case DisplaySize.Small:
				dictionary2[15] = 15;
				dictionary2[300] = 300;
				break;
			case DisplaySize.Medium:
				dictionary2[15] = 30;
				dictionary2[300] = 600;
				break;
			case DisplaySize.Large:
				dictionary2[15] = 45;
				dictionary2[300] = 1000;
				break;
			}
			List<string> list = new List<string>();
			list.Add("<STYLE type=\"text/css\">");
			bool flag = false;
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			if (entryAssembly != null)
			{
				string path = string.Concat(new object[]
				{
					FileName.Directory(entryAssembly.Location),
					"Style.",
					size,
					".css"
				});
				if (File.Exists(path))
				{
					string[] collection = File.ReadAllLines(path);
					list.AddRange(collection);
					flag = true;
				}
			}
			if (!flag)
			{
				list.Add("body                 { font-family: Arial; font-size: " + dictionary[9] + "pt }");
				list.Add("h1, h2, h3, h4       { color: #000060 }");
				list.Add("h1                   { font-size: " + dictionary[24] + "pt; font-weight: bold; text-align: center }");
				list.Add("h2                   { font-size: " + dictionary[16] + "pt; font-weight: bold; text-align: center }");
				list.Add("h3                   { font-size: " + dictionary[12] + "pt }");
				list.Add("h4                   { font-size: " + dictionary[9] + "pt }");
				list.Add("p                    { }");
				list.Add("p.instruction        { color: #666666; text-align: center; font-size: " + dictionary[8] + "pt }");
				list.Add("p.description        { }");
				list.Add("p.signature          { color: #666666; text-align: center }");
				list.Add(string.Concat(new object[]
				{
					"p.readaloud          { padding-left: ",
					dictionary2[15],
					"px; padding-right: ",
					dictionary2[15],
					"px; font-style: italic }"
				}));
				list.Add("p.background         { }");
				list.Add("p.encounter_note     { }");
				list.Add("p.encyclopedia_entry { }");
				list.Add("p.note               { }");
				list.Add("p.table              { text-align: center }");
				list.Add("p.figure             { text-align: center }");
				list.Add(string.Concat(new object[]
				{
					"table                { font-size: ",
					dictionary[8],
					"pt; border-color: #BBBBBB; border-style: solid; border-width: 1px; border-collapse: collapse; table-layout: fixed; width: ",
					dictionary2[300],
					"px }"
				}));
				list.Add("table.clear          { border-style: none; table-layout: fixed; width: 99% }");
				list.Add("table.wide           { width: 99% }");
				list.Add("table.initiative     { table-layout: auto; border-style: none; width=99% }");
				list.Add("tr                   { background-color: #E1E7C5 }");
				list.Add("tr.clear             { background-color: #FFFFFF }");
				list.Add("tr.heading           { background-color: #143D5F; color: #FFFFFF }");
				list.Add("tr.trap              { background-color: #5B1F34; color: #FFFFFF }");
				list.Add("tr.template          { background-color: #5B1F34; color: #FFFFFF }");
				list.Add("tr.creature          { background-color: #364F27; color: #FFFFFF }");
				list.Add("tr.hero              { background-color: #143D5F; color: #FFFFFF }");
				list.Add("tr.item              { background-color: #D06015; color: #FFFFFF }");
				list.Add("tr.artifact          { background-color: #5B1F34; color: #FFFFFF }");
				list.Add("tr.encounterlog      { background-color: #303030; color: #FFFFFF }");
				list.Add("tr.shaded            { background-color: #9FA48D }");
				list.Add("tr.dimmed            { color: #666666; text-decoration: line-through }");
				list.Add("tr.shaded_dimmed     { background-color: #9FA48D; color: #666666 }");
				list.Add("tr.atwill            { background-color: #238E23; color: #FFFFFF }");
				list.Add("tr.encounter         { background-color: #8B0000; color: #FFFFFF }");
				list.Add("tr.daily             { background-color: #000000; color: #FFFFFF }");
				list.Add("tr.warning           { background-color: #E5A0A0; color: #000000; text-align: center }");
				list.Add("td                   { padding-top: 2px; padding-bottom: 2px; vertical-align: top }");
				list.Add("td.clear             { vertical-align: top }");
				list.Add("td.indent            { padding-left: " + dictionary2[15] + "px }");
				list.Add("td.readaloud         { font-style: italic }");
				list.Add("td.dimmed            { color: #666666 }");
				list.Add("td.pvlogentry        { color: lightgrey; background-color: #000000 }");
				list.Add("td.pvlogindent       { color: #FFFFFF; background-color: #000000; padding-left: " + dictionary2[15] + "px }");
				list.Add("ul, ol               { font-size: " + dictionary[8] + "pt }");
				list.Add("a                    { text-decoration: none }");
				list.Add("a:link               { color: #0000C0 }");
				list.Add("a:visited            { color: #0000C0 }");
				list.Add("a:active             { color: #FF0000 }");
				list.Add("a.missing            { color: #FF0000 }");
				list.Add("a:hover              { text-decoration: underline }");
			}
			list.Add("</STYLE>");
			HTML.fStyles[size] = list;
			return list;
		}

		private static string wrap(string content, string tag)
		{
			string str = "<" + tag.ToUpper() + ">";
			string str2 = "</" + tag.ToUpper() + ">";
			return str + content + str2;
		}

		private List<string> get_content()
		{
			List<string> list = new List<string>();
			string description = Session.Project.Name + ": " + this.get_description();
			list.Add("<HTML>");
			list.AddRange(HTML.GetHead(Session.Project.Name, description, DisplaySize.Small));
			list.AddRange(this.get_body());
			list.Add("</HTML>");
			return list;
		}

		private List<string> get_body()
		{
			List<string> list = new List<string>();
			list.Add("<BODY>");
			list.Add("<H1>" + Session.Project.Name + "</H1>");
			list.Add("<P class=description>" + this.get_description() + "</P>");
			if (Session.Project.Author != "")
			{
				list.Add("<P class=description>By " + HTML.Process(Session.Project.Author, true) + "</P>");
			}
			if (Session.Project.Backgrounds.Count != 0)
			{
				list.AddRange(this.get_backgrounds());
			}
			if (Session.Project.Plot.Points.Count != 0)
			{
				list.Add("<HR>");
				list.AddRange(this.get_full_plot());
			}
			if (Session.Project.NPCs.Count != 0)
			{
				list.Add("<HR>");
				list.AddRange(this.get_npcs());
			}
			if (Session.Project.Encyclopedia.Entries.Count != 0)
			{
				list.Add("<HR>");
				list.AddRange(this.get_encyclopedia());
			}
			if (Session.Project.Notes.Count != 0)
			{
				list.Add("<HR>");
				list.AddRange(this.get_notes());
			}
			list.Add("<HR>");
			list.Add("<P class=signature>Designed using <A href=\"http://www.habitualindolence.net/masterplan\">Masterplan</A></P>");
			list.Add("</BODY>");
			return list;
		}

		private List<string> get_backgrounds()
		{
			List<string> list = new List<string>();
			foreach (Background current in Session.Project.Backgrounds)
			{
				if (!(current.Details == ""))
				{
					list.Add(HTML.wrap(HTML.Process(current.Title, true), "h3"));
					list.Add("<P class=background>" + HTML.Process(current.Details, false) + "</P>");
				}
			}
			return list;
		}

		private List<string> get_full_plot()
		{
			List<string> list = new List<string>();
			list.Add(HTML.wrap(HTML.Process(Session.Project.Name, true), "h2"));
			list.AddRange(this.get_plot(Session.Project.Name, Session.Project.Plot));
			return list;
		}

		private List<string> get_npcs()
		{
			List<string> list = new List<string>();
			list.Add(HTML.wrap("Encyclopedia", "h2"));
			foreach (NPC current in Session.Project.NPCs)
			{
				list.Add(HTML.wrap(HTML.Process(current.Name, true), "h3"));
				string text = HTML.Process(current.Details, true);
				if (text != "")
				{
					list.Add("<P>" + text + "</P>");
				}
				list.Add("<P class=table>");
				list.AddRange(new EncounterCard
				{
					CreatureID = current.ID
				}.AsText(null, CardMode.View, true));
				list.Add("</P>");
			}
			return list;
		}

		private List<string> get_encyclopedia()
		{
			List<string> list = new List<string>();
			list.Add(HTML.wrap("Encyclopedia", "h2"));
			foreach (EncyclopediaEntry current in Session.Project.Encyclopedia.Entries)
			{
				list.Add(HTML.wrap(HTML.Process(current.Name, true), "h3"));
				list.Add("<P class=encyclopedia_entry>" + HTML.Process(current.Details, false) + "</P>");
			}
			return list;
		}

		private static string process_encyclopedia_info(string details, Encyclopedia encyclopedia, bool include_entry_links, bool include_dm_info)
		{
			while (true)
			{
				string text = "[[DM]]";
				int num = details.IndexOf(text);
				if (num == -1)
				{
					break;
				}
				int num2 = details.IndexOf(text, num + text.Length);
				if (num2 == -1)
				{
					break;
				}
				int num3 = num + text.Length;
				string text2 = details.Substring(num3, num2 - num3);
				if (include_dm_info)
				{
					details = string.Concat(new string[]
					{
						details.Substring(0, num),
						"<B>",
						text2,
						"</B>",
						details.Substring(num2 + text.Length)
					});
				}
				else
				{
					details = details.Substring(0, num) + details.Substring(num2 + text.Length);
				}
			}
			while (true)
			{
				string text3 = "[[";
				string text4 = "]]";
				int num4 = details.IndexOf(text3);
				if (num4 == -1)
				{
					break;
				}
				int num5 = details.IndexOf(text4, num4 + text3.Length);
				if (num5 == -1)
				{
					break;
				}
				int num6 = num4 + text3.Length;
				string text5 = details.Substring(num6, num5 - num6);
				string text6 = text5;
				string text7 = text5;
				if (text5.Contains("|"))
				{
					int num7 = text5.IndexOf("|");
					text6 = text5.Substring(0, num7);
					text7 = text5.Substring(num7 + 1);
					text7 = text7.Trim();
				}
				string str;
				if (include_entry_links)
				{
					EncyclopediaEntry encyclopediaEntry = encyclopedia.FindEntry(text6);
					if (encyclopediaEntry == null)
					{
						str = string.Concat(new string[]
						{
							"<A class=\"missing\" href=\"missing:",
							text6,
							"\" title=\"Create entry '",
							text6,
							"'\">",
							text7,
							"</A>"
						});
					}
					else
					{
						str = string.Concat(new object[]
						{
							"<A href=\"entry:",
							encyclopediaEntry.ID,
							"\" title=\"",
							encyclopediaEntry.Name,
							"\">",
							text7,
							"</A>"
						});
					}
				}
				else
				{
					str = text7;
				}
				details = details.Substring(0, num4) + str + details.Substring(num5 + text4.Length);
			}
			details = HTML.fMarkdown.Transform(details);
			details = details.Replace("<p>", "<p class=encyclopedia_entry>");
			return details;
		}

		private List<string> get_notes()
		{
			List<string> list = new List<string>();
			list.Add(HTML.wrap("Notes", "h2"));
			foreach (Note current in Session.Project.Notes)
			{
				list.Add("<P class=note>" + HTML.Process(current.Content, true) + "</P>");
			}
			return list;
		}

		private string get_description()
		{
			return string.Concat(new object[]
			{
				"An adventure for ",
				Session.Project.Party.Size,
				" characters of level ",
				Session.Project.Party.Level,
				"."
			});
		}

		private List<string> get_plot(string plot_name, Plot p)
		{
			List<string> list = new List<string>();
			if (p.Points.Count > 1)
			{
				this.fPlots.Add(new Pair<string, Plot>(plot_name, p));
				string text = this.get_filename(plot_name, "jpg", false);
				list.Add(string.Concat(new string[]
				{
					"<P class=figure><A href=\"",
					text,
					"\"><IMG src=\"",
					text,
					"\" alt=\"",
					HTML.Process(plot_name, true),
					"\" height=200></A></P>"
				}));
			}
			List<List<PlotPoint>> list2 = Workspace.FindLayers(p);
			foreach (List<PlotPoint> current in list2)
			{
				foreach (PlotPoint current2 in current)
				{
					list.AddRange(this.get_plot_point(current2));
				}
			}
			return list;
		}

		private List<string> get_plot_point(PlotPoint pp)
		{
			List<string> list = new List<string>();
			list.Add(HTML.wrap(HTML.Process(pp.Name, true), "h3"));
			if (pp.ReadAloud != "")
			{
				list.Add("<P class=readaloud>" + HTML.Process(pp.ReadAloud, false) + "</P>");
			}
			if (pp.Details != "")
			{
				list.Add("<P>" + HTML.Process(pp.Details, false) + "</P>");
			}
			if (pp.Date != null)
			{
				list.Add("<P>Date: " + pp.Date + "</P>");
			}
			Encounter encounter = pp.Element as Encounter;
			if (encounter != null)
			{
				list.AddRange(HTML.get_encounter(encounter));
				if (encounter.MapID != Guid.Empty)
				{
					this.add_map(encounter.MapID, encounter.MapAreaID);
					string item_name = this.get_map_name(encounter.MapID, encounter.MapAreaID);
					string text = this.get_filename(item_name, "jpg", false);
					list.Add(string.Concat(new string[]
					{
						"<P class=figure><A href=\"",
						text,
						"\"><IMG src=\"",
						text,
						"\" alt=\"",
						HTML.Process(pp.Name, true),
						"\" height=200></A></P>"
					}));
				}
			}
			TrapElement trapElement = pp.Element as TrapElement;
			if (trapElement != null)
			{
				list.AddRange(HTML.get_trap(trapElement.Trap, null, false, false));
			}
			SkillChallenge skillChallenge = pp.Element as SkillChallenge;
			if (skillChallenge != null)
			{
				list.AddRange(HTML.get_skill_challenge(skillChallenge, false));
			}
			Quest quest = pp.Element as Quest;
			if (quest != null)
			{
				list.AddRange(HTML.get_quest(quest));
			}
			MapElement mapElement = pp.Element as MapElement;
			if (mapElement != null && mapElement.MapID != Guid.Empty)
			{
				this.add_map(mapElement.MapID, mapElement.MapAreaID);
				string text2 = this.get_map_name(mapElement.MapID, mapElement.MapAreaID);
				string text3 = this.get_filename(text2, "jpg", false);
				list.Add(string.Concat(new string[]
				{
					"<P class=figure><A href=\"",
					text3,
					"\"><IMG src=\"",
					text3,
					"\" alt=\"",
					HTML.Process(text2, true),
					"\" height=200></A></P>"
				}));
			}
			if (pp.Parcels.Count != 0)
			{
				list.AddRange(HTML.get_parcels(pp, false));
			}
			if (pp.Subplot.Points.Count != 0)
			{
				list.Add("<BLOCKQUOTE>");
				list.AddRange(this.get_plot(pp.Name, pp.Subplot));
				list.Add("</BLOCKQUOTE>");
			}
			return list;
		}

		private static List<string> get_map_area_details(PlotPoint pp)
		{
			List<string> list = new List<string>();
			Map map = null;
			MapArea mapArea = null;
			pp.GetTacticalMapArea(ref map, ref mapArea);
			if (map != null && mapArea != null && mapArea.Details != "")
			{
				list.Add("<P><B>" + HTML.Process(mapArea.Name, true) + "</B>:</P>");
				list.Add("<P>" + HTML.Process(mapArea.Details, true) + "</P>");
			}
			return list;
		}

		private static List<string> get_encounter(Encounter enc)
		{
			List<string> list = new List<string>();
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			list.Add("<TR class=heading>");
			list.Add("<TD colspan=2>");
			list.Add("<B>Encounter</B>");
			list.Add("</TD>");
			list.Add("<TD>");
			list.Add(enc.GetXP() + " XP");
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD colspan=3>");
			list.Add("<B>Level</B> " + enc.GetLevel(Session.Project.Party.Size));
			list.Add("</TD>");
			list.Add("</TR>");
			if (enc.Slots.Count != 0)
			{
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=2>");
				list.Add("<B>Combatants</B>");
				list.Add("</TD>");
				list.Add("<TD>");
				list.Add("<B>" + enc.Count + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				foreach (EncounterSlot current in enc.Slots)
				{
					list.Add("<TR>");
					list.Add("<TD colspan=2>");
					list.Add(current.Card.Title);
					list.Add("</TD>");
					list.Add("<TD>");
					if (current.CombatData.Count > 1)
					{
						list.Add("x" + current.CombatData.Count);
					}
					list.Add("</TD>");
					list.Add("</TR>");
				}
			}
			foreach (EncounterWave current2 in enc.Waves)
			{
				if (current2.Count != 0)
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD colspan=2>");
					list.Add("<B>" + current2.Name + "</B>");
					list.Add("</TD>");
					list.Add("<TD>");
					list.Add("<B>" + current2.Count + "</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					foreach (EncounterSlot current3 in current2.Slots)
					{
						list.Add("<TR>");
						list.Add("<TD colspan=2>");
						list.Add(current3.Card.Title);
						list.Add("</TD>");
						list.Add("<TD>");
						if (current3.CombatData.Count > 1)
						{
							list.Add("x" + current3.CombatData.Count);
						}
						list.Add("</TD>");
						list.Add("</TR>");
					}
				}
			}
			if (enc.Traps.Count != 0)
			{
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=2>");
				list.Add("<B>Traps / Hazards</B>");
				list.Add("</TD>");
				list.Add("<TD>");
				list.Add("<B>" + enc.Traps.Count + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				foreach (Trap current4 in enc.Traps)
				{
					list.Add("<TR>");
					list.Add("<TD colspan=3>");
					list.Add(HTML.Process(current4.Name, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
			}
			if (enc.SkillChallenges.Count != 0)
			{
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=2>");
				list.Add("<B>Skill Challenges</B>");
				list.Add("</TD>");
				list.Add("<TD>");
				list.Add("<B>" + enc.SkillChallenges.Count + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				foreach (SkillChallenge current5 in enc.SkillChallenges)
				{
					list.Add("<TR>");
					list.Add("<TD colspan=3>");
					list.Add(HTML.Process(current5.Name, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
			}
			list.Add("</TABLE>");
			list.Add("</P>");
			foreach (EncounterNote current6 in enc.Notes)
			{
				if (!(current6.Contents == ""))
				{
					list.Add("<P class=encounter_note>");
					list.Add("<B>" + HTML.Process(current6.Title, true) + "</B>");
					list.Add("</P>");
					list.Add("<P class=encounter_note>");
					list.Add(HTML.Process(current6.Contents, false));
					list.Add("</P>");
				}
			}
			List<string> list2 = new List<string>();
			foreach (EncounterSlot current7 in enc.AllSlots)
			{
				if (!list2.Contains(current7.Card.Title))
				{
					list.Add("<P class=table>");
					list.AddRange(current7.Card.AsText(null, CardMode.View, true));
					list.Add("</P>");
					list2.Add(current7.Card.Title);
				}
			}
			foreach (Trap current8 in enc.Traps)
			{
				list.AddRange(HTML.get_trap(current8, null, false, false));
			}
			foreach (SkillChallenge current9 in enc.SkillChallenges)
			{
				list.AddRange(HTML.get_skill_challenge(current9, false));
			}
			foreach (CustomToken current10 in enc.CustomTokens)
			{
				if (current10.Type != CustomTokenType.Token)
				{
					list.AddRange(HTML.get_custom_token(current10));
				}
			}
			return list;
		}

		private static List<string> get_trap(Trap trap, CombatData cd, bool initiative_holder, bool builder)
		{
			List<string> list = new List<string>();
			if (initiative_holder)
			{
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=heading>");
				list.Add("<TD>");
				list.Add("<B>Information</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add(HTML.Process(trap.Name, true) + " is the current initiative holder");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			list.Add("<TR class=trap>");
			list.Add("<TD colspan=2>");
			list.Add("<B>" + HTML.Process(trap.Name, true) + "</B>");
			list.Add("<BR>");
			list.Add(HTML.Process(trap.Info, true));
			list.Add("</TD>");
			list.Add("<TD>");
			list.Add(trap.XP + " XP");
			list.Add("</TD>");
			list.Add("</TR>");
			if (builder)
			{
				list.Add("<TR class=trap>");
				list.Add("<TD colspan=3 align=center>");
				list.Add("<A href=build:profile style=\"color:white\">(click here to edit this top section)</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			if (builder)
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3 align=center>");
				list.Add("<A href=build:addskill>add a skill</A>");
				list.Add("|");
				list.Add("<A href=build:addattack>add an attack</A>");
				list.Add("|");
				list.Add("<A href=build:addcm>add a countermeasure</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			string text = HTML.Process(trap.ReadAloud, true);
			if (builder)
			{
				if (text == "")
				{
					text = "<A href=build:readaloud>Click here to enter read-aloud text</A>";
				}
				else
				{
					text += " <A href=build:readaloud>(edit)</A>";
				}
			}
			if (text != "")
			{
				list.Add("<TR class=shaded>");
				list.Add("<TD class=readaloud colspan=3>");
				list.Add(text);
				list.Add("</TD>");
				list.Add("</TR>");
			}
			string text2 = HTML.Process(trap.Description, true);
			if (builder)
			{
				if (text2 == "")
				{
					text2 = "<A href=build:desc>Click here to enter a description</A>";
				}
				else
				{
					text2 += " <A href=build:desc>(edit)</A>";
				}
			}
			if (text2 != "")
			{
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=3>");
				list.Add(text2);
				list.Add("</TD>");
				list.Add("</TR>");
			}
			string text3 = HTML.Process(trap.Details, true);
			if (builder)
			{
				if (text3 == "")
				{
					text3 = "<A href=build:details>(no trap details entered)</A>";
				}
				else
				{
					text3 += " <A href=build:details>(edit)</A>";
				}
			}
			if (text3 != "")
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>" + trap.Type + "</B>: ");
				list.Add(text3);
				list.Add("</TD>");
				list.Add("</TR>");
			}
			List<string> list2 = new List<string>();
			Dictionary<string, List<TrapSkillData>> dictionary = new Dictionary<string, List<TrapSkillData>>();
			foreach (TrapSkillData current in trap.Skills)
			{
				if (!(current.Details == ""))
				{
					if (current.SkillName != "Perception" && !list2.Contains(current.SkillName))
					{
						list2.Add(current.SkillName);
					}
					if (!dictionary.ContainsKey(current.SkillName))
					{
						dictionary[current.SkillName] = new List<TrapSkillData>();
					}
					dictionary[current.SkillName].Add(current);
				}
			}
			list2.Sort();
			if (dictionary.ContainsKey("Perception"))
			{
				list2.Insert(0, "Perception");
			}
			foreach (string current2 in list2)
			{
				List<TrapSkillData> list3 = dictionary[current2];
				list3.Sort();
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=3>");
				list.Add("<B>" + HTML.Process(current2, true) + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				foreach (TrapSkillData current3 in list3)
				{
					list.Add("<TR>");
					list.Add("<TD colspan=3>");
					if (current3.DC != 0)
					{
						list.Add("<B>DC " + current3.DC + "</B>:");
					}
					list.Add(HTML.Process(current3.Details, true));
					if (builder)
					{
						list.Add(string.Concat(new object[]
						{
							"(<A href=skill:",
							current3.ID,
							">edit</A> | <A href=skillremove:",
							current3.ID,
							">remove</A>)"
						}));
					}
					list.Add("</TD>");
					list.Add("</TR>");
				}
			}
			if (trap.Initiative != -2147483648)
			{
				string text4 = trap.Initiative.ToString();
				if (trap.Initiative >= 0)
				{
					text4 = "+" + text4;
				}
				if (cd != null)
				{
					text4 = string.Concat(new object[]
					{
						cd.Initiative,
						" (",
						text4,
						")"
					});
				}
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Initiative</B>:");
				if (builder)
				{
					list.Add("<A href=build:profile>");
				}
				if (cd != null)
				{
					list.Add("<A href=init:" + cd.ID + ">");
				}
				list.Add(text4);
				if (cd != null)
				{
					list.Add("</A>");
				}
				if (builder)
				{
					list.Add("</A>");
				}
				list.Add("</TD>");
				list.Add("</TR>");
			}
			else if (builder)
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Initiative</B>: <A href=build:profile>The trap does not roll initiative</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			if (trap.Trigger != "")
			{
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Trigger</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				if (builder)
				{
					list.Add("<A href=build:trigger>");
				}
				list.Add(HTML.Process(trap.Trigger, true));
				if (builder)
				{
					list.Add("</A>");
				}
				list.Add("</TD>");
				list.Add("</TR>");
			}
			else if (builder)
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Trigger</B>: <A href=build:trigger>Set trigger</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			foreach (TrapAttack current4 in trap.Attacks)
			{
				list.AddRange(HTML.get_trap_attack(current4, cd != null, builder));
			}
			if (trap.Countermeasures.Count != 0)
			{
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Countermeasures</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				for (int num = 0; num != trap.Countermeasures.Count; num++)
				{
					string raw_text = trap.Countermeasures[num];
					list.Add("<TR>");
					list.Add("<TD colspan=3>");
					if (builder)
					{
						list.Add("<A href=cm:" + num + ">");
					}
					list.Add(HTML.Process(raw_text, true));
					if (builder)
					{
						list.Add("</A>");
					}
					list.Add("</TD>");
					list.Add("</TR>");
				}
			}
			Trap trap2 = Session.FindTrap(trap.ID, SearchType.External);
			if (trap2 != null)
			{
				Library library = Session.FindLibrary(trap2);
				if (library != null)
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD colspan=3>");
					list.Add(HTML.Process(library.Name, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
			}
			list.Add("</TABLE>");
			list.Add("</P>");
			return list;
		}

		private static List<string> get_trap_attack(TrapAttack trap_attack, bool links, bool builder)
		{
			List<string> list = new List<string>();
			string text = trap_attack.Name;
			if (text == "")
			{
				text = "Attack";
			}
			list.Add("<TR class=shaded>");
			list.Add("<TD colspan=3>");
			list.Add("<B>" + text + "</B>");
			if (builder)
			{
				list.Add("<A href=attackaction:" + trap_attack.ID + ">");
				list.Add("(edit)");
				list.Add("</A>");
				list.Add("<A href=attackremove:" + trap_attack.ID + ">");
				list.Add("(remove)");
				list.Add("</A>");
			}
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD colspan=3>");
			list.Add("<B>Action</B>:");
			if (builder)
			{
				list.Add("<A href=attackaction:" + trap_attack.ID + ">");
			}
			list.Add(trap_attack.Action.ToString().ToLower());
			if (builder)
			{
				list.Add("</A>");
			}
			list.Add("</TD>");
			list.Add("</TR>");
			if (trap_attack.Range != "")
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Range</B>:");
				if (builder)
				{
					list.Add("<A href=attackaction:" + trap_attack.ID + ">");
				}
				list.Add(HTML.Process(trap_attack.Range, true));
				if (builder)
				{
					list.Add("</A>");
				}
				list.Add("</TD>");
				list.Add("</TR>");
			}
			else if (builder)
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Range</B>: <A href=attackaction:" + trap_attack.ID + ">Set range</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			if (trap_attack.Target != "")
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Target</B>:");
				if (builder)
				{
					list.Add("<A href=attackaction:" + trap_attack.ID + ">");
				}
				list.Add(HTML.Process(trap_attack.Target, true));
				if (builder)
				{
					list.Add("</A>");
				}
				list.Add("</TD>");
				list.Add("</TR>");
			}
			else if (builder)
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Target</B>: <A href=attackaction:" + trap_attack.ID + ">Set target</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			if (trap_attack.Attack != null)
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Attack</B>:");
				if (builder)
				{
					list.Add("<A href=attackattack:" + trap_attack.ID + ">");
				}
				if (links)
				{
					list.Add("<A href=power:" + trap_attack.ID + ">");
				}
				list.Add(trap_attack.Attack.ToString());
				if (links)
				{
					list.Add("</A>");
				}
				if (builder)
				{
					list.Add("</A>");
				}
				list.Add("</TD>");
				list.Add("</TR>");
			}
			else if (builder)
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Attack</B>: <A href=attackattack:" + trap_attack.ID + ">Set attack</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			if (trap_attack.OnHit != "")
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Hit</B>:");
				if (builder)
				{
					list.Add("<A href=attackhit:" + trap_attack.ID + ">");
				}
				list.Add(HTML.Process(trap_attack.OnHit, true));
				if (builder)
				{
					list.Add("</A>");
				}
				list.Add("</TD>");
				list.Add("</TR>");
			}
			else if (builder)
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Hit</B>: <A href=attackhit:" + trap_attack.ID + ">Set hit</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			if (trap_attack.OnMiss != "")
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Miss</B>:");
				if (builder)
				{
					list.Add("<A href=attackmiss:" + trap_attack.ID + ">");
				}
				list.Add(HTML.Process(trap_attack.OnMiss, true));
				if (builder)
				{
					list.Add("</A>");
				}
				list.Add("</TD>");
				list.Add("</TR>");
			}
			else if (builder)
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Miss</B>: <A href=attackmiss:" + trap_attack.ID + ">Set miss</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			if (trap_attack.Effect != "")
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Effect</B>:");
				if (builder)
				{
					list.Add("<A href=attackeffect:" + trap_attack.ID + ">");
				}
				list.Add(HTML.Process(trap_attack.Effect, true));
				if (builder)
				{
					list.Add("</A>");
				}
				list.Add("</TD>");
				list.Add("</TR>");
			}
			else if (builder)
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Effect</B>: <A href=attackeffect:" + trap_attack.ID + ">Set effect</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			if (trap_attack.Notes != "")
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Notes</B>:");
				if (builder)
				{
					list.Add("<A href=attacknotes:" + trap_attack.ID + ">");
				}
				list.Add(HTML.Process(trap_attack.Notes, true));
				if (builder)
				{
					list.Add("</A>");
				}
				list.Add("</TD>");
				list.Add("</TR>");
			}
			else if (builder)
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Notes</B>: <A href=attacknotes:" + trap_attack.ID + ">Set notes</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			return list;
		}

		private static List<string> get_skill_challenge(SkillChallenge sc, bool include_links)
		{
			List<SkillChallengeData> list = new List<SkillChallengeData>();
			List<SkillChallengeData> list2 = new List<SkillChallengeData>();
			List<SkillChallengeData> list3 = new List<SkillChallengeData>();
			foreach (SkillChallengeData current in sc.Skills)
			{
				switch (current.Type)
				{
				case SkillType.Primary:
					list.Add(current);
					break;
				case SkillType.Secondary:
					list2.Add(current);
					break;
				case SkillType.AutoFail:
					list3.Add(current);
					break;
				}
			}
			List<string> list4 = new List<string>();
			list4.Add("<P class=table>");
			list4.Add("<TABLE>");
			list4.Add("<TR class=trap>");
			list4.Add("<TD colspan=2>");
			list4.Add("<B>" + HTML.Process(sc.Name, true) + "</B>");
			list4.Add("</TD>");
			list4.Add("<TD>");
			list4.Add(sc.GetXP() + " XP");
			list4.Add("</TD>");
			list4.Add("</TR>");
			list4.Add("<TR>");
			list4.Add("<TD colspan=3>");
			list4.Add("<B>Level</B> " + sc.Level);
			list4.Add("<BR>");
			list4.Add(string.Concat(new object[]
			{
				"<B>Complexity</B> ",
				sc.Complexity,
				" (requires ",
				sc.Successes,
				" successes before 3 failures)"
			}));
			list4.Add("</TD>");
			list4.Add("</TR>");
			SkillChallengeResult results = sc.Results;
			if (results.Successes + results.Fails != 0)
			{
				string str = "In Progress";
				if (results.Fails >= 3)
				{
					str = "Failed";
				}
				else if (results.Successes >= sc.Successes)
				{
					str = "Succeeded";
				}
				list4.Add("<TR class=shaded>");
				list4.Add("<TD colspan=3>");
				list4.Add("<B>" + str + "</B>");
				list4.Add("</TD>");
				list4.Add("</TR>");
				list4.Add("<TR>");
				list4.Add("<TD colspan=3>");
				list4.Add(string.Concat(new object[]
				{
					"<B>Successes</B> ",
					results.Successes,
					" <B>Failures</B> ",
					results.Fails
				}));
				if (include_links)
				{
					list4.Add("(<A href=\"sc:reset\">reset</A>)");
				}
				list4.Add("</TD>");
				list4.Add("</TR>");
			}
			if (list.Count != 0)
			{
				list4.Add("<TR class=shaded>");
				list4.Add("<TD colspan=3>");
				list4.Add("<B>Primary Skills</B>");
				list4.Add("</TD>");
				list4.Add("</TR>");
				foreach (SkillChallengeData current2 in list)
				{
					list4.AddRange(HTML.get_skill(current2, sc.Level, true, include_links));
				}
			}
			if (list2.Count != 0)
			{
				list4.Add("<TR class=shaded>");
				list4.Add("<TD colspan=3>");
				list4.Add("<B>Other Skills</B>");
				list4.Add("</TD>");
				list4.Add("</TR>");
				foreach (SkillChallengeData current3 in list2)
				{
					list4.AddRange(HTML.get_skill(current3, sc.Level, true, false));
				}
			}
			if (list3.Count != 0)
			{
				list4.Add("<TR class=shaded>");
				list4.Add("<TD colspan=3>");
				list4.Add("<B>Automatic Failure</B>");
				list4.Add("</TD>");
				list4.Add("</TR>");
				foreach (SkillChallengeData current4 in list3)
				{
					list4.AddRange(HTML.get_skill(current4, sc.Level, false, false));
				}
			}
			if (sc.Success != "")
			{
				list4.Add("<TR class=shaded>");
				list4.Add("<TD colspan=3>");
				list4.Add("<B>Victory</B>");
				list4.Add("</TD>");
				list4.Add("</TR>");
				list4.Add("<TR>");
				list4.Add("<TD colspan=3>");
				list4.Add(HTML.Process(sc.Success, true));
				list4.Add("</TD>");
				list4.Add("</TR>");
			}
			if (sc.Failure != "")
			{
				list4.Add("<TR class=shaded>");
				list4.Add("<TD colspan=3>");
				list4.Add("<B>Defeat</B>");
				list4.Add("</TD>");
				list4.Add("</TR>");
				list4.Add("<TR>");
				list4.Add("<TD colspan=3>");
				list4.Add(HTML.Process(sc.Failure, true));
				list4.Add("</TD>");
				list4.Add("</TR>");
			}
			if (sc.Notes != "")
			{
				list4.Add("<TR class=shaded>");
				list4.Add("<TD colspan=3>");
				list4.Add("<B>Notes</B>");
				list4.Add("</TD>");
				list4.Add("</TR>");
				list4.Add("<TR>");
				list4.Add("<TD colspan=3>");
				list4.Add(HTML.Process(sc.Notes, true));
				list4.Add("</TD>");
				list4.Add("</TR>");
			}
			SkillChallenge skillChallenge = Session.FindSkillChallenge(sc.ID, SearchType.External);
			if (skillChallenge != null)
			{
				Library library = Session.FindLibrary(skillChallenge);
				if (library != null)
				{
					list4.Add("<TR class=shaded>");
					list4.Add("<TD colspan=3>");
					list4.Add(HTML.Process(library.Name, true));
					list4.Add("</TD>");
					list4.Add("</TR>");
				}
			}
			list4.Add("</TABLE>");
			list4.Add("</P>");
			return list4;
		}

		private static List<string> get_skill(SkillChallengeData scd, int level, bool include_details, bool include_links)
		{
			List<string> list = new List<string>();
			string text = "<B>" + scd.SkillName + "</B>";
			if (include_details)
			{
				int num = AI.GetSkillDC(scd.Difficulty, level) + scd.DCModifier;
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					" (DC ",
					num,
					")"
				});
			}
			if (scd.Details != "")
			{
				text = text + ": " + scd.Details;
			}
			list.Add("<TR>");
			list.Add("<TD colspan=3>");
			list.Add(HTML.Process(text, false));
			list.Add("</TD>");
			list.Add("</TR>");
			if (include_details)
			{
				if (scd.Success != "")
				{
					list.Add("<TR>");
					list.Add("<TD class=indent colspan=3>");
					list.Add("<B>Success</B>: " + HTML.Process(scd.Success, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (scd.Failure != "")
				{
					list.Add("<TR>");
					list.Add("<TD class=indent colspan=3>");
					list.Add("<B>Failure</B>: " + HTML.Process(scd.Failure, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
			}
			list.Add("<TR>");
			list.Add("<TD class=indent colspan=3>");
			if (include_links)
			{
				list.Add("Add a <A href=\"success:" + scd.SkillName + "\">success</A>");
				if (scd.Results.Successes > 0)
				{
					list.Add("(" + scd.Results.Successes + ")");
				}
				list.Add("or <A href=\"failure:" + scd.SkillName + "\">failure</A>");
				if (scd.Results.Fails > 0)
				{
					list.Add("(" + scd.Results.Fails + ")");
				}
			}
			list.Add("</TD>");
			list.Add("</TR>");
			return list;
		}

		private static List<string> get_quest(Quest q)
		{
			string str = (q.Type == QuestType.Major) ? "Major Quest" : "Minor Quest";
			return new List<string>
			{
				"<P class=table>",
				"<TABLE>",
				"<TR class=heading>",
				"<TD colspan=2>",
				"<B>" + str + "</B>",
				"</TD>",
				"<TD>",
				q.GetXP() + " XP",
				"</TD>",
				"</TR>",
				"<TR>",
				"<TD colspan=3>",
				"<B>Level</B> " + q.Level,
				"</TD>",
				"</TR>",
				"</TABLE>",
				"</P>"
			};
		}

		private static List<string> get_hero(Hero hero, Encounter enc, bool initiative_holder, bool show_effects)
		{
			List<string> list = new List<string>();
			if (enc != null)
			{
				list.AddRange(HTML.get_combat_data(hero.CombatData, hero.HP, enc, initiative_holder));
			}
			if (show_effects && hero.Effects.Count != 0)
			{
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=heading><TD colspan=3><B>Effects</B></TD></TR>");
				foreach (OngoingCondition current in hero.Effects)
				{
					int num = hero.Effects.IndexOf(current);
					list.Add("<TR><TD colspan=2>" + current.ToString(enc, true) + "</TD>");
					list.Add("<TD align=right><A href=addeffect:" + num + ">Apply &#8658</A></TD></TR>");
				}
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			list.Add("<TR class=hero>");
			list.Add("<TD colspan=2>");
			list.Add("<B>" + HTML.Process(hero.Name, true) + "</B>");
			list.Add("</TD>");
			list.Add("<TD align=right>");
			list.Add(HTML.Process(hero.Player, true));
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD colspan=3>");
			list.Add(HTML.Process(hero.Info, true));
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("<TR class=shaded>");
			list.Add("<TD colspan=3>");
			list.Add("<B>Combat</B>");
			list.Add("</TD>");
			list.Add("</TR>");
			string text = hero.InitBonus.ToString();
			if (hero.InitBonus >= 0)
			{
				text = "+" + text;
			}
			if (hero.CombatData != null && hero.CombatData.Initiative != -2147483648)
			{
				text = string.Concat(new object[]
				{
					hero.CombatData.Initiative,
					" (",
					text,
					")"
				});
			}
			if (enc != null)
			{
				text = string.Concat(new object[]
				{
					"<A href=init:",
					hero.CombatData.ID,
					">",
					text,
					"</A>"
				});
			}
			list.Add("<TR>");
			list.Add("<TD colspan=3>");
			list.Add("<B>Initiative</B> " + text);
			list.Add("</TD>");
			list.Add("</TR>");
			string str = hero.HP.ToString();
			if (hero.CombatData != null && hero.CombatData.Damage != 0)
			{
				int num2 = hero.HP - hero.CombatData.Damage;
				str = num2 + " of " + hero.HP;
			}
			string text2 = "<B>HP</B> " + str;
			if (enc != null)
			{
				text2 = string.Concat(new object[]
				{
					"<A href=hp:",
					hero.ID,
					">",
					text2,
					"</A>"
				});
			}
			text2 = text2 + "; <B>Bloodied</B> " + hero.HP / 2;
			if (hero.CombatData != null && hero.CombatData.TempHP > 0)
			{
				text2 = text2 + "; <B>Temp HP</B> " + hero.CombatData.TempHP;
			}
			list.Add("<TR>");
			list.Add("<TD colspan=3>");
			list.Add(text2);
			list.Add("</TD>");
			list.Add("</TR>");
			int num3 = hero.AC;
			int num4 = hero.Fortitude;
			int num5 = hero.Reflex;
			int num6 = hero.Will;
			if (hero.CombatData != null)
			{
				foreach (OngoingCondition current2 in hero.CombatData.Conditions)
				{
					if (current2.Type == OngoingType.DefenceModifier)
					{
						if (current2.Defences.Contains(DefenceType.AC))
						{
							num3 += current2.DefenceMod;
						}
						if (current2.Defences.Contains(DefenceType.Fortitude))
						{
							num4 += current2.DefenceMod;
						}
						if (current2.Defences.Contains(DefenceType.Reflex))
						{
							num5 += current2.DefenceMod;
						}
						if (current2.Defences.Contains(DefenceType.Will))
						{
							num6 += current2.DefenceMod;
						}
					}
				}
			}
			string text3 = num3.ToString();
			if (num3 != hero.AC)
			{
				text3 = "<B><I>" + text3 + "</I></B>";
			}
			string text4 = num4.ToString();
			if (num4 != hero.Fortitude)
			{
				text4 = "<B><I>" + text4 + "</I></B>";
			}
			string text5 = num5.ToString();
			if (num5 != hero.Reflex)
			{
				text5 = "<B><I>" + text5 + "</I></B>";
			}
			string text6 = num6.ToString();
			if (num6 != hero.Will)
			{
				text6 = "<B><I>" + text6 + "</I></B>";
			}
			list.Add("<TR>");
			list.Add("<TD colspan=3>");
			list.Add(string.Concat(new string[]
			{
				"<B>AC</B> ",
				text3,
				"; <B>Fort</B> ",
				text4,
				"; <B>Ref</B> ",
				text5,
				"; <B>Will</B> ",
				text6
			}));
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("<TR class=shaded>");
			list.Add("<TD colspan=3>");
			list.Add("<B>Skills</B>");
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD colspan=3>");
			list.Add("<B>Passive Insight</B> " + hero.PassiveInsight);
			list.Add("<BR>");
			list.Add("<B>Passive Perception</B> " + hero.PassivePerception);
			list.Add("</TD>");
			list.Add("</TR>");
			if (hero.Languages != "")
			{
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Languages</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add(HTML.Process(hero.Languages, true));
				list.Add("</TD>");
				list.Add("</TR>");
			}
			list.Add("</TABLE>");
			list.Add("</P>");
			return list;
		}

		private static List<string> get_combat_data(CombatData cd, int max_hp, Encounter enc, bool initiative_holder)
		{
			int num = max_hp / 2;
			int num2 = max_hp - cd.Damage;
			bool flag = max_hp != 0 && num2 <= num;
			bool flag2 = max_hp != 0 && num2 <= 0;
			List<string> list = new List<string>();
			if (cd.Conditions.Count != 0 || flag || flag2 || initiative_holder || cd.Altitude != 0)
			{
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=heading>");
				list.Add("<TD>");
				list.Add("<B>Information</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				if (initiative_holder)
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add(cd.DisplayName + " is the current initiative holder");
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (cd.Altitude != 0)
				{
					string text = (Math.Abs(cd.Altitude) == 1) ? "square" : "squares";
					string text2 = (cd.Altitude > 0) ? "up" : "down";
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add(string.Concat(new object[]
					{
						cd.DisplayName,
						" is ",
						Math.Abs(cd.Altitude),
						" ",
						text,
						" <B>",
						text2,
						"</B>"
					}));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (flag2)
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD>");
					list.Add("<B>Hit Points</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add(cd.DisplayName + " is <B>dead</B>");
					list.Add("</TD>");
					list.Add("</TR>");
				}
				else if (flag)
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD>");
					list.Add("<B>Hit Points</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add(cd.DisplayName + " is <B>bloodied</B>");
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (cd.Conditions.Count != 0)
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD>");
					list.Add("<B>Effects</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					foreach (OngoingCondition current in cd.Conditions)
					{
						list.Add("<TR>");
						list.Add("<TD>");
						list.Add(current.ToString(enc, true));
						int num3 = cd.Conditions.IndexOf(current);
						list.Add(string.Concat(new object[]
						{
							"<A href=\"effect:",
							cd.ID,
							":",
							num3,
							"\">(remove)</A>"
						}));
						if (current.Type == OngoingType.Condition)
						{
							list.Add("</TD>");
							list.Add("</TR>");
							list.Add("<TR>");
							list.Add("<TD class=indent>");
							List<string> conditionInfo = Conditions.GetConditionInfo(current.Data);
							if (conditionInfo.Count != 0)
							{
								list.AddRange(conditionInfo);
							}
							list.Add("</TD>");
							list.Add("</TR>");
						}
					}
				}
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			return list;
		}

		private static List<string> get_magic_item(MagicItem item, bool builder)
		{
			List<string> list = new List<string>();
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			list.Add("<TR class=item>");
			list.Add("<TD colspan=2>");
			list.Add("<B>" + HTML.Process(item.Name, true) + "</B>");
			list.Add("</TD>");
			list.Add("<TD>");
			list.Add(HTML.Process(item.Type, true));
			list.Add("</TD>");
			list.Add("</TR>");
			if (builder)
			{
				list.Add("<TR class=item>");
				list.Add("<TD colspan=3 align=center>");
				list.Add("<A href=build:profile style=\"color:white\">(click here to edit this top section)</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			string text = HTML.Process(item.Description, true);
			if (builder && text == "")
			{
				text = "(no description set)";
			}
			if (text != "")
			{
				if (builder)
				{
					text = "<A href=build:desc>" + text + "</A>";
				}
				list.Add("<TR>");
				list.Add("<TD class=readaloud colspan=3>");
				list.Add(text);
				list.Add("</TD>");
				list.Add("</TR>");
			}
			string str = item.Rarity.ToString();
			if (builder)
			{
				str = "<A href=build:profile>" + str + "</A>";
			}
			list.Add("<TR>");
			list.Add("<TD colspan=3>");
			list.Add("<B>Availability</B> " + str);
			list.Add("</TD>");
			list.Add("</TR>");
			string str2 = item.Level.ToString();
			if (builder)
			{
				str2 = "<A href=build:profile>" + str2 + "</A>";
			}
			list.Add("<TR>");
			list.Add("<TD colspan=3>");
			list.Add("<B>Level</B> " + str2);
			list.Add("</TD>");
			list.Add("</TR>");
			foreach (MagicItemSection current in item.Sections)
			{
				int num = item.Sections.IndexOf(current);
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=3>");
				list.Add("<B>" + HTML.Process(current.Header, true) + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD class=indent colspan=3>");
				list.Add(HTML.Process(current.Details, true));
				list.Add("</TD>");
				list.Add("</TR>");
				if (builder)
				{
					list.Add("<TR>");
					list.Add("<TD colspan=3 align=center>");
					list.Add("<A href=edit:" + num + ">edit</A>");
					list.Add("|");
					list.Add("<A href=remove:" + num + ">remove</A>");
					if (item.Sections.Count > 1)
					{
						if (num != 0)
						{
							list.Add("|");
							list.Add("<A href=moveup:" + num + ">move up</A>");
						}
						if (num != item.Sections.Count - 1)
						{
							list.Add("|");
							list.Add("<A href=movedown:" + num + ">move down</A>");
						}
					}
					list.Add("</TD>");
					list.Add("</TR>");
				}
			}
			if (builder)
			{
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Sections</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				if (item.Sections.Count == 0)
				{
					list.Add("<TR>");
					list.Add("<TD class=indent colspan=3>");
					list.Add("No details set");
					list.Add("</TD>");
					list.Add("</TR>");
				}
				list.Add("<TR>");
				list.Add("<TD colspan=3 align=center>");
				list.Add("<A href=section:new>add a new section</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			Library library = Session.FindLibrary(item);
			if (library != null)
			{
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=3>");
				list.Add(HTML.Process(library.Name, true));
				list.Add("</TD>");
				list.Add("</TR>");
			}
			list.Add("</TABLE>");
			list.Add("</P>");
			return list;
		}

		private static List<string> get_artifact(Artifact artifact, bool builder)
		{
			List<string> list = new List<string>();
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			list.Add("<TR class=artifact>");
			list.Add("<TD colspan=2>");
			list.Add("<B>" + HTML.Process(artifact.Name, true) + "</B>");
			list.Add("</TD>");
			list.Add("<TD align=center>");
			list.Add(artifact.Tier + " tier");
			list.Add("</TD>");
			list.Add("</TR>");
			if (builder)
			{
				list.Add("<TR class=artifact>");
				list.Add("<TD colspan=3 align=center>");
				list.Add("<A href=build:profile style=\"color:white\">(click here to edit this top section)</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			string text = HTML.Process(artifact.Description, true);
			if (builder)
			{
				if (text == "")
				{
					text = "click to set description";
				}
				text = "<A href=build:description>" + text + "</A>";
			}
			if (text != "")
			{
				list.Add("<TR>");
				list.Add("<TD class=readaloud colspan=3>");
				list.Add(text);
				list.Add("</TD>");
				list.Add("</TR>");
			}
			string text2 = HTML.Process(artifact.Details, true);
			if (builder)
			{
				if (text2 == "")
				{
					text2 = "click to set details";
				}
				text2 = "<A href=build:details>" + text2 + "</A>";
			}
			if (text2 != "")
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add(text2);
				list.Add("</TD>");
				list.Add("</TR>");
			}
			foreach (MagicItemSection current in artifact.Sections)
			{
				int num = artifact.Sections.IndexOf(current);
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=3>");
				list.Add("<B>" + HTML.Process(current.Header, true) + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD class=indent colspan=3>");
				list.Add(HTML.Process(current.Details, true));
				list.Add("</TD>");
				list.Add("</TR>");
				if (builder)
				{
					list.Add("<TR>");
					list.Add("<TD colspan=3 align=center>");
					list.Add("<A href=sectionedit:" + num + ">edit</A>");
					list.Add("|");
					list.Add("<A href=sectionremove:" + num + ">remove</A>");
					list.Add("</TD>");
					list.Add("</TR>");
				}
			}
			if (builder)
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3 align=center>");
				list.Add("<A href=section:new>add a section</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			string text3 = HTML.Process(artifact.Goals, true);
			if (builder)
			{
				if (text3 == "")
				{
					text3 = "click to set goals";
				}
				text3 = "<A href=build:goals>" + text3 + "</A>";
			}
			if (text3 != "")
			{
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Goals of " + HTML.Process(artifact.Name, true) + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add(text3);
				list.Add("</TD>");
				list.Add("</TR>");
			}
			string text4 = HTML.Process(artifact.RoleplayingTips, true);
			if (builder)
			{
				if (text4 == "")
				{
					text4 = "click to set roleplaying tips";
				}
				text4 = "<A href=build:rp>" + text4 + "</A>";
			}
			if (text4 != "")
			{
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Roleplaying " + HTML.Process(artifact.Name, true) + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add(text4);
				list.Add("</TD>");
				list.Add("</TR>");
			}
			list.Add("<TR class=shaded>");
			list.Add("<TD colspan=3>");
			list.Add("<B>Concordance</B>");
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD colspan=2>Starting score</TD>");
			list.Add("<TD align=center>5</TD>");
			list.Add("</TR>");
			foreach (Pair<string, string> current2 in artifact.ConcordanceRules)
			{
				int num2 = artifact.ConcordanceRules.IndexOf(current2);
				list.Add("<TR>");
				list.Add("<TD colspan=2>");
				list.Add(current2.First);
				if (builder)
				{
					list.Add("<A href=ruleedit:" + num2 + ">edit</A>");
					list.Add("|");
					list.Add("<A href=ruleremove:" + num2 + ">remove</A>");
				}
				list.Add("</TD>");
				list.Add("<TD align=center>");
				list.Add(current2.Second);
				list.Add("</TD>");
				list.Add("</TR>");
			}
			if (builder)
			{
				list.Add("<TR>");
				list.Add("<TD colspan=3 align=center>");
				list.Add("<A href=rule:new>add a concordance rule</A>");
				list.Add("</TD>");
				list.Add("</TR>");
			}
			foreach (ArtifactConcordance current3 in artifact.ConcordanceLevels)
			{
				int num3 = artifact.ConcordanceLevels.IndexOf(current3);
				string text5 = HTML.Process(current3.Name, true);
				if (current3.ValueRange != "")
				{
					text5 = text5 + " (" + HTML.Process(current3.ValueRange, true) + ")";
				}
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=3>");
				list.Add("<B>" + text5 + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				string text6 = HTML.Process(current3.Quote, true);
				if (builder)
				{
					if (text6 == "")
					{
						text6 = "click to set a quote for this concordance level";
					}
					text6 = string.Concat(new object[]
					{
						"<A href=quote:",
						num3,
						">",
						text6,
						"</A>"
					});
				}
				if (text6 != "")
				{
					list.Add("<TR class=readaloud>");
					list.Add("<TD colspan=3>");
					list.Add(text6);
					list.Add("</TD>");
					list.Add("</TR>");
				}
				string text7 = HTML.Process(current3.Description, true);
				if (builder)
				{
					if (text7 == "")
					{
						text7 = "click to set concordance details";
					}
					text7 = string.Concat(new object[]
					{
						"<A href=desc:",
						num3,
						">",
						text7,
						"</A>"
					});
				}
				if (text7 != "")
				{
					list.Add("<TR>");
					list.Add("<TD colspan=3>");
					list.Add(text7);
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (current3.ValueRange != "")
				{
					foreach (MagicItemSection current4 in current3.Sections)
					{
						int num4 = artifact.Sections.IndexOf(current4);
						list.Add("<TR class=shaded>");
						list.Add("<TD colspan=3>");
						list.Add("<B>" + HTML.Process(current4.Header, true) + "</B>");
						list.Add("</TD>");
						list.Add("</TR>");
						list.Add("<TR>");
						list.Add("<TD class=indent colspan=3>");
						list.Add(HTML.Process(current4.Details, true));
						list.Add("</TD>");
						list.Add("</TR>");
						if (builder)
						{
							list.Add("<TR>");
							list.Add("<TD colspan=3 align=center>");
							list.Add(string.Concat(new object[]
							{
								"<A href=sectionedit:",
								num3,
								",",
								num4,
								">edit</A>"
							}));
							list.Add("|");
							list.Add(string.Concat(new object[]
							{
								"<A href=sectionremove:",
								num3,
								",",
								num4,
								">remove</A>"
							}));
							list.Add("</TD>");
							list.Add("</TR>");
						}
					}
					if (builder)
					{
						list.Add("<TR>");
						list.Add("<TD colspan=3 align=center>");
						list.Add("<A href=section:" + num3 + ",new>add a section</A>");
						list.Add("</TD>");
						list.Add("</TR>");
					}
				}
			}
			list.Add("</TABLE>");
			list.Add("</P>");
			return list;
		}

		private static List<string> get_custom_token(CustomToken ct)
		{
			List<string> list = new List<string>();
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			list.Add("<TR class=heading>");
			list.Add("<TD>");
			list.Add("<B>" + HTML.Process(ct.Name, true) + "</B>");
			list.Add("</TD>");
			list.Add("</TR>");
			list.Add("<TR>");
			list.Add("<TD>");
			list.Add((ct.Details != "") ? HTML.Process(ct.Details, true) : "(no details)");
			list.Add("</TD>");
			list.Add("</TR>");
			if (ct.TerrainPower != null)
			{
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add(HTML.Process("Includes the terrain power \"" + ct.TerrainPower.Name + "\"", true));
				list.Add("</TD>");
				list.Add("</TR>");
			}
			list.Add("</TABLE>");
			if (ct.TerrainPower != null)
			{
				list.Add("<BR>");
				list.AddRange(HTML.get_terrain_power(ct.TerrainPower));
			}
			list.Add("</BODY>");
			list.Add("</HTML>");
			list.Add("</P>");
			return list;
		}

		private static List<string> get_terrain_power(TerrainPower tp)
		{
			List<string> list = new List<string>();
			if (tp != null)
			{
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=heading>");
				list.Add("<TD>");
				list.Add("<B>" + HTML.Process(tp.Name, true) + "</B>");
				list.Add("</TD>");
				list.Add("<TD>");
				list.Add((tp.Type == TerrainPowerType.AtWill) ? "At-Will Terrain" : "Single-Use Terrain");
				list.Add("</TD>");
				list.Add("</TR>");
				if (tp.FlavourText != "")
				{
					list.Add("<TR>");
					list.Add("<TD class=readaloud colspan=2>");
					list.Add(HTML.Process(tp.FlavourText, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (tp.Requirement != "")
				{
					list.Add("<TR>");
					list.Add("<TD colspan=2>");
					list.Add("<B>Requirement</B> " + HTML.Process(tp.Requirement, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (tp.Check != "")
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD colspan=2>");
					list.Add("<B>Check</B> " + HTML.Process(tp.Check, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (tp.Success != "")
				{
					list.Add("<TR>");
					list.Add("<TD colspan=2>");
					list.Add("<B>Success</B> " + HTML.Process(tp.Success, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (tp.Failure != "")
				{
					list.Add("<TR>");
					list.Add("<TD colspan=2>");
					list.Add("<B>Failure</B> " + HTML.Process(tp.Failure, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (tp.Attack != "")
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD colspan=2>");
					list.Add("<B>Attack</B> " + HTML.Process(tp.Attack, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (tp.Target != "")
				{
					list.Add("<TR>");
					list.Add("<TD colspan=2>");
					list.Add("<B>Target</B> " + HTML.Process(tp.Target, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (tp.Hit != "")
				{
					list.Add("<TR>");
					list.Add("<TD colspan=2>");
					list.Add("<B>Hit</B> " + HTML.Process(tp.Hit, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (tp.Miss != "")
				{
					list.Add("<TR>");
					list.Add("<TD colspan=2>");
					list.Add("<B>Miss</B> " + HTML.Process(tp.Miss, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (tp.Effect != "")
				{
					list.Add("<TR>");
					list.Add("<TD colspan=2>");
					list.Add("<B>Effect</B> " + HTML.Process(tp.Effect, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			else
			{
				list.Add("<P class=instruction>(none)</P>");
			}
			return list;
		}

		private static List<string> get_parcels(PlotPoint pp, bool links)
		{
			List<string> list = new List<string>();
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			list.Add("<TR class=heading>");
			list.Add("<TD>");
			list.Add("<B>Treasure Parcels</B>");
			list.Add("</TD>");
			list.Add("</TR>");
			foreach (Parcel current in pp.Parcels)
			{
				MagicItem magicItem = null;
				if (current.MagicItemID != Guid.Empty)
				{
					magicItem = Session.FindMagicItem(current.MagicItemID, SearchType.Global);
				}
				string text = (current.Name != "") ? HTML.Process(current.Name, true) : "(undefined parcel)";
				if (links && magicItem != null)
				{
					text = string.Concat(new object[]
					{
						"<A href=\"item:",
						magicItem.ID,
						"\">",
						text,
						"</A>"
					});
				}
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<B>" + text + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				if (current.Details != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add(HTML.Process(current.Details, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (current.MagicItemID != Guid.Empty && magicItem != null)
				{
					Library library = Session.FindLibrary(magicItem);
					if (library != null && (Session.Project == null || Session.Project.Library != library))
					{
						list.Add("<TR>");
						list.Add("<TD>");
						list.Add(HTML.Process(library.Name, true));
						list.Add("</TD>");
						list.Add("</TR>");
					}
				}
			}
			list.Add("</TABLE>");
			list.Add("</P>");
			return list;
		}

		private static List<string> get_player_option(IPlayerOption option)
		{
			List<string> list = new List<string>();
			if (option is Race)
			{
				Race race = option as Race;
				if (race.Quote != null && race.Quote != "")
				{
					list.Add("<P class=readaloud>");
					list.Add(HTML.Process(race.Quote, true));
					list.Add("</P>");
				}
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=heading>");
				list.Add("<TD>");
				list.Add("<B>" + HTML.Process(race.Name, true) + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				if (race.HeightRange != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Average Height</B> " + HTML.Process(race.HeightRange, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (race.WeightRange != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Average Weight</B> " + HTML.Process(race.WeightRange, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (race.AbilityScores != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Ability Scores</B> " + HTML.Process(race.AbilityScores, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<B>Size</B> " + race.Size);
				list.Add("</TD>");
				list.Add("</TR>");
				if (race.Speed != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Speed</B> " + HTML.Process(race.Speed, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (race.Vision != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Vision</B> " + HTML.Process(race.Vision, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (race.Languages != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Languages</B> " + HTML.Process(race.Languages, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (race.SkillBonuses != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Skill Bonuses</B> " + HTML.Process(race.SkillBonuses, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				foreach (Feature current in race.Features)
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD>");
					list.Add("<B>" + HTML.Process(current.Name, true) + "</B> " + HTML.Process(current.Details, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				list.Add("</TABLE>");
				list.Add("</P>");
				foreach (PlayerPower current2 in race.Powers)
				{
					list.AddRange(HTML.get_player_power(current2, 0));
				}
				if (race.Details != "")
				{
					list.Add("<P>");
					list.Add(HTML.Process(race.Details, true));
					list.Add("</P>");
				}
			}
			if (option is Class)
			{
				Class @class = option as Class;
				if (@class.Quote != null && @class.Quote != "")
				{
					list.Add("<P class=readaloud>");
					list.Add(HTML.Process(@class.Quote, true));
					list.Add("</P>");
				}
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=heading>");
				list.Add("<TD>");
				list.Add("<B>" + HTML.Process(@class.Name, true) + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				if (@class.Role != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Role</B> " + HTML.Process(@class.Role, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (@class.PowerSource != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Power Source</B> " + HTML.Process(@class.PowerSource, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (@class.KeyAbilities != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Key Abilities</B> " + HTML.Process(@class.KeyAbilities, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (@class.ArmourProficiencies != "")
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD>");
					list.Add("<B>Armour Proficiencies</B> " + HTML.Process(@class.ArmourProficiencies, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (@class.WeaponProficiencies != "")
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD>");
					list.Add("<B>Weapon Proficiencies</B> " + HTML.Process(@class.WeaponProficiencies, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (@class.Implements != "")
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD>");
					list.Add("<B>Implements</B> " + HTML.Process(@class.Implements, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (@class.DefenceBonuses != "")
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD>");
					list.Add("<B>Defence Bonuses</B> " + HTML.Process(@class.DefenceBonuses, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<B>Hit Points at 1st Level</B> " + @class.HPFirst + " + Constitution score");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<B>HP per Level Gained</B> " + @class.HPSubsequent);
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<B>Healing Surges per Day</B> " + @class.HealingSurges + " + Constitution modifier");
				list.Add("</TD>");
				list.Add("</TR>");
				if (@class.TrainedSkills != "")
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD>");
					list.Add("<B>Trained Skills</B> " + HTML.Process(@class.TrainedSkills, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				string text = "";
				foreach (Feature current3 in @class.FeatureData.Features)
				{
					if (text != "")
					{
						text += ", ";
					}
					text += current3.Name;
				}
				if (text != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Class Features</B> " + HTML.Process(text, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				list.Add("</TABLE>");
				list.Add("</P>");
				if (@class.Description != "")
				{
					list.Add("<P>");
					list.Add(HTML.Process(@class.Description, true));
					list.Add("</P>");
				}
				if (@class.OverviewCharacteristics != "" || @class.OverviewReligion != "" || @class.OverviewRaces != "")
				{
					list.Add("<P class=table>");
					list.Add("<TABLE>");
					list.Add("<TR class=heading>");
					list.Add("<TD>");
					list.Add("<B>Overview</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					if (@class.OverviewCharacteristics != "")
					{
						list.Add("<TR>");
						list.Add("<TD>");
						list.Add("<B>Characteristics</B> " + HTML.Process(@class.OverviewCharacteristics, true));
						list.Add("</TD>");
						list.Add("</TR>");
					}
					if (@class.OverviewReligion != "")
					{
						list.Add("<TR>");
						list.Add("<TD>");
						list.Add("<B>Religion</B> " + HTML.Process(@class.OverviewReligion, true));
						list.Add("</TD>");
						list.Add("</TR>");
					}
					if (@class.OverviewRaces != "")
					{
						list.Add("<TR>");
						list.Add("<TD>");
						list.Add("<B>Races</B> " + HTML.Process(@class.OverviewRaces, true));
						list.Add("</TD>");
						list.Add("</TR>");
					}
					list.Add("</TABLE>");
					list.Add("</P>");
				}
				if (@class.FeatureData.Features.Count != 0)
				{
					list.Add("<H4>Class Features</H4>");
					foreach (Feature current4 in @class.FeatureData.Features)
					{
						list.Add("<P class=table>");
						list.Add("<TABLE>");
						list.Add("<TR class=heading>");
						list.Add("<TD>");
						list.Add("<B>" + HTML.Process(current4.Name, true) + "</B>");
						list.Add("</TD>");
						list.Add("</TR>");
						list.Add("<TR>");
						list.Add("<TD>");
						list.Add(HTML.Process(current4.Details, true));
						list.Add("</TD>");
						list.Add("</TR>");
						list.Add("</TABLE>");
						list.Add("</P>");
					}
				}
				foreach (PlayerPower current5 in @class.FeatureData.Powers)
				{
					list.AddRange(HTML.get_player_power(current5, 0));
				}
				foreach (LevelData current6 in @class.Levels)
				{
					if (current6.Powers.Count != 0)
					{
						list.Add("<H4>Level " + current6.Level + " Powers</H4>");
						foreach (PlayerPower current7 in current6.Powers)
						{
							list.AddRange(HTML.get_player_power(current7, current6.Level));
						}
					}
				}
			}
			if (option is Theme)
			{
				Theme theme = option as Theme;
				if (theme.Quote != null && theme.Quote != "")
				{
					list.Add("<P class=readaloud>");
					list.Add(HTML.Process(theme.Quote, true));
					list.Add("</P>");
				}
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=heading>");
				list.Add("<TD>");
				list.Add("<B>" + HTML.Process(theme.Name, true) + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				if (theme.Prerequisites != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Prerequisites</B> " + HTML.Process(theme.Prerequisites, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (theme.SecondaryRole != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Secondary Role</B> " + HTML.Process(theme.SecondaryRole, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (theme.PowerSource != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Power Source</B> " + HTML.Process(theme.PowerSource, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<B>Granted Power</B> " + HTML.Process(theme.GrantedPower.Name, true));
				list.Add("</TD>");
				list.Add("</TR>");
				foreach (LevelData current8 in theme.Levels)
				{
					foreach (Feature current9 in current8.Features)
					{
						list.Add("<TR class=shaded>");
						list.Add("<TD>");
						list.Add(string.Concat(new object[]
						{
							"<B>",
							HTML.Process(current9.Name, true),
							" (level ",
							current8.Level,
							")</B> ",
							HTML.Process(current9.Details, true)
						}));
						list.Add("</TD>");
						list.Add("</TR>");
					}
				}
				list.Add("</TABLE>");
				list.Add("</P>");
				list.AddRange(HTML.get_player_power(theme.GrantedPower, 0));
				foreach (LevelData current10 in theme.Levels)
				{
					foreach (PlayerPower current11 in current10.Powers)
					{
						list.AddRange(HTML.get_player_power(current11, current10.Level));
					}
				}
				if (theme.Details != "")
				{
					list.Add("<P>");
					list.Add(HTML.Process(theme.Details, true));
					list.Add("</P>");
				}
			}
			if (option is ParagonPath)
			{
				ParagonPath paragonPath = option as ParagonPath;
				if (paragonPath.Quote != null && paragonPath.Quote != "")
				{
					list.Add("<P class=readaloud>");
					list.Add(HTML.Process(paragonPath.Quote, true));
					list.Add("</P>");
				}
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=heading>");
				list.Add("<TD>");
				list.Add("<B>" + HTML.Process(paragonPath.Name, true) + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				if (paragonPath.Prerequisites != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Prerequisites</B> " + HTML.Process(paragonPath.Prerequisites, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				foreach (LevelData current12 in paragonPath.Levels)
				{
					foreach (Feature current13 in current12.Features)
					{
						list.Add("<TR class=shaded>");
						list.Add("<TD>");
						list.Add(string.Concat(new object[]
						{
							"<B>",
							HTML.Process(current13.Name, true),
							" (level ",
							current12.Level,
							")</B> ",
							HTML.Process(current13.Details, true)
						}));
						list.Add("</TD>");
						list.Add("</TR>");
					}
				}
				list.Add("</TABLE>");
				list.Add("</P>");
				foreach (LevelData current14 in paragonPath.Levels)
				{
					foreach (PlayerPower current15 in current14.Powers)
					{
						list.AddRange(HTML.get_player_power(current15, current14.Level));
					}
				}
				if (paragonPath.Details != "")
				{
					list.Add("<P>");
					list.Add(HTML.Process(paragonPath.Details, true));
					list.Add("</P>");
				}
			}
			if (option is EpicDestiny)
			{
				EpicDestiny epicDestiny = option as EpicDestiny;
				if (epicDestiny.Quote != null && epicDestiny.Quote != "")
				{
					list.Add("<P class=readaloud>");
					list.Add(HTML.Process(epicDestiny.Quote, true));
					list.Add("</P>");
				}
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=heading>");
				list.Add("<TD>");
				list.Add("<B>" + HTML.Process(epicDestiny.Name, true) + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				if (epicDestiny.Prerequisites != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Prerequisites</B> " + HTML.Process(epicDestiny.Prerequisites, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				foreach (LevelData current16 in epicDestiny.Levels)
				{
					foreach (Feature current17 in current16.Features)
					{
						list.Add("<TR class=shaded>");
						list.Add("<TD>");
						list.Add(string.Concat(new object[]
						{
							"<B>",
							HTML.Process(current17.Name, true),
							" (level ",
							current16.Level,
							")</B> ",
							HTML.Process(current17.Details, true)
						}));
						list.Add("</TD>");
						list.Add("</TR>");
					}
				}
				list.Add("</TABLE>");
				list.Add("</P>");
				foreach (LevelData current18 in epicDestiny.Levels)
				{
					foreach (PlayerPower current19 in current18.Powers)
					{
						list.AddRange(HTML.get_player_power(current19, current18.Level));
					}
				}
				if (epicDestiny.Details != "")
				{
					list.Add("<P>");
					list.Add(HTML.Process(epicDestiny.Details, true));
					list.Add("</P>");
				}
				if (epicDestiny.Immortality != "")
				{
					list.Add("<P>");
					list.Add("<B>Immortality</B> " + HTML.Process(epicDestiny.Immortality, true));
					list.Add("</P>");
				}
			}
			if (option is PlayerBackground)
			{
				PlayerBackground playerBackground = option as PlayerBackground;
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=heading>");
				list.Add("<TD>");
				list.Add("<B>" + HTML.Process(playerBackground.Name, true) + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				if (playerBackground.Details != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add(HTML.Process(playerBackground.Details, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (playerBackground.AssociatedSkills != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Associated Skills</B> " + HTML.Process(playerBackground.AssociatedSkills, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (playerBackground.RecommendedFeats != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Recommended Feats</B> " + HTML.Process(playerBackground.RecommendedFeats, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			if (option is Feat)
			{
				Feat feat = option as Feat;
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=heading>");
				list.Add("<TD>");
				list.Add("<B>" + HTML.Process(feat.Name, true) + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				if (feat.Prerequisites != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Prerequisites</B> " + HTML.Process(feat.Prerequisites, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (feat.Benefits != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Benefit</B> " + HTML.Process(feat.Benefits, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			if (option is Weapon)
			{
				Weapon weapon = option as Weapon;
				string text2 = weapon.Type + " " + weapon.Category;
				text2 += (weapon.TwoHanded ? " two-handed weapon" : " one-handed weapon");
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=item>");
				list.Add("<TD>");
				list.Add("<B>" + HTML.Process(weapon.Name, true) + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add(text2);
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<B>Proficiency</B> +" + weapon.Proficiency);
				list.Add("</TD>");
				list.Add("</TR>");
				if (weapon.Damage != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Damage</B> " + weapon.Damage);
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (weapon.Range != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Range</B> " + weapon.Range);
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (weapon.Price != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Price</B> " + weapon.Price);
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (weapon.Weight != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Weight</B> " + weapon.Weight);
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (weapon.Group != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Group</B> " + weapon.Group);
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (weapon.Properties != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Properties</B> " + weapon.Properties);
					list.Add("</TD>");
					list.Add("</TR>");
				}
				list.Add("</TABLE>");
				list.Add("</P>");
				if (weapon.Description != "")
				{
					list.Add("<P>" + HTML.Process(weapon.Description, true) + "</P>");
				}
			}
			if (option is Ritual)
			{
				Ritual ritual = option as Ritual;
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=heading>");
				list.Add("<TD>");
				list.Add("<B>" + HTML.Process(ritual.Name, true) + "</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				if (ritual.ReadAloud != "")
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD class=readaloud>");
					list.Add(HTML.Process(ritual.ReadAloud, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<B>Level</B> " + ritual.Level);
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<B>Category</B> " + ritual.Category);
				list.Add("</TD>");
				list.Add("</TR>");
				if (ritual.Time != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Time</B> " + HTML.Process(ritual.Time, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (ritual.Duration != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Duration</B> " + HTML.Process(ritual.Duration, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (ritual.ComponentCost != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Component Cost</B> " + HTML.Process(ritual.ComponentCost, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (ritual.MarketPrice != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Market Price</B> " + HTML.Process(ritual.MarketPrice, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (ritual.KeySkill != "")
				{
					list.Add("<TR>");
					list.Add("<TD>");
					list.Add("<B>Key Skill</B> " + HTML.Process(ritual.KeySkill, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (ritual.Details != "")
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD>");
					list.Add(HTML.Process(ritual.Details, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			if (option is CreatureLore)
			{
				CreatureLore creatureLore = option as CreatureLore;
				list.Add("<H3>" + HTML.Process(creatureLore.Name, true) + " Lore</H3>");
				list.Add("<P>");
				list.Add("A character knows the following information with a successful <B>" + creatureLore.SkillName + "</B> check:");
				list.Add("</P>");
				list.Add("<UL>");
				foreach (Pair<int, string> current20 in creatureLore.Information)
				{
					list.Add("<LI>");
					list.Add(string.Concat(new object[]
					{
						"<B>DC ",
						current20.First,
						"</B>: ",
						current20.Second
					}));
					list.Add("</LI>");
				}
				list.Add("</UL>");
			}
			if (option is Disease)
			{
				Disease disease = option as Disease;
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=trap>");
				list.Add("<TD colspan=2>");
				list.Add("<B>" + HTML.Process(disease.Name, true) + "</B>");
				list.Add("</TD>");
				list.Add("<TD>");
				if (disease.Level != "")
				{
					list.Add("Level " + disease.Level + " Disease");
				}
				list.Add("</TD>");
				list.Add("</TR>");
				if (disease.Details != "")
				{
					list.Add("<TR>");
					list.Add("<TD class=readaloud colspan=3>");
					list.Add(HTML.Process(disease.Details, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				if (disease.Levels.Count != 0)
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD colspan=3>");
					list.Add("<B>Stages</B>");
					list.Add("</TD>");
					list.Add("</TR>");
					list.Add("<TR>");
					list.Add("<TD colspan=3>");
					list.Add("<B>Cured</B>: The target is cured.");
					list.Add("</TD>");
					list.Add("</TR>");
					foreach (string current21 in disease.Levels)
					{
						list.Add("<TR>");
						list.Add("<TD colspan=3>");
						if (disease.Levels.Count > 1)
						{
							int num = disease.Levels.IndexOf(current21);
							if (num == 0)
							{
								list.Add("<B>Initial state</B>:");
							}
							if (num == disease.Levels.Count - 1)
							{
								list.Add("<B>Final state</B>:");
							}
						}
						list.Add(HTML.Process(current21, true));
						list.Add("</TD>");
						list.Add("</TR>");
					}
				}
				list.Add("<TR class=shaded>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Check</B>");
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Maintain</B>: DC " + HTML.Process(disease.MaintainDC, true));
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("<TR>");
				list.Add("<TD colspan=3>");
				list.Add("<B>Improve</B>: DC " + HTML.Process(disease.ImproveDC, true));
				list.Add("</TD>");
				list.Add("</TR>");
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			if (option is Poison)
			{
				Poison poison = option as Poison;
				list.Add("<P class=table>");
				list.Add("<TABLE>");
				list.Add("<TR class=trap>");
				list.Add("<TD>");
				list.Add(string.Concat(new object[]
				{
					"<B>",
					HTML.Process(poison.Name, true),
					"</B> (level ",
					poison.Level,
					")"
				}));
				list.Add("</TD>");
				list.Add("</TR>");
				if (poison.Details != "")
				{
					list.Add("<TR class=shaded>");
					list.Add("<TD class=readaloud>");
					list.Add(HTML.Process(poison.Details, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				int num2 = Treasure.GetItemValue(poison.Level) / 4;
				list.Add("<TR>");
				list.Add("<TD>");
				list.Add("<B>Price</B>: " + num2 + " gp");
				list.Add("</TD>");
				list.Add("</TR>");
				foreach (PlayerPowerSection current22 in poison.Sections)
				{
					list.Add("<TR>");
					if (current22.Indent == 0)
					{
						list.Add("<TD>");
					}
					else
					{
						int num3 = current22.Indent * 15;
						list.Add("<TD style=\"padding-left=" + num3 + "px\">");
					}
					list.Add("<B>" + HTML.Process(current22.Header, true) + "</B> " + HTML.Process(current22.Details, true));
					list.Add("</TD>");
					list.Add("</TR>");
				}
				list.Add("</TABLE>");
				list.Add("</P>");
			}
			return list;
		}

		private static List<string> get_player_power(PlayerPower power, int level)
		{
			List<string> list = new List<string>();
			list.Add("<P class=table>");
			list.Add("<TABLE>");
			string text = HTML.Process(power.Name, true);
			if (text == "")
			{
				text = "(unnamed power)";
			}
			string text2 = "<B>" + text + "</B>";
			if (level != 0)
			{
				object obj = text2;
				text2 = string.Concat(new object[]
				{
					obj,
					" (level ",
					level,
					")"
				});
			}
			switch (power.Type)
			{
			case PlayerPowerType.AtWill:
				list.Add("<TR class=atwill>");
				break;
			case PlayerPowerType.Encounter:
				list.Add("<TR class=encounter>");
				break;
			case PlayerPowerType.Daily:
				list.Add("<TR class=daily>");
				break;
			}
			list.Add("<TD>");
			list.Add(text2);
			list.Add("</TD>");
			list.Add("</TR>");
			if (power.ReadAloud != "")
			{
				list.Add("<TR class=shaded>");
				list.Add("<TD class=readaloud>");
				list.Add(HTML.Process(power.ReadAloud, true));
				list.Add("</TD>");
				list.Add("</TR>");
			}
			string text3 = power.Type.ToString();
			if (power.Keywords != "")
			{
				text3 = text3 + " &diams; " + HTML.Process(power.Keywords, true);
			}
			list.Add("<TR>");
			list.Add("<TD>");
			list.Add(text3);
			list.Add("</TD>");
			list.Add("</TR>");
			string text4 = "<B>Action</B> " + power.Action;
			if (power.Range != "")
			{
				text4 = text4 + "; <B>Range</B> " + HTML.Process(power.Range, true);
			}
			list.Add("<TR>");
			list.Add("<TD>");
			list.Add(text4);
			list.Add("</TD>");
			list.Add("</TR>");
			foreach (PlayerPowerSection current in power.Sections)
			{
				list.Add("<TR>");
				if (current.Indent == 0)
				{
					list.Add("<TD>");
				}
				else
				{
					int num = current.Indent * 15;
					list.Add("<TD style=\"padding-left=" + num + "px\">");
				}
				list.Add("<B>" + HTML.Process(current.Header, true) + "</B> " + HTML.Process(current.Details, true));
				list.Add("</TD>");
				list.Add("</TR>");
			}
			list.Add("</TABLE>");
			list.Add("</P>");
			return list;
		}

		private void add_map(Guid map_id, Guid area_id)
		{
			if (map_id == Guid.Empty)
			{
				return;
			}
			if (!this.fMaps.ContainsKey(map_id))
			{
				this.fMaps[map_id] = new List<Guid>();
			}
			if (!this.fMaps[map_id].Contains(area_id))
			{
				this.fMaps[map_id].Add(area_id);
			}
		}

		private string get_filename(string item_name, string extension, bool full_path)
		{
			string text = item_name;
			foreach (string current in new List<string>
			{
				"\\",
				"/",
				":",
				"*",
				"?",
				"\"",
				"<",
				">",
				"|"
			})
			{
				text = text.Replace(current, "");
			}
			string text2 = (full_path ? this.fFullPath : this.fRelativePath) + text + "." + extension;
			if (!full_path)
			{
				text2 = text2.Replace(" ", "%20");
			}
			return text2;
		}

		private string get_map_name(Guid map_id, Guid area_id)
		{
			Map map = Session.Project.FindTacticalMap(map_id);
			if (map == null)
			{
				return "";
			}
			if (area_id == Guid.Empty)
			{
				return map.Name;
			}
			MapArea mapArea = map.FindArea(area_id);
			return map.Name + " - " + mapArea.Name;
		}

		private static string get_time(TimeSpan ts)
		{
			return string.Concat(new string[]
			{
				ts.Hours.ToString("00"),
				":",
				ts.Minutes.ToString("00"),
				":",
				ts.Seconds.ToString("00")
			});
		}
	}
}
