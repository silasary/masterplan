using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Party
	{
		private int fSize = 5;

		private int fXP;

		private int fLevel = 1;

		public int Size
		{
			get
			{
				return this.fSize;
			}
			set
			{
				this.fSize = value;
			}
		}

		public int XP
		{
			get
			{
				return this.fXP;
			}
			set
			{
				this.fXP = value;
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

		public Party()
		{
		}

		public Party(int size, int level)
		{
			this.fSize = size;
			this.fLevel = level;
		}

		public Difficulty GetDifficulty(int level)
		{
			if (level <= this.fLevel - 3)
			{
				return Difficulty.Trivial;
			}
			if (level <= this.fLevel - 1)
			{
				return Difficulty.Easy;
			}
			if (level <= this.fLevel + 1)
			{
				return Difficulty.Moderate;
			}
			if (level <= this.fLevel + 4)
			{
				return Difficulty.Hard;
			}
			return Difficulty.Extreme;
		}

		public Party Copy()
		{
			return new Party
			{
				Size = this.fSize,
				Level = this.fLevel,
				XP = this.fXP
			};
		}
	}
}
