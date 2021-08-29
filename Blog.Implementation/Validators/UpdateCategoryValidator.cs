using Blog.Application.DataTransfer;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Validators
{
    public class UpdateCategoryValidator : AbstractValidator<CategoryDto>
    {
        public UpdateCategoryValidator(BlogContext context)
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("Category {PropertyName} can't be empty")
               .DependentRules(() =>
               {
                   RuleFor(x => x.Name)
                   .Must((dto, name) => !context.Categories.Any(c => c.Name == name && c.Id != dto.Id))
                   .WithMessage(dto => $"Category - {dto.Name} - already exists in database.");
               });
        }
    }
}
