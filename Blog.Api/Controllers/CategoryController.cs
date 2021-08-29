using Blog.Application;
using Blog.Application.Commands;
using Blog.Application.Commands.Categories;
using Blog.Application.DataTransfer;
using Blog.Application.Exceptions;
using Blog.Application.Queries;
using Blog.Application.Queries.Categories;
using Blog.Application.Searches;
using EfDataAccess;
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
    //[Authorize]
    public class CategoryController : ControllerBase
    {
  
        private readonly UseCaseExecutor _executor;
        private readonly IApplicationActor _actor;

        public CategoryController(UseCaseExecutor executor, IApplicationActor actor)
        {
         
            _executor = executor;
            _actor = actor;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public IActionResult Get([FromQuery] CategorySearch search,[FromServices] IGetCategoriesQuery query)
        {
            
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id,[FromServices] IGetCategoryQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<CategoryController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CategoryDto dto,[FromServices] ICreateCategoryCommand command)
        {
         
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
          
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id,[FromBody] CategoryDto dto,[FromServices] IUpdateCategoryCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id, [FromServices] IDeleteCategoryCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return StatusCode(StatusCodes.Status204NoContent);

        }
    }
}
