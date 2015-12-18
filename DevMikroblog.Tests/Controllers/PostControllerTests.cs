using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevMikroblog.Tests.Helpers;
using DevMikroblog.WebApp.Controllers;
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
            var postServiceMock = MockHelper.MockPostService();
            var tagServiceMock = MockHelper.MockTagService();
            _controller = new PostController(postServiceMock.Object, tagServiceMock.Object);
        }

        [Test]
        public void Get()
        {
            var result = _controller.Get();
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotEmpty(result.Value);
        }
    }
}
