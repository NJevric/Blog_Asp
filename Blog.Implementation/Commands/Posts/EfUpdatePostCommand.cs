using Blog.Application.Commands.Posts;
using Blog.Application.DataTransfer;
using Blog.Application.Exceptions;
using Blog.Domain;
using Blog.Implementation.Validators;
using EfDataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Commands.Posts
{
    public class EfUpdatePostCommand : IUpdatePostCommand
    {
        public readonly BlogContext _context;
        public readonly UpdatePostValidator _validator;
        public EfUpdatePostCommand(BlogContext context, UpdatePostValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 12;

        public string Name => "Update Post Using EF";

        public void Execute(PostDto request)
        {
            var post = _context.Posts.Include(p => p.PostCategories).Include(p => p.PostImages).FirstOrDefault(o => o.Id == request.Id);

            if(post == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Post));
            }

            _validator.ValidateAndThrow(request);

            post.Title = request.Title;
            post.Content = request.Content;
            post.UserId = request.UserId;

            post.PostCategories
                .Where(pc => !request.CategoryIds.Contains(pc.CategoryId))
                .ToList()
                .ForEach(pc => post.PostCategories.Remove(pc));


            var existingCategoryIds = post.PostCategories.Select(pc => pc.CategoryId);

            _context.Categories
                .Where(c => request.CategoryIds.Except(existingCategoryIds)
                .Contains(c.Id))
                .ToList()
                .ForEach(c => _context.PostCategories.Add(new PostCategory
                {
                    Post = post,
                    Category = c 
                }));

            foreach (var iid in request.Images)
            {

                if (iid != null)
                {
                    var guid = Guid.NewGuid();

                    var extension = Path.GetExtension(iid.FileName);

                    var newFileName = guid + "_" + iid.FileName;

                    var path = Path.Combine("wwwroot", "Images", newFileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        iid.CopyTo(fileStream);
                    }


                    _context.PostImages.Add(new PostImage
                    {
                        Post = post,
                        Src = newFileName,
                        Alt = post.Title + " " + newFileName,

                    });

                }
            }
                

            _context.SaveChanges();

        }
    }
}
