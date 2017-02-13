using Masterplan.Tools;
using System;

namespace Masterplan.Data
{
	[Serializable]
	public class DifficultyIssue : IIssue
	{
		private PlotPoint fPoint;

		public PlotPoint Point
		{
			get
			{
				return this.fPoint;
			}
		}

		public string Reason
		{
			get
			{
				if (this.fPoint.State != PlotPointState.Normal)
				{
					return "";
				}
				if (this.fPoint.Element == null)
				{
					return "";
				}
				string text = "game element";
				if (this.fPoint.Element is Encounter)
				{
					text = "encounter";
				}
				if (this.fPoint.Element is TrapElement)
				{
					TrapElement trapElement = this.fPoint.Element as TrapElement;
					text = ((trapElement.Trap.Type == TrapType.Trap) ? "trap" : "hazard");
				}
				if (this.fPoint.Element is SkillChallenge)
				{
					text = "skill challenge";
				}
				if (this.fPoint.Element is Quest)
				{
					text = "quest";
				}
				int partyLevel = Workspace.GetPartyLevel(this.fPoint);
				Difficulty difficulty = this.fPoint.Element.GetDifficulty(partyLevel, Session.Project.Party.Size);
				Difficulty difficulty2 = difficulty;
				if (difficulty2 == Difficulty.Trivial)
				{
					return string.Concat(new object[]
					{
						"This ",
						text,
						" is too easy for a party of level ",
						partyLevel,
						"."
					});
				}
				if (difficulty2 != Difficulty.Extreme)
				{
					return "";
				}
				return string.Concat(new object[]
				{
					"This ",
					text,
					" is too difficult for a party of level ",
					partyLevel,
					"."
				});
			}
		}

		public DifficultyIssue(PlotPoint point)
		{
			this.fPoint = point;
		}

		public override string ToString()
		{
			return this.fPoint.Name + ": " + this.Reason;
		}
	}
}
