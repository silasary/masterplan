using Masterplan.Data;
using System;

namespace Masterplan.Events
{
	public class MapSketchEventArgs : EventArgs
	{
		private MapSketch fSketch;

		public MapSketch Sketch
		{
			get
			{
				return this.fSketch;
			}
		}

		public MapSketchEventArgs(MapSketch sketch)
		{
			this.fSketch = sketch;
		}
	}
}
