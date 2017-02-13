using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	internal class RoundLog
	{
		private int fRound;

		private List<TurnLog> fTurns = new List<TurnLog>();

		public int Round
		{
			get
			{
				return this.fRound;
			}
		}

		public List<TurnLog> Turns
		{
			get
			{
				return this.fTurns;
			}
		}

		public int Count
		{
			get
			{
				int num = 0;
				foreach (TurnLog current in this.fTurns)
				{
					num += current.Entries.Count;
				}
				return num;
			}
		}

		public RoundLog(int round)
		{
			this.fRound = round;
		}

		public TurnLog GetTurn(Guid id)
		{
			foreach (TurnLog current in this.fTurns)
			{
				if (current.ID == id)
				{
					return current;
				}
			}
			return null;
		}
	}
}
