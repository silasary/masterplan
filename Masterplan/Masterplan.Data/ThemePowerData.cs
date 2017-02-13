using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class ThemePowerData
	{
		private CreaturePower fPower = new CreaturePower();

		private PowerType fType;

		private List<RoleType> fRoles = new List<RoleType>();

		public CreaturePower Power
		{
			get
			{
				return this.fPower;
			}
			set
			{
				this.fPower = value;
			}
		}

		public PowerType Type
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

		public List<RoleType> Roles
		{
			get
			{
				return this.fRoles;
			}
			set
			{
				this.fRoles = value;
			}
		}

		public ThemePowerData Copy()
		{
			ThemePowerData themePowerData = new ThemePowerData();
			themePowerData.Power = this.fPower.Copy();
			themePowerData.Type = this.fType;
			foreach (RoleType current in this.fRoles)
			{
				themePowerData.Roles.Add(current);
			}
			return themePowerData;
		}

		public override string ToString()
		{
			return this.fPower.Name;
		}
	}
}
