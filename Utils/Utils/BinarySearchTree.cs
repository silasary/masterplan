using System;
using System.Collections.Generic;

namespace Utils
{
	public class BinarySearchTree<T> where T : IComparable<T>
	{
		private T fData = default(T);

		private BinarySearchTree<T> fLeft;

		private BinarySearchTree<T> fRight;

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

		public BinarySearchTree()
		{
		}

		public BinarySearchTree(T item)
		{
			this.fData = item;
		}

		public BinarySearchTree(IEnumerable<T> list)
		{
			this.Add(list);
		}

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

		public void Add(IEnumerable<T> list)
		{
			foreach (T current in list)
			{
				this.Add(current);
			}
		}

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
