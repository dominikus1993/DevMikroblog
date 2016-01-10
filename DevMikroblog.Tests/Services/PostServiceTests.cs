using System.Linq;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Interface;
using DevMikroblog.Domain.Services.Implementation;
using DevMikroblog.Domain.Services.Interface;
using DevMikroblog.Tests.Helpers;
using Moq;
using NUnit.Framework;

namespace DevMikroblog.Tests.Services
{
    [TestFixture]
    public class PostServiceTests
    {
        private Mock<IPostRepository> _repositoryMock;
        private PostService _service;
        private DataGenerator _data;

        [OneTimeSetUp]
        public void Init()
        {
            _data = DataGenerator.Get();
            _repositoryMock = MockHelper.MockPostRepository();
            _service = new PostService(_repositoryMock.Object);
        }

        [Test]
        public void GetAll()
        {
            var result = _service.Posts;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public void GetValidId()
        {
            const int id = 1;
            var result = _service.Read(id);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(result.Value.Id, id);
        }

        [Test]
        public void GetInValidId()
        {
            const int id = 2;
            var result = _service.Read(id);
            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void Update()
        {
            var post = new Post()
            {
                Id = 1,
                Message = "Pozdro",
                Title = "Pozdro",
                AuthorId = "d1u2p3a",
                Author = _data.Users[0]
            };

            var result = _service.Update(post);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Value);
        }

        [Test]
        public void Create()
        {
            var post = new Post()
            {
                Id = 22,
                Message = "Pozdro",
                Title = "Pozdro",
                AuthorId = "d1u2p3a",
                Author = _data.Users[0]
            };

            var result = _service.Create(post);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public void ValidVoteUp()
        {
            const int id = 1;
            string userId = _data.Users.First().Id;
            var vote = new Vote()
            {
                PostId = id,
                UserId = userId
            };

            var result = _service.VoteUp(vote);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public void InValidVoteDown()
        {
            const int id = 111;
            string userId = _data.Users.First().Id;

            var vote = new Vote()
            {
                PostId = id,
                UserId = userId
            };

            var result = _service.VoteDown(vote);
            Assert.IsTrue(result.IsWarning);
            Assert.IsNull(result.Value);
        }

        [Test]
        public void ValidVoteDown()
        {
            const int id = 1;
            string userId = _data.Users.First().Id;

            var vote = new Vote()
            {
                PostId = id,
                UserId = userId
            };

            var result = _service.VoteDown(vote);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public void GetPostsByUserId()
        {
            const string userId = "d1u2p3a";
            var result = _service.GetPostsByUser(userId);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotEmpty(result.Value);
        }

        [Test]
        public void GetPostsByinvalidUserId()
        {
            const string userId = "d1u2p3121221212121211221a";
            var result = _service.GetPostsByUser(userId);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsEmpty(result.Value);
        }

        [Test]
        public void GetPostByAuthorName()
        {
            const string authorName = "d1u2p3a";
            var result = _service.Read(authorName);
            Assert.That(result, Is.Not.Null);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotEmpty(result.Value);
        }

        [Test]
        public void GetPostByInvalidAuthorName()
        {
            const string authorName = "d1u2p3a111111111";
            var result = _service.Read(authorName);
            Assert.That(result, Is.Not.Null);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsEmpty(result.Value);
        }
    }
}
