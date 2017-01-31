using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class DamageLogEntry : IEncounterLogEntry
	{
		private Guid fID = Guid.Empty;

		private DateTime fTimestamp = DateTime.Now;

		private int fAmount;

		private List<DamageType> fTypes = new List<DamageType>();

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

		public int Amount
		{
			get
			{
				return this.fAmount;
			}
			set
			{
				this.fAmount = value;
			}
		}

		public List<DamageType> Types
		{
			get
			{
				return this.fTypes;
			}
			set
			{
				this.fTypes = value;
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
			string text = "";
			if (this.fTypes != null)
			{
				foreach (DamageType current in this.fTypes)
				{
					text += " ";
					text += current.ToString().ToLower();
				}
			}
			string text2 = (this.fAmount >= 0) ? "takes" : "heals";
			return string.Concat(new object[]
			{
				EncounterLog.GetName(this.fID, enc, detailed),
				" ",
				text2,
				" ",
				Math.Abs(this.fAmount),
				text,
				" damage"
			});
		}
	}
}
