using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class HitPointGauge : UserControl
	{
		private IContainer components;

		private int fFullHP;

		private int fDamage;

		private int fTempHP;

		public int FullHP
		{
			get
			{
				return this.fFullHP;
			}
			set
			{
				this.fFullHP = value;
				base.Invalidate();
			}
		}

		public int Damage
		{
			get
			{
				return this.fDamage;
			}
			set
			{
				this.fDamage = value;
				base.Invalidate();
			}
		}

		public int TempHP
		{
			get
			{
				return this.fTempHP;
			}
			set
			{
				this.fTempHP = value;
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

		public HitPointGauge()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.fFullHP == 0)
			{
				e.Graphics.DrawRectangle(Pens.Black, 0, 0, base.Width - 1, base.Height - 1);
				return;
			}
			int num = this.fFullHP - this.fDamage;
			int num2 = this.fFullHP / 2;
			int num3 = (int)((double)base.Width * 0.8);
			int num4 = this.get_level(0);
			int num5 = this.get_level(num2);
			int num6 = this.get_level(this.fFullHP);
			int val = this.get_level(num);
			if (this.fFullHP != 0)
			{
				Rectangle rect = new Rectangle(num3, num6, base.Width - num3, num4 - num6);
				Brush brush = new LinearGradientBrush(rect, Color.Black, Color.LightGray, LinearGradientMode.Horizontal);
				e.Graphics.FillRectangle(brush, rect);
			}
			if (num != 0)
			{
				int num7 = Math.Min(num4, val);
				int num8 = Math.Max(num4, val);
				Rectangle rect2 = new Rectangle(0, num7, num3, num8 - num7);
				Brush brush2;
				if (num > num2)
				{
					brush2 = new LinearGradientBrush(rect2, Color.Green, Color.DarkGreen, LinearGradientMode.Vertical);
				}
				else
				{
					brush2 = new LinearGradientBrush(rect2, Color.Red, Color.DarkRed, LinearGradientMode.Vertical);
				}
				e.Graphics.FillRectangle(brush2, rect2);
				e.Graphics.DrawRectangle(Pens.DarkGray, rect2);
			}
			if (this.fTempHP != 0)
			{
				int num9 = Math.Max(0, num + this.fTempHP);
				int num10 = this.get_level(num9);
				int value = num9 - this.fTempHP;
				int num11 = this.get_level(value);
				Rectangle rect3 = new Rectangle(0, num10, num3, num11 - num10);
				Brush brush3 = new LinearGradientBrush(rect3, Color.Blue, Color.Navy, LinearGradientMode.Vertical);
				e.Graphics.FillRectangle(brush3, rect3);
				e.Graphics.DrawRectangle(Pens.DarkGray, rect3);
			}
			if (this.fFullHP != 0)
			{
				e.Graphics.DrawLine(Pens.DarkGray, 0, num4, num3, num4);
				e.Graphics.DrawLine(Pens.DarkGray, 0, num5, num3, num5);
				e.Graphics.DrawLine(Pens.DarkGray, 0, num6, num3, num6);
			}
		}

		private int get_level(int value)
		{
			int num = Math.Min(0, this.fFullHP - this.fDamage);
			int num2 = Math.Max(this.fFullHP + this.fTempHP - this.fDamage, this.fFullHP);
			int num3 = num2 - num;
			if (num3 == 0)
			{
				return 0;
			}
			int num4 = num2 - value;
			return num4 * base.Height / num3;
		}
	}
}
