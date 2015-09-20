using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Autofac.Integration.Mvc;

namespace Weikeren.Utility.DenpendencyInjection
{
    public class AutofacContainer : IDenpendencyContainer
    {
        #region private fields
        private readonly IContainer _container;
        #endregion

        public AutofacContainer(IContainer container)
        {
            this._container = container;
        }

        #region methods
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <typeparam name="TService">接口类型</typeparam>
        /// <param name="key">关键词</param>
        /// <param name="lifeTime">生命周期</param>
        public void AddComponent<TService>(string key = "", ComponentServiceLifetime lifeTime = ComponentServiceLifetime.Never)
        {
            AddComponent<TService, TService>(key, lifeTime);
        }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="serviceType">接口类型</param>
        /// <param name="key">关键词</param>
        /// <param name="lifeTime">生命周期</param>
        public void AddComponent(Type serviceType, string key = "", ComponentServiceLifetime lifeTime = ComponentServiceLifetime.Never)
        {
            AddComponent(serviceType, serviceType, key, lifeTime);
        }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <typeparam name="TService">接口类型</typeparam>
        /// <typeparam name="TImplementation">实例类型</typeparam>
        /// <param name="key">关键词</param>
        /// <param name="lifeTime">生命周期</param>
        public void AddComponent<TService, TImplementation>(string key = "", ComponentServiceLifetime lifeTime = ComponentServiceLifetime.Never)
        {
            AddComponent(typeof(TService), typeof(TImplementation), key, lifeTime);
        }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="serviceType">接口类型</param>
        /// <param name="implementation">实例类型</param>
        /// <param name="key">关键词</param>
        /// <param name="lifeTime">生命周期</param>
        public void AddComponent(Type serviceType, Type implementation, string key = "", ComponentServiceLifetime lifeTime = ComponentServiceLifetime.Never)
        {
            UpdateContainer(c =>
            {
                var serviceTypes = new List<Type> { serviceType };

                if (serviceType.IsGenericType)
                {
                    var register = c.RegisterGeneric(implementation).As(serviceTypes.ToArray()).PerLifetimeStyle(lifeTime);
                    if (!string.IsNullOrEmpty(key))
                        register.Keyed(key, serviceType);
                }
                else
                {
                    var register = c.RegisterType(implementation).As(serviceTypes.ToArray()).PerLifetimeStyle(lifeTime);
                    if (!string.IsNullOrEmpty(key))
                        register.Keyed(key, serviceType);
                }
            });
        }
        /// <summary>
        /// 注册实体（注：该注册方式为SINGLETON方式）
        /// </summary>
        /// <typeparam name="TService">接口类型</typeparam>
        /// <param name="instance">实体</param>
        /// <param name="key">关键词</param>
        public void AddComponentInstance<TService>(object instance, string key = "")
        {
            AddComponentInstance(typeof(TService), instance, key);
        }
        /// <summary>
        /// 注册实体（注：该注册方式为SINGLETON方式）
        /// </summary>
        /// <param name="service">接口类型</param>
        /// <param name="instance">实体</param>
        /// <param name="key">关键词</param>
        public void AddComponentInstance(Type service, object instance, string key = "")
        {
            UpdateContainer(x =>
            {
                var registration = x.RegisterInstance(instance).Keyed(key, service).As(service);
            });
        }
        /// <summary>
        /// 注册实体（注：该注册方式为SINGLETON方式）
        /// </summary>
        /// <param name="instance">实体</param>
        /// <param name="key">关键词</param>
        public void AddComponentInstance(object instance, string key = "")
        {
            AddComponentInstance(instance.GetType(), instance, key);
        }

        /// <summary>
        /// 注册对象带参数
        /// </summary>
        /// <param name="instance">实例类型</param>
        /// <param name="paras">参数，IDictionary<string, object> string：参数名，object：参数值</param>
        /// <param name="key">关键词</param>
        /// <param name="lifeStyle">生命周期</param>
        public void AddComponentWithParameters(Type instance, IDictionary<string, object> paras, string key = "", ComponentServiceLifetime lifeStyle = ComponentServiceLifetime.Never)
        {
            AddComponentWithParameters(instance, instance, paras, key, lifeStyle);
        }
        /// <summary>
        /// 注册对象带参数
        /// </summary>
        /// <param name="service">接口类型</param>
        /// <param name="instance">实例类型</param>
        /// <param name="paras">参数，IDictionary<string, object> string：参数名，object：参数值</param>
        /// <param name="key">关键词</param>
        /// <param name="lifeStyle">生命周期</param>
        public void AddComponentWithParameters(Type service, Type instance, IDictionary<string, object> paras, string key = "", ComponentServiceLifetime lifeStyle = ComponentServiceLifetime.Never)
        {
            UpdateContainer(x =>
            {
                var registration = x.RegisterType(instance).As(service).WithParameters(paras.Select(y => new NamedParameter(y.Key, y.Value))).PerLifetimeStyle(lifeStyle);
                if (!string.IsNullOrEmpty(key))
                    registration.Keyed(key, service);
            });
        }

