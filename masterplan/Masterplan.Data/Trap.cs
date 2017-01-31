using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Trap : IComparable<Trap>
	{
		private Guid fID = Guid.NewGuid();

		private TrapType fType;

		private string fName = "";

		private int fLevel = 1;

		private IRole fRole = new ComplexRole(RoleType.Blaster);

		private string fReadAloud = "";

		private string fDescription = "";

		private string fDetails = "";

		private List<TrapSkillData> fSkills = new List<TrapSkillData>();

		private int fInitiative = int.MinValue;

		private string fTrigger = "";

		private TrapAttack fAttack = new TrapAttack();

		private List<TrapAttack> fAttacks = new List<TrapAttack>();

		private List<string> fCountermeasures = new List<string>();

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

		public TrapType Type
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

		public string ReadAloud
		{
			get
			{
				return this.fReadAloud;
			}
			set
			{
				this.fReadAloud = value;
			}
		}

		public string Description
		{
			get
			{
				return this.fDescription;
			}
			set
			{
				this.fDescription = value;
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

		public List<TrapSkillData> Skills
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

		public string Trigger
		{
			get
			{
				return this.fTrigger;
			}
			set
			{
				this.fTrigger = value;
			}
		}

		public TrapAttack Attack
		{
			get
			{
				return this.fAttack;
			}
			set
			{
				this.fAttack = value;
			}
		}

		public List<TrapAttack> Attacks
		{
			get
			{
				return this.fAttacks;
			}
			set
			{
				this.fAttacks = value;
			}
		}

		public List<string> Countermeasures
		{
			get
			{
				return this.fCountermeasures;
			}
			set
			{
				this.fCountermeasures = value;
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

		public int XP
		{
			get
			{
				int num2;
				if (this.fRole is Minion)
				{
					float num = (float)Experience.GetCreatureXP(this.fLevel) / 4f;
					num2 = (int)Math.Round((double)num, MidpointRounding.AwayFromZero);
				}
				else
				{
					ComplexRole complexRole = this.fRole as ComplexRole;
					num2 = Experience.GetCreatureXP(this.fLevel);
					switch (complexRole.Flag)
					{
					case RoleFlag.Elite:
						num2 *= 2;
						break;
					case RoleFlag.Solo:
						num2 *= 5;
						break;
					}
				}
				if (Session.Project != null)
				{
					num2 = (int)((double)num2 * Session.Project.CampaignSettings.XP);
				}
				return num2;
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
					this.fRole,
					" ",
					this.fType.ToString().ToLower()
				});
			}
		}

		public Trap Copy()
		{
			Trap trap = new Trap();
			trap.ID = this.fID;
			trap.Type = this.fType;
			trap.Name = this.fName;
			trap.Level = this.fLevel;
			trap.Role = this.fRole.Copy();
			trap.ReadAloud = this.fReadAloud;
			trap.Description = this.fDescription;
			trap.Details = this.fDetails;
			foreach (TrapSkillData current in this.fSkills)
			{
				trap.Skills.Add(current.Copy());
			}
			trap.Initiative = this.fInitiative;
			trap.Trigger = this.fTrigger;
			trap.Attack = ((this.fAttack != null) ? this.fAttack.Copy() : null);
			foreach (TrapAttack current2 in this.fAttacks)
			{
				trap.Attacks.Add(current2.Copy());
			}
			foreach (string current3 in this.fCountermeasures)
			{
				trap.Countermeasures.Add(current3);
			}
			trap.URL = this.fURL;
			return trap;
		}

		public override string ToString()
		{
			return this.fName;
		}

		public int CompareTo(Trap rhs)
		{
			return this.fName.CompareTo(rhs.Name);
		}

		public TrapSkillData FindSkill(string skillname)
		{
			foreach (TrapSkillData current in this.fSkills)
			{
				if (current.SkillName == skillname)
				{
					return current;
				}
			}
			return null;
		}

		public TrapSkillData FindSkill(Guid id)
		{
			foreach (TrapSkillData current in this.fSkills)
			{
				if (current.ID == id)
				{
					return current;
				}
			}
			return null;
		}

		public TrapAttack FindAttack(Guid id)
		{
			foreach (TrapAttack current in this.fAttacks)
			{
				if (current.ID == id)
				{
					return current;
				}
			}
			return null;
		}

		public void AdjustLevel(int delta)
		{
			this.fLevel += delta;
			this.fLevel = Math.Max(1, this.fLevel);
			if (this.fInitiative != int.MinValue)
			{
				this.Initiative += delta;
				this.fInitiative = Math.Max(1, this.fInitiative);
			}
			foreach (TrapAttack current in this.fAttacks)
			{
				if (current.Attack != null)
				{
					current.Attack.Bonus += delta;
					current.Attack.Bonus = Math.Max(1, current.Attack.Bonus);
				}
				string text = AI.ExtractDamage(current.OnHit);
				if (text != "")
				{
					DiceExpression diceExpression = DiceExpression.Parse(text);
					if (diceExpression != null)
					{
						DiceExpression diceExpression2 = diceExpression.Adjust(delta);
						if (diceExpression2 != null && diceExpression.ToString() != diceExpression2.ToString())
						{
							current.OnHit = current.OnHit.Replace(text, diceExpression2 + " damage");
						}
					}
				}
				string text2 = AI.ExtractDamage(current.OnMiss);
				if (text2 != "")
				{
					DiceExpression diceExpression3 = DiceExpression.Parse(text2);
					if (diceExpression3 != null)
					{
						DiceExpression diceExpression4 = diceExpression3.Adjust(delta);
						if (diceExpression4 != null && diceExpression3.ToString() != diceExpression4.ToString())
						{
							current.OnMiss = current.OnMiss.Replace(text2, diceExpression4 + " damage");
						}
					}
				}
				string text3 = AI.ExtractDamage(current.Effect);
				if (text3 != "")
				{
					DiceExpression diceExpression5 = DiceExpression.Parse(text3);
					if (diceExpression5 != null)
					{
						DiceExpression diceExpression6 = diceExpression5.Adjust(delta);
						if (diceExpression6 != null && diceExpression5.ToString() != diceExpression6.ToString())
						{
							current.Effect = current.Effect.Replace(text3, diceExpression6 + " damage");
						}
					}
				}
			}
			foreach (TrapSkillData current2 in this.fSkills)
			{
				current2.DC += delta;
			}
		}
	}
}
