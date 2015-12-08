using System;
using System.Collections.Generic;

namespace DevMikroblog.Domain.Model
{
    public class Comment
    {
        public long Id { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }

        public long Rate { get; set; }

        public string AuthorName { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public long? PostId { get; set; }

        public Post Post { get; set; }

        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
