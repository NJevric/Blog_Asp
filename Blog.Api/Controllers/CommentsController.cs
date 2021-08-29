using Blog.Application;
using Blog.Application.Commands.Comments;
using Blog.Application.DataTransfer;
using Blog.Application.Queries.Comments;
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
    public class CommentsController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;
        private readonly IApplicationActor _actor;

        public CommentsController(UseCaseExecutor executor, IApplicationActor actor)
        {
           
            _executor = executor;
            _actor = actor;
        }

        // GET: api/<CommentsController>
        [HttpGet]
        public IActionResult Get([FromQuery] CommentSearch search,[FromServices] IGetCommentsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query,search));
        }

        // GET api/<CommentsController>/5
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id, [FromServices] IGetCommentQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<CommentsController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CommentDto dto, [FromServices] ICreateCommentCommand command)
        {
            dto.CommentAuthor = _actor.Identity;
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<CommentsController>/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] CommentDto dto,[FromServices] IUpdateCommentCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
        }
    }
}
