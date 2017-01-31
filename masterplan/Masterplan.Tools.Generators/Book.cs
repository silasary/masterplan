using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Generators
{
	internal class Book
	{
		private static string[] fPrepositions = new string[]
		{
			"and",
			"in",
			"of",
			"with",
			"without",
			"against",
			"for",
			"to"
		};

		private static string[] fAbout = new string[]
		{
			"about",
			"on",
			"concerning",
			"regarding"
		};

		public static string Title()
		{
			string text = "";
			switch (Session.Random.Next(5))
			{
			case 0:
			{
				bool flag = Session.Random.Next(2) == 0;
				bool concrete_noun = Session.Random.Next(2) == 0;
				text = Book.noun_phrase(flag, flag) + "'s " + Book.noun_phrase(concrete_noun, false);
				break;
			}
			case 1:
			{
				bool flag2 = Session.Random.Next(2) == 0;
				bool flag3 = Session.Random.Next(2) == 0;
				text = string.Concat(new string[]
				{
					Book.noun_phrase(flag2, flag2),
					" ",
					Book.preposition(),
					" ",
					Book.noun_phrase(flag3, flag3)
				});
				break;
			}
			case 2:
			{
				bool concrete_noun2 = Session.Random.Next(2) == 0;
				text = Book.gerund() + " the " + Book.noun_phrase(concrete_noun2, false);
				break;
			}
			case 3:
				if (Session.Random.Next(2) == 0)
				{
					text = Book.about() + " " + Book.noun(true) + "s";
				}
				else
				{
					text = Book.about() + " " + Book.noun(false);
				}
				break;
			case 4:
			{
				bool concrete_noun3 = Session.Random.Next(2) == 0;
				text = Book.noun_phrase(concrete_noun3, true);
				break;
			}
			}
			if (Session.Random.Next(10) == 0)
			{
				string str = "";
				switch (Session.Random.Next(2))
				{
				case 0:
					str = "volume";
					break;
				case 1:
					str = "part";
					break;
				}
				switch (Session.Random.Next(5))
				{
				case 0:
					text = text + ", " + str + " one";
					break;
				case 1:
					text = text + ", " + str + " two";
					break;
				case 2:
					text = text + ", " + str + " three";
					break;
				case 3:
					text = text + ", " + str + " four";
					break;
				case 4:
					text = text + ", " + str + " five";
					break;
				}
			}
			return TextHelper.Capitalise(text, true);
		}

		private static string noun_phrase(bool concrete_noun, bool article)
		{
			string text = Book.noun(concrete_noun);
			bool flag = false;
			if (concrete_noun && Session.Random.Next(5) == 0)
			{
				text += "s";
				flag = true;
			}
			if (Session.Random.Next(3) == 0)
			{
				string str = Book.adjective();
				text = str + " " + text;
			}
			if (article && Session.Random.Next(2) == 0)
			{
				switch (Session.Random.Next(2))
				{
				case 0:
					text = "the " + text;
					break;
				case 1:
					if (!flag)
					{
						switch (Session.Random.Next(2))
						{
						case 0:
							text = (TextHelper.StartsWithVowel(text) ? "an" : "a") + " " + text;
							break;
						case 1:
							text = "one " + text;
							break;
						}
					}
					else
					{
						switch (Session.Random.Next(6))
						{
						case 0:
							text = "two " + text;
							break;
						case 1:
							text = "three " + text;
							break;
						case 2:
							text = "four " + text;
							break;
						case 3:
							text = "five " + text;
							break;
						case 4:
							text = "six " + text;
							break;
						case 5:
							text = "seven " + text;
							break;
						}
					}
					break;
				}
			}
			return text;
		}

		private static string noun(bool concrete)
		{
			List<string> list = new List<string>();
			if (concrete)
			{
				list.Add("elf");
				list.Add("eladrin");
				list.Add("halfling");
				list.Add("dwarf");
				list.Add("gnome");
				list.Add("deva");
				list.Add("tiefling");
				list.Add("dragonborn");
				list.Add("goliath");
				list.Add("changeling");
				list.Add("drow");
				list.Add("minotaur");
				list.Add("beast");
				list.Add("orc");
				list.Add("goblin");
				list.Add("hobgoblin");
				list.Add("dragon");
				list.Add("demon");
				list.Add("devil");
				list.Add("angel");
				list.Add("god");
				list.Add("gith");
				list.Add("night");
				list.Add("day");
				list.Add("eclipse");
				list.Add("shadow");
				list.Add("sun");
				list.Add("moon");
				list.Add("star");
				list.Add("battle");
				list.Add("war");
				list.Add("brawl");
				list.Add("fist");
				list.Add("blade");
				list.Add("arrow");
				list.Add("spell");
				list.Add("prayer");
				list.Add("eye");
				list.Add("wing");
				list.Add("army");
				list.Add("legion");
				list.Add("brigade");
				list.Add("galleon");
				list.Add("warship");
				list.Add("frigate");
				list.Add("potion");
				list.Add("jewel");
				list.Add("ring");
				list.Add("amulet");
				list.Add("cloak");
				list.Add("sword");
				list.Add("spear");
				list.Add("helm");
				list.Add("flame");
				list.Add("wizard");
				list.Add("king");
				list.Add("queen");
				list.Add("prince");
				list.Add("princess");
				list.Add("warlock");
				list.Add("barbarian");
				list.Add("sorcerer");
				list.Add("thief");
				list.Add("mage");
				list.Add("child");
				list.Add("wayfarer");
				list.Add("adventurer");
				list.Add("pirate");
				list.Add("spy");
				list.Add("sage");
				list.Add("assassin");
				list.Add("mountain");
				list.Add("forest");
				list.Add("peak");
				list.Add("cave");
				list.Add("cavern");
				list.Add("lake");
				list.Add("swamp");
				list.Add("marshland");
				list.Add("island");
				list.Add("shore");
				list.Add("city");
				list.Add("town");
				list.Add("village");
				list.Add("tower");
				list.Add("arena");
				list.Add("castle");
				list.Add("citadel");
				list.Add("game");
				list.Add("wager");
				list.Add("quest");
				list.Add("challenge");
				list.Add("rose");
				list.Add("lily");
				list.Add("thorn");
				list.Add("word");
				list.Add("snake");
				list.Add("song");
				list.Add("lament");
				list.Add("dirge");
				list.Add("elegy");
			}
			else
			{
				list.Add("darkness");
				list.Add("light");
				list.Add("dusk");
				list.Add("twilight");
				list.Add("revenge");
				list.Add("vengeance");
				list.Add("blood");
				list.Add("earth");
				list.Add("water");
				list.Add("ice");
				list.Add("wood");
				list.Add("metal");
				list.Add("lightning");
				list.Add("thunder");
				list.Add("mist");
				list.Add("destruction");
				list.Add("life");
				list.Add("death");
				list.Add("time");
				list.Add("end");
				list.Add("danger");
				list.Add("luck");
				list.Add("chaos");
				list.Add("truth");
				list.Add("music");
				list.Add("one");
				list.Add("two");
				list.Add("three");
				list.Add("four");
				list.Add("five");
				list.Add("six");
				list.Add("seven");
				list.Add("eight");
				list.Add("nine");
				list.Add("ten");
				list.Add("eleven");
				list.Add("twelve");
			}
			list.Add("wind");
			list.Add("stone");
			list.Add("fire");
			list.Add("storm");
			int index = Session.Random.Next(list.Count);
			return list[index];
		}

		private static string adjective()
		{
			List<string> list = new List<string>();
			list.Add("dark");
			list.Add("bright");
			list.Add("tyrannous");
			list.Add("devout");
			list.Add("noble");
			list.Add("eldritch");
			list.Add("mystical");
			list.Add("magical");
			list.Add("sorcerous");
			list.Add("savage");
			list.Add("silent");
			list.Add("lonely");
			list.Add("violent");
			list.Add("peaceful");
			list.Add("black");
			list.Add("white");
			list.Add("gold");
			list.Add("silver");
			list.Add("red");
			list.Add("pale");
			list.Add("dying");
			list.Add("living");
			list.Add("ascending");
			list.Add("defiled");
			list.Add("mythical");
			list.Add("legendary");
			list.Add("heroic");
			list.Add("empty");
			list.Add("mighty");
			list.Add("despairing");
			list.Add("spellbound");
			list.Add("enchanted");
			list.Add("soaring");
			list.Add("falling");
			list.Add("visionary");
			list.Add("bold");
			list.Add("perilous");
			int index = Session.Random.Next(list.Count);
			return list[index];
		}

		private static string gerund()
		{
			List<string> list = new List<string>();
			list.Add("killing");
			list.Add("murdering");
			list.Add("watching");
			list.Add("examining");
			list.Add("enchanting");
			list.Add("destroying");
			list.Add("defying");
			list.Add("betraying");
			list.Add("protecting");
			list.Add("silencing");
			list.Add("bearing");
			list.Add("fighting");
			int index = Session.Random.Next(list.Count);
			return list[index];
		}

		private static string preposition()
		{
			int num = Session.Random.Next(Book.fPrepositions.Length);
			return Book.fPrepositions[num];
		}

		private static string about()
		{
			int num = Session.Random.Next(Book.fAbout.Length);
			return Book.fAbout[num];
		}
	}
}
