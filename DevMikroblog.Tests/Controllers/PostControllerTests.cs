using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Xml;
using DevMikroblog.Tests.Helpers;
using DevMikroblog.WebApp.Controllers;
using DevMikroblog.WebApp.Models;
using NUnit.Framework;

namespace DevMikroblog.Tests.Controllers
{
    [TestFixture]
    public class PostControllerTests
    {

        private DataGenerator _data;
        private PostController _controller;

        [OneTimeSetUp]
        public void Init()
        {
            _data = DataGenerator.Get();
            var postTagService = MockHelper.MockPostTagService();
            _controller = new PostController(postTagService.Object) { Request = new HttpRequestMessage() };
            _controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            _controller.User = MockHelper.MockIdentity();
        }

        [Test]
        public void Get()
        {
            var result = _controller.Get();
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotEmpty(result.Value);
        }

        [Test]
        public void GetById()
        {
            var result = _controller.Get(1);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public void GetByInvalidId()
        {
            var result = _controller.Get(55);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsWarning);
            Assert.IsNull(result.Value);
        }

        [Test]
        public void Create()
        {
            var postViewModel = new CreatePostViewModel()
            {
                Title = "ssaasas",
                Message = "asasas"
            };

            var result = _controller.Create(postViewModel);
            Assert.IsTrue(result.IsSuccess);
            Assert.That(result.Value, Is.Not.Null);
        }

        [Test]
        public void Delete()
        {
            const int id = 1;
            var result = _controller.Delete(id);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Value);
        }

        [Test]
        public void GetPostByTagName()
        {
            const string tagName = "java";
            var result = _controller.GetPostsByTagName(tagName);
            Assert.IsTrue(result.IsSuccess);
            Assert.That(() => result.Value, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void GetPostByInvalidTagName()
        {
            const string tagName = "javaaaaaaaaaaaaaaaaaaaaaaa";
            var result = _controller.GetPostsByTagName(tagName);
            Assert.IsTrue(result.IsSuccess);
            Assert.That(() => result.Value, Is.Not.Null.And.Empty);
        }
    }
}
