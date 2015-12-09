using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.Repositories.Interface
{
    public interface IPostRepository : IRepository<Post>, ICrudable<Post, long>
    {
        IQueryable<Post> Posts { get; }
    }
}
