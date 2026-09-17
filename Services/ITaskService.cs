using MyBlazorApp.Models;

namespace MyBlazorApp.Services;

public interface ITaskService
{
    Task<List<TaskItem>> GetTasksAsync();

    Task<TaskItem> AddTaskAsync(string title, TaskPriority priority, DateTime? dueDate, string? notes);

    Task ToggleDoneAsync(int id);

    Task DeleteTaskAsync(int id);
}
