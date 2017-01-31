using System;

namespace Masterplan.Data
{
	[Serializable]
	public class TrapSkillData : IComparable<TrapSkillData>
	{
		private Guid fID = Guid.NewGuid();

		private string fSkillName = "Perception";

		private int fDC = 10;

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

		public string SkillName
		{
			get
			{
				return this.fSkillName;
			}
			set
			{
				this.fSkillName = value;
			}
		}

		public int DC
		{
			get
			{
				return this.fDC;
			}
			set
			{
				this.fDC = value;
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

		public override string ToString()
		{
			if (this.fDC == 0)
			{
				return this.fSkillName + ": " + this.fDetails;
			}
			return string.Concat(new object[]
			{
				this.fSkillName,
				" DC ",
				this.fDC,
				": ",
				this.fDetails
			});
		}

		public TrapSkillData Copy()
		{
			return new TrapSkillData
			{
				ID = this.fID,
				SkillName = this.fSkillName,
				DC = this.fDC,
				Details = this.fDetails
			};
		}

		public int CompareTo(TrapSkillData rhs)
		{
			if (!(this.fSkillName != rhs.SkillName))
			{
				return this.fDC.CompareTo(rhs.DC);
			}
			if (this.fSkillName == "Perception")
			{
				return -1;
			}
			if (rhs.SkillName == "Perception")
			{
				return 1;
			}
			return this.fSkillName.CompareTo(rhs.SkillName);
		}
	}
}
