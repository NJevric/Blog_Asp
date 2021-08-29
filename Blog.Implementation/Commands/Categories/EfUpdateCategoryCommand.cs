using Blog.Application.Commands;
using Blog.Application.Commands.Categories;
using Blog.Application.DataTransfer;
using Blog.Application.Exceptions;
using Blog.Domain;
using Blog.Implementation.Validators;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Implementation.Commands.Categories
{
    public class EfUpdateCategoryCommand : IUpdateCategoryCommand
    {
        private readonly BlogContext _context;
        private readonly UpdateCategoryValidator _validator;

        public EfUpdateCategoryCommand(BlogContext context, UpdateCategoryValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 5;

        public string Name => "Update Category Using EF";

        

        public void Execute(CategoryDto request)
        {
           

            var category = _context.Categories.Find(request.Id);

            if (category == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Category));
            }

            _validator.ValidateAndThrow(request);

            category.Name = request.Name;

            _context.SaveChanges();
        }
    }
}
