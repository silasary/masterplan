using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class Hero : IToken, IComparable<Hero>
	{
		private Guid fID = Guid.NewGuid();

		private string fKey = "";

		private string fName = "";

		private string fPlayer = "";

		private CreatureSize fSize = CreatureSize.Medium;

		private string fRace = "";

		private int fLevel = Session.Project.Party.Level;

		private string fClass = "";

		private string fParagonPath = "";

		private string fEpicDestiny = "";

		private string fPowerSource = "";

		private HeroRoleType fRole;

		private CombatData fCombatData = new CombatData();

		private int fHP;

		private int fAC = 10;

		private int fFortitude = 10;

		private int fReflex = 10;

		private int fWill = 10;

		private int fInitBonus;

		private int fPassivePerception = 10;

		private int fPassiveInsight = 10;

		private string fLanguages = "";

		private List<OngoingCondition> fEffects = new List<OngoingCondition>();

		private List<CustomToken> fTokens = new List<CustomToken>();

		private Image fPortrait;

		public Guid ID
		{
			get
			{
				return this.fID;
			}
			set
			{
				this.fID = value;
				if (this.fCombatData != null)
				{
					this.fCombatData.ID = value;
				}
			}
		}

		public string Key
		{
			get
			{
				return this.fKey;
			}
			set
			{
				this.fKey = value;
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
				if (this.fCombatData != null)
				{
					this.fCombatData.DisplayName = value;
				}
			}
		}

		public string Player
		{
			get
			{
				return this.fPlayer;
			}
			set
			{
				this.fPlayer = value;
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

		public string Race
		{
			get
			{
				return this.fRace;
			}
			set
			{
				this.fRace = value;
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

		public string Class
		{
			get
			{
				return this.fClass;
			}
			set
			{
				this.fClass = value;
			}
		}

		public string ParagonPath
		{
			get
			{
				return this.fParagonPath;
			}
			set
			{
				this.fParagonPath = value;
			}
		}

		public string EpicDestiny
		{
			get
			{
				return this.fEpicDestiny;
			}
			set
			{
				this.fEpicDestiny = value;
			}
		}

		public string PowerSource
		{
			get
			{
				return this.fPowerSource;
			}
			set
			{
				this.fPowerSource = value;
			}
		}

		public HeroRoleType Role
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

		public CombatData CombatData
		{
			get
			{
				return this.fCombatData;
			}
			set
			{
				this.fCombatData = value;
				this.fCombatData.ID = this.fID;
				this.fCombatData.DisplayName = this.fName;
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

		public int InitBonus
		{
			get
			{
				return this.fInitBonus;
			}
			set
			{
				this.fInitBonus = value;
			}
		}

		public int PassivePerception
		{
			get
			{
				return this.fPassivePerception;
			}
			set
			{
				this.fPassivePerception = value;
			}
		}

		public int PassiveInsight
		{
			get
			{
				return this.fPassiveInsight;
			}
			set
			{
				this.fPassiveInsight = value;
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

		public List<OngoingCondition> Effects
		{
			get
			{
				return this.fEffects;
			}
			set
			{
				this.fEffects = value;
			}
		}

		public List<CustomToken> Tokens
		{
			get
			{
				return this.fTokens;
			}
			set
			{
				this.fTokens = value;
			}
		}

		public string Info
		{
			get
			{
				string text = "Level " + this.fLevel;
				if (this.fRace != "")
				{
					if (text != "")
					{
						text += " ";
					}
					text += this.fRace;
				}
				if (this.fClass != "")
				{
					if (text != "")
					{
						text += " ";
					}
					text += this.fClass;
				}
				if (this.fParagonPath != "")
				{
					if (text != "")
					{
						text += " / ";
					}
					text += this.fParagonPath;
				}
				if (this.fEpicDestiny != "")
				{
					if (text != "")
					{
						text += " / ";
					}
					text += this.fEpicDestiny;
				}
				return text;
			}
		}

		public Image Portrait
		{
			get
			{
				return this.fPortrait;
			}
			set
			{
				this.fPortrait = value;
			}
		}

		public CreatureState GetState(int damage)
		{
			if (this.fHP != 0)
			{
				int num = this.fHP - damage;
				int num2 = this.fHP / 2;
				if (num <= 0)
				{
					return CreatureState.Defeated;
				}
				if (num <= num2)
				{
					return CreatureState.Bloodied;
				}
			}
			return CreatureState.Active;
		}

		public Hero Copy()
		{
			Hero hero = new Hero();
			hero.ID = this.fID;
			hero.Key = this.fKey;
			hero.Name = this.fName;
			hero.Player = this.fPlayer;
			hero.Size = this.fSize;
			hero.Race = this.fRace;
			hero.Level = this.fLevel;
			hero.Class = this.fClass;
			hero.ParagonPath = this.fParagonPath;
			hero.EpicDestiny = this.fEpicDestiny;
			hero.PowerSource = this.fPowerSource;
			hero.Role = this.fRole;
			hero.CombatData = this.fCombatData.Copy();
			hero.HP = this.fHP;
			hero.AC = this.fAC;
			hero.Fortitude = this.fFortitude;
			hero.Reflex = this.fReflex;
			hero.Will = this.fWill;
			hero.InitBonus = this.fInitBonus;
			hero.PassivePerception = this.fPassivePerception;
			hero.PassiveInsight = this.fPassiveInsight;
			hero.Languages = this.fLanguages;
			hero.Portrait = this.fPortrait;
			foreach (OngoingCondition current in this.fEffects)
			{
				hero.Effects.Add(current.Copy());
			}
			foreach (CustomToken current2 in this.fTokens)
			{
				hero.Tokens.Add(current2.Copy());
			}
			return hero;
		}

		public int CompareTo(Hero rhs)
		{
			return this.fName.CompareTo(rhs.Name);
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}
