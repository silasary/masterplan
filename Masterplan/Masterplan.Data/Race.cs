using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
    ///<summary>
    ///Class representing a race.
    ///</summary>
    [Serializable]
	public class Race : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fQuote = "";

		private string fHeightRange = "";

		private string fWeightRange = "";

		private string fAbilityScores = "";

		private CreatureSize fSize = CreatureSize.Medium;

		private string fSpeed = "6 squares";

		private string fVision = "Normal";

		private string fLanguages = "Common";

		private string fSkillBonuses = "";

		private List<Feature> fFeatures = new List<Feature>();

		private List<PlayerPower> fPowers = new List<PlayerPower>();

		private string fDetails = "";

        ///<summary>
        ///Gets or sets the unique ID of the race.
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
        ///Gets or sets the name of the race.
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
        ///Gets or sets the defining quote.
        ///</summary>
        public string Quote
		{
			get
			{
				return this.fQuote;
			}
			set
			{
				this.fQuote = value;
			}
		}

        ///<summary>
        ///Gets or sets the height range of the race.
        ///</summary>
        public string HeightRange
		{
			get
			{
				return this.fHeightRange;
			}
			set
			{
				this.fHeightRange = value;
			}
		}

        ///<summary>
        ///Gets or sets the weight range of the race.
        ///</summary>
        public string WeightRange
		{
			get
			{
				return this.fWeightRange;
			}
			set
			{
				this.fWeightRange = value;
			}
		}

        ///<summary>
        ///Gets or sets the ability score modifiers for the race.
        ///</summary>
        public string AbilityScores
		{
			get
			{
				return this.fAbilityScores;
			}
			set
			{
				this.fAbilityScores = value;
			}
		}

        ///<summary>
        ///Gets or sets the size of the race.
        ///</summary>
        public CreatureSize Size
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

        ///<summary>
        ///Gets or sets the speed of the race.
        ///</summary>
        public string Speed
		{
			get
			{
				return this.fSpeed;
			}
			set
			{
				this.fSpeed = value;
			}
		}

        ///<summary>
        ///Gets or sets the race's vision.
        ///</summary>
        public string Vision
		{
			get
			{
				return this.fVision;
			}
			set
			{
				this.fVision = value;
			}
		}

        ///<summary>
        ///Gets or sets the race's starting languages.
        ///</summary>
        public string Languages
		{
			get
			{
				return this.fLanguages;
			}
			set
			{
				this.fLanguages = value;
			}
		}

        ///<summary>
        ///Gets or sets the race's skill bonuses.
        ///</summary>
        public string SkillBonuses
		{
			get
			{
				return this.fSkillBonuses;
			}
			set
			{
				this.fSkillBonuses = value;
			}
		}

        ///<summary>
        ///Gets or sets the racial features.
        ///</summary>
        public List<Feature> Features
		{
			get
			{
				return this.fFeatures;
			}
			set
			{
				this.fFeatures = value;
			}
		}

        ///<summary>
        ///Gets or sets the racial powers.
        ///</summary>
        public List<PlayerPower> Powers
		{
			get
			{
				return this.fPowers;
			}
			set
			{
				this.fPowers = value;
			}
		}

        ///<summary>
        ///Gets or sets the race details.
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
        ///Creates a copy of the race.
        ///</summary>
        ///<returns>Returns the copy.</returns>
        public Race Copy()
		{
            Race race = new Race()
            {
                ID = this.fID,
                Name = this.fName,
                Quote = this.fQuote,
                HeightRange = this.fHeightRange,
                WeightRange = this.fWeightRange,
                AbilityScores = this.fAbilityScores,
                Size = this.fSize,
                Speed = this.fSpeed,
                Vision = this.fVision,
                Languages = this.fLanguages,
                SkillBonuses = this.fSkillBonuses,
                Details = this.fDetails
            };
            foreach (Feature current in this.fFeatures)
			{
				race.Features.Add(current.Copy());
			}
			foreach (PlayerPower current2 in this.fPowers)
			{
				race.Powers.Add(current2.Copy());
			}
			return race;
		}

        ///<summary>
        ///Returns the name of the race.
        ///</summary>
        ///<returns>Returns the name of the race.</returns>
        public override string ToString()
		{
			return this.fName;
		}
	}
}
