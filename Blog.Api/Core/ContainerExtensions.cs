using Blog.Application.Commands;
using Blog.Application.Commands.Categories;
using Blog.Application.Commands.Posts;
using Blog.Application.Email;
using Blog.Application.Queries;
using Blog.Application.Queries.Categories;
using Blog.Application.Queries.Logs;
using Blog.Application.Queries.Posts;
using Blog.Implementation.Commands;
using Blog.Implementation.Commands.Categories;
using Blog.Implementation.Commands.Posts;
using Blog.Implementation.Queries;
using Blog.Implementation.Queries.Categories;
using Blog.Implementation.Queries.Logs;
using Blog.Implementation.Queries.Posts;
using Microsoft.Extensions.DependencyInjection;
using Blog.Implementation.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Commands.Comments;
using Blog.Application.Queries.Comments;
using Blog.Implementation.Queries.Comments;
using Blog.Implementation.Commands.Comments;
using Blog.Application.Commands.Likes;
using Blog.Implementation.Commands.Likes;
using Blog.Application.Commands.Images;
using Blog.Implementation.Commands.Images;
using Blog.Application.Queries.Images;
using Blog.Implementation.Queries.Images;

namespace Blog.Api.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            //users services
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IGetUserQuery, EfGetUserQuery>();
            services.AddTransient<ICreateUserCommand, EfCreateUserCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();

            //categories services
            services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
            services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();
            services.AddTransient<IUpdateCategoryCommand, EfUpdateCategoryCommand>();
            services.AddTransient<IGetCategoriesQuery, EfGetCategoriesQuery>();
            services.AddTransient<IGetCategoryQuery, EfGetCategoryQuery>();

            //posts services
            services.AddTransient<ICreatePostCommand, EfCreatePostCommand>();
            services.AddTransient<IUpdatePostCommand, EfUpdatePostCommand>();
            services.AddTransient<IDeletePostCommand, EfDeletePostCommand>();
            services.AddTransient<IGetPostsQuery, EfGetPostsQuery>();
            services.AddTransient<IGetPostQuery, EfGetPostQuery>();

            //logs services
            services.AddTransient<IGetLogsQuery, EfGetLogsQuery>();

            //email services
            services.AddTransient<IEmailSender, SmtpEmailSender>();

            //comments services
            services.AddTransient<ICreateCommentCommand, EfCreateCommentCommand>();
            services.AddTransient<IUpdateCommentCommand, EfUpdateCommentCommand>();
            services.AddTransient<IDeleteCommentCommand, EfDeleteCommentCommand>();
            services.AddTransient<IGetCommentsQuery, EfGetCommentsQuery>();
            services.AddTransient<IGetCommentQuery, EfGetCommentQuery>();

            //likes services
            services.AddTransient<ICreateLikeCommand, EfCreateLikeCommand>();
            services.AddTransient<IDeleteLikeCommand, EfDeleteLikeCommand>();

            //images services
            services.AddTransient<IDeleteImageCommand, EfDeleteImageCommand>();
            services.AddTransient<IGetImagesQuery, EfGetImagesQuery>();
        }
    }
}
