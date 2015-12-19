using System.Collections.Generic;
using System.Linq;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.Repositories.Interface
{
    public interface ICommentsRepository: IRepository<Comment>, ICrudable<Comment, long>, IVotable<Comment>
    {
        IQueryable<Comment> Comments { get; }
        List<Comment> GetCommentsByPostId(long postId);
    }
}
