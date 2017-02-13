using System;

namespace Masterplan.Data
{
	public interface IEncounterLogEntry
	{
		Guid CombatantID
		{
			get;
		}

		DateTime Timestamp
		{
			get;
		}

		bool Important
		{
			get;
		}

		string Description(Encounter enc, bool detailed);
	}
}
