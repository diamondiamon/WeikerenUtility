using System;
using System.Collections.Generic;
using System.Linq;

namespace Weikeren.Utility
{
    /// <summary>
    /// 分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public PagedList(IQueryable<T> query, int pageIndex, int pageSize)
        {
            int total = query.Count();
            this.TotalCount = total;
            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(query.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enume"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="TotalCount"></param>
        public PagedList(IEnumerable<T> enume, int pageIndex, int pageSize,int TotalCount)
        {
            this.TotalCount = TotalCount;
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(enume);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="list"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public PagedList(IList<T> list, int pageIndex, int pageSize)
        {
            TotalCount = list.Count();
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(list.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }

        #endregion

        #region 属性

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalCount { get; set; }

        #endregion




    }
}
