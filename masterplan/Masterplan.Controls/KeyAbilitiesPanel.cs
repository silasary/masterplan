using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class KeyAbilitiesPanel : UserControl
	{
		private IContainer components;

		private StringFormat fCentred = new StringFormat();

		private Dictionary<string, int> fBreakdown;

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

		public KeyAbilitiesPanel()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
		}

		public void Analyse(SkillChallenge sc)
		{
			this.fBreakdown = new Dictionary<string, int>();
			this.fBreakdown["Strength"] = 0;
			this.fBreakdown["Constitution"] = 0;
			this.fBreakdown["Dexterity"] = 0;
			this.fBreakdown["Intelligence"] = 0;
			this.fBreakdown["Wisdom"] = 0;
			this.fBreakdown["Charisma"] = 0;
			foreach (SkillChallengeData current in sc.Skills)
			{
				if (current.Type != SkillType.AutoFail)
				{
					string text;
					if (Skills.GetAbilityNames().Contains(current.SkillName))
					{
						text = current.SkillName;
					}
					else
					{
						text = Skills.GetKeyAbility(current.SkillName);
					}
					if (this.fBreakdown.ContainsKey(text))
					{
						fBreakdown[text] = fBreakdown[text] + 1;
					}
				}
			}
			base.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(Brushes.White, base.ClientRectangle);
			if (this.fBreakdown == null)
			{
				return;
			}
			int num = 0;
			foreach (string current in this.fBreakdown.Keys)
			{
				int val = this.fBreakdown[current];
				num = Math.Max(val, num);
			}
			int num2 = 20;
			Rectangle rectangle = new Rectangle(num2, num2, base.ClientRectangle.Width - 2 * num2, base.ClientRectangle.Height - 3 * num2);
			float num3 = (float)rectangle.Width / 6f;
			for (int num4 = 0; num4 != 6; num4++)
			{
				string text = this.get_label(num4);
				if (!(text == ""))
				{
					float num5 = num3 * (float)num4;
					RectangleF layoutRectangle = new RectangleF((float)rectangle.Left + num5, (float)rectangle.Bottom, num3, (float)num2);
					e.Graphics.DrawString(text, this.Font, Brushes.Black, layoutRectangle, this.fCentred);
					int num6 = this.get_count(num4);
					if (num6 != 0)
					{
						int num7 = (rectangle.Height - num2) * num6 / num;
						RectangleF rect = new RectangleF((float)rectangle.Left + num5, (float)(rectangle.Bottom - num7), num3, (float)num7);
						using (Brush brush = new LinearGradientBrush(base.ClientRectangle, Color.LightGray, Color.White, LinearGradientMode.Vertical))
						{
							e.Graphics.FillRectangle(brush, rect);
						}
						e.Graphics.DrawRectangle(Pens.Gray, rect.X, rect.Y, rect.Width, rect.Height);
						RectangleF layoutRectangle2 = new RectangleF((float)rectangle.Left + num5, (float)rectangle.Top, num3, (float)num2);
						e.Graphics.DrawString(num6.ToString(), this.Font, Brushes.Gray, layoutRectangle2, this.fCentred);
					}
				}
			}
			e.Graphics.DrawLine(Pens.Black, rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Top);
			e.Graphics.DrawLine(Pens.Black, rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
		}

		private string get_label(int column_index)
		{
			switch (column_index)
			{
			case 0:
				return "Strength";
			case 1:
				return "Constitution";
			case 2:
				return "Dexterity";
			case 3:
				return "Intelligence";
			case 4:
				return "Wisdom";
			case 5:
				return "Charisma";
			default:
				return "";
			}
		}

		private int get_count(int column_index)
		{
			string key = this.get_label(column_index);
			return this.fBreakdown[key];
		}
	}
}
