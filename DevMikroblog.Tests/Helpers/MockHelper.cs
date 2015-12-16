using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevMikroblog.Domain.DatabaseContext.Interface;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Interface;
using Moq;

namespace DevMikroblog.Tests.Helpers
{
    public class MockHelper
    {

        public static MockHelper Get() => new MockHelper();

        private static readonly DataGenerator Generator;

        static MockHelper()
        {
            Generator = DataGenerator.Get();
        }

        public MockHelper()
        {
        }

        public Mock<IDbContext> GetMockContext()
        {
            var result = new Mock<IDbContext>();
            result.Setup(x => x.Posts).Returns(GetMockDbSet(Generator.Posts).Object);
            result.Setup(x => x.Tags).Returns(GetMockDbSet(Generator.Tags).Object);
            result.Setup(x => x.SaveChanges()).Verifiable();
            return result;
        }

        private Mock<DbSet<T>> GetMockDbSet<T>(List<T> entityCollection) where T : class
        {
            var result = new Mock<DbSet<T>>();
            result.As<IQueryable<T>>().Setup(x => x.Provider).Returns(entityCollection.AsQueryable().Provider);
            result.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entityCollection.AsQueryable().Expression);
            result.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entityCollection.AsQueryable().ElementType);
            result.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entityCollection.AsQueryable().GetEnumerator());
            result.Setup(m => m.Add(It.IsAny<T>())).Verifiable();
            result.Setup(x => x.Include(It.IsAny<string>())).Returns(result.Object);
            return result;
        }

        public static Mock<IPostRepository> MockPostRepository()
        {
            var result = new Mock<IPostRepository>();
            result.Setup(x => x.Posts).Returns(Generator.Posts.AsQueryable());
            result.Setup(x => x.Read(1)).Returns(Generator.Posts.First());// valid id
            result.Setup(x => x.Read(2)).Returns((Post)null);// invalid id
            return result;
        }
    }

}
