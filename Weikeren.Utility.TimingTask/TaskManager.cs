using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Weikeren.Utility.TimingTask
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TaskManager
    {
        private static readonly TaskManager _instance = new TaskManager();

        private CancellationTokenSource _cancellationTokenSource;
        /// <summary>
        /// 
        /// </summary>
        public CancellationTokenSource CancelTokenSource
        {
            get {
                //if (_cancellationTokenSource == null)
                //    _cancellationTokenSource = new CancellationTokenSource();

                return _cancellationTokenSource;
            }
        }


        private CancellationToken _token;

        private List<WorkItem> _workItems;
        /// <summary>
        /// 工作任务项
        /// </summary>
        public List<WorkItem> WorkItems
        {
            get {
                return _workItems; }
        }

        /// <summary>
        /// 
        /// </summary>
        private TaskManager()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }
        /// <summary>
        /// 
        /// </summary>
        public static TaskManager Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 读取配置任务(非完成，停止)
        /// </summary>
        /// <returns></returns>
        private List<WorkItem> getAllTasks()
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string relativePath = System.Configuration.ConfigurationManager.AppSettings["TaskXmlPath"];
                string path = string.Format("{0}/{1}/", basePath.Trim('/'),
                    relativePath.Trim('/').Trim('\\'));

                var tasks = System.IO.Directory.GetFiles(path, "*.xml");

                var workers = new List<WorkItem>();
                foreach (var task in tasks)
                {
                    var worker = new WorkItem(task);
                    //if (worker.State == Enums.TaskStates.Stop || worker.State == Enums.TaskStates.Completed)
                    //    continue;

                    workers.Add(worker);
                }

                return workers;
            }
            catch (Exception ex)
            {
                TaskLogger.Instance.Write("ERROR", ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void StartTask()
        {
            //if (!CancelTokenSource.IsCancellationRequested && _workItems!=null)
            //{
            //    return;
            //}

            //_token = CancelTokenSource.Token;
            //_workItems = getAllTasks();
            //var mainTask = Task.Factory.StartNew(
            //    () =>
            //    {
            //        checkWorksToDo();
            //    }, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            _workItems = getAllTasks();
            checkWorksToDo();

        }

        ///// <summary>
        ///// 重置(不能使用)
        ///// </summary>
        //public void Reset()
        //{
        //    Stop();
        //    StartTask();
        //}

        /// <summary>
        /// 停止
        /// </summary>
        public void StopAll()
        {
            if (!CancelTokenSource.IsCancellationRequested)
            {
                CancelTokenSource.Cancel();
                CancelTokenSource.Dispose();
            }
            //while (_mainTask.Status != TaskStatus.RanToCompletion)
            //{ }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="guidFlag"></param>
        public void StopOne(string guidFlag)
        {
            if (_workItems == null)
            {
                return;
            }
            var item = _workItems.Where(c => c.GuidFlag.Equals(guidFlag)).FirstOrDefault();
            if (item == null)
                return;

            item.Stop();

            
        }

        /// <summary>
        /// 保存一个工作项
        /// </summary>
        /// <param name="item"></param>
        public void Save(WorkItem item)
        {

            if (_workItems == null)
            {
                _workItems = new List<WorkItem>();
                _workItems.Add(item);
                startOneWork(item);
                return;
            }
            else
            {
                var work = _workItems.Where(c => c.GuidFlag.Equals(item.GuidFlag)).FirstOrDefault();
                if (work == null)
                {
                    _workItems.Add(item);
                    startOneWork(item);
                    return;
                }

                item.CopyTo(work); //把item的值复制到work上
                startOneWork(item);
            }

           

        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="guidFlag"></param>
        public void Remove(string guidFlag)
        {
            if (_workItems == null)
            {
                return;
            }
            var item = _workItems.Where(c => c.GuidFlag.Equals(guidFlag)).FirstOrDefault();
            if (item == null)
                return;

            item.State = Enums.TaskStates.Stop;
            _workItems.Remove(item);
            item.Delete();


        }
        /// <summary>
        /// 
        /// </summary>
        private void checkWorksToDo()
        {
            if (_workItems == null || _workItems.Count == 0)
                return;

            foreach (var worker in _workItems)
            {
                startOneWork(worker);
            }
        }

        /// <summary>
        /// 开启一个新的工作
        /// </summary>
        /// <param name="work"></param>
        private void startOneWork(WorkItem work)
        {
            var tokenSrc = new CancellationTokenSource();
            var task = Task.Factory.StartNew((state) =>
            {
                if (work == null)
                    return;

                var workItem = state as WorkItem;

                for (; ; )
                {
                    
                    if (tokenSrc.IsCancellationRequested)
                    {
                        workItem.State = Enums.TaskStates.Stop;
                        workItem.Save();
                    }


                    if (work.State != Enums.TaskStates.Running)
                    {
                        cancelTokenSource(tokenSrc);
                        break;
                    }

                    bool canExecute = true;
                    
                    if (workItem.StartAt.HasValue && workItem.StartAt.Value > DateTime.Now)
                    {
                        canExecute = false;
                    }
                    if (workItem.NextStart.HasValue && workItem.NextStart.Value > DateTime.Now)
                    {
                        canExecute = false;
                    }

                    //不到时间，不能执行
                    if (!canExecute)
                    {
                        if (workItem.IsFixedTime)
                        {
                            //固定时间每分钟检查一次。
                            var sleep = DateTime.Now.AddSeconds(1) - DateTime.Now;
                            tokenSrc.Token.WaitHandle.WaitOne(sleep);
                        }
                        else
                        {
                            //固定时间每分秒检查一次。
                            var sleep = DateTime.Now.AddSeconds(1) - DateTime.Now;
                            tokenSrc.Token.WaitHandle.WaitOne(sleep);
                        }

                        continue;
                    }

                    //执行前
                    Action beforeExecute = () => {
                        DateTime beforeExecuteTime = DateTime.Now;
                        if (workItem.StartAt == null)
                        {
                            workItem.StartAt = beforeExecuteTime;
                        }
                        //非固定时间
                        if (workItem.IsFixedTime)
                        {
                            workItem.LastRunTime = beforeExecuteTime;
                            workItem.NextStart = workItem.GetNextStartTime(beforeExecuteTime);

                        }
                    };

                    //执行后
                    Action afterExecute = () => {

                        DateTime executedTime = DateTime.Now;
                        //非固定时间
                        if(!workItem.IsFixedTime)
                        {
                            workItem.LastRunTime = executedTime;
                            workItem.NextStart = workItem.GetNextStartTime(executedTime);

                        }
                    };

                    //执行
                    workItem.Run(tokenSrc, beforeExecute, afterExecute);
                    //System.Diagnostics.Debug.WriteLine("下次执行时间："+workItem.NextStart.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    workItem.Save();

                    //var ts = work.GetWaitSeconds();
                    
                    //if (ts.TotalMilliseconds > 0)
                    //{
                    //    if (!tokenSrc.IsCancellationRequested)
                    //    {
                    //        tokenSrc.Token.WaitHandle.WaitOne(ts);
                    //    }
                    //}
                    //else
                    //{
                    //    cancelTokenSource(tokenSrc);
                    //    break;
                    //}


                    //主线程停止，副线程也要停止
                    if (CancelTokenSource.IsCancellationRequested)
                    {
                        cancelTokenSource(tokenSrc);
                        //flag = false;
                        break;
                    }

                }
            }, work, tokenSrc.Token);
        }

        private void cancelTokenSource(CancellationTokenSource tokenSrc)
        {
            tokenSrc.Cancel();
        }

    }
}
