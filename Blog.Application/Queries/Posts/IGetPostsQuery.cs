using Blog.Application.DataTransfer;
using Blog.Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Queries.Posts
{
    public interface IGetPostsQuery : IQuery<PostSearch, PageResponse<ReadPostDto>>
    {
    }
}
