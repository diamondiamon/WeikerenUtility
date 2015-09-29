using MongoDB.Driver;
using System;
using System.Linq;

namespace Weikeren.Utility.MDB
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
        IMongoCollection<TEntity> Set<TEntity>(string collectionName="") where TEntity : BaseEntity, new();

        /// <summary>
        /// 可查询实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IQueryable<TEntity> Table<TEntity>(string collectionName = "") where TEntity : BaseEntity, new();

        /// <summary>
        /// 得到自增Id
        /// </summary>
        /// <param name="pkCollectionName"></param>
        /// <returns></returns>
        int GetIncrementId<TEntity>(string pkCollectionName = "") where TEntity : BaseEntity, new();

    }
}
