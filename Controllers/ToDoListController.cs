using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;

namespace ToDoList.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly TodoContext _context;
        public ToDoListController(TodoContext context)
        {
            _context = context;
        }

        // GET: Todos

        public async Task<ActionResult> Index()
        {
            //gets all the todolists
            var todoList = await _context.TodoLists.ToListAsync();

            return View(todoList);
        }
    }
}