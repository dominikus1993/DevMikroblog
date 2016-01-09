using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using static System.DateTime;

namespace DevMikroblog.Domain.Model
{

    [DataContract]
    public class Comment
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public DateTime Date { get; set; } = Now.ToUniversalTime();

        [DataMember]
        public long Rate { get; set; }

        [DataMember]
        public string AuthorName { get; set; }

        [DataMember]
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public long? PostId { get; set; }

        public Post Post { get; set; }

        [DataMember]
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
