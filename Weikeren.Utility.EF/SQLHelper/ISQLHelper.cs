using System;
using System.Data;
using System.Collections.Generic;
using Dapper;

namespace Weikeren.Utility.EF
{
    public interface ISQLHelper
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        int Execute(Dapper.CommandDefinition command);
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        
        ///// <summary>
        ///// 执行查询，返回首行首列
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <param name="param"></param>
        ///// <param name="transaction"></param>
        ///// <param name="commandTimeout"></param>
        ///// <param name="commandType"></param>
        ///// <returns></returns>
        //object ExecuteScalar(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 执行查询，返回首行首列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        T ExecuteScalar<T>(Dapper.CommandDefinition command);
        /// <summary>
        /// 执行查询，返回首行首列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// 处理事务
        /// </summary>
        /// <param name="action"></param>
        void ProcessInTrans(Action<IDbConnection, IDbTransaction> action);
        /// <summary>
        /// 处理事务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        T ProcessInTrans<T>(Func<IDbConnection, IDbTransaction, T> action);
        /// <summary>
        /// 执行查询，返回对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(Dapper.CommandDefinition command);
        /// <summary>
        /// 执行查询，返回对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// 执行查询，返回单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        T QuerySingle<T>(Dapper.CommandDefinition command);
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
        T QuerySingle<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        ///// <summary>
        ///// 执行查询
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="command"></param>
        ///// <returns></returns>
        //SqlMapper.GridReader QueryMultiple(CommandDefinition command);

        ///// <summary>
        ///// 执行查询
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="sql"></param>
        ///// <param name="param"></param>
        ///// <param name="transaction"></param>
        ///// <param name="buffered"></param>
        ///// <param name="commandTimeout"></param>
        ///// <param name="commandType"></param>
        ///// <returns></returns>
        //SqlMapper.GridReader QueryMultiple(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

    }
}
