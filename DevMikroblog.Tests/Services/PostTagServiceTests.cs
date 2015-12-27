using System.Collections.Generic;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Services.Implementation;
using DevMikroblog.Domain.Services.Interface;
using DevMikroblog.Tests.Helpers;
using DevMikroblog.WebApp.Models;
using Moq;
using NUnit.Framework;

namespace DevMikroblog.Tests.Services
{

    [TestFixture]
    public class PostTagServiceTests
    {
        private Mock<IPostService> _postRepositoryMock;
        private Mock<ITagService> _tagServiceMock;
        private PostTagService _service;
        private DataGenerator _data;

        [OneTimeSetUp]
        public void Init()
        {
            _data = DataGenerator.Get();
            _postRepositoryMock = MockHelper.MockPostService();
            _tagServiceMock = MockHelper.MockTagService();
            _service = new PostTagService(_postRepositoryMock.Object, _tagServiceMock.Object);
        }
        [Test]
        public void Posts()
        {
            var result = _service.Posts;
            Assert.That(() => result.IsSuccess, Is.True);
            Assert.IsNotEmpty(result.Value);
        }

        [Test]
        public void GetById()
        {
            const int id = 1;
            var result = _service.GetPostById(id);
            Assert.IsTrue(result.IsSuccess);
            Assert.That(() => result.Value, Is.Not.Null);
        }

        [Test]
        public void CreatePostTestWithoutTag()
        {
            var post = new Post()
            {
                Title = "ssaasas",
                Message = "asasas"
            };

            var result = _service.CreatePost(post);
            _postRepositoryMock.Verify(x => x.Create(It.IsAny<Post>()), Times.Once);
            Assert.IsTrue(result.IsSuccess);
            Assert.That(result.Value, Is.Not.Null);
        }
        [Test]
        public void CreatePostTestWithNewTag()
        {
            var post = new Post()
            {
                Title = "ssaasas",
                Message = "asasas #heheszki"
            };

            var result = _service.CreatePost(post);
            _tagServiceMock.Verify(x => x.CreateOrUpdateTags(It.IsAny<List<Tag>>()), Times.Once);
            Assert.IsTrue(result.IsSuccess);
            Assert.That(result.Value, Is.Not.Null);
        }
    }
}
