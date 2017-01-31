using Masterplan.Data;
using System;
using System.Collections.Generic;

namespace Masterplan.Tools
{
	internal class Workspace
	{
		public static List<List<PlotPoint>> FindLayers(Plot plot)
		{
			List<List<PlotPoint>> list = new List<List<PlotPoint>>();
			List<PlotPoint> list2 = new List<PlotPoint>(plot.Points);
			while (list2.Count > 0)
			{
				List<PlotPoint> list3 = new List<PlotPoint>();
				foreach (PlotPoint current in list2)
				{
					bool flag = true;
					foreach (PlotPoint current2 in list2)
					{
						if (current2 != current && current2.Links.Contains(current.ID))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						list3.Add(current);
					}
				}
				if (list3.Count == 0)
				{
					list3.AddRange(list2);
				}
				list.Add(list3);
				foreach (PlotPoint current3 in list3)
				{
					list2.Remove(current3);
				}
			}
			return list;
		}

		public static int GetTotalXP(PlotPoint pp)
		{
			int num = Session.Project.Party.XP * Session.Project.Party.Size;
			do
			{
				Plot plot = Session.Project.FindParent(pp);
				if (plot == null)
				{
					break;
				}
				List<List<PlotPoint>> list = Workspace.FindLayers(plot);
				foreach (List<PlotPoint> current in list)
				{
					bool flag = false;
					foreach (PlotPoint current2 in current)
					{
						if (current2.ID == pp.ID)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						break;
					}
					int layerXP = Workspace.GetLayerXP(current);
					num += layerXP;
				}
				pp = Session.Project.FindParent(plot);
			}
			while (pp != null);
			return num;
		}

		public static int GetLayerXP(List<PlotPoint> layer)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			foreach (PlotPoint current in layer)
			{
				if (current != null)
				{
					switch (current.State)
					{
					case PlotPointState.Normal:
						num2 += current.GetXP();
						num3++;
						break;
					case PlotPointState.Completed:
						num += current.GetXP();
						break;
					}
				}
			}
			int num4 = num2;
			if (!Session.Preferences.AllXP)
			{
				num4 = ((num3 != 0) ? (num2 / num3) : 0);
			}
			return num + num4;
		}

		public static int GetPartyLevel(PlotPoint pp)
		{
			int totalXP = Workspace.GetTotalXP(pp);
			int xp = totalXP / Session.Project.Party.Size;
			return Experience.GetHeroLevel(xp);
		}
	}
}
