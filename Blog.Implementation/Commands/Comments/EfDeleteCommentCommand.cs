using Blog.Application.Commands.Comments;
using Blog.Application.Exceptions;
using Blog.Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Implementation.Commands.Comments
{
    public class EfDeleteCommentCommand : IDeleteCommentCommand
    {
        private readonly BlogContext _context;

        public EfDeleteCommentCommand(BlogContext context)
        {
            _context = context;

        }
        public int Id => 19;

        public string Name => "Delete Comment Using EF";

        public void Execute(int request)
        {
            var comment = _context.Comments.Find(request);

            if(comment == null)
            {
                throw new EntityNotFoundException(request, typeof(Post));
            }

            comment.IsActive = false;
            comment.IsDeleted = true;
            comment.DeletedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
