using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class IssuesForm : Form
	{
		private IContainer components;

		private WebBrowser Browser;

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
			this.Browser = new WebBrowser();
			base.SuspendLayout();
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.IsWebBrowserContextMenuEnabled = false;
			this.Browser.Location = new Point(0, 0);
			this.Browser.Name = "Browser";
			this.Browser.ScriptErrorsSuppressed = true;
			this.Browser.Size = new Size(468, 291);
			this.Browser.TabIndex = 2;
			this.Browser.WebBrowserShortcutsEnabled = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(468, 291);
			base.Controls.Add(this.Browser);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "IssuesForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Plot Design Issues";
			base.ResumeLayout(false);
		}

		public IssuesForm(Plot plot)
		{
			this.InitializeComponent();
			List<PlotPoint> allPlotPoints = plot.AllPlotPoints;
			List<string> list = new List<string>();
			list.AddRange(HTML.GetHead("Plot Design Issues", "", DisplaySize.Small));
			list.Add("<BODY>");
			List<DifficultyIssue> list2 = new List<DifficultyIssue>();
			foreach (PlotPoint current in allPlotPoints)
			{
				DifficultyIssue difficultyIssue = new DifficultyIssue(current);
				if (difficultyIssue.Reason != "")
				{
					list2.Add(difficultyIssue);
				}
			}
			list.Add("<H4>Difficulty Issues</H4>");
			if (list2.Count != 0)
			{
				using (List<DifficultyIssue>.Enumerator enumerator2 = list2.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						DifficultyIssue current2 = enumerator2.Current;
						list.Add("<P>");
						list.Add(string.Concat(new object[]
						{
							"<B>",
							current2.Point,
							"</B>: ",
							current2.Reason
						}));
						list.Add("</P>");
					}
					goto IL_141;
				}
			}
			list.Add("<P class=instruction>");
			list.Add("(none)");
			list.Add("</P>");
			IL_141:
			list.Add("<HR>");
			List<CreatureIssue> list3 = new List<CreatureIssue>();
			foreach (PlotPoint current3 in allPlotPoints)
			{
				if (current3.Element is Encounter)
				{
					CreatureIssue creatureIssue = new CreatureIssue(current3);
					if (creatureIssue.Reason != "")
					{
						list3.Add(creatureIssue);
					}
				}
			}
			list.Add("<H4>Creature Choice Issues</H4>");
			if (list2.Count != 0)
			{
				using (List<CreatureIssue>.Enumerator enumerator4 = list3.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						CreatureIssue current4 = enumerator4.Current;
						list.Add("<P>");
						list.Add(string.Concat(new object[]
						{
							"<B>",
							current4.Point,
							"</B>: ",
							current4.Reason
						}));
						list.Add("</P>");
					}
					goto IL_269;
				}
			}
			list.Add("<P class=instruction>");
			list.Add("(none)");
			list.Add("</P>");
			IL_269:
			list.Add("<HR>");
			List<SkillIssue> list4 = new List<SkillIssue>();
			foreach (PlotPoint current5 in allPlotPoints)
			{
				if (current5.Element is SkillChallenge)
				{
					SkillIssue skillIssue = new SkillIssue(current5);
					if (skillIssue.Reason != "")
					{
						list4.Add(skillIssue);
					}
				}
			}
			list.Add("<H4>Undefined Skill Challenges</H4>");
			if (list4.Count != 0)
			{
				using (List<SkillIssue>.Enumerator enumerator6 = list4.GetEnumerator())
				{
					while (enumerator6.MoveNext())
					{
						SkillIssue current6 = enumerator6.Current;
						list.Add("<P>");
						list.Add(string.Concat(new object[]
						{
							"<B>",
							current6.Point,
							"</B>: ",
							current6.Reason
						}));
						list.Add("</P>");
					}
					goto IL_392;
				}
			}
			list.Add("<P class=instruction>");
			list.Add("(none)");
			list.Add("</P>");
			IL_392:
			list.Add("<HR>");
			List<ParcelIssue> list5 = new List<ParcelIssue>();
			foreach (PlotPoint current7 in allPlotPoints)
			{
				foreach (Parcel current8 in current7.Parcels)
				{
					if (current8.Name == "")
					{
						ParcelIssue item = new ParcelIssue(current8, current7);
						list5.Add(item);
					}
				}
			}
			list.Add("<H4>Undefined Treasure Parcels</H4>");
			if (list5.Count != 0)
			{
				using (List<ParcelIssue>.Enumerator enumerator9 = list5.GetEnumerator())
				{
					while (enumerator9.MoveNext())
					{
						ParcelIssue current9 = enumerator9.Current;
						list.Add("<P>");
						list.Add(string.Concat(new object[]
						{
							"<B>",
							current9.Point,
							"</B>: ",
							current9.Reason
						}));
						list.Add("</P>");
					}
					goto IL_4E1;
				}
			}
			list.Add("<P class=instruction>");
			list.Add("(none)");
			list.Add("</P>");
			IL_4E1:
			list.Add("<HR>");
			List<TreasureIssue> list6 = new List<TreasureIssue>();
			PlotPoint plotPoint = Session.Project.FindParent(plot);
			string plotname = (plotPoint != null) ? plotPoint.Name : Session.Project.Name;
			this.add_treasure_issues(plotname, plot, list6);
			list.Add("<H4>Treasure Allocation Issues</H4>");
			if (list6.Count != 0)
			{
				using (List<TreasureIssue>.Enumerator enumerator10 = list6.GetEnumerator())
				{
					while (enumerator10.MoveNext())
					{
						TreasureIssue current10 = enumerator10.Current;
						list.Add("<P>");
						list.Add("<B>" + current10.PlotName + "</B>: " + current10.Reason);
						list.Add("</P>");
					}
					goto IL_5BF;
				}
			}
			list.Add("<P class=instruction>");
			list.Add("(none)");
			list.Add("</P>");
			IL_5BF:
			list.Add("</BODY>");
			list.Add("</HTML>");
			this.Browser.DocumentText = HTML.Concatenate(list);
		}

		private void add_treasure_issues(string plotname, Plot plot, List<TreasureIssue> treasure_issues)
		{
			TreasureIssue treasureIssue = new TreasureIssue(plotname, plot);
			if (treasureIssue.Reason != "")
			{
				treasure_issues.Add(treasureIssue);
				foreach (PlotPoint current in plot.Points)
				{
					this.add_treasure_issues(current.Name, current.Subplot, treasure_issues);
				}
			}
		}
	}
}
