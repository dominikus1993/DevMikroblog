using DevMikroblog.Domain.Services.Interface;
using DevMikroblog.Tests.Helpers;
using DevMikroblog.WebApp.Controllers;
using Moq;
using NUnit.Framework;

namespace DevMikroblog.Tests.Controllers
{
    [TestFixture]
    public class VoteControllerTest
    {
        private Mock<IPostService> _postService;
        private VoteController _controller;
        private Mock<ICommentsService> _commentService;

        [OneTimeSetUp]
        public void Init()
        {
            _postService = MockHelper.MockPostService();
            _commentService = MockHelper.MockCommentService();
            _controller = new VoteController(_postService.Object, _commentService.Object);
        }

        [Test]
        public void PostVoteUp()
        {
            const int postId = 1;
            var result = _controller.PostVoteUp(postId);
            Assert.IsTrue(result.IsSuccess);
        }


        [Test]
        public void PostVoteDown()
        {
            const int postId = 1;
            var result = _controller.PostVoteDown(postId);
            Assert.IsTrue(result.IsSuccess);
        }


        [Test]
        public void CommentVoteUp()
        {
            const int commentId = 1;
            var result = _controller.CommentVoteUp(commentId);
            Assert.IsTrue(result.IsSuccess);
        }


        [Test]
        public void CommentVoteDown()
        {
            const int commentId = 1;
            var result = _controller.CommentVoteDown(commentId);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
