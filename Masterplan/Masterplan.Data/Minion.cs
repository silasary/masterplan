using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Minion : IRole
	{
		private bool fHasRole;

		private RoleType fType;

		public bool HasRole
		{
			get
			{
				return this.fHasRole;
			}
			set
			{
				this.fHasRole = value;
			}
		}

		public RoleType Type
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

		public IRole Copy()
		{
			return new Minion
			{
				HasRole = this.fHasRole,
				Type = this.fType
			};
		}

		public override string ToString()
		{
			if (this.fHasRole)
			{
				return "Minion " + this.fType;
			}
			return "Minion";
		}
	}
}
