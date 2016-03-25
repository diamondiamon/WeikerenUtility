using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.RedisCache
{
    /// <summary>
    /// 终结点配置
    /// </summary>
    public class RedisEndpoint
    {
        /// <summary>
        /// 可写的Redis链接地址
        /// </summary>
        public List<string> WriteServerList
        {
            get;
            set;
        }

        /// <summary>
        /// 可读的Redis链接地址
        /// </summary>
        public List<string> ReadServerList
        {
            get;
            set;
        }

        /// <summary>
        /// 最大写链接数
        /// </summary>
        public int MaxWritePoolSize
        {
            get;
            set;
        }

        /// <summary>
        /// 最大读链接数
        /// </summary>
        public int MaxReadPoolSize
        {
            get;
            set;
        }

        /// <summary>
        /// 自动重启
        /// </summary>
        public bool AutoStart
        {
            get;
            set;
        }

        /// <summary>
        /// 本地缓存到期时间，单位:秒
        /// </summary>
        public int LocalCacheTime
        {
            get;
            set;
        }

        /// <summary>
        /// 是否记录日志,该设置仅用于排查redis运行时出现的问题,如redis工作正常,请关闭该项
        /// </summary>
        public bool RecordeLog
        {
            get;
            set;
        }
        /// 数据库
        /// </summary>
        public int Db { get; set; }



        ///// <summary>
        ///// 连接超时（秒）
        ///// </summary>
        //public int ConnectTimeout { get; set; }
        ///// <summary>
        ///// 数据库
        ///// </summary>
        //public long Db { get; set; }
        ///// <summary>
        ///// 主机地址
        ///// </summary>
        //public string Host { get; set; }
        ///// <summary>
        ///// 密码
        ///// </summary>
        //public string Password { get; set; }
        ///// <summary>
        ///// 端口
        ///// </summary>
        //public int Port { get; set; }


        //public string Client { get; set; }
        //public int IdleTimeOutSecs { get; set; }
        //public string NamespacePrefix { get; set; }
        //public int ReceiveTimeout { get; set; }
        //public int RetryTimeout { get; set; }
        //public int SendTimeout { get; set; }
        //public bool Ssl { get; set; }
    }
}
