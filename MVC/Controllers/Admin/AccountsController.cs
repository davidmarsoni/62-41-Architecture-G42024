using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using MVC.Services.Interfaces;
using MVCProject.Controllers;
using MVC.Services;
using System.Collections;
using DTO;

namespace MVC.Controllers.Admin
{
    public class AccountsController : Controller
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AccountsController(ILogger<AccountsController> logger, IAccountService accountService, IUserService userService)
        {
            _logger = logger;
            _accountService = accountService;
            _userService = userService;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return View(await _accountService.GetAllAccounts());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            */
            return View(/*account*/);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            IEnumerable<UserDTO> users = _userService.GetUsersWithoutAccount().Result;
            ViewData["Userselect"] = new SelectList(users, nameof(UserDTO.Id), nameof(UserDTO.Username));
            ViewData["UsersAvailable"] = users.Count() > 0;
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Balance,CreatedAt,UpdatedAt")] AccountDTO account)
        {
            // remove the UserName from the model state
            ModelState.Remove(nameof(UserDTO.Username));
            if (ModelState.IsValid)
            {
                account = await _accountService.CreateAccount(account);
                // redirect to the new account page
                return RedirectToAction(nameof(Index));
            }

            // otherwise return to the create page
            IEnumerable<UserDTO> users = _userService.GetUsersWithoutAccount().Result;
            ViewData["Userselect"] = new SelectList(users, nameof(UserDTO.Id), nameof(UserDTO.Username));
            ViewData["UsersAvailable"] = users.Count() > 0;
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            /*
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", account.UserId);*/
            return View(/*account*/);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Balance,CreatedAt,UpdatedAt")] Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            /*if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", account.UserId);*/
            return View(/*account*/);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            /*
            var account = await _context.Accounts
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }*/

            return View(/*account*/);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }

            await _context.SaveChangesAsync();*/
            return RedirectToAction(/*nameof(Index)*/);
        }

        private bool AccountExists(int id)
        {
            return true /*_context.Accounts.Any(e => e.Id == id)*/;
        }
    }
}
