using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class DamageModifier
	{
		private DamageType fType = DamageType.Fire;

		private int fValue = -5;

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

		public DamageModifier Copy()
		{
			return new DamageModifier
			{
				Type = this.fType,
				Value = this.fValue
			};
		}

		public override string ToString()
		{
			if (this.fValue == 0)
			{
				return "Immune to " + this.fType.ToString().ToLower();
			}
			string text = (this.fValue < 0) ? "Resist" : "Vulnerable";
			int num = Math.Abs(this.fValue);
			return string.Concat(new object[]
			{
				text,
				" ",
				num,
				" ",
				this.fType.ToString().ToLower()
			});
		}

		public static DamageModifier Parse(string damage_type, int value)
		{
			string[] names = Enum.GetNames(typeof(DamageType));
			List<string> list = new List<string>();
			string[] array = names;
			for (int i = 0; i < array.Length; i++)
			{
				string item = array[i];
				list.Add(item);
			}
			try
			{
				return new DamageModifier
				{
					Type = (DamageType)Enum.Parse(typeof(DamageType), damage_type, true),
					Value = value
				};
			}
			catch
			{
			}
			return null;
		}
	}
}
