using Masterplan.Data;
using System;

namespace Masterplan
{
	internal class HeroData
	{
		public RaceData Race;

		public ClassData Class;

		public HeroData(RaceData rd, ClassData cd)
		{
			this.Race = rd;
			this.Class = cd;
		}

		public static HeroData Create()
		{
			int index = Session.Random.Next() % Sourcebook.Classes.Count;
			ClassData cd = Sourcebook.Classes[index];
			int index2 = Session.Random.Next() % Sourcebook.Races.Count;
			RaceData rd = Sourcebook.Races[index2];
			return new HeroData(rd, cd);
		}

		public Hero ConvertToHero()
		{
			return new Hero
			{
				Name = this.Race.Name + " " + this.Class.Name,
				Class = this.Class.Name,
				Role = this.Class.Role,
				PowerSource = this.Class.PowerSource.ToString(),
				Race = this.Race.Name
			};
		}
	}
}
