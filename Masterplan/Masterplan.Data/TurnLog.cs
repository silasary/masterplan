using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	internal class TurnLog
	{
		private Guid fID = Guid.Empty;

		private List<IEncounterLogEntry> fEntries = new List<IEncounterLogEntry>();

		public DateTime Start = DateTime.MinValue;

		public DateTime End = DateTime.MinValue;

		public Guid ID
		{
			get
			{
				return this.fID;
			}
		}

		public List<IEncounterLogEntry> Entries
		{
			get
			{
				return this.fEntries;
			}
		}

		public TurnLog(Guid id)
		{
			this.fID = id;
		}

		public TimeSpan Time()
		{
			TimeSpan timeSpan = this.End - this.Start;
			if (timeSpan.Ticks < 0L)
			{
				return new TimeSpan(0L);
			}
			IEncounterLogEntry encounterLogEntry = null;
			foreach (IEncounterLogEntry current in this.fEntries)
			{
				if (current is PauseLogEntry)
				{
					encounterLogEntry = current;
				}
				else if (current is ResumeLogEntry && encounterLogEntry != null)
				{
					TimeSpan t = current.Timestamp - encounterLogEntry.Timestamp;
					timeSpan -= t;
					encounterLogEntry = null;
				}
			}
			return timeSpan;
		}

		public int Damage(List<Guid> allyIDs)
		{
			int num = 0;
			foreach (IEncounterLogEntry current in this.fEntries)
			{
				DamageLogEntry damageLogEntry = current as DamageLogEntry;
				if (damageLogEntry != null && allyIDs.Contains(damageLogEntry.CombatantID))
				{
					num += damageLogEntry.Amount;
				}
			}
			return num;
		}

		public int Movement()
		{
			int num = 0;
			foreach (IEncounterLogEntry current in this.fEntries)
			{
				MoveLogEntry moveLogEntry = current as MoveLogEntry;
				if (moveLogEntry != null && moveLogEntry.Distance > 0)
				{
					num += moveLogEntry.Distance;
				}
			}
			return num;
		}
	}
}
