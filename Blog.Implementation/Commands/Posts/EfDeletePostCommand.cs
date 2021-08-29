using Blog.Application.Commands.Posts;
using Blog.Application.Exceptions;
using Blog.Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Implementation.Commands.Posts
{
    public class EfDeletePostCommand : IDeletePostCommand
    {
        public readonly BlogContext _context;

        public EfDeletePostCommand(BlogContext context)
        {
            _context = context;
        }

        public int Id => 13;

        public string Name => "Delete Post Using EF";

        public void Execute(int request)
        {
            var post = _context.Posts.Find(request);

            if(post == null)
            {
                throw new EntityNotFoundException(request, typeof(Post));
            }

            post.IsActive = false;
            post.IsDeleted = true;
            post.DeletedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
