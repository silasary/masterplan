using System;

namespace Masterplan.Tools
{
	internal class DiceExpression
	{
		private int fThrows;

		private int fSides;

		private int fConstant;

		public int Throws
		{
			get
			{
				return this.fThrows;
			}
			set
			{
				this.fThrows = value;
			}
		}

		public int Sides
		{
			get
			{
				return this.fSides;
			}
			set
			{
				this.fSides = value;
			}
		}

		public int Constant
		{
			get
			{
				return this.fConstant;
			}
			set
			{
				this.fConstant = value;
			}
		}

		public int Maximum
		{
			get
			{
				return this.fThrows * this.fSides + this.fConstant;
			}
		}

		public double Average
		{
			get
			{
				double num = (double)(this.fSides + 1) / 2.0;
				return (double)this.fThrows * num + (double)this.fConstant;
			}
		}

		public DiceExpression()
		{
			this.fThrows = 0;
			this.fSides = 0;
			this.fConstant = 0;
		}

		public DiceExpression(int throws, int sides)
		{
			this.fThrows = throws;
			this.fSides = sides;
			this.fConstant = 0;
		}

		public DiceExpression(int throws, int sides, int constant)
		{
			this.fThrows = throws;
			this.fSides = sides;
			this.fConstant = constant;
		}

		public static DiceExpression Parse(string diceNotation)
		{
			DiceExpression diceExpression = new DiceExpression();
			try
			{
				bool flag = false;
				bool minus = false;
				char[] digits = new char[]
				{
					'1',
					'2',
					'3',
					'4',
					'5',
					'6',
					'7',
					'8',
					'9',
					'0'
				};
				diceNotation = diceNotation.ToLower();
				diceNotation = diceNotation.Replace("+", " + ");
				diceNotation = diceNotation.Replace("-", " - ");
				string[] array = diceNotation.Split(null);
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i];
					if (text == "damage" || text == "dmg")
					{
						break;
					}
					if (text == "-" && flag)
					{
						minus = true;
					}
					else if (text.IndexOfAny(digits) != -1)
					{
						int num = text.IndexOf("d");
						if (num != -1)
						{
							string text2 = text.Substring(0, num);
							string s = text.Substring(num + 1);
							if (text2 != "")
							{
								diceExpression.Throws = int.Parse(text2);
							}
							diceExpression.Sides = int.Parse(s);
						}
						else if (diceExpression.Constant == 0)
						{
							diceExpression.Constant = int.Parse(text);
							if (minus)
							{
								diceExpression.Constant = -diceExpression.Constant;
							}
						}
						flag = true;
					}
				}
			}
			catch
			{
				diceExpression = null;
			}
			if (diceExpression != null && diceExpression.Throws == 0 && diceExpression.Constant == 0)
			{
				diceExpression = null;
			}
			return diceExpression;
		}

		public int Evaluate()
		{
			return Session.Dice(this.fThrows, this.fSides) + this.fConstant;
		}

		public override string ToString()
		{
			string text = "";
			if (this.fThrows != 0)
			{
				text = this.fThrows + "d" + this.fSides;
			}
			if (this.fConstant != 0)
			{
				if (text != "")
				{
					text += " ";
					if (this.fConstant > 0)
					{
						text += "+";
					}
				}
				text += this.fConstant.ToString();
			}
			if (text == "")
			{
				text = "0";
			}
			return text;
		}

		public DiceExpression Adjust(int level_adjustment)
		{
			Array values = Enum.GetValues(typeof(DamageExpressionType));
			int num = 2147483647;
			int num2 = 0;
			DamageExpressionType det = DamageExpressionType.Normal;
			DiceExpression diceExpression = null;
			for (int i = 1; i <= 30; i++)
			{
				foreach (DamageExpressionType damageExpressionType in values)
				{
					DiceExpression diceExpression2 = DiceExpression.Parse(Statistics.Damage(i, damageExpressionType));
					int num3 = Math.Abs(this.fThrows - diceExpression2.Throws);
					int num4 = Math.Abs(this.fSides - diceExpression2.Sides) / 2;
					int num5 = Math.Abs(this.fConstant - diceExpression2.Constant);
					int num6 = num3 * 10 + num4 * 100 + num5;
					if (num6 < num)
					{
						num = num6;
						num2 = i;
						det = damageExpressionType;
						diceExpression = diceExpression2;
					}
				}
			}
			if (diceExpression == null)
			{
				return this;
			}
			int num7 = this.fThrows - diceExpression.Throws;
			int num8 = this.fSides - diceExpression.Sides;
			int num9 = this.fConstant - diceExpression.Constant;
			int level = Math.Max(num2 + level_adjustment, 1);
			DiceExpression diceExpression3 = DiceExpression.Parse(Statistics.Damage(level, det));
			diceExpression3.Throws += num7;
			diceExpression3.Sides += num8;
			diceExpression3.Constant += num9;
			if (this.fThrows == 0)
			{
				diceExpression3.Throws = 0;
			}
			else
			{
				diceExpression3.Throws = Math.Max(diceExpression3.Throws, 1);
			}
			switch (diceExpression3.Sides)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
				diceExpression3.Sides = 4;
				break;
			case 5:
			case 6:
				diceExpression3.Sides = 6;
				break;
			case 7:
			case 8:
				diceExpression3.Sides = 8;
				break;
			case 9:
			case 10:
				diceExpression3.Sides = 10;
				break;
			case 11:
			case 12:
			case 13:
			case 14:
			case 15:
			case 16:
				diceExpression3.Sides = 12;
				break;
			default:
				diceExpression3.Sides = 20;
				break;
			}
			return diceExpression3;
		}
	}
}
