using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using Utils;

namespace Masterplan.Tools.Generators
{
	internal class MapBuilder
	{
		private static MapBuilderData fData = null;

		private static Map fMap = null;

		private static Dictionary<TileCategory, List<Tile>> fTiles = new Dictionary<TileCategory, List<Tile>>();

		private static List<Tile> fRoomTiles = new List<Tile>();

		private static List<Tile> fCorridorTiles = new List<Tile>();

		private static List<Endpoint> fEndpoints = new List<Endpoint>();

		public static void BuildMap(MapBuilderData data, Map map, EventHandler callback)
		{
			MapBuilder.fData = data;
			MapBuilder.fMap = map;
			MapBuilder.fMap.Tiles.Clear();
			MapBuilder.fMap.Areas.Clear();
			switch (MapBuilder.fData.Type)
			{
			case MapAutoBuildType.Warren:
				MapBuilder.fEndpoints.Clear();
				MapBuilder.build_tile_lists();
				MapBuilder.build_warren(callback);
				return;
			case MapAutoBuildType.FilledArea:
				MapBuilder.build_filled_area(callback);
				return;
			case MapAutoBuildType.Freeform:
				MapBuilder.build_freeform_area(callback);
				return;
			default:
				return;
			}
		}

		private static void build_tile_lists()
		{
			MapBuilder.fTiles.Clear();
			foreach (TileCategory key in Enum.GetValues(typeof(TileCategory)))
			{
				MapBuilder.fTiles[key] = new List<Tile>();
			}
			foreach (Library current in MapBuilder.fData.Libraries)
			{
				foreach (Tile current2 in current.Tiles)
				{
					MapBuilder.fTiles[current2.Category].Add(current2);
				}
			}
			MapBuilder.fRoomTiles.Clear();
			MapBuilder.fCorridorTiles.Clear();
			foreach (Tile current3 in MapBuilder.fTiles[TileCategory.Plain])
			{
				int num = Math.Min(current3.Size.Width, current3.Size.Height);
				if (num == 2)
				{
					MapBuilder.fCorridorTiles.Add(current3);
				}
				if (num > 2)
				{
					MapBuilder.fRoomTiles.Add(current3);
				}
			}
		}

		private static void build_warren(EventHandler callback)
		{
			MapBuilder.begin_map();
			int num = 0;
			while (MapBuilder.fMap.Areas.Count < MapBuilder.fData.MaxAreaCount && MapBuilder.fEndpoints.Count != 0 && num != 100)
			{
				int index = Session.Random.Next() % MapBuilder.fEndpoints.Count;
				Endpoint endpoint = MapBuilder.fEndpoints[index];
				bool flag = true;
				switch (Session.Random.Next() % 10)
				{
				case 0:
				case 1:
				case 2:
					try
					{
						flag = MapBuilder.add_area(endpoint);
						break;
					}
					catch (Exception ex)
					{
						LogSystem.Trace(ex);
						flag = false;
						break;
					}
					goto Block_5;
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
					goto IL_9D;
                        break;
				case 8:
					goto IL_B4;
                        break;
				case 9:
					goto IL_D3;
                        break;
				}
				IL_E9:
				if (flag)
				{
					MapBuilder.fEndpoints.Remove(endpoint);
					num = 0;
					callback(null, null);
					continue;
				}
				num++;
				continue;
				Block_5:
					IL_9D:
				try
				{
					flag = MapBuilder.add_corridor(endpoint, false);
					goto IL_E9;
				}
				catch (Exception ex2)
				{
					LogSystem.Trace(ex2);
					flag = false;
					goto IL_E9;
				}
					IL_B4:
				try
				{
					if (endpoint.Category != TileCategory.Doorway)
					{
						flag = MapBuilder.add_doorway(endpoint);
					}
					goto IL_E9;
				}
				catch (Exception ex3)
				{
					LogSystem.Trace(ex3);
					flag = false;
					goto IL_E9;
				}
					IL_D3:
				try
				{
					flag = MapBuilder.add_stairway(endpoint);
				}
				catch (Exception ex4)
				{
					LogSystem.Trace(ex4);
					flag = false;
				}
				goto IL_E9;
			}
			List<TileData> list = new List<TileData>();
			foreach (TileData current in MapBuilder.fMap.Tiles)
			{
				Tile tile = Session.FindTile(current.TileID, SearchType.Global);
				if (tile == null)
				{
					list.Add(current);
				}
				else if (tile.Category == TileCategory.Doorway)
				{
					Rectangle rectangle = MapBuilder.get_rect(tile, current);
					int num2 = 0;
					for (int num3 = rectangle.Left; num3 != rectangle.Right; num3++)
					{
						int y = rectangle.Top - 1;
						Point pt = new Point(num3, y);
						if (MapBuilder.tile_at_point(pt) == null)
						{
							num2++;
							break;
						}
					}
					for (int num4 = rectangle.Left; num4 != rectangle.Right; num4++)
					{
						int y2 = rectangle.Bottom + 1;
						Point pt2 = new Point(num4, y2);
						if (MapBuilder.tile_at_point(pt2) == null)
						{
							num2++;
							break;
						}
					}
					for (int num5 = rectangle.Top; num5 != rectangle.Bottom; num5++)
					{
						int x = rectangle.Left - 1;
						Point pt3 = new Point(x, num5);
						if (MapBuilder.tile_at_point(pt3) == null)
						{
							num2++;
							break;
						}
					}
					for (int num6 = rectangle.Top; num6 != rectangle.Bottom; num6++)
					{
						int x2 = rectangle.Right + 1;
						Point pt4 = new Point(x2, num6);
						if (MapBuilder.tile_at_point(pt4) == null)
						{
							num2++;
							break;
						}
					}
					if (num2 != 2)
					{
						list.Add(current);
					}
				}
			}
			foreach (TileData current2 in list)
			{
				MapBuilder.fMap.Tiles.Remove(current2);
				callback(null, null);
			}
		}

