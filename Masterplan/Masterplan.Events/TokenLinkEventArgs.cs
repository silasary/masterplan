using Masterplan.Data;
using System;

namespace Masterplan.Events
{
	public class TokenLinkEventArgs : EventArgs
	{
		private TokenLink fLink;

		public TokenLink Link
		{
			get
			{
				return this.fLink;
			}
		}

		public TokenLinkEventArgs(TokenLink link)
		{
			this.fLink = link;
		}
	}
}
