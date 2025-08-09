using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly TodoContext _context;

    public HomeController(TodoContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.RecentTasks = await _context.TodoLists
            .OrderByDescending(t => t.id)
            .Take(3)
            .ToListAsync();

        ViewBag.TotalTasks = await _context.TodoLists.CountAsync();
        ViewBag.CompletedTasks = await _context.TodoLists.CountAsync(t => t.IsCompleted);
        ViewBag.PendingTasks = await _context.TodoLists.CountAsync(t => !t.IsCompleted);

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
