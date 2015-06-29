using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.DenpendencyInjection
{
    /// <summary>
    /// 依赖容量接口
    /// </summary>
    public interface IDenpendencyContainer
    {

        #region AddComponent
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <typeparam name="TService">接口类型</typeparam>
        /// <param name="key">关键词</param>
        /// <param name="lifeTime">生命周期</param>
        void AddComponent<TService>(string key = "", ComponentServiceLifetime lifeTime = ComponentServiceLifetime.Never);

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="serviceType">接口类型</param>
        /// <param name="key">关键词</param>
        /// <param name="lifeTime">生命周期</param>
        void AddComponent(Type serviceType, string key = "", ComponentServiceLifetime lifeTime = ComponentServiceLifetime.Never);

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <typeparam name="TService">接口类型</typeparam>
        /// <typeparam name="TImplementation">实例类型</typeparam>
        /// <param name="key">关键词</param>
        /// <param name="lifeTime">生命周期</param>
        void AddComponent<TService, TImplementation>(string key = "", ComponentServiceLifetime lifeTime = ComponentServiceLifetime.Never);

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="serviceType">接口类型</param>
        /// <param name="implementation">实例类型</param>
        /// <param name="key">关键词</param>
        /// <param name="lifeTime">生命周期</param>
        void AddComponent(Type serviceType, Type implementation, string key = "", ComponentServiceLifetime lifeTime = ComponentServiceLifetime.Never);


        /// <summary>
        /// 注册实体（注：该注册方式为SINGLETON方式）
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="instance"></param>
        /// <param name="key"></param>
        void AddComponentInstance<TService>(object instance, string key = "");

        /// <summary>
        /// 注册实体（注：该注册方式为SINGLETON方式）
        /// </summary>
        /// <param name="service"></param>
        /// <param name="instance"></param>
        /// <param name="key"></param>
        void AddComponentInstance(Type service, object instance, string key = "");

        /// <summary>
        /// 注册实体（注：该注册方式为SINGLETON方式）
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="key"></param>
        void AddComponentInstance(object instance, string key = "");

        #endregion

        /// <summary>
        /// 从容量获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Resolve<T>(string key = "") where T : class;

        /// <summary>
        /// 从容量获取对象
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        object Resolve(Type serviceType);

        /// <summary>
        /// 从容量获取所有对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T[] ResolveAll<T>(string key = "");

        /// <summary>
        /// 是否已经注册
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        bool IsRegistered(Type serviceType);

        /// <summary>
        /// 注册对象带参数
        /// </summary>
        /// <param name="service"></param>
        /// <param name="instance"></param>
        /// <param name="paras">IDictionary<string, object> string：参数名，object：参数值</param>
        /// <param name="key"></param>
        /// <param name="lifeStyle"></param>
        void AddComponentWithParameters(Type service, Type instance, IDictionary<string, object> paras, string key = "", ComponentServiceLifetime lifeStyle = ComponentServiceLifetime.Never);

        /// <summary>
        /// 注册对象带参数
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="paras">IDictionary<string, object> string：参数名，object：参数值</param>
        /// <param name="key"></param>
        /// <param name="lifeStyle"></param>
        void AddComponentWithParameters(Type instance, IDictionary<string, object> paras, string key = "", ComponentServiceLifetime lifeStyle = ComponentServiceLifetime.Never);

        /// <summary>
        /// 根据Func进行注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="key"></param>
        /// <param name="lifeStyle"></param>
        void Register<T>(Func<T> action, string key = "", ComponentServiceLifetime lifeStyle = ComponentServiceLifetime.Never);

        void Register<T>(Type service, Func<T> action, string key = "", ComponentServiceLifetime lifeStyle = ComponentServiceLifetime.Never);

        object ResolveOptional(Type serviceType);


    }
}
