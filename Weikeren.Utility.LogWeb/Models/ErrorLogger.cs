using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Weikeren.Utility.LogWeb.Utility;

namespace Weikeren.Utility.LogWeb.Models
{
    /// <summary>
    /// 错误日志
    /// </summary>
    public class ErrorLogger:Logger
    {
        public ErrorLogger()
        {
            MappingTableName = "Error";
        }
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string SenderClassName { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 请求的URL参数
        /// </summary>
        public string UrlParameters { get; set; }
        /// <summary>
        /// 堆栈信息
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// 内部异常
        /// </summary>
        public string InnerException { get; set; }

        /// <summary>
        /// 把消息写入数据库
        /// </summary>
        public override void WriteMessageInDB()
        {
            string sql = string.Format("INSERT INTO {0} (LogTime,LogDate,Message,Remark,SenderClassName,Path,UrlParameters,StackTrace,InnerException) VALUES (@LogTime,@LogDate,@Message,@Remark,@SenderClassName,@Path,@UrlParameters,@StackTrace,@InnerException)", MappingTableName);

            var parameters = new List<MySqlParameter>() { 
            new MySqlParameter(){ ParameterName="@TableName", Value=MappingTableName, DbType= System.Data.DbType.String},
            new MySqlParameter(){ ParameterName="@LogTime", Value=LogTime, DbType= System.Data.DbType.DateTime},
            new MySqlParameter(){ ParameterName="@LogDate", Value=LogDate, DbType= System.Data.DbType.DateTime},
            new MySqlParameter(){ ParameterName="@Message", Value=Message, DbType= System.Data.DbType.String},
            new MySqlParameter(){ ParameterName="@Remark", Value=Remark, DbType= System.Data.DbType.String},
            new MySqlParameter(){ ParameterName="@SenderClassName", Value=SenderClassName, DbType= System.Data.DbType.String},
            new MySqlParameter(){ ParameterName="@Path", Value=Path, DbType= System.Data.DbType.String},
            new MySqlParameter(){ ParameterName="@UrlParameters", Value=UrlParameters, DbType= System.Data.DbType.String},
            new MySqlParameter(){ ParameterName="@StackTrace", Value=StackTrace, DbType= System.Data.DbType.String},
            new MySqlParameter(){ ParameterName="@InnerException", Value=InnerException, DbType= System.Data.DbType.String}
            
            };

            WKRMySqlHelper.ExecuteNonQuery(sql, parameters.ToArray());
        }

    }
}