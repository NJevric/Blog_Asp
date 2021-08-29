using Blog.Application.Commands.Posts;
using Blog.Application.DataTransfer;
using Blog.Domain;
using Blog.Implementation.Validators;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blog.Implementation.Commands.Posts
{
    public class EfCreatePostCommand : ICreatePostCommand
    {
        private readonly BlogContext _context;
        private readonly CreatePostValidator _validator;

        public EfCreatePostCommand(BlogContext context, CreatePostValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public int Id => 11;

        public string Name => "Create Post Using EF";

        public void Execute(PostDto request)
        {
            _validator.ValidateAndThrow(request);

            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                Like = request.Like,
                UserId = request.UserId
            };

            foreach (var cid in request.CategoryIds)
            {
                _context.PostCategories.Add(new PostCategory
                {
                    Post = post,
                    CategoryId = cid
                });

            }

            foreach (var iid in request.Images)
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


            _context.Posts.Add(post);
            _context.SaveChanges();
        }
    }
}
