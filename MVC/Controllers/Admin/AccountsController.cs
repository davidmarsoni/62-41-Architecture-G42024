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
using MVC.Models;

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
            if (id == null)
            {
                return NotFound();
            }

            var account = await _accountService.GetAccountById(id.Value);

            if (account == null)
            {
                return RedirectToAction("Index", "Error");
            }
            
            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            // fetch the available users
            IEnumerable<UserDTO> users = GetUsersWithoutAccount();
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
                if (await _accountService.CreateAccount(account) == null)
                {
                    return BadRequest();
                }
                // redirect to the new account page
                return RedirectToAction(nameof(Index));
            }

            // fetch the available users
            IEnumerable<UserDTO> users = GetUsersWithoutAccount();
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

            var account = await _accountService.GetAccountById(id.Value);

            if (account == null)
            {
                return BadRequest();
            }

            // fetch the available users
            IEnumerable<UserDTO> users = await _userService.GetUsersWithoutAccount();
            ViewData["Userselect"] = new SelectList(users, nameof(UserDTO.Id), nameof(UserDTO.Username));
            ViewData["UsersAvailable"] = users.Count() > 0;
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Balance,CreatedAt,UpdatedAt")] AccountDTO account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            //remove the UserName from the model state
            ModelState.Remove(nameof(UserDTO.Username));
            if (ModelState.IsValid)
            {
                if (await _accountService.UpdateAccount(account) == null)
                {
                       return BadRequest();
                }
                RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index));
            }

            // fetch the available users
            IEnumerable<UserDTO> users = await _userService.GetUsersWithoutAccount();
            ViewData["Userselect"] = new SelectList(users, nameof(UserDTO.Id), nameof(UserDTO.Username));
            ViewData["UsersAvailable"] = users.Count() > 0;
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var account = await _accountService.GetAccountById(id.Value);

            if (account == null)
            {
                return BadRequest();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _accountService.DeleteAccount(id))
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }

        private IEnumerable<UserDTO> GetUsersWithoutAccount()
        {
            return _userService.GetUsersWithoutAccount().Result;
        }

        private async Task<IEnumerable<UserDTO>> GetUsersWithoutAccountAsync(int userId) {
            // fetch the available users
            IEnumerable<UserDTO> users = await _userService.GetUsersWithoutAccount();
            // fetch the current user
            UserDTO userDTO = await _userService.GetUser(userId);
            // add the current user to the list of available users
            return users.Append(userDTO);
        }
    }
}
