using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            return Context.Comments.Include(comment => comment.Votes).SingleOrDefault(comment => comment.Id == id);
        }

        public List<Comment> GetByPost(long postId)
        {
            return
                Context.Comments.Include(comment => comment.Votes).Where(comment => comment.PostId == postId).ToList();
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
                var votes = Context.Votes.Where(vote => vote.CommentId == id);
                Context.Votes.RemoveRange(votes);
                Context.Comments.Remove(comment);
                return true;
            }

            return false;
        }

        public Comment Vote(long id, Vote vote, Func<long, long> downOrUpFunc)
        {
            var query = Context.Comments.Include(comment => comment.Votes).SingleOrDefault(post => post.Id == id);
            var userHasVote = query?.Votes.SingleOrDefault(x => x.CommentId == vote.CommentId && x.UserId == vote.UserId);
            if (query != null && userHasVote == null)
            {
                query.Votes.Add(vote);
                query.Rate = downOrUpFunc(query.Rate);
                Context.Votes.Add(vote);
                return query;
            }
            if (query != null && userHasVote.UserVote != vote.UserVote)
            {
                Context.Votes.Remove(userHasVote);
                query.Votes.Add(vote);
                query.Rate = downOrUpFunc(query.Rate) + ((-1) * downOrUpFunc(query.Rate));
                Context.Votes.Add(vote);
                return query;
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
