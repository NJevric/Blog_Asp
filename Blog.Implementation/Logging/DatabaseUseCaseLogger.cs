using Blog.Application;
using Blog.Domain;
using EfDataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Implementation.Logging
{
    public class DatabaseUseCaseLogger : IUseCaseLogger
    {
        private readonly BlogContext _context;

        public DatabaseUseCaseLogger(BlogContext context)
        {
            _context = context;
        }

        public void Log(IUseCase useCase, IApplicationActor actor, object useCaseData)
        {
            _context.ActivityLogs.Add(new ActivityLog
            {
                CreatedAt = DateTime.Now,
                UseCaseName = useCase.Name,
                Data = JsonConvert.SerializeObject(useCaseData),
                Actor = actor.Identity
            });

            _context.SaveChanges();
        }
    }
}
