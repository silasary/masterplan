using Masterplan.Data;
using System;
using System.Collections.Generic;

namespace Masterplan
{
	internal class Sourcebook
	{
		private static List<ClassData> all_classes;

		private static List<RaceData> all_races;

		public static List<ClassData> Classes
		{
			get
			{
				if (all_classes == null)
				{
					Sourcebook.all_classes = new List<ClassData>();
					Sourcebook.all_classes.Add(new ClassData("Cleric", PowerSource.Divine, PrimaryAbility.Wisdom, HeroRoleType.Leader));
					Sourcebook.all_classes.Add(new ClassData("Cleric", PowerSource.Divine, PrimaryAbility.Strength, HeroRoleType.Leader));
					Sourcebook.all_classes.Add(new ClassData("Fighter", PowerSource.Martial, PrimaryAbility.Strength, HeroRoleType.Defender));
					Sourcebook.all_classes.Add(new ClassData("Paladin", PowerSource.Divine, PrimaryAbility.Strength, HeroRoleType.Defender));
					Sourcebook.all_classes.Add(new ClassData("Paladin", PowerSource.Divine, PrimaryAbility.Charisma, HeroRoleType.Defender));
					Sourcebook.all_classes.Add(new ClassData("Ranger", PowerSource.Martial, PrimaryAbility.Strength, HeroRoleType.Striker));
					Sourcebook.all_classes.Add(new ClassData("Ranger", PowerSource.Martial, PrimaryAbility.Dexterity, HeroRoleType.Striker));
					Sourcebook.all_classes.Add(new ClassData("Rogue", PowerSource.Martial, PrimaryAbility.Dexterity, HeroRoleType.Striker));
					Sourcebook.all_classes.Add(new ClassData("Warlock", PowerSource.Arcane, PrimaryAbility.Charisma, HeroRoleType.Striker));
					Sourcebook.all_classes.Add(new ClassData("Warlord", PowerSource.Martial, PrimaryAbility.Strength, HeroRoleType.Leader));
					Sourcebook.all_classes.Add(new ClassData("Wizard", PowerSource.Arcane, PrimaryAbility.Intelligence, HeroRoleType.Controller));
					Sourcebook.all_classes.Add(new ClassData("Avenger", PowerSource.Divine, PrimaryAbility.Wisdom, HeroRoleType.Striker));
					Sourcebook.all_classes.Add(new ClassData("Barbarian", PowerSource.Primal, PrimaryAbility.Strength, HeroRoleType.Striker));
					Sourcebook.all_classes.Add(new ClassData("Bard", PowerSource.Arcane, PrimaryAbility.Charisma, HeroRoleType.Leader));
					Sourcebook.all_classes.Add(new ClassData("Druid", PowerSource.Primal, PrimaryAbility.Wisdom, HeroRoleType.Controller));
					Sourcebook.all_classes.Add(new ClassData("Invoker", PowerSource.Divine, PrimaryAbility.Wisdom, HeroRoleType.Controller));
					Sourcebook.all_classes.Add(new ClassData("Shaman", PowerSource.Primal, PrimaryAbility.Wisdom, HeroRoleType.Leader));
					Sourcebook.all_classes.Add(new ClassData("Sorcerer", PowerSource.Arcane, PrimaryAbility.Charisma, HeroRoleType.Striker));
					Sourcebook.all_classes.Add(new ClassData("Warden", PowerSource.Primal, PrimaryAbility.Strength, HeroRoleType.Defender));
					Sourcebook.all_classes.Add(new ClassData("Ardent", PowerSource.Psionic, PrimaryAbility.Charisma, HeroRoleType.Leader));
					Sourcebook.all_classes.Add(new ClassData("Battlemind", PowerSource.Psionic, PrimaryAbility.Constitution, HeroRoleType.Defender));
					Sourcebook.all_classes.Add(new ClassData("Monk", PowerSource.Psionic, PrimaryAbility.Dexterity, HeroRoleType.Striker));
					Sourcebook.all_classes.Add(new ClassData("Psion", PowerSource.Psionic, PrimaryAbility.Intelligence, HeroRoleType.Controller));
					Sourcebook.all_classes.Add(new ClassData("Runepriest", PowerSource.Divine, PrimaryAbility.Strength, HeroRoleType.Leader));
					Sourcebook.all_classes.Add(new ClassData("Seeker", PowerSource.Primal, PrimaryAbility.Wisdom, HeroRoleType.Controller));
					Sourcebook.all_classes.Add(new ClassData("Artificer", PowerSource.Arcane, PrimaryAbility.Intelligence, HeroRoleType.Leader));
					Sourcebook.all_classes.Add(new ClassData("Assassin", PowerSource.Shadow, PrimaryAbility.Dexterity, HeroRoleType.Striker));
					Sourcebook.all_classes.Add(new ClassData("Swordmage", PowerSource.Arcane, PrimaryAbility.Intelligence, HeroRoleType.Defender));
					Sourcebook.all_classes.Add(new ClassData("Vampire", PowerSource.Shadow, PrimaryAbility.Dexterity, HeroRoleType.Striker));
				}
				return Sourcebook.all_classes;
			}
		}

