using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Data
{
	internal class EncounterReport
	{
		private List<RoundLog> fRounds = new List<RoundLog>();

		public List<RoundLog> Rounds
		{
			get
			{
				return this.fRounds;
			}
		}

		public List<Guid> Combatants
		{
			get
			{
				List<Guid> list = new List<Guid>();
				foreach (RoundLog current in this.fRounds)
				{
					foreach (TurnLog current2 in current.Turns)
					{
						if (!list.Contains(current2.ID))
						{
							list.Add(current2.ID);
						}
					}
				}
				return list;
			}
		}

		public RoundLog GetRound(int round)
		{
			foreach (RoundLog current in this.fRounds)
			{
				if (current.Round == round)
				{
					return current;
				}
			}
			return null;
		}

		private List<TurnLog> get_turns(Guid id)
		{
			List<TurnLog> list = new List<TurnLog>();
			foreach (RoundLog current in this.fRounds)
			{
				foreach (TurnLog current2 in current.Turns)
				{
					if (current2.ID == id)
					{
						list.Add(current2);
					}
				}
			}
			return list;
		}

		public List<Guid> MVPs(Encounter enc)
		{
			Dictionary<Guid, int> dictionary = new Dictionary<Guid, int>();
			ReportTable reportTable = this.CreateTable(ReportType.Time, BreakdownType.Controller, enc);
			reportTable.ReduceToPCs();
			this.add_table(reportTable, dictionary);
			ReportTable reportTable2 = this.CreateTable(ReportType.DamageToEnemies, BreakdownType.Controller, enc);
			reportTable2.ReduceToPCs();
			this.add_table(reportTable2, dictionary);
			ReportTable reportTable3 = this.CreateTable(ReportType.DamageToAllies, BreakdownType.Controller, enc);
			reportTable3.ReduceToPCs();
			this.add_table(reportTable3, dictionary);
			List<Guid> list = new List<Guid>();
			int num = -2147483648;
			foreach (Guid current in dictionary.Keys)
			{
				int num2 = dictionary[current];
				if (num2 > num)
				{
					num = num2;
					list.Clear();
				}
				if (num2 == num)
				{
					list.Add(current);
				}
			}
			return list;
		}

		private void add_table(ReportTable table, Dictionary<Guid, int> standings)
		{
			List<int> list = new List<int>
			{
				25,
				18,
				15,
				12,
				10,
				8,
				6,
				4,
				2,
				1
			};
			List<int> list2 = new List<int>();
			foreach (ReportRow current in table.Rows)
			{
				if (!list2.Contains(current.Total))
				{
					list2.Add(current.Total);
				}
			}
			Dictionary<Guid, int> dictionary = new Dictionary<Guid, int>();
			foreach (int current2 in list2)
			{
				int value = 0;
				if (dictionary.Count < list.Count)
				{
					value = list[dictionary.Count];
				}
				foreach (ReportRow current3 in table.Rows)
				{
					if (current3.Total == current2)
					{
						dictionary[current3.CombatantID] = value;
					}
				}
			}
			foreach (Guid current4 in dictionary.Keys)
			{
				if (!standings.ContainsKey(current4))
				{
					standings[current4] = 0;
				}
				Guid key;
				standings[key = current4] = standings[key] + dictionary[current4];
			}
		}

		public int Time(Guid id, int round)
		{
			TimeSpan t = default(TimeSpan);
			foreach (RoundLog current in this.fRounds)
			{
				if (current.Round == round || round == 0)
				{
					foreach (TurnLog current2 in current.Turns)
					{
						if (current2.ID == id || id == Guid.Empty)
						{
							t += current2.Time();
						}
					}
				}
			}
			return (int)t.TotalSeconds;
		}

		public int Damage(Guid id, int round, bool allies, Encounter enc)
		{
			int num = 0;
			foreach (RoundLog current in this.fRounds)
			{
				if (current.Round == round || round == 0)
				{
					foreach (TurnLog current2 in current.Turns)
					{
						if (current2.ID == id || id == Guid.Empty)
						{
							List<Guid> list = EncounterReport.get_allies(current2.ID, enc);
							List<Guid> list2 = new List<Guid>();
							if (allies)
							{
								list2.AddRange(list);
							}
							else
							{
								foreach (Guid current3 in this.Combatants)
								{
									if (!list.Contains(current3))
									{
										list2.Add(current3);
									}
								}
							}
							num += current2.Damage(list2);
						}
					}
				}
			}
			return num;
		}

		public int Movement(Guid id, int round)
		{
			int num = 0;
			foreach (RoundLog current in this.fRounds)
			{
				if (current.Round == round || round == 0)
				{
					foreach (TurnLog current2 in current.Turns)
					{
						if (current2.ID == id || id == Guid.Empty)
						{
							num += current2.Movement();
						}
					}
				}
			}
			return num;
		}

		private static List<Guid> get_allies(Guid id, Encounter enc)
		{
			List<Guid> list = new List<Guid>();
			if (Session.Project.FindHero(id) != null)
			{
				foreach (Hero current in Session.Project.Heroes)
				{
					list.Add(current.ID);
				}
				using (List<EncounterSlot>.Enumerator enumerator2 = enc.Slots.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						EncounterSlot current2 = enumerator2.Current;
						if (current2.Type == EncounterSlotType.Ally)
						{
							foreach (CombatData current3 in current2.CombatData)
							{
								list.Add(current3.ID);
							}
						}
					}
					return list;
				}
			}
			CombatData combatData = enc.FindCombatData(id);
			if (combatData != null)
			{
				EncounterSlot encounterSlot = enc.FindSlot(combatData);
				if (encounterSlot != null)
				{
					foreach (EncounterSlot current4 in enc.Slots)
					{
						if (current4.Type == encounterSlot.Type)
						{
							foreach (CombatData current5 in current4.CombatData)
							{
								list.Add(current5.ID);
							}
						}
					}
					if (encounterSlot.Type == EncounterSlotType.Ally)
					{
						foreach (Hero current6 in Session.Project.Heroes)
						{
							list.Add(current6.ID);
						}
					}
				}
			}
			return list;
		}

		public ReportTable CreateTable(ReportType report_type, BreakdownType breakdown_type, Encounter enc)
		{
			ReportTable reportTable = new ReportTable();
			reportTable.ReportType = report_type;
			reportTable.BreakdownType = breakdown_type;
			List<Pair<string, List<Guid>>> list = new List<Pair<string, List<Guid>>>();
			switch (breakdown_type)
			{
			case BreakdownType.Individual:
			{
				List<Guid> combatants = this.Combatants;
				using (List<Guid>.Enumerator enumerator = combatants.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Guid current = enumerator.Current;
						List<Guid> list2 = new List<Guid>();
						list2.Add(current);
						list.Add(new Pair<string, List<Guid>>(enc.WhoIs(current), list2));
					}
					goto IL_221;
				}
				break;
			}
			case BreakdownType.Controller:
				break;
			case BreakdownType.Faction:
			{
				List<Guid> list3 = new List<Guid>();
				List<Guid> list4 = new List<Guid>();
				List<Guid> list5 = new List<Guid>();
				List<Guid> list6 = new List<Guid>();
				List<Guid> combatants2 = this.Combatants;
				foreach (Guid current2 in combatants2)
				{
					if (Session.Project.FindHero(current2) != null)
					{
						list3.Add(current2);
					}
					else
					{
						CombatData data = enc.FindCombatData(current2);
						EncounterSlot encounterSlot = enc.FindSlot(data);
						switch (encounterSlot.Type)
						{
						case EncounterSlotType.Opponent:
							list6.Add(current2);
							break;
						case EncounterSlotType.Ally:
							list4.Add(current2);
							break;
						case EncounterSlotType.Neutral:
							list5.Add(current2);
							break;
						}
					}
				}
				list.Add(new Pair<string, List<Guid>>("PCs", list3));
				list.Add(new Pair<string, List<Guid>>("Allies", list4));
				list.Add(new Pair<string, List<Guid>>("Neutral", list5));
				list.Add(new Pair<string, List<Guid>>("Enemies", list6));
				goto IL_221;
			}
			default:
				goto IL_221;
			}
			List<Guid> list7 = new List<Guid>();
			List<Guid> combatants3 = this.Combatants;
			foreach (Guid current3 in combatants3)
			{
				if (Session.Project.FindHero(current3) != null)
				{
					List<Guid> list8 = new List<Guid>();
					list8.Add(current3);
					list.Add(new Pair<string, List<Guid>>(enc.WhoIs(current3), list8));
				}
				else
				{
					list7.Add(current3);
				}
			}
			list.Add(new Pair<string, List<Guid>>("DM", list7));
			IL_221:
			foreach (Pair<string, List<Guid>> current4 in list)
			{
				if (current4.Second.Count != 0)
				{
					ReportRow reportRow = new ReportRow();
					reportRow.Heading = current4.First;
					if (current4.Second.Count == 1)
					{
						reportRow.CombatantID = current4.Second[0];
					}
					for (int i = 1; i <= this.fRounds.Count; i++)
					{
						switch (report_type)
						{
						case ReportType.Time:
						{
							int num = 0;
							foreach (Guid current5 in current4.Second)
							{
								num += this.Time(current5, i);
							}
							reportRow.Values.Add(num);
							break;
						}
						case ReportType.DamageToEnemies:
						{
							int num2 = 0;
							foreach (Guid current6 in current4.Second)
							{
								num2 += this.Damage(current6, i, false, enc);
							}
							reportRow.Values.Add(num2);
							break;
						}
						case ReportType.DamageToAllies:
						{
							int num3 = 0;
							foreach (Guid current7 in current4.Second)
							{
								num3 += this.Damage(current7, i, true, enc);
							}
							reportRow.Values.Add(num3);
							break;
						}
						case ReportType.Movement:
						{
							int num4 = 0;
							foreach (Guid current8 in current4.Second)
							{
								num4 += this.Movement(current8, i);
							}
							reportRow.Values.Add(num4);
							break;
						}
						}
					}
					reportTable.Rows.Add(reportRow);
				}
			}
			reportTable.Rows.Sort();
			switch (reportTable.ReportType)
			{
			case ReportType.Time:
			case ReportType.DamageToAllies:
				reportTable.Rows.Reverse();
				break;
			}
			return reportTable;
		}
	}
}
