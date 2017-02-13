using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Generators
{
	internal class RoomBuilder
	{
		public static string Name()
		{
			List<string> list = new List<string>();
			list.Add("Antechamber");
			list.Add("Arena");
			list.Add("Armoury");
			list.Add("Aviary");
			list.Add("Audience Chamber");
			list.Add("Banquet Hall");
			list.Add("Bath Chamber");
			list.Add("Barracks");
			list.Add("Bedroom");
			list.Add("Boudoir");
			list.Add("Bestiary");
			list.Add("Burial Chamber");
			list.Add("Cells");
			list.Add("Chamber");
			list.Add("Chantry");
			list.Add("Chapel");
			list.Add("Classroom");
			list.Add("Closet");
			list.Add("Court");
			list.Add("Crypt");
			list.Add("Dining Room");
			list.Add("Dormitory");
			list.Add("Dressing Room");
			list.Add("Dumping Ground");
			list.Add("Entrance Hall");
			list.Add("Gallery");
			list.Add("Game Room");
			list.Add("Great Hall");
			list.Add("Guard Post");
			list.Add("Hall");
			list.Add("Harem");
			list.Add("Hoard");
			list.Add("Infirmary");
			list.Add("Kennels");
			list.Add("Kitchens");
			list.Add("Laboratory");
			list.Add("Lair");
			list.Add("Library");
			list.Add("Mausoleum");
			list.Add("Meditation Room");
			list.Add("Museum");
			list.Add("Nursery");
			list.Add("Observatory");
			list.Add("Office");
			list.Add("Pantry");
			list.Add("Prison");
			list.Add("Quarters");
			list.Add("Reception Room");
			list.Add("Refectory");
			list.Add("Ritual Chamber");
			list.Add("Shrine");
			list.Add("Smithy");
			list.Add("Stable");
			list.Add("Storeroom");
			list.Add("Study");
			list.Add("Temple");
			list.Add("Throne Room");
			list.Add("Torture Chamber");
			list.Add("Trophy Room");
			list.Add("Training Area");
			list.Add("Treasury");
			list.Add("Waiting Room");
			list.Add("Workroom");
			list.Add("Workshop");
			list.Add("Vault");
			list.Add("Vestibule");
			int index = Session.Random.Next() % list.Count;
			return list[index];
		}

		public static string Details()
		{
			List<string> list = new List<string>();
			while (list.Count == 0)
			{
				if (Session.Random.Next(2) == 0)
				{
					list.Add(RoomBuilder.random_wall());
				}
				if (Session.Random.Next(3) == 0)
				{
					list.Add(RoomBuilder.random_finish());
				}
				if (Session.Random.Next(4) == 0)
				{
					list.Add(RoomBuilder.random_air());
				}
				if (Session.Random.Next(5) == 0)
				{
					list.Add(RoomBuilder.random_smell());
				}
				if (Session.Random.Next(5) == 0)
				{
					list.Add(RoomBuilder.random_sound());
				}
				if (Session.Random.Next(10) == 0)
				{
					list.Add(RoomBuilder.random_activity());
				}
			}
			string text = "";
			foreach (string current in list)
			{
				if (text != "")
				{
					text += " ";
				}
				text += current;
			}
			return text;
		}

		private static string random_wall()
		{
			List<string> list = new List<string>();
			list.Add("The walls of this area are " + RoomBuilder.random_material() + ".");
			list.Add(string.Concat(new string[]
			{
				"The walls of this area are ",
				RoomBuilder.random_material(),
				" and ",
				RoomBuilder.random_material(),
				"."
			}));
			list.Add("The floor of this area is made from " + RoomBuilder.random_material() + ".");
			list.Add(string.Concat(new string[]
			{
				"The walls of this area are made of ",
				RoomBuilder.random_material(),
				", while the ceiling and floor are ",
				RoomBuilder.random_material(),
				"."
			}));
			list.Add("This area has been hewn out of solid rock.");
			list.Add("This area has been panelled in dark wood.");
			int index = Session.Random.Next() % list.Count;
			return list[index];
		}

		private static string random_material()
		{
			List<string> list = new List<string>();
			list.Add("granite");
			list.Add("slate");
			list.Add("sandstone");
			list.Add("brick");
			list.Add("marble");
			list.Add("slabs of rock");
			int index = Session.Random.Next() % list.Count;
			string text = list[index];
			if (Session.Random.Next(3) == 0)
			{
				List<string> list2 = new List<string>();
				list2.Add("polished");
				list2.Add("rough");
				list2.Add("chiselled");
				list2.Add("uneven");
				list2.Add("worked");
				int index2 = Session.Random.Next() % list2.Count;
				text = list2[index2] + " " + text;
			}
			return text;
		}

		private static string random_finish()
		{
			List<string> list = new List<string>();
			list.Add("The walls here are covered in black soot.");
			list.Add(string.Concat(new string[]
			{
				"The walls are ",
				RoomBuilder.random_style(),
				" with ",
				RoomBuilder.random_deco(),
				"."
			}));
			list.Add("Claw marks cover the walls here.");
			list.Add("The walls have been painted " + RoomBuilder.random_colour() + ".");
			list.Add("It is possible to tell that the walls were once plastered.");
			list.Add("Here and there, graffiti covers the walls.");
			list.Add("A thick layer of dust covers the room.");
			list.Add("Moss and lichen grows here and there on the walls.");
			list.Add("The patina of age covers the walls.");
			list.Add("Childlike drawings and sketches cover the walls.");
			list.Add("Cryptic signs have been scratched into the walls.");
			int index = Session.Random.Next() % list.Count;
			return list[index];
		}

		private static string random_style()
		{
			List<string> list = new List<string>();
			list.Add("painted");
			list.Add("engraved");
			int index = Session.Random.Next() % list.Count;
			return list[index];
		}

		private static string random_deco()
		{
			List<string> list = new List<string>();
			list.Add("abstract artwork");
			list.Add("battle scenes");
			list.Add("landscape scenes");
			list.Add("hunting scenes");
			list.Add("pastoral scenes");
			list.Add("religious symbols");
			list.Add("runes");
			list.Add("glyphs");
			list.Add("sigils");
			list.Add("cryptic signs");
			int index = Session.Random.Next() % list.Count;
			return list[index];
		}

		private static string random_colour()
		{
			List<string> list = new List<string>();
			list.Add("black");
			list.Add("white");
			list.Add("grey");
			list.Add("red");
			list.Add("blue");
			list.Add("yellow");
			list.Add("purple");
			list.Add("green");
			list.Add("orange");
			list.Add("brown");
			list.Add("pink");
			int index = Session.Random.Next() % list.Count;
			return list[index];
		}

		private static string random_air()
		{
			List<string> list = new List<string>();
			list.Add("The room is bitterly cold.");
			list.Add("There is a distinct chill in the air.");
			list.Add("A cold breeze blows through this area.");
			list.Add("A warm draught blows through this area.");
			list.Add("The area is uncomfortably hot.");
			list.Add("The air is dank.");
			list.Add("The air here is warm and humid.");
			list.Add("A strange mist hangs in the air here.");
			list.Add("The room's surfaces are covered in frost.");
			int index = Session.Random.Next() % list.Count;
			return list[index];
		}

		private static string random_smell()
		{
			List<string> list = new List<string>();
			list.Add("A smell of burning hangs in the air.");
			list.Add("The air feels stagnant.");
			list.Add("From time to time the smell of blood catches your nostrils.");
			list.Add("The stench of rotting meat is in the air.");
			list.Add("The area has a strange musky smell.");
			list.Add("You notice a strangly acrid smell throughout the area.");
			list.Add("The area is filled with an unpleasant putrid smell.");
			int index = Session.Random.Next() % list.Count;
			return list[index];
		}

		private static string random_sound()
		{
			List<string> list = new List<string>();
			list.Add("You can hear distant chanting.");
			list.Add("You can hear a quiet buzzing sound.");
			list.Add("The sound of running water fills this area.");
			list.Add("A low humming sound can be heard.");
			list.Add("The area is unnaturally quiet.");
			list.Add("A quiet susurration can just be heard.");
			list.Add("Creaking sounds fill the area.");
			list.Add("Scratching sounds can be heard.");
			list.Add("From time to time, a distant voice can be heard.");
			int index = Session.Random.Next() % list.Count;
			return list[index];
		}

		private static string random_activity()
		{
			List<string> list = new List<string>();
			list.Add("The dust swirls as if disturbed by movement.");
			list.Add("You catch a sudden movement out of the corner of your eye.");
			list.Add("From time to time tiny pieces of debris fall from the ceiling.");
			list.Add("Water drips slowly from a crack in the ceiling.");
			list.Add("Water drips down the walls.");
			int index = Session.Random.Next() % list.Count;
			return list[index];
		}
	}
}
