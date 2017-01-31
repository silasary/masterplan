using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Utils.Graphics
{
	public abstract class RoundedRectangle
	{
		public enum RectangleCorners
		{
			None,
			TopLeft,
			TopRight,
			BottomLeft = 4,
			BottomRight = 8,
			All = 15
		}

		public static GraphicsPath Create(float x, float y, float width, float height, float radius, RoundedRectangle.RectangleCorners corners)
		{
			float num = x + width;
			float num2 = y + height;
			float num3 = num - radius;
			float num4 = num2 - radius;
			float num5 = x + radius;
			float num6 = y + radius;
			float num7 = radius * 2f;
			float x2 = num - num7;
			float y2 = num2 - num7;
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.StartFigure();
			if ((RoundedRectangle.RectangleCorners.TopLeft & corners) == RoundedRectangle.RectangleCorners.TopLeft)
			{
				graphicsPath.AddArc(x, y, num7, num7, 180f, 90f);
			}
			else
			{
				graphicsPath.AddLine(x, num6, x, y);
				graphicsPath.AddLine(x, y, num5, y);
			}
			graphicsPath.AddLine(num5, y, num3, y);
			if ((RoundedRectangle.RectangleCorners.TopRight & corners) == RoundedRectangle.RectangleCorners.TopRight)
			{
				graphicsPath.AddArc(x2, y, num7, num7, 270f, 90f);
			}
			else
			{
				graphicsPath.AddLine(num3, y, num, y);
				graphicsPath.AddLine(num, y, num, num6);
			}
			graphicsPath.AddLine(num, num6, num, num4);
			if ((RoundedRectangle.RectangleCorners.BottomRight & corners) == RoundedRectangle.RectangleCorners.BottomRight)
			{
				graphicsPath.AddArc(x2, y2, num7, num7, 0f, 90f);
			}
			else
			{
				graphicsPath.AddLine(num, num4, num, num2);
				graphicsPath.AddLine(num, num2, num3, num2);
			}
			graphicsPath.AddLine(num3, num2, num5, num2);
			if ((RoundedRectangle.RectangleCorners.BottomLeft & corners) == RoundedRectangle.RectangleCorners.BottomLeft)
			{
				graphicsPath.AddArc(x, y2, num7, num7, 90f, 90f);
			}
			else
			{
				graphicsPath.AddLine(num5, num2, x, num2);
				graphicsPath.AddLine(x, num2, x, num4);
			}
			graphicsPath.AddLine(x, num4, x, num6);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		public static GraphicsPath Create(RectangleF rect, float radius, RoundedRectangle.RectangleCorners corners)
		{
			return RoundedRectangle.Create(rect.X, rect.Y, rect.Width, rect.Height, radius, corners);
		}

		public static GraphicsPath Create(float x, float y, float width, float height, float radius)
		{
			return RoundedRectangle.Create(x, y, width, height, radius, RoundedRectangle.RectangleCorners.All);
		}

		public static GraphicsPath Create(RectangleF rect, float radius)
		{
			return RoundedRectangle.Create(rect.X, rect.Y, rect.Width, rect.Height, radius);
		}
	}
}
