﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace Masterplan.Compendium {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="CompendiumSearchSoap", Namespace="http://ww2.wizards.com")]
    public partial class CompendiumSearch : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback KeywordSearchOperationCompleted;
        
        private System.Threading.SendOrPostCallback KeywordSearchWithFiltersOperationCompleted;
        
        private System.Threading.SendOrPostCallback ViewAllOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetFilterSelectOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public CompendiumSearch() {
            this.Url = global::Masterplan.Properties.Settings.Default.Masterplan_Compendium_CompendiumSearch;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event KeywordSearchCompletedEventHandler KeywordSearchCompleted;
        
        /// <remarks/>
        public event KeywordSearchWithFiltersCompletedEventHandler KeywordSearchWithFiltersCompleted;
        
        /// <remarks/>
        public event ViewAllCompletedEventHandler ViewAllCompleted;
        
        /// <remarks/>
        public event GetFilterSelectCompletedEventHandler GetFilterSelectCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ww2.wizards.com/KeywordSearch", RequestNamespace="http://ww2.wizards.com", ResponseNamespace="http://ww2.wizards.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode KeywordSearch(string Keywords, string NameOnly, string Tab) {
            object[] results = this.Invoke("KeywordSearch", new object[] {
                        Keywords,
                        NameOnly,
                        Tab});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void KeywordSearchAsync(string Keywords, string NameOnly, string Tab) {
            this.KeywordSearchAsync(Keywords, NameOnly, Tab, null);
        }
        
        /// <remarks/>
        public void KeywordSearchAsync(string Keywords, string NameOnly, string Tab, object userState) {
            if ((this.KeywordSearchOperationCompleted == null)) {
                this.KeywordSearchOperationCompleted = new System.Threading.SendOrPostCallback(this.OnKeywordSearchOperationCompleted);
            }
            this.InvokeAsync("KeywordSearch", new object[] {
                        Keywords,
                        NameOnly,
                        Tab}, this.KeywordSearchOperationCompleted, userState);
        }
        
        private void OnKeywordSearchOperationCompleted(object arg) {
            if ((this.KeywordSearchCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.KeywordSearchCompleted(this, new KeywordSearchCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ww2.wizards.com/KeywordSearchWithFilters", RequestNamespace="http://ww2.wizards.com", ResponseNamespace="http://ww2.wizards.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode KeywordSearchWithFilters(string Keywords, string Tab, string Filters, string NameOnly) {
            object[] results = this.Invoke("KeywordSearchWithFilters", new object[] {
                        Keywords,
                        Tab,
                        Filters,
                        NameOnly});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void KeywordSearchWithFiltersAsync(string Keywords, string Tab, string Filters, string NameOnly) {
            this.KeywordSearchWithFiltersAsync(Keywords, Tab, Filters, NameOnly, null);
        }
        
        /// <remarks/>
        public void KeywordSearchWithFiltersAsync(string Keywords, string Tab, string Filters, string NameOnly, object userState) {
            if ((this.KeywordSearchWithFiltersOperationCompleted == null)) {
                this.KeywordSearchWithFiltersOperationCompleted = new System.Threading.SendOrPostCallback(this.OnKeywordSearchWithFiltersOperationCompleted);
            }
            this.InvokeAsync("KeywordSearchWithFilters", new object[] {
                        Keywords,
                        Tab,
                        Filters,
                        NameOnly}, this.KeywordSearchWithFiltersOperationCompleted, userState);
        }
        
        private void OnKeywordSearchWithFiltersOperationCompleted(object arg) {
            if ((this.KeywordSearchWithFiltersCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.KeywordSearchWithFiltersCompleted(this, new KeywordSearchWithFiltersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ww2.wizards.com/ViewAll", RequestNamespace="http://ww2.wizards.com", ResponseNamespace="http://ww2.wizards.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode ViewAll(string Tab) {
            object[] results = this.Invoke("ViewAll", new object[] {
                        Tab});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void ViewAllAsync(string Tab) {
            this.ViewAllAsync(Tab, null);
        }
        
        /// <remarks/>
        public void ViewAllAsync(string Tab, object userState) {
            if ((this.ViewAllOperationCompleted == null)) {
                this.ViewAllOperationCompleted = new System.Threading.SendOrPostCallback(this.OnViewAllOperationCompleted);
            }
            this.InvokeAsync("ViewAll", new object[] {
                        Tab}, this.ViewAllOperationCompleted, userState);
        }
        
        private void OnViewAllOperationCompleted(object arg) {
            if ((this.ViewAllCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ViewAllCompleted(this, new ViewAllCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ww2.wizards.com/GetFilterSelect", RequestNamespace="http://ww2.wizards.com", ResponseNamespace="http://ww2.wizards.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetFilterSelect() {
            object[] results = this.Invoke("GetFilterSelect", new object[0]);
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void GetFilterSelectAsync() {
            this.GetFilterSelectAsync(null);
        }
        
        /// <remarks/>
        public void GetFilterSelectAsync(object userState) {
            if ((this.GetFilterSelectOperationCompleted == null)) {
                this.GetFilterSelectOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFilterSelectOperationCompleted);
            }
            this.InvokeAsync("GetFilterSelect", new object[0], this.GetFilterSelectOperationCompleted, userState);
        }
        
        private void OnGetFilterSelectOperationCompleted(object arg) {
            if ((this.GetFilterSelectCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFilterSelectCompleted(this, new GetFilterSelectCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void KeywordSearchCompletedEventHandler(object sender, KeywordSearchCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class KeywordSearchCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal KeywordSearchCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void KeywordSearchWithFiltersCompletedEventHandler(object sender, KeywordSearchWithFiltersCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class KeywordSearchWithFiltersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal KeywordSearchWithFiltersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void ViewAllCompletedEventHandler(object sender, ViewAllCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ViewAllCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ViewAllCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void GetFilterSelectCompletedEventHandler(object sender, GetFilterSelectCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFilterSelectCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFilterSelectCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591