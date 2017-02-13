using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Satellite
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private int fPeriod = 1;

		private int fOffset;

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

		public int Period
		{
			get
			{
				return this.fPeriod;
			}
			set
			{
				this.fPeriod = value;
			}
		}

		public int Offset
		{
			get
			{
				return this.fOffset;
			}
			set
			{
				this.fOffset = value;
			}
		}

		public Satellite Copy()
		{
			return new Satellite
			{
				ID = this.fID,
				Name = this.fName,
				Period = this.fPeriod,
				Offset = this.fOffset
			};
		}
	}
}
