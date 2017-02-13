using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Generators
{
	internal class ExoticName
	{
		public static string SingleName()
		{
			return TextHelper.Capitalise(ExoticName.get_word(), true);
		}

		public static string FullName()
		{
			string str = TextHelper.Capitalise(ExoticName.get_word(), true);
			string str2 = TextHelper.Capitalise(ExoticName.get_word(), true);
			return str + " " + str2;
		}

		public static string Sentence()
		{
			string text = "";
			int num = Session.Dice(3, 6);
			for (int num2 = 0; num2 != num; num2++)
			{
				if (text != "")
				{
					text += " ";
				}
				text += ExoticName.get_word();
			}
			text += ".";
			return TextHelper.Capitalise(text, false);
		}

		private static string get_word()
		{
			List<string> list = new List<string>();
			list.Add("a");
			list.Add("e");
			list.Add("i");
			list.Add("o");
			list.Add("u");
			list.Add("ae");
			list.Add("ai");
			list.Add("ao");
			list.Add("au");
			list.Add("ea");
			list.Add("ee");
			list.Add("ei");
			list.Add("eo");
			list.Add("eu");
			list.Add("ia");
			list.Add("ie");
			list.Add("io");
			list.Add("iu");
			list.Add("oa");
			list.Add("oe");
			list.Add("oi");
			list.Add("oo");
			list.Add("ou");
			list.Add("ua");
			list.Add("ue");
			list.Add("ui");
			list.Add("uo");
			list.Add("y");
			List<string> list2 = new List<string>();
			list2.AddRange(new string[]
			{
				"b"
			});
			list2.AddRange(new string[]
			{
				"c",
				"ch"
			});
			list2.AddRange(new string[]
			{
				"d"
			});
			list2.AddRange(new string[]
			{
				"f",
				"fl",
				"fr"
			});
			list2.AddRange(new string[]
			{
				"g",
				"gh",
				"gn",
				"gr"
			});
			list2.AddRange(new string[]
			{
				"h"
			});
			list2.AddRange(new string[]
			{
				"j"
			});
			list2.AddRange(new string[]
			{
				"k",
				"kh",
				"kr"
			});
			list2.AddRange(new string[]
			{
				"l",
				"ll"
			});
			list2.AddRange(new string[]
			{
				"m"
			});
			list2.AddRange(new string[]
			{
				"n"
			});
			list2.AddRange(new string[]
			{
				"p",
				"ph",
				"pr"
			});
			list2.AddRange(new string[]
			{
				"q"
			});
			list2.AddRange(new string[]
			{
				"r",
				"rh"
			});
			list2.AddRange(new string[]
			{
				"s",
				"sc",
				"sch",
				"sh",
				"sk",
				"sp",
				"st"
			});
			list2.AddRange(new string[]
			{
				"t",
				"th"
			});
			list2.AddRange(new string[]
			{
				"v"
			});
			list2.AddRange(new string[]
			{
				"w",
				"wr"
			});
			string str = "-";
			if (Session.Random.Next(3) == 0)
			{
				str = "'";
			}
			string text = "";
			int num = Session.Random.Next(2) + 1;
			for (int num2 = 0; num2 != num; num2++)
			{
				if (text != "" && Session.Random.Next(10) == 0)
				{
					text += str;
				}
				if (text == "")
				{
					int index = Session.Random.Next(list2.Count);
					text += list2[index];
				}
				int index2 = Session.Random.Next(list.Count);
				text += list[index2];
				int index3 = Session.Random.Next(list2.Count);
				text += list2[index3];
			}
			if (Session.Random.Next(4) == 0)
			{
				int index4 = Session.Random.Next(list.Count);
				string text2 = list[index4];
				text += text2[0];
			}
			return text;
		}
	}
}
