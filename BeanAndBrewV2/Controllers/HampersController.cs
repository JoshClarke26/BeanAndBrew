using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeanAndBrewV2.Data;
using BeanAndBrewV2.Models;
using System.Dynamic;

namespace BeanAndBrewV2.Controllers
{
    public class HampersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HampersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hampers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Hamper.Include(h => h.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Hampers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hamper == null)
            {
                return NotFound();
            }

            var hamper = await _context.Hamper
                .Include(h => h.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hamper == null)
            {
                return NotFound();
            }

            return View(hamper);
        }

        // GET: Hampers/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "UserName");
            return View();
        }

        // POST: Hampers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,UserId")] Hamper hamper)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hamper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "UserName", hamper.UserId);
            return View(hamper);
        }

        // GET: Hampers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hamper == null)
            {
                return NotFound();
            }

            var hamper = await _context.Hamper.FindAsync(id);
            if (hamper == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", hamper.UserId);
            return View(hamper);
        }

        // POST: Hampers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,UserId")] Hamper hamper)
        {
            if (id != hamper.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hamper);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HamperExists(hamper.Id))
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", hamper.UserId);
            return View(hamper);
        }

        // GET: Hampers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hamper == null)
            {
                return NotFound();
            }

            var hamper = await _context.Hamper
                .Include(h => h.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hamper == null)
            {
                return NotFound();
            }

            return View(hamper);
        }

        // POST: Hampers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hamper == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Hamper'  is null.");
            }
            var hamper = await _context.Hamper.FindAsync(id);
            if (hamper != null)
            {
                _context.Hamper.Remove(hamper);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HamperExists(int id)
        {
          return (_context.Hamper?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Order()
        {
            dynamic myModels = new ExpandoObject();
            myModels.Hampers = await _context.Hamper.ToListAsync();
            myModels.Items = await _context.HamperItem.ToListAsync();
            myModels.Orders = await _context.HamperItemOrder.ToListAsync();

            return View(myModels);
        }

    }
}
