using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weikeren.Utility.WebTest.DB.Model;
using Weikeren.Utility.WebTest.DB.Service;
using Weikeren.Utility.WebTest.DITest;
using Weikeren.Utility.DenpendencyInjection;
using Weikeren.Utility.TimingTask;
using Weikeren.Utility.WebTest.TimingTaskWork;
using System.Threading;
using System.Threading.Tasks;
using Weikeren.Utility.WebTest.WkrLoggerService;
using Weikeren.Utility.WebTest.CacheTest;
using Weikeren.Utility.Expressage;
using System.Text;

namespace Weikeren.Utility.WebTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "99sModify this template to jump-start your ASP.NET MVC application.";
            var dt = DateTime.MinValue;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region DITest

        public ActionResult DITest1()
        {
            var ia = DIManager.Instance.Container.Resolve<IA>();

            string k = ia.Test1();

            return Content(k);

        }
        #endregion

        #region DBTest

        public ActionResult AddStudent()
        {
            Student s = new Student();
            s.Name = "111";
            s.Age = 12;
            s.Score = 100;
            StudentService ss = new StudentService();
            ss.Add(s);
            var list = ss.GetStudent();
            return Content("");
        }

        #endregion


        #region Pay

        public ActionResult Pay()
        {
            return View();
        }

        #endregion

        #region TimingTask

        public ActionResult AddTask()
        {
            WorkItem wi = new WorkItem()
            {
                Title = "Test0",
                Description = "Description",
                State = Utility.TimingTask.Enums.TaskStates.Ready,
                AssemblyInfo = "Weikeren.Utility.WebTest,Weikeren.Utility.WebTest.TimingTaskWork.Task1",
                Frequency = Utility.TimingTask.Enums.Frequencies.EverySecond,
                LastRunTime = DateTime.Now,
                Recurs = 1
            };
            wi.Save();

            wi = new WorkItem()
            {
                Title = "Test1",
                Description = "Description",
                State = Utility.TimingTask.Enums.TaskStates.Ready,
                AssemblyInfo = "Weikeren.Utility.WebTest,Weikeren.Utility.WebTest.TimingTaskWork.Task2",
                Frequency = Utility.TimingTask.Enums.Frequencies.EveryMinute,
                LastRunTime = DateTime.Now,
                Recurs = 1
            };
            wi.Save();


            wi = new WorkItem()
            {
                Title = "Test2",
                Description = "Description",
                State = Utility.TimingTask.Enums.TaskStates.Ready,
                AssemblyInfo = "Weikeren.Utility.WebTest,Weikeren.Utility.WebTest.TimingTaskWork.Task3",
                Frequency = Utility.TimingTask.Enums.Frequencies.EverySecond,
                Recurs = 3
            };
            wi.Save();

            wi = new WorkItem()
            {
                Title = "Test3",
                Description = "Description",
                State = Utility.TimingTask.Enums.TaskStates.Ready,
                AssemblyInfo = "Weikeren.Utility.WebTest,Weikeren.Utility.WebTest.TimingTaskWork.Task4",
                Frequency = Utility.TimingTask.Enums.Frequencies.EverySecond,
                Recurs = 4
            };
            wi.Save();


            wi = new WorkItem()
            {
                Title = "Test4",
                Description = "Description",
                State = Utility.TimingTask.Enums.TaskStates.Ready,
                AssemblyInfo = "Weikeren.Utility.WebTest,Weikeren.Utility.WebTest.TimingTaskWork.Task5",
                Frequency = Utility.TimingTask.Enums.Frequencies.OneTime,
                Recurs = 2
            };
            wi.Save();










            return Content("Success");
        }

        public ActionResult TimingTask()
        {
            //string basePath = AppDomain.CurrentDomain.BaseDirectory;
            //string relativePath = System.Configuration.ConfigurationManager.AppSettings["TaskXmlPath"];
            //string path = string.Format("{0}/{1}/", basePath.Trim('/'),
            //    relativePath.Trim('/').Trim('\\'));

            //var tasks = System.IO.Directory.GetFiles(path, "*.xml");

            //var workers = new List<WorkItem>();
            //foreach (var task in tasks)
            //{
            //    var worker = new WorkItem(task);
            //    workers.Add(worker);
            //}

            //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            //var token = cancellationTokenSource.Token;

            //var mainTask = Task.Factory.StartNew(
            //    () =>
            //    {
            //        //for (; ; )
            //        //{
            //        //    CheckWorksToDo(workers);
            //        //}
            //        CheckWorksToDo(workers);
            //    }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);


            //TaskManager.Instance.StartTask();

            return View();

            //return Content("View End");
        }

        public ActionResult Start()
        {
            TaskManager.Instance.StartTask();
            return Content("Has Start");
        }

        public ActionResult Stop()
        {
            //TaskManager.Instance.Stop();
            return Content("Has Stop");
        }

        //public ActionResult Reset()
        //{
        //    TaskManager.Instance.Reset();
        //    return Content("Has Reset");
        //}

        public ActionResult Remove()
        {
            //TaskManager.Instance.Remove("Weikeren.Utility.WebTest,Weikeren.Utility.WebTest.TimingTaskWork.Task1");
            TaskManager.Instance.Remove("GuidFlag");
            return Content("Has Remove");
        }

        public ActionResult AddOne()
        {
            WorkItem wi = new WorkItem()
            {
                Title = "Test0",
                Description = "Description",
                State = Utility.TimingTask.Enums.TaskStates.Ready,
                AssemblyInfo = "Weikeren.Utility.WebTest,Weikeren.Utility.WebTest.TimingTaskWork.Task1",
                Frequency = Utility.TimingTask.Enums.Frequencies.EverySecond,
                LastRunTime = DateTime.Now,
                Recurs = 1
            };
            wi.Save();
            TaskManager.Instance.Save(wi);

            return Content("Has AddOne");
        }

        private void CheckWorksToDo(List<WorkItem> workers)
        {
            foreach (var worker in workers)
            {
                var tokenSrc = new CancellationTokenSource();
                //worker.Cancellation = tokenSrc;
                var task = Task.Factory.StartNew((state) =>
                {
                    bool flag = true;
                    while (flag)
                    {
                        var workItem = state as WorkItem;
                        var ts = worker.GetWaitSeconds();
                        //workItem.Run();
                        if (ts.TotalMilliseconds > 0)
                        {
                            tokenSrc.Token.WaitHandle.WaitOne(ts);
                        }
                        else
                        {
                            flag = false;
                            tokenSrc.Cancel();
                        }
                    }

                    ////线程被取消后执行
                    //tokenSrc.Token.Register(() =>
                    //{
                    //    flag = false;
                    //});

                }, worker, tokenSrc.Token);




            }
        }
        #endregion

        #region Log Test

        public ActionResult LogTest()
        {
            Task task = new Task(() => {
                WkrLoggerService.LoggerService service = new WkrLoggerService.LoggerService();
                //new WkrLoggerService.DebugLogger() { LogDate=DateTime.Now.Date, LogTime= DateTime.Now, Message="Debug", Remark="Remark" }
                var log = new ErrorLogger() { InnerException = "InnerException", LogDate = DateTime.Now.Date, LogTime = DateTime.Now };
                service.ErrorMessage(log);
            });

            task.Start();
            
            return Content("SUCCESS");
            
        }

        #endregion


        #region CacheTest
        public ActionResult CacheTest()
        {
            MemcacheHelper helper = new MemcacheHelper();
            //var k = helper.TestGet();

            //string s = string.Format("Name:{0} <br/> Age:{1}",k.Name,k.Age);

            var s = helper.Test1();

            return Content(s);

        }
        #endregion

        #region ExpressageTest

        public ActionResult ExpressageTest()
        {
            var expressageGetter = ExpressageFactory.CreateExpressageGetter();
            var result = expressageGetter.GetExpressageMessage(new MQueryParameter() { TypeCom = "申通", OrderId = "3300363402956" });

            if (result != null && result.Result == true)
            {
                if (result.Data != null && result.Data.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in result.Data)
                    {
                        sb.AppendLine(string.Format("[{0}]:{1}<br/>",item.Time,item.Context));
                    }

                    return Content(sb.ToString());
                }
                else
                {
                    return Content(result.Error.ErrorMessage);
                }
            }

            return Content("查询失败");

        }

        #endregion
    }
}
