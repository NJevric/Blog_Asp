using Blog.Application.DataTransfer;
using Blog.Application.Exceptions;
using Blog.Application.Queries;
using Blog.Domain;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Queries
{
    public class EfGetUserQuery : IGetUserQuery
    {
        public readonly BlogContext _context;

        public EfGetUserQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 7;

        public string Name => "Get User Using EF";

        public UserDto Execute(int search)
        {
            var user = _context.Users.Include(u => u.UserUseCases).FirstOrDefault(u => u.Id == search);

            if(user == null)
            {
                throw new EntityNotFoundException(search, typeof(User));
            }

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                AllowedUseCases = user.UserUseCases.Select(x => x.UseCaseId).ToList()
            };
        }
    }
}
