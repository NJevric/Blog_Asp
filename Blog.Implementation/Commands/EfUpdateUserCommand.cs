using Blog.Application.Commands;
using Blog.Application.DataTransfer;
using Blog.Application.Exceptions;
using Blog.Domain;
using Blog.Implementation.Validators;
using EfDataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Commands
{
    public class EfUpdateUserCommand : IUpdateUserCommand
    {
        private readonly BlogContext _context;
        private readonly UpdateUserValidator _validator;
        public EfUpdateUserCommand(BlogContext context, UpdateUserValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public int Id => 9;

        public string Name => "Update User Using EF";

        public void Execute(UserDto request)
        {
            var user = _context.Users.Include(u => u.UserUseCases).FirstOrDefault(u => u.Id == request.Id);

            if (user == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(User));
            }

            _validator.ValidateAndThrow(request);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Username = request.Username;
            user.Password = request.Password;

            user.UserUseCases
                .Where(uuc => !request.AllowedUseCases.Contains(uuc.UseCaseId))
                .ToList()
                .ForEach(uc => user.UserUseCases.Remove(uc));

            var existingUserUseCaseIds = user.UserUseCases.Select(uuc => uuc.UseCaseId);

            request.AllowedUseCases.Except(existingUserUseCaseIds).ToList().ForEach(useCaseId => _context.UserUseCase.Add(new UserUseCase
            {
                User = user,
                UseCaseId = useCaseId
            }));

            _context.SaveChanges();
        }
    }
}
