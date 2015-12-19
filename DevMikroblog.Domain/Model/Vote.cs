﻿using System.Runtime.Serialization;

namespace DevMikroblog.Domain.Model
{
    [DataContract(IsReference = true)]
    public class Vote
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public UserVote UserVote { get; set; }
        [DataMember]
        public long? PostId { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public long? CommentId { get; set; }

        public Comment Comment { get; set; }

        public ApplicationUser User { get; set; }

        public Post Post { get; set; }
    }
}
