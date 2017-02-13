using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class EncounterSlot
	{
		private Guid fID = Guid.NewGuid();

		private EncounterCard fCard = new EncounterCard();

		private EncounterSlotType fType;

		private List<CombatData> fCombatData = new List<CombatData>();

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

		public EncounterCard Card
		{
			get
			{
				return this.fCard;
			}
			set
			{
				this.fCard = value;
			}
		}

		public EncounterSlotType Type
		{
			get
			{
				return this.fType;
			}
			set
			{
				this.fType = value;
			}
		}

		public List<CombatData> CombatData
		{
			get
			{
				return this.fCombatData;
			}
			set
			{
				this.fCombatData = value;
			}
		}

		public int XP
		{
			get
			{
				int num = 0;
				switch (this.fType)
				{
				case EncounterSlotType.Opponent:
					num = 1;
					break;
				case EncounterSlotType.Ally:
					num = -1;
					break;
				case EncounterSlotType.Neutral:
					num = 0;
					break;
				}
				return this.fCard.XP * this.fCombatData.Count * num;
			}
		}

		public CombatData FindCombatData(Point location)
		{
			foreach (CombatData current in this.fCombatData)
			{
				if (current.Location == location)
				{
					return current;
				}
			}
			return null;
		}

		public EncounterSlot Copy()
		{
			EncounterSlot encounterSlot = new EncounterSlot();
			encounterSlot.ID = this.fID;
			encounterSlot.Card = this.fCard.Copy();
			encounterSlot.Type = this.fType;
			foreach (CombatData current in this.fCombatData)
			{
				encounterSlot.CombatData.Add(current.Copy());
			}
			return encounterSlot;
		}

		public void SetDefaultDisplayNames()
		{
			string title = this.fCard.Title;
			if (this.fCombatData == null)
			{
				this.fCombatData = new List<CombatData>();
				this.fCombatData.Add(new CombatData());
			}
			foreach (CombatData current in this.fCombatData)
			{
				if (this.fCombatData.Count == 1)
				{
					current.DisplayName = title;
				}
				else
				{
					int num = this.fCombatData.IndexOf(current) + 1;
					current.DisplayName = title + " " + num;
				}
			}
		}

		public CreatureState GetState(CombatData data)
		{
			int hP = this.fCard.HP;
			int num = hP / 2;
			int num2 = hP - data.Damage;
			if (num2 <= 0)
			{
				return CreatureState.Defeated;
			}
			if (num2 <= num)
			{
				return CreatureState.Bloodied;
			}
			return CreatureState.Active;
		}
	}
}
