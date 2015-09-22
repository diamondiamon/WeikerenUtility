using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.MDB
{
    public class Repository<TEntity> : DisposableObject,IRepository<TEntity>
        where TEntity : BaseEntity,new()
    {
        private readonly IDataBaseContext _context;
        private IMongoCollection<TEntity> _entities;

        /// <summary>
        /// 数据库上下文      
        /// </summary>
        public IDataBaseContext Context
        {
            get { return _context; }
        }

        /// <summary>
        /// 数据实体
        /// </summary>
        private IMongoCollection<TEntity> Entities
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
                return _context.Set<TEntity>().AsQueryable();
            }
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
            if (model.Id == 0)
            {
                model.Id = _context.GetIncrementId<TEntity>();
            }
            Entities.InsertOneAsync(model).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 添加多个实体
        /// </summary>
        /// <param name="models"></param>
        public void Add(IList<TEntity> models)
        {
            if (models == null)
                throw new ArgumentNullException("entity null");

            Entities.InsertManyAsync(models).GetAwaiter().GetResult();
            
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity null");

            var filter = Builders<TEntity>.Filter.Eq(s => s.Id, entity.Id);

            
            var fieldList = new List<UpdateDefinition<TEntity>>();
            foreach (var property in typeof(TEntity).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (property.Name != entity.EntityKey)//更新集中不能有实体键_id
                {
                    fieldList.Add(Builders<TEntity>.Update.Set(property.Name, property.GetValue(entity)));
                }
            }
            
            ForWait(() => Entities.UpdateOneAsync(filter, Builders<TEntity>.Update.Combine(fieldList))).GetAwaiter().GetResult();
            
                
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="models"></param>
        public void Update(IList<TEntity> models)
        {
            if (models == null)
                throw new ArgumentNullException("models is null");
            
            if(models!=null && models.Count>0)
            {
                foreach (var item in models)
                {
                    Update(item);
                }
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity null");

            var filter = Builders<TEntity>.Filter.Eq(s => s.Id, entity.Id);
            var result  = Entities.DeleteOneAsync(filter).GetAwaiter().GetResult();
        }

        
        /// <summary>
        /// 根据ID删除实体
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int id)
        {
            var filter = Builders<TEntity>.Filter.Eq(s => s.Id, id);
            var result = Entities.DeleteOneAsync(filter).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="whereLamdba">条件</param>
        public void DeleteBy(Expression<Func<TEntity, bool>> whereLamdba)
        {
            Entities.DeleteManyAsync(whereLamdba).GetAwaiter().GetResult();
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetbyId(int id)
        {
            var filter = Builders<TEntity>.Filter.Eq(s => s.Id, id);
            var model = Entities.Find(filter).FirstOrDefaultAsync().GetAwaiter().GetResult();
            //var model = Table.Where(c => c.Id == id).FirstOrDefault();

            return model;
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
                query = this.Table.Where(searchCondition.Compile()).AsQueryable();
            else
                query = this.Table;

            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Asc:
                        query = query.OrderBy(sortPredicate);
                        break;
                    case SortOrder.Desc:
                        query = query.OrderByDescending(sortPredicate);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                query = query.OrderBy(c => c.Id);
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
            return (int)Entities.CountAsync(expression).GetAwaiter().GetResult();
            //Entities.CountAsync()
            //throw new NotImplementedException();
            //_context.Set<TEntity>().Count();
            //return Entities.Where(expression).Count();
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            var result = Table.Where(expression).FirstOrDefault();
            return (result != null);
        }

        /// <summary>
        ///  通用查询
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public IList<TEntity> Search(Expression<Func<TEntity, bool>> expression)
        {
            return Table.Where(expression).Select(c => c).ToList();
        }
        /// <summary>
        ///  通用查询
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="sector">选择器</param>
        /// <returns></returns>
        public IList<TResult> Search<TResult>(Expression<Func<TEntity, bool>> expression, Func<TEntity, TResult> sector)
        {
            return Table.Where(expression).Select(sector).ToList();
        }
        /// <summary>
        ///  通用查询
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public TEntity SearchSingle(Expression<Func<TEntity, bool>> expression)
        {
            return Table.Where(expression).Select(c => c).FirstOrDefault();
        }
        #endregion



        #region Dispose

        public override void DoDispose()
        {
            DisposeIt(_context);
        }
        #endregion

        /// <summary>
        /// 等待Task执行完成后再返回
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        private Task ForWait(Func<Task> func)
        {
            var t = func();
            t.Wait();
            return t;
        }
    }
}
