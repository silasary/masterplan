using System;

namespace Masterplan.Data
{
	[Serializable]
	public class CalendarEvent
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private Guid fMonthID = Guid.Empty;

		private int fDayIndex = 1;

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

		public Guid MonthID
		{
			get
			{
				return this.fMonthID;
			}
			set
			{
				this.fMonthID = value;
			}
		}

		public int DayIndex
		{
			get
			{
				return this.fDayIndex;
			}
			set
			{
				this.fDayIndex = value;
			}
		}

		public CalendarEvent Copy()
		{
			return new CalendarEvent
			{
				ID = this.fID,
				Name = this.fName,
				MonthID = this.fMonthID,
				DayIndex = this.fDayIndex
			};
		}
	}
}
