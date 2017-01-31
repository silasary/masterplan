using Masterplan.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;

namespace Masterplan.Compendium
{
	[GeneratedCode("System.Web.Services", "2.0.50727.4927"), DesignerCategory("code"), DebuggerStepThrough, WebServiceBinding(Name = "CompendiumSearchSoap", Namespace = "http://ww2.wizards.com")]
	public class CompendiumSearch : SoapHttpClientProtocol
	{
		private SendOrPostCallback KeywordSearchOperationCompleted;

		private SendOrPostCallback KeywordSearchWithFiltersOperationCompleted;

		private SendOrPostCallback ViewAllOperationCompleted;

		private SendOrPostCallback GetFilterSelectOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;

        public event KeywordSearchCompletedEventHandler KeywordSearchCompleted;

        public event KeywordSearchWithFiltersCompletedEventHandler KeywordSearchWithFiltersCompleted;

        public event ViewAllCompletedEventHandler ViewAllCompleted;

        public event GetFilterSelectCompletedEventHandler GetFilterSelectCompleted;

		public new string Url
		{
			get
			{
				return base.Url;
			}
			set
			{
				if (this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value))
				{
					base.UseDefaultCredentials = false;
				}
				base.Url = value;
			}
		}

		public new bool UseDefaultCredentials
		{
			get
			{
				return base.UseDefaultCredentials;
			}
			set
			{
				base.UseDefaultCredentials = value;
				this.useDefaultCredentialsSetExplicitly = true;
			}
		}

		public CompendiumSearch()
		{
			this.Url = Settings.Default.Masterplan_Compendium_CompendiumSearch;
			if (this.IsLocalFileSystemWebService(this.Url))
			{
				this.UseDefaultCredentials = true;
				this.useDefaultCredentialsSetExplicitly = false;
				return;
			}
			this.useDefaultCredentialsSetExplicitly = true;
		}

		[SoapDocumentMethod("http://ww2.wizards.com/KeywordSearch", RequestNamespace = "http://ww2.wizards.com", ResponseNamespace = "http://ww2.wizards.com", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode KeywordSearch(string Keywords, string NameOnly, string Tab)
		{
			object[] array = base.Invoke("KeywordSearch", new object[]
			{
				Keywords,
				NameOnly,
				Tab
			});
			return (XmlNode)array[0];
		}

		public void KeywordSearchAsync(string Keywords, string NameOnly, string Tab)
		{
			this.KeywordSearchAsync(Keywords, NameOnly, Tab, null);
		}

		public void KeywordSearchAsync(string Keywords, string NameOnly, string Tab, object userState)
		{
			if (this.KeywordSearchOperationCompleted == null)
			{
				this.KeywordSearchOperationCompleted = new SendOrPostCallback(this.OnKeywordSearchOperationCompleted);
			}
			base.InvokeAsync("KeywordSearch", new object[]
			{
				Keywords,
				NameOnly,
				Tab
			}, this.KeywordSearchOperationCompleted, userState);
		}

		private void OnKeywordSearchOperationCompleted(object arg)
		{
			if (this.KeywordSearchCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.KeywordSearchCompleted(this, new KeywordSearchCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://ww2.wizards.com/KeywordSearchWithFilters", RequestNamespace = "http://ww2.wizards.com", ResponseNamespace = "http://ww2.wizards.com", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode KeywordSearchWithFilters(string Keywords, string Tab, string Filters, string NameOnly)
		{
			object[] array = base.Invoke("KeywordSearchWithFilters", new object[]
			{
				Keywords,
				Tab,
				Filters,
				NameOnly
			});
			return (XmlNode)array[0];
		}

		public void KeywordSearchWithFiltersAsync(string Keywords, string Tab, string Filters, string NameOnly)
		{
			this.KeywordSearchWithFiltersAsync(Keywords, Tab, Filters, NameOnly, null);
		}

		public void KeywordSearchWithFiltersAsync(string Keywords, string Tab, string Filters, string NameOnly, object userState)
		{
			if (this.KeywordSearchWithFiltersOperationCompleted == null)
			{
				this.KeywordSearchWithFiltersOperationCompleted = new SendOrPostCallback(this.OnKeywordSearchWithFiltersOperationCompleted);
			}
			base.InvokeAsync("KeywordSearchWithFilters", new object[]
			{
				Keywords,
				Tab,
				Filters,
				NameOnly
			}, this.KeywordSearchWithFiltersOperationCompleted, userState);
		}

		private void OnKeywordSearchWithFiltersOperationCompleted(object arg)
		{
			if (this.KeywordSearchWithFiltersCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.KeywordSearchWithFiltersCompleted(this, new KeywordSearchWithFiltersCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://ww2.wizards.com/ViewAll", RequestNamespace = "http://ww2.wizards.com", ResponseNamespace = "http://ww2.wizards.com", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode ViewAll(string Tab)
		{
			object[] array = base.Invoke("ViewAll", new object[]
			{
				Tab
			});
			return (XmlNode)array[0];
		}

		public void ViewAllAsync(string Tab)
		{
			this.ViewAllAsync(Tab, null);
		}

		public void ViewAllAsync(string Tab, object userState)
		{
			if (this.ViewAllOperationCompleted == null)
			{
				this.ViewAllOperationCompleted = new SendOrPostCallback(this.OnViewAllOperationCompleted);
			}
			base.InvokeAsync("ViewAll", new object[]
			{
				Tab
			}, this.ViewAllOperationCompleted, userState);
		}

		private void OnViewAllOperationCompleted(object arg)
		{
			if (this.ViewAllCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ViewAllCompleted(this, new ViewAllCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://ww2.wizards.com/GetFilterSelect", RequestNamespace = "http://ww2.wizards.com", ResponseNamespace = "http://ww2.wizards.com", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode GetFilterSelect()
		{
			object[] array = base.Invoke("GetFilterSelect", new object[0]);
			return (XmlNode)array[0];
		}

		public void GetFilterSelectAsync()
		{
			this.GetFilterSelectAsync(null);
		}

		public void GetFilterSelectAsync(object userState)
		{
			if (this.GetFilterSelectOperationCompleted == null)
			{
				this.GetFilterSelectOperationCompleted = new SendOrPostCallback(this.OnGetFilterSelectOperationCompleted);
			}
			base.InvokeAsync("GetFilterSelect", new object[0], this.GetFilterSelectOperationCompleted, userState);
		}

		private void OnGetFilterSelectOperationCompleted(object arg)
		{
			if (this.GetFilterSelectCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetFilterSelectCompleted(this, new GetFilterSelectCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		private bool IsLocalFileSystemWebService(string url)
		{
			if (url == null || url == string.Empty)
			{
				return false;
			}
			Uri uri = new Uri(url);
			return uri.Port >= 1024 && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0;
		}
	}
}
