using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Utils;
using Utils.Graphics;

namespace Masterplan.Controls
{
	internal class CardDeck : UserControl
	{
		private IContainer components;

		private List<Pair<EncounterCard, int>> fCards;

		private StringFormat fCentred = new StringFormat();

		private StringFormat fTitle = new StringFormat();

		private StringFormat fInfo = new StringFormat();

		private float fRadius = 10f;

		private int fVisibleCards;

		private List<Pair<RectangleF, EncounterCard>> fRegions;

		private EncounterCard fHoverCard;

        public event EventHandler DeckOrderChanged;

		public EncounterCard TopCard
		{
			get
			{
				if (this.fCards == null || this.fCards.Count == 0)
				{
					return null;
				}
				return this.fCards[0].First;
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

		public CardDeck()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
			this.fTitle.Alignment = StringAlignment.Near;
			this.fTitle.LineAlignment = StringAlignment.Center;
			this.fTitle.Trimming = StringTrimming.Character;
			this.fInfo.Alignment = StringAlignment.Far;
			this.fInfo.LineAlignment = StringAlignment.Center;
			this.fInfo.Trimming = StringTrimming.Character;
		}

		public void SetCards(List<EncounterCard> cards)
		{
			this.fCards = new List<Pair<EncounterCard, int>>();
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			foreach (EncounterCard current in cards)
			{
				binarySearchTree.Add(current.Title);
			}
			List<string> sortedList = binarySearchTree.SortedList;
			foreach (string current2 in sortedList)
			{
				Pair<EncounterCard, int> pair = new Pair<EncounterCard, int>();
				foreach (EncounterCard current3 in cards)
				{
					if (current3.Title == current2)
					{
						pair.First = current3;
						pair.Second++;
					}
				}
				this.fCards.Add(pair);
			}
			base.Invalidate();
		}

		protected void OnDeckOrderChanged()
		{
            DeckOrderChanged?.Invoke(this, new EventArgs());
        }

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(Brushes.Transparent, base.ClientRectangle);
			if (this.fCards == null || this.fCards.Count == 0)
			{
				e.Graphics.DrawString("(no cards)", this.Font, Brushes.Black, base.ClientRectangle, this.fCentred);
				return;
			}
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			RectangleF rectangleF = new RectangleF((float)base.ClientRectangle.X, (float)base.ClientRectangle.Y, (float)(base.ClientRectangle.Width - 1), (float)(base.ClientRectangle.Height - 1));
			this.fRegions = new List<Pair<RectangleF, EncounterCard>>();
			float num = (float)this.Font.Height * 1.8f;
			float num2 = num * 0.2f;
			float num3 = (float)base.Height - 4f * this.fRadius;
			int val = (int)(num3 / num);
			this.fVisibleCards = Math.Min(val, this.fCards.Count);
			if (this.fVisibleCards + 1 == this.fCards.Count)
			{
				this.fVisibleCards++;
			}
			bool flag = this.fCards.Count > this.fVisibleCards;
			int num4 = flag ? (this.fVisibleCards + 1) : this.fVisibleCards;
			if (flag)
			{
				float num5 = num2 * (float)this.fVisibleCards;
				float x = rectangleF.X + num5;
				float y = rectangleF.Y;
				float width = rectangleF.Width - num2 * (float)(num4 - 1);
				float height = rectangleF.Height - y;
				RectangleF rect = new RectangleF(x, y, width, height);
				this.DrawCard(null, 0, false, rect, e.Graphics);
			}
			for (int i = this.fVisibleCards - 1; i >= 0; i--)
			{
				float num6 = num2 * (float)i;
				float num7 = num * (float)(num4 - i - 1);
				float x2 = rectangleF.X + num6;
				float num8 = rectangleF.Y + num7;
				float width2 = rectangleF.Width - num2 * (float)(num4 - 1);
				float height2 = rectangleF.Height - num8;
				RectangleF rectangleF2 = new RectangleF(x2, num8, width2, height2);
				Pair<EncounterCard, int> pair = this.fCards[i];
				bool topmost = i == 0;
                DrawCard(pair.First, pair.Second, topmost, rectangleF2, e.Graphics);
                fRegions.Add(new Pair<RectangleF, EncounterCard>(rectangleF2, pair.First));
			}
		}

