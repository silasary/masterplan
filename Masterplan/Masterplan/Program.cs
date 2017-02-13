using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Utils;
using Utils.Forms;

namespace Masterplan
{
	internal static class Program
	{
#if DEBUG
		internal static bool fIsBeta = true;
#else
		internal static bool fIsBeta = false;
#endif

		public static ProgressScreen SplashScreen = null;

		public static string ProjectFilter = "Masterplan Project|*.masterplan";

		public static string LibraryFilter = "Masterplan Library|*.library";

		public static string EncounterFilter = "Masterplan Encounter|*.encounter";

		public static string BackgroundFilter = "Masterplan Campaign Background|*.background";

		public static string EncyclopediaFilter = "Masterplan Campaign Encyclopedia|*.encyclopedia";

		public static string RulesFilter = "Masterplan Rules|*.crunch";

		public static string CreatureAndMonsterFilter = "Creatures|*.creature;*.monster";

		public static string MonsterFilter = "Adventure Tools Creatures|*.monster";

		public static string CreatureFilter = "Creatures|*.creature";

		public static string CreatureTemplateFilter = "Creature Template|*.creaturetemplate";

		public static string ThemeFilter = "Themes|*.theme";

		public static string CreatureTemplateAndThemeFilter = "Creature Templates and Themes|*.creaturetemplate;*.theme";

		public static string TrapFilter = "Traps|*.trap";

		public static string SkillChallengeFilter = "Skill Challenges|*.skillchallenge";

		public static string MagicItemFilter = "Magic Items|*.magicitem";

		public static string ArtifactFilter = "Artifacts|*.artifact";

		public static string MapTileFilter = "Map Tiles|*.maptile";

		public static string TerrainPowerFilter = "Terrain Powers|*.terrainpower";

		public static string HTMLFilter = "HTML File|*.htm";

		public static string ImageFilter = "Image File|*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tga";

		internal static bool IsBeta
		{
			get
			{
				return Program.fIsBeta;
			}
		}

		internal static string SecurityData
		{
			get
			{
				string username = SystemInformation.UserName.ToLower();
				string computername = SystemInformation.ComputerName.ToLower();
				return username + " on " + computername;
			}
		}

		internal static bool CopyProtection
		{
			get
			{
				return !Program.IsBeta;
			}
		}

		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			try
			{
				Program.init_logging();
                SplashScreen = new ProgressScreen("Masterplan", 0)
                {
                    CurrentAction = "Loading..."
                };
                Program.SplashScreen.Show();
				Program.load_preferences();
				Program.load_libraries();
				for (int i = 0; i < args.Length; i++)
				{
					string arg = args[i];
					Program.handle_arg(arg);
				}
				Program.SplashScreen.CurrentAction = "Starting Masterplan...";
				Program.SplashScreen.Actions = 0;
				try
				{
					MainForm mainForm = new MainForm();
					Application.Run(mainForm);
				}
				catch (Exception ex)
				{
					LogSystem.Trace(ex);
				}
				List<Form> list = new List<Form>();
				foreach (Form item in Application.OpenForms)
				{
					list.Add(item);
				}
				foreach (Form current in list)
				{
					current.Close();
				}
				Program.save_preferences();
				if (Program.IsBeta)
				{
                    check_for_logs();
				}
			}
			catch (Exception ex2)
			{
				LogSystem.Trace(ex2);
			}
		}

		private static void init_logging()
		{
			try
			{
				string arg = FileName.Directory(Application.ExecutablePath);
				string text = arg + "Log" + Path.DirectorySeparatorChar;
				if (!Directory.Exists(text) && Directory.CreateDirectory(text) == null)
				{
					throw new UnauthorizedAccessException();
				}
				string logFile = text + DateTime.Now.Ticks + ".log";
				LogSystem.LogFile = logFile;
			}
			catch
			{
			}
		}

