using Blog.Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Queries
{
    public interface IGetUserQuery : IQuery<int, UserDto>
    {
    }
}
