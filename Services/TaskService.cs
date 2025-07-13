using Microsoft.AspNetCore.Http;
using TaskManagnmentApi.Data;
using TaskManagnmentApi.Dtos;
using TaskManagnmentApi.Models;

namespace TaskManagnmentApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TaskService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<TaskItem>> GetTasksAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.Identity.Name;
            return await Task.FromResult(_context.Tasks
                .Where(t => t.UserId == userId)
                .ToList());
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            var userId = _httpContextAccessor.HttpContext.User.Identity.Name;
            return await Task.FromResult(_context.Tasks
                .FirstOrDefault(t => t.Id == id && t.UserId == userId));
        }

        public async Task<TaskItem> CreateTaskAsync(TaskCreateDto taskDto)
        {
            var userId = _httpContextAccessor.HttpContext.User.Identity.Name;
            var task = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> UpdateTaskAsync(int id, TaskUpdateDto taskDto)
        {
            var userId = _httpContextAccessor.HttpContext.User.Identity.Name;
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id && t.UserId == userId);
            if (task == null)
                return false;

            task.Title = taskDto.Title;
            task.Description = taskDto.Description;
            task.IsCompleted = taskDto.IsCompleted;
            task.DueDate = taskDto.DueDate;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var userId = _httpContextAccessor.HttpContext.User.Identity.Name;
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id && t.UserId == userId);
            if (task == null)
                return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}