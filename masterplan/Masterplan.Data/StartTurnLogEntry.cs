using System;

namespace Masterplan.Data
{
	[Serializable]
	public class StartTurnLogEntry : IEncounterLogEntry
	{
		private Guid fID = Guid.Empty;

		private DateTime fTimestamp = DateTime.Now;

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

		public bool Important
		{
			get
			{
				return true;
			}
		}

		public string Description(Encounter enc, bool detailed)
		{
			return "Start turn: " + EncounterLog.GetName(this.fID, enc, detailed);
		}
	}
}
