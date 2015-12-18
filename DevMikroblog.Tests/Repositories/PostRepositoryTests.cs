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
            var postToVoteUp = _data.Posts.First();
            var vote = new Vote()
            {
                Post = postToVoteUp,
                PostId = postToVoteUp.Id,
                UserVote = UserVote.VoteUp
            };
            long votesBefore = postToVoteUp.Rate;
            var result = _postRepository.Vote(postToVoteUp, vote, rate => rate + 1);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Rate, votesBefore + 1);
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
                Post = postToVoteUp,
                PostId = postToVoteUp.Id,
                UserVote = UserVote.VoteUp
            };

            var result = _postRepository.Vote(postToVoteUp, vote, rate => rate + 1);
            Assert.IsNull(result);
        }

        [Test]
        public void ValidVoteDown()
        {
            var postToVoteUp = _data.Posts.First();
            var vote = new Vote()
            {
                Post = postToVoteUp,
                PostId = postToVoteUp.Id,
                UserVote = UserVote.VoteDown
            };
            long votesBefore = postToVoteUp.Rate;
            var result = _postRepository.Vote(postToVoteUp, vote, rate => rate - 1);
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
                Post = postToVoteUp,
                PostId = postToVoteUp.Id,
                UserVote = UserVote.VoteDown
            };

            var result = _postRepository.Vote(postToVoteUp, vote, rate => rate - 1);
            Assert.IsNull(result);
        }
    }
}
