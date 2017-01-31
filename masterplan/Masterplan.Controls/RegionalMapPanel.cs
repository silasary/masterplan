using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Utils;
using Utils.Graphics;

namespace Masterplan.Controls
{
	public class RegionalMapPanel : UserControl
	{
		private const float LOCATION_RADIUS = 8f;

		private IContainer components;

		private RegionalMap fMap;

		private MapViewMode fMode;

		private Plot fPlot;

		private bool fShowLocations = true;

		private bool fAllowEditing;

		private MapLocation fHoverLocation;

		private MapLocation fSelectedLocation;

		private MapLocation fHighlightedLocation;

		private StringFormat fCentred = new StringFormat();

        public event EventHandler SelectedLocationModified;

        public event EventHandler LocationModified;

		public RegionalMap Map
		{
			get
			{
				return this.fMap;
			}
			set
			{
				this.fMap = value;
				base.Invalidate();
			}
		}

		public MapViewMode Mode
		{
			get
			{
				return this.fMode;
			}
			set
			{
				this.fMode = value;
				base.Invalidate();
			}
		}

		public Plot Plot
		{
			get
			{
				return this.fPlot;
			}
			set
			{
				this.fPlot = value;
				base.Invalidate();
			}
		}

		public bool ShowLocations
		{
			get
			{
				return this.fShowLocations;
			}
			set
			{
				this.fShowLocations = value;
				base.Invalidate();
			}
		}

		public bool AllowEditing
		{
			get
			{
				return this.fAllowEditing;
			}
			set
			{
				this.fAllowEditing = value;
			}
		}

		public MapLocation HoverLocation
		{
			get
			{
				return this.fHoverLocation;
			}
		}

		public MapLocation SelectedLocation
		{
			get
			{
				return this.fSelectedLocation;
			}
			set
			{
				this.fSelectedLocation = value;
				base.Invalidate();
			}
		}

		public MapLocation HighlightedLocation
		{
			get
			{
				return this.fHighlightedLocation;
			}
			set
			{
				this.fHighlightedLocation = value;
				base.Invalidate();
			}
		}

		public RectangleF MapRectangle
		{
			get
			{
				if (this.fMap == null || this.fMap.Image == null)
				{
					return RectangleF.Empty;
				}
				double val = (double)base.ClientRectangle.Width / (double)this.fMap.Image.Width;
				double val2 = (double)base.ClientRectangle.Height / (double)this.fMap.Image.Height;
				float num = (float)Math.Min(val, val2);
				float num2 = num * (float)this.fMap.Image.Width;
				float num3 = num * (float)this.fMap.Image.Height;
				float x = ((float)base.ClientRectangle.Width - num2) / 2f;
				float y = ((float)base.ClientRectangle.Height - num3) / 2f;
				return new RectangleF(x, y, num2, num3);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			base.AutoScaleMode = AutoScaleMode.Font;
		}

		public RegionalMapPanel()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
		}

		protected void OnSelectedLocationModified()
		{
			if (this.SelectedLocationModified != null)
			{
				this.SelectedLocationModified(this, new EventArgs());
			}
		}

