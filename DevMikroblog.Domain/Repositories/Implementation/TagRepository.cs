using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DevMikroblog.Domain.DatabaseContext.Interface;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Interface;

namespace DevMikroblog.Domain.Repositories.Implementation

{
    public class TagRepository:BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(IDbContext context) : base(context)
        {
        }

        public IQueryable<Tag> Tags => Context.Tags;


        public Tag Create(Tag tag)
        {
            return Context.Tags.Add(tag);
        }

        public bool Update(Tag tag)
        {
            var tagToEdit = Context.Tags.Include(tag_ => tag_.Posts).SingleOrDefault(x => x.Name == tag.Name);

            if (tagToEdit != null)
            {
                foreach (var post in tag.Posts)
                {
                    tagToEdit.Posts.Add(post);
                }
                return true;
            }
            return false;
        }

        public Tag Delete(string tagName)
        {
            throw new NotImplementedException();
        }

        public Tag Find(string tagName)
        {
            return Context.Tags.SingleOrDefault(x => x.Name == tagName);
        }

        public List<Post> GetPostsByTagName(string tagName)
        {
            return Context.Tags.Include(x => x.Posts).SingleOrDefault(x => x.Name == tagName)?.Posts.ToList();
        }

        public bool Exist(string tagName)
        {
            var result = Context.Tags.SingleOrDefault(x => x.Name == tagName);
            return result != null;
        }


        public override T Query<T>(Func<IQueryable<Tag>, T> func)
        {
            return func(Context.Tags);
        }
    }
}
