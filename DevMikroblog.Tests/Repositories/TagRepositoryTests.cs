using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DevMikroblog.Domain.DatabaseContext.Interface;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Implementation;
using DevMikroblog.Tests.Helpers;
using Moq;
using NUnit.Framework;

namespace DevMikroblog.Tests.Repositories
{
    [TestFixture]
    public class TagRepositoryTests
    {
        private TagRepository _tagRepository;
        private DataGenerator _data;
        private Mock<IDbContext> _context;

        [OneTimeSetUp]
        public void Init()
        {
            _data = DataGenerator.Get();
            _context = MockHelper.Get().GetMockContext();
            _tagRepository = new TagRepository(_context.Object);
        }

        [Test]
        public void GetAllTags()
        {
            var result = _tagRepository.Tags;
            Assert.IsNotNull(result);
        }

        [Test]
        public void Create()
        {
            //Should return new tag
            var tag = new Tag()
            {
                Id = 3,
                Name = "programowanie",
                Posts = _data.Posts.Take(2).ToList()
            };

            var result = _tagRepository.Create(tag);
            _context.Object.SaveChanges();
            _context.Verify();
        }

        [Test]
        public void Query()
        {
            var result = _tagRepository.Query(x => x.SingleOrDefault(post => post.Id == 1));
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
        }

        [Test]
        public void FindTag()
        {
            const string tagName = "csharp";
            var tag = _tagRepository.Find(tagName);
            Assert.IsNotNull(tag);
            Assert.AreEqual(tag.Name, tagName);
        }

        [Test]
        public void GetPostByTagName()
        {
            const string tagName = "csharp";
            var result = _tagRepository.GetPostsByTagName(tagName);
        }

        [Test]
        public void UpdateWithValidTag()
        {
            var tag = new Tag()
            {
                Id = 1,
                Name = "aa",
                Posts = new List<Post>()
            };

            bool result = _tagRepository.Update(tag);
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateWithInValidTag()
        {
            var tag = new Tag()
            {
                Id = 11111,
                Name = "aaa",
                Posts = new List<Post>()
            };

            bool result = _tagRepository.Update(tag);
            Assert.IsFalse(result);
        }

        [Test]
        public void Exist()
        {
            const string tagName = "csharp";
            bool result = _tagRepository.Exist(tagName);
            Assert.IsTrue(result);
        }
    }
}
