using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eage.BPM.Services
{
    using IRepository;
    using Eage.BPM.IServices;
    using System.Linq.Expressions;

    /// <summary>
    /// 对于通用业务操作的实现
    /// By: Albert.joey 20151203
    /// </summary>
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class
    {
        //定义数据仓储的接口
        protected IBaseRepository<TEntity> _basedal;

        #region Query 查询
        public List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> condition)
        {
            return _basedal.QueryWhere(condition);
        }
        public List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> condition, string[] tablenames)
        {
            return _basedal.QueryWhere(condition, tablenames);
        }
        public List<TEntity> QueryOrderBy<Tkey>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, Tkey>> order)
        {
            return _basedal.QueryOrderBy(condition, order);
        }
        public List<TEntity> OrderByDescending<Tkey>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, Tkey>> order)
        {
            return _basedal.OrderByDescending(condition, order);
        }
        public List<TEntity> QueryByPage<Tkey>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, Tkey>> order, int pageSize, int pageIndex, out int rowCount)
        {
            return _basedal.QueryByPage<Tkey>(condition, order, pageSize, pageIndex,out rowCount);
        }
        #endregion

        #region 添加方法
        public void Add(TEntity model)
        {
            _basedal.Add(model);
        }
        #endregion

        #region 编辑方法
        public void Edit(TEntity model, string[] propertys)
        {
            _basedal.Edit(model, propertys);
        }
        #endregion

        #region 删除方法
        public void Delete(TEntity model, bool isAdded)
        {
            _basedal.Delete(model, isAdded);
        }
        #endregion

        #region 统一添加
        public int SaveChanges()
        {
            return _basedal.SaveChanges();
        }
        #endregion 
    }
}
