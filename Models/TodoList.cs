using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.Models;

public class TodoList
{
    public int id { get; set; }

    [Required]
    public required string Content { get; set; }
    public bool IsCompleted { get; set; } = false;

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string? UserId { get; set; }
    public string? Category { get; set; }

    public virtual IdentityUser? User { get; set; }

    
}
