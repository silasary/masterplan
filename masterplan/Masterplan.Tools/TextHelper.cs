using System;
using System.Collections.Generic;

namespace Masterplan.Tools
{
	internal class TextHelper
	{
		private static int LINE_LENGTH = 50;

		private static List<char> fVowels = null;

		public static string Wrap(string str)
		{
			List<string> list = new List<string>();
			while (str != "")
			{
				string item = TextHelper.get_first_line(ref str);
				list.Add(item);
			}
			string text = "";
			foreach (string current in list)
			{
				if (text != "")
				{
					text += Environment.NewLine;
				}
				text += current;
			}
			return text;
		}

		private static string get_first_line(ref string str)
		{
			int startIndex = Math.Min(TextHelper.LINE_LENGTH, str.Length);
			int num = str.IndexOf(" ", startIndex);
			string result;
			if (num == -1)
			{
				result = str;
				str = "";
			}
			else
			{
				result = str.Substring(0, num);
				str = str.Substring(num + 1);
			}
			return result;
		}

		public static string Abbreviation(string title)
		{
			string text = "";
			string[] array = title.Split(null);
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				if (!(text2 == ""))
				{
					bool flag = false;
					try
					{
						int.Parse(text2);
						flag = true;
					}
					catch
					{
						flag = false;
					}
					if (flag)
					{
						text += text2;
					}
					else
					{
						char c = text2[0];
						if (char.IsUpper(c))
						{
							text += c;
						}
					}
				}
			}
			return text;
		}

		public static bool IsVowel(char ch)
		{
			if (TextHelper.fVowels == null)
			{
				TextHelper.fVowels = new List<char>();
				TextHelper.fVowels.Add('a');
				TextHelper.fVowels.Add('e');
				TextHelper.fVowels.Add('i');
				TextHelper.fVowels.Add('o');
				TextHelper.fVowels.Add('u');
			}
			return TextHelper.fVowels.Contains(ch);
		}

		public static bool StartsWithVowel(string str)
		{
			if (str.Length == 0)
			{
				return false;
			}
			char ch = char.ToLower(str[0]);
			return TextHelper.IsVowel(ch);
		}

		public static string Capitalise(string str, bool title_case)
		{
			if (title_case)
			{
				string[] array = str.Split(null);
				str = "";
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string str2 = array2[i];
					if (str != "")
					{
						str += " ";
					}
					str += TextHelper.Capitalise(str2, false);
				}
				return str;
			}
			char c = str[0];
			return char.ToUpper(c) + str.Substring(1);
		}
	}
}
