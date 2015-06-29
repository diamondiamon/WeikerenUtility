using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Weikeren.Utility.TimingTask
{
    /// <summary>
    /// 任务输出日志
    /// </summary>
    internal sealed class TaskLogger
    {
        private static readonly TaskLogger _instance = new TaskLogger();
        /// <summary>
        /// 构造函数
        /// </summary>
        private TaskLogger()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        internal static TaskLogger Instance
        {
            get { return _instance; }
        }


        /// <summary>
        /// 添加日志
        /// </summary>
        internal void Write(string taskName, string message)
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string relativePath = System.Configuration.ConfigurationManager.AppSettings["TaskXmlPath"];
                string path = string.Format("{0}/{1}/Log", basePath.Trim('/'),
                    relativePath.Trim('/').Trim('\\'));

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (StreamWriter sw = File.AppendText(string.Format(@"{0}/{1}_{2:yyyy_MM_dd}.log", path, taskName,DateTime.Now)))
                {
                    sw.Write("日志记录时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\r\n");
                    sw.Write(message);
                    sw.Write("\r\n");
                    sw.Write("\r\n");
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }






    }
}
