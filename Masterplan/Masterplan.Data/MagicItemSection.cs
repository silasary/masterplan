using System;

namespace Masterplan.Data
{
	[Serializable]
	public class MagicItemSection
	{
		private string fHeader = "";

		private string fDetails = "";

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

		public MagicItemSection Copy()
		{
			return new MagicItemSection
			{
				Header = this.fHeader,
				Details = this.fDetails
			};
		}

		public override string ToString()
		{
			if (this.fDetails != "")
			{
				return this.fHeader + ": " + this.fDetails;
			}
			return this.fHeader;
		}
	}
}