		protected void OnLocationModified()
		{
			if (this.LocationModified != null)
			{
				this.LocationModified(this, new EventArgs());
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			try
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
				switch (this.fMode)
				{
				case MapViewMode.Normal:
				case MapViewMode.Thumbnail:
				{
					Color color = Color.FromArgb(240, 240, 240);
					Color color2 = Color.FromArgb(170, 170, 170);
					Brush brush = new LinearGradientBrush(base.ClientRectangle, color, color2, LinearGradientMode.Vertical);
					e.Graphics.FillRectangle(brush, base.ClientRectangle);
					break;
				}
				case MapViewMode.Plain:
					e.Graphics.FillRectangle(Brushes.White, base.ClientRectangle);
					break;
				case MapViewMode.PlayerView:
					e.Graphics.FillRectangle(Brushes.Black, base.ClientRectangle);
					break;
				}
				if (this.fMap == null || this.fMap.Image == null)
				{
					e.Graphics.DrawString("(no map selected)", this.Font, Brushes.Black, base.ClientRectangle, this.fCentred);
				}
				else
				{
					RectangleF mapRectangle = this.MapRectangle;
					e.Graphics.DrawImage(this.fMap.Image, mapRectangle);
					if (this.fShowLocations)
					{
						foreach (MapLocation current in this.fMap.Locations)
						{
							if (current != null && (this.fHighlightedLocation == null || !(current.ID != this.fHighlightedLocation.ID)))
							{
								Color color3 = Color.White;
								if (current == this.fHoverLocation)
								{
									color3 = Color.Blue;
								}
								if (current == this.fSelectedLocation)
								{
									color3 = Color.Blue;
								}
								RectangleF rect = this.get_loc_rect(current, mapRectangle);
								e.Graphics.DrawEllipse(new Pen(Color.Black, 5f), rect);
								e.Graphics.DrawEllipse(new Pen(color3, 2f), rect);
							}
						}
					}
					if (this.fPlot != null)
					{
						foreach (PlotPoint current2 in this.fPlot.Points)
						{
							if (!(current2.RegionalMapID != this.fMap.ID))
							{
								MapLocation mapLocation = this.fMap.FindLocation(current2.MapLocationID);
								if (mapLocation != null)
								{
									PointF pt = this.get_loc_pt(mapLocation, mapRectangle);
									RectangleF rect2 = this.get_loc_rect(mapLocation, mapRectangle);
									rect2.Inflate(-5f, -5f);
									foreach (Guid current3 in current2.Links)
									{
										PlotPoint plotPoint = this.fPlot.FindPoint(current3);
										if (plotPoint != null && !(plotPoint.RegionalMapID != this.fMap.ID))
										{
											MapLocation mapLocation2 = this.fMap.FindLocation(plotPoint.MapLocationID);
											if (mapLocation2 != null)
											{
												PointF pt2 = this.get_loc_pt(mapLocation2, mapRectangle);
												e.Graphics.DrawLine(new Pen(Color.Red, 3f), pt, pt2);
												RectangleF rect3 = this.get_loc_rect(mapLocation2, mapRectangle);
												rect3.Inflate(-5f, -5f);
												e.Graphics.FillEllipse(Brushes.Red, rect2);
												e.Graphics.FillEllipse(Brushes.Red, rect3);
											}
										}
									}
								}
							}
						}
					}
					if (this.fShowLocations)
					{
						foreach (MapLocation current4 in this.fMap.Locations)
						{
							if ((this.fHighlightedLocation == null || current4 == this.fHighlightedLocation) && (current4 == this.fHoverLocation || current4 == this.fSelectedLocation || current4 == this.fHighlightedLocation))
							{
								bool flag = current4.Category != "" && (this.fMode == MapViewMode.Normal || this.fMode == MapViewMode.Thumbnail);
								RectangleF rectangleF = this.get_loc_rect(current4, mapRectangle);
								SizeF sizeF = e.Graphics.MeasureString(current4.Name, this.Font);
								SizeF sizeF2 = e.Graphics.MeasureString(current4.Category, this.Font);
								float width = flag ? Math.Max(sizeF.Width, sizeF2.Width) : sizeF.Width;
								float height = flag ? (sizeF.Height + sizeF2.Height) : sizeF.Height;
								SizeF size = new SizeF(width, height);
								size.Width += 2f;
								size.Height += 2f;
								float num = rectangleF.X + rectangleF.Width / 2f - size.Width / 2f;
								float num2 = rectangleF.Top - size.Height - 5f;
								if (num2 < (float)base.ClientRectangle.Top)
								{
									num2 = rectangleF.Bottom + 5f;
								}
								num = Math.Max(num, 0f);
								float num3 = num + size.Width - (float)base.ClientRectangle.Right;
								if (num3 > 0f)
								{
									num -= num3;
								}
								RectangleF rectangleF2 = new RectangleF(new PointF(num, num2), size);
								GraphicsPath path = RoundedRectangle.Create(rectangleF2, (float)this.Font.Height * 0.35f);
								e.Graphics.FillPath(Brushes.LightYellow, path);
								e.Graphics.DrawPath(Pens.Black, path);
								if (flag)
								{
									float num4 = rectangleF2.Height / 2f;
									float num5 = rectangleF2.Y + num4;
									RectangleF layoutRectangle = new RectangleF(rectangleF2.X, rectangleF2.Y, rectangleF2.Width, num4);
									RectangleF layoutRectangle2 = new RectangleF(rectangleF2.X, num5, rectangleF2.Width, num4);
									e.Graphics.DrawLine(Pens.Gray, rectangleF2.X, num5, rectangleF2.X + rectangleF2.Width, num5);
									e.Graphics.DrawString(current4.Name, this.Font, Brushes.Black, layoutRectangle, this.fCentred);
									e.Graphics.DrawString(current4.Category, this.Font, Brushes.DarkGray, layoutRectangle2, this.fCentred);
								}
								else
								{
									e.Graphics.DrawString(current4.Name, this.Font, Brushes.Black, rectangleF2, this.fCentred);
								}
							}
						}
					}
					if (this.fMode == MapViewMode.Normal && this.fMap.Locations.Count == 0)
					{
						string text = "Double-click on the map to set a location.";
						float num6 = 10f;
						float num7 = (float)base.ClientRectangle.Width - 2f * num6;
						float num8 = e.Graphics.MeasureString(text, this.Font, (int)num7).Height * 2f;
						RectangleF rectangleF3 = new RectangleF(num6, num6, num7, num8);
						GraphicsPath path2 = RoundedRectangle.Create(rectangleF3, num8 / 3f);
						e.Graphics.FillPath(new SolidBrush(Color.FromArgb(200, Color.Black)), path2);
						e.Graphics.DrawPath(Pens.Black, path2);
						e.Graphics.DrawString(text, this.Font, Brushes.White, rectangleF3, this.fCentred);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (this.fMode == MapViewMode.Plain || this.fMode == MapViewMode.PlayerView)
			{
				return;
			}
			Point pt = base.PointToClient(Cursor.Position);
			this.fHoverLocation = this.get_location_at(pt);
			if (this.fAllowEditing && e.Button == MouseButtons.Left && this.fSelectedLocation != null)
			{
				PointF pointF = this.get_point(pt);
				if (pointF == PointF.Empty)
				{
					this.fSelectedLocation = null;
				}
				else
				{
					this.fSelectedLocation.Point = pointF;
					this.OnLocationModified();
				}
			}
			base.Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (this.fMode == MapViewMode.Plain || this.fMode == MapViewMode.PlayerView)
			{
				return;
			}
			this.fSelectedLocation = this.fHoverLocation;
			this.OnSelectedLocationModified();
			base.Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			if (this.fMode == MapViewMode.Plain || this.fMode == MapViewMode.PlayerView)
			{
				return;
			}
			this.fHoverLocation = null;
			base.Invalidate();
		}

		private PointF get_loc_pt(MapLocation loc, RectangleF img_rect)
		{
			float x = img_rect.X + img_rect.Width * loc.Point.X;
			float y = img_rect.Y + img_rect.Height * loc.Point.Y;
			return new PointF(x, y);
		}

		private RectangleF get_loc_rect(MapLocation loc, RectangleF img_rect)
		{
			PointF pointF = this.get_loc_pt(loc, img_rect);
			float num = 8f;
			return new RectangleF(pointF.X - num, pointF.Y - num, 2f * num, 2f * num);
		}

		private MapLocation get_location_at(Point pt)
		{
			if (this.fMap == null)
			{
				return null;
			}
			RectangleF mapRectangle = this.MapRectangle;
			foreach (MapLocation current in this.fMap.Locations)
			{
				if (this.get_loc_rect(current, mapRectangle).Contains(pt))
				{
					return current;
				}
			}
			return null;
		}

		private PointF get_point(Point pt)
		{
			RectangleF mapRectangle = this.MapRectangle;
			if (!mapRectangle.Contains(pt))
			{
				return PointF.Empty;
			}
			float x = ((float)pt.X - mapRectangle.X) / mapRectangle.Width;
			float y = ((float)pt.Y - mapRectangle.Y) / mapRectangle.Height;
			return new PointF(x, y);
		}
	}
}