		private void DrawCard(EncounterCard card, int count, bool topmost, RectangleF rect, Graphics g)
		{
			int alpha = (card != null) ? 255 : 100;
			GraphicsPath path = RoundedRectangle.Create(rect, this.fRadius, (RoundedRectangle.RectangleCorners)3);
			using (Brush brush = new SolidBrush(Color.FromArgb(alpha, 54, 79, 39)))
			{
				g.FillPath(brush, path);
			}
			g.DrawPath(Pens.White, path);
			float height = (float)this.Font.Height * 1.5f;
			RectangleF layoutRectangle = new RectangleF(rect.X + this.fRadius, rect.Y, rect.Width - 2f * this.fRadius, height);
			if (card != null)
			{
				string text = card.Title;
				if (count > 1)
				{
					text = string.Concat(new object[]
					{
						"(",
						count,
						"x) ",
						text
					});
				}
				Color color = (card != this.fHoverCard) ? Color.White : Color.PaleGreen;
				using (Brush brush2 = new SolidBrush(color))
				{
					using (Font font = new Font(this.Font, this.Font.Style | FontStyle.Bold))
					{
						g.DrawString(text, font, brush2, layoutRectangle, this.fTitle);
					}
					g.DrawString(card.Info, this.Font, brush2, layoutRectangle, this.fInfo);
				}
				if (topmost)
				{
					float num = this.fRadius * 0.2f;
					RectangleF rectangleF = new RectangleF(rect.X + num, rect.Y + layoutRectangle.Height, rect.Width - 2f * num, rect.Height - layoutRectangle.Height);
					using (Brush brush3 = new SolidBrush(Color.FromArgb(225, 231, 197)))
					{
						g.FillRectangle(brush3, rectangleF);
					}
					string s = "Click on a card to move it to the front of the deck.";
					g.DrawString(s, this.Font, Brushes.Black, rectangleF, this.fCentred);
					return;
				}
			}
			else
			{
				int num2 = this.fCards.Count - this.fVisibleCards;
				g.DrawString("(" + num2 + " more cards)", this.Font, Brushes.White, layoutRectangle, this.fCentred);
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (this.fRegions == null)
			{
				return;
			}
			EncounterCard encounterCard = null;
			foreach (Pair<RectangleF, EncounterCard> current in this.fRegions)
			{
				if (current.First.Top <= (float)e.Location.Y && current.First.Bottom >= (float)e.Location.Y)
				{
					encounterCard = current.Second;
				}
			}
			this.fHoverCard = encounterCard;
			base.Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			this.fHoverCard = null;
			base.Invalidate();
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (this.fHoverCard == null)
			{
				return;
			}
			EncounterCard encounterCard = this.fHoverCard;
			this.fHoverCard = null;
			while (this.fCards[0].First != encounterCard)
			{
				Pair<EncounterCard, int> item = this.fCards[0];
				this.fCards.RemoveAt(0);
				this.fCards.Add(item);
				this.Refresh();
			}
			this.OnDeckOrderChanged();
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (e.Delta > 0)
			{
				Pair<EncounterCard, int> item = this.fCards[0];
				this.fCards.RemoveAt(0);
				this.fCards.Add(item);
			}
			else
			{
				Pair<EncounterCard, int> item2 = this.fCards[this.fCards.Count - 1];
				this.fCards.RemoveAt(this.fCards.Count - 1);
				this.fCards.Insert(0, item2);
			}
			this.fHoverCard = null;
			this.Refresh();
			this.OnDeckOrderChanged();
		}
	}
}
