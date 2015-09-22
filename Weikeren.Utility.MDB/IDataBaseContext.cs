using MongoDB.Driver;
using System;

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
        IMongoCollection<TEntity> Set<TEntity>() where TEntity : BaseEntity,new();

        /// <summary>
        /// 得到自增Id
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        int GetIncrementId<TEntity>() where TEntity : BaseEntity, new();

    }
}
