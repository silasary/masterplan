using Masterplan.Data;
using Masterplan.Extensibility;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Utils;

namespace Masterplan
{
	internal class Session
	{
		private static Project fProject = null;

		private static PlayerViewForm fPlayerView = null;

		private static bool fModified = false;

		private static string fFileName = "";

		private static Random fRandom = new Random();

		private static List<IAddIn> fAddIns = new List<IAddIn>();

		public static List<Library> Libraries = new List<Library>();

		private static MainForm fMainForm = null;

		private static Encounter fCurrentEncounter = null;

		private static List<string> fDisabledLibraries = new List<string>();

		public static Project Project
		{
			get
			{
				return Session.fProject;
			}
			set
			{
				Session.fProject = value;
			}
		}

		public static Preferences Preferences { get; set; } = new Preferences();

        public static PlayerViewForm PlayerView
		{
			get
			{
				return Session.fPlayerView;
			}
			set
			{
				Session.fPlayerView = value;
			}
		}

		public static bool Modified
		{
			get
			{
				return Session.fModified;
			}
			set
			{
				Session.fModified = value;
			}
		}

		public static string FileName
		{
			get
			{
				return Session.fFileName;
			}
			set
			{
				Session.fFileName = value;
			}
		}

		public static Random Random
		{
			get
			{
				return Session.fRandom;
			}
		}

		public static List<IAddIn> AddIns
		{
			get
			{
				return Session.fAddIns;
			}
		}

		public static string LibraryFolder
		{
			get
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				return Utils.FileName.Directory(entryAssembly.Location) + "Libraries\\";
			}
		}

		public static List<Creature> Creatures
		{
			get
			{
				List<Creature> list = new List<Creature>();
				foreach (Library current in Session.Libraries)
				{
					foreach (Creature current2 in current.Creatures)
					{
						if (current2 != null)
						{
							list.Add(current2);
						}
					}
				}
				if (Session.fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (Creature current3 in list)
					{
						if (current3 != null)
						{
							binarySearchTree.Add(current3.ID);
						}
					}
					foreach (Creature current4 in Session.fProject.Library.Creatures)
					{
						if (current4 != null && !binarySearchTree.Contains(current4.ID))
						{
							list.Add(current4);
						}
					}
				}
				return list;
			}
		}

		public static List<CreatureTemplate> Templates
		{
			get
			{
				List<CreatureTemplate> list = new List<CreatureTemplate>();
				foreach (Library current in Session.Libraries)
				{
					foreach (CreatureTemplate current2 in current.Templates)
					{
						if (current2 != null)
						{
							list.Add(current2);
						}
					}
				}
				if (Session.fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (CreatureTemplate current3 in list)
					{
						if (current3 != null)
						{
							binarySearchTree.Add(current3.ID);
						}
					}
					foreach (CreatureTemplate current4 in Session.fProject.Library.Templates)
					{
						if (current4 != null && !binarySearchTree.Contains(current4.ID))
						{
							list.Add(current4);
						}
					}
				}
				return list;
			}
		}

		public static List<MonsterTheme> Themes
		{
			get
			{
				List<MonsterTheme> list = new List<MonsterTheme>();
				foreach (Library library in Session.Libraries)
				{
					foreach (MonsterTheme theme in library.Themes)
					{
						if (theme != null)
						{
							list.Add(theme);
						}
					}
				}
				if (fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (MonsterTheme theme in list)
					{
						if (theme != null) // Is this even possible?
						{
							binarySearchTree.Add(theme.ID);
						}
					}
					foreach (MonsterTheme theme in fProject.Library.Themes)
					{
						if (theme != null && !binarySearchTree.Contains(theme.ID))
						{
							list.Add(theme);
						}
					}
				}
				return list;
			}
		}

		public static List<Trap> Traps
		{
			get
			{
				List<Trap> list = new List<Trap>();
				foreach (Library current in Session.Libraries)
				{
					foreach (Trap current2 in current.Traps)
					{
						if (current2 != null)
						{
							list.Add(current2);
						}
					}
				}
				if (Session.fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (Trap current3 in list)
					{
						if (current3 != null)
						{
							binarySearchTree.Add(current3.ID);
						}
					}
					foreach (Trap current4 in Session.fProject.Library.Traps)
					{
						if (current4 != null && !binarySearchTree.Contains(current4.ID))
						{
							list.Add(current4);
						}
					}
				}
				return list;
			}
		}

