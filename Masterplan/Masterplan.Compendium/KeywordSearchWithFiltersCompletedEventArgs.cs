using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Masterplan.Compendium
{
	[GeneratedCode("System.Web.Services", "2.0.50727.4927"), DesignerCategory("code"), DebuggerStepThrough]
	public class KeywordSearchWithFiltersCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public XmlNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (XmlNode)this.results[0];
			}
		}

		internal KeywordSearchWithFiltersCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
