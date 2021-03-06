﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevMikroblog.Domain.Model;
using DevMikroblog.Domain.Services.Interface;

namespace DevMikroblog.Domain.Services.Implementation
{
    public class PostTagService : IPostTagService
    {

        private readonly IPostService _postService;
        private readonly ITagService _tagService;

        public PostTagService(IPostService postService, ITagService tagService)
        {
            _postService = postService;
            _tagService = tagService;
        }

        public Result<List<Post>> Posts => _postService.Posts;


        public Result<Post> GetPostById(long id)
        {
            return _postService.Read(id);
        }

        public Result<Post> CreatePost(Post post)
        {
            var tags = _tagService.ParseTags(post.Message);
            Result<Post> result;
            if (tags.IsSuccess && tags.Value.Any())
            {
                var tagsToCreateOrUpdate = tags.Value.Select(x =>
                {
                    x.Posts.Add(post);
                    return x;
                }).ToList();
                var resultTags = _tagService.CreateOrUpdateTags(tagsToCreateOrUpdate);
                result = Result<Post>.WarningWhenNoData(post);
                _tagService.SaveChanges();
            }
            else
            {
                result = _postService.Create(post);
                _postService.SaveChanges();
            }

            return result;
        }

        public Result<bool> UpdatePost(Post postToUpdate, string userId)
        {
            var post = _postService.Read(postToUpdate.Id);
            var tags = _tagService.ParseTags(postToUpdate.Message);
            Result<bool> result;
            if (post.IsSuccess && post.Value.AuthorId == userId )
            {
                postToUpdate = new Post()
                {
                    Id = postToUpdate.Id,
                    Title = postToUpdate.Title,
                    Message = postToUpdate.Message,
                    Tags = tags.Value
                };
                result = _postService.UpdateWithTags(postToUpdate);
            }
            else
            {
                result = new Result<bool>();
            }
            return result;

        }

        public Result<bool> DeletePost(long id, string userId)
        {
            var postResult = _postService.Read(id);
            if (postResult.IsSuccess && postResult.Value.AuthorId == userId)
            {
                return _postService.Delete(id);
            }
            return new Result<bool>();
        }

        public Result<List<Post>> GetPostByTagName(string tagName)
        {
            return _tagService.GetPostByTagName(tagName);
        }

        public Result<List<Post>> GetPostByAuthorName(string authorName)
        {
            return _postService.Read(authorName);
        }
    }
}