		public static List<SkillChallenge> SkillChallenges
		{
			get
			{
				List<SkillChallenge> list = new List<SkillChallenge>();
				foreach (Library current in Session.Libraries)
				{
					list.AddRange(current.SkillChallenges);
				}
				if (Session.fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (SkillChallenge current2 in list)
					{
						if (current2 != null)
						{
							binarySearchTree.Add(current2.ID);
						}
					}
					foreach (SkillChallenge current3 in Session.fProject.Library.SkillChallenges)
					{
						if (current3 != null && !binarySearchTree.Contains(current3.ID))
						{
							list.Add(current3);
						}
					}
				}
				return list;
			}
		}

		public static List<MagicItem> MagicItems
		{
			get
			{
				List<MagicItem> list = new List<MagicItem>();
				foreach (Library current in Session.Libraries)
				{
					foreach (MagicItem current2 in current.MagicItems)
					{
						if (current2 != null)
						{
							list.Add(current2);
						}
					}
				}
				if (Session.fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (MagicItem current3 in list)
					{
						if (current3 != null)
						{
							binarySearchTree.Add(current3.ID);
						}
					}
					foreach (MagicItem current4 in Session.fProject.Library.MagicItems)
					{
						if (current4 != null && !binarySearchTree.Contains(current4.ID))
						{
							list.Add(current4);
						}
					}
				}
				return list;
			}
		}

		public static List<Artifact> Artifacts
		{
			get
			{
				List<Artifact> list = new List<Artifact>();
				foreach (Library current in Session.Libraries)
				{
					foreach (Artifact current2 in current.Artifacts)
					{
						if (current2 != null)
						{
							list.Add(current2);
						}
					}
				}
				if (Session.fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (Artifact current3 in list)
					{
						if (current3 != null)
						{
							binarySearchTree.Add(current3.ID);
						}
					}
					foreach (Artifact current4 in Session.fProject.Library.Artifacts)
					{
						if (current4 != null && !binarySearchTree.Contains(current4.ID))
						{
							list.Add(current4);
						}
					}
				}
				return list;
			}
		}

		public static List<Tile> Tiles
		{
			get
			{
				List<Tile> list = new List<Tile>();
				foreach (Library current in Session.Libraries)
				{
					foreach (Tile current2 in current.Tiles)
					{
						if (current2 != null)
						{
							list.Add(current2);
						}
					}
				}
				if (Session.fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (Tile current3 in list)
					{
						if (current3 != null)
						{
							binarySearchTree.Add(current3.ID);
						}
					}
					foreach (Tile current4 in Session.fProject.Library.Tiles)
					{
						if (current4 != null && !binarySearchTree.Contains(current4.ID))
						{
							list.Add(current4);
						}
					}
				}
				return list;
			}
		}

		public static List<TerrainPower> TerrainPowers
		{
			get
			{
				List<TerrainPower> list = new List<TerrainPower>();
				foreach (Library current in Session.Libraries)
				{
					foreach (TerrainPower current2 in current.TerrainPowers)
					{
						if (current2 != null)
						{
							list.Add(current2);
						}
					}
				}
				if (Session.fProject != null)
				{
					BinarySearchTree<Guid> binarySearchTree = new BinarySearchTree<Guid>();
					foreach (TerrainPower current3 in list)
					{
						if (current3 != null)
						{
							binarySearchTree.Add(current3.ID);
						}
					}
					foreach (TerrainPower current4 in Session.fProject.Library.TerrainPowers)
					{
						if (current4 != null && !binarySearchTree.Contains(current4.ID))
						{
							list.Add(current4);
						}
					}
				}
				return list;
			}
		}

		public static MainForm MainForm
		{
			get
			{
				return Session.fMainForm;
			}
			set
			{
				Session.fMainForm = value;
			}
		}

		public static Encounter CurrentEncounter
		{
			get
			{
				return Session.fCurrentEncounter;
			}
			set
			{
				Session.fCurrentEncounter = value;
			}
		}

		public static List<string> DisabledLibraries
		{
			get
			{
				return Session.fDisabledLibraries;
			}
			set
			{
				Session.fDisabledLibraries = value;
			}
		}

		public static string GetLibraryFilename(Library lib)
		{
			DirectoryInfo arg = new DirectoryInfo(Session.LibraryFolder);
			string arg2 = Utils.FileName.TrimInvalidCharacters(lib.Name);
			return arg + arg2 + ".library";
		}

		public static Library FindLibrary(string name)
		{
			string b = Utils.FileName.TrimInvalidCharacters(name);
			foreach (Library current in Session.Libraries)
			{
				if (current.Name == name)
				{
					Library result = current;
					return result;
				}
				string a = Utils.FileName.TrimInvalidCharacters(current.Name);
				if (a == b)
				{
					Library result = current;
					return result;
				}
			}
			return null;
		}

