﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weikeren.Utility.Cache;
using Weikeren.Utility.Cache.MemcachedContainer;

namespace Efm.LuojiaCache.Web.Commons
{
    public sealed class MemcachedStrategyFactory
    {

        //private readonly static MemcachedStrategyFactory _instance = new MemcachedStrategyFactory();

        //private MemcachedStrategyFactory()
        //{ 
        //}

        //public static MemcachedStrategyFactory Instance
        //{
        //    get { return _instance; }
        //}


        private static ICacheStrategy _cacheStrategy;

        public static ICacheStrategy CreateCacheStrategy()
        {
            if (_cacheStrategy == null)
                _cacheStrategy = new MemcachedStrategy();

            return _cacheStrategy;
        }

    }
}