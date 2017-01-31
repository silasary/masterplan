using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Generators
{
	internal class HalflingName
	{
		public static string MaleName()
		{
			return HalflingName.name(true);
		}

		public static string FemaleName()
		{
			return HalflingName.name(false);
		}

		private static string name(bool male)
		{
			string text = "";
			string text2 = "";
			switch (Session.Random.Next(20))
			{
			case 0:
			case 1:
			case 2:
				text = HalflingName.simple(true);
				break;
			case 3:
			case 4:
			case 5:
			case 6:
				text = HalflingName.simple(true) + HalflingName.simple(false);
				break;
			case 7:
			case 8:
			case 9:
			case 10:
				text = HalflingName.simple(true);
				text2 = HalflingName.simple(true) + HalflingName.simple(false);
				break;
			case 11:
			case 12:
			case 13:
			case 14:
			case 15:
				text = HalflingName.simple(true) + HalflingName.simple(false);
				text2 = HalflingName.simple(true);
				break;
			case 16:
			case 17:
			case 18:
			case 19:
				text = HalflingName.simple(true) + HalflingName.simple(false);
				text2 = HalflingName.earned(true) + HalflingName.earned(false);
				break;
			}
			if (!male)
			{
				char c = text[text.Length - 1];
				if (!TextHelper.IsVowel(c))
				{
					text += c;
					text += "a";
				}
			}
			string text3 = text;
			if (text2 != "")
			{
				text3 = text3 + " " + text2;
			}
			return TextHelper.Capitalise(text3, true);
		}

		private static string simple(bool start)
		{
			List<string> list = new List<string>();
			list.Add("arv");
			list.Add("baris");
			list.Add("brand");
			list.Add("bren");
			list.Add("cal");
			list.Add("chen");
			list.Add("cyrr");
			list.Add("dair");
			list.Add("dal");
			list.Add("deree");
			list.Add("dric");
			list.Add("essel");
			list.Add("fur");
			list.Add("galan");
			list.Add("gen");
			list.Add("gren");
			list.Add("ien");
			list.Add("illi");
			list.Add("indy");
			list.Add("iss");
			list.Add("kal");
			list.Add("kep");
			list.Add("kin");
			list.Add("li");
			list.Add("lur");
			list.Add("mel");
			list.Add("opee");
			list.Add("ped");
			list.Add("pery");
			list.Add("penel");
			list.Add("reen");
			list.Add("rill");
			list.Add("royl");
			list.Add("sheel");
			list.Add("thea");
			list.Add("ur");
			list.Add("wort");
			list.Add("yon");
			if (!start)
			{
				list.Add("eere");
				list.Add("llalee");
			}
			int index = Session.Random.Next(list.Count);
			return list[index];
		}

		private static string earned(bool first)
		{
			List<string> list = new List<string>();
			if (first)
			{
				list.Add("laughing");
				list.Add("fast");
				list.Add("happy");
				list.Add("kind");
				list.Add("nimble");
				list.Add("little");
				list.Add("proud");
				list.Add("quick");
				list.Add("sly");
				list.Add("small");
				list.Add("smooth");
				list.Add("snug");
				list.Add("stout");
				list.Add("sweet");
				list.Add("swift");
				list.Add("warm");
				list.Add("wild");
				list.Add("young");
				list.Add("under");
			}
			else
			{
				list.Add("caller");
				list.Add("dancer");
				list.Add("strider");
				list.Add("weaver");
				list.Add("wanderer");
			}
			list.Add("badger");
			list.Add("burrow");
			list.Add("home");
			list.Add("rascal");
			list.Add("riddle");
			list.Add("bottom");
			list.Add("cloak");
			list.Add("earth");
			list.Add("eye");
			list.Add("fellow");
			list.Add("flower");
			list.Add("finger");
			list.Add("foot");
			list.Add("glen");
			list.Add("glitter");
			list.Add("gold");
			list.Add("hand");
			list.Add("heart");
			list.Add("hearth");
			list.Add("hill");
			list.Add("hollow");
			list.Add("leaf");
			list.Add("light");
			list.Add("love");
			list.Add("meadow");
			list.Add("moon");
			list.Add("reed");
			list.Add("silver");
			list.Add("skin");
			list.Add("sun");
			list.Add("thistle");
			list.Add("will");
			list.Add("whisper");
			int index = Session.Random.Next(list.Count);
			return list[index];
		}
	}
}
