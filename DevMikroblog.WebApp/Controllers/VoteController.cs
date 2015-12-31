﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Services.Interface;
using Microsoft.AspNet.Identity;

namespace DevMikroblog.WebApp.Controllers
{
    public class VoteController : ApiController
    {
        private readonly IPostService _postService;

        public VoteController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        [Route("post/{postId}/voteup")]
        public Result<Post> PostVoteUp(long postId)
        {
            return _postService.VoteUp(postId, User.Identity.GetUserId());
        }


        [HttpGet]
        [Route("post/{postId}/votedown")]
        public Result<Post> PostVoteDown(long postId)
        {
            return _postService.VoteUp(postId, User.Identity.GetUserId());
        }
    }
}