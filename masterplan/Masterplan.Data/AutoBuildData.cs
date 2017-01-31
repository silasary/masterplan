using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class AutoBuildData
	{
		private Difficulty fDifficulty;

		private int fLevel = Session.Project.Party.Level;

		private int fSize = Session.Project.Party.Size;

		private string fType = "";

		private List<string> fCategories;

		private List<string> fKeywords = new List<string>();

		public Difficulty Difficulty
		{
			get
			{
				return this.fDifficulty;
			}
			set
			{
				this.fDifficulty = value;
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

		public int Size
		{
			get
			{
				return this.fSize;
			}
			set
			{
				this.fSize = value;
			}
		}

		public string Type
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

		public List<string> Categories
		{
			get
			{
				return this.fCategories;
			}
			set
			{
				this.fCategories = value;
			}
		}

		public List<string> Keywords
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

		public AutoBuildData Copy()
		{
			AutoBuildData autoBuildData = new AutoBuildData();
			autoBuildData.Difficulty = this.fDifficulty;
			autoBuildData.Level = this.fLevel;
			autoBuildData.Size = this.fSize;
			autoBuildData.Type = this.fType;
			if (this.fKeywords != null)
			{
				autoBuildData.Keywords = new List<string>();
				autoBuildData.Keywords.AddRange(this.fKeywords);
			}
			if (this.fCategories != null)
			{
				autoBuildData.Categories = new List<string>();
				autoBuildData.Categories.AddRange(this.fCategories);
			}
			return autoBuildData;
		}
	}
}
