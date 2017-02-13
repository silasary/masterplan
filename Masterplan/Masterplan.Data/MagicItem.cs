using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class MagicItem : IComparable<MagicItem>
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fType = "Weapon";

		private MagicItemRarity fRarity = MagicItemRarity.Uncommon;

		private int fLevel = 1;

		private string fDescription = "";

		private List<MagicItemSection> fSections = new List<MagicItemSection>();

		private string fURL = "";

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

		public string Type
		{
			get
			{
				return this.fType;
			}
			set
			{
				this.fType = value;
			}
		}

		public MagicItemRarity Rarity
		{
			get
			{
				return this.fRarity;
			}
			set
			{
				this.fRarity = value;
			}
		}

		public int Level
		{
			get
			{
				return this.fLevel;
			}
			set
			{
				this.fLevel = value;
			}
		}

		public string Description
		{
			get
			{
				return this.fDescription;
			}
			set
			{
				this.fDescription = value;
			}
		}

		public List<MagicItemSection> Sections
		{
			get
			{
				return this.fSections;
			}
			set
			{
				this.fSections = value;
			}
		}

		public string URL
		{
			get
			{
				return this.fURL;
			}
			set
			{
				this.fURL = value;
			}
		}

		public string Info
		{
			get
			{
				return string.Concat(new object[]
				{
					"Level ",
					this.fLevel,
					" ",
					this.fType.ToLower()
				});
			}
		}

		public MagicItem Copy()
		{
			MagicItem magicItem = new MagicItem();
			magicItem.ID = this.fID;
			magicItem.Name = this.fName;
			magicItem.Type = this.fType;
			magicItem.Rarity = this.fRarity;
			magicItem.Level = this.fLevel;
			magicItem.Description = this.fDescription;
			foreach (MagicItemSection current in this.fSections)
			{
				magicItem.Sections.Add(current.Copy());
			}
			magicItem.URL = this.fURL;
			return magicItem;
		}

		public int CompareTo(MagicItem rhs)
		{
			return this.fName.CompareTo(rhs.Name);
		}
	}
}
