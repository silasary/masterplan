using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class PartyGoals
	{
		private List<Goal> fGoals = new List<Goal>();

		public List<Goal> Goals
		{
			get
			{
				return this.fGoals;
			}
			set
			{
				this.fGoals = value;
			}
		}

		public List<Goal> FindList(Goal goal)
		{
			return this.find_list(goal, this.fGoals);
		}

		private List<Goal> find_list(Goal target, List<Goal> list)
		{
			if (list.Contains(target))
			{
				return list;
			}
			foreach (Goal current in list)
			{
				List<Goal> list2 = this.find_list(target, current.Prerequisites);
				if (list2 != null)
				{
					return list2;
				}
			}
			return null;
		}

		public PartyGoals Copy()
		{
			PartyGoals partyGoals = new PartyGoals();
			foreach (Goal current in this.fGoals)
			{
				partyGoals.Goals.Add(current.Copy());
			}
			return partyGoals;
		}
	}
}
