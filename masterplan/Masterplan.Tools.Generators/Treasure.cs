using Masterplan.Data;
using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Generators
{
	internal class Treasure
	{
		private static List<Guid> fPlaceholderIDs = null;

		private static List<int> fValues = new List<int>(new int[]
		{
			125000,
			100000,
			75000,
			50000,
			25000,
			20000,
			10000,
			7500,
			5000,
			2500,
			2000,
			1000,
			7500,
			5000,
			2500,
			2000,
			1000,
			750,
			500,
			250,
			200,
			100,
			50
		});

		private static List<string> fObjects = new List<string>(new string[]
		{
			"medal",
			"statuette",
			"sculpture",
			"idol",
			"chalice",
			"goblet",
			"dish",
			"bowl"
		});

		private static List<string> fJewellery = new List<string>(new string[]
		{
			"ring",
			"necklace",
			"crown",
			"circlet",
			"bracelet",
			"anklet",
			"torc",
			"brooch",
			"pendant",
			"locket",
			"diadem",
			"tiara",
			"earring"
		});

		private static List<string> fInstruments = new List<string>(new string[]
		{
			"lute",
			"lyre",
			"mandolin",
			"violin",
			"drum",
			"flute",
			"clarinet",
			"accordion",
			"banjo",
			"bodhran",
			"ocarina",
			"zither",
			"djembe"
		});

		private static List<string> fStones = new List<string>(new string[]
		{
			"diamond",
			"ruby",
			"sapphire",
			"emerald",
			"amethyst",
			"garnet",
			"topaz",
			"pearl",
			"black pearl",
			"opal",
			"fire opal",
			"amber",
			"coral",
			"agate",
			"carnelian",
			"jade",
			"peridot",
			"moonstone",
			"alexandrite",
			"aquamarine",
			"jacinth",
			"marble"
		});

		private static List<string> fMetals = new List<string>(new string[]
		{
			"gold",
			"silver",
			"bronze",
			"platinum",
			"electrum",
			"mithral",
			"orium",
			"adamantine"
		});

		public static List<Guid> PlaceholderIDs
		{
			get
			{
				if (Treasure.fPlaceholderIDs == null)
				{
					Treasure.fPlaceholderIDs = new List<Guid>();
					for (int i = 1; i <= 30; i++)
					{
						Guid item = new Guid(i, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
						Treasure.fPlaceholderIDs.Add(item);
					}
				}
				return Treasure.fPlaceholderIDs;
			}
		}

		private static MagicItem get_placeholder_item(int level)
		{
			int num = Math.Min(level, 30);
			return new MagicItem
			{
				Name = "Magic Item (level " + num + ")",
				Level = num,
				ID = Treasure.PlaceholderIDs[num - 1]
			};
		}

		public static int GetItemValue(int level)
		{
			switch (level)
			{
			case 1:
				return 360;
			case 2:
				return 520;
			case 3:
				return 680;
			case 4:
				return 840;
			case 5:
				return 1000;
			case 6:
				return 1800;
			case 7:
				return 2600;
			case 8:
				return 3400;
			case 9:
				return 4200;
			case 10:
				return 5000;
			case 11:
				return 9000;
			case 12:
				return 13000;
			case 13:
				return 17000;
			case 14:
				return 21000;
			case 15:
				return 25000;
			case 16:
				return 45000;
			case 17:
				return 65000;
			case 18:
				return 85000;
			case 19:
				return 105000;
			case 20:
				return 125000;
			case 21:
				return 225000;
			case 22:
				return 325000;
			case 23:
				return 425000;
			case 24:
				return 525000;
			case 25:
				return 625000;
			case 26:
				return 1125000;
			case 27:
				return 1625000;
			case 28:
				return 2125000;
			case 29:
				return 2625000;
			case 30:
				return 3125000;
			default:
				return 0;
			}
		}

		public static List<Parcel> CreateParcelSet(int level, int size, bool placeholder_items)
		{
			List<Parcel> list = new List<Parcel>();
			switch (size)
			{
			case 1:
				list.Add(Treasure.get_magic_item(level + 2, placeholder_items));
				break;
			case 2:
				list.Add(Treasure.get_magic_item(level + 2, placeholder_items));
				break;
			case 3:
				list.Add(Treasure.get_magic_item(level + 4, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 2, placeholder_items));
				break;
			case 4:
				list.Add(Treasure.get_magic_item(level + 4, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 3, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 1, placeholder_items));
				break;
			case 5:
				list.Add(Treasure.get_magic_item(level + 4, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 3, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 2, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 1, placeholder_items));
				break;
			case 6:
				list.Add(Treasure.get_magic_item(level + 4, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 3, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 2, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 2, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 1, placeholder_items));
				break;
			case 7:
				list.Add(Treasure.get_magic_item(level + 4, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 3, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 2, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 2, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 1, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 1, placeholder_items));
				break;
			case 8:
				list.Add(Treasure.get_magic_item(level + 4, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 3, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 3, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 2, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 2, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 1, placeholder_items));
				list.Add(Treasure.get_magic_item(level + 1, placeholder_items));
				break;
			}
			List<int> list2 = Treasure.get_gp_values(level);
			if (size == 1)
			{
				list2.RemoveAt(0);
			}
			foreach (int current in list2)
			{
				Parcel item = Treasure.CreateParcel(current, placeholder_items);
				list.Add(item);
			}
			return list;
		}

		public static Parcel CreateParcel(int level, int size, bool placeholder)
		{
			List<Parcel> list = Treasure.CreateParcelSet(level, size, placeholder);
			int index = Session.Random.Next() % list.Count;
			return list[index];
		}

		public static Parcel CreateParcel(int value, bool placeholder)
		{
			Parcel parcel = new Parcel();
			parcel.Name = "Items worth " + value + " GP";
			parcel.Value = value;
			if (!placeholder)
			{
				parcel.Details = Treasure.RandomMundaneItem(value);
			}
			return parcel;
		}

		public static MagicItem RandomMagicItem(int level)
		{
			int num = Math.Min(30, level);
			List<MagicItem> list = new List<MagicItem>();
			foreach (MagicItem current in Session.MagicItems)
			{
				if (current.Level == num)
				{
					list.Add(current);
				}
			}
			if (list.Count != 0)
			{
				int index = Session.Random.Next() % list.Count;
				return list[index];
			}
			return null;
		}

		public static Artifact RandomArtifact(Tier tier)
		{
			List<Artifact> list = new List<Artifact>();
			foreach (Artifact current in Session.Artifacts)
			{
				if (current.Tier == tier)
				{
					list.Add(current);
				}
			}
			if (list.Count != 0)
			{
				int index = Session.Random.Next() % list.Count;
				return list[index];
			}
			return null;
		}

		public static string RandomMundaneItem(int value)
		{
			List<string> list = Treasure.create_from_gp(value);
			string text = "";
			foreach (string current in list)
			{
				if (text != "")
				{
					text += "; ";
				}
				text += current;
			}
			return text;
		}

		public static string ArtObject()
		{
			string text = Treasure.random_item_type(false, false);
			return (TextHelper.StartsWithVowel(text) ? "An" : "A") + " " + text;
		}

		private static Parcel get_magic_item(int level, bool placeholder)
		{
			int num = Math.Min(30, level);
			if (placeholder)
			{
				return new Parcel(Treasure.get_placeholder_item(level));
			}
			MagicItem magicItem = Treasure.RandomMagicItem(num);
			if (magicItem != null)
			{
				return new Parcel(magicItem);
			}
			return new Parcel
			{
				Details = "Random magic item (level " + num + ")"
			};
		}

		private static List<int> get_gp_values(int level)
		{
			switch (level)
			{
			case 1:
				return new List<int>(new int[]
				{
					200,
					180,
					120,
					120,
					60,
					40
				});
			case 2:
				return new List<int>(new int[]
				{
					290,
					260,
					170,
					170,
					90,
					60
				});
			case 3:
				return new List<int>(new int[]
				{
					380,
					340,
					225,
					225,
					110,
					75
				});
			case 4:
				return new List<int>(new int[]
				{
					470,
					420,
					280,
					280,
					140,
					90
				});
			case 5:
				return new List<int>(new int[]
				{
					550,
					500,
					340,
					340,
					160,
					110
				});
			case 6:
				return new List<int>(new int[]
				{
					1000,
					900,
					600,
					600,
					300,
					200
				});
			case 7:
				return new List<int>(new int[]
				{
					1500,
					1300,
					850,
					850,
					400,
					300
				});
			case 8:
				return new List<int>(new int[]
				{
					1900,
					1700,
					1100,
					1100,
					600,
					400
				});
			case 9:
				return new List<int>(new int[]
				{
					2400,
					2100,
					1400,
					1400,
					700,
					400
				});
			case 10:
				return new List<int>(new int[]
				{
					2800,
					2500,
					1700,
					1700,
					800,
					500
				});
			case 11:
				return new List<int>(new int[]
				{
					5000,
					4000,
					3000,
					3000,
					2000,
					1000
				});
			case 12:
				return new List<int>(new int[]
				{
					7200,
					7000,
					4400,
					4400,
					2000,
					1000
				});
			case 13:
				return new List<int>(new int[]
				{
					9500,
					8500,
					5700,
					5700,
					2800,
					1800
				});
			case 14:
				return new List<int>(new int[]
				{
					12000,
					10000,
					7000,
					7000,
					4000,
					2000
				});
			case 15:
				return new List<int>(new int[]
				{
					14000,
					12000,
					8500,
					8500,
					5000,
					2000
				});
			case 16:
				return new List<int>(new int[]
				{
					25000,
					22000,
					15000,
					15000,
					8000,
					5000
				});
			case 17:
				return new List<int>(new int[]
				{
					36000,
					33000,
					22000,
					22000,
					11000,
					6000
				});
			case 18:
				return new List<int>(new int[]
				{
					48000,
					42000,
					29000,
					29000,
					15000,
					7000
				});
			case 19:
				return new List<int>(new int[]
				{
					60000,
					52000,
					35000,
					35000,
					18000,
					10000
				});
			case 20:
				return new List<int>(new int[]
				{
					70000,
					61000,
					42000,
					42000,
					21000,
					14000
				});
			case 21:
				return new List<int>(new int[]
				{
					125000,
					112000,
					75000,
					75000,
					38000,
					25000
				});
			case 22:
				return new List<int>(new int[]
				{
					180000,
					160000,
					110000,
					110000,
					55000,
					35000
				});
			case 23:
				return new List<int>(new int[]
				{
					240000,
					210000,
					140000,
					140000,
					70000,
					50000
				});
			case 24:
				return new List<int>(new int[]
				{
					300000,
					250000,
					175000,
					175000,
					90000,
					60000
				});
			case 25:
				return new List<int>(new int[]
				{
					350000,
					320000,
					200000,
					200000,
					100000,
					80000
				});
			case 26:
				return new List<int>(new int[]
				{
					625000,
					560000,
					375000,
					375000,
					190000,
					125000
				});
			case 27:
				return new List<int>(new int[]
				{
					900000,
					800000,
					550000,
					550000,
					280000,
					170000
				});
			case 28:
				return new List<int>(new int[]
				{
					1200000,
					1000000,
					720000,
					720000,
					360000,
					250000
				});
			case 29:
				return new List<int>(new int[]
				{
					1500000,
					1300000,
					875000,
					875000,
					450000,
					250000
				});
			case 30:
				return new List<int>(new int[]
				{
					1750000,
					1500000,
					1000000,
					1000000,
					600000,
					400000
				});
			default:
				return null;
			}
		}

		private static List<string> create_from_gp(int gp)
		{
			List<string> list = new List<string>();
			if (Session.Random.Next() % 4 == 0)
			{
				list.Add(Treasure.coins(gp));
			}
			else
			{
				int num;
				int num2;
				int num3;
				for (num = gp; num != 0; num -= num2 * num3)
				{
					num2 = Treasure.get_value(num);
					if (num2 == 0)
					{
						break;
					}
					num3 = num / num2;
					string text = Treasure.random_item_type(num3 != 1, true);
					if (num3 == 1)
					{
						string text2 = TextHelper.StartsWithVowel(text) ? "an" : "a";
						list.Add(string.Concat(new object[]
						{
							text2,
							" ",
							text,
							" (worth ",
							num2,
							" GP)"
						}));
					}
					else
					{
						list.Add(string.Concat(new object[]
						{
							num3,
							" ",
							text,
							" (worth ",
							num2,
							" GP each)"
						}));
					}
				}
				if (num != 0)
				{
					list.Add(Treasure.coins(num));
				}
			}
			for (int num4 = 0; num4 != list.Count; num4++)
			{
				list[num4] = TextHelper.Capitalise(list[num4], false);
			}
			return list;
		}

		private static int get_value(int total)
		{
			List<int> list = new List<int>();
			foreach (int current in Treasure.fValues)
			{
				int num = total / current;
				if (num >= 1 && num <= 10)
				{
					list.Add(current);
				}
			}
			if (list.Count == 0)
			{
				return 0;
			}
			int index = Session.Random.Next() % list.Count;
			return list[index];
		}

		private static string random_item_type(bool plural, bool allow_potion)
		{
			string text = "";
			if (allow_potion && Session.Random.Next() % 4 == 0)
			{
				text = "potion";
				if (plural)
				{
					text += "s";
				}
				return text;
			}
			switch (Session.Random.Next() % 12)
			{
			case 0:
			case 1:
			case 2:
			{
				int index = Session.Random.Next() % Treasure.fStones.Count;
				string text2 = Treasure.fStones[index];
				switch (Session.Random.Next() % 2)
				{
				case 0:
					text2 += " gemstone";
					break;
				case 1:
					text2 = "piece of " + text2;
					break;
				}
				switch (Session.Random.Next() % 12)
				{
				case 0:
					text2 = "well cut " + text2;
					break;
				case 1:
					text2 = "rough-cut " + text2;
					break;
				case 2:
					text2 = "poorly cut " + text2;
					break;
				case 3:
					text2 = "small " + text2;
					break;
				case 4:
					text2 = "large " + text2;
					break;
				case 5:
					text2 = "oddly shaped " + text2;
					break;
				case 6:
					text2 = "highly polished " + text2;
					break;
				}
				text = text2;
				break;
			}
			case 3:
			case 4:
			case 5:
			{
				int index2 = Session.Random.Next() % Treasure.fObjects.Count;
				string str = Treasure.fObjects[index2];
				List<string> list = new List<string>();
				list.Add("small");
				list.Add("large");
				list.Add("light");
				list.Add("heavy");
				list.Add("delicate");
				list.Add("fragile");
				list.Add("masterwork");
				list.Add("elegant");
				int index3 = Session.Random.Next() % list.Count;
				string str2 = list[index3];
				text = str2 + " " + str;
				break;
			}
			case 6:
			case 7:
			case 8:
			{
				int index4 = Session.Random.Next() % Treasure.fJewellery.Count;
				string str3 = Treasure.fJewellery[index4];
				int index5 = Session.Random.Next() % Treasure.fMetals.Count;
				string text3 = Treasure.fMetals[index5];
				text = text3 + " " + str3;
				switch (Session.Random.Next(5))
				{
				case 0:
				{
					string str4 = (Session.Random.Next(2) == 0) ? "enamelled" : "laquered";
					text = str4 + " " + text;
					break;
				}
				case 1:
				{
					index5 = Session.Random.Next() % Treasure.fMetals.Count;
					text3 = Treasure.fMetals[index5];
					string text4 = (Session.Random.Next(2) == 0) ? "plated" : "filigreed";
					text = string.Concat(new string[]
					{
						text3,
						"-",
						text4,
						" ",
						text
					});
					break;
				}
				}
				switch (Session.Random.Next() % 10)
				{
				case 0:
					text = "delicate " + text;
					break;
				case 1:
					text = "intricate " + text;
					break;
				case 2:
					text = "elegant " + text;
					break;
				case 3:
					text = "simple " + text;
					break;
				case 4:
					text = "plain " + text;
					break;
				}
				break;
			}
			case 9:
			case 10:
			{
				string text5 = "";
				switch (Session.Random.Next(2))
				{
				case 0:
					text5 = "painting";
					switch (Session.Random.Next(2))
					{
					case 0:
						text5 = "oil " + text5;
						break;
					case 1:
						text5 = "watercolour " + text5;
						break;
					}
					break;
				case 1:
					text5 = "drawing";
					switch (Session.Random.Next(2))
					{
					case 0:
						text5 = "pencil " + text5;
						break;
					case 1:
						text5 = "charcoal " + text5;
						break;
					}
					break;
				}
				List<string> list2 = new List<string>();
				list2.Add("small");
				list2.Add("large");
				list2.Add("delicate");
				list2.Add("fragile");
				list2.Add("elegant");
				list2.Add("detailed");
				List<string> list3 = new List<string>();
				list3.Add("canvas");
				list3.Add("paper");
				list3.Add("parchment");
				list3.Add("wood panels");
				list3.Add("fabric");
				int index6 = Session.Random.Next() % list2.Count;
				string text6 = list2[index6];
				int index7 = Session.Random.Next() % list3.Count;
				string text7 = list3[index7];
				text = string.Concat(new string[]
				{
					text6,
					" ",
					text5,
					" on ",
					text7
				});
				break;
			}
			case 11:
			{
				int index8 = Session.Random.Next() % Treasure.fInstruments.Count;
				string str5 = Treasure.fInstruments[index8];
				List<string> list4 = new List<string>();
				list4.Add("small");
				list4.Add("large");
				list4.Add("light");
				list4.Add("heavy");
				list4.Add("delicate");
				list4.Add("fragile");
				list4.Add("masterwork");
				list4.Add("elegant");
				int index9 = Session.Random.Next() % list4.Count;
				string str6 = list4[index9];
				text = str6 + " " + str5;
				break;
			}
			}
			if (plural)
			{
				text += "s";
			}
			switch (Session.Random.Next() % 5)
			{
			case 0:
			{
				List<string> list5 = new List<string>();
				list5.Add("feywild");
				list5.Add("shadowfell");
				list5.Add("elemental chaos");
				list5.Add("astral plane");
				list5.Add("abyss");
				list5.Add("distant north");
				list5.Add("distant east");
				list5.Add("distant west");
				list5.Add("distant south");
				int index10 = Session.Random.Next() % list5.Count;
				string str7 = list5[index10];
				text = text + " from the " + str7;
				break;
			}
			case 1:
			{
				List<string> list6 = new List<string>();
				list6.Add("decorated with");
				list6.Add("inscribed with");
				list6.Add("engraved with");
				list6.Add("embossed with");
				list6.Add("carved with");
				List<string> list7 = new List<string>();
				list7.Add("indecipherable");
				list7.Add("ancient");
				list7.Add("curious");
				list7.Add("unusual");
				list7.Add("dwarven");
				list7.Add("eladrin");
				list7.Add("elven");
				list7.Add("draconic");
				list7.Add("gith");
				List<string> list8 = new List<string>();
				list8.Add("script");
				list8.Add("designs");
				list8.Add("sigils");
				list8.Add("runes");
				list8.Add("glyphs");
				list8.Add("patterns");
				int index11 = Session.Random.Next() % list6.Count;
				string text8 = list6[index11];
				int index12 = Session.Random.Next() % list7.Count;
				string text9 = list7[index12];
				int index13 = Session.Random.Next() % list8.Count;
				string text10 = list8[index13];
				string text11 = text;
				text = string.Concat(new string[]
				{
					text11,
					" ",
					text8,
					" ",
					text9,
					" ",
					text10
				});
				break;
			}
			case 2:
			{
				List<string> list9 = new List<string>();
				list9.Add("glowing with");
				list9.Add("suffused with");
				list9.Add("infused with");
				list9.Add("humming with");
				list9.Add("pulsing with");
				List<string> list10 = new List<string>();
				list10.Add("arcane");
				list10.Add("divine");
				list10.Add("primal");
				list10.Add("psionic");
				list10.Add("shadow");
				list10.Add("elemental");
				list10.Add("unknown");
				List<string> list11 = new List<string>();
				list11.Add("energy");
				list11.Add("power");
				list11.Add("magic");
				int index14 = Session.Random.Next() % list9.Count;
				string text12 = list9[index14];
				int index15 = Session.Random.Next() % list10.Count;
				string text13 = list10[index15];
				int index16 = Session.Random.Next() % list11.Count;
				string text14 = list11[index16];
				string text11 = text;
				text = string.Concat(new string[]
				{
					text11,
					" ",
					text12,
					" ",
					text13,
					" ",
					text14
				});
				break;
			}
			case 4:
			{
				List<string> list12 = new List<string>();
				list12.Add("set with");
				list12.Add("inlaid with");
				list12.Add("studded with");
				list12.Add("with shards of");
				int index17 = Session.Random.Next() % Treasure.fStones.Count;
				string text15 = Treasure.fStones[index17];
				if (Session.Random.Next() % 2 == 0)
				{
					text15 += "s";
				}
				else
				{
					text15 = "a single " + text15;
				}
				int index18 = Session.Random.Next() % list12.Count;
				string text16 = list12[index18];
				string text11 = text;
				text = string.Concat(new string[]
				{
					text11,
					" ",
					text16,
					" ",
					text15
				});
				break;
			}
			}
			return text;
		}

		private static string coins(int gp)
		{
			int num = gp / 10000;
			int num2 = gp % 10000;
			if (num > 0 && num2 == 0)
			{
				string text = "astral diamond";
				if (num > 1)
				{
					text += "s";
				}
				return num + " " + text;
			}
			int num3 = gp / 100;
			int num4 = gp % 100;
			if (num3 >= 100 && num4 == 0)
			{
				return num3 + " PP";
			}
			int num5 = gp * 10;
			if (num5 <= 100)
			{
				return num5 + " SP";
			}
			return gp + " GP";
		}
	}
}
