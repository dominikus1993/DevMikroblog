using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Interface;
using DevMikroblog.Domain.Services.Interface;

namespace DevMikroblog.Domain.Services.Implementation
{
    public class PostService:BaseService<Post>,IPostService
    {

        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public override Result<T> Query<T>(Expression<Func<IQueryable<Post>, T>> func)
        {
            var compiledFunc = func.Compile();
            return Result<T>.WarningWhenNoData(_postRepository.Query(compiledFunc));
        }

        public override int SaveChanges()
        {
            return _postRepository.SaveChanges();
        }

        public Result<List<Post>> Posts => Result<List<Post>>.WarningWhenNoData(_postRepository.Posts?.ToList());

        public Result<Post> Read(long id)
        {
            return Result<Post>.WarningWhenNoData(_postRepository.Read(id));
        }

        public Result<bool> Update(Post post)
        {
            bool queryResult = _postRepository.Update(post);
            _postRepository.SaveChanges();
            return Result<bool>.WarningWhenNoData(queryResult);
        }

        public Result<bool> UpdateWithTags(Post post)
        {
            bool queryResult = _postRepository.UpdateWithTags(post);
            _postRepository.SaveChanges();
            return Result<bool>.WarningWhenNoData(queryResult);
        }

        public Result<Post> Create(Post post)
        {

            var queryResult = _postRepository.Create(post);
            _postRepository.SaveChanges();
            return Result<Post>.WarningWhenNoData(queryResult);
        }

        public Result<bool> Delete(long id)
        {
            bool queryResult = _postRepository.Delete(id);
            _postRepository.SaveChanges();
            return Result<bool>.WarningWhenNoData(queryResult);
        }

        public Result<Post> VoteUp(long id, string userId)
        {
            var vote = new Vote()
            {
                PostId = id,
                UserId = userId,
                UserVote = UserVote.VoteUp
            };
            var queryResult = _postRepository.Vote(id, vote, rate => rate + 1);
            _postRepository.SaveChanges();
            return Result<Post>.WarningWhenNoData(queryResult);

        }

        public Result<Post> VoteDown(long id, string userId)
        {
            var vote = new Vote()
            {
                PostId = id,
                UserId = userId,
                UserVote = UserVote.VoteDown
            };
            var queryResult = _postRepository.Vote(id, vote, rate => rate - 1);
            return Result<Post>.WarningWhenNoData(queryResult);
        }

        public Result<List<Post>> GetPostsByUser(string userId)
        {
            var queryResult = _postRepository.Posts.Where(x => x.AuthorId == userId).ToList();
            return Result<List<Post>>.WarningWhenNoData(queryResult);
        }

        public Result<List<Post>> Read(string authorName)
        {
            var queryResult = _postRepository.Read(authorName);
            return Result<List<Post>>.WarningWhenNoData(queryResult);
        }
    }
}
