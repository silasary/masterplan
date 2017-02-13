using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	internal class ReportRow : IComparable<ReportRow>
	{
		private string fHeading = "";

		private Guid fCombatantID = Guid.Empty;

		private List<int> fValues = new List<int>();

		public string Heading
		{
			get
			{
				return this.fHeading;
			}
			set
			{
				this.fHeading = value;
			}
		}

		public Guid CombatantID
		{
			get
			{
				return this.fCombatantID;
			}
			set
			{
				this.fCombatantID = value;
			}
		}

		public List<int> Values
		{
			get
			{
				return this.fValues;
			}
		}

		public int Total
		{
			get
			{
				int num = 0;
				foreach (int current in this.fValues)
				{
					num += current;
				}
				return num;
			}
		}

		public double Average
		{
			get
			{
				return (double)this.Total / (double)this.fValues.Count;
			}
		}

		public int CompareTo(ReportRow rhs)
		{
			return this.Total.CompareTo(rhs.Total) * -1;
		}
	}
}
