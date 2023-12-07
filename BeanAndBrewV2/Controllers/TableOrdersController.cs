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
using System.Security.Claims;
using System.Security.Principal;

namespace BeanAndBrewV2.Controllers
{
    [Authorize]
    public class TableOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TableOrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TableOrders
        public async Task<IActionResult> Index()
        {

            string? userId = _userManager.GetUserId(HttpContext.User);
            int userPermission = _userManager.GetUserAsync(HttpContext.User).Result!.StaffPermission;
            if (userPermission == 0)
            {
                return Redirect("~/");
            }
            var applicationDbContext = _context.TableOrder.Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TableOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TableOrder == null)
            {
                return NotFound();
            }

            var tableOrder = await _context.TableOrder
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tableOrder == null)
            {
                return NotFound();
            }

            return View(tableOrder);
        }

        // GET: TableOrders/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: TableOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookingTime,NoOfPeople,Location,UserId")] TableOrder tableOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tableOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", tableOrder.UserId);
            return View(tableOrder);
        }

        // GET: TableOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TableOrder == null)
            {
                return NotFound();
            }

            var tableOrder = await _context.TableOrder.FindAsync(id);
            if (tableOrder == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", tableOrder.UserId);
            return View(tableOrder);
        }

        // POST: TableOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookingTime,NoOfPeople,Location,UserId")] TableOrder tableOrder)
        {
            if (id != tableOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tableOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TableOrderExists(tableOrder.Id))
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", tableOrder.UserId);
            return View(tableOrder);
        }

        // GET: TableOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TableOrder == null)
            {
                return NotFound();
            }

            var tableOrder = await _context.TableOrder
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tableOrder == null)
            {
                return NotFound();
            }

            return View(tableOrder);
        }

        // POST: TableOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TableOrder == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TableOrder'  is null.");
            }
            var tableOrder = await _context.TableOrder.FindAsync(id);
            if (tableOrder != null)
            {
                _context.TableOrder.Remove(tableOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TableOrderExists(int id)
        {
          return (_context.TableOrder?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult Book()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book([Bind("Id,BookingTime,NoOfPeople,Location")] TableOrder tableOrder)
        {
            if (ModelState.IsValid)
            {
                tableOrder.User = await _userManager.GetUserAsync(User);
                _context.Add(tableOrder);
                await _context.SaveChangesAsync();
                return Redirect("~/TableOrders/Confirmation/" + tableOrder.Id);
            }
            return View(tableOrder);
        }

        public async Task<IActionResult> Confirmation(int? id)
        {
            if (id == null || _context.TableOrder == null)
            {
                return NotFound();
            }

            var tableOrder = await _context.TableOrder
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tableOrder == null)
            {
                return NotFound();
            }

            return View(tableOrder);
        }

    }
}
