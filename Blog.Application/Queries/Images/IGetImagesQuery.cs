using Blog.Application.DataTransfer;
using Blog.Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Queries.Images
{
    public interface IGetImagesQuery : IQuery<ImageSearch, PageResponse<ImageDto>>
    {
    }
}
