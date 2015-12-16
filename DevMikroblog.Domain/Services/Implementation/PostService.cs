using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Interface;
using DevMikroblog.Domain.Services.Interface;

namespace DevMikroblog.Domain.Services.Implementation
{
    public class PostService:BaseService<Post>,IPostService
    {

        private readonly IPostRepository _repository;

        public PostService(IPostRepository repository)
        {
            _repository = repository;
        }

        public override Result<T> Query<T>(Expression<Func<IQueryable<Post>, T>> func)
        {
            var compiledFunc = func.Compile();
            return Result<T>.WarningWhenNoData(compiledFunc(_repository.Posts));
        }

        public Result<IQueryable<Post>> Posts => Result<IQueryable<Post>>.WarningWhenNoData(_repository.Posts);

        public Result<Post> Read(long id)
        {
            return Result<Post>.WarningWhenNoData(_repository.Read(id));
        }
    }
}
