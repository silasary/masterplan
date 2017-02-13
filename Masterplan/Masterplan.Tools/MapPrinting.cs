using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

namespace Masterplan.Tools
{
	internal class MapPrinting
	{
		private static Map fMap = null;

		private static Rectangle fViewpoint = Rectangle.Empty;

		private static Encounter fEncounter = null;

		private static bool fShowGridlines = false;

		private static bool fPosterMode = false;

		private static List<Rectangle> fPages = null;

		public static void Print(MapView mapview, bool poster, PrinterSettings settings)
		{
			MapPrinting.fMap = mapview.Map;
			MapPrinting.fViewpoint = mapview.Viewpoint;
			MapPrinting.fEncounter = mapview.Encounter;
			MapPrinting.fShowGridlines = (mapview.ShowGrid == MapGridMode.Overlay);
			MapPrinting.fPosterMode = poster;
			PrintDocument printDocument = new PrintDocument();
			printDocument.DocumentName = MapPrinting.fMap.Name;
			printDocument.PrinterSettings = settings;
			MapPrinting.fPages = null;
			printDocument.PrintPage += new PrintPageEventHandler(MapPrinting.print_map_page);
			printDocument.Print();
		}

		private static void print_map_page(object sender, PrintPageEventArgs e)
		{
			MapView mapView = new MapView();
			mapView.Map = MapPrinting.fMap;
			mapView.Viewpoint = MapPrinting.fViewpoint;
			mapView.Encounter = MapPrinting.fEncounter;
			mapView.LineOfSight = false;
			mapView.Mode = MapViewMode.Plain;
			mapView.Size = e.PageBounds.Size;
			mapView.BorderSize = 1;
			if (MapPrinting.fShowGridlines)
			{
				mapView.ShowGrid = MapGridMode.Overlay;
			}
			if (MapPrinting.fPages == null)
			{
				if (MapPrinting.fPosterMode)
				{
					int square_count_h = e.PageSettings.PaperSize.Width / 100;
					int square_count_v = e.PageSettings.PaperSize.Height / 100;
					MapPrinting.fPages = MapPrinting.get_pages(mapView, square_count_h, square_count_v);
				}
				else
				{
					MapPrinting.fPages = new List<Rectangle>();
					MapPrinting.fPages.Add(mapView.Viewpoint);
				}
			}
			mapView.Viewpoint = MapPrinting.fPages[0];
			MapPrinting.fPages.RemoveAt(0);
			bool flag = mapView.LayoutData.Width > mapView.LayoutData.Height;
			bool flag2 = e.PageBounds.Width > e.PageBounds.Height;
			bool flag3 = flag != flag2;
			if (flag3)
			{
				mapView.Width = e.PageBounds.Height;
				mapView.Height = e.PageBounds.Width;
			}
			Bitmap bitmap = new Bitmap(mapView.Width, mapView.Height);
			mapView.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
			if (flag3)
			{
				bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
			}
			e.Graphics.DrawImage(bitmap, e.PageBounds);
			e.HasMorePages = (MapPrinting.fPages.Count != 0);
		}

		private static List<Rectangle> get_pages(MapView ctrl, int square_count_h, int square_count_v)
		{
			int num = Math.Max(square_count_h, square_count_v);
			int num2 = Math.Min(square_count_h, square_count_v);
			List<Point> list = new List<Point>();
			for (int i = ctrl.LayoutData.MinX; i <= ctrl.LayoutData.MaxX; i++)
			{
				for (int j = ctrl.LayoutData.MinY; j <= ctrl.LayoutData.MaxY; j++)
				{
					Point point = new Point(i, j);
					TileData tileAtSquare = ctrl.LayoutData.GetTileAtSquare(point);
					if (tileAtSquare != null)
					{
						list.Add(point);
					}
				}
			}
			List<Rectangle> list2 = new List<Rectangle>();
			for (int k = ctrl.LayoutData.MinX; k <= ctrl.LayoutData.MaxX; k += num)
			{
				for (int l = ctrl.LayoutData.MinY; l <= ctrl.LayoutData.MaxY; l += num2)
				{
					Rectangle item = new Rectangle(k, l, num, num2);
					bool flag = false;
					foreach (Point current in list)
					{
						if (item.Contains(current))
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						list2.Add(item);
					}
				}
			}
			return list2;
		}
	}
}
