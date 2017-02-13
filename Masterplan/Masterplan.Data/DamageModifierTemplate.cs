using System;

namespace Masterplan.Data
{
	[Serializable]
	public class DamageModifierTemplate
	{
		private DamageType fType;

		private int fHeroicValue = -5;

		private int fParagonValue = -10;

		private int fEpicValue = -15;

		public DamageType Type
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

		public int HeroicValue
		{
			get
			{
				return this.fHeroicValue;
			}
			set
			{
				this.fHeroicValue = value;
			}
		}

		public int ParagonValue
		{
			get
			{
				return this.fParagonValue;
			}
			set
			{
				this.fParagonValue = value;
			}
		}

		public int EpicValue
		{
			get
			{
				return this.fEpicValue;
			}
			set
			{
				this.fEpicValue = value;
			}
		}

		public DamageModifierTemplate Copy()
		{
			return new DamageModifierTemplate
			{
				Type = this.fType,
				HeroicValue = this.fHeroicValue,
				ParagonValue = this.fParagonValue,
				EpicValue = this.fEpicValue
			};
		}

		public override string ToString()
		{
			if (this.fHeroicValue + this.fParagonValue + this.fEpicValue == 0)
			{
				return "Immune to " + this.fType.ToString().ToLower();
			}
			string text = (this.fHeroicValue < 0) ? "Resist" : "Vulnerable";
			int num = Math.Abs(this.fHeroicValue);
			int num2 = Math.Abs(this.fParagonValue);
			int num3 = Math.Abs(this.fEpicValue);
			return string.Concat(new object[]
			{
				text,
				" ",
				num,
				" / ",
				num2,
				" / ",
				num3,
				" ",
				this.fType.ToString().ToLower()
			});
		}
	}
}
