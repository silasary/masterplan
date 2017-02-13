using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class RegionalMap
	{
		private string fName = "";

		private Guid fID = Guid.NewGuid();

		private Image fImage;

		private List<MapLocation> fLocations = new List<MapLocation>();

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

		public Image Image
		{
			get
			{
				return this.fImage;
			}
			set
			{
				this.fImage = value;
			}
		}

		public List<MapLocation> Locations
		{
			get
			{
				return this.fLocations;
			}
			set
			{
				this.fLocations = value;
			}
		}

		public MapLocation FindLocation(Guid location_id)
		{
			foreach (MapLocation current in this.fLocations)
			{
				if (current.ID == location_id)
				{
					return current;
				}
			}
			return null;
		}

		public RegionalMap Copy()
		{
			RegionalMap regionalMap = new RegionalMap();
			regionalMap.Name = this.fName;
			regionalMap.ID = this.fID;
			regionalMap.Image = this.fImage;
			foreach (MapLocation current in this.fLocations)
			{
				regionalMap.Locations.Add(current.Copy());
			}
			return regionalMap;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}
