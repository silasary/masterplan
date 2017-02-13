using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Controls
{
	internal class MapData
	{
		public Dictionary<TileData, Tile> Tiles = new Dictionary<TileData, Tile>();

		public Dictionary<TileData, Rectangle> TileSquares = new Dictionary<TileData, Rectangle>();

		public Dictionary<TileData, RectangleF> TileRegions = new Dictionary<TileData, RectangleF>();

		public double ScalingFactor;

		public float SquareSize;

		public SizeF MapOffset = default(SizeF);

		public int MinX = 2147483647;

		public int MinY = 2147483647;

		public int MaxX = int.MinValue;

		public int MaxY = int.MinValue;

		public int Width
		{
			get
			{
				return this.MaxX - this.MinX + 1;
			}
		}

		public int Height
		{
			get
			{
				return this.MaxY - this.MinY + 1;
			}
		}

		public MapData(MapView mapview, double scaling_factor)
		{
			this.ScalingFactor = scaling_factor;
			if ((mapview.Map != null && mapview.Map.Tiles.Count != 0) || (mapview.BackgroundMap != null && mapview.BackgroundMap.Tiles.Count != 0))
			{
				List<TileData> list = new List<TileData>();
				list.AddRange(mapview.Map.Tiles);
				if (mapview.BackgroundMap != null)
				{
					list.AddRange(mapview.BackgroundMap.Tiles);
				}
				using (List<TileData>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TileData current = enumerator.Current;
						Tile tile = Session.FindTile(current.TileID, SearchType.Global);
						if (tile != null)
						{
							this.Tiles[current] = tile;
							Rectangle value;
							if (current.Rotations % 2 == 0)
							{
								value = new Rectangle(current.Location.X, current.Location.Y, tile.Size.Width, tile.Size.Height);
							}
							else
							{
								value = new Rectangle(current.Location.X, current.Location.Y, tile.Size.Height, tile.Size.Width);
							}
							this.TileSquares[current] = value;
							if (value.X < this.MinX)
							{
								this.MinX = value.X;
							}
							if (value.Y < this.MinY)
							{
								this.MinY = value.Y;
							}
							int num = value.X + value.Width - 1;
							if (num > this.MaxX)
							{
								this.MaxX = num;
							}
							int num2 = value.Y + value.Height - 1;
							if (num2 > this.MaxY)
							{
								this.MaxY = num2;
							}
						}
					}
					goto IL_25E;
				}
			}
			this.MinX = 0;
			this.MinY = 0;
			this.MaxX = 0;
			this.MaxY = 0;
			IL_25E:
			if (mapview.Map != null && mapview.Viewpoint != Rectangle.Empty)
			{
				this.MinX = mapview.Viewpoint.X;
				this.MinY = mapview.Viewpoint.Y;
				this.MaxX = mapview.Viewpoint.X + mapview.Viewpoint.Width - 1;
				this.MaxY = mapview.Viewpoint.Y + mapview.Viewpoint.Height - 1;
			}
			else
			{
				this.MinX -= mapview.BorderSize;
				this.MinY -= mapview.BorderSize;
				this.MaxX += mapview.BorderSize;
				this.MaxY += mapview.BorderSize;
			}
			float val = (float)mapview.ClientRectangle.Width / (float)this.Width;
			float val2 = (float)mapview.ClientRectangle.Height / (float)this.Height;
			this.SquareSize = Math.Min(val, val2);
			this.SquareSize *= (float)this.ScalingFactor;
			float num3 = (float)this.Width * this.SquareSize;
			float num4 = (float)this.Height * this.SquareSize;
			float num5 = (float)mapview.ClientRectangle.Width - num3;
			float num6 = (float)mapview.ClientRectangle.Height - num4;
			this.MapOffset = new SizeF(num5 / 2f, num6 / 2f);
			if (mapview.Map != null)
			{
				foreach (TileData current2 in this.Tiles.Keys)
				{
					Rectangle rectangle = this.TileSquares[current2];
					this.TileRegions[current2] = this.GetRegion(rectangle.Location, rectangle.Size);
				}
			}
		}

		~MapData()
		{
			this.Tiles.Clear();
			this.TileSquares.Clear();
			this.TileRegions.Clear();
		}

		public Point GetSquareAtPoint(Point pt)
		{
			int num = (int)((float)pt.X - this.MapOffset.Width);
			int num2 = (int)((float)pt.Y - this.MapOffset.Height);
			num = (int)((float)num / this.SquareSize);
			num2 = (int)((float)num2 / this.SquareSize);
			return new Point(num + this.MinX, num2 + this.MinY);
		}

		public TileData GetTileAtSquare(Point square)
		{
			TileData result = null;
			foreach (TileData current in this.TileSquares.Keys)
			{
				if (this.TileSquares[current].Contains(square))
				{
					result = current;
				}
			}
			return result;
		}

		public RectangleF GetRegion(Point square, Size size)
		{
			float x = (float)(square.X - this.MinX) * this.SquareSize + this.MapOffset.Width;
			float y = (float)(square.Y - this.MinY) * this.SquareSize + this.MapOffset.Height;
			float num = (float)size.Width * this.SquareSize;
			float num2 = (float)size.Height * this.SquareSize;
			return new RectangleF(x, y, num + 1f, num2 + 1f);
		}
	}
}
