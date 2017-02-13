using System;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class MapSketchPoint
	{
		private Point fSquare = new Point(0, 0);

		private PointF fLocation = new PointF(0f, 0f);

		public Point Square
		{
			get
			{
				return this.fSquare;
			}
			set
			{
				this.fSquare = value;
			}
		}

		public PointF Location
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

		public MapSketchPoint Copy()
		{
			return new MapSketchPoint
			{
				Square = new Point(this.fSquare.X, this.fSquare.Y),
				Location = new PointF(this.fLocation.X, this.fLocation.Y)
			};
		}
	}
}
