using System;

namespace Masterplan.Data
{
	[Serializable]
	public class PauseLogEntry : IEncounterLogEntry
	{
		private DateTime fTimestamp = DateTime.Now;

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

		public bool Important
		{
			get
			{
				return false;
			}
		}

		public string Description(Encounter enc, bool detailed)
		{
			return string.Concat(new string[]
			{
				"Paused (",
				this.fTimestamp.ToShortTimeString(),
				" ",
				this.fTimestamp.ToShortDateString(),
				")"
			});
		}
	}
}
