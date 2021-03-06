﻿using System;
using System.Linq;
using System.Linq.Expressions;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.Services.Interface
{
    public interface IService<TEntity>
    {
        Result<T> Query<T>(Expression<Func<IQueryable<TEntity>, T>> func);
        int SaveChanges();
    }
}
