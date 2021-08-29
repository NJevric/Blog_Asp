using Blog.Application.DataTransfer;
using Blog.Application.Exceptions;
using Blog.Application.Queries.Comments;
using Blog.Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Implementation.Queries.Comments
{
    public class EfGetCommentQuery : IGetCommentQuery
    {
        private readonly BlogContext _context;

        public EfGetCommentQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 21;

        public string Name => "Get Comment Using EF";

        public CommentDto Execute(int search)
        {
            var comment = _context.Comments.Find(search);

            if(comment == null)
            {
                throw new EntityNotFoundException(search, typeof(Comment));
            }

            return new CommentDto
            {
                Id = comment.Id,
                CommentAuthor = comment.Author,
                CommentText = comment.CommetText,
                PostId = comment.PostId,
                Approved = comment.Approved
            };
        }
    }
}
