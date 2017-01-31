using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class PlotPoint
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private PlotPointState fState;

		private PlotPointColour fColour;

		private string fDetails = "";

		private string fReadAloud = "";

		private List<Guid> fLinks = new List<Guid>();

		private Plot fSubplot = new Plot();

		private IElement fElement;

		private List<Parcel> fParcels = new List<Parcel>();

		private List<Guid> fEncyclopediaEntries = new List<Guid>();

		private CalendarDate fDate;

		private Guid fRegionalMapID = Guid.Empty;

		private Guid fMapLocationID = Guid.Empty;

		private int fAdditionalXP;

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

		public PlotPointState State
		{
			get
			{
				return this.fState;
			}
			set
			{
				this.fState = value;
			}
		}

		public PlotPointColour Colour
		{
			get
			{
				return this.fColour;
			}
			set
			{
				this.fColour = value;
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

		public string ReadAloud
		{
			get
			{
				return this.fReadAloud;
			}
			set
			{
				this.fReadAloud = value;
			}
		}

		public List<Guid> Links
		{
			get
			{
				return this.fLinks;
			}
			set
			{
				this.fLinks = value;
			}
		}

		public Plot Subplot
		{
			get
			{
				return this.fSubplot;
			}
			set
			{
				this.fSubplot = value;
			}
		}

		public IElement Element
		{
			get
			{
				return this.fElement;
			}
			set
			{
				this.fElement = value;
			}
		}

		public List<Parcel> Parcels
		{
			get
			{
				return this.fParcels;
			}
			set
			{
				this.fParcels = value;
			}
		}

		public List<Guid> EncyclopediaEntryIDs
		{
			get
			{
				return this.fEncyclopediaEntries;
			}
			set
			{
				this.fEncyclopediaEntries = value;
			}
		}

		public CalendarDate Date
		{
			get
			{
				return this.fDate;
			}
			set
			{
				this.fDate = value;
			}
		}

		public Guid RegionalMapID
		{
			get
			{
				return this.fRegionalMapID;
			}
			set
			{
				this.fRegionalMapID = value;
			}
		}

		public Guid MapLocationID
		{
			get
			{
				return this.fMapLocationID;
			}
			set
			{
				this.fMapLocationID = value;
			}
		}

		public int AdditionalXP
		{
			get
			{
				return this.fAdditionalXP;
			}
			set
			{
				this.fAdditionalXP = value;
			}
		}

		public List<PlotPoint> Subtree
		{
			get
			{
				List<PlotPoint> list = new List<PlotPoint>();
				list.Add(this);
				foreach (PlotPoint current in this.fSubplot.Points)
				{
					list.AddRange(current.Subtree);
				}
				return list;
			}
		}

		public PlotPoint()
		{
		}

		public PlotPoint(string name)
		{
			this.fName = name;
		}

		public int GetXP()
		{
			int num = this.fAdditionalXP;
			if (this.fElement != null)
			{
				num += this.fElement.GetXP();
			}
			if (this.fSubplot.Points.Count != 0)
			{
				List<List<PlotPoint>> list = Workspace.FindLayers(this.fSubplot);
				foreach (List<PlotPoint> current in list)
				{
					num += Workspace.GetLayerXP(current);
				}
			}
			return num;
		}

		public void GetTacticalMapArea(ref Map map, ref MapArea map_area)
		{
			Guid guid = Guid.Empty;
			Guid guid2 = Guid.Empty;
			Encounter encounter = this.fElement as Encounter;
			if (encounter != null)
			{
				guid = encounter.MapID;
				guid2 = encounter.MapAreaID;
			}
			SkillChallenge skillChallenge = this.fElement as SkillChallenge;
			if (skillChallenge != null)
			{
				guid = skillChallenge.MapID;
				guid2 = skillChallenge.MapAreaID;
			}
			TrapElement trapElement = this.fElement as TrapElement;
			if (trapElement != null)
			{
				guid = trapElement.MapID;
				guid2 = trapElement.MapAreaID;
			}
			MapElement mapElement = this.fElement as MapElement;
			if (mapElement != null)
			{
				guid = mapElement.MapID;
				guid2 = mapElement.MapAreaID;
			}
			if (guid != Guid.Empty && guid2 != Guid.Empty)
			{
				map = Session.Project.FindTacticalMap(guid);
				if (map != null)
				{
					map_area = map.FindArea(guid2);
				}
			}
		}

		public void GetRegionalMapArea(ref RegionalMap map, ref MapLocation map_location, Project project)
		{
			if (this.fRegionalMapID != Guid.Empty && this.fMapLocationID != Guid.Empty)
			{
				map = Session.Project.FindRegionalMap(this.fRegionalMapID);
				if (map != null)
				{
					map_location = map.FindLocation(this.fMapLocationID);
				}
			}
		}

		public override string ToString()
		{
			return this.fName;
		}

		public PlotPoint Copy()
		{
			PlotPoint plotPoint = new PlotPoint();
			plotPoint.ID = this.fID;
			plotPoint.Name = this.fName;
			plotPoint.State = this.fState;
			plotPoint.Colour = this.fColour;
			plotPoint.Details = this.fDetails;
			plotPoint.ReadAloud = this.fReadAloud;
			plotPoint.Links.AddRange(this.fLinks);
			plotPoint.Subplot = this.fSubplot.Copy();
			plotPoint.Element = ((this.fElement != null) ? this.fElement.Copy() : null);
			plotPoint.Date = ((this.fDate != null) ? this.fDate.Copy() : null);
			plotPoint.RegionalMapID = this.fRegionalMapID;
			plotPoint.MapLocationID = this.fMapLocationID;
			plotPoint.AdditionalXP = this.fAdditionalXP;
			foreach (Parcel current in this.fParcels)
			{
				plotPoint.Parcels.Add(current.Copy());
			}
			foreach (Guid current2 in this.fEncyclopediaEntries)
			{
				plotPoint.EncyclopediaEntryIDs.Add(current2);
			}
			return plotPoint;
		}
	}
}
