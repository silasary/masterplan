using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class FiveByFiveItem
	{
		private Guid fID = Guid.NewGuid();

		private string fDetails = "";

		private List<Guid> fLinkIDs = new List<Guid>();

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

		public List<Guid> LinkIDs
		{
			get
			{
				return this.fLinkIDs;
			}
			set
			{
				this.fLinkIDs = value;
			}
		}

		public FiveByFiveItem Copy()
		{
			FiveByFiveItem fiveByFiveItem = new FiveByFiveItem();
			fiveByFiveItem.ID = this.fID;
			fiveByFiveItem.Details = this.fDetails;
			foreach (Guid current in this.fLinkIDs)
			{
				fiveByFiveItem.LinkIDs.Add(current);
			}
			return fiveByFiveItem;
		}
	}
}
