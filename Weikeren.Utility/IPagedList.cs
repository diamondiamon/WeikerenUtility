using System;
using System.Collections.Generic;

namespace Weikeren.Utility
{
    /// <summary>
    /// 分页接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedList<T> : IList<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        int PageIndex { get; }
        /// <summary>
        /// 页大小
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// 总页数
        /// </summary>
        int TotalPages { get; }
        /// <summary>
        /// 总数量
        /// </summary>
        int TotalCount { get; }
    }
}