		private static void begin_map()
		{
			List<TileCategory> list = new List<TileCategory>();
			if (MapBuilder.fCorridorTiles.Count != 0)
			{
				list.Add(TileCategory.Plain);
			}
			if (MapBuilder.fTiles[TileCategory.Stairway].Count != 0)
			{
				list.Add(TileCategory.Stairway);
			}
			if (list.Count == 0)
			{
				return;
			}
			int index = Session.Random.Next() % list.Count;
			switch (list[index])
			{
			case TileCategory.Plain:
				MapBuilder.add_corridor(null, false);
				return;
			case TileCategory.Doorway:
				break;
			case TileCategory.Stairway:
				MapBuilder.add_stairway(null);
				break;
			default:
				return;
			}
		}

		private static bool add_area(Endpoint ep)
		{
			if (MapBuilder.fRoomTiles.Count == 0)
			{
				return false;
			}
			List<Tile> list = new List<Tile>();
			int num = 1 + Session.Random.Next() % 5;
			while (list.Count != num)
			{
				int index = Session.Random.Next() % MapBuilder.fRoomTiles.Count;
				Tile item = MapBuilder.fRoomTiles[index];
				list.Add(item);
			}
			List<Endpoint> list2 = new List<Endpoint>();
			list2.Add(ep);
			List<Pair<Tile, TileData>> list3 = new List<Pair<Tile, TileData>>();
			foreach (Tile current in list)
			{
				if (list2.Count == 0)
				{
					break;
				}
				int index2 = Session.Random.Next() % list2.Count;
				Endpoint endpoint = list2[index2];
				Pair<TileData, Direction> pair = MapBuilder.add_tile(current, endpoint, false, false);
				if (pair != null)
				{
					list2.Remove(endpoint);
					list3.Add(new Pair<Tile, TileData>(current, pair.First));
					if (pair.Second != Direction.South)
					{
						list2.Add(MapBuilder.get_endpoint(current, pair.First, Direction.North));
					}
					if (pair.Second != Direction.West)
					{
						list2.Add(MapBuilder.get_endpoint(current, pair.First, Direction.East));
					}
					if (pair.Second != Direction.North)
					{
						list2.Add(MapBuilder.get_endpoint(current, pair.First, Direction.South));
					}
					if (pair.Second != Direction.East)
					{
						list2.Add(MapBuilder.get_endpoint(current, pair.First, Direction.West));
					}
				}
			}
			if (list3.Count != 0)
			{
				MapBuilder.add_map_area(list3);
				List<Tile> list4 = MapBuilder.fTiles[TileCategory.Feature];
				if (list4.Count != 0)
				{
					int num2 = 0;
					foreach (Pair<Tile, TileData> current2 in list3)
					{
						num2 += current2.First.Area;
					}
					int num3 = Session.Random.Next() % (num2 / 10);
					int num4 = 0;
					int num5 = 0;
					List<Pair<Tile, TileData>> list5 = new List<Pair<Tile, TileData>>();
					while (num4 != num3 && num5 != 1000)
					{
						int index3 = Session.Random.Next() % list4.Count;
						Tile tile = list4[index3];
						TileData tileData = new TileData();
						tileData.TileID = tile.ID;
						tileData.Rotations = Session.Random.Next() % 4;
						int num6 = (tileData.Rotations % 2 == 0) ? tile.Size.Width : tile.Size.Height;
						int num7 = (tileData.Rotations % 2 == 0) ? tile.Size.Height : tile.Size.Width;
						List<Pair<Tile, TileData>> list6 = new List<Pair<Tile, TileData>>();
						foreach (Pair<Tile, TileData> current3 in list3)
						{
							int num8 = (current3.Second.Rotations % 2 == 0) ? current3.First.Size.Width : current3.First.Size.Height;
							int num9 = (current3.Second.Rotations % 2 == 0) ? current3.First.Size.Height : current3.First.Size.Width;
							int num10 = num8 - num6;
							int num11 = num9 - num7;
							if (num10 >= 0 && num11 >= 0)
							{
								list6.Add(current3);
							}
						}
						bool flag = false;
						if (list6.Count != 0)
						{
							int index4 = Session.Random.Next() % list6.Count;
							Pair<Tile, TileData> pair2 = list6[index4];
							int num12 = (pair2.Second.Rotations % 2 == 0) ? pair2.First.Size.Width : pair2.First.Size.Height;
							int num13 = (pair2.Second.Rotations % 2 == 0) ? pair2.First.Size.Height : pair2.First.Size.Width;
							int num14 = num12 - num6;
							int num15 = num13 - num7;
							if (num14 >= 0 && num15 >= 0)
							{
								int num16 = pair2.Second.Location.X;
								if (num14 != 0)
								{
									num16 += Session.Random.Next() % num14;
								}
								int num17 = pair2.Second.Location.Y;
								if (num15 != 0)
								{
									num17 += Session.Random.Next() % num15;
								}
								tileData.Location = new Point(num16, num17);
								bool flag2 = true;
								Rectangle rect = MapBuilder.get_rect(tile, tileData);
								foreach (Pair<Tile, TileData> current4 in list5)
								{
									if (MapBuilder.get_rect(current4.First, current4.Second).IntersectsWith(rect))
									{
										flag2 = false;
										break;
									}
								}
								if (flag2)
								{
									MapBuilder.fMap.Tiles.Add(tileData);
									list5.Add(new Pair<Tile, TileData>(tile, tileData));
									flag = true;
									break;
								}
							}
						}
						if (flag)
						{
							num4++;
							num5 = 0;
						}
						else
						{
							num5++;
						}
					}
				}
				int num18 = 1 + Session.Random.Next() % 3;
				int num19 = 0;
				int num20 = 0;
				while (num19 != num18 && list2.Count != 0 && num20 != 1000)
				{
					int index5 = Session.Random.Next() % list2.Count;
					Endpoint endpoint2 = list2[index5];
					bool flag3 = true;
					switch (Session.Random.Next() % 2)
					{
					case 0:
						flag3 = MapBuilder.add_doorway(endpoint2);
						break;
					case 1:
						flag3 = MapBuilder.add_corridor(endpoint2, true);
						break;
					}
					if (flag3)
					{
						num19++;
						list2.Remove(endpoint2);
						num20 = 0;
					}
					else
					{
						num20++;
					}
				}
			}
			return list3.Count != 0;
		}

