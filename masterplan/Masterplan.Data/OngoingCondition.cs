using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class OngoingCondition : IComparable<OngoingCondition>
	{
		private OngoingType fType;

		private string fData = "";

		private DamageType fDamageType;

		private int fValue = 2;

		private int fDefenceMod = 2;

		private List<DefenceType> fDefences = new List<DefenceType>();

		private Regeneration fRegeneration = new Regeneration();

		private DamageModifier fDamageModifier = new DamageModifier();

		private Aura fAura = new Aura();

		private DurationType fDuration = DurationType.SaveEnds;

		private Guid fDurationCreatureID = Guid.Empty;

		private int fDurationRound = -2147483648;

		private int fSavingThrowModifier;

		public OngoingType Type
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

		public string Data
		{
			get
			{
				return this.fData;
			}
			set
			{
				this.fData = value;
			}
		}

		public DamageType DamageType
		{
			get
			{
				return this.fDamageType;
			}
			set
			{
				this.fDamageType = value;
			}
		}

		public int Value
		{
			get
			{
				return this.fValue;
			}
			set
			{
				this.fValue = value;
			}
		}

		public int DefenceMod
		{
			get
			{
				return this.fDefenceMod;
			}
			set
			{
				this.fDefenceMod = value;
			}
		}

		public List<DefenceType> Defences
		{
			get
			{
				return this.fDefences;
			}
			set
			{
				this.fDefences = value;
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

		public DamageModifier DamageModifier
		{
			get
			{
				return this.fDamageModifier;
			}
			set
			{
				this.fDamageModifier = value;
			}
		}

		public Aura Aura
		{
			get
			{
				return this.fAura;
			}
			set
			{
				this.fAura = value;
			}
		}

		public DurationType Duration
		{
			get
			{
				return this.fDuration;
			}
			set
			{
				this.fDuration = value;
			}
		}

		public Guid DurationCreatureID
		{
			get
			{
				return this.fDurationCreatureID;
			}
			set
			{
				this.fDurationCreatureID = value;
			}
		}

		public int DurationRound
		{
			get
			{
				return this.fDurationRound;
			}
			set
			{
				this.fDurationRound = value;
			}
		}

		public int SavingThrowModifier
		{
			get
			{
				return this.fSavingThrowModifier;
			}
			set
			{
				this.fSavingThrowModifier = value;
			}
		}

		public string GetDuration(Encounter enc)
		{
			string text = "";
			switch (this.fDuration)
			{
			case DurationType.SaveEnds:
				text = "save ends";
				if (this.SavingThrowModifier != 0)
				{
					string text2 = (this.SavingThrowModifier >= 0) ? "+" : "";
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
						" with ",
						text2,
						this.SavingThrowModifier,
						" mod"
					});
				}
				break;
			case DurationType.BeginningOfTurn:
			{
				string str;
				if (this.fDurationCreatureID == Guid.Empty)
				{
					str = "someone else's";
				}
				else if (enc != null)
				{
					str = enc.WhoIs(this.fDurationCreatureID) + "'s";
				}
				else
				{
					str = "my";
				}
				text = text + "until the start of " + str + " next turn";
				break;
			}
			case DurationType.EndOfTurn:
			{
				string str2;
				if (this.fDurationCreatureID == Guid.Empty)
				{
					str2 = "someone else's";
				}
				else if (enc != null)
				{
					str2 = enc.WhoIs(this.fDurationCreatureID) + "'s";
				}
				else
				{
					str2 = "my";
				}
				text = text + "until the end of " + str2 + " next turn";
				break;
			}
			}
			return text;
		}

		public string ToString(Encounter enc, bool html)
		{
			string text = this.ToString();
			if (html)
			{
				text = "<B>" + text + "</B>";
			}
			string duration = this.GetDuration(enc);
			if (duration != "")
			{
				text = text + " (" + duration + ")";
			}
			return text;
		}

		public override string ToString()
		{
			string text = "";
			switch (this.fType)
			{
			case OngoingType.Condition:
				text = this.fData;
				break;
			case OngoingType.Damage:
				if (this.fDamageType == DamageType.Untyped)
				{
					text = this.fValue + " ongoing damage";
				}
				else
				{
					string text2 = this.fDamageType.ToString().ToLower();
					text = string.Concat(new object[]
					{
						this.fValue,
						" ongoing ",
						text2,
						" damage"
					});
				}
				break;
			case OngoingType.DefenceModifier:
			{
				text = this.fDefenceMod.ToString();
				if (this.fDefenceMod >= 0)
				{
					text = "+" + text;
				}
				string text3 = "";
				if (this.fDefences.Count == 4)
				{
					text3 = "defences";
				}
				else
				{
					foreach (DefenceType current in this.fDefences)
					{
						if (text3 != "")
						{
							text3 += ", ";
						}
						text3 += current.ToString();
					}
				}
				text = text + " to " + text3;
				break;
			}
			case OngoingType.DamageModifier:
				text = this.fDamageModifier.ToString();
				break;
			case OngoingType.Regeneration:
				text = "Regeneration " + this.fRegeneration.Value;
				break;
			case OngoingType.Aura:
				text = string.Concat(new object[]
				{
					"Aura ",
					this.fAura.Radius,
					": ",
					this.fAura.Description
				});
				break;
			}
			return text;
		}

		public OngoingCondition Copy()
		{
			OngoingCondition ongoingCondition = new OngoingCondition();
			ongoingCondition.Type = this.fType;
			ongoingCondition.Data = this.fData;
			ongoingCondition.DamageType = this.fDamageType;
			ongoingCondition.Value = this.fValue;
			ongoingCondition.DefenceMod = this.fDefenceMod;
			ongoingCondition.Defences = new List<DefenceType>();
			foreach (DefenceType current in this.fDefences)
			{
				ongoingCondition.fDefences.Add(current);
			}
			ongoingCondition.Regeneration = ((this.fRegeneration != null) ? this.fRegeneration.Copy() : null);
			ongoingCondition.DamageModifier = ((this.fDamageModifier != null) ? this.fDamageModifier.Copy() : null);
			ongoingCondition.Aura = ((this.fAura != null) ? this.fAura.Copy() : null);
			ongoingCondition.Duration = this.fDuration;
			ongoingCondition.DurationCreatureID = this.fDurationCreatureID;
			ongoingCondition.DurationRound = this.fDurationRound;
			ongoingCondition.SavingThrowModifier = this.fSavingThrowModifier;
			return ongoingCondition;
		}

		public int CompareTo(OngoingCondition rhs)
		{
			return this.ToString().CompareTo(rhs.ToString());
		}
	}
}
