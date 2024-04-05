using BookShop.Data;
using BookShop.Models;
using BookShop.Models.ViewModels;
using BookShop.Repository;
using BookShop.Repository.IRepository;
using BookShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Areas.Admin.Controllers
{
    [Area(SD.Admin)]
    [Authorize(Roles = SD.Admin)]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Book> myList = _unitOfWork.BookRepository.GetAll("Category").ToList();
            return View(myList);
        }
        public IActionResult Create()
        {
            BookVM bookVM = new BookVM()
            {
                Categories = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Book = new Book()
            };
            return View(bookVM);
        }
        [HttpPost]
        public IActionResult Create(BookVM bookVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string bookPath = Path.Combine(wwwRootPath, @"img\Books");
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    using (var fileStream = new FileStream(Path.Combine(bookPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    bookVM.Book.ImageUrl = @"\img\Books\" + fileName;
                }
                _unitOfWork.BookRepository.Add(bookVM.Book);
                _unitOfWork.Save();
                TempData["success"] = "Book created succesfully";
                return RedirectToAction("Index");
            }
            bookVM.Categories = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            return View(bookVM);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            BookVM bookVM = new BookVM()
            {
                Categories = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Book = _unitOfWork.BookRepository.Get(c => c.Id == id)
            };
            return View(bookVM);
        }
        [HttpPost]
        public IActionResult Edit(BookVM bookVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string bookPath = Path.Combine(wwwRootPath, @"img\Books");
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    //Delete Image if it exists
                    if (!string.IsNullOrEmpty(bookVM.Book.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, bookVM.Book.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(bookPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    bookVM.Book.ImageUrl = @"\img\Books\" + fileName;
                }
                _unitOfWork.BookRepository.Update(bookVM.Book);
                _unitOfWork.Save();
                TempData["success"] = "Book updated succesfully";
                return RedirectToAction("Index");
            }
            bookVM.Categories = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            return View(bookVM);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Book? Book = _unitOfWork.BookRepository.Get(c => c.Id == id, "Category");
            if (Book == null)
            {
                return NotFound();
            }
            return View(Book);
        }
        [HttpPost]
        public IActionResult Delete(Book Book)
        {
            _unitOfWork.BookRepository.Delete(Book);
            _unitOfWork.Save();
            TempData["success"] = "Book deleted succesfully";
            return RedirectToAction("Index");

        }
    }
}
