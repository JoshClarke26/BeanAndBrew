using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeanAndBrewV2.Data;
using BeanAndBrewV2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BeanAndBrewV2.Controllers
{
    [Authorize]
    public class BakingLessonSlotsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BakingLessonSlotsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: BakingLessonSlots
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BakingLessonSlot.Include(b => b.BakingLesson).Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BakingLessonSlots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BakingLessonSlot == null)
            {
                return NotFound();
            }

            var bakingLessonSlot = await _context.BakingLessonSlot
                .Include(b => b.BakingLesson)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bakingLessonSlot == null)
            {
                return NotFound();
            }

            return View(bakingLessonSlot);
        }

        // GET: BakingLessonSlots/Create
        public IActionResult Create()
        {
            ViewData["BakingLessonId"] = new SelectList(_context.BakingLesson, "Id", "Product");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "UserName");
            return View();
        }

        // POST: BakingLessonSlots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tickets,BakingLessonId,UserId")] BakingLessonSlot bakingLessonSlot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bakingLessonSlot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BakingLessonId"] = new SelectList(_context.BakingLesson, "Id", "Product", bakingLessonSlot.BakingLessonId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "UserName", bakingLessonSlot.UserId);
            return View(bakingLessonSlot);
        }

        // GET: BakingLessonSlots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BakingLessonSlot == null)
            {
                return NotFound();
            }

            var bakingLessonSlot = await _context.BakingLessonSlot.FindAsync(id);
            if (bakingLessonSlot == null)
            {
                return NotFound();
            }
            ViewData["BakingLessonId"] = new SelectList(_context.BakingLesson, "Id", "Product", bakingLessonSlot.BakingLessonId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", bakingLessonSlot.UserId);
            return View(bakingLessonSlot);
        }

        // POST: BakingLessonSlots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tickets,BakingLessonId,UserId")] BakingLessonSlot bakingLessonSlot)
        {
            if (id != bakingLessonSlot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bakingLessonSlot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BakingLessonSlotExists(bakingLessonSlot.Id))
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
            ViewData["BakingLessonId"] = new SelectList(_context.BakingLesson, "Id", "Product", bakingLessonSlot.BakingLessonId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", bakingLessonSlot.UserId);
            return View(bakingLessonSlot);
        }

        // GET: BakingLessonSlots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BakingLessonSlot == null)
            {
                return NotFound();
            }

            var bakingLessonSlot = await _context.BakingLessonSlot
                .Include(b => b.BakingLesson)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bakingLessonSlot == null)
            {
                return NotFound();
            }

            return View(bakingLessonSlot);
        }

        // POST: BakingLessonSlots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BakingLessonSlot == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BakingLessonSlot'  is null.");
            }
            var bakingLessonSlot = await _context.BakingLessonSlot.FindAsync(id);
            if (bakingLessonSlot != null)
            {
                _context.BakingLessonSlot.Remove(bakingLessonSlot);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BakingLessonSlotExists(int id)
        {
          return (_context.BakingLessonSlot?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult Book()
        {
            ViewData["BakingLessonId"] = new SelectList(_context.BakingLesson, "Id", "Product");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book([Bind("Id,Tickets,BakingLessonId")] BakingLessonSlot bakingLessonSlot)
        {
            if (ModelState.IsValid)
            {
                bakingLessonSlot.User = await _userManager.GetUserAsync(User);
                _context.Add(bakingLessonSlot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BakingLessonId"] = new SelectList(_context.BakingLesson, "Id", "Product", bakingLessonSlot.BakingLessonId);
            return View(bakingLessonSlot);
        }

    }
}
