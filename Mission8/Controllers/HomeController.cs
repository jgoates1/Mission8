using Microsoft.AspNetCore.Mvc;
using Mission8.Models;

namespace Mission8.Controllers;

// Handles the quadrant matrix view, adding/editing tasks, and delete/mark-complete actions.
public class HomeController : Controller
{
    private readonly ITaskRepository _repo;

    public HomeController(ITaskRepository temp)
    {
        _repo = temp;
    }

    // Displays the Covey matrix: only uncompleted tasks, grouped by quadrant (1-4).
    [HttpGet]
    public IActionResult Index()
    {
        var incomplete = _repo.TaskItem.Where(x => !x.Completed);
        var tasks = incomplete.ToList();

        ViewBag.Quad1 = incomplete.Where(x => x.Quadrant == 1).ToList();
        ViewBag.Quad2 = incomplete.Where(x => x.Quadrant == 2).ToList();
        ViewBag.Quad3 = incomplete.Where(x => x.Quadrant == 3).ToList();
        ViewBag.Quad4 = incomplete.Where(x => x.Quadrant == 4).ToList();

        return View(tasks);
    }

    // Serves the add/edit form; categories come from the repo for the dropdown.
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