using BookShop.Data;
using BookShop.Models;
using BookShop.Repository;
using BookShop.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository BookRepository;
        public BookController(IBookRepository context)
        {
            BookRepository = context;
        }

        public IActionResult Index()
        {
            List<Book> myList = BookRepository.GetAll().ToList();
            return View(myList);
        }
        public IActionResult Create() 
        { 
            return View();
        }
        [HttpPost]
		public IActionResult Create(Book Book)
		{
          
            if (ModelState.IsValid)
            {
				BookRepository.Add(Book);
				BookRepository.Save();
                TempData["success"] = "Book created succesfully";
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
			Book? Book= BookRepository.Get(c=>c.Id ==id);
            if (Book == null)
            {
                return NotFound();
            }
            return View(Book);
        }
		[HttpPost]
		public IActionResult Edit(Book Book)
		{
			if (ModelState.IsValid)
			{
				BookRepository.Update(Book);
				BookRepository.Save();
				TempData["success"] = "Book updated succesfully";
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
			Book? Book = BookRepository.Get(c => c.Id == id);
			if (Book == null)
			{
				return NotFound();
			}
			return View(Book);
		}
		[HttpPost]
		public IActionResult Delete(Book Book)
		{
				BookRepository.Delete(Book);
				BookRepository.Save();
				TempData["success"] = "Book deleted succesfully";
				return RedirectToAction("Index");

		}
	}
}
