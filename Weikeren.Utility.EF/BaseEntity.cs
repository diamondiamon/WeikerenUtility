using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.EF
{
    /// <summary>
    /// 聚合根
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
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
