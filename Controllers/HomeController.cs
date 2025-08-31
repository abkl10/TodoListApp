using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly TodoContext _context;
    private readonly UserManager<IdentityUser> _userManager;


    public HomeController(TodoContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;

    }

    public async Task<IActionResult> Index()
{
    var userId = _userManager.GetUserId(User);

    ViewBag.RecentTasks = await _context.TodoLists
        .Where(t => t.UserId == userId)
        .OrderByDescending(t => t.id)
        .Take(3)
        .ToListAsync();

    ViewBag.TotalTasks = !User.IsInRole("Admin") ? await _context.TodoLists
        .Where(t => t.UserId == userId)
        .CountAsync() : await _context.TodoLists.CountAsync();

    ViewBag.CompletedTasks = !User.IsInRole("Admin") ? await _context.TodoLists
        .Where(t => t.UserId == userId && t.IsCompleted)
        .CountAsync() : await _context.TodoLists
        .Where(t => t.IsCompleted)
        .CountAsync();

    ViewBag.PendingTasks = !User.IsInRole("Admin") ? await _context.TodoLists
        .Where(t => t.UserId == userId && !t.IsCompleted)
        .CountAsync() : await _context.TodoLists
        .Where(t => !t.IsCompleted)
        .CountAsync();

    var users = await _userManager.Users.ToListAsync();
        ViewBag.UserCount = users.Count;    

    

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
