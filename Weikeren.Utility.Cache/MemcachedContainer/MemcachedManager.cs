using Memcached.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weikeren.Utility.Cache.MemcachedContainer
{
    /// <summary>
    /// MemCache管理操作类
    /// </summary>
    public sealed class MemcachedManager
    {
        #region 静态方法和属性
        private static MemcachedClient mc = null;
        private static SockIOPool pool = null;
        private static string[] serverList = null;
        private static MemCachedConfigInfo memCachedConfigInfo = MemCachedConfig.GetConfig();

        #endregion

        static MemcachedManager()
        {
            CreateManager();
        }

        private static void CreateManager()
        {
            serverList = memCachedConfigInfo.ServerList;
            pool = SockIOPool.GetInstance(memCachedConfigInfo.PoolName);
            pool.SetServers(serverList);
            pool.InitConnections = memCachedConfigInfo.IntConnections;//初始化链接数
            pool.MinConnections = memCachedConfigInfo.MinConnections;//最少链接数
            pool.MaxConnections = memCachedConfigInfo.MaxConnections;//最大连接数
            pool.SocketConnectTimeout = memCachedConfigInfo.SocketConnectTimeout;//Socket链接超时时间
            pool.SocketTimeout = memCachedConfigInfo.SocketTimeout;// Socket超时时间
            pool.MaintenanceSleep = memCachedConfigInfo.MaintenanceSleep;//维护线程休息时间
            pool.Failover = memCachedConfigInfo.FailOver; //失效转移(一种备份操作模式)
            pool.Nagle = memCachedConfigInfo.Nagle;//是否用nagle算法启动socket
            pool.HashingAlgorithm = HashingAlgorithm.NewCompatibleHash;

            pool.Initialize();

            mc = new MemcachedClient();
            mc.PoolName = memCachedConfigInfo.PoolName;
            mc.EnableCompression = false;
        }

        /// <summary>
        /// 缓存服务器地址列表
        /// </summary>
        public static string[] ServerList
        {
            set
            {
                if (value != null)
                    serverList = value;
            }
            get { return serverList; }
        }

        /// <summary>
        /// 客户端缓存操作对象
        /// </summary>
        public static MemcachedClient CacheClient
        {
            get
            {
                if (mc == null)
                    CreateManager();

                return mc;
            }
        }

        public static void Dispose()
        {
            //if (pool != null)
            //    pool.Shutdown();
        }

    }
}