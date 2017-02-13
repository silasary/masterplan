using System;

namespace Masterplan.Data
{
	[Serializable]
	public class SkillChallengeData : IComparable<SkillChallengeData>
	{
		private string fSkillName = "";

		private Difficulty fDifficulty = Difficulty.Moderate;

		private int fDCModifier;

		private SkillType fType;

		private string fDetails = "";

		private string fSuccess = "";

		private string fFailure = "";

		private SkillChallengeResult fResults = new SkillChallengeResult();

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

		public Difficulty Difficulty
		{
			get
			{
				return this.fDifficulty;
			}
			set
			{
				this.fDifficulty = value;
			}
		}

		public int DCModifier
		{
			get
			{
				return this.fDCModifier;
			}
			set
			{
				this.fDCModifier = value;
			}
		}

		public SkillType Type
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

		public string Success
		{
			get
			{
				return this.fSuccess;
			}
			set
			{
				this.fSuccess = value;
			}
		}

		public string Failure
		{
			get
			{
				return this.fFailure;
			}
			set
			{
				this.fFailure = value;
			}
		}

		public SkillChallengeResult Results
		{
			get
			{
				return this.fResults;
			}
			set
			{
				this.fResults = value;
			}
		}

		public SkillChallengeData Copy()
		{
			return new SkillChallengeData
			{
				SkillName = this.fSkillName,
				Difficulty = this.fDifficulty,
				DCModifier = this.fDCModifier,
				Type = this.fType,
				Details = this.fDetails,
				Success = this.fSuccess,
				Failure = this.fFailure,
				Results = this.fResults.Copy()
			};
		}

		public int CompareTo(SkillChallengeData rhs)
		{
			int num = this.fSkillName.CompareTo(rhs.SkillName);
			if (num == 0)
			{
				num = this.fDifficulty.CompareTo(rhs.Difficulty);
			}
			if (num == 0)
			{
				num = this.fDCModifier.CompareTo(rhs.DCModifier);
			}
			return num;
		}
	}
}
