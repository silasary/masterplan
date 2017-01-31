using System;

namespace Masterplan.Data
{
	[Serializable]
	public class CalendarDate
	{
		private Guid fID = Guid.NewGuid();

		private Guid fCalendarID = Guid.Empty;

		private int fYear;

		private Guid fMonthID = Guid.Empty;

		private int fDayIndex;

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

		public Guid CalendarID
		{
			get
			{
				return this.fCalendarID;
			}
			set
			{
				this.fCalendarID = value;
			}
		}

		public int Year
		{
			get
			{
				return this.fYear;
			}
			set
			{
				this.fYear = value;
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

		public override string ToString()
		{
			Calendar calendar = Session.Project.FindCalendar(this.fCalendarID);
			if (calendar == null)
			{
				return "";
			}
			MonthInfo monthInfo = calendar.FindMonth(this.fMonthID);
			if (monthInfo == null)
			{
				return "";
			}
			int num = this.fDayIndex + 1;
			return string.Concat(new object[]
			{
				monthInfo.Name,
				" ",
				num,
				", ",
				this.fYear
			});
		}

		public CalendarDate Copy()
		{
			return new CalendarDate
			{
				ID = this.fID,
				Year = this.fYear,
				CalendarID = this.fCalendarID,
				MonthID = this.fMonthID,
				DayIndex = this.fDayIndex
			};
		}
	}
}
