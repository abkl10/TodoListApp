using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    
    [Authorize]
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

        // GET: Todos/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST : /todo/create
        [HttpPost]
        public async Task<ActionResult> Create(TodoList item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Item has been added";
                return RedirectToAction("Index");
            }
            return View(item);
        }

        //GET /Todo/edit/id
        public async Task<ActionResult> Edit(int id)
        {
            TodoList item = await _context.TodoLists.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        //POST /Todo/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TodoList item)
        {
            if (ModelState.IsValid)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Item has been updated";

                return RedirectToAction("Index");
            }

            return View(item);
        }

        //GET /Todo/Delete
        public async Task<ActionResult> Delete(int id)
        {
            TodoList item = await _context.TodoLists.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = "The item does not exist";

            }
            else
            {
                _context.TodoLists.Remove(item);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The item has been deleted";

            }

            return RedirectToAction("Index");
        }


    }
}