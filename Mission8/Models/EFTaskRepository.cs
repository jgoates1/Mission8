using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mission8.Data;

namespace Mission8.Models
{
    // Concrete repository: implements ITaskRepository using Entity Framework and TaskDbContext.
    public class EFTaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public EFTaskRepository(TaskDbContext temp)
        {
            _context = temp;
        }

        // Include Category so the quadrant view can show task.Category.CategoryName without extra queries.
        public IQueryable<TaskItem> TaskItem => _context.TaskItem.Include(t => t.Category);

        public IQueryable<Category> Categories => _context.Categories;

        public void AddTask(TaskItem task) => _context.TaskItem.Add(task);
        public void UpdateTask(TaskItem task) => _context.TaskItem.Update(task);
        public void DeleteTask(TaskItem task) => _context.TaskItem.Remove(task);
        public void Save() => _context.SaveChanges();
    }
}