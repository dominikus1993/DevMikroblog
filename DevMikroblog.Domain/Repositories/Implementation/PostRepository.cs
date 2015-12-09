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
    public class PostRepository:BaseRepository<Post>,IPostRepository
    {
        public PostRepository(IDbContext context) : base(context)
        {
        }

        public override T Query<T>(Expression<Func<IQueryable<Post>, T>> func)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Post> Posts => Context.Posts;

        public Post Create(Post entity)
        {
            throw new NotImplementedException();
        }

        public Post Read(long id)
        {
            return Context.Posts.SingleOrDefault(post => post.Id == id);
        }

        public bool Update(Post entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
