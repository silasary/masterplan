using System;

namespace Masterplan.Data
{
    ///<summary>
    ///Class representing a section in a player power.
    ///</summary>
    [Serializable]
	public class PlayerPowerSection
	{
		private Guid fID = Guid.NewGuid();

		private string fHeader = "Effect";

		private string fDetails = "";

		private int fIndent;

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
        ///Gets or sets the section header.
        ///</summary>
        public string Header
		{
			get
			{
				return this.fHeader;
			}
			set
			{
				this.fHeader = value;
			}
		}

        ///<summary>
        ///Gets or sets the section details.
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
        ///Gets or sets the degree of indent for the section.
        ///</summary>
        public int Indent
		{
			get
			{
				return this.fIndent;
			}
			set
			{
				this.fIndent = value;
			}
		}

        ///<summary>
        ///Creates a copy of the power section.
        ///</summary>
        ///<returns>Returns the copy.</returns>
        public PlayerPowerSection Copy()
		{
			return new PlayerPowerSection
			{
				ID = this.fID,
				Header = this.fHeader,
				Details = this.fDetails,
				Indent = this.fIndent
			};
		}
	}
}
