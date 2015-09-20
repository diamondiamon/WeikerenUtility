using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace Efm.LuojiaCache.MemcachedFactory
{
    public class MemcachedStrategy : ICacheStrategy
    {
        #region ICacheStrategy 成员

        /// <summary>
        /// 添加数据到缓存
        /// </summary>
        /// <param name="objId">缓存名称</param>
        /// <param name="o">缓存内容</param>
        /// <param name="second">缓存时间(秒)</param>
        public void Add(string objId, object o, int second)
        {
            if (second > 0)
            {
                MemcachedManager.CacheClient.Set(objId, o, DateTime.Now.AddSeconds(second));
            }
            else
            {
                MemcachedManager.CacheClient.Set(objId, o);
            }
        }

        #region 缓存依赖没有用到memcached
        /// <summary>
        /// 添加数据到缓存 (依赖其它缓存)
        /// </summary>
        /// <param name="objId">缓存名称</param>
        /// <param name="o">缓存内容</param>
        /// <param name="second">缓存时间(秒)</param>
        /// <param name="dependKey">依赖缓存名称数组</param>
        public void AddCacheDepend(string objId, object o, int second, string[] dependKey)
        {
            CacheDependency dependencies = new CacheDependency(null, dependKey, DateTime.Now);
            HttpRuntime.Cache.Insert(objId, o, dependencies, DateTime.Now.AddSeconds((double)second), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }

        /// <summary>
        /// 添加数据到缓存 (依赖文件)
        /// </summary>
        /// <param name="objId">缓存名称</param>
        /// <param name="o">缓存内容</param>
        /// <param name="second">缓存时间(秒)</param>
        /// <param name="files">依赖缓存名称文件名数组</param>
        public void AddFileDepend(string objId, object o, int second, string[] files)
        {
            CacheDependency dependencies = new CacheDependency(files, DateTime.Now);
            HttpRuntime.Cache.Insert(objId, o, dependencies, DateTime.Now.AddSeconds((double)second), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }
        #endregion

        /// <summary>
        /// 删除指定缓存
        /// </summary>
        /// <param name="objId"></param>
        public void RemoveCache(string objId)
        {
            if (MemcachedManager.CacheClient.KeyExists(objId))
                MemcachedManager.CacheClient.Delete(objId);
        }

        /// <summary>
        /// 删除所有缓存
        /// </summary>
        public void RemoveCacheAll()
        {
            MemcachedManager.CacheClient.FlushAll();
        }

        ///// <summary>
        ///// 删除匹配到的缓存
        ///// </summary>
        ///// <param name="pattern"></param>
        //public void RemoveCacheRegex(string pattern)
        //{
        //    IList<string> list = SearchCacheRegex(pattern);
        //    foreach (var item in list)
        //    {
        //        MemcachedManager.CacheClient.Delete(item);
        //    }
        //}

        ///// <summary>
        ///// 获取所有缓存键
        ///// </summary>
        ///// <returns></returns>
        //public IList<string> GetCacheKeys()
        //{
        //    return MemcachedManager.GetAllKeys();
        //}

        ///// <summary>
        ///// 搜索 匹配到的缓存
        ///// </summary>
        ///// <param name="pattern"></param>
        ///// <returns></returns>
        //public IList<string> SearchCacheRegex(string pattern)
        //{
        //    List<string> l = new List<string>();
        //    IList<string> cacheKeys = MemcachedManager.GetAllKeys();
        //    foreach (var item in cacheKeys)
        //    {
        //        if (Regex.IsMatch(item, pattern))
        //        {
        //            l.Add(item);
        //        }
        //    }
        //    return l.AsReadOnly();
        //}

        /// <summary>
        /// 获得缓存数据
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public object GetCacheData(string objId)
        {
            return MemcachedManager.CacheClient.Get(objId);
        }

        /// <summary>
        /// 判断此缓存是否有效
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        public bool HasCache(string objID)
        {
            return MemcachedManager.CacheClient.KeyExists(objID);
        }
        #endregion
    }
}