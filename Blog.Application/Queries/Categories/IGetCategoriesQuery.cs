﻿using Blog.Application.DataTransfer;
using Blog.Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Queries.Categories
{
    public interface IGetCategoriesQuery : IQuery<CategorySearch, PageResponse<CategoryDto>>
    {
    }
}
