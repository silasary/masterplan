using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class TreasureIssue : IIssue
	{
		private string fName = "";

		private Plot fPlot;

		public string PlotName
		{
			get
			{
				return this.fName;
			}
		}

		public Plot Plot
		{
			get
			{
				return this.fPlot;
			}
		}

		public string Reason
		{
			get
			{
				int num = 0;
				int num2 = 0;
				foreach (PlotPoint current in this.fPlot.Points)
				{
					num += current.GetXP();
					List<PlotPoint> subtree = current.Subtree;
					foreach (PlotPoint current2 in subtree)
					{
						num2 += current2.Parcels.Count;
					}
				}
				int num3 = Experience.GetHeroXP(Session.Project.Party.Level);
				num3 += num / Session.Project.Party.Size;
				int heroLevel = Experience.GetHeroLevel(num3);
				int num4 = heroLevel - Session.Project.Party.Level;
				int num5 = num3 - Experience.GetHeroXP(heroLevel);
				int num6 = Experience.GetHeroXP(heroLevel + 1) - Experience.GetHeroXP(heroLevel);
				if (num6 == 0)
				{
					return "";
				}
				int num7 = 10 + (Session.Project.Party.Size - 5);
				int num8 = num7 * num4;
				num8 += num5 * num7 / num6;
				int num9 = (int)((double)num8 * 0.3);
				int num10 = num8 + num9;
				int num11 = num8 - num9;
				string text = "";
				if (num2 < num11)
				{
					text = "Too few treasure parcels are available, compared to the amount of XP given.";
				}
				if (num2 > num10)
				{
					text = "Too many treasure parcels are available, compared to the amount of XP given.";
				}
				if (text != "")
				{
					bool flag = false;
					foreach (PlotPoint current3 in this.fPlot.Points)
					{
						if (current3.Subplot.Points.Count != 0)
						{
							flag = true;
							break;
						}
					}
					text += Environment.NewLine;
					text += "This plot";
					if (flag)
					{
						text += " (and its subplots)";
					}
					text += " should contain ";
					if (num11 == num10)
					{
						text += num10.ToString();
					}
					else
					{
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							num11,
							" - ",
							num10
						});
					}
					object obj2 = text;
					text = string.Concat(new object[]
					{
						obj2,
						" parcels; currently ",
						num2,
						" are available."
					});
				}
				return text;
			}
		}

		public TreasureIssue(string name, Plot plot)
		{
			this.fName = name;
			this.fPlot = plot;
		}

		public override string ToString()
		{
			return this.fName + ": " + this.Reason;
		}
	}
}
