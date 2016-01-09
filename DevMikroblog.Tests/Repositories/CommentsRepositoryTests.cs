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
    public class CommentsRepositoryTests
    {
        private CommentsRepository _commentsRepository;
        private DataGenerator _data;
        private Mock<IDbContext> _context;

        [OneTimeSetUp]
        public void Init()
        {
            var contextMock = MockHelper.Get().GetMockContext();
            _context = contextMock;
            _commentsRepository = new CommentsRepository(_context.Object);
            _data = DataGenerator.Get();
        }

        [Test]
        public void GetCommentTest()
        {
            const int expextedId = 1;
            var result = _commentsRepository.Read(expextedId);
            Assert.IsNotNull(result);
            Assert.AreEqual(expextedId, result.Id);
        }

        [Test]
        public void GetCommentByInvalidId()
        {
            const int expectedId = int.MaxValue;
            var result = _commentsRepository.Read(expectedId);
            Assert.IsNull(result);
        }

        [Test]
        public void AddComment()
        {
            var comment = new Comment()
            {
                Message = "Pozdro",
                AuthorId = "d1u2p3a",
                Author = _data.Users[0]
            };
            var result = _commentsRepository.Create(comment);
            _context.Object.SaveChanges();
            _context.Verify();

        }

        [Test]
        public void UpdateCommentTest()
        {
            var comment = new Comment()
            {
                Id = 1,
                Message = "Pozdro",
                AuthorId = "d1u2p3a",
                Author = _data.Users[0]
            };

            var updateResult = _commentsRepository.Update(comment);
            Assert.That(() => updateResult, Is.True);
        }

        [Test]
        public void QueryResult()
        {
            const int expectedId = 2;
            var result = _commentsRepository.Query(x => x.SingleOrDefault(comment => comment.Id == expectedId));
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedId, result.Id);
        }

        [Test]
        public void DeleteCommentByValidId()
        {
            const int expectedId = 5;
            bool result = _commentsRepository.Delete(expectedId);
            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteCommentByInValidId()
        {
            const int expectedId = int.MaxValue;
            bool result = _commentsRepository.Delete(expectedId);
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidVoteUp()
        {
            var comment = _data.Comments.Skip(1).Take(1).First();
            var vote = new Vote()
            {
                CommentId = comment.Id,
                UserVote = UserVote.VoteUp
            };
            long votesBefore = comment.Rate;
            var result = _commentsRepository.Vote(comment.Id, vote, rate => rate + 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(votesBefore + 1, result.Rate);
        }

        [Test]
        public void InValidVoteUp()
        {
            var comment = new Comment()
            {
                Id = 111111
            };
            var vote = new Vote()
            {
                CommentId = comment.Id,
                UserVote = UserVote.VoteUp
            };

            var result = _commentsRepository.Vote(comment.Id, vote, rate => rate + 1);
            Assert.IsNull(result);
        }

        [Test]
        public void ValidVoteDown()
        {
            var comment = _data.Comments.First();
            var vote = new Vote()
            {
                CommentId = comment.Id,
                UserVote = UserVote.VoteDown
            };
            long votesBefore = comment.Rate;
            var result = _commentsRepository.Vote(comment.Id, vote, rate => rate - 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Rate, votesBefore - 1);
        }

        [Test]
        public void InValidVoteDown()
        {
            var comment = new Comment()
            {
                Id = 111111
            };
            var vote = new Vote()
            {
                CommentId = comment.Id,
                UserVote = UserVote.VoteDown
            };

            var result = _commentsRepository.Vote(comment.Id, vote, rate => rate - 1);
            Assert.IsNull(result);
        }

        [Test]
        public void GetCommentsByPostId()
        {
            const int postId = 1;
            var result = _commentsRepository.GetCommentsByPostId(postId);
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetCommentsByPostInvalidId()
        {
            const int postId = 1111;
            var result = _commentsRepository.GetCommentsByPostId(postId);
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetByPostId()
        {
            const int postId = 1;
            var result = _commentsRepository.GetCommentsByPostId(postId);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetByPostIvalidId()
        {
            const int postId = 111111;
            var result = _commentsRepository.GetCommentsByPostId(postId);
            Assert.IsEmpty(result);
        }
    }
}
