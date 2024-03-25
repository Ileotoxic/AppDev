using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Category> myList = _context.Categories.ToList();
            return View(myList);
        }
        public IActionResult Create() 
        { 
            return View();
        }
        [HttpPost]
		public IActionResult Create(Category category)
		{
            if (category.Name == category.Description)
            {
                ModelState.AddModelError("Description","Description must be different than Name");
            }
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["success"] = "Category created succesfully";
                return RedirectToAction("Index");
            }
			return View();
		}
        public IActionResult Edit(int? id)
        {
            if (id==null || id == 0)
            {
                return NotFound();
            }
            Category? category=_context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
		[HttpPost]
		public IActionResult Edit(Category category)
		{
			if (ModelState.IsValid)
			{
				_context.Categories.Update(category);
				_context.SaveChanges();
				TempData["success"] = "Category updated succesfully";
				return RedirectToAction("Index");
			}
			return View();
		}
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category? category = _context.Categories.Find(id);
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}
		[HttpPost]
		public IActionResult Delete(Category category)
		{
				_context.Categories.Remove(category);
				_context.SaveChanges();
				TempData["success"] = "Category deleted succesfully";
				return RedirectToAction("Index");

		}
	}
}
