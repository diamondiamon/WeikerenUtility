using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Efm.LuojiaCache.MemcachedFactory
{
    public interface ICacheStrategy
    {
        /// <summary>
        /// 添加数据到缓存
        /// </summary>
        /// <param name="objId">缓存名称</param>
        /// <param name="o">缓存内容</param>
        /// <param name="second">缓存时间(秒)</param>
        void Add(string objId, object o, int second);

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
        /// <param name="objId"></param>
        void RemoveCache(string objId);

        /// <summary>
        /// 删除所有缓存
        /// </summary>
        void RemoveCacheAll();

        ///// <summary>
        ///// 删除匹配到的缓存
        ///// </summary>
        ///// <param name="pattern"></param>
        ///// <returns></returns>
        //void RemoveCacheRegex(string pattern);

        ///// <summary>
        ///// 获取所有缓存键
        ///// </summary>
        ///// <returns></returns>
        //IList<string> GetCacheKeys();

        ///// <summary>
        ///// 搜索 匹配到的缓存
        ///// </summary>
        ///// <param name="pattern"></param>
        ///// <returns></returns>
        //IList<string> SearchCacheRegex(string pattern);

        /// <summary>
        /// 获得缓存数据
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        object GetCacheData(string objId);

        /// <summary>
        /// 判断此缓存是否有效
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        bool HasCache(string objID);
    }
}
