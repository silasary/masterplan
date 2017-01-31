using System;

namespace Utils
{
	[Serializable]
	public class Pair<T1, T2> : IComparable<Pair<T1, T2>>
	{
		private T1 fFirst = default(T1);

		private T2 fSecond = default(T2);

		public T1 First
		{
			get
			{
				return this.fFirst;
			}
			set
			{
				this.fFirst = value;
			}
		}

		public T2 Second
		{
			get
			{
				return this.fSecond;
			}
			set
			{
				this.fSecond = value;
			}
		}

		public Pair()
		{
		}

		public Pair(T1 first, T2 second)
		{
			this.First = first;
			this.Second = second;
		}

		public override string ToString()
		{
			return this.fFirst + ", " + this.fSecond;
		}

		public int CompareTo(Pair<T1, T2> rhs)
		{
			string text = this.fFirst.ToString();
			T1 first = rhs.First;
			string strB = first.ToString();
			return text.CompareTo(strB);
		}
	}
}
