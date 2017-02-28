using System;
using System.Collections.Generic;

namespace Utils
{
    ///<summary>
    ///Utility class for performing quick searches.
    ///</summary>
    ///<typeparam name="T">Type to create the tree for; must implement the IComparable interface.</typeparam>
    public class BinarySearchTree<T> where T : IComparable<T>
	{
		private T fData = default(T);

		private BinarySearchTree<T> fLeft;

		private BinarySearchTree<T> fRight;

        ///<summary>
        ///Gets the number of items in the tree.
        ///</summary>
        public int Count
		{
			get
			{
				if (this.fData == null)
				{
					return 0;
				}
				int num = 1;
				if (this.fLeft != null)
				{
					num += this.fLeft.Count;
				}
				if (this.fRight != null)
				{
					num += this.fRight.Count;
				}
				return num;
			}
		}

        ///<summary>
        ///Gets a List containing all the items in the tree in sorted order.
        ///</summary>
        public List<T> SortedList
		{
			get
			{
				List<T> list = new List<T>();
				if (this.fData != null)
				{
					if (this.fLeft != null)
					{
						list.AddRange(this.fLeft.SortedList);
					}
					list.Add(this.fData);
					if (this.fRight != null)
					{
						list.AddRange(this.fRight.SortedList);
					}
				}
				return list;
			}
		}

        ///<summary>
        ///Default constructor.
        ///</summary>
        public BinarySearchTree()
		{
		}

        ///<summary>
        ///Constructor.
        ///</summary>
        ///<param name="item">The item to begin the tree with.</param>
        public BinarySearchTree(T item)
		{
			this.fData = item;
		}

        ///<summary>
        ///Constructor.
        ///</summary>
        ///<param name="list">The list of items to build the tree with.</param>
        public BinarySearchTree(IEnumerable<T> list)
		{
			this.Add(list);
		}

        ///<summary>
        ///Adds an item to the tree.
        ///</summary>
        ///<param name="item">The item to add to the tree.</param>
        public void Add(T item)
		{
			if (this.fData == null)
			{
				this.fData = item;
				return;
			}
			int num = this.fData.CompareTo(item);
			if (num > 0)
			{
				if (this.fLeft == null)
				{
					this.fLeft = new BinarySearchTree<T>(item);
				}
				else
				{
					this.fLeft.Add(item);
				}
			}
			if (num < 0)
			{
				if (this.fRight == null)
				{
					this.fRight = new BinarySearchTree<T>(item);
					return;
				}
				this.fRight.Add(item);
			}
		}

        ///<summary>
        ///Adds a list of items to the tree.
        ///</summary>
        ///<param name="list">The items to add to the tree.</param>
        public void Add(IEnumerable<T> list)
		{
			foreach (T current in list)
			{
				this.Add(current);
			}
		}

        ///<summary>
        ///Searches the tree for the given item.
        ///</summary>
        ///<param name="item">The item to look for.</param>
        ///<returns>Returns true if the item is present in the tree; false otherwise.</returns>
        public bool Contains(T item)
		{
			if (this.fData == null)
			{
				return false;
			}
			int num = this.fData.CompareTo(item);
			if (num > 0)
			{
				return this.fLeft != null && this.fLeft.Contains(item);
			}
			return num >= 0 || (this.fRight != null && this.fRight.Contains(item));
		}
	}
}
