using Masterplan.Tools.Generators;
using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Parcel
	{
		private string fName = "";

		private string fDetails = "";

		private int fValue;

		private Guid fMagicItemID = Guid.Empty;

		private Guid fArtifactID = Guid.Empty;

		private Guid fHeroID = Guid.Empty;

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

		public int Value
		{
			get
			{
				return this.fValue;
			}
			set
			{
				this.fValue = value;
			}
		}

		public Guid MagicItemID
		{
			get
			{
				return this.fMagicItemID;
			}
			set
			{
				this.fMagicItemID = value;
			}
		}

		public Guid ArtifactID
		{
			get
			{
				return this.fArtifactID;
			}
			set
			{
				this.fArtifactID = value;
			}
		}

		public Guid HeroID
		{
			get
			{
				return this.fHeroID;
			}
			set
			{
				this.fHeroID = value;
			}
		}

		public Parcel()
		{
		}

		public Parcel(MagicItem item)
		{
			this.SetAsMagicItem(item);
		}

		public Parcel(Artifact artifact)
		{
			this.SetAsArtifact(artifact);
		}

		public void SetAsMagicItem(MagicItem item)
		{
			this.fName = item.Name;
			this.fDetails = item.Description;
			this.fMagicItemID = item.ID;
			this.fArtifactID = Guid.Empty;
			this.fValue = Treasure.GetItemValue(item.Level);
		}

		public void SetAsArtifact(Artifact artifact)
		{
			this.fName = artifact.Name;
			this.fDetails = artifact.Description;
			this.fMagicItemID = Guid.Empty;
			this.fArtifactID = artifact.ID;
			this.fValue = 0;
		}

		public int FindItemLevel()
		{
			MagicItem magicItem = Session.FindMagicItem(this.fMagicItemID, SearchType.Global);
			if (magicItem != null)
			{
				return magicItem.Level;
			}
			int num = Treasure.PlaceholderIDs.IndexOf(this.fMagicItemID);
			if (num != -1)
			{
				return num + 1;
			}
			if (this.fValue > 0)
			{
				for (int i = 30; i >= 1; i--)
				{
					int itemValue = Treasure.GetItemValue(i);
					if (itemValue < this.fValue)
					{
						return i;
					}
				}
			}
			return -1;
		}

		public Tier FindItemTier()
		{
			Artifact artifact = Session.FindArtifact(this.fArtifactID, SearchType.Global);
			if (artifact != null)
			{
				return artifact.Tier;
			}
			switch (Treasure.PlaceholderIDs.IndexOf(this.fMagicItemID))
			{
			case 0:
				return Tier.Heroic;
			case 1:
				return Tier.Paragon;
			case 2:
				return Tier.Epic;
			default:
				return Tier.Heroic;
			}
		}

		public Parcel Copy()
		{
			return new Parcel
			{
				Name = this.fName,
				Details = this.fDetails,
				Value = this.fValue,
				MagicItemID = this.fMagicItemID,
				ArtifactID = this.fArtifactID,
				HeroID = this.fHeroID
			};
		}
	}
}
