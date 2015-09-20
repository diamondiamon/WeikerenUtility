using Efm.LuojiaCache.Web.Commons;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Weikeren.Utility.Cache;

namespace Weikeren.Utility.WebTest.CacheTest
{
    public class MemcacheHelper
    {

        //public void TestAdd(MemcacheA a)
        //{
        //    //ICacheStrategy cache = MemcachedStrategyFactory.CreateCacheStrategy();
        //    ////var a = getMemcacheA();
        //    ////cache.Add<MemcacheA>("abc", a, 600);
        //    //cache.Add("abcd", "200", 10);
        //    //cache.Add("xyz", "230", 0);

        //    MemcachedClient mc = new MemcachedClient();
        //    mc.Store(StoreMode.Set, "MyKey", "Hello World");
        //    Console.WriteLine(mc.Get("MyKey"));
        //}

        //public MemcacheA TestGet()
        //{
        //    ICacheStrategy cache = MemcachedStrategyFactory2.CreateCacheStrategy();
        //    //if(cache.isExists("abc"))
        //    //{
        //    //    return cache.Get<MemcacheA>("abc");
        //    //}
        //    if (cache.isExists("abcd"))
        //    {
        //        var d = cache.Get("abcd");
        //    }
        //    var value = getMemcacheA();
        //    TestAdd(value);

        //    return value;
        //}

        public string Test1()
        {
            ICacheStrategy _cacheStrategy = MemcachedStrategyFactory.CreateCacheStrategy();

            var list = new List<MemcacheA>() { 
             new MemcacheA(){ Age=12, Name="Q"},
             new MemcacheA(){ Age=132, Name="Q2"},
             new MemcacheA(){ Age=123, Name="Q1"}
            };

            try
            {
                _cacheStrategy.Add("aaa", getMemcacheA(), 1000);
                return ("成功");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return (ex.Message);
            }
        }

        private MemcacheA getMemcacheA()
        {
            return new MemcacheA() { Age = 12, Name = "MemcacheA" };
        }

    }

    [Serializable]
    public class MemcacheA
    {
        public string Name{get;set;}

        public int Age{get;set;}
    }
}