		private static void add_map_area(List<Pair<Tile, TileData>> tiles)
		{
			int num = 2147483647;
			int num2 = 2147483647;
			int num3 = int.MinValue;
			int num4 = int.MinValue;
			foreach (Pair<Tile, TileData> current in tiles)
			{
				Rectangle rectangle = MapBuilder.get_rect(current.First, current.Second);
				if (rectangle.Left < num)
				{
					num = rectangle.Left;
				}
				if (rectangle.Right > num3)
				{
					num3 = rectangle.Right;
				}
				if (rectangle.Top < num2)
				{
					num2 = rectangle.Top;
				}
				if (rectangle.Bottom > num4)
				{
					num4 = rectangle.Bottom;
				}
			}
			num--;
			num2--;
			num3++;
			num4++;
			MapArea mapArea = new MapArea();
			mapArea.Name = "Area " + (MapBuilder.fMap.Areas.Count + 1);
			mapArea.Region = new Rectangle(num, num2, num3 - num, num4 - num2);
			MapBuilder.fMap.Areas.Add(mapArea);
		}

		private static bool add_corridor(Endpoint ep, bool follow)
		{
			if (MapBuilder.fCorridorTiles.Count == 0)
			{
				return false;
			}
			int index = Session.Random.Next() % MapBuilder.fCorridorTiles.Count;
			Tile t = MapBuilder.fCorridorTiles[index];
			if (ep == null)
			{
				TileData td = MapBuilder.add_first_tile(t);
				Orientation orient = MapBuilder.get_orientation(t, td);
				Direction dir = MapBuilder.get_starting_direction(orient);
				MapBuilder.fEndpoints.Add(MapBuilder.get_endpoint(t, td, dir));
			}
			else
			{
				Pair<TileData, Direction> pair = MapBuilder.add_tile(t, ep, follow, true);
				if (pair == null)
				{
					return false;
				}
				MapBuilder.fEndpoints.Add(MapBuilder.get_endpoint(t, pair.First, pair.Second));
			}
			return true;
		}

