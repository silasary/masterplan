using System;

namespace Masterplan.Data
{
	[Serializable]
	public class CreatureToken : IToken
	{
		public Guid SlotID = Guid.Empty;

		public CombatData Data;

		public CreatureToken()
		{
		}

		public CreatureToken(Guid slot_id, CombatData data)
		{
			this.SlotID = slot_id;
			this.Data = data;
		}
	}
}
