using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class EncounterDeck
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private int fLevel = 1;

		private List<EncounterCard> fCards = new List<EncounterCard>();

		public Guid ID
		{
			get
			{
				return this.fID;
			}
			set
			{
				this.fID = value;
			}
		}

		public string Name
		{
			get
			{
				return this.fName;
			}
			set
			{
				this.fName = value;
			}
		}

		public int Level
		{
			get
			{
				return this.fLevel;
			}
			set
			{
				this.fLevel = value;
			}
		}

		public List<EncounterCard> Cards
		{
			get
			{
				return this.fCards;
			}
			set
			{
				this.fCards = value;
			}
		}

		public bool DrawEncounter(Encounter enc)
		{
			if (this.fCards.Count == 0)
			{
				return false;
			}
			List<EncounterCard> list = new List<EncounterCard>();
			List<EncounterCard> list2 = new List<EncounterCard>();
			foreach (EncounterCard current in this.fCards)
			{
				if (!current.Drawn)
				{
					list2.Add(current);
				}
			}
			int num = 0;
			while (true)
			{
				num++;
				bool flag = false;
				int num2 = Session.Project.Party.Size;
				while (list.Count < num2 && list2.Count != 0)
				{
					int index = Session.Random.Next() % list2.Count;
					EncounterCard encounterCard = list2[index];
					list.Add(encounterCard);
					list2.Remove(encounterCard);
					if (encounterCard.Category == CardCategory.Lurker && !flag)
					{
						num2++;
						flag = true;
					}
				}
				int num3 = 0;
				foreach (EncounterCard current2 in list)
				{
					if (current2.Category == CardCategory.SoldierBrute)
					{
						num3++;
					}
				}
				if (num3 == 1 || num == 1000)
				{
					break;
				}
				list2.AddRange(list);
				list.Clear();
			}
			foreach (EncounterCard current3 in list)
			{
				if (current3.Category == CardCategory.Solo)
				{
					list.Remove(current3);
					list2.AddRange(list);
					list.Clear();
					list.Add(current3);
					break;
				}
			}
			foreach (EncounterCard current4 in list)
			{
				current4.Drawn = true;
			}
			enc.Slots.Clear();
			foreach (EncounterCard current5 in list)
			{
				EncounterSlot encounterSlot = null;
				foreach (EncounterSlot current6 in enc.Slots)
				{
					if (current6.Card.CreatureID == current5.CreatureID)
					{
						encounterSlot = current6;
						break;
					}
				}
				if (encounterSlot == null)
				{
					encounterSlot = new EncounterSlot();
					encounterSlot.Card = current5;
					enc.Slots.Add(encounterSlot);
				}
				int num4 = 1;
				switch (current5.Category)
				{
				case CardCategory.SoldierBrute:
					num4 = 2;
					break;
				case CardCategory.Minion:
					num4 += 4;
					break;
				}
				for (int num5 = 0; num5 != num4; num5++)
				{
					CombatData item = new CombatData();
					encounterSlot.CombatData.Add(item);
				}
			}
			foreach (EncounterSlot current7 in enc.Slots)
			{
				current7.SetDefaultDisplayNames();
			}
			return true;
		}

		public bool DrawDelve(PlotPoint pp, Map map)
		{
			foreach (MapArea current in map.Areas)
			{
				Encounter encounter = new Encounter();
				if (!this.DrawEncounter(encounter))
				{
					return false;
				}
				PlotPoint plotPoint = new PlotPoint(current.Name);
				plotPoint.Element = encounter;
				pp.Subplot.Points.Add(plotPoint);
			}
			return true;
		}

		public int Count(CardCategory cat)
		{
			int num = 0;
			foreach (EncounterCard current in this.fCards)
			{
				if (current.Category == cat)
				{
					num++;
				}
			}
			return num;
		}

		public int Count(int level)
		{
			int num = 0;
			foreach (EncounterCard current in this.fCards)
			{
				if (current.Level == level)
				{
					num++;
				}
			}
			return num;
		}

		public EncounterDeck Copy()
		{
			EncounterDeck encounterDeck = new EncounterDeck();
			encounterDeck.ID = this.fID;
			encounterDeck.Name = this.fName;
			encounterDeck.Level = this.fLevel;
			foreach (EncounterCard current in this.fCards)
			{
				encounterDeck.Cards.Add(current.Copy());
			}
			return encounterDeck;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}
