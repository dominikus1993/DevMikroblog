using System;
using System.Linq;
using System.Linq.Expressions;
using DevMikroblog.Domain.DatabaseContext.Interface;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Interface;

namespace DevMikroblog.Domain.Repositories.Implementation
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
    {
        public IDbContext Context { get; }

        protected BaseRepository(IDbContext context)
        {
            Context = context;
        }

        public abstract T Query<T>(Func<IQueryable<TEntity>, T> func);

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

    }
}
