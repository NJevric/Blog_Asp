using Blog.Application.DataTransfer;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Validators
{
    public class CreateLoginValidator : AbstractValidator<LoginRequest>
    {
        public CreateLoginValidator(BlogContext context)
        {
            RuleFor(x => x.Username)
              .NotEmpty()
              .WithMessage("Username is required")
              .DependentRules(() => {
                  RuleFor(x => x.Username)
                  .Must(username => context.Users.Any(u => u.Username == username))
                  .WithMessage("Wrong username input");
              });
            RuleFor(x => x.Password)
               .NotEmpty()
               .WithMessage("Password is required")
               .DependentRules(() => {
                   RuleFor(x => x.Password)
                   .Must(password => context.Users.Any(u => u.Password == password))
                   .WithMessage("Wrong password input");
               });
        }
    }
}
