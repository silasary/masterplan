using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class Creature : ICreature, IComparable<Creature>
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

		private string fMovement = "6";

		private string fAlignment = "";

		private string fLanguages = "";

		private string fSkills = "";

		private string fEquipment = "";

		private string fCategory = "";

		private Ability fStrength = new Ability();

		private Ability fConstitution = new Ability();

		private Ability fDexterity = new Ability();

		private Ability fIntelligence = new Ability();

		private Ability fWisdom = new Ability();

		private Ability fCharisma = new Ability();

		private int fHP;

		private int fInitiative;

		private int fAC = 10;

		private int fFortitude = 10;

		private int fReflex = 10;

		private int fWill = 10;

		private Regeneration fRegeneration;

		private List<Aura> fAuras = new List<Aura>();

		private List<CreaturePower> fCreaturePowers = new List<CreaturePower>();

		private List<DamageModifier> fDamageModifiers = new List<DamageModifier>();

		private string fResist = "";

		private string fVulnerable = "";

		private string fImmune = "";

		private string fTactics = "";

		private Image fImage;

		private string fURL = "";

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
				return this.fCategory;
			}
			set
			{
				this.fCategory = value;
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

		public int HP
		{
			get
			{
				return this.fHP;
			}
			set
			{
				this.fHP = value;
			}
		}

		public int Initiative
		{
			get
			{
				return this.fInitiative;
			}
			set
			{
				this.fInitiative = value;
			}
		}

		public int AC
		{
			get
			{
				return this.fAC;
			}
			set
			{
				this.fAC = value;
			}
		}

		public int Fortitude
		{
			get
			{
				return this.fFortitude;
			}
			set
			{
				this.fFortitude = value;
			}
		}

		public int Reflex
		{
			get
			{
				return this.fReflex;
			}
			set
			{
				this.fReflex = value;
			}
		}

		public int Will
		{
			get
			{
				return this.fWill;
			}
			set
			{
				this.fWill = value;
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

		public string URL
		{
			get
			{
				return this.fURL;
			}
			set
			{
				this.fURL = value;
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

		public Creature()
		{
		}

		public Creature(ICreature c)
		{
			CreatureHelper.CopyFields(c, this);
		}

		public Creature Copy()
		{
			Creature creature = new Creature();
			creature.ID = this.fID;
			creature.Name = this.fName;
			creature.Details = this.fDetails;
			creature.Size = this.fSize;
			creature.Origin = this.fOrigin;
			creature.Type = this.fType;
			creature.Keywords = this.fKeywords;
			creature.Level = this.fLevel;
			creature.Role = this.fRole.Copy();
			creature.Senses = this.fSenses;
			creature.Movement = this.fMovement;
			creature.Alignment = this.fAlignment;
			creature.Languages = this.fLanguages;
			creature.Skills = this.fSkills;
			creature.Equipment = this.fEquipment;
			creature.Category = this.fCategory;
			creature.Strength = this.fStrength.Copy();
			creature.Constitution = this.fConstitution.Copy();
			creature.Dexterity = this.fDexterity.Copy();
			creature.Intelligence = this.fIntelligence.Copy();
			creature.Wisdom = this.fWisdom.Copy();
			creature.Charisma = this.fCharisma.Copy();
			creature.HP = this.fHP;
			creature.Initiative = this.fInitiative;
			creature.AC = this.fAC;
			creature.Fortitude = this.fFortitude;
			creature.Reflex = this.fReflex;
			creature.Will = this.fWill;
			creature.Regeneration = ((this.fRegeneration != null) ? this.fRegeneration.Copy() : null);
			foreach (Aura current in this.fAuras)
			{
				creature.Auras.Add(current.Copy());
			}
			foreach (CreaturePower current2 in this.fCreaturePowers)
			{
				creature.CreaturePowers.Add(current2.Copy());
			}
			foreach (DamageModifier current3 in this.fDamageModifiers)
			{
				creature.DamageModifiers.Add(current3.Copy());
			}
			creature.Resist = this.fResist;
			creature.Vulnerable = this.fVulnerable;
			creature.Immune = this.fImmune;
			creature.Tactics = this.fTactics;
			creature.Image = this.fImage;
			creature.URL = this.fURL;
			return creature;
		}

		public override string ToString()
		{
			return this.fName + " (" + this.Info + ")";
		}

		public int CompareTo(Creature rhs)
		{
			return this.fName.CompareTo(rhs.Name);
		}

		public static int GetSize(CreatureSize size)
		{
			switch (size)
			{
			case CreatureSize.Large:
				return 2;
			case CreatureSize.Huge:
				return 3;
			case CreatureSize.Gargantuan:
				return 4;
			default:
				return 1;
			}
		}

		public static int GetSpeed(CreatureSize size)
		{
			switch (size)
			{
			case CreatureSize.Tiny:
			case CreatureSize.Small:
				return 4;
			case CreatureSize.Medium:
				return 6;
			case CreatureSize.Large:
				return 6;
			case CreatureSize.Huge:
				return 8;
			case CreatureSize.Gargantuan:
				return 10;
			default:
				return 6;
			}
		}
	}
}
