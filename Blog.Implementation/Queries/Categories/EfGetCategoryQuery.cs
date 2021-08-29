using Blog.Application.DataTransfer;
using Blog.Application.Exceptions;
using Blog.Application.Queries;
using Blog.Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Blog.Application.Queries.Categories;

namespace Blog.Implementation.Queries.Categories
{
    public class EfGetCategoryQuery : IGetCategoryQuery
    {
        public readonly BlogContext _context;

        public EfGetCategoryQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 4;

        public string Name => "Get Category Using EF";

        public CategoryDto Execute(int search)
        {
            var category = _context.Categories.Find(search);

            if(category == null)
            {
                throw new EntityNotFoundException(search, typeof(Category));
            }

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
