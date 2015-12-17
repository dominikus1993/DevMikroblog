using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Interface;
using DevMikroblog.Domain.Services.Interface;
using static Tools.TagUtils.Parsers;

namespace DevMikroblog.Domain.Services.Implementation
{
    public class TagService:BaseService<Tag>, ITagService
    {

        private ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }


        public override Result<T> Query<T>(Expression<Func<IQueryable<Tag>, T>> func)
        {
            throw new NotImplementedException();
        }

        public Result<List<Tag>> ParseTags(string text)
        {
            var tags = TagParser(text, new Regex(@"(?<=#)\w+"));
            var result =  tags.Select(x => new Tag() { Name = x }).ToList();
            return Result<List<Tag>>.WarningWhenNoData(result);
        }

        public Result<List<Tag>> CreateOrUpdateTags(List<Tag> tags)
        {
            var result = tags.Select(tag =>
            {
                if (_tagRepository.Exist(tag.Name))
                {
                    tag.Posts = _tagRepository.Find(tag.Name).Posts;
                    _tagRepository.Update(tag);
                    return tag;
                }
                else
                {
                    return _tagRepository.Create(tag);
                }
            }).ToList();
            return Result<List<Tag>>.WarningWhenNoData(result);        
        }

    }
}
