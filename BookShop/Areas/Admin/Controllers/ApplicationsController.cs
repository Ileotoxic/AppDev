using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Areas.Admin.Controllers
{
   
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicationModels
        [Area("Admin")]
        [Authorize("Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationModel.ToListAsync());
        }

        // GET: ApplicationModels/Details/5
        [Area("Admin")]
        [Authorize("Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationModel = await _context.ApplicationModel
                .FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (applicationModel == null)
            {
                return NotFound();
            }

            return View(applicationModel);
        }

        // GET: ApplicationModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationId,JobListingId,Message,Description,DisplayOrder")] ApplicationModel applicationModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicationModel);
        }

        // GET: ApplicationModels/Edit/5
        [Area("Admin")]
        [Authorize("Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationModel = await _context.ApplicationModel.FindAsync(id);
            if (applicationModel == null)
            {
                return NotFound();
            }
            return View(applicationModel);
        }

        // POST: ApplicationModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Area("Admin")]
        [Authorize("Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationId,JobListingId,Message,Description,DisplayOrder")] ApplicationModel applicationModel)
        {
            if (id != applicationModel.ApplicationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationModelExists(applicationModel.ApplicationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(applicationModel);
        }

        // GET: ApplicationModels/Delete/5
        [Area("Admin")]
        [Authorize("Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationModel = await _context.ApplicationModel
                .FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (applicationModel == null)
            {
                return NotFound();
            }

            return View(applicationModel);
        }

        // POST: ApplicationModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Area("Admin")]
        [Authorize("Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationModel = await _context.ApplicationModel.FindAsync(id);
            if (applicationModel != null)
            {
                _context.ApplicationModel.Remove(applicationModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationModelExists(int id)
        {
            return _context.ApplicationModel.Any(e => e.ApplicationId == id);
        }
    }
}
