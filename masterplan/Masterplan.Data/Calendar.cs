using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Calendar
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fDetails = "";

		private int fCampaignYear = 1000;

		private List<MonthInfo> fMonths = new List<MonthInfo>();

		private List<DayInfo> fDays = new List<DayInfo>();

		private List<CalendarEvent> fSeasons = new List<CalendarEvent>();

		private List<CalendarEvent> fEvents = new List<CalendarEvent>();

		private List<Satellite> fSatellites = new List<Satellite>();

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

		public int CampaignYear
		{
			get
			{
				return this.fCampaignYear;
			}
			set
			{
				this.fCampaignYear = value;
			}
		}

		public List<MonthInfo> Months
		{
			get
			{
				return this.fMonths;
			}
			set
			{
				this.fMonths = value;
			}
		}

		public List<DayInfo> Days
		{
			get
			{
				return this.fDays;
			}
			set
			{
				this.fDays = value;
			}
		}

		public List<CalendarEvent> Seasons
		{
			get
			{
				return this.fSeasons;
			}
			set
			{
				this.fSeasons = value;
			}
		}

		public List<CalendarEvent> Events
		{
			get
			{
				return this.fEvents;
			}
			set
			{
				this.fEvents = value;
			}
		}

		public List<Satellite> Satellites
		{
			get
			{
				return this.fSatellites;
			}
			set
			{
				this.fSatellites = value;
			}
		}

		public int DayCount(int year)
		{
			int num = 0;
			foreach (MonthInfo current in this.fMonths)
			{
				num += current.DayCount;
				if (current.LeapModifier != 0 && current.LeapPeriod != 0 && year % current.LeapPeriod == 0)
				{
					num += current.LeapModifier;
				}
			}
			return num;
		}

		public MonthInfo FindMonth(Guid month_id)
		{
			foreach (MonthInfo current in this.fMonths)
			{
				if (current.ID == month_id)
				{
					return current;
				}
			}
			return null;
		}

		public override string ToString()
		{
			return this.fName;
		}

		public Calendar Copy()
		{
			Calendar calendar = new Calendar();
			calendar.ID = this.fID;
			calendar.Name = this.fName;
			calendar.Details = this.fDetails;
			calendar.CampaignYear = this.fCampaignYear;
			foreach (MonthInfo current in this.fMonths)
			{
				calendar.Months.Add(current.Copy());
			}
			foreach (DayInfo current2 in this.fDays)
			{
				calendar.Days.Add(current2.Copy());
			}
			foreach (CalendarEvent current3 in this.fSeasons)
			{
				calendar.Seasons.Add(current3.Copy());
			}
			foreach (CalendarEvent current4 in this.fEvents)
			{
				calendar.Events.Add(current4.Copy());
			}
			foreach (Satellite current5 in this.fSatellites)
			{
				calendar.Satellites.Add(current5.Copy());
			}
			return calendar;
		}
	}
}
