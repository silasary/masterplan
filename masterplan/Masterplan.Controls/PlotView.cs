using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Utils;

namespace Masterplan.Controls
{
	public class PlotView : UserControl
	{
		private class DragLocation
		{
			public PlotPoint LHS;

			public PlotPoint RHS;

			public RectangleF Rect = RectangleF.Empty;
		}

		private const int ARROW_SIZE = 6;

		private PlotPoint fHoverPoint;

		private StringFormat fCentred = new StringFormat();

		private List<List<PlotPoint>> fLayers;

		private Dictionary<Guid, RectangleF> fRegions;

		private Dictionary<Guid, Dictionary<Guid, List<PointF>>> fLinkPaths;

		private Rectangle fUpRect = Rectangle.Empty;

		private Rectangle fDownRect = Rectangle.Empty;

		private Plot fPlot;

		private PlotViewMode fMode;

		private PlotViewLinkStyle fLinkStyle;

		private string fFilter = "";

		private bool fShowLevels = true;

		private PlotPoint fSelectedPoint;

		private bool fShowTooltips = true;

		private PlotView.DragLocation fDragLocation;

		private IContainer components;

		private ToolTip Tooltip;

        [Category("Property Changed"), Description("Occurs when the selected point changes.")]
        public event EventHandler SelectionChanged;

        [Category("Property Changed"), Description("Occurs when the plot layout changes.")]
        public event EventHandler PlotLayoutChanged;

        [Category("Property Changed"), Description("Occurs when the current plot changes.")]
        public event EventHandler PlotChanged;

		[Category("Data"), Description("The plot to display.")]
		public Plot Plot
		{
			get
			{
				return this.fPlot;
			}
			set
			{
				if (this.fPlot != value)
				{
					this.fPlot = value;
					this.fSelectedPoint = null;
					this.fHoverPoint = null;
					this.RecalculateLayout();
					base.Invalidate();
					this.OnSelectionChanged();
				}
			}
		}

		[Category("Appearance"), Description("How the plot should be displayed.")]
		public PlotViewMode Mode
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

		[Category("Appearance"), Description("How plot point links should be displayed.")]
		public PlotViewLinkStyle LinkStyle
		{
			get
			{
				return this.fLinkStyle;
			}
			set
			{
				this.fLinkStyle = value;
				this.RecalculateLayout();
				base.Invalidate();
			}
		}

		[Category("Behavior"), Description("Plot points which do not contain this text are not shown.")]
		public string Filter
		{
			get
			{
				return this.fFilter;
			}
			set
			{
				this.fFilter = value;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("Determines whether levelling information is shown.")]
		public bool ShowLevels
		{
			get
			{
				return this.fShowLevels;
			}
			set
			{
				this.fShowLevels = value;
				base.Invalidate();
			}
		}

		[Category("Behavior"), Description("The selected point.")]
		public PlotPoint SelectedPoint
		{
			get
			{
				return this.fSelectedPoint;
			}
			set
			{
				if (this.fSelectedPoint != value)
				{
					this.fSelectedPoint = value;
					base.Invalidate();
					this.OnSelectionChanged();
				}
			}
		}

		[Category("Appearance"), Description("Determines whether tooltips are shown.")]
		public bool ShowTooltips
		{
			get
			{
				return this.fShowTooltips;
			}
			set
			{
				this.fShowTooltips = value;
			}
		}

		public PlotView()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
		}

