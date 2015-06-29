using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Weikeren.Utility.EF
{
    /// <summary>
    /// EF的数据上下文
    /// </summary>
    public class DataBaseContext : DbContext,IDataBaseContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        public DataBaseContext(string connectionName)
            : base(connectionName)
        {
            this.Database.Log = (c) =>
            {
                Debug.Print(c);
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        private static readonly object _syncRoot = new object();

        /// <summary>
        /// 处理事务
        /// </summary>
        /// <param name="action"></param>
        public void ProcessTransaction(Action<IDataBaseContext> action)
        {
            using (var scope = new TransactionScope())
            {
                lock (_syncRoot)
                {
                    action.Invoke(this);
                    scope.Complete();
                }
            }
        }

        /// <summary>
        /// 数据库访问助手
        /// </summary>
        public ISQLHelper SQLHelper
        {
            get
            {
                var context = ((IObjectContextAdapter)(this)).ObjectContext;
                var connection = this.Database.Connection;

                return new SQLHelper(connection);
            }
        }

        #region 支持SQL
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Obsolete("请使用SQLHelper")]
        public IEnumerable<TElement> ExecuteSqlQuery<TElement>(string sql, params DbParameter[] parameters)
        {
            //var context = ((IObjectContextAdapter)(this)).ObjectContext;
            //if (parameters == null)
            //    return context.ExecuteStoreQuery<TElement>(sql, new object[] { });

            //return context.ExecuteStoreQuery<TElement>(sql, parameters);

            if (parameters == null)
                return this.Database.SqlQuery<TElement>(sql, new object[] { });

            return this.Database.SqlQuery<TElement>(sql, parameters);
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Obsolete("请使用SQLHelper")]
        public int ExecuteSqlCommand(string sql, params DbParameter[] parameters)
        {
            var result = this.Database.ExecuteSqlCommand(sql, parameters);
            return result;
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Obsolete("请使用SQLHelper")]
        public int ExecuteSqlCommand(string sql, CommandType cmdType, params DbParameter[] parameters)
        {

            var context = ((IObjectContextAdapter)(this)).ObjectContext;
            var connection = this.Database.Connection;

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = cmdType;

                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);

                var result = cmd.ExecuteNonQuery();

                return result;
            }
        }
        /// <summary>
        /// 返回第一行
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Obsolete("请使用SQLHelper")]
        public object ExecuteScalar(string sql, params DbParameter[] parameters)
        {
            return ExecuteScalar(sql, CommandType.Text, parameters);
        }
        /// <summary>
        /// 返回第一行
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="cmdType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Obsolete("请使用SQLHelper")]
        public object ExecuteScalar(string sql, CommandType cmdType, params DbParameter[] parameters)
        {
            var context = ((IObjectContextAdapter)(this)).ObjectContext;
            var connection = this.Database.Connection;

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = cmdType;

                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);

                var result = cmd.ExecuteScalar();

                return result;
            }
        }
        #endregion



       
    }
}
