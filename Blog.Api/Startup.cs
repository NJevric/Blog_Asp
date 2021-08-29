using Blog.Api.Core;
using Blog.Application;
using Blog.Application.Commands;
using Blog.Application.Commands.Categories;
using Blog.Application.Commands.Comments;
using Blog.Application.Commands.Images;
using Blog.Application.Commands.Likes;
using Blog.Application.Commands.Posts;
using Blog.Application.Email;
using Blog.Application.Queries;
using Blog.Application.Queries.Categories;
using Blog.Application.Queries.Comments;
using Blog.Application.Queries.Images;
using Blog.Application.Queries.Logs;
using Blog.Application.Queries.Posts;
using Blog.Implementation.Commands;
using Blog.Implementation.Commands.Categories;
using Blog.Implementation.Commands.Comments;
using Blog.Implementation.Commands.Images;
using Blog.Implementation.Commands.Likes;
using Blog.Implementation.Commands.Posts;
using Blog.Implementation.Logging;
using Blog.Implementation.Queries;
using Blog.Implementation.Queries.Categories;
using Blog.Implementation.Queries.Comments;
using Blog.Implementation.Queries.Images;
using Blog.Implementation.Queries.Logs;
using Blog.Implementation.Queries.Posts;
using Blog.Implementation.Validators;
using EfDataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<BlogContext>();

            //extension method for services
            services.AddUseCases();

            //validators
            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<UpdateCategoryValidator>();
            services.AddTransient<CreateCommentValidator>();
            services.AddTransient<CreatePostValidator>();
            services.AddTransient<UpdatePostValidator>();
            services.AddTransient<CreateUserValidator>();
            services.AddTransient<UpdateUserValidator>();
            services.AddTransient<CreateLoginValidator>();

            //exceptions
            services.AddTransient<UseCaseExecutor>();

            //logging
            //services.AddTransient<IUseCaseLogger, ConsoleUseCaseLogger>();
            services.AddTransient<IUseCaseLogger, DatabaseUseCaseLogger>();

            //actorUseCase
            //services.AddTransient<IApplicationActor, FakeApiActor>();
            services.AddHttpContextAccessor();
            services.AddTransient<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();

                var user = accessor.HttpContext.User;

                if (user.FindFirst("ActorData") == null)
                {
                    return new AnonymousActor();
                }

                var actorString = user.FindFirst("ActorData").Value;

                var actor = JsonConvert.DeserializeObject<JwtActor>(actorString);

                return actor;

            });
            services.AddTransient<JwtManager>();
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "asp_api",
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMyVerySecretKey")),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();


            //swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog v1");
            });

            app.UseRouting();


            app.UseMiddleware<GlobalExceptionHandler>();
            app.UseAuthentication();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
