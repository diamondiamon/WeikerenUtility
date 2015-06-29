using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Weikeren.Utility.Cache.MemcachedContainer;
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
            loadMemcachedConfig();


            //开始定时任务
            Weikeren.Utility.TimingTask.TaskManager.Instance.StartTask();
        }

        /// <summary>
        /// 加载缓存的配置文件
        /// </summary>
        private void loadMemcachedConfig()
        {
            MemCachedConfig.LoadConfig(Server.MapPath("/memcached.config"));
        }
    }
}