		public static Library LoadLibrary(string filename)
		{
			try
			{
				if (Program.SplashScreen != null)
				{
					Program.SplashScreen.CurrentSubAction = Utils.FileName.Name(filename);
					Program.SplashScreen.Progress++;
				}
				string text = Program.SimplifySecurityData(Program.SecurityData);
				Library library = Serialisation<Library>.Load(filename, SerialisationMode.Binary);
				Library result;
				if (library != null)
				{
					library.Name = Utils.FileName.Name(filename);
					library.Update();
					if (Program.CopyProtection)
					{
						if (library.SecurityData == null || library.SecurityData == "")
						{
							library.SecurityData = text;
							if (!Serialisation<Library>.Save(filename, library, SerialisationMode.Binary))
							{
								LogSystem.Trace("Could not save " + library.Name);
							}
						}
						string text2 = Program.SimplifySecurityData(library.SecurityData);
						if (text2 != text)
						{
							LogSystem.Trace(string.Concat(new string[]
							{
								"Could not load ",
								library.Name,
								": ",
								text2,
								" vs ",
								text
							}));
							Session.DisabledLibraries.Add(library.Name);
							result = null;
							return result;
						}
					}
					else if (library.SecurityData != "")
					{
						library.SecurityData = "";
						Serialisation<Library>.Save(filename, library, SerialisationMode.Binary);
					}
					Session.Libraries.Add(library);
				}
				else
				{
					LogSystem.Trace("Could not load " + Utils.FileName.Name(filename));
				}
				result = library;
				return result;
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return null;
		}

		public static void DeleteLibrary(Library lib)
		{
			string libraryFilename = Session.GetLibraryFilename(lib);
			FileInfo fileInfo = new FileInfo(libraryFilename);
			fileInfo.Delete();
			Session.Libraries.Remove(lib);
		}

		public static Library FindLibrary(Creature c)
		{
			if (c == null)
			{
				return null;
			}
			foreach (Library current in Session.Libraries)
			{
				foreach (Creature current2 in current.Creatures)
				{
					if (current2 != null && current2.ID == c.ID)
					{
						Library result = current;
						return result;
					}
				}
			}
			if (Session.fProject != null)
			{
				foreach (Creature current3 in Session.fProject.Library.Creatures)
				{
					if (current3 != null && current3.ID == c.ID)
					{
						Library result = Session.fProject.Library;
						return result;
					}
				}
			}
			return null;
		}

		public static Library FindLibrary(CreatureTemplate ct)
		{
			if (ct == null)
			{
				return null;
			}
			foreach (Library current in Session.Libraries)
			{
				foreach (CreatureTemplate current2 in current.Templates)
				{
					if (current2 != null && current2.ID == ct.ID)
					{
						Library result = current;
						return result;
					}
				}
			}
			if (Session.fProject != null)
			{
				foreach (CreatureTemplate current3 in Session.fProject.Library.Templates)
				{
					if (current3 != null && current3.ID == ct.ID)
					{
						Library result = Session.fProject.Library;
						return result;
					}
				}
			}
			return null;
		}

		public static Library FindLibrary(MonsterTheme mt)
		{
			if (mt == null)
			{
				return null;
			}
			foreach (Library current in Session.Libraries)
			{
				foreach (MonsterTheme current2 in current.Themes)
				{
					if (current2 != null && current2.ID == mt.ID)
					{
						Library result = current;
						return result;
					}
				}
			}
			if (Session.fProject != null)
			{
				foreach (MonsterTheme current3 in Session.fProject.Library.Themes)
				{
					if (current3 != null && current3.ID == mt.ID)
					{
						Library result = Session.fProject.Library;
						return result;
					}
				}
			}
			return null;
		}

		public static Library FindLibrary(Trap t)
		{
			if (t == null)
			{
				return null;
			}
			foreach (Library current in Session.Libraries)
			{
				foreach (Trap current2 in current.Traps)
				{
					if (current2 != null && current2.ID == t.ID)
					{
						Library result = current;
						return result;
					}
				}
			}
			if (Session.fProject != null)
			{
				foreach (Trap current3 in Session.fProject.Library.Traps)
				{
					if (current3 != null && current3.ID == t.ID)
					{
						Library result = Session.fProject.Library;
						return result;
					}
				}
			}
			return null;
		}

		public static Library FindLibrary(SkillChallenge sc)
		{
			if (sc == null)
			{
				return null;
			}
			foreach (Library current in Session.Libraries)
			{
				foreach (SkillChallenge current2 in current.SkillChallenges)
				{
					if (current2 != null && current2.ID == sc.ID)
					{
						Library result = current;
						return result;
					}
				}
			}
			if (Session.fProject != null)
			{
				foreach (SkillChallenge current3 in Session.fProject.Library.SkillChallenges)
				{
					if (current3 != null && current3.ID == sc.ID)
					{
						Library result = Session.fProject.Library;
						return result;
					}
				}
			}
			return null;
		}

		public static Library FindLibrary(MagicItem mi)
		{
			if (mi == null)
			{
				return null;
			}
			foreach (Library current in Session.Libraries)
			{
				foreach (MagicItem current2 in current.MagicItems)
				{
					if (current2 != null && current2.ID == mi.ID)
					{
						Library result = current;
						return result;
					}
				}
			}
			if (Session.fProject != null)
			{
				foreach (MagicItem current3 in Session.fProject.Library.MagicItems)
				{
					if (current3 != null && current3.ID == mi.ID)
					{
						Library result = Session.fProject.Library;
						return result;
					}
				}
			}
			return null;
		}

		public static Library FindLibrary(Artifact a)
		{
			if (a == null)
			{
				return null;
			}
			foreach (Library current in Session.Libraries)
			{
				foreach (Artifact current2 in current.Artifacts)
				{
					if (current2 != null && current2.ID == a.ID)
					{
						Library result = current;
						return result;
					}
				}
			}
			if (Session.fProject != null)
			{
				foreach (Artifact current3 in Session.fProject.Library.Artifacts)
				{
					if (current3 != null && current3.ID == a.ID)
					{
						Library result = Session.fProject.Library;
						return result;
					}
				}
			}
			return null;
		}

		public static Library FindLibrary(Tile t)
		{
			if (t == null)
			{
				return null;
			}
			foreach (Library current in Session.Libraries)
			{
				foreach (Tile current2 in current.Tiles)
				{
					if (current2 != null && current2.ID == t.ID)
					{
						Library result = current;
						return result;
					}
				}
			}
			if (Session.fProject != null)
			{
				foreach (Tile current3 in Session.fProject.Library.Tiles)
				{
					if (current3 != null && current3.ID == t.ID)
					{
						Library result = Session.fProject.Library;
						return result;
					}
				}
			}
			return null;
		}

		public static Library FindLibrary(TerrainPower tp)
		{
			if (tp == null)
			{
				return null;
			}
			foreach (Library current in Session.Libraries)
			{
				foreach (TerrainPower current2 in current.TerrainPowers)
				{
					if (current2 != null && current2.ID == tp.ID)
					{
						Library result = current;
						return result;
					}
				}
			}
			if (Session.fProject != null)
			{
				foreach (TerrainPower current3 in Session.fProject.Library.TerrainPowers)
				{
					if (current3 != null && current3.ID == tp.ID)
					{
						Library result = Session.fProject.Library;
						return result;
					}
				}
			}
			return null;
		}

		public static ICreature FindCreature(Guid creature_id, SearchType search_type)
		{
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				foreach (Library current in Session.Libraries)
				{
					Creature creature = current.FindCreature(creature_id);
					if (creature != null)
					{
						return creature;
					}
				}
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				Creature creature2 = Session.Project.Library.FindCreature(creature_id);
				if (creature2 != null)
				{
					return creature2;
				}
				CustomCreature customCreature = Session.Project.FindCustomCreature(creature_id);
				if (customCreature != null)
				{
					return customCreature;
				}
				NPC nPC = Session.Project.FindNPC(creature_id);
				if (nPC != null)
				{
					return nPC;
				}
			}
			return null;
		}

