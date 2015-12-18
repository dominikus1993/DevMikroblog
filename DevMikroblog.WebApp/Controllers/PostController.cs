using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Services.Interface;

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
    }
}
