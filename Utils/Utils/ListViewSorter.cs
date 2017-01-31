using System;
using System.Collections;
using System.Windows.Forms;

namespace Utils
{
	public class ListViewSorter : IComparer
	{
		private int fColumn;

		private bool fAscending = true;

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
