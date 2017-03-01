using System;

namespace Masterplan.Data
{
    ///<summary>
    ///Class representing a feat.
    ///</summary>
    [Serializable]
	public class Feat : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private Tier fTier;

		private string fPrerequisites = "";

		private string fBenefits = "";

        ///<summary>
        ///Gets or sets the unique ID of the feat.
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
        ///Gets or sets the name of the feat.
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
        ///Gets or sets the feat's tier.
        ///</summary>
        public Tier Tier
		{
			get
			{
				return this.fTier;
			}
			set
			{
				this.fTier = value;
			}
		}

        ///<summary>
        ///Gets or sets the prerequisites for the feat.
        ///</summary>
        public string Prerequisites
		{
			get
			{
				return this.fPrerequisites;
			}
			set
			{
				this.fPrerequisites = value;
			}
		}

        ///<summary>
        ///Gets or sets the feat benefits.
        ///</summary>
        public string Benefits
		{
			get
			{
				return this.fBenefits;
			}
			set
			{
				this.fBenefits = value;
			}
		}

        ///<summary>
        ///Creates a copy of the feat.
        ///</summary>
        ///<returns>Returns the copy.</returns>
        public Feat Copy()
		{
			return new Feat
			{
				ID = this.fID,
				Name = this.fName,
				Tier = this.fTier,
				Prerequisites = this.fPrerequisites,
				Benefits = this.fBenefits
			};
		}

        ///<summary>
        ///Returns the name of the feat.
        ///</summary>
        ///<returns>Returns the name of the feat.</returns>
        public override string ToString()
		{
			return this.fName;
		}
	}
}
