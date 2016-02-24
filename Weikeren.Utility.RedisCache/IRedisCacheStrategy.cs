using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weikeren.Utility.RedisCache
{
    /// <summary>
    /// Redis策略类接口
    /// </summary>
    public interface IRedisCacheStrategy
    {
        /// <summary>
        /// 添加数据到缓存
        /// </summary>
        /// <param name="key">缓存名称</param>
        /// <param name="o">缓存内容</param>
        /// <param name="second">缓存时间(秒)</param>
        void Add<T>(string key, T o, int second);
        
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
