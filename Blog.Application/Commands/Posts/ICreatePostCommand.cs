using Blog.Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Commands.Posts
{
    public interface ICreatePostCommand : ICommand<PostDto>
    {

    }
}
