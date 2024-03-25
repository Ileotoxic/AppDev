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
                return RedirectToAction("Index");
            }
			return View();
		}
	}
}
