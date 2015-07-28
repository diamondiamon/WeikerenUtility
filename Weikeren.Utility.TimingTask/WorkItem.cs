using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using Weikeren.Utility.TimingTask.Enums;

namespace Weikeren.Utility.TimingTask
{
    /// <summary>
    /// 工作项
    /// </summary>
    public class WorkItem : TaskDescriptor
    {
        private IJob _job;

        private CancellationTokenSource _tokenSrc;

        /// <summary>
        /// 标识
        /// </summary>
        public string GuidFlag { get;set; }
        
        /// <summary>
        /// 下一次运行的时间
        /// </summary>
        [XmlIgnore]
        public DateTime? NextStart { get; set; }

        /// <summary>
        /// 替代屬性
        /// </summary>
        [XmlElement("NextStart")]
        public string NextStartString
        {
            get
            {
                if (NextStart == null)
                    return string.Empty;

                return this.NextStart.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            set
            {
                this.NextStart = DateTime.Parse(value);
            }
        }


        /// <summary>
        /// 上一次运行的时间
        /// </summary>
        [XmlIgnore]
        public DateTime? LastRunTime { get; set; }

        /// <summary>
        /// 替代屬性
        /// </summary>
        [XmlElement("LastRunTime")]
        public string LastRunTimeString
        {
            get
            {
                if (LastRunTime == null)
                    return string.Empty;

                return this.LastRunTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            set
            {
                this.LastRunTime = DateTime.Parse(value);
            }
        }

        /// <summary>
        /// 程序集信息
        /// </summary>
        public string AssemblyInfo { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public TaskStates State { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public WorkItem()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xmlPath"></param>
        public WorkItem(string xmlPath)
        {
            var item = (WorkItem)XmlHelper.XmlDeserialize(xmlPath, this.GetType());
            //var item = (WorkItem)Serializer.DeserializeFromXmlFile(this.GetType(), xmlPath);
            this.Title = item.Title;
            this.Description = item.Description;
            this.StartAt = item.StartAt;
            this.Frequency = item.Frequency;
            this.Recurs = item.Recurs;
            this.NextStart = item.NextStart;
            this.LastRunTime = item.LastRunTime;
            this.AssemblyInfo = item.AssemblyInfo;
            this.State = item.State;
            this.GuidFlag = item.GuidFlag;
        }

        /// <summary>
        /// 保存到XML
        /// </summary>
        public void Save()
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string relativePath = System.Configuration.ConfigurationManager.AppSettings["TaskXmlPath"];
                string path = string.Format("{0}/{1}/{2}.xml", basePath.Trim('/'),
                    relativePath.Trim('/').Trim('\\'), Title);
                if (string.IsNullOrEmpty(this.GuidFlag))
                {
                    this.GuidFlag = Guid.NewGuid().ToString("N");
                }
                XmlHelper.XmlSerialize(path, this, this.GetType());

                System.Diagnostics.Debug.WriteLine(string.Format("{0:yyyy-MM-dd HH:mm:ss}：{1}", DateTime.Now, this.State));
            }
            catch { 
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        public void Delete()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = System.Configuration.ConfigurationManager.AppSettings["TaskXmlPath"];
            string path = string.Format("{0}/{1}/{2}.xml", basePath.Trim('/'),
                relativePath.Trim('/').Trim('\\'), Title);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

        }


        /// <summary>
        /// 运行任务
        /// </summary>
        public void Run(CancellationTokenSource tokenSrc)
        {
            if (StartAt.HasValue && StartAt.Value > DateTime.Now)
            {
                return;
            }
            if (NextStart.HasValue && NextStart.Value > DateTime.Now)
            {
                return;
            }
            try
            {
                if (_job == null)
                {
                    string[] classInfo = AssemblyInfo.Split(',');
                    _job = (IJob)Assembly.Load(classInfo[0]).CreateInstance(classInfo[1]);
                }
                _tokenSrc = tokenSrc;

                writeMessageToLog(string.Format("任务（{1}）在[{0:yyyy-MM-dd HH:mm:ss}]开始执行",DateTime.Now,Title));
                _job.Execute();
                writeMessageToLog(string.Format("任务（{1}）在[{0:yyyy-MM-dd HH:mm:ss}]执行完成", DateTime.Now, Title));
                this.LastRunTime = DateTime.Now;
                //this.NextStart = GetNextStartTime(this.LastRunTime);


                if (Frequency == Frequencies.OneTime)
                {
                    this.State = TaskStates.Completed;
                    //writeMessageToLog(string.Format("任务（{1}）在[{0:yyyy-MM-dd HH:mm:ss}]完成任务，不再执行", DateTime.Now, Title));
                }
                else
                {
                    if (StartAt == null)
                    {
                        StartAt = LastRunTime;
                    }

                    this.State = TaskStates.Running;
                    NextStart = GetNextStartTime(LastRunTime);
                    //writeMessageToLog(string.Format("任务（{1}）下次执行时间在[{0:yyyy-MM-dd HH:mm:ss}]", NextStart, Title));
                }
                this.Save();
            }
            catch (Exception e)
            {
                //this.State = TaskStates.Stop;
                writeMessageToLog(e.StackTrace);
                writeMessageToLog(string.Format("任务（{0}）发生异常，请管理员检查代码，任务尚未结束", Title));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            _tokenSrc.Cancel();
        }

        /// <summary>
        /// 写信息
        /// </summary>
        /// <param name="message"></param>
        private void writeMessageToLog(string message)
        {
            TaskLogger.Instance.Write(this.Title, message);
        }

        /// <summary>
        /// 得制对象
        /// </summary>
        /// <param name="item"></param>
        public void CopyTo(WorkItem item)
        {
            item.AssemblyInfo = this.AssemblyInfo;
            item.Description = this.Description;
            item.Frequency = this.Frequency;
            item.LastRunTime = this.LastRunTime;
            item.NextStart = this.NextStart;
            item.Recurs = this.Recurs;
            item.StartAt = this.StartAt;
            item.State = this.State;
            item.Title = this.Title;
            item.GuidFlag = this.GuidFlag;
        }
    }
}
