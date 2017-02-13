using System;

namespace Masterplan.Data
{
	[Serializable]
	public class PowerAttack
	{
		private int fBonus;

		private DefenceType fDefence;

		public int Bonus
		{
			get
			{
				return this.fBonus;
			}
			set
			{
				this.fBonus = value;
			}
		}

		public DefenceType Defence
		{
			get
			{
				return this.fDefence;
			}
			set
			{
				this.fDefence = value;
			}
		}

		public PowerAttack Copy()
		{
			return new PowerAttack
			{
				Bonus = this.fBonus,
				Defence = this.fDefence
			};
		}

		public override string ToString()
		{
			string text = (this.fBonus >= 0) ? "+" : "";
			return string.Concat(new object[]
			{
				text,
				this.fBonus,
				" vs ",
				this.fDefence
			});
		}
	}
}
