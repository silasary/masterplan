using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class EncounterGauge : UserControl
	{
		private const int CONTROL_HEIGHT = 20;

		private Party fParty;

		private int fXP;

		private IContainer components;

		public Party Party
		{
			get
			{
				return this.fParty;
			}
			set
			{
				this.fParty = value;
				base.Invalidate();
			}
		}

		public int XP
		{
			get
			{
				return this.fXP;
			}
			set
			{
				this.fXP = value;
				base.Invalidate();
			}
		}

		public EncounterGauge()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			base.Height = 20;
		}

		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
			base.Height = 20;
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			base.Height = 20;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.fParty == null)
			{
				return;
			}
			Font font = new Font(this.Font.FontFamily, 7f);
			Rectangle rect = new Rectangle(0, 4, this.get_x(this.fXP), base.Height - 8);
			if (rect.Width > 0)
			{
				Brush brush = new LinearGradientBrush(rect, SystemColors.Control, SystemColors.ControlDark, LinearGradientMode.Horizontal);
				e.Graphics.FillRectangle(brush, rect);
			}
			int num = Math.Max(this.get_min_level(), 1);
			int max_level = this.get_max_level();
			for (int num2 = num; num2 != max_level; num2++)
			{
				int xp = Experience.GetCreatureXP(num2) * this.fParty.Size;
				int num3 = this.get_x(xp);
				e.Graphics.DrawLine(Pens.Black, new Point(num3, 1), new Point(num3, base.Height - 3));
				e.Graphics.DrawString(num2.ToString(), font, SystemBrushes.WindowText, new PointF((float)num3, 1f));
			}
		}

		private int get_min_level()
		{
			int creatureLevel = Experience.GetCreatureLevel(this.fXP / this.fParty.Size);
			int val = Math.Min(this.fParty.Level - 3, creatureLevel);
			return Math.Max(val, 0);
		}

		private int get_max_level()
		{
			int creatureLevel = Experience.GetCreatureLevel(this.fXP / this.fParty.Size);
			return Math.Max(this.fParty.Level + 5, creatureLevel + 1);
		}

		private int get_x(int xp)
		{
			int val = Experience.GetCreatureXP(this.get_min_level()) * this.fParty.Size;
			int val2 = Experience.GetCreatureXP(this.get_max_level()) * this.fParty.Size;
			int num = Math.Min(this.fXP, val);
			int num2 = Math.Max(this.fXP, val2);
			int num3 = num2 - num;
			int num4 = xp - num;
			return num4 * base.Width / num3;
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
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "EncounterGauge";
			base.ResumeLayout(false);
		}
	}
}
