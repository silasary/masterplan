using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Ritual : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fReadAloud = "";

		private int fLevel = 1;

		private RitualCategory fCategory;

		private string fTime = "";

		private string fDuration = "";

		private string fComponentCost = "";

		private string fMarketPrice = "";

		private string fKeySkill = "";

		private string fDetails = "";

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

		public int Level
		{
			get
			{
				return this.fLevel;
			}
			set
			{
				this.fLevel = value;
			}
		}

		public RitualCategory Category
		{
			get
			{
				return this.fCategory;
			}
			set
			{
				this.fCategory = value;
			}
		}

		public string Time
		{
			get
			{
				return this.fTime;
			}
			set
			{
				this.fTime = value;
			}
		}

		public string Duration
		{
			get
			{
				return this.fDuration;
			}
			set
			{
				this.fDuration = value;
			}
		}

		public string ComponentCost
		{
			get
			{
				return this.fComponentCost;
			}
			set
			{
				this.fComponentCost = value;
			}
		}

		public string MarketPrice
		{
			get
			{
				return this.fMarketPrice;
			}
			set
			{
				this.fMarketPrice = value;
			}
		}

		public string KeySkill
		{
			get
			{
				return this.fKeySkill;
			}
			set
			{
				this.fKeySkill = value;
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

		public Ritual Copy()
		{
			return new Ritual
			{
				ID = this.fID,
				Name = this.fName,
				ReadAloud = this.fReadAloud,
				Level = this.fLevel,
				Category = this.fCategory,
				Time = this.fTime,
				Duration = this.fDuration,
				ComponentCost = this.fComponentCost,
				MarketPrice = this.fMarketPrice,
				KeySkill = this.fKeySkill,
				Details = this.fDetails
			};
		}
	}
}
