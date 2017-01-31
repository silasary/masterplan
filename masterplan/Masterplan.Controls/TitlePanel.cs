using Masterplan.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class TitlePanel : UserControl
	{
		public enum TitlePanelMode
		{
			WelcomeScreen,
			PlayerView
		}

		private const int MAX_ALPHA = 255;

		private const int MAX_COLOR = 60;

		private string fTitle = "";

		private TitlePanel.TitlePanelMode fMode;

		private bool fZooming;

		private string fVersion = TitlePanel.get_version_string();

		private Rectangle fTitleRect = Rectangle.Empty;

		private Rectangle fVersionRect = Rectangle.Empty;

		private StringFormat fFormat = new StringFormat();

		private int fAlpha;

		private IContainer components;

		private Timer FadeTimer;

		private Timer PulseTimer;

        public event EventHandler FadeFinished;

		[Category("Appearance")]
		public string Title
		{
			get
			{
				return this.fTitle;
			}
			set
			{
				this.fTitle = value;
			}
		}

		[Category("Layout")]
		public TitlePanel.TitlePanelMode Mode
		{
			get
			{
				return this.fMode;
			}
			set
			{
				this.fMode = value;
				base.Invalidate();
			}
		}

		[Category("Behavior")]
		public bool Zooming
		{
			get
			{
				return this.fZooming;
			}
			set
			{
				this.fZooming = value;
			}
		}

		public TitlePanel()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fFormat.Alignment = StringAlignment.Center;
			this.fFormat.LineAlignment = StringAlignment.Center;
			this.fFormat.Trimming = StringTrimming.EllipsisWord;
			this.FadeTimer.Enabled = true;
		}

		protected void OnFadeFinished()
		{
			if (this.FadeFinished != null)
			{
				this.FadeFinished(this, new EventArgs());
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.reset_view();
		}

		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
			this.reset_view();
		}

		private void reset_view()
		{
			this.fTitleRect = Rectangle.Empty;
			this.fVersionRect = Rectangle.Empty;
			base.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			if (this.fTitleRect == Rectangle.Empty)
			{
				Rectangle clientRectangle = base.ClientRectangle;
				double num = (double)(e.Graphics.MeasureString(this.fVersion, this.Font).Height + (float)(base.Height / 10));
				this.fTitleRect = new Rectangle(clientRectangle.Left, clientRectangle.Top, clientRectangle.Width - 1, (int)((double)clientRectangle.Height - num - 1.0));
				this.fVersionRect = new Rectangle(clientRectangle.Left, this.fTitleRect.Bottom, clientRectangle.Width - 1, (int)num);
			}
			if (this.fMode == TitlePanel.TitlePanelMode.WelcomeScreen)
			{
				ColorMatrix colorMatrix = new ColorMatrix();
				colorMatrix.Matrix33 = 0.25f * (float)this.fAlpha / 255f;
				ImageAttributes imageAttributes = new ImageAttributes();
				imageAttributes.SetColorMatrix(colorMatrix);
				Image scroll = Resources.Scroll;
				int y = base.ClientRectangle.Y + (int)((double)base.ClientRectangle.Height * 0.1);
				int num2 = (int)((double)base.ClientRectangle.Height * 0.8);
				int num3 = scroll.Width * num2 / scroll.Height;
				if (num3 > base.ClientRectangle.Width)
				{
					num3 = base.ClientRectangle.Width;
					num2 = scroll.Height * num3 / scroll.Width;
				}
				int x = base.ClientRectangle.X + (base.ClientRectangle.Width - num3) / 2;
				Rectangle destRect = new Rectangle(x, y, num3, num2);
				e.Graphics.DrawImage(scroll, destRect, 0, 0, scroll.Width, scroll.Height, GraphicsUnit.Pixel, imageAttributes);
			}
			using (Brush brush = new SolidBrush(Color.FromArgb(this.fAlpha, this.ForeColor)))
			{
				float num4 = (float)this.fTitleRect.Height / 2f;
				float val = (float)(this.fTitleRect.Width / this.fTitle.Length);
				float num5 = Math.Min(num4, val);
				if (this.fZooming)
				{
					float num6 = 0.1f * (float)this.fAlpha / 255f;
					num5 *= 0.9f + num6;
				}
				if (num4 > 0f)
				{
					using (Font font = new Font(this.Font.FontFamily, num5))
					{
						e.Graphics.DrawString(this.fTitle, font, brush, this.fTitleRect, this.fFormat);
					}
				}
				if (this.fMode == TitlePanel.TitlePanelMode.WelcomeScreen)
				{
					e.Graphics.DrawString(this.fVersion, this.Font, brush, this.fVersionRect, this.fFormat);
				}
			}
		}

		private void FadeTimer_Tick(object sender, EventArgs e)
		{
			this.fAlpha = Math.Min(this.fAlpha + 4, 255);
			base.Invalidate();
			if (this.fAlpha == 255)
			{
				this.FadeTimer.Enabled = false;
				this.OnFadeFinished();
				if (this.fMode == TitlePanel.TitlePanelMode.PlayerView)
				{
					this.PulseTimer.Enabled = true;
				}
			}
		}

		private void PulseTimer_Tick(object sender, EventArgs e)
		{
			this.fAlpha = Math.Max(this.fAlpha - 1, 0);
			if (Session.Random.Next() % 10 == 0)
			{
				this.BackColor = this.change_colour(this.BackColor);
			}
			base.Invalidate();
		}

		public void Wake()
		{
			if (this.PulseTimer.Enabled)
			{
				this.PulseTimer.Enabled = false;
				this.FadeTimer.Enabled = true;
			}
		}

		private static string get_version_string()
		{
			string text = "Adventure Design Studio";
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			if (entryAssembly != null)
			{
				Version version = entryAssembly.GetName().Version;
				if (version != null)
				{
					if (text != "")
					{
						text += Environment.NewLine;
					}
					text = text + "Version " + version.Major;
					if (version.Build != 0)
					{
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							".",
							version.Minor,
							".",
							version.Build
						});
					}
					else if (version.Minor != 0)
					{
						text = text + "." + version.Minor;
					}
				}
			}
			if (Program.IsBeta)
			{
				if (text != "")
				{
					text = text + Environment.NewLine + Environment.NewLine;
				}
				text += "BETA";
			}
			return text;
		}

		private Color change_colour(Color colour)
		{
			int num = (int)colour.R;
			int num2 = (int)colour.G;
			int num3 = (int)colour.B;
			switch (Session.Random.Next() % 4)
			{
			case 0:
				num = Math.Min(60, num + 1);
				break;
			case 1:
				num2 = Math.Min(60, num2 + 1);
				break;
			case 2:
				num3 = Math.Min(60, num3 + 1);
				break;
			case 3:
				num = Math.Max(0, num - 1);
				break;
			case 4:
				num2 = Math.Max(0, num2 - 1);
				break;
			case 5:
				num3 = Math.Max(0, num3 - 1);
				break;
			}
			return Color.FromArgb(num, num2, num3);
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
			this.FadeTimer = new Timer(this.components);
			this.PulseTimer = new Timer(this.components);
			base.SuspendLayout();
			this.FadeTimer.Interval = 25;
			this.FadeTimer.Tick += new EventHandler(this.FadeTimer_Tick);
			this.PulseTimer.Tick += new EventHandler(this.PulseTimer_Tick);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.ForeColor = Color.MidnightBlue;
			base.Margin = new Padding(2, 3, 2, 3);
			base.Name = "TitlePanel";
			base.Size = new Size(150, 151);
			base.ResumeLayout(false);
		}
	}
}
