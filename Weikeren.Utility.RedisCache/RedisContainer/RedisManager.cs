using ServiceStack.Redis;
using System;

namespace Weikeren.Utility.RedisCache
{
    //public delegate void InitCompleted(RedisClient client);

    /// <summary>
    /// MemCache管理操作类
    /// </summary>
    public sealed class RedisManager
    {
        private RedisClient _client = null;
        //private RedisEndpoint config = null;
        //public Action<RedisClient> InitCompleted;


        #region 单例
        private static readonly RedisManager _instance = new RedisManager();

        /// <summary>
        /// 
        /// </summary>
        public static RedisManager Instance
        {
            get { return _instance; }
        }

        private RedisManager()
        {
            //_endPoint = RedisCachedConfig.GetConfig();
            //init();
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(string configfilepath)
        {
            try
            {
                var config = (RedisEndpoint)SerializationHelper.Load(typeof(RedisEndpoint), configfilepath);
                _client = new RedisClient(config);
                System.Console.WriteLine("－－－－－－－－－－－－－－－Redis初始化成功－－－－－－－－－－－－－－－");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("－－－－－－－－－－－－－－－Redis初始化失败－－－－－－－－－－－－－－－");
                System.Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        ///// <summary>
        ///// 初始化
        ///// </summary>
        ///// <param name="host">主机名</param>
        ///// <param name="port">端口号</param>
        ///// <param name="password">密码</param>
        ///// <param name="db">数据库</param>
        //public void Init(string host, int port=6379,string password=null, long db = 0)
        //{
        //    try
        //    {
        //        _client = new RedisClient(host, port, password, db);
        //        System.Console.WriteLine("Redis初始化成功");
        //        if(InitCompleted!=null)
        //        {
        //            InitCompleted.Invoke(_client);
        //        }

        //    }
        //    catch(Exception ex)
        //    {
        //        System.Console.WriteLine(ex.Message);
        //        throw ex;
        //    }
        //}



        /// <summary>
        /// 客户端缓存操作对象
        /// </summary>
        public RedisClient CacheClient
        {
            get
            {
                return _client;
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            if (_client != null)
            {
                //_client.Shutdown();
                _client.Dispose();
                _client = null;
            }
        }

    }
}