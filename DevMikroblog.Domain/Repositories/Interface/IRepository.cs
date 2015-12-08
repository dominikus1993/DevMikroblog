using System;
using System.Linq;
using System.Linq.Expressions;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.Repositories.Interface
{
    public interface IRepository<TEntity>
    {
        Result<T> Query<T>(Expression<Func<IQueryable<TEntity>, Result<T>>> func);
        int SaveChanges { get; }
    }
}
