using Blog.Application.DataTransfer;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Validators
{
    public class CreateCommentValidator : AbstractValidator<CommentDto>
    {
        public CreateCommentValidator(BlogContext context)
        {
            RuleFor(x => x.CommentText)
                .NotEmpty()
                .WithMessage("Comment field can't be empty")
                .DependentRules(() =>
                {
                    RuleFor(x => x.PostId)
                   .Must(id => context.Posts.Any(p => p.Id == id))
                   .WithMessage((dto, id) => $"Post - {id} - doesn't exist");
                });

           

           

        }
    }
}
