using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class CustomCreature : ICreature
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fDetails = "";

		private CreatureSize fSize = CreatureSize.Medium;

		private CreatureOrigin fOrigin = CreatureOrigin.Natural;

		private CreatureType fType = CreatureType.MagicalBeast;

		private string fKeywords = "";

		private int fLevel = 1;

		private IRole fRole = new ComplexRole();

		private string fSenses = "";

		private string fMovement = "";

		private string fAlignment = "";

		private string fLanguages = "";

		private string fSkills = "";

		private string fEquipment = "";

		private Ability fStrength = new Ability();

		private Ability fConstitution = new Ability();

		private Ability fDexterity = new Ability();

		private Ability fIntelligence = new Ability();

		private Ability fWisdom = new Ability();

		private Ability fCharisma = new Ability();

		private int fInitiativeModifier;

		private int fHPModifier;

		private int fACModifier;

		private int fFortitudeModifier;

		private int fReflexModifier;

		private int fWillModifier;

		private Regeneration fRegeneration;

		private List<Aura> fAuras = new List<Aura>();

		private List<CreaturePower> fCreaturePowers = new List<CreaturePower>();

		private List<DamageModifier> fDamageModifiers = new List<DamageModifier>();

		private string fResist = "";

		private string fVulnerable = "";

		private string fImmune = "";

		private string fTactics = "";

		private Image fImage;

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

		public string Details
		{
			get
			{
				return this.fDetails;
			}
			set
			{
				this.fDetails = value;
			}
		}

		public CreatureSize Size
		{
			get
			{
				return this.fSize;
			}
			set
			{
				this.fSize = value;
			}
		}

		public CreatureOrigin Origin
		{
			get
			{
				return this.fOrigin;
			}
			set
			{
				this.fOrigin = value;
			}
		}

		public CreatureType Type
		{
			get
			{
				return this.fType;
			}
			set
			{
				this.fType = value;
			}
		}

		public string Keywords
		{
			get
			{
				return this.fKeywords;
			}
			set
			{
				this.fKeywords = value;
			}
		}

		public int Level
		{
			get
			{
				return this.fLevel;
			}
			set
			{
				this.fLevel = value;
			}
		}

		public IRole Role
		{
			get
			{
				return this.fRole;
			}
			set
			{
				this.fRole = value;
			}
		}

		public string Senses
		{
			get
			{
				return this.fSenses;
			}
			set
			{
				this.fSenses = value;
			}
		}

		public string Movement
		{
			get
			{
				if (this.fMovement == null || this.fMovement == "")
				{
					return Creature.GetSpeed(this.fSize) + " squares";
				}
				return this.fMovement;
			}
			set
			{
				this.fMovement = value;
			}
		}

		public string Alignment
		{
			get
			{
				return this.fAlignment;
			}
			set
			{
				this.fAlignment = value;
			}
		}

		public string Languages
		{
			get
			{
				return this.fLanguages;
			}
			set
			{
				this.fLanguages = value;
			}
		}

		public string Skills
		{
			get
			{
				return this.fSkills;
			}
			set
			{
				this.fSkills = value;
			}
		}

		public string Equipment
		{
			get
			{
				return this.fEquipment;
			}
			set
			{
				this.fEquipment = value;
			}
		}

		public string Category
		{
			get
			{
				return "";
			}
			set
			{
			}
		}

		public Ability Strength
		{
			get
			{
				return this.fStrength;
			}
			set
			{
				this.fStrength = value;
			}
		}

		public Ability Constitution
		{
			get
			{
				return this.fConstitution;
			}
			set
			{
				this.fConstitution = value;
			}
		}

		public Ability Dexterity
		{
			get
			{
				return this.fDexterity;
			}
			set
			{
				this.fDexterity = value;
			}
		}

		public Ability Intelligence
		{
			get
			{
				return this.fIntelligence;
			}
			set
			{
				this.fIntelligence = value;
			}
		}

		public Ability Wisdom
		{
			get
			{
				return this.fWisdom;
			}
			set
			{
				this.fWisdom = value;
			}
		}

		public Ability Charisma
		{
			get
			{
				return this.fCharisma;
			}
			set
			{
				this.fCharisma = value;
			}
		}

		public int Initiative
		{
			get
			{
				int num = Statistics.Initiative(this.fLevel, this.fRole);
				return num + this.fDexterity.Modifier + this.fInitiativeModifier;
			}
			set
			{
				int num = Statistics.Initiative(this.fLevel, this.fRole);
				this.fInitiativeModifier = value - num - this.fDexterity.Modifier;
			}
		}

		public int HP
		{
			get
			{
				if (this.fRole is Minion)
				{
					return 1;
				}
				int num = Statistics.HP(this.fLevel, this.fRole as ComplexRole, this.fConstitution.Score);
				return num + this.fHPModifier;
			}
			set
			{
				if (this.fRole is Minion)
				{
					return;
				}
				int num = Statistics.HP(this.fLevel, this.fRole as ComplexRole, this.fConstitution.Score);
				this.fHPModifier = value - num;
			}
		}

		public int AC
		{
			get
			{
				int num = Statistics.AC(this.fLevel, this.fRole);
				return num + this.fACModifier;
			}
			set
			{
				int num = Statistics.AC(this.fLevel, this.fRole);
				this.fACModifier = value - num;
			}
		}

		public int Fortitude
		{
			get
			{
				int num = Statistics.NAD(this.fLevel, this.fRole);
				int score = Math.Max(this.fStrength.Score, this.fConstitution.Score);
				int modifier = Ability.GetModifier(score);
				return num + modifier + this.fFortitudeModifier;
			}
			set
			{
				int num = Statistics.NAD(this.fLevel, this.fRole);
				int score = Math.Max(this.fStrength.Score, this.fConstitution.Score);
				int modifier = Ability.GetModifier(score);
				this.fFortitudeModifier = value - num - modifier;
			}
		}

		public int Reflex
		{
			get
			{
				int num = Statistics.NAD(this.fLevel, this.fRole);
				int score = Math.Max(this.fDexterity.Score, this.fIntelligence.Score);
				int modifier = Ability.GetModifier(score);
				return num + modifier + this.fReflexModifier;
			}
			set
			{
				int num = Statistics.NAD(this.fLevel, this.fRole);
				int score = Math.Max(this.fDexterity.Score, this.fIntelligence.Score);
				int modifier = Ability.GetModifier(score);
				this.fReflexModifier = value - num - modifier;
			}
		}

		public int Will
		{
			get
			{
				int num = Statistics.NAD(this.fLevel, this.fRole);
				int score = Math.Max(this.fWisdom.Score, this.fCharisma.Score);
				int modifier = Ability.GetModifier(score);
				return num + modifier + this.fWillModifier;
			}
			set
			{
				int num = Statistics.NAD(this.fLevel, this.fRole);
				int score = Math.Max(this.fWisdom.Score, this.fCharisma.Score);
				int modifier = Ability.GetModifier(score);
				this.fWillModifier = value - num - modifier;
			}
		}

		public int InitiativeModifier
		{
			get
			{
				return this.fInitiativeModifier;
			}
			set
			{
				this.fInitiativeModifier = value;
			}
		}

		public int HPModifier
		{
			get
			{
				return this.fHPModifier;
			}
			set
			{
				this.fHPModifier = value;
			}
		}

		public int ACModifier
		{
			get
			{
				return this.fACModifier;
			}
			set
			{
				this.fACModifier = value;
			}
		}

		public int FortitudeModifier
		{
			get
			{
				return this.fFortitudeModifier;
			}
			set
			{
				this.fFortitudeModifier = value;
			}
		}

		public int ReflexModifier
		{
			get
			{
				return this.fReflexModifier;
			}
			set
			{
				this.fReflexModifier = value;
			}
		}

		public int WillModifier
		{
			get
			{
				return this.fWillModifier;
			}
			set
			{
				this.fWillModifier = value;
			}
		}

		public Regeneration Regeneration
		{
			get
			{
				return this.fRegeneration;
			}
			set
			{
				this.fRegeneration = value;
			}
		}

		public List<Aura> Auras
		{
			get
			{
				return this.fAuras;
			}
			set
			{
				this.fAuras = value;
			}
		}

		public List<CreaturePower> CreaturePowers
		{
			get
			{
				return this.fCreaturePowers;
			}
			set
			{
				this.fCreaturePowers = value;
			}
		}

		public List<DamageModifier> DamageModifiers
		{
			get
			{
				return this.fDamageModifiers;
			}
			set
			{
				this.fDamageModifiers = value;
			}
		}

		public string Resist
		{
			get
			{
				return this.fResist;
			}
			set
			{
				this.fResist = value;
			}
		}

		public string Vulnerable
		{
			get
			{
				return this.fVulnerable;
			}
			set
			{
				this.fVulnerable = value;
			}
		}

		public string Immune
		{
			get
			{
				return this.fImmune;
			}
			set
			{
				this.fImmune = value;
			}
		}

		public string Tactics
		{
			get
			{
				return this.fTactics;
			}
			set
			{
				this.fTactics = value;
			}
		}

		public Image Image
		{
			get
			{
				return this.fImage;
			}
			set
			{
				this.fImage = value;
			}
		}

		public string Info
		{
			get
			{
				return string.Concat(new object[]
				{
					"Level ",
					this.fLevel,
					" ",
					this.fRole
				});
			}
		}

		public string Phenotype
		{
			get
			{
				string text = this.fSize + " " + this.fOrigin.ToString().ToLower();
				if (this.fType == CreatureType.MagicalBeast)
				{
					text += " magical beast";
				}
				else
				{
					text = text + " " + this.fType.ToString().ToLower();
				}
				if (this.fKeywords != null && this.fKeywords != "")
				{
					text = text + " (" + this.fKeywords.ToLower() + ")";
				}
				return text;
			}
		}

		public CustomCreature()
		{
		}

		public CustomCreature(ICreature c)
		{
			CreatureHelper.CopyFields(c, this);
		}

		public override string ToString()
		{
			return this.fName;
		}

		public CustomCreature Copy()
		{
			CustomCreature customCreature = new CustomCreature();
			customCreature.ID = this.fID;
			customCreature.Name = this.fName;
			customCreature.Details = this.fDetails;
			customCreature.Size = this.fSize;
			customCreature.Origin = this.fOrigin;
			customCreature.Type = this.fType;
			customCreature.Keywords = this.fKeywords;
			customCreature.Level = this.fLevel;
			customCreature.Role = this.fRole.Copy();
			customCreature.Senses = this.fSenses;
			customCreature.Movement = this.fMovement;
			customCreature.Alignment = this.fAlignment;
			customCreature.Languages = this.fLanguages;
			customCreature.Skills = this.fSkills;
			customCreature.Equipment = this.fEquipment;
			customCreature.Strength = this.fStrength.Copy();
			customCreature.Constitution = this.fConstitution.Copy();
			customCreature.Dexterity = this.fDexterity.Copy();
			customCreature.Intelligence = this.fIntelligence.Copy();
			customCreature.Wisdom = this.fWisdom.Copy();
			customCreature.Charisma = this.fCharisma.Copy();
			customCreature.InitiativeModifier = this.fInitiativeModifier;
			customCreature.HPModifier = this.fHPModifier;
			customCreature.ACModifier = this.fACModifier;
			customCreature.FortitudeModifier = this.fFortitudeModifier;
			customCreature.ReflexModifier = this.fReflexModifier;
			customCreature.WillModifier = this.fWillModifier;
			customCreature.Regeneration = ((this.fRegeneration != null) ? this.fRegeneration.Copy() : null);
			foreach (Aura current in this.fAuras)
			{
				customCreature.Auras.Add(current.Copy());
			}
			foreach (CreaturePower current2 in this.fCreaturePowers)
			{
				customCreature.CreaturePowers.Add(current2.Copy());
			}
			foreach (DamageModifier current3 in this.fDamageModifiers)
			{
				customCreature.DamageModifiers.Add(current3.Copy());
			}
			customCreature.Resist = this.fResist;
			customCreature.Vulnerable = this.fVulnerable;
			customCreature.Immune = this.fImmune;
			customCreature.Tactics = this.fTactics;
			customCreature.Image = this.fImage;
			return customCreature;
		}
	}
}
