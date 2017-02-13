using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class FiveByFiveData
	{
		private List<FiveByFiveColumn> fColumns = new List<FiveByFiveColumn>();

		public List<FiveByFiveColumn> Columns
		{
			get
			{
				return this.fColumns;
			}
			set
			{
				this.fColumns = value;
			}
		}

		public void Initialise()
		{
			this.fColumns.Clear();
			List<PlotPointColour> list = new List<PlotPointColour>();
			foreach (PlotPointColour item in Enum.GetValues(typeof(PlotPointColour)))
			{
				list.Add(item);
			}
			for (int num = 0; num != 5; num++)
			{
				int index = num % list.Count;
				PlotPointColour plotPointColour = list[index];
				FiveByFiveColumn fiveByFiveColumn = new FiveByFiveColumn();
				fiveByFiveColumn.Name = plotPointColour.ToString();
				fiveByFiveColumn.Colour = plotPointColour;
				this.fColumns.Add(fiveByFiveColumn);
				for (int i = 1; i <= 5; i++)
				{
					FiveByFiveItem fiveByFiveItem = new FiveByFiveItem();
					fiveByFiveItem.Details = fiveByFiveColumn.Name + " " + i;
					fiveByFiveColumn.Items.Add(fiveByFiveItem);
				}
			}
		}

		public FiveByFiveData Copy()
		{
			FiveByFiveData fiveByFiveData = new FiveByFiveData();
			foreach (FiveByFiveColumn current in this.fColumns)
			{
				fiveByFiveData.Columns.Add(current.Copy());
			}
			return fiveByFiveData;
		}
	}
}
