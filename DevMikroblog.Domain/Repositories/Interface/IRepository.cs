using System;
using System.Linq;
using System.Linq.Expressions;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.Repositories.Interface
{
    public interface IRepository<TEntity>
    {
        T Query<T>(Expression<Func<IQueryable<TEntity>,T>> func);
        int SaveChanges();
    }
}
