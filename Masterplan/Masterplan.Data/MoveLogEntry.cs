using System;

namespace Masterplan.Data
{
	[Serializable]
	public class MoveLogEntry : IEncounterLogEntry
	{
		private Guid fID = Guid.Empty;

		private DateTime fTimestamp = DateTime.Now;

		private int fDistance;

		private string fDetails = "";

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

		public int Distance
		{
			get
			{
				return this.fDistance;
			}
			set
			{
				this.fDistance = value;
			}
		}

		public string Details
		{
			get
			{
				return this.fDetails;
			}
			set
			{
				this.fDetails = value;
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
			string text = name + " moves";
			if (this.fDistance > 0)
			{
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					" ",
					this.fDistance,
					" sq"
				});
			}
			if (this.fDetails != "")
			{
				text = text + " " + this.fDetails.Trim();
			}
			return text;
		}
	}
}
