using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Tools.Generators
{
	internal class DelveBuilder
	{
		public static PlotPoint AutoBuild(Map map, AutoBuildData data)
		{
			PlotPoint delve = new PlotPoint(map.Name + " Delve");
			delve.Details = "This delve was automatically generated.";
			delve.Element = new MapElement(map.ID, Guid.Empty);
			int level = data.Level;
			List<Parcel> list = Treasure.CreateParcelSet(data.Level, Session.Project.Party.Size, false);
			foreach (MapArea current in map.Areas)
			{
				PlotPoint plotPoint = new PlotPoint(current.Name);
				switch (Session.Random.Next() % 8)
				{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
					plotPoint.Element = DelveBuilder.get_encounter(map, current, data);
					break;
				case 6:
					plotPoint.Element = DelveBuilder.get_encounter(map, current, data);
					break;
				case 7:
					plotPoint.Element = DelveBuilder.get_encounter(map, current, data);
					break;
				}
				int num2 = 0;
				switch (Session.Random.Next() % 8)
				{
				case 0:
				case 1:
					num2 = 0;
					break;
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
					num2 = 1;
					break;
				case 7:
					num2 = 2;
					break;
				}
				for (int num3 = 0; num3 != num2; num3++)
				{
					if (list.Count == 0)
					{
						level = Math.Min(30, level + 1);
						list = Treasure.CreateParcelSet(level, Session.Project.Party.Size, false);
					}
					int index = Session.Random.Next() % list.Count;
					Parcel item = list[index];
					list.RemoveAt(index);
					plotPoint.Parcels.Add(item);
				}
				delve.Subplot.Points.Add(plotPoint);
			}
			return delve;
		}

		private static Encounter get_encounter(Map map, MapArea ma, AutoBuildData data)
		{
			Encounter encounter = new Encounter();
			encounter.MapID = map.ID;
			encounter.MapAreaID = ma.ID;
			EncounterBuilder.Build(data, encounter, false);
			Difficulty difficulty = encounter.GetDifficulty(Session.Project.Party.Level, Session.Project.Party.Size);
			if (difficulty != Difficulty.Extreme)
			{
				switch (Session.Random.Next() % 6)
				{
				case 0:
				case 1:
				case 3:
				{
					Trap trap = DelveBuilder.select_trap(data);
					if (trap != null)
					{
						encounter.Traps.Add(trap);
					}
					break;
				}
				case 4:
				{
					SkillChallenge skillChallenge = DelveBuilder.select_challenge(data);
					if (skillChallenge != null)
					{
						encounter.SkillChallenges.Add(skillChallenge);
					}
					break;
				}
				}
			}
			List<Rectangle> list = new List<Rectangle>();
			foreach (TileData current in map.Tiles)
			{
				Tile tile = Session.FindTile(current.TileID, SearchType.Global);
				int width = (current.Rotations % 2 == 0) ? tile.Size.Width : tile.Size.Height;
				int height = (current.Rotations % 2 == 0) ? tile.Size.Height : tile.Size.Width;
				Size size = new Size(width, height);
				Rectangle item = new Rectangle(current.Location, size);
				list.Add(item);
			}
			Dictionary<Point, bool> dictionary = new Dictionary<Point, bool>();
			for (int num = ma.Region.Left; num != ma.Region.Right; num++)
			{
				for (int num2 = ma.Region.Top; num2 != ma.Region.Bottom; num2++)
				{
					Point point = new Point(num, num2);
					bool value = false;
					foreach (Rectangle current2 in list)
					{
						if (current2.Contains(point))
						{
							value = true;
							break;
						}
					}
					dictionary[point] = value;
				}
			}
			foreach (EncounterSlot current3 in encounter.Slots)
			{
				ICreature creature = Session.FindCreature(current3.Card.CreatureID, SearchType.Global);
				int size2 = Creature.GetSize(creature.Size);
				foreach (CombatData current4 in current3.CombatData)
				{
					List<Point> list2 = new List<Point>();
					for (int num3 = ma.Region.Left; num3 != ma.Region.Right; num3++)
					{
						for (int num4 = ma.Region.Top; num4 != ma.Region.Bottom; num4++)
						{
							Point item2 = new Point(num3, num4);
							bool flag = true;
							for (int num5 = item2.X; num5 != item2.X + size2; num5++)
							{
								for (int num6 = item2.Y; num6 != item2.Y + size2; num6++)
								{
									Point key = new Point(num5, num6);
									if (!dictionary.ContainsKey(key) || !dictionary[key])
									{
										flag = false;
									}
								}
							}
							if (flag)
							{
								list2.Add(item2);
							}
						}
					}
					if (list2.Count != 0)
					{
						int index = Session.Random.Next() % list2.Count;
						Point location = list2[index];
						current4.Location = location;
						for (int num7 = location.X; num7 != location.X + size2; num7++)
						{
							for (int num8 = location.Y; num8 != location.Y + size2; num8++)
							{
								Point key2 = new Point(num7, num8);
								dictionary[key2] = false;
							}
						}
					}
				}
			}
			encounter.SetStandardEncounterNotes();
			EncounterNote encounterNote = encounter.FindNote("Illumination");
			if (encounterNote != null)
			{
				switch (Session.Random.Next(6))
				{
				case 0:
				case 1:
				case 2:
					encounterNote.Contents = "The area is in bright light.";
					break;
				case 3:
				case 4:
					encounterNote.Contents = "The area is in dim light.";
					break;
				case 5:
					encounterNote.Contents = "None.";
					break;
				}
			}
			EncounterNote encounterNote2 = encounter.FindNote("Victory Conditions");
			if (encounterNote2 != null)
			{
				List<string> list3 = new List<string>();
				List<string> list4 = new List<string>();
				bool flag2 = false;
				int num9 = 0;
				foreach (EncounterSlot current5 in encounter.Slots)
				{
					if (current5.CombatData.Count == 1 && (current5.Card.Leader || current5.Card.Flag == RoleFlag.Elite || current5.Card.Flag == RoleFlag.Solo))
					{
						list4.Add(current5.CombatData[0].DisplayName);
					}
					ICreature creature2 = Session.FindCreature(current5.Card.CreatureID, SearchType.Global);
					if (creature2 != null)
					{
						if (creature2.Role is Minion)
						{
							flag2 = true;
						}
						else
						{
							num9 += current5.CombatData.Count;
						}
					}
				}
				if (list4.Count != 0)
				{
					int index2 = Session.Random.Next() % list4.Count;
					string text = list4[index2];
					if (Session.Random.Next() % 12 == 0)
					{
						list3.Add("Defeat " + text + ".");
						list3.Add("Capture " + text + ".");
					}
					if (Session.Random.Next() % 12 == 0)
					{
						int num10 = Session.Dice(2, 4);
						list3.Add(string.Concat(new object[]
						{
							"The party must defeat ",
							text,
							" within ",
							num10,
							" rounds."
						}));
					}
					if (Session.Random.Next() % 12 == 0)
					{
						int num11 = Session.Dice(2, 4);
						list3.Add(string.Concat(new object[]
						{
							"After ",
							num11,
							", ",
							text,
							" will flee or surrender."
						}));
					}
					if (Session.Random.Next() % 12 == 0)
					{
						int num12 = 10 * Session.Dice(1, 4);
						list3.Add(string.Concat(new object[]
						{
							"At ",
							num12,
							"% HP, ",
							text,
							" will flee or surrender."
						}));
					}
					if (Session.Random.Next() % 12 == 0)
					{
						list3.Add("The party must obtain an item from " + text + ".");
					}
					if (Session.Random.Next() % 12 == 0)
					{
						list3.Add("Defeat " + text + " by destroying a guarded object in the area.");
					}
					if (flag2)
					{
						list3.Add("Minions will flee or surrender when " + text + " is defeated.");
					}
				}
				if (Session.Random.Next() % 12 == 0)
				{
					int num13 = 2 + Session.Random.Next() % 4;
					list3.Add("The party must defeat their opponents within " + num13 + " rounds.");
				}
				if (flag2 && Session.Random.Next() % 12 == 0)
				{
					int num14 = 2 + Session.Random.Next() % 4;
					list3.Add("The party must defend a certain area from " + num14 + " waves of minions.");
				}
				if (Session.Random.Next() % 12 == 0)
				{
					int num15 = 2 + Session.Random.Next() % 4;
					list3.Add("At least one character must get to a certain area and stay there for " + num15 + " consecutive rounds.");
				}
				if (Session.Random.Next() % 12 == 0)
				{
					int num16 = 2 + Session.Random.Next() % 4;
					list3.Add("The party must leave the area within " + num16 + " rounds.");
				}
				if (Session.Random.Next() % 12 == 0)
				{
					list3.Add("The party must keep the enemy away from a certain area for the duration of the encounter.");
				}
				if (Session.Random.Next() % 12 == 0)
				{
					list3.Add("The party must escort an NPC safely through the encounter area.");
				}
				if (Session.Random.Next() % 12 == 0)
				{
					list3.Add("The party must rescue an NPC from their opponents.");
				}
				if (Session.Random.Next() % 12 == 0)
				{
					list3.Add("The party must avoid contact with the enemy in this area.");
				}
				if (Session.Random.Next() % 12 == 0)
				{
					list3.Add("The party must attack and destroy a feature of the area.");
				}
				if (num9 > 1 && Session.Random.Next() % 12 == 0)
				{
					int num17 = 1 + Session.Random.Next(num9);
					list3.Add("The party must defeat " + num17 + " non-minion opponents.");
				}
				if (list3.Count != 0)
				{
					int index3 = Session.Random.Next() % list3.Count;
					encounterNote2.Contents = list3[index3];
				}
			}
			return encounter;
		}

		private static TrapElement get_trap(Map map, MapArea ma, AutoBuildData data)
		{
			Trap trap = DelveBuilder.select_trap(data);
			if (trap != null)
			{
				return new TrapElement
				{
					Trap = trap,
					MapID = map.ID,
					MapAreaID = ma.ID
				};
			}
			return null;
		}

		private static SkillChallenge get_challenge(Map map, MapArea ma, AutoBuildData data)
		{
			SkillChallenge skillChallenge = DelveBuilder.select_challenge(data);
			if (skillChallenge != null)
			{
				skillChallenge.MapID = map.ID;
				skillChallenge.MapAreaID = ma.ID;
				return skillChallenge;
			}
			return null;
		}

		private static Trap select_trap(AutoBuildData data)
		{
			List<Trap> list = new List<Trap>();
			int num = data.Level - 3;
			int num2 = data.Level + 5;
			list.Clear();
			foreach (Trap current in Session.Traps)
			{
				if (current.Level >= num && current.Level <= num2)
				{
					list.Add(current.Copy());
				}
			}
			if (list.Count != 0)
			{
				int index = Session.Random.Next() % list.Count;
				return list[index];
			}
			return null;
		}

		private static SkillChallenge select_challenge(AutoBuildData data)
		{
			List<SkillChallenge> list = new List<SkillChallenge>();
			int num = data.Level - 3;
			int num2 = data.Level + 5;
			list.Clear();
			foreach (SkillChallenge current in Session.SkillChallenges)
			{
				if (current.Level == -1)
				{
					SkillChallenge skillChallenge = current.Copy() as SkillChallenge;
					skillChallenge.Level = Session.Project.Party.Level;
					list.Add(skillChallenge);
				}
				else if (current.Level >= num && current.Level <= num2)
				{
					list.Add(current.Copy() as SkillChallenge);
				}
			}
			if (list.Count != 0)
			{
				int index = Session.Random.Next() % list.Count;
				return list[index];
			}
			return null;
		}
	}
}
