using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Weikeren.Utility.RedisCache;
using ServiceStack.Redis;

namespace Weikeren.Utility.UnitTest
{
    [TestClass]
    public class RedisTest
    {
        [TestMethod]
        public void Add()
        {
            //RedisManager.Instance.Init(("E:\\编程\\TFS\\通用库\\WeikerenUtility\\Weikeren.Utility.UnitTest\\redis.config"));
            //IRedisCacheStrategy _cacheStrategy = new RedisStrategy();

            RedisClient redisClient = new RedisClient("192.168.1.123", 6379); 

            for (int i = 0; i < 7000; i++)
            {
                string key = "student" + i;

                redisClient.Set(key, Guid.NewGuid().ToString(), DateTime.Now.AddSeconds(30));
            }
        }
    }
}
