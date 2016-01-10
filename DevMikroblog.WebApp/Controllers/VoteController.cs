using System.Web.Http;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Services.Interface;
using Microsoft.AspNet.Identity;

namespace DevMikroblog.WebApp.Controllers
{
    [RoutePrefix("api/vote")]
    public class VoteController : ApiController
    {
        private readonly IPostService _postService;
        private readonly ICommentsService _commentsService;

        public VoteController(IPostService postService, ICommentsService commentsService)
        {
            _postService = postService;
            _commentsService = commentsService;
        }

        [HttpGet]
        [Authorize]
        [Route("post/{postId}/voteup")]
        public Result<Post> PostVoteUp(long postId)
        {
            var vote = new Vote()
            {
                PostId = postId,
                UserId = User.Identity.GetUserId(),
                UserName = User.Identity.GetUserName(),
                UserVote = UserVote.VoteUp
            };
            return _postService.VoteUp(vote);
        }


        [HttpGet]
        [Authorize]
        [Route("post/{postId}/votedown")]
        public Result<Post> PostVoteDown(long postId)
        {
            var vote = new Vote()
            {
                PostId = postId,
                UserId = User.Identity.GetUserId(),
                UserName = User.Identity.GetUserName(),
                UserVote = UserVote.VoteDown
            };
            return _postService.VoteUp(vote);
        }

        [HttpGet]
        [Authorize]
        [Route("comment/{commentId}/voteup")]
        public Result<Comment> CommentVoteUp(long commentId)
        {
            var vote = new Vote()
            {
                CommentId = commentId,
                UserId = User.Identity.GetUserId(),
                UserName = User.Identity.GetUserName(),
                UserVote = UserVote.VoteUp
            };
            return _commentsService.VoteUp(vote);
        }

        [HttpGet]
        [Authorize]
        [Route("comment/{commentId}/votedown")]
        public Result<Comment> CommentVoteDown(long commentId)
        {
            var vote = new Vote()
            {
                CommentId = commentId,
                UserId = User.Identity.GetUserId(),
                UserName = User.Identity.GetUserName(),
                UserVote = UserVote.VoteDown
            };
            return _commentsService.VoteUp(vote);
        }
    }
}
