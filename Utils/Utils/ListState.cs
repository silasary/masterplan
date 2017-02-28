using System;
using System.Windows.Forms;

namespace Utils
{
    ///<summary>
    ///Class providing static methods for saving and restoring the visible items in a ListView control.
    ///</summary>
    public class ListState
	{
		private int fTopIndex = -1;

		private int fSelectedIndex = -1;

        ///<summary>
        ///Saves the state of a ListView control.
        ///The state can be re-applied to the listview by calling ListState.SetState.
        ///</summary>
        ///<param name="list">The ListView control.</param>
        ///<returns>Returns a ListState object containing the current state of the ListView control.</returns>
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

        ///<summary>
        ///Restores a saved view to a ListView control.
        ///</summary>
        ///<param name="list">The ListView control.</param>
        ///<param name="state">The ListState object containing the saved state to be reset.</param>
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
