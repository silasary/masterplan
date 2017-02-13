using Masterplan.Data;
using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Tools
{
	internal class GoalBuilder
	{
		public static void Build(Plot plot)
		{
			Dictionary<Guid, Pair<PlotPoint, PlotPoint>> map = new Dictionary<Guid, Pair<PlotPoint, PlotPoint>>();
			GoalBuilder.add_points(plot, plot.Goals.Goals, map);
			GoalBuilder.add_links(plot.Goals.Goals, map);
		}

		private static void add_points(Plot plot, List<Goal> goals, Dictionary<Guid, Pair<PlotPoint, PlotPoint>> map)
		{
			foreach (Goal current in goals)
			{
				PlotPoint plotPoint = new PlotPoint("Discover: " + current.Name);
				plotPoint.Details = current.Details;
				PlotPoint plotPoint2 = new PlotPoint("Complete: " + current.Name);
				plotPoint2.Details = current.Details;
				plot.Points.Add(plotPoint);
				plot.Points.Add(plotPoint2);
				map[current.ID] = new Pair<PlotPoint, PlotPoint>(plotPoint, plotPoint2);
				GoalBuilder.add_points(plot, current.Prerequisites, map);
			}
		}

		private static void add_links(List<Goal> goals, Dictionary<Guid, Pair<PlotPoint, PlotPoint>> map)
		{
			foreach (Goal current in goals)
			{
				Pair<PlotPoint, PlotPoint> pair = map[current.ID];
				foreach (Goal current2 in current.Prerequisites)
				{
					Pair<PlotPoint, PlotPoint> pair2 = map[current2.ID];
					pair.First.Links.Add(pair2.First.ID);
					pair2.Second.Links.Add(pair.Second.ID);
				}
				if (current.Prerequisites.Count == 0)
				{
					pair.First.Links.Add(pair.Second.ID);
				}
				GoalBuilder.add_links(current.Prerequisites, map);
			}
		}
	}
}
