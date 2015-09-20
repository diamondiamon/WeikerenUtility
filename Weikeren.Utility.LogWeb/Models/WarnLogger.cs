using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Weikeren.Utility.LogWeb.Models
{
    /// <summary>
    /// 警告日志
    /// </summary>
    public class WarnLogger:Logger
    {
        public WarnLogger()
        {
            MappingTableName = "Warn";
        }
        /// <summary>
        /// 把消息写入数据库
        /// </summary>
        public override void WriteMessageInDB()
        {
            base.WriteMessageInDB();
        }
    }
}