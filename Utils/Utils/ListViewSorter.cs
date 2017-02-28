using System;
using System.Collections;
using System.Windows.Forms;

namespace Utils
{
    ///<summary>
    ///Provides methods for sorting ListView contents by column.
    ///An instance of this class should be set as the ListView's ListViewItemSorter property.
    ///</summary>
	public class ListViewSorter : IComparer
	{
		private int fColumn;

		private bool fAscending = true;

        ///<summary>
        ///Gets or sets a value indicating the column the ListView contents should be sorted by.
        ///</summary>
        public int Column
		{
			get
			{
				return this.fColumn;
			}
			set
			{
				this.fColumn = value;
			}
		}

        ///<summary>
        ///Gets or sets a value indicating whether the ListView contents should be sorted in ascending order.
        ///</summary>
        public bool Ascending
		{
			get
			{
				return this.fAscending;
			}
			set
			{
				this.fAscending = value;
			}
		}

        ///<summary>
        ///Sets the column used for sorting.
        ///If this method is called multiple times with the same column, the value of the Ascending property is toggled on and off.
        ///</summary>
        ///<param name="col">The column to be used for sorting.</param>
        public void SetColumn(int col)
		{
			if (this.fColumn == col)
			{
				this.fAscending = !this.fAscending;
				return;
			}
			this.fColumn = col;
			this.fAscending = true;
		}

        ///<summary>
        ///Compares two ListViewItem objects, given the values of the Column and Ascending properties.
        ///</summary>
        ///<param name="x">The first ListViewItem object to compare.</param>
        ///<param name="y">The second ListViewItem object to compare.</param>
        ///<returns>Returns -1 if x should be sorted before y, +1 if y should be sorted before x, and 0 if they are identical.</returns>
        public int Compare(object x, object y)
		{
			ListViewItem listViewItem = x as ListViewItem;
			ListViewItem listViewItem2 = y as ListViewItem;
			if (listViewItem == null || listViewItem2 == null)
			{
				throw new ArgumentException();
			}
			string text = listViewItem.SubItems[this.Column].Text;
			string text2 = listViewItem2.SubItems[this.Column].Text;
			try
			{
				int num = int.Parse(text);
				int value = int.Parse(text2);
				int result = num.CompareTo(value) * (this.fAscending ? 1 : -1);
				return result;
			}
			catch
			{
			}
			try
			{
				float num2 = float.Parse(text);
				float value2 = float.Parse(text2);
				int result = num2.CompareTo(value2) * (this.fAscending ? 1 : -1);
				return result;
			}
			catch
			{
			}
			try
			{
				DateTime dateTime = DateTime.Parse(text);
				DateTime value3 = DateTime.Parse(text2);
				int result = dateTime.CompareTo(value3) * (this.fAscending ? 1 : -1);
				return result;
			}
			catch
			{
			}
			return text.CompareTo(text2) * (this.fAscending ? 1 : -1);
		}

        ///<summary>
        ///Sorts the contents of a ListView control.
        ///This method should be called in response to a ListView.ColumnClicked event.
        ///</summary>
        ///<param name="list">The ListView control to be sorted.</param>
        ///<param name="column">The column to sort by.</param>
        public static void Sort(ListView list, int column)
		{
			ListViewSorter listViewSorter = list.ListViewItemSorter as ListViewSorter;
			if (listViewSorter != null)
			{
				listViewSorter.SetColumn(column);
				list.Sort();
			}
		}
	}
}
