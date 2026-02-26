using Microsoft.AspNetCore.Mvc;
using Mission8.Models;

namespace Mission8.Controllers;

public class HomeController : Controller
{
    private readonly ITaskRepository _repo;

    public HomeController(ITaskRepository temp)
    {
        _repo = temp;
    }

    [HttpGet]
    public IActionResult Index()
    {
        // Base query: only incomplete tasks
        var incomplete = _repo.TaskItem.Where(x => !x.Completed);

        // Pass all incomplete tasks to the view (so it can render them)
        var tasks = incomplete.ToList();

        // Quadrant-specific lists (still incomplete)
        ViewBag.Quad1 = incomplete.Where(x => x.Quadrant == 1).ToList();
        ViewBag.Quad2 = incomplete.Where(x => x.Quadrant == 2).ToList();
        ViewBag.Quad3 = incomplete.Where(x => x.Quadrant == 3).ToList();
        ViewBag.Quad4 = incomplete.Where(x => x.Quadrant == 4).ToList();

        return View(tasks);
    }

    [HttpGet]
    public IActionResult Add_Task()
    {
        ViewBag.Categories = _repo.Categories.ToList();
        return View(new TaskItem());
    }

    [HttpPost]
    public IActionResult Add_Task(TaskItem item)
    {
        if (ModelState.IsValid)
        {
            _repo.AddTask(item);
            _repo.Save();
            return RedirectToAction("Index");
        }

        // If there are errors, return this view with the errors
        ViewBag.Categories = _repo.Categories.ToList();
        return View(item);
    }

    [HttpGet]
    public IActionResult Edit_Task(int id)
    {
        var taskToEdit = _repo.TaskItem.Single(x => x.TaskItemId == id);
        ViewBag.Categories = _repo.Categories.ToList();
        return View("Add_Task", taskToEdit);
    }

    [HttpPost]
    public IActionResult Edit_Task(TaskItem updatedTask)
    {
        if (ModelState.IsValid)
        {
            _repo.UpdateTask(updatedTask);
            _repo.Save();
            return RedirectToAction("Index");
        }

        ViewBag.Categories = _repo.Categories.ToList();
        return View("Add_Task", updatedTask);
    }

    [HttpGet]
    public IActionResult Delete_Task(int id)
    {
        var taskToDelete = _repo.TaskItem.Single(x => x.TaskItemId == id);
        _repo.DeleteTask(taskToDelete);
        _repo.Save();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult MarkComplete(int id)
    {
        var task = _repo.TaskItem.Single(x => x.TaskItemId == id);
        task.Completed = true;
        _repo.UpdateTask(task);
        _repo.Save();
        return RedirectToAction("Index");
    }
}