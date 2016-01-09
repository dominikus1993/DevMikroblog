using System.Collections.Generic;
using System.Web.Http;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Services.Interface;
using DevMikroblog.WebApp.Models;
using Microsoft.AspNet.Identity;

namespace DevMikroblog.WebApp.Controllers
{
    public class CommentController : ApiController
    {
        private readonly ICommentsService _commentsService;

        public CommentController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpGet]
        public Result<List<Comment>> GetByPost(long postId)
        {
            return _commentsService.GetByPost(postId);
        }

        [Authorize]
        [HttpPost]
        public Result<Comment> Create([FromBody]AddCommentViewModel comment)
        {
            var commentToAdd = new Comment()
            {
                AuthorId = User.Identity.GetUserId(),
                AuthorName = User.Identity.GetUserName(),
                Message = comment.Message,
                PostId = comment.PostId,
            };
            return _commentsService.Create(commentToAdd);
        }

        [Authorize]
        [HttpDelete]
        public Result<bool> Delete(long id)
        {
            return IsOwnerOrAdministrator(id) ? _commentsService.Delete(id) : new Result<bool>();
        }

        public bool IsOwnerOrAdministrator(long id)
        {
            var comment = _commentsService.Read(id);
            if (comment.IsSuccess)
            {
                return comment.Value.AuthorId == User.Identity.GetUserId() || User.IsInRole(UserRole.Administrator.ToString());
            }
            return false;
        }

    }
}