		public bool Navigate(Keys key)
		{
			try
			{
				if (this.SelectedPoint == null)
				{
					bool result = false;
					return result;
				}
				List<List<PlotPoint>> list = Workspace.FindLayers(this.fPlot);
				int num = 0;
				while (num != list.Count && !list[num].Contains(this.SelectedPoint))
				{
					num++;
				}
				if (key == Keys.Up)
				{
					if (num != 0)
					{
						List<PlotPoint> list2 = list[num - 1];
						foreach (PlotPoint current in list2)
						{
							if (current.Links.Contains(this.SelectedPoint.ID))
							{
								this.SelectedPoint = current;
								break;
							}
						}
					}
					bool result = true;
					return result;
				}
				if (key == Keys.Down)
				{
					if (num != list.Count - 1)
					{
						List<PlotPoint> list3 = list[num + 1];
						foreach (PlotPoint current2 in list3)
						{
							if (this.SelectedPoint.Links.Contains(current2.ID))
							{
								this.SelectedPoint = list3[0];
								break;
							}
						}
					}
					bool result = true;
					return result;
				}
				if (key == Keys.Left)
				{
					List<PlotPoint> list4 = list[num];
					int num2 = list4.IndexOf(this.SelectedPoint);
					if (num2 != 0)
					{
						this.SelectedPoint = list4[num2 - 1];
					}
					bool result = true;
					return result;
				}
				if (key == Keys.Right)
				{
					List<PlotPoint> list5 = list[num];
					int num3 = list5.IndexOf(this.SelectedPoint);
					if (num3 != list5.Count - 1)
					{
						this.SelectedPoint = list5[num3 + 1];
					}
					bool result = true;
					return result;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return false;
		}

		public void RecalculateLayout()
		{
			this.clear_layout_calculations();
		}

		protected void OnSelectionChanged()
		{
			try
			{
				if (this.SelectionChanged != null)
				{
					this.SelectionChanged(this, new EventArgs());
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		protected void OnPlotLayoutChanged()
		{
			try
			{
				if (this.PlotLayoutChanged != null)
				{
					this.PlotLayoutChanged(this, new EventArgs());
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		protected void OnPlotChanged()
		{
			try
			{
				if (this.PlotChanged != null)
				{
					this.PlotChanged(this, new EventArgs());
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			try
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
				if (this.fLayers == null)
				{
					this.do_layout_calculations();
				}
				if (this.fMode == PlotViewMode.Plain)
				{
					e.Graphics.FillRectangle(SystemBrushes.Window, base.ClientRectangle);
				}
				else
				{
					Color color = Color.FromArgb(240, 240, 240);
					Color color2 = Color.FromArgb(170, 170, 170);
					using (Brush brush = new LinearGradientBrush(base.ClientRectangle, color, color2, LinearGradientMode.Vertical))
					{
						e.Graphics.FillRectangle(brush, base.ClientRectangle);
					}
					Point pt = base.PointToClient(Cursor.Position);
					PlotPoint plotPoint = Session.Project.FindParent(this.fPlot);
					if (plotPoint != null)
					{
						using (Font font = new Font(this.Font.FontFamily, this.Font.Size * 2f))
						{
							e.Graphics.DrawString(plotPoint.Name, font, Brushes.DarkGray, (float)(base.ClientRectangle.Left + 10), (float)(base.ClientRectangle.Top + 10));
						}
					}
					Color color3 = (plotPoint == null) ? Color.DarkGray : Color.Black;
					Color color4 = (this.fSelectedPoint == null) ? Color.DarkGray : Color.Black;
					using (Pen pen = new Pen(color3))
					{
						using (Pen pen2 = new Pen(color4))
						{
							using (Brush brush2 = new SolidBrush(color3))
							{
								using (Brush brush3 = new SolidBrush(color4))
								{
									Point point = new Point(this.fUpRect.Left + 5, this.fUpRect.Bottom - 5);
									Point point2 = new Point(this.fUpRect.Right - 5, this.fUpRect.Bottom - 5);
									Point point3 = new Point((this.fUpRect.Right + this.fUpRect.Left) / 2, this.fUpRect.Top + 5);
									if (this.fUpRect.Contains(pt))
									{
										e.Graphics.FillPolygon(brush2, new Point[]
										{
											point,
											point2,
											point3
										});
									}
									else
									{
										e.Graphics.DrawPolygon(pen, new Point[]
										{
											point,
											point2,
											point3
										});
									}
									Point point4 = new Point(this.fDownRect.Left + 5, this.fDownRect.Top + 5);
									Point point5 = new Point(this.fDownRect.Right - 5, this.fDownRect.Top + 5);
									Point point6 = new Point((this.fDownRect.Right + this.fDownRect.Left) / 2, this.fDownRect.Bottom - 5);
									if (this.fDownRect.Contains(pt))
									{
										e.Graphics.FillPolygon(brush3, new Point[]
										{
											point4,
											point5,
											point6
										});
									}
									else
									{
										e.Graphics.DrawPolygon(pen2, new Point[]
										{
											point4,
											point5,
											point6
										});
									}
								}
							}
						}
					}
				}
				if (Session.Project == null)
				{
					string s = "(no project)";
					e.Graphics.DrawString(s, this.Font, SystemBrushes.WindowText, base.ClientRectangle, this.fCentred);
				}
				else if (this.fPlot == null || this.fPlot.Points.Count == 0)
				{
					string s2 = "(no plot points)";
					e.Graphics.DrawString(s2, this.Font, SystemBrushes.WindowText, base.ClientRectangle, this.fCentred);
				}
				else
				{
					if (this.fDragLocation != null && this.fHoverPoint == null)
					{
						float num = this.fDragLocation.Rect.Left + this.fDragLocation.Rect.Width / 2f;
						using (Pen pen3 = new Pen(Color.DarkBlue, 2f))
						{
							e.Graphics.DrawLine(pen3, num, this.fDragLocation.Rect.Top, num, this.fDragLocation.Rect.Bottom);
						}
						float num2 = 3f;
						using (Pen pen4 = new Pen(Color.DarkBlue, 1f))
						{
							PointF pt2 = new PointF(num - num2, this.fDragLocation.Rect.Top);
							PointF pt3 = new PointF(num + num2, this.fDragLocation.Rect.Top);
							e.Graphics.DrawLine(pen4, pt2, pt3);
							PointF pt4 = new PointF(num - num2, this.fDragLocation.Rect.Bottom);
							PointF pt5 = new PointF(num + num2, this.fDragLocation.Rect.Bottom);
							e.Graphics.DrawLine(pen4, pt4, pt5);
						}
					}
					if (this.fShowLevels)
					{
						for (int num3 = 0; num3 != this.fLayers.Count; num3++)
						{
							List<PlotPoint> list = this.fLayers[num3];
							int totalXP = Workspace.GetTotalXP(list[0]);
							int num4 = totalXP + Workspace.GetLayerXP(list);
							int heroLevel = Experience.GetHeroLevel(totalXP / Session.Project.Party.Size);
							int heroLevel2 = Experience.GetHeroLevel(num4 / Session.Project.Party.Size);
							if (heroLevel != heroLevel2)
							{
								PlotPoint plotPoint2 = list[0];
								RectangleF rectangleF = this.fRegions[plotPoint2.ID];
								int num5 = this.fLayers.IndexOf(list);
								float num6 = rectangleF.Height * ((float)(num5 * 2) + 2.5f);
								PointF pt6 = new PointF(0f, num6);
								PointF pt7 = new PointF((float)base.Width, num6);
								e.Graphics.DrawLine(SystemPens.ControlDarkDark, pt6, pt7);
								PointF point7 = new PointF(0f, num6 - (float)this.Font.Height);
								e.Graphics.DrawString("Level " + heroLevel2, this.Font, SystemBrushes.WindowText, point7);
							}
						}
					}
					foreach (PlotPoint current in this.fPlot.Points)
					{
						if (this.fRegions.ContainsKey(current.ID))
						{
							foreach (Guid current2 in current.Links)
							{
								if (this.fRegions.ContainsKey(current2))
								{
									RectangleF rectangleF2 = this.fRegions[current.ID];
									RectangleF rectangleF3 = this.fRegions[current2];
									PointF pt8 = new PointF(rectangleF2.X + rectangleF2.Width / 2f, rectangleF2.Bottom);
									PointF pointF = new PointF(rectangleF2.X + rectangleF2.Width / 2f, rectangleF2.Bottom + 12f);
									PointF pointF2 = new PointF(rectangleF3.X + rectangleF3.Width / 2f, rectangleF3.Top - 12f);
									PointF pt9 = new PointF(pointF2.X, pointF2.Y + 6f);
									new PointF(rectangleF3.X + rectangleF3.Width / 2f, rectangleF3.Top);
									int alpha = 130;
									float width = 2f;
									PlotPoint pp = this.fPlot.FindPoint(current2);
									if (this.draw_link(current, pp))
									{
										PointF pointF3 = new PointF(pt9.X - 6f, pt9.Y);
										PointF pointF4 = new PointF(pt9.X + 6f, pt9.Y);
										PointF pointF5 = new PointF(pt9.X, pt9.Y + 6f);
										e.Graphics.FillPolygon(SystemBrushes.Window, new PointF[]
										{
											pointF3,
											pointF4,
											pointF5
										});
										e.Graphics.DrawPolygon(Pens.Maroon, new PointF[]
										{
											pointF3,
											pointF4,
											pointF5
										});
									}
									else
									{
										alpha = 60;
										width = 0.5f;
										pt9 = new PointF(pointF2.X, rectangleF3.Top);
									}
									using (Pen pen5 = new Pen(Color.FromArgb(alpha, Color.Maroon), width))
									{
										e.Graphics.DrawLine(pen5, pt8, pointF);
										e.Graphics.DrawLine(pen5, pointF2, pt9);
										switch (this.fLinkStyle)
										{
										case PlotViewLinkStyle.Curved:
										{
											bool flag = false;
											if (this.fLinkPaths.ContainsKey(current.ID))
											{
												Dictionary<Guid, List<PointF>> dictionary = this.fLinkPaths[current.ID];
												if (dictionary.ContainsKey(current2))
												{
													List<PointF> list2 = dictionary[current2];
													e.Graphics.DrawCurve(pen5, list2.ToArray());
													flag = true;
												}
											}
											if (!flag)
											{
												e.Graphics.DrawLine(pen5, pointF, pointF2);
											}
											break;
										}
										case PlotViewLinkStyle.Angled:
										{
											bool flag2 = false;
											if (this.fLinkPaths.ContainsKey(current.ID))
											{
												Dictionary<Guid, List<PointF>> dictionary2 = this.fLinkPaths[current.ID];
												if (dictionary2.ContainsKey(current2))
												{
													List<PointF> list3 = dictionary2[current2];
													e.Graphics.DrawLines(pen5, list3.ToArray());
													flag2 = true;
												}
											}
											if (!flag2)
											{
												e.Graphics.DrawLine(pen5, pointF, pointF2);
											}
											break;
										}
										case PlotViewLinkStyle.Straight:
											e.Graphics.DrawLine(pen5, pointF, pointF2);
											break;
										}
									}
								}
							}
						}
					}
					foreach (PlotPoint current3 in this.fPlot.Points)
					{
						if (this.fRegions.ContainsKey(current3.ID))
						{
							RectangleF rectangleF4 = this.fRegions[current3.ID];
							int alpha2 = 255;
							if (current3.State != PlotPointState.Normal)
							{
								alpha2 = 50;
							}
							Brush brush4 = null;
							if (current3 == this.fSelectedPoint)
							{
								brush4 = Brushes.White;
							}
							else
							{
								Pair<Color, Color> colourGradient = PlotView.GetColourGradient(current3.Colour, alpha2);
								brush4 = new LinearGradientBrush(rectangleF4, colourGradient.First, colourGradient.Second, LinearGradientMode.Vertical);
							}
							Pen pen6 = (current3 == this.fHoverPoint) ? SystemPens.Highlight : SystemPens.WindowText;
							Font font2 = (current3 != this.fSelectedPoint) ? this.Font : new Font(this.Font, this.Font.Style | FontStyle.Bold);
							if (current3.State == PlotPointState.Skipped)
							{
								font2 = new Font(font2, this.Font.Style | FontStyle.Strikeout);
							}
							if (current3.Element != null)
							{
								int partyLevel = Workspace.GetPartyLevel(current3);
								Difficulty difficulty = current3.Element.GetDifficulty(partyLevel, Session.Project.Party.Size);
								if ((difficulty == Difficulty.Trivial || difficulty == Difficulty.Extreme) && current3 != this.fSelectedPoint)
								{
									brush4 = new SolidBrush(Color.FromArgb(alpha2, Color.Pink));
									pen6 = Pens.Red;
								}
							}
							if (this.draw_point(current3))
							{
								Brush brush5 = SystemBrushes.WindowText;
								if (current3.State == PlotPointState.Normal)
								{
									RectangleF rect = new RectangleF(rectangleF4.Location, rectangleF4.Size);
									rect.Offset(3f, 4f);
									using (Brush brush6 = new SolidBrush(Color.FromArgb(100, Color.Black)))
									{
										e.Graphics.FillRectangle(brush6, rect);
										goto IL_CD4;
									}
									goto IL_CB7;
								}
								goto IL_CB7;
								IL_CD4:
								e.Graphics.FillRectangle(brush4, rectangleF4);
								e.Graphics.DrawRectangle(pen6, rectangleF4.X, rectangleF4.Y, rectangleF4.Width, rectangleF4.Height);
								float num7 = font2.Size;
								while (e.Graphics.MeasureString(current3.Name, font2, (int)rectangleF4.Width).Height > rectangleF4.Height)
								{
									num7 *= 0.95f;
									font2 = new Font(font2.FontFamily, num7, font2.Style);
								}
								e.Graphics.DrawString(current3.Name, font2, brush5, rectangleF4, this.fCentred);
								if (current3.Subplot.Points.Count > 0)
								{
									rectangleF4 = RectangleF.Inflate(rectangleF4, -2f, -2f);
									e.Graphics.DrawRectangle(pen6, rectangleF4.X, rectangleF4.Y, rectangleF4.Width, rectangleF4.Height);
								}
								if (current3.Details != "" || current3.ReadAloud != "")
								{
									double num8 = Math.Atan(0.25) * 2.0;
									float num9 = 20f - (float)(20.0 * Math.Cos(num8));
									float num10 = (float)(20.0 * Math.Sin(num8));
									PointF pointF6 = new PointF(rectangleF4.Right - 20f, rectangleF4.Bottom);
									PointF pointF7 = new PointF(rectangleF4.Right, rectangleF4.Bottom - 5f);
									PointF pointF8 = new PointF(rectangleF4.Right - num9, rectangleF4.Bottom - num10);
									PointF pointF9 = new PointF(rectangleF4.Right, rectangleF4.Bottom);
									e.Graphics.DrawPolygon(Pens.Gray, new PointF[]
									{
										pointF8,
										pointF7,
										pointF6
									});
									using (Brush brush7 = new SolidBrush(Color.FromArgb(80, 0, 0, 0)))
									{
										e.Graphics.FillPolygon(brush7, new PointF[]
										{
											pointF7,
											pointF6,
											pointF9
										});
										continue;
									}
									goto IL_F55;
								}
								continue;
								IL_CB7:
								if (current3 != this.fSelectedPoint)
								{
									brush5 = new SolidBrush(Color.FromArgb(alpha2, Color.Black));
									goto IL_CD4;
								}
								goto IL_CD4;
							}
							IL_F55:
							using (new Pen(Color.FromArgb(60, pen6.Color)))
							{
								e.Graphics.DrawRectangle(pen6, rectangleF4.X, rectangleF4.Y, rectangleF4.Width, rectangleF4.Height);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void clear_layout_calculations()
		{
			this.fUpRect = Rectangle.Empty;
			this.fDownRect = Rectangle.Empty;
			this.fLayers = null;
			this.fRegions = null;
			this.fLinkPaths = null;
		}

		private void do_layout_calculations()
		{
			try
			{
				this.clear_layout_calculations();
				this.fUpRect = new Rectangle(base.ClientRectangle.Right - 35, base.ClientRectangle.Top + 15, 25, 20);
				this.fDownRect = new Rectangle(base.ClientRectangle.Right - 35, base.ClientRectangle.Top + 40, 25, 20);
				this.fLayers = Workspace.FindLayers(this.fPlot);
				this.fRegions = new Dictionary<Guid, RectangleF>();
				int num = this.fLayers.Count * 2 + 1;
				float num2 = (float)(base.ClientRectangle.Height - 1) / (float)num;
				foreach (List<PlotPoint> current in this.fLayers)
				{
					int num3 = this.fLayers.IndexOf(current) * 2 + 1;
					float y = (float)num3 * num2;
					RectangleF rectangleF = new RectangleF((float)base.ClientRectangle.X, y, (float)base.ClientRectangle.Width, num2);
					int num4 = current.Count * 2 + 1;
					float num5 = rectangleF.Width / (float)num4;
					foreach (PlotPoint current2 in current)
					{
						int num6 = current.IndexOf(current2) * 2 + 1;
						float x = (float)num6 * num5;
						RectangleF value = new RectangleF(x, y, num5, num2);
						this.fRegions[current2.ID] = value;
					}
				}
				if (this.fLinkStyle != PlotViewLinkStyle.Straight)
				{
					this.fLinkPaths = new Dictionary<Guid, Dictionary<Guid, List<PointF>>>();
					foreach (PlotPoint current3 in this.fPlot.Points)
					{
						if (this.fRegions.ContainsKey(current3.ID))
						{
							Dictionary<Guid, List<PointF>> dictionary = new Dictionary<Guid, List<PointF>>();
							foreach (Guid current4 in current3.Links)
							{
								if (this.fRegions.ContainsKey(current4))
								{
									RectangleF rectangleF2 = this.fRegions[current3.ID];
									RectangleF rectangleF3 = this.fRegions[current4];
									PointF pointF = new PointF(rectangleF2.X + rectangleF2.Width / 2f, rectangleF2.Bottom + 12f);
									PointF pointF2 = new PointF(rectangleF3.X + rectangleF3.Width / 2f, rectangleF3.Top - 12f);
									List<PointF> list = new List<PointF>();
									list.Add(pointF);
									bool flag = false;
									while (!flag)
									{
										PlotPoint plotPoint = null;
										int num7 = this.find_layer_index(current4, this.fLayers) - this.find_layer_index(current3.ID, this.fLayers);
										if (num7 > 1)
										{
											plotPoint = this.get_blocking_point(pointF, pointF2);
										}
										if (plotPoint == null)
										{
											flag = true;
										}
										else
										{
											RectangleF rectangleF4 = this.fRegions[plotPoint.ID];
											int index = this.find_layer_index(plotPoint.ID, this.fLayers);
											List<PlotPoint> list2 = this.fLayers[index];
											float num8 = rectangleF4.Width / 3f;
											if (list2.Count == 1)
											{
												num8 = rectangleF4.Width / 6f;
											}
											float num9 = (float)Math.Round((double)(rectangleF4.Left + rectangleF4.Width / 2f));
											float x2 = (num9 >= pointF2.X) ? (rectangleF4.Left - num8) : (rectangleF4.Right + num8);
											PointF item = new PointF(x2, rectangleF4.Top);
											PointF pointF3 = new PointF(x2, rectangleF4.Bottom);
											list.Add(item);
											list.Add(pointF3);
											pointF = pointF3;
										}
									}
									list.Add(pointF2);
									dictionary[current4] = list;
								}
							}
							this.fLinkPaths[current3.ID] = dictionary;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		protected override void OnResize(EventArgs e)
		{
			try
			{
				base.OnResize(e);
				this.clear_layout_calculations();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			try
			{
				this.fHoverPoint = null;
				base.Invalidate();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			try
			{
				if (this.fMode != PlotViewMode.Plain)
				{
					Point pt = base.PointToClient(Cursor.Position);
					if (this.fUpRect.Contains(pt))
					{
						PlotPoint plotPoint = Session.Project.FindParent(this.fPlot);
						if (plotPoint != null)
						{
							Plot plot = Session.Project.FindParent(plotPoint);
							if (plot != null)
							{
								this.Plot = plot;
								this.OnPlotChanged();
							}
						}
					}
					if (this.fDownRect.Contains(pt) && this.fSelectedPoint != null)
					{
						this.Plot = this.fSelectedPoint.Subplot;
						this.OnPlotChanged();
					}
					PlotPoint plotPoint2 = this.find_point_at(pt);
					if (this.fSelectedPoint != plotPoint2)
					{
						this.fSelectedPoint = plotPoint2;
						base.Invalidate();
						this.OnSelectionChanged();
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
			try
			{
				if (this.fMode != PlotViewMode.Plain)
				{
					Point pt = base.PointToClient(Cursor.Position);
					PlotPoint plotPoint = this.find_point_at(pt);
					if (this.fHoverPoint != plotPoint)
					{
						this.fHoverPoint = plotPoint;
						this.set_tooltip();
					}
					if (e.Button == MouseButtons.Left && this.fSelectedPoint != null)
					{
						base.DoDragDrop(this.fSelectedPoint, DragDropEffects.All);
					}
					base.Invalidate();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		protected override void OnDragOver(DragEventArgs e)
		{
			try
			{
				Point pt = base.PointToClient(Cursor.Position);
				this.fHoverPoint = this.find_point_at(pt);
				PlotPoint dragged = e.Data.GetData(typeof(PlotPoint)) as PlotPoint;
				e.Effect = DragDropEffects.None;
				if (this.fHoverPoint != null)
				{
					if (this.allow_drop(dragged, this.fHoverPoint))
					{
						e.Effect = DragDropEffects.Move;
					}
				}
				else
				{
					this.fDragLocation = this.allow_drop(dragged, pt);
					if (this.fDragLocation != null)
					{
						e.Effect = DragDropEffects.Move;
					}
				}
				base.Invalidate();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		protected override void OnDragDrop(DragEventArgs e)
		{
			try
			{
				PlotPoint plotPoint = e.Data.GetData(typeof(PlotPoint)) as PlotPoint;
				if (this.fHoverPoint != null)
				{
					if (this.allow_drop(plotPoint, this.fHoverPoint))
					{
						this.fHoverPoint.Links.Add(plotPoint.ID);
						this.OnPlotLayoutChanged();
					}
				}
				else
				{
					if (this.fDragLocation != null)
					{
						this.fPlot.Points.Remove(plotPoint);
						if (this.fDragLocation.RHS != null)
						{
							int index = this.fPlot.Points.IndexOf(this.fDragLocation.RHS);
							this.fPlot.Points.Insert(index, plotPoint);
							this.OnPlotLayoutChanged();
						}
						else
						{
							this.fPlot.Points.Add(plotPoint);
							this.OnPlotLayoutChanged();
						}
					}
					this.fDragLocation = null;
				}
				this.do_layout_calculations();
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private bool allow_drop(PlotPoint dragged, PlotPoint target)
		{
			try
			{
				if (dragged == target)
				{
					bool result = false;
					return result;
				}
				if (target.Links.Contains(dragged.ID))
				{
					bool result = false;
					return result;
				}
				List<PlotPoint> list = this.fPlot.FindSubtree(dragged);
				if (list.Contains(target))
				{
					bool result = false;
					return result;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return true;
		}

		private PlotView.DragLocation allow_drop(PlotPoint dragged, Point pt)
		{
			try
			{
				RectangleF rectangleF = this.fRegions[dragged.ID];
				RectangleF rectangleF2 = new RectangleF(0f, rectangleF.Y, (float)base.ClientRectangle.Width, rectangleF.Height);
				if (!rectangleF2.Contains(pt))
				{
					PlotView.DragLocation result = null;
					return result;
				}
				List<PlotPoint> list = new List<PlotPoint>();
				foreach (PlotPoint current in this.fPlot.Points)
				{
					RectangleF rect = this.fRegions[current.ID];
					if (rectangleF2.Contains(rect))
					{
						if (rect.Contains(pt))
						{
							PlotView.DragLocation result = null;
							return result;
						}
						list.Add(current);
					}
				}
				if (list.Count == 0)
				{
					PlotView.DragLocation result = null;
					return result;
				}
				List<Pair<PlotPoint, PlotPoint>> list2 = new List<Pair<PlotPoint, PlotPoint>>();
				foreach (PlotPoint current2 in list)
				{
					int num = list.IndexOf(current2);
					if (num == 0)
					{
						list2.Add(new Pair<PlotPoint, PlotPoint>(null, current2));
					}
					else
					{
						list2.Add(new Pair<PlotPoint, PlotPoint>(list[num - 1], current2));
					}
					if (num == list.Count - 1)
					{
						list2.Add(new Pair<PlotPoint, PlotPoint>(current2, null));
					}
				}
				foreach (Pair<PlotPoint, PlotPoint> current3 in list2)
				{
					if (current3.First != dragged && current3.Second != dragged)
					{
						float num2 = 0f;
						float num3 = (float)base.ClientRectangle.Width;
						if (current3.First != null)
						{
							num2 = this.fRegions[current3.First.ID].Right;
						}
						if (current3.Second != null)
						{
							num3 = this.fRegions[current3.Second.ID].Left;
						}
						RectangleF rect2 = new RectangleF(num2, rectangleF2.Y, num3 - num2, rectangleF2.Height);
						if (rect2.Contains(pt))
						{
							PlotView.DragLocation result = new PlotView.DragLocation
							{
								LHS = current3.First,
								RHS = current3.Second,
								Rect = rect2
							};
							return result;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return null;
		}

		private PlotPoint find_point_at(Point pt)
		{
			try
			{
				if (this.fRegions == null)
				{
					this.do_layout_calculations();
				}
				foreach (Guid current in this.fRegions.Keys)
				{
					if (this.fRegions[current].Contains(pt))
					{
						return this.fPlot.FindPoint(current);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return null;
		}

		private bool draw_point(PlotPoint pp)
		{
			try
			{
				if (this.fFilter != "")
				{
					bool result = this.match_filter(pp);
					return result;
				}
				if (this.fMode == PlotViewMode.HighlightSelected)
				{
					bool result = this.fSelectedPoint == null || pp == this.fSelectedPoint || this.fSelectedPoint.Links.Contains(pp.ID) || pp.Links.Contains(this.fSelectedPoint.ID);
					return result;
				}
				if (this.fMode == PlotViewMode.HighlightEncounter)
				{
					bool result = pp.Element != null && pp.Element is Encounter;
					return result;
				}
				if (this.fMode == PlotViewMode.HighlightTrap)
				{
					if (pp.Element == null)
					{
						bool result = false;
						return result;
					}
					if (pp.Element is TrapElement)
					{
						bool result = true;
						return result;
					}
					if (pp.Element is Encounter)
					{
						Encounter encounter = pp.Element as Encounter;
						bool result = encounter.Traps.Count != 0;
						return result;
					}
				}
				if (this.fMode == PlotViewMode.HighlightChallenge)
				{
					if (pp.Element == null)
					{
						bool result = false;
						return result;
					}
					if (pp.Element is SkillChallenge)
					{
						bool result = true;
						return result;
					}
					if (pp.Element is Encounter)
					{
						Encounter encounter2 = pp.Element as Encounter;
						bool result = encounter2.SkillChallenges.Count != 0;
						return result;
					}
				}
				if (this.fMode == PlotViewMode.HighlightQuest)
				{
					bool result = pp.Element != null && pp.Element is Quest;
					return result;
				}
				if (this.fMode == PlotViewMode.HighlightParcel)
				{
					bool result = pp.Parcels.Count != 0;
					return result;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return true;
		}

		private bool draw_link(PlotPoint pp1, PlotPoint pp2)
		{
			try
			{
				if (this.fFilter != "")
				{
					bool result = this.draw_point(pp1) && this.draw_point(pp2);
					return result;
				}
				if (this.fMode == PlotViewMode.HighlightSelected)
				{
					bool result = this.fSelectedPoint == null || pp1 == this.fSelectedPoint || pp2 == this.fSelectedPoint;
					return result;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return true;
		}

		private bool match_filter(PlotPoint pp)
		{
			try
			{
				string[] array = this.fFilter.Split(new char[0]);
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string token = array2[i];
					if (!this.match_token(pp, token))
					{
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return true;
		}

		private bool match_token(PlotPoint pp, string token)
		{
			try
			{
				token = token.ToLower();
				if (pp.Name.ToLower().Contains(token))
				{
					bool result = true;
					return result;
				}
				if (pp.Details.ToLower().Contains(token))
				{
					bool result = true;
					return result;
				}
				if (pp.ReadAloud.ToLower().Contains(token))
				{
					bool result = true;
					return result;
				}
				if (pp.Element is Encounter)
				{
					Encounter encounter = pp.Element as Encounter;
					foreach (EncounterSlot current in encounter.Slots)
					{
						ICreature creature = Session.FindCreature(current.Card.CreatureID, SearchType.Global);
						if (creature.Name.ToLower().Contains(token))
						{
							bool result = true;
							return result;
						}
					}
					foreach (EncounterNote current2 in encounter.Notes)
					{
						if (current2.Contents.ToLower().Contains(token))
						{
							bool result = true;
							return result;
						}
					}
				}
				if (pp.Element is SkillChallenge)
				{
					SkillChallenge skillChallenge = pp.Element as SkillChallenge;
					if (skillChallenge.Success.ToLower().Contains(token))
					{
						bool result = true;
						return result;
					}
					if (skillChallenge.Failure.ToLower().Contains(token))
					{
						bool result = true;
						return result;
					}
					foreach (SkillChallengeData current3 in skillChallenge.Skills)
					{
						if (current3.SkillName.ToLower().Contains(token))
						{
							bool result = true;
							return result;
						}
						if (current3.Details.ToLower().Contains(token))
						{
							bool result = true;
							return result;
						}
					}
				}
				if (pp.Element is TrapElement)
				{
					TrapElement trapElement = pp.Element as TrapElement;
					if (trapElement.Trap.Name.ToLower().Contains(token))
					{
						bool result = true;
						return result;
					}
					foreach (TrapSkillData current4 in trapElement.Trap.Skills)
					{
						if (current4.SkillName.ToLower().Contains(token))
						{
							bool result = true;
							return result;
						}
						if (current4.Details.ToLower().Contains(token))
						{
							bool result = true;
							return result;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return false;
		}

		private void set_tooltip()
		{
			try
			{
				if (this.fShowTooltips && this.fHoverPoint != null)
				{
					List<string> list = new List<string>();
					if (this.fHoverPoint.Element != null)
					{
						if (this.fHoverPoint.Element is Encounter)
						{
							Encounter encounter = this.fHoverPoint.Element as Encounter;
							string text = "Encounter: " + encounter.GetXP() + " XP";
							foreach (EncounterSlot current in encounter.Slots)
							{
								ICreature creature = Session.FindCreature(current.Card.CreatureID, SearchType.Global);
								if (creature != null)
								{
									text = text + Environment.NewLine + creature.Name;
									if (current.CombatData.Count > 1)
									{
										object obj = text;
										text = string.Concat(new object[]
										{
											obj,
											" (x",
											current.CombatData.Count,
											")"
										});
									}
								}
							}
							foreach (Trap current2 in encounter.Traps)
							{
								string text2 = text;
								text = string.Concat(new string[]
								{
									text2,
									Environment.NewLine,
									current2.Name,
									": ",
									current2.Info
								});
							}
							foreach (SkillChallenge current3 in encounter.SkillChallenges)
							{
								string text3 = text;
								text = string.Concat(new string[]
								{
									text3,
									Environment.NewLine,
									current3.Name,
									": ",
									current3.Info
								});
							}
							list.Add(text);
						}
						if (this.fHoverPoint.Element is TrapElement)
						{
							TrapElement trapElement = this.fHoverPoint.Element as TrapElement;
							string text4 = string.Concat(new object[]
							{
								trapElement.Trap.Name,
								": ",
								trapElement.GetXP(),
								" XP"
							});
							string text5 = text4;
							text4 = string.Concat(new string[]
							{
								text5,
								Environment.NewLine,
								trapElement.Trap.Info,
								" ",
								trapElement.Trap.Type.ToString().ToLower()
							});
							list.Add(text4);
						}
						if (this.fHoverPoint.Element is SkillChallenge)
						{
							SkillChallenge skillChallenge = this.fHoverPoint.Element as SkillChallenge;
							string text6 = string.Concat(new object[]
							{
								skillChallenge.Name,
								": ",
								skillChallenge.GetXP(),
								" XP"
							});
							text6 = text6 + Environment.NewLine + skillChallenge.Info;
							list.Add(text6);
						}
						if (this.fHoverPoint.Element is Quest)
						{
							Quest quest = this.fHoverPoint.Element as Quest;
							string item = "";
							switch (quest.Type)
							{
							case QuestType.Major:
								item = "Major quest: " + quest.GetXP() + " XP";
								break;
							case QuestType.Minor:
								item = "Minor quest: " + quest.GetXP() + " XP";
								break;
							}
							list.Add(item);
						}
					}
					string text7 = "";
					foreach (Parcel current4 in this.fHoverPoint.Parcels)
					{
						if (text7 != "")
						{
							text7 += ", ";
						}
						text7 += current4.Name;
					}
					if (text7 != "")
					{
						list.Add("Treasure parcels: " + text7);
					}
					string text8 = "";
					foreach (string current5 in list)
					{
						if (text8 != "")
						{
							text8 = text8 + Environment.NewLine + Environment.NewLine;
						}
						text8 += TextHelper.Wrap(current5);
					}
					this.Tooltip.ToolTipTitle = this.fHoverPoint.Name;
					this.Tooltip.ToolTipIcon = ToolTipIcon.Info;
					this.Tooltip.SetToolTip(this, text8);
				}
				else
				{
					this.Tooltip.ToolTipTitle = "";
					this.Tooltip.ToolTipIcon = ToolTipIcon.None;
					this.Tooltip.SetToolTip(this, null);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private int find_layer_index(Guid point_id, List<List<PlotPoint>> layers)
		{
			try
			{
				for (int num = 0; num != layers.Count; num++)
				{
					List<PlotPoint> list = layers[num];
					foreach (PlotPoint current in list)
					{
						if (current.ID == point_id)
						{
							return num;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return -1;
		}

		private PlotPoint get_blocking_point(PointF from_pt, PointF to_pt)
		{
			try
			{
				List<PlotPoint> list = this.find_layer(from_pt.Y);
				List<PlotPoint> list2 = this.find_layer(to_pt.Y);
				if (list == null || list == list2)
				{
					PlotPoint result = null;
					return result;
				}
				if (list != null)
				{
					int num = this.fLayers.IndexOf(list);
					int val = this.fLayers.IndexOf(list2);
					int num2 = Math.Min(val, this.fLayers.Count) - 1;
					for (int i = num; i <= num2; i++)
					{
						list = this.fLayers[i];
						PlotPoint plotPoint = list[0];
						RectangleF rectangleF = this.fRegions[plotPoint.ID];
						float top = rectangleF.Top;
						float bottom = rectangleF.Bottom;
						float num3 = to_pt.X - from_pt.X;
						float num4 = to_pt.Y - from_pt.Y;
						float num5 = (top - from_pt.Y) / num4;
						float num6 = (bottom - from_pt.Y) / num4;
						float x = from_pt.X + num5 * num3;
						float x2 = from_pt.X + num6 * num3;
						PointF pt = new PointF(x, top);
						PointF pt2 = new PointF(x2, bottom);
						foreach (PlotPoint current in list)
						{
							RectangleF rect = this.fRegions[current.ID];
							RectangleF rectangleF2 = RectangleF.Inflate(rect, 2f, 2f);
							if (rectangleF2.Contains(pt) || rectangleF2.Contains(pt2))
							{
								PlotPoint result = current;
								return result;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return null;
		}

		private List<PlotPoint> find_layer(float y)
		{
			try
			{
				foreach (List<PlotPoint> current in this.fLayers)
				{
					PlotPoint plotPoint = current[0];
					if (y < this.fRegions[plotPoint.ID].Bottom)
					{
						return current;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return null;
		}

		internal static Pair<Color, Color> GetColourGradient(PlotPointColour colour, int alpha)
		{
			Color first = Color.White;
			Color second = Color.Black;
			switch (colour)
			{
			case PlotPointColour.Yellow:
				first = Color.FromArgb(alpha, 255, 255, 215);
				second = Color.FromArgb(alpha, 255, 255, 165);
				break;
			case PlotPointColour.Blue:
				first = Color.FromArgb(alpha, 215, 215, 255);
				second = Color.FromArgb(alpha, 165, 165, 255);
				break;
			case PlotPointColour.Green:
				first = Color.FromArgb(alpha, 215, 255, 215);
				second = Color.FromArgb(alpha, 165, 255, 165);
				break;
			case PlotPointColour.Purple:
				first = Color.FromArgb(alpha, 240, 205, 255);
				second = Color.FromArgb(alpha, 240, 150, 255);
				break;
			case PlotPointColour.Orange:
				first = Color.FromArgb(alpha, 255, 240, 210);
				second = Color.FromArgb(alpha, 255, 165, 120);
				break;
			case PlotPointColour.Brown:
				first = Color.FromArgb(alpha, 255, 240, 215);
				second = Color.FromArgb(alpha, 170, 140, 110);
				break;
			case PlotPointColour.Grey:
				first = Color.FromArgb(alpha, 225, 225, 225);
				second = Color.FromArgb(alpha, 175, 175, 175);
				break;
			}
			return new Pair<Color, Color>(first, second);
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
			this.Tooltip = new ToolTip(this.components);
			base.SuspendLayout();
			this.AllowDrop = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "PlotView";
			base.ResumeLayout(false);
		}
	}
}
