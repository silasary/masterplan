using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class EncounterWave
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private bool fActive;

		private List<EncounterSlot> fSlots = new List<EncounterSlot>();

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

		public bool Active
		{
			get
			{
				return this.fActive;
			}
			set
			{
				this.fActive = value;
			}
		}

		public List<EncounterSlot> Slots
		{
			get
			{
				return this.fSlots;
			}
			set
			{
				this.fSlots = value;
			}
		}

		public int Count
		{
			get
			{
				int num = 0;
				foreach (EncounterSlot current in this.fSlots)
				{
					num += current.CombatData.Count;
				}
				return num;
			}
		}

		public EncounterWave Copy()
		{
			EncounterWave encounterWave = new EncounterWave();
			encounterWave.ID = this.fID;
			encounterWave.Name = this.fName;
			encounterWave.Active = this.fActive;
			foreach (EncounterSlot current in this.fSlots)
			{
				encounterWave.Slots.Add(current.Copy());
			}
			return encounterWave;
		}
	}
}
