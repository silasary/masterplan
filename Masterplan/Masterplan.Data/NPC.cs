using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class NPC : ICreature
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fDetails = "";

		private CreatureSize fSize = CreatureSize.Medium;

		private CreatureOrigin fOrigin = CreatureOrigin.Natural;

		private CreatureType fType = CreatureType.MagicalBeast;

		private string fKeywords = "";

		private int fLevel = 1;

		private Guid fTemplateID = Guid.Empty;

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

		public Guid TemplateID
		{
			get
			{
				return this.fTemplateID;
			}
			set
			{
				this.fTemplateID = value;
			}
		}

		public IRole Role
		{
			get
			{
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					return new ComplexRole
					{
						Type = creatureTemplate.Role,
						Leader = creatureTemplate.Leader
					};
				}
				return null;
			}
			set
			{
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
				return "NPCs";
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
				int num = this.fLevel / 2 + this.fDexterity.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					num += creatureTemplate.Initiative;
				}
				return num + this.fInitiativeModifier;
			}
			set
			{
				int num = this.fLevel / 2 + this.fDexterity.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					num += creatureTemplate.Initiative;
				}
				this.fInitiativeModifier = value - num;
			}
		}

		public int HP
		{
			get
			{
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					int num = this.fConstitution.Score;
					num += (this.fLevel + 1) * creatureTemplate.HP;
					return num + this.fHPModifier;
				}
				return 0;
			}
			set
			{
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					int num = this.fConstitution.Score;
					num += (this.fLevel + 1) * creatureTemplate.HP;
					this.fHPModifier = value - num;
				}
			}
		}

		public int AC
		{
			get
			{
				int num = 10 + this.fLevel / 2;
				num += new Ability
				{
					Score = Math.Max(this.fDexterity.Score, this.fIntelligence.Score)
				}.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					num += creatureTemplate.AC;
				}
				return num + this.fACModifier;
			}
			set
			{
				int num = 10 + this.fLevel / 2;
				num += new Ability
				{
					Score = Math.Max(this.fDexterity.Score, this.fIntelligence.Score)
				}.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					num += creatureTemplate.AC;
				}
				this.fACModifier = value - num;
			}
		}

		public int Fortitude
		{
			get
			{
				int num = 10 + this.fLevel / 2;
				num += new Ability
				{
					Score = Math.Max(this.fStrength.Score, this.fConstitution.Score)
				}.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					num += creatureTemplate.Fortitude;
				}
				return num + this.fFortitudeModifier;
			}
			set
			{
				int num = 10 + this.fLevel / 2;
				num += new Ability
				{
					Score = Math.Max(this.fStrength.Score, this.fConstitution.Score)
				}.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					num += creatureTemplate.Fortitude;
				}
				this.fFortitudeModifier = value - num;
			}
		}

		public int Reflex
		{
			get
			{
				int num = 10 + this.fLevel / 2;
				num += new Ability
				{
					Score = Math.Max(this.fDexterity.Score, this.fIntelligence.Score)
				}.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					num += creatureTemplate.Reflex;
				}
				return num + this.fReflexModifier;
			}
			set
			{
				int num = 10 + this.fLevel / 2;
				num += new Ability
				{
					Score = Math.Max(this.fDexterity.Score, this.fIntelligence.Score)
				}.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					num += creatureTemplate.Reflex;
				}
				this.fReflexModifier = value - num;
			}
		}

		public int Will
		{
			get
			{
				int num = 10 + this.fLevel / 2;
				num += new Ability
				{
					Score = Math.Max(this.fWisdom.Score, this.fCharisma.Score)
				}.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					num += creatureTemplate.Will;
				}
				return num + this.fWillModifier;
			}
			set
			{
				int num = 10 + this.fLevel / 2;
				num += new Ability
				{
					Score = Math.Max(this.fWisdom.Score, this.fCharisma.Score)
				}.Modifier;
				CreatureTemplate creatureTemplate = Session.FindTemplate(this.fTemplateID, SearchType.Global);
				if (creatureTemplate != null)
				{
					num += creatureTemplate.Will;
				}
				this.fWillModifier = value - num;
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
					this.Role
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

		public int AbilityCost
		{
			get
			{
				int num = 0;
				int num2 = 0;
				int num3 = 10;
				num += this.fStrength.Cost;
				if (this.fStrength.Score < 10)
				{
					num2++;
				}
				if (this.fStrength.Score < num3)
				{
					num3 = this.fStrength.Score;
				}
				num += this.fConstitution.Cost;
				if (this.fConstitution.Score < 10)
				{
					num2++;
				}
				if (this.fConstitution.Score < num3)
				{
					num3 = this.fConstitution.Score;
				}
				num += this.fDexterity.Cost;
				if (this.fDexterity.Score < 10)
				{
					num2++;
				}
				if (this.fDexterity.Score < num3)
				{
					num3 = this.fDexterity.Score;
				}
				num += this.fIntelligence.Cost;
				if (this.fIntelligence.Score < 10)
				{
					num2++;
				}
				if (this.fIntelligence.Score < num3)
				{
					num3 = this.fIntelligence.Score;
				}
				num += this.fWisdom.Cost;
				if (this.fWisdom.Score < 10)
				{
					num2++;
				}
				if (this.fWisdom.Score < num3)
				{
					num3 = this.fWisdom.Score;
				}
				num += this.fCharisma.Cost;
				if (this.fCharisma.Score < 10)
				{
					num2++;
				}
				if (this.fCharisma.Score < num3)
				{
					num3 = this.fCharisma.Score;
				}
				if (num2 > 1)
				{
					return -1;
				}
				if (num3 < 8)
				{
					return -1;
				}
				if (num3 == 9)
				{
					num++;
				}
				if (num3 > 9)
				{
					num += 2;
				}
				return num;
			}
		}

		public override string ToString()
		{
			return this.fName;
		}

		public NPC Copy()
		{
			NPC nPC = new NPC();
			nPC.ID = this.fID;
			nPC.Name = this.fName;
			nPC.Details = this.fDetails;
			nPC.Size = this.fSize;
			nPC.Origin = this.fOrigin;
			nPC.Type = this.fType;
			nPC.Keywords = this.fKeywords;
			nPC.Level = this.fLevel;
			nPC.TemplateID = this.fTemplateID;
			nPC.Senses = this.fSenses;
			nPC.Movement = this.fMovement;
			nPC.Alignment = this.fAlignment;
			nPC.Languages = this.fLanguages;
			nPC.Skills = this.fSkills;
			nPC.Equipment = this.fEquipment;
			nPC.Strength = this.fStrength.Copy();
			nPC.Constitution = this.fConstitution.Copy();
			nPC.Dexterity = this.fDexterity.Copy();
			nPC.Intelligence = this.fIntelligence.Copy();
			nPC.Wisdom = this.fWisdom.Copy();
			nPC.Charisma = this.fCharisma.Copy();
			nPC.InitiativeModifier = this.fInitiativeModifier;
			nPC.HPModifier = this.fHPModifier;
			nPC.ACModifier = this.fACModifier;
			nPC.FortitudeModifier = this.fFortitudeModifier;
			nPC.ReflexModifier = this.fReflexModifier;
			nPC.WillModifier = this.fWillModifier;
			nPC.Regeneration = ((this.fRegeneration != null) ? this.fRegeneration.Copy() : null);
			foreach (Aura current in this.fAuras)
			{
				nPC.Auras.Add(current.Copy());
			}
			foreach (CreaturePower current2 in this.fCreaturePowers)
			{
				nPC.CreaturePowers.Add(current2.Copy());
			}
			foreach (DamageModifier current3 in this.fDamageModifiers)
			{
				nPC.DamageModifiers.Add(current3.Copy());
			}
			nPC.Resist = this.fResist;
			nPC.Vulnerable = this.fVulnerable;
			nPC.Immune = this.fImmune;
			nPC.Tactics = this.fTactics;
			nPC.Image = this.fImage;
			return nPC;
		}
	}
}
