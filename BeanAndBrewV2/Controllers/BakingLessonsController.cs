using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeanAndBrewV2.Data;
using BeanAndBrewV2.Models;

namespace BeanAndBrewV2.Controllers
{
    public class BakingLessonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BakingLessonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BakingLessons
        public async Task<IActionResult> Index()
        {
              return _context.BakingLesson != null ? 
                          View(await _context.BakingLesson.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.BakingLesson'  is null.");
        }

        // GET: BakingLessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BakingLesson == null)
            {
                return NotFound();
            }

            var bakingLesson = await _context.BakingLesson
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bakingLesson == null)
            {
                return NotFound();
            }

            return View(bakingLesson);
        }

        // GET: BakingLessons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BakingLessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartTime,EndTime,Product,Description,Price")] BakingLesson bakingLesson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bakingLesson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bakingLesson);
        }

        // GET: BakingLessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BakingLesson == null)
            {
                return NotFound();
            }

            var bakingLesson = await _context.BakingLesson.FindAsync(id);
            if (bakingLesson == null)
            {
                return NotFound();
            }
            return View(bakingLesson);
        }

        // POST: BakingLessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,EndTime,Product,Description,Price")] BakingLesson bakingLesson)
        {
            if (id != bakingLesson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bakingLesson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BakingLessonExists(bakingLesson.Id))
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
            return View(bakingLesson);
        }

        // GET: BakingLessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BakingLesson == null)
            {
                return NotFound();
            }

            var bakingLesson = await _context.BakingLesson
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bakingLesson == null)
            {
                return NotFound();
            }

            return View(bakingLesson);
        }

        // POST: BakingLessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BakingLesson == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BakingLesson'  is null.");
            }
            var bakingLesson = await _context.BakingLesson.FindAsync(id);
            if (bakingLesson != null)
            {
                _context.BakingLesson.Remove(bakingLesson);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BakingLessonExists(int id)
        {
          return (_context.BakingLesson?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
