using Blog.Application.Commands;
using Blog.Application.Commands.Categories;
using Blog.Application.DataTransfer;
using Blog.Application.Exceptions;
using Blog.Domain;
using Blog.Implementation.Validators;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Implementation.Commands.Categories
{
    public class EfDeleteCategoryCommand : IDeleteCategoryCommand
    {
        private readonly BlogContext _context;

        public EfDeleteCategoryCommand(BlogContext context)
        {
            _context = context;
        }

        public int Id => 2;

        public string Name => "Delete Group Using EF";

        public void Execute(int request)
        {
            var category = _context.Categories.Find(request);

            if(category == null)
            {
                throw new EntityNotFoundException(request, typeof(Category));
            }

            category.IsDeleted = true;
            category.IsActive = false;
            category.DeletedAt = DateTime.Now;

            _context.SaveChanges();
           
        }
    }
}
