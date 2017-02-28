using System;

namespace Utils
{
	[Serializable]
	public class Pair<T1, T2> : IComparable<Pair<T1, T2>>
	{
		private T1 fFirst = default(T1);

		private T2 fSecond = default(T2);

        ///<summary>
        ///The first part of the Pair.
        ///</summary>
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

        ///<summary>
        ///The second part of the Pair.
        ///</summary>
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

        ///<summary>
        ///Constructor which initialises the Pair object.
        ///</summary>
        ///<param name="first">The first part.</param>
        ///<param name="second">The second part.</param>
        public Pair(T1 first, T2 second)
		{
			this.First = first;
			this.Second = second;
		}

        ///<summary>
        ///Returns a string representation of the Pair object in the format [first], [second].
        ///</summary>
        ///<returns>Returns a string representation of the Pair object.</returns>
        public override string ToString()
		{
			return this.fFirst + ", " + this.fSecond;
		}

        ///<summary>
        ///Compares this Pair object to another by the contents of their First property.
        ///</summary>
        ///<param name="rhs">The Pair object to compare to.</param>
        ///<returns>Returns -1 if this object should be sorted before the other, +1 if it should be sorted after the other, or 0 if they are identical.</returns>
        public int CompareTo(Pair<T1, T2> rhs)
		{
			string text = this.fFirst.ToString();
			T1 first = rhs.First;
			string strB = first.ToString();
			return text.CompareTo(strB);
		}
	}
}
