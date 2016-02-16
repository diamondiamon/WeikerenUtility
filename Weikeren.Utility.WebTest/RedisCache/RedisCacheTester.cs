using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weikeren.Utility.RedisCache;
using Weikeren.Utility.WebTest.DB.Model;

namespace Weikeren.Utility.WebTest.RedisCache
{
    public class RedisCacheTester
    {
        private IRedisCacheStrategy _cacheStrategy;

        public RedisCacheTester()
        {
            _cacheStrategy = new RedisStrategy();
        }

        public void Add()
        {
            Student st = new Student() { Name=Guid.NewGuid().ToString("N"), Score = 20, techer = new Techer() { Name="我是老师", Description="这是描述" } };

            _cacheStrategy.Add("111", st, 30);
            
        }

        public Student Get()
        {
            return _cacheStrategy.Get<Student>("111");
        }

        public void Remove()
        {
            _cacheStrategy.Remove("111");
        }

    }
}