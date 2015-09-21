using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.EF
{
    public class Repository<TEntity> : DisposableObject,IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly IDataBaseContext _context;
        private IDbSet<TEntity> _entities;

        /// <summary>
        /// 数据实体
        /// </summary>
        private IDbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<TEntity>();
                return _entities;
            }
        }

        /// <summary>
        /// 可查询实体
        /// </summary>
        public virtual IQueryable<TEntity> Table
        {
            get
            {
                return this.Entities;
            }
        }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public IDataBaseContext Context
        {
            get { return _context; }
        }

        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public Repository(IDataBaseContext context)
        {
            this._context = context;
        }

        #region 增，删，改，查
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        public void Add(TEntity model)
        {
            if (model == null)
                throw new ArgumentNullException("entity null");
            this.Entities.Add(model);
            this._context.SaveChanges();
        }

        /// <summary>
        /// 添加多个实体
        /// </summary>
        /// <param name="models"></param>
        public void Add(IList<TEntity> models)
        {
            if (models == null)
                throw new ArgumentNullException("entity null");

            foreach (TEntity model in models)
            {
                this.Entities.Add(model);
            }
            this._context.SaveChanges();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity null");

            //this.Entities.Add(entity);
            this._context.SaveChanges();
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="models"></param>
        public void Update(IList<TEntity> models)
        {
            if (models == null)
                throw new ArgumentNullException("models is null");
            //foreach (TEntity model in models)
            //{
            //    this.Entities.Add(model);
            //}
            this._context.SaveChanges();
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity null");

            this.Entities.Remove(entity);

            this._context.SaveChanges();
        }

        
        /// <summary>
        /// 根据ID删除实体
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int id)
        {
            var entity = GetbyId(id);
            if (entity == null)
                throw new ArgumentNullException("entity null");

            this.Entities.Remove(entity);

            this._context.SaveChanges();
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="whereLamdba">条件</param>
        public void DeleteBy(Expression<Func<TEntity, bool>> whereLamdba)
        {
            var deleteModels = Entities.Where<TEntity>(whereLamdba).Select(c => c);

            if (deleteModels == null || deleteModels.Count() == 0)
                return;

            foreach (var deleteModel in deleteModels)
            {
                Entities.Remove(deleteModel);
            }
            _context.SaveChanges();

        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetbyId(object id)
        {
            return this.Entities.Find(id);
        }

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="searchCondition">搜索表达式</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <returns>分页集合类</returns>
        public IPagedList<TEntity> SearchWithPager(Expression<Func<TEntity, bool>> searchCondition, int pageSize, int pageIndex)
        {
            return SearchWithPager(searchCondition, pageSize, pageIndex, null);
        }

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="searchCondition">搜索表达式</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="sortOrder">排序方向</param>
        /// <param name="sortPredicate">排序条件</param>
        /// <returns>分页集合类</returns>
        public IPagedList<TEntity> SearchWithPager(Expression<Func<TEntity, bool>> searchCondition, int pageSize, int pageIndex, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder= SortOrder.Desc)
        {
            IQueryable<TEntity> query;

            if (searchCondition != null)
                query = this.Entities.Where(searchCondition.Compile()).AsQueryable();
            else
                query = this.Entities;

            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Asc:
                        query = query.SortBy(sortPredicate);
                        break;
                    case SortOrder.Desc:
                        query = query.SortByDescending(sortPredicate);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                query = query.SortBy(c => c.Id);
            }

            return new PagedList<TEntity>(query, pageIndex, pageSize);

        }
        /// <summary>
        /// 统计数量
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.Where(expression).Count();
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return Count(expression) > 0;
        }

        /// <summary>
        ///  通用查询
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public IList<TEntity> Search(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.Where(expression).Select(c => c).ToList();
        }
        /// <summary>
        ///  通用查询
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="sector">选择器</param>
        /// <returns></returns>
        public IList<TResult> Search<TResult>(Expression<Func<TEntity, bool>> expression, Func<TEntity, TResult> sector)
        {
            return Entities.Where(expression).Select(sector).ToList();
        }
        /// <summary>
        ///  通用查询
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public TEntity SearchSingle(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.Where(expression).Select(c => c).FirstOrDefault();
        }
        #endregion



        #region Dispose

        public override void DoDispose()
        {
            DisposeIt(_context);
        }
        #endregion
    }
}
