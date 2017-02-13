using Masterplan.Data;
using System;

namespace Masterplan
{
	internal class ClassData
	{
		public string Name = "";

		public PowerSource PowerSource;

		public PrimaryAbility KeyAbility;

		public HeroRoleType Role;

		public ClassData(string name, PowerSource power_source, PrimaryAbility key_ability, HeroRoleType role)
		{
			this.Name = name;
			this.PowerSource = power_source;
			this.KeyAbility = key_ability;
			this.Role = role;
		}

		public override string ToString()
		{
			return this.Name;
		}
	}
}
