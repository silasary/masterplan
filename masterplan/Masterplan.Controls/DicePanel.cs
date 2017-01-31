using Masterplan.Tools;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.Controls
{
	internal class DicePanel : UserControl
	{
		private class DiceSorter : IComparer<Pair<int, int>>
		{
			public int Compare(Pair<int, int> lhs, Pair<int, int> rhs)
			{
				int num = lhs.First.CompareTo(rhs.First);
				if (num == 0)
				{
					num = lhs.Second.CompareTo(rhs.Second);
				}
				return num;
			}
		}

		private const int DIE_SIZE = 32;

		private StringFormat fCentred = new StringFormat();

		private List<Pair<int, int>> fDice = new List<Pair<int, int>>();

		private int fConstant;

		private bool fUpdating;

		private IContainer components;

		private ToolStrip DiceToolbar;

		private ToolStripButton RollBtn;

		private ToolStripButton ClearBtn;

		private Label DiceLbl;

		private ListView DiceList;

		private ListView DiceSourceList;

		private Panel ResultPanel;

		private ToolStripButton OddsBtn;

		private TextBox ExpressionBox;

		public DiceExpression Expression
		{
			get
			{
				return DiceExpression.Parse(this.ExpressionBox.Text);
			}
			set
			{
				this.ExpressionBox.Text = ((value != null) ? value.ToString() : "");
			}
		}

		public Pair<int, int> SelectedDie
		{
			get
			{
				if (this.DiceSourceList.SelectedItems.Count != 0)
				{
					return this.DiceSourceList.SelectedItems[0].Tag as Pair<int, int>;
				}
				return null;
			}
		}

		public Pair<int, int> SelectedRoll
		{
			get
			{
				if (this.DiceList.SelectedItems.Count != 0)
				{
					return this.DiceList.SelectedItems[0].Tag as Pair<int, int>;
				}
				return null;
			}
		}

		public DicePanel()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RollBtn.Enabled = (this.fDice.Count != 0);
			this.ClearBtn.Enabled = (this.fDice.Count != 0);
			this.OddsBtn.Enabled = (this.fDice.Count != 0);
		}

		public void UpdateView()
		{
			this.update_dice_source();
			this.update_dice_rolls();
			this.update_dice_result();
		}

		private void RollBtn_Click(object sender, EventArgs e)
		{
			foreach (Pair<int, int> current in this.fDice)
			{
				int second = Session.Dice(1, current.First);
				current.Second = second;
			}
			this.fDice.Sort(new DicePanel.DiceSorter());
			this.update_dice_rolls();
			this.update_dice_result();
		}

		private void ClearBtn_Click(object sender, EventArgs e)
		{
			this.fDice.Clear();
			this.fConstant = 0;
			this.update_dice_rolls();
			this.update_dice_result();
		}

		private void OddsBtn_Click(object sender, EventArgs e)
		{
			List<int> list = new List<int>();
			foreach (Pair<int, int> current in this.fDice)
			{
				list.Add(current.First);
			}
			OddsForm oddsForm = new OddsForm(list, this.fConstant, this.ExpressionBox.Text);
			oddsForm.ShowDialog();
		}

		private void DiceSourceList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedDie != null)
			{
				DragDropEffects dragDropEffects = base.DoDragDrop(this.SelectedDie, DragDropEffects.Move);
				if (dragDropEffects == DragDropEffects.Move)
				{
					this.add_die(this.SelectedDie.First);
				}
			}
		}

		private void DiceList_DragOver(object sender, DragEventArgs e)
		{
			Pair<int, int> pair = e.Data.GetData(typeof(Pair<int, int>)) as Pair<int, int>;
			if (pair != null)
			{
				e.Effect = DragDropEffects.Move;
			}
		}

		private void DiceSourceList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedDie != null)
			{
				this.add_die(this.SelectedDie.First);
			}
		}

		private void DiceList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedRoll != null)
			{
				this.SelectedRoll.Second = Session.Dice(1, this.SelectedRoll.First);
				this.update_dice_rolls();
				this.update_dice_result();
			}
		}

		private void ExpressionBox_TextChanged(object sender, EventArgs e)
		{
			if (this.fUpdating)
			{
				return;
			}
			DiceExpression diceExpression = DiceExpression.Parse(this.ExpressionBox.Text);
			if (diceExpression != null)
			{
				this.fUpdating = true;
				this.ClearBtn_Click(sender, e);
				this.fConstant = diceExpression.Constant;
				for (int num = 0; num != diceExpression.Throws; num++)
				{
					this.add_die(diceExpression.Sides);
				}
				this.fUpdating = false;
			}
		}

		private void update_dice_source()
		{
			this.DiceSourceList.Items.Clear();
			List<int> list = new List<int>();
			list.Add(4);
			list.Add(6);
			list.Add(8);
			list.Add(10);
			list.Add(12);
			list.Add(20);
			this.DiceSourceList.LargeImageList = new ImageList();
			this.DiceSourceList.LargeImageList.ImageSize = new Size(32, 32);
			foreach (int current in list)
			{
				string caption = "d" + current;
				ListViewItem listViewItem = this.DiceSourceList.Items.Add("");
				listViewItem.Tag = new Pair<int, int>(current, -1);
				this.DiceSourceList.LargeImageList.Images.Add(this.get_image(current, caption));
				listViewItem.ImageIndex = this.DiceSourceList.LargeImageList.Images.Count - 1;
			}
		}

		private void update_dice_rolls()
		{
			this.DiceList.Items.Clear();
			this.DiceList.LargeImageList = new ImageList();
			this.DiceList.LargeImageList.ImageSize = new Size(32, 32);
			List<int> list = new List<int>();
			foreach (Pair<int, int> current in this.fDice)
			{
				ListViewItem listViewItem = this.DiceList.Items.Add("");
				listViewItem.Tag = current;
				this.DiceList.LargeImageList.Images.Add(this.get_image(current.First, current.Second.ToString()));
				listViewItem.ImageIndex = this.DiceList.LargeImageList.Images.Count - 1;
				list.Add(current.First);
			}
			if (!this.fUpdating)
			{
				this.fUpdating = true;
				this.ExpressionBox.Text = ((this.fDice.Count != 0) ? DiceStatistics.Expression(list, this.fConstant) : "");
				this.fUpdating = false;
			}
		}

		private void update_dice_result()
		{
			if (this.fDice.Count != 0)
			{
				int num = this.fConstant;
				foreach (Pair<int, int> current in this.fDice)
				{
					num += current.Second;
				}
				this.DiceLbl.ForeColor = SystemColors.WindowText;
				this.DiceLbl.Text = num.ToString();
				return;
			}
			this.DiceLbl.ForeColor = SystemColors.GrayText;
			this.DiceLbl.Text = "-";
		}

		private void add_die(int sides)
		{
			int second = Session.Dice(1, sides);
			this.fDice.Add(new Pair<int, int>(sides, second));
			this.fDice.Sort(new DicePanel.DiceSorter());
			this.update_dice_rolls();
			this.update_dice_result();
		}

		private Image get_image(int sides, string caption)
		{
			Bitmap bitmap = new Bitmap(32, 32);
			Graphics graphics = Graphics.FromImage(bitmap);
			RectangleF layoutRectangle = new RectangleF(0f, 0f, 31f, 31f);
			switch (sides)
			{
			case 4:
			{
				float num = layoutRectangle.Width / 6f;
				PointF pointF = new PointF(layoutRectangle.Left, layoutRectangle.Bottom - num);
				PointF pointF2 = new PointF(layoutRectangle.Right, layoutRectangle.Bottom - num);
				PointF pointF3 = new PointF(layoutRectangle.Left + layoutRectangle.Width / 2f, layoutRectangle.Top);
				graphics.FillPolygon(Brushes.LightGray, new PointF[]
				{
					pointF,
					pointF2,
					pointF3
				});
				graphics.DrawPolygon(Pens.Gray, new PointF[]
				{
					pointF,
					pointF2,
					pointF3
				});
				break;
			}
			case 5:
			case 7:
			case 9:
			case 11:
				break;
			case 6:
			{
				float num2 = layoutRectangle.Width / 8f;
				RectangleF rect = new RectangleF(layoutRectangle.X + num2, layoutRectangle.Y + num2, layoutRectangle.Width - 2f * num2, layoutRectangle.Height - 2f * num2);
				graphics.FillRectangle(Brushes.LightGray, rect);
				graphics.DrawRectangle(Pens.Gray, rect.X, rect.Y, rect.Width, rect.Height);
				break;
			}
			case 8:
			{
				float num3 = layoutRectangle.Width / 8f;
				PointF pointF4 = new PointF(layoutRectangle.Left + num3, layoutRectangle.Top + layoutRectangle.Height / 2f);
				PointF pointF5 = new PointF(layoutRectangle.Right - num3, layoutRectangle.Top + layoutRectangle.Height / 2f);
				PointF pointF6 = new PointF(layoutRectangle.Left + layoutRectangle.Width / 2f, layoutRectangle.Top);
				PointF pointF7 = new PointF(layoutRectangle.Left + layoutRectangle.Width / 2f, layoutRectangle.Bottom);
				graphics.FillPolygon(Brushes.LightGray, new PointF[]
				{
					pointF4,
					pointF7,
					pointF5,
					pointF6
				});
				graphics.DrawPolygon(Pens.Gray, new PointF[]
				{
					pointF4,
					pointF7,
					pointF5,
					pointF6
				});
				break;
			}
			case 10:
			{
				float num4 = layoutRectangle.Left + layoutRectangle.Width / 2f;
				float num5 = layoutRectangle.Top + layoutRectangle.Height / 2f;
				List<PointF> list = new List<PointF>();
				for (int num6 = 0; num6 != 10; num6++)
				{
					float num7 = layoutRectangle.Width / 2f;
					double num8 = (double)num6 * 6.2831853071795862 / 10.0;
					double num9 = (double)num7 * Math.Sin(num8);
					double num10 = (double)num7 * Math.Cos(num8);
					list.Add(new PointF((float)((double)num4 + num9), (float)((double)num5 + num10)));
				}
				graphics.FillPolygon(Brushes.LightGray, list.ToArray());
				graphics.DrawPolygon(Pens.Gray, list.ToArray());
				break;
			}
			case 12:
			{
				float num11 = layoutRectangle.Width / 3f;
				PointF pointF8 = new PointF(layoutRectangle.Left, layoutRectangle.Top + layoutRectangle.Height / 2f);
				PointF pointF9 = new PointF(layoutRectangle.Right, layoutRectangle.Top + layoutRectangle.Height / 2f);
				PointF pointF10 = new PointF(layoutRectangle.Left + num11, layoutRectangle.Top);
				PointF pointF11 = new PointF(layoutRectangle.Right - num11, layoutRectangle.Top);
				PointF pointF12 = new PointF(layoutRectangle.Left + num11, layoutRectangle.Bottom);
				PointF pointF13 = new PointF(layoutRectangle.Right - num11, layoutRectangle.Bottom);
				graphics.FillPolygon(Brushes.LightGray, new PointF[]
				{
					pointF8,
					pointF10,
					pointF11,
					pointF9,
					pointF13,
					pointF12
				});
				graphics.DrawPolygon(Pens.Gray, new PointF[]
				{
					pointF8,
					pointF10,
					pointF11,
					pointF9,
					pointF13,
					pointF12
				});
				break;
			}
			default:
				if (sides == 20)
				{
					float num12 = layoutRectangle.Width / 5f;
					PointF pointF14 = new PointF(layoutRectangle.Left, layoutRectangle.Top + num12);
					PointF pointF15 = new PointF(layoutRectangle.Left, layoutRectangle.Bottom - num12);
					PointF pointF16 = new PointF(layoutRectangle.Right, layoutRectangle.Top + num12);
					PointF pointF17 = new PointF(layoutRectangle.Right, layoutRectangle.Bottom - num12);
					PointF pointF18 = new PointF(layoutRectangle.Left + layoutRectangle.Width / 2f, layoutRectangle.Top);
					PointF pointF19 = new PointF(layoutRectangle.Left + layoutRectangle.Width / 2f, layoutRectangle.Bottom);
					graphics.FillPolygon(Brushes.LightGray, new PointF[]
					{
						pointF14,
						pointF15,
						pointF19,
						pointF17,
						pointF16,
						pointF18
					});
					graphics.DrawPolygon(Pens.Gray, new PointF[]
					{
						pointF14,
						pointF15,
						pointF19,
						pointF17,
						pointF16,
						pointF18
					});
				}
				break;
			}
			graphics.DrawString(caption, this.Font, SystemBrushes.WindowText, layoutRectangle, this.fCentred);
			return bitmap;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(DicePanel));
			this.DiceToolbar = new ToolStrip();
			this.RollBtn = new ToolStripButton();
			this.ClearBtn = new ToolStripButton();
			this.OddsBtn = new ToolStripButton();
			this.DiceLbl = new Label();
			this.DiceList = new ListView();
			this.DiceSourceList = new ListView();
			this.ResultPanel = new Panel();
			this.ExpressionBox = new TextBox();
			this.DiceToolbar.SuspendLayout();
			this.ResultPanel.SuspendLayout();
			base.SuspendLayout();
			this.DiceToolbar.Items.AddRange(new ToolStripItem[]
			{
				this.RollBtn,
				this.ClearBtn,
				this.OddsBtn
			});
			this.DiceToolbar.Location = new Point(0, 0);
			this.DiceToolbar.Name = "DiceToolbar";
			this.DiceToolbar.Size = new Size(287, 25);
			this.DiceToolbar.TabIndex = 8;
			this.DiceToolbar.Text = "toolStrip1";
			this.RollBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RollBtn.Image = (Image)resources.GetObject("RollBtn.Image");
			this.RollBtn.ImageTransparentColor = Color.Magenta;
			this.RollBtn.Name = "RollBtn";
			this.RollBtn.Size = new Size(41, 22);
			this.RollBtn.Text = "Reroll";
			this.RollBtn.Click += new EventHandler(this.RollBtn_Click);
			this.ClearBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ClearBtn.Image = (Image)resources.GetObject("ClearBtn.Image");
			this.ClearBtn.ImageTransparentColor = Color.Magenta;
			this.ClearBtn.Name = "ClearBtn";
			this.ClearBtn.Size = new Size(38, 22);
			this.ClearBtn.Text = "Clear";
			this.ClearBtn.Click += new EventHandler(this.ClearBtn_Click);
			this.OddsBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.OddsBtn.Image = (Image)resources.GetObject("OddsBtn.Image");
			this.OddsBtn.ImageTransparentColor = Color.Magenta;
			this.OddsBtn.Name = "OddsBtn";
			this.OddsBtn.Size = new Size(39, 22);
			this.OddsBtn.Text = "Odds";
			this.OddsBtn.Click += new EventHandler(this.OddsBtn_Click);
			this.DiceLbl.Dock = DockStyle.Fill;
			this.DiceLbl.Font = new Font("Calibri", 30f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.DiceLbl.Location = new Point(0, 0);
			this.DiceLbl.Name = "DiceLbl";
			this.DiceLbl.Size = new Size(287, 50);
			this.DiceLbl.TabIndex = 7;
			this.DiceLbl.Text = "-";
			this.DiceLbl.TextAlign = ContentAlignment.MiddleCenter;
			this.DiceList.AllowDrop = true;
			this.DiceList.Dock = DockStyle.Fill;
			this.DiceList.Location = new Point(0, 169);
			this.DiceList.Name = "DiceList";
			this.DiceList.Size = new Size(287, 136);
			this.DiceList.TabIndex = 6;
			this.DiceList.UseCompatibleStateImageBehavior = false;
			this.DiceList.DoubleClick += new EventHandler(this.DiceList_DoubleClick);
			this.DiceList.DragOver += new DragEventHandler(this.DiceList_DragOver);
			this.DiceSourceList.Dock = DockStyle.Top;
			this.DiceSourceList.Location = new Point(0, 25);
			this.DiceSourceList.Name = "DiceSourceList";
			this.DiceSourceList.Size = new Size(287, 144);
			this.DiceSourceList.TabIndex = 5;
			this.DiceSourceList.UseCompatibleStateImageBehavior = false;
			this.DiceSourceList.DoubleClick += new EventHandler(this.DiceSourceList_DoubleClick);
			this.DiceSourceList.ItemDrag += new ItemDragEventHandler(this.DiceSourceList_ItemDrag);
			this.ResultPanel.Controls.Add(this.DiceLbl);
			this.ResultPanel.Controls.Add(this.ExpressionBox);
			this.ResultPanel.Dock = DockStyle.Bottom;
			this.ResultPanel.Location = new Point(0, 235);
			this.ResultPanel.Name = "ResultPanel";
			this.ResultPanel.Size = new Size(287, 70);
			this.ResultPanel.TabIndex = 10;
			this.ExpressionBox.Dock = DockStyle.Bottom;
			this.ExpressionBox.Location = new Point(0, 50);
			this.ExpressionBox.Name = "ExpressionBox";
			this.ExpressionBox.Size = new Size(287, 20);
			this.ExpressionBox.TabIndex = 10;
			this.ExpressionBox.TextAlign = HorizontalAlignment.Center;
			this.ExpressionBox.TextChanged += new EventHandler(this.ExpressionBox_TextChanged);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.ResultPanel);
			base.Controls.Add(this.DiceList);
			base.Controls.Add(this.DiceSourceList);
			base.Controls.Add(this.DiceToolbar);
			base.Name = "DicePanel";
			base.Size = new Size(287, 305);
			this.DiceToolbar.ResumeLayout(false);
			this.DiceToolbar.PerformLayout();
			this.ResultPanel.ResumeLayout(false);
			this.ResultPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
