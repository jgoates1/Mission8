using Mission8.Data;

namespace Mission8.Models
{
    public class EFTaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public EFTaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public IQueryable<TaskItem> TaskItem => _context.TaskItem;
        public IQueryable<Category> Categories => _context.Categories;

        public void AddTask(TaskItem TaskName)
        {
            _context.TaskItem.Add(TaskName);
        }

        public void UpdateTask(TaskItem TaskName)
        {
            _context.TaskItem.Update(TaskName);
        }

        public void DeleteTask(TaskItem TaskName)
        {
            _context.TaskItem.Remove(TaskName);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}