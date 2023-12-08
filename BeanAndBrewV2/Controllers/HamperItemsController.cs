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
    public class HamperItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HamperItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HamperItems
        public async Task<IActionResult> Index()
        {
              return _context.HamperItem != null ? 
                          View(await _context.HamperItem.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.HamperItem'  is null.");
        }

        // GET: HamperItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HamperItem == null)
            {
                return NotFound();
            }

            var hamperItem = await _context.HamperItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hamperItem == null)
            {
                return NotFound();
            }

            return View(hamperItem);
        }

        // GET: HamperItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HamperItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price")] HamperItem hamperItem, IFormFile imageFile)
        {
            string fileName = imageFile.FileName;
            fileName = Path.GetFileName(fileName);
            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\hamperItems", fileName);
            var stream = new FileStream(uploadPath, FileMode.Create);
            await imageFile.CopyToAsync(stream);
            if (ModelState.IsValid)
            {
                hamperItem.ImagePath = fileName;
                _context.Add(hamperItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hamperItem);
        }

        // GET: HamperItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HamperItem == null)
            {
                return NotFound();
            }

            var hamperItem = await _context.HamperItem.FindAsync(id);
            if (hamperItem == null)
            {
                return NotFound();
            }
            return View(hamperItem);
        }

        // POST: HamperItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,ImagePath")] HamperItem hamperItem)
        {
            if (id != hamperItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hamperItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HamperItemExists(hamperItem.Id))
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
            return View(hamperItem);
        }

        // GET: HamperItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HamperItem == null)
            {
                return NotFound();
            }

            var hamperItem = await _context.HamperItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hamperItem == null)
            {
                return NotFound();
            }

            return View(hamperItem);
        }

        // POST: HamperItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HamperItem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HamperItem'  is null.");
            }
            var hamperItem = await _context.HamperItem.FindAsync(id);
            if (hamperItem != null)
            {
                _context.HamperItem.Remove(hamperItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HamperItemExists(int id)
        {
          return (_context.HamperItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
