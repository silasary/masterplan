using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class EpicDestiny : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fQuote = "";

		private string fPrerequisites = "21st level";

		private string fDetails = "";

		private string fImmortality = "";

		private List<LevelData> fLevels = new List<LevelData>();

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

		public string Immortality
		{
			get
			{
				return this.fImmortality;
			}
			set
			{
				this.fImmortality = value;
			}
		}

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

		public EpicDestiny Copy()
		{
			EpicDestiny epicDestiny = new EpicDestiny();
			epicDestiny.ID = this.fID;
			epicDestiny.Name = this.fName;
			epicDestiny.Quote = this.fQuote;
			epicDestiny.Prerequisites = this.fPrerequisites;
			epicDestiny.Details = this.fDetails;
			epicDestiny.Immortality = this.fImmortality;
			foreach (LevelData current in this.fLevels)
			{
				epicDestiny.Levels.Add(current.Copy());
			}
			return epicDestiny;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}
