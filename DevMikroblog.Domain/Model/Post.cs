using System;
using System.Collections.Generic;
using static System.DateTime;

namespace DevMikroblog.Domain.Model
{
    public class Post
    {
        public Post()
        {

        }

        public long Id { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; } = Now;

        public long Rate { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public ApplicationUser Author { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<Vote> Votes { get; set; } = new List<Vote>();

    }
}
