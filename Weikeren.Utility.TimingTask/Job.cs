using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weikeren.Utility.TimingTask
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Job:IJob
    {
        //private string _description = "";
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description
        {
            //get
            //{
            //    if (_description == "")
            //    {
            //        _description = this.GetType().Name;
            //    }
            //    return _description;
            //}
            //set { _description = value; }

            get { return this.GetType().Name; }
        }

        //private string _assemblyInfo = "";
        /// <summary>
        /// 程序集信息
        /// </summary>
        public virtual string AssemblyInfo
        {
            //get
            //{
            //    if (_assemblyInfo == "")
            //    {
            //        _assemblyInfo = string.Format("{0},{1}", this.GetType().Assembly.ManifestModule.Name.Replace(".dll", ""), this.GetType().FullName);
            //    }
            //    return _assemblyInfo;
            //}
            //set { _assemblyInfo = value; }
            get { return string.Format("{0},{1}", this.GetType().Assembly.ManifestModule.Name.Replace(".dll", ""), this.GetType().FullName); }
        }



        /// <summary>
        /// 任务开始
        /// </summary>
        protected virtual void OnStart() { }
        /// <summary>
        /// 任务执行
        /// </summary>
        protected abstract void OnExecute();
        /// <summary>
        /// 任务结束
        /// </summary>
        protected virtual void OnCompleted() { }

        /// <summary>
        /// 执行
        /// </summary>
        public void Execute()
        {
            OnStart();
            OnExecute();
            OnCompleted();
        }
        /// <summary>
        /// 处理异常。（处理异常后请调用Base.OnError 进行写入日志）
        /// </summary>
        /// <param name="exception"></param>
        public virtual void OnError(Exception exception)
        {
            TaskLogger.Instance.Write(this.GetType().ToString(), exception.StackTrace);
        }


       
    }
}
