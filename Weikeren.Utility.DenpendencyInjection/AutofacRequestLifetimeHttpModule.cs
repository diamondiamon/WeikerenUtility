using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Weikeren.Utility.DenpendencyInjection
{/// <summary>
    /// 为HTTP请求创建Autofac生命周期
    /// </summary>
    public class AutofacRequestLifetimeHttpModule : IHttpModule
    {
        public static readonly object _HttpRequestTag = "AutofacWebRequest";

        public void Dispose() { }

        static ILifetimeScope LifetimeScope
        {
            get
            {
                return (ILifetimeScope)HttpContext.Current.Items[typeof(ILifetimeScope)];
            }
            set
            {
                HttpContext.Current.Items[typeof(ILifetimeScope)] = value;
            }
        }

        public void Init(HttpApplication context)
        {
            context.EndRequest += ContextEndRequest;
        }

        static ILifetimeScope InitializeLifetimeScope(Action<ContainerBuilder> configurationAction, ILifetimeScope container)
        {
            return (configurationAction == null) ? container.BeginLifetimeScope(_HttpRequestTag) : container.BeginLifetimeScope(_HttpRequestTag, configurationAction);
        }

        static void ContextEndRequest(object sender, EventArgs e)
        {
            ILifetimeScope lifetimeScope = LifetimeScope;
            if (lifetimeScope != null)
                lifetimeScope.Dispose();
        }

        public static ILifetimeScope GetLifeScope(ILifetimeScope container, Action<ContainerBuilder> configurationAction)
        {
            if (HttpContext.Current != null)
                return LifetimeScope ?? (LifetimeScope = InitializeLifetimeScope(configurationAction, container));
            else
                return InitializeLifetimeScope(configurationAction, container);
        }

    }
}
