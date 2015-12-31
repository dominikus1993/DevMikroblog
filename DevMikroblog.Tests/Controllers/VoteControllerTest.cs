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

        [OneTimeSetUp]
        public void Init()
        {
            _postService = MockHelper.MockPostService();
            _controller = new VoteController(_postService.Object);
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
    }
}
