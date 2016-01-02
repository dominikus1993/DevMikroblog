using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using DevMikroblog.Domain.Services.Interface;
using DevMikroblog.Tests.Helpers;
using DevMikroblog.WebApp.Controllers;
using DevMikroblog.WebApp.Models;
using Moq;
using NUnit.Framework;

namespace DevMikroblog.Tests.Controllers
{
    [TestFixture]
    public class CommentControllerTest
    {
        private CommentController _controller;
        private Mock<ICommentsService> _commentServiceMock;

        [OneTimeSetUp]
        public void Init()
        {
            _commentServiceMock = MockHelper.MockCommentService();
            _controller = new CommentController(_commentServiceMock.Object) { Request = new HttpRequestMessage() };
            _controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            _controller.User = MockHelper.MockIdentity();
        }

        [Test]
        public void Create()
        {
            var comment = new AddCommentViewModel()
            {
                Message = "saddaads",
                PostId = 1
            };
            var result = _controller.Create(comment);
            Assert.IsTrue(result.IsSuccess);
            Assert.That(result.Value, Is.Not.Null);
        }

        [Test]
        public void Delete()
        {
            const int id = 1;
            var result = _controller.Delete(id);
            Assert.IsTrue(result.IsSuccess);
            Assert.That(result.Value, Is.True);
        }
    }
}
