using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUD_C_.Data;
using CRUD_C_.CreateViewModel;
using CRUD_C_.Models;

namespace CRUD_C_.Controllers
{
    [ApiController]
    [Route("v1")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        [Route("todos")]
        public async Task<IActionResult> Get(
            [FromServices] AppDbContext context)
        {
            var todos = await context
                            .Todos
                            .AsNoTracking()
                            .ToListAsync();
                
           return Ok(todos);
        }

        [HttpGet]
        [Route("todos/{id}")]
        public async Task<IActionResult> GetById(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var todo = await context
                            .Todos
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == id);
                
           return todo == null ? BadRequest() : Ok(todo);
        }

        [HttpPost("todos")]
        public async Task<IActionResult> Post(
            [FromServices] AppDbContext context,
            [FromBody] CreateTodoViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return  BadRequest();                
            }

            var todo = new Todo 
            {
                Date = DateTime.Now,
                Done = false,
                Title = model.Title                
            };

            try
            {
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();

                return Created($"v1/todos/{todo.Id}", todo);
                
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("todos/{id}")]
        public async Task<IActionResult> Put(
            [FromServices]AppDbContext context,
            [FromBody] CreateTodoViewModel model,
            [FromRoute] int id)
        {   

            if (!ModelState.IsValid)
            {
                return BadRequest();                
            }
            var todo =  await context
                .Todos
                .FirstOrDefaultAsync(x => x.Id == id);
                
            if (todo == null)
            {
                return NotFound();
            }

            try
            {   
    
                todo.Title = model.Title;
                todo.Done = model.Done;

                context.Todos.Update(todo);
                await context.SaveChangesAsync();

                return Ok(todo);
                
            }
            catch (System.Exception)
            {
                return BadRequest();
            }                            
        }

        [HttpDelete("todos/{id}")]
        public async Task<IActionResult> Delete(
            [FromServices]AppDbContext context,
            [FromRoute] int id)
        {
            var todo =  await context
                .Todos
                .FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Todos.Remove(todo);
                await context.SaveChangesAsync();

                return Ok();
                
            }
            catch (System.Exception)
            {
                return BadRequest();
            } 

            
        }    
        
    }
} 