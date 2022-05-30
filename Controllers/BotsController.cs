using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Quintrix_Web_App_Core_MVC.Data;
using Quintrix_Web_App_Core_MVC.Models;

namespace Quintrix_Web_App_Core_MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BotsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BotsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bots
        public async Task<IActionResult> Index()
        {
              return _context.Bots != null ? 
                          View(await _context.Bots.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Bots'  is null.");
        }

        // GET: Bots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bots == null)
            {
                return NotFound();
            }

            var bots = await _context.Bots
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bots == null)
            {
                return NotFound();
            }

            return View(bots);
        }

        // GET: Bots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BotId")] Bots bots)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bots);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bots);
        }

        // GET: Bots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bots == null)
            {
                return NotFound();
            }

            var bots = await _context.Bots.FindAsync(id);
            if (bots == null)
            {
                return NotFound();
            }
            return View(bots);
        }

        // POST: Bots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BotId")] Bots bots)
        {
            if (id != bots.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bots);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BotsExists(bots.Id))
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
            return View(bots);
        }

        // GET: Bots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bots == null)
            {
                return NotFound();
            }

            var bots = await _context.Bots
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bots == null)
            {
                return NotFound();
            }

            return View(bots);
        }

        // POST: Bots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bots == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bots'  is null.");
            }
            var bots = await _context.Bots.FindAsync(id);
            if (bots != null)
            {
                _context.Bots.Remove(bots);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BotsExists(int id)
        {
          return (_context.Bots?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
