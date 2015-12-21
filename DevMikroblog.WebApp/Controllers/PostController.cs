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
    public class PostController : ApiController
    {
        private readonly IPostService _postService;
        private readonly ITagService _tagService;

        public PostController(IPostService postService, ITagService tagService)
        {
            _postService = postService;
            _tagService = tagService;
        }

        public Result<List<Post>> Get()
        {
            return _postService.Posts;
        }

        public Result<Post> Get(int id)
        {
            return _postService.Read(id);
        }

        [HttpPost]
        [Authorize]
        public Result<Post> Create([FromBody] CreatePostViewModel post)
        {
            var tags = _tagService.CreateOrUpdateTags(_tagService.ParseTags(post.Message).Value);
            var postToCreate = new Post()
            {
                AuthorId = User.Identity.GetUserId(),
                AuthorName = User.Identity.GetUserName(),
                Title  = post.Title,
                Message = post.Message,
                Tags = tags.Value
                
            };

            return _postService.Create(postToCreate);
        }

        [HttpPost]
        [Authorize]
        public Result<Post> Update(long postId)
        {
            return null;
        }

    }
}
