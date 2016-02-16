using System;
using System.Web;

namespace Weikeren.Utility.RedisCache
{
    /// <summary>
    /// Redis缓存策略
    /// </summary>
    public class RedisStrategy : IRedisCacheStrategy
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
                RedisManager.Instance.CacheClient.Set(key, o, DateTime.Now.AddSeconds(second));
            }
            else
            {
                RedisManager.Instance.CacheClient.Set(key, o);
            }
        }
        
        /// <summary>
        /// 删除指定缓存
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            if (RedisManager.Instance.CacheClient.ContainsKey(key))
                RedisManager.Instance.CacheClient.Remove(key);
        }
        
        /// <summary>
        /// 删除所有缓存
        /// </summary>
        public void RemoveAll()
        {
            RedisManager.Instance.CacheClient.FlushAll();
        }

        /// <summary>
        /// 获得缓存数据
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            return RedisManager.Instance.CacheClient.Get(key);
        }
        /// <summary>
        /// 获得缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return RedisManager.Instance.CacheClient.Get<T>(key);
        }

        /// <summary>
        /// 判断此缓存是否有效
        /// </summary>
        /// <param name="objID"></param>
        /// <returns></returns>
        public bool isExists(string key)
        {
            return RedisManager.Instance.CacheClient.ContainsKey(key);
        }
        #endregion
    }
}