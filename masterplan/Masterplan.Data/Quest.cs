using Masterplan.Tools;
using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Quest : IElement
	{
		private int fLevel = Session.Project.Party.Level;

		private QuestType fType = QuestType.Minor;

		private int fXP;

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

		public QuestType Type
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

		public int XP
		{
			get
			{
				switch (this.fType)
				{
				case QuestType.Major:
					return Experience.GetCreatureXP(this.fLevel);
				case QuestType.Minor:
					return this.fXP;
				default:
					return int.MinValue;
				}
			}
			set
			{
				if (this.fType == QuestType.Minor)
				{
					this.fXP = value;
				}
			}
		}

		public int GetXP()
		{
			return this.XP * Session.Project.Party.Size;
		}

		public Difficulty GetDifficulty(int party_level, int party_size)
		{
			return new Party
			{
				Level = party_level,
				Size = party_size
			}.GetDifficulty(this.fLevel);
		}

		public IElement Copy()
		{
			return new Quest
			{
				Level = this.fLevel,
				Type = this.fType,
				XP = this.fXP
			};
		}
	}
}
