using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;

        public ToDoListController(TodoContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Todos
        public async Task<ActionResult> Index()
        {
            //gets all the todolists
            var userId = _userManager.GetUserId(User);
            var todoList = await _context.TodoLists
                .Where(t => t.UserId == userId)
                .ToListAsync();
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
                item.UserId = _userManager.GetUserId(User);
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
            var userId = _userManager.GetUserId(User);

            var item = await _context.TodoLists
                .Where(t => t.UserId == userId && t.id == id)
                .FirstOrDefaultAsync();

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
                var userId = _userManager.GetUserId(User);

                var existingItem = await _context.TodoLists
                    .Where(t => t.UserId == userId && t.id == item.id)
                    .FirstOrDefaultAsync();

                if (existingItem == null)
                {
                    return NotFound();
                }

            
                _context.Update(existingItem);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Item has been updated";

                return RedirectToAction("Index");
            }

            return View(item);
        }


        //GET /Todo/Delete
        public async Task<ActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);

            var item = await _context.TodoLists
                .Where(t => t.UserId == userId && t.id == id)
                .FirstOrDefaultAsync();
                
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

        [HttpPost]
        public async Task<ActionResult> ToggleComplete(int id)
        {
            var userId = _userManager.GetUserId(User);

            var item = await _context.TodoLists
                .Where(t => t.UserId == userId && t.id == id)
                .FirstOrDefaultAsync();

            if (item == null)
            {
                return NotFound();
            }

            item.IsCompleted = !item.IsCompleted;
            _context.Update(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}