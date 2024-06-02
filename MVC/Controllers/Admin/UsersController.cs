using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using DTO;
using MVC.Controllers.Util;
using MVC.Services.Interfaces;

namespace MVC.Controllers.Admin
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            IEnumerable<UserDTO>? users = await _userService.GetAllUsers();
            if (users == null)
            {
                ToastrUtil.ToastrError(this, "Unable to fetch users, please contact support");
                return Redirect("/");
            }
            return View(users);
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return idNotProvided();
            }

            var user = await _userService.GetUserById(id.Value);

            if (user == null)
            {
                return userNotFound();
            }

            return View(user);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,LastName,FirstName,Gender,Address,Email,IsDeleted")] UserDTO user)
        {
            if (ModelState.IsValid)
            {
                if (await _userService.CreateUser(user) == null)
                {
                    ToastrUtil.ToastrError(this, "Unable to create user");
                    return View(user);
                }
                // redirect to the new account page
                ToastrUtil.ToastrSuccess(this, "User successfully created");
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return idNotProvided();
            }

            var user = await _userService.GetUserById(id.Value);

            if (user == null)
            {
                return userNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Username,LastName,FirstName,Gender,Address,Email,IsDeleted")] UserDTO user)
        {
            if (id != user.UserId)
            {
                ToastrUtil.ToastrError(this, "An error has occured with the edit of users, please contact support");
                return View(user);
            }

            //remove the UserName from the model state
            if (ModelState.IsValid)
            {
                if (!await _userService.UpdateUser(user))
                {
                    ToastrUtil.ToastrError(this, "User update failed");
                    return RedirectToAction(nameof(Index));
                }
                ToastrUtil.ToastrSuccess(this, "User successfully updated");
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return idNotProvided();
            }

            var user = await _userService.GetUserById(id.Value);

            if (user == null)
            {
                return userNotFound();
            }

            return View(user);
        }


        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _userService.DeleteUser(id))
            {
                ToastrUtil.ToastrSuccess(this, "User successfully deleted");
                return RedirectToAction(nameof(Index));
            }
            ToastrUtil.ToastrError(this, "User deletion failed");
            return RedirectToAction(nameof(Delete), id);
        }

        private IActionResult idNotProvided()
        {
            ToastrUtil.ToastrError(this, "Id was not provided");
            return RedirectToAction(nameof(Index));
        }

        private IActionResult userNotFound()
        {
            ToastrUtil.ToastrError(this, "User not found");
            return RedirectToAction(nameof(Index));
        }
    }
}
