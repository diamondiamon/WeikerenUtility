using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;

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
        public DataBaseContext(string connectionName)
        {
            string connection = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            if (string.IsNullOrEmpty(connection))
                throw new Exception("链接字符串为空");

            var spiltString = connection.Split(';');
            if (spiltString.Length != 2)
                throw new Exception("链接字符串格式有误");


            _mongoClient = new MongoClient(spiltString[0]);
            _mongoDatabase = _mongoClient.GetDatabase(spiltString[1]);
            
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IMongoCollection<TEntity> Set<TEntity>(string collectionName="") where TEntity : BaseEntity,new()
        {
            if(string.IsNullOrEmpty(collectionName))
            {
                collectionName = typeof(TEntity).Name;
            }
            var collection = _mongoDatabase.GetCollection<TEntity>(collectionName);
            return collection;
        }

        /// <summary>
        /// 可查询实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IQueryable<TEntity> Table<TEntity>(string collectionName = "") where TEntity : BaseEntity, new()
        {
            return Set<TEntity>(collectionName).AsQueryable();
        }

        private readonly static object lockFlag = new object();
        /// <summary>
        /// 得到自增Id
        /// </summary>
        /// <param name="pkCollectionName"></param>
        /// <returns></returns>
        public int GetIncrementId<TEntity>(string pkCollectionName = "") where TEntity : BaseEntity, new()
        {
            lock(lockFlag)
            {
                if(string.IsNullOrEmpty(pkCollectionName))
                {
                    pkCollectionName = typeof(TEntity).Name;
                }
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
