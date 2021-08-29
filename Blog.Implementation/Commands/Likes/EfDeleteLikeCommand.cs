
using Blog.Application;
using Blog.Application.Commands.Likes;
using Blog.Application.Exceptions;
using Blog.Domain;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Commands.Likes
{
    public class EfDeleteLikeCommand : IDeleteLikeCommand
    {
        private readonly BlogContext _context;
        private readonly IApplicationActor _actor;
        public EfDeleteLikeCommand(BlogContext context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;
        }
        public int Id => 101;

        public string Name => "Remove Like Using EF";

        public void Execute(int request)
        {
            var like = _context.Likes.Find(request);
            if (_actor.Id != like.UserId)
            {
                throw new EntityNotFoundException(request, typeof(User));
            }
            
            var post = _context.Posts.Include(p => p.Likes).FirstOrDefault(o => o.Id == like.PostId);

            post.Like -= 1;

            _context.Remove(like);
            _context.SaveChanges();
            

           
        }
    }
}
