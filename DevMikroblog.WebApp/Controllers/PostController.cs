﻿using System.Collections.Generic;
using System.Web.Http;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Services.Interface;
using DevMikroblog.WebApp.Models;
using Microsoft.AspNet.Identity;

namespace DevMikroblog.WebApp.Controllers
{
    public class PostController : ApiController
    {
        private readonly IPostTagService _postTagService;
        public PostController(IPostTagService postTagService)
        {
            _postTagService = postTagService;
        }

        public Result<List<Post>> Get()
        {
            return _postTagService.Posts;
        }

        public Result<Post> Get(int id)
        {
            return _postTagService.GetPostById(id);
        }

        [HttpPost]
        [Authorize]
        public Result<Post> Create([FromBody] CreatePostViewModel post)
        {
            var resultPost = new Post()
            {
                AuthorId = User.Identity.GetUserId(),
                AuthorName = User.Identity.GetUserName(),
                Message = post.Message,
                Title = post.Title,
            };
            return _postTagService.CreatePost(resultPost);
        }

        [HttpPut]
        [Authorize]
        public Result<Post> Update(UpdatePostViewModel post)
        {
            var resultPost = new Post()
            {
                Id = post.Id,
                Message = post.Message,
                Title = post.Title,
            };

            var result = _postTagService.UpdatePost(resultPost, User.Identity.GetUserId());

            if (result.Value)
            {
                return _postTagService.GetPostById(post.Id);
            }
            return new Result<Post>();
        }

        [HttpDelete]
        [Authorize]
        public Result<bool> Delete(long id)
        {
            return _postTagService.DeletePost(id, User.Identity.GetUserId());
        }

        [HttpGet]
        public Result<List<Post>> GetPostsByTagName(string tagName)
        {
            return _postTagService.GetPostByTagName(tagName);
        }

        [HttpGet]
        public Result<List<Post>> GetPostsByAuthorName(string authorName)
        {
            return _postTagService.GetPostByAuthorName(authorName);
        }
    }
}

