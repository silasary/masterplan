using System;

namespace Masterplan.Data
{
	[Serializable]
	public class SkillChallengeResult
	{
		private int fSuccesses;

		private int fFails;

		public int Successes
		{
			get
			{
				return this.fSuccesses;
			}
			set
			{
				this.fSuccesses = value;
			}
		}

		public int Fails
		{
			get
			{
				return this.fFails;
			}
			set
			{
				this.fFails = value;
			}
		}

		public SkillChallengeResult Copy()
		{
			return new SkillChallengeResult
			{
				Successes = this.fSuccesses,
				Fails = this.fFails
			};
		}
	}
}
