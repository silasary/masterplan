using Masterplan.Tools;
using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Data
{
	[Serializable]
	public class Project
	{
		private string fName = "";

		private string fAuthor = "";

		private Party fParty = new Party();

		private List<Hero> fHeroes = new List<Hero>();

		private List<Hero> fInactiveHeroes = new List<Hero>();

		private Plot fPlot = new Plot();

		private Encyclopedia fEncyclopedia = new Encyclopedia();

		private List<Note> fNotes = new List<Note>();

		private List<Map> fMaps = new List<Map>();

		private List<RegionalMap> fRegionalMaps = new List<RegionalMap>();

		private List<EncounterDeck> fDecks = new List<EncounterDeck>();

		private List<NPC> fNPCs = new List<NPC>();

		private List<CustomCreature> fCustomCreatures = new List<CustomCreature>();

		private List<Calendar> fCalendars = new List<Calendar>();

		private List<Attachment> fAttachments = new List<Attachment>();

		private List<Background> fBackgrounds = new List<Background>();

		private List<Parcel> fTreasureParcels = new List<Parcel>();

		private List<IPlayerOption> fPlayerOptions = new List<IPlayerOption>();

		private List<CombatState> fSavedCombats = new List<CombatState>();

		private Dictionary<string, string> fAddInData = new Dictionary<string, string>();

		private CampaignSettings fCampaignSettings = new CampaignSettings();

		private string fPassword = "";

		private string fPasswordHint = "";

		private Library fLibrary = new Library();

		public string Name
		{
			get
			{
				return this.fName;
			}
			set
			{
				this.fName = value;
			}
		}

		public string Author
		{
			get
			{
				return this.fAuthor;
			}
			set
			{
				this.fAuthor = value;
			}
		}

		public Party Party
		{
			get
			{
				return this.fParty;
			}
			set
			{
				this.fParty = value;
			}
		}

		public List<Hero> Heroes
		{
			get
			{
				return this.fHeroes;
			}
			set
			{
				this.fHeroes = value;
			}
		}

		public List<Hero> InactiveHeroes
		{
			get
			{
				return this.fInactiveHeroes;
			}
			set
			{
				this.fInactiveHeroes = value;
			}
		}

		public Plot Plot
		{
			get
			{
				return this.fPlot;
			}
			set
			{
				this.fPlot = value;
			}
		}

		public Encyclopedia Encyclopedia
		{
			get
			{
				return this.fEncyclopedia;
			}
			set
			{
				this.fEncyclopedia = value;
			}
		}

		public List<Note> Notes
		{
			get
			{
				return this.fNotes;
			}
			set
			{
				this.fNotes = value;
			}
		}

		public List<Map> Maps
		{
			get
			{
				return this.fMaps;
			}
			set
			{
				this.fMaps = value;
			}
		}

		public List<RegionalMap> RegionalMaps
		{
			get
			{
				return this.fRegionalMaps;
			}
			set
			{
				this.fRegionalMaps = value;
			}
		}

		public List<EncounterDeck> Decks
		{
			get
			{
				return this.fDecks;
			}
			set
			{
				this.fDecks = value;
			}
		}

		public List<NPC> NPCs
		{
			get
			{
				return this.fNPCs;
			}
			set
			{
				this.fNPCs = value;
			}
		}

		public List<CustomCreature> CustomCreatures
		{
			get
			{
				return this.fCustomCreatures;
			}
			set
			{
				this.fCustomCreatures = value;
			}
		}

		public List<Calendar> Calendars
		{
			get
			{
				return this.fCalendars;
			}
			set
			{
				this.fCalendars = value;
			}
		}

		public List<Attachment> Attachments
		{
			get
			{
				return this.fAttachments;
			}
			set
			{
				this.fAttachments = value;
			}
		}

		public List<Background> Backgrounds
		{
			get
			{
				return this.fBackgrounds;
			}
			set
			{
				this.fBackgrounds = value;
			}
		}

		public List<Parcel> TreasureParcels
		{
			get
			{
				return this.fTreasureParcels;
			}
			set
			{
				this.fTreasureParcels = value;
			}
		}

		public List<IPlayerOption> PlayerOptions
		{
			get
			{
				return this.fPlayerOptions;
			}
			set
			{
				this.fPlayerOptions = value;
			}
		}

		public List<CombatState> SavedCombats
		{
			get
			{
				return this.fSavedCombats;
			}
			set
			{
				this.fSavedCombats = value;
			}
		}

		public Dictionary<string, string> AddInData
		{
			get
			{
				return this.fAddInData;
			}
			set
			{
				this.fAddInData = value;
			}
		}

		public CampaignSettings CampaignSettings
		{
			get
			{
				return this.fCampaignSettings;
			}
			set
			{
				this.fCampaignSettings = value;
			}
		}

		public string Password
		{
			get
			{
				return this.fPassword;
			}
			set
			{
				this.fPassword = value;
			}
		}

		public string PasswordHint
		{
			get
			{
				return this.fPasswordHint;
			}
			set
			{
				this.fPasswordHint = value;
			}
		}

		public List<PlotPoint> AllPlotPoints
		{
			get
			{
				List<PlotPoint> list = new List<PlotPoint>();
				foreach (PlotPoint current in this.fPlot.Points)
				{
					list.AddRange(current.Subtree);
				}
				return list;
			}
		}

		public List<Parcel> AllTreasureParcels
		{
			get
			{
				List<Parcel> list = new List<Parcel>();
				list.AddRange(this.fTreasureParcels);
				List<PlotPoint> allPlotPoints = this.AllPlotPoints;
				foreach (PlotPoint current in allPlotPoints)
				{
					list.AddRange(current.Parcels);
				}
				return list;
			}
		}

		public Library Library
		{
			get
			{
				return this.fLibrary;
			}
			set
			{
				this.fLibrary = value;
			}
		}

		public Map FindTacticalMap(Guid map_id)
		{
			foreach (Map current in this.fMaps)
			{
				if (current.ID == map_id)
				{
					return current;
				}
			}
			return null;
		}

		public RegionalMap FindRegionalMap(Guid map_id)
		{
			foreach (RegionalMap current in this.fRegionalMaps)
			{
				if (current.ID == map_id)
				{
					return current;
				}
			}
			return null;
		}

		public EncounterDeck FindDeck(Guid deck_id)
		{
			foreach (EncounterDeck current in this.fDecks)
			{
				if (current.ID == deck_id)
				{
					return current;
				}
			}
			return null;
		}

		public NPC FindNPC(Guid npc_id)
		{
			foreach (NPC current in this.fNPCs)
			{
				if (current.ID == npc_id)
				{
					return current;
				}
			}
			return null;
		}

		public CustomCreature FindCustomCreature(Guid creature_id)
		{
			foreach (CustomCreature current in this.fCustomCreatures)
			{
				if (current.ID == creature_id)
				{
					return current;
				}
			}
			return null;
		}

		public CustomCreature FindCustomCreature(string creature_name)
		{
			foreach (CustomCreature current in this.fCustomCreatures)
			{
				if (current.Name == creature_name)
				{
					return current;
				}
			}
			return null;
		}

		public Calendar FindCalendar(Guid calendar_id)
		{
			foreach (Calendar current in this.fCalendars)
			{
				if (current.ID == calendar_id)
				{
					return current;
				}
			}
			return null;
		}

		public Note FindNote(Guid note_id)
		{
			foreach (Note current in this.fNotes)
			{
				if (current.ID == note_id)
				{
					return current;
				}
			}
			return null;
		}

		public Attachment FindAttachment(string name)
		{
			foreach (Attachment current in this.fAttachments)
			{
				if (current.Name == name)
				{
					return current;
				}
			}
			return null;
		}

		public Background FindBackground(string title)
		{
			foreach (Background current in this.fBackgrounds)
			{
				if (current.Title == title)
				{
					return current;
				}
			}
			return null;
		}

		public Hero FindHero(Guid hero_id)
		{
			foreach (Hero current in this.fHeroes)
			{
				if (current.ID == hero_id)
				{
					Hero result = current;
					return result;
				}
			}
			foreach (Hero current2 in this.fInactiveHeroes)
			{
				if (current2.ID == hero_id)
				{
					Hero result = current2;
					return result;
				}
			}
			return null;
		}

		public Hero FindHero(string hero_name)
		{
			foreach (Hero current in this.fHeroes)
			{
				if (current.Name == hero_name)
				{
					Hero result = current;
					return result;
				}
			}
			foreach (Hero current2 in this.fInactiveHeroes)
			{
				if (current2.Name == hero_name)
				{
					Hero result = current2;
					return result;
				}
			}
			return null;
		}

		public IPlayerOption FindPlayerOption(Guid option_id)
		{
			foreach (IPlayerOption current in this.fPlayerOptions)
			{
				if (current.ID == option_id)
				{
					return current;
				}
			}
			return null;
		}

		public PlotPoint FindParent(Plot p)
		{
			List<PlotPoint> list = new List<PlotPoint>();
			this.get_all_points(Session.Project.Plot, list);
			foreach (PlotPoint current in list)
			{
				if (current.Subplot == p)
				{
					return current;
				}
			}
			return null;
		}

		private void get_all_points(Plot p, List<PlotPoint> points)
		{
			List<PlotPoint> list = (p != null) ? p.Points : Session.Project.Plot.Points;
			foreach (PlotPoint current in list)
			{
				points.Add(current);
				this.get_all_points(current.Subplot, points);
			}
		}

		public Plot FindParent(PlotPoint pp)
		{
			List<Plot> list = new List<Plot>();
			this.get_all_plots(Session.Project.Plot, list);
			foreach (Plot current in list)
			{
				foreach (PlotPoint current2 in current.Points)
				{
					if (current2.ID == pp.ID)
					{
						return current;
					}
				}
			}
			return null;
		}

		private void get_all_plots(Plot p, List<Plot> plots)
		{
			plots.Add(p);
			List<PlotPoint> list = (p != null) ? p.Points : Session.Project.Plot.Points;
			foreach (PlotPoint current in list)
			{
				this.get_all_plots(current.Subplot, plots);
			}
		}

		public void Update()
		{
			this.fLibrary.Update();
			if (this.fPassword == null)
			{
				this.fPassword = "";
			}
			if (this.fPasswordHint == null)
			{
				this.fPasswordHint = "";
			}
			if (this.fParty.XP == 0)
			{
				this.fParty.XP = Experience.GetHeroXP(this.fParty.Level);
			}
			if (this.fAuthor == null)
			{
				this.fAuthor = "";
			}
			if (this.fRegionalMaps == null)
			{
				this.fRegionalMaps = new List<RegionalMap>();
			}
			foreach (RegionalMap current in this.fRegionalMaps)
			{
				foreach (MapLocation current2 in current.Locations)
				{
					if (current2.Category == null)
					{
						current2.Category = "";
					}
				}
				Program.SetResolution(current.Image);
			}
			foreach (Hero current3 in this.fHeroes)
			{
				if (current3.Key == null)
				{
					current3.Key = "";
				}
				if (current3.Level == 0)
				{
					current3.Level = this.fParty.Level;
				}
				if (current3.Effects == null)
				{
					current3.Effects = new List<OngoingCondition>();
				}
				foreach (OngoingCondition current4 in current3.Effects)
				{
					if (current4.Defences == null)
					{
						current4.Defences = new List<DefenceType>();
					}
					if (current4.DamageModifier == null)
					{
						current4.DamageModifier = new DamageModifier();
					}
					if (current4.Regeneration == null)
					{
						current4.Regeneration = new Regeneration();
					}
					if (current4.Aura == null)
					{
						current4.Aura = new Aura();
					}
				}
				if (current3.Tokens == null)
				{
					current3.Tokens = new List<CustomToken>();
				}
				foreach (CustomToken current5 in current3.Tokens)
				{
					if (current5.TerrainPower != null && current5.TerrainPower.ID == Guid.Empty)
					{
						current5.ID = Guid.NewGuid();
					}
				}
				if (current3.Portrait != null)
				{
					Program.SetResolution(current3.Portrait);
				}
				if (current3.CombatData == null)
				{
					current3.CombatData = new CombatData();
				}
			}
			if (this.fInactiveHeroes == null)
			{
				this.fInactiveHeroes = new List<Hero>();
			}
			foreach (Hero current6 in this.fInactiveHeroes)
			{
				if (current6.Effects == null)
				{
					current6.Effects = new List<OngoingCondition>();
				}
				foreach (OngoingCondition current7 in current6.Effects)
				{
					if (current7.Defences == null)
					{
						current7.Defences = new List<DefenceType>();
					}
					if (current7.DamageModifier == null)
					{
						current7.DamageModifier = new DamageModifier();
					}
					if (current7.Regeneration == null)
					{
						current7.Regeneration = new Regeneration();
					}
				}
				if (current6.Tokens == null)
				{
					current6.Tokens = new List<CustomToken>();
				}
				foreach (CustomToken current8 in current6.Tokens)
				{
					if (current8.TerrainPower != null && current8.TerrainPower.ID == Guid.Empty)
					{
						current8.ID = Guid.NewGuid();
					}
				}
				if (current6.Portrait != null)
				{
					Program.SetResolution(current6.Portrait);
				}
				if (current6.CombatData == null)
				{
					current6.CombatData = new CombatData();
				}
			}
			if (this.fNPCs == null)
			{
				this.fNPCs = new List<NPC>();
			}
			while (this.fNPCs.Contains(null))
			{
				this.fNPCs.Remove(null);
			}
			using (List<NPC>.Enumerator enumerator9 = this.fNPCs.GetEnumerator())
			{
				while (enumerator9.MoveNext())
				{
					NPC current9 = enumerator9.Current;
					if (current9 != null)
					{
						if (current9.Auras == null)
						{
							current9.Auras = new List<Aura>();
						}
						foreach (Aura current10 in current9.Auras)
						{
							if (current10.Keywords == null)
							{
								current10.Keywords = "";
							}
						}
						if (current9.CreaturePowers == null)
						{
							current9.CreaturePowers = new List<CreaturePower>();
						}
						CreatureHelper.UpdateRegen(current9);
						foreach (CreaturePower current11 in current9.CreaturePowers)
						{
							CreatureHelper.UpdatePowerRange(current9, current11);
						}
						if (current9.Tactics == null)
						{
							current9.Tactics = "";
						}
						if (current9.Image != null)
						{
							Program.SetResolution(current9.Image);
						}
					}
				}
				goto IL_54B;
			}
			IL_53E:
			this.fCustomCreatures.Remove(null);
			IL_54B:
			if (!this.fCustomCreatures.Contains(null))
			{
				foreach (CustomCreature current12 in this.fCustomCreatures)
				{
					if (current12 != null)
					{
						if (current12.Auras == null)
						{
							current12.Auras = new List<Aura>();
						}
						foreach (Aura current13 in current12.Auras)
						{
							if (current13.Keywords == null)
							{
								current13.Keywords = "";
							}
						}
						if (current12.CreaturePowers == null)
						{
							current12.CreaturePowers = new List<CreaturePower>();
						}
						if (current12.DamageModifiers == null)
						{
							current12.DamageModifiers = new List<DamageModifier>();
						}
						CreatureHelper.UpdateRegen(current12);
						foreach (CreaturePower current14 in current12.CreaturePowers)
						{
							CreatureHelper.UpdatePowerRange(current12, current14);
						}
						if (current12.Tactics == null)
						{
							current12.Tactics = "";
						}
						if (current12.Image != null)
						{
							Program.SetResolution(current12.Image);
						}
					}
				}
				if (this.fCalendars == null)
				{
					this.fCalendars = new List<Calendar>();
				}
				if (this.fEncyclopedia == null)
				{
					this.fEncyclopedia = new Encyclopedia();
				}
				while (this.fEncyclopedia.Entries.Contains(null))
				{
					this.fEncyclopedia.Entries.Remove(null);
				}
				foreach (EncyclopediaEntry current15 in this.fEncyclopedia.Entries)
				{
					if (current15.Category == null)
					{
						current15.Category = "";
					}
					if (current15.DMInfo == null)
					{
						current15.DMInfo = "";
					}
					if (current15.Images == null)
					{
						current15.Images = new List<EncyclopediaImage>();
					}
					foreach (EncyclopediaImage current16 in current15.Images)
					{
						Program.SetResolution(current16.Image);
					}
				}
				if (this.fEncyclopedia.Groups == null)
				{
					this.fEncyclopedia.Groups = new List<EncyclopediaGroup>();
				}
				if (this.fNotes == null)
				{
					this.fNotes = new List<Note>();
				}
				foreach (Note current17 in this.fNotes)
				{
					if (current17.Category == null)
					{
						current17.Category = "";
					}
				}
				if (this.fAttachments == null)
				{
					this.fAttachments = new List<Attachment>();
				}
				if (this.fBackgrounds == null)
				{
					this.fBackgrounds = new List<Background>();
					this.SetStandardBackgroundItems();
				}
				if (this.fTreasureParcels == null)
				{
					this.fTreasureParcels = new List<Parcel>();
				}
				if (this.fPlayerOptions == null)
				{
					this.fPlayerOptions = new List<IPlayerOption>();
				}
				if (this.fSavedCombats == null)
				{
					this.fSavedCombats = new List<CombatState>();
				}
				foreach (CombatState current18 in this.fSavedCombats)
				{
					if (current18.Encounter.Waves == null)
					{
						current18.Encounter.Waves = new List<EncounterWave>();
					}
					if (current18.Sketches == null)
					{
						current18.Sketches = new List<MapSketch>();
					}
					if (current18.Log == null)
					{
						current18.Log = new EncounterLog();
					}
					foreach (OngoingCondition current19 in current18.QuickEffects)
					{
						if (current19.Defences == null)
						{
							current19.Defences = new List<DefenceType>();
						}
						if (current19.DamageModifier == null)
						{
							current19.DamageModifier = new DamageModifier();
						}
						if (current19.Regeneration == null)
						{
							current19.Regeneration = new Regeneration();
						}
						if (current19.Aura == null)
						{
							current19.Aura = new Aura();
						}
					}
				}
				if (this.fAddInData == null)
				{
					this.fAddInData = new Dictionary<string, string>();
				}
				if (this.fCampaignSettings == null)
				{
					this.fCampaignSettings = new CampaignSettings();
				}
				if (this.fCampaignSettings.XP == 0.0)
				{
					this.fCampaignSettings.XP = 1.0;
				}
				this.update_plot(this.fPlot);
				return;
			}
			goto IL_53E;
		}

		private void update_plot(Plot p)
		{
			if (p.Goals == null)
			{
				p.Goals = new PartyGoals();
			}
			if (p.FiveByFive == null)
			{
				p.FiveByFive = new FiveByFiveData();
			}
			foreach (PlotPoint current in p.Points)
			{
				if (current.ReadAloud == null)
				{
					current.ReadAloud = "";
				}
				if (current.Parcels == null)
				{
					current.Parcels = new List<Parcel>();
				}
				if (current.EncyclopediaEntryIDs == null)
				{
					current.EncyclopediaEntryIDs = new List<Guid>();
				}
				if (current.Element is Encounter)
				{
					Encounter encounter = current.Element as Encounter;
					if (encounter.Traps == null)
					{
						encounter.Traps = new List<Trap>();
					}
					foreach (Trap current2 in encounter.Traps)
					{
						if (current2.Description == null)
						{
							current2.Description = "";
						}
						if (current2.Attacks == null)
						{
							current2.Attacks = new List<TrapAttack>();
						}
						if (current2.Attack != null)
						{
							current2.Attacks.Add(current2.Attack);
							current2.Initiative = (current2.Attack.HasInitiative ? current2.Attack.Initiative : -2147483648);
							current2.Trigger = current2.Attack.Trigger;
							current2.Attack = null;
						}
						foreach (TrapAttack current3 in current2.Attacks)
						{
							if (current3.ID == Guid.Empty)
							{
								current3.ID = Guid.NewGuid();
							}
							if (current3.Name == null)
							{
								current3.Name = "Attack";
							}
							if (current3.Keywords == null)
							{
								current3.Keywords = "";
							}
							if (current3.Notes == null)
							{
								current3.Notes = "";
							}
						}
						if (current2.Trigger == null)
						{
							current2.Trigger = "";
						}
						foreach (TrapSkillData current4 in current2.Skills)
						{
							if (current4.ID == Guid.Empty)
							{
								current4.ID = Guid.NewGuid();
							}
						}
					}
					if (encounter.SkillChallenges == null)
					{
						encounter.SkillChallenges = new List<SkillChallenge>();
					}
					foreach (SkillChallenge current5 in encounter.SkillChallenges)
					{
						if (current5.Notes == null)
						{
							current5.Notes = "";
						}
						foreach (SkillChallengeData current6 in current5.Skills)
						{
							if (current6.Results == null)
							{
								current6.Results = new SkillChallengeResult();
							}
						}
					}
					if (encounter.CustomTokens == null)
					{
						encounter.CustomTokens = new List<CustomToken>();
					}
					foreach (CustomToken current7 in encounter.CustomTokens)
					{
						if (current7.TerrainPower != null && current7.TerrainPower.ID == Guid.Empty)
						{
							current7.TerrainPower.ID = Guid.NewGuid();
						}
					}
					if (encounter.Notes == null)
					{
						encounter.Notes = new List<EncounterNote>();
						encounter.SetStandardEncounterNotes();
					}
					if (encounter.Waves == null)
					{
						encounter.Waves = new List<EncounterWave>();
					}
					foreach (EncounterSlot current8 in encounter.AllSlots)
					{
						current8.SetDefaultDisplayNames();
						foreach (CombatData current9 in current8.CombatData)
						{
							current9.Initiative = -2147483648;
							if (current9.ID == Guid.Empty)
							{
								current9.ID = Guid.NewGuid();
							}
							if (current9.UsedPowers == null)
							{
								current9.UsedPowers = new List<Guid>();
							}
						}
					}
				}
				if (current.Element is SkillChallenge)
				{
					SkillChallenge skillChallenge = current.Element as SkillChallenge;
					if (skillChallenge.ID == Guid.Empty)
					{
						skillChallenge.ID = Guid.NewGuid();
					}
					if (skillChallenge.Name == null)
					{
						skillChallenge.Name = "Skill Challenge";
					}
					if (skillChallenge.Level <= 0)
					{
						skillChallenge.Level = this.fParty.Level;
					}
					if (skillChallenge.Notes == null)
					{
						skillChallenge.Notes = "";
					}
					foreach (SkillChallengeData current10 in skillChallenge.Skills)
					{
						if (current10.Difficulty == Difficulty.Random)
						{
							current10.Difficulty = Difficulty.Moderate;
						}
						if (current10.Results == null)
						{
							current10.Results = new SkillChallengeResult();
						}
					}
				}
				if (current.Element is TrapElement)
				{
					TrapElement trapElement = current.Element as TrapElement;
					if (trapElement.Trap.Description == null)
					{
						trapElement.Trap.Description = "";
					}
					if (trapElement.Trap.Attacks == null)
					{
						trapElement.Trap.Attacks = new List<TrapAttack>();
					}
					if (trapElement.Trap.Attack != null)
					{
						trapElement.Trap.Attacks.Add(trapElement.Trap.Attack);
						trapElement.Trap.Initiative = (trapElement.Trap.Attack.HasInitiative ? trapElement.Trap.Attack.Initiative : -2147483648);
						trapElement.Trap.Trigger = trapElement.Trap.Attack.Trigger;
						trapElement.Trap.Attack = null;
					}
					foreach (TrapAttack current11 in trapElement.Trap.Attacks)
					{
						if (current11.ID == Guid.Empty)
						{
							current11.ID = Guid.NewGuid();
						}
						if (current11.Name == null)
						{
							current11.Name = "Attack";
						}
						if (current11.Keywords == null)
						{
							current11.Keywords = "";
						}
						if (current11.Notes == null)
						{
							current11.Notes = "";
						}
					}
					if (trapElement.Trap.Trigger == null)
					{
						trapElement.Trap.Trigger = "";
					}
					foreach (TrapSkillData current12 in trapElement.Trap.Skills)
					{
						if (current12.ID == Guid.Empty)
						{
							current12.ID = Guid.NewGuid();
						}
					}
				}
				if (current.Element is Quest)
				{
					Quest quest = current.Element as Quest;
					if (quest.Type == QuestType.Minor && quest.XP == 0)
					{
						Pair<int, int> minorQuestXP = Experience.GetMinorQuestXP(quest.Level);
						quest.XP = minorQuestXP.First;
					}
				}
				this.update_plot(current.Subplot);
			}
		}

		public void Import(Project p)
		{
			p.Update();
			PlotPoint plotPoint = new PlotPoint(p.Name);
			plotPoint.Subplot = p.Plot;
			this.fPlot.Points.Add(plotPoint);
			this.fEncyclopedia.Import(p.Encyclopedia);
			this.fNotes.AddRange(p.Notes);
			this.fMaps.AddRange(p.Maps);
			this.fRegionalMaps.AddRange(p.RegionalMaps);
			this.fDecks.AddRange(p.Decks);
			this.fNPCs.AddRange(p.NPCs);
			this.fCustomCreatures.AddRange(p.CustomCreatures);
			this.fCalendars.AddRange(p.Calendars);
			this.fAttachments.AddRange(p.Attachments);
			this.fPlayerOptions.AddRange(p.PlayerOptions);
			foreach (Background current in p.Backgrounds)
			{
				if (!(current.Details == ""))
				{
					Background background = this.FindBackground(current.Title);
					if (background == null)
					{
						this.fBackgrounds.AddRange(p.Backgrounds);
					}
					else
					{
						if (background.Details != "")
						{
							Background expr_137 = background;
							expr_137.Details += Environment.NewLine;
						}
						Background expr_14D = background;
						expr_14D.Details += current.Details;
					}
				}
			}
			this.PopulateProjectLibrary();
			this.fLibrary.Import(p.Library);
			this.SimplifyProjectLibrary();
		}

		public void SimplifyProjectLibrary()
		{
			List<Creature> list = new List<Creature>();
			foreach (Creature current in this.fLibrary.Creatures)
			{
				if (current != null)
				{
					ICreature creature = Session.FindCreature(current.ID, SearchType.External);
					if (creature != null && creature is Creature)
					{
						list.Add(creature as Creature);
					}
				}
			}
			foreach (Creature current2 in list)
			{
				this.fLibrary.Creatures.Remove(current2);
			}
			List<CreatureTemplate> list2 = new List<CreatureTemplate>();
			foreach (CreatureTemplate current3 in this.fLibrary.Templates)
			{
				if (current3 != null)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current3.ID, SearchType.External);
					if (creatureTemplate != null)
					{
						list2.Add(current3);
					}
				}
			}
			foreach (CreatureTemplate current4 in list2)
			{
				this.fLibrary.Templates.Remove(current4);
			}
			List<MonsterTheme> list3 = new List<MonsterTheme>();
			foreach (MonsterTheme current5 in this.fLibrary.Themes)
			{
				if (current5 != null)
				{
					MonsterTheme monsterTheme = Session.FindTheme(current5.ID, SearchType.External);
					if (monsterTheme != null)
					{
						list3.Add(current5);
					}
				}
			}
			foreach (MonsterTheme current6 in list3)
			{
				this.fLibrary.Themes.Remove(current6);
			}
			List<Trap> list4 = new List<Trap>();
			foreach (Trap current7 in this.fLibrary.Traps)
			{
				if (current7 != null)
				{
					Trap trap = Session.FindTrap(current7.ID, SearchType.External);
					if (trap != null)
					{
						list4.Add(current7);
					}
				}
			}
			foreach (Trap current8 in list4)
			{
				this.fLibrary.Traps.Remove(current8);
			}
			List<SkillChallenge> list5 = new List<SkillChallenge>();
			foreach (SkillChallenge current9 in this.fLibrary.SkillChallenges)
			{
				if (current9 != null)
				{
					SkillChallenge skillChallenge = Session.FindSkillChallenge(current9.ID, SearchType.External);
					if (skillChallenge != null)
					{
						list5.Add(current9);
					}
				}
			}
			foreach (SkillChallenge current10 in list5)
			{
				this.fLibrary.SkillChallenges.Remove(current10);
			}
			List<MagicItem> list6 = new List<MagicItem>();
			foreach (MagicItem current11 in this.fLibrary.MagicItems)
			{
				if (current11 != null)
				{
					MagicItem magicItem = Session.FindMagicItem(current11.ID, SearchType.External);
					if (magicItem != null)
					{
						list6.Add(current11);
					}
				}
			}
			foreach (MagicItem current12 in list6)
			{
				this.fLibrary.MagicItems.Remove(current12);
			}
			List<Tile> list7 = new List<Tile>();
			foreach (Tile current13 in this.fLibrary.Tiles)
			{
				if (current13 != null)
				{
					Tile tile = Session.FindTile(current13.ID, SearchType.External);
					if (tile != null)
					{
						list7.Add(current13);
					}
				}
			}
			foreach (Tile current14 in list7)
			{
				this.fLibrary.Tiles.Remove(current14);
			}
		}

		public void PopulateProjectLibrary()
		{
			List<Guid> creature_ids = new List<Guid>();
			List<Guid> template_ids = new List<Guid>();
			List<Guid> theme_ids = new List<Guid>();
			List<Guid> trap_ids = new List<Guid>();
			List<Guid> challenge_ids = new List<Guid>();
			List<Guid> magic_item_ids = new List<Guid>();
			foreach (PlotPoint current in this.fPlot.Points)
			{
				this.add_data(current, creature_ids, template_ids, theme_ids, trap_ids, challenge_ids, magic_item_ids);
			}
			foreach (EncounterDeck current2 in this.fDecks)
			{
				this.add_data(current2, creature_ids, template_ids, theme_ids);
			}
			foreach (NPC current3 in this.fNPCs)
			{
				this.add_data(current3, template_ids);
			}
			this.populate_creatures(creature_ids);
			this.populate_templates(template_ids);
			this.populate_themes(theme_ids);
			this.populate_traps(trap_ids);
			this.populate_challenges(challenge_ids);
			this.populate_magic_items(magic_item_ids);
			this.populate_tiles();
		}

		private void add_data(PlotPoint pp, List<Guid> creature_ids, List<Guid> template_ids, List<Guid> theme_ids, List<Guid> trap_ids, List<Guid> challenge_ids, List<Guid> magic_item_ids)
		{
			if (pp.Element is Encounter)
			{
				Encounter encounter = pp.Element as Encounter;
				foreach (EncounterSlot current in encounter.Slots)
				{
					this.add_data(current.Card, creature_ids, template_ids, theme_ids);
				}
				foreach (Trap current2 in encounter.Traps)
				{
					if (!trap_ids.Contains(current2.ID))
					{
						trap_ids.Add(current2.ID);
					}
				}
				foreach (SkillChallenge current3 in encounter.SkillChallenges)
				{
					if (!challenge_ids.Contains(current3.ID))
					{
						challenge_ids.Add(current3.ID);
					}
				}
			}
			if (pp.Element is SkillChallenge)
			{
				SkillChallenge skillChallenge = pp.Element as SkillChallenge;
				if (!challenge_ids.Contains(skillChallenge.ID))
				{
					challenge_ids.Add(skillChallenge.ID);
				}
			}
			if (pp.Element is Trap)
			{
				Trap trap = pp.Element as Trap;
				if (!trap_ids.Contains(trap.ID))
				{
					trap_ids.Add(trap.ID);
				}
			}
			foreach (Parcel current4 in pp.Parcels)
			{
				if (current4.MagicItemID != Guid.Empty && !magic_item_ids.Contains(current4.MagicItemID))
				{
					magic_item_ids.Add(current4.MagicItemID);
				}
			}
			foreach (PlotPoint current5 in pp.Subplot.Points)
			{
				this.add_data(current5, creature_ids, template_ids, theme_ids, trap_ids, challenge_ids, magic_item_ids);
			}
		}

		private void add_data(EncounterDeck deck, List<Guid> creature_ids, List<Guid> template_ids, List<Guid> theme_ids)
		{
			foreach (EncounterCard current in deck.Cards)
			{
				this.add_data(current, creature_ids, template_ids, theme_ids);
			}
		}

		private void add_data(EncounterCard card, List<Guid> creature_ids, List<Guid> template_ids, List<Guid> theme_ids)
		{
			if (!creature_ids.Contains(card.CreatureID))
			{
				creature_ids.Add(card.CreatureID);
			}
			foreach (Guid current in card.TemplateIDs)
			{
				if (!template_ids.Contains(current))
				{
					template_ids.Add(current);
				}
			}
			if (card.ThemeID != Guid.Empty && !theme_ids.Contains(card.ThemeID))
			{
				theme_ids.Add(card.ThemeID);
			}
		}

		private void add_data(NPC npc, List<Guid> template_ids)
		{
			if (!template_ids.Contains(npc.TemplateID))
			{
				template_ids.Add(npc.TemplateID);
			}
		}

		private void populate_creatures(List<Guid> creature_ids)
		{
			List<Creature> list = new List<Creature>();
			foreach (Creature current in this.fLibrary.Creatures)
			{
				if (current == null || !creature_ids.Contains(current.ID))
				{
					list.Add(current);
				}
			}
			foreach (Creature current2 in list)
			{
				this.fLibrary.Creatures.Remove(current2);
			}
			foreach (Guid current3 in creature_ids)
			{
				if (this.fLibrary.FindCreature(current3) == null)
				{
					ICreature creature = Session.FindCreature(current3, SearchType.Global);
					if (creature != null)
					{
						this.fLibrary.Creatures.Add(creature as Creature);
					}
				}
			}
		}

		private void populate_templates(List<Guid> template_ids)
		{
			List<CreatureTemplate> list = new List<CreatureTemplate>();
			foreach (CreatureTemplate current in this.fLibrary.Templates)
			{
				if (current == null || !template_ids.Contains(current.ID))
				{
					list.Add(current);
				}
			}
			foreach (CreatureTemplate current2 in list)
			{
				this.fLibrary.Templates.Remove(current2);
			}
			foreach (Guid current3 in template_ids)
			{
				if (this.fLibrary.FindTemplate(current3) == null)
				{
					CreatureTemplate creatureTemplate = Session.FindTemplate(current3, SearchType.Global);
					if (creatureTemplate != null)
					{
						this.fLibrary.Templates.Add(creatureTemplate);
					}
				}
			}
		}

		private void populate_themes(List<Guid> theme_ids)
		{
			List<MonsterTheme> list = new List<MonsterTheme>();
			foreach (MonsterTheme current in this.fLibrary.Themes)
			{
				if (current == null || !theme_ids.Contains(current.ID))
				{
					list.Add(current);
				}
			}
			foreach (MonsterTheme current2 in list)
			{
				this.fLibrary.Themes.Remove(current2);
			}
			foreach (Guid current3 in theme_ids)
			{
				if (this.fLibrary.FindTheme(current3) == null)
				{
					MonsterTheme monsterTheme = Session.FindTheme(current3, SearchType.Global);
					if (monsterTheme != null)
					{
						this.fLibrary.Themes.Add(monsterTheme);
					}
				}
			}
		}

		private void populate_traps(List<Guid> trap_ids)
		{
			List<Trap> list = new List<Trap>();
			foreach (Trap current in this.fLibrary.Traps)
			{
				if (current == null || !trap_ids.Contains(current.ID))
				{
					list.Add(current);
				}
			}
			foreach (Trap current2 in list)
			{
				this.fLibrary.Traps.Remove(current2);
			}
			foreach (Guid current3 in trap_ids)
			{
				if (this.fLibrary.FindTrap(current3) == null)
				{
					Trap trap = Session.FindTrap(current3, SearchType.Global);
					if (trap != null)
					{
						this.fLibrary.Traps.Add(trap);
					}
				}
			}
		}

		private void populate_challenges(List<Guid> challenge_ids)
		{
			List<SkillChallenge> list = new List<SkillChallenge>();
			foreach (SkillChallenge current in this.fLibrary.SkillChallenges)
			{
				if (current == null || !challenge_ids.Contains(current.ID))
				{
					list.Add(current);
				}
			}
			foreach (SkillChallenge current2 in list)
			{
				this.fLibrary.SkillChallenges.Remove(current2);
			}
			foreach (Guid current3 in challenge_ids)
			{
				if (this.fLibrary.FindSkillChallenge(current3) == null)
				{
					SkillChallenge skillChallenge = Session.FindSkillChallenge(current3, SearchType.Global);
					if (skillChallenge != null)
					{
						this.fLibrary.SkillChallenges.Add(skillChallenge);
					}
				}
			}
		}

		private void populate_magic_items(List<Guid> magic_item_ids)
		{
			List<MagicItem> list = new List<MagicItem>();
			foreach (MagicItem current in this.fLibrary.MagicItems)
			{
				if (current == null || !magic_item_ids.Contains(current.ID))
				{
					list.Add(current);
				}
			}
			foreach (MagicItem current2 in list)
			{
				this.fLibrary.MagicItems.Remove(current2);
			}
			foreach (Guid current3 in magic_item_ids)
			{
				if (this.fLibrary.FindMagicItem(current3) == null)
				{
					MagicItem magicItem = Session.FindMagicItem(current3, SearchType.Global);
					if (magicItem != null)
					{
						this.fLibrary.MagicItems.Add(magicItem);
					}
				}
			}
		}

		private void populate_tiles()
		{
			List<Guid> list = new List<Guid>();
			foreach (Map current in this.fMaps)
			{
				foreach (TileData current2 in current.Tiles)
				{
					if (!list.Contains(current2.TileID))
					{
						list.Add(current2.TileID);
					}
				}
			}
			List<Tile> list2 = new List<Tile>();
			foreach (Tile current3 in this.fLibrary.Tiles)
			{
				if (current3 == null || !list.Contains(current3.ID))
				{
					list2.Add(current3);
				}
			}
			foreach (Tile current4 in list2)
			{
				this.fLibrary.Tiles.Remove(current4);
			}
			foreach (Guid current5 in list)
			{
				if (Session.FindTile(current5, SearchType.Project) == null)
				{
					Tile tile = Session.FindTile(current5, SearchType.External);
					if (tile != null)
					{
						this.fLibrary.Tiles.Add(tile);
					}
				}
			}
		}

		public void SetStandardBackgroundItems()
		{
			this.fBackgrounds.Add(new Background("Introduction"));
			this.fBackgrounds.Add(new Background("Adventure Synopsis"));
			this.fBackgrounds.Add(new Background("Adventure Background"));
			this.fBackgrounds.Add(new Background("DM Information"));
			this.fBackgrounds.Add(new Background("Player Introduction"));
			this.fBackgrounds.Add(new Background("Character Hooks"));
			this.fBackgrounds.Add(new Background("Treasure Preparation"));
			this.fBackgrounds.Add(new Background("Continuing the Story"));
		}
	}
}
