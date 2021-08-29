using Blog.Application;
using Blog.Application.DataTransfer;
using Blog.Application.Queries;
using Blog.Application.Queries.Comments;
using Blog.Application.Searches;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Queries.Comments
{
    public class EfGetCommentsQuery : IGetCommentsQuery
    {
        private readonly BlogContext _context;

        public EfGetCommentsQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 20;

        public string Name => "Get Comments Using EF";

        public PageResponse<CommentDto> Execute(CommentSearch search)
        {
            var query = _context.Comments.AsQueryable();
            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PageResponse<CommentDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new CommentDto
                {
                    Id = x.Id,
                    CommentAuthor = x.Author,
                    CommentText = x.CommetText,
                    PostId = x.PostId,
                    Approved = x.Approved
                }).ToList()
            };

            return response;
        }

       
    }
}
