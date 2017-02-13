using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	public class CalendarPanel : UserControl
	{
		private Calendar fCalendar;

		private int fYear;

		private int fMonthIndex;

		private StringFormat fCentred = new StringFormat();

		private StringFormat fTopRight = new StringFormat();

		private int fWeeks;

		private int fDayOffset;

		private IContainer components;

		[Category("Data"), Description("The calendar to display.")]
		public Calendar Calendar
		{
			get
			{
				return this.fCalendar;
			}
			set
			{
				this.fCalendar = value;
				base.Invalidate();
			}
		}

		[Category("Data"), Description("The year to be displayed.")]
		public int Year
		{
			get
			{
				return this.fYear;
			}
			set
			{
				this.fYear = value;
				base.Invalidate();
			}
		}

		[Category("Data"), Description("The 0-based index of the month to be displayed.")]
		public int MonthIndex
		{
			get
			{
				return this.fMonthIndex;
			}
			set
			{
				this.fMonthIndex = value;
				base.Invalidate();
			}
		}

		public CalendarPanel()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
			this.fTopRight.Alignment = StringAlignment.Far;
			this.fTopRight.LineAlignment = StringAlignment.Near;
			this.fTopRight.Trimming = StringTrimming.EllipsisWord;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Brush brush = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(225, 225, 225), Color.FromArgb(180, 180, 180), LinearGradientMode.Vertical);
			e.Graphics.FillRectangle(brush, base.ClientRectangle);
			if (this.fCalendar == null)
			{
				e.Graphics.DrawString("(no calendar)", this.Font, SystemBrushes.WindowText, base.ClientRectangle, this.fCentred);
				return;
			}
			this.analyse_month();
			Font font = new Font(this.Font, FontStyle.Bold);
			for (int num = 0; num != this.fCalendar.Days.Count; num++)
			{
				DayInfo dayInfo = this.fCalendar.Days[num];
				RectangleF layoutRectangle = this.get_rect(num, -1);
				e.Graphics.DrawString(dayInfo.Name, font, SystemBrushes.WindowText, layoutRectangle, this.fCentred);
			}
			MonthInfo monthInfo = this.fCalendar.Months[this.fMonthIndex];
			int num2 = monthInfo.DayCount;
			if (monthInfo.LeapModifier != 0 && monthInfo.LeapPeriod != 0 && this.fYear % monthInfo.LeapPeriod == 0)
			{
				num2 += monthInfo.LeapModifier;
			}
			Dictionary<int, List<PlotPoint>> dictionary = new Dictionary<int, List<PlotPoint>>();
			foreach (PlotPoint current in Session.Project.AllPlotPoints)
			{
				if (current.Date != null && !(current.Date.CalendarID != this.fCalendar.ID) && !(current.Date.MonthID != monthInfo.ID) && current.Date.Year == this.fYear)
				{
					if (!dictionary.ContainsKey(current.Date.DayIndex))
					{
						dictionary[current.Date.DayIndex] = new List<PlotPoint>();
					}
					dictionary[current.Date.DayIndex].Add(current);
				}
			}
			for (int num3 = 0; num3 != num2; num3++)
			{
				int num4 = num3 + 1;
				int num5 = this.get_days_so_far() + num3;
				string text = "";
				string text2 = "";
				foreach (Satellite current2 in this.fCalendar.Satellites)
				{
					if (current2.Period != 0)
					{
						int num6 = (num5 - current2.Offset) % current2.Period;
						if (num6 < 0)
						{
							num6 += current2.Period;
						}
						if (num6 == 0)
						{
							text2 += "●";
						}
						if (num6 == current2.Period / 2)
						{
							text2 += "○";
						}
					}
				}
				foreach (CalendarEvent current3 in this.fCalendar.Seasons)
				{
					if (current3.MonthID == monthInfo.ID && current3.DayIndex == num3)
					{
						if (text != "")
						{
							text += Environment.NewLine;
						}
						text = text + "Start of " + current3.Name;
					}
				}
				foreach (CalendarEvent current4 in this.fCalendar.Events)
				{
					if (current4.MonthID == monthInfo.ID && current4.DayIndex == num3)
					{
						if (text != "")
						{
							text += Environment.NewLine;
						}
						text += current4.Name;
					}
				}
				if (dictionary.ContainsKey(num3))
				{
					foreach (PlotPoint current5 in dictionary[num3])
					{
						if (text != "")
						{
							text += Environment.NewLine;
						}
						text += current5.Name;
					}
				}
				RectangleF rectangleF = this.get_rect(num3);
				e.Graphics.FillRectangle(SystemBrushes.Window, rectangleF);
				RectangleF layoutRectangle2 = new RectangleF(rectangleF.X, rectangleF.Y, 25f, 20f);
				e.Graphics.DrawString(num4.ToString(), this.Font, SystemBrushes.WindowText, layoutRectangle2, this.fCentred);
				e.Graphics.DrawRectangle(Pens.Gray, layoutRectangle2.X, layoutRectangle2.Y, layoutRectangle2.Width, layoutRectangle2.Height);
				if (text2 != "")
				{
					e.Graphics.DrawString(text2, this.Font, SystemBrushes.WindowText, rectangleF, this.fTopRight);
				}
				if (text != "")
				{
					RectangleF layoutRectangle3 = new RectangleF(rectangleF.X, layoutRectangle2.Bottom, rectangleF.Width, rectangleF.Bottom - layoutRectangle2.Bottom);
					e.Graphics.DrawString(text, this.Font, SystemBrushes.WindowText, layoutRectangle3, this.fCentred);
				}
				e.Graphics.DrawRectangle(SystemPens.ControlDark, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
			}
		}

		private void analyse_month()
		{
			this.fWeeks = 0;
			this.fDayOffset = 0;
			if (this.fCalendar == null)
			{
				return;
			}
			int days_so_far = this.get_days_so_far();
			this.fDayOffset = days_so_far % this.fCalendar.Days.Count;
			if (this.fDayOffset < 0)
			{
				this.fDayOffset += this.fCalendar.Days.Count;
			}
			MonthInfo monthInfo = this.fCalendar.Months[this.fMonthIndex];
			int num = monthInfo.DayCount + this.fDayOffset;
			if (monthInfo.LeapModifier != 0 && monthInfo.LeapPeriod != 0 && this.fYear % monthInfo.LeapPeriod == 0)
			{
				num += monthInfo.LeapModifier;
			}
			this.fWeeks = num / this.fCalendar.Days.Count;
			int num2 = num % this.fCalendar.Days.Count;
			if (num2 != 0)
			{
				this.fWeeks++;
			}
		}

		private int get_days_so_far()
		{
			int num = 0;
			int num2 = Math.Min(this.fYear, this.fCalendar.CampaignYear);
			int num3 = Math.Max(this.fYear, this.fCalendar.CampaignYear);
			for (int num4 = num2; num4 != num3; num4++)
			{
				num += this.fCalendar.DayCount(num4);
			}
			if (this.fYear < this.fCalendar.CampaignYear)
			{
				num = -num;
			}
			for (int num5 = 0; num5 != this.fMonthIndex; num5++)
			{
				MonthInfo monthInfo = this.fCalendar.Months[num5];
				num += monthInfo.DayCount;
			}
			return num;
		}

		private RectangleF get_rect(int day_index)
		{
			int num = this.fDayOffset + day_index;
			int day = num % this.fCalendar.Days.Count;
			int week = num / this.fCalendar.Days.Count;
			return this.get_rect(day, week);
		}

		private RectangleF get_rect(int day, int week)
		{
			float num = 25f;
			float num2 = (float)base.ClientRectangle.Width / (float)this.fCalendar.Days.Count;
			float num3 = ((float)base.ClientRectangle.Height - num) / (float)this.fWeeks;
			if (week == -1)
			{
				return new RectangleF((float)day * num2, 0f, num2, num);
			}
			return new RectangleF((float)day * num2, (float)week * num3 + num, num2, num3);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			base.AutoScaleMode = AutoScaleMode.Font;
		}
	}
}
