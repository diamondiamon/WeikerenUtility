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
            if (!CancelTokenSource.IsCancellationRequested && _workItems!=null)
            {
                return;
            }

            _token = CancelTokenSource.Token;
            _workItems = getAllTasks();
            var mainTask = Task.Factory.StartNew(
                () =>
                {
                    checkWorksToDo();
                }, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
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
            var date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var tokenSrc = new CancellationTokenSource();
            var task = Task.Factory.StartNew((state) =>
            {
                //bool flag = true;
                for (; ; )
                {
                    if (work == null)
                        break;

                    var workItem = state as WorkItem;

                    if (tokenSrc.IsCancellationRequested)
                    {
                        workItem.State = Enums.TaskStates.Stop;
                        workItem.Save();
                    }


                    //if (work.State == Enums.TaskStates.Stop || work.State == Enums.TaskStates.Completed || work.State== Enums.TaskStates.Ready)
                    if (work.State != Enums.TaskStates.Running)
                    {
                        cancelTokenSource(tokenSrc);
                        break;
                    }

                    var ts = work.GetWaitSeconds();
                    workItem.Run(tokenSrc);
                    if (ts.TotalMilliseconds > 0)
                    {
                        if (!tokenSrc.IsCancellationRequested)
                        {
                            tokenSrc.Token.WaitHandle.WaitOne(ts);
                        }
                    }
                    else
                    {
                        cancelTokenSource(tokenSrc);
                        break;
                    }
                    //主线程停止，副线程也要停止
                    if (CancelTokenSource.IsCancellationRequested)
                    {
                        cancelTokenSource(tokenSrc);
                        //flag = false;
                        break;
                    }

                    //System.Diagnostics.Debug.WriteLine(string.Format("[{0}]的正在运行", date));
                    //System.Diagnostics.Debug.WriteLine(_mainTask.Status);
                }
            }, work, tokenSrc.Token);
        }

        private void cancelTokenSource(CancellationTokenSource tokenSrc)
        {
            tokenSrc.Cancel();
        }

    }
}
