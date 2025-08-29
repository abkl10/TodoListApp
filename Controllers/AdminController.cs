using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly TodoContext _context;

    public AdminController(UserManager<IdentityUser> userManager, TodoContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<IActionResult> Dashboard()
    {
        var users = await _userManager.Users.ToListAsync();
        ViewBag.UserCount = users.Count;
        ViewBag.TasksCount = await _context.TodoLists.CountAsync();
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> SetRole(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (role == "Admin" || role == "User")
                await _userManager.AddToRoleAsync(user, role);
        }

        return RedirectToAction("Dashboard");
    }
    
}