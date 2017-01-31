using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Tools.Generators
{
	internal class NPCBuilder
	{
		private static string[] fProfession = new string[]
		{
			"apothecary",
			"architect",
			"armourer",
			"arrowsmith",
			"astrologer",
			"baker",
			"barber",
			"lawyer",
			"beggar",
			"bellfounder",
			"blacksmith",
			"bookbinder",
			"brewer",
			"bricklayer",
			"butcher",
			"carpenter",
			"carter",
			"cartwright",
			"chandler",
			"peddler",
			"clerk",
			"clockmaker",
			"cobbler",
			"cook",
			"cooper",
			"merchant",
			"embroiderer",
			"engraver",
			"fisherman",
			"fishmonger",
			"forester",
			"furrier",
			"gardener",
			"gemcutter",
			"glassblower",
			"goldsmith",
			"grocer",
			"haberdasher",
			"stableman",
			"courtier",
			"herbalist",
			"innkeeper",
			"ironmonger",
			"labourer",
			"painter",
			"locksmith",
			"mason",
			"messenger",
			"miller",
			"miner",
			"minstrel",
			"ploughman",
			"farmer",
			"porter",
			"sailor",
			"scribe",
			"seamstress",
			"shepherd",
			"shipwright",
			"soapmaker",
			"tailor",
			"tinker",
			"vintner",
			"weaver"
		};

		private static string[] fAge = new string[]
		{
			"elderly",
			"middle-aged",
			"teenage",
			"youthful",
			"young",
			"old"
		};

		private static string[] fHeight = new string[]
		{
			"gangly",
			"gigantic",
			"hulking",
			"lanky",
			"short",
			"small",
			"stumpy",
			"tall",
			"tiny",
			"willowy"
		};

		private static string[] fWeight = new string[]
		{
			"broad-shouldered",
			"fat",
			"gaunt",
			"obese",
			"plump",
			"pot-bellied",
			"rotund",
			"skinny",
			"slender",
			"slim",
			"statuesque",
			"stout",
			"thin"
		};

		private static string[] fHairColour = new string[]
		{
			"black",
			"brown",
			"dark brown",
			"light brown",
			"red",
			"ginger",
			"strawberry blonde",
			"blonde",
			"ash blonde",
			"graying",
			"silver",
			"white",
			"gray",
			"auburn"
		};

		private static string[] fHairStyle = new string[]
		{
			"short",
			"cropped",
			"long",
			"braided",
			"dreadlocked",
			"shoulder-length",
			"wiry",
			"balding",
			"receeding",
			"curly",
			"tightly-curled",
			"straight",
			"greasy",
			"limp",
			"sparse",
			"thinning",
			"wavy"
		};

		private static string[] fPhysical = new string[]
		{
			"bearded",
			"buck-toothed",
			"chiselled",
			"doe-eyed",
			"fine-featured",
			"florid",
			"gap-toothed",
			"goggle-eyed",
			"grizzled",
			"jowly",
			"jug-eared",
			"pock-marked",
			"broken nose",
			"red-cheeked",
			"scarred",
			"squinting",
			"thin-lipped",
			"toothless",
			"weather-beaten",
			"wrinkled"
		};

		private static string[] fMental = new string[]
		{
			"hot-tempered",
			"overbearing",
			"antagonistic",
			"haughty",
			"elitist",
			"proud",
			"rude",
			"aloof",
			"mischievous",
			"impulsive",
			"lusty",
			"irreverent",
			"madcap",
			"thoughtless",
			"absent-minded",
			"insensitive",
			"brave",
			"craven",
			"shy",
			"fearless",
			"obsequious",
			"inquisitive",
			"prying",
			"intellectual",
			"perceptive",
			"keen",
			"perfectionist",
			"stern",
			"harsh",
			"punctual",
			"driven",
			"trusting",
			"kind-hearted",
			"forgiving",
			"easy-going",
			"compassionate",
			"miserly",
			"hard-hearted",
			"covetous",
			"avaricious",
			"thrifty",
			"wastrel",
			"spendthrift",
			"extravagant",
			"kind",
			"charitable",
			"gloomy",
			"morose",
			"compulsive",
			"irritable",
			"vengeful",
			"honest",
			"truthful",
			"innocent",
			"gullible",
			"bigoted",
			"biased",
			"narrow-minded",
			"cheerful",
			"happy",
			"diplomatic",
			"pleasant",
			"foolhardy",
			"affable",
			"fatalistic",
			"depressing",
			"cynical",
			"sarcastic",
			"realistic",
			"secretive",
			"retiring",
			"practical",
			"level-headed",
			"dull",
			"reverent",
			"scheming",
			"paranoid",
			"cautious",
			"deceitful",
			"nervous",
			"uncultured",
			"boorish",
			"barbaric",
			"graceless",
			"crude",
			"cruel",
			"sadistic",
			"immoral",
			"jealous",
			"belligerent",
			"argumentative",
			"arrogant",
			"careless",
			"curious",
			"exacting",
			"friendly",
			"greedy",
			"generous",
			"moody",
			"naive",
			"opinionated",
			"optimistic",
			"pessimistic",
			"quiet",
			"sober",
			"suspicious",
			"uncivilised",
			"violent",
			"peaceful"
		};

		private static string[] fSpeech = new string[]
		{
			"accented",
			"articulate",
			"garrulous",
			"breathless",
			"crisp",
			"gutteral",
			"high-pitched",
			"lisping",
			"loud",
			"nasal",
			"slow",
			"fast",
			"squeaky",
			"stuttering",
			"wheezy",
			"whiny",
			"whispery",
			"soft-spoken",
			"laconic",
			"blustering"
		};

		public static string Description()
		{
			string str = NPCBuilder.fAge[Session.Random.Next(NPCBuilder.fAge.Length)];
			string text = NPCBuilder.fProfession[Session.Random.Next(NPCBuilder.fProfession.Length)];
			string text2 = "";
			switch (Session.Random.Next(3))
			{
			case 0:
			case 1:
				text2 = text;
				break;
			case 2:
				text2 = str + " " + text;
				break;
			}
			text2 = (TextHelper.StartsWithVowel(text2) ? "An" : "A") + " " + text2;
			string text3 = NPCBuilder.fHeight[Session.Random.Next(NPCBuilder.fHeight.Length)];
			string text4 = NPCBuilder.fWeight[Session.Random.Next(NPCBuilder.fWeight.Length)];
			string text5 = "";
			switch (Session.Random.Next(4))
			{
			case 0:
			case 1:
				text5 = text3 + " and " + text4;
				break;
			case 2:
				text5 = text3;
				break;
			case 3:
				text5 = text4;
				break;
			}
			string str2 = NPCBuilder.fHairColour[Session.Random.Next(NPCBuilder.fHairColour.Length)];
			string str3 = NPCBuilder.fHairStyle[Session.Random.Next(NPCBuilder.fHairStyle.Length)];
			string text6 = str3 + " " + str2;
			string result = "";
			switch (Session.Random.Next(4))
			{
			case 0:
			case 1:
				result = string.Concat(new string[]
				{
					text2,
					", ",
					text5,
					" with ",
					text6,
					" hair."
				});
				break;
			case 2:
				result = text2 + " with " + text6 + " hair.";
				break;
			case 3:
				result = text2 + ", " + text5 + ".";
				break;
			}
			return result;
		}

		public static string Physical()
		{
			return NPCBuilder.get_values(NPCBuilder.fPhysical);
		}

		public static string Personality()
		{
			return NPCBuilder.get_values(NPCBuilder.fMental);
		}

		public static string Speech()
		{
			return NPCBuilder.get_values(NPCBuilder.fSpeech);
		}

		private static string get_values(string[] array)
		{
			int number = NPCBuilder.get_number();
			BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>();
			while (binarySearchTree.Count != number)
			{
				string item = array[Session.Random.Next(array.Length)];
				binarySearchTree.Add(item);
			}
			List<string> sortedList = binarySearchTree.SortedList;
			string text = "";
			foreach (string current in sortedList)
			{
				if (text != "")
				{
					if (current == sortedList[sortedList.Count - 1])
					{
						text += " and ";
					}
					else
					{
						text += ", ";
					}
				}
				text += current;
			}
			if (text != "")
			{
				text = TextHelper.Capitalise(text, false);
			}
			return text;
		}

		private static int get_number()
		{
			switch (Session.Random.Next(10))
			{
			case 0:
				return 0;
			case 1:
			case 2:
			case 3:
				return 1;
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
				return 2;
			case 9:
				return 3;
			default:
				return 1;
			}
		}
	}
}
