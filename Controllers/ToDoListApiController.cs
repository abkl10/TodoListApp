using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  
    public class ToDoListapiController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ToDoListapiController(TodoContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/tasksapi
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {

            var userId = _userManager.GetUserId(User);

            var tasks = await _context.TodoLists
                .Where(t => t.UserId == userId) 
                .OrderByDescending(t => t.CreatedDate)
                .ToListAsync();

            return Ok(tasks);
        }

        // GET: api/tasksapi/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var userId = _userManager.GetUserId(User);
            var task = await _context.TodoLists
                .Where(t => t.UserId == userId && t.id == id)
                .FirstOrDefaultAsync();

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // POST: api/tasksapi
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TodoList task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            task.UserId = _userManager.GetUserId(User);
            task.CreatedDate = DateTime.Now;

            _context.TodoLists.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.id }, task);
        }

        // PUT: api/tasksapi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TodoList task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != task.id)
                return BadRequest();

            var userId = _userManager.GetUserId(User);
            var existingTask = await _context.TodoLists
                .Where(t => t.UserId == userId && t.id == id)
                .FirstOrDefaultAsync();

            if (existingTask == null)
                return NotFound();

            existingTask.Content = task.Content;
            existingTask.IsCompleted = task.IsCompleted;
            

            _context.TodoLists.Update(existingTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/tasksapi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userId = _userManager.GetUserId(User);
            var task = await _context.TodoLists
                .Where(t => t.UserId == userId && t.id == id)
                .FirstOrDefaultAsync();

            if (task == null)
                return NotFound();

            _context.TodoLists.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
