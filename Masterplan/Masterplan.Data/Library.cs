using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Library : IComparable<Library>
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fSecurityData = Program.SecurityData;

		private List<Creature> fCreatures = new List<Creature>();

		private List<CreatureTemplate> fTemplates = new List<CreatureTemplate>();

		private List<MonsterTheme> fThemes = new List<MonsterTheme>();

		private List<Trap> fTraps = new List<Trap>();

		private List<SkillChallenge> fSkillChallenges = new List<SkillChallenge>();

		private List<MagicItem> fMagicItems = new List<MagicItem>();

		private List<Artifact> fArtifacts = new List<Artifact>();

		private List<Tile> fTiles = new List<Tile>();

		private List<TerrainPower> fTerrainPowers = new List<TerrainPower>();

		public Guid ID
		{
			get
			{
				return this.fID;
			}
			set
			{
				this.fID = value;
			}
		}

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

		internal string SecurityData
		{
			get
			{
				return this.fSecurityData;
			}
			set
			{
				this.fSecurityData = value;
			}
		}

		public List<Creature> Creatures
		{
			get
			{
				return this.fCreatures;
			}
			set
			{
				this.fCreatures = value;
			}
		}

		public List<CreatureTemplate> Templates
		{
			get
			{
				return this.fTemplates;
			}
			set
			{
				this.fTemplates = value;
			}
		}

		public List<MonsterTheme> Themes
		{
			get
			{
				return this.fThemes;
			}
			set
			{
				this.fThemes = value;
			}
		}

		public List<Trap> Traps
		{
			get
			{
				return this.fTraps;
			}
			set
			{
				this.fTraps = value;
			}
		}

		public List<SkillChallenge> SkillChallenges
		{
			get
			{
				return this.fSkillChallenges;
			}
			set
			{
				this.fSkillChallenges = value;
			}
		}

		public List<MagicItem> MagicItems
		{
			get
			{
				return this.fMagicItems;
			}
			set
			{
				this.fMagicItems = value;
			}
		}

		public List<Artifact> Artifacts
		{
			get
			{
				return this.fArtifacts;
			}
			set
			{
				this.fArtifacts = value;
			}
		}

		public List<Tile> Tiles
		{
			get
			{
				return this.fTiles;
			}
			set
			{
				this.fTiles = value;
			}
		}

		public List<TerrainPower> TerrainPowers
		{
			get
			{
				return this.fTerrainPowers;
			}
			set
			{
				this.fTerrainPowers = value;
			}
		}

		public bool ShowInAutoBuild
		{
			get
			{
				foreach (Tile current in this.fTiles)
				{
					if (current != null && current.Category != TileCategory.Special && current.Category != TileCategory.Map)
					{
						return true;
					}
				}
				return false;
			}
		}

		public Creature FindCreature(Guid creature_id)
		{
			foreach (Creature current in this.fCreatures)
			{
				if (current != null && current.ID == creature_id)
				{
					return current;
				}
			}
			return null;
		}

		public Creature FindCreature(string creature_name)
		{
			foreach (Creature current in this.fCreatures)
			{
				if (current != null && current.Name == creature_name)
				{
					return current;
				}
			}
			return null;
		}

		public Creature FindCreature(string creature_name, int level)
		{
			foreach (Creature current in this.fCreatures)
			{
				if (current != null && current.Name == creature_name && current.Level == level)
				{
					return current;
				}
			}
			return null;
		}

		public CreatureTemplate FindTemplate(Guid template_id)
		{
			foreach (CreatureTemplate current in this.fTemplates)
			{
				if (current != null && current.ID == template_id)
				{
					return current;
				}
			}
			return null;
		}

		public MonsterTheme FindTheme(Guid theme_id)
		{
			foreach (MonsterTheme current in this.fThemes)
			{
				if (current != null && current.ID == theme_id)
				{
					return current;
				}
			}
			return null;
		}

		public Trap FindTrap(Guid trap_id)
		{
			foreach (Trap current in this.fTraps)
			{
				if (current != null && current.ID == trap_id)
				{
					return current;
				}
			}
			return null;
		}

		public Trap FindTrap(string trap_name)
		{
			foreach (Trap current in this.fTraps)
			{
				if (current != null && current.Name == trap_name)
				{
					return current;
				}
			}
			return null;
		}

		public Trap FindTrap(string trap_name, int level, string role_string)
		{
			foreach (Trap current in this.fTraps)
			{
				if (current != null && current.Name == trap_name && current.Level == level && current.Role.ToString() == role_string)
				{
					return current;
				}
			}
			return null;
		}

		public SkillChallenge FindSkillChallenge(Guid sc_id)
		{
			foreach (SkillChallenge current in this.fSkillChallenges)
			{
				if (current != null && current.ID == sc_id)
				{
					return current;
				}
			}
			return null;
		}

		public MagicItem FindMagicItem(Guid item_id)
		{
			foreach (MagicItem current in this.fMagicItems)
			{
				if (current != null && current.ID == item_id)
				{
					return current;
				}
			}
			return null;
		}

		public MagicItem FindMagicItem(string item_name)
		{
			foreach (MagicItem current in this.fMagicItems)
			{
				if (current != null && current.Name == item_name)
				{
					return current;
				}
			}
			return null;
		}

		public MagicItem FindMagicItem(string item_name, int level)
		{
			foreach (MagicItem current in this.fMagicItems)
			{
				if (current != null && current.Name == item_name && current.Level == level)
				{
					return current;
				}
			}
			return null;
		}

		public Artifact FindArtifact(Guid item_id)
		{
			foreach (Artifact current in this.fArtifacts)
			{
				if (current != null && current.ID == item_id)
				{
					return current;
				}
			}
			return null;
		}

		public Artifact FindArtifact(string item_name)
		{
			foreach (Artifact current in this.fArtifacts)
			{
				if (current != null && current.Name == item_name)
				{
					return current;
				}
			}
			return null;
		}

		public Tile FindTile(Guid tile_id)
		{
			foreach (Tile current in this.fTiles)
			{
				if (current != null && current.ID == tile_id)
				{
					return current;
				}
			}
			return null;
		}

		public TerrainPower FindTerrainPower(Guid item_id)
		{
			foreach (TerrainPower current in this.fTerrainPowers)
			{
				if (current != null && current.ID == item_id)
				{
					return current;
				}
			}
			return null;
		}

		public TerrainPower FindTerrainPower(string item_name)
		{
			foreach (TerrainPower current in this.fTerrainPowers)
			{
				if (current != null && current.Name == item_name)
				{
					return current;
				}
			}
			return null;
		}

		public int CompareTo(Library rhs)
		{
			return this.fName.CompareTo(rhs.Name);
		}

		public override string ToString()
		{
			return this.fName;
		}

		public void Update()
		{
			if (this.fID == Guid.Empty)
			{
				this.fID = Guid.NewGuid();
			}
			foreach (Creature current in this.fCreatures)
			{
				if (current != null)
				{
					if (current.Category == null)
					{
						current.Category = "";
					}
					if (current.Senses == null)
					{
						current.Senses = "";
					}
					if (current.Movement == null)
					{
						current.Movement = "";
					}
					if (current.Auras == null)
					{
						current.Auras = new List<Aura>();
					}
					foreach (Aura current2 in current.Auras)
					{
						if (current2.Keywords == null)
						{
							current2.Keywords = "";
						}
					}
					if (current.CreaturePowers == null)
					{
						current.CreaturePowers = new List<CreaturePower>();
					}
					if (current.DamageModifiers == null)
					{
						current.DamageModifiers = new List<DamageModifier>();
					}
					foreach (CreaturePower current3 in current.CreaturePowers)
					{
						if (current3.Condition == null)
						{
							current3.Condition = "";
						}
						current3.ExtractAttackDetails();
					}
					CreatureHelper.UpdateRegen(current);
					foreach (CreaturePower current4 in current.CreaturePowers)
					{
						CreatureHelper.UpdatePowerRange(current, current4);
					}
					if (current.Tactics == null)
					{
						current.Tactics = "";
					}
					if (current.URL == null)
					{
						current.URL = "";
					}
					if (current.Image != null)
					{
						Program.SetResolution(current.Image);
					}
				}
			}
			foreach (CreatureTemplate current5 in this.fTemplates)
			{
				if (current5 != null)
				{
					if (current5.Senses == null)
					{
						current5.Senses = "";
					}
					if (current5.Movement == null)
					{
						current5.Movement = "";
					}
					if (current5.Auras == null)
					{
						current5.Auras = new List<Aura>();
					}
					foreach (Aura current6 in current5.Auras)
					{
						if (current6.Keywords == null)
						{
							current6.Keywords = "";
						}
					}
					if (current5.CreaturePowers == null)
					{
						current5.CreaturePowers = new List<CreaturePower>();
					}
					if (current5.DamageModifierTemplates == null)
					{
						current5.DamageModifierTemplates = new List<DamageModifierTemplate>();
					}
					foreach (CreaturePower current7 in current5.CreaturePowers)
					{
						if (current7.Condition == null)
						{
							current7.Condition = "";
						}
						current7.ExtractAttackDetails();
					}
					if (current5.Tactics == null)
					{
						current5.Tactics = "";
					}
				}
			}
			if (this.fThemes == null)
			{
				this.fThemes = new List<MonsterTheme>();
			}
			foreach (MonsterTheme current8 in this.fThemes)
			{
				if (current8 != null)
				{
					foreach (ThemePowerData current9 in current8.Powers)
					{
						current9.Power.ExtractAttackDetails();
					}
				}
			}
			if (this.fTraps == null)
			{
				this.fTraps = new List<Trap>();
			}
			foreach (Trap current10 in this.fTraps)
			{
				if (current10.Description == null)
				{
					current10.Description = "";
				}
				if (current10.Attacks == null)
				{
					current10.Attacks = new List<TrapAttack>();
				}
				if (current10.Attack != null)
				{
					current10.Attacks.Add(current10.Attack);
					current10.Initiative = (current10.Attack.HasInitiative ? current10.Attack.Initiative : int.MinValue);
					current10.Trigger = current10.Attack.Trigger;
					current10.Attack = null;
				}
				foreach (TrapAttack current11 in current10.Attacks)
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
				if (current10.Trigger == null)
				{
					current10.Trigger = "";
				}
				foreach (TrapSkillData current12 in current10.Skills)
				{
					if (current12.ID == Guid.Empty)
					{
						current12.ID = Guid.NewGuid();
					}
				}
			}
			if (this.fSkillChallenges == null)
			{
				this.fSkillChallenges = new List<SkillChallenge>();
			}
			foreach (SkillChallenge current13 in this.fSkillChallenges)
			{
				if (current13.Notes == null)
				{
					current13.Notes = "";
				}
				foreach (SkillChallengeData current14 in current13.Skills)
				{
					if (current14.Results == null)
					{
						current14.Results = new SkillChallengeResult();
					}
				}
			}
			if (this.fMagicItems == null)
			{
				this.fMagicItems = new List<MagicItem>();
			}
			if (this.fArtifacts == null)
			{
				this.fArtifacts = new List<Artifact>();
			}
			foreach (Tile current15 in this.fTiles)
			{
				Program.SetResolution(current15.Image);
				if (current15.Keywords == null)
				{
					current15.Keywords = "";
				}
			}
			if (this.fTerrainPowers == null)
			{
				this.fTerrainPowers = new List<TerrainPower>();
			}
		}

		public void Import(Library lib)
		{
			foreach (Creature creature in lib.Creatures)
			{
				if (creature != null && this.FindCreature(creature.ID) == null)
				{
					this.fCreatures.Add(creature);
				}
			}
			foreach (CreatureTemplate template in lib.Templates)
			{
				if (template != null && this.FindTemplate(template.ID) == null)
				{
					this.fTemplates.Add(template);
				}
			}
			foreach (MonsterTheme theme in lib.Themes)
			{
				if (theme != null && this.FindTheme(theme.ID) == null)
				{
					this.fThemes.Add(theme);
				}
			}
			foreach (Trap trap in lib.Traps)
			{
				if (trap != null && this.FindTrap(trap.ID) == null)
				{
					this.fTraps.Add(trap);
				}
			}
			foreach (SkillChallenge skillChallenge in lib.SkillChallenges)
			{
				if (skillChallenge != null && FindSkillChallenge(skillChallenge.ID) == null)
				{
                    fSkillChallenges.Add(skillChallenge);
				}
			}
			foreach (MagicItem magicItem in lib.MagicItems)
			{
				if (magicItem != null && this.FindMagicItem(magicItem.ID) == null)
				{
					this.fMagicItems.Add(magicItem);
				}
			}
			foreach (Artifact current7 in lib.Artifacts)
			{
				if (current7 != null && this.FindArtifact(current7.ID) == null)
				{
					this.fArtifacts.Add(current7);
				}
			}
			foreach (Tile current8 in lib.Tiles)
			{
				if (current8 != null && this.FindTile(current8.ID) == null)
				{
					this.fTiles.Add(current8);
				}
			}
		}
	}
}
