using System;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class TileData
	{
		private Guid fID = Guid.NewGuid();

		private Guid fTileID = Guid.Empty;

		private Point fLocation = new Point(0, 0);

		private int fRotations;

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

		public Guid TileID
		{
			get
			{
				return this.fTileID;
			}
			set
			{
				this.fTileID = value;
			}
		}

		public Point Location
		{
			get
			{
				return this.fLocation;
			}
			set
			{
				this.fLocation = value;
			}
		}

		public int Rotations
		{
			get
			{
				return this.fRotations;
			}
			set
			{
				this.fRotations = value;
				while (this.fRotations < 0)
				{
					this.fRotations += 4;
				}
				this.fRotations %= 4;
			}
		}

		public TileData Copy()
		{
			return new TileData
			{
				ID = this.fID,
				TileID = this.fTileID,
				Location = new Point(this.fLocation.X, this.fLocation.Y),
				Rotations = this.fRotations
			};
		}
	}
}
