using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class FiveByFiveColumn
	{
		private Guid fID = Guid.NewGuid();

		private string fName = "";

		private PlotPointColour fColour;

		private List<FiveByFiveItem> fItems = new List<FiveByFiveItem>();

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

		public PlotPointColour Colour
		{
			get
			{
				return this.fColour;
			}
			set
			{
				this.fColour = value;
			}
		}

		public List<FiveByFiveItem> Items
		{
			get
			{
				return this.fItems;
			}
			set
			{
				this.fItems = value;
			}
		}

		public FiveByFiveColumn Copy()
		{
			FiveByFiveColumn fiveByFiveColumn = new FiveByFiveColumn();
			fiveByFiveColumn.ID = this.fID;
			fiveByFiveColumn.Name = this.fName;
			fiveByFiveColumn.Colour = this.fColour;
			foreach (FiveByFiveItem current in this.fItems)
			{
				fiveByFiveColumn.Items.Add(current.Copy());
			}
			return fiveByFiveColumn;
		}
	}
}
