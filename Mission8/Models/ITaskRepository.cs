using System.Linq;

namespace Mission8.Models
{
    // Repository contract for task and category data; keeps controllers independent of EF/database details.
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