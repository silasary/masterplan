using System;

namespace Masterplan.Data
{
	[Serializable]
	public class DayInfo
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		public Guid ID
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

		public string Name
		{
			get
			{
				return this.fName;
			}
			set
			{
				this.fName = value;
			}
		}

		public DayInfo Copy()
		{
			return new DayInfo
			{
				ID = this.fID,
				Name = this.fName
			};
		}
	}
}
