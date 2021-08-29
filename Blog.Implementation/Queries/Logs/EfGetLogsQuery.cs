using Blog.Application;
using Blog.Application.DataTransfer;
using Blog.Application.Queries;
using Blog.Application.Queries.Logs;
using Blog.Application.Searches;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Implementation.Queries.Logs
{
    public class EfGetLogsQuery : IGetLogsQuery
    {
        public readonly BlogContext _context;

        public EfGetLogsQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 16;

        public string Name => "Get Logs Using EF";

        public PageResponse<ReadLogsDto> Execute(LogSearch search)
        {
            var logs = _context.ActivityLogs.AsQueryable();

            if(!string.IsNullOrEmpty(search.Actor) || !string.IsNullOrWhiteSpace(search.Actor))
            {
                logs = logs.Where(x => x.Actor.ToLower().Contains(search.Actor.ToLower()));
            }
            if (search.MinDate.HasValue)
            {
                logs = logs.Where(x => x.CreatedAt >= search.MinDate);
            }
            if (search.MaxDate.HasValue)
            {
                logs = logs.Where(x => x.CreatedAt <= search.MaxDate);
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PageResponse<ReadLogsDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = logs.Count(),
                Items = logs.Skip(skipCount).Take(search.PerPage).Select(x => new ReadLogsDto
                {
                    Id = x.Id,
                    Actor = x.Actor,
                    Data = x.Data,
                    UseCaseName = x.UseCaseName
                }).ToList()
            };

            return response;
        }

        
    }
}
