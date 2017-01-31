using Masterplan.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class FiveByFivePanel : UserControl
	{
		private IContainer components;

		private StringFormat fCentred = new StringFormat();

		private Guid fSelectedItem = Guid.Empty;

		private Guid fHoveredItem = Guid.Empty;

		private FiveByFiveData fData;

		public FiveByFiveData Data
		{
			get
			{
				return this.fData;
			}
			set
			{
				this.fData = value;
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

		public FiveByFivePanel()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
		}

		protected override void OnDoubleClick(EventArgs e)
		{
		}
	}
}
