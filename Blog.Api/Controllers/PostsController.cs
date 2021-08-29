using Blog.Application;
using Blog.Application.Commands.Posts;
using Blog.Application.DataTransfer;
using Blog.Application.Queries.Posts;
using Blog.Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class PostsController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;
        private readonly IApplicationActor _actor;
        public PostsController(UseCaseExecutor executor, IApplicationActor actor)
        {
            _executor = executor;
            _actor = actor;
        }

        // GET: api/<PostsController>
        [HttpGet]
        public IActionResult Get([FromQuery] PostSearch search, [FromServices] IGetPostsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id,[FromServices] IGetPostQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<PostsController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromForm] PostDto dto,[FromServices] ICreatePostCommand command)
        {
            dto.UserId = _actor.Id;
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromForm] PostDto dto,[FromServices] IUpdatePostCommand command)
        {
            dto.Id = id;
            dto.UserId = _actor.Id;
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id,[FromServices] IDeletePostCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
