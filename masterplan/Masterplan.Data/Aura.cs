using System;

namespace Masterplan.Data
{
	[Serializable]
	public class Aura
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fKeywords = "";

		private string fDetails = "";

		private bool fExtractedData;

		private int fRadius = -2147483648;

		private string fDescription = "";

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

		public string Keywords
		{
			get
			{
				return this.fKeywords;
			}
			set
			{
				this.fKeywords = value;
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
				this.extract();
			}
		}

		internal string Description
		{
			get
			{
				if (!this.fExtractedData)
				{
					this.extract();
				}
				return this.fDescription;
			}
		}

		internal int Radius
		{
			get
			{
				if (!this.fExtractedData)
				{
					this.extract();
				}
				return this.fRadius;
			}
		}

		public Aura Copy()
		{
			return new Aura
			{
				ID = this.fID,
				Name = this.fName,
				Keywords = this.fKeywords,
				Details = this.fDetails
			};
		}

		private void extract()
		{
			string text = "";
			for (int num = 0; num != this.fDetails.Length; num++)
			{
				char c = this.fDetails[num];
				bool flag = char.IsDigit(c);
				if (!flag && text != "")
				{
					this.fDescription = this.fDetails.Substring(num);
					break;
				}
				if (flag)
				{
					text += c;
				}
			}
			int num2 = 1;
			try
			{
				num2 = int.Parse(text);
			}
			catch
			{
				num2 = 1;
			}
			if (this.fDescription == null)
			{
				this.fDescription = "";
			}
			else
			{
				if (this.fDescription.StartsWith(":"))
				{
					this.fDescription = this.fDescription.Substring(1);
				}
				this.fDescription = this.fDescription.Trim();
			}
			this.fRadius = num2;
			this.fExtractedData = true;
		}
	}
}
