using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DevMikroblog.Domain.DatabaseContext.Interface;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Repositories.Interface;
using DevMikroblog.Domain.Services.Interface;
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
            result.Setup(x => x.Votes).Returns(GetMockDbSet(Generator.Votes).Object);
            result.Setup(x => x.Comments).Returns(GetMockDbSet(Generator.Comments).Object);
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
            result.Setup(x => x.Update(It.IsAny<Post>())).Returns(true);
            result.Setup(x => x.Create(It.IsAny<Post>())).Returns(Generator.Posts.First());
            result.Setup(x => x.Vote(It.IsAny<long>(), It.IsAny<Vote>(), It.IsAny<Func<long, long>>()))
                .Returns<long, Vote, Func<long, long>>((id, vote, func) => Generator.Posts.SingleOrDefault(post => post.Id == id));
            return result;
        }

        public static Mock<ICommentsRepository> MockCommentsRepository()
        {
            var result = new Mock<ICommentsRepository>();
            result.Setup(x => x.Comments).Returns(Generator.Comments.AsQueryable());
            result.Setup(x => x.Read(1)).Returns(Generator.Comments.First());// valid id
            result.Setup(x => x.Read(2)).Returns((Comment)null);// invalid id
            result.Setup(x => x.Update(It.IsAny<Comment>())).Returns(true);
            result.Setup(x => x.Create(It.IsAny<Comment>())).Returns(Generator.Comments.First());
            result.Setup(x => x.Vote(It.IsAny<long>(), It.IsAny<Vote>(), It.IsAny<Func<long, long>>()))
                .Returns<long, Vote, Func<long, long>>((id, vote, func) => Generator.Comments.SingleOrDefault(c => c.Id == id));
            return result;
        }

        public static Mock<ITagRepository> MockTagRepository()
        {
            var result = new Mock<ITagRepository>();
            result.Setup(x => x.Create(It.IsAny<Tag>())).Returns(Generator.Tags.First());
            result.Setup(x => x.Update(It.IsAny<Tag>())).Returns(true);
            result.Setup(x => x.Exist(It.IsAny<string>()))
                .Returns<string>(x => Generator.Tags.SingleOrDefault(tag => tag.Name == x) != null);
            result.Setup(x => x.Find(It.IsAny<string>()))
                .Returns<string>(x => Generator.Tags.SingleOrDefault(tag => tag.Name == x));
            result.Setup(x => x.GetPostsByTagName(It.IsAny<string>()))
                .Returns<string>(x => Generator.Tags.SingleOrDefault(tag => tag.Name == x)?.Posts.ToList());
            return result;
        }

        public static Mock<IPostService> MockPostService()
        {
            var result = new Mock<IPostService>();
            result.Setup(x => x.Posts).Returns(Result<List<Post>>.WarningWhenNoData(Generator.Posts));
            result.Setup(x => x.Read(It.IsAny<long>()))
                .Returns<long>(id => Result<Post>.WarningWhenNoData(Generator.Posts.SingleOrDefault(post => post.Id == id)));
            result.Setup(x => x.Create(It.IsAny<Post>())).Returns<Post>(x => Result<Post>.WarningWhenNoData(x));
            result.Setup(x => x.Delete(It.IsAny<long>())).Returns<long>(id => Result<bool>.WarningWhenNoData(Generator.Posts.Any(post => post.Id == id)));
            result.Setup(x => x.VoteUp(It.IsAny<long>(), It.IsAny<string>()))
                .Returns<long, string>(
                    (id, userId) =>
                        Result<Post>.WarningWhenNoData(
                            Generator.Posts.SingleOrDefault(x => x.Id == id)));
            result.Setup(x => x.VoteDown(It.IsAny<long>(), It.IsAny<string>()))
                .Returns<long, string>(
                    (id, userId) =>
                        Result<Post>.WarningWhenNoData(
                            Generator.Posts.SingleOrDefault(x => x.Id == id)));
            return result;
        }

        public static Mock<ITagService> MockTagService()
        {
            var result = new Mock<ITagService>();
            result.Setup(x => x.ParseTags(It.IsAny<string>())).Returns<string>(x => Result<List<Tag>>.WarningWhenNoData(Tools.TagUtils.Parsers.TagParser(x, new Regex(@"(?<=#)\w+")).Select(tag => new Tag() { Name = tag.ToLower() }).ToList()));
            result.Setup(x => x.CreateOrUpdateTags(It.IsAny<List<Tag>>())).Returns<List<Tag>>(x => Result<List<Tag>>.WarningWhenNoData(x));
            return result;
        }


        public static Mock<IPostTagService> MockPostTagService()
        {
            var result = new Mock<IPostTagService>();
            result.Setup(x => x.CreatePost(It.IsAny<Post>())).Returns<Post>(x => Result<Post>.WarningWhenNoData(x));
            result.Setup(x => x.Posts).Returns(Result<List<Post>>.WarningWhenNoData(Generator.Posts));
            result.Setup(x => x.GetPostById(It.IsAny<long>())).Returns<long>(id => Result<Post>.WarningWhenNoData(Generator.Posts.SingleOrDefault(post => post.Id == id)));
            result.Setup(x => x.DeletePost(It.IsAny<long>(), It.IsAny<string>())).Returns<long,string>((id, userId) => Result<bool>.WarningWhenNoData(Generator.Posts.SingleOrDefault(x => x.Id == id && x.AuthorId == userId) != null));
            result.Setup(x => x.GetPostByTagName(It.IsAny<string>())).Returns<string>(tagName => Result<List<Post>>.WarningWhenNoData(Generator.Posts.Where(post => post.Tags.Any(tag => tag.Name == tagName)).ToList()));
            return result;
        }

        public static Mock<ICommentsService> MockCommentService()
        {
            var result = new Mock<ICommentsService>();
            result.Setup(x => x.Create(It.IsAny<Comment>())).Returns<Comment>(comment => Result<Comment>.WarningWhenNoData(comment));
            return result;
        } 

        public static IPrincipal MockIdentity()
        {
            var claim = new Claim("dominikus1993", "d1u2p3a");
            var mockIdentity = Mock.Of<ClaimsIdentity>(identity => identity.FindFirst(It.IsAny<string>()) == claim);
            return Mock.Of<IPrincipal>(x => x.Identity == mockIdentity);
        }
    }

}
