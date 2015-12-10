using System;
using System.Linq;
using DevMikroblog.Domain.DatabaseContext.Interface;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Implementation;
using DevMikroblog.Tests.Helpers;
using Moq;
using NUnit.Framework;

namespace DevMikroblog.Tests.Repositories
{
    [TestFixture]
    public class PostRepositoryTests
    {
        private PostRepository _postRepository;
        private DataGenerator _data;
        private Mock<IDbContext> _context;

        [OneTimeSetUp]
        public void Init()
        {
            var contextMock = MockHelper.Get().GetMockContext();
            _context = contextMock;
            _postRepository = new PostRepository(_context.Object);
            _data = DataGenerator.Get();
        }

        [Test]
        public void GetPostTest()
        {
            const int expextedId = 1;
            var result = _postRepository.Read(expextedId);
            Assert.IsNotNull(result);
            Assert.AreEqual(expextedId, result.Id);
        }
        
        [Test]
        public void GetPostByInvalidId()
        {
            const int expectedId = int.MaxValue;
            var result = _postRepository.Read(expectedId);
            Assert.IsNull(result);
        }

        [Test]
        public void AddPost()
        {
            var post = new Post()
            {
                Message = "Pozdro",
                Title = "Pozdro",
                AuthorId = "d1u2p3a",
                Author = _data.Users[0]
            };

            var result = _postRepository.Create(post);
            _context.Object.SaveChanges();
            _context.Verify();
            
        }

        [Test]
        public void UpdatePostTest()
        {
            var post = new Post()
            {
                Id = 1,
                Message = "Pozdro",
                Title = "Pozdro",
                AuthorId = "d1u2p3a",
                Author = _data.Users[0]
            };

            var updateResult = _postRepository.Update(post);
            Assert.IsTrue(updateResult);
        }

        [Test]
        public void QueryResult()
        {
            const int expectedId = 2;
            var result = _postRepository.Query(x => x.SingleOrDefault(post => post.Id == expectedId));
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedId, result.Id);
        }
    }
}
