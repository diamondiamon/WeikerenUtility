using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Weikeren.Utility.WebTest.MDB.Model;
using Weikeren.Utility.WebTest.MDB.Service;

namespace Weikeren.Utility.WebTest.Controllers
{
    public class DBController : Controller
    {
        public ActionResult AddStudent()
        {

            StringBuilder sb = new StringBuilder();
            System.Diagnostics.Debug.WriteLine("进入任务");
            Task task1 = new Task(()=>{
                for (int i = 0; i < 100; i++)
                {
                    //System.Diagnostics.Debug.WriteLine("任务1："+i);
                    add("任务1",i);
                }
            });

            Task task2 = new Task(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                   // System.Diagnostics.Debug.WriteLine("任务2：" + i);
                    add("任务2", i);
                }
            });

            Task task3 = new Task(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    // System.Diagnostics.Debug.WriteLine("任务2：" + i);
                    add("任务3", i);
                }
            });

            Task task4 = new Task(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    // System.Diagnostics.Debug.WriteLine("任务2：" + i);
                    add("任务4", i);
                }
            });

            task1.Start();
            System.Diagnostics.Debug.WriteLine("任务1开始");

            task2.Start();
            System.Diagnostics.Debug.WriteLine("任务2开始");


            task3.Start();
            System.Diagnostics.Debug.WriteLine("任务3开始");

            task4.Start();
            System.Diagnostics.Debug.WriteLine("任务4开始");


            //Student s = new Student();
            ////s.Id = 1;
            //s.CollectionName = "Student";
            //s.Name = "111";
            //s.Age = 12;
            //s.Score = 100;
            StudentService ss = new StudentService();
            //ss.Add(s);
            var list = ss.GetStudent();

            //StringBuilder sb = new StringBuilder();

            foreach (var item in list)
            {
                sb.AppendLine("Name:" + item.Name + "<br/>");
            }
            System.Diagnostics.Debug.WriteLine("结束");

            //string html = sb.ToString().Replace("\r\n","<br/>");
            return Content(sb.ToString());
        }


        private void add(string name,int age)
        {
            Student s = new Student();
            //s.Id = 1;
            s.CollectionName = "Student";
            s.Name = name;
            s.Age = age;
            s.Score = 100;
            StudentService ss = new StudentService();
            ss.Add(s);
        }

        public ActionResult UpdateStudent()
        {
            Student s = new Student();
            s.Id = 89;
            s.Name = "XXXXXXX";

            StudentService ss = new StudentService();
            ss.Update(s);

            return Content("");
        }

        public ActionResult Get()
        {
            //Student s = new Student();
            //s.Id = "56001dc142ce06616c6d45db";

            StudentService ss = new StudentService();
            var stu = ss.GetById(89);

            //var l = ss.Search();

            return Content("");
        }

        public ActionResult del()
        {
            StudentService ss = new StudentService();
            ss.Delete();

            return Content("");
        }
    }
}
