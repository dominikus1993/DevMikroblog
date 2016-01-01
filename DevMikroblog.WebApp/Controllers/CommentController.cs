using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    }
}
