using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace TodoListApp.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

    }
}
