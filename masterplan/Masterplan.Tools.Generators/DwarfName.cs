using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Generators
{
	internal class DwarfName
	{
		public static string MaleName()
		{
			return DwarfName.name(true);
		}

		public static string FemaleName()
		{
			return DwarfName.name(false);
		}

		public static string Sentence()
		{
			string text = "";
			int num = Session.Dice(4, 8);
			for (int num2 = 0; num2 != num; num2++)
			{
				string text2 = "";
				int num3 = 0;
				switch (Session.Random.Next(4))
				{
				case 0:
					num3 = 1;
					break;
				case 1:
				case 2:
					num3 = 2;
					break;
				case 3:
					num3 = 3;
					break;
				}
				for (int num4 = 0; num4 != num3; num4++)
				{
					switch (Session.Random.Next(2))
					{
					case 0:
						text2 += DwarfName.prefix();
						break;
					case 1:
						text2 += DwarfName.suffix_male();
						break;
					}
					if (num4 != num3 && Session.Random.Next(10) == 0)
					{
						List<string> list = new List<string>();
						list.Add("k");
						list.Add("z");
						list.Add("g");
						list.Add("-");
						list.Add("'");
						int index = Session.Random.Next(list.Count);
						text2 += list[index];
					}
				}
				text2 = text2.ToLower();
				if (text == "")
				{
					text2 = TextHelper.Capitalise(text2, false);
				}
				else
				{
					text += " ";
					if (Session.Random.Next(20) == 0)
					{
						text2 = TextHelper.Capitalise(text2, false);
					}
				}
				text += text2;
			}
			return text + ".";
		}

		private static string name(bool male)
		{
			string str = "";
			switch (Session.Random.Next(8))
			{
			case 0:
				str = DwarfName.prefix() + (male ? DwarfName.suffix_male() : DwarfName.suffix_female());
				break;
			case 1:
			case 2:
			case 3:
			case 4:
				str = string.Concat(new string[]
				{
					DwarfName.prefix(),
					male ? DwarfName.suffix_male() : DwarfName.suffix_female(),
					" ",
					DwarfName.thing(true),
					DwarfName.thing(false)
				});
				break;
			case 5:
			case 6:
				str = string.Concat(new string[]
				{
					DwarfName.prefix(),
					male ? DwarfName.suffix_male() : DwarfName.suffix_female(),
					" ",
					DwarfName.prefix(),
					male ? DwarfName.suffix_male() : DwarfName.suffix_female()
				});
				break;
			case 7:
				str = string.Concat(new string[]
				{
					DwarfName.prefix(),
					male ? DwarfName.suffix_male() : DwarfName.suffix_female(),
					" ",
					DwarfName.prefix(),
					male ? DwarfName.suffix_male() : DwarfName.suffix_female(),
					" '",
					DwarfName.thing(true),
					"-",
					DwarfName.thing(false),
					"'"
				});
				break;
			}
			return TextHelper.Capitalise(str, true);
		}

		private static string prefix()
		{
			List<string> list = new List<string>();
			list.Add("Al");
			list.Add("An");
			list.Add("Ar");
			list.Add("Ara");
			list.Add("Az");
			list.Add("Bal");
			list.Add("Bar");
			list.Add("Bari");
			list.Add("Baz");
			list.Add("Bel");
			list.Add("Bof");
			list.Add("Bol");
			list.Add("Dal");
			list.Add("Dar");
			list.Add("Dare");
			list.Add("Del");
			list.Add("Dol");
			list.Add("Dor");
			list.Add("Dora");
			list.Add("Duer");
			list.Add("Dur");
			list.Add("Duri");
			list.Add("Dw");
			list.Add("Dwo");
			list.Add("Dy");
			list.Add("El");
			list.Add("Er");
			list.Add("Eri");
			list.Add("Fal");
			list.Add("Fall");
			list.Add("Far");
			list.Add("Gar");
			list.Add("Gil");
			list.Add("Gim");
			list.Add("Glan");
			list.Add("Glor");
			list.Add("Glori");
			list.Add("Har");
			list.Add("Hel");
			list.Add("Jar");
			list.Add("Kil");
			list.Add("Ma");
			list.Add("Mar");
			list.Add("Mor");
			list.Add("Mori");
			list.Add("Nal");
			list.Add("Nor");
			list.Add("Nora");
			list.Add("Nur");
			list.Add("Nura");
			list.Add("Ol");
			list.Add("Or");
			list.Add("Ori");
			list.Add("Ov");
			list.Add("Rei");
			list.Add("Th");
			list.Add("Ther");
			list.Add("Tho");
			list.Add("Thor");
			list.Add("Thr");
			list.Add("Thra");
			list.Add("Tor");
			list.Add("Tore");
			list.Add("Ur");
			list.Add("Urni");
			list.Add("Val");
			list.Add("Von");
			list.Add("Wer");
			list.Add("Wera");
			list.Add("Whur");
			list.Add("Yur");
			int index = Session.Random.Next(list.Count);
			return list[index];
		}

		private static string suffix_male()
		{
			List<string> list = new List<string>();
			list.Add("aim");
			list.Add("and");
			list.Add("ain");
			list.Add("arn");
			list.Add("ak");
			list.Add("ar");
			list.Add("ard");
			list.Add("auk");
			list.Add("bere");
			list.Add("bir");
			list.Add("bin");
			list.Add("dak");
			list.Add("dek");
			list.Add("dal");
			list.Add("din");
			list.Add("el");
			list.Add("ent");
			list.Add("erl");
			list.Add("gal");
			list.Add("gan");
			list.Add("gar");
			list.Add("gath");
			list.Add("gen");
			list.Add("grim");
			list.Add("gur");
			list.Add("guk");
			list.Add("ik");
			list.Add("ias");
			list.Add("ili");
			list.Add("li");
			list.Add("im");
			list.Add("rim");
			list.Add("in");
			list.Add("rin");
			list.Add("ir");
			list.Add("init");
			list.Add("kas");
			list.Add("kral");
			list.Add("lond");
			list.Add("oak");
			list.Add("on");
			list.Add("lon");
			list.Add("or");
			list.Add("ror");
			list.Add("oril");
			list.Add("oric");
			list.Add("rak");
			list.Add("ral");
			list.Add("rek");
			list.Add("ric");
			list.Add("rid");
			list.Add("rim");
			list.Add("ring");
			list.Add("ster");
			list.Add("stili");
			list.Add("sun");
			list.Add("ten");
			list.Add("thal");
			list.Add("then");
			list.Add("thic");
			list.Add("thur");
			list.Add("ur");
			list.Add("rur");
			list.Add("urt");
			list.Add("ut");
			list.Add("unt");
			list.Add("val");
			list.Add("var");
			list.Add("villi");
			int index = Session.Random.Next(list.Count);
			return list[index];
		}

		private static string suffix_female()
		{
			List<string> list = new List<string>();
			list.Add("aed");
			list.Add("ala");
			list.Add("la");
			list.Add("alsia");
			list.Add("ana");
			list.Add("ani");
			list.Add("astr");
			list.Add("bela");
			list.Add("bera");
			list.Add("bena");
			list.Add("bo");
			list.Add("bryn");
			list.Add("deth");
			list.Add("dis");
			list.Add("dred");
			list.Add("drid");
			list.Add("dris");
			list.Add("esli");
			list.Add("gret");
			list.Add("gunn");
			list.Add("hild");
			list.Add("ia");
			list.Add("ida");
			list.Add("iess");
			list.Add("iff");
			list.Add("ifra");
			list.Add("ila");
			list.Add("ild");
			list.Add("ina");
			list.Add("ip");
			list.Add("ippa");
			list.Add("isi");
			list.Add("iz");
			list.Add("ja");
			list.Add("kara");
			list.Add("li");
			list.Add("ili");
			list.Add("lin");
			list.Add("lydd");
			list.Add("mora");
			list.Add("moa");
			list.Add("ola");
			list.Add("on");
			list.Add("ona");
			list.Add("ora");
			list.Add("oa");
			list.Add("re");
			list.Add("rra");
			list.Add("ren");
			list.Add("serd");
			list.Add("shar");
			list.Add("sha");
			list.Add("thra");
			list.Add("tia");
			list.Add("tryd");
			list.Add("unn");
			list.Add("wynn");
			list.Add("ya");
			list.Add("ydd");
			int index = Session.Random.Next(list.Count);
			return list[index];
		}

		private static string thing(bool first)
		{
			List<string> list = new List<string>();
			list.Add("forge");
			list.Add("anvil");
			list.Add("hammer");
			list.Add("maul");
			list.Add("shield");
			list.Add("hide");
			list.Add("gold");
			list.Add("silver");
			list.Add("bronze");
			list.Add("brass");
			list.Add("steel");
			list.Add("iron");
			list.Add("copper");
			list.Add("glint");
			list.Add("rock");
			list.Add("stone");
			list.Add("crag");
			list.Add("mountain");
			list.Add("cave");
			list.Add("wrath");
			list.Add("foe");
			list.Add("bound");
			if (first)
			{
				list.Add("goblin");
				list.Add("drake");
				list.Add("giant");
				list.Add("wolf");
				list.Add("boar");
				list.Add("bear");
				list.Add("red");
				list.Add("black");
				list.Add("white");
				list.Add("winter");
				list.Add("ice");
				list.Add("storm");
				list.Add("deep");
				list.Add("dark");
				list.Add("mighty");
				list.Add("stout");
				list.Add("proud");
				list.Add("brave");
				list.Add("bold");
				list.Add("sure");
				list.Add("strong");
				list.Add("wise");
				list.Add("riven");
			}
			else
			{
				list.Add("tooth");
				list.Add("bone");
				list.Add("heart");
				list.Add("fist");
				list.Add("hold");
				list.Add("fast");
				list.Add("guard");
				list.Add("hunter");
				list.Add("killer");
				list.Add("delver");
				list.Add("crusher");
				list.Add("mauler");
				list.Add("breaker");
				list.Add("smasher");
				list.Add("slayer");
				list.Add("striker");
				list.Add("keeper");
				list.Add("warden");
				list.Add("smith");
				list.Add("mason");
				list.Add("friend");
				list.Add("master");
			}
			int index = Session.Random.Next(list.Count);
			return list[index];
		}
	}
}
