using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;
using DevMikroblog.Domain.Model;
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
    }
}
