using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class BreakdownPanel : UserControl
	{
		private IContainer components;

		private List<Hero> fHeroes;

		private List<HeroRoleType> fRows;

		private List<string> fColumns;

		private Dictionary<Point, int> fCells;

		private Dictionary<int, int> fRowTotals;

		private Dictionary<int, int> fColumnTotals;

		private StringFormat fCentred = new StringFormat();

		public List<Hero> Heroes
		{
			get
			{
				return this.fHeroes;
			}
			set
			{
				this.fHeroes = value;
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

		public BreakdownPanel()
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
			if (this.fHeroes == null)
			{
				e.Graphics.DrawString("(no heroes)", this.Font, SystemBrushes.WindowText, base.ClientRectangle, this.fCentred);
				return;
			}
			this.analyse_party();
			Font font = new Font(this.Font, FontStyle.Bold);
			for (int num = 0; num != this.fRows.Count + 1; num++)
			{
				string s = "Total";
				if (num != this.fRows.Count)
				{
					HeroRoleType heroRoleType = this.fRows[num];
					s = heroRoleType.ToString();
				}
				RectangleF layoutRectangle = this.get_rect(0, num + 1);
				e.Graphics.DrawString(s, font, SystemBrushes.WindowText, layoutRectangle, this.fCentred);
			}
			for (int num2 = 0; num2 != this.fColumns.Count + 1; num2++)
			{
				string s2 = "Total";
				if (num2 != this.fColumns.Count)
				{
					s2 = this.fColumns[num2];
				}
				RectangleF layoutRectangle2 = this.get_rect(num2 + 1, 0);
				e.Graphics.DrawString(s2, font, SystemBrushes.WindowText, layoutRectangle2, this.fCentred);
			}
			for (int num3 = 0; num3 != this.fRows.Count; num3++)
			{
				for (int num4 = 0; num4 != this.fColumns.Count; num4++)
				{
					int num5 = this.fCells[new Point(num3, num4)];
					RectangleF layoutRectangle3 = this.get_rect(num4 + 1, num3 + 1);
					e.Graphics.DrawString(num5.ToString(), this.Font, SystemBrushes.WindowText, layoutRectangle3, this.fCentred);
				}
			}
			for (int num6 = 0; num6 != this.fRows.Count; num6++)
			{
				HeroRoleType arg_1B4_0 = this.fRows[num6];
				int num7 = this.fRowTotals[num6];
				RectangleF layoutRectangle4 = this.get_rect(this.fColumns.Count + 1, num6 + 1);
				e.Graphics.DrawString(num7.ToString(), font, SystemBrushes.WindowText, layoutRectangle4, this.fCentred);
			}
			for (int num8 = 0; num8 != this.fColumns.Count; num8++)
			{
				string arg_224_0 = this.fColumns[num8];
				int num9 = this.fColumnTotals[num8];
				RectangleF layoutRectangle5 = this.get_rect(num8 + 1, this.fRows.Count + 1);
				e.Graphics.DrawString(num9.ToString(), font, SystemBrushes.WindowText, layoutRectangle5, this.fCentred);
			}
			RectangleF layoutRectangle6 = this.get_rect(this.fColumns.Count + 1, this.fRows.Count + 1);
			e.Graphics.DrawString(this.fHeroes.Count.ToString(), font, SystemBrushes.WindowText, layoutRectangle6, this.fCentred);
			float num10 = (float)base.ClientRectangle.Width / (float)(this.fColumns.Count + 2);
			float num11 = (float)base.ClientRectangle.Height / (float)(this.fRows.Count + 2);
			Pen pen = new Pen(SystemColors.ControlDark);
			for (int num12 = 0; num12 != this.fRows.Count + 1; num12++)
			{
				float y = (float)(num12 + 1) * num11;
				e.Graphics.DrawLine(pen, new PointF((float)base.ClientRectangle.Left, y), new PointF((float)base.ClientRectangle.Right, y));
			}
			for (int num13 = 0; num13 != this.fColumns.Count + 1; num13++)
			{
				float x = (float)(num13 + 1) * num10;
				e.Graphics.DrawLine(pen, new PointF(x, (float)base.ClientRectangle.Top), new PointF(x, (float)base.ClientRectangle.Bottom));
			}
		}

		private void analyse_party()
		{
			if (this.fHeroes == null)
			{
				return;
			}
			this.fRows = new List<HeroRoleType>();
			foreach (HeroRoleType item in Enum.GetValues(typeof(HeroRoleType)))
			{
				this.fRows.Add(item);
			}
			this.fColumns = new List<string>();
			foreach (Hero current in this.fHeroes)
			{
				if (!this.fColumns.Contains(current.PowerSource))
				{
					this.fColumns.Add(current.PowerSource);
				}
			}
			this.fColumns.Sort();
			this.fCells = new Dictionary<Point, int>();
			this.fRowTotals = new Dictionary<int, int>();
			this.fColumnTotals = new Dictionary<int, int>();
			for (int num = 0; num != this.fRows.Count; num++)
			{
				HeroRoleType heroRoleType = this.fRows[num];
				if (!this.fRowTotals.ContainsKey(num))
				{
					this.fRowTotals[num] = 0;
				}
				for (int num2 = 0; num2 != this.fColumns.Count; num2++)
				{
					string b = this.fColumns[num2];
					int num3 = 0;
					foreach (Hero current2 in this.fHeroes)
					{
						if (current2.Role == heroRoleType && current2.PowerSource == b)
						{
							num3++;
						}
					}
                    fCells[new Point(num, num2)] = num3;
                    fRowTotals[num] = fRowTotals[num] + num3;
					if (!fColumnTotals.ContainsKey(num2))
					{
                        fColumnTotals[num2] = 0;
					}
                    fColumnTotals[num2] = fColumnTotals[num2] + num3;
				}
			}
		}

		private RectangleF get_rect(int x, int y)
		{
			float num = (float)base.ClientRectangle.Width / (float)(this.fColumns.Count + 2);
			float num2 = (float)base.ClientRectangle.Height / (float)(this.fRows.Count + 2);
			return new RectangleF((float)x * num, (float)y * num2, num, num2);
		}
	}
}
