using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DevMikroblog.Domain.DatabaseContext.Interface;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Interface;

namespace DevMikroblog.Domain.Repositories.Implementation
{
    public class TagRepository:BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(IDbContext context) : base(context)
        {
        }

        public Tag CreateOrModify(string tagName, Post post)
        {
            throw new NotImplementedException();
        }

        public Tag Delete(string tagName)
        {
            throw new NotImplementedException();
        }

        public Tag Find(string tagName)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetPostsByTagName(string tagName)
        {
            throw new NotImplementedException();
        }

        public override T Query<T>(Expression<Func<IQueryable<Tag>, T>> func)
        {
            throw new NotImplementedException();
        }
    }
}
