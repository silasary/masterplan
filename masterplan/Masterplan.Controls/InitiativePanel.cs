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
	internal class InitiativePanel : UserControl
	{
		private const int BORDER = 8;

		private IContainer components;

		private int fHoveredInit = -2147483648;

		private StringFormat fCentred = new StringFormat();

		private Pen fTickPen = new Pen(Color.Gray, 0.5f);

		private List<int> fInitiatives = new List<int>();

		private int fCurrent;

        public event EventHandler InitiativeChanged;

		public List<int> InitiativeScores
		{
			get
			{
				return this.fInitiatives;
			}
			set
			{
				this.fInitiatives = value;
				base.Invalidate();
			}
		}

		public int CurrentInitiative
		{
			get
			{
				return this.fCurrent;
			}
			set
			{
				this.fCurrent = value;
				base.Invalidate();
			}
		}

		public int Minimum
		{
			get
			{
				Pair<int, int> range = this.get_range();
				return range.First;
			}
		}

		public int Maximum
		{
			get
			{
				Pair<int, int> range = this.get_range();
				return range.Second;
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

		public InitiativePanel()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
		}

		protected void OnInitiativeChanged()
		{
			if (this.InitiativeChanged != null)
			{
				this.InitiativeChanged(this, new EventArgs());
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			e.Graphics.FillRectangle(Brushes.White, base.ClientRectangle);
			float num = (float)(base.ClientRectangle.Right - 8);
			PointF pt = new PointF(num, 8f);
			PointF pt2 = new PointF(num, (float)(base.ClientRectangle.Bottom - 8));
			e.Graphics.DrawLine(Pens.Black, pt, pt2);
			Pair<int, int> range = this.get_range();
			for (int i = range.First; i <= range.Second; i++)
			{
				if (i % 5 == 0)
				{
					float y = this.get_y(i);
					PointF pt3 = new PointF(num - 5f, y);
					PointF pt4 = new PointF(num, y);
					e.Graphics.DrawLine(this.fTickPen, pt3, pt4);
				}
			}
			foreach (int current in this.fInitiatives)
			{
				float num2 = this.get_y(current);
				PointF pointF = new PointF(num, num2);
				PointF pointF2 = new PointF(num - 10f, num2 - 5f);
				PointF pointF3 = new PointF(num - 10f, num2 + 5f);
				e.Graphics.FillPolygon(Brushes.White, new PointF[]
				{
					pointF,
					pointF2,
					pointF3
				});
				e.Graphics.DrawPolygon(Pens.Gray, new PointF[]
				{
					pointF,
					pointF2,
					pointF3
				});
			}
			if (this.fCurrent != -2147483648)
			{
				float num3 = this.get_y(this.fCurrent);
				RectangleF rectangleF = new RectangleF(8f, num3 - 8f, (float)(base.ClientRectangle.Width - 16), 16f);
				using (Brush brush = new LinearGradientBrush(rectangleF, Color.Blue, Color.DarkBlue, LinearGradientMode.Vertical))
				{
					e.Graphics.FillRectangle(brush, rectangleF);
					e.Graphics.DrawRectangle(Pens.Black, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
					e.Graphics.DrawString(this.fCurrent.ToString(), this.Font, Brushes.White, rectangleF, this.fCentred);
				}
			}
			if (this.fHoveredInit != -2147483648 && this.fHoveredInit != this.fCurrent)
			{
				float num4 = this.get_y(this.fHoveredInit);
				RectangleF rectangleF2 = new RectangleF(8f, num4 - 8f, (float)(base.ClientRectangle.Width - 16), 16f);
				e.Graphics.FillRectangle(Brushes.White, rectangleF2);
				e.Graphics.DrawRectangle(Pens.Gray, rectangleF2.X, rectangleF2.Y, rectangleF2.Width, rectangleF2.Height);
				e.Graphics.DrawString(this.fHoveredInit.ToString(), this.Font, Brushes.Gray, rectangleF2, this.fCentred);
			}
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			this.fCurrent = this.get_score(base.PointToClient(Cursor.Position).Y);
			this.OnInitiativeChanged();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			this.fHoveredInit = this.get_score(base.PointToClient(Cursor.Position).Y);
			base.Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			this.fHoveredInit = -2147483648;
			base.Invalidate();
		}

		private Pair<int, int> get_range()
		{
			int num = 2147483647;
			int num2 = -2147483648;
			foreach (int current in this.fInitiatives)
			{
				num = Math.Min(num, current);
				num2 = Math.Max(num2, current);
			}
			if (this.fCurrent != -2147483648)
			{
				num = Math.Min(num, this.fCurrent);
				num2 = Math.Max(num2, this.fCurrent);
			}
			if (num == 2147483647)
			{
				num = 0;
			}
			if (num2 == -2147483648)
			{
				num2 = 20;
			}
			if (num == num2)
			{
				num -= 5;
				num2 += 5;
			}
			return new Pair<int, int>(num, num2);
		}

		private float get_y(int score)
		{
			RectangleF rectangleF = this.get_rect(score);
			return rectangleF.Top + rectangleF.Height / 2f;
		}

		private RectangleF get_rect(int score)
		{
			Pair<int, int> range = this.get_range();
			int num = range.Second - range.First + 1;
			int num2 = base.ClientRectangle.Height - 16;
			float num3 = (float)(num2 / num);
			int num4 = score - range.First;
			float num5 = (float)(base.ClientRectangle.Height - 8);
			num5 -= (float)num4 * num3;
			num5 -= num3;
			return new RectangleF(0f, num5, (float)base.ClientRectangle.Width, num3);
		}

		private int get_score(int y)
		{
			Pair<int, int> range = this.get_range();
			for (int i = range.First; i <= range.Second; i++)
			{
				RectangleF rectangleF = this.get_rect(i);
				if (rectangleF.Top <= (float)y && rectangleF.Bottom >= (float)y)
				{
					return i;
				}
			}
			return -2147483648;
		}
	}
}
