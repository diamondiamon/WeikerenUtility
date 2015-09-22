using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.MDB
{
    /// <summary>
    /// 聚合根
    /// </summary>
    [Serializable]
    public class BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseEntity()
        {

        }
        /// <summary>
        /// 主键名称
        /// </summary>
        protected internal readonly string EntityKey = "Id";

        /// <summary>
        /// 主键
        /// </summary>
        [BsonId]
        public virtual int Id { get; set; }

        /// <summary>
        /// 对应的文档名称
        /// </summary>
        public virtual string CollectionName { get; set; }
    }
    ///// <summary>
    ///// 聚合根
    ///// </summary>
    ///// <typeparam name="PKType"></typeparam>
    //public abstract class BaseEntity<PKType>
    //{
    //    /// <summary>
    //    /// 主键
    //    /// </summary>
    //    public virtual PKType Id { get; set; }
    //}

}
