using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Feature
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

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

		public Feature Copy()
		{
			return new Feature
			{
				ID = this.fID,
				Name = this.fName,
				Details = this.fDetails
			};
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}
