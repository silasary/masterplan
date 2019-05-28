﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using Utils;
using Utils.Forms;

using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.UI;

namespace Masterplan
{
	static class Program
	{
		internal static bool IsBeta
		{
			get
			{
				return fIsBeta;
			}
		}

#if DEBUG
        static bool fIsBeta = true;
#else
        static bool fIsBeta = false;
#endif

        public static bool IsInstalled { get; } = typeof(Program).Assembly.Location.Contains(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

        public static string RootDirectory { get; } = Path.GetDirectoryName(typeof(Program).Assembly.Location);

        public static string UserDirectory { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), nameof(Masterplan));

        [STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			try
            {
                init_logging();

                SplashScreen = new ProgressScreen("Masterplan", 0)
                {
                    CurrentAction = "Loading..."
                };
                SplashScreen.Show();

                load_preferences();
                load_libraries();

                //handle_arg("-creaturestats");

                foreach (string arg in args)
                    handle_arg(arg);

                SplashScreen.CurrentAction = "Starting Masterplan...";
                SplashScreen.Actions = 0;

                try
                {
                    var main_form = new MainForm();
                    Application.Run(main_form);
                }
                catch (Exception ex)
                {
                    LogSystem.Trace(ex);
                }

                var forms = new List<Form>();
                foreach (Form form in Application.OpenForms)
                    forms.Add(form);
                foreach (Form form in forms)
                    form.Close();

                save_preferences();

                if (IsBeta)
                    check_for_logs();
            }
            catch (Exception ex)
			{
				LogSystem.Trace(ex);
			}
		}

#region Bootstrapping

		static void init_logging()
		{
			try
			{
                // Logging
                string mp_dir;
                if (IsInstalled)
                    mp_dir = UserDirectory;
                else
                    mp_dir = RootDirectory;

				// Make sure the log directory exists
				var logdir = Path.Combine(mp_dir, "Log");
                if (!Directory.Exists(logdir))
				{
					var di = Directory.CreateDirectory(logdir);
                    if (di == null)
						throw new UnauthorizedAccessException();
				}

                // Begin logging
                var logfile = Path.Combine(logdir, DateTime.Now.Ticks + ".log");
                LogSystem.LogFile = logfile;
			}
			catch
			{
			}
		}

		static void load_libraries()
		{
			try
            {
                SplashScreen.CurrentAction = "Loading libraries...";

                if (!IsInstalled)
                {
                    load_libraries(RootDirectory);
                }

                if (Directory.Exists(UserDirectory) || IsInstalled)
                {
                    load_libraries(UserDirectory);
                }

                Session.Libraries.Sort();
            }
            catch (Exception ex)
            {
                LogSystem.Trace(ex);
            }
		}

        private static void load_libraries(string root_dir)
        {
            var lib_dir = Path.Combine(root_dir, "Libraries");
            if (!Directory.Exists(lib_dir))
                Directory.CreateDirectory(lib_dir);

            // Move libraries from root directory
            var files = Directory.GetFiles(root_dir, "*.library");
            foreach (string filename in files)
            {
                try
                {
                    var lib_name = Path.Combine(lib_dir, Path.GetFileNameWithoutExtension(filename) + ".library");

                    if (!File.Exists(lib_name))
                        File.Move(filename, lib_name);
                }
                catch (Exception ex)
                {
                    LogSystem.Trace(ex);
                }
            }

            // Load libraries
            var libraries = Directory.GetFiles(lib_dir, "*.library");
            SplashScreen.Actions = libraries.Length;
            foreach (var filename in libraries)
                Session.LoadLibrary(filename);
        }

        static void load_preferences()
        {
            try
            {
                string root_dir;
                if (IsInstalled)
                    root_dir = UserDirectory;
                else
                    root_dir = RootDirectory;
                var filename = Path.Combine(root_dir, "Preferences.xml");

                if (File.Exists(filename))
                {
                    SplashScreen.CurrentAction = "Loading user preferences";

                    var prefs = Serialisation<Preferences>.Load(filename, SerialisationMode.XML);
                    if (prefs != null)
                        Session.Preferences = prefs;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Trace(ex);
            }
        }

        static void save_preferences()
        {
            try
            {
                string root_dir;
                if (IsInstalled)
                    root_dir = UserDirectory;
                else
                    root_dir = RootDirectory;
                string filename = Path.Combine(root_dir, "Preferences.xml");
                Serialisation<Preferences>.Save(filename, Session.Preferences, SerialisationMode.XML);
            }
            catch (Exception ex)
            {
				LogSystem.Trace(ex);
            }
        }

		static void handle_arg(string arg)
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
					run_creature_stats();
				}

