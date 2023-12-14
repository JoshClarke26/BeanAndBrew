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
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace BeanAndBrewV2.Controllers
{
    [Authorize]
    public class CoffeeOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CoffeeOrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CoffeeOrders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CoffeeOrder.Include(c => c.Coffee).Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CoffeeOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CoffeeOrder == null)
            {
                return NotFound();
            }

            var coffeeOrder = await _context.CoffeeOrder
                .Include(c => c.Coffee)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coffeeOrder == null)
            {
                return NotFound();
            }

            return View(coffeeOrder);
        }

        // GET: CoffeeOrders/Create
        public IActionResult Create()
        {
            ViewData["CoffeeId"] = new SelectList(_context.Coffee, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: CoffeeOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,CoffeeId,UserId")] CoffeeOrder coffeeOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coffeeOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoffeeId"] = new SelectList(_context.Coffee, "Id", "Name", coffeeOrder.CoffeeId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", coffeeOrder.UserId);
            return View(coffeeOrder);
        }

        // GET: CoffeeOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CoffeeOrder == null)
            {
                return NotFound();
            }

            var coffeeOrder = await _context.CoffeeOrder.FindAsync(id);
            if (coffeeOrder == null)
            {
                return NotFound();
            }
            ViewData["CoffeeId"] = new SelectList(_context.Coffee, "Id", "Name", coffeeOrder.CoffeeId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", coffeeOrder.UserId);
            return View(coffeeOrder);
        }

        // POST: CoffeeOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,CoffeeId,UserId")] CoffeeOrder coffeeOrder)
        {
            if (id != coffeeOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coffeeOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoffeeOrderExists(coffeeOrder.Id))
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
            ViewData["CoffeeId"] = new SelectList(_context.Coffee, "Id", "Name", coffeeOrder.CoffeeId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", coffeeOrder.UserId);
            return View(coffeeOrder);
        }

        // GET: CoffeeOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CoffeeOrder == null)
            {
                return NotFound();
            }

            var coffeeOrder = await _context.CoffeeOrder
                .Include(c => c.Coffee)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coffeeOrder == null)
            {
                return NotFound();
            }

            return View(coffeeOrder);
        }

        // POST: CoffeeOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CoffeeOrder == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CoffeeOrder'  is null.");
            }
            var coffeeOrder = await _context.CoffeeOrder.FindAsync(id);
            if (coffeeOrder != null)
            {
                _context.CoffeeOrder.Remove(coffeeOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoffeeOrderExists(int id)
        {
          return (_context.CoffeeOrder?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [Route("/order/coffee/{CoffeeId}")]
        public async Task<IActionResult> Order(int CoffeeId)
        {
            CoffeeOrder order = new CoffeeOrder();
            order.Amount = 1;
            order.Coffee = _context.Coffee.Find(CoffeeId);
            order.CoffeeId = CoffeeId;
            order.User = await _userManager.GetUserAsync(User);
            order.UserId = _userManager.GetUserAsync(User).Result!.Id;
            _context.Add(order);
            await _context.SaveChangesAsync();
            return Redirect("/order/coffee/confirmation/" + order.Id);
        }

        [Route("/order/coffee/confirmation/{id?}")]
        public async Task<IActionResult> Confirmation(int? id)
        {
            if (id == null || _context.CoffeeOrder == null)
            {
                return NotFound();
            }

            var coffeeOrder = await _context.CoffeeOrder
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            ViewBag.Coffee = _context.Coffee.Find(_context.CoffeeOrder.Find(id)!.CoffeeId)!.Name;
            ViewBag.Price = _context.Coffee.Find(_context.CoffeeOrder.Find(id)!.CoffeeId)!.Price.ToString("C", CultureInfo.CurrentCulture);
            if (coffeeOrder == null)
            {
                return NotFound();
            }

            return View(coffeeOrder);
        }
    }
}
