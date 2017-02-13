using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class EncounterTemplateGroup
	{
		private string fCategory = "";

		private string fName = "";

		private List<EncounterTemplate> fTemplates = new List<EncounterTemplate>();

		public string Category
		{
			get
			{
				return this.fCategory;
			}
			set
			{
				this.fCategory = value;
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

		public List<EncounterTemplate> Templates
		{
			get
			{
				return this.fTemplates;
			}
			set
			{
				this.fTemplates = value;
			}
		}

		public EncounterTemplateGroup()
		{
		}

		public EncounterTemplateGroup(string name, string category)
		{
			this.fName = name;
			this.fCategory = category;
		}
	}
}
