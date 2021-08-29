using Blog.Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Queries.Categories
{
    public interface IGetCategoryQuery : IQuery<int, CategoryDto>
    {
    }
}
