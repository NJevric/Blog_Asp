using Blog.Application.Commands.Images;
using Blog.Application.Exceptions;
using Blog.Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Implementation.Commands.Images
{
    public class EfDeleteImageCommand : IDeleteImageCommand
    {
        private readonly BlogContext _context;

        public EfDeleteImageCommand(BlogContext context)
        {
            _context = context;
        }

        public int Id => 110;

        public string Name => "Delete Image Using EF";

        public void Execute(int request)
        {
            var image = _context.PostImages.Find(request);
            
            if(image == null)
            {
                throw new EntityNotFoundException(request, typeof(PostImage));
            }

            image.IsDeleted = true;
            image.IsActive = false;
            image.DeletedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
