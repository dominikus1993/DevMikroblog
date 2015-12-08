namespace DevMikroblog.Domain.Model
{
    public class Vote
    {
        public long Id { get; set; }

        public UserVote UserVote { get; set; }

        public long? PostId { get; set; }

        public string UserId { get; set; }

        public long? CommentId { get; set; }

        public Comment Comment { get; set; }

        public ApplicationUser User { get; set; }

        public Post Post { get; set; }
    }
}
