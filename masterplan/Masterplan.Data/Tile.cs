using System;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class Tile
	{
		private Guid fID = Guid.NewGuid();

		private TileCategory fCategory = TileCategory.Special;

		private Size fSize = new Size(2, 2);

		private Image fImage;

		private Color fBlankColour = Color.White;

		private string fKeywords = "";

		public Guid ID
		{
			get
			{
				return this.fID;
			}
			set
			{
				this.fID = value;
			}
		}

		public TileCategory Category
		{
			get
			{
				return this.fCategory;
			}
			set
			{
				this.fCategory = value;
			}
		}

		public Size Size
		{
			get
			{
				return this.fSize;
			}
			set
			{
				this.fSize = value;
			}
		}

		public Image Image
		{
			get
			{
				return this.fImage;
			}
			set
			{
				this.fImage = value;
			}
		}

		public Color BlankColour
		{
			get
			{
				return this.fBlankColour;
			}
			set
			{
				this.fBlankColour = value;
			}
		}

		public string Keywords
		{
			get
			{
				return this.fKeywords;
			}
			set
			{
				this.fKeywords = value;
			}
		}

		public int Area
		{
			get
			{
				return this.fSize.Width * this.fSize.Height;
			}
		}

		public Image BlankImage
		{
			get
			{
				int num = 32;
				int num2 = this.fSize.Width * num + 1;
				int num3 = this.fSize.Height * num + 1;
				Bitmap bitmap = new Bitmap(num2, num3);
				for (int num4 = 0; num4 != num2; num4++)
				{
					for (int num5 = 0; num5 != num3; num5++)
					{
						Color darkGray = this.fBlankColour;
						if (num4 % num == 0 || num5 % num == 0)
						{
							darkGray = Color.DarkGray;
						}
						bitmap.SetPixel(num4, num5, darkGray);
					}
				}
				return bitmap;
			}
		}

		public override string ToString()
		{
			return this.fSize.Width + " x " + this.fSize.Height;
		}

		public Tile Copy()
		{
			return new Tile
			{
				ID = this.fID,
				Category = this.fCategory,
				Size = new Size(this.fSize.Width, this.fSize.Height),
				Image = this.fImage,
				Keywords = this.fKeywords
			};
		}
	}
}
