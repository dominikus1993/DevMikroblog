using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Interface;
using DevMikroblog.Domain.Services.Interface;
using static Tools.TagUtils.Parsers;

namespace DevMikroblog.Domain.Services.Implementation
{
    public class TagService:BaseService<Tag>, ITagService
    {

        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }


        public override Result<T> Query<T>(Expression<Func<IQueryable<Tag>, T>> func)
        {
            var queryFunc = func.Compile();
            return Result<T>.WarningWhenNoData(_tagRepository.Query(queryFunc));
        }

        public override int SaveChanges()
        {
            return _tagRepository.SaveChanges();
        }

        public Result<List<Tag>> ParseTags(string text)
        {
            var tags = TagParser(text, new Regex(@"(?<=#)\w+"));
            var result =  tags.Select(x => new Tag() { Name = x.ToLower() }).ToList();
            return Result<List<Tag>>.WarningWhenNoData(result);
        }

        public Result<List<Tag>> CreateOrUpdateTags(List<Tag> tags)
        {
            var result = tags.Select(tag =>
            {
                if (_tagRepository.Exist(tag.Name.ToLower()))
                {
                    _tagRepository.Update(tag);
                    return tag;
                }
                else
                {
                    return _tagRepository.Create(tag);
                }
            }).ToList();
            _tagRepository.SaveChanges();
            return Result<List<Tag>>.WarningWhenNoData(result);        
        }

        public Result<List<Post>> GetPostByTagName(string tagName)
        {
            var queryResult = _tagRepository.GetPostsByTagName(tagName);
            return Result<List<Post>>.WarningWhenNoData(queryResult);
        }

        public Result<bool> Exist(string tagName)
        {
            return Result<bool>.WarningWhenNoData(_tagRepository.Exist(tagName.ToLower()));
        }
    }
}
