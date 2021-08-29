using Blog.Application.Commands.Likes;
using Blog.Application.DataTransfer;
using Blog.Domain;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Commands.Likes
{
    public class EfCreateLikeCommand : ICreateLikeCommand
    {
        private readonly BlogContext _context;

        public EfCreateLikeCommand(BlogContext context)
        {
            _context = context;
        }

        public int Id => 100;

        public string Name => "Create Like Using EF";

        public void Execute(LikeDto request)
        {
            //var likeUser = _context.Likes.Select(x => x.UserId);
            if(!_context.Likes.Where(x => x.PostId == request.PostId).Any(x => x.UserId == request.UserId))
            {
                var like = new Like
                {
                    PostId = request.PostId,
                    UserId = request.UserId
                };

                _context.Likes.Add(like);

                var post = _context.Posts.Include(p => p.Likes).FirstOrDefault(o => o.Id == request.PostId);



                post.Like = _context.Likes.Count(x => x.PostId == request.PostId) + 1;

                _context.SaveChanges();
            }

            Console.WriteLine("Nedozvoljeno lajkovati ponovo");

        }
    }
}
