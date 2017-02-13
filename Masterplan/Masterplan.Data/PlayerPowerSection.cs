using System;

namespace Masterplan.Data
{
	[Serializable]
	public class PlayerPowerSection
	{
		private Guid fID = Guid.NewGuid();

		private string fHeader = "Effect";

		private string fDetails = "";

		private int fIndent;

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
