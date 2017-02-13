using Masterplan.Tools;
using System;

namespace Masterplan.Data
{
	[Serializable]
	public class CreatureIssue : IIssue
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
				Encounter encounter = this.fPoint.Element as Encounter;
				if (encounter == null)
				{
					return "";
				}
				int partyLevel = Workspace.GetPartyLevel(this.fPoint);
				foreach (EncounterSlot current in encounter.Slots)
				{
					int num = current.Card.Level - partyLevel;
					if (num < -4)
					{
						string result = current.Card.Title + " is more than four levels lower than the party level.";
						return result;
					}
					if (num > 5)
					{
						string result = current.Card.Title + " is more than five levels higher than the party level.";
						return result;
					}
				}
				return "";
			}
		}

		public CreatureIssue(PlotPoint point)
		{
			this.fPoint = point;
		}

		public override string ToString()
		{
			return this.fPoint.Name + ": " + this.Reason;
		}
	}
}
