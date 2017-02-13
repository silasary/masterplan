using Masterplan.Data;
using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Tools.Generators
{
	internal class EncounterBuilder
	{
		private const int TRIES = 100;

		private static List<EncounterTemplateGroup> fTemplateGroups = new List<EncounterTemplateGroup>();

		private static List<EncounterCard> fCreatures = new List<EncounterCard>();

		private static List<Trap> fTraps = new List<Trap>();

		private static List<SkillChallenge> fChallenges = new List<SkillChallenge>();

		public static bool Build(AutoBuildData data, Encounter enc, bool include_individual)
		{
			int min_level = Math.Max(data.Level - 4, 1);
			int max_level = data.Level + 5;
			EncounterBuilder.build_creature_list(min_level, max_level, data.Categories, data.Keywords, true);
			if (EncounterBuilder.fCreatures.Count == 0)
			{
				return false;
			}
			EncounterBuilder.build_template_list(data.Type, data.Difficulty, data.Level, include_individual);
			if (EncounterBuilder.fTemplateGroups.Count == 0)
			{
				return false;
			}
			EncounterBuilder.build_trap_list(data.Level);
			EncounterBuilder.build_challenge_list(data.Level);
			int i = 0;
			while (i < 100)
			{
				i++;
				int index = Session.Random.Next() % EncounterBuilder.fTemplateGroups.Count;
				EncounterTemplateGroup encounterTemplateGroup = EncounterBuilder.fTemplateGroups[index];
				int index2 = Session.Random.Next() % encounterTemplateGroup.Templates.Count;
				EncounterTemplate encounterTemplate = encounterTemplateGroup.Templates[index2];
				bool flag = true;
				List<EncounterSlot> list = new List<EncounterSlot>();
				foreach (EncounterTemplateSlot current in encounterTemplate.Slots)
				{
					List<EncounterCard> list2 = new List<EncounterCard>();
					foreach (EncounterCard current2 in EncounterBuilder.fCreatures)
					{
						if (current.Match(current2, data.Level))
						{
							list2.Add(current2);
						}
					}
					if (list2.Count == 0)
					{
						flag = false;
						break;
					}
					int index3 = Session.Random.Next() % list2.Count;
					EncounterCard card = list2[index3];
					EncounterSlot encounterSlot = new EncounterSlot();
					encounterSlot.Card = card;
					for (int num = 0; num != current.Count; num++)
					{
						CombatData item = new CombatData();
						encounterSlot.CombatData.Add(item);
					}
					list.Add(encounterSlot);
				}
				if (flag)
				{
					enc.Slots = list;
					enc.Traps.Clear();
					enc.SkillChallenges.Clear();
					switch (Session.Random.Next(12))
					{
					case 4:
					case 5:
						if (EncounterBuilder.add_trap(enc))
						{
							EncounterBuilder.remove_creature(enc);
						}
						break;
					case 6:
						if (EncounterBuilder.add_challenge(enc))
						{
							EncounterBuilder.remove_creature(enc);
						}
						break;
					case 7:
						if (EncounterBuilder.add_lurker(enc))
						{
							EncounterBuilder.remove_creature(enc);
						}
						break;
					case 8:
					case 9:
					{
						EncounterBuilder.add_trap(enc);
						Difficulty difficulty = enc.GetDifficulty(data.Level, data.Size);
						if (difficulty == Difficulty.Hard || difficulty == Difficulty.Extreme)
						{
							EncounterBuilder.remove_creature(enc);
						}
						break;
					}
					case 10:
					{
						Difficulty difficulty2 = enc.GetDifficulty(data.Level, data.Size);
						if (difficulty2 == Difficulty.Hard || difficulty2 == Difficulty.Extreme)
						{
							EncounterBuilder.remove_creature(enc);
						}
						EncounterBuilder.add_challenge(enc);
						break;
					}
					case 11:
					{
						EncounterBuilder.add_lurker(enc);
						Difficulty difficulty3 = enc.GetDifficulty(data.Level, data.Size);
						if (difficulty3 == Difficulty.Hard || difficulty3 == Difficulty.Extreme)
						{
							EncounterBuilder.remove_creature(enc);
						}
						break;
					}
					}
					while (enc.GetDifficulty(data.Level, data.Size) == Difficulty.Extreme && enc.Count > 1)
					{
						EncounterBuilder.remove_creature(enc);
					}
					foreach (EncounterSlot current3 in enc.Slots)
					{
						current3.SetDefaultDisplayNames();
					}
					return true;
				}
			}
			return false;
		}

		private static void remove_creature(Encounter enc)
		{
			if (enc.Count == 0)
			{
				return;
			}
			int index = Session.Random.Next() % enc.Slots.Count;
			EncounterSlot encounterSlot = enc.Slots[index];
			if (encounterSlot.CombatData.Count == 1)
			{
				enc.Slots.Remove(encounterSlot);
				return;
			}
			encounterSlot.CombatData.RemoveAt(encounterSlot.CombatData.Count - 1);
		}

		private static bool add_trap(Encounter enc)
		{
			if (EncounterBuilder.fTraps.Count != 0)
			{
				int index = Session.Random.Next() % EncounterBuilder.fTraps.Count;
				Trap trap = EncounterBuilder.fTraps[index];
				enc.Traps.Add(trap.Copy());
				return true;
			}
			return false;
		}

		private static bool add_challenge(Encounter enc)
		{
			if (EncounterBuilder.fChallenges.Count != 0)
			{
				int index = Session.Random.Next() % EncounterBuilder.fChallenges.Count;
				SkillChallenge skillChallenge = EncounterBuilder.fChallenges[index];
				enc.SkillChallenges.Add(skillChallenge.Copy() as SkillChallenge);
				return true;
			}
			return false;
		}

		private static bool add_lurker(Encounter enc)
		{
			List<EncounterCard> list = new List<EncounterCard>();
			foreach (EncounterCard current in EncounterBuilder.fCreatures)
			{
				if (current.Flag == RoleFlag.Standard && current.Roles.Contains(RoleType.Lurker))
				{
					list.Add(current);
				}
			}
			if (list.Count != 0)
			{
				int index = Session.Random.Next() % list.Count;
				EncounterSlot encounterSlot = new EncounterSlot();
				encounterSlot.Card = list[index];
				encounterSlot.CombatData.Add(new CombatData());
				enc.Slots.Add(encounterSlot);
				return true;
			}
			return false;
		}

		public static EncounterDeck BuildDeck(int level, List<string> categories, List<string> keywords)
		{
			EncounterBuilder.build_creature_list(level - 2, level + 5, categories, keywords, false);
			if (EncounterBuilder.fCreatures.Count == 0)
			{
				return null;
			}
			Dictionary<CardCategory, Pair<int, int>> dictionary = new Dictionary<CardCategory, Pair<int, int>>();
			dictionary[CardCategory.SoldierBrute] = new Pair<int, int>(0, 18);
			dictionary[CardCategory.Skirmisher] = new Pair<int, int>(0, 14);
			dictionary[CardCategory.Minion] = new Pair<int, int>(0, 5);
			dictionary[CardCategory.Artillery] = new Pair<int, int>(0, 5);
			dictionary[CardCategory.Controller] = new Pair<int, int>(0, 5);
			dictionary[CardCategory.Lurker] = new Pair<int, int>(0, 2);
			dictionary[CardCategory.Solo] = new Pair<int, int>(0, 1);
			Dictionary<Difficulty, Pair<int, int>> dictionary2 = new Dictionary<Difficulty, Pair<int, int>>();
			if (level >= 3)
			{
				dictionary2[Difficulty.Trivial] = new Pair<int, int>(0, 7);
				dictionary2[Difficulty.Easy] = new Pair<int, int>(0, 30);
			}
			else
			{
				dictionary2[Difficulty.Easy] = new Pair<int, int>(0, 37);
			}
			dictionary2[Difficulty.Moderate] = new Pair<int, int>(0, 8);
			dictionary2[Difficulty.Hard] = new Pair<int, int>(0, 5);
			dictionary2[Difficulty.Extreme] = new Pair<int, int>(0, 0);
			EncounterDeck encounterDeck = new EncounterDeck();
			encounterDeck.Level = level;
			int i = 0;
			while (i < 100)
			{
				i++;
				int index = Session.Random.Next() % EncounterBuilder.fCreatures.Count;
				EncounterCard encounterCard = EncounterBuilder.fCreatures[index];
				CardCategory category = encounterCard.Category;
				Pair<int, int> pair = dictionary[category];
				bool flag = pair.First < pair.Second;
				if (flag)
				{
					Difficulty difficulty = encounterCard.GetDifficulty(level);
					Pair<int, int> pair2 = dictionary2[difficulty];
					bool flag2 = pair2.First < pair2.Second;
					if (flag2)
					{
						encounterDeck.Cards.Add(encounterCard);
						dictionary[category].First++;
						dictionary2[difficulty].First++;
						if (encounterDeck.Cards.Count == 50)
						{
							break;
						}
					}
				}
			}
			EncounterBuilder.FillDeck(encounterDeck);
			return encounterDeck;
		}

		public static void FillDeck(EncounterDeck deck)
		{
			EncounterBuilder.build_creature_list(deck.Level - 2, deck.Level + 5, null, null, false);
			if (EncounterBuilder.fCreatures.Count == 0)
			{
				return;
			}
			while (deck.Cards.Count < 50)
			{
				int index = Session.Random.Next() % EncounterBuilder.fCreatures.Count;
				EncounterCard item = EncounterBuilder.fCreatures[index];
				deck.Cards.Add(item);
			}
		}

		public static List<Pair<EncounterTemplateGroup, EncounterTemplate>> FindTemplates(Encounter enc, int level, bool include_individual)
		{
			EncounterBuilder.build_template_list("", Difficulty.Random, level, include_individual);
			List<Pair<EncounterTemplateGroup, EncounterTemplate>> list = new List<Pair<EncounterTemplateGroup, EncounterTemplate>>();
			foreach (EncounterTemplateGroup current in EncounterBuilder.fTemplateGroups)
			{
				foreach (EncounterTemplate current2 in current.Templates)
				{
					EncounterTemplate encounterTemplate = current2.Copy();
					bool flag = true;
					foreach (EncounterSlot current3 in enc.Slots)
					{
						EncounterTemplateSlot encounterTemplateSlot = encounterTemplate.FindSlot(current3, level);
						if (encounterTemplateSlot == null)
						{
							flag = false;
							break;
						}
						encounterTemplateSlot.Count -= current3.CombatData.Count;
						if (encounterTemplateSlot.Count <= 0)
						{
							encounterTemplate.Slots.Remove(encounterTemplateSlot);
						}
					}
					if (flag)
					{
						bool flag2 = true;
						foreach (EncounterTemplateSlot current4 in encounterTemplate.Slots)
						{
							bool flag3 = false;
							int num = level + current4.LevelAdjustment;
							EncounterBuilder.build_creature_list(num, num, null, null, true);
							foreach (EncounterCard current5 in EncounterBuilder.fCreatures)
							{
								if (current4.Match(current5, level))
								{
									flag3 = true;
									break;
								}
							}
							if (!flag3)
							{
								flag2 = false;
								break;
							}
						}
						if (flag2)
						{
							list.Add(new Pair<EncounterTemplateGroup, EncounterTemplate>(current, encounterTemplate));
						}
					}
				}
			}
			return list;
		}

		public static List<string> FindTemplateNames()
		{
			EncounterBuilder.build_template_list("", Difficulty.Random, -1, true);
			List<string> list = new List<string>();
			foreach (EncounterTemplateGroup current in EncounterBuilder.fTemplateGroups)
			{
				list.Add(current.Name);
			}
			list.Sort();
			return list;
		}

		public static List<EncounterCard> FindCreatures(EncounterTemplateSlot slot, int party_level, string query)
		{
			int num = party_level + slot.LevelAdjustment;
			EncounterBuilder.build_creature_list(num, num, null, null, true);
			List<EncounterCard> list = new List<EncounterCard>();
			foreach (EncounterCard current in EncounterBuilder.fCreatures)
			{
				if (slot.Match(current, party_level) && EncounterBuilder.match(current, query))
				{
					list.Add(current);
				}
			}
			return list;
		}

		private static bool match(EncounterCard card, string query)
		{
			string[] array = query.ToLower().Split(new char[0]);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string token = array2[i];
				if (!EncounterBuilder.match_token(card, token))
				{
					return false;
				}
			}
			return true;
		}

		private static bool match_token(EncounterCard card, string token)
		{
			return card.Title.ToLower().Contains(token) || card.Category.ToString().ToLower().Contains(token);
		}

		private static void build_template_list(string group_name, Difficulty diff, int level, bool include_individual)
		{
			EncounterBuilder.fTemplateGroups.Clear();
			EncounterBuilder.build_template_battlefield_control();
			EncounterBuilder.build_template_commander_and_troops();
			EncounterBuilder.build_template_double_line();
			EncounterBuilder.build_template_dragons_den();
			EncounterBuilder.build_template_grand_melee();
			EncounterBuilder.build_template_wolf_pack();
			if (include_individual)
			{
				EncounterBuilder.build_template_duel();
			}
			if (group_name != "")
			{
				List<EncounterTemplateGroup> list = new List<EncounterTemplateGroup>();
				foreach (EncounterTemplateGroup current in EncounterBuilder.fTemplateGroups)
				{
					if (current.Name != group_name)
					{
						list.Add(current);
					}
				}
				foreach (EncounterTemplateGroup current2 in list)
				{
					EncounterBuilder.fTemplateGroups.Remove(current2);
				}
			}
			if (diff != Difficulty.Random)
			{
				List<EncounterTemplateGroup> list2 = new List<EncounterTemplateGroup>();
				foreach (EncounterTemplateGroup current3 in EncounterBuilder.fTemplateGroups)
				{
					List<EncounterTemplate> list3 = new List<EncounterTemplate>();
					foreach (EncounterTemplate current4 in current3.Templates)
					{
						if (current4.Difficulty != diff)
						{
							list3.Add(current4);
						}
					}
					foreach (EncounterTemplate current5 in list3)
					{
						current3.Templates.Remove(current5);
					}
					if (current3.Templates.Count == 0)
					{
						list2.Add(current3);
					}
				}
				foreach (EncounterTemplateGroup current6 in list2)
				{
					EncounterBuilder.fTemplateGroups.Remove(current6);
				}
			}
			if (level != -1)
			{
				List<EncounterTemplateGroup> list4 = new List<EncounterTemplateGroup>();
				foreach (EncounterTemplateGroup current7 in EncounterBuilder.fTemplateGroups)
				{
					List<EncounterTemplate> list5 = new List<EncounterTemplate>();
					foreach (EncounterTemplate current8 in current7.Templates)
					{
						bool flag = true;
						foreach (EncounterTemplateSlot current9 in current8.Slots)
						{
							if (level + current9.LevelAdjustment < 1)
							{
								flag = false;
								break;
							}
						}
						if (!flag)
						{
							list5.Add(current8);
						}
					}
					foreach (EncounterTemplate current10 in list5)
					{
						current7.Templates.Remove(current10);
					}
					if (current7.Templates.Count == 0)
					{
						list4.Add(current7);
					}
				}
				foreach (EncounterTemplateGroup current11 in list4)
				{
					EncounterBuilder.fTemplateGroups.Remove(current11);
				}
			}
		}

		private static void build_template_battlefield_control()
		{
			EncounterTemplateGroup encounterTemplateGroup = new EncounterTemplateGroup("Battlefield Control", "Entire Party");
			EncounterTemplate encounterTemplate = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(1, -2, RoleType.Controller));
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(6, -4, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate);
			EncounterTemplate encounterTemplate2 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(1, 1, RoleType.Controller));
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(6, -2, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate2);
			EncounterTemplate encounterTemplate3 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate3.Slots.Add(new EncounterTemplateSlot(1, 5, RoleType.Controller));
			encounterTemplate3.Slots.Add(new EncounterTemplateSlot(5, 1, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate3);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup);
		}

		private static void build_template_commander_and_troops()
		{
			EncounterTemplateGroup encounterTemplateGroup = new EncounterTemplateGroup("Commander and Troops", "Entire Party");
			EncounterTemplate encounterTemplate = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(1, 0, new RoleType[]
			{
				RoleType.Controller,
				RoleType.Soldier,
				RoleType.Lurker,
				RoleType.Skirmisher
			}));
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(4, -3, new RoleType[]
			{
				RoleType.Brute,
				RoleType.Soldier
			}));
			encounterTemplateGroup.Templates.Add(encounterTemplate);
			EncounterTemplate encounterTemplate2 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(1, 3, new RoleType[]
			{
				RoleType.Controller,
				RoleType.Soldier,
				RoleType.Lurker,
				RoleType.Skirmisher
			}));
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(5, -2, new RoleType[]
			{
				RoleType.Brute,
				RoleType.Soldier
			}));
			encounterTemplateGroup.Templates.Add(encounterTemplate2);
			EncounterTemplate encounterTemplate3 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate3.Slots.Add(new EncounterTemplateSlot(1, 5, new RoleType[]
			{
				RoleType.Controller,
				RoleType.Soldier,
				RoleType.Lurker,
				RoleType.Skirmisher
			}));
			encounterTemplate3.Slots.Add(new EncounterTemplateSlot(3, 1, new RoleType[]
			{
				RoleType.Brute,
				RoleType.Soldier
			}));
			List<EncounterTemplateSlot> arg_158_0 = encounterTemplate3.Slots;
			int arg_153_0 = 2;
			int arg_153_1 = 1;
			RoleType[] roles = new RoleType[1];
			arg_158_0.Add(new EncounterTemplateSlot(arg_153_0, arg_153_1, roles));
			encounterTemplateGroup.Templates.Add(encounterTemplate3);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup);
		}

		private static void build_template_dragons_den()
		{
			EncounterTemplateGroup encounterTemplateGroup = new EncounterTemplateGroup("Dragon's Den", "Entire Party");
			EncounterTemplate encounterTemplate = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(1, -2, RoleFlag.Solo));
			encounterTemplateGroup.Templates.Add(encounterTemplate);
			EncounterTemplate encounterTemplate2 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(1, 0, RoleFlag.Solo));
			encounterTemplateGroup.Templates.Add(encounterTemplate2);
			EncounterTemplate encounterTemplate3 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate3.Slots.Add(new EncounterTemplateSlot(1, 1, RoleFlag.Solo));
			encounterTemplateGroup.Templates.Add(encounterTemplate3);
			EncounterTemplate encounterTemplate4 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate4.Slots.Add(new EncounterTemplateSlot(1, 3, RoleFlag.Solo));
			encounterTemplateGroup.Templates.Add(encounterTemplate4);
			EncounterTemplate encounterTemplate5 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate5.Slots.Add(new EncounterTemplateSlot(1, 1, RoleFlag.Solo));
			encounterTemplate5.Slots.Add(new EncounterTemplateSlot(1, 0, RoleFlag.Elite));
			encounterTemplateGroup.Templates.Add(encounterTemplate5);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup);
		}

		private static void build_template_double_line()
		{
			EncounterTemplateGroup encounterTemplateGroup = new EncounterTemplateGroup("Double Line", "Entire Party");
			EncounterTemplate encounterTemplate = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(3, -4, new RoleType[]
			{
				RoleType.Brute,
				RoleType.Soldier
			}));
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(2, -2, new RoleType[]
			{
				RoleType.Artillery,
				RoleType.Controller
			}));
			encounterTemplateGroup.Templates.Add(encounterTemplate);
			EncounterTemplate encounterTemplate2 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(3, 0, new RoleType[]
			{
				RoleType.Brute,
				RoleType.Soldier
			}));
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(2, 0, new RoleType[]
			{
				RoleType.Artillery,
				RoleType.Controller
			}));
			encounterTemplateGroup.Templates.Add(encounterTemplate2);
			EncounterTemplate encounterTemplate3 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate3.Slots.Add(new EncounterTemplateSlot(3, -2, new RoleType[]
			{
				RoleType.Brute,
				RoleType.Soldier
			}));
			encounterTemplate3.Slots.Add(new EncounterTemplateSlot(2, 3, new RoleType[]
			{
				RoleType.Artillery,
				RoleType.Controller
			}));
			encounterTemplateGroup.Templates.Add(encounterTemplate3);
			EncounterTemplate encounterTemplate4 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate4.Slots.Add(new EncounterTemplateSlot(3, 2, new RoleType[]
			{
				RoleType.Brute,
				RoleType.Soldier
			}));
			encounterTemplate4.Slots.Add(new EncounterTemplateSlot(1, 4, RoleType.Controller));
			encounterTemplate4.Slots.Add(new EncounterTemplateSlot(1, 4, new RoleType[]
			{
				RoleType.Artillery,
				RoleType.Lurker
			}));
			encounterTemplateGroup.Templates.Add(encounterTemplate4);
			EncounterTemplate encounterTemplate5 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate5.Slots.Add(new EncounterTemplateSlot(3, 0, new RoleType[]
			{
				RoleType.Brute,
				RoleType.Soldier
			}));
			encounterTemplate5.Slots.Add(new EncounterTemplateSlot(2, 1, RoleType.Artillery));
			encounterTemplate5.Slots.Add(new EncounterTemplateSlot(1, 2, RoleType.Controller));
			encounterTemplate5.Slots.Add(new EncounterTemplateSlot(1, 2, RoleType.Lurker));
			encounterTemplateGroup.Templates.Add(encounterTemplate5);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup);
		}

		private static void build_template_wolf_pack()
		{
			EncounterTemplateGroup encounterTemplateGroup = new EncounterTemplateGroup("Wolf Pack", "Entire Party");
			EncounterTemplate encounterTemplate = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(7, -4, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate);
			EncounterTemplate encounterTemplate2 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(7, -2, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate2);
			EncounterTemplate encounterTemplate3 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate3.Slots.Add(new EncounterTemplateSlot(5, 0, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate3);
			EncounterTemplate encounterTemplate4 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate4.Slots.Add(new EncounterTemplateSlot(3, 5, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate4);
			EncounterTemplate encounterTemplate5 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate5.Slots.Add(new EncounterTemplateSlot(4, 5, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate5);
			EncounterTemplate encounterTemplate6 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate6.Slots.Add(new EncounterTemplateSlot(6, 2, RoleType.Skirmisher));
			encounterTemplateGroup.Templates.Add(encounterTemplate6);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup);
		}

		private static void build_template_duel()
		{
			EncounterTemplateGroup encounterTemplateGroup = new EncounterTemplateGroup("Duel vs Controller", "Individual PC");
			EncounterTemplate encounterTemplate = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(1, 0, RoleType.Artillery));
			encounterTemplateGroup.Templates.Add(encounterTemplate);
			EncounterTemplate encounterTemplate2 = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(1, -1, new RoleType[]
			{
				RoleType.Controller,
				RoleType.Skirmisher
			}));
			encounterTemplateGroup.Templates.Add(encounterTemplate2);
			EncounterTemplate encounterTemplate3 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate3.Slots.Add(new EncounterTemplateSlot(1, 2, RoleType.Artillery));
			encounterTemplateGroup.Templates.Add(encounterTemplate3);
			EncounterTemplate encounterTemplate4 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate4.Slots.Add(new EncounterTemplateSlot(1, 1, new RoleType[]
			{
				RoleType.Controller,
				RoleType.Skirmisher
			}));
			encounterTemplateGroup.Templates.Add(encounterTemplate4);
			EncounterTemplate encounterTemplate5 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate5.Slots.Add(new EncounterTemplateSlot(1, 4, RoleType.Artillery));
			encounterTemplateGroup.Templates.Add(encounterTemplate5);
			EncounterTemplate encounterTemplate6 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate6.Slots.Add(new EncounterTemplateSlot(1, 3, new RoleType[]
			{
				RoleType.Controller,
				RoleType.Skirmisher
			}));
			encounterTemplateGroup.Templates.Add(encounterTemplate6);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup);
			EncounterTemplateGroup encounterTemplateGroup2 = new EncounterTemplateGroup("Duel vs Defender", "Individual PC");
			EncounterTemplate encounterTemplate7 = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate7.Slots.Add(new EncounterTemplateSlot(1, 0, RoleType.Skirmisher));
			encounterTemplateGroup2.Templates.Add(encounterTemplate7);
			EncounterTemplate encounterTemplate8 = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate8.Slots.Add(new EncounterTemplateSlot(1, -1, new RoleType[]
			{
				RoleType.Brute,
				RoleType.Soldier
			}));
			encounterTemplateGroup2.Templates.Add(encounterTemplate8);
			EncounterTemplate encounterTemplate9 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate9.Slots.Add(new EncounterTemplateSlot(1, 2, RoleType.Skirmisher));
			encounterTemplateGroup2.Templates.Add(encounterTemplate9);
			EncounterTemplate encounterTemplate10 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate10.Slots.Add(new EncounterTemplateSlot(1, 1, new RoleType[]
			{
				RoleType.Brute,
				RoleType.Soldier
			}));
			encounterTemplateGroup2.Templates.Add(encounterTemplate10);
			EncounterTemplate encounterTemplate11 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate11.Slots.Add(new EncounterTemplateSlot(1, 4, RoleType.Skirmisher));
			encounterTemplateGroup2.Templates.Add(encounterTemplate11);
			EncounterTemplate encounterTemplate12 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate12.Slots.Add(new EncounterTemplateSlot(1, 3, new RoleType[]
			{
				RoleType.Controller,
				RoleType.Skirmisher
			}));
			encounterTemplateGroup2.Templates.Add(encounterTemplate12);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup2);
			EncounterTemplateGroup encounterTemplateGroup3 = new EncounterTemplateGroup("Duel vs Leader", "Individual PC");
			EncounterTemplate encounterTemplate13 = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate13.Slots.Add(new EncounterTemplateSlot(1, 0, RoleType.Skirmisher));
			encounterTemplateGroup3.Templates.Add(encounterTemplate13);
			EncounterTemplate encounterTemplate14 = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate14.Slots.Add(new EncounterTemplateSlot(1, -1, new RoleType[]
			{
				RoleType.Controller,
				RoleType.Soldier
			}));
			encounterTemplateGroup3.Templates.Add(encounterTemplate14);
			EncounterTemplate encounterTemplate15 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate15.Slots.Add(new EncounterTemplateSlot(1, 2, RoleType.Skirmisher));
			encounterTemplateGroup3.Templates.Add(encounterTemplate15);
			EncounterTemplate encounterTemplate16 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate16.Slots.Add(new EncounterTemplateSlot(1, 1, new RoleType[]
			{
				RoleType.Controller,
				RoleType.Soldier
			}));
			encounterTemplateGroup3.Templates.Add(encounterTemplate16);
			EncounterTemplate encounterTemplate17 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate17.Slots.Add(new EncounterTemplateSlot(1, 4, RoleType.Skirmisher));
			encounterTemplateGroup3.Templates.Add(encounterTemplate17);
			EncounterTemplate encounterTemplate18 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate18.Slots.Add(new EncounterTemplateSlot(1, 3, new RoleType[]
			{
				RoleType.Controller,
				RoleType.Soldier
			}));
			encounterTemplateGroup3.Templates.Add(encounterTemplate18);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup3);
			EncounterTemplateGroup encounterTemplateGroup4 = new EncounterTemplateGroup("Duel vs Striker", "Individual PC");
			EncounterTemplate encounterTemplate19 = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate19.Slots.Add(new EncounterTemplateSlot(1, 0, RoleType.Skirmisher));
			encounterTemplateGroup4.Templates.Add(encounterTemplate19);
			EncounterTemplate encounterTemplate20 = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate20.Slots.Add(new EncounterTemplateSlot(1, -1, new RoleType[]
			{
				RoleType.Brute,
				RoleType.Soldier
			}));
			encounterTemplateGroup4.Templates.Add(encounterTemplate20);
			EncounterTemplate encounterTemplate21 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate21.Slots.Add(new EncounterTemplateSlot(1, 2, RoleType.Skirmisher));
			encounterTemplateGroup4.Templates.Add(encounterTemplate21);
			EncounterTemplate encounterTemplate22 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate22.Slots.Add(new EncounterTemplateSlot(1, 1, new RoleType[]
			{
				RoleType.Brute,
				RoleType.Soldier
			}));
			encounterTemplateGroup4.Templates.Add(encounterTemplate22);
			EncounterTemplate encounterTemplate23 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate23.Slots.Add(new EncounterTemplateSlot(1, 4, RoleType.Skirmisher));
			encounterTemplateGroup4.Templates.Add(encounterTemplate23);
			EncounterTemplate encounterTemplate24 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate24.Slots.Add(new EncounterTemplateSlot(1, 3, new RoleType[]
			{
				RoleType.Brute,
				RoleType.Soldier
			}));
			encounterTemplateGroup4.Templates.Add(encounterTemplate24);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup4);
		}

		private static void build_template_grand_melee()
		{
			EncounterTemplateGroup encounterTemplateGroup = new EncounterTemplateGroup("Grand Melee", "Entire Party");
			EncounterTemplate encounterTemplate = new EncounterTemplate(Difficulty.Easy);
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(4, -2, RoleType.Brute));
			encounterTemplate.Slots.Add(new EncounterTemplateSlot(11, -4, true));
			encounterTemplateGroup.Templates.Add(encounterTemplate);
			EncounterTemplate encounterTemplate2 = new EncounterTemplate(Difficulty.Moderate);
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(2, -1, RoleType.Soldier));
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(4, -2, RoleType.Brute));
			encounterTemplate2.Slots.Add(new EncounterTemplateSlot(12, -4, true));
			encounterTemplateGroup.Templates.Add(encounterTemplate2);
			EncounterTemplate encounterTemplate3 = new EncounterTemplate(Difficulty.Hard);
			encounterTemplate3.Slots.Add(new EncounterTemplateSlot(2, 0, RoleType.Soldier));
			encounterTemplate3.Slots.Add(new EncounterTemplateSlot(4, -1, RoleType.Brute));
			encounterTemplate3.Slots.Add(new EncounterTemplateSlot(17, -2, true));
			encounterTemplateGroup.Templates.Add(encounterTemplate3);
			EncounterBuilder.fTemplateGroups.Add(encounterTemplateGroup);
		}

		private static void build_creature_list(int min_level, int max_level, List<string> categories, List<string> keywords, bool include_templates)
		{
			EncounterBuilder.fCreatures.Clear();
			List<Creature> creatures = Session.Creatures;
			foreach (Creature current in creatures)
			{
				if (current != null && (min_level == -1 || current.Level >= min_level) && (max_level == -1 || current.Level <= max_level) && (categories == null || categories.Count == 0 || categories.Contains(current.Category)))
				{
					if (keywords != null && keywords.Count != 0)
					{
						bool flag = false;
						foreach (string current2 in keywords)
						{
							if (current.Phenotype.ToLower().Contains(current2.ToLower()))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							continue;
						}
					}
					EncounterCard encounterCard = new EncounterCard();
					encounterCard.CreatureID = current.ID;
					EncounterBuilder.fCreatures.Add(encounterCard);
					if (include_templates)
					{
						EncounterBuilder.add_templates(current);
					}
				}
			}
			foreach (CustomCreature current3 in Session.Project.CustomCreatures)
			{
				EncounterCard encounterCard2 = new EncounterCard();
				encounterCard2.CreatureID = current3.ID;
				EncounterBuilder.fCreatures.Add(encounterCard2);
				if (include_templates)
				{
					EncounterBuilder.add_templates(current3);
				}
			}
		}

		private static void add_templates(ICreature creature)
		{
			if (creature.Role is Minion)
			{
				return;
			}
			ComplexRole complexRole = creature.Role as ComplexRole;
			if (complexRole.Flag == RoleFlag.Solo)
			{
				return;
			}
			foreach (Library current in Session.Libraries)
			{
				foreach (CreatureTemplate current2 in current.Templates)
				{
					EncounterCard encounterCard = new EncounterCard();
					encounterCard.CreatureID = creature.ID;
					encounterCard.TemplateIDs.Add(current2.ID);
				}
			}
		}

		private static void build_trap_list(int level)
		{
			int num = level - 3;
			int num2 = level + 5;
			EncounterBuilder.fTraps.Clear();
			foreach (Trap current in Session.Traps)
			{
				if (current.Level >= num && current.Level <= num2)
				{
					EncounterBuilder.fTraps.Add(current);
				}
			}
		}

		private static void build_challenge_list(int level)
		{
			int num = level - 3;
			int num2 = level + 5;
			EncounterBuilder.fChallenges.Clear();
			foreach (SkillChallenge current in Session.SkillChallenges)
			{
				if (current.Level >= num && current.Level <= num2)
				{
					EncounterBuilder.fChallenges.Add(current);
				}
			}
		}
	}
}
