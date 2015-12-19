using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using static System.DateTime;

namespace DevMikroblog.Domain.Model
{
    [DataContract(IsReference = true)]
    public class Post
    {
        public Post()
        {

        }

        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public DateTime Date { get; set; } = Now;

        [DataMember]
        public long Rate { get; set; }

        [DataMember]
        public string AuthorId { get; set; }

        [DataMember]
        public string AuthorName { get; set; }

        public ApplicationUser Author { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<Vote> Votes { get; set; } = new List<Vote>();

    }
}
