using System;

namespace Masterplan.Data
{
    ///<summary>
    ///Class representing a player background.
    ///</summary>
    [Serializable]
	public class PlayerBackground : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fDetails = "";

		private string fAssociatedSkills = "";

		private string fRecommendedFeats = "";

        ///<summary>
        ///Gets or sets the unique ID of the background.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the name of the background.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the background details.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the associated skills for the background.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the recommended feats for the background.
        ///</summary>
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

        ///<summary>
        ///Creates a copy of the background.
        ///</summary>
        ///<returns>Returns the copy.</returns>
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

        ///<summary>
        ///Returns the name of the background.
        ///</summary>
        ///<returns>Returns the name of the background.</returns>
        public override string ToString()
		{
			return this.fName;
		}
	}
}
