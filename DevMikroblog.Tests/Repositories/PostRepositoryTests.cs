using DevMikroblog.Domain.Repositories.Implementation;
using DevMikroblog.Tests.Helpers;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace DevMikroblog.Tests.Repositories
{
    [TestFixture]
    public class PostRepositoryTests
    {
        private PostRepository _postRepository;
        private DataGenerator _data;
        [OneTimeSetUp]
        public void Init()
        {
            var contextMock = MockHelper.Get().GetMockContext();
            _postRepository = new PostRepository(contextMock.Object);
            _data = DataGenerator.Get();
        }

        [Test]
        public void GetPostTest()
        {
            const int expextedId = 1;
            var result = _postRepository.Read(expextedId);
            Assert.IsNotNull(result);
            Assert.AreEqual(expextedId, result.Id);
        }
    }
}
