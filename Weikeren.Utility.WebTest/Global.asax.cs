using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Weikeren.Utility.Cache.MemcachedContainer;
using Weikeren.Utility.RedisCache;
//using Weikeren.Utility.RedisCache;
using Weikeren.Utility.WebTest.DITest;

namespace Weikeren.Utility.WebTest
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Weikeren.Utility.DenpendencyInjection.DIManager.Instance.Init(new DIRegister());

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            loadCacheConfig();


            //开始定时任务
            //Weikeren.Utility.TimingTask.TaskManager.Instance.StartTask();

            //Weikeren.Utility.RedisCache.RedisManager.Instance.InitCompleted = (client) => {
            //    System.Console.WriteLine("加载完成了。呵呵！");
            //};
            //Weikeren.Utility.RedisCache.RedisManager.Instance.Init("192.168.1.123", 6379, "test123456");

            //RedisCachedConfig.SetConfig(new ServiceStack.Redis.RedisEndpoint() { Host = "192.168.1.123", Port = 6379, Password = "test123456" }, Server.MapPath("/redis.config"));
        }

        /// <summary>
        /// 加载缓存的配置文件
        /// </summary>
        private void loadCacheConfig()
        {
            MemCachedConfig.LoadConfig(Server.MapPath("/memcached.config"));
            RedisManager.Instance.Init(Server.MapPath("/redis.config"));
        }

        protected void Application_End(object sender, EventArgs e)
        {
            Weikeren.Utility.RedisCache.RedisManager.Instance.Close();

        }

    }
}