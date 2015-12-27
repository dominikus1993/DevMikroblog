using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Services.Interface;

namespace DevMikroblog.Domain.Services.Implementation
{
    public abstract class BaseService<TEntity>:IService<TEntity> 
    {
        public abstract Result<T> Query<T>(Expression<Func<IQueryable<TEntity>, T>> func);
        public abstract int SaveChanges();
    }
}
