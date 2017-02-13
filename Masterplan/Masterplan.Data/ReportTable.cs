using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	internal class ReportTable
	{
		private ReportType fReportType;

		private BreakdownType fBreakdownType;

		private List<ReportRow> fRows = new List<ReportRow>();

		public ReportType ReportType
		{
			get
			{
				return this.fReportType;
			}
			set
			{
				this.fReportType = value;
			}
		}

		public BreakdownType BreakdownType
		{
			get
			{
				return this.fBreakdownType;
			}
			set
			{
				this.fBreakdownType = value;
			}
		}

		public List<ReportRow> Rows
		{
			get
			{
				return this.fRows;
			}
		}

		public int Rounds
		{
			get
			{
				int num = 0;
				foreach (ReportRow current in this.fRows)
				{
					num = Math.Max(num, current.Values.Count);
				}
				return num;
			}
		}

		public int GrandTotal
		{
			get
			{
				int num = 0;
				foreach (ReportRow current in this.fRows)
				{
					num += current.Total;
				}
				return num;
			}
		}

		public int ColumnTotal(int column)
		{
			int num = 0;
			foreach (ReportRow current in this.fRows)
			{
				num += current.Values[column];
			}
			return num;
		}

		public void ReduceToPCs()
		{
			List<ReportRow> list = new List<ReportRow>();
			foreach (ReportRow current in this.fRows)
			{
				if (Session.Project.FindHero(current.CombatantID) == null)
				{
					list.Add(current);
				}
			}
			foreach (ReportRow current2 in list)
			{
				this.fRows.Remove(current2);
			}
		}
	}
}
