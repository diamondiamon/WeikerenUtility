
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Weikeren.Utility.WebTest.Filters
{
    public class ExceptionFilter : System.Web.Mvc.FilterAttribute, System.Web.Mvc.IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("当前控制器位置:" + filterContext.Controller.ToString());
            sb.AppendLine("当前Action位置:" + filterContext.HttpContext.Request.Url.AbsolutePath);
            sb.AppendLine("请求类型:" + filterContext.HttpContext.Request.HttpMethod);
            sb.AppendLine("当前GET参数:" + filterContext.HttpContext.Request.Url.Query);
            if (filterContext.HttpContext.Request.HttpMethod.ToUpper() == "POST")
            {
                sb.AppendLine("当前POST参数:" + filterContext.HttpContext.Request.Form.ToString());
            }
            Uri referrer = filterContext.HttpContext.Request.UrlReferrer;
            if (referrer != null)
            {
                sb.AppendLine("上一页面:" + referrer.ToString());
            }
            
            if (filterContext.Exception.InnerException != null)
            {
                sb.AppendLine(getInnerExceptionMessage(filterContext.Exception.InnerException));
            }
            sb.AppendLine("错误信息:" + filterContext.Exception.Message);
            sb.AppendLine("堆栈信息:" + filterContext.Exception.StackTrace);

           
            LogHelper.Instance.Error(sb.ToString());
        }



        private string getInnerExceptionMessage(Exception ex)
        {
            if (ex == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            if (ex.InnerException != null)
            {
                sb.AppendLine(getInnerExceptionMessage(ex.InnerException));
            }

            sb.AppendLine("    错误信息:" + ex.Message);
            sb.AppendLine("    堆栈信息:" + ex.StackTrace);

            return sb.ToString();

        }

    }
}