using Blog.Application;
using Blog.Application.DataTransfer;
using Blog.Application.Queries;
using Blog.Application.Queries.Posts;
using Blog.Application.Searches;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Queries.Posts
{
    public class EfGetPostsQuery : IGetPostsQuery
    {
        public readonly BlogContext _context;

        public EfGetPostsQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 14;

        public string Name => "Get Posts Using EF";

        public PageResponse<ReadPostDto> Execute(PostSearch search)
        {
            var posts = _context.Posts
               .Include(p => p.User)
               .Include(c => c.Comments)
               .Include(p => p.PostCategories)
               .ThenInclude(pc => pc.Category)
               .AsQueryable();

            if(!string.IsNullOrEmpty(search.Title) || !string.IsNullOrWhiteSpace(search.Title))
            {
                posts = posts.Where(x => x.Title.ToLower().Contains(search.Title.ToLower()));
            }

            if (search.MinLike.HasValue)
            {
                posts = posts.Where(x => x.Like >= search.MinLike);
            }
            if (search.MaxLike.HasValue)
            {
                posts = posts.Where(x => x.Like <= search.MaxLike);
            }
            if(search.CategoryIds.Count() > 0)
            {
                posts = posts.Where(x => x.PostCategories.Any(pc => search.CategoryIds.Contains(pc.CategoryId)));
            }
           

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PageResponse<ReadPostDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = posts.Count(),
                Items = posts.Skip(skipCount).Take(search.PerPage).Select(x => new ReadPostDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    Like = x.Like,
                    AuthorName = x.User.FirstName + " " + x.User.LastName,
                    Categories = x.PostCategories.Select(pc => new CategoryDto
                    {
                        Id = pc.CategoryId,
                        Name = pc.Category.Name

                    }),
                    Comments = x.Comments.Select(c => new CommentDto
                    {
                        Id = c.Id,
                        CommentAuthor = c.Author,
                        CommentText = c.CommetText,
                        Approved = c.Approved
                    }),
                    Images = x.PostImages.Select(i => new ImageDto
                    {
                        Id = i.Id,
                        Src = i.Src,
                        Alt = i.Alt
                    })
                }).ToList()
            };

            return response;
        }

      
    }
}
