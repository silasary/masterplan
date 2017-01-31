using Masterplan.Data;
using System;

namespace Masterplan.Events
{
	public class CreatureEventArgs : EventArgs
	{
		private CreatureToken fToken;

		public CreatureToken Token
		{
			get
			{
				return this.fToken;
			}
		}

		public CreatureEventArgs(CreatureToken token)
		{
			this.fToken = token;
		}
	}
}
