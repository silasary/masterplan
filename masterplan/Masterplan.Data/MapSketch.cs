using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class MapSketch
	{
		private Color fColour = Color.Black;

		private int fWidth = 3;

		private List<MapSketchPoint> fPoints = new List<MapSketchPoint>();

		public Color Colour
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

		public int Width
		{
			get
			{
				return this.fWidth;
			}
			set
			{
				this.fWidth = value;
			}
		}

		public List<MapSketchPoint> Points
		{
			get
			{
				return this.fPoints;
			}
		}

		public MapSketch Copy()
		{
			MapSketch mapSketch = new MapSketch();
			mapSketch.Colour = this.fColour;
			mapSketch.Width = this.fWidth;
			foreach (MapSketchPoint current in this.fPoints)
			{
				mapSketch.Points.Add(current.Copy());
			}
			return mapSketch;
		}
	}
}
