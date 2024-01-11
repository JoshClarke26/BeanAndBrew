using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeanAndBrewV2.Data;
using BeanAndBrewV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BeanAndBrewV2.Controllers
{
    public class HamperItemOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HamperItemOrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: HamperItemOrders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HamperItemOrder.Include(h => h.Hamper).Include(h => h.HamperItem);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HamperItemOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HamperItemOrder == null)
            {
                return NotFound();
            }

            var hamperItemOrder = await _context.HamperItemOrder
                .Include(h => h.Hamper)
                .Include(h => h.HamperItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hamperItemOrder == null)
            {
                return NotFound();
            }

            return View(hamperItemOrder);
        }

        // GET: HamperItemOrders/Create
        public IActionResult Create()
        {
            ViewData["HamperId"] = new SelectList(_context.Hamper, "Id", "UserId");
            ViewData["HamperItemId"] = new SelectList(_context.HamperItem, "Id", "Name");
            return View();
        }

        // POST: HamperItemOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,HamperItemId,HamperId")] HamperItemOrder hamperItemOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hamperItemOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HamperId"] = new SelectList(_context.Hamper, "Id", "UserId", hamperItemOrder.HamperId);
            ViewData["HamperItemId"] = new SelectList(_context.HamperItem, "Id", "Name", hamperItemOrder.HamperItemId);
            return View(hamperItemOrder);
        }

        // GET: HamperItemOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HamperItemOrder == null)
            {
                return NotFound();
            }

            var hamperItemOrder = await _context.HamperItemOrder.FindAsync(id);
            if (hamperItemOrder == null)
            {
                return NotFound();
            }
            ViewData["HamperId"] = new SelectList(_context.Hamper, "Id", "UserId", hamperItemOrder.HamperId);
            ViewData["HamperItemId"] = new SelectList(_context.HamperItem, "Id", "Name", hamperItemOrder.HamperItemId);
            return View(hamperItemOrder);
        }

        // POST: HamperItemOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,HamperItemId,HamperId")] HamperItemOrder hamperItemOrder)
        {
            if (id != hamperItemOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hamperItemOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HamperItemOrderExists(hamperItemOrder.Id))
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
            ViewData["HamperId"] = new SelectList(_context.Hamper, "Id", "UserId", hamperItemOrder.HamperId);
            ViewData["HamperItemId"] = new SelectList(_context.HamperItem, "Id", "Name", hamperItemOrder.HamperItemId);
            return View(hamperItemOrder);
        }

        // GET: HamperItemOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HamperItemOrder == null)
            {
                return NotFound();
            }

            var hamperItemOrder = await _context.HamperItemOrder
                .Include(h => h.Hamper)
                .Include(h => h.HamperItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hamperItemOrder == null)
            {
                return NotFound();
            }

            return View(hamperItemOrder);
        }

        // POST: HamperItemOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HamperItemOrder == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HamperItemOrder'  is null.");
            }
            var hamperItemOrder = await _context.HamperItemOrder.FindAsync(id);
            if (hamperItemOrder != null)
            {
                _context.HamperItemOrder.Remove(hamperItemOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HamperItemOrderExists(int id)
        {
          return (_context.HamperItemOrder?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [Authorize]
        [Route("order/hamper/item/{itemAmount}/{itemID}")]
        public async Task<IActionResult> Add(int itemAmount, int itemID)
        {
            Hamper? currentHamper = null;
            foreach (var hamper in _context.Hamper)
            {
                if (hamper.UserId == _userManager.GetUserAsync(User).Result!.Id && hamper.isActive)
                {
                    currentHamper = hamper;
                }
            }
            if (currentHamper == null)
            {
                currentHamper = new Hamper();
                currentHamper.User = await _userManager.GetUserAsync(User);
                currentHamper.UserId = _userManager.GetUserAsync(User).Result!.Id;
                currentHamper.isActive = true;
                _context.Add(currentHamper);
                await _context.SaveChangesAsync();
            }

            HamperItemOrder itemOrder = new HamperItemOrder();
            itemOrder.Amount = itemAmount;
            itemOrder.HamperItemId = itemID;
            itemOrder.HamperItem = _context.HamperItem.Find(itemID);
            itemOrder.HamperId = currentHamper.Id;
            itemOrder.Hamper = currentHamper;
            _context.Add(itemOrder);
            await _context.SaveChangesAsync();

            return Redirect("/order/hamper/" + currentHamper.Id); 
        }

    }
}
