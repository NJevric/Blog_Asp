using Blog.Application.Commands;
using Blog.Application.DataTransfer;
using Blog.Application.Email;
using Blog.Domain;
using Blog.Implementation.Validators;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Implementation.Commands
{
    public class EfCreateUserCommand : ICreateUserCommand
    {
        private readonly BlogContext _context;
        private readonly IEmailSender _sender;
        private readonly CreateUserValidator _validator;
        public EfCreateUserCommand(BlogContext context, IEmailSender sender,CreateUserValidator validator)
        {
            _context = context;
            _sender = sender;
            _validator = validator;
        }

        public int Id => 8;

        public string Name => "Create User Using EF";

        public void Execute(UserDto request)
        {
            _validator.ValidateAndThrow(request);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password
            };

            var userUseCases = new List<int> { 3, 4, 14, 15, 8, 17,19,100,101 };

            userUseCases.ForEach(useCase => _context.UserUseCase.Add(new UserUseCase
            {
                User = user,
                UseCaseId = useCase
            }));

            _context.Users.Add(user);
            _context.SaveChanges();

            _sender.Send(new SendEmailDto
            {
                Subject = "User Registration",
                Content = "<h1>Successfull registration</h1>",
                SendTo = request.Email
                
            });
        }
    }
}
