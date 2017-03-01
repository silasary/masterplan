using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
    ///<summary>
    ///Class representing a playable class.
    ///</summary>
    [Serializable]
	public class Class : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fQuote = "";

		private string fRole = "";

		private string fPowerSource = "";

		private string fKeyAbilities = "";

		private string fArmourProficiencies = "";

		private string fWeaponProficiencies = "";

		private string fImplements = "";

		private string fDefenceBonuses = "";

		private int fHPFirst;

		private int fHPSubsequent;

		private int fHealingSurges;

		private string fTrainedSkills = "";

		private string fDescription = "";

		private string fOverviewCharacteristics = "";

		private string fOverviewReligion = "";

		private string fOverviewRaces = "";

		private LevelData fFeatureData = new LevelData();

		private List<LevelData> fLevels = new List<LevelData>();

        ///<summary>
        ///Gets or sets the unique ID of the class.
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
        ///Gets or sets the name of the class.
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
        ///Gets or sets the class role.
        ///</summary>
        public string Role
		{
			get
			{
				return this.fRole;
			}
			set
			{
				this.fRole = value;
			}
		}

        ///<summary>
        ///Gets or sets the class's power source.
        ///</summary>
        public string PowerSource
		{
			get
			{
				return this.fPowerSource;
			}
			set
			{
				this.fPowerSource = value;
			}
		}

        ///<summary>
        ///Gets or sets the class's key abilities.
        ///</summary>
        public string KeyAbilities
		{
			get
			{
				return this.fKeyAbilities;
			}
			set
			{
				this.fKeyAbilities = value;
			}
		}

        ///<summary>
        ///Gets or sets the class's armour proficiencies.
        ///</summary>
        public string ArmourProficiencies
		{
			get
			{
				return this.fArmourProficiencies;
			}
			set
			{
				this.fArmourProficiencies = value;
			}
		}

        ///<summary>
        ///Gets or sets the class's weapon proficiencies.
        ///</summary>
        public string WeaponProficiencies
		{
			get
			{
				return this.fWeaponProficiencies;
			}
			set
			{
				this.fWeaponProficiencies = value;
			}
		}

        ///<summary>
        ///Gets or sets the class's implement proficiencies.
        ///</summary>
        public string Implements
		{
			get
			{
				return this.fImplements;
			}
			set
			{
				this.fImplements = value;
			}
		}

        ///<summary>
        ///Gets or sets the class's defence bonuses.
        ///</summary>
        public string DefenceBonuses
		{
			get
			{
				return this.fDefenceBonuses;
			}
			set
			{
				this.fDefenceBonuses = value;
			}
		}

        ///<summary>
        ///Gets or sets the class's first level HP.
        ///</summary>
        public int HPFirst
		{
			get
			{
				return this.fHPFirst;
			}
			set
			{
				this.fHPFirst = value;
			}
		}

        ///<summary>
        ///Gets or sets the class's HP per level.
        ///</summary>
        public int HPSubsequent
		{
			get
			{
				return this.fHPSubsequent;
			}
			set
			{
				this.fHPSubsequent = value;
			}
		}

        ///<summary>
        ///Gets or sets the class's healing surges.
        ///</summary>
        public int HealingSurges
		{
			get
			{
				return this.fHealingSurges;
			}
			set
			{
				this.fHealingSurges = value;
			}
		}

        ///<summary>
        ///Gets or sets the class's trained skills.
        ///</summary>
        public string TrainedSkills
		{
			get
			{
				return this.fTrainedSkills;
			}
			set
			{
				this.fTrainedSkills = value;
			}
		}

        ///<summary>
        ///Gets or sets the description for the class.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the class's overview characteristics.
        ///</summary>
        public string OverviewCharacteristics
		{
			get
			{
				return this.fOverviewCharacteristics;
			}
			set
			{
				this.fOverviewCharacteristics = value;
			}
		}

        ///<summary>
        ///Gets or sets the class's religion information.
        ///</summary>
        public string OverviewReligion
		{
			get
			{
				return this.fOverviewReligion;
			}
			set
			{
				this.fOverviewReligion = value;
			}
		}

        ///<summary>
        ///Gets or sets the class's race information.
        ///</summary>
        public string OverviewRaces
		{
			get
			{
				return this.fOverviewRaces;
			}
			set
			{
				this.fOverviewRaces = value;
			}
		}

        ///<summary>
        ///Gets or sets the class's feature powers.
        ///</summary>
        public LevelData FeatureData
		{
			get
			{
				return this.fFeatureData;
			}
			set
			{
				this.fFeatureData = value;
			}
		}

        ///<summary>
        ///Gets or sets the class's powers.
        ///</summary>
        public List<LevelData> Levels
		{
			get
			{
				return this.fLevels;
			}
			set
			{
				this.fLevels = value;
			}
		}

        ///<summary>
        ///Creates a copy of the class.
        ///</summary>
        ///<returns>Returns the copy.</returns>
        public Class Copy()
		{
            Class copy = new Class()
            {
                ID = this.fID,
                Name = this.fName,
                Quote = this.fQuote,
                Role = this.fRole,
                PowerSource = this.fPowerSource,
                KeyAbilities = this.fKeyAbilities,
                ArmourProficiencies = this.fArmourProficiencies,
                WeaponProficiencies = this.fWeaponProficiencies,
                Implements = this.fImplements,
                DefenceBonuses = this.fDefenceBonuses,
                HPFirst = this.fHPFirst,
                HPSubsequent = this.fHPSubsequent,
                HealingSurges = this.fHealingSurges,
                TrainedSkills = this.fTrainedSkills,
                Description = this.fDescription,
                OverviewCharacteristics = this.fOverviewCharacteristics,
                OverviewReligion = this.fOverviewReligion,
                OverviewRaces = this.fOverviewRaces,
                FeatureData = this.fFeatureData.Copy()
            };
            foreach (LevelData level in this.fLevels)
			{
				copy.Levels.Add(level.Copy());
			}
			return copy;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}
