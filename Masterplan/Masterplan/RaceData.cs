using System;
using System.Collections.Generic;

namespace Masterplan
{
	internal class RaceData
	{
		public string Name = "";

		public List<PrimaryAbility> Abilities;

		public RaceData(string name, PrimaryAbility[] abilities)
		{
			this.Name = name;
			this.Abilities = new List<PrimaryAbility>(abilities);
		}

		public override string ToString()
		{
			return this.Name;
		}
	}
}
