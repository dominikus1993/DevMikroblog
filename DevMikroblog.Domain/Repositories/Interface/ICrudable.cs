using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevMikroblog.Domain.Repositories.Interface
{
    public interface ICrudable<TEntity, TId>
    {
        TEntity Create(TEntity entity);
        TEntity Read(TId id);
        bool Update(TEntity entity);
        bool Delete(TId id);
    }
}
