# ToDoList MVC Application

A simple ASP.NET Core MVC ToDoList application with user authentication and personalized task management.

## Features

- User registration and login with ASP.NET Core Identity
- Each user has their own task list (tasks are user-specific)
- Create, edit, delete, and toggle completion status of tasks
- Pagination for task lists
- Dashboard with task summaries and recent tasks
- Responsive UI with Bootstrap 5
- Secure actions with `[Authorize]` attribute

## Technologies Used

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core with MySQL (Pomelo provider)
- ASP.NET Core Identity for authentication and authorization
- Bootstrap 5 for styling
- FontAwesome icons

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- MySQL server
- A code editor (Visual Studio, VS Code, etc.)

### Setup

1. Clone this repository:
```bash
   git clone <your-repo-url>
   cd ToDoList
   ```

2. Update your appsettings.json with your MySQL connection string:
```bash
   "ConnectionStrings": {
   "DefaultConnection": "server=localhost;database=ToDoList;user=root;password=your_password;"
}
   ```

3. Apply migrations and update the database:
```bash
dotnet ef database update
```
4. Run the application:
```bash
dotnet run
```

5. Open your browser and navigate to https://localhost:5001 or the URL shown in the console.

6. Usage

MVC Web Application
- Register a new user account.
- Manage your personal task list: add, edit, delete, and mark tasks as completed.
- View your dashboard with recent tasks and task statistics.

RESTful API
- Obtain a JWT token by authenticating with your user credentials (via API login endpoint).
- Use the JWT token in the Authorization header with Bearer <token> to access the secure API endpoints.
- API base URL: https://localhost:5001/api/ToDoListapi
- Supported API operations:
- GET /api/ToDoListapi — Get all your tasks
- GET /api/ToDoListapi/{id} — Get a specific task by ID
- POST /api/ToDoListapi — Create a new task
- PUT /api/ToDoListapi/{id} — Update a task
- DELETE /api/ToDoListapi/{id} — Delete a task