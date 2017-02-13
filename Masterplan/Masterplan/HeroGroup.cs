using Masterplan.Data;
using System;
using System.Collections.Generic;

namespace Masterplan
{
	internal class HeroGroup
	{
		public List<HeroData> Heroes = new List<HeroData>();

		public List<PowerSource> RequiredPowerSources
		{
			get
			{
				Array values = Enum.GetValues(typeof(PowerSource));
				int num = int.MaxValue;
				foreach (PowerSource power_source in values)
				{
					int num2 = this.Count(power_source);
					if (num2 < num)
					{
						num = num2;
					}
				}
				List<PowerSource> list = new List<PowerSource>();
				foreach (PowerSource powerSource in values)
				{
					int num3 = this.Count(powerSource);
					if (num3 == num)
					{
						list.Add(powerSource);
					}
				}
				return list;
			}
		}

		public List<PrimaryAbility> RequiredAbilities
		{
			get
			{
				Array values = Enum.GetValues(typeof(PrimaryAbility));
				int num = 2147483647;
				foreach (PrimaryAbility key_ability in values)
				{
					int num2 = this.Count(key_ability);
					if (num2 < num)
					{
						num = num2;
					}
				}
				List<PrimaryAbility> list = new List<PrimaryAbility>();
				foreach (PrimaryAbility primaryAbility in values)
				{
					int num3 = this.Count(primaryAbility);
					if (num3 == num)
					{
						list.Add(primaryAbility);
					}
				}
				return list;
			}
		}

		public List<HeroRoleType> RequiredRoles
		{
			get
			{
				Array values = Enum.GetValues(typeof(HeroRoleType));
				int num = int.MaxValue;
				foreach (HeroRoleType role in values)
				{
					int num2 = this.Count(role);
					if (num2 < num)
					{
						num = num2;
					}
				}
				List<HeroRoleType> list = new List<HeroRoleType>();
				foreach (HeroRoleType heroRoleType in values)
				{
					if (heroRoleType != HeroRoleType.Hybrid)
					{
						int num3 = this.Count(heroRoleType);
						if (num3 == num)
						{
							list.Add(heroRoleType);
						}
					}
				}
				return list;
			}
		}

		public static HeroGroup CreateGroup(int size)
		{
			HeroGroup heroGroup = new HeroGroup();
			int num = 0;
			while (heroGroup.Heroes.Count != size)
			{
				HeroData heroData = heroGroup.Suggest();
				if (heroData != null)
				{
					heroGroup.Heroes.Add(heroData);
				}
				else
				{
					num++;
				}
				if (num >= 100)
				{
					return HeroGroup.CreateGroup(size - 1);
				}
			}
			return heroGroup;
		}

		public HeroData Suggest()
		{
			List<PowerSource> requiredPowerSources = this.RequiredPowerSources;
			List<PrimaryAbility> requiredAbilities = this.RequiredAbilities;
			List<HeroRoleType> requiredRoles = this.RequiredRoles;
			List<ClassData> list = Sourcebook.Filter(requiredPowerSources, requiredAbilities, requiredRoles);
			if (list.Count == 0)
			{
				list = Sourcebook.Filter(requiredPowerSources, new List<PrimaryAbility>(), requiredRoles);
				if (list.Count == 0)
				{
					return null;
				}
			}
			List<ClassData> list2 = new List<ClassData>();
			foreach (ClassData current in list)
			{
				if (this.Contains(current))
				{
					list2.Add(current);
				}
			}
			if (list2.Count != list.Count)
			{
				foreach (ClassData current2 in list2)
				{
					list.Remove(current2);
				}
			}
			int index = Session.Random.Next() % list.Count;
			ClassData classData = list[index];
			List<RaceData> list3 = Sourcebook.Filter(classData.KeyAbility);
			if (list3.Count == 0)
			{
				return null;
			}
			List<RaceData> list4 = new List<RaceData>();
			foreach (RaceData current3 in list3)
			{
				if (this.Contains(current3))
				{
					list4.Add(current3);
				}
			}
			if (list4.Count != list3.Count)
			{
				foreach (RaceData current4 in list4)
				{
					list3.Remove(current4);
				}
			}
			int index2 = Session.Random.Next() % list3.Count;
			RaceData rd = list3[index2];
			return new HeroData(rd, classData);
		}

		public bool Contains(ClassData cd)
		{
			foreach (HeroData current in this.Heroes)
			{
				if (current.Class == cd)
				{
					return true;
				}
			}
			return false;
		}

		public bool Contains(RaceData rd)
		{
			foreach (HeroData current in this.Heroes)
			{
				if (current.Race == rd)
				{
					return true;
				}
			}
			return false;
		}

		public int Count(PowerSource power_source)
		{
			int num = 0;
			foreach (HeroData current in Heroes)
			{
				if (current.Class != null && current.Class.PowerSource == power_source)
				{
					num++;
				}
			}
			return num;
		}

		public int Count(PrimaryAbility key_ability)
		{
			int num = 0;
			foreach (HeroData current in this.Heroes)
			{
				if (current.Class != null && current.Class.KeyAbility == key_ability)
				{
					num++;
				}
			}
			return num;
		}

		public int Count(HeroRoleType role)
		{
			int num = 0;
			foreach (HeroData current in this.Heroes)
			{
				if (current.Class != null && current.Class.Role == role)
				{
					num++;
				}
			}
			return num;
		}
	}
}
