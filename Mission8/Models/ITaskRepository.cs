using Mission8.Models;

namespace Mission8.Models
{
    public interface ITaskRepository
    {
        IQueryable<TaskItem> TaskItem { get; }
        IQueryable<Category> Categories { get; }

        void AddTask(TaskItem TaskName);
        void UpdateTask(TaskItem TaskName);
        void DeleteTask(TaskItem TaskName);
        void Save();
    }
}