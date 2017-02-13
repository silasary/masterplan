using Masterplan.Data;
using System;

namespace Masterplan.Tools
{
	internal class FiveByFive
	{
		public static void Build(Plot plot)
		{
			foreach (FiveByFiveColumn current in plot.FiveByFive.Columns)
			{
				PlotPoint plotPoint = null;
				foreach (FiveByFiveItem current2 in current.Items)
				{
					PlotPoint plotPoint2 = new PlotPoint(current2.Details);
					plotPoint2.Details = current2.Details;
					plotPoint2.Colour = current.Colour;
					plot.Points.Add(plotPoint2);
					if (plotPoint != null)
					{
						plotPoint.Links.Add(plotPoint2.ID);
					}
					plotPoint = plotPoint2;
				}
			}
		}
	}
}
