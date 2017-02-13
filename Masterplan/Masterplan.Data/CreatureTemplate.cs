using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class CreatureTemplate
	{
		private string fName = "";

		private Guid fID = Guid.NewGuid();

		private CreatureTemplateType fType;

		private RoleType fRole;

		private bool fLeader;

		private string fSenses = "";

		private string fMovement = "";

		private int fHP;

		private int fInitiative;

		private int fAC;

		private int fFortitude;

		private int fReflex;

		private int fWill;

		private Regeneration fRegeneration;

		private List<Aura> fAuras = new List<Aura>();

		private List<CreaturePower> fCreaturePowers = new List<CreaturePower>();

		private List<DamageModifierTemplate> fDamageModifierTemplates = new List<DamageModifierTemplate>();

		private string fResist = "";

		private string fVulnerable = "";

		private string fImmune = "";

		private string fTactics = "";

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

		public CreatureTemplateType Type
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

		public RoleType Role
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

		public bool Leader
		{
			get
			{
				return this.fLeader;
			}
			set
			{
				this.fLeader = value;
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
				return this.fMovement;
			}
			set
			{
				this.fMovement = value;
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

		public List<DamageModifierTemplate> DamageModifierTemplates
		{
			get
			{
				return this.fDamageModifierTemplates;
			}
			set
			{
				this.fDamageModifierTemplates = value;
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

		public string Info
		{
			get
			{
				string arg = (this.fType == CreatureTemplateType.Functional) ? "Elite " : "";
				string arg2 = this.fLeader ? " (L)" : "";
				return arg + this.fRole + arg2;
			}
		}

		public CreatureTemplate Copy()
		{
			CreatureTemplate creatureTemplate = new CreatureTemplate();
			creatureTemplate.Name = this.fName;
			creatureTemplate.ID = this.fID;
			creatureTemplate.Type = this.fType;
			creatureTemplate.Role = this.fRole;
			creatureTemplate.Leader = this.fLeader;
			creatureTemplate.Senses = this.fSenses;
			creatureTemplate.Movement = this.fMovement;
			creatureTemplate.HP = this.fHP;
			creatureTemplate.Initiative = this.fInitiative;
			creatureTemplate.AC = this.fAC;
			creatureTemplate.Fortitude = this.fFortitude;
			creatureTemplate.Reflex = this.fReflex;
			creatureTemplate.Will = this.fWill;
			creatureTemplate.Regeneration = ((this.fRegeneration != null) ? this.fRegeneration.Copy() : null);
			foreach (Aura current in this.fAuras)
			{
				creatureTemplate.Auras.Add(current.Copy());
			}
			foreach (CreaturePower current2 in this.fCreaturePowers)
			{
				creatureTemplate.CreaturePowers.Add(current2.Copy());
			}
			foreach (DamageModifierTemplate current3 in this.fDamageModifierTemplates)
			{
				creatureTemplate.DamageModifierTemplates.Add(current3.Copy());
			}
			creatureTemplate.Resist = this.fResist;
			creatureTemplate.Vulnerable = this.fVulnerable;
			creatureTemplate.Immune = this.fImmune;
			creatureTemplate.Tactics = this.fTactics;
			return creatureTemplate;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}
