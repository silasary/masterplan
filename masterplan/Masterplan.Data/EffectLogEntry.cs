using System;

namespace Masterplan.Data
{
	[Serializable]
	public class EffectLogEntry : IEncounterLogEntry
	{
		private Guid fID = Guid.Empty;

		private DateTime fTimestamp = DateTime.Now;

		private string fEffectText = "";

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

		public string EffectText
		{
			get
			{
				return this.fEffectText;
			}
			set
			{
				this.fEffectText = value;
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
				return name + " gained " + this.fEffectText;
			}
			return name + " lost " + this.fEffectText;
		}
	}
}
