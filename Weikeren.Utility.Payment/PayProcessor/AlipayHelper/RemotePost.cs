using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace Weikeren.Utility.Payment.PayProcessor.AlipayHelper
{
    public class RemotePost
    {
        private readonly HttpContextBase _httpContext;
        private readonly NameValueCollection _inputValues;

        public string Url { get; set; }
        public string Method { get; set; }
        public string FormName { get; set; }
        public string AcceptCharset { get; set; }
        public bool NewInputForEachValue { get; set; }

        public NameValueCollection Params
        {
            get
            {
                return _inputValues;
            }
        }
        public RemotePost()
            :this(HttpContext.Current!=null ? new HttpContextWrapper(HttpContext.Current) as HttpContextBase : (new FakeHttpContext("~/") as HttpContextBase))
        {
        
        }

        public RemotePost(HttpContextBase httpContextBase)
        {
            this._inputValues = new NameValueCollection();
            this.Url = "";
            this.Method = "post";
            this.FormName = "paymentForm1";

            this._httpContext = httpContextBase;
        }

        public void Add(string name, string value)
        {
            _inputValues.Add(name, value);
        }

        public void Post()
        {
            StringBuilder sb = new StringBuilder();
            _httpContext.Response.Clear();
            sb.AppendLine("<html><head>");
            sb.AppendLine(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));            
            if (!string.IsNullOrEmpty(AcceptCharset))
            {
                sb.AppendLine(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" accept-charset=\"{3}\">", FormName, Method, Url, AcceptCharset));
            }
            else
            {
                sb.AppendLine(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
            }
            if (NewInputForEachValue)
            {
                foreach (string key in _inputValues.Keys)
                {
                    string[] values = _inputValues.GetValues(key);
                    if (values != null)
                    {
                        foreach (string value in values)
                        {
                            sb.AppendLine(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode(key), HttpUtility.HtmlEncode(value)));
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < _inputValues.Keys.Count; i++)
                    sb.AppendLine(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode(_inputValues.Keys[i]), HttpUtility.HtmlEncode(_inputValues[_inputValues.Keys[i]])));
            }
            sb.AppendLine("如果您的浏览器没有自动跳转，请点击<input type=\"submit\" value=\"确认付款\">");
            sb.AppendLine("</form>");
            sb.AppendLine("</body></html>");

            _httpContext.Response.Write(sb.ToString());
            _httpContext.Response.End();
        }

    }
}
