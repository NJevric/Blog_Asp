using Blog.Application.DataTransfer;
using Blog.Application.Queries;
using Blog.Application.Searches;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Queries
{
    public class EfGetUsersQuery : IGetUsersQuery
    {
        public readonly BlogContext _context;

        public EfGetUsersQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 6;

        public string Name => "Get Users Using EF";

        public PageResponse<UserDto> Execute(UserSearch search)
        {
            var users = _context.Users.Include(x => x.UserUseCases).AsQueryable();

            var skipCount = search.PerPage * (search.Page - 1);

            if(!string.IsNullOrEmpty(search.FirstName) || !string.IsNullOrWhiteSpace(search.FirstName))
            {
                users = users.Where(x => x.FirstName.ToLower().Contains(search.FirstName.ToLower()));
            }
            if (!string.IsNullOrEmpty(search.LastName) || !string.IsNullOrWhiteSpace(search.LastName))
            {
                users = users.Where(x => x.LastName.ToLower().Contains(search.LastName.ToLower()));
            }
            if (!string.IsNullOrEmpty(search.Username) || !string.IsNullOrWhiteSpace(search.Username))
            {
                users = users.Where(x => x.Username.ToLower().Contains(search.Username.ToLower()));
            }
            if (search.UseCaseIds.Count() > 0)
            {
                users = users.Where(x => x.UserUseCases.Any(pc => search.UseCaseIds.Contains(pc.UseCaseId)));
            }
            var response = new PageResponse<UserDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = users.Count(),
                Items = users.Skip(skipCount).Take(search.PerPage).Select(x => new UserDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Username = x.Username,
                    AllowedUseCases = x.UserUseCases.Select(x => x.UseCaseId).ToList()
                }).ToList()
            };
       
            return response;
        }

        
    }
}
