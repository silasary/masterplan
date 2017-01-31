using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Data
{
	[Serializable]
	public class Plot
	{
		private List<PlotPoint> fPoints = new List<PlotPoint>();

		private PartyGoals fGoals = new PartyGoals();

		private FiveByFiveData f5x5 = new FiveByFiveData();

		public List<PlotPoint> Points
		{
			get
			{
				return this.fPoints;
			}
			set
			{
				this.fPoints = value;
			}
		}

		public PartyGoals Goals
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

		public FiveByFiveData FiveByFive
		{
			get
			{
				return this.f5x5;
			}
			set
			{
				this.f5x5 = value;
			}
		}

		public List<PlotPoint> AllPlotPoints
		{
			get
			{
				List<PlotPoint> list = new List<PlotPoint>();
				foreach (PlotPoint current in this.fPoints)
				{
					list.Add(current);
					list.AddRange(current.Subplot.AllPlotPoints);
				}
				return list;
			}
		}

		public PlotPoint FindPoint(Guid id)
		{
			foreach (PlotPoint current in this.fPoints)
			{
				if (current.ID == id)
				{
					return current;
				}
			}
			return null;
		}

		public void RemovePoint(PlotPoint point)
		{
			new List<Guid>();
			foreach (PlotPoint current in this.fPoints)
			{
				if (current.Links.Contains(point.ID))
				{
					while (current.Links.Contains(point.ID))
					{
						current.Links.Remove(point.ID);
					}
					foreach (Guid current2 in point.Links)
					{
						if (!current.Links.Contains(current2))
						{
							current.Links.Add(current2);
						}
					}
				}
			}
			this.fPoints.Remove(point);
		}

		public List<PlotPoint> FindPrerequisites(Guid point_id)
		{
			List<PlotPoint> list = new List<PlotPoint>();
			foreach (PlotPoint current in this.fPoints)
			{
				if (current.Links.Contains(point_id))
				{
					list.Add(current);
				}
			}
			return list;
		}

		public List<PlotPoint> FindSubtree(PlotPoint pp)
		{
			List<PlotPoint> list = new List<PlotPoint>();
			list.Add(pp);
			foreach (Guid current in pp.Links)
			{
				PlotPoint pp2 = this.FindPoint(current);
				List<PlotPoint> collection = this.FindSubtree(pp2);
				list.AddRange(collection);
			}
			return list;
		}

		public PlotPoint FindPointForMapArea(Map map, MapArea area)
		{
			foreach (PlotPoint current in this.fPoints)
			{
				Map map2 = null;
				MapArea mapArea = null;
				current.GetTacticalMapArea(ref map2, ref mapArea);
				if (map2 == map && mapArea == area)
				{
					return current;
				}
			}
			return null;
		}

		public List<Guid> FindTacticalMaps()
		{
			BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
			foreach (PlotPoint current in this.fPoints)
			{
				if (current.Element != null)
				{
					if (current.Element is Encounter)
					{
						Encounter encounter = current.Element as Encounter;
						if (encounter.MapID != Guid.Empty && encounter.MapAreaID != Guid.Empty)
						{
							binarySearchTree.Add(encounter.MapID);
						}
					}
					if (current.Element is TrapElement)
					{
						TrapElement trapElement = current.Element as TrapElement;
						if (trapElement.MapID != Guid.Empty && trapElement.MapAreaID != Guid.Empty)
						{
							binarySearchTree.Add(trapElement.MapID);
						}
					}
					if (current.Element is SkillChallenge)
					{
						SkillChallenge skillChallenge = current.Element as SkillChallenge;
						if (skillChallenge.MapID != Guid.Empty && skillChallenge.MapAreaID != Guid.Empty)
						{
							binarySearchTree.Add(skillChallenge.MapID);
						}
					}
					if (current.Element is MapElement)
					{
						MapElement mapElement = current.Element as MapElement;
						if (mapElement.MapID != Guid.Empty)
						{
							binarySearchTree.Add(mapElement.MapID);
						}
					}
				}
			}
			List<Guid> sortedList = binarySearchTree.SortedList;
			sortedList.Remove(Guid.Empty);
			return sortedList;
		}

		public List<Guid> FindRegionalMaps()
		{
			BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
			foreach (PlotPoint current in this.fPoints)
			{
				if (current.RegionalMapID != Guid.Empty && current.MapLocationID != Guid.Empty)
				{
					binarySearchTree.Add(current.RegionalMapID);
				}
			}
			List<Guid> sortedList = binarySearchTree.SortedList;
			sortedList.Remove(Guid.Empty);
			return sortedList;
		}

		public Plot Copy()
		{
			Plot plot = new Plot();
			foreach (PlotPoint current in this.fPoints)
			{
				plot.Points.Add(current.Copy());
			}
			plot.Goals = this.fGoals.Copy();
			plot.FiveByFive = this.f5x5.Copy();
			return plot;
		}
	}
}
