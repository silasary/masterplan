using Masterplan.Data;
using System;

namespace Masterplan.Tools
{
	public class AI
	{
		public static Difficulty GetThreatDifficulty(int threat_level, int party_level)
		{
			int num = threat_level - party_level;
			if (num > 5)
			{
				return Difficulty.Extreme;
			}
			if (num < -3)
			{
				return Difficulty.Trivial;
			}
			return Difficulty.Easy;
		}

		public static Difficulty GetSkillDifficulty(int dc, int party_level)
		{
			int skillDC = AI.GetSkillDC(Difficulty.Easy, party_level);
			int skillDC2 = AI.GetSkillDC(Difficulty.Moderate, party_level);
			int skillDC3 = AI.GetSkillDC(Difficulty.Hard, party_level);
			int num = skillDC3 + (skillDC3 - skillDC) / 2;
			if (dc < skillDC)
			{
				return Difficulty.Trivial;
			}
			if (dc < skillDC2)
			{
				return Difficulty.Easy;
			}
			if (dc < skillDC3)
			{
				return Difficulty.Moderate;
			}
			if (dc < num)
			{
				return Difficulty.Hard;
			}
			return Difficulty.Extreme;
		}

		public static int GetSkillDC(Difficulty diff, int level)
		{
            int tier = level / 11;
            int half_level = level / 2;
			switch (diff)
			{
			case Difficulty.Easy:
				return 8 + half_level + (tier / 2); // Once every second level, plus once at 21.
			case Difficulty.Moderate:
				return 12 + half_level + tier; // Once every second level, plus once at 11 and 21.
			case Difficulty.Hard:
				return 19 + half_level + tier; // Once every second level, plus once at 11 and 21.
			}
			return 0;
		}

		public static string ExtractDamage(string source)
		{
			string[] separator = new string[]
			{
				",",
				";",
				".",
				":",
				Environment.NewLine
			};
			string[] array = source.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				string text2 = text.Trim().ToLower();
				if (text2.Contains("damage") || text2.Contains("dmg"))
				{
					string result = text2;
					return result;
				}
			}
			string[] array3 = array;
			for (int j = 0; j < array3.Length; j++)
			{
				string text3 = array3[j];
				DiceExpression diceExpression = DiceExpression.Parse(text3);
				if (diceExpression != null)
				{
					string result = text3;
					return result;
				}
			}
			return "";
		}
	}
}
