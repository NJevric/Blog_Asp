using Blog.Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Queries.Comments
{
    public interface IGetCommentQuery : IQuery<int,CommentDto>
    {

    }
}
