using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using DevMikroblog.Domain.DatabaseContext.Interface;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Interface;
using DevMikroblog.Domain.Services.Implementation;

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

        public IQueryable<Post> Posts => Context.Posts.Include(post => post.Tags).Include(x => x.Comments).Include(x => x.Votes);

        public List<Post> Read(string authorName)
        {
            return Context.Posts.Include(post => post.Tags)
                    .Include(x => x.Comments)
                    .Include(x => x.Votes)
                    .Where(post => post.AuthorName == authorName)
                    .ToList();
        }

        public Post Create(Post entity)
        {
            return Context.Posts.Add(entity);
        }

        public Post Read(long id)
        {
            return Context.Posts.Include(post => post.Tags)
                .Include(x => x.Comments)
                .Include(x => x.Votes)
                .SingleOrDefault(post => post.Id == id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool UpdateWithTags(Post entity)
        {
            var post = Context.Posts.SingleOrDefault(x => x.Id == entity.Id);

            if (post != null)
            {
                post.Message = entity.Message;
                post.Title = entity.Title;

                var postTags = Context.Tags.Where(tag => tag.Posts.Any(post1 => post1.Id == post.Id)).ToList();

                postTags.ForEach(tag =>
                {
                    post.Tags.Remove(tag);
                });
                var allTags = Context.Tags;
                entity.Tags.ToList().ForEach(tag =>
                {
                    if (!allTags.Any(x => tag.Name == x.Name))
                    {
                        var tagToAdd = Context.Tags.Add(tag);
                        post.Tags.Add(tagToAdd);
                    }
                    else
                    {
                        post.Tags.Add(allTags.Single(x => tag.Name == x.Name));
                    }
                });
                return true;
            }
            return false;
        }

        public bool Delete(long id)
        {
            var postToRemoving = Context.Posts.Include(x => x.Tags).Include(x => x.Comments).Include(x => x.Votes).SingleOrDefault(post => post.Id == id);
       
            if (postToRemoving != null)
            {
                Context.Comments.RemoveRange(postToRemoving.Comments);
                Context.Votes.RemoveRange(postToRemoving.Votes);
                Context.Posts.Remove(postToRemoving);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Post Vote(long id, Vote vote, Func<long, long> downOrUpFunc)
        {
            var query = Context.Posts.Include(post => post.Votes).SingleOrDefault(post => post.Id == id);
            var userHasVote = query?.Votes.SingleOrDefault(x => x.PostId == vote.PostId && x.UserId == vote.UserId);
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
    }
}
