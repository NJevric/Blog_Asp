using Blog.Application.DataTransfer;
using Blog.Application.Queries;
using Blog.Application.Queries.Categories;
using Blog.Application.Searches;
using Blog.Implementation.Validators;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Queries.Categories
{
    public class EfGetCategoriesQuery : IGetCategoriesQuery
    {
        public readonly BlogContext _context;
       
        public EfGetCategoriesQuery(BlogContext context)
        {
            _context = context;
         
        }

        public int Id => 3;

        public string Name => "Get Categories Using EF";

        public PageResponse<CategoryDto> Execute(CategorySearch search)
        {
            var query = _context.Categories.AsQueryable();

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PageResponse<CategoryDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };

            return response;
            
        }

     
    }
}
