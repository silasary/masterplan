using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class EncounterTemplate
	{
		private Difficulty fDifficulty = Difficulty.Moderate;

		private List<EncounterTemplateSlot> fSlots = new List<EncounterTemplateSlot>();

		public Difficulty Difficulty
		{
			get
			{
				return this.fDifficulty;
			}
			set
			{
				this.fDifficulty = value;
			}
		}

		public List<EncounterTemplateSlot> Slots
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

		public EncounterTemplate()
		{
		}

		public EncounterTemplate(Difficulty diff)
		{
			this.fDifficulty = diff;
		}

		public EncounterTemplateSlot FindSlot(EncounterSlot enc_slot, int level)
		{
			foreach (EncounterTemplateSlot current in this.fSlots)
			{
				if (current.Count >= enc_slot.CombatData.Count)
				{
					bool flag = current.Match(enc_slot.Card, level);
					if (flag)
					{
						return current;
					}
				}
			}
			return null;
		}

		public EncounterTemplate Copy()
		{
			EncounterTemplate encounterTemplate = new EncounterTemplate();
			encounterTemplate.Difficulty = this.fDifficulty;
			foreach (EncounterTemplateSlot current in this.fSlots)
			{
				encounterTemplate.Slots.Add(current.Copy());
			}
			return encounterTemplate;
		}
	}
}
