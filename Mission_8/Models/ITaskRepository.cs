using Mission08.Models;

namespace Mission08.Models
{
    public interface ITaskRepository
    {
        IQueryable<TaskItem> Tasks { get; }
        IQueryable<Category> Categories { get; }

        void AddTask(TaskItem task);
        void UpdateTask(TaskItem task);
        void DeleteTask(TaskItem task);
        void Save();
    }
}