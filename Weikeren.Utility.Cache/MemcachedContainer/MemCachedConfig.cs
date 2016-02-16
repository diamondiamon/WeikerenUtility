﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weikeren.Utility.Cache.MemcachedContainer
{
    public class MemCachedConfig
    {
        private static object _lockHelper = new object();
        private static MemCachedConfigInfo config = null;

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="configfilepath"></param>
        public static void LoadConfig(string configfilepath)
        {
            if (config == null)
            {
                lock (_lockHelper)
                {
                    if (config == null)
                    {
                        config = (MemCachedConfigInfo)SerializationHelper.Load(typeof(MemCachedConfigInfo), configfilepath);
                    }
                }
            }
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="anConfig"></param>
        internal static MemCachedConfigInfo GetConfig()
        {
            return config;
        }

        //#region 私有方法
        //public static void SetConfig(MemCachedConfigInfo verifyImageServiceInfoList, string configfilepath)
        //{
        //    SerializationHelper.Save(verifyImageServiceInfoList, configfilepath);
        //}

        //#endregion
    }
}