using Blog.Application;
using Blog.Application.Commands.Images;
using Blog.Application.Queries.Images;
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
    [Authorize]
    public class ImagesController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;
        private readonly IApplicationActor _actor;

        public ImagesController(UseCaseExecutor executor, IApplicationActor actor)
        {

            _executor = executor;
            _actor = actor;
        }

        // GET: api/<ImagesController>
        [HttpGet]
        public IActionResult Get([FromQuery] ImageSearch search, [FromServices] IGetImagesQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // DELETE api/<ImagesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,[FromServices] IDeleteImageCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