		private static bool add_doorway(Endpoint ep)
		{
			List<Tile> list = MapBuilder.fTiles[TileCategory.Doorway];
			if (list.Count == 0)
			{
				return false;
			}
			int index = Session.Random.Next() % list.Count;
			Tile t = list[index];
			if (ep != null)
			{
				Pair<TileData, Direction> pair = MapBuilder.add_tile(t, ep, true, true);
				if (pair == null)
				{
					return false;
				}
				MapBuilder.fEndpoints.Add(MapBuilder.get_endpoint(t, pair.First, pair.Second));
			}
			return true;
		}

		private static bool add_stairway(Endpoint ep)
		{
			List<Tile> list = MapBuilder.fTiles[TileCategory.Stairway];
			if (list.Count == 0)
			{
				return false;
			}
			int index = Session.Random.Next() % list.Count;
			Tile t = list[index];
			if (ep == null)
			{
				TileData td = MapBuilder.add_first_tile(t);
				Orientation orient = MapBuilder.get_orientation(t, td);
				Direction dir = MapBuilder.get_starting_direction(orient);
				MapBuilder.fEndpoints.Add(MapBuilder.get_endpoint(t, td, dir));
			}
			else if (MapBuilder.add_tile(t, ep, true, true) == null)
			{
				return false;
			}
			return true;
		}

		private static TileData add_first_tile(Tile t)
		{
			TileData tileData = new TileData();
			tileData.TileID = t.ID;
			tileData.Location = new Point(0, 0);
			tileData.Rotations = Session.Random.Next() % 4;
			MapBuilder.fMap.Tiles.Add(tileData);
			return tileData;
		}

