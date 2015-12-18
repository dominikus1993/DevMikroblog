using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.Services.Interface
{
    public interface IPostService:IService<Post>
    {
        Result<IQueryable<Post>> Posts { get; }
        Result<Post> Read(long id);
        Result<bool> Update(Post post);
        Result<Post> Create(Post post);
        Result<bool> Delete(long id);
    }
}
