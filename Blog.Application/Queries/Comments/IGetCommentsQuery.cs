using Blog.Application.DataTransfer;
using Blog.Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Queries.Comments
{
    public interface IGetCommentsQuery : IQuery<CommentSearch, PageResponse<CommentDto>>
    {
    }
}
