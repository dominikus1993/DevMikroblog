using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevMikroblog.Domain.DatabaseContext.Interface;
using Moq;

namespace DevMikroblog.Tests.Helpers
{
    public class MockHelper
    {

        public static MockHelper Get() => new MockHelper();

        private readonly DataGenerator _generator;

        public MockHelper()
        {
            _generator = DataGenerator.Get();
        }

        public Mock<IDbContext> GetMockContext()
        {
            var result = new Mock<IDbContext>();
            result.Setup(x => x.Posts).Returns(GetMockDbSet(_generator.Posts.AsQueryable()).Object);
            return result;
        }

        private Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> entityCollection) where T : class
        {
            var result = new Mock<DbSet<T>>();
            result.As<IQueryable<T>>().Setup(x => x.Provider).Returns(entityCollection.Provider);
            result.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entityCollection.Expression);
            result.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entityCollection.ElementType);
            result.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entityCollection.GetEnumerator());
            return result;
        }
    }

}
