using System;

namespace Masterplan.Data
{
	[Serializable]
	public class ComplexRole : IRole
	{
		private RoleType fType;

		private RoleFlag fFlag;

		private bool fLeader;

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

		public RoleFlag Flag
		{
			get
			{
				return this.fFlag;
			}
			set
			{
				this.fFlag = value;
			}
		}

		public bool Leader
		{
			get
			{
				return this.fLeader;
			}
			set
			{
				this.fLeader = value;
			}
		}

		public ComplexRole()
		{
		}

		public ComplexRole(RoleType type)
		{
			this.fType = type;
		}

		public IRole Copy()
		{
			return new ComplexRole
			{
				Type = this.fType,
				Flag = this.fFlag,
				Leader = this.fLeader
			};
		}

		public override string ToString()
		{
			string str = "";
			switch (this.fFlag)
			{
			case RoleFlag.Elite:
				str = "Elite ";
				break;
			case RoleFlag.Solo:
				str = "Solo ";
				break;
			}
			string str2 = this.fType.ToString();
			string str3 = this.fLeader ? " (L)" : "";
			return str + str2 + str3;
		}
	}
}
