using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Data
{
	[Serializable]
	public class Artifact
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private Tier fTier;

		private string fDescription = "";

		private string fDetails = "";

		private string fGoals = "";

		private string fRoleplayingTips = "";

		private List<MagicItemSection> fSections = new List<MagicItemSection>();

		private List<Pair<string, string>> fConcordanceRules = new List<Pair<string, string>>();

		private List<ArtifactConcordance> fConcordanceLevels = new List<ArtifactConcordance>();

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

		public Tier Tier
		{
			get
			{
				return this.fTier;
			}
			set
			{
				this.fTier = value;
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

		public string Goals
		{
			get
			{
				return this.fGoals;
			}
			set
			{
				this.fGoals = value;
			}
		}

		public string RoleplayingTips
		{
			get
			{
				return this.fRoleplayingTips;
			}
			set
			{
				this.fRoleplayingTips = value;
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

		public List<Pair<string, string>> ConcordanceRules
		{
			get
			{
				return this.fConcordanceRules;
			}
			set
			{
				this.fConcordanceRules = value;
			}
		}

		public List<ArtifactConcordance> ConcordanceLevels
		{
			get
			{
				return this.fConcordanceLevels;
			}
			set
			{
				this.fConcordanceLevels = value;
			}
		}

		public Artifact()
		{
			this.AddStandardConcordanceLevels();
		}

		public void AddStandardConcordanceLevels()
		{
			this.fConcordanceLevels.Add(new ArtifactConcordance("Pleased", "16-20"));
			this.fConcordanceLevels.Add(new ArtifactConcordance("Satisfied", "12-15"));
			this.fConcordanceLevels.Add(new ArtifactConcordance("Normal", "5-11"));
			this.fConcordanceLevels.Add(new ArtifactConcordance("Unsatisfied", "1-4"));
			this.fConcordanceLevels.Add(new ArtifactConcordance("Angered", "0 or lower"));
			this.fConcordanceLevels.Add(new ArtifactConcordance("Moving On", ""));
		}

		public Artifact Copy()
		{
			Artifact artifact = new Artifact();
			artifact.ID = this.fID;
			artifact.Name = this.fName;
			artifact.Tier = this.fTier;
			artifact.Description = this.fDescription;
			artifact.Details = this.fDetails;
			artifact.Goals = this.fGoals;
			artifact.RoleplayingTips = this.fRoleplayingTips;
			artifact.Sections.Clear();
			foreach (MagicItemSection current in this.fSections)
			{
				artifact.Sections.Add(current.Copy());
			}
			artifact.ConcordanceRules.Clear();
			foreach (Pair<string, string> current2 in this.fConcordanceRules)
			{
				Pair<string, string> item = new Pair<string, string>(current2.First, current2.Second);
				artifact.ConcordanceRules.Add(item);
			}
			artifact.ConcordanceLevels.Clear();
			foreach (ArtifactConcordance current3 in this.fConcordanceLevels)
			{
				artifact.ConcordanceLevels.Add(current3.Copy());
			}
			return artifact;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}
