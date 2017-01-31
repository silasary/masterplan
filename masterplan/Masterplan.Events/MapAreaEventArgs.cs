using Masterplan.Data;
using System;

namespace Masterplan.Events
{
	public class MapAreaEventArgs : EventArgs
	{
		private MapArea fArea;

		public MapArea MapArea
		{
			get
			{
				return this.fArea;
			}
		}

		public MapAreaEventArgs(MapArea area)
		{
			this.fArea = area;
		}
	}
}
