using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Interface;
using DevMikroblog.Domain.Services.Implementation;
using DevMikroblog.Tests.Helpers;
using Moq;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace DevMikroblog.Tests.Services
{
    [TestFixture]
    class TagServiceTests
    {

        private TagService _tagService;
        private Mock<ITagRepository> _tagRepository;
        private DataGenerator _generator;
        [OneTimeSetUp]
        public void Init()
        {
            _generator = DataGenerator.Get();
            _tagRepository = MockHelper.MockTagRepository();
            _tagService = new TagService(_tagRepository.Object);
        }

        [Test]
        public void ParseTag()
        {
            const string text = "Który z tych języków #programowanie jest najłatwiejszy: c/c++/pascal/java?";
            const string exceptTag = "programowanie";
            var result = _tagService.ParseTags(text);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.IsNotEmpty(result.Value);
            Assert.AreEqual(result.Value.First().Name, exceptTag);
        }

        [Test]
        public void ParseTagbyTextWithoutTags()
        {
            const string text = "Który z tych języków jest najłatwiejszy: c/c++/pascal/java?";
            var result = _tagService.ParseTags(text);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.IsEmpty(result.Value);
        }

        [Test]
        public void CreateOrUpdateTag()
        {
            var tags = new List<Tag>()
            {
                 new Tag()
                 {
                     Id = 3,
                     Name = "programowanie",
                 },
                 new Tag()
                 {
                     Id = 4,
                     Name = "heheszki",
                 },
                 new Tag()
                 {
                     Name = "test"
                 }
            };

            var result = _tagService.CreateOrUpdateTags(tags);
            _tagRepository.Verify(x => x.Create(It.IsAny<Tag>()),Times.Once);
            _tagRepository.Verify(x => x.Update(It.IsAny<Tag>()),Times.Exactly(2));
        }


        [Test]
        public void CreateOrUpdateTagWithEmptyTagCollection()
        {
            var tags = new List<Tag>();

            var result = _tagService.CreateOrUpdateTags(tags);
            Assert.IsEmpty(result.Value);

        }

        [Test]
        public void GetPostsByTagName()
        {
            const string tagName = "programowanie";
            var result = _tagService.GetPostByTagName(tagName);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public void GetPostsByInvalidTagName()
        {
            const string tagName = "testy";
            var result = _tagService.GetPostByTagName(tagName);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsWarning);
            Assert.IsNull(result.Value);
        }
    }
}
