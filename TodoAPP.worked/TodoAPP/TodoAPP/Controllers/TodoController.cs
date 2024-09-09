using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPP.Context;
using TodoAPP.Models;
using TodoAPP.ViewModels;

namespace TodoAPP.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {

        TodoContext context;
        public TodoController()
        {
            context = new TodoContext();
        }

        // R
        [AllowAnonymous]
        public IActionResult Index()
        {
          var todos = context.Todos.Include(t => t.Category).ToList();

            return View(todos);
        }

        public IActionResult NewTodo()
        {

            ViewData["Cate"] = context.Categories.ToList();

            return View();
        }


        public IActionResult SaveNewTodo(CreateTodo todo)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Cate"] = context.Categories.ToList();
                return View("NewTodo", todo);
            }



            var newTodo = new Todo()
            {
                Name = todo.Name,
                Description = todo.Description,
                CategoryId = todo.CategoryId,
                IsDone = false,
                CreatedAt = DateTime.Now,
            };

            context.Todos.Add(newTodo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // U
        public IActionResult UpdateTodo(int id)
        {
            var todo = context.Todos.FirstOrDefault(t => t.Id == id);
            ViewData["Cate"] = context.Categories.ToList();

            var model = new CreateTodo()
            {
                Id = id,
                Name = todo.Name,
                Description = todo.Description,
                CategoryId = todo.CategoryId,
            };
            return View("NewTodo", model);
        }

        public IActionResult SaveUpdateTodo(CreateTodo todo)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Cate"] = context.Categories.ToList();
                return View("NewTodo", todo);
            }

            var editedTodo = context.Todos.FirstOrDefault(t => t.Id == todo.Id);
            editedTodo.Name = todo.Name;
            editedTodo.Description = todo.Description;
            editedTodo.CategoryId = todo.CategoryId;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // D
        public IActionResult DeleteTodo(int id)
        {
            var todo = context.Todos.FirstOrDefault(t => t.Id == id);
            context.Todos.Remove(todo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ToggleTodo(int id)
        {
            var todo = context.Todos.FirstOrDefault(t => t.Id == id);
            todo.IsDone = !todo.IsDone;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
