using System;
using System.Collections.Generic;

namespace Masterplan.Tools
{
	internal class DiceStatistics
	{
		public static Dictionary<int, int> Odds(List<int> dice, int constant)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			if (dice.Count > 0)
			{
				int num = 1;
				foreach (int current in dice)
				{
					num *= current;
				}
				int[] array = new int[dice.Count];
				array[dice.Count - 1] = 1;
				for (int i = dice.Count - 2; i >= 0; i--)
				{
					array[i] = array[i + 1] * dice[i + 1];
				}
				for (int num2 = 0; num2 != num; num2++)
				{
					List<int> list = new List<int>();
					for (int num3 = 0; num3 != dice.Count; num3++)
					{
						int num4 = dice[num3];
						int item = num2 / array[num3] % num4 + 1;
						list.Add(item);
					}
					int num5 = constant;
					foreach (int current2 in list)
					{
						num5 += current2;
					}
					if (!dictionary.ContainsKey(num5))
					{
						dictionary[num5] = 0;
					}
					dictionary[num5] = dictionary[num5] + 1;
				}
			}
			return dictionary;
		}

		public static string Expression(List<int> dice, int constant)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			foreach (int current in dice)
			{
				int num7 = current;
				switch (num7)
				{
				case 4:
					num++;
					break;
				case 5:
				case 7:
				case 9:
				case 11:
					break;
				case 6:
					num2++;
					break;
				case 8:
					num3++;
					break;
				case 10:
					num4++;
					break;
				case 12:
					num5++;
					break;
				default:
					if (num7 == 20)
					{
						num6++;
					}
					break;
				}
			}
			string text = "";
			if (num != 0)
			{
				if (text != "")
				{
					text += " + ";
				}
				text = text + num + "d4";
			}
			if (num2 != 0)
			{
				if (text != "")
				{
					text += " + ";
				}
				text = text + num2 + "d6";
			}
			if (num3 != 0)
			{
				if (text != "")
				{
					text += " + ";
				}
				text = text + num3 + "d8";
			}
			if (num4 != 0)
			{
				if (text != "")
				{
					text += " + ";
				}
				text = text + num4 + "d10";
			}
			if (num5 != 0)
			{
				if (text != "")
				{
					text += " + ";
				}
				text = text + num5 + "d12";
			}
			if (num6 != 0)
			{
				if (text != "")
				{
					text += " + ";
				}
				text = text + num6 + "d20";
			}
			if (constant != 0)
			{
				text += " ";
				if (constant > 0)
				{
					text += "+";
				}
				text += constant.ToString();
			}
			return text;
		}
	}
}
