using System;
using System.Windows.Forms;

namespace Utils
{
	public class ListState
	{
		private int fTopIndex = -1;

		private int fSelectedIndex = -1;

		public static ListState GetState(ListView list)
		{
			ListState listState = new ListState();
			listState.fTopIndex = list.Items.IndexOf(list.TopItem);
			listState.fSelectedIndex = -1;
			if (list.SelectedIndices.Count != 0)
			{
				listState.fSelectedIndex = list.SelectedIndices[0];
			}
			return listState;
		}

		public static void SetState(ListView list, ListState state)
		{
			if (state.fTopIndex != -1 && state.fTopIndex < list.Items.Count)
			{
				list.TopItem = list.Items[state.fTopIndex];
			}
			if (state.fSelectedIndex != -1 && state.fSelectedIndex < list.Items.Count)
			{
				list.SelectedIndices.Add(state.fSelectedIndex);
			}
		}
	}
}
