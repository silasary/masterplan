using System;
using Utils;

namespace Masterplan.Data
{
	[Serializable]
	public class Attachment : IComparable<Attachment>
	{
		private Guid fID = Guid.NewGuid();

		private string fName;

		private byte[] fContents;

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

		public byte[] Contents
		{
			get
			{
				return this.fContents;
			}
			set
			{
				this.fContents = value;
			}
		}

		public AttachmentType Type
		{
			get
			{
				string a = FileName.Extension(this.fName).ToLower();
				if (a == "txt")
				{
					return AttachmentType.PlainText;
				}
				if (a == "rtf")
				{
					return AttachmentType.RichText;
				}
				if (a == "bmp")
				{
					return AttachmentType.Image;
				}
				if (a == "jpg")
				{
					return AttachmentType.Image;
				}
				if (a == "jpeg")
				{
					return AttachmentType.Image;
				}
				if (a == "gif")
				{
					return AttachmentType.Image;
				}
				if (a == "tga")
				{
					return AttachmentType.Image;
				}
				if (a == "png")
				{
					return AttachmentType.Image;
				}
				if (a == "url")
				{
					return AttachmentType.URL;
				}
				if (a == "htm")
				{
					return AttachmentType.HTML;
				}
				if (a == "html")
				{
					return AttachmentType.HTML;
				}
				return AttachmentType.Miscellaneous;
			}
		}

		public Attachment Copy()
		{
			Attachment attachment = new Attachment();
			attachment.ID = this.fID;
			attachment.Name = this.fName;
			attachment.Contents = new byte[this.fContents.Length];
			for (int num = 0; num != this.fContents.Length; num++)
			{
				attachment.Contents[num] = this.fContents[num];
			}
			return attachment;
		}

		public int CompareTo(Attachment rhs)
		{
			string text = FileName.Name(this.fName);
			string strB = FileName.Name(rhs.Name);
			return text.CompareTo(strB);
		}
	}
}
