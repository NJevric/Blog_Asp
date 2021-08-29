using Blog.Application.DataTransfer;
using Blog.Application.Queries;
using Blog.Application.Queries.Images;
using Blog.Application.Searches;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Queries.Images
{
    public class EfGetImagesQuery : IGetImagesQuery
    {
        private readonly BlogContext _context;

        public EfGetImagesQuery(BlogContext context)
        {
            _context = context;
        }
        public int Id => 111;

        public string Name => "Get Images Using EF";


        public PageResponse<ImageDto> Execute(ImageSearch search)
        {

            var images = _context.PostImages.AsQueryable();

            var skipCount = search.PerPage * (search.Page - 1);


            var response = new PageResponse<ImageDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = images.Count(),
                Items = images.Skip(skipCount).Take(search.PerPage).Select(x => new ImageDto
                {
                    Id = x.Id,
                    Src = x.Src,
                    Alt = x.Alt
                }).ToList()
            };

            return response;

        }
    }
}