				var fi = new FileInfo(arg);
                if (fi.Exists)
				{
					SplashScreen.CurrentAction = "Loading project...";
					SplashScreen.CurrentSubAction = Path.GetFileNameWithoutExtension(fi.Name);

					// Load file
					var p = Serialisation<Project>.Load(arg, SerialisationMode.Binary);
					if (p != null)
					{
						Session.CreateBackup(arg);
					}
					else
					{
						p = Session.LoadBackup(arg);
					}

					if (p != null)
					{
						if (Session.CheckPassword(p))
						{
							Session.Project = p;
							Session.FileName = arg;

							p.Update();
							p.SimplifyProjectLibrary();
						}
					}
				}
			}
			catch
			{
			}
		}

		static void check_for_logs()
		{
			string logfile = LogSystem.LogFile;

			if ((logfile == null) || (logfile == ""))
				return;

			if (!File.Exists(logfile))
				return;

			string logdir = Path.GetDirectoryName(logfile);
			Process.Start(logdir);
		}

#endregion

#region Stats

		private static void run_creature_stats()
		{
			// Run stats
			List<Creature> creatures = Session.Creatures;
			bool[] is_minion_options = { false, true };
			bool[] is_leader_options = { false, true };

			string datafile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Creatures.csv";
			StreamWriter sw = new StreamWriter(datafile);
			try
			{
				sw.Write("Level,Flag,Role,Minion,Leader,Tier,TierX,Creatures,Powers");
				// Conditions
				foreach (string condition in Conditions.GetConditions())
					sw.Write("," + condition);
				// Damage types
				foreach (DamageType damage in Enum.GetValues(typeof(DamageType)))
					sw.Write("," + damage);
				sw.WriteLine();

				for (int level = 1; level <= 40; ++level)
				{
					foreach (bool is_minion in is_minion_options)
					{
						foreach (bool is_leader in is_leader_options)
						{
							foreach (RoleType role in Enum.GetValues(typeof(RoleType)))
							{
								foreach (RoleFlag flag in Enum.GetValues(typeof(RoleFlag)))
								{
									List<Creature> list = get_creatures(creatures, level, is_minion, is_leader, role, flag);

									List<CreaturePower> powers = new List<CreaturePower>();
									foreach (Creature c in list)
										powers.AddRange(c.CreaturePowers);
									if (powers.Count == 0)
										continue;

									string tier = "";
									if (level < 11)
										tier = "heroic";
									else if (level < 21)
										tier = "paragon";
									else
										tier = "epic";

									string tierx = "";
									if (level < 4)
										tierx = "early heroic";
									else if (level < 8)
										tierx = "mid heroic";
									else if (level < 11)
										tierx = "late heroic";
									else if (level < 14)
										tierx = "early paragon";
									else if (level < 18)
										tierx = "mid paragon";
									else if (level < 21)
										tierx = "late paragon";
									else if (level < 24)
										tierx = "early epic";
									else if (level < 28)
										tierx = "mid epic";
									else if (level < 31)
										tierx = "late epic";
									else
										tierx = "epic plus";

									sw.Write(level + "," + flag + "," + role + "," + is_minion + "," + is_leader + "," + tier + "," + tierx + "," + list.Count + "," + powers.Count);

									foreach (string condition in Conditions.GetConditions())
									{
										var count = 0;

                                        var str = condition.ToLower();
                                        foreach (CreaturePower power in powers)
											if (power.Details.ToLower().Contains(str))
												count += 1;

										double pc = 0;
										if (powers.Count != 0)
											pc = (double)count / powers.Count;

										sw.Write("," + pc);
									}

									foreach (DamageType damage in Enum.GetValues(typeof(DamageType)))
									{
										var count = 0;

                                        var str = damage.ToString().ToLower();
                                        foreach (CreaturePower power in powers)
											if (power.Details.ToLower().Contains(str))
												count += 1;

										double pc = 0;
										if (powers.Count != 0)
											pc = (double)count / powers.Count;

										sw.Write("," + pc);
									}

									sw.WriteLine();
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
				sw.Close();
			}
		}

		private static List<Creature> get_creatures(List<Creature> creatures, int level, bool is_minion, bool is_leader, RoleType role, RoleFlag flag)
		{
			var list = new List<Creature>();

            foreach (Creature c in creatures)
			{
				if (c.Level != level)
					continue;

				var cr = c.Role as ComplexRole;
                var m = c.Role as Minion;

                if ((m != null) && (!m.HasRole))
					continue;

				var minion = m != null;
                if (minion != is_minion)
					continue;

				var leader = ((cr != null) && (cr.Leader));
                if (leader != is_leader)
					continue;

				var rt = RoleType.Blaster;
                var rf = RoleFlag.Standard;
                if (cr != null)
				{
					rt = cr.Type;
					rf = cr.Flag;
				}
				if (m != null)
				{
					rt = m.Type;
					rf = RoleFlag.Standard;
				}

				if (rt != role)
					continue;

				if (rf != flag)
					continue;

				list.Add(c);
			}

			return list;
		}

#endregion

		internal static void SetResolution(Image img)
		{
            if (img is Bitmap bmp)
            {
                try
                {
                    float x_dpi = Math.Min(bmp.HorizontalResolution, 96);
                    float y_dpi = Math.Min(bmp.VerticalResolution, 96);

                    bmp.SetResolution(x_dpi, y_dpi);
                }
                catch
                {
                    // Didn't set anything
                }
            }
        }

        public static ProgressScreen SplashScreen = null;

#region File filters

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

#endregion
	}
}
