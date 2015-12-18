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

        public Result<List<Post>> Posts => Result<List<Post>>.WarningWhenNoData(_postRepository.Posts?.ToList());

        public Result<Post> Read(long id)
        {
            return Result<Post>.WarningWhenNoData(_postRepository.Read(id));
        }

        public Result<bool> Update(Post post)
        {
            bool queryResult = _postRepository.Update(post);
            return Result<bool>.WarningWhenNoData(queryResult);
        }

        public Result<Post> Create(Post post)
        {

            var queryResult = _postRepository.Create(post);
            return Result<Post>.WarningWhenNoData(queryResult);
        }

        public Result<bool> Delete(long id)
        {
            bool queryResult = _postRepository.Delete(id);
            return Result<bool>.WarningWhenNoData(queryResult);
        }
    }
}
