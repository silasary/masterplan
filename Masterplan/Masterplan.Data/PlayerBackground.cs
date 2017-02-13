using System;

namespace Masterplan.Data
{
	[Serializable]
	public class PlayerBackground : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fDetails = "";

		private string fAssociatedSkills = "";

		private string fRecommendedFeats = "";

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

		public string AssociatedSkills
		{
			get
			{
				return this.fAssociatedSkills;
			}
			set
			{
				this.fAssociatedSkills = value;
			}
		}

		public string RecommendedFeats
		{
			get
			{
				return this.fRecommendedFeats;
			}
			set
			{
				this.fRecommendedFeats = value;
			}
		}

		public PlayerBackground Copy()
		{
			return new PlayerBackground
			{
				ID = this.fID,
				Name = this.fName,
				Details = this.fDetails,
				AssociatedSkills = this.fAssociatedSkills,
				RecommendedFeats = this.fRecommendedFeats
			};
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}
