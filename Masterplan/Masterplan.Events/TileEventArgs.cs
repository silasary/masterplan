using Masterplan.Data;
using System;

namespace Masterplan.Events
{
	public class TileEventArgs : EventArgs
	{
		private TileData fTile;

		public TileData Tile
		{
			get
			{
				return this.fTile;
			}
		}

		public TileEventArgs(TileData tile)
		{
			this.fTile = tile;
		}
	}
}
