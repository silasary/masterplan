using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class Disease : IPlayerOption
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private string fLevel = "";

		private string fDetails = "";

		private string fAttack = "";

		private string fImproveDC = "";

		private string fMaintainDC = "";

		private List<string> fLevels = new List<string>();

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

		public string Level
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

		public string Attack
		{
			get
			{
				return this.fAttack;
			}
			set
			{
				this.fAttack = value;
			}
		}

		public string ImproveDC
		{
			get
			{
				return this.fImproveDC;
			}
			set
			{
				this.fImproveDC = value;
			}
		}

		public string MaintainDC
		{
			get
			{
				return this.fMaintainDC;
			}
			set
			{
				this.fMaintainDC = value;
			}
		}

		public List<string> Levels
		{
			get
			{
				return this.fLevels;
			}
			set
			{
				this.fLevels = value;
			}
		}

		public Disease Copy()
		{
			Disease disease = new Disease();
			disease.ID = this.fID;
			disease.Name = this.fName;
			disease.Level = this.fLevel;
			disease.Details = this.fDetails;
			disease.Attack = this.fAttack;
			disease.ImproveDC = this.fImproveDC;
			disease.MaintainDC = this.fMaintainDC;
			disease.Levels.AddRange(this.fLevels);
			return disease;
		}

		public override string ToString()
		{
			return this.fName;
		}
	}
}
