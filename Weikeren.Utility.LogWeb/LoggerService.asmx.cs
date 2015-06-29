using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using Weikeren.Utility.LogWeb.Models;

namespace Weikeren.Utility.LogWeb
{
    /// <summary>
    /// LoggerService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class LoggerService : System.Web.Services.WebService
    {
        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="log"></param>
        [WebMethod(Description="写入错误日志")]
        public void ErrorMessage(ErrorLogger log)
        {
            wirteMessage(log);
        }

        /// <summary>
        /// 写入调试日志
        /// </summary>
        /// <param name="log"></param>
        [WebMethod(Description = "写入调试日志")]
        public void DebugMessage(DebugLogger log)
        {
            wirteMessage(log);
        }
        /// <summary>
        /// 写入消息日志
        /// </summary>
        /// <param name="log"></param>
        [WebMethod(Description = "写入消息日志")]
        public void InfoMessage(InfoLogger log)
        {
            wirteMessage(log);
        }
        /// <summary>
        /// 写入警告日志
        /// </summary>
        /// <param name="log"></param>
        [WebMethod(Description = "写入警告日志")]
        public void WarnMessage(WarnLogger log)
        {
            wirteMessage(log);
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="log"></param>
        private void wirteMessage(Logger log)
        {
            if (log == null)
                return;
            try
            {
                log.ExcuteWrite();
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("错误信息:" + ex.Message);
                sb.AppendLine("堆栈信息:" + ex.StackTrace);
                writeMessage("Error", sb.ToString());
            }

        }

        /// <summary>
        /// 添加日志
        /// </summary>
        private void writeMessage(string type, string message)
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string path = string.Format("{0}/AppLog/{1}", basePath.Trim('/'), type);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (StreamWriter sw = File.AppendText(string.Format(@"{0}/{1:yyyy_MM_dd}.log", path, DateTime.Now)))
                {
                    sw.Write("日志记录时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
                    sw.Write(message);
                    sw.Write("\r\n------------------------------------------------------------------------\r\n");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

    }
}