		private static Pair<TileData, Direction> add_tile(Tile t, Endpoint ep, bool follow_direction, bool not_alongside)
		{
			TileData tileData = new TileData();
			tileData.TileID = t.ID;
			Direction direction = ep.Direction;
			if (!follow_direction)
			{
				List<Direction> list = new List<Direction>();
				if (ep.Direction != Direction.North)
				{
					list.Add(Direction.South);
				}
				if (ep.Direction != Direction.East)
				{
					list.Add(Direction.West);
				}
				if (ep.Direction != Direction.South)
				{
					list.Add(Direction.North);
				}
				if (ep.Direction != Direction.West)
				{
					list.Add(Direction.East);
				}
				int index = Session.Random.Next() % list.Count;
				direction = list[index];
			}
			if (follow_direction)
			{
				int num = Math.Min(t.Size.Width, t.Size.Height);
				if (direction == Direction.North || direction == Direction.South)
				{
					if (num > 1)
					{
						if (t.Size.Width > t.Size.Height)
						{
							tileData.Rotations = 1;
						}
					}
					else if (t.Size.Width < t.Size.Height)
					{
						tileData.Rotations = 1;
					}
				}
				if (direction == Direction.East || direction == Direction.West)
				{
					if (num > 1)
					{
						if (t.Size.Height > t.Size.Width)
						{
							tileData.Rotations = 1;
						}
					}
					else if (t.Size.Height < t.Size.Width)
					{
						tileData.Rotations = 1;
					}
				}
			}
			else
			{
				tileData.Rotations = Session.Random.Next() % 4;
			}
			int num2 = (tileData.Rotations % 2 == 0) ? t.Size.Width : t.Size.Height;
			int num3 = (tileData.Rotations % 2 == 0) ? t.Size.Height : t.Size.Width;
			switch (ep.Direction)
			{
			case Direction.North:
				tileData.Location = new Point(ep.TopLeft.X, ep.TopLeft.Y - (num3 - 1));
				break;
			case Direction.East:
				tileData.Location = ep.TopLeft;
				break;
			case Direction.South:
				tileData.Location = ep.TopLeft;
				break;
			case Direction.West:
				tileData.Location = new Point(ep.TopLeft.X - (num2 - 1), ep.TopLeft.Y);
				break;
			}
			Rectangle rect = MapBuilder.get_rect(t, tileData);
			if (not_alongside)
			{
				switch (direction)
				{
				case Direction.North:
				case Direction.South:
					rect = new Rectangle(rect.X - 1, rect.Y, rect.Width + 2, rect.Height);
					break;
				case Direction.East:
				case Direction.West:
					rect = new Rectangle(rect.X, rect.Y - 1, rect.Width, rect.Height + 2);
					break;
				}
			}
			if (!MapBuilder.check_rect_is_empty(rect))
			{
				return null;
			}
			MapBuilder.fMap.Tiles.Add(tileData);
			return new Pair<TileData, Direction>(tileData, direction);
		}

		private static Direction get_starting_direction(Orientation orient)
		{
			switch (orient)
			{
			case Orientation.NorthSouth:
				return Direction.South;
			case Orientation.EastWest:
				return Direction.East;
			default:
				if (Session.Random.Next() % 2 != 0)
				{
					return Direction.South;
				}
				return Direction.East;
			}
		}

		private static Endpoint get_endpoint(Tile t, TileData td, Direction dir)
		{
			Endpoint endpoint = new Endpoint();
			endpoint.Category = t.Category;
			endpoint.Direction = dir;
			int num = (td.Rotations % 2 == 0) ? t.Size.Width : t.Size.Height;
			int num2 = (td.Rotations % 2 == 0) ? t.Size.Height : t.Size.Width;
			switch (dir)
			{
			case Direction.North:
				endpoint.TopLeft = new Point(td.Location.X, td.Location.Y - 1);
				endpoint.BottomRight = new Point(td.Location.X + num - 1, td.Location.Y - 1);
				break;
			case Direction.East:
				endpoint.TopLeft = new Point(td.Location.X + num, td.Location.Y);
				endpoint.BottomRight = new Point(td.Location.X + num, td.Location.Y + num2 - 1);
				break;
			case Direction.South:
				endpoint.TopLeft = new Point(td.Location.X, td.Location.Y + num2);
				endpoint.BottomRight = new Point(td.Location.X + num - 1, td.Location.Y + num2);
				break;
			case Direction.West:
				endpoint.TopLeft = new Point(td.Location.X - 1, td.Location.Y);
				endpoint.BottomRight = new Point(td.Location.X - 1, td.Location.Y + num2 - 1);
				break;
			}
			return endpoint;
		}

		private static Orientation get_orientation(Tile t, TileData td)
		{
			bool flag = t.Size.Width >= t.Size.Height;
			if (td.Rotations % 2 == 0)
			{
				if (!flag)
				{
					return Orientation.NorthSouth;
				}
				return Orientation.EastWest;
			}
			else
			{
				if (!flag)
				{
					return Orientation.EastWest;
				}
				return Orientation.NorthSouth;
			}
		}

