using Blog.Application.Commands;
using Blog.Application.Exceptions;
using Blog.Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Implementation.Commands
{
    public class EfDeleteUserCommand : IDeleteUserCommand
    {
        public readonly BlogContext _context;

        public EfDeleteUserCommand(BlogContext context)
        {
            _context = context;
        }

        public int Id => 10;

        public string Name => "Delete User Using EF";

        public void Execute(int request)
        {
            var user = _context.Users.Find(request);

            if(user == null)
            {
                throw new EntityNotFoundException(request, typeof(User));
            }

            user.IsActive = false;
            user.IsDeleted = true;
            user.DeletedAt = DateTime.Now;

            _context.SaveChanges();

        }
    }
}
