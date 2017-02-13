using System;

namespace Masterplan.Data
{
	[Serializable]
	public class MapElement : IElement
	{
		private Guid fMapID = Guid.Empty;

		private Guid fMapAreaID = Guid.Empty;

		public Guid MapID
		{
			get
			{
				return this.fMapID;
			}
			set
			{
				this.fMapID = value;
			}
		}

		public Guid MapAreaID
		{
			get
			{
				return this.fMapAreaID;
			}
			set
			{
				this.fMapAreaID = value;
			}
		}

		public MapElement()
		{
		}

		public MapElement(Guid map_id, Guid map_area_id)
		{
			this.fMapID = map_id;
			this.fMapAreaID = map_area_id;
		}

		public int GetXP()
		{
			return 0;
		}

		public Difficulty GetDifficulty(int party_level, int party_size)
		{
			return Difficulty.Moderate;
		}

		public IElement Copy()
		{
			return new MapElement
			{
				MapID = this.fMapID,
				MapAreaID = this.fMapAreaID
			};
		}
	}
}
