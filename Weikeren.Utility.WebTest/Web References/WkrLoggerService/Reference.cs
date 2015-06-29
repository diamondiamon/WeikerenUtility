﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34011
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.34011 版自动生成。
// 
#pragma warning disable 1591

namespace Weikeren.Utility.WebTest.WkrLoggerService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="LoggerServiceSoap", Namespace="http://tempuri.org/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Logger))]
    public partial class LoggerService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ErrorMessageOperationCompleted;
        
        private System.Threading.SendOrPostCallback DebugMessageOperationCompleted;
        
        private System.Threading.SendOrPostCallback InfoMessageOperationCompleted;
        
        private System.Threading.SendOrPostCallback WarnMessageOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public LoggerService() {
            this.Url = global::Weikeren.Utility.WebTest.Properties.Settings.Default.Weikeren_Utility_WebTest_WkrLoggerService_LoggerService;
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
        public event ErrorMessageCompletedEventHandler ErrorMessageCompleted;
        
        /// <remarks/>
        public event DebugMessageCompletedEventHandler DebugMessageCompleted;
        
        /// <remarks/>
        public event InfoMessageCompletedEventHandler InfoMessageCompleted;
        
        /// <remarks/>
        public event WarnMessageCompletedEventHandler WarnMessageCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ErrorMessage", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void ErrorMessage(ErrorLogger log) {
            this.Invoke("ErrorMessage", new object[] {
                        log});
        }
        
        /// <remarks/>
        public void ErrorMessageAsync(ErrorLogger log) {
            this.ErrorMessageAsync(log, null);
        }
        
        /// <remarks/>
        public void ErrorMessageAsync(ErrorLogger log, object userState) {
            if ((this.ErrorMessageOperationCompleted == null)) {
                this.ErrorMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnErrorMessageOperationCompleted);
            }
            this.InvokeAsync("ErrorMessage", new object[] {
                        log}, this.ErrorMessageOperationCompleted, userState);
        }
        
        private void OnErrorMessageOperationCompleted(object arg) {
            if ((this.ErrorMessageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ErrorMessageCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DebugMessage", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void DebugMessage(DebugLogger log) {
            this.Invoke("DebugMessage", new object[] {
                        log});
        }
        
        /// <remarks/>
        public void DebugMessageAsync(DebugLogger log) {
            this.DebugMessageAsync(log, null);
        }
        
        /// <remarks/>
        public void DebugMessageAsync(DebugLogger log, object userState) {
            if ((this.DebugMessageOperationCompleted == null)) {
                this.DebugMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDebugMessageOperationCompleted);
            }
            this.InvokeAsync("DebugMessage", new object[] {
                        log}, this.DebugMessageOperationCompleted, userState);
        }
        
        private void OnDebugMessageOperationCompleted(object arg) {
            if ((this.DebugMessageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DebugMessageCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/InfoMessage", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void InfoMessage(InfoLogger log) {
            this.Invoke("InfoMessage", new object[] {
                        log});
        }
        
        /// <remarks/>
        public void InfoMessageAsync(InfoLogger log) {
            this.InfoMessageAsync(log, null);
        }
        
        /// <remarks/>
        public void InfoMessageAsync(InfoLogger log, object userState) {
            if ((this.InfoMessageOperationCompleted == null)) {
                this.InfoMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnInfoMessageOperationCompleted);
            }
            this.InvokeAsync("InfoMessage", new object[] {
                        log}, this.InfoMessageOperationCompleted, userState);
        }
        
        private void OnInfoMessageOperationCompleted(object arg) {
            if ((this.InfoMessageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.InfoMessageCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/WarnMessage", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void WarnMessage(WarnLogger log) {
            this.Invoke("WarnMessage", new object[] {
                        log});
        }
        
        /// <remarks/>
        public void WarnMessageAsync(WarnLogger log) {
            this.WarnMessageAsync(log, null);
        }
        
        /// <remarks/>
        public void WarnMessageAsync(WarnLogger log, object userState) {
            if ((this.WarnMessageOperationCompleted == null)) {
                this.WarnMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnWarnMessageOperationCompleted);
            }
            this.InvokeAsync("WarnMessage", new object[] {
                        log}, this.WarnMessageOperationCompleted, userState);
        }
        
        private void OnWarnMessageOperationCompleted(object arg) {
            if ((this.WarnMessageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.WarnMessageCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class ErrorLogger : Logger {
        
        private string senderClassNameField;
        
        private string pathField;
        
        private string urlParametersField;
        
        private string stackTraceField;
        
        private string innerExceptionField;
        
        /// <remarks/>
        public string SenderClassName {
            get {
                return this.senderClassNameField;
            }
            set {
                this.senderClassNameField = value;
            }
        }
        
        /// <remarks/>
        public string Path {
            get {
                return this.pathField;
            }
            set {
                this.pathField = value;
            }
        }
        
        /// <remarks/>
        public string UrlParameters {
            get {
                return this.urlParametersField;
            }
            set {
                this.urlParametersField = value;
            }
        }
        
        /// <remarks/>
        public string StackTrace {
            get {
                return this.stackTraceField;
            }
            set {
                this.stackTraceField = value;
            }
        }
        
        /// <remarks/>
        public string InnerException {
            get {
                return this.innerExceptionField;
            }
            set {
                this.innerExceptionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(WarnLogger))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(InfoLogger))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DebugLogger))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ErrorLogger))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Logger {
        
        private int idField;
        
        private System.DateTime logTimeField;
        
        private System.DateTime logDateField;
        
        private string messageField;
        
        private string remarkField;
        
        /// <remarks/>
        public int Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime LogTime {
            get {
                return this.logTimeField;
            }
            set {
                this.logTimeField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime LogDate {
            get {
                return this.logDateField;
            }
            set {
                this.logDateField = value;
            }
        }
        
        /// <remarks/>
        public string Message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }
        
        /// <remarks/>
        public string Remark {
            get {
                return this.remarkField;
            }
            set {
                this.remarkField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class WarnLogger : Logger {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class InfoLogger : Logger {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class DebugLogger : Logger {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void ErrorMessageCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void DebugMessageCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void InfoMessageCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void WarnMessageCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591