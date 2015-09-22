using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.MDB
{
    public class PrimaryKeyInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        [BsonId]
        public string Id { get; set; }

        /// <summary>
        /// 当前下标
        /// </summary>
        public int CurrentIndex { get; set; }

        /// <summary>
        /// 集合名称
        /// </summary>
        public string CollectionName { get; set; }




    }
}
