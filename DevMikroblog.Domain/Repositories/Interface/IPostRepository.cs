using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.Repositories.Interface
{
    public interface IPostRepository : IRepository<Post>, ICrudable<Post, long>, IVotable<Post>
    {
        IQueryable<Post> Posts { get; }
        bool UpdateWithTags(Post entity);
        List<Post> Read(string authorName);
    }
}
