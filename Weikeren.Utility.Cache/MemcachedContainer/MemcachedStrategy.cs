using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace Weikeren.Utility.Cache.MemcachedContainer
{
    public class MemcachedStrategy : ICacheStrategy
    {
        #region ICacheStrategy 成员

        /// <summary>
        /// 添加数据到缓存
        /// </summary>
        /// <param name="key">缓存名称</param>
        /// <param name="o">缓存内容</param>
        /// <param name="second">缓存时间(秒)</param>
        public void Add<T>(string key, T o, int second)
        {
            if (second > 0)
            {
                MemcachedManager.CacheClient.Set(key, o, DateTime.Now.AddSeconds(second));
            }
            else
            {
                MemcachedManager.CacheClient.Set(key, o);
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
            HttpRuntime.Cache.Insert(objId, o, dependencies, DateTime.Now.AddSeconds((double)second), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
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
            HttpRuntime.Cache.Insert(objId, o, dependencies, DateTime.Now.AddSeconds((double)second), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }
        #endregion

        /// <summary>
        /// 删除指定缓存
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            if (MemcachedManager.CacheClient.KeyExists(key))
                MemcachedManager.CacheClient.Delete(key);
        }

        //public void RemoveRegex(string pattern)
        //{
        //    Hashtable stats = MemcachedManager.CacheClient.Stats();
        //    ArrayList statsArray = new ArrayList();
        //    foreach (string key in stats.Keys)
        //    {
        //        statsArray.Add(key);
        //        Hashtable values = (Hashtable)stats[key];
        //        foreach (string key2 in values.Keys)
        //        {
        //            statsArray.Add(key2 + ":" + values[key2]);
        //        }
        //    }
        //}

        /// <summary>
        /// 删除所有缓存
        /// </summary>
        public void RemoveAll()
        {
            MemcachedManager.CacheClient.FlushAll();
        }

        /// <summary>
        /// 获得缓存数据
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            return MemcachedManager.CacheClient.Get(key);
        }
        /// <summary>
        /// 获得缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            object obj = Get(key);
            T result = default(T);
            if (obj != null)
            {
                result = (T)obj;
            }
            return result;
        }

        /// <summary>
        /// 判断此缓存是否有效
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        public bool isExists(string key)
        {
            return MemcachedManager.CacheClient.KeyExists(key);
        }
        #endregion
    }
}