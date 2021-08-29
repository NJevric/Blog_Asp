using Blog.Application;
using Blog.Application.Commands.Likes;
using Blog.Application.DataTransfer;
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
    [Authorize]
    public class LikesController : ControllerBase
    {

        private readonly UseCaseExecutor _executor;
        private readonly IApplicationActor _actor;

        public LikesController(UseCaseExecutor executor, IApplicationActor actor)
        {
            _executor = executor;
            _actor = actor;
        }
        // POST api/<LikesController>
        [HttpPost]
        public IActionResult Post([FromBody] LikeDto dto,[FromServices] ICreateLikeCommand command)
        {
            dto.UserId = _actor.Id;
             _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,[FromServices] IDeleteLikeCommand command)
        {
            
            _executor.ExecuteCommand(command,id);
            return StatusCode(StatusCodes.Status204NoContent);
        }


    }
}
