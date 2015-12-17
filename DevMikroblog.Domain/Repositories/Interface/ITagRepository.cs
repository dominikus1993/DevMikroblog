using System.Collections.Generic;
using System.Linq;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.Repositories.Interface
{
    public interface ITagRepository:IRepository<Tag>
    {
        IQueryable<Tag> Tags { get; }
        Tag Create(Tag tag);
        bool Update(Tag tag);
        Tag Delete(string tagName);
        Tag Find(string tagName);
        List<Post> GetPostsByTagName(string tagName);
        bool Exist(string tagName);
    }
}
