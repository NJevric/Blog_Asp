using Blog.Application;
using Blog.Application.Queries.Logs;
using Blog.Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LogsController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;
        private readonly IApplicationActor _actor;

        public LogsController(UseCaseExecutor executor, IApplicationActor actor)
        {
            _executor = executor;
            _actor = actor;
        }

      

        // GET: api/<LogsController>
        [HttpGet]
        public IActionResult Get([FromQuery] LogSearch search, [FromServices] IGetLogsQuery query)
        {
            
            return Ok(_executor.ExecuteQuery(query, search));
        }

       
    }
}
