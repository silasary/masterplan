using System;
using System.IO;
using Utils;

namespace Masterplan.Data
{
	/// <summary>
	/// Enumeration containing the known types of attachment.
	/// </summary>
	public enum AttachmentType
	{
		/// <summary>
		/// Miscellaneous file.
		/// </summary>
		Miscellaneous,

		/// <summary>
		/// Plan text file.
		/// </summary>
		PlainText,

		/// <summary>
		/// Rich text file.
		/// </summary>
		RichText,

		/// <summary>
		/// Image file.
		/// </summary>
		Image,

		/// <summary>
		/// URL link.
		/// </summary>
		URL,

		/// <summary>
		/// HTML file.
		/// </summary>
		HTML
	}

	/// <summary>
	/// Class representing a handout file.
	/// </summary>
	[Serializable]
	public class Attachment : IComparable<Attachment>
	{
		/// <summary>
		/// Gets or sets the unique ID.
		/// </summary>
		public Guid ID
		{
			get { return fID; }
			set { fID = value; }
		}
		Guid fID = Guid.NewGuid();

		/// <summary>
		/// Gets or sets the name of the handout.
		/// </summary>
		public string Name
		{
			get { return fName; }
			set { fName = value; }
		}
		string fName;

		/// <summary>
		/// Gets or sets the handout file contents.
		/// </summary>
		public byte[] Contents
		{
			get { return fContents; }
			set { fContents = value; }
		}
		byte[] fContents;

		/// <summary>
		/// Gets the type of file.
		/// </summary>
		public AttachmentType Type
		{
            get
            {
                var ext = Path.GetExtension(fName).ToLower();

                switch (ext)
                {
                    // Text

                    case ".txt":
                        return AttachmentType.PlainText;
                    case ".rtf":
                        return AttachmentType.RichText;

                    // Images

                    case ".bmp":
                    case ".jpg":
                    case ".jpeg":
                    case ".gif":
                    case ".tga":
                    case ".png":
                        return AttachmentType.Image;

                    // Web

                    case ".url":
                        return AttachmentType.URL;

                    case ".htm":
                    case ".html":
                        return AttachmentType.HTML;

                    default:
                        return AttachmentType.Miscellaneous;
                }
			}
		}

		/// <summary>
		/// Creates a copy of the handout.
		/// </summary>
		/// <returns>Returns the copy.</returns>
		public Attachment Copy()
        {
            var h = new Attachment
            {

                ID = fID,
                Name = fName,

                Contents = new byte[fContents.Length]
            };
            for (int index = 0; index != fContents.Length; ++index)
                h.Contents[index] = fContents[index];

            return h;
        }

        /// <summary>
        /// Compares this attachment to another.
        /// </summary>
        /// <param name="rhs">The other attachment.</param>
        /// <returns>Returns -1 if this attachment should be sorted before the other, +1 if the other should be sorted before this; 0 otherwise.</returns>
        public int CompareTo(Attachment rhs)
		{
			var lhs_name = Path.GetFileNameWithoutExtension(fName);
            var rhs_name = Path.GetFileNameWithoutExtension(rhs.Name);

            return lhs_name.CompareTo(rhs_name);
		}
	}
}