		private static void build_filled_area(EventHandler callback)
		{
			List<Tile> list = new List<Tile>();
			List<Tile> list2 = new List<Tile>();
			foreach (Library current in MapBuilder.fData.Libraries)
			{
				foreach (Tile current2 in current.Tiles)
				{
					if (current2.Category == TileCategory.Plain || current2.Category == TileCategory.Feature)
					{
						list.Add(current2);
						if (current2.Area == 1)
						{
							list2.Add(current2);
						}
					}
				}
			}
			if (list.Count == 0 || list2.Count == 0)
			{
				return;
			}
			MapArea mapArea = new MapArea();
			mapArea.Name = "Area";
			mapArea.Region = new Rectangle(0, 0, MapBuilder.fData.Width, MapBuilder.fData.Height);
			MapBuilder.fMap.Areas.Add(mapArea);
			int num = 0;
			int num2 = 0;
			int num5;
			do
			{
				bool flag = Session.Random.Next(20) == 0;
				List<Tile> list3 = flag ? list2 : list;
				int index = Session.Random.Next(list3.Count);
				Tile tile = list3[index];
				TileData tileData = new TileData();
				tileData.TileID = tile.ID;
				tileData.Rotations = Session.Random.Next(4);
				int width = tile.Size.Width;
				int height = tile.Size.Height;
				if (tileData.Rotations == 1 || tileData.Rotations == 3)
				{
					width = tile.Size.Height;
					height = tile.Size.Width;
				}
				List<Point> list4 = new List<Point>();
				if (flag)
				{
					for (int i = 0; i <= MapBuilder.fData.Width; i++)
					{
						for (int j = 0; j <= MapBuilder.fData.Height; j++)
						{
							Point point = new Point(i, j);
							if (MapBuilder.tile_at_point(point) == null)
							{
								int num3 = 0;
								if (MapBuilder.tile_at_point(new Point(i + 1, j)) != null)
								{
									num3++;
								}
								if (MapBuilder.tile_at_point(new Point(i - 1, j)) != null)
								{
									num3++;
								}
								if (MapBuilder.tile_at_point(new Point(i, j + 1)) != null)
								{
									num3++;
								}
								if (MapBuilder.tile_at_point(new Point(i, j - 1)) != null)
								{
									num3++;
								}
								if (num3 >= 3)
								{
									list4.Add(point);
								}
							}
						}
					}
				}
				else
				{
					int num4 = (tile.Area < 4) ? 1 : 2;
					for (int k = 0; k <= MapBuilder.fData.Width; k += num4)
					{
						for (int l = 0; l <= MapBuilder.fData.Height; l += num4)
						{
							Rectangle rect = new Rectangle(k, l, width, height);
							if (rect.Right <= MapBuilder.fData.Width && rect.Bottom <= MapBuilder.fData.Height && MapBuilder.check_rect_is_empty(rect))
							{
								Point item = new Point(k, l);
								list4.Add(item);
							}
						}
					}
				}
				if (list4.Count != 0)
				{
					int index2 = Session.Random.Next(list4.Count);
					Point location = list4[index2];
					tileData.Location = location;
					MapBuilder.fMap.Tiles.Add(tileData);
					num += tile.Area;
				}
				else
				{
					num2++;
					if (num2 >= 100)
					{
						num2 = 0;
						if (MapBuilder.fMap.Tiles.Count != 0)
						{
							int index3 = Session.Random.Next(MapBuilder.fMap.Tiles.Count);
							TileData tileData2 = MapBuilder.fMap.Tiles[index3];
							MapBuilder.fMap.Tiles.Remove(tileData2);
							Tile tile2 = Session.FindTile(tileData2.TileID, SearchType.Global);
							num -= tile2.Area;
						}
					}
				}
				callback(null, null);
				num5 = MapBuilder.fData.Width * MapBuilder.fData.Height;
			}
			while (num != num5);
			MapBuilder.fMap.Areas.Clear();
		}

