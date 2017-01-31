using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
	public class StringHelper
	{
		public static string LongestCommonToken(string str1, string str2)
		{
			string[] array = str1.Split(null);
			string[] array2 = str2.Split(null);
			List<string> list = new List<string>();
			string[] array3 = array;
			for (int i = 0; i < array3.Length; i++)
			{
				string str3 = array3[i];
				string[] array4 = array2;
				for (int j = 0; j < array4.Length; j++)
				{
					string str4 = array4[j];
					string text = StringHelper.LongestCommonSubstring(str3, str4);
					if (text != "")
					{
						list.Add(text);
					}
				}
			}
			string text2 = "";
			foreach (string current in list)
			{
				if (current.Length > text2.Length)
				{
					text2 = current;
				}
			}
			return text2;
		}

		public static string LongestCommonSubstring(string str1, string str2)
		{
			if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
			{
				return "";
			}
			int[,] array = new int[str1.Length, str2.Length];
			int num = 0;
			int num2 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < str1.Length; i++)
			{
				for (int j = 0; j < str2.Length; j++)
				{
					if (str1[i] != str2[j])
					{
						array[i, j] = 0;
					}
					else
					{
						if (i == 0 || j == 0)
						{
							array[i, j] = 1;
						}
						else
						{
							array[i, j] = 1 + array[i - 1, j - 1];
						}
						if (array[i, j] > num)
						{
							num = array[i, j];
							int num3 = i - array[i, j] + 1;
							if (num2 == num3)
							{
								stringBuilder.Append(str1[i]);
							}
							else
							{
								num2 = num3;
								stringBuilder.Remove(0, stringBuilder.Length);
								stringBuilder.Append(str1.Substring(num2, i + 1 - num2));
							}
						}
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
}
