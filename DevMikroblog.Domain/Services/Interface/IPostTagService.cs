using System;
using System.Collections.Generic;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.Services.Interface
{
    public interface IPostTagService
    {
        Result<List<Post>> Posts { get; }
        Result<Post> GetPostById(long id);
        Result<Post> CreatePost(Post post);
        Result<bool> DeletePost(long id, string userId);
        Result<List<Post>> GetPostByTagName(string tagName);
        Result<List<Post>> GetPostByAuthorName(string authorName);
        Result<bool> UpdatePost(Post postToUpdate, string userId);
    }
}
