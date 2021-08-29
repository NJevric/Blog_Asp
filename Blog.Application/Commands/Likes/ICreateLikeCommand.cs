using Blog.Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Commands.Likes
{
    public interface ICreateLikeCommand : ICommand<LikeDto>
    {
    }
}
