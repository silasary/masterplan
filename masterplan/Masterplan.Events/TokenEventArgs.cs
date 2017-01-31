using Masterplan.Data;
using System;

namespace Masterplan.Events
{
	public class TokenEventArgs : EventArgs
	{
		private IToken fToken;

		public IToken Token
		{
			get
			{
				return this.fToken;
			}
		}

		public TokenEventArgs(IToken token)
		{
			this.fToken = token;
		}
	}
}
