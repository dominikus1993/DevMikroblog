﻿using System.Collections.Generic;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.Services.Interface
{
    public interface IPostTagService
    {
        Result<List<Post>> Posts { get; }
        Result<Post> GetPostById(long id);
        Result<Post> CreatePost(Post post);
    }
}