using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Poison : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private int fLevel = 1;

		private string fDetails = "";

		private List<PlayerPowerSection> fSections = new List<PlayerPowerSection>();

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

		public List<PlayerPowerSection> Sections
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

		public Poison Copy()
		{
			Poison poison = new Poison();
			poison.ID = this.fID;
			poison.Name = this.fName;
			poison.Level = this.fLevel;
			poison.Details = this.fDetails;
			foreach (PlayerPowerSection current in this.fSections)
			{
				poison.Sections.Add(current.Copy());
			}
			return poison;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}
