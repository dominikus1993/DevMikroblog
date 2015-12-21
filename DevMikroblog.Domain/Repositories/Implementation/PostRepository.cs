using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DevMikroblog.Domain.DatabaseContext.Interface;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Interface;

namespace DevMikroblog.Domain.Repositories.Implementation
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(IDbContext context) : base(context)
        {
        }

        public override T Query<T>(Func<IQueryable<Post>, T> func)
        {
            return func(Context.Posts);
        }

        public IQueryable<Post> Posts => Context.Posts;

        public Post Create(Post entity)
        {
            return Context.Posts.Add(entity);
        }

        public Post Read(long id)
        {
            return Context.Posts.SingleOrDefault(post => post.Id == id);
        }

        public bool Update(Post entity)
        {
            var post = Context.Posts.SingleOrDefault(x => x.Id == entity.Id);

            if (post != null)
            {
                post.Message = entity.Message;
                post.Title = entity.Title;
                return true;
            }
            return false;
        }

        public bool Delete(long id)
        {
            var postToRemoving = Context.Posts.Include(x => x.Tags).SingleOrDefault(post => post.Id == id);

            if (postToRemoving != null)
            {
                Context.Posts.Remove(postToRemoving);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Post Vote(long id, Vote vote, Func<long, long> downOrUpFunc)
        {
            var query = Context.Posts.SingleOrDefault(post => post.Id == id);

            if (query != null)
            {
                query.Votes.Add(vote);
                query.Rate = downOrUpFunc(query.Rate);
                Context.Votes.Add(vote);
                return query;
            }

            return null;
        }
    }
}
