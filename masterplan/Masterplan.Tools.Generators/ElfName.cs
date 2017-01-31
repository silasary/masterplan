using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Generators
{
	internal class ElfName
	{
		public static string MaleName()
		{
			return ElfName.name(true);
		}

		public static string FemaleName()
		{
			return ElfName.name(false);
		}

		public static string Sentence()
		{
			string text = "";
			int num = Session.Dice(4, 8);
			for (int num2 = 0; num2 != num; num2++)
			{
				string text2 = "";
				int num3 = 0;
				switch (Session.Random.Next(6))
				{
				case 0:
					num3 = 1;
					break;
				case 1:
				case 2:
					num3 = 2;
					break;
				case 3:
				case 4:
					num3 = 3;
					break;
				case 5:
					num3 = 4;
					break;
				}
				for (int num4 = 0; num4 != num3; num4++)
				{
					switch (Session.Random.Next(3))
					{
					case 0:
						text2 += ElfName.prefix();
						break;
					case 1:
						text2 += ElfName.suffix(true);
						break;
					case 2:
						text2 += ElfName.suffix(false);
						break;
					}
					if (num4 != num3 && Session.Random.Next(10) == 0)
					{
						List<string> list = new List<string>();
						list.Add("y");
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
			switch (Session.Random.Next(10))
			{
			case 0:
			case 1:
			case 2:
			case 3:
				str = ElfName.prefix() + ElfName.suffix(male);
				break;
			case 4:
			case 5:
			case 6:
				str = ElfName.prefix() + ElfName.suffix(male) + ElfName.suffix(male);
				break;
			case 7:
			case 8:
				str = string.Concat(new string[]
				{
					ElfName.prefix(),
					ElfName.suffix(male),
					" ",
					ElfName.prefix(),
					ElfName.suffix(male)
				});
				break;
			case 9:
				str = string.Concat(new string[]
				{
					ElfName.suffix(male),
					"'",
					ElfName.prefix(),
					ElfName.suffix(male),
					ElfName.suffix(male)
				});
				break;
			}
			return TextHelper.Capitalise(str, true);
		}

		private static string prefix()
		{
			List<string> list = new List<string>();
			list.Add("ael");
			list.Add("aer");
			list.Add("af");
			list.Add("ah");
			list.Add("al");
			list.Add("am");
			list.Add("ama");
			list.Add("an");
			list.Add("ang");
			list.Add("ansr");
			list.Add("ar");
			list.Add("ari");
			list.Add("arn");
			list.Add("aza");
			list.Add("bael");
			list.Add("bes");
			list.Add("cael");
			list.Add("cal");
			list.Add("cas");
			list.Add("cla");
			list.Add("cor");
			list.Add("cy");
			list.Add("dae");
			list.Add("dho");
			list.Add("dre");
			list.Add("du");
			list.Add("eli");
			list.Add("eir");
			list.Add("el");
			list.Add("er");
			list.Add("ev");
			list.Add("fera");
			list.Add("fi");
			list.Add("fir");
			list.Add("fis");
			list.Add("gael");
			list.Add("gar");
			list.Add("gil");
			list.Add("ha");
			list.Add("hu");
			list.Add("ia");
			list.Add("il");
			list.Add("ja");
			list.Add("jar");
			list.Add("ka");
			list.Add("kan");
			list.Add("ker");
			list.Add("keth");
			list.Add("koeh");
			list.Add("kor");
			list.Add("ky");
			list.Add("la");
			list.Add("laf");
			list.Add("lam");
			list.Add("lue");
			list.Add("ly");
			list.Add("mai");
			list.Add("mal");
			list.Add("mara");
			list.Add("my");
			list.Add("na");
			list.Add("nai");
			list.Add("nim");
			list.Add("nu");
			list.Add("ny");
			list.Add("py");
			list.Add("raer");
			list.Add("re");
			list.Add("ren");
			list.Add("rid");
			list.Add("ru");
			list.Add("rua");
			list.Add("rum");
			list.Add("ry");
			list.Add("sae");
			list.Add("seh");
			list.Add("sel");
			list.Add("sha");
			list.Add("she");
			list.Add("si");
			list.Add("sim");
			list.Add("sol");
			list.Add("sum");
			list.Add("syl");
			list.Add("ta");
			list.Add("tahl");
			list.Add("tha");
			list.Add("tho");
			list.Add("ther");
			list.Add("thro");
			list.Add("tia");
			list.Add("tra");
			list.Add("ty");
			list.Add("uth");
			list.Add("ver");
			list.Add("vil");
			list.Add("von");
			list.Add("ya");
			list.Add("za");
			list.Add("zy");
			int index = Session.Random.Next(list.Count);
			return list[index];
		}

		private static string suffix(bool male)
		{
			List<string> list = new List<string>();
			list.Add("ae");
			list.Add("nae");
			list.Add("ael");
			list.Add(male ? "aer" : "aera");
			list.Add(male ? "aias" : "aia");
			list.Add(male ? "ah" : "aha");
			list.Add(male ? "aith" : "aira");
			list.Add(male ? "al" : "ala");
			list.Add("ali");
			list.Add(male ? "am" : "ama");
			list.Add(male ? "an" : "ana");
			list.Add(male ? "ar" : "ara");
			list.Add("ari");
			list.Add("ri");
			list.Add("aro");
			list.Add("ro");
			list.Add("as");
			list.Add("ash");
			list.Add("sah");
			list.Add("ath");
			list.Add("avel");
			list.Add("brar");
			list.Add("abrar");
			list.Add("ibrar");
			list.Add("dar");
			list.Add("adar");
			list.Add("odar");
			list.Add("deth");
			list.Add("eath");
			list.Add("eth");
			list.Add("dre");
			list.Add("drim");
			list.Add("drimme");
			list.Add("udrim");
			list.Add("dul");
			list.Add("ean");
			list.Add("el");
			list.Add(male ? "ele" : "ela");
			list.Add("emar");
			list.Add("en");
			list.Add("er");
			list.Add("erl");
			list.Add("ern");
			list.Add("ess");
			list.Add("esti");
			list.Add("evar");
			list.Add("fel");
			list.Add("afel");
			list.Add("efel");
			list.Add("hal");
			list.Add("ahal");
			list.Add("ihal");
			list.Add("har");
			list.Add("ihar");
			list.Add("uhar");
			list.Add("hel");
			list.Add("ahel");
			list.Add("ihel");
			list.Add(male ? "ian" : "ianna");
			list.Add("ia");
			list.Add("ii");
			list.Add("ion");
			list.Add("iat");
			list.Add("ik");
			list.Add(male ? "il" : "ila");
			list.Add("iel");
			list.Add("lie");
			list.Add("im");
			list.Add("in");
			list.Add("inar");
			list.Add("ine");
			list.Add(male ? "ir" : "ira");
			list.Add("ire");
			list.Add("is");
			list.Add("iss");
			list.Add("ist");
			list.Add("ith");
			list.Add("lath");
			list.Add("lith");
			list.Add("lyth");
			list.Add("kash");
			list.Add("ashk");
			list.Add("okash");
			list.Add("ki");
			list.Add(male ? "lan" : "lanna");
			list.Add("lean");
			list.Add(male ? "olan" : "ola");
			list.Add("lam");
			list.Add("ilam");
			list.Add("ulam");
			list.Add("lar");
			list.Add("lirr");
			list.Add("las");
			list.Add(male ? "lian" : "lia");
			list.Add("lis");
			list.Add("elis");
			list.Add("lys");
			list.Add("lon");
			list.Add("ellon");
			list.Add("lyn");
			list.Add("llin");
			list.Add("lihn");
			list.Add(male ? "mah" : "ma");
			list.Add("mahs");
			list.Add("mil");
			list.Add("imil");
			list.Add("umil");
			list.Add("mus");
			list.Add("nal");
			list.Add("inal");
			list.Add("onal");
			list.Add("nes");
			list.Add("nin");
			list.Add("nine");
			list.Add("nyn");
			list.Add("nis");
			list.Add("anis");
			list.Add(male ? "on" : "onna");
			list.Add("or");
			list.Add("oro");
			list.Add("oth");
			list.Add("othi");
			list.Add("que");
			list.Add("quis");
			list.Add("rah");
			list.Add("rae");
			list.Add("raee");
			list.Add("rad");
			list.Add("rahd");
			list.Add(male ? "rail" : "ria");
			list.Add("aral");
			list.Add("ral");
			list.Add("ryl");
			list.Add("ran");
			list.Add("re");
			list.Add("reen");
			list.Add("reth");
			list.Add("rath");
			list.Add("ro");
			list.Add("ri");
			list.Add("ron");
			list.Add("ruil");
			list.Add("aruil");
			list.Add("eruil");
			list.Add("sal");
			list.Add("isal");
			list.Add("sali");
			list.Add("san");
			list.Add("sar");
			list.Add("asar");
			list.Add("isar");
			list.Add("sel");
			list.Add("asel");
			list.Add("isel");
			list.Add("sha");
			list.Add("she");
			list.Add("shor");
			list.Add("spar");
			list.Add("tae");
			list.Add("itae");
			list.Add("tas");
			list.Add("itas");
			list.Add("ten");
			list.Add("iten");
			list.Add(male ? "thal" : "tha");
			list.Add(male ? "ethel" : "etha");
			list.Add("thar");
			list.Add("ethar");
			list.Add("ithar");
			list.Add("ther");
			list.Add("ather");
			list.Add("thir");
			list.Add("thi");
			list.Add("ethil");
			list.Add("thil");
			list.Add(male ? "thus" : "thas");
			list.Add(male ? "aethus" : "aethas");
			list.Add("ti");
			list.Add("eti");
			list.Add("til");
			list.Add(male ? "tril" : "tria");
			list.Add("atri");
			list.Add(male ? "atril" : "atria");
			list.Add("ual");
			list.Add("lua");
			list.Add("uath");
			list.Add("uth");
			list.Add("luth");
			list.Add(male ? "us" : "ua");
			list.Add(male ? "van" : "vanna");
			list.Add(male ? "var" : "vara");
			list.Add(male ? "avar" : "avara");
			list.Add("vain");
			list.Add("avain");
			list.Add("via");
			list.Add("avia");
			list.Add("vin");
			list.Add("avin");
			list.Add("wyn");
			list.Add("ya");
			list.Add(male ? "yr" : "yn");
			list.Add("yth");
			list.Add(male ? "zair" : "zara");
			list.Add(male ? "azair" : "ezara");
			int index = Session.Random.Next(list.Count);
			return list[index];
		}
	}
}
