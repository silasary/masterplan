using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Generators
{
	internal class Potion
	{
		public static string Description()
		{
			string text = "";
			string text2 = Potion.colour(true);
			string text3 = Potion.adjective();
			string text4 = Potion.feature();
			List<string> list = new List<string>();
			list.Add("liquid");
			list.Add("solution");
			list.Add("draught");
			list.Add("oil");
			list.Add("elixir");
			list.Add("potion");
			int index = Session.Random.Next(list.Count);
			string text5 = list[index];
			switch (Session.Random.Next(5))
			{
			case 0:
				text = text2 + " " + text5;
				break;
			case 1:
				text = string.Concat(new string[]
				{
					text2,
					" ",
					text5,
					" ",
					text4
				});
				break;
			case 2:
				text = string.Concat(new string[]
				{
					text3,
					" ",
					text2,
					" ",
					text5
				});
				break;
			case 3:
				text = string.Concat(new string[]
				{
					text3,
					" ",
					text2,
					" ",
					text5,
					" ",
					text4
				});
				break;
			case 4:
				text = string.Concat(new string[]
				{
					text3,
					" ",
					text5,
					", ",
					text2,
					" ",
					text4,
					","
				});
				break;
			}
			string text6 = TextHelper.StartsWithVowel(text) ? "An" : "A";
			text = string.Concat(new string[]
			{
				text6,
				" ",
				text,
				" in ",
				Potion.container(),
				"."
			});
			switch (Session.Random.Next(5))
			{
			case 0:
				text = text + " It smells " + Potion.smell() + ".";
				break;
			case 1:
				text = text + " It tastes " + Potion.smell() + ".";
				break;
			case 2:
			{
				string text7 = text;
				text = string.Concat(new string[]
				{
					text7,
					" It smells ",
					Potion.smell(),
					" but tastes ",
					Potion.smell(),
					"."
				});
				break;
			}
			case 3:
				text = text + " It smells and tastes " + Potion.smell() + ".";
				break;
			}
			return text;
		}

		private static string adjective()
		{
			List<string> list = new List<string>();
			list.Add("watery");
			list.Add("syrupy");
			list.Add("thick");
			list.Add("viscous");
			list.Add("gloopy");
			list.Add("thin");
			list.Add("runny");
			list.Add("translucent");
			list.Add("effervescent");
			list.Add("fizzing");
			list.Add("bubbling");
			list.Add("foaming");
			list.Add("volatile");
			list.Add("smoking");
			list.Add("fuming");
			list.Add("vaporous");
			list.Add("steaming");
			list.Add("cold");
			list.Add("icy cold");
			list.Add("hot");
			list.Add("sparkling");
			list.Add("iridescent");
			list.Add("cloudy");
			list.Add("opalescent");
			list.Add("luminous");
			list.Add("phosphorescent");
			list.Add("glowing");
			int index = Session.Random.Next(list.Count);
			return list[index];
		}

		private static string colour(bool complex)
		{
			List<string> list = new List<string>();
			list.Add("red");
			list.Add("scarlet");
			list.Add("crimson");
			list.Add("vermillion");
			if (complex)
			{
				list.Add("blood red");
				list.Add("cherry red");
				list.Add("ruby-coloured");
			}
			list.Add("pink");
			if (complex)
			{
				list.Add("rose-coloured");
			}
			list.Add("blue");
			list.Add("royal blue");
			list.Add("sky blue");
			list.Add("light blue");
			list.Add("dark blue");
			list.Add("midnight blue");
			list.Add("indigo");
			if (complex)
			{
				list.Add("sapphire-coloured");
			}
			list.Add("yellow");
			list.Add("lemon yellow");
			list.Add("amber");
			if (complex)
			{
				list.Add("straw-coloured");
			}
			list.Add("green");
			list.Add("light green");
			list.Add("dark green");
			list.Add("sea green");
			list.Add("turquoise");
			list.Add("aquamarine");
			list.Add("emerald");
			if (complex)
			{
				list.Add("olive-coloured");
			}
			list.Add("purple");
			list.Add("lavender");
			list.Add("lilac");
			list.Add("mauve");
			if (complex)
			{
				list.Add("plum-coloured");
			}
			list.Add("orange");
			list.Add("brown");
			list.Add("maroon");
			list.Add("ochre");
			if (complex)
			{
				list.Add("mud-coloured");
			}
			list.Add("black");
			list.Add("dark grey");
			list.Add("grey");
			list.Add("light grey");
			if (complex)
			{
				list.Add("cream-coloured");
				list.Add("ivory-coloured");
			}
			list.Add("off-white");
			list.Add("white");
			list.Add("golden");
			list.Add("silver");
			if (complex)
			{
				list.Add("bronze-coloured");
			}
			if (complex)
			{
				list.Add("colourless");
				list.Add("clear");
				list.Add("transparent");
			}
			int index = Session.Random.Next(list.Count);
			return list[index];
		}

		private static string feature()
		{
			switch (Session.Random.Next(5))
			{
			case 0:
				return "with " + Potion.colour(true) + " specks";
			case 1:
				return "with flecks of " + Potion.colour(false);
			case 2:
			{
				string text = Potion.colour(true);
				string text2 = TextHelper.StartsWithVowel(text) ? "an" : "a";
				return string.Concat(new string[]
				{
					"with ",
					text2,
					" ",
					text,
					" suspension"
				});
			}
			case 3:
				return "with a floating " + Potion.colour(true) + " layer";
			case 4:
				return "with a ribbon of " + Potion.colour(false);
			default:
				return "";
			}
		}

		private static string container()
		{
			List<string> list = new List<string>();
			list.Add("small");
			list.Add("rounded");
			list.Add("tall");
			list.Add("square");
			list.Add("irregularly-shaped");
			list.Add("long-necked");
			list.Add("cylindrical");
			list.Add("round-bottomed");
			List<string> list2 = new List<string>();
			list2.Add("glass");
			list2.Add("metal");
			list2.Add("ceramic");
			list2.Add("crystal");
			List<string> list3 = new List<string>();
			list3.Add("vial");
			list3.Add("jar");
			list3.Add("bottle");
			list3.Add("flask");
			int index = Session.Random.Next(list.Count);
			string text = list[index];
			int index2 = Session.Random.Next(list2.Count);
			string text2 = list2[index2];
			int index3 = Session.Random.Next(list3.Count);
			string text3 = list3[index3];
			if (Session.Random.Next(3) == 0)
			{
				text2 = Potion.colour(true) + " " + text2;
			}
			string text4 = "";
			switch (Session.Random.Next(2))
			{
			case 0:
				text4 = text2 + " " + text3;
				break;
			case 1:
				text4 = string.Concat(new string[]
				{
					text,
					" ",
					text2,
					" ",
					text3
				});
				break;
			}
			string str = TextHelper.StartsWithVowel(text4) ? "an" : "a";
			return str + " " + text4;
		}

		private static string smell()
		{
			List<string> list = new List<string>();
			list.Add("acidic");
			list.Add("acrid");
			list.Add("of ammonia");
			list.Add("of apples");
			list.Add("bitter");
			list.Add("brackish");
			list.Add("buttery");
			list.Add("of cherries");
			list.Add("delicious");
			list.Add("earthy");
			list.Add("of earwax");
			list.Add("of fish");
			list.Add("floral");
			list.Add("of lavender");
			list.Add("lemony");
			list.Add("of honey");
			list.Add("fruity");
			list.Add("meaty");
			list.Add("metallic");
			list.Add("musty");
			list.Add("of onions");
			list.Add("of oranges");
			list.Add("peppery");
			list.Add("of perfume");
			list.Add("rotten");
			list.Add("salty");
			list.Add("sickly sweet");
			list.Add("starchy");
			list.Add("sugary");
			list.Add("smokey");
			list.Add("sour");
			list.Add("spicy");
			list.Add("of sweat");
			list.Add("sweet");
			list.Add("unpleasant");
			list.Add("vile");
			list.Add("vinegary");
			int index = Session.Random.Next(list.Count);
			return list[index];
		}
	}
}
