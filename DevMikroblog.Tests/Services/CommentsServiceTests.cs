using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Interface;
using DevMikroblog.Domain.Services.Implementation;
using DevMikroblog.Tests.Helpers;
using Moq;
using NUnit.Framework;

namespace DevMikroblog.Tests.Services
{
    [TestFixture]
    class CommentsServiceTests
    {
        private Mock<ICommentsRepository> _repositoryMock;
        private CommentsService _service;
        private DataGenerator _data;

        [OneTimeSetUp]
        public void Init()
        {
            _data = DataGenerator.Get();
            _repositoryMock = MockHelper.MockCommentsRepository();
            _service = new CommentsService(_repositoryMock.Object);
        }

        [Test]
        public void GetAll()
        {
            var result = _service.Comments;
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
            var comment = new Comment()
            {
                Id = 1,
                Message = "Pozdro",
                AuthorId = "d1u2p3a",
                Author = _data.Users[0]
            };

            var result = _service.Update(comment);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Value);
        }

        [Test]
        public void Create()
        {
            var comment = new Comment()
            {
                Id = 22,
                Message = "Pozdro",
                AuthorId = "d1u2p3a",
                Author = _data.Users[0]
            };

            var result = _service.Create(comment);
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
                CommentId = id,
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
                CommentId = id,
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
                CommentId = id,
                UserId = userId
            };

            var result = _service.VoteDown(vote);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
        }
    }
}
