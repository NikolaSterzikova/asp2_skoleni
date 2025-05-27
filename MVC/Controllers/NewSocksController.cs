using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers
{
    public class NewSocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewSocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NewSocks
        public async Task<IActionResult> Index()
        {
            return View(await _context.NewSocks.ToListAsync());
        }

        // GET: NewSocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newSocks = await _context.NewSocks
                .FirstOrDefaultAsync(m => m.ID == id);
            if (newSocks == null)
            {
                return NotFound();
            }

            return View(newSocks);
        }

        // GET: NewSocks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewSocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Brand,Size,Price,OnStock")] NewSocks newSocks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newSocks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newSocks);
        }

        // GET: NewSocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newSocks = await _context.NewSocks.FindAsync(id);
            if (newSocks == null)
            {
                return NotFound();
            }
            return View(newSocks);
        }

        // POST: NewSocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Brand,Size,Price,OnStock")] NewSocks newSocks)
        {
            if (id != newSocks.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newSocks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewSocksExists(newSocks.ID))
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
            return View(newSocks);
        }

        // GET: NewSocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newSocks = await _context.NewSocks
                .FirstOrDefaultAsync(m => m.ID == id);
            if (newSocks == null)
            {
                return NotFound();
            }

            return View(newSocks);
        }

        // POST: NewSocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newSocks = await _context.NewSocks.FindAsync(id);
            if (newSocks != null)
            {
                _context.NewSocks.Remove(newSocks);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewSocksExists(int id)
        {
            return _context.NewSocks.Any(e => e.ID == id);
        }
    }
}
