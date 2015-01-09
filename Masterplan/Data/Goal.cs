using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	/// <summary>
	/// Class to hold a set of party goals.
	/// </summary>
	[Serializable]
	public class PartyGoals
	{
		/// <summary>
		/// The list of goals.
		/// </summary>
		public List<Goal> Goals
		{
			get { return fGoals; }
			set { fGoals = value; }
		}
		List<Goal> fGoals = new List<Goal>();

		/// <summary>
		/// Returns the containing list for the given goal.
		/// </summary>
		/// <param name="goal">The goal to search for.</param>
		/// <returns>Returns the list containing the goal.</returns>
		public List<Goal> FindList(Goal goal)
		{
			return find_list(goal, fGoals);
		}

		List<Goal> find_list(Goal target, List<Goal> list)
		{
			if (list.Contains(target))
				return list;

			foreach (Goal g in list)
			{
				List<Goal> parent = find_list(target, g.Prerequisites);
				if (parent != null)
					return parent;
			}

			return null;
		}

		/// <summary>
		/// Creates a copy.
		/// </summary>
		/// <returns>Returns the copy.</returns>
		public PartyGoals Copy()
		{
			PartyGoals pg = new PartyGoals();

			foreach (Goal goal in fGoals)
				pg.Goals.Add(goal.Copy());

			return pg;
		}
	}

	/// <summary>
	/// Class representing a party goal.
	/// </summary>
	[Serializable]
	public class Goal
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Goal()
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="name">The goal's name.</param>
		public Goal(string name)
		{
			fName = name;
		}

		/// <summary>
		/// Gets or sets the goal's unique ID.
		/// </summary>
		public Guid ID
		{
			get { return fID; }
			set { fID = value; }
		}
		Guid fID = Guid.NewGuid();

		/// <summary>
		/// Gets or sets the name of the goal.
		/// </summary>
		public string Name
		{
			get { return fName; }
			set { fName = value; }
		}
		string fName = "";

		/// <summary>
		/// Gets or sets the goal's details.
		/// </summary>
		public string Details
		{
			get { return fDetails; }
			set { fDetails = value; }
		}
		string fDetails = "";

		/// <summary>
		/// Gets or sets the goal's prerequisite goals.
		/// </summary>
		public List<Goal> Prerequisites
		{
			get { return fPrerequisites; }
			set { fPrerequisites = value; }
		}
		List<Goal> fPrerequisites = new List<Goal>();

		/// <summary>
		/// Gets the list of goals in this subtree.
		/// </summary>
		public List<Goal> Subtree
		{
			get
			{
				List<Goal> subtree = new List<Goal>();

				subtree.Add(this);

				foreach (Goal goal in fPrerequisites)
					subtree.AddRange(goal.Subtree);

				return subtree;
			}
		}

		/// <summary>
		/// Returns the name of the goal.
		/// </summary>
		/// <returns>Returns the name of the goal.</returns>
		public override string ToString()
		{
			return fName;
		}

		/// <summary>
		/// Creates a copy of the goal.
		/// </summary>
		/// <returns>Returns the copy.</returns>
		public Goal Copy()
		{
			Goal g = new Goal();

			g.ID = fID;
			g.Name = fName;
			g.Details = fDetails;

			foreach (Goal subgoal in fPrerequisites)
				g.Prerequisites.Add(subgoal.Copy());

			return g;
		}
	}
}
