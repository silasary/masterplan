using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Utils;

namespace Masterplan.Controls
{
	public class DemographicsPanel : UserControl
	{
		private IContainer components;

		private Library fLibrary;

		private DemographicsSource fSource;

		private DemographicsMode fMode;

		private StringFormat fCentred = new StringFormat();

		private Dictionary<string, int> fBreakdown;

		[Category("Data"), Description("The library to display.")]
		public Library Library
		{
			get
			{
				return this.fLibrary;
			}
			set
			{
				this.fLibrary = value;
				this.fBreakdown = null;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("The category of information to show.")]
		public DemographicsSource Source
		{
			get
			{
				return this.fSource;
			}
			set
			{
				this.fSource = value;
				this.fBreakdown = null;
				base.Invalidate();
			}
		}

		[Category("Appearance"), Description("The type of breakdown to show.")]
		public DemographicsMode Mode
		{
			get
			{
				return this.fMode;
			}
			set
			{
				this.fMode = value;
				this.fBreakdown = null;
				base.Invalidate();
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

		public DemographicsPanel()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
		}

		internal void ShowTable(ReportTable table)
		{
			this.fBreakdown = new Dictionary<string, int>();
			foreach (ReportRow current in table.Rows)
			{
				this.fBreakdown[current.Heading] = current.Total;
			}
			base.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (this.fBreakdown == null)
			{
				this.analyse_data();
			}
			e.Graphics.FillRectangle(Brushes.White, base.ClientRectangle);
			int count = this.fBreakdown.Keys.Count;
			if (count == 0)
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			foreach (string current in this.fBreakdown.Keys)
			{
				int val = this.fBreakdown[current];
				num2 = Math.Max(num2, val);
				num = Math.Min(num, val);
			}
			int num3 = num2 - num;
			if (num3 == 0)
			{
				return;
			}
			int num4 = 20;
			Rectangle rectangle = new Rectangle(num4, num4, base.ClientRectangle.Width - 2 * num4, base.ClientRectangle.Height - 3 * num4);
			float num5 = (float)rectangle.Width / (float)count;
			List<string> list = new List<string>();
			list.AddRange(this.fBreakdown.Keys);
			using (Font font = new Font(this.Font.FontFamily, this.Font.Size * 0.8f))
			{
				for (int num6 = 0; num6 != list.Count; num6++)
				{
					string text = list[num6];
					float num7 = num5 * (float)num6;
					RectangleF layoutRectangle = new RectangleF((float)rectangle.Left + num7, (float)rectangle.Bottom, num5, (float)num4);
					e.Graphics.DrawString(text, font, Brushes.Black, layoutRectangle, this.fCentred);
					int num8 = this.fBreakdown[text];
					if (num8 != 0)
					{
						Color color = (num8 >= 0) ? Color.LightGray : Color.White;
						Color color2 = (num8 >= 0) ? Color.White : Color.LightGray;
						int num9 = Math.Max(num8, 0);
						int num10 = Math.Min(num8, 0);
						int num11 = rectangle.Bottom - (rectangle.Height - num4) * (num9 - num) / num3;
						int num12 = (rectangle.Height - num4) * (num9 - num10) / num3;
						RectangleF rect = new RectangleF((float)rectangle.Left + num7, (float)num11, num5, (float)num12);
						using (Brush brush = new LinearGradientBrush(rect, color, color2, LinearGradientMode.Vertical))
						{
							e.Graphics.FillRectangle(brush, rect);
							e.Graphics.DrawRectangle(Pens.Gray, rect.X, rect.Y, rect.Width, rect.Height);
						}
						RectangleF layoutRectangle2 = new RectangleF((float)rectangle.Left + num7, (float)rectangle.Top, num5, (float)num4);
						e.Graphics.DrawString(num8.ToString(), font, Brushes.Gray, layoutRectangle2, this.fCentred);
					}
				}
			}
			int num13 = rectangle.Bottom - (rectangle.Height - num4) * -num / num3;
			e.Graphics.DrawLine(Pens.Black, rectangle.Left, num13, rectangle.Right, num13);
			e.Graphics.DrawLine(Pens.Black, rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Top);
		}

		private void analyse_data()
		{
			try
			{
				List<Library> list = new List<Library>();
				if (this.fLibrary == null)
				{
					list.AddRange(Session.Libraries);
				}
				else
				{
					list.Add(this.fLibrary);
				}
				this.fBreakdown = new Dictionary<string, int>();
				this.set_labels(list);
				foreach (Library current in list)
				{
					this.add_library(current);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private void set_labels(List<Library> libraries)
		{
			switch (this.fMode)
			{
			case DemographicsMode.Level:
			{
				int num = this.find_max_level(this.fSource, libraries);
				for (int i = 1; i <= num; i++)
				{
					this.fBreakdown[i.ToString()] = 0;
				}
				return;
			}
			case DemographicsMode.Role:
				switch (this.fSource)
				{
				case DemographicsSource.Creatures:
					this.fBreakdown["Artillery"] = 0;
					this.fBreakdown["Brute"] = 0;
					this.fBreakdown["Controller"] = 0;
					this.fBreakdown["Lurker"] = 0;
					this.fBreakdown["Skirmisher"] = 0;
					this.fBreakdown["Soldier"] = 0;
					return;
				case DemographicsSource.Traps:
					this.fBreakdown["Blaster"] = 0;
					this.fBreakdown["Lurker"] = 0;
					this.fBreakdown["Obstacle"] = 0;
					this.fBreakdown["Warder"] = 0;
					return;
				default:
					return;
				}
				break;
			case DemographicsMode.Status:
				this.fBreakdown["Standard"] = 0;
				this.fBreakdown["Elite"] = 0;
				this.fBreakdown["Solo"] = 0;
				this.fBreakdown["Minion"] = 0;
				this.fBreakdown["Leader"] = 0;
				return;
			default:
				return;
			}
		}

		private int find_max_level(DemographicsSource source, List<Library> libraries)
		{
			int num = 0;
            foreach (Library current in libraries)
            {
                switch (source)
                {
                    case DemographicsSource.Creatures:
                        foreach (var current2 in current.Creatures)
                        {
                            if (current2.Level > num)
                                {
                                    num = current2.Level;
                                }
                        }
                        break;
                    case DemographicsSource.Traps:
                        using (List<Trap>.Enumerator enumerator3 = current.Traps.GetEnumerator())
                        {
                            while (enumerator3.MoveNext())
                            {
                                Trap current3 = enumerator3.Current;
                                if (current3.Level > num)
                                {
                                    num = current3.Level;
                                }
                            }
                            continue;
                        }
                        break;
                    case DemographicsSource.MagicItems:
                        foreach (MagicItem current4 in current.MagicItems)
                        {
                            if (current4.Level > num)
                            {
                                num = current4.Level;
                            }
                        }
                        break;
                    default:
                        continue;
                }

            }
			return num;
		}

		private void add_library(Library library)
		{
			switch (this.fSource)
			{
			case DemographicsSource.Creatures:
				using (List<Creature>.Enumerator enumerator = library.Creatures.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Creature current = enumerator.Current;
						switch (this.fMode)
						{
						case DemographicsMode.Level:
							this.add(current.Level.ToString());
							break;
						case DemographicsMode.Role:
						case DemographicsMode.Status:
							this.analyse_role(current.Role);
							break;
						}
					}
					return;
				}
				break;
			case DemographicsSource.Traps:
				break;
			case DemographicsSource.MagicItems:
				goto IL_FD;
			default:
				return;
			}
			using (List<Trap>.Enumerator enumerator2 = library.Traps.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Trap current2 = enumerator2.Current;
					switch (this.fMode)
					{
					case DemographicsMode.Level:
						this.add(current2.Level.ToString());
						break;
					case DemographicsMode.Role:
					case DemographicsMode.Status:
						this.analyse_role(current2.Role);
						break;
					}
				}
				return;
			}
			IL_FD:
			foreach (MagicItem current3 in library.MagicItems)
			{
				DemographicsMode demographicsMode = this.fMode;
				if (demographicsMode == DemographicsMode.Level)
				{
					this.add(current3.Level.ToString());
				}
			}
		}

		private void analyse_role(IRole role)
		{
			ComplexRole complexRole = role as ComplexRole;
			if (complexRole != null)
			{
				switch (this.fMode)
				{
				case DemographicsMode.Role:
					this.add(complexRole.Type.ToString());
					break;
				case DemographicsMode.Status:
					this.add(complexRole.Flag.ToString());
					if (complexRole.Leader)
					{
						this.add("Leader");
					}
					break;
				}
			}
			Minion minion = role as Minion;
			if (minion != null)
			{
				switch (this.fMode)
				{
				case DemographicsMode.Role:
					if (minion.HasRole)
					{
						this.add(minion.Type.ToString());
						return;
					}
					break;
				case DemographicsMode.Status:
					this.add("Minion");
					break;
				default:
					return;
				}
			}
		}

		private void add(string label)
		{
			if (this.fBreakdown.ContainsKey(label))
			{
				fBreakdown[label] = fBreakdown[label] + 1;
			}
		}
	}
}
