using System.Collections.Generic;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.Repositories.Interface
{
    public interface ITagRepository:IRepository<Tag>
    {
        Tag CreateOrModify(string tagName, Post post);
        Tag Delete(string tagName);
        Tag Find(string tagName);
        List<Post> GetPostsByTagName(string tagName);
    }
}
