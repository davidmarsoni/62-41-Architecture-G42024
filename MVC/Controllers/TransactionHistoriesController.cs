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
    public class TransactionHistoriesController : Controller
    {
        private readonly PrintOMatic_Context _context;


        public TransactionHistoriesController(PrintOMatic_Context context)
        {
            _context = context;
        }

        // GET: TransactionHistories
        public async Task<IActionResult> Index()
        {
            var printOMatic_Context = _context.TransactionHistory.Include(t => t.Account);
            return View(await printOMatic_Context.ToListAsync());
        }

        // GET: TransactionHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionHistory = await _context.TransactionHistory
                .Include(t => t.Account)
                .FirstOrDefaultAsync(m => m.TransactionHistoryId == id);
            if (transactionHistory == null)
            {
                return NotFound();
            }

            return View(transactionHistory);
        }

        // GET: TransactionHistories/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id");
            return View();
        }

        // POST: TransactionHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionHistoryId,AccountId,DateTime,Src,TransactionType,Amount,ConversionName,ConversionValue")] TransactionHistory transactionHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transactionHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", transactionHistory.AccountId);
            ViewBag.Src = Enum.GetValues(typeof(DAL.Classes.Src))
            .Cast<DAL.Classes.Src>()
            .Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            });

            ViewBag.TransactionType = Enum.GetValues(typeof(DAL.Classes.TransactionType))
            .Cast<DAL.Classes.TransactionType>()
            .Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            });

            return View(transactionHistory);
        }

        // GET: TransactionHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionHistory = await _context.TransactionHistory.FindAsync(id);
            if (transactionHistory == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", transactionHistory.AccountId);
            ViewBag.Src = Enum.GetValues(typeof(DAL.Classes.Src))
            .Cast<DAL.Classes.Src>()
            .Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            });

            ViewBag.TransactionType = Enum.GetValues(typeof(DAL.Classes.TransactionType))
            .Cast<DAL.Classes.TransactionType>()
            .Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            });
            return View(transactionHistory);
        }

        // POST: TransactionHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionHistoryId,AccountId,DateTime,Src,TransactionType,Amount,ConversionName,ConversionValue")] TransactionHistory transactionHistory)
        {
            if (id != transactionHistory.TransactionHistoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transactionHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionHistoryExists(transactionHistory.TransactionHistoryId))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", transactionHistory.AccountId);
            return View(transactionHistory);
        }

        // GET: TransactionHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionHistory = await _context.TransactionHistory
                .Include(t => t.Account)
                .FirstOrDefaultAsync(m => m.TransactionHistoryId == id);
            if (transactionHistory == null)
            {
                return NotFound();
            }

            return View(transactionHistory);
        }

        // POST: TransactionHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionHistory = await _context.TransactionHistory.FindAsync(id);
            if (transactionHistory != null)
            {
                _context.TransactionHistory.Remove(transactionHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionHistoryExists(int id)
        {
            return _context.TransactionHistory.Any(e => e.TransactionHistoryId == id);
        }
    }
}
