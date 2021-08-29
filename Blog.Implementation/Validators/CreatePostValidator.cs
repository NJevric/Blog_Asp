using Blog.Application.DataTransfer;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Validators
{
    public class CreatePostValidator : AbstractValidator<PostDto>
    {
        public CreatePostValidator(BlogContext context)
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage("Post Title can't be empty")
                .DependentRules(() =>
                {
                    RuleFor(p => p.Title)
                        .Must(title => !context.Posts.Any(p => p.Title == title))
                        .WithMessage(dto => $"Post with title - {dto.Title} - already exists");
                });

            RuleFor(p => p.Content)
               .NotEmpty()
               .WithMessage("Post Content can't be empty")
               .DependentRules(() =>
               {
                   RuleFor(p => p.Content)
                       .Must(content => !context.Posts.Any(p => p.Content == content))
                       .WithMessage(dto => $"Post with content - {dto.Content} - already exists");
               });

            RuleFor(p => p.CategoryIds)
                .NotEmpty()
                .WithMessage("You must choose category")
                .DependentRules(() =>
                {
                    RuleFor(p => p.CategoryIds)
                        .Must(ids => ids.Distinct().Count() == ids.Count())
                        .WithMessage("You can't choose same categories");

                    RuleForEach(p => p.CategoryIds)
                        .Must(id => context.Categories.Any(c => c.Id == id))
                        .WithMessage((dto, id) => $"Color with an id of {id} does not exists in database.");
                });

            RuleFor(p => p.Images)
                .NotEmpty()
                .WithMessage("You must choose minimum one picture for the post");


        }
    }
}
