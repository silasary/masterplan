using System;
using System.Collections.Generic;

namespace Masterplan.Tools
{
	internal class Skills
	{
		public static List<string> GetAbilityNames()
		{
			return new List<string>
			{
				"Strength",
				"Constitution",
				"Dexterity",
				"Intelligence",
				"Wisdom",
				"Charisma"
			};
		}

		public static List<string> GetSkillNames()
		{
			return new List<string>
			{
				"Acrobatics",
				"Arcana",
				"Athletics",
				"Bluff",
				"Diplomacy",
				"Dungeoneering",
				"Endurance",
				"Heal",
				"History",
				"Insight",
				"Intimidate",
				"Nature",
				"Perception",
				"Religion",
				"Stealth",
				"Streetwise",
				"Thievery"
			};
		}

		public static string GetKeyAbility(string skill_name)
		{
			if (skill_name == "Athletics")
			{
				return "Strength";
			}
			if (skill_name == "Endurance")
			{
				return "Constitution";
			}
			if (skill_name == "Acrobatics" || skill_name == "Stealth" || skill_name == "Thievery")
			{
				return "Dexterity";
			}
			if (skill_name == "Arcana" || skill_name == "History" || skill_name == "Religion")
			{
				return "Intelligence";
			}
			if (skill_name == "Dungeoneering" || skill_name == "Heal" || skill_name == "Insight" || skill_name == "Nature" || skill_name == "Perception")
			{
				return "Wisdom";
			}
			if (skill_name == "Bluff" || skill_name == "Diplomacy" || skill_name == "Intimidate" || skill_name == "Streetwise")
			{
				return "Charisma";
			}
			return "";
		}
	}
}
