using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class DiceGraphPanel : UserControl
	{
		private List<int> fDice = new List<int>();

		private int fConstant;

		private string fTitle = "";

		private float fRange = 0.5f;

		private Dictionary<int, int> fDistribution;

		private StringFormat fCentred = new StringFormat();

		private IContainer components;

		public List<int> Dice
		{
			get
			{
				return this.fDice;
			}
			set
			{
				this.fDice = value;
				this.fDistribution = null;
				base.Invalidate();
			}
		}

		public int Constant
		{
			get
			{
				return this.fConstant;
			}
			set
			{
				this.fConstant = value;
				this.fDistribution = null;
				base.Invalidate();
			}
		}

		public string Title
		{
			get
			{
				return this.fTitle;
			}
			set
			{
				this.fTitle = value;
				base.Invalidate();
			}
		}

		public DiceGraphPanel()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			try
			{
				if (this.fDistribution == null)
				{
					this.fDistribution = DiceStatistics.Odds(this.fDice, this.fConstant);
				}
				if (this.fDistribution != null && this.fDistribution.Keys.Count != 0)
				{
					int num = base.Width / 10;
					int num2 = base.Height / 10;
					Rectangle rectangle = new Rectangle(num, 3 * num2, base.Width - 2 * num, base.Height - 5 * num2);
					if (this.fTitle != null && this.fTitle != "")
					{
						Rectangle rectangle2 = new Rectangle(rectangle.X, rectangle.Y - 2 * num2, rectangle.Width, num2);
						e.Graphics.FillRectangle(Brushes.White, rectangle2);
						e.Graphics.DrawRectangle(Pens.DarkGray, rectangle2);
						e.Graphics.DrawString(this.fTitle, new Font(this.Font.FontFamily, (float)(num2 / 3)), Brushes.Black, rectangle2, this.fCentred);
					}
					int num3 = 2147483647;
					int num4 = int.MinValue;
					int num5 = int.MinValue;
					int num6 = 0;
					foreach (int current in this.fDistribution.Keys)
					{
						num3 = Math.Min(num3, current);
						num4 = Math.Max(num4, current);
						num5 = Math.Max(num5, this.fDistribution[current]);
						num6 += this.fDistribution[current];
					}
					float num7 = (1f - this.fRange) / 2f;
					float num8 = 1f - num7;
					Point p = base.PointToClient(Cursor.Position);
					int num9 = num4 - num3 + 1;
					float num10 = (float)rectangle.Width / (float)num9;
					float emSize = Math.Min(this.Font.Size, num10 / 2f);
					Font font = new Font(this.Font.FontFamily, emSize);
					List<PointF> list = new List<PointF>();
					int num11 = 0;
					foreach (int current2 in this.fDistribution.Keys)
					{
						int num12 = current2 - num3;
						float num13 = num10 * (float)num12;
						float num14 = (float)(rectangle.Height * (num5 - this.fDistribution[current2]) / num5);
						RectangleF rectangleF = new RectangleF(num13 + (float)rectangle.X, (float)rectangle.Y + num14, num10, (float)rectangle.Height - num14);
						num11 += this.fDistribution[current2];
						float num15 = (float)num11 / (float)num6;
						bool flag = rectangleF.Contains(p);
						bool arg_2C8_0 = num15 >= num7 && num15 <= num8;
						bool flag2 = false;
						float num16 = num13 + (float)rectangle.X + num10 / 2f;
						float num17 = (float)rectangle.Y + num14;
						list.Add(new PointF(num16, num17));
						Pen pen = Pens.Gray;
						if (flag2 || flag)
						{
							pen = Pens.Black;
						}
						e.Graphics.DrawLine(pen, num16, (float)rectangle.Bottom, num16, num17);
						RectangleF layoutRectangle = new RectangleF(rectangleF.Left, rectangleF.Bottom, num10, (float)num2);
						e.Graphics.DrawString(current2.ToString(), font, flag ? Brushes.Black : Brushes.DarkGray, layoutRectangle, this.fCentred);
					}
					e.Graphics.DrawLine(Pens.Black, rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
					for (int i = 1; i < list.Count; i++)
					{
						e.Graphics.DrawLine(new Pen(Color.Red, 2f), list[i - 1], list[i]);
					}
				}
			}
			catch
			{
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			base.Invalidate();
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
	}
}
