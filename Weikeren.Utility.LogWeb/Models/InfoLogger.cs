using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Weikeren.Utility.LogWeb.Models
{
    /// <summary>
    /// 消息日志
    /// </summary>
    public class InfoLogger :Logger
    {
        public InfoLogger()
        {
            MappingTableName = "Info";
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