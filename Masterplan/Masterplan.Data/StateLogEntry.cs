using System;

namespace Masterplan.Data
{
	[Serializable]
	public class StateLogEntry : IEncounterLogEntry
	{
		private Guid fID = Guid.Empty;

		private DateTime fTimestamp = DateTime.Now;

		private CreatureState fState;

		public Guid CombatantID
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

		public DateTime Timestamp
		{
			get
			{
				return this.fTimestamp;
			}
			set
			{
				this.fTimestamp = value;
			}
		}

		public CreatureState State
		{
			get
			{
				return this.fState;
			}
			set
			{
				this.fState = value;
			}
		}

		public bool Important
		{
			get
			{
				return true;
			}
		}

		public string Description(Encounter enc, bool detailed)
		{
			string str = "not bloodied";
			if (this.fState != CreatureState.Active)
			{
				str = this.fState.ToString().ToLower();
			}
			return EncounterLog.GetName(this.fID, enc, detailed) + " is <B>" + str + "</B>";
		}
	}
}
