using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class EncyclopediaGroup : IEncyclopediaItem, IComparable<EncyclopediaGroup>
	{
		private string fName = "";

		private Guid fID = Guid.NewGuid();

		private List<Guid> fIDs = new List<Guid>();

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

		public List<Guid> EntryIDs
		{
			get
			{
				return this.fIDs;
			}
			set
			{
				this.fIDs = value;
			}
		}

		public EncyclopediaGroup Copy()
		{
			EncyclopediaGroup encyclopediaGroup = new EncyclopediaGroup();
			encyclopediaGroup.Name = this.fName;
			encyclopediaGroup.ID = this.fID;
			foreach (Guid current in this.fIDs)
			{
				encyclopediaGroup.EntryIDs.Add(current);
			}
			return encyclopediaGroup;
		}

		public int CompareTo(EncyclopediaGroup rhs)
		{
			return this.fName.CompareTo(rhs.Name);
		}
	}
}
