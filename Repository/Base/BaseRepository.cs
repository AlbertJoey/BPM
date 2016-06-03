using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eage.BPM.Repository
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq.Expressions;
    using IRepository;
    //线程缓存
    using System.Runtime.Remoting.Messaging;
    using System.Security;
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        //1.0实例化EF容器上下文对象类
        //利用此方法每个子线程都会实例化一次EF容器对象，应该优化成2.0
        //BaseDbContext db = new BaseDbContext();

        //2.0针对1.0存在的问题进行优化
        BaseDbContext db
        {
            get
            {
                //从线程缓存中读取数据
                object obj = CallContext.GetData("BaseDbContext");
                if (obj == null)
                {
                    obj = new BaseDbContext();
                    CallContext.SetData("BaseDbContext", obj);
                }
                return obj as BaseDbContext;
            }
        }

        DbSet<TEntity> _dbset;

        public BaseRepository()
        {
            _dbset = db.Set<TEntity>();
        }

        #region Query 查询
        public List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> condition)
        {
            return _dbset.Where(condition).ToList();
        }
        public List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> condition, string[] tablenames)
        {
            if (tablenames == null || tablenames.Any() == false)
            {
                throw new Exception("关联查询表名至少要有一个！！");
            }
            DbQuery<TEntity> query = _dbset;
            foreach (var item in tablenames)
            {
                query = query.Include(item);
            }
            return query.Where(condition).ToList();
        }
        public List<TEntity> QueryOrderBy<Tkey>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, Tkey>> order)
        {
            return _dbset.Where(condition).OrderBy(order).ToList();
        }
        public List<TEntity> OrderByDescending<Tkey>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, Tkey>> order)
        {
            return _dbset.Where(condition).OrderByDescending(order).ToList();
        }
        public List<TEntity> QueryByPage<Tkey>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, Tkey>> order, int pageSize, int pageIndex, out int rowCount)
        {
            if (pageIndex == null || pageIndex < 1)
            {
                pageIndex = 1;
            }
            if (pageSize == null || pageSize < 1)
            {
                pageSize = 10;
            }
            //需要跳过多少行进行查询
            int skipCount = (pageIndex - 1) * pageSize;
            //受影响的行数
            rowCount = _dbset.Where(condition).Count();
            //按照查询条件和排序条件返回对应页码和相应数据量的结果集
            
            try
            {
                var list =_dbset.Where(condition).OrderBy(order).Skip(skipCount).Take(pageSize).ToList(); 
            }
            catch (Exception)
            {
                
                throw;
            }
            return _dbset.Where(condition).OrderBy(order).Skip(skipCount).Take(pageSize).ToList();
        }
        #endregion

        #region 添加方法
        public void Add(TEntity model)
        {
            _dbset.Add(model);
        }
        #endregion

        #region 编辑方法
        public void Edit(TEntity model, string[] propertys)
        {
            if (model == null)
            {
                throw new Exception("要修改的实体不能为空！！");
            }
            if (propertys.Any() == false)
            {
                throw new Exception("要修改的属性至少要有一个！！！");
            }
            //将model追加到EF容器中
            System.Data.Entity.Infrastructure.DbEntityEntry entry = db.Entry(model);
            entry.State = System.Data.Entity.EntityState.Unchanged;
            foreach (var item in propertys)
            {
                entry.Property(item).IsModified = true;
            }
            db.Configuration.ValidateOnSaveEnabled = false;
        }
        #endregion

        #region 删除方法
        public void Delete(TEntity model, bool isAdded)
        {
            if (!isAdded)
            {
                //将model追加到EF容器
                _dbset.Attach(model);
            }
            _dbset.Remove(model);
        }
        #endregion

        #region 调用存储过程返回一个自己指定的类型TResult
        public List<TResult> RunProc<TResult>(string sql, params object[] pamrs)
        {
            return db.Database.SqlQuery<TResult>(sql, pamrs).ToList();
        }

        #endregion

        #region 统一添加
        public int SaveChanges()
        {
            return db.SaveChanges();
        }
        #endregion
    }
}
