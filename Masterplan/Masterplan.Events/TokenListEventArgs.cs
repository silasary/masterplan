using Masterplan.Data;
using System;
using System.Collections.Generic;

namespace Masterplan.Events
{
	public class TokenListEventArgs : EventArgs
	{
		private List<IToken> fTokenLink = new List<IToken>();

		public List<IToken> Tokens
		{
			get
			{
				return this.fTokenLink;
			}
		}

		public TokenListEventArgs(List<IToken> tokens)
		{
			this.fTokenLink = tokens;
		}
	}
}