		private static void load_libraries()
		{
			try
			{
				Program.SplashScreen.CurrentAction = "Loading libraries...";
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				string text = FileName.Directory(entryAssembly.Location);
				string text2 = text + "Libraries\\";
				if (!Directory.Exists(text2))
				{
					Directory.CreateDirectory(text2);
				}
				string[] files = Directory.GetFiles(text, "*.library");
				string[] array = files;
				for (int i = 0; i < array.Length; i++)
				{
					string text3 = array[i];
					try
					{
						string text4 = text2 + FileName.Name(text3) + ".library";
						if (!File.Exists(text4))
						{
							File.Move(text3, text4);
						}
					}
					catch (Exception ex)
					{
						LogSystem.Trace(ex);
					}
				}
				string[] files2 = Directory.GetFiles(text2, "*.library");
				Program.SplashScreen.Actions = files2.Length;
				string[] array2 = files2;
				for (int j = 0; j < array2.Length; j++)
				{
					string filename = array2[j];
					Session.LoadLibrary(filename);
				}
				Session.Libraries.Sort();
			}
			catch (Exception ex2)
			{
				LogSystem.Trace(ex2);
			}
		}

		private static void load_preferences()
		{
			try
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				string assemblyPath = FileName.Directory(entryAssembly.Location);
				string prefPath = assemblyPath + "Preferences.xml";
				if (File.Exists(prefPath))
				{
					Program.SplashScreen.CurrentAction = "Loading user preferences";
					Preferences preferences = Serialisation<Preferences>.Load(prefPath, SerialisationMode.XML);
					if (preferences != null)
					{
						Session.Preferences = preferences;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private static void save_preferences()
		{
			try
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				string str = FileName.Directory(entryAssembly.Location);
				string filename = str + "Preferences.xml";
				Serialisation<Preferences>.Save(filename, Session.Preferences, SerialisationMode.XML);
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

		private static void handle_arg(string arg)
		{
			try
			{
				if (arg == "-headlines")
				{
					Session.Preferences.ShowHeadlines = true;
				}
				if (arg == "-noheadlines")
				{
					Session.Preferences.ShowHeadlines = false;
				}
				if (arg == "-creaturestats")
				{
					Program.run_creature_stats();
				}
				FileInfo fileInfo = new FileInfo(arg);
				if (fileInfo.Exists)
				{
					Program.SplashScreen.CurrentAction = "Loading project...";
					Program.SplashScreen.CurrentSubAction = FileName.Name(fileInfo.Name);
					Project project = Serialisation<Project>.Load(arg, SerialisationMode.Binary);
					if (project != null)
					{
						Session.CreateBackup(arg);
					}
					else
					{
						project = Session.LoadBackup(arg);
					}
					if (project != null && Session.CheckPassword(project))
					{
						Session.Project = project;
						Session.FileName = arg;
						project.Update();
						project.SimplifyProjectLibrary();
					}
				}
			}
			catch
			{
			}
		}

		private static void check_for_logs()
		{
			string logFile = LogSystem.LogFile;
			if (logFile == null || logFile == "")
			{
				return;
			}
			if (!File.Exists(logFile))
			{
				return;
			}
			string fileName = FileName.Directory(logFile);
			Process.Start(fileName);
		}

		private static void run_creature_stats()
		{
			List<Creature> creatures = Session.Creatures;
			bool[] array = new bool[]
			{
				default(bool),
				true
			};
			bool[] array2 = new bool[]
			{
				default(bool),
				true
			};
			string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Creatures.csv";
			StreamWriter streamWriter = new StreamWriter(path);
			try
			{
				streamWriter.Write("Level,Flag,Role,Minion,Leader,Tier,TierX,Creatures,Powers");
				foreach (string current in Conditions.GetConditions())
				{
					streamWriter.Write("," + current);
				}
				foreach (DamageType damageType in Enum.GetValues(typeof(DamageType)))
				{
					streamWriter.Write("," + damageType);
				}
				streamWriter.WriteLine();
				for (int i = 1; i <= 40; i++)
				{
					bool[] array3 = array;
					for (int j = 0; j < array3.Length; j++)
					{
						bool flag = array3[j];
						bool[] array4 = array2;
						for (int k = 0; k < array4.Length; k++)
						{
							bool flag2 = array4[k];
							foreach (RoleType roleType in Enum.GetValues(typeof(RoleType)))
							{
								foreach (RoleFlag roleFlag in Enum.GetValues(typeof(RoleFlag)))
								{
									List<Creature> list = Program.get_creatures(creatures, i, flag, flag2, roleType, roleFlag);
									List<CreaturePower> list2 = new List<CreaturePower>();
									foreach (Creature current2 in list)
									{
										list2.AddRange(current2.CreaturePowers);
									}
									if (list2.Count != 0)
									{
										string text;
										if (i < 11)
										{
											text = "heroic";
										}
										else if (i < 21)
										{
											text = "paragon";
										}
										else
										{
											text = "epic";
										}
										string text2;
										if (i < 4)
										{
											text2 = "early heroic";
										}
										else if (i < 8)
										{
											text2 = "mid heroic";
										}
										else if (i < 11)
										{
											text2 = "late heroic";
										}
										else if (i < 14)
										{
											text2 = "early paragon";
										}
										else if (i < 18)
										{
											text2 = "mid paragon";
										}
										else if (i < 21)
										{
											text2 = "late paragon";
										}
										else if (i < 24)
										{
											text2 = "early epic";
										}
										else if (i < 28)
										{
											text2 = "mid epic";
										}
										else if (i < 31)
										{
											text2 = "late epic";
										}
										else
										{
											text2 = "epic plus";
										}
										streamWriter.Write(string.Concat(new object[]
										{
											i,
											",",
											roleFlag,
											",",
											roleType,
											",",
											flag,
											",",
											flag2,
											",",
											text,
											",",
											text2,
											",",
											list.Count,
											",",
											list2.Count
										}));
										foreach (string current3 in Conditions.GetConditions())
										{
											int num = 0;
											string value = current3.ToLower();
											foreach (CreaturePower current4 in list2)
											{
												if (current4.Details.ToLower().Contains(value))
												{
													num++;
												}
											}
											double num2 = 0.0;
											if (list2.Count != 0)
											{
												num2 = (double)num / (double)list2.Count;
											}
											streamWriter.Write("," + num2);
										}
										foreach (DamageType damageType2 in Enum.GetValues(typeof(DamageType)))
										{
											int num3 = 0;
											string value2 = damageType2.ToString().ToLower();
											foreach (CreaturePower current5 in list2)
											{
												if (current5.Details.ToLower().Contains(value2))
												{
													num3++;
												}
											}
											double num4 = 0.0;
											if (list2.Count != 0)
											{
												num4 = (double)num3 / (double)list2.Count;
											}
											streamWriter.Write("," + num4);
										}
										streamWriter.WriteLine();
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
			finally
			{
				streamWriter.Close();
			}
		}

		private static List<Creature> get_creatures(List<Creature> creatures, int level, bool is_minion, bool is_leader, RoleType role, RoleFlag flag)
		{
			List<Creature> list = new List<Creature>();
			foreach (Creature current in creatures)
			{
				if (current.Level == level)
				{
					ComplexRole complexRole = current.Role as ComplexRole;
					Minion minion = current.Role as Minion;
					if (minion == null || minion.HasRole)
					{
						bool flag2 = minion != null;
						if (flag2 == is_minion)
						{
							bool flag3 = complexRole != null && complexRole.Leader;
							if (flag3 == is_leader)
							{
								RoleType roleType = RoleType.Blaster;
								RoleFlag roleFlag = RoleFlag.Standard;
								if (complexRole != null)
								{
									roleType = complexRole.Type;
									roleFlag = complexRole.Flag;
								}
								if (minion != null)
								{
									roleType = minion.Type;
									roleFlag = RoleFlag.Standard;
								}
								if (roleType == role && roleFlag == flag)
								{
									list.Add(current);
								}
							}
						}
					}
				}
			}
			return list;
		}

		internal static string SimplifySecurityData(string raw_data)
		{
			string[] separator = new string[]
			{
				" on "
			};
			string[] array = raw_data.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 2)
			{
				return "";
			}
			string text = array[0].ToLower();
			int num = text.IndexOf(".");
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			string text2 = array[1].ToLower();
			int num2 = text2.IndexOf(".");
			if (num2 != -1)
			{
				text2 = text2.Substring(0, num2);
			}
			return text + " on " + text2;
		}

		internal static void SetResolution(Image img)
		{
			Bitmap bitmap = img as Bitmap;
			if (bitmap != null)
			{
				try
				{
					float xDpi = Math.Min(bitmap.HorizontalResolution, 96f);
					float yDpi = Math.Min(bitmap.VerticalResolution, 96f);
					bitmap.SetResolution(xDpi, yDpi);
				}
				catch
				{
				}
			}
		}
	}
}