		public static CreatureTemplate FindTemplate(Guid template_id, SearchType search_type)
		{
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				foreach (Library current in Session.Libraries)
				{
					CreatureTemplate creatureTemplate = current.FindTemplate(template_id);
					if (creatureTemplate != null)
					{
						return creatureTemplate;
					}
				}
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				CreatureTemplate creatureTemplate2 = Session.Project.Library.FindTemplate(template_id);
				if (creatureTemplate2 != null)
				{
					return creatureTemplate2;
				}
			}
			return null;
		}

		public static MonsterTheme FindTheme(Guid theme_id, SearchType search_type)
		{
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				foreach (Library current in Session.Libraries)
				{
					MonsterTheme monsterTheme = current.FindTheme(theme_id);
					if (monsterTheme != null)
					{
						return monsterTheme;
					}
				}
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				MonsterTheme monsterTheme2 = Session.Project.Library.FindTheme(theme_id);
				if (monsterTheme2 != null)
				{
					return monsterTheme2;
				}
			}
			return null;
		}

		public static Trap FindTrap(Guid trap_id, SearchType search_type)
		{
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				foreach (Library current in Session.Libraries)
				{
					Trap trap = current.FindTrap(trap_id);
					if (trap != null)
					{
						return trap;
					}
				}
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				Trap trap2 = Session.Project.Library.FindTrap(trap_id);
				if (trap2 != null)
				{
					return trap2;
				}
			}
			return null;
		}

		public static SkillChallenge FindSkillChallenge(Guid sc_id, SearchType search_type)
		{
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				foreach (Library current in Session.Libraries)
				{
					SkillChallenge skillChallenge = current.FindSkillChallenge(sc_id);
					if (skillChallenge != null)
					{
						return skillChallenge;
					}
				}
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				SkillChallenge skillChallenge2 = Session.Project.Library.FindSkillChallenge(sc_id);
				if (skillChallenge2 != null)
				{
					return skillChallenge2;
				}
			}
			return null;
		}

		public static MagicItem FindMagicItem(Guid item_id, SearchType search_type)
		{
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				foreach (Library current in Session.Libraries)
				{
					MagicItem magicItem = current.FindMagicItem(item_id);
					if (magicItem != null)
					{
						return magicItem;
					}
				}
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				MagicItem magicItem2 = Session.Project.Library.FindMagicItem(item_id);
				if (magicItem2 != null)
				{
					return magicItem2;
				}
			}
			return null;
		}

		public static Artifact FindArtifact(Guid artifact_id, SearchType search_type)
		{
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				foreach (Library current in Session.Libraries)
				{
					Artifact artifact = current.FindArtifact(artifact_id);
					if (artifact != null)
					{
						return artifact;
					}
				}
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				Artifact artifact2 = Session.Project.Library.FindArtifact(artifact_id);
				if (artifact2 != null)
				{
					return artifact2;
				}
			}
			return null;
		}

		public static Tile FindTile(Guid tile_id, SearchType search_type)
		{
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				foreach (Library current in Session.Libraries)
				{
					Tile tile = current.FindTile(tile_id);
					if (tile != null)
					{
						return tile;
					}
				}
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				Tile tile2 = Session.Project.Library.FindTile(tile_id);
				if (tile2 != null)
				{
					return tile2;
				}
			}
			return null;
		}

		public static TerrainPower FindTerrainPower(Guid power_id, SearchType search_type)
		{
			if (search_type == SearchType.External || search_type == SearchType.Global)
			{
				foreach (Library current in Session.Libraries)
				{
					TerrainPower terrainPower = current.FindTerrainPower(power_id);
					if (terrainPower != null)
					{
						return terrainPower;
					}
				}
			}
			if ((search_type == SearchType.Project || search_type == SearchType.Global) && Session.Project != null)
			{
				TerrainPower terrainPower2 = Session.Project.Library.FindTerrainPower(power_id);
				if (terrainPower2 != null)
				{
					return terrainPower2;
				}
			}
			return null;
		}

		public static void CreateBackup(string filename)
		{
			try
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				string text = Utils.FileName.Directory(entryAssembly.Location) + "Backup\\";
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				string destFileName = text + Utils.FileName.Name(filename);
				File.Copy(filename, destFileName, true);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		public static Project LoadBackup(string filename)
		{
			Project project = null;
			try
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				string text = Utils.FileName.Directory(entryAssembly.Location) + "Backup\\";
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				string text2 = text + Utils.FileName.Name(filename);
				if (File.Exists(text2))
				{
					project = Serialisation<Project>.Load(text2, SerialisationMode.Binary);
					if (project != null)
					{
						string text3 = "There was a problem opening this project; it has been recovered from its most recent backup version.";
						MessageBox.Show(text3, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			return project;
		}

		public static int Dice(int throws, int sides)
		{
			int num = 0;
			for (int num2 = 0; num2 != throws; num2++)
			{
				int num3 = 1 + Session.fRandom.Next() % sides;
				num += num3;
			}
			return num;
		}

		public static bool CheckPassword(Project p)
		{
			if (p.Password == null || p.Password == "")
			{
				return true;
			}
			PasswordCheckForm passwordCheckForm = new PasswordCheckForm(p.Password, p.PasswordHint);
			return passwordCheckForm.ShowDialog() == DialogResult.OK;
		}
	}
}
