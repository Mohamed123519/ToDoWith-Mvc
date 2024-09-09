using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoAPP.Context;
using TodoAPP.Models;
using TodoAPP.ViewModels;

namespace TodoAPP.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class CategoryController : Controller
    {
        TodoContext context;
        public CategoryController()
        {
            context = new TodoContext();
        }

        // R
        public IActionResult Index()
        {
            var categories = context.Categories.ToList();
            return View(categories);
        }


        // C
        public IActionResult NewCategory()
        {
            var model = new CreateCategory(); // Initialize a new instance of CreateCategory
            return View(model);
        }
        public IActionResult SaveNewCategory(CreateCategory category)
        {
            if (ModelState.IsValid)
            {
                var newCate = new Category()
                {
                    Name = category.Name,
                };
                context.Categories.Add(newCate);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Ensure the category is initialized if null
            if (category == null)
            {
                category = new CreateCategory();
            }

            return View("NewCategory", category);
        }



        //public IActionResult SaveNewCategory(CreateCategory category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var newCate = new Category()
        //        {
        //            Name = category.Name,
        //        };
        //        context.Categories.Add(newCate);
        //        context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View("NewCategory", category);
        //}


        // U
        public IActionResult UpdateCategory(int id)
        {
            var category = context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return View("Invalid", new InvalidOperation() { Message = $"Category With Id {id} Not Found" });
            }


            var model = new CreateCategory()
            {
                Name = category.Name,
                Id = id
            };

            return View("NewCategory", model);
        }

        public IActionResult SaveUpdateCategory(CreateCategory category)
        {

            if (!ModelState.IsValid)
            {
                return View("NewCategory", category);
            }
            var cate = context.Categories.FirstOrDefault(c => c.Id == category.Id);
            cate.Name = category.Name;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // D
        public ActionResult DeleteCategory(int id)
        {
            var cate = context.Categories.FirstOrDefault(c => c.Id == id);
            context.Remove(cate);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
