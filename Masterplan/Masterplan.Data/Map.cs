using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Map
	{
		private string fName = "";

		private Guid fID = Guid.NewGuid();

		private string fCategory = "";

		private List<TileData> fTiles = new List<TileData>();

		private List<MapArea> fAreas = new List<MapArea>();

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

		public string Category
		{
			get
			{
				return this.fCategory;
			}
			set
			{
				this.fCategory = value;
			}
		}

		public List<TileData> Tiles
		{
			get
			{
				return this.fTiles;
			}
			set
			{
				this.fTiles = value;
			}
		}

		public List<MapArea> Areas
		{
			get
			{
				return this.fAreas;
			}
			set
			{
				this.fAreas = value;
			}
		}

		public MapArea FindArea(Guid area_id)
		{
			foreach (MapArea current in this.fAreas)
			{
				if (current.ID == area_id)
				{
					return current;
				}
			}
			return null;
		}

		public override string ToString()
		{
			return this.fName;
		}

		public Map Copy()
		{
			Map map = new Map();
			map.Name = this.fName;
			map.ID = this.fID;
			map.Category = this.fCategory;
			foreach (TileData current in this.fTiles)
			{
				map.Tiles.Add(current.Copy());
			}
			foreach (MapArea current2 in this.fAreas)
			{
				map.Areas.Add(current2.Copy());
			}
			return map;
		}
	}
}
