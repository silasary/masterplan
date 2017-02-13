using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.Controls
{
    internal class DeckGrid : UserControl
	{
		private IContainer components;

		private EncounterDeck fDeck;

		private List<CardCategory> fRows;

		private List<Difficulty> fColumns;

		private Dictionary<int, int> fRowTotals;

		private Dictionary<int, int> fColumnTotals;

		private Dictionary<Point, int> fCells;

		private StringFormat fCentred = new StringFormat();

		private Point fHoverCell = Point.Empty;

		private Point fSelectedCell = Point.Empty;

        public event EventHandler SelectedCellChanged;

        public event EventHandler CellActivated;

		public EncounterDeck Deck
		{
			get
			{
				return this.fDeck;
			}
			set
			{
				this.fDeck = value;
				this.fSelectedCell = Point.Empty;
				base.Invalidate();
			}
		}

		public bool IsCellSelected
		{
			get
			{
				return this.fSelectedCell != Point.Empty;
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

		public DeckGrid()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
		}

		public bool InSelectedCell(EncounterCard card)
		{
			if (this.fSelectedCell == Point.Empty)
			{
				return false;
			}
			int index = this.fSelectedCell.X - 1;
			Difficulty difficulty = this.fColumns[index];
			int index2 = this.fSelectedCell.Y - 1;
			CardCategory cardCategory = this.fRows[index2];
			return card.Category == cardCategory && card.GetDifficulty(this.fDeck.Level) == difficulty;
		}

		protected void OnSelectedCellChanged()
		{
            this.SelectedCellChanged?.Invoke(this, new EventArgs());
        }

		protected void OnCellActivated()
		{
            this.CellActivated?.Invoke(this, new EventArgs());
        }

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(Brushes.White, base.ClientRectangle);
			if (this.fDeck == null)
			{
				e.Graphics.DrawString("(no deck)", this.Font, SystemBrushes.WindowText, base.ClientRectangle, this.fCentred);
				return;
			}
			this.analyse_deck();
			float num = (float)base.ClientRectangle.Width / (float)(this.fColumns.Count + 2);
			float num2 = (float)base.ClientRectangle.Height / (float)(this.fRows.Count + 2);
			using (Pen pen = new Pen(SystemColors.ControlDark))
			{
				for (int num3 = 0; num3 != this.fRows.Count + 1; num3++)
				{
					float y = (float)(num3 + 1) * num2;
					e.Graphics.DrawLine(pen, new PointF((float)base.ClientRectangle.Left, y), new PointF((float)base.ClientRectangle.Right, y));
				}
				for (int num4 = 0; num4 != this.fColumns.Count + 1; num4++)
				{
					float x = (float)(num4 + 1) * num;
					e.Graphics.DrawLine(pen, new PointF(x, (float)base.ClientRectangle.Top), new PointF(x, (float)base.ClientRectangle.Bottom));
				}
			}
			e.Graphics.FillRectangle(Brushes.Black, this.get_rect(0, 0));
			for (int num5 = 1; num5 != this.fColumns.Count + 2; num5++)
			{
				RectangleF rect = this.get_rect(num5, 0);
				e.Graphics.FillRectangle(Brushes.Black, rect);
			}
			for (int num6 = 1; num6 != this.fRows.Count + 2; num6++)
			{
				RectangleF rect2 = this.get_rect(0, num6);
				e.Graphics.FillRectangle(Brushes.Black, rect2);
			}
			using (Brush brush = new SolidBrush(Color.FromArgb(30, Color.Gray)))
			{
				for (int num7 = 1; num7 != this.fColumns.Count + 1; num7++)
				{
					RectangleF rect3 = this.get_rect(num7, this.fRows.Count + 1);
					e.Graphics.FillRectangle(brush, rect3);
				}
				for (int num8 = 1; num8 != this.fRows.Count + 1; num8++)
				{
					RectangleF rect4 = this.get_rect(this.fColumns.Count + 1, num8);
					e.Graphics.FillRectangle(brush, rect4);
				}
			}
			if (this.fHoverCell != Point.Empty && this.fHoverCell.X <= this.fColumns.Count && this.fHoverCell.Y <= this.fRows.Count)
			{
				RectangleF rect5 = this.get_rect(this.fHoverCell.X, this.fHoverCell.Y);
				e.Graphics.DrawRectangle(SystemPens.Highlight, rect5.X, rect5.Y, rect5.Width, rect5.Height);
				using (Brush brush2 = new SolidBrush(Color.FromArgb(30, SystemColors.Highlight)))
				{
					e.Graphics.FillRectangle(brush2, rect5);
				}
			}
			if (this.fSelectedCell != Point.Empty)
			{
				RectangleF rect6 = this.get_rect(this.fSelectedCell.X, this.fSelectedCell.Y);
				using (Brush brush3 = new SolidBrush(Color.FromArgb(100, SystemColors.Highlight)))
				{
					e.Graphics.FillRectangle(brush3, rect6);
				}
			}
			Font font = new Font(this.Font, FontStyle.Bold);
			for (int num9 = 0; num9 != this.fRows.Count + 1; num9++)
			{
				string s = "Total";
				if (num9 != this.fRows.Count)
				{
					CardCategory cardCategory = this.fRows[num9];
					s = cardCategory.ToString();
					if (cardCategory == CardCategory.SoldierBrute)
					{
						s = "Sldr / Brute";
					}
				}
				RectangleF layoutRectangle = this.get_rect(0, num9 + 1);
				e.Graphics.DrawString(s, font, Brushes.White, layoutRectangle, this.fCentred);
			}
			for (int num10 = 0; num10 != this.fColumns.Count + 1; num10++)
			{
				string s2 = "Total";
				if (num10 != this.fColumns.Count)
				{
					switch (this.fColumns[num10])
					{
					case Difficulty.Trivial:
						s2 = "Lower";
						break;
					case Difficulty.Easy:
					{
						int num11 = Math.Max(1, this.fDeck.Level - 1);
						s2 = string.Concat(new object[]
						{
							"Lvl ",
							num11,
							" to ",
							this.fDeck.Level + 1
						});
						break;
					}
					case Difficulty.Moderate:
						s2 = string.Concat(new object[]
						{
							"Lvl ",
							this.fDeck.Level + 2,
							" to ",
							this.fDeck.Level + 3
						});
						break;
					case Difficulty.Hard:
						s2 = string.Concat(new object[]
						{
							"Lvl ",
							this.fDeck.Level + 4,
							" to ",
							this.fDeck.Level + 5
						});
						break;
					case Difficulty.Extreme:
						s2 = "Higher";
						break;
					}
				}
				RectangleF layoutRectangle2 = this.get_rect(num10 + 1, 0);
				e.Graphics.DrawString(s2, font, Brushes.White, layoutRectangle2, this.fCentred);
			}
			for (int num12 = 0; num12 != this.fRows.Count; num12++)
			{
				for (int num13 = 0; num13 != this.fColumns.Count; num13++)
				{
					Point key = new Point(num12, num13);
					int num14 = this.fCells[key];
					if (num14 != 0)
					{
						RectangleF layoutRectangle3 = this.get_rect(num13 + 1, num12 + 1);
						e.Graphics.DrawString(num14.ToString(), this.Font, SystemBrushes.WindowText, layoutRectangle3, this.fCentred);
					}
				}
			}
			for (int num15 = 0; num15 != this.fRows.Count; num15++)
			{
				CardCategory cardCategory2 = this.fRows[num15];
				int num16 = this.fRowTotals[num15];
				int num17 = 0;
				switch (cardCategory2)
				{
				case CardCategory.Artillery:
					num17 = 5;
					break;
				case CardCategory.Controller:
					num17 = 5;
					break;
				case CardCategory.Lurker:
					num17 = 2;
					break;
				case CardCategory.Skirmisher:
					num17 = 14;
					break;
				case CardCategory.SoldierBrute:
					num17 = 18;
					break;
				case CardCategory.Minion:
					num17 = 5;
					break;
				case CardCategory.Solo:
					num17 = 1;
					break;
				}
				RectangleF layoutRectangle4 = this.get_rect(this.fColumns.Count + 1, num15 + 1);
				e.Graphics.DrawString(string.Concat(new object[]
				{
					num16,
					" (",
					num17,
					")"
				}), font, SystemBrushes.WindowText, layoutRectangle4, this.fCentred);
			}
			for (int num18 = 0; num18 != this.fColumns.Count; num18++)
			{
				Difficulty arg_77F_0 = this.fColumns[num18];
				int num19 = this.fColumnTotals[num18];
				RectangleF layoutRectangle5 = this.get_rect(num18 + 1, this.fRows.Count + 1);
				e.Graphics.DrawString(num19.ToString(), font, SystemBrushes.WindowText, layoutRectangle5, this.fCentred);
			}
			RectangleF layoutRectangle6 = this.get_rect(this.fColumns.Count + 1, this.fRows.Count + 1);
			e.Graphics.DrawString(this.fDeck.Cards.Count + " cards", font, SystemBrushes.WindowText, layoutRectangle6, this.fCentred);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (this.fColumns == null || this.fRows == null)
			{
				return;
			}
			float num = (float)base.ClientRectangle.Width / (float)(this.fColumns.Count + 2);
			float num2 = (float)base.ClientRectangle.Height / (float)(this.fRows.Count + 2);
			Point point = base.PointToClient(Cursor.Position);
			int num3 = (int)((float)point.X / num);
			int num4 = (int)((float)point.Y / num2);
			if (num3 == 0 || num4 == 0)
			{
				this.fHoverCell = Point.Empty;
				base.Invalidate();
				return;
			}
			if (num3 != this.fHoverCell.X || num4 != this.fHoverCell.Y)
			{
				this.fHoverCell = new Point(num3, num4);
				base.Invalidate();
			}
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.fHoverCell = Point.Empty;
			base.Invalidate();
		}

		protected override void OnClick(EventArgs e)
		{
			this.fSelectedCell = this.fHoverCell;
			if (this.fSelectedCell.X > this.fColumns.Count || this.fSelectedCell.Y > this.fRows.Count)
			{
				this.fSelectedCell = Point.Empty;
			}
			base.Invalidate();
			this.OnSelectedCellChanged();
		}

		protected override void OnDoubleClick(EventArgs e)
		{
			this.OnCellActivated();
		}

		private void analyse_deck()
		{
			if (this.fDeck == null)
			{
				return;
			}
			this.fRows = new List<CardCategory>();
			Array values = Enum.GetValues(typeof(CardCategory));
			foreach (CardCategory item in values)
			{
				this.fRows.Add(item);
			}
			this.fColumns = new List<Difficulty>();
			Array values2 = Enum.GetValues(typeof(Difficulty));
			foreach (Difficulty difficulty in values2)
			{
				if ((difficulty != Difficulty.Trivial || this.fDeck.Level >= 3) && difficulty != Difficulty.Random)
				{
					this.fColumns.Add(difficulty);
				}
			}
			this.fCells = new Dictionary<Point, int>();
			this.fRowTotals = new Dictionary<int, int>();
			this.fColumnTotals = new Dictionary<int, int>();
			for (int num = 0; num != this.fRows.Count; num++)
			{
				CardCategory cardCategory = this.fRows[num];
				for (int num2 = 0; num2 != this.fColumns.Count; num2++)
				{
					Difficulty difficulty2 = this.fColumns[num2];
					int num3 = 0;
					foreach (EncounterCard current in this.fDeck.Cards)
					{
						if (current.Category == cardCategory && current.GetDifficulty(this.fDeck.Level) == difficulty2)
						{
							num3++;
						}
					}
					this.fCells[new Point(num, num2)] = num3;
					if (!this.fRowTotals.ContainsKey(num))
					{
						this.fRowTotals[num] = 0;
					}
					fRowTotals[num] = fColumnTotals[num] + num3;
					if (!this.fColumnTotals.ContainsKey(num2))
					{
						this.fColumnTotals[num2] = 0;
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
