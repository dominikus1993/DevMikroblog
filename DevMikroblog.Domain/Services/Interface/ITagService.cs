using System.Collections.Generic;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.Services.Interface
{
    public interface ITagService
    {
        Result<List<Tag>> ParseTags(string text);
        Result<List<Tag>> CreateOrUpdateTags(List<Tag> tags);
        Result<List<Post>> GetPostByTagName(string tagName);
    }
}
