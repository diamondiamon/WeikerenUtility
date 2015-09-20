using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace Weikeren.Utility.MDB
{
    /// <summary>
    /// EF的数据上下文
    /// </summary>
    public class DataBaseContext : DisposableObject, IDataBaseContext
    {
        private MongoClient _mongoClient;
        private MongoDatabase _mongoDatabase;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        public DataBaseContext(string connectionString,string dbName)
        {
            _mongoClient = new MongoClient(connectionString);
            _mongoDatabase = _mongoClient.GetServer().GetDatabase(dbName);
            
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public new MongoCollection<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            var d = default(TEntity);

            var collection = _mongoDatabase.GetCollection<TEntity>(d.CollectionName);
            return collection;
        }

        #region Dispose

        public override void DoDispose()
        {
            if(_mongoClient != null)
            {
                _mongoClient = null;
            }
            if (_mongoDatabase != null)
            {
                _mongoDatabase = null;
            }
        }
        #endregion

    }
}
