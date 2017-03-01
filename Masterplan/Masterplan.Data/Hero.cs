using System;
using System.Collections.Generic;
using System.Drawing;

namespace Masterplan.Data
{
    /// <summary>
    /// Class representing a PC.
    /// </summary>
    [Serializable]
	public class Hero : IToken, IComparable<Hero>
	{
		private Guid fID = Guid.NewGuid();

		private string fKey = "";

		private string fName = "";

		private string fRace = "";

		private int fLevel = Session.Project?.Party?.Level ?? 1;

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

        ///<summary>
        ///Gets or sets the unique ID of the hero.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the External Hero Provider key.
        ///</summary>
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

        /// <summary>
        /// Sets the name of the External Hero Provider
        /// </summary>
        public string KeyProvider { get; set; }

        ///<summary>
        ///Gets or sets the name of the hero.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the player name.
        ///</summary>
        public string Player { get; set; } = "";

        ///<summary>
        ///Gets or sets the size of the PC.
        ///</summary>
        public CreatureSize Size { get; set; } = CreatureSize.Medium;

        ///<summary>
        ///Gets or sets the name of the PC's race.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the name of the PC's class.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the name of the PC's paragon path.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the name of the PC's epic destiny.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the power source of the PC's class.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the PC's role.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the hero's hit points.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the AC defence.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the Fortitude defence.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the Reflex defence.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the Will defence.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the hero's initiative bonus
        ///</summary>
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

        ///<summary>
        ///Gets or sets the PC's passive perception.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the PC's passive insight.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the languages spoken by the hero.
        ///</summary>
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

        ///<summary>
        ///Gets or sets the PC's portrait image.
        ///</summary>
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

        ///<summary>
        ///Creates a copy of the Hero.
        ///</summary>
        ///<returns>Returns the copy.</returns>
        public Hero Copy()
		{
            Hero hero = new Hero()
            {
                ID = this.fID,
                Key = this.fKey,
                Name = this.fName,
                Player = this.Player,
                Size = this.Size,
                Race = this.fRace,
                Level = this.fLevel,
                Class = this.fClass,
                ParagonPath = this.fParagonPath,
                EpicDestiny = this.fEpicDestiny,
                PowerSource = this.fPowerSource,
                Role = this.fRole,
                CombatData = this.fCombatData.Copy(),
                HP = this.fHP,
                AC = this.fAC,
                Fortitude = this.fFortitude,
                Reflex = this.fReflex,
                Will = this.fWill,
                InitBonus = this.fInitBonus,
                PassivePerception = this.fPassivePerception,
                PassiveInsight = this.fPassiveInsight,
                Languages = this.fLanguages,
                Portrait = this.fPortrait
            };
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

        ///<summary>
        ///Compares this Hero to another.
        ///</summary>
        ///<param name="rhs">The other Hero object.</param>
        ///<returns>Returns -1 if this Hero should be sorted before rhs, +1 if rhs should be sorted before this, 0 otherwise.</returns>
        public int CompareTo(Hero rhs)
		{
			return this.fName.CompareTo(rhs.Name);
		}

        ///<summary>
        ///Returns the hero's name.
        ///</summary>
        ///<returns></returns>
        public override string ToString()
		{
			return this.fName;
		}
	}
}
