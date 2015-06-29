using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.EF
{
    /// <summary>
    /// SQL助手
    /// </summary>
    public class SQLHelper :ISQLHelper
    {
        private IDbConnection _conn;

        public SQLHelper(IDbConnection conn)
        {
            this._conn = conn;
        }


        #region Query

        //
        // 摘要: 
        //     Executes a query, returning the data typed as per T
        //
        // 返回结果: 
        //     A sequence of data of the supplied type; if a basic type (int, string, etc)
        //     is queried then the data from the first column in assumed, otherwise an instance
        //     is created per row, and a direct column-name===member-name mapping is assumed
        //     (case insensitive).
        //
        // 备注: 
        //     the dynamic param may seem a bit odd, but this works around a major usability
        //     issue in vs, if it is Object vs completion gets annoying. Eg type new [space]
        //     get new object
        public  IEnumerable<T> Query<T>(CommandDefinition command)
        {
            return ExecuteAction(conn =>
            {
                return conn.Query<T>(command);
            });
        }

        //
        // 摘要: 
        //     Executes a query, returning the data typed as per T
        //
        // 返回结果: 
        //     A sequence of data of the supplied type; if a basic type (int, string, etc)
        //     is queried then the data from the first column in assumed, otherwise an instance
        //     is created per row, and a direct column-name===member-name mapping is assumed
        //     (case insensitive).
        //
        // 备注: 
        //     the dynamic param may seem a bit odd, but this works around a major usability
        //     issue in vs, if it is Object vs completion gets annoying. Eg type new [space]
        //     get new object
        public  IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return ExecuteAction(conn =>
            {
                return conn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
            });
        }
        /// <summary>
        /// 执行查询，返回单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public  T QuerySingle<T>(CommandDefinition command)
        {
            var list = Query<T>(command).ToList();
            if (list == null || list.Count == 0)
                return default(T);

            return list[0];

        }

        /// <summary>
        /// 执行查询，返回单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public  T QuerySingle<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            var list = Query<T>(sql, param, transaction, buffered, commandTimeout, commandType).ToList();
            if (list == null || list.Count == 0)
                return default(T);

            return list[0];
        }
        #endregion

        #region Execute
        
        //
        // 摘要: 
        //     Execute parameterized SQL
        //
        // 返回结果: 
        //     Number of rows affected
        public  int Execute(CommandDefinition command)
        {
            return ExecuteAction(conn =>
            {
                return conn.Execute(command);
            });
        }
        //
        // 摘要: 
        //     Execute parameterized SQL
        //
        // 返回结果: 
        //     Number of rows affected
        public  int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return ExecuteAction(conn =>
            {
                return conn.Execute(sql, param);
            });
        }

        #endregion

        #region ExecuteScalar
        //
        // 摘要: 
        //     Execute parameterized SQL that selects a single value
        //
        // 返回结果: 
        //     The first cell selected
        public  T ExecuteScalar<T>(CommandDefinition command)
        {
            return ExecuteAction(conn =>
            {
                return conn.ExecuteScalar<T>(command);
            });
        }
        //
        // 摘要: 
        //     Execute parameterized SQL that selects a single value
        //
        // 返回结果: 
        //     The first cell selected
        public  T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return ExecuteAction(conn =>
            {
                return conn.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
            });
        }

        //
        // 摘要: 
        //     Execute parameterized SQL that selects a single value
        //
        // 返回结果: 
        //     The first cell selected
        public  object ExecuteScalar(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return ExecuteAction(conn =>
            {
                return conn.ExecuteScalar(sql, param, transaction, commandTimeout, commandType);
            });
        }

        #endregion

        #region ProcessInTrans
        /// <summary>
        /// 处理事务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public T ProcessInTrans<T>(Func<IDbConnection, IDbTransaction, T> action)
        {
            using (_conn)
            {
                IDbTransaction tran = null;
                try
                {
                    _conn.Open();
                    tran = _conn.BeginTransaction();
                    var result = action.Invoke(_conn, tran);
                    if (tran.Connection != null)
                    {
                        tran.Commit();
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    throw new Exception("数据库执行命令错误", ex);
                }
            }
        }

        /// <summary>
        /// 处理事务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public void ProcessInTrans(Action<IDbConnection, IDbTransaction> action)
        {
            using (_conn)
            {
                IDbTransaction tran = null;
                try
                {
                    _conn.Open();
                    tran = _conn.BeginTransaction();
                    action.Invoke(_conn, tran);
                    if (tran.Connection != null)
                    {
                        tran.Commit();
                    }
                }
                catch (Exception ex)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    throw new Exception("数据库执行命令错误", ex);
                }
            }
        }
        #endregion

        protected T ExecuteAction<T>(Func<IDbConnection, T> action)
        {
            using (_conn)
            {
                try
                {
                    return action.Invoke(_conn);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("发生错误了：" + ex.Message);
                    throw new Exception("数据库执行命令错误", ex);
                }

            }
        }

    }
}
