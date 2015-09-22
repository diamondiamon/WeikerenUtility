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
        private IMongoDatabase _mongoDatabase;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        public DataBaseContext(string connectionString,string dbName)
        {
            _mongoClient = new MongoClient(connectionString);
            _mongoDatabase = _mongoClient.GetDatabase(dbName);
            
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IMongoCollection<TEntity> Set<TEntity>() where TEntity : BaseEntity,new()
        {
            var collectionName = typeof(TEntity).Name;
            var collection = _mongoDatabase.GetCollection<TEntity>(collectionName);
            return collection;
        }

        private readonly static object lockFlag = new object();
        /// <summary>
        /// 得到自增Id
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public int GetIncrementId<TEntity>() where TEntity : BaseEntity, new()
        {
            lock(lockFlag)
            {
                var pkCollectionName = typeof(TEntity).Name;
                var collectionName = "PrimaryKeyInfo";
                var collection = _mongoDatabase.GetCollection<PrimaryKeyInfo>(collectionName);
                var currentKey = collection.Find(c => c.CollectionName == pkCollectionName).FirstOrDefaultAsync().GetAwaiter().GetResult();
                if (currentKey == null)
                {
                    currentKey = new PrimaryKeyInfo() { Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(), CollectionName = pkCollectionName, CurrentIndex = 1 };
                    collection.InsertOneAsync(currentKey).GetAwaiter().GetResult();
                    return 1;
                }
                else
                {
                    var currentIndex = currentKey.CurrentIndex + 1;

                    var filter = Builders<PrimaryKeyInfo>.Filter.Eq(s => s.CollectionName, pkCollectionName);
                    var fields = Builders<PrimaryKeyInfo>.Update.Set(c => c.CurrentIndex, currentIndex);
                    collection.UpdateOneAsync(filter, fields).GetAwaiter().GetResult();

                    return currentIndex;
                }
            }
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
