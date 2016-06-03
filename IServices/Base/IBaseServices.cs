using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eage.BPM.IServices
{
    using System.Linq.Expressions;
    /// <summary>
    /// 通用业务操作的约定
    /// By: Albert.joey 20151203
    /// </summary>
    public interface IBaseServices<TEntity>where TEntity:class
    {
        #region Query 查询
        List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> condition);
        List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> condition, string[] tablenames);
        List<TEntity> QueryOrderBy<Tkey>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, Tkey>> order);
        List<TEntity> OrderByDescending<Tkey>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, Tkey>> order);
        List<TEntity> QueryByPage<Tkey>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, Tkey>> order, int pageSize, int pageIndex, out int rowCount);
        #endregion

        #region 添加方法
        void Add(TEntity model);
        #endregion

        #region 编辑方法
        void Edit(TEntity model, string[] propertys);
        #endregion

        #region 删除方法
        void Delete(TEntity model, bool isAdded);
        #endregion

        #region 统一添加
        int SaveChanges();
        #endregion 
    }
}
