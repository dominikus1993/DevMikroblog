using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Tests.Helpers
{
    public class DataGenerator
    {
        public List<Post> Posts { get; private set; }

        public List<Tag> Tags { get; private set; }

        public List<ApplicationUser> Users { get; private set; }

        public List<Comment> Comments { get; set; }

        public List<Vote> Votes { get; set; }

        public DataGenerator()
        {
            GenerateUsers();
            GenarateTags();
            GeneratePost();
            GenerateComments();
            GenerateVotes();
            Tags.ForEach(x => x.Posts = Posts);
        }

        private void GeneratePost()
        {
            Posts = new List<Post>()
            {
                new Post(){Id = 1,Date = DateTime.Now,Title = "A",Message = "B",AuthorId = "d1u2p3a",Rate = 5,Tags = Tags,Author = Users[0]},
                new Post(){Id = 2,Date = DateTime.MaxValue,Title = "A",Message = "B",AuthorId = "d1u2p3a",Rate = 10,Tags = Tags,Author = Users[0]},
                new Post(){Id = 3,Date = DateTime.Now,Title = "A",Message = "B",AuthorId = "d1u2p3a",Rate = 44545,Tags = Tags,Author = Users[0]},
                new Post(){Id = 4,Date = DateTime.Now,Title = "A",Message = "B",AuthorId = "d1u2p3a",Rate = 44,Tags = Tags,Author = Users[0]},
                new Post(){Id = 5,Date = DateTime.Now,Title = "A",Message = "B",AuthorId = "d1u2p3a",Rate = -555,Tags = Tags,Author = Users[0]},
                new Post(){Id = 6,Date = DateTime.Now,Title = "A",Message = "B",AuthorId = "d1u2p3a4d5u6p7a",Rate = -11111,Tags = Tags,Author = Users[1]}
            };
        }

        private void GenerateComments()
        {
            Comments = new List<Comment>()
            {
                new Comment(){AuthorId = "d1u2p3a",Author = Users[0],Id = 1,Message = "No siema",PostId = 1},
                new Comment(){AuthorId = "d1u2p3a",Author = Users[0],Id = 2,Message = "No siema",PostId = 2},
                new Comment(){AuthorId = "d1u2p3a",Author = Users[0],Id = 3,Message = "No siema",PostId = 3},
                new Comment(){AuthorId = "d1u2p3a",Author = Users[0],Id = 4,Message = "No siema",PostId = 4},
                new Comment(){AuthorId = "d1u2p3a",Author = Users[0],Id = 5,Message = "No siema",PostId = 5}
            };
        }

        private void GenarateTags()
        {
            Tags = new List<Tag>()
             {
                 new Tag()
                 {
                     Id = 1,
                     Name = "java",
                 },
                 new Tag()
                 {
                     Id = 2,
                     Name = "csharp",
                 },
                 new Tag()
                 {
                     Id = 3,
                     Name = "programowanie",
                 },
                 new Tag()
                 {
                     Id = 4,
                     Name = "heheszki",
                 }
             };
        }

        private void GenerateUsers()
        {
            Users = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "d1u2p3a",
                    UserName = "dominikus1993"
                },
                new ApplicationUser()
                {
                    Id = "d1u2p3a4d5u6p7a",
                    UserName = "dominikus1910"
                },
            };
        }

        private void GenerateVotes()
        {
            Votes = new List<Vote>()
            {
                new Vote(){UserVote = UserVote.VoteUp,PostId = 4},
                new Vote(){UserVote = UserVote.VoteUp,CommentId = 4}
            };
        }

        public static DataGenerator Get() => new DataGenerator();
    }
}
