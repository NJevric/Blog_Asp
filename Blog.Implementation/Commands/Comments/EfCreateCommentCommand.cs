using Blog.Application;
using Blog.Application.Commands.Comments;
using Blog.Application.DataTransfer;
using Blog.Domain;
using Blog.Implementation.Validators;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Implementation.Commands.Comments
{
    public class EfCreateCommentCommand : ICreateCommentCommand
    {
        private readonly BlogContext _context;
        private readonly CreateCommentValidator _validator;

        public EfCreateCommentCommand(BlogContext context, CreateCommentValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 17;

        public string Name => "Create Comment Using EF";

        public void Execute(CommentDto request)
        {
            _validator.ValidateAndThrow(request);
            var comment = new Comment
            {
                Author = request.CommentAuthor,
                CommetText = request.CommentText,
                PostId = request.PostId
               
            };
           
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}
