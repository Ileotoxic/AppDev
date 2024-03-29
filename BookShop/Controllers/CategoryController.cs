using BookShop.Data;
using BookShop.Models;
using BookShop.Repository;
using BookShop.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository CategoryRepository;
        public CategoryController(ICategoryRepository context)
        {
            CategoryRepository = context;
        }

        public IActionResult Index()
        {
            List<Category> myList = CategoryRepository.GetAll().ToList();
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
				CategoryRepository.Add(category);
				CategoryRepository.Save();
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
			Category? category= CategoryRepository.Get(c=>c.Id ==id);
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
				CategoryRepository.Update(category);
				CategoryRepository.Save();
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
			Category? category = CategoryRepository.Get(c => c.Id == id);
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}
		[HttpPost]
		public IActionResult Delete(Category category)
		{
				CategoryRepository.Delete(category);
				CategoryRepository.Save();
				TempData["success"] = "Category deleted succesfully";
				return RedirectToAction("Index");

		}
	}
}
