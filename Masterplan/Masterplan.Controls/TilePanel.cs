using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class TilePanel : UserControl
	{
		private IContainer components;

		private Image fTileImage;

		private Color fTileColour = Color.White;

		private Size fTileSize = new Size(2, 2);

		private bool fShowGridlines = true;

		public Image TileImage
		{
			get
			{
				return this.fTileImage;
			}
			set
			{
				this.fTileImage = value;
				base.Invalidate();
			}
		}

		public Color TileColour
		{
			get
			{
				return this.fTileColour;
			}
			set
			{
				this.fTileColour = value;
				base.Invalidate();
			}
		}

		public Size TileSize
		{
			get
			{
				return this.fTileSize;
			}
			set
			{
				this.fTileSize = value;
				base.Invalidate();
			}
		}

		public bool ShowGridlines
		{
			get
			{
				return this.fShowGridlines;
			}
			set
			{
				this.fShowGridlines = value;
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

		public TilePanel()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			e.Graphics.FillRectangle(new SolidBrush(this.BackColor), base.ClientRectangle);
			double val = (double)base.ClientRectangle.Width / (double)this.fTileSize.Width;
			double val2 = (double)base.ClientRectangle.Height / (double)this.fTileSize.Height;
			float num = (float)Math.Min(val, val2);
			float num2 = num * (float)this.fTileSize.Width;
			float num3 = num * (float)this.fTileSize.Height;
			float num4 = ((float)base.ClientRectangle.Width - num2) / 2f;
			float num5 = ((float)base.ClientRectangle.Height - num3) / 2f;
			RectangleF rect = new RectangleF(num4, num5, num2, num3);
			if (this.fTileImage != null)
			{
				e.Graphics.DrawImage(this.fTileImage, rect);
			}
			else
			{
				using (Brush brush = new SolidBrush(this.fTileColour))
				{
					e.Graphics.FillRectangle(brush, rect);
				}
				using (Pen pen = new Pen(Color.Black, 2f))
				{
					e.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
				}
			}
			if (this.fShowGridlines)
			{
				using (Pen pen2 = new Pen(Color.DarkGray))
				{
					for (int num6 = 1; num6 != this.fTileSize.Width; num6++)
					{
						float num7 = num4 + (float)num6 * num;
						e.Graphics.DrawLine(pen2, num7, num5, num7, num5 + num3);
					}
					for (int num8 = 1; num8 != this.fTileSize.Height; num8++)
					{
						float num9 = num5 + (float)num8 * num;
						e.Graphics.DrawLine(pen2, num4, num9, num4 + num2, num9);
					}
				}
			}
		}
	}
}
