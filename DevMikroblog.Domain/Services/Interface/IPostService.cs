using System;
using System.Collections.Generic;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.Services.Interface
{
    public interface IPostService:IService<Post>
    {
        Result<List<Post>> Posts { get; }
        Result<Post> Read(long id);
        Result<bool> Update(Post post);
        Result<bool> UpdateWithTags(Post post);
        Result<Post> Create(Post post);
        Result<bool> Delete(long id);
        Result<Post> VoteUp(Vote vote);
        Result<Post> VoteDown(Vote vote);
        Result<List<Post>> GetPostsByUser(string userId);
        Result<List<Post>> Read(string authorName);
    }
}
