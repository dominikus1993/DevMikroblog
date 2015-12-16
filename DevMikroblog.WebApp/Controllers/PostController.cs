using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DevMikroblog.Domain.Services.Interface;

namespace DevMikroblog.WebApp.Controllers
{
    public class PostController : ApiController
    {
        private IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
    }
}
