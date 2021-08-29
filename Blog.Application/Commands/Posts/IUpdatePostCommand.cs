using Blog.Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Commands.Posts
{
    public interface IUpdatePostCommand : ICommand<PostDto>
    {

    }
}
