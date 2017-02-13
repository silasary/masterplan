using Masterplan.Tools;
using System;

namespace Masterplan.Data
{
	[Serializable]
	public class TrapElement : IElement
	{
		private Trap fTrap = new Trap();

		private Guid fMapID = Guid.Empty;

		private Guid fMapAreaID = Guid.Empty;

		public Trap Trap
		{
			get
			{
				return this.fTrap;
			}
			set
			{
				this.fTrap = value;
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

		public int GetXP()
		{
			return this.fTrap.XP;
		}

		public Difficulty GetDifficulty(int party_level, int party_size)
		{
			return AI.GetThreatDifficulty(this.fTrap.Level, party_level);
		}

		public IElement Copy()
		{
			return new TrapElement
			{
				Trap = this.fTrap.Copy(),
				MapID = this.fMapID,
				MapAreaID = this.fMapAreaID
			};
		}
	}
}
