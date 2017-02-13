using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class ArtifactConcordance
	{
		private string fName = "";

		private string fValueRange = "";

		private string fQuote = "";

		private string fDescription = "";

		private List<MagicItemSection> fSections = new List<MagicItemSection>();

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

		public string ValueRange
		{
			get
			{
				return this.fValueRange;
			}
			set
			{
				this.fValueRange = value;
			}
		}

		public string Quote
		{
			get
			{
				return this.fQuote;
			}
			set
			{
				this.fQuote = value;
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

		public List<MagicItemSection> Sections
		{
			get
			{
				return this.fSections;
			}
			set
			{
				this.fSections = value;
			}
		}

		public ArtifactConcordance()
		{
		}

		public ArtifactConcordance(string name, string value_range)
		{
			this.fName = name;
			this.fValueRange = value_range;
		}

		public ArtifactConcordance Copy()
		{
            ArtifactConcordance artifactConcordance = new ArtifactConcordance()
            {
                Name = this.fName,
                ValueRange = this.fValueRange,
                Quote = this.fQuote,
                Description = this.fDescription
            };
            artifactConcordance.Sections.Clear();
			foreach (MagicItemSection current in this.fSections)
			{
				artifactConcordance.Sections.Add(current.Copy());
			}
			return artifactConcordance;
		}
	}
}
