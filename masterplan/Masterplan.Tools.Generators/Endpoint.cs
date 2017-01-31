using Masterplan.Data;
using System;
using System.Drawing;

namespace Masterplan.Tools.Generators
{
	internal class Endpoint
	{
		public TileCategory Category;

		public Direction Direction;

		public Point TopLeft = Point.Empty;

		public Point BottomRight = Point.Empty;

		public int Size
		{
			get
			{
				int val = this.BottomRight.X - this.TopLeft.X;
				int val2 = this.BottomRight.Y - this.TopLeft.Y;
				return Math.Max(val, val2);
			}
		}

		public Orientation Orientation
		{
			get
			{
				if (this.TopLeft.X == this.BottomRight.X)
				{
					return Orientation.NorthSouth;
				}
				return Orientation.EastWest;
			}
		}
	}
}
