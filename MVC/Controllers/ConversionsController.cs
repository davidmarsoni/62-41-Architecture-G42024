using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;

namespace MVC.Controllers
{
    public class ConversionsController : Controller
    {
        private readonly PrintOMatic_Context _context;

        public ConversionsController(PrintOMatic_Context context)
        {
            _context = context;
        }

        // GET: Conversions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Conversions.ToListAsync());
        }

        // GET: Conversions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversion = await _context.Conversions
                .FirstOrDefaultAsync(m => m.ConversionId == id);
            if (conversion == null)
            {
                return NotFound();
            }

            return View(conversion);
        }

        // GET: Conversions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conversions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConversionId,Name,Value")] Conversion conversion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conversion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conversion);
        }

        // GET: Conversions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversion = await _context.Conversions.FindAsync(id);
            if (conversion == null)
            {
                return NotFound();
            }
            return View(conversion);
        }

        // POST: Conversions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConversionId,Name,Value")] Conversion conversion)
        {
            if (id != conversion.ConversionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conversion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConversionExists(conversion.ConversionId))
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
            return View(conversion);
        }

        // GET: Conversions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversion = await _context.Conversions
                .FirstOrDefaultAsync(m => m.ConversionId == id);
            if (conversion == null)
            {
                return NotFound();
            }

            return View(conversion);
        }

        // POST: Conversions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conversion = await _context.Conversions.FindAsync(id);
            if (conversion != null)
            {
                _context.Conversions.Remove(conversion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversionExists(int id)
        {
            return _context.Conversions.Any(e => e.ConversionId == id);
        }
    }
}
