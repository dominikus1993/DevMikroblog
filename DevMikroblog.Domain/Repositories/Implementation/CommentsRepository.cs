using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevMikroblog.Domain.DatabaseContext.Interface;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Interface;

namespace DevMikroblog.Domain.Repositories.Implementation
{
    public class CommentsRepository : BaseRepository<Comment>, ICommentsRepository
    {
        public CommentsRepository(IDbContext context) : base(context)
        {
        }

        public override T Query<T>(Func<IQueryable<Comment>, T> func)
        {
            return func(Context.Comments);
        }

        public Comment Create(Comment entity)
        {
            return Context.Comments.Add(entity);
        }

        public Comment Read(long id)
        {
            return Context.Comments.SingleOrDefault(comment => comment.Id == id);
        }

        public bool Update(Comment entity)
        {
            var comment = Context.Comments.SingleOrDefault(x => x.Id == entity.Id);

            if (comment != null)
            {
                comment.Message = entity.Message;
                return true;
            }
            return false;
        }

        public bool Delete(long id)
        {
            var comment = Context.Comments.SingleOrDefault(x => x.Id == id);

            if (comment != null)
            {
                Context.Comments.Remove(comment);
                return true;
            }

            return false;
        }

        public Comment Vote(long id, Vote vote, Func<long, long> downOrUpFunc)
        {
            var comment = Context.Comments.SingleOrDefault(x => x.Id == id);

            if (comment != null)
            {
                comment.Votes.Add(vote);
                comment.Rate = downOrUpFunc(comment.Rate);
                Context.Votes.Add(vote);
                return comment;
            }
            return null;
        }

        public List<Comment> GetCommentsByPostId(long postId)
        {
            return Context.Comments.Where(comment => comment.PostId == postId).ToList();
        } 

        public IQueryable<Comment> Comments => Context.Comments;
    }
}
