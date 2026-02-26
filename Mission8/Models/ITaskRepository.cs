using System.Linq;

namespace Mission8.Models
{
    public interface ITaskRepository
    {
        IQueryable<TaskItem> TaskItem { get; }
        IQueryable<Category> Categories { get; }

        void AddTask(TaskItem task);
        void UpdateTask(TaskItem task);
        void DeleteTask(TaskItem task);
        void Save();
    }
}