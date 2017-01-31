using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Data
{
	[Serializable]
	public class CreatureLore : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fSkillName = "";

		private List<Pair<int, string>> fInformation = new List<Pair<int, string>>();

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

		public string SkillName
		{
			get
			{
				return this.fSkillName;
			}
			set
			{
				this.fSkillName = value;
			}
		}

		public List<Pair<int, string>> Information
		{
			get
			{
				return this.fInformation;
			}
			set
			{
				this.fInformation = value;
			}
		}

		public CreatureLore Copy()
		{
			CreatureLore creatureLore = new CreatureLore();
			creatureLore.ID = this.fID;
			creatureLore.Name = this.fName;
			creatureLore.SkillName = this.fSkillName;
			foreach (Pair<int, string> current in this.fInformation)
			{
				creatureLore.Information.Add(new Pair<int, string>(current.First, current.Second));
			}
			return creatureLore;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}
