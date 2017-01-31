using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class TokenLink
	{
		private string fText = "";

		private List<IToken> fTokens = new List<IToken>();

		public string Text
		{
			get
			{
				return this.fText;
			}
			set
			{
				this.fText = value;
			}
		}

		public List<IToken> Tokens
		{
			get
			{
				return this.fTokens;
			}
			set
			{
				this.fTokens = value;
			}
		}

		public TokenLink Copy()
		{
			TokenLink tokenLink = new TokenLink();
			tokenLink.Text = this.fText;
			foreach (IToken current in this.fTokens)
			{
				tokenLink.Tokens.Add(current);
			}
			return tokenLink;
		}
	}
}
