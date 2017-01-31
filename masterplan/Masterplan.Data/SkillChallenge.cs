using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class SkillChallenge : IElement
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private int fLevel = -1;

		private int fComplexity = 1;

		private List<SkillChallengeData> fSkills = new List<SkillChallengeData>();

		private string fSuccess = "";

		private string fFailure = "";

		private string fNotes = "";

		private Guid fMapID = Guid.Empty;

		private Guid fMapAreaID = Guid.Empty;

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

		public int Complexity
		{
			get
			{
				return this.fComplexity;
			}
			set
			{
				this.fComplexity = value;
			}
		}

		public List<SkillChallengeData> Skills
		{
			get
			{
				return this.fSkills;
			}
			set
			{
				this.fSkills = value;
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

		public string Notes
		{
			get
			{
				return this.fNotes;
			}
			set
			{
				this.fNotes = value;
			}
		}

		public Guid MapID
		{
			get
			{
				return this.fMapID;
			}
			set
			{
				this.fMapID = value;
			}
		}

		public Guid MapAreaID
		{
			get
			{
				return this.fMapAreaID;
			}
			set
			{
				this.fMapAreaID = value;
			}
		}

		public int Successes
		{
			get
			{
				return SkillChallenge.GetSuccesses(this.fComplexity);
			}
		}

		public SkillChallengeResult Results
		{
			get
			{
				SkillChallengeResult skillChallengeResult = new SkillChallengeResult();
				foreach (SkillChallengeData current in this.fSkills)
				{
					if (current.Results != null)
					{
						skillChallengeResult.Successes += current.Results.Successes;
						skillChallengeResult.Fails += current.Results.Fails;
					}
				}
				return skillChallengeResult;
			}
		}

		public string Info
		{
			get
			{
				if (this.fLevel != -1)
				{
					return string.Concat(new object[]
					{
						"Level ",
						this.fLevel,
						", ",
						this.Successes,
						" successes before 3 failures"
					});
				}
				return this.Successes + " successes before 3 failures";
			}
		}

		public static int GetSuccesses(int complexity)
		{
			return (complexity + 1) * 2;
		}

		public static int GetXP(int level, int complexity)
		{
			int num = Experience.GetCreatureXP(level) * complexity;
			if (Session.Project != null)
			{
				num = (int)((double)num * Session.Project.CampaignSettings.XP);
			}
			return num;
		}

		public int GetXP()
		{
			return SkillChallenge.GetXP(this.fLevel, this.fComplexity);
		}

		public Difficulty GetDifficulty(int party_level, int party_size)
		{
			if (this.fSkills.Count == 0)
			{
				return Difficulty.Trivial;
			}
			List<Difficulty> list = new List<Difficulty>();
			list.Add(AI.GetThreatDifficulty(this.fLevel, party_level));
			foreach (SkillChallengeData current in this.fSkills)
			{
				list.Add(current.Difficulty);
			}
			if (list.Contains(Difficulty.Extreme))
			{
				return Difficulty.Extreme;
			}
			if (list.Contains(Difficulty.Hard))
			{
				return Difficulty.Hard;
			}
			if (list.Contains(Difficulty.Moderate))
			{
				return Difficulty.Moderate;
			}
			if (list.Contains(Difficulty.Easy))
			{
				return Difficulty.Easy;
			}
			return Difficulty.Trivial;
		}

		public SkillChallengeData FindSkill(string skill_name)
		{
			foreach (SkillChallengeData current in this.fSkills)
			{
				if (current.SkillName == skill_name)
				{
					return current;
				}
			}
			return null;
		}

		public IElement Copy()
		{
			SkillChallenge skillChallenge = new SkillChallenge();
			skillChallenge.ID = this.fID;
			skillChallenge.Name = this.fName;
			skillChallenge.Level = this.fLevel;
			skillChallenge.Complexity = this.fComplexity;
			foreach (SkillChallengeData current in this.fSkills)
			{
				skillChallenge.Skills.Add(current.Copy());
			}
			skillChallenge.Success = this.fSuccess;
			skillChallenge.Failure = this.fFailure;
			skillChallenge.Notes = this.fNotes;
			skillChallenge.MapID = this.fMapID;
			skillChallenge.MapAreaID = this.fMapAreaID;
			return skillChallenge;
		}
	}
}
