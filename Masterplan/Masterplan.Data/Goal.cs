using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Goal
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fDetails = "";

		private List<Goal> fPrerequisites = new List<Goal>();

		public Guid ID
		{
			get
			{
				return this.fID;
			}
			set
			{
				this.fID = value;
			}
		}

		public string Name
		{
			get
			{
				return this.fName;
			}
			set
			{
				this.fName = value;
			}
		}

		public string Details
		{
			get
			{
				return this.fDetails;
			}
			set
			{
				this.fDetails = value;
			}
		}

		public List<Goal> Prerequisites
		{
			get
			{
				return this.fPrerequisites;
			}
			set
			{
				this.fPrerequisites = value;
			}
		}

		public List<Goal> Subtree
		{
			get
			{
				List<Goal> list = new List<Goal>();
				list.Add(this);
				foreach (Goal current in this.fPrerequisites)
				{
					list.AddRange(current.Subtree);
				}
				return list;
			}
		}

		public Goal()
		{
		}

		public Goal(string name)
		{
			this.fName = name;
		}

		public override string ToString()
		{
			return this.fName;
		}

		public Goal Copy()
		{
			Goal goal = new Goal();
			goal.ID = this.fID;
			goal.Name = this.fName;
			goal.Details = this.fDetails;
			foreach (Goal current in this.fPrerequisites)
			{
				goal.Prerequisites.Add(current.Copy());
			}
			return goal;
		}
	}
}
