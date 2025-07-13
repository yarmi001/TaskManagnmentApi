using TaskManagnmentApi.Dtos;
using TaskManagnmentApi.Models;

namespace TaskManagnmentApi.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetTasksAsync();
    Task<TaskItem?> GetTaskByIdAsync(int id);
    Task<TaskItem> CreateTaskAsync(TaskCreateDto taskDto);
    Task<bool> UpdateTaskAsync(int id, TaskUpdateDto taskDto);
    Task<bool> DeleteTaskAsync(int id);
}