using System;
using System.Collections.Generic;
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
        public void GetPostByAuthorName()
        {
            const string authorName = "d1u2p3a";
            var result = _postRepository.Read(authorName);
            Assert.That(result, Is.Not.Null);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetPostByInvalidAuthorName()
        {
            const string authorName = "d1u2p3a11111";
            var result = _postRepository.Read(authorName);
            Assert.That(result, Is.Not.Null);
            Assert.IsEmpty(result);
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

            bool updateResult = _postRepository.Update(post);
            Assert.IsTrue(updateResult);
        }

        [Test]
        public void UpdatePostAndDeleteTagsTest()
        {
            var post = new Post()
            {
                Id = 1,
                Message = "Pozdro",
                Title = "Pozdro",
                AuthorId = "d1u2p3a",
                Author = _data.Users[0]
            };

            bool updateResult = _postRepository.Update(post);
            Assert.IsTrue(updateResult);
        }
        [Test]
        public void QueryResult()
        {
            const int expectedId = 2;
            var result = _postRepository.Query(x => x.SingleOrDefault(post => post.Id == expectedId));
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(expectedId, result.Id);
        }

        [Test]
        public void DeletePostByValidId()
        {
            const int expectedId = 6;
            bool result = _postRepository.Delete(expectedId);
            Assert.IsTrue(result);
        }

        [Test]
        public void DeletePostByInValidId()
        {
            const int expectedId = int.MaxValue;
            bool result = _postRepository.Delete(expectedId);
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidVoteUp()
        {
            var postToVoteUp = _data.Posts.Skip(1).Take(1).First();
            var vote = new Vote()
            {
                PostId = postToVoteUp.Id,
                UserVote = UserVote.VoteUp
            };
            long votesBefore = postToVoteUp.Rate;
            var result = _postRepository.Vote(postToVoteUp.Id, vote, rate => rate + 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(votesBefore + 1, result.Rate);
        }

        [Test]
        public void InValidVoteUp()
        {
            var postToVoteUp = new Post()
            {
                Id = 111111
            };
            var vote = new Vote()
            {
                PostId = postToVoteUp.Id,
                UserVote = UserVote.VoteUp
            };

            var result = _postRepository.Vote(postToVoteUp.Id, vote, rate => rate + 1);
            Assert.IsNull(result);
        }

        [Test]
        public void ValidVoteDown()
        {
            var postToVoteUp = _data.Posts.First();
            var vote = new Vote()
            {
                PostId = postToVoteUp.Id,
                UserVote = UserVote.VoteDown
            };
            long votesBefore = postToVoteUp.Rate;
            var result = _postRepository.Vote(postToVoteUp.Id, vote, rate => rate - 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Rate, votesBefore - 1);
        }

        [Test]
        public void InValidVoteDown()
        {
            var postToVoteUp = new Post()
            {
                Id = 111111
            };
            var vote = new Vote()
            {
                PostId = postToVoteUp.Id,
                UserVote = UserVote.VoteDown
            };

            var result = _postRepository.Vote(postToVoteUp.Id, vote, rate => rate - 1);
            Assert.IsNull(result);
        }
    }
}
