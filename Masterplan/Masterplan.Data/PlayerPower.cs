using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
    ///<summary>
    ///Class representing a player power.
    ///</summary>
    [Serializable]
	public class PlayerPower : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private PlayerPowerType fType = PlayerPowerType.Encounter;

		private string fReadAloud = "";

		private string fKeywords = "";

		private ActionType fAction = ActionType.Standard;

		private string fRange = "Melee weapon";

		private List<PlayerPowerSection> fSections = new List<PlayerPowerSection>();

        ///<summary>
        ///Gets or sets the unique ID of the power.
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
        ///Gets or sets the name of the power.
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
        ///Gets or sets the power's usage type.
        ///</summary>
        public PlayerPowerType Type
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

        ///<summary>
        ///Gets or sets the power's read-aloud text.
        ///</summary>
        public string ReadAloud
		{
			get
			{
				return this.fReadAloud;
			}
			set
			{
				this.fReadAloud = value;
			}
		}

        ///<summary>
        ///Gets or sets the keywords for the power.
        ///</summary>
        public string Keywords
		{
			get
			{
				return this.fKeywords;
			}
			set
			{
				this.fKeywords = value;
			}
		}

        ///<summary>
        ///Gets or sets the action required to use the power.
        ///</summary>
        public ActionType Action
		{
			get
			{
				return this.fAction;
			}
			set
			{
				this.fAction = value;
			}
		}

        ///<summary>
        ///Gets or sets the power's range.
        ///</summary>
        public string Range
		{
			get
			{
				return this.fRange;
			}
			set
			{
				this.fRange = value;
			}
		}

        ///<summary>
        ///Gets or sets the power sections.
        ///</summary>
        public List<PlayerPowerSection> Sections
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

        ///<summary>
        ///Creates a copy of the power.
        ///</summary>
        ///<returns>Returns the copy.</returns>
        public PlayerPower Copy()
		{
            PlayerPower playerPower = new PlayerPower()
            {
                ID = this.fID,
                Name = this.fName,
                Type = this.fType,
                ReadAloud = this.fReadAloud,
                Keywords = this.fKeywords,
                Action = this.fAction,
                Range = this.fRange
            };
            foreach (PlayerPowerSection current in this.fSections)
			{
				playerPower.Sections.Add(current.Copy());
			}
			return playerPower;
		}

        ///<summary>
        ///Returns the name of the power.
        ///</summary>
        ///<returns>Returns the name of the power.</returns>
        public override string ToString()
		{
			return this.fName;
		}
	}
}
