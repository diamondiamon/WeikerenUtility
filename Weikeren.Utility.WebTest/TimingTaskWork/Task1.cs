using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weikeren.Utility.TimingTask;

namespace Weikeren.Utility.WebTest.TimingTaskWork
{
    public class Task1:Job
    {

        protected override void OnExecute()
        {
            System.Diagnostics.Debug.WriteLine("任务1，时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
        }
    }

    public class Task2 : Job
    {
        protected override void OnExecute()
        {
            System.Diagnostics.Debug.WriteLine("任务2，时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
        }
    }
    public class Task3 : Job
    {
        protected override void OnExecute()
        {
            System.Diagnostics.Debug.WriteLine("任务3，时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            //throw new Exception("Test");
        }
    }

    public class Task4 : Job
    {
        protected override void OnExecute()
        {
            System.Diagnostics.Debug.WriteLine("任务4，时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
        }
    }

    public class Task5 : Job
    {
        protected override void OnExecute()
        {
            System.Diagnostics.Debug.WriteLine("任务5，时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
        }
    }
}