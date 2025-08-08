using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models;

public class TodoList
{
    public int id { get; set; }
    
    [Required]
    public required string Content { get; set;  }
}
