using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.Services.Interface
{
    public interface IService<TEntity>
    {
        Result<T> Query<T>(Expression<Func<IQueryable<TEntity>, T>> func);
    }
}