		public static List<RaceData> Races
		{
			get
			{
				if (Sourcebook.all_races == null)
				{
					Sourcebook.all_races = new List<RaceData>();
					Sourcebook.all_races.Add(new RaceData("Dragonborn", new PrimaryAbility[]
                    {
					    PrimaryAbility.Charisma,
                        PrimaryAbility.Strength

                    }));
					Sourcebook.all_races.Add(new RaceData("Dwarf", new PrimaryAbility[]
					{
						PrimaryAbility.Constitution,
						PrimaryAbility.Wisdom
					}));
					Sourcebook.all_races.Add(new RaceData("Eladrin", new PrimaryAbility[]
					{
						PrimaryAbility.Dexterity,
						PrimaryAbility.Intelligence
					}));
					Sourcebook.all_races.Add(new RaceData("Elf", new PrimaryAbility[]
					{
						PrimaryAbility.Dexterity,
						PrimaryAbility.Wisdom
					}));
					Sourcebook.all_races.Add(new RaceData("Half-Elf", new PrimaryAbility[]
					{
						PrimaryAbility.Charisma,
						PrimaryAbility.Constitution
					}));
					Sourcebook.all_races.Add(new RaceData("Halfling", new PrimaryAbility[]
					{
						PrimaryAbility.Charisma,
						PrimaryAbility.Dexterity
					}));
					Sourcebook.all_races.Add(new RaceData("Human", new PrimaryAbility[]
					{
						PrimaryAbility.Strength,
						PrimaryAbility.Constitution,
						PrimaryAbility.Dexterity,
						PrimaryAbility.Intelligence,
						PrimaryAbility.Wisdom,
						PrimaryAbility.Charisma
					}));
					Sourcebook.all_races.Add(new RaceData("Tiefling", new PrimaryAbility[]
					{
						PrimaryAbility.Charisma,
						PrimaryAbility.Intelligence
					}));
					Sourcebook.all_races.Add(new RaceData("Deva", new PrimaryAbility[]
					{
						PrimaryAbility.Intelligence,
						PrimaryAbility.Wisdom
					}));
					Sourcebook.all_races.Add(new RaceData("Gnome", new PrimaryAbility[]
					{
						PrimaryAbility.Charisma,
						PrimaryAbility.Intelligence
					}));
					List<RaceData> arg_1BD_0 = Sourcebook.all_races;
					string arg_1B8_0 = "Goliath";
					PrimaryAbility[] array2 = new PrimaryAbility[2];
					array2[0] = PrimaryAbility.Constitution;
					arg_1BD_0.Add(new RaceData(arg_1B8_0, array2));
					List<RaceData> arg_1E0_0 = Sourcebook.all_races;
					string arg_1DB_0 = "Half-Orc";
					PrimaryAbility[] array3 = new PrimaryAbility[2];
					array3[0] = PrimaryAbility.Dexterity;
					arg_1E0_0.Add(new RaceData(arg_1DB_0, array3));
					Sourcebook.all_races.Add(new RaceData("Longtooth Shifter", new PrimaryAbility[]
					{
						PrimaryAbility.Strength,
						PrimaryAbility.Wisdom
					}));
					Sourcebook.all_races.Add(new RaceData("Razorclaw Shifter", new PrimaryAbility[]
					{
						PrimaryAbility.Dexterity,
						PrimaryAbility.Wisdom
					}));
					Sourcebook.all_races.Add(new RaceData("Githzerai", new PrimaryAbility[]
					{
						PrimaryAbility.Wisdom,
						PrimaryAbility.Dexterity,
						PrimaryAbility.Intelligence
					}));
					Sourcebook.all_races.Add(new RaceData("Minotaur", new PrimaryAbility[]
					{
						PrimaryAbility.Strength,
						PrimaryAbility.Constitution,
						PrimaryAbility.Wisdom
					}));
					Sourcebook.all_races.Add(new RaceData("Shardmind", new PrimaryAbility[]
					{
						PrimaryAbility.Intelligence,
						PrimaryAbility.Charisma,
						PrimaryAbility.Wisdom
					}));
					Sourcebook.all_races.Add(new RaceData("Wilden", new PrimaryAbility[]
					{
						PrimaryAbility.Wisdom,
						PrimaryAbility.Constitution,
						PrimaryAbility.Dexterity
					}));
					Sourcebook.all_races.Add(new RaceData("Drow", new PrimaryAbility[]
					{
						PrimaryAbility.Charisma,
						PrimaryAbility.Dexterity
					}));
					List<RaceData> arg_325_0 = Sourcebook.all_races;
					string arg_320_0 = "Genasi";
					PrimaryAbility[] array4 = new PrimaryAbility[2];
					array4[0] = PrimaryAbility.Intelligence;
					arg_325_0.Add(new RaceData(arg_320_0, array4));
					Sourcebook.all_races.Add(new RaceData("Changeling", new PrimaryAbility[]
					{
						PrimaryAbility.Charisma,
						PrimaryAbility.Dexterity,
						PrimaryAbility.Intelligence
					}));
					Sourcebook.all_races.Add(new RaceData("Kalashtar", new PrimaryAbility[]
					{
						PrimaryAbility.Charisma,
						PrimaryAbility.Wisdom
					}));
					List<RaceData> arg_39D_0 = Sourcebook.all_races;
					string arg_398_0 = "Warforged";
					PrimaryAbility[] array5 = new PrimaryAbility[2];
					array5[0] = PrimaryAbility.Constitution;
					arg_39D_0.Add(new RaceData(arg_398_0, array5));
					Sourcebook.all_races.Add(new RaceData("Revenant", new PrimaryAbility[]
					{
						PrimaryAbility.Constitution,
						PrimaryAbility.Dexterity
					}));
					Sourcebook.all_races.Add(new RaceData("Shadar-kai", new PrimaryAbility[]
					{
						PrimaryAbility.Dexterity,
						PrimaryAbility.Intelligence
					}));
					Sourcebook.all_races.Add(new RaceData("Shade", new PrimaryAbility[]
					{
						PrimaryAbility.Dexterity,
						PrimaryAbility.Charisma
					}));
					Sourcebook.all_races.Add(new RaceData("Vryloka", new PrimaryAbility[]
					{
						PrimaryAbility.Dexterity,
						PrimaryAbility.Charisma
					}));
				}
				return Sourcebook.all_races;
			}
		}

		public static List<ClassData> Filter(List<PowerSource> power_sources, List<PrimaryAbility> abilities, List<HeroRoleType> roles)
		{
			List<ClassData> list = new List<ClassData>();
			foreach (ClassData current in Sourcebook.Classes)
			{
				if ((power_sources.Count == 0 || power_sources.Contains(current.PowerSource)) && (abilities.Count == 0 || abilities.Contains(current.KeyAbility)) && (roles.Count == 0 || roles.Contains(current.Role)))
				{
					list.Add(current);
				}
			}
			return list;
		}

		public static List<RaceData> Filter(PrimaryAbility ability)
		{
			List<RaceData> list = new List<RaceData>();
			foreach (RaceData current in Sourcebook.Races)
			{
				if (current.Abilities.Contains(ability))
				{
					list.Add(current);
				}
			}
			return list;
		}

		public static ClassData GetClass(string name)
		{
			foreach (ClassData current in Sourcebook.Classes)
			{
				if (current.Name == name)
				{
					return current;
				}
			}
			return null;
		}

		public static RaceData GetRace(string name)
		{
			foreach (RaceData current in Sourcebook.Races)
			{
				if (current.Name == name)
				{
					return current;
				}
			}
			return null;
		}
	}
}