        /// <summary>
        /// 更新容器
        /// </summary>
        /// <param name="action"></param>
        public void UpdateContainer(Action<ContainerBuilder> action)
        {
            var builder = new ContainerBuilder();
            action.Invoke(builder);
            builder.Update(_container);
        }
        /// <summary>
        /// 从容量获取对象
        /// </summary>
        /// <typeparam name="TService">接口类型</typeparam>
        /// <param name="key">关键词</param>
        /// <returns></returns>
        public TService Resolve<TService>(string key = "") where TService : class
        {
            if (string.IsNullOrEmpty(key))
                return Scope().Resolve<TService>();
            return Scope().ResolveKeyed<TService>(key);
        }
        /// <summary>
        /// 从容量获取对象
        /// </summary>
        /// <param name="serviceType">接口类型</param>
        /// <returns></returns>
        public object Resolve(Type serviceType)
        {
            return Scope().Resolve(serviceType);
        }
        /// <summary>
        /// 从容量获取全部对象
        /// </summary>
        /// <typeparam name="TService">接口类型</typeparam>
        /// <param name="key">关键词</param>
        /// <returns></returns>
        public TService[] ResolveAll<TService>(string key = "")
        {
            if (string.IsNullOrEmpty(key))
            {
                return Scope().Resolve<IEnumerable<TService>>().ToArray();
            }
            return Scope().ResolveKeyed<IEnumerable<TService>>(key).ToArray();
        }
        /// <summary>
        /// 该接口是否已经注册过
        /// </summary>
        /// <param name="serviceType">接口类型</param>
        /// <returns></returns>
        public bool IsRegistered(Type serviceType)
        {
            return Scope().IsRegistered(serviceType);
        }

        /// <summary>
        /// 根据Func进行注册
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="service"></param>
        /// <param name="action"></param>
        /// <param name="key"></param>
        /// <param name="lifeStyle"></param>
        public void Register<TService>(Type service, Func<TService> action, string key = "", ComponentServiceLifetime lifeStyle = ComponentServiceLifetime.Never)
        {
            UpdateContainer(x =>
            {
                var registration = x.Register(d =>
                {
                    return action.Invoke();
                }).As(service).PerLifetimeStyle(lifeStyle);
                if (!string.IsNullOrEmpty(key))
                    registration.Keyed(key, typeof(TService));
            });
        }
        /// <summary>
        /// 根据Func进行注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="key"></param>
        /// <param name="lifeStyle"></param>
        public void Register<T>(Func<T> action, string key = "", ComponentServiceLifetime lifeStyle = ComponentServiceLifetime.Never)
        {
            Register<T>(typeof(T), action, key, lifeStyle);
        }

        public ILifetimeScope Scope()
        {
            try
            {
                return AutofacRequestLifetimeHttpModule.GetLifeScope(_container, null);
            }
            catch
            {
                return _container;
            }

        }

        public object ResolveOptional(Type serviceType)
        {
            return Scope().ResolveOptional(serviceType);
        }
        #endregion
    }

   
    public static class IDenpendencyContainerExtensions
    {
        /// <summary>
        /// 设置生命周期
        /// </summary>
        /// <typeparam name="TLimit"></typeparam>
        /// <typeparam name="TActivatorData"></typeparam>
        /// <typeparam name="TRegistrationStyle"></typeparam>
        /// <param name="builder"></param>
        /// <param name="lifetimeStyle"></param>
        /// <returns></returns>
        public static Autofac.Builder.IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> PerLifetimeStyle<TLimit, TActivatorData, TRegistrationStyle>(this Autofac.Builder.IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder, ComponentServiceLifetime lifetimeStyle)
        {
            switch (lifetimeStyle)
            {
                case ComponentServiceLifetime.Never:
                    return builder.SingleInstance();
                case ComponentServiceLifetime.LifetimeScope:
                    return HttpContext.Current != null ? builder.InstancePerHttpRequest() : builder.InstancePerDependency();
                case ComponentServiceLifetime.PreInstance:
                    return builder.InstancePerDependency();
                default:
                    return builder.SingleInstance();
            }
        }
    }
}
