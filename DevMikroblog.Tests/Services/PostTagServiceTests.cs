using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
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

        [Test]
        public void Delete()
        {
            const int id = 1;
            const string userId = "d1u2p3a";
            var result = _service.DeletePost(id, userId);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Value);
        }

        [Test]
        public void DeleteByInvalidId()
        {
            const int id = 1;
            const string userId = "d1212121222222222222222222222222222222222222222222222222222222u2p3a";
            var result = _service.DeletePost(id, userId);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsFalse(result.Value);
        }

        [Test]
        public void GetPostByAuthorName()
        {
            const string authorName = "d1u2p3a";
            var result = _service.GetPostByAuthorName(authorName);
            Assert.That(result, Is.Not.Null);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotEmpty(result.Value);
        }

        [Test]
        public void GetPostByInvalidAuthorName()
        {
            const string authorName = "d1u2p3a111111111";
            var result = _service.GetPostByAuthorName(authorName);
            Assert.That(result, Is.Not.Null);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsEmpty(result.Value);
        }

        [Test]
        public void UpdatePost()
        {
            var post = new Post()
            {
                Id = 1,
                Title = string.Empty,
                Message = string.Empty
            };
            var result = _service.UpdatePost(post, _data.Posts.First(x => x.Id ==post.Id).AuthorId);
            Assert.IsTrue(result.Value);
        }

        [Test]
        public void UpdatePostByInvalidId()
        {
            var post = new Post()
            {
                Id = 13345,
                Title = string.Empty,
                Message = string.Empty
            };
            var result = _service.UpdatePost(post, _data.Posts.FirstOrDefault(x => x.Id == post.Id)?.AuthorId);
            Assert.IsFalse(result.Value);
        }

        [Test]
        public void UpdatePostByInvalidAuthorName()
        {
            var post = new Post()
            {
                Id = 1,
                Title = string.Empty,
                Message = string.Empty
            };
            var result = _service.UpdatePost(post, "");
            Assert.IsFalse(result.Value);
        }
    }
}
