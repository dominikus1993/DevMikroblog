using System.Collections.Generic;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.Services.Interface
{
    public interface ICommentsService:IService<Comment>
    {
        Result<List<Comment>> Comments { get; }
        Result<Comment> Read(long id);
        Result<List<Comment>> GetByPost(long postId);
        Result<bool> Update(Comment comment);
        Result<Comment> Create(Comment comment);
        Result<bool> Delete(long id);
        Result<Comment> VoteUp(Vote vote);
        Result<Comment> VoteDown(Vote vote);
    }
}
