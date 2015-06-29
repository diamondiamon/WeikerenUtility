using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.EF
{
    /// <summary>
    /// 数据库上下文接口
    /// </summary>
    public interface IDataBaseContext : IDisposable
    {
        /// <summary>
        /// 返回数据库领域实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// 对CUD操作写入数据库
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// 处理事务
        /// </summary>
        /// <param name="action"></param>
        void ProcessTransaction(Action<IDataBaseContext> action);

        /// <summary>
        /// SQL助手
        /// </summary>
        ISQLHelper SQLHelper { get; }
        
        #region 支持SQL
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Obsolete("请使用SQLHelper")]
        IEnumerable<TElement> ExecuteSqlQuery<TElement>(string sql, params DbParameter[] parameters);
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Obsolete("请使用SQLHelper")]
        int ExecuteSqlCommand(string sql, params DbParameter[] parameters);
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Obsolete("请使用SQLHelper")]
        int ExecuteSqlCommand(string sql,CommandType cmdType, params DbParameter[] parameters);
        /// <summary>
        /// 返回第一行
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Obsolete("请使用SQLHelper")]
        object ExecuteScalar(string commandText, params DbParameter[] parameters);
        /// <summary>
        /// 返回第一行
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Obsolete("请使用SQLHelper")]
        object ExecuteScalar(string sql, CommandType cmdType, params DbParameter[] parameters);
        #endregion

    }
}
