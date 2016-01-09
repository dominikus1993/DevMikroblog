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
            return _postService.VoteUp(postId, User.Identity.GetUserId());
        }


        [HttpGet]
        [Authorize]
        [Route("post/{postId}/votedown")]
        public Result<Post> PostVoteDown(long postId)
        {
            return _postService.VoteUp(postId, User.Identity.GetUserId());
        }

        [HttpGet]
        [Authorize]
        [Route("comment/{commentId}/voteup")]
        public Result<Comment> CommentVoteUp(long commentId)
        {
            return _commentsService.VoteUp(commentId, User.Identity.GetUserId());
        }

        [HttpGet]
        [Authorize]
        [Route("comment/{commentId}/votedown")]
        public Result<Comment> CommentVoteDown(long commentId)
        {
            return _commentsService.VoteUp(commentId, User.Identity.GetUserId());
        }
    }
}
