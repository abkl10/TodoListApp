using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models;

public class TodoList
{
    public int id { get; set; }

    [Required]
    public required string Content { get; set; }
    public bool IsCompleted { get; set; } = false;

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string? UserId { get; set; }
    
}
