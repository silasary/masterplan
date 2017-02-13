using Masterplan.Data;
using System;

namespace Masterplan.Events
{
	public class CardEventArgs : EventArgs
	{
		private EncounterCard fCard;

		public EncounterCard Card
		{
			get
			{
				return this.fCard;
			}
		}

		public CardEventArgs(EncounterCard card)
		{
			this.fCard = card;
		}
	}
}
