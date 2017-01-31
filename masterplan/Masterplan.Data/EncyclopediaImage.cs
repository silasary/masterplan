using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace Masterplan.Data
{
	[Serializable]
	public class EncyclopediaImage
	{
		private string fName = "";

		private Guid fID = Guid.NewGuid();

		private Image fImage;

		public string Name
		{
			get
			{
				return this.fName;
			}
			set
			{
				this.fName = value;
			}
		}

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

		[XmlIgnore]
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

		public byte[] ImageData
		{
			get
			{
				if (this.fImage != null)
				{
					TypeConverter converter = TypeDescriptor.GetConverter(this.fImage.GetType());
					return (byte[])converter.ConvertTo(this.fImage, typeof(byte[]));
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.fImage = new Bitmap(new MemoryStream(value));
					return;
				}
				this.fImage = null;
			}
		}

		public EncyclopediaImage Copy()
		{
			return new EncyclopediaImage
			{
				ID = this.fID,
				Name = this.fName,
				Image = this.fImage
			};
		}
	}
}
