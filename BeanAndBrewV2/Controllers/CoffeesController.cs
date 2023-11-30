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
    public class CoffeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoffeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Coffees
        public async Task<IActionResult> Index()
        {
              return _context.Coffee != null ? 
                          View(await _context.Coffee.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Coffee'  is null.");
        }

        // GET: Coffees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Coffee == null)
            {
                return NotFound();
            }

            var coffee = await _context.Coffee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coffee == null)
            {
                return NotFound();
            }

            return View(coffee);
        }

        // GET: Coffees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coffees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Size")] Coffee coffee, IFormFile userFile)
        {

            string fileName = userFile.FileName;
            fileName = Path.GetFileName(fileName);
            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\coffees", fileName);
            var stream = new FileStream(uploadPath, FileMode.Create);
            await userFile.CopyToAsync(stream);
            if (ModelState.IsValid)
            {
                coffee.ImagePath = fileName;
                _context.Add(coffee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coffee);
        }



        // GET: Coffees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Coffee == null)
            {
                return NotFound();
            }

            var coffee = await _context.Coffee.FindAsync(id);
            if (coffee == null)
            {
                return NotFound();
            }
            return View(coffee);
        }

        // POST: Coffees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Size")] Coffee coffee)
        {
            if (id != coffee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coffee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoffeeExists(coffee.Id))
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
            return View(coffee);
        }

        // GET: Coffees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Coffee == null)
            {
                return NotFound();
            }

            var coffee = await _context.Coffee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coffee == null)
            {
                return NotFound();
            }

            return View(coffee);
        }

        // POST: Coffees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Coffee == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Coffee'  is null.");
            }
            var coffee = await _context.Coffee.FindAsync(id);
            if (coffee != null)
            {
                _context.Coffee.Remove(coffee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoffeeExists(int id)
        {
          return (_context.Coffee?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Browse()
        {
            return _context.Coffee != null ?
                        View(await _context.Coffee.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Coffee'  is null.");
        }

    }
}
