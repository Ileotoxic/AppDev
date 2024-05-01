using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Customer")]

    public class JobListingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobListingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobListingModels
        [Area("Admin")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobListingModel.ToListAsync());
        }


        // GET: JobListingModels/Details/5
        [Area("Admin")]
        [Authorize("Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobListingModel = await _context.JobListingModel
                .FirstOrDefaultAsync(m => m.JobListingId == id);
            if (jobListingModel == null)
            {
                return NotFound();
            }

            return View(jobListingModel);
        }
        [Area("Admin")]
        [Authorize(Roles = "Admin,Customer")]
        // GET: JobListingModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobListingModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Admin")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Create([Bind("JobListingId,Title,Description,ApplicationDeadline,Location,ApplicationId,Image")] JobListingModel jobListingModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobListingModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobListingModel);
        }

        [Area("Admin")]
        [Authorize("Admin")]
        // GET: JobListingModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobListingModel = await _context.JobListingModel.FindAsync(id);
            if (jobListingModel == null)
            {
                return NotFound();
            }
            return View(jobListingModel);
        }

        // POST: JobListingModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Admin")]
        [Authorize("Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("JobListingId,Title,Description,ApplicationDeadline,Location,ApplicationId,Image")] JobListingModel jobListingModel)
        {
            if (id != jobListingModel.JobListingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobListingModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobListingModelExists(jobListingModel.JobListingId))
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
            return View(jobListingModel);
        }

        // GET: JobListingModels/Delete/5
        [Area("Admin")]
        [Authorize("Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobListingModel = await _context.JobListingModel
                .FirstOrDefaultAsync(m => m.JobListingId == id);
            if (jobListingModel == null)
            {
                return NotFound();
            }

            return View(jobListingModel);
        }

        // POST: JobListingModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Area("Admin")]
        [Authorize("Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobListingModel = await _context.JobListingModel.FindAsync(id);
            if (jobListingModel != null)
            {
                _context.JobListingModel.Remove(jobListingModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobListingModelExists(int id)
        {
            return _context.JobListingModel.Any(e => e.JobListingId == id);
        }
    }
}
