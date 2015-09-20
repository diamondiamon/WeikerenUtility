using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.PluginMvcWeb
{
    /// <summary>
    /// 任务输出日志
    /// </summary>
    public sealed class LogHelper
    {
        private static readonly LogHelper _instance = new LogHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        private LogHelper()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public static LogHelper Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            Task task = new Task(() =>
            {
                writeMessage("Error", message);
            });
            task.Start();
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message"></param>
        public void Error(Exception ex)
        {
            Task task = new Task(() =>
            {
                string message = getExceptionMessage(ex);
                Error(message);
            });
            task.Start();

        }
        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            //writeMessage("Info", message);
            Task task = new Task(() =>
            {
                writeMessage("Info", message);
            });
            task.Start();
        }
        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="message"></param>
        public void Warn(string message)
        {
            //writeMessage("Warn", message);
            Task task = new Task(() =>
            {
                writeMessage("Warn", message);
            });
            task.Start();
        }
        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            //writeMessage("Debug", message);
            Task task = new Task(() =>
            {
                writeMessage("Debug", message);
            });
            task.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        private static object obj = new object();

        /// <summary>
        /// 添加日志
        /// </summary>
        private void writeMessage(string type, string message)
        {
            lock (obj)
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

        /// <summary>
        /// 获取异常信息
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private string getExceptionMessage(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("错误信息:" + ex.Message);
            sb.AppendLine("堆栈信息:" + ex.StackTrace);

            return sb.ToString();
        }





    }
}
