using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Interface;
using DevMikroblog.Domain.Services.Interface;

namespace DevMikroblog.Domain.Services.Implementation
{
    public class CommentsService:BaseService<Comment>,ICommentsService
    {

        private readonly ICommentsRepository _commentsRepository;

        public CommentsService(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }


        public override Result<T> Query<T>(Expression<Func<IQueryable<Comment>, T>> func)
        {
            var queryFunc = func.Compile();
            return Result<T>.WarningWhenNoData(_commentsRepository.Query(queryFunc));
        }

        public override int SaveChanges()
        {
            return _commentsRepository.SaveChanges();
        }

        public Result<List<Comment>> Comments => Result<List<Comment>>.WarningWhenNoData(_commentsRepository.Comments?.ToList());


        public Result<Comment> Read(long id)
        {
            return Result<Comment>.WarningWhenNoData(_commentsRepository.Read(id));
        }

        public Result<bool> Update(Comment comment)
        {
            bool queryResult = _commentsRepository.Update(comment);
            _commentsRepository.SaveChanges();
            return Result<bool>.WarningWhenNoData(queryResult);
        }

        public Result<Comment> Create(Comment comment)
        {
            var queryResult = _commentsRepository.Create(comment);
            _commentsRepository.SaveChanges();
            return Result<Comment>.WarningWhenNoData(queryResult);
        }

        public Result<bool> Delete(long id)
        {
            bool queryResult = _commentsRepository.Delete(id);
            _commentsRepository.SaveChanges();
            return Result<bool>.WarningWhenNoData(queryResult);
        }

        public Result<Comment> VoteUp(long id, string userId)
        {
            var vote = new Vote()
            {
                CommentId = id,
                UserId = userId,
                UserVote = UserVote.VoteUp
            };
            var queryResult = _commentsRepository.Vote(id, vote, rate => rate + 1);
            return Result<Comment>.WarningWhenNoData(queryResult);
        }

        public Result<Comment> VoteDown(long id, string userId)
        {
            var vote = new Vote()
            {
                CommentId = id,
                UserId = userId,
                UserVote = UserVote.VoteDown
            };
            var queryResult = _commentsRepository.Vote(id, vote, rate => rate - 1);
            return Result<Comment>.WarningWhenNoData(queryResult);
        }
    }
}
