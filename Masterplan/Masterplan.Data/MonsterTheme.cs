using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Data
{
	[Serializable]
	public class MonsterTheme
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private List<Pair<string, int>> fSkillBonuses = new List<Pair<string, int>>();

		private List<ThemePowerData> fPowers = new List<ThemePowerData>();

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

		public List<Pair<string, int>> SkillBonuses
		{
			get
			{
				return this.fSkillBonuses;
			}
			set
			{
				this.fSkillBonuses = value;
			}
		}

		public List<ThemePowerData> Powers
		{
			get
			{
				return this.fPowers;
			}
			set
			{
				this.fPowers = value;
			}
		}

		public ThemePowerData FindPower(Guid power_id)
		{
			foreach (ThemePowerData current in this.fPowers)
			{
				if (current.Power.ID == power_id)
				{
					return current;
				}
			}
			return null;
		}

		public List<ThemePowerData> ListPowers(List<RoleType> creature_roles, PowerType type)
		{
			List<ThemePowerData> list = new List<ThemePowerData>();
			foreach (ThemePowerData current in this.fPowers)
			{
				if (current.Type == type)
				{
					if (current.Roles.Count == 0)
					{
						list.Add(current);
					}
					else
					{
						bool flag = false;
						foreach (RoleType current2 in creature_roles)
						{
							if (current.Roles.Contains(current2))
							{
								flag = true;
							}
						}
						if (flag)
						{
							list.Add(current);
						}
					}
				}
			}
			return list;
		}

		public MonsterTheme Copy()
		{
			MonsterTheme monsterTheme = new MonsterTheme();
			monsterTheme.ID = this.fID;
			monsterTheme.Name = this.fName;
			foreach (Pair<string, int> current in this.fSkillBonuses)
			{
				monsterTheme.SkillBonuses.Add(new Pair<string, int>(current.First, current.Second));
			}
			foreach (ThemePowerData current2 in this.fPowers)
			{
				monsterTheme.Powers.Add(current2.Copy());
			}
			return monsterTheme;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}
