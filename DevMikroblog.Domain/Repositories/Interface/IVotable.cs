using System;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.Repositories.Interface
{
    public interface IVotable<TEntity>
    {
        TEntity Vote(long id, Vote vote, Func<long,long> downOrUpFunc);
    }
}
