using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;
using Weikeren.Utility.LogWeb.Utility;

namespace Weikeren.Utility.LogWeb.Models
{

    public class Logger 
    {
       
        protected string MappingTableName { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 日志记录时间
        /// </summary>
        public DateTime LogTime { get; set; }
        /// <summary>
        /// 日志记录时间
        /// </summary>
        public DateTime LogDate { get; set; }
        /// <summary>
        /// 日志消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        private readonly static object _thisLock = new object();
        /// <summary>
        /// 执行
        /// </summary>
        public void ExcuteWrite()
        {
            lock (_thisLock)
            {
                WriteMessageInDB();
            }
        }

        /// <summary>
        /// 写入消息
        /// </summary>
        public virtual void WriteMessageInDB()
        {
            string sql = string.Format("INSERT INTO {0} (LogTime,LogDate,Message,Remark) VALUES (@LogTime,@LogDate,@Message,@Remark)", MappingTableName);

            var parameters = new List<MySqlParameter>() { 
            new MySqlParameter(){ ParameterName="@LogTime", Value=LogTime, DbType= System.Data.DbType.DateTime},
            new MySqlParameter(){ ParameterName="@LogDate", Value=LogDate, DbType= System.Data.DbType.DateTime},
            new MySqlParameter(){ ParameterName="@Message", Value=Message, DbType= System.Data.DbType.String},
            new MySqlParameter(){ ParameterName="@Remark", Value=Remark, DbType= System.Data.DbType.String}
            
            };

            WKRMySqlHelper.ExecuteNonQuery(sql, parameters.ToArray());
        }


    }
}