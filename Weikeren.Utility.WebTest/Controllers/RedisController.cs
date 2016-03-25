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
using Weikeren.Utility.WebTest.RedisCache;

namespace Weikeren.Utility.WebTest.Controllers
{
    public class RedisController : Controller
    {
        public ActionResult Add()
        {
            RedisCacheTester test = new RedisCacheTester();
            test.Add();

            return Content("添加成功");
        }

        public ActionResult AddMore()
        {
            RedisCacheTester test = new RedisCacheTester();
            test.AddMore();

            return Content("添加成功");
        }

        public ActionResult Delete()
        {
            RedisCacheTester test = new RedisCacheTester();
            test.Remove();

            return Content("删除成功");
        }

        public ActionResult Get()
        {
            RedisCacheTester test = new RedisCacheTester();
            var s = test.Get();

            return Content("获取成功");
        }
        public ActionResult Close()
        {
            //Weikeren.Utility.RedisCache.RedisManager.Instance.Close();

            return Content("关闭成功");
        }
    }
}
