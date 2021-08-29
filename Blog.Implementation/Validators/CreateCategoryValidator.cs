using Blog.Application.DataTransfer;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CategoryDto>
    {
       

        public CreateCategoryValidator(BlogContext context)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Category name is required")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name)
                    .Must(name => !context.Categories.Any(c => c.Name == name))
                    .WithMessage("Category name - {PropertyValue} -  already exists in database");
                });
             
                
        }
    }
}
