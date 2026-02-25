using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission8.Models;

namespace Mission8.Controllers;

public class HomeController : Controller
{
    private ITaskRepository _repo;

    public HomeController(ITaskRepository temp)
    {
        _repo = temp;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var tasks = _repo.TaskItem
            .Where(x => x.Completed == false)
            .ToList();
        
        ViewBag.Quad2 = 
            _repo.Where(x => x.Completed == false).AndWhere(x => x.Quadrant == 2)
                .ToList();
        
        ViewBag.Quad3 = 
            _repo.Where(x => x.Completed == false).AndWhere(x => x.Quadrant == 3)
                .ToList();

        ViewBag.Quad4 = 
            _repo.Where(x => x.Completed == false).AndWhere(x => x.Quadrant == 4)
                .ToList();
        
        return View();
    }
    
    [HttpGet]
    public IActionResult Add_Task()
    {
        ViewBag.Categories = _repo.Categories.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult Add_Task(TaskItem item)
    {
        // use repository to add something to the database
        if (ModelState.IsValid)
        {
            _repo.AddTask(item);
            return RedirectToAction("Index");
        }
        
        // if there are errors return this view with the errors
        ViewBag.Categories = _repo.Categories.ToList();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit_Task(int id)
    {
        var tasktoEdit = _repo.TaskItem.Single(x => x.TaskItemId == id);
        ViewBag.Categories = _repo.Categories.ToList();
        return View("Add_Task", tasktoEdit);
    }

    [HttpPost]
    public IActionResult Edit_Task(TaskItem updatedTask)
    {
        _repo.UpdateTask(updatedTask);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete_Task(int id)
    {
        var TaskName= _repo.TaskItem.Single(x => x.TaskItemId == id);
        _repo.DeleteTask(TaskName);
    
        return RedirectToAction("Index");
    }
    
}