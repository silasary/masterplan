using System;

namespace Masterplan.Data
{
	[Serializable]
	public class StartRoundLogEntry : IEncounterLogEntry
	{
		private DateTime fTimestamp = DateTime.Now;

		private int fRound = 1;

		public Guid CombatantID
		{
			get
			{
				return Guid.Empty;
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

		public int Round
		{
			get
			{
				return this.fRound;
			}
			set
			{
				this.fRound = value;
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
			return "Round " + this.fRound;
		}
	}
}
