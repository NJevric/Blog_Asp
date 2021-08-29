using Blog.Application.DataTransfer;
using Blog.Application.Exceptions;
using Blog.Application.Queries.Posts;
using Blog.Domain;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Queries.Posts
{
    public class EfGetPostQuery : IGetPostQuery
    {
        public readonly BlogContext _context;

        public EfGetPostQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 15;

        public string Name => "Get Post Using EF";

        public ReadPostDto Execute(int search)
        {
            var post = _context.Posts
                .Include(u => u.User)
                .Include(p => p.PostCategories)
                .ThenInclude(pc => pc.Category)
                .Include(c => c.Comments)
                .Include(i => i.PostImages)
                .FirstOrDefault(x => x.Id == search);
            

            if (post == null)
            {
                throw new EntityNotFoundException(search, typeof(Post));
            }

            return new ReadPostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Like = post.Like,
                AuthorName = post.User.FirstName + " " + post.User.LastName,
                Categories = post.PostCategories.Select(pc => new CategoryDto
                {
                    Id = pc.CategoryId,
                    Name = pc.Category.Name
                }).ToList(),
                Comments = post.Comments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    CommentAuthor = c.Author,
                    CommentText = c.CommetText
                }).ToList(),
                Images = post.PostImages.Select(i => new ImageDto
                {
                    Id = i.Id,
                    Src = i.Src,
                    Alt = i.Alt
                }).ToList()
            };
        }
    }
}
