using System;

namespace Masterplan.Data
{
	[Serializable]
	public class SkillChallengeLogEntry : IEncounterLogEntry
	{
		private Guid fID = Guid.Empty;

		private DateTime fTimestamp = DateTime.Now;

		private bool fSuccess = true;

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

		public bool Success
		{
			get
			{
				return this.fSuccess;
			}
			set
			{
				this.fSuccess = value;
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
			if (this.fSuccess)
			{
				return name + " gained a success";
			}
			return name + " incurred a failure";
		}
	}
}
