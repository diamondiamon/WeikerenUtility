using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weikeren.Utility.Cache
{
    public interface ICacheStrategy
    {
        /// <summary>
        /// 添加数据到缓存
        /// </summary>
        /// <param name="key">缓存名称</param>
        /// <param name="o">缓存内容</param>
        /// <param name="second">缓存时间(秒)</param>
        void Add<T>(string key, T o, int second);

        /// <summary>
        /// 添加数据到缓存 (依赖其它缓存)
        /// </summary>
        /// <param name="objId">缓存名称</param>
        /// <param name="o">缓存内容</param>
        /// <param name="second">缓存时间(秒)</param>
        /// <param name="dependKey">依赖缓存名称数组</param>
        void AddCacheDepend(string objId, object o, int second, string[] dependKey);

        /// <summary>
        /// 添加数据到缓存 (依赖文件)
        /// </summary>
        /// <param name="objId">缓存名称</param>
        /// <param name="o">缓存内容</param>
        /// <param name="second">缓存时间(秒)</param>
        /// <param name="files">依赖缓存名称文件名数组</param>
        void AddFileDepend(string objId, object o, int second, string[] files);

        /// <summary>
        /// 删除指定缓存
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
        //void RemoveRegex(string pattern);
        /// <summary>
        /// 删除所有缓存
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// 获得缓存数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        /// 获得缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 判断此缓存是否有效
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool isExists(string key);
    }
}
