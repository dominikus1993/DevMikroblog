using System;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.Repositories.Interface
{
    public interface IVotable<TEntity>
    {
        TEntity Vote(TEntity entity, Vote vote, Func<long,long> downOrUpFunc);
    }
}
