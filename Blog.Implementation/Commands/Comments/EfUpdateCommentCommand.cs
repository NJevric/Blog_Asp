using Blog.Application.Commands.Comments;
using Blog.Application.DataTransfer;
using Blog.Application.Exceptions;
using Blog.Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Implementation.Commands.Comments
{
    public class EfUpdateCommentCommand : IUpdateCommentCommand
    {
        private readonly BlogContext _context;

        public EfUpdateCommentCommand(BlogContext context)
        {
            _context = context;
        }

        public int Id => 18;

        public string Name => "Update Comment Using EF";

        public void Execute(CommentDto request)
        {
            var comment = _context.Comments.Find(request.Id);

            if(comment == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Post));
            }

            comment.Approved = true;

            _context.SaveChanges();
        }
    }
}
