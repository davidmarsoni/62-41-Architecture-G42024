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
    public class User_GroupController : Controller
    {
        private readonly PrintOMatic_Context _context;

        public User_GroupController(PrintOMatic_Context context)
        {
            _context = context;
        }

        // GET: User_Group
        public async Task<IActionResult> Index()
        {
            var printOMatic_Context = _context.User_Groups.Include(u => u.Group).Include(u => u.User);
            return View(await printOMatic_Context.ToListAsync());
        }

        // GET: User_Group/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user_Group = await _context.User_Groups
                .Include(u => u.Group)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (user_Group == null)
            {
                return NotFound();
            }

            return View(user_Group);
        }

        // GET: User_Group/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: User_Group/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,GroupId")] User_Group user_Group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user_Group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "Name", user_Group.GroupId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", user_Group.UserId);
            return View(user_Group);
        }

        // GET: User_Group/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user_Group = await _context.User_Groups.FindAsync(id);
            if (user_Group == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "Name", user_Group.GroupId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", user_Group.UserId);
            return View(user_Group);
        }

        // POST: User_Group/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,GroupId")] User_Group user_Group)
        {
            if (id != user_Group.GroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user_Group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!User_GroupExists(user_Group.GroupId))
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
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "Name", user_Group.GroupId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", user_Group.UserId);
            return View(user_Group);
        }

        // GET: User_Group/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user_Group = await _context.User_Groups
                .Include(u => u.Group)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (user_Group == null)
            {
                return NotFound();
            }

            return View(user_Group);
        }

        // POST: User_Group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user_Group = await _context.User_Groups.FindAsync(id);
            if (user_Group != null)
            {
                _context.User_Groups.Remove(user_Group);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool User_GroupExists(int id)
        {
            return _context.User_Groups.Any(e => e.GroupId == id);
        }
    }
}