		private static void build_freeform_area(EventHandler callback)
		{
			List<Tile> list = new List<Tile>();
			foreach (Library current in MapBuilder.fData.Libraries)
			{
				foreach (Tile current2 in current.Tiles)
				{
					if (current2.Category == TileCategory.Plain || current2.Category == TileCategory.Feature)
					{
						list.Add(current2);
					}
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			int i = MapBuilder.fData.Height * MapBuilder.fData.Width;
			while (i > 0)
			{
				callback(null, null);
				bool flag = false;
				if (flag)
				{
					Tile tile = null;
					if (tile != null)
					{
						Point location = new Point(0, 0);
						TileData tileData = new TileData();
						tileData.TileID = tile.ID;
						tileData.Location = location;
						MapBuilder.fMap.Tiles.Add(tileData);
						i -= tile.Area;
					}
				}
				else
				{
					int index = Session.Random.Next() % list.Count;
					Tile tile2 = list[index];
					Point location2 = new Point(0, 0);
					if (MapBuilder.fMap.Tiles.Count != 0)
					{
						int index2 = Session.Random.Next() % MapBuilder.fMap.Tiles.Count;
						TileData tileData2 = MapBuilder.fMap.Tiles[index2];
						Tile tile3 = Session.FindTile(tileData2.TileID, SearchType.Global);
						List<Rectangle> list2 = new List<Rectangle>();
						int num = tileData2.Location.X - (tile2.Size.Width - 1);
						int num2 = tileData2.Location.X + (tile3.Size.Width - 1);
						int num3 = tileData2.Location.Y - (tile2.Size.Height - 1);
						int num4 = tileData2.Location.Y + (tile3.Size.Height - 1);
						for (int j = num; j <= num2; j++)
						{
							int y = tileData2.Location.Y - tile2.Size.Height;
							Rectangle item = new Rectangle(j, y, tile2.Size.Width, tile2.Size.Height);
							list2.Add(item);
						}
						for (int k = num; k <= num2; k++)
						{
							int y2 = tileData2.Location.Y + tile3.Size.Height;
							Rectangle item2 = new Rectangle(k, y2, tile2.Size.Width, tile2.Size.Height);
							list2.Add(item2);
						}
						for (int l = num3; l <= num4; l++)
						{
							int x = tileData2.Location.X - tile3.Size.Width;
							Rectangle item3 = new Rectangle(x, l, tile2.Size.Width, tile2.Size.Height);
							list2.Add(item3);
						}
						for (int m = num3; m <= num4; m++)
						{
							int x2 = tileData2.Location.X + tile3.Size.Width;
							Rectangle item4 = new Rectangle(x2, m, tile2.Size.Width, tile2.Size.Height);
							list2.Add(item4);
						}
						List<Rectangle> list3 = new List<Rectangle>();
						foreach (Rectangle current3 in list2)
						{
							if (MapBuilder.check_rect_is_empty(current3))
							{
								list3.Add(current3);
							}
						}
						if (list3.Count == 0)
						{
							continue;
						}
						int index3 = Session.Random.Next() % list3.Count;
						location2 = list3[index3].Location;
					}
					TileData tileData3 = new TileData();
					tileData3.TileID = tile2.ID;
					tileData3.Location = location2;
					MapBuilder.fMap.Tiles.Add(tileData3);
					i -= tile2.Area;
				}
			}
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			foreach (TileData current4 in MapBuilder.fMap.Tiles)
			{
				Tile tile4 = Session.FindTile(current4.TileID, SearchType.Global);
				Rectangle rectangle = new Rectangle(current4.Location, tile4.Size);
				num5 = Math.Min(num5, rectangle.Left);
				num6 = Math.Max(num6, rectangle.Right);
				num7 = Math.Min(num7, rectangle.Top);
				num8 = Math.Max(num8, rectangle.Bottom);
			}
			MapArea mapArea = new MapArea();
			mapArea.Name = "Area";
			mapArea.Region = new Rectangle(num5, num7, num6 - num5, num8 - num7);
			MapBuilder.fMap.Areas.Add(mapArea);
		}

		private static bool check_rect_is_empty(Rectangle rect)
		{
			foreach (TileData current in MapBuilder.fMap.Tiles)
			{
				Tile t = Session.FindTile(current.TileID, SearchType.Global);
				if (MapBuilder.get_rect(t, current).IntersectsWith(rect))
				{
					return false;
				}
			}
			return true;
		}

		private static TileData tile_at_point(Point pt)
		{
			foreach (TileData current in MapBuilder.fMap.Tiles)
			{
				Tile t = Session.FindTile(current.TileID, SearchType.Global);
				if (MapBuilder.get_rect(t, current).Contains(pt))
				{
					return current;
				}
			}
			return null;
		}

		private static Rectangle get_rect(Tile t, TileData td)
		{
			int width = (td.Rotations % 2 == 0) ? t.Size.Width : t.Size.Height;
			int height = (td.Rotations % 2 == 0) ? t.Size.Height : t.Size.Width;
			return new Rectangle(td.Location.X, td.Location.Y, width, height);
		}
	}
}
