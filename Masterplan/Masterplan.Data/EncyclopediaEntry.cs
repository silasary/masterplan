using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class EncyclopediaEntry : IEncyclopediaItem, IComparable<EncyclopediaEntry>
	{
		private string fName = "";

		private Guid fID = Guid.NewGuid();

		private string fCategory = "";

		private Guid fAttachmentID = Guid.Empty;

		private string fDetails = "";

		private string fDM = "";

		private List<EncyclopediaImage> fImages = new List<EncyclopediaImage>();

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

		public string Category
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

		public Guid AttachmentID
		{
			get
			{
				return this.fAttachmentID;
			}
			set
			{
				this.fAttachmentID = value;
			}
		}

		public string Details
		{
			get
			{
				return this.fDetails;
			}
			set
			{
				this.fDetails = value;
			}
		}

		public string DMInfo
		{
			get
			{
				return this.fDM;
			}
			set
			{
				this.fDM = value;
			}
		}

		public List<EncyclopediaImage> Images
		{
			get
			{
				return this.fImages;
			}
			set
			{
				this.fImages = value;
			}
		}

		public EncyclopediaImage FindImage(Guid id)
		{
			foreach (EncyclopediaImage current in this.fImages)
			{
				if (current.ID == id)
				{
					return current;
				}
			}
			return null;
		}

		public EncyclopediaEntry Copy()
		{
			EncyclopediaEntry encyclopediaEntry = new EncyclopediaEntry();
			encyclopediaEntry.Name = this.fName;
			encyclopediaEntry.ID = this.fID;
			encyclopediaEntry.Category = this.fCategory;
			encyclopediaEntry.AttachmentID = this.fAttachmentID;
			encyclopediaEntry.Details = this.fDetails;
			encyclopediaEntry.DMInfo = this.fDM;
			foreach (EncyclopediaImage current in this.fImages)
			{
				encyclopediaEntry.Images.Add(current.Copy());
			}
			return encyclopediaEntry;
		}

		public int CompareTo(EncyclopediaEntry rhs)
		{
			return this.fName.CompareTo(rhs.Name);
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}
