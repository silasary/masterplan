using Masterplan.Data;
using System;
using System.Collections.Generic;

namespace Masterplan.Events
{
	public class CardListEventArgs : EventArgs
	{
		private List<EncounterCard> fCards = new List<EncounterCard>();

		public List<EncounterCard> Cards
		{
			get
			{
				return this.fCards;
			}
		}

		public CardListEventArgs(List<EncounterCard> cards)
		{
			this.fCards = cards;
		}
	}
}
