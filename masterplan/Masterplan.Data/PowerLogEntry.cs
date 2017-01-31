using System;

namespace Masterplan.Data
{
	[Serializable]
	public class PowerLogEntry : IEncounterLogEntry
	{
		private Guid fID = Guid.Empty;

		private DateTime fTimestamp = DateTime.Now;

		private string fPowerName = "";

		private bool fAdded = true;

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

		public string PowerName
		{
			get
			{
				return this.fPowerName;
			}
			set
			{
				this.fPowerName = value;
			}
		}

		public bool Added
		{
			get
			{
				return this.fAdded;
			}
			set
			{
				this.fAdded = value;
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
			string name = EncounterLog.GetName(this.fID, enc, detailed);
			if (this.fAdded)
			{
				return name + " used <B>" + this.fPowerName + "</B>";
			}
			return name + " recharged <B>" + this.fPowerName + "</B>";
		}
	}
}
