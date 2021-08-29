using Blog.Application.DataTransfer;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Validators
{
    public class CreateUserValidator : AbstractValidator<UserDto>
    {
        public CreateUserValidator(BlogContext context)
        {
             RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First Name is required.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.FirstName)
                        .MaximumLength(30)
                        .WithMessage("First Name can have a maximum of 30 characters");
                });

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last Name is required.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.LastName)
                        .MaximumLength(30)
                        .WithMessage("Last Name can have a maximum of 30 characters");
                });

            RuleFor(x => x.Username)
              .NotEmpty()
              .WithMessage("Username is required.")
              .DependentRules(() =>
              {
                  RuleFor(x => x.Username)
                      .MaximumLength(30)
                      .WithMessage("Username can have a maximum of 30 characters")
                        .DependentRules(() =>
                        {
                            RuleFor(x => x.Username)
                            .Must((dto, username) => !context.Users.Any(user => user.Username == username))
                            .WithMessage(dto => $"Username - {dto.Username} - already exists");
                        });
              });

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .DependentRules(() => {
                    RuleFor(x => x.Password)
                        .MinimumLength(6)
                        .WithMessage("Minimum length for password is 6 characters")
                        .MaximumLength(50)
                        .WithMessage("Maximum length for password is 50 characters");
                });

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Email)
                        .EmailAddress()
                        .WithMessage("Entered email is not valid.")
                        .DependentRules(() =>
                        {
                            RuleFor(x => x.Email)
                            .Must((dto, email) => !context.Users.Any(user => user.Email == email))
                            .WithMessage(dto => $"Email - {dto.Email} - already exists");
                        });
                });
        }
    }
}
