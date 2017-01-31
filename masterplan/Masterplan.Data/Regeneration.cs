using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Regeneration
	{
		private int fValue = 2;

		private string fDetails = "";

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

		public Regeneration()
		{
		}

		public Regeneration(int value, string details)
		{
			this.fValue = value;
			this.fDetails = details;
		}

		public override string ToString()
		{
			string text = this.fValue.ToString();
			if (this.fDetails != "")
			{
				bool flag = text != "";
				if (flag)
				{
					text += " (";
				}
				text += this.fDetails;
				if (flag)
				{
					text += ")";
				}
			}
			return text;
		}

		public Regeneration Copy()
		{
			return new Regeneration
			{
				Value = this.fValue,
				Details = this.fDetails
			};
		}
	}
}
