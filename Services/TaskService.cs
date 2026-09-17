using MyBlazorApp.Models;

namespace MyBlazorApp.Services;

/// <summary>
/// Simple in-memory task store. Registered as a singleton so tasks
/// persist for the lifetime of the browser tab (Blazor WebAssembly
/// runs entirely client-side, so there's no shared server state to
/// worry about). Swap this out for an HttpClient-backed implementation
/// later without touching any of the pages that consume ITaskService.
/// </summary>
public class TaskService : ITaskService
{
    private readonly List<TaskItem> _tasks = new();
    private int _nextId = 1;

    public TaskService()
    {
        // A few sample tasks so the dashboard isn't empty on first run.
        _tasks.Add(new TaskItem
        {
            Id = _nextId++,
            Title = "Feed the cat",
            Notes = "She will remind you loudly if you forget.",
            Priority = TaskPriority.High,
            DueDate = DateTime.Today
        });
        _tasks.Add(new TaskItem
        {
            Id = _nextId++,
            Title = "Finish Balogbog app",
            Notes = "Wire up the dashboard and login screens.",
            Priority = TaskPriority.Medium,
            DueDate = DateTime.Today.AddDays(2)
        });
        _tasks.Add(new TaskItem
        {
            Id = _nextId++,
            Title = "Take a break",
            Priority = TaskPriority.Low,
            IsDone = true
        });
    }

    public Task<List<TaskItem>> GetTasksAsync()
    {
        var ordered = _tasks
            .OrderBy(t => t.IsDone)
            .ThenByDescending(t => t.Priority)
            .ThenBy(t => t.DueDate ?? DateTime.MaxValue)
            .ToList();

        return Task.FromResult(ordered);
    }

    public Task<TaskItem> AddTaskAsync(string title, TaskPriority priority, DateTime? dueDate, string? notes)
    {
        var task = new TaskItem
        {
            Id = _nextId++,
            Title = title,
            Priority = priority,
            DueDate = dueDate,
            Notes = notes
        };

        _tasks.Add(task);
        return Task.FromResult(task);
    }

    public Task ToggleDoneAsync(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task is not null)
        {
            task.IsDone = !task.IsDone;
        }

        return Task.CompletedTask;
    }

    public Task DeleteTaskAsync(int id)
    {
        _tasks.RemoveAll(t => t.Id == id);
        return Task.CompletedTask;
    }
}
