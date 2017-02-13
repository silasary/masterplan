using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Tools
{
	internal class Screenshot
	{
		public static Bitmap Plot(Plot plot, Size size)
		{
			PlotView plotView = new PlotView();
			plotView.Plot = plot;
			plotView.Mode = PlotViewMode.Plain;
			plotView.Size = size;
			Bitmap bitmap = new Bitmap(plotView.Width, plotView.Height);
			plotView.DrawToBitmap(bitmap, plotView.ClientRectangle);
			return bitmap;
		}

		public static Bitmap Map(Map map, Rectangle view, Encounter enc, Dictionary<Guid, CombatData> heroes, List<TokenLink> tokens)
		{
			return Screenshot.Map(new MapView
			{
				Map = map,
				Viewpoint = view,
				Mode = MapViewMode.Plain,
				LineOfSight = false,
				Encounter = enc,
				TokenLinks = tokens
			});
		}

		public static Bitmap Map(MapView mapview)
		{
			if (mapview.Viewpoint != Rectangle.Empty)
			{
				mapview.Size = new Size(mapview.Viewpoint.Width * 64, mapview.Viewpoint.Height * 64);
			}
			else
			{
				mapview.Size = new Size(mapview.LayoutData.Width * 64, mapview.LayoutData.Height * 64);
			}
			Bitmap bitmap = new Bitmap(mapview.Width, mapview.Height);
			mapview.DrawToBitmap(bitmap, mapview.ClientRectangle);
			return bitmap;
		}

		public static Bitmap Calendar(Calendar calendar, int month_index, int year, Size size)
		{
			CalendarPanel calendarPanel = new CalendarPanel();
			calendarPanel.Calendar = calendar;
			calendarPanel.MonthIndex = month_index;
			calendarPanel.Year = year;
			calendarPanel.Size = size;
			Bitmap bitmap = new Bitmap(calendarPanel.Width, calendarPanel.Height);
			calendarPanel.DrawToBitmap(bitmap, calendarPanel.ClientRectangle);
			return bitmap;
		}
	}
}
