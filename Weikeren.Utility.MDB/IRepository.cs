using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Weikeren.Utility.MDB
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : BaseEntity
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //IQueryable<TEntity> Table { get; }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        IDataBaseContext Context { get; }

        #region 增，删，改，查
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        void Add(TEntity model);

        /// <summary>
        /// 添加多个实体
        /// </summary>
        /// <param name="entitys"></param>
        void Add(IList<TEntity> models);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="models"></param>
        void Update(IList<TEntity> models);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);


        /// <summary>
        /// 根据ID删除实体
        /// </summary>
        /// <param name="id"></param>
        void DeleteById(int id);

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="whereLamdba">条件</param>
        void DeleteBy(Expression<Func<TEntity, bool>> whereLamdba);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetbyId(int id);

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="expression">搜索表达式</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <returns>分页集合类</returns>
        IPagedList<TEntity> SearchWithPager(Expression<Func<TEntity, bool>> searchCondition, int pageSize, int pageIndex);

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="expression">搜索表达式</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="sortOrder">排序方向</param>
        /// <param name="sortPredicate">排序条件</param>
        /// <returns>分页集合类</returns>
        IPagedList<TEntity> SearchWithPager(Expression<Func<TEntity, bool>> searchCondition, int pageSize, int pageIndex, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder = SortOrder.Desc);

        /// <summary>
        /// 统计数量
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        ///  通用查询
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        IList<TEntity> Search(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        ///  通用查询
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="sector">选择器</param>
        /// <returns></returns>
        IList<TResult> Search<TResult>(Expression<Func<TEntity, bool>> expression, Func<TEntity, TResult> sector);
        /// <summary>
        ///  通用查询
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        TEntity SearchSingle(Expression<Func<TEntity, bool>> expression);
        #endregion
    }
